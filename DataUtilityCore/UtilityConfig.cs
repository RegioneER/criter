using System;
using System.Configuration;
using System.Globalization;

namespace DataUtilityCore
{
    public static class UtilityConfig
    {
        public static bool OnlineMode
        {
            get
            {
 
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["OnlineMode"]) && ConfigurationManager.AppSettings["OnlineMode"] == "on")
                    return true;

                return false;
            }
        }
        
        public static int CommandTimeOutTime
        {
            get
            {
                var v = ConfigurationManager.AppSettings["CommandTimeOutTime"];
                if (!string.IsNullOrEmpty(v))
                {

                    int vInt;
                    if (int.TryParse(v, out vInt))
                    {
                        return vInt;
                    }
                }

                return 30; //default, se non ho messo nella config
            }
        }
        
        public static string UrlPortafoglioUtente
        {
            get
            {
                return GetPageFullUrl("MNG_Portafoglio.aspx");
            }
        }
       
        public static string GetPageFullUrl(string relativeUrl)
        {
            var homePage = ConfigurationManager.AppSettings["UrlPortal"];
            if (!homePage.EndsWith("/"))
                homePage += "/";

            return string.Concat(homePage, relativeUrl);
        }

        #region Gestione percorsi di salvataggio attestati (NUOVA VERSIONE 2016 Simatica)
        public static string PathSalvataggioDati
        {
            get
            {
                return ConfigurationManager.AppSettings["PathSalvataggioDati"];
            }
        }

        public static string LinkSalvataggioDati
        {
            get
            {
                return ConfigurationManager.AppSettings["LinkSalvataggioDati"];
            }
        }
        #endregion

        #region Percorsi di salvataggio vecchi Pomiager
        public static string UploadRapportiControllo
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadRapportiControllo"];
            }
        }


        public static string LinkRapporti
        {
            get
            {
                return ConfigurationManager.AppSettings["LinkRapporti"];
            }
        }

        public static string PathXmlFile
        {
            get
            {
                return ConfigurationManager.AppSettings["PathXmlFile"];
            }
        }

        public static string PathP7mFile
        {
            get
            {
                return ConfigurationManager.AppSettings["PathP7mFile"];
            }
        }

        public static string LinkP7m
        {
            get
            {
                return ConfigurationManager.AppSettings["LinkP7m"];
            }
        }

        #endregion
        
        public static string EmailFromVerify
        {
            get { return ConfigurationManager.AppSettings["EmailFromVerify"]; }
        }
        
        public static string [] DestinatariMailPerRicevutaPagamento
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DestinatariMailPerRicevutaPagamento"]))
                {
                    var email = ConfigurationManager.AppSettings["DestinatariMailPerRicevutaPagamento"]; //possono essere separate da virgola
                    if (email.Contains(","))
                    {
                        return email.Split(',');
                    }
                    else
                    {
                        return new[] {email}; //solo una
                    }
                }
                 return new string[0] ;
            }
        }

        public static string PayerPrefissoNumeroDocumento
        {
            get { return ConfigurationManager.AppSettings["PayerPrefissoNumeroDocumento"]; }
        }

        public static bool PayerAbilitato
        {
            get
            {
                bool v; 
                bool.TryParse( ConfigurationManager.AppSettings["PayerAbilitato"], out v);
                return v;
            }
        }

        public static decimal CostoBollino
        {
            get
            {
                var v = ConfigurationManager.AppSettings["CostoBollino"];
                if (!string.IsNullOrEmpty(v))
                {

                    int vInt;
                    if (int.TryParse(v, out vInt))
                    {
                        return vInt;
                    }
                }

                return 7; //default, se non ho messo nella config }
            }
        }

               
    }
}