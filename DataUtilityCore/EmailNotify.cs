using System;
using System.Configuration;
using System.Web;
using System.Net.Mail;
using EncryptionQS;
using System.Linq;
using System.IO;
using DataLayer;
using System.Collections.Generic;
using System.Linq.Dynamic;
using DevExpress.Web;

namespace DataUtilityCore
{
    public class EmailNotify
    {                       
        public static SmtpClient TypeSmtpClient(string typeSmtpClient)
        {
            SmtpClient SmtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpMailServer"].ToString());
            SmtpClient.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPortMailServer"].ToString());

            switch (typeSmtpClient)
            {
                case "SmtpGmailProvider":
                    SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Net.NetworkCredential mailAuthenticationGMail = new System.Net.NetworkCredential(CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["SmtpMailUser"].ToString()), CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["SmtpMailPassword"].ToString()));
                    SmtpClient.UseDefaultCredentials = false;
                    SmtpClient.EnableSsl = true;
                    SmtpClient.Credentials = mailAuthenticationGMail;
                    break;
                case "CustomProvider":
                    SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["SmtpMailUser"].ToString()), CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["SmtpMailPassword"].ToString()));
                    SmtpClient.UseDefaultCredentials = false;
                    SmtpClient.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPortMailServer"].ToString());
                    SmtpClient.EnableSsl = true;
                    SmtpClient.Credentials = mailAuthentication;
                    break;
                case "SmtpProvider":
                    SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpClient.UseDefaultCredentials = true;
                    SmtpClient.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPortMailServer"].ToString());
                    break;
                case "Exchange":
                    SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpClient.UseDefaultCredentials = true;
                    break;
            }

            return SmtpClient;
        }

        //public static MailKit.Net.Smtp.SmtpClient TypeSmtpPecClient()
        //{
        //    var client = new MailKit.Net.Smtp.SmtpClient();
        //    try
        //    {
        //        client.Connect(ConfigurationManager.AppSettings["PecSmtpServerHost"].ToString(),
        //         int.Parse(ConfigurationManager.AppSettings["PecSmtpServerPort"].ToString()), true);

        //        client.Authenticate(CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["PecSmtpServerUsername"].ToString()),
        //                            CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["PecSmtpServerPassword"].ToString())
        //                            );
        //    }
        //    catch (MailKit.Net.Smtp.SmtpCommandException ex)
        //    {
        //        //Console.WriteLine("Errore connessione alla Pec: {0}", ex.Message);
        //        //Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);
        //    }
        //    catch (MailKit.Net.Smtp.SmtpProtocolException ex)
        //    {
        //        //Console.WriteLine("Errore connessione al protocollo della Pec: {0}", ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }

        //    return client;
        //}

        public static bool SendPecMail(MailMessage msg)
        {
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    string host = ConfigurationManager.AppSettings["PecSmtpServerHost"].ToString();
                    int port = int.Parse(ConfigurationManager.AppSettings["PecSmtpServerPort"].ToString());

                    string decriptUsername = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["PecSmtpServerUsername"].ToString());
                    string decriptPassword = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["PecSmtpServerPassword"].ToString());


                    client.Connect(host, port, true);
                    client.Authenticate(decriptUsername, decriptPassword);
                    client.Send(MimeKit.MimeMessage.CreateFromMailMessage(msg));
                    client.Disconnect(true);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public static void SendTestPec(string EmailPec)
        //{
        //    MailMessage msg = new MailMessage();
        //    var client = TypeSmtpPecClient();

        //    string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:12px;\">";
        //    mess += "<img src='http://criter.pomiager.com/images/LogoCriter.png'<br/><br/>";
        //    mess += "<b><u>AVVIO DELLE PROCEDURE PER L’ATTIVAZIONE DEL CATASTO REGIONALE DEGLI IMPIANTI TERMICI</u></b><br/><br/>";
        //    mess += "<div>";
        //    mess += "Il 1° giugno entrano in vigore le disposizioni del Regolamento regionale 3 aprile 2017 n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”, emanato ai sensi dell’art. 25-quater della Legge regionale n. 26 del 23 dicembre 2004.<br/><br/>";
        //    mess += "Il regolamento disciplina le modalità per la costituzione e la gestione del sistema informativo regionale relativo agli impianti termici per la climatizzazione invernale ed estiva degli edifici, denominato catasto regionale degli impianti termici (CRITER), e le funzioni delle imprese di installazione e manutenzione degli impianti medesimi. Tali imprese sono infatti tenute a:<br/><br/>";
        //    mess += "•	acquisire i codici per la targatura degli impianti<br/>";
        //    mess += "•	effettuare l’accatastamento dell’impianto, mediante registrazione del Libretto di impianto associato al relativo codice di targatura, e provvedere al successivo eventuale aggiornamento<br/>";
        //    mess += "•	acquisire i bollini “calore pulito”<br/>";
        //    mess += "•	effettuare la registrazione dei Rapporti di controllo di efficienza energetica, associandoli al relativo bollino<br/><br/>";
        //    mess += "Per la gestione del catasto è stato messo a punto un applicativo informatico (denominato anch’esso CRITER) che consente la dematerializzazione di tutte le procedure: l’accesso al sistema avviene dalla pagina web http://energia.regione.emilia-romagna.it/servizi-on-line/criter , nella quale sono segnalate anche le modalità di contatto con l’Organismo di Accreditamento ed Ispezione per la risoluzione di eventuali problematiche.<br/><br/>";
        //    mess += "Le funzionalità dell’applicativo informatico sono rese disponibili dalle ore 11 del giorno 1° giugno 2017, momento a partire dal quale le imprese di installazione e manutenzione impianti possono effettuare la registrazione per l’acquisizione delle credenziali necessarie ad operare le funzioni di propria competenza.<br/><br/>";
        //    mess += "Si precisa che in fase di avvio la procedura di registrazione può comportare una attesa di qualche giorno per consentire la effettuazione dei relativi controlli: la attribuzione delle prime credenziali di accesso, con la conseguente possibilità di iniziare ad operare nell’ambito del sistema, avverrà a partire da lunedì 5 giugno.<br/><br/>";
        //    mess += "Alleghiamo una breve sintesi del contesto normativo ed operativo. Vogliamo rassicurare tutte le imprese che la Regione intende evitare un approccio rigidamente burocratico, nella consapevolezza che il successo dell’operazione passa attraverso il loro coinvolgimento fattivo: l’indirizzo assunto è quindi quello di ricercare la massima collaborazione degli operatori del settore, fornendo loro il necessario supporto all’espletamento delle funzioni attribuite.<br/><br/><br/>";
        //    mess += "<i>Arter Spa <br> Organismo Regionale di Accreditamento ed Ispezione</i>";
        //    mess += "</div>";

        //    msg.IsBodyHtml = true;
        //    msg.Body = mess;
        //    msg.To.Add(new MailAddress(EmailPec));

        //    string pathDocument = "D:\\Applicazioni\\CriterV01\\CriterPortal\\UploadIscrizioneSoggetti\\comunicazione.pdf";
        //    if ((pathDocument != ""))
        //    {
        //        Attachment AttachmentPdf1 = new Attachment(pathDocument);
        //        msg.Attachments.Add(AttachmentPdf1);
        //    }
            
        //    msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
        //    msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Procedura di Attivazione";
        //    try
        //    {
        //        client.Send(MimeKit.MimeMessage.CreateFromMailMessage(msg));
        //        client.Disconnect(true);
        //    }
        //    catch (HttpException ex)
        //    {

        //    }
        //}

        public static void SendApplicationSpid(string headerSpid)
        {
            MailMessage msg = new MailMessage();
            SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "images/LogoCriter.png' />";
            mess += "<br/><br/>";
            mess += "<b>Header Spid: " + headerSpid + "</b><br/>";
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailToError"]));
            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFromVerify"]);
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " SPID - IP HOST: " + HttpContext.Current.Request.UserHostAddress.ToString();
            try
            {
                smtpClient.Send(msg);
            }
            catch (System.Web.HttpException ex)
            {

            }
        }

        public static void SendApplicationError()
        {
            MailMessage msg = new MailMessage();
            SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "images/LogoCriter.png' />";
            mess += "<br/><br/>";
            mess += "<b>DETTAGLI ERRORE RILEVATO SUL PORTALE CRITER DALL'IP: " + HttpContext.Current.Request.UserHostAddress.ToString() + "</b>";
            mess += "<br/><br/>";
            mess += "<b>Errore nella pagina:</b>" + HttpContext.Current.Request.Path;
            mess += "<br/><br/>";
            mess += "<b>Errore all'url:</b>" + HttpContext.Current.Request.RawUrl;
            Exception myError = HttpContext.Current.Server.GetLastError();
            mess += "<br/><br/> <b>Messaggio Errore:</b>" + myError.Message;
            mess += "<br/><br/> <b>Sorgente Errore:</b>" + myError.Source;
            mess += "<br/><br/> <b>Errore InnerException:</b><br/> " + myError.InnerException;
            mess += "<br/><br/> <b>Errore Stack Trace:</b><br/>" + myError.StackTrace;
            mess += "<br/><br/> <b>Errore TargetSite:</b>" + myError.TargetSite;
            mess += "<br/><br/> <b>Errore Data:</b>" + myError.Data;
            mess += "<br/><br/> <b>Errore HelpLink:</b>" + myError.HelpLink;
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailToError"]));
            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Errore nell'applicazione - IP HOST: " + HttpContext.Current.Request.UserHostAddress.ToString();
            try
            {
                smtpClient.Send(msg);
            }
            catch (System.Web.HttpException ex)
            {

            }
        }

        public static void SendApplicationViolation()
        {
            MailMessage msg = new MailMessage();
            SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "images/LogoCriter.png' />";
            mess += "<br/><br/>";
            mess += "<b>DESCRIZIONE DI INTRUSIONE SUL PORTALE CRITER DALL'IP: " + HttpContext.Current.Request.UserHostAddress.ToString() + "</b>";
            mess += "<br/><br/>";
            mess += "<b>Tentativo di intrusione nella pagina:</b>" + HttpContext.Current.Request.Path;
            mess += "<b>all'url:</b>" + HttpContext.Current.Request.RawUrl;
            mess += "<br/><br/>";
            mess += "<b>dall'utente:</b>" + HttpContext.Current.User.Identity.Name.ToString();
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailToError"]));
            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Tentativo di intrusione - IP: " + HttpContext.Current.Request.UserHostAddress.ToString();
            try
            {
                smtpClient.Send(msg);
            }
            catch (System.Web.HttpException ex)
            {

            }
        }

        public static void SendLinkCompletamentoIscrizione(int? iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var user = (from a in ctx.V_COM_AnagraficaSoggetti
                            where (a.IDSoggetto == iDSoggetto)
                            select new
                            {
                                IDSoggetto = a.IDSoggetto,
                                Soggetto = a.Soggetto,
                                Email = a.Email,
                                IDTipoSoggetto = a.IDTipoSoggetto
                            }).FirstOrDefault();

                MailMessage msg = new MailMessage();
                SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

                string linkAttivazioneUtenza = "";
                QueryString qs = new QueryString();
                qs.Add("IDSoggetto", user.IDSoggetto.ToString());
                qs.Add("fSpid", "false");
                qs.Add("IDTipoSoggetto", user.IDTipoSoggetto.ToString());
                qs.Add("codicefiscale", "");
                qs.Add("key", "");
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = ConfigurationManager.AppSettings["UrlPortal"] + "IscrizioneConferma.aspx";
                url += qsEncrypted.ToString();
                linkAttivazioneUtenza = url;

                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br /><br /><br/><b><u>COMUNICAZIONE COMPLETAMENTO ISCRIZIONE AL SISTEMA TELEMATICO CRITER</u></b><br/><br/>";
                mess += "<br/>";
                mess += "Gentile " + "<b>" + UtilityApp.GetPrimaLetteraMaiuscola(user.Soggetto) + "</b><br />";
                mess += "per completare la registrazione al sistema Criter, è necessario collegarsi a questo indirizzo (nel caso in cui non sia attivo il link copiare ed incollare per intero il seguente indirizzo nella barra degli indirizzi del suo browser):";
                mess += "<br/><br/>" + linkAttivazioneUtenza + "<br/><br/>";
                mess += "In allegato la scheda di Iscrizione che andrà opportunatamente firmata digitalmente.";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(user.Email));
                
                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Completamento Iscrizione al Sistema Criter";

                string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
                string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                string reportName = string.Empty;
                string pathPdfFile = string.Empty;
                string destinationFile = string.Empty;

                switch (user.IDTipoSoggetto.ToString())
                {
                    case "1": //Persona

                        break;
                    case "2": //Impresa
                    case "3": //Terzo responsabile
                        destinationFile = ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"];
                        reportName = ConfigurationManager.AppSettings["ReportNameSchedaIscrizione"];
                        pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "Iscrizione_" + user.IDSoggetto.ToString() + ".pdf";
                        break;
                    case "4": //Persona Responsabile tecnico

                        break;
                    case "5": //Distributori di combustibile

                        break;
                    case "6": //Software house

                        break;
                    case "7": //Ispettori
                        destinationFile = ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + user.IDSoggetto.ToString();
                        reportName = ConfigurationManager.AppSettings["ReportNameSchedaIscrizioneIspettore"];
                        pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + user.IDSoggetto.ToString() + @"\" + "IscrizioneIspettore_" + user.IDSoggetto.ToString() + ".pdf";
                        break;
                    case "9": //Ente locale
                        destinationFile = ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"];
                        reportName = ConfigurationManager.AppSettings["ReportNameSchedaIscrizioneEnteLocale"];
                        pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "IscrizioneEnteLocale_" + user.IDSoggetto.ToString() + ".pdf";
                        break;
                }

                string urlPdf = ReportingServices.GetSchedaIscrizioneReport(iDSoggetto.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
                
                Attachment AttachmentPdf = new Attachment(pathPdfFile);
                msg.Attachments.Add(AttachmentPdf);

                try
                {
                    smtpClient.Send(msg);
                }
                catch (HttpException ex)
                {

                }
            }
        }

        public static void SendIscrizioneIspettore(int? iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var user = (from a in ctx.V_COM_AnagraficaSoggetti
                            where (a.IDSoggetto == iDSoggetto)
                            select new
                            {
                                IDSoggetto = a.IDSoggetto,
                                Soggetto = a.Soggetto,
                                Email = a.Email,
                                IDTipoSoggetto = a.IDTipoSoggetto
                            }).FirstOrDefault();

                MailMessage msg = new MailMessage();
                SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br /><br /><br/><b><u>COMUNICAZIONE CONFERMA RICHIESTA REGISTRAZIONE SISTEMA CRITER</u></b><br/><br/>";
                mess += "<br/>";
                mess += "Gentile " + "<b>" + UtilityApp.GetPrimaLetteraMaiuscola(user.Soggetto) + "</b><br />";
                mess += "Le comunichiamo che la Sua richiesta di iscrizione al sistema CRITER come ispettore degli impianti termici della Regione Emilia-Romagna è stata correttamente registrata. Si precisa che la registrazione al sistema CRITER non comporta automaticamente l’iscrizione all’elenco regionale degli ispettori. Si ricorda, infatti,  che secondo quanto previsto dal “Disciplinare di Accreditamento degli ispettori”, l’accreditamento all’elenco regionale avviene a seguito:<br /><br />";
                mess += "1) della frequenza del percorso formativo di specializzazione CRITER (della durata complessiva di 20 ore);<br />";
                mess += "2) aver partecipato ad almeno tre ispezioni di affiancamento ad un ispettore qualificato, ed aver ottenuto da questi valutazione positiva rispetto alla capacità di conduzione delle relative attività;<br />";
                mess += "3) del riconoscimento della funzione di “agente accertatore” di cui all’art. 6 della legge regionale n. 21 del 1984 ratificato dalla Regione Emilia-Romagna con Determina del Dirigente responsabile.<br /><br />";
                mess += "Per coloro che, antecedentemente alla data di entrata in vigore del Regolamento regionale 1/2017 (1°Giugno 2017), operavano come ispettori degli impianti termici su incarico delle Autorità competenti (Province e Comuni della Regione Emilia-Romagna) il punto 2) dell’elenco soprariportato non è richiesto.<br /><br />";
                mess += "Distinti saluti<br />";
                mess += "Organismo regionale di accreditamento ed ispezione";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(user.Email));
                //msg.CC.Add(new MailAddress(ConfigurationManager.AppSettings["EmailToError"]));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Comunicazione conferma richiesta iscrizione al Sistema Criter";

                try
                {
                    smtpClient.Send(msg);
                }
                catch (HttpException ex)
                {

                }
            }
        }

        public static void SendLinkCredenziali(int? iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var user = (from a in ctx.V_COM_AnagraficaSoggetti
                            where (a.IDSoggetto == iDSoggetto)
                            select new
                            {
                                IDSoggetto = a.IDSoggetto,
                                Soggetto = a.Soggetto,
                                Email = a.Email,
                                IDTipoSoggetto = a.IDTipoSoggetto
                            }).FirstOrDefault(); 

                MailMessage msg = new MailMessage();
                SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

                string linkAttivazioneUtenza = "";
                QueryString qs = new QueryString();
                qs.Add("IDSoggetto", user.IDSoggetto.ToString());
                qs.Add("IDTipoSoggetto", user.IDTipoSoggetto.ToString());
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = ConfigurationManager.AppSettings["UrlPortal"] + ConfigurationManager.AppSettings["LinkPageAttivazioneUtenza"];
                url += qsEncrypted.ToString();
                linkAttivazioneUtenza = url;

                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br /><br /><br/><b><u>COMUNICAZIONE CREDENZIALI PER L'ACCESSO ALL'AREA RISERVATA DEL SISTEMA TELEMATICO CRITER</u></b><br/><br/>";
                mess += "<br/>";
                mess += "Gentile " + "<b>" + UtilityApp.GetPrimaLetteraMaiuscola(user.Soggetto) + "</b><br />";
                mess += "per completare la registrazione ed accedere all'area riservata del sistema Criter, è necessario collegarsi a questo indirizzo (nel caso in cui non sia attivo il link copiare ed incollare per intero il seguente indirizzo nella barra degli indirizzi del suo browser):";
                mess += "<br/><br/>" + linkAttivazioneUtenza + "<br/><br/>";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(user.Email));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Attribuzioni Credenziali Sistema Criter";
                try
                {
                    smtpClient.Send(msg);
                }
                catch (System.Web.HttpException ex)
                {

                }
            }
        }

        public static void SendConfermaCredenziali(int? iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var user = (from a in ctx.V_COM_AnagraficaSoggetti
                            where (a.IDSoggetto == iDSoggetto)
                            select new
                            {
                                Soggetto = a.Soggetto,
                                Email = a.Email,
                                CodiceSoggetto = a.CodiceSoggetto,
                                IDTipoSoggetto = a.IDTipoSoggetto
                            }).FirstOrDefault();

                var utente = (from a in ctx.V_COM_Utenti
                              where (a.IDSoggetto == iDSoggetto)
                              select new
                              {
                                  ApiKey = a.KeyApi
                              }).FirstOrDefault();

                MailMessage msg = new MailMessage();
                SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

                string mess = string.Empty;
                
                if ((user.IDTipoSoggetto != 5) && (user.IDTipoSoggetto != 9) && (user.IDTipoSoggetto != 7))
                {
                    mess += "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                    mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                    mess += "<br /><br /><br/><b><u>COMUNICAZIONE ATTIVAZIONE ACCESSO ALL'AREA RISERVATA DEL SISTEMA TELEMATICO CRITER</u></b><br/><br/>";
                    mess += "<br/>";
                    mess += "Gentile " + "<b>" + UtilityApp.GetPrimaLetteraMaiuscola(user.Soggetto) + "</b><br />";
                    mess += "le comunichiamo che il suo Codice di Accreditamento al Sistema Criter è <b><font color='green'>" + user.CodiceSoggetto + "</font></b>.<br/>";
                    mess += "Nel caso in cui desidera interfacciare il suo sistema di calcolo, per l'invio dei Libretti di Impianto e dei Rapporti di Controllo Tecnico, con il sistema Criter le forniamo la Api Key personale da conservare e non comunicare a soggetti terzi <b><font color='green'>" + utente.ApiKey + "</font></b>.<br/>";
                    mess += "Le comunichiamo altresì che a partire dalla data odierna le sarà possibile operare all'interno del Catasto Regionale degli Impianti Termici.<br/>";
                    mess += "<br/>Accedendo all'area riservata troverà una guida per il corretto utilizzo di tutte le funzioni del sistema e le sarà altresì possibile procedere all'aggiornamento dei propri dati personali.<br>";
                    mess += "</div>";
                }
                else
                {
                    mess += "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                    mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                    mess += "<br /><br /><br/><b><u>COMUNICAZIONE ATTIVAZIONE ACCESSO ALL'AREA RISERVATA DEL SISTEMA TELEMATICO CRITER</u></b><br/><br/>";
                    mess += "<br/>";
                    mess += "Gentile " + "<b>" + UtilityApp.GetPrimaLetteraMaiuscola(user.Soggetto) + "</b><br />";
                    mess += "le comunichiamo che a partire dalla data odierna le sarà possibile operare all'interno del Catasto Regionale degli Impianti Termici.<br/>";
                    mess += "<br/>Accedendo all'area riservata troverà una guida per il corretto utilizzo di tutte le funzioni del sistema e le sarà altresì possibile procedere all'aggiornamento dei propri dati personali.<br>";
                    mess += "</div>";
                }
                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(user.Email));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Attivazione accesso Sistema Criter";
                try
                {
                    smtpClient.Send(msg);
                }
                catch (HttpException ex)
                {

                }
            }
        }

        public static void SendCredenzialiSoftwareHouse(int? iDSoggetto)
        {
            //var db = DataLayer.Common.ApplicationContext.Current.Context;
            using (var ctx = new CriterDataModel())
            {
                var soggetto = (from a in ctx.V_COM_AnagraficaSoggetti
                                where (a.IDSoggetto == iDSoggetto)
                                select new
                                {
                                    Soggetto = a.Soggetto,
                                    Email = a.Email,
                                    PartitaIva = a.PartitaIVA
                                }).FirstOrDefault();

                var utente = (from a in ctx.V_COM_Utenti
                              where (a.IDSoggetto == iDSoggetto)
                              select new
                              {
                                  Username = a.Username,
                                  Password = a.Password,
                                  ApiKey = a.KeyApi
                              }).FirstOrDefault();

                MailMessage msg = new MailMessage();
                SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br /><br /><br/><b><u>COMUNICAZIONE ATTIVAZIONE ACCESSO ALL'AREA RISERVATA DEL SISTEMA TELEMATICO CRITER</u></b><br/><br/>";
                mess += "<br/>";
                mess += "Gentile Software House " + "<b>" + UtilityApp.GetPrimaLetteraMaiuscola(soggetto.Soggetto) + "</b><br />";
                mess += "le comunichiamo le credenziali di accesso al Sistema Criter per poter utilizzare le Application Programming Interface (Api).<br/><br/>";
                mess += "<b>Username</b>:" + utente.Username + "<br/>";
                mess += "<b>Password</b>:" + soggetto.PartitaIva + "<br/>";
                mess += "<b>Url</b>:" + ConfigurationManager.AppSettings["UrlPortal"] + "<br/>";
                mess += "<b>Api key</b>:" + utente.ApiKey + "<br/>";

                mess += "La <b>Api Key</b> le consentirà di effettuare delle chiamate alle Api messe a disposizione e quindi di interfacciare i Suoi sistemi con il Sistema Criter.<br/>";
                mess += "<br/>Accedendo all'area riservata troverà inoltre la documentazione per il corretto utilizzo di tutte le Api messe a disposizione dal sistema Criter.<br>";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(soggetto.Email));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Attivazione accesso utilizzo Api Sistema Criter";
                try
                {
                    smtpClient.Send(msg);
                }
                catch (HttpException ex)
                {

                }
            }
        }
        
        public static void SendCredenzialiUsername(string username, string password)
        {
            UserInfo info = SecurityManager.GetUserInfo(username, false);
            
            MailMessage msg = new MailMessage();
            SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
            mess += "<br><br>La presente e-mail viene generata dal sistema Criter.";
            mess += "<br><br>Il suo username è: " + info.UserName;
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            msg.To.Add(new MailAddress(info.Email));

            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Recupero Username";
            try
            {
                smtpClient.Send(msg);
            }
            catch (System.Web.HttpException ex)
            {

            }
        }

        public static void SendCredenzialiPassword(string username, string password)
        {
            UserInfo info = SecurityManager.GetUserInfo(username, false);

            MailMessage msg = new MailMessage();
            SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
            mess += "<br><br>La presente e-mail viene generata dal sistema Criter.";
            mess += "<br><br>La sua nuova password è: " + password;
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            msg.To.Add(new MailAddress(info.Email));

            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Recupero Password";
            try
            {
                smtpClient.Send(msg);
            }
            catch (System.Web.HttpException ex)
            {

            }
        }

        public static void SendRicevutaPagamentoAmministrazione(Guid IDMovimento)
        {
            //try
            //{
            //    using (var ctx = new CriterDataModel())
            //    {
            //        var rigaPortafoglio = ctx.COM_RigaPortafoglio.Find(IDMovimento);
            //        MailMessage msg = new MailMessage();
            //        SmtpClient SmtpClient = EmailUtil.TypeSmtpClient();

            //        msg.IsBodyHtml = true;
            //        var codiceEnomeCertificatore = string.Format("{0} - {1}",
            //            rigaPortafoglio.COM_Portafoglio.COM_AnagraficaSoggetti.NumeroAccreditamento,
            //            rigaPortafoglio.COM_Portafoglio.COM_AnagraficaSoggetti.NomeCognomeCalcolato);
            //        StringBuilder sb = new StringBuilder();
            //        sb.Append("<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:12px;\">");
            //        sb.Append("<b>CRITER - RICEVUTA DI PAGAMENTO</b><br/><br/>");
            //        sb.AppendFormat("<p>In allegato la ricevuta di pagamento dell'azienda manutentrice <b>{0}</b></p>",
            //            codiceEnomeCertificatore);
            //        sb.AppendFormat("<p>ID TRANSAZIONE: {0}</p>", rigaPortafoglio.IDMovimento.ToString().ToUpper());
            //        sb.AppendFormat("<p>Data/ora: {0:dd/MM/yyyy HH.mm.ss}</p>", rigaPortafoglio.DataRegistrazione);
            //        sb.AppendFormat("<p>Importo: {0:n2}</p>", rigaPortafoglio.Importo);
            //        var note = string.Empty;
            //        if (rigaPortafoglio.COM_MovimentoBonifico != null)
            //        {
            //            note = "Incasso Bonifico del " +
            //                   rigaPortafoglio.COM_MovimentoBonifico.DataBonifico.ToShortDateString();
            //        }
            //        else if (rigaPortafoglio.COM_MovimentoCassa != null)
            //        {
            //            note = "Incasso Diretto del " +
            //                   rigaPortafoglio.COM_MovimentoCassa.DataVersamento.ToShortDateString();
            //        }
            //        else if (rigaPortafoglio.COM_PayerPaymentRequest != null)
            //        {
            //            note = "Incasso PayER";
            //        }
            //        sb.AppendFormat("<p>Note: {0}</p>", note);
            //        sb.Append("</div>");
            //        msg.Body = sb.ToString();
            //        msg.To.Add(new MailAddress(rigaPortafoglio.COM_Portafoglio.COM_AnagraficaSoggetti.Email));
            //        msg.From = new MailAddress(ConfigUtility.EmailFromVerify);
            //        msg.Subject = string.Format("CRITER - Ricevuta di pagamento Acquisto Bollini Calore Pulito {0}",
            //            null);

            //        //allegato
            //        var reportRicevuta = ReportUtil.GetPdfRicevutaPagamento(rigaPortafoglio.IDMovimento);
            //        using (Stream stream = new MemoryStream(reportRicevuta))
            //        {
            //            Attachment attachFile = new Attachment(stream,
            //                string.Format("Ricevuta {0} {1}.pdf", codiceEnomeCertificatore,
            //                    rigaPortafoglio.IDMovimento.ToString().ToUpper()));
            //            msg.Attachments.Add(attachFile);
            //            //DEVO inviare prima di chiudere lo stream!!
            //            SmtpClient.Send(msg);
            //            //invio anche all'amministrazione
            //            if (ConfigUtility.DestinatariMailPerRicevutaPagamento.Length > 0)
            //            {
            //                msg.To.Clear();
            //                foreach (var dest in ConfigUtility.DestinatariMailPerRicevutaPagamento)
            //                {
            //                    msg.To.Add(new MailAddress(dest));
            //                }

            //                SmtpClient.Send(msg);
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //TODO fallito invio mail


            //}
        }

        #region Accertamenti
        public static void SendConfermaAccertamento(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = (from a in ctx.V_VER_Accertamenti
                                    where (a.IDAccertamento == iDAccertamento)
                                    select new
                                    {
                                        iDSoggetto = a.IDSoggetto,
                                        soggetto = a.NomeAzienda,
                                        emailPec = a.EmailPec,
                                        testoEmail = a.TestoEmail
                                    }).FirstOrDefault();

                MailMessage msg = new MailMessage();
                
                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += accertamento.testoEmail;
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(accertamento.emailPec));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Comunicazione conferma Accertamento";

                bool fSendEmail = SendPecMail(msg);

                if (fSendEmail)
                {
                    string destinatarioName = accertamento.soggetto;
                    string destinatarioSurname = string.Empty;
                    Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Accertamento;
                    Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PG;

                    UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                }
            }
        }

        public static void SendMailComunicazioneAccertamento(long IDAccertamento, int IDProceduraAccertamento)
        {
            try
            {
                if (IDProceduraAccertamento == 1 || IDProceduraAccertamento == 2 || IDProceduraAccertamento == 5 || IDProceduraAccertamento == 6)
                {
                    #region Email
                    using (var ctx = new CriterDataModel())
                    {
                        var accertamento = (from a in ctx.V_VER_Accertamenti
                                            where (a.IDAccertamento == IDAccertamento)
                                            select new
                                            {
                                                CodiceAccertamento = a.CodiceAccertamento,
                                                Comune = a.Comune,
                                                Indirizzo = a.Indirizzo,
                                                SiglaProvincia = a.SiglaProvincia,
                                                NomeResponsabile = a.NomeResponsabile,
                                                CognomeResponsabile = a.CognomeResponsabile,
                                                CodiceFiscaleResponsabile = a.CodiceFiscaleResponsabile,
                                                CodiceTargaturaImpianto = a.CodiceTargatura,
                                                EmailPecComune = a.EmailPecComune,
                                                IDRapportoControlloTecnico = a.IDRapportoDiControlloTecnicoBase,
                                                Distributore = a.Distributore,
                                                EmailPecDistributore = a.EmailPecDistributore,
                                                DataAccertamento = a.DataRilevazione,
                                                IDDistributore = a.IDDistributore,
                                                IDTipoAccertamento = a.IDTipoAccertamento
                                            }).FirstOrDefault();

                        bool fInviaDistributore = accertamento.IDDistributore != null ? true : false;
                        //if (IDProceduraAccertamento.ToString() == "1")
                        //{
                        //    fInviaDistributore = true;
                        //}
                        //else if (IDProceduraAccertamento.ToString() == "2")
                        //{
                        //    fInviaDistributore = false;
                        //}
                        //if (accertamento.IDDistributore != null)
                        //{
                        //    fInviaDistributore = true;
                        //}


                        MailMessage msg = new MailMessage();

                        string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                        mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                        mess += "<br/>";
                        mess += "Preg.mo Sindaco del Comune di <b>" + accertamento.Comune + "</b><br/>";
                        if (fInviaDistributore)
                        {
                            mess += "Spett.le Azienda distributrice di zona gas di rete <b>" + accertamento.Distributore + "</b><br/>";
                        }
                        mess += "Dal 1° Giugno 2017 è entrato in vigore il Regolamento regionale 3 aprile 2017 n. 1 che riporta le disposizioni regionali in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari.<br/><br/>";
                        mess += "Il citato regolamento disciplina:<br/><br/>";
                        mess += "•	le condizioni ed i limiti da rispettare nell’esercizio degli impianti termici per la climatizzazione invernale ed estiva degli edifici, e le relative responsabilità;<br/>";
                        mess += "•	le modalità e la frequenza di esecuzione degli interventi di manutenzione e controllo funzionale, e di efficienza energetica, degli impianti termici, e le relative responsabilità;<br/>";
                        mess += "•	il sistema di verifica del rispetto di tali prescrizioni, realizzato dalla Regione e basato su attività di accertamento ed ispezione degli impianti stessi, al fine di garantire la loro adeguata efficienza energetica e la riduzione delle emissioni inquinanti, la conformità alle norme vigenti ed il rispetto delle prescrizioni e degli obblighi stabiliti;<br/>";
                        mess += "•	il sistema di accreditamento dei soggetti a cui affidare le attività di accertamento ed ispezione;<br/>";
                        mess += "•	i criteri per la costituzione e la gestione del catasto regionale degli impianti termici (Criter). Il sistema prevede anche la targatura degli impianti, mediante rilascio di un codice univoco di riconoscimento da allegare al libretto di impianto.<br/>";
                        mess += "<br/>";
                        mess += "Per consentire la gestione del sistema Criter, la Regione ha istituito un apposito “Organismo regionale di accreditamento e di ispezione”, le cui funzioni sono affidate alla Società “in house” ART-ER S. cons. p. a. cui compete la implementazione, gestione e aggiornamento del sistema informativo regionale relativo agli impianti termici Criter e la realizzazione dei programmi di verifica periodica dell’efficienza energetica degli impianti termici.<br/><br/>";
                        switch (accertamento.IDTipoAccertamento)
                        {
                            case 1: //Accertamento RCT
                                mess += "Ai sensi dell’art. 17, comma 4 del regolamento sopracitato, si trasmette la comunicazione prevista relativa alla presenza di impianto pericoloso risultante dall’accertamento dei rapporti di controllo di efficienza energetica trasmessi al CRITER, per l’assunzione dei necessari provvedimenti di propria competenza<br/><br/>";
                                break;
                            case 2: //Accertamento Post Ispezione
                                mess += "Ai sensi dell’art. 20, comma 2 del regolamento sopracitato, si trasmette la comunicazione prevista relativa alla presenza di impianto pericoloso risultante dal rapporto di ispezione trasmesso al CRITER dall'agente accertatore, per l’assunzione dei necessari provvedimenti di propria competenza<br/><br/>";
                                break;
                        }

                        mess += "Distinti saluti<br/>";
                        mess += "Organismo Regionale di Accreditamento ed Ispezione";
                        mess += "</div>";

                        msg.IsBodyHtml = true;
                        msg.Body = mess;
                        msg.To.Add(new MailAddress(accertamento.EmailPecComune));
                        if (fInviaDistributore)
                        {
                            msg.To.Add(new MailAddress(accertamento.EmailPecDistributore));
                        }
                        msg.CC.Add(new MailAddress("criter@art-er.it"));

                        string pathP7m = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + accertamento.CodiceAccertamento + @"\";
                        string fileP7m = pathP7m + IDAccertamento + "_" + IDProceduraAccertamento + ".pdf.p7m";
                        if (File.Exists(fileP7m))
                        {
                            Attachment AttachmentP7m = new Attachment(fileP7m);
                            msg.Attachments.Add(AttachmentP7m);
                        }

                        msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);

                        switch (accertamento.IDTipoAccertamento)
                        {
                            case 1: //Accertamento RCT
                                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Comunicazione ai sensi art 17 del Regolamento Regionale 1/2017 - Segnalazione impianto termico pericoloso n. " + accertamento.CodiceAccertamento + " del " + string.Format("{0:dd/MM/yyyy}", accertamento.DataAccertamento);
                                break;
                            case 2: //Accertamento Post Ispezione
                                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Comunicazione ai sensi art 20 del Regolamento Regionale 1/2017 - Segnalazione impianto termico pericoloso n. " + accertamento.CodiceAccertamento + " del " + string.Format("{0:dd/MM/yyyy}", accertamento.DataAccertamento);
                                break;
                        }


                        bool fSendEmail = SendPecMail(msg);

                        if (fSendEmail)
                        {
                            string destinatarioName = "Comune di " + accertamento.Comune;
                            string destinatarioSurname = string.Empty;
                            Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Accertamento;
                            Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PG;

                            UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }           
        }

        #endregion

        #region Gestione Interventi Accertamenti
        public static void SendMailRevocaAccertamento(long IDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = (from a in ctx.V_VER_Accertamenti
                                    where (a.IDAccertamento == IDAccertamento)
                                    select new
                                    {
                                        DataRilevazione = a.DataRilevazione,
                                        CodiceAccertamento = a.CodiceAccertamento,
                                        Comune = a.Comune,
                                        Indirizzo = a.Indirizzo,
                                        SiglaProvincia = a.SiglaProvincia,
                                        NomeResponsabile = a.NomeResponsabile,
                                        CognomeResponsabile = a.CognomeResponsabile,
                                        CodiceFiscaleResponsabile = a.CodiceFiscaleResponsabile,
                                        CodiceTargaturaImpianto = a.CodiceTargatura,
                                        EmailPecComune = a.EmailPecComune,
                                        IDRapportoControlloTecnico = a.IDRapportoDiControlloTecnicoBase,
                                        Distributore = a.Distributore,
                                        EmailPecDistributore = a.EmailPecDistributore,
                                        IDDistributore = a.IDDistributore
                                    }).FirstOrDefault();

                bool fInviaDistributore = false;
                if (accertamento.IDDistributore != null)
                {
                    fInviaDistributore = true;
                }

                MailMessage msg = new MailMessage();
                
                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += "Preg.mo Sindaco del Comune di <b>" + accertamento.Comune + "</b><br/>";
                if (fInviaDistributore)
                {
                    mess += "Spett.le Azienda distributrice di zona gas di rete <b>" + accertamento.Distributore + "</b><br/>";
                }
                mess += "Si allega alla presente la comunicazione relativa alla revoca di segnalazione di non conformità di cui all’oggetto, in base alla quale si ritengono rimosse le condizioni di non conformità rilevate, venendo così a mancare le motivazioni per la disattivazione dell’impianto.<br/><br/>";
                mess += "Distinti saluti<br/>";
                mess += "Organismo Regionale di Accreditamento ed Ispezione";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(accertamento.EmailPecComune));
                if (fInviaDistributore)
                {
                    msg.To.Add(new MailAddress(accertamento.EmailPecDistributore));
                }
                
                string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + accertamento.CodiceAccertamento + @"\" + "RevocaInterventi_" + IDAccertamento.ToString() + ".pdf";
                if (File.Exists(pathPdfFile))
                {
                    Attachment AttachmentPdf = new Attachment(pathPdfFile);
                    msg.Attachments.Add(AttachmentPdf);
                }

                msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Revoca disposizioni di cui alla segnalazione di non conformità n. " + accertamento.CodiceAccertamento + " del " + String.Format("{0:dd/MM/yyyy}", accertamento.DataRilevazione);

                bool fSendEmail = SendPecMail(msg);

                if (fSendEmail)
                {
                    string destinatarioName = "Comune di " + accertamento.Comune;
                    string destinatarioSurname = string.Empty;
                    Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Accertamento;
                    Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PG;

                    UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                }
            }
        }

        public static void SendMailInterventiPresenzaNuovoRCT(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = (from a in ctx.V_VER_Accertamenti
                                    where (a.IDAccertamento == iDAccertamento)
                                    select new
                                    {
                                        CodiceTargaturaImpianto = a.CodiceTargatura,
                                        CodiceAccertamento = a.CodiceAccertamento
                                    }).FirstOrDefault();

                MailMessage msg = new MailMessage();
                SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += "E' stato inserito un nuovo rapporto di controllo con codice targatura <b>" + accertamento.CodiceTargaturaImpianto + "</b>";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]));
                
                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Nuovo RCT su accertamento n. " + accertamento.CodiceAccertamento;

                try
                {
                    smtpClient.Send(msg);
                }
                catch (HttpException ex)
                {

                }
            }
        }
        #endregion

        #region Ispezioni

        public static bool SendMailPerIspettore_RichiestaDisponibilitaPerIspezione(VER_IspezioneGruppoVerifica ispettoreCandidatoGruppoVerifica, bool usaPec)
        {
            string ore = UtilityApp.GetMinutesToHours(int.Parse(ConfigurationManager.AppSettings["IntervalloProssimoIspettoreMinuti"]));

            bool fInviata = false;
                       
            var smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());
            
            MailMessage msg = new MailMessage();

            string linkIspezione = ConfigurationManager.AppSettings["UrlPortal"] + "ConfermaIspezione.aspx" + ispettoreCandidatoGruppoVerifica.SecurityCode;
            
            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
            mess += "<br/><br/><br/>";
            mess += "Pregiatissimo <b>"+ ispettoreCandidatoGruppoVerifica.COM_AnagraficaSoggetti.Nome + " "+ ispettoreCandidatoGruppoVerifica.COM_AnagraficaSoggetti.Cognome + "</b>,<br />";
            mess += "l'Organismo di Accreditamento Regionale richiede la sua disponibilità per una serie di ispezioni sugli impianti termici censiti all’interno del Catasto Regionale degli Impianti termici (CRITER).<br/><br/>";
            mess += "Gli impianti per i quali è richiesta la disponibilità sono i seguenti:<br/><br/>";

            mess += "<table style=\"border:1px solid #ffcc3d; border-collapse:collapse; font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<tr>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Codice Ispezione</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Responsabile</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Indirizzo Impianto</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Città Impianto</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Provincia Impianto</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Potenza Impianto (kW)</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Combustibile</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Anno installazione</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Libretto di impianto compilato da</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Rapporto di controllo compilato da</b></th>";
            mess += "</tr>";
            using (var ctx = new CriterDataModel())
            {
                var ispezioni = ctx.V_VER_Ispezioni.Where(a => a.IDIspezioneVisita == ispettoreCandidatoGruppoVerifica.IDIspezioneVisita).ToList();
                foreach (var isp in ispezioni)
                {
                    mess += "<tr>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.CodiceIspezione +"</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.NomeResponsabile + " "+ isp.CognomeResponsabile + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.Indirizzo + " " + isp.Civico + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.Comune + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.SiglaProvincia + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.PotenzaTermicaUtileNominaleKw + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.TipologiaCombustibile + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + DateTime.Parse(isp.DataInstallazione.ToString()).Year + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.NomeAzienda + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.NomeAzienda + "</td>";
                    mess += "</tr>";
                }
            }
            mess += "</table><br/><br/>";

            mess += "Nel caso sia disponibile a svolgere la verifica ispettiva nel quadro delle condizioni contrattuali in essere e con le modalità pattuite, proceda entro <b>"+ ore +" ore</b> a confermare la presa in carico dell'ispezione operando le apposite funzioni del sistema CRITER";
            mess += "<br/><br/>" + linkIspezione + "<br/><br/>";
            mess += "Cordiali saluti<br/>";
            mess += "Organismo di Accreditamento ed Ispezione";
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            if (usaPec)
            {
                msg.To.Add(new MailAddress(ispettoreCandidatoGruppoVerifica.COM_AnagraficaSoggetti.EmailPec));
                msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
            }
            else
            {
                msg.To.Add(new MailAddress(ispettoreCandidatoGruppoVerifica.COM_AnagraficaSoggetti.Email));
                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            }
            
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Richiesta di disponibilità per visita ispettiva n. " + ispettoreCandidatoGruppoVerifica.IDIspezioneVisita + " censiti nel catasto";

            try
            {
                if (usaPec)
                {
                    bool fSendEmail = SendPecMail(msg);

                    if (fSendEmail)
                    {
                        string destinatarioName = ispettoreCandidatoGruppoVerifica.COM_AnagraficaSoggetti.Nome;
                        string destinatarioSurname = ispettoreCandidatoGruppoVerifica.COM_AnagraficaSoggetti.Cognome;
                        Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Ispezione;
                        Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PF;

                        UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                    }
                }
                else
                {
                    smtpClient.Send(msg);
                }
                
                fInviata = true;
            }
            catch (HttpException ex)
            {

            }
            
            return fInviata;
        }

        public static bool SendMailPerIspettore_PresaInCaricoIspezione(CriterDataModel ctx, VER_IspezioneGruppoVerifica ispettoreGruppoVerifica, bool usaPec)
        {
            string ore = UtilityApp.GetMinutesToHours(int.Parse(ConfigurationManager.AppSettings["IntervalloNoFirmaLaiIspezioneMinuti"]));

            bool fInviata = false;
                        
            var smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            MailMessage msg = new MailMessage();

            string linkIspezione = ConfigurationManager.AppSettings["UrlPortal"] + "ConfermaIspezioneIncarico.aspx" + ispettoreGruppoVerifica.SecurityCode;

            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
            mess += "<br/><br/><br/>";
            mess += "Pregiatissimo <b>" + ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Nome + " " + ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Cognome + "</b>,<br />";
            mess += "a seguito del ricevimento della sua conferma relativa all'affidamento di incarico relativo alla visita ispettiva n. "+ ispettoreGruppoVerifica.IDIspezioneVisita +", la informiamo che è ora necessario completare la relativa procedura. Le chiediamo quindi di collegarsi entro <b>"+ ore +" ore</b> al link sottostante per sottoscrivere digitalmente la lettera di incarico e quindi procedere alla pianificazione delle ispezioni di cui alla seguente tabella.<br/><br/>";
            mess += "Per sua comodità a scopo di consultazione preventiva, troverà in allegato alla presente email un fac-simile della lettera di incarico da sottoscrivere<br/><br/>";
            mess += "Al link di seguito indicato, oltre alla lettera di incarico da sottoscrivere, troverà disponibile la relativa documentazione, le check list ivi compreso il Rapporto.";
            mess += "<br/><br/>" + linkIspezione + "<br/><br/>";
            
            mess += "<table style=\"border:1px solid #ffcc3d; border-collapse:collapse; font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<tr>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Codice Ispezione</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Responsabile</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Indirizzo Impianto</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Città Impianto</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Provincia Impianto</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Potenza Impianto (kW)</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Combustibile</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Anno installazione</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Libretto di impianto compilato da</b></th>";
            mess += "<th style=\"border:1px solid #ffcc3d; background:#F0F0F0;\"><b>Rapporto di controllo compilato da</b></th>";
            mess += "</tr>";
            
                var ispezioni = ctx.V_VER_Ispezioni.Where(a => a.IDIspezioneVisita == ispettoreGruppoVerifica.IDIspezioneVisita).ToList();
                foreach (var isp in ispezioni)
                {
                    mess += "<tr>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.CodiceIspezione + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.NomeResponsabile + " " + isp.CognomeResponsabile + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.Indirizzo + " " + isp.Civico + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.Comune + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.SiglaProvincia + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.PotenzaTermicaUtileNominaleKw + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.TipologiaCombustibile + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + DateTime.Parse(isp.DataInstallazione.ToString()).Year + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.NomeAzienda + "</td>";
                    mess += "<td style=\"border:1px solid #ffcc3d;\">" + isp.NomeAzienda + "</td>";
                    mess += "</tr>";
                }
            
            mess += "</table><br/><br/>";
            mess += "Le ricordiamo che la programmazione delle ispezioni dovrà necessariamente avvenire entro e non oltre le successive 72 ore. Trascorso inutilmente tale periodo l'affidamento di incarico Le verrà annullato automaticamente<br/><br/>";

            mess += "Cordiali saluti<br/>";
            mess += "Organismo di Accreditamento ed Ispezione";
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            if (usaPec)
            {
                msg.To.Add(new MailAddress(ispettoreGruppoVerifica.COM_AnagraficaSoggetti.EmailPec));
                msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
            }
            else
            {
                msg.To.Add(new MailAddress(ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Email));
                msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            }

            string pathVisita = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + ispettoreGruppoVerifica.IDIspezioneVisita;
            string fileLAI = pathVisita + @"\" + "LetteraIncarico_" + ispettoreGruppoVerifica.IDIspezioneVisita + ".pdf";
            if (File.Exists(fileLAI))
            {
                Attachment AttachmentLAI = new Attachment(fileLAI);
                msg.Attachments.Add(AttachmentLAI);
            }
            
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Comunicazione di presa in carico ispezione n." + ispettoreGruppoVerifica.IDIspezioneVisita;

            try
            {
                if (usaPec)
                {
                    bool fSendEmail = SendPecMail(msg);

                    if (fSendEmail)
                    {
                        string destinatarioName = ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Nome;
                        string destinatarioSurname = ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Cognome;
                        Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Ispezione;
                        Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PF;

                        UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                    }
                }
                else
                {
                    smtpClient.Send(msg);
                }

                fInviata = true;
            }
            catch (HttpException ex)
            {

            }

            return fInviata;
        }

        public static bool SendMailPerIspettore_AnnullaIspezione(long iDIspezioneVisita, bool usaPec)
        {
            bool fInviata = false;
                        
            var smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            MailMessage msg = new MailMessage();

            using (var ctx = new CriterDataModel())
            {
                var ispettoreGruppoVerifica = ctx.VER_IspezioneGruppoVerifica.Where(a => a.IDIspezioneVisita == iDIspezioneVisita && a.fInGruppoVerifica == true).FirstOrDefault();
                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += "Spett. ispettore,<br />";
                mess += "l'Organismo di Accreditamento Regionale ha annullato l'ispezione in oggetto.<br/>";
                mess += "Cordiali saluti<br/>";
                mess += "Organismo di Accreditamento ed Ispezione";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                if (usaPec)
                {
                    msg.To.Add(new MailAddress(ispettoreGruppoVerifica.COM_AnagraficaSoggetti.EmailPec));
                    msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                }
                else
                {
                    msg.To.Add(new MailAddress(ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Email));
                    msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                }
                                
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Annullamento ispezione ";

                try
                {
                    if (usaPec)
                    {
                        bool fSendEmail = SendPecMail(msg);

                        if (fSendEmail)
                        {
                            string destinatarioName = ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Nome;
                            string destinatarioSurname = ispettoreGruppoVerifica.COM_AnagraficaSoggetti.Cognome;
                            Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Ispezione;
                            Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PF;

                            UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                        }
                    }
                    else
                    {
                        smtpClient.Send(msg);
                    }

                    fInviata = true;
                }
                catch (HttpException ex)
                {

                }
            }

            return fInviata;
        }
                
        public static void SendMailPerBackOffice_NotificaRimozioneGeneratoreDaVisita(VER_IspezioneVisitaInfo visitaIspettiva, LIM_LibrettiImpiantiGruppiTermici gruppoTermico, string motivoRimozione)
        {
            MailMessage msg = new MailMessage();
            SmtpClient smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());

            string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
            mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
            mess += "<br/>";
            mess += "Notifica di rimozione dal sistema Criter dalla visita Ispettiva n. "+ visitaIspettiva.IDIspezioneVisita + " il generatore " + gruppoTermico.Prefisso + " " + gruppoTermico.CodiceProgressivo +" con potenza di "+ gruppoTermico.PotenzaTermicaUtileNominaleKw +" kW appartenente al libretto con codice targatura <b>" + gruppoTermico.LIM_LibrettiImpianti.LIM_TargatureImpianti.CodiceTargatura + "</b>.";
            mess += "</div>";

            msg.IsBodyHtml = true;
            msg.Body = mess;
            msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]));
            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);

            string typeNotifica = string.Empty;
            if (motivoRimozione == "DismissioneGeneratore")
            {
                typeNotifica = "dismissione generatore";
            }
            else if (motivoRimozione == "AnnullamentoLibretto")
            {
                typeNotifica = "annullamento libretto";
            }
            msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Notifica di Rimozione generatore da visita per " + typeNotifica;

            try
            {
                smtpClient.Send(msg);
            }
            catch (HttpException ex)
            {

            }
        }

        public static void SendMailPecPerImpresaComunicazioneIspezione(long iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezione = (from a in ctx.V_VER_Ispezioni
                                    where (a.IDIspezione == iDIspezione)
                                    select new
                                    {
                                        IDIspezione = a.IDIspezione,
                                        NomeAzienda = a.NomeAzienda,
                                        EmailPecAzienda = a.EmailPec,
                                        CodiceTaragaturaImpianto = a.CodiceTargatura,
                                        IndirizzoImpianto = a.Indirizzo + " " + a.Civico + " " + a.Comune + "("+ a.SiglaProvincia+")",
                                        Responsabile = a.NomeResponsabile + " " + a.CognomeResponsabile,
                                        CodiceIspezione = a.CodiceIspezione,
                                        DataIspezione = a.DataIspezione,
                                        FasciaOraria = a.OrarioDa + " - "+ a.OrarioA,
                                        Ispettore = a.Ispettore,
                                        IDIspezioneVisita = a.IDIspezioneVisita,
                                        CodiceTargatura = a.CodiceTargatura,
                                        EmailIspettore = a.EmailIspettore,
                                        EmailPecIspettore = a.EmailPecIspettore,
                                        CellulareIspettore = a.CellulareIspettore
                                    }).FirstOrDefault();

                MailMessage msg = new MailMessage();
                
                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:12px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += "Spett.le  <b>" + ispezione.NomeAzienda + "</b><br/>";
                mess += "<br/>La presente per segnalarLe che:<br/><br/>";
                mess += "l’impianto installato nell’unità immobiliare di "+ ispezione.IndirizzoImpianto + " avente il codice targatura " + ispezione.CodiceTargatura + " di cui risulta responsabile "+ ispezione.Responsabile + " e, che al momento risulta in Vostra gestione, è oggetto di controllo con l’ispezione " + ispezione.CodiceIspezione + " in data " + String.Format("{0:dd/MM/yyyy}", ispezione.DataIspezione) + " nella fascia oraria compresa dalle " + ispezione.FasciaOraria + ".<br/><br/>";

                mess += "L’ispezione sarà condotta dall’ispettore " + ispezione.Ispettore + ", incaricato dall’Organismo Regionale di Accreditamento e Ispezione. L’ispettore potrebbe contattarVi qualora avesse bisogno di specifiche informazioni.Di seguito, i contatti dell’ispettore per eventuali chiarimenti:<br/><br/>";
                mess += "Email: "+ ispezione.EmailIspettore +"<br/>";
                mess += "PEC: " + ispezione.EmailPecIspettore + "<br/>";
                mess += "Telefono: " + ispezione.CellulareIspettore;

                mess += "<br/><br/>";
                mess += "Le attività di controllo sugli impianti termici in Emilia Romagna sono effettuate da ART-ER S.cons.pa per conto dell’Organismo Regionale di Accreditamento e Ispezione, in accordo al Regolamento Regionale 3 aprile 2017 n. 1 al fine di accertarne la conformità delle norme e l’effettivo stato di manutenzione e di esercizio.<br/><br/>";
                mess += "Nel caso in cui la potenza complessiva al focolare dell’impianto di cui sopra sia superiore a 232 kW, si rammenta che è obbligatoria la presenza all’ispezione anche del conduttore dell’impianto termico.<br/><br/><br/>";
                mess += "Distinti saluti<br/>";
                mess += "Organismo Regionale di Accreditamento ed Ispezione";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(ispezione.EmailPecAzienda));
                                
                msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Comunicazione avviso ispezione impianto in vostra gestione n." + ispezione.CodiceIspezione;

                try
                {
                    bool fSendEmail = SendPecMail(msg);

                    if (fSendEmail)
                    {
                        string destinatarioName = ispezione.NomeAzienda;
                        string destinatarioSurname = string.Empty;
                        Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Ispezione;
                        Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PG;

                        UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                    }
                }
                catch (HttpException ex)
                {

                }
            }
        }

        public static void SendMailPecPerTerzoResponsabileComunicazioneIspezione(long iDIspezione)
        {
            object[] DettaglioTerzoResponsabile = new object[2];
            DettaglioTerzoResponsabile[0] = false;
            DettaglioTerzoResponsabile[1] = string.Empty;

            using (var ctx = new CriterDataModel())
            {
                var ispezione = (from a in ctx.V_VER_Ispezioni
                                 where (a.IDIspezione == iDIspezione)
                                 select new
                                 {
                                     IDIspezione = a.IDIspezione,
                                     TerzoResponsabile = a.RagioneSocialeTerzoResponsabile,
                                     EmailPecTerzoResponsabile = a.EmailPecTerzoResponsabile,
                                     CodiceTaragaturaImpianto = a.CodiceTargatura,
                                     IndirizzoImpianto = a.Indirizzo + " " + a.Civico + " " + a.Comune + "(" + a.SiglaProvincia + ")",
                                     Responsabile = a.NomeResponsabile + " " + a.CognomeResponsabile,
                                     CodiceIspezione = a.CodiceIspezione,
                                     DataIspezione = a.DataIspezione,
                                     FasciaOraria = a.OrarioDa + " - " + a.OrarioA,
                                     Ispettore = a.Ispettore,
                                     IDIspezioneVisita = a.IDIspezioneVisita,
                                     CodiceTargatura = a.CodiceTargatura
                                 }).FirstOrDefault();

                MailMessage msg = new MailMessage();

                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:12px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += "Spett.le  <b>" + ispezione.TerzoResponsabile + "</b><br/>";
                mess += "<br/>La presente per segnalarLe che:<br/><br/>";
                mess += "l’impianto installato nell’unità immobiliare di " + ispezione.IndirizzoImpianto + " avente il codice targatura " + ispezione.CodiceTargatura + " di cui risultate nominato Terzo Responsabile è oggetto di controllo con l’ispezione " + ispezione.CodiceIspezione + " in data " + String.Format("{0:dd/MM/yyyy}", ispezione.DataIspezione) + " nella fascia oraria compresa dalle " + ispezione.FasciaOraria + ". Tale ispezione sarà effettuata dall’ispettore " + ispezione.Ispettore + " incaricato dall’Organismo Regionale di Accreditamento e Ispezione.<br/><br/>";
                mess += "Le attività di controllo sugli impianti termici in Emilia Romagna sono effettuate da ART-ER S.cons.pa per conto dell’Organismo Regionale di Accreditamento e Ispezione, in accordo al Regolamento Regionale 3 aprile 2017 n. 1 al fine di accertarne la conformità delle norme e l’effettivo stato di manutenzione e di esercizio.<br/><br/>";
                mess += "Nel caso in cui la potenza complessiva al focolare dell’impianto di cui sopra sia superiore a 232 kW, si rammenta che è obbligatoria la presenza all’ispezione anche del conduttore dell’impianto termico.<br/><br/>";
                mess += "Si allega alla presente l’avviso di programmazione dell’ispezione.<br/><br/><br/>";
                mess += "Distinti saluti<br/>";
                mess += "Organismo Regionale di Accreditamento ed Ispezione";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                msg.To.Add(new MailAddress(ispezione.EmailPecTerzoResponsabile));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + " Comunicazione avviso ispezione impianto in vostra gestione n." + ispezione.CodiceIspezione;

                string pathIspezione = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + ispezione.IDIspezioneVisita + @"\" + ispezione.CodiceIspezione;
                string fileIspezione = pathIspezione + @"\" + ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneConferma"] + "_" + ispezione.IDIspezione + ".pdf";

                if (File.Exists(fileIspezione))
                {
                    Attachment Attachment = new Attachment(fileIspezione);
                    msg.Attachments.Add(Attachment);
                }

                try
                {
                    bool fSendEmail = SendPecMail(msg);

                    if (fSendEmail)
                    {
                        DettaglioTerzoResponsabile[0] = true;
                        DettaglioTerzoResponsabile[1] = "Terzo Responsabile: " + ispezione.TerzoResponsabile + "<br/>Email terzo responsabile: " + ispezione.EmailPecTerzoResponsabile + "<br/>Data invio pec: " + DateTime.Now;
                        UtilityVerifiche.SaveIspezioneDettaglioPecTerzoResponsabile(iDIspezione, bool.Parse(DettaglioTerzoResponsabile[0].ToString()), DettaglioTerzoResponsabile[1].ToString());

                        string destinatarioName = ispezione.TerzoResponsabile;
                        string destinatarioSurname = string.Empty;
                        Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Ispezione;
                        Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PG;

                        UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                    }
                }
                catch (HttpException ex)
                {

                }
            }
        }

        public static bool SendMailPerIspettore_MancataChiusuraIspezione(long IDIspezione, bool usaPec)
        {
            bool fInviata = false;

            MailMessage msg = new MailMessage();

            using (var ctx = new CriterDataModel())
            {
                var datiIspezione = ctx.VER_Ispezione.Where(a => a.IDIspezione == IDIspezione).FirstOrDefault();
                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += "Spett. ispettore,<br />";
                mess += "l'Organismo di Accreditamento Regionale richiede la chiusura dell'ispezione in oggetto.<br/>";
                mess += "Cordiali saluti<br/>";
                mess += "Organismo di Accreditamento ed Ispezione";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                if (usaPec)
                {
                    msg.To.Add(new MailAddress(datiIspezione.COM_AnagraficaSoggetti.EmailPec));
                    msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                }
                else
                {
                    msg.To.Add(new MailAddress(datiIspezione.COM_AnagraficaSoggetti.Email));
                    msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                }

                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Avviso di chiusura ispezione n. " + datiIspezione.CodiceIspezione;

                try
                {
                    if (usaPec)
                    {
                        bool fSendEmail = SendPecMail(msg);
                    }
                    else
                    {
                        var smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());
                        smtpClient.Send(msg);
                    }

                    fInviata = true;
                }
                catch (HttpException ex)
                {

                }
            }

            return fInviata;
        }

        public static bool SendMailPerIspettore_NotificaRiaperturaIspezione(long IDIspezione, bool usaPec)
        {
            bool fInviata = false;

            MailMessage msg = new MailMessage();

            using (var ctx = new CriterDataModel())
            {
                var datiIspezione = ctx.VER_Ispezione.Where(a => a.IDIspezione == IDIspezione).FirstOrDefault();
                var datiNotificaAdIspettore = ctx.VER_IspezioneNotificaRiaperturaIspezione.Where(a => a.IDIspezione == IDIspezione).FirstOrDefault();
                var datiIspettore = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == datiIspezione.IDIspettore).FirstOrDefault();

                string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                mess += "<br/>";
                mess += datiNotificaAdIspettore.NotificaAdIspettore;
                mess += "<br/><br/>Cordiali saluti<br/>";
                mess += "Organismo di Accreditamento ed Ispezione";
                mess += "</div>";

                msg.IsBodyHtml = true;
                msg.Body = mess;
                if (usaPec)
                {
                    msg.To.Add(new MailAddress(datiIspettore.EmailPec));
                    msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                }
                else
                {
                    msg.To.Add(new MailAddress(datiIspettore.Email));
                    msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                }

                msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Avviso di riapertura ispezione n. " + datiIspezione.CodiceIspezione;

                try
                {
                    if (usaPec)
                    {
                        bool fSendEmail = SendPecMail(msg);
                    }
                    else
                    {
                        var smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());
                        smtpClient.Send(msg);
                    }

                    fInviata = true;
                }
                catch (HttpException ex)
                {

                }
            }

            return fInviata;
        }
        #endregion

        #region Procedura Automatica Cancellazioni Libretti Bozza/Revisioni
        public static void SendMailCancellazioneLibrettiBozza(List<LIM_LibrettiImpianti> LibrettiBozzaNotifyDto)
        {
            using (var ctx = new CriterDataModel())
            {
                var ListIDSoggetti = LibrettiBozzaNotifyDto.Select(r => r.IDSoggettoDerived).Distinct().ToList();
                foreach (var IDSoggetto in ListIDSoggetti)
                {
                    var ListIDLibrettiPerSoggetto = LibrettiBozzaNotifyDto.Where(a => a.IDSoggettoDerived == IDSoggetto).Select(r => r.IDLibrettoImpianto).ToList();
                    var ListLibrettiPerSoggetto = ctx.V_LIM_LibrettiImpianti.Where(a => ListIDLibrettiPerSoggetto.Contains(a.IDLibrettoImpianto)).ToList();
                    var CodiciTargatura = string.Join(",", ListLibrettiPerSoggetto.Select(t => t.CodiceTargatura));

                    var soggetto = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == IDSoggetto).FirstOrDefault();
                    if (soggetto != null)
                    {
                        MailMessage msg = new MailMessage();
                        string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                        mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                        mess += "<br/>";
                        mess += "Gentile impresa,<br/>";
                        mess += "Le comunichiamo che i libretti in stato di bozza saranno cancellati in automatico dal sistema trascorsi " + ConfigurationManager.AppSettings["DaysCancellazioneLibrettiBozzaRevisionati"] + " giorni dalla data del loro inserimento.<br/>";
                        mess += "Pertanto Le comunichiamo che tra " + ConfigurationManager.AppSettings["DaysEmailNotifyCancellazioneLibrettiBozzaRevisionati"] + " giorni i libretti con codici targatura " + CodiciTargatura + ", in assenza della conversione nello stato definitivo saranno eliminati dal sistema CRITER.<br/><br/><br/>";
                        mess += "Distinti saluti<br/>";
                        mess += "Organismo regionale di accreditamento ed ispezione";
                        mess += "</div>";

                        msg.IsBodyHtml = true;
                        msg.Body = mess;
                        msg.To.Add(new MailAddress(soggetto.EmailPec));

                        msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                        msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Avviso di cancellazione libretti di impianto in bozza";

                        bool fSendEmail = SendPecMail(msg);
                    }                    
                }                
            }
        }

        public static void SendMailCancellazioneLibrettiRevisionati(List<LIM_LibrettiImpianti> LibrettiRevisioneNotifyDto)
        {
            using (var ctx = new CriterDataModel())
            {
                var ListIDSoggetti = LibrettiRevisioneNotifyDto.Select(r => r.IDSoggettoDerived).Distinct().ToList();
                foreach (var IDSoggetto in ListIDSoggetti)
                {
                    var ListIDLibrettiPerSoggetto = LibrettiRevisioneNotifyDto.Where(a => a.IDSoggettoDerived == IDSoggetto).Select(r => r.IDLibrettoImpianto).ToList();
                    var ListLibrettiPerSoggetto = ctx.V_LIM_LibrettiImpianti.Where(a => ListIDLibrettiPerSoggetto.Contains(a.IDLibrettoImpianto)).ToList();
                    var CodiciTargatura = string.Join(",", ListLibrettiPerSoggetto.Select(t => t.CodiceTargatura));

                    var soggetto = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == IDSoggetto).FirstOrDefault();
                    if (soggetto != null)
                    {
                        MailMessage msg = new MailMessage();

                        string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                        mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                        mess += "<br/>";
                        mess += "Gentile impresa,<br/>";
                        mess += "Le comunichiamo che i libretti in stato di revisione saranno riportati nello versione precedente rispetto all’ultima data di revisione. Se il libretto è stato preso in carico da un altro soggetto impresa sarà assegnato all’impresa che aveva in gestione il libretto prima della presa in carico.<br/>";
                        mess += "Pertanto Le comunichiamo che tra " + ConfigurationManager.AppSettings["DaysEmailNotifyCancellazioneLibrettiBozzaRevisionati"] + " giorni i libretti con codici targatura " + CodiciTargatura + ", in assenza della conversione nello stato definitivo sarà riportato alla versione precedente.<br/><br/><br/>";
                        mess += "Distinti saluti<br/>";
                        mess += "Organismo regionale di accreditamento ed ispezione";
                        mess += "</div>";

                        msg.IsBodyHtml = true;
                        msg.Body = mess;
                        msg.To.Add(new MailAddress(soggetto.EmailPec));

                        msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                        msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Avviso di cancellazione libretti di impianto in revisione";

                        bool fSendEmail = SendPecMail(msg);
                    }
                }
            }
        }

        public static void SendMailCancellazioneRctBozza(List<RCT_RapportoDiControlloTecnicoBase> RctBozzaNotifyDto)
        {
            using (var ctx = new CriterDataModel())
            {
                var ListIDSoggetti = RctBozzaNotifyDto.Select(r => r.IDSoggetto).Distinct().ToList();
                foreach (var IDSoggetto in ListIDSoggetti)
                {
                    var ListIDRctPerSoggetto = RctBozzaNotifyDto.Where(a => a.IDSoggetto == IDSoggetto).Select(r => r.IDRapportoControlloTecnico).ToList();
                    var ListRctPerSoggetto = ctx.V_RCT_RapportiControlloTecnico.Where(a => ListIDRctPerSoggetto.Contains(a.IDRapportoControlloTecnico)).ToList();
                    var CodiciTargatura = string.Join(",", ListRctPerSoggetto.Select(t => t.CodiceTargatura));

                    var soggetto = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == IDSoggetto).FirstOrDefault();
                    if (soggetto != null)
                    {
                        MailMessage msg = new MailMessage();

                        string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                        mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                        mess += "<br/>";
                        mess += "Gentile impresa,<br/>";
                        mess += "Le comunichiamo che i rapporti di controllo in stato di bozza saranno cancellati in automatico dal sistema trascorsi " + ConfigurationManager.AppSettings["DaysCancellazioneRctBozza"] + " giorni dalla data del loro inserimento.<br/>";
                        mess += "Pertanto Le comunichiamo che tra " + ConfigurationManager.AppSettings["DaysEmailNotifyCancellazioneRctBozza"] + " giorni i rapporti di controllo con codici targatura " + CodiciTargatura + ", in assenza della conversione nello stato definitivo saranno eliminati dal sistema CRITER.<br/><br/><br/>";
                        mess += "Distinti saluti<br/>";
                        mess += "Organismo regionale di accreditamento ed ispezione";
                        mess += "</div>";

                        msg.IsBodyHtml = true;
                        msg.Body = mess;
                        msg.To.Add(new MailAddress(soggetto.EmailPec));

                        msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                        msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Avviso di cancellazione rapporti di controllo tecnico in bozza";

                        bool fSendEmail = SendPecMail(msg);
                    }
                }
            }
        }
        #endregion

        public static bool SendMailPerIspettore_SanzioniDaFirmare(long IDAccertamento, bool usaPec)
        {
            bool fInviata = false;

            MailMessage msg = new MailMessage();

            using (var ctx = new CriterDataModel())
            {
                var sanzione = ctx.VER_Accertamento.Where(a => a.IDAccertamento == IDAccertamento).FirstOrDefault();

                if (sanzione.IDIspezione != null)
                {
                    #region Send
                    var ispezione = ctx.VER_Ispezione.Where(a => a.IDIspezione == sanzione.IDIspezione).FirstOrDefault();
                    var ispettore = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == ispezione.IDIspettore).FirstOrDefault();

                    string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                    mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                    mess += "<br/>";
                    mess += "Gentile Agente accertatore " + ispettore.Nome + " " + ispettore.Cognome + ",<br />";
                    mess += "Con la presente, desideriamo portare alla Sua attenzione la necessità di provvedere alla firma del verbale di illecito amministrativo di cui all'oggetto.<br/><br/>";
                    mess += "Per procedere alla firma è necessario accedere alla sezione dell'applicativo “Sanzioni da firmare” e seguire le istruzioni che sono visualizzate.<br/><br/>";
                    mess += "Si sottolinea che i verbali di illecito amministrativo vengono redatti ai sensi del comma 3, art. 25 quindicies della Legge Regionale 26 dicembre 2014 e s.m. a seguito di constatazione di violazione delle vigenti norme in materia di esercizio, conduzione, manutenzione e controllo degli impianti termici di cui al Regolamento regionale n. 1/2017.<br/><br/><br/>";

                    mess += "Cordiali saluti<br/>";
                    mess += "Organismo Regionale di Accreditamento ed Ispezione";
                    mess += "</div>";

                    msg.IsBodyHtml = true;
                    msg.Body = mess;
                    if (usaPec)
                    {
                        msg.To.Add(new MailAddress(ispettore.EmailPec));
                        msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                    }
                    else
                    {
                        msg.To.Add(new MailAddress(ispettore.Email));
                        msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                    }

                    msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Notifica richiesta firma verbale illecito amministrativo N. " + sanzione.CodiceSanzione;

                    try
                    {
                        if (usaPec)
                        {
                            bool fSendEmail = SendPecMail(msg);

                            if (fSendEmail)
                            {
                                string destinatarioName = ispettore.Nome;
                                string destinatarioSurname = ispettore.Cognome;
                                Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Ispezione;
                                Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PF;

                                UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                            }
                        }
                        else
                        {
                            var smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());
                            smtpClient.Send(msg);
                        }

                        fInviata = true;
                    }
                    catch (HttpException ex)
                    {

                    }
                    #endregion
                }
            }

            return fInviata;
        }

        public static bool SendMailUfficioSanzioniRegione(long IDAccertamento, bool usaPec)
        {
            bool fInviata = false;

            MailMessage msg = new MailMessage();

            using (var ctx = new CriterDataModel())
            {
                var sanzione = ctx.V_VER_Accertamenti.AsNoTracking().Where(a => a.IDAccertamento == IDAccertamento).FirstOrDefault();

                if (sanzione.IDIspezione != null)
                {
                    string PathSanzione = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadSanzioni"] + @"\" + sanzione.CodiceAccertamento;

                    string FileVerbaleSanzionePdf = "VerbaleSanzione_" + sanzione.IDAccertamento + ".pdf";
                    string PathFileVerbaleSanzionePdf = PathSanzione + @"\" + FileVerbaleSanzionePdf;

                    string FileVerbaleSanzioneP7m = "VerbaleSanzione_" + sanzione.IDAccertamento + ".pdf.p7m";
                    string PathFileVerbaleSanzioneP7m = PathSanzione + @"\" + FileVerbaleSanzioneP7m;

                    var documento = UtilityVerifiche.GetDocumentiAccertamenti(IDAccertamento).FirstOrDefault(); //IL PRIMO VERBALE ACCERTAMENTO
                    string PathAccertamento = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + sanzione.CodiceAccertamento;
                    string FileVerbaleAccertamento = IDAccertamento + "_" + documento.IDProceduraAccertamento + ".pdf";
                    string PathFileVerbaleAccertamento = PathAccertamento + @"\" + FileVerbaleAccertamento;


                    string Responsabile = !string.IsNullOrEmpty(sanzione.NomeResponsabile) ? sanzione.NomeResponsabile + " " + sanzione.CognomeResponsabile : sanzione.RagioneSocialeResponsabile;

                    #region Send
                    string mess = "<div style=\"font-family:Arial, Helvetica, sans-serif; font-size:10px; background: #f5f9fc;\">";
                    mess += "<img src='" + ConfigurationManager.AppSettings["UrlPortal"] + "images/LogoCriter.png' />";
                    mess += "<br/>";
                    mess += "Pregiatissimi,<br />";
                    mess += "a seguito della contestazione della violazione prevista dal comma 3 dell’art. 25-quindecies della Legge Regionale n. 26 del 23 dicembre 2014 e s.m.i., nell'ambito delle attività di controllo sugli impianti di climatizzazione di cui al Regolamento Regionale n. 1/2017 s.m.i., elevata nei confronti di:<br/><br/>";
                    mess += "<b>" + Responsabile + "</b><br/>";
                    mess += "<b>CF: " + sanzione.CodiceFiscaleResponsabile;
                    if (!string.IsNullOrEmpty(sanzione.PartitaIvaResponsabile))
                    {
                        mess += "<br/> PIVA: " + sanzione.PartitaIvaResponsabile + "</b>";
                    }
                    
                    mess += "<br/><br/>si comunica che:<br/>";
                    mess += "•	in data odierna è stato inviato, mediante PEC, il Verbale di Accertamento di illecito amministrativo n. "+ sanzione.CodiceSanzione + ".<br/>";
                    mess += "•  tale Verbale di Accertamento, in conformità alla normativa vigente, riportava:<br/>";
                    mess += "    - La facoltà di pagamento in misura ridotta entro il termine perentorio di 60 giorni dalla data di notificazione del verbale<br/>";
                    mess += "    - La possibilità, per il trasgressore, di presentare scritti difensivi o memorie giustificative entro il termine perentorio di 30 giorni dalla notificazione del verbale.<br/>";
                    mess += "• Alla data odierna risultano trascorsi i termini sopra indicati, senza che il trasgressore abbia esercitato alcuna delle azioni previste<br/><br/>";
                    mess += "Pertanto,<br/>"; 
                    mess += "in conformità a quanto disposto dal comma 9 dell’art. 24 del Regolamento Regionale n. 1/2017, si trasmette la documentazione allegata per l’avvio della procedura di ingiunzione di Vostra competenza.<br/>";
                    mess += "<br/><br/><br/>";
                    mess += "Cordiali saluti<br/>";
                    mess += "Organismo Regionale di Accreditamento ed Ispezione - ART-ER S.c.p.a.";
                    mess += "<br/><br/><br/>";
                    mess += "ALLEGATI:<br/>";
                    mess += "- VERBALE ILLECITO AMMINISTRATIVO " + FileVerbaleSanzionePdf + "<br/>";
                    mess += "- VERBALE ILLECITO AMMINISTRATIVO FIRMATO DIGITALMENTE "+ FileVerbaleSanzioneP7m + "<br/>";
                    mess += "- VERBALE DI ACCERTAMENTO " + FileVerbaleAccertamento + "<br/>";
                    mess += "</div>";

                    msg.IsBodyHtml = true;
                    msg.Body = mess;
                    if (usaPec)
                    {
                        msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["PecUfficioSanzioniRegione"]));
                        msg.From = new MailAddress(ConfigurationManager.AppSettings["PecSmtpServerEmail"]);
                        msg.CC.Add(new MailAddress("criter@art-er.it"));
                    }

                    if (File.Exists(PathFileVerbaleSanzionePdf))
                    {
                        Attachment AttachmentVerbaleSanzionePdf = new Attachment(PathFileVerbaleSanzionePdf);
                        msg.Attachments.Add(AttachmentVerbaleSanzionePdf);
                    }

                    if (File.Exists(PathFileVerbaleSanzioneP7m))
                    {
                        Attachment AttachmentVerbaleSanzioneP7m = new Attachment(PathFileVerbaleSanzioneP7m);
                        msg.Attachments.Add(AttachmentVerbaleSanzioneP7m);
                    }

                    if (File.Exists(PathFileVerbaleAccertamento))
                    {
                        Attachment AttachmentVerbaleSanzioneVerbaleAccertamento = new Attachment(PathFileVerbaleAccertamento);
                        msg.Attachments.Add(AttachmentVerbaleSanzioneVerbaleAccertamento);
                    }

                    msg.Subject = ConfigurationManager.AppSettings["EmailSubject"] + " - " + "Trasmissione di documentazione relativa a irrogazione di sanzione amministrativa ai sensi al comma 3 dell’art.25-quindecies della Legge Regionale 26 del 23 dicembre 2014 e s.m.i. - Verbale di illecito amministrativo n. " + sanzione.CodiceSanzione;

                    try
                    {
                        if (usaPec)
                        {
                            bool fSendEmail = SendPecMail(msg);

                            if (fSendEmail)
                            {
                                string destinatarioName = "Ufficio Sanzioni Amministrative";
                                string destinatarioSurname = "Regione Emilia-Romagna";
                                Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum = Pomiager.Service.Parer.TypeEmailEnum.Ispezione;
                                Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum = Pomiager.Service.Parer.TypePecToTypeEnum.PG;

                                UtilityParer.SendToParer(msg, destinatarioName, destinatarioSurname, typeEmailEnum, typePecToEnum);
                            }
                        }
                        else
                        {
                            var smtpClient = TypeSmtpClient(ConfigurationManager.AppSettings["SmtpTypeClient"].ToString());
                            smtpClient.Send(msg);
                        }

                        fInviata = true;
                    }
                    catch (HttpException ex)
                    {

                    }
                    #endregion
                }
            }

            return fInviata;
        }
    }
}