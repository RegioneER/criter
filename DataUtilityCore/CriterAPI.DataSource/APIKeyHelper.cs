using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriterAPI.DataSource
{
    public class APIKeyHelper
    {
        public static bool IsKeyValid(string CriterAPIKey)
        {
            bool fok = false;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var utente = ctx.COM_Utenti.Where(r => r.KeyApi == CriterAPIKey).ToList();
                if (utente.Count == 1)
                {
                    fok = true;
                }
            }

            return fok;
        }

        public static bool IsCodiceSoggettoValid(string CodiceSoggetto, string CriterAPIKeySoggetto)
        {
            bool fok = false;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var anagrafica = (from a in ctx.COM_AnagraficaSoggetti
                                  join l in ctx.COM_Utenti on a.IDSoggetto equals l.IDSoggetto
                                  where (a.CodiceSoggetto == CodiceSoggetto.Substring(0,6) && l.KeyApi == CriterAPIKeySoggetto)
                                  select new
                                  {
                                      IDSoggetto = a.IDSoggetto,
                                      IDSoggettoDerived = a.IDSoggettoDerived,
                                      IDUtente = l.IDUtente
                                  }).Any();
                
                if (anagrafica)
                {
                    fok = true;
                }
            }

            return fok;
        }

        public static bool IsCodiceTargaturaImpiantoValid(object CodiceTargatura)
        {
            bool fok = false;
            if (CodiceTargatura != null)
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var targatura = (from a in ctx.LIM_LibrettiImpianti
                                     join c in ctx.LIM_TargatureImpianti on a.IDTargaturaImpianto equals c.IDTargaturaImpianto
                                     where c.CodiceTargatura == CodiceTargatura.ToString() && a.fAttivo == true
                                     select a.IDTargaturaImpianto).SingleOrDefault();

                    if (!targatura.HasValue)
                    {
                        fok = true;
                    }
                }
            }
            else
            {
                fok = true;
            }

            return fok;
        }

        public static bool IsCodiceTargaturaImpiantoLibrettoDefinitivoValid(string CodiceTargatura)
        {
            bool fok = false;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var targatura = (from a in ctx.LIM_LibrettiImpianti
                                 join c in ctx.LIM_TargatureImpianti on a.IDTargaturaImpianto equals c.IDTargaturaImpianto
                                 where c.CodiceTargatura == CodiceTargatura && a.IDStatoLibrettoImpianto == 2 && a.fAttivo == true
                                 select a.IDTargaturaImpianto).FirstOrDefault();

                if (targatura.HasValue)
                {
                    fok = true;
                }
            }

            return fok;
        }


    }    
}