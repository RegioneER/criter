using System;
using System.Linq;
using System.Web;
using DataLayer;

namespace DataUtilityCore
{
    public class Logger
    {
        public static void LogNote(TipoEvento TipoEvento, TipoOggetto TipoOggetto, string Note)
        {
            LogIt(TipoEvento, TipoOggetto, null, null, Note);
        }

        public static void LogNote(TipoEvento TipoEvento, string Note)
        {
            LogIt(TipoEvento, TipoOggetto.Nulla, null, null, Note);
        }

        public static void LogIt(TipoEvento TipoEvento)
        {
            LogIt(TipoEvento, TipoOggetto.Nulla, null, null, null);
        }

        public static void LogIt(TipoEvento TipoEvento, TipoOggetto TipoOggetto, string NomeOggetto)
        {
            LogIt(TipoEvento, TipoOggetto, NomeOggetto, null, null);
        }

        public static void LogIt(TipoEvento TipoEvento, TipoOggetto TipoOggetto, string NomeOggetto, object ID)
        {
            LogIt(TipoEvento, TipoOggetto, NomeOggetto, ID, null);
        }

        public static void LogIt(TipoEvento TipoEvento, TipoOggetto TipoOggetto, string NomeOggetto, object ID, string Note)
        {
            LogInfo l = new LogInfo(TipoEvento, TipoOggetto, NomeOggetto, ID, Note);
            LogInternal(l);
        }

        public static void LogUser(TipoEvento TipoEvento, string NomeOggetto, string UserName, string indirizzoIP)
        {
            LogInfo l = new LogInfo(TipoEvento, TipoOggetto.Nulla, NomeOggetto, UserName, "alcune informazioni (HostName) non recuperabili in questo contesto");
            l.HostName = null;
            l.IndirizzoIP = indirizzoIP;

            LogInternal(l);
        }


        private static void LogInternal(LogInfo l)
        {
            //var db = DataLayer.Common.ApplicationContext.Current.Context;
            using (var ctx = new CriterDataModel())
            {
                string username = string.Empty;
                string tipoEvento = string.Empty;
                string hostName = string.Empty;
                string indirizzoIp = string.Empty;
                string tipoOggetto = string.Empty;
                string nomeOggetto = string.Empty;
                string id = string.Empty;
                string note = string.Empty;

                if (l.UserName != null)
                {
                    username = l.UserName;
                }
                if (l.TipoEvento != null)
                {
                    tipoEvento = l.TipoEvento.ToString();
                }
                if (l.HostName != null)
                {
                    hostName = l.HostName;
                }
                if (l.IndirizzoIP != null)
                {
                    indirizzoIp = l.IndirizzoIP;
                }
                if (l.TipoOggetto != null)
                {
                    tipoOggetto = l.TipoOggetto;
                }
                if (l.NomeOggetto != null)
                {
                    nomeOggetto = l.NomeOggetto;
                }
                if (l.ID != null)
                {
                    id = l.ID;
                }
                if (l.Note != null)
                {
                    note = l.Note;
                }
                var sp = ctx.sp_upLogIt(username,
                                       tipoEvento,
                                       hostName,
                                       indirizzoIp,
                                       tipoOggetto,
                                       nomeOggetto,
                                       id,
                                       note);
            }
        }
    }

    public enum TipoEvento
    {
        LoginSpid,
        Login,
        Logout,
        LoginFallito,
        LetturaDati,
        ModificaDati,
        InserimentoDati,
        ModificaAmministrativa,
        ErroreApplicativo
    }

    public enum TipoOggetto
    {
        Pagina,
        Tabella,
        Nulla
    }

    public class LogInfo
    {
        public LogInfo(TipoEvento tipoEvento, TipoOggetto tipoOggetto, string nomeOggetto, object id, string note)
        {
            if (HttpContext.Current.User != null && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                UserName = HttpContext.Current.User.Identity.Name;
            }
            else
            {
                if (id != null)
                {
                    UserName = id.ToString();
                }
            }
            if (HttpContext.Current.Request.UserHostName != null)
                HostName = HttpContext.Current.Request.UserHostName;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["RERFwFor"])) //(HttpContext.Current.Request.UserHostAddress != null)
            {
                IndirizzoIP = HttpContext.Current.Request.Headers["RERFwFor"];
            }
            else
            {
                IndirizzoIP = HttpContext.Current.Request.UserHostAddress;
            }
                

            this.TipoEvento = tipoEvento;
            this.TipoOggetto = (tipoOggetto == DataUtilityCore.TipoOggetto.Nulla) ? null : tipoOggetto.ToString();
            this.NomeOggetto = nomeOggetto;
            //if (id != null)
            //{
            //    this.ID = id.ToString();
            //}

            this.Note = note;
        }

        public TipoEvento TipoEvento { get; set; }
        public string TipoOggetto { get; set; }
        public string NomeOggetto { get; set; }
        public string UserName { get; set; }
        public string HostName { get; set; }
        public string IndirizzoIP { get; set; }
        public string ID { get; set; }
        public string Note { get; set; }
    }
}