using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DataLayer;
using System.Text;
using Hangfire.Dashboard;

namespace DataUtilityCore
{
    public class SecurityManager
    {
        public const int ISaltlen = 8;

        public static object[] SpidSingleSignOn(string StreemCodiceFiscale)
        {
            object[] outVal = new object[3];
            outVal[0] = false;  //Utente Esiste true / false
            outVal[1] = null;  //Username
            outVal[2] = null; //Password
                        
            using (var ctx = new CriterDataModel())
            {
                var user = (from a in ctx.V_COM_Utenti
                            where (a.CodiceFiscale == StreemCodiceFiscale
                            && a.fSpid == true
                            && a.fattivoUser == true
                            && ((a.IDRuolo == 2) || (a.IDRuolo == 5) || (a.IDRuolo == 13)))
                            select new
                            {
                                iDUtente = a.IDUtente,
                                username = a.Username,
                                password = a.Password
                            });

                if (user.Count() > 0)
                {
                    outVal[0] = true; //Autenticato true / false
                    outVal[1] = user.FirstOrDefault().username;
                    outVal[2] = user.FirstOrDefault().password;
                }
            }

            return outVal;
        }

        #region Current Users: lista degli utenti correnti e controllo del doppio accesso

        //Lista degli utenti correnti
        public static List<CriterUser> CurrentUsers = new List<CriterUser>();

        // aggiunge l'utente corrente alla lista (o lo rinfresca se c'è già a pari userName e IP)
        public static void RegisterCurrentUser()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string ip = HttpContext.Current.Request.UserHostAddress;

            var ce = CurrentUsers.Where(u => u.UserName == userName && u.IP == ip).FirstOrDefault();

            if (null != ce)
            {
                ce.LastPresence = DateTime.Now;
            }
            else
            {
                CriterUser newUser = new CriterUser()
                {
                    UserName = userName,
                    IP = ip,
                    LastPresence = DateTime.Now
                };
                CurrentUsers.Add(newUser);
            }
        }

        // verifica l'esistenza di un utente con diverso IP
        public static bool CheckDoubleConnection(string userName, out string message)
        {
            message = null;

            // string userName = HttpContext.Current.User.Identity.Name;
            string ip = HttpContext.Current.Request.UserHostAddress;

            var query = CurrentUsers.Where(u=> u.UserName == userName && u.IP != ip).ToList();
            if (query.Any())
            {
                // comunque lo registro ? no.
                message = String.Format("ATTENZIONE: E' presente un'altra sessione attiva da {0} minuti con queste credenziali proveniente da un'indirizzo diverso: {1}", DateTime.Now.Subtract(query[0].LastPresence).Minutes, query[0].IP);
                return true;
            }
            return false;
        }

        // toglie l'utente corrente dalla lista (in occasione del Logout)
        public static void UnRegisterCurrentUser()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string ip = HttpContext.Current.Request.UserHostAddress;

            var query = (from u in CurrentUsers where u.UserName == userName && u.IP == ip select u).ToList();
            if (query.Count > 0)
            {
                CurrentUsers.Remove(query[0]);
                Logger.LogIt(TipoEvento.Logout);
            }
        }

        // rimuove gli utenti che non hanno dato presenza di sè nell'arco del TimeOut previsto dalla FormAuthentication
        public static void RefreshCurrentUsers()
        {
            TimeSpan maxDurata = FormsAuthentication.Timeout;

            List<CriterUser> toDelete = new List<CriterUser>();
            foreach (CriterUser user in CurrentUsers)
            {
                if (DateTime.Now.Subtract(user.LastPresence) > maxDurata)
                {
                    toDelete.Add(user);
                }
            }

            if (toDelete.Any())
            {
                foreach (CriterUser user in toDelete)
                {
                    CurrentUsers.Remove(user);
                    // registro il logout dell'utente
                    Logger.LogUser(TipoEvento.Logout, string.Empty, user.UserName, user.IP);
                }
            }
        }

        #endregion

        public static bool CheckUsername(string username, int? iDUtente)
        {
            bool checkUsername = false;

            if (!String.IsNullOrEmpty(username))
            {
                //var db = DataLayer.Common.ApplicationContext.Current.Context;
                using (var ctx = new CriterDataModel())
                {
                    var user = (from a in ctx.COM_Utenti
                                where (a.Username == username)
                                select new
                                {
                                    IDUtente = a.IDUtente
                                });

                    if (iDUtente != null)
                    {
                        user = user.Where(a => a.IDUtente != iDUtente);
                    }

                    if (user.Count() > 0)
                    {
                        checkUsername = true;
                    }
                }
            }

            return checkUsername;
        }

        #region Password Policy

        public enum LoginStatus
        {
            LoginOK,
            LoginKO,
            LoginFailed,
            LoginExpired,
            LoginInactive,
            LoginLocked
        }

        public enum ChangePwdStatus
        {
            ChangeOk,		// ok, cambio
            ChangeKo,		// errore
            EmptyPwd,		// uno dei campi newPwd o newConfirmPwd è vuoto
            DifferentPwd,	// newPwd e newConfirmPwd sono diversi
            ShortPwd,		// la nuova pwd è troppo breve
            SamePwd			// newPwd è uguale a oldPwd
        }

        public static ChangePwdStatus ChangePassword(string newPwd, string newConfirmPwd, string minChar, string username)
        {
            int minCharNumber = Convert.ToInt16(minChar);

            ChangePwdStatus status = ChangePwdStatus.ChangeKo;

            //controllo che abbiano inserito entrambi i campi
            if (newPwd != "" && newConfirmPwd != "")
            {
                #region EmptyPwd
                //controllo uguaglianza delle due password inserite
                if (newPwd != newConfirmPwd)
                {
                    //---valori diversi
                    return ChangePwdStatus.DifferentPwd;
                }
                else
                {
                    if (newPwd.Length >= minCharNumber)
                    {
                        if (ComparePassword(newPwd, username, "fAttivo"))
                        {
                            //---stessa pwd vecchia
                            return ChangePwdStatus.SamePwd;
                        }
                        else
                        {
                            if (UpdatePassword(username, newPwd))
                            {
                                // ok aggiornata
                                return ChangePwdStatus.ChangeOk;
                            }
                            else
                            {
                                // errore ???
                                return ChangePwdStatus.ChangeKo;
                            }

                        }
                    }
                    else
                    {
                        //---numero caratteri inferiori alla lunghezza minima
                        return ChangePwdStatus.ShortPwd;
                    }
                }
                #endregion EmptyPwd
            }
            else
            {
                //---manca una delle due
                return ChangePwdStatus.EmptyPwd;
            }

            return status;
        }
                
        public static LoginStatus ValidateUser(string username, string password, string app)
        {
            LoginStatus status;

            int dayExpired = int.Parse(ConfigurationManager.AppSettings["dayExpired"]);
            int logonRetriesMax = int.Parse(ConfigurationManager.AppSettings["logonRetriesMax"]);
            int logonRetriesWindow = int.Parse(ConfigurationManager.AppSettings["logonRetriesWindow"]);

            //var db = DataLayer.Common.ApplicationContext.Current.Context;
            using (var ctx = new CriterDataModel())
            {
                var utente = ctx.COM_Utenti.FirstOrDefault(i => i.Username == username && i.fAttivo == true && i.fSpid == false);

                string pwd = "";
                DateTime? scadenza = DateTime.MinValue;
                string salt = "";
                DateTime? dataUltimoLogon = DateTime.MinValue;
                DateTime? dataOraPrimoLogonFallito = DateTime.MinValue;
                int? nrTentativiFalliti = 0;
                bool bloccato = false;

                if (utente != null)
                {
                    if (utente.Password != null)
                    {
                        pwd = utente.Password;
                    }
                    if (utente.DataScadenzaPassword != null)
                    {
                        scadenza = utente.DataScadenzaPassword;
                    }
                    if (utente.Salt != null)
                    {
                        salt = utente.Salt;
                    }
                    if (utente.DataUltimoAccesso != null)
                    {
                        dataUltimoLogon = utente.DataUltimoAccesso;
                    }
                    if (utente.DataPrimoLogonFallito != null)
                    {
                        dataOraPrimoLogonFallito = utente.DataPrimoLogonFallito;
                    }
                    if (utente.NrTentativiFalliti != null)
                    {
                        nrTentativiFalliti = utente.NrTentativiFalliti;
                    }
                    bloccato = utente.fBloccato;

                    if (bloccato)
                    {
                        status = LoginStatus.LoginLocked;
                    }
                    else if (pwd == "")
                    {
                        // se la password non è specificata nel record utente considero la pwd scaduta
                        status = LoginStatus.LoginExpired;
                    }
                    else
                    {
                        if (ControlloCheckPassword(password, pwd, salt, app))
                        {
                            if (dayExpired < DateTime.Now.Subtract((DateTime)dataUltimoLogon).Days)
                            {
                                // se non si è connesso entro tot giorni l'account è scaduto
                                status = LoginStatus.LoginInactive;
                            }
                            else if (scadenza == DateTime.MinValue)
                            {
                                // se non c'è la data scadenza è scaduto
                                status = LoginStatus.LoginExpired;
                            }
                            else if (DateTime.Parse(scadenza.ToString()).Subtract(DateTime.Now).TotalHours < 0)
                            {
                                // se c'è la data scadenza ed è passata allora l'account è scaduto
                                status = LoginStatus.LoginExpired;
                            }
                            else
                            {
                                status = LoginStatus.LoginOK;
                            }
                        }
                        else
                        {
                            status = LoginStatus.LoginFailed;
                        }
                    }
                }
                else
                {
                    // userName non trovato
                    status = LoginStatus.LoginKO;
                }

                switch (status)
                {
                    case (LoginStatus.LoginFailed):
                        // determinazione della finestra di login
                        if (dataOraPrimoLogonFallito == DateTime.MinValue)
                        {
                            // è la prima volta (nella finestra)
                            utente.DataPrimoLogonFallito = DateTime.Now;
                            utente.NrTentativiFalliti = 1;
                            ctx.SaveChanges();
                        }
                        else if (DateTime.Now.Subtract((DateTime)dataOraPrimoLogonFallito).TotalMinutes < logonRetriesWindow)
                        {
                            // sono all'interno della finestra
                            if (nrTentativiFalliti >= logonRetriesMax)
                            {
                                // superato: blocchiamo
                                utente.fBloccato = true;
                                ctx.SaveChanges();
                            }
                            else
                            {
                                // non superato: incrementiamo
                                utente.NrTentativiFalliti = utente.NrTentativiFalliti + 1;
                                ctx.SaveChanges();
                            }
                        }
                        break;
                    case (LoginStatus.LoginOK):
                        // si registra e si azzera tutto
                        utente.DataUltimoAccesso = DateTime.Now;
                        utente.DataPrimoLogonFallito = null;
                        utente.NrTentativiFalliti = 0;
                        ctx.SaveChanges();
                        break;
                }
            }

            return status;
        }

        public static LoginStatus ValidateUserApp(string username, string password, string app)
        {
            LoginStatus status;

            int dayExpired = int.Parse(ConfigurationManager.AppSettings["dayExpired"]);
            int logonRetriesMax = int.Parse(ConfigurationManager.AppSettings["logonRetriesMax"]);
            int logonRetriesWindow = int.Parse(ConfigurationManager.AppSettings["logonRetriesWindow"]);

            using (var ctx = new CriterDataModel())
            {
                var utente = ctx.COM_Utenti.FirstOrDefault(i => i.Username == username && i.fAttivo == true && i.IDRuolo == 3 && i.fSpid == false);

                string pwd = "";
                DateTime? scadenza = DateTime.MinValue;
                string salt = "";
                DateTime? dataUltimoLogon = DateTime.MinValue;
                DateTime? dataOraPrimoLogonFallito = DateTime.MinValue;
                int? nrTentativiFalliti = 0;
                bool bloccato = false;

                if (utente != null)
                {
                    if (utente.Password != null)
                    {
                        pwd = utente.Password;
                    }
                    if (utente.DataScadenzaPassword != null)
                    {
                        scadenza = utente.DataScadenzaPassword;
                    }
                    if (utente.Salt != null)
                    {
                        salt = utente.Salt;
                    }
                    if (utente.DataUltimoAccesso != null)
                    {
                        dataUltimoLogon = utente.DataUltimoAccesso;
                    }
                    if (utente.DataPrimoLogonFallito != null)
                    {
                        dataOraPrimoLogonFallito = utente.DataPrimoLogonFallito;
                    }
                    if (utente.NrTentativiFalliti != null)
                    {
                        nrTentativiFalliti = utente.NrTentativiFalliti;
                    }
                    bloccato = utente.fBloccato;

                    if (bloccato)
                    {
                        status = LoginStatus.LoginLocked;
                    }
                    else if (pwd == "")
                    {
                        // se la password non è specificata nel record utente considero la pwd scaduta
                        status = LoginStatus.LoginExpired;
                    }
                    else
                    {
                        if (ControlloCheckPassword(password, pwd, salt, app))
                        {
                            if (dayExpired < DateTime.Now.Subtract((DateTime)dataUltimoLogon).Days)
                            {
                                // se non si è connesso entro tot giorni l'account è scaduto
                                status = LoginStatus.LoginInactive;
                            }
                            else if (scadenza == DateTime.MinValue)
                            {
                                // se non c'è la data scadenza è scaduto
                                status = LoginStatus.LoginExpired;
                            }
                            else if (DateTime.Parse(scadenza.ToString()).Subtract(DateTime.Now).TotalHours < 0)
                            {
                                // se c'è la data scadenza ed è passata allora l'account è scaduto
                                status = LoginStatus.LoginExpired;
                            }
                            else
                            {
                                status = LoginStatus.LoginOK;
                            }
                        }
                        else
                        {
                            status = LoginStatus.LoginFailed;
                        }
                    }
                }
                else
                {
                    // userName non trovato
                    status = LoginStatus.LoginKO;
                }

                switch (status)
                {
                    case (LoginStatus.LoginFailed):
                        // determinazione della finestra di login
                        if (dataOraPrimoLogonFallito == DateTime.MinValue)
                        {
                            // è la prima volta (nella finestra)
                            utente.DataPrimoLogonFallito = DateTime.Now;
                            utente.NrTentativiFalliti = 1;
                            ctx.SaveChanges();
                        }
                        else if (DateTime.Now.Subtract((DateTime)dataOraPrimoLogonFallito).TotalMinutes < logonRetriesWindow)
                        {
                            // sono all'interno della finestra
                            if (nrTentativiFalliti >= logonRetriesMax)
                            {
                                // superato: blocchiamo
                                utente.fBloccato = true;
                                ctx.SaveChanges();
                            }
                            else
                            {
                                // non superato: incrementiamo
                                utente.NrTentativiFalliti = utente.NrTentativiFalliti + 1;
                                ctx.SaveChanges();
                            }
                        }
                        break;
                    case (LoginStatus.LoginOK):
                        // si registra e si azzera tutto
                        utente.DataUltimoAccesso = DateTime.Now;
                        utente.DataPrimoLogonFallito = null;
                        utente.NrTentativiFalliti = 0;
                        ctx.SaveChanges();
                        break;
                }
            }

            return status;
        }

        private static bool ControlloCheckPassword(string password, string dbpassword, string salt, string app)
        {
            string pass1 = password;
            if (salt.Length > 0) pass1 = String.Concat(pass1, salt);

            string pass2 = dbpassword;

            string fEncodedPwd = ConfigurationManager.AppSettings["fEncodedPwd"];

            switch (fEncodedPwd)
            {
                case "on":
                    pass1 = EncodePassword(pass1);
                    break;
            }

            if ((pass1 == pass2) || (app =="spid"))
            {
                return true;
            }

            return false;
        }

        public static string EncodePassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
        }

        public static bool ComparePassword(string newPwd, string username, string app)
        {
            string oldPwd = "";
            string salt = "";

            if (username != "")
            {
                //var db = DataLayer.Common.ApplicationContext.Current.Context;
                using (var ctx = new CriterDataModel())
                {
                    var utente = ctx.COM_Utenti.FirstOrDefault(i => i.Username == username && i.fAttivo == true);

                    if (utente != null)
                    {
                        if (utente.Password != null)
                        {
                            oldPwd = utente.Password;
                        }
                        if (utente.Salt != null)
                        {
                            salt = utente.Salt;
                        }
                    }
                }
            }

            return ControlloCheckPassword(newPwd, oldPwd, salt, app);
        }

        internal static string CreaSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        
        public static bool UpdatePassword(string username, string newPwd)
        {
            bool outVal = false;
            string salt = "";

            string fEncodedPwd = ConfigurationManager.AppSettings["fEncodedPwd"];

            switch (fEncodedPwd)
            {
                case "on":
                    salt = CreaSalt(ISaltlen);
                    newPwd = String.Concat(newPwd, salt);
                    newPwd = EncodePassword(newPwd);
                    break;
            }

            DateTime nuovascadenza, oggi;
            oggi = DateTime.Now;
            nuovascadenza = DateTime.Now.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["dayExpired"]));

            if (username != "")
            {
                //var db = DataLayer.Common.ApplicationContext.Current.Context;
                using (var ctx = new CriterDataModel())
                {
                    var utente = ctx.COM_Utenti.FirstOrDefault(i => (i.Username == username && i.fAttivo == true));

                    utente.Password = newPwd;
                    utente.DataUltimaModificaPassword = oggi;
                    utente.DataScadenzaPassword = nuovascadenza;
                    utente.DataUltimoAccesso = DateTime.Now;
                    utente.Salt = salt;
                    ctx.SaveChanges();
                }

                outVal = true;
            }

            return outVal;
        }

        public static string GetRandomPassword()
        {
            int LunghezzaPassword = int.Parse(ConfigurationManager.AppSettings["minCharPwd"]);
            string PasswordResult = Guid.NewGuid().ToString();
            //Rimuovo gli hyphens
            PasswordResult = PasswordResult.Replace("-", String.Empty);

            return PasswordResult.Substring(0, LunghezzaPassword);
        }

        public static bool ControllaRecuperaPassword(string codFiscalePiva, string codiceSoggetto, string tipoSoggetto)
        {
            bool fOK = false;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var user = new V_COM_Utenti();

                switch (tipoSoggetto)
                {
                    case "1": //Impresa
                    case "2": //Manutentore
                        user = ctx.V_COM_Utenti.Where(s => ((s.CodiceFiscale == codFiscalePiva || s.PartitaIVA == codFiscalePiva) && s.CodiceSoggetto == codiceSoggetto && s.fattivoUser == true && s.fBloccato == false)
                                            ).FirstOrDefault();
                        break;
                    case "3": //Distributore
                    case "4": //Ente Locale
                    case "5": //Software house
                    case "6": //Ispettori
                        user = ctx.V_COM_Utenti.Where(s => ((s.CodiceFiscale == codFiscalePiva || s.PartitaIVA == codFiscalePiva) && s.Email == codiceSoggetto && s.fattivoUser == true && s.fBloccato == false)
                                            ).FirstOrDefault();
                        break;
                }

                if (user != null)
                {
                    fOK = true;
                }
            }

            return fOK;
        }

        public static bool RecuperaCredenziali(string codFiscalePiva, string codiceSoggetto, string tipoSoggetto)
        {
            bool fOk = false;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var user = new V_COM_Utenti();

                switch (tipoSoggetto)
                {
                    case "1": //Impresa
                    case "2": //Manutentore
                        user = ctx.V_COM_Utenti.Where(s => ((s.CodiceFiscale == codFiscalePiva || s.PartitaIVA == codFiscalePiva) && s.CodiceSoggetto == codiceSoggetto && s.fattivoUser == true && s.fBloccato == false)
                                            ).FirstOrDefault();
                        break;
                    case "3": //Distributore
                    case "4": //Ente Locale
                    case "5": //Software house
                    case "6": //Ispettori
                        user = ctx.V_COM_Utenti.Where(s => ((s.CodiceFiscale == codFiscalePiva || s.PartitaIVA == codFiscalePiva) && s.Email == codiceSoggetto && s.fattivoUser == true && s.fBloccato == false)
                                            ).FirstOrDefault();
                        break;
                }


                if (user !=null)
                {
                    string newPwd = SecurityManager.GetRandomPassword();
                    EmailNotify.SendCredenzialiPassword(user.Username, newPwd);
                    fOk = SecurityManager.UpdatePassword(user.Username, newPwd);
                }
            }

            return fOk;
        }

        #endregion
        
        public static UserInfo GetUserInfo(string username, bool reload)
        {
            UserInfo info = new UserInfo();
            if (!string.IsNullOrEmpty(username))
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var user = ctx.COM_Utenti.Where(s => (s.Username == username)
                                                ).SingleOrDefault();
                    info.IDUtente = user.IDUtente;
                    info.IDSoggetto = user.IDSoggetto;
                    info.IDSoggettoDerived = user.COM_AnagraficaSoggetti.IDSoggettoDerived;
                    info.IDTipoSoggetto = user.COM_AnagraficaSoggetti.IDTipoSoggetto;
                    info.CodiceSoggetto = user.COM_AnagraficaSoggetti.CodiceSoggetto;
                    info.IDRuolo = user.IDRuolo;
                    info.Ruolo = user.SYS_UserRole.Role;
                    info.UserName = user.Username;
                    if ((user.COM_AnagraficaSoggetti.IDTipoSoggetto == 1) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 4) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 7) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 8))
                    {
                        info.Utente = user.COM_AnagraficaSoggetti.Nome + " " + user.COM_AnagraficaSoggetti.Cognome;
                        info.cfPIva = user.COM_AnagraficaSoggetti.CodiceFiscale;
                    }
                    else if ((user.COM_AnagraficaSoggetti.IDTipoSoggetto == 2) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 3) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 5) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 6) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 9))
                    {
                        info.Utente = user.COM_AnagraficaSoggetti.NomeAzienda;
                        info.cfPIva = user.COM_AnagraficaSoggetti.PartitaIVA;
                    }
                    info.Email = user.COM_AnagraficaSoggetti.Email;
                    info.EmailPec = user.COM_AnagraficaSoggetti.EmailPec;
                    info.fSpid = user.fSpid;
                    info.KeyApi = user.KeyApi;
                }
            }

            return info;
        }

        public static string GetNomeCognomeFromIDUtente(int? IDUtente)
        {
            string soggetto = string.Empty;
            if (IDUtente !=null)
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var user = ctx.COM_Utenti.Where(s => (s.IDUtente == IDUtente)
                                                ).SingleOrDefault();
                    
                    if ((user.COM_AnagraficaSoggetti.IDTipoSoggetto == 1) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 4) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 7) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 8))
                    {
                        soggetto = user.COM_AnagraficaSoggetti.Nome + " " + user.COM_AnagraficaSoggetti.Cognome;
                        
                    }
                    else if ((user.COM_AnagraficaSoggetti.IDTipoSoggetto == 2) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 3) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 5) || (user.COM_AnagraficaSoggetti.IDTipoSoggetto == 6))
                    {
                        soggetto = user.COM_AnagraficaSoggetti.NomeAzienda;
                    }
                }
            }

            return soggetto;
        }

        public static bool GetSpidUser(int iDSoggetto)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                bool fSpid = false;
                var user = ctx.COM_Utenti.Where(s => (s.IDSoggetto == iDSoggetto)).FirstOrDefault();

                fSpid = user.fSpid;

                return fSpid;
            }
        }

        public static void CheckPagePermissions(string username, string pageUrl)
        {
            bool checkflag = false;

            if ((username != "") & (pageUrl != ""))
            {
                UserInfo userInfo = GetUserInfo(username, false);
                int? iDRole = userInfo.IDRuolo;

                if (iDRole == 1) // amministratore
                {
                    checkflag = true;
                }
                else
                {
                    //var db = DataLayer.Common.ApplicationContext.Current.Context;
                    using (var ctx = new CriterDataModel())
                    {
                        var menu = ctx.V_MNU_Menu.Where(i => i.IDRuolo == userInfo.IDRuolo
                                                    && (i.IDUtente == userInfo.IDUtente)
                                                    //&& (i.PageUrl == pageUrl)
                                                    && (i.fattivoMenu == true)
                                                    && (i.fattivoUtente == true)
                                                    && (i.fattivoAccessoUtente == true)
                                                    && (i.fPrivate == true)).ToList();

                        foreach (var item in menu)
                        {
                            if (item.PageUrl.Contains("?"))
                            {
                                var url = item.PageUrl.Remove(item.PageUrl.IndexOf("?"));
                                if (url == pageUrl)
                                {
                                    checkflag = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (item.PageUrl == pageUrl)
                                {
                                    checkflag = true;
                                    break;
                                }
                            }
                        }
                    }
                    //if (menu > 0)
                    //{
                    //    checkflag = true;
                    //}
                }

                if (!checkflag)
                {
                    HttpContext.Current.Response.Redirect("~/ErrorPagePermission.aspx");
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/ErrorPagePermission.aspx");
            }
        }

        public static bool CheckAccessPageConfirmCredential(int? iDSoggetto)
        {
            bool fAccesso = false;

            if (iDSoggetto != null)
            {
                //var db = DataLayer.Common.ApplicationContext.Current.Context;
                using (var ctx = new CriterDataModel())
                {
                    int count = ctx.COM_Utenti.Count(i => i.IDSoggetto == iDSoggetto
                                                && (i.fVerificaCredenziali == false)
                                                && (i.CodiceVerificaCredenziali != null)
                                                );

                    if (count > 0)
                    {
                        fAccesso = true;
                    }
                }
            }

            return fAccesso;
        }

        public static bool CheckAccessPageConfirmIscrizione(int? iDSoggetto)
        {
            bool fAccesso = false;

            if (iDSoggetto != null)
            {
                using (var ctx = new CriterDataModel())
                {
                    int count = ctx.COM_AnagraficaSoggetti.Count(i => i.IDSoggetto == iDSoggetto
                                                            && (i.fIscrizione == false)
                                                            );

                    if (count > 0)
                    {
                        fAccesso = true;
                    }
                }
            }

            return fAccesso;
        }

        public static void RedirectToErrorPermission()
        {
            var currentUrl = System.Web.HttpContext.Current.Request.Url;
            HttpContext.Current.Response.Redirect("~/COM_ErrorPermission.aspx?ref=" + currentUrl);
        }

        public static string GetUsernameUnique(string possibleUsername)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int userNameSuffix = 0;

                //var possibleUsername = string.Format("{0}{1}", "UTENTE", userNameSuffix.ToString());

                var users = ctx.COM_Utenti.Where(s => (s.Username == possibleUsername)
                                                ).ToList();

                var newuserName = "";
                var isNotUniqueName = true;
                newuserName = possibleUsername;
                while (isNotUniqueName)
                {
                    if (users.Count > 0)
                    {
                        userNameSuffix++;
                        newuserName = String.Format("{0}{1}", possibleUsername, userNameSuffix);
                        users = ctx.COM_Utenti.Where(s => (s.Username == newuserName)
                                                ).ToList();
                    }
                    else
                    {
                        isNotUniqueName = false;
                    }
                }

                return newuserName;
            }
        }
        
        public static string GetApiKey(string name, DateTime now)
        {
            String input = name + now.ToLongTimeString();
            String hash = "";
            using (SHA256 alg = SHA256.Create())
            {
                hash = GetSha256Hash(alg, input);
            }
            return hash;
        }

        private static string GetSha256Hash(SHA256 shaHash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = shaHash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        
        public static void ActivateUserCredential(int? iDSoggetto, int? iDTipoSoggetto, int? iDRuolo, object username, object password, object operationType)
        {
            using (var ctx = new CriterDataModel())
            {
                var user = new COM_Utenti();

                if (operationType == "insert")
                {
                    user.IDSoggetto = (int)iDSoggetto;
                    user.IDRuolo = (int)iDRuolo;
                    user.Username = null;
                    user.Password = null;
                    user.fAttivo = false;
                    user.DataUltimaModificaPassword = null;
                    user.DataScadenzaPassword = null;
                    user.fVerificaCredenziali = false;
                    user.CodiceVerificaCredenziali = Guid.NewGuid().ToString();
                    user.Salt = null;
                    user.DataUltimoAccesso = DateTime.Now;
                    user.DataPrimoLogonFallito = null;
                    user.NrTentativiFalliti = 0;
                    user.fBloccato = false;
                    user.fSpid = false;
                    user.KeyApi = null;

                    ctx.COM_Utenti.Add(user);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    EmailNotify.SendLinkCredenziali(iDSoggetto);
                }
                else if (operationType == "update")
                {
                    user = ctx.COM_Utenti.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);
                    string encriptPassword = EncodePassword(password.ToString());

                    user.Username = (string)username;
                    string salt = CreaSalt(ISaltlen);
                    string newPwd = String.Concat(password, salt);
                    newPwd = EncodePassword(newPwd);
                    user.Password = newPwd;
                    user.Salt = salt;
                    user.fAttivo = true;
                    user.DataUltimaModificaPassword = DateTime.Now;
                    user.DataScadenzaPassword = DateTime.Now.AddDays(int.Parse(ConfigurationManager.AppSettings["dayExpired"]));
                    user.fVerificaCredenziali = true;
                    user.CodiceVerificaCredenziali = null;
                    user.fSpid = false;
                    if (iDTipoSoggetto == 2)
                    {
                        user.KeyApi = GetApiKey(username.ToString(), DateTime.Now); //solo per azienda
                    }
                    else
                    {
                        user.KeyApi = null;
                    }

                    ctx.SaveChanges();

                    //Attribuzione numero di registrazione
                    if (iDTipoSoggetto != 5 && iDTipoSoggetto !=9 && iDTipoSoggetto !=7)
                    {
                        UtilitySoggetti.SetCodiceSoggetto(iDSoggetto, iDTipoSoggetto);
                    }

                    UtilitySoggetti.SetIscrizioneEffettuata(iDSoggetto);
                    EmailNotify.SendConfermaCredenziali(iDSoggetto);
                }
                else if (operationType == "Spid")
                {
                    var soggetto = new COM_AnagraficaSoggetti();
                    soggetto = ctx.COM_AnagraficaSoggetti.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);

                    user.IDSoggetto = (int)iDSoggetto;
                    user.IDRuolo = (int)iDRuolo;
                    user.Username = GetUsernameUnique(soggetto.Email);
                    user.Password = null;
                    user.fAttivo = true;
                    user.DataUltimaModificaPassword = null;
                    user.DataScadenzaPassword = null;
                    user.fVerificaCredenziali = true;
                    user.CodiceVerificaCredenziali = null;
                    user.Salt = null;
                    user.DataUltimoAccesso = DateTime.Now;
                    user.DataPrimoLogonFallito = null;
                    user.NrTentativiFalliti = 0;
                    user.fBloccato = false;
                    user.fSpid = true;
                    if (iDTipoSoggetto == 2)
                    {
                        user.KeyApi = GetApiKey(username.ToString(), DateTime.Now); //per azienda
                    }
                    else
                    {
                        user.KeyApi = null;
                    }

                    ctx.COM_Utenti.Add(user);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    //Attribuzione numero di registrazione
                    UtilitySoggetti.SetCodiceSoggetto(iDSoggetto, iDTipoSoggetto);
                    UtilitySoggetti.SetIscrizioneEffettuata(iDSoggetto);
                }
                else if (operationType == "SpidCittadino")
                {
                    var soggetto = new COM_AnagraficaSoggetti();
                    soggetto = ctx.COM_AnagraficaSoggetti.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);

                    user.IDSoggetto = (int)iDSoggetto;
                    user.IDRuolo = (int)iDRuolo;
                    user.Username = GetUsernameUnique(soggetto.Nome.Substring(0, 1) + soggetto.Cognome);
                    user.Password = null;
                    user.fAttivo = true;
                    user.DataUltimaModificaPassword = null;
                    user.DataScadenzaPassword = null;
                    user.fVerificaCredenziali = true;
                    user.CodiceVerificaCredenziali = null;
                    user.Salt = null;
                    user.DataUltimoAccesso = DateTime.Now;
                    user.DataPrimoLogonFallito = null;
                    user.NrTentativiFalliti = 0;
                    user.fBloccato = false;
                    user.fSpid = true;
                    user.KeyApi = null;

                    ctx.COM_Utenti.Add(user);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    UtilitySoggetti.SetIscrizioneEffettuata(iDSoggetto);
                }
                else if (operationType == "SoftwareHouse")
                {
                    string encriptPassword = EncodePassword(password.ToString());

                    user.IDSoggetto = (int)iDSoggetto;
                    user.IDRuolo = (int)iDRuolo;
                    user.Username = GetUsernameUnique(username.ToString());
                    string salt = CreaSalt(ISaltlen);
                    string newPwd = String.Concat(password, salt);
                    newPwd = EncodePassword(newPwd);
                    user.Password = newPwd;
                    user.fAttivo = true;
                    user.DataUltimaModificaPassword = DateTime.Now;
                    user.DataScadenzaPassword = DateTime.Now.AddDays(int.Parse(ConfigurationManager.AppSettings["dayExpired"]));
                    user.fVerificaCredenziali = true;
                    user.CodiceVerificaCredenziali = null;
                    user.Salt = salt;
                    user.DataUltimoAccesso = DateTime.Now;
                    user.DataPrimoLogonFallito = null;
                    user.NrTentativiFalliti = 0;
                    user.fBloccato = false;
                    user.fSpid = false;
                    user.KeyApi = GetApiKey(username.ToString(), DateTime.Now);

                    ctx.COM_Utenti.Add(user);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    EmailNotify.SendCredenzialiSoftwareHouse(iDSoggetto);
                }
            }
        }

        public static string[] GetDatiPermission()
        {
            string[] outVal = new string[4];
            outVal[0] = GetUserIDSoggettoDaUsername(HttpContext.Current.User.Identity.Name);
            outVal[1] = GetUserIDRole(HttpContext.Current.User.Identity.Name);
            outVal[2] = GetUserIDAziendaDelSoggetto(GetUserIDSoggettoDaUsername(HttpContext.Current.User.Identity.Name));
            outVal[3] = "";
            switch (outVal[1])
            {
                case "1": //Amministratore Criter
                    outVal[2] = "IDSoggetto=IDSoggetto";
                    outVal[3] = "";
                    break;
                case "2": //Amministratore azienda
                    outVal[2] = "IDSoggetto=" + outVal[0];
                    outVal[3] = "IDSoggetto IN (" + GetUserIDSoggettiCollegateDelAzienda(outVal[0]) + ")";
                    break;
                case "3": //Operatore/Addetto
                    //outVal[2] = "IDSoggetto=" + outVal[0];
                    outVal[3] = "";
                    break;
                case "10": //Responsabile tecnico
                    //outVal[2] = "IDSoggetto=" + outVal[0];
                    outVal[3] = "IDSoggetto IN (" + GetUserIDSoggettiCollegateDelAzienda(outVal[2]) + ")";
                    break;
            }

            return outVal;
        }
        
        public static string GetUserIDRole(string username)
        {
            string iDRole = "";

            SqlConnection conn = BuildConnection.GetConn();
            string select = "SELECT IDRuolo FROM V_COM_Utenti WHERE Username=@Username";

            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDRuolo"] != DBNull.Value)
                {
                    iDRole = objDR["IDRuolo"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDRole;
        }
        
        public static string GetUserDescription(string username)
        {
            string userDescription = "";

            SqlConnection conn = BuildConnection.GetConn();
            string select = "SELECT Utente FROM V_COM_Utenti WHERE Username=@Username";

            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["Utente"] != DBNull.Value)
                {
                    userDescription = objDR["Utente"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return userDescription;
        }

        public static string GetUserIDSoggettoDaUsername(string username)
        {
            string iDSoggetto = "";

            string select = "SELECT IDSoggetto FROM COM_Utenti WHERE Username=@Username";
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDSoggetto"] != DBNull.Value)
                {
                    iDSoggetto = objDR["IDSoggetto"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDSoggetto;
        }

        public static string GetUserIDSoggettoDaIDUtente(string iDUtente)
        {
            string iDSoggetto = "";

            string select = "SELECT IDSoggetto FROM COM_Utenti WHERE IDUtente=@IDUtente";
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@IDUtente", SqlDbType.Int, 4).Value = iDUtente;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDSoggetto"] != DBNull.Value)
                {
                    iDSoggetto = objDR["IDSoggetto"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDSoggetto;
        }

        public static string GetUserIDTipoSoggettoDaIDSoggetto(string iDSoggetto)
        {
            string iDTipoSoggetto = "";

            string select = "SELECT IDTipoSoggetto FROM COM_AnagraficaSoggetti WHERE IDSoggetto=@IDSoggetto";
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@IDSoggetto", SqlDbType.Int, 4).Value = iDSoggetto;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDTipoSoggetto"] != DBNull.Value)
                {
                    iDTipoSoggetto = objDR["IDTipoSoggetto"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDTipoSoggetto;
        }

        public static string GetUserIDAziendaDelSoggetto(string iDSoggetto)
        {
            string iDAzienda = "";

            string select = "SELECT IDSoggettoDerived AS IDSoggetto FROM COM_AnagraficaSoggetti WHERE IDSoggetto=@IDSoggetto";
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@IDSoggetto", SqlDbType.Int, 4).Value = iDSoggetto;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDSoggetto"] != DBNull.Value)
                {
                    iDAzienda = objDR["IDSoggetto"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDAzienda;
        }

        public static string GetUserIDSoggettiCollegateDelAzienda(string iDAzienda)
        {
            string iDSoggetti = "0,";
                        
            string select = "SELECT IDSoggetto FROM COM_AnagraficaSoggetti WHERE IDSoggettoDerived=@IDSoggettoDerived AND IDTipoSoggetto IN(1)"; //AND CodiceSoggetto IS NOT NULL
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@IDSoggettoDerived", SqlDbType.Int, 4).Value = iDAzienda;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (objDR.Read())
            {
                if (objDR["IDSoggetto"] != DBNull.Value)
                {
                    iDSoggetti += objDR["IDSoggetto"] + ",";
                }
            }
            objDR.Close();
            conn.Close();

            if (iDSoggetti.Length > 0)
            {
                iDSoggetti = iDSoggetti.Substring(0, iDSoggetti.Length - 1);
            }
                        
            return iDSoggetti;
        }

        public static string GetUserIDUtente(string username)
        {
            string iDUtente = "";

            SqlConnection conn = BuildConnection.GetConn();
            string select = "SELECT IDUtente FROM COM_Utenti WHERE Username=@Username";
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDUtente"] != DBNull.Value)
                {
                    iDUtente = objDR["IDUtente"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDUtente;
        }

        public static string GetUserIDUtenteDaEmail(string email)
        {
            string iDUtente = "";

            SqlConnection conn = BuildConnection.GetConn();
            string select = "SELECT COM_Utenti.IDUtente FROM dbo.COM_Utenti INNER JOIN COM_AnagraficaSoggetti ON COM_Utenti.IDSoggetto = COM_AnagraficaSoggetti.IDSoggetto WHERE COM_AnagraficaSoggetti.Email=@Email";
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDUtente"] != DBNull.Value)
                {
                    iDUtente = objDR["IDUtente"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDUtente;
        }

        public static string GetUserIDUtenteDaIDSoggetto(string iDSoggetto)
        {
            string iDUtente = "";

            SqlConnection conn = BuildConnection.GetConn();
            string select = "SELECT IDUtente FROM COM_Utenti WHERE IDSoggetto=@IDSoggetto";
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@IDSoggetto", SqlDbType.Int, 4).Value = iDSoggetto;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["IDUtente"] != DBNull.Value)
                {
                    iDUtente = objDR["IDUtente"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return iDUtente;
        }

        public static string GetUserDescriptionSoggetto(string iDSoggetto)
        {
            string soggetto = "";

            string select = "SELECT Soggetto FROM V_COM_AnagraficaSoggetti WHERE IDSoggetto=@IDSoggetto";
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.Add("@IDSoggetto", SqlDbType.Int, 4).Value = iDSoggetto;

            conn.Open();
            SqlDataReader objDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (objDR.Read())
            {
                if (objDR["Soggetto"] != DBNull.Value)
                {
                    soggetto = objDR["Soggetto"].ToString();
                }
            }
            objDR.Close();
            conn.Close();

            return soggetto;
        }
        
        public static bool ChangefAttivo(int iDSoggetto, bool fAttivo)
        {
            bool fAttivoReturn = false;

            using (var ctx = new CriterDataModel())
            {
                var utente = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();

                if (utente != null)
                {
                    if (fAttivo)
                    {
                        utente.fAttivo = false;
                    }
                    else
                    {
                        utente.fAttivo = true;
                    }

                    fAttivoReturn = utente.fAttivo;

                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                
                return fAttivoReturn;
            }
        }

        public static bool CheckSoggettoWithUtenza(int? iDSoggetto)
        {
            bool fCheck = false;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();

                if (utenza != null)
                {
                    fCheck = true;
                }
            }

            return fCheck;
        }
    }

    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }

    public class CriterUser
    {
        public string UserName { get; set; }
        public string IP { get; set; }
        public DateTime LastPresence { get; set; }
    }

    [Serializable]
    public class UserInfo
    {
        public int? IDUtente { get; set; }
        public int? IDSoggetto { get; set; }
        public int? IDSoggettoDerived { get; set; }
        public int? IDTipoSoggetto { get; set; }
        public string CodiceSoggetto { get; set; }
        public int? IDRuolo { get; set; }
        public string Ruolo { get; set; }
        public string UserName { get; set; }
        public string Utente { get; set; }
        public string Email { get; set; }
        public string EmailPec { get; set; }
        public bool fSpid { get; set; }
        public string KeyApi { get; set; }
        public string cfPIva { get; set; }
    }
    
 }