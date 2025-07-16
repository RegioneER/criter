using Criter.Libretto;
using Criter.Rapporti;
using DataLayer;
using DataUtilityCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;

namespace CriterAPI.Entity
{
    public class CriterEntity
    {
        public static object[] GetIDSoggettiFromCodiceSoggetto(string codiceSoggetto)
        {
            object[] outVal = new object[3];
            outVal[0] = null;  //IDSoggetto
            outVal[1] = null;  //IDSoggettoDerived
            outVal[2] = null;  //IDUtente

            if (!string.IsNullOrEmpty(codiceSoggetto))
            {
                char[] separator = new char[1] { '-' };
                string[] codiciSoggetti = codiceSoggetto.Split(separator);

                using (var ctx = new CriterDataModel())
                {
                    if (codiciSoggetti.Count() == 2) //Manutentore
                    {
                        var manutentore = (from a in ctx.COM_AnagraficaSoggetti
                                           join l in ctx.COM_Utenti on a.IDSoggetto equals l.IDSoggetto
                                           where (a.CodiceSoggetto == codiceSoggetto & (a.IDTipoSoggetto == 1 || a.IDTipoSoggetto == 4))
                                           select new
                                           {
                                               iDSoggetto = a.IDSoggetto,
                                               iDSoggettoDerived = a.IDSoggettoDerived,
                                               iDUtente = l.IDUtente
                                           }).Take(1).SingleOrDefault();

                        outVal[0] = manutentore.iDSoggetto;
                        outVal[1] = manutentore.iDSoggettoDerived;
                        outVal[2] = manutentore.iDUtente;
                    }
                    else
                    {
                        string codiceAzienda = codiciSoggetti[0].ToString();
                        var azienda = (from a in ctx.COM_AnagraficaSoggetti
                                       join l in ctx.COM_Utenti on a.IDSoggetto equals l.IDSoggetto
                                       where (a.CodiceSoggetto == codiceAzienda & a.IDTipoSoggetto == 2
                                       )
                                       select new
                                       {
                                           iDSoggetto = a.IDSoggetto,
                                           iDSoggettoDerived = a.IDSoggettoDerived,
                                           iDUtente = l.IDUtente
                                       }).SingleOrDefault();

                        outVal[0] = azienda.iDSoggettoDerived.ToString();
                        outVal[1] = azienda.iDSoggetto.ToString();
                        outVal[2] = azienda.iDUtente.ToString();
                    }
                }
            }

            return outVal;
        }

        public static int? GetIDTargaturaImpiantoFromCodiceTargatura(string CodiceTargatura, int iDSoggetto, string type)
        {
            int? IDTargaturaImpianto = null;

            if (!string.IsNullOrEmpty(CodiceTargatura))
            {
                using (var ctx = new CriterDataModel())
                {
                    if (type == "LIB")
                    {
                        var targaturaForLibretto = (from a in ctx.LIM_TargatureImpianti
                                                 where (a.CodiceTargatura == CodiceTargatura && a.IDSoggetto == iDSoggetto)
                                                 select new
                                                 {
                                                     iDTargaturaImpianto = a.IDTargaturaImpianto
                                                 }
                                                 ).FirstOrDefault();

                        if (targaturaForLibretto != null)
                        {
                            IDTargaturaImpianto = targaturaForLibretto.iDTargaturaImpianto;
                        }
                    }
                    else
                    {
                        var targaturaForRct = (from a in ctx.LIM_TargatureImpianti
                                            where (a.CodiceTargatura == CodiceTargatura)
                                            select new
                                            {
                                                iDTargaturaImpianto = a.IDTargaturaImpianto
                                            }
                                            ).FirstOrDefault();

                        if (targaturaForRct != null)
                        {
                            IDTargaturaImpianto = targaturaForRct.iDTargaturaImpianto;
                        }
                    }
                }
            }

            return IDTargaturaImpianto;
        }

        public static int? GetIDStatoLibrettoImpiantoByCodiceTargatura(string CodiceTargatura)
        {
            int? iDStato = null;

            if (!string.IsNullOrEmpty(CodiceTargatura))
            {
                using (var ctx = new CriterDataModel())
                {
                    var libretto = (from a in ctx.LIM_LibrettiImpianti
                                    join l in ctx.LIM_TargatureImpianti on a.IDTargaturaImpianto equals l.IDTargaturaImpianto
                                    where (l.CodiceTargatura == CodiceTargatura && a.fAttivo == true)
                                    select new
                                    {
                                        IDStatoLibrettoImpianto = a.IDStatoLibrettoImpianto
                                    }
                                    ).FirstOrDefault();

                    if (libretto != null)
                    {
                        iDStato = libretto.IDStatoLibrettoImpianto;
                    }
                }
            }

            return iDStato;
        }

        public static int? GetIDLibrettoImpiantoByCodiceTargatura(string CodiceTargatura)
        {
            int? iDLibrettoImpianto = null;

            if (!string.IsNullOrEmpty(CodiceTargatura))
            {
                using (var ctx = new CriterDataModel())
                {
                    var targatura = (from a in ctx.LIM_TargatureImpianti
                                     where (a.CodiceTargatura == CodiceTargatura)
                                    select new
                                    {
                                        IDTargaturaImpianto = a.IDTargaturaImpianto
                                    }
                                    ).FirstOrDefault();

                    if (targatura != null)
                    {
                        var libretto = (from a in ctx.LIM_LibrettiImpianti
                                        where (a.IDTargaturaImpianto == targatura.IDTargaturaImpianto && a.fAttivo == true)
                                        select new
                                        {
                                            IDLibrettoImpianto = a.IDLibrettoImpianto
                                        }
                                        ).FirstOrDefault();

                        if (libretto != null)
                        {
                            iDLibrettoImpianto = libretto.IDLibrettoImpianto;
                        }
                    }
                }
            }

            return iDLibrettoImpianto;
        }

        public static int? GetIDStatoRapportoDiControlloByGuid(string guidRapporto)
        {
            int? iDStato = null;

            if (!string.IsNullOrEmpty(guidRapporto))
            {
                using (var ctx = new CriterDataModel())
                {
                    var rapporto = (from a in ctx.RCT_RapportoDiControlloTecnicoBase
                                    where (a.GuidRapportoTecnico == guidRapporto)
                                    select new
                                    {
                                        IDStatoRapportoDiControllo = a.IDStatoRapportoDiControllo
                                    }
                                    ).FirstOrDefault();

                    if (rapporto != null)
                    {
                        iDStato = rapporto.IDStatoRapportoDiControllo;
                    }
                }
            }

            return iDStato;
        }

        public static long? GetIDRapportoControlloTecnicoByGuid(string guidRapporto)
        {
            long? iDRapportoControlloTecnico = null;

            if (!string.IsNullOrEmpty(guidRapporto))
            {
                using (var ctx = new CriterDataModel())
                {
                    var rapporto = (from a in ctx.RCT_RapportoDiControlloTecnicoBase
                                    where (a.GuidRapportoTecnico == guidRapporto)
                                    select new
                                    {
                                        IDRapportoControlloTecnico = a.IDRapportoControlloTecnico
                                    }
                                    ).FirstOrDefault();

                    if (rapporto != null)
                    {
                        iDRapportoControlloTecnico = rapporto.IDRapportoControlloTecnico;
                    }
                }
            }

            return iDRapportoControlloTecnico;
        }

        public static int? GetIDCodiceCatastaleFromCodiceCatastale(string CodiceCatastale)
        {
            int? iDCodiceCatastale = null;

            if (!string.IsNullOrEmpty(CodiceCatastale))
            {
                using (var ctx = new CriterDataModel())
                {
                    var catasto = (from a in ctx.SYS_CodiciCatastali
                                   where (a.CodiceCatastale == CodiceCatastale & a.fAttivo == true)
                                   select new
                                   {
                                       IDCodiceCatastale = a.IDCodiceCatastale
                                   }).FirstOrDefault();
                    if (catasto != null)
                    {
                        iDCodiceCatastale = catasto.IDCodiceCatastale;
                    }
                }
            }
            return iDCodiceCatastale;
        }

        public static bool CheckCodiceCatastaleRERIsValid(string CodiceCatastale)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(CodiceCatastale))
            {
                using (var ctx = new CriterDataModel())
                {
                    var catasto = (from a in ctx.SYS_CodiciCatastali
                                   join l in ctx.SYS_Province on a.IDProvincia equals l.IDProvincia
                                   where (a.CodiceCatastale == CodiceCatastale & a.fAttivo == true & l.IDRegione == 5)
                                   select new
                                   {
                                       IDCodiceCatastale = a.IDCodiceCatastale
                                   }).FirstOrDefault();

                    if (catasto != null)
                    {
                        isValid = true;
                    }
                }
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        public static int? GetIDCodiceCatastaleSezioneFromCodiceCatastale(string CodiceCatastale, string CodiceSezione)
        {
            int? iDCodiceCatastaleSezione = null;

            if (!string.IsNullOrEmpty(CodiceCatastale) && !string.IsNullOrEmpty(CodiceSezione))
            {
                using (var ctx = new CriterDataModel())
                {
                    var catasto = (from a in ctx.SYS_CodiciCatastaliSezioni
                                   join l in ctx.SYS_CodiciCatastali on a.IDCodiceCatastale equals l.IDCodiceCatastale
                                   where (a.CodiceSezione == CodiceSezione & l.CodiceCatastale == CodiceCatastale)
                                   select new
                                   {
                                       IDCodiceCatastaleSezione = a.IDCodiceCatastaleSezione
                                   }).FirstOrDefault();
                    if (catasto != null)
                    {
                        iDCodiceCatastaleSezione = catasto.IDCodiceCatastaleSezione;
                    }
                }
            }
            return iDCodiceCatastaleSezione;
        }

        public static string GetComuneFromCodiceCatastale(string CodiceCatastale)
        {
            string Comune = null;

            if (!string.IsNullOrEmpty(CodiceCatastale))
            {
                using (var ctx = new CriterDataModel())
                {
                    var catasto = (from a in ctx.SYS_CodiciCatastali
                                   where (a.CodiceCatastale == CodiceCatastale & a.fAttivo == true)
                                   select new
                                   {
                                       Comune = a.Comune
                                   }).FirstOrDefault();

                    Comune = catasto.Comune;
                }
            }

            return Comune;
        }

        public static int? GetIDProvinciaFromCodiceCatastale(string CodiceCatastale)
        {
            int? iDProvincia = null;

            if (!string.IsNullOrEmpty(CodiceCatastale))
            {
                using (var ctx = new CriterDataModel())
                {
                    var catasto = (from a in ctx.SYS_CodiciCatastali
                                   where (a.CodiceCatastale == CodiceCatastale)
                                   select new
                                   {
                                       iDProvincia = a.IDProvincia
                                   }).FirstOrDefault();

                    if (catasto != null)
                    {
                        iDProvincia = catasto.iDProvincia;
                    }
                }
            }

            return iDProvincia;
        }

        public static int? GetIDCodiceCatastaleFromCodiceTargaturaImpianto(string CodiceTargatura)
        {
            int? iDCodiceCatastale = null;

            if (!string.IsNullOrEmpty(CodiceTargatura))
            {
                using (var ctx = new CriterDataModel())
                {
                    var catasto = (from a in ctx.SYS_CodiciCatastali
                                   join l in ctx.LIM_LibrettiImpianti on a.IDCodiceCatastale equals l.IDCodiceCatastale
                                   join t in ctx.LIM_TargatureImpianti on l.IDTargaturaImpianto equals t.IDTargaturaImpianto
                                   where (t.CodiceTargatura == CodiceTargatura & a.fAttivo == true)
                                   select new
                                   {
                                       IDCodiceCatastale = a.IDCodiceCatastale
                                   }).FirstOrDefault();
                    if (catasto != null)
                    {
                        iDCodiceCatastale = catasto.IDCodiceCatastale;
                    }
                }
            }
            return iDCodiceCatastale;
        }

        public static int? GetIDLibrettoImpiantoFromCodiceTargaturaImpianto(string CodiceTargatura)
        {
            int? iDLibrettoImpianto = null;

            if (!string.IsNullOrEmpty(CodiceTargatura))
            {
                using (var ctx = new CriterDataModel())
                {
                    var libretto = (from a in ctx.LIM_LibrettiImpianti
                                    join t in ctx.LIM_TargatureImpianti on a.IDTargaturaImpianto equals t.IDTargaturaImpianto
                                    where (t.CodiceTargatura == CodiceTargatura & a.fAttivo == true)
                                    select new
                                    {
                                        IDLibrettoImpianto = a.IDLibrettoImpianto
                                    }).FirstOrDefault();

                    if (libretto != null)
                    {
                        iDLibrettoImpianto = libretto.IDLibrettoImpianto;
                    }
                }
            }
            return iDLibrettoImpianto;
        }


        public static int? GetIDLibrettoImpiantoGruppoTermico(int CodiceProgressivo, int iDLibrettoImpianto)
        {
            int? IDLibrettoImpiantoGruppoTermico = null;

            using (var ctx = new CriterDataModel())
            {
                var libretto = (from a in ctx.LIM_LibrettiImpiantiGruppiTermici
                                where (a.IDLibrettoImpianto == iDLibrettoImpianto && a.fAttivo == true && a.CodiceProgressivo == CodiceProgressivo)
                                select new
                                {
                                    IDLibrettoImpiantoGruppoTermico = a.IDLibrettoImpiantoGruppoTermico
                                }).FirstOrDefault();

                if (libretto != null)
                {
                    IDLibrettoImpiantoGruppoTermico = libretto.IDLibrettoImpiantoGruppoTermico;
                }
            }

            return IDLibrettoImpiantoGruppoTermico;
        }

        public static int? GetIDLibrettoImpiantoMacchineFrigorifere(int CodiceProgressivo, int iDLibrettoImpianto)
        {
            int? IDLibrettoImpiantoMacchinaFrigorifera = null;

            using (var ctx = new CriterDataModel())
            {
                var libretto = (from a in ctx.LIM_LibrettiImpiantiMacchineFrigorifere
                                where (a.IDLibrettoImpianto == iDLibrettoImpianto && a.fAttivo == true && a.CodiceProgressivo == CodiceProgressivo)
                                select new
                                {
                                    IDLibrettoImpiantoMacchinaFrigorifera = a.IDLibrettoImpiantoMacchinaFrigorifera
                                }).FirstOrDefault();

                if (libretto != null)
                {
                    IDLibrettoImpiantoMacchinaFrigorifera = libretto.IDLibrettoImpiantoMacchinaFrigorifera;
                }
            }

            return IDLibrettoImpiantoMacchinaFrigorifera;
        }

        public static int? GetIDLibrettoImpiantoCogeneratori(int CodiceProgressivo, int iDLibrettoImpianto)
        {
            int? IDLibrettoImpiantoCogeneratore = null;

            using (var ctx = new CriterDataModel())
            {
                var libretto = (from a in ctx.LIM_LibrettiImpiantiCogeneratori
                                where (a.IDLibrettoImpianto == iDLibrettoImpianto && a.fAttivo == true && a.CodiceProgressivo == CodiceProgressivo)
                                select new
                                {
                                    IDLibrettoImpiantoCogeneratore = a.IDLibrettoImpiantoCogeneratore
                                }).FirstOrDefault();
                if (libretto != null)
                {
                    IDLibrettoImpiantoCogeneratore = libretto.IDLibrettoImpiantoCogeneratore;
                }
            }

            return IDLibrettoImpiantoCogeneratore;
        }

        public static int? GetIDLibrettoImpiantoScambiatori(int CodiceProgressivo, int iDLibrettoImpianto)
        {
            int? IDLibrettoImpiantoScambiatoreCalore = null;

            using (var ctx = new CriterDataModel())
            {
                var libretto = (from a in ctx.LIM_LibrettiImpiantiScambiatoriCalore
                                where (a.IDLibrettoImpianto == iDLibrettoImpianto && a.fAttivo == true && a.CodiceProgressivo == CodiceProgressivo)
                                select new
                                {
                                    IDLibrettoImpiantoScambiatoreCalore = a.IDLibrettoImpiantoScambiatoreCalore
                                }).FirstOrDefault();
                if (libretto != null)
                {
                    IDLibrettoImpiantoScambiatoreCalore = libretto.IDLibrettoImpiantoScambiatoreCalore;
                }
            }

            return IDLibrettoImpiantoScambiatoreCalore;
        }

        public static object[] EvaluateOperationLibrettoFromCodiceTargaturaImpianto(object CodiceTargatura, object CodiceSoggetto)
        {
            object[] outVal = new object[3];
            outVal[0] = null;  //IDLibretto
            outVal[1] = null;  //IDTargaturaImpianto
            outVal[2] = null;  //Operazione 1 - Inserimento - 2 Aggiornamento - 3 Revisione - 4 Non faccio nulla perchè annullato - 5 Non faccio nulla perchè la targatura non appartiene al soggetto

            if (CodiceTargatura != null)
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var libretto = (from a in ctx.LIM_LibrettiImpianti
                                    join c in ctx.LIM_TargatureImpianti on a.IDTargaturaImpianto equals c.IDTargaturaImpianto
                                    where c.CodiceTargatura == CodiceTargatura.ToString() && a.fAttivo == true
                                    select new
                                    {
                                        iDLibrettoImpianto = a.IDLibrettoImpianto,
                                        iDTargaturaImpianto = a.IDTargaturaImpianto,
                                        iDStatoLibrettoImpianto = a.IDStatoLibrettoImpianto,
                                        iDSoggettoDerived = c.IDSoggetto
                                    }
                                    ).FirstOrDefault();

                    if (libretto != null)
                    {
                        object[] getVal = new object[3];
                        getVal = GetIDSoggettiFromCodiceSoggetto(CodiceSoggetto.ToString());
                        if (int.Parse(getVal[1].ToString()) == libretto.iDSoggettoDerived)
                        {
                            #region Logica di valutazione 
                            //Se libretto in bozza o revisione allora aggiorno
                            if ((libretto.iDStatoLibrettoImpianto == 1) || (libretto.iDStatoLibrettoImpianto == 3))
                            {
                                //Aggiornamento
                                outVal[0] = libretto.iDLibrettoImpianto;
                                outVal[1] = libretto.iDTargaturaImpianto;
                                outVal[2] = 2;
                            }
                            else if (libretto.iDStatoLibrettoImpianto == 2)
                            {
                                //Revisione
                                outVal[0] = libretto.iDLibrettoImpianto;
                                outVal[1] = libretto.iDTargaturaImpianto;
                                outVal[2] = 3;
                            }
                            else if (libretto.iDStatoLibrettoImpianto == 4)
                            {
                                //Nessuna operazione perchè annullato
                                outVal[0] = libretto.iDLibrettoImpianto;
                                outVal[1] = libretto.iDTargaturaImpianto;
                                outVal[2] = 4;
                            }
                            #endregion
                        }
                        else
                        {
                            //Nessuna operazione perchè la targatura non appartiene al soggetto
                            outVal[0] = null;
                            outVal[1] = null;
                            outVal[2] = 5;
                        }
                    }
                    else
                    {
                        //Inserimento
                        outVal[0] = null;
                        outVal[1] = null;
                        outVal[2] = 1;
                    }
                }
            }
            else
            {
                //Inserimento
                outVal[0] = null;
                outVal[1] = null;
                outVal[2] = 1;
            }

            return outVal;
        }

        public static object[] EvaluateOperationRapportiFromCodiceTargaturaImpianto(object CodiceTargatura, object codiceProgressivo, object prefisso, DateTime? dataControllo, DateTime? oraArrivo, DateTime? oraPartenza, string guidRapportoControllo)
        {
            object[] outVal = new object[3];
            outVal[0] = null;  //IDRapportoDiControlloTecnico
            outVal[1] = null;  //IDTargaturaImpianto
            outVal[2] = null;  //Operazione 1 - Inserimento - 2 Aggiornamento - 3 Non faccio nulla Attesa di firma

            using (CriterDataModel ctx = new CriterDataModel())
            {
                if (!string.IsNullOrEmpty(guidRapportoControllo))
                {
                    var rapporto = (from a in ctx.RCT_RapportoDiControlloTecnicoBase
                                    join c in ctx.LIM_TargatureImpianti on a.IDTargaturaImpianto equals c.IDTargaturaImpianto
                                    where a.GuidRapportoTecnico == guidRapportoControllo.ToString()
                                    select new
                                    {
                                        iDRapportoControlloTecnico = a.IDRapportoControlloTecnico,
                                        iDTargaturaImpianto = a.IDTargaturaImpianto,
                                        iDStatoRapportoDiControllo = a.IDStatoRapportoDiControllo,
                                        dataControllo = a.DataControllo,
                                        oraArrivo = a.OraArrivo,
                                        oraPartenza = a.OraPartenza
                                    }
                                    ).OrderByDescending(u => u.iDRapportoControlloTecnico).FirstOrDefault();

                    if (rapporto != null)
                    {
                        //Se rapporto in bozza aggiorno
                        if (rapporto.iDStatoRapportoDiControllo == 1)
                        {
                            //Aggiornamento
                            outVal[0] = rapporto.iDRapportoControlloTecnico;
                            outVal[1] = rapporto.iDTargaturaImpianto;
                            outVal[2] = 2;
                        }
                        else if ((rapporto.iDStatoRapportoDiControllo == 2) || (rapporto.iDStatoRapportoDiControllo == 3) || (rapporto.iDStatoRapportoDiControllo == 4))
                        {
                            //Inserimento
                            outVal[0] = null;
                            outVal[1] = null;
                            outVal[2] = 1;
                        }
                    }
                    else
                    {
                        //Inserimento
                        outVal[0] = null;
                        outVal[1] = null;
                        outVal[2] = 1;
                    }
                }
                else
                {
                    #region Gestione del rapporto con le date ed orari
                    int codiceProgressivoInt = int.Parse(codiceProgressivo.ToString());
                    var rapporto = (from a in ctx.RCT_RapportoDiControlloTecnicoBase
                                    join c in ctx.LIM_TargatureImpianti on a.IDTargaturaImpianto equals c.IDTargaturaImpianto
                                    where c.CodiceTargatura == CodiceTargatura.ToString() &&
                                          a.CodiceProgressivo == codiceProgressivoInt &&
                                          a.Prefisso == prefisso.ToString()
                                    select new
                                    {
                                        iDRapportoControlloTecnico = a.IDRapportoControlloTecnico,
                                        iDTargaturaImpianto = a.IDTargaturaImpianto,
                                        iDStatoRapportoDiControllo = a.IDStatoRapportoDiControllo,
                                        dataControllo = a.DataControllo,
                                        oraArrivo = a.OraArrivo,
                                        oraPartenza = a.OraPartenza
                                    }
                                    ).OrderByDescending(u => u.iDRapportoControlloTecnico).FirstOrDefault();

                    if (rapporto != null)
                    {
                        //Se rapporto in bozza aggiorno
                        if (rapporto.iDStatoRapportoDiControllo == 1)
                        {
                            if ((dataControllo != null) && (oraArrivo != null) && (oraPartenza != null))
                            {
                                int hourOraArrivoJson = DateTime.Parse(oraArrivo.ToString()).Hour;
                                int hourOraPartenzaJson = DateTime.Parse(oraPartenza.ToString()).Hour;

                                int hourOraArrivo = DateTime.Parse(rapporto.oraArrivo.ToString()).Hour;
                                int hourOraPartenza = DateTime.Parse(rapporto.oraPartenza.ToString()).Hour;

                                if ((rapporto.dataControllo == dataControllo)
                                    && (hourOraArrivoJson == hourOraArrivo)
                                    && (hourOraPartenzaJson == hourOraPartenza))
                                {
                                    //Aggiornamento
                                    outVal[0] = rapporto.iDRapportoControlloTecnico;
                                    outVal[1] = rapporto.iDTargaturaImpianto;
                                    outVal[2] = 2;
                                }
                                else
                                {
                                    //Inserimento
                                    outVal[0] = null;
                                    outVal[1] = null;
                                    outVal[2] = 1;
                                }
                            }
                            else
                            {
                                //Inserimento
                                outVal[0] = null;
                                outVal[1] = null;
                                outVal[2] = 1;
                            }
                        }
                        else if ((rapporto.iDStatoRapportoDiControllo == 2) || (rapporto.iDStatoRapportoDiControllo == 3) || (rapporto.iDStatoRapportoDiControllo == 4))
                        {
                            //Inserimento
                            outVal[0] = null;
                            outVal[1] = null;
                            outVal[2] = 1;
                        }
                    }
                    else
                    {
                        //Inserimento
                        outVal[0] = null;
                        outVal[1] = null;
                        outVal[2] = 1;
                    }
                    #endregion
                }
            }

            return outVal;
        }

        public bool Equals<T>(T first, T second)
        {
            var f = new List<T>() { first };
            var s = new List<T>() { second };
            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (f.Select(x => propertyInfo.Name).FirstOrDefault() != s.Select(x => propertyInfo.Name).FirstOrDefault())
                    return false;
            }
            return true;
        }

        public static string LoadLibrettoImpianto(POMJ_LibrettoImpianto librettoImpianto)
        {
            string result = "";

            if (CheckCodiceCatastaleRERIsValid(librettoImpianto.CodiceCatastaleComune))
            {
                object[] getvaluateOperation = new object[3];
                getvaluateOperation = EvaluateOperationLibrettoFromCodiceTargaturaImpianto(librettoImpianto.CodiceTargaturaImpianto.CodiceTargatura, librettoImpianto.CodiceSoggetto);

                if (int.Parse(getvaluateOperation[2].ToString()) == 4) //Annullato
                {
                    result = "Libretto impianto non inserito correttamente perchè Annullato";
                }
                else if (int.Parse(getvaluateOperation[2].ToString()) == 5)
                {
                    result = "Libretto impianto non inserito correttamente perchè il codice targatura appartiene ad un'altra impresa";
                }
                else
                {
                    #region Libretto
                    using (var ctx = new CriterDataModel())
                    {
                        using (var dbContextTransaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                var libretto = new LIM_LibrettiImpianti();

                                if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                {
                                    int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                    libretto = ctx.LIM_LibrettiImpianti.FirstOrDefault(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt);
                                }
                                else if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                {
                                    int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                    var librettoOriginale = new LIM_LibrettiImpianti();
                                    librettoOriginale = ctx.LIM_LibrettiImpianti.FirstOrDefault(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt);
                                    var librettoRevisionato = UtilityLibrettiImpianti.RevisionaLibretto(librettoOriginale);
                                    libretto = ctx.LIM_LibrettiImpianti.FirstOrDefault(a => a.IDLibrettoImpianto == librettoRevisionato.IDLibrettoImpianto);
                                }

                                object[] getVal = new object[3];
                                getVal = GetIDSoggettiFromCodiceSoggetto(librettoImpianto.CodiceSoggetto);

                                int idUtenteIns = Convert.ToInt32(getVal[2]);

                                #region Testata libretto --> LIM_LibrettiImpianti
                                if (!string.IsNullOrEmpty(getVal[0].ToString()))
                                {
                                    libretto.IDSoggetto = Convert.ToInt32(getVal[0]);
                                }
                                libretto.IDSoggettoDerived = Convert.ToInt32(getVal[1]);
                                libretto.IDUtenteInserimento = idUtenteIns;
                                libretto.IDUtenteUltimaModifica = idUtenteIns;
                                if (!string.IsNullOrEmpty(librettoImpianto.CodiceTargaturaImpianto.CodiceTargatura))
                                {
                                    libretto.IDTargaturaImpianto = GetIDTargaturaImpiantoFromCodiceTargatura(librettoImpianto.CodiceTargaturaImpianto.CodiceTargatura, Convert.ToInt32(getVal[1]), "LIB");
                                }

                                if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                                {
                                    libretto.IDStatoLibrettoImpianto = 1;
                                }
                                else if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                {
                                    if (libretto.IDStatoLibrettoImpianto == 3)
                                    {
                                        libretto.IDStatoLibrettoImpianto = 3;
                                    }
                                    else
                                    {
                                        libretto.IDStatoLibrettoImpianto = 1;
                                    }
                                }
                                else if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                {
                                    libretto.IDStatoLibrettoImpianto = 3;
                                }

                                libretto.IDTipologiaIntervento = librettoImpianto.TipologiaIntervento;
                                libretto.DataIntervento = librettoImpianto.DataIntervento;
                                if (!string.IsNullOrEmpty(librettoImpianto.Indirizzo))
                                {
                                    libretto.Indirizzo = librettoImpianto.Indirizzo;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.Civico))
                                {
                                    libretto.Civico = librettoImpianto.Civico;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.Palazzo))
                                {
                                    libretto.Palazzo = librettoImpianto.Palazzo;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.Scala))
                                {
                                    libretto.Scala = librettoImpianto.Scala;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.Interno))
                                {
                                    libretto.Interno = librettoImpianto.Interno;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.CodiceCatastaleComune))
                                {
                                    libretto.IDCodiceCatastale = GetIDCodiceCatastaleFromCodiceCatastale(librettoImpianto.CodiceCatastaleComune);
                                }
                                libretto.fUnitaImmobiliare = librettoImpianto.fUnitaImmobiliare;
                                if (librettoImpianto.DestinazioneUso != null)
                                {
                                    libretto.IDDestinazioneUso = librettoImpianto.DestinazioneUso;
                                }
                                if (librettoImpianto.VolumeLordoRiscaldato != null)
                                {
                                    libretto.VolumeLordoRiscaldato = librettoImpianto.VolumeLordoRiscaldato;
                                }
                                if (librettoImpianto.VolumeLordoRaffrescato != null)
                                {
                                    libretto.VolumeLordoRaffrescato = librettoImpianto.VolumeLordoRaffrescato;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.NumeroAPE))
                                {
                                    libretto.NumeroAPE = librettoImpianto.NumeroAPE;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.NumeroPDR))
                                {
                                    libretto.NumeroPDR = librettoImpianto.NumeroPDR;
                                }
                                if (!string.IsNullOrEmpty(librettoImpianto.NumeroPOD))
                                {
                                    libretto.NumeroPOD = librettoImpianto.NumeroPOD;
                                }
                                libretto.fAcs = librettoImpianto.fAcs;
                                if (librettoImpianto.PotenzaAcs != null)
                                {
                                    libretto.PotenzaAcs = librettoImpianto.PotenzaAcs;
                                }
                                libretto.fClimatizzazioneEstiva = librettoImpianto.fClimatizzazioneEstiva;
                                libretto.fClimatizzazioneInvernale = librettoImpianto.fClimatizzazioneInvernale;
                                if (librettoImpianto.PotenzaClimatizzazioneEstiva != null)
                                {
                                    libretto.PotenzaClimatizzazioneEstiva = librettoImpianto.PotenzaClimatizzazioneEstiva;
                                }
                                if (librettoImpianto.PotenzaClimatizzazioneInvernale != null)
                                {
                                    libretto.PotenzaClimatizzazioneInvernale = librettoImpianto.PotenzaClimatizzazioneInvernale;
                                }

                                libretto.fClimatizzazioneAltro = librettoImpianto.fClimatizzazioneAltro;
                                libretto.ClimatizzazioneAltro = librettoImpianto.ClimatizzazioneAltro;
                                libretto.fPannelliSolariTermici = librettoImpianto.fPannelliSolariTermici;
                                if (librettoImpianto.SuperficieTotaleSolariTermici != null)
                                {
                                    libretto.SuperficieTotaleSolariTermici = librettoImpianto.SuperficieTotaleSolariTermici;
                                }
                                if (librettoImpianto.PotenzaSolariTermici != null)
                                {
                                    libretto.PotenzaSolariTermici = librettoImpianto.PotenzaSolariTermici;
                                }
                                libretto.fPannelliSolariTermiciAltro = librettoImpianto.fPannelliSolariTermiciAltro;
                                libretto.PannelliSolariTermiciAltro = librettoImpianto.PannelliSolariTermiciAltro;
                                libretto.fPannelliSolariClimatizzazioneAcs = librettoImpianto.fPannelliSolariClimatizzazioneAcs;
                                libretto.fPannelliSolariClimatizzazioneEstiva = librettoImpianto.fPannelliSolariClimatizzazioneEstiva;
                                libretto.fPannelliSolariClimatizzazioneInvernale = librettoImpianto.fPannelliSolariClimatizzazioneInvernale;
                                if (librettoImpianto.ContenutoAcquaImpianto != null)
                                {
                                    libretto.ContenutoAcquaImpianto = librettoImpianto.ContenutoAcquaImpianto;
                                }
                                if (librettoImpianto.DurezzaTotaleAcquaImpianto != null)
                                {
                                    libretto.DurezzaTotaleAcquaImpianto = librettoImpianto.DurezzaTotaleAcquaImpianto;
                                }
                                libretto.fTrattamentoAcquaInvernale = librettoImpianto.fTrattamentoAcquaInvernale;
                                if (librettoImpianto.DurezzaTotaleAcquaImpiantoInvernale != null)
                                {
                                    libretto.DurezzaTotaleAcquaImpiantoInvernale = librettoImpianto.DurezzaTotaleAcquaImpiantoInvernale;
                                }
                                libretto.fProtezioneGelo = librettoImpianto.fProtezioneGelo;
                                if (librettoImpianto.TipologiaProtezioneGelo != null)
                                {
                                    libretto.IDTipologiaProtezioneGelo = librettoImpianto.TipologiaProtezioneGelo;
                                }
                                libretto.fTrattamentoAcquaAcs = librettoImpianto.fTrattamentoAcquaAcs;
                                if (librettoImpianto.DurezzaTotaleAcquaAcs != null)
                                {
                                    libretto.DurezzaTotaleAcquaAcs = librettoImpianto.DurezzaTotaleAcquaAcs;
                                }
                                if (librettoImpianto.PercentualeGlicole != null)
                                {
                                    libretto.PercentualeGlicole = librettoImpianto.PercentualeGlicole;
                                }
                                if (librettoImpianto.PhGlicole != null)
                                {
                                    libretto.PhGlicole = librettoImpianto.PhGlicole;
                                }
                                libretto.fTrattamentoAcquaEstiva = librettoImpianto.fTrattamentoAcquaEstiva;
                                if (librettoImpianto.TipologiaCircuitoRaffreddamento != null)
                                {
                                    libretto.IDTipologiaCircuitoRaffreddamento = librettoImpianto.TipologiaCircuitoRaffreddamento;
                                }
                                else
                                {
                                    libretto.IDTipologiaCircuitoRaffreddamento = 1;
                                }
                                if (librettoImpianto.TipologiaAcquaAlimento != null)
                                {
                                    libretto.IDTipologiaAcquaAlimento = librettoImpianto.TipologiaAcquaAlimento;
                                }
                                else
                                {
                                    libretto.IDTipologiaAcquaAlimento = 1;
                                }
                                libretto.fSistemaSpurgoAutomatico = librettoImpianto.fSistemaSpurgoAutomatico;
                                if (librettoImpianto.ConducibilitaAcquaIngresso != null)
                                {
                                    libretto.ConducibilitaAcquaIngresso = librettoImpianto.ConducibilitaAcquaIngresso;
                                }
                                if (librettoImpianto.ConducibilitaInizioSpurgo != null)
                                {
                                    libretto.ConducibilitaInizioSpurgo = librettoImpianto.ConducibilitaInizioSpurgo;
                                }
                                libretto.fSistemaRegolazioneOnOff = librettoImpianto.fSistemaRegolazioneOnOff;
                                libretto.fSistemaRegolazioneIntegrato = librettoImpianto.fSistemaRegolazioneIntegrato;
                                libretto.fSistemaRegolazioneIndipendente = librettoImpianto.fSistemaRegolazioneIndipendente;
                                libretto.fValvoleRegolazione = librettoImpianto.fValvoleRegolazione;
                                libretto.fSistemaRegolazioneMultigradino = librettoImpianto.fSistemaRegolazioneMultigradino;
                                libretto.fSistemaRegolazioneAInverter = librettoImpianto.fSistemaRegolazioneAInverter;
                                libretto.fAltroSistemaRegolazionePrimaria = librettoImpianto.fAltroSistemaRegolazionePrimaria;
                                libretto.SistemaRegolazionePrimariaAltro = librettoImpianto.SistemaRegolazionePrimariaAltro;
                                if (librettoImpianto.TipologiaTermostatoZona != null)
                                {
                                    libretto.IDTipologiaTermostatoZona = librettoImpianto.TipologiaTermostatoZona;
                                }
                                libretto.fControlloEntalpico = librettoImpianto.fControlloEntalpico;
                                libretto.fControlloPortataAriaVariabile = librettoImpianto.fControlloPortataAriaVariabile;
                                libretto.fValvoleTermostatiche = librettoImpianto.fValvoleTermostatiche;
                                libretto.fValvoleDueVie = librettoImpianto.fValvoleDueVie;
                                libretto.fValvoleTreVie = librettoImpianto.fValvoleTreVie;
                                if (!string.IsNullOrEmpty(librettoImpianto.NoteRegolazioneSingoloAmbiente))
                                {
                                    libretto.NoteRegolazioneSingoloAmbiente = librettoImpianto.NoteRegolazioneSingoloAmbiente;
                                }
                                libretto.fTelelettura = librettoImpianto.fTelelettura;
                                libretto.fTelegestione = librettoImpianto.fTelegestione;
                                libretto.fContabilizzazione = librettoImpianto.fContabilizzazione;
                                libretto.fContabilizzazioneRiscaldamento = librettoImpianto.fContabilizzazioneRiscaldamento;
                                libretto.fContabilizzazioneRaffrescamento = librettoImpianto.fContabilizzazioneRaffrescamento;
                                libretto.fContabilizzazioneAcquaCalda = librettoImpianto.fContabilizzazioneAcquaCalda;
                                libretto.IDTipologiaSistemaContabilizzazione = librettoImpianto.TipologiaSistemaContabilizzazione;
                                libretto.fCoibentazioneReteDistribuzione = librettoImpianto.fCoibentazioneReteDistribuzione;
                                libretto.NoteCoibentazioneReteDistribuzione = librettoImpianto.NoteCoibentazioneReteDistribuzione;
                                libretto.SistemaEmissioneAltro = librettoImpianto.SistemaEmissioneAltro;

                                if (librettoImpianto.ResponsabileImpianto != null)
                                {
                                    if (librettoImpianto.TipologiaResponsabile != null)
                                    {
                                        libretto.IDTipologiaResponsabile = librettoImpianto.TipologiaResponsabile;
                                    }
                                    if (librettoImpianto.ResponsabileImpianto.TipoSoggetto != null)
                                    {
                                        libretto.IDTipoSoggetto = librettoImpianto.ResponsabileImpianto.TipoSoggetto;
                                    }
                                    else
                                    {
                                        libretto.IDTipoSoggetto = 1;
                                    }
                                    libretto.NomeResponsabile = librettoImpianto.ResponsabileImpianto.Nome;
                                    libretto.CognomeResponsabile = librettoImpianto.ResponsabileImpianto.Cognome;
                                    libretto.CodiceFiscaleResponsabile = librettoImpianto.ResponsabileImpianto.CodiceFiscale;
                                    libretto.RagioneSocialeResponsabile = librettoImpianto.ResponsabileImpianto.NomeAzienda;
                                    libretto.PartitaIvaResponsabile = librettoImpianto.ResponsabileImpianto.PartitaIVA;
                                    libretto.EmailResponsabile = librettoImpianto.ResponsabileImpianto.Email;
                                    libretto.EmailPecResponsabile = librettoImpianto.ResponsabileImpianto.EmailPec;
                                    libretto.IndirizzoResponsabile = librettoImpianto.ResponsabileImpianto.Indirizzo;
                                    libretto.CivicoResponsabile = librettoImpianto.ResponsabileImpianto.Civico;
                                    libretto.IDComuneResponsabile = GetIDCodiceCatastaleFromCodiceCatastale(librettoImpianto.ResponsabileImpianto.CodiceCatastaleComune);
                                    libretto.IDProvinciaResponsabile = librettoImpianto.ResponsabileImpianto.ProvinciaSedeLegale;
                                }
                                else
                                {
                                    libretto.IDTipologiaResponsabile = 1;
                                    libretto.IDTipoSoggetto = 1;
                                    libretto.NomeResponsabile = null;
                                    libretto.CognomeResponsabile = null;
                                    libretto.CodiceFiscaleResponsabile = null;
                                    libretto.RagioneSocialeResponsabile = null;
                                    libretto.PartitaIvaResponsabile = null;
                                    libretto.EmailResponsabile = null;
                                    libretto.EmailPecResponsabile = null;
                                    libretto.IndirizzoResponsabile = null;
                                    libretto.CivicoResponsabile = null;
                                    libretto.IDComuneResponsabile = null;
                                    libretto.IDProvinciaResponsabile = null;
                                }

                                libretto.fTerzoResponsabile = librettoImpianto.fTerzoResponsabile;
                                libretto.IDUtenteInserimento = idUtenteIns;
                                libretto.DataInserimento = DateTime.Now;
                                libretto.IDUtenteUltimaModifica = idUtenteIns;
                                libretto.DataUltimaModifica = DateTime.Now;
                                libretto.DataAnnullamento = null;
                                libretto.fAttivo = true;
                                libretto.JsonFormat = JsonConvert.SerializeObject(librettoImpianto);
                                if (!string.IsNullOrEmpty(librettoImpianto.CriterAPIKey))
                                {
                                    libretto.KeyApi = librettoImpianto.CriterAPIKey;
                                }
                                if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                {
                                    int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                    libretto.IDLibrettoImpiantoRevisione = iDLibrettoImpiantoInt;
                                }
                                else
                                {
                                    if (libretto.IDStatoLibrettoImpianto != 3)
                                    {
                                        libretto.IDLibrettoImpiantoRevisione = null;
                                        libretto.NumeroRevisione = null;
                                        libretto.DataRevisione = null;
                                    }
                                }
                                #endregion

                                #region Dati catastali --> LIM_LibrettiImpiantiDatiCatastali
                                if (librettoImpianto.DatiCatastali != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiDatiCatastali.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiDatiCatastali.Remove(dati);
                                        }
                                    }
                                    else if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiDatiCatastali.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiDatiCatastali.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.DatiCatastali)
                                    {
                                        var datoCatastaleDb = new LIM_LibrettiImpiantiDatiCatastali();
                                        datoCatastaleDb.LIM_LibrettiImpianti = libretto;
                                        if (!string.IsNullOrEmpty(item.Foglio))
                                        {
                                            datoCatastaleDb.Foglio = item.Foglio;
                                        }
                                        if (!string.IsNullOrEmpty(item.Mappale))
                                        {
                                            datoCatastaleDb.Mappale = item.Mappale;
                                        }
                                        if (!string.IsNullOrEmpty(item.Subalterno))
                                        {
                                            datoCatastaleDb.Subalterno = item.Subalterno;
                                        }
                                        if (!string.IsNullOrEmpty(item.Identificativo))
                                        {
                                            datoCatastaleDb.Identificativo = item.Identificativo;
                                        }
                                        //if (!string.IsNullOrEmpty(item.CodiceSezione))
                                        //{
                                        //    datoCatastaleDb.IDCodiceCatastaleSezione = GetIDCodiceCatastaleSezioneFromCodiceCatastale(librettoImpianto.CodiceCatastaleComune, item.CodiceSezione);
                                        //}
                                        datoCatastaleDb.fValidazioneMoka = false;

                                        ctx.LIM_LibrettiImpiantiDatiCatastali.Add(datoCatastaleDb);
                                    }
                                }
                                #endregion


                                if (int.Parse(getvaluateOperation[2].ToString()) != 3) //Revisione
                                {
                                    #region Accumulatori --> LIM_LibrettiImpiantiAccumuli
                                    int i = 1;
                                    if (librettoImpianto.Accumuli != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiAccumuli.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiAccumuli.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.Accumuli)
                                        {
                                            var accumulieDb = new LIM_LibrettiImpiantiAccumuli();
                                            accumulieDb.LIM_LibrettiImpianti = libretto;
                                            accumulieDb.IDLibrettoImpiantoInserimento = 0;
                                            accumulieDb.Prefisso = "AC";
                                            accumulieDb.CodiceProgressivo = i++;
                                            accumulieDb.Fabbricante = item.Fabbricante;
                                            accumulieDb.Modello = item.Modello;
                                            accumulieDb.Matricola = item.Matricola;
                                            accumulieDb.CapacitaLt = item.CapacitaLt;
                                            accumulieDb.fAcquaCalda = item.fAcquaCalda;
                                            accumulieDb.fRiscaldamento = item.fRiscaldamento;
                                            accumulieDb.fRaffrescamento = item.fRaffrescamento;
                                            accumulieDb.fCoibentazionePresente = item.fCoibentazionePresente;
                                            accumulieDb.DataInstallazione = item.DataInstallazione;
                                            accumulieDb.DataInserimento = DateTime.Now;
                                            accumulieDb.DataUltimaModifica = DateTime.Now;
                                            accumulieDb.IDUtenteInserimento = idUtenteIns;
                                            accumulieDb.IDUtenteUltimaModifica = idUtenteIns;
                                            accumulieDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiAccumuli.Add(accumulieDb);
                                        }
                                    }

                                    #endregion

                                    #region Gruppi termici --> LIM_LibrettiImpiantiGruppiTermici - LIM_LibrettiImpiantiBruciatori - LIM_LibrettiImpiantiRecuperatori
                                    i = 1;
                                    if (librettoImpianto.GruppiTermiciCaldaie != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiGruppiTermici.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.GruppiTermiciCaldaie)
                                        {
                                            var gruppitermiciDb = new LIM_LibrettiImpiantiGruppiTermici();
                                            gruppitermiciDb.LIM_LibrettiImpianti = libretto;
                                            gruppitermiciDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            gruppitermiciDb.Prefisso = "GT";
                                            gruppitermiciDb.CodiceProgressivo = i++;
                                            gruppitermiciDb.AnalisiFumoPrevisteNr = item.AnalisiFumoPrevisteNr;
                                            gruppitermiciDb.CombustibileAltro = item.CombustibileAltro;
                                            gruppitermiciDb.DataInstallazione = item.DataInstallazione;
                                            gruppitermiciDb.Fabbricante = item.Fabbricante;
                                            gruppitermiciDb.FluidoTermovettoreAltro = item.FluidoTermovettoreAltro;
                                            gruppitermiciDb.Matricola = item.Matricola;
                                            gruppitermiciDb.Modello = item.Modello;
                                            gruppitermiciDb.PotenzaTermicaUtileNominaleKw = item.PotenzaTermicaUtileNominaleKw;
                                            gruppitermiciDb.RendimentoTermicoUtilePc = item.RendimentoTermicoUtilePc;
                                            gruppitermiciDb.IDTipologiaCombustibile = item.TipologiaCombustibile;
                                            gruppitermiciDb.IDTipologiaFluidoTermoVettore = item.TipologiaFluidoTermoVettore;
                                            gruppitermiciDb.IDTipologiaGruppiTermici = item.TipologiaGruppiTermici;
                                            gruppitermiciDb.DataInserimento = DateTime.Now;
                                            gruppitermiciDb.DataUltimaModifica = DateTime.Now;
                                            gruppitermiciDb.IDUtenteInserimento = idUtenteIns;
                                            gruppitermiciDb.IDUtenteUltimaModifica = idUtenteIns;
                                            gruppitermiciDb.fAttivo = true;

                                            int j = 1;
                                            if (item.Bruciatori != null)
                                            {
                                                foreach (var b in item.Bruciatori)
                                                {
                                                    var bruciatoriDb = new LIM_LibrettiImpiantiBruciatori();
                                                    bruciatoriDb.LIM_LibrettiImpiantiGruppiTermici = gruppitermiciDb;
                                                    bruciatoriDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                                    bruciatoriDb.Prefisso = "BR";
                                                    bruciatoriDb.CodiceProgressivo = j++;
                                                    bruciatoriDb.Combustibile = b.Combustibile;
                                                    bruciatoriDb.DataInstallazione = b.DataInstallazione;
                                                    bruciatoriDb.Fabbricante = b.Fabbricante;
                                                    bruciatoriDb.Matricola = b.Matricola;
                                                    bruciatoriDb.Modello = b.Modello;
                                                    bruciatoriDb.Tipologia = b.Tipologia;
                                                    bruciatoriDb.PortataTermicaMaxNominaleKw = b.PortataTermicaMaxNominaleKw;
                                                    bruciatoriDb.PortataTermicaMinNominaleKw = b.PortataTermicaMinNominaleKw;
                                                    bruciatoriDb.DataInserimento = DateTime.Now;
                                                    bruciatoriDb.DataUltimaModifica = DateTime.Now;
                                                    bruciatoriDb.IDUtenteInserimento = idUtenteIns;
                                                    bruciatoriDb.IDUtenteUltimaModifica = idUtenteIns;
                                                    bruciatoriDb.fAttivo = true;

                                                    ctx.LIM_LibrettiImpiantiBruciatori.Add(bruciatoriDb);
                                                }
                                            }

                                            j = 1;
                                            if (item.Recuperatori != null)
                                            {
                                                foreach (var r in item.Recuperatori)
                                                {
                                                    var recuperatoriDb = new LIM_LibrettiImpiantiRecuperatori();
                                                    recuperatoriDb.LIM_LibrettiImpiantiGruppiTermici = gruppitermiciDb;
                                                    recuperatoriDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                                    recuperatoriDb.Prefisso = "RC";
                                                    recuperatoriDb.CodiceProgressivo = j++;
                                                    recuperatoriDb.DataInstallazione = r.DataInstallazione;
                                                    recuperatoriDb.Fabbricante = r.Fabbricante;
                                                    recuperatoriDb.Matricola = r.Matricola;
                                                    recuperatoriDb.Modello = r.Modello;
                                                    recuperatoriDb.PortataTermicaNominaleTotaleKw = r.PortataTermicaNominaleTotaleKw;
                                                    recuperatoriDb.DataInserimento = DateTime.Now;
                                                    recuperatoriDb.DataUltimaModifica = DateTime.Now;
                                                    recuperatoriDb.IDUtenteInserimento = idUtenteIns;
                                                    recuperatoriDb.IDUtenteUltimaModifica = idUtenteIns;
                                                    recuperatoriDb.fAttivo = true;

                                                    ctx.LIM_LibrettiImpiantiRecuperatori.Add(recuperatoriDb);
                                                }
                                            }

                                            ctx.LIM_LibrettiImpiantiGruppiTermici.Add(gruppitermiciDb);
                                        }
                                    }
                                    #endregion
                                    
                                    #region Altri generatori --> LIM_LibrettiImpiantiAltriGeneratori
                                    i = 1;
                                    if (librettoImpianto.AltriGeneratori != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiAltriGeneratori.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiAltriGeneratori.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.AltriGeneratori)
                                        {
                                            var altrigeneratoriDb = new LIM_LibrettiImpiantiAltriGeneratori();
                                            altrigeneratoriDb.LIM_LibrettiImpianti = libretto;
                                            altrigeneratoriDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            altrigeneratoriDb.Prefisso = "AG";
                                            altrigeneratoriDb.CodiceProgressivo = i++;
                                            altrigeneratoriDb.Fabbricante = item.Fabbricante;
                                            altrigeneratoriDb.Modello = item.Modello;
                                            altrigeneratoriDb.Matricola = item.Matricola;
                                            altrigeneratoriDb.Tipologia = item.Tipologia;
                                            altrigeneratoriDb.PotenzaUtileKw = item.PotenzaUtileKw;
                                            altrigeneratoriDb.DataInstallazione = item.DataInstallazione;
                                            altrigeneratoriDb.DataInserimento = DateTime.Now;
                                            altrigeneratoriDb.DataUltimaModifica = DateTime.Now;
                                            altrigeneratoriDb.IDUtenteInserimento = idUtenteIns;
                                            altrigeneratoriDb.IDUtenteUltimaModifica = idUtenteIns;
                                            altrigeneratoriDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiAltriGeneratori.Add(altrigeneratoriDb);
                                        }
                                    }
                                    #endregion

                                    #region Campi solari termici --> LIM_LibrettiImpiantiCampiSolariTermici
                                    i = 1;
                                    if (librettoImpianto.CampiSolariTermici != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiCampiSolariTermici.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiCampiSolariTermici.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.CampiSolariTermici)
                                        {
                                            var campisolariDb = new LIM_LibrettiImpiantiCampiSolariTermici();
                                            campisolariDb.LIM_LibrettiImpianti = libretto;
                                            campisolariDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            campisolariDb.Prefisso = "CS";
                                            campisolariDb.CodiceProgressivo = i++;
                                            campisolariDb.Fabbricante = item.Fabbricante;
                                            campisolariDb.CollettoriNum = item.CollettoriNum;
                                            campisolariDb.SuperficieTotaleAperturaMq = item.SuperficieTotaleAperturaMq;
                                            campisolariDb.DataInstallazione = item.DataInstallazione;
                                            campisolariDb.DataInserimento = DateTime.Now;
                                            campisolariDb.DataUltimaModifica = DateTime.Now;
                                            campisolariDb.IDUtenteInserimento = idUtenteIns;
                                            campisolariDb.IDUtenteUltimaModifica = idUtenteIns;
                                            campisolariDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiCampiSolariTermici.Add(campisolariDb);
                                        }
                                    }
                                    #endregion

                                    #region Circuiti interrati --> LIM_LibrettiImpiantiCircuitiInterrati
                                    i = 1;
                                    if (librettoImpianto.CircuitiInterrati != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiCircuitiInterrati.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiCircuitiInterrati.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.CircuitiInterrati)
                                        {
                                            var circuitiinterratiDb = new LIM_LibrettiImpiantiCircuitiInterrati();
                                            circuitiinterratiDb.LIM_LibrettiImpianti = libretto;
                                            circuitiinterratiDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            circuitiinterratiDb.Prefisso = "CI";
                                            circuitiinterratiDb.CodiceProgressivo = i++;
                                            circuitiinterratiDb.LunghezzaCircuitoMt = item.LunghezzaCircuitoMt;
                                            circuitiinterratiDb.SuperficieScambiatoreMq = item.SuperficieScambiatoreMq;
                                            circuitiinterratiDb.ProfonditaInstallazioneMt = item.ProfonditaInstallazioneMt;
                                            circuitiinterratiDb.DataInstallazione = item.DataInstallazione;
                                            circuitiinterratiDb.DataInserimento = DateTime.Now;
                                            circuitiinterratiDb.DataUltimaModifica = DateTime.Now;
                                            circuitiinterratiDb.IDUtenteInserimento = idUtenteIns;
                                            circuitiinterratiDb.IDUtenteUltimaModifica = idUtenteIns;
                                            circuitiinterratiDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiCircuitiInterrati.Add(circuitiinterratiDb);
                                        }
                                    }
                                    #endregion

                                    #region Cogeneratori --> LIM_LibrettiImpiantiCogeneratori
                                    i = 1;
                                    if (librettoImpianto.CogeneratoriTrigeneratori != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiCogeneratori.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiCogeneratori.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.CogeneratoriTrigeneratori)
                                        {
                                            var cogeneratoriDb = new LIM_LibrettiImpiantiCogeneratori();
                                            cogeneratoriDb.LIM_LibrettiImpianti = libretto;
                                            cogeneratoriDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;

                                            cogeneratoriDb.Prefisso = "CG";
                                            cogeneratoriDb.CodiceProgressivo = i++;
                                            cogeneratoriDb.Fabbricante = item.Fabbricante;
                                            cogeneratoriDb.Modello = item.Modello;
                                            cogeneratoriDb.Matricola = item.Matricola;
                                            cogeneratoriDb.IDTipologiaCogeneratore = item.TipologiaCogeneratore;
                                            cogeneratoriDb.IDTipologiaCombustibile = item.TipologiaCombustibile;
                                            cogeneratoriDb.CombustibileAltro = item.CombustibileAltro;
                                            cogeneratoriDb.PotenzaTermicaNominaleKw = item.PotenzaTermicaNominaleKw;
                                            cogeneratoriDb.PotenzaElettricaNominaleKw = item.PotenzaElettricaNominaleKw;
                                            cogeneratoriDb.TemperaturaAcquaUscitaGradiMin = item.TemperaturaAcquaUscitaGradiMin;
                                            cogeneratoriDb.TemperaturaAcquaUscitaGradiMax = item.TemperaturaAcquaUscitaGradiMax;
                                            cogeneratoriDb.TemperaturaAcquaIngressoGradiMin = item.TemperaturaAcquaIngressoGradiMin;
                                            cogeneratoriDb.TemperaturaAcquaIngressoGradiMax = item.TemperaturaAcquaIngressoGradiMax;
                                            cogeneratoriDb.TemperaturaAcquaMotoreMin = item.TemperaturaAcquaMotoreMin;
                                            cogeneratoriDb.TemperaturaAcquaMotoreMax = item.TemperaturaAcquaMotoreMax;
                                            cogeneratoriDb.TemperaturaFumiValleMin = item.TemperaturaFumiValleMin;
                                            cogeneratoriDb.TemperaturaFumiValleMax = item.TemperaturaFumiValleMax;
                                            cogeneratoriDb.TemperaturaFumiMonteMin = item.TemperaturaFumiMonteMin;
                                            cogeneratoriDb.TemperaturaFumiMonteMax = item.TemperaturaFumiMonteMax;
                                            cogeneratoriDb.EmissioniCOMin = item.EmissioniCOMin;
                                            cogeneratoriDb.EmissioniCOMax = item.EmissioniCOMax;
                                            cogeneratoriDb.DataInstallazione = item.DataInstallazione;
                                            cogeneratoriDb.DataInserimento = DateTime.Now;
                                            cogeneratoriDb.DataUltimaModifica = DateTime.Now;
                                            cogeneratoriDb.IDUtenteInserimento = idUtenteIns;
                                            cogeneratoriDb.IDUtenteUltimaModifica = idUtenteIns;
                                            cogeneratoriDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiCogeneratori.Add(cogeneratoriDb);
                                        }
                                    }
                                    #endregion

                                    #region Pompe Circolazione --> LIM_LibrettiImpiantiPompeCircolazione
                                    i = 1;
                                    if (librettoImpianto.PompaCircolazione != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiPompeCircolazione.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiPompeCircolazione.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.PompaCircolazione)
                                        {
                                            var pompacircolazioneDb = new LIM_LibrettiImpiantiPompeCircolazione();
                                            pompacircolazioneDb.LIM_LibrettiImpianti = libretto;
                                            pompacircolazioneDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            pompacircolazioneDb.Prefisso = "PO";
                                            pompacircolazioneDb.CodiceProgressivo = i++;
                                            pompacircolazioneDb.Fabbricante = item.Fabbricante;
                                            pompacircolazioneDb.Modello = item.Modello;
                                            pompacircolazioneDb.fGiriVariabili = item.fGiriVariabili;
                                            pompacircolazioneDb.PotenzaNominaleKw = item.PotenzaNominaleKw;
                                            pompacircolazioneDb.DataInstallazione = item.DataInstallazione;
                                            pompacircolazioneDb.DataInserimento = DateTime.Now;
                                            pompacircolazioneDb.DataUltimaModifica = DateTime.Now;
                                            pompacircolazioneDb.IDUtenteInserimento = idUtenteIns;
                                            pompacircolazioneDb.IDUtenteUltimaModifica = idUtenteIns;
                                            pompacircolazioneDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiPompeCircolazione.Add(pompacircolazioneDb);
                                        }
                                    }
                                    #endregion

                                    #region Recuperatori di calore --> LIM_LibrettiImpiantiRecuperatoriCalore
                                    i = 1;
                                    if (librettoImpianto.RecuperatoriCalore != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiRecuperatoriCalore.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiRecuperatoriCalore.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.RecuperatoriCalore)
                                        {
                                            var recuperatoricaloreDb = new LIM_LibrettiImpiantiRecuperatoriCalore();
                                            recuperatoricaloreDb.LIM_LibrettiImpianti = libretto;
                                            recuperatoricaloreDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            recuperatoricaloreDb.Prefisso = "RC";
                                            recuperatoricaloreDb.CodiceProgressivo = i++;
                                            recuperatoricaloreDb.Tipologia = item.Tipologia;
                                            recuperatoricaloreDb.IDModalitaInstallazione = item.ModalitaInstallazioneRecuperatoriCalore;
                                            recuperatoricaloreDb.PortataVentilatoreMandataLts = item.PortataVentilatoreMandataLts;
                                            recuperatoricaloreDb.PortataVentilatoreRipresaLts = item.PortataVentilatoreRipresaLts;
                                            recuperatoricaloreDb.PotenzaVentilatoreMandataKw = item.PotenzaVentilatoreMandataKw;
                                            recuperatoricaloreDb.PotenzaVentilatoreRipresaKw = item.PotenzaVentilatoreRipresaKw;
                                            recuperatoricaloreDb.DataInstallazione = item.DataInstallazione;
                                            recuperatoricaloreDb.DataInserimento = DateTime.Now;
                                            recuperatoricaloreDb.DataUltimaModifica = DateTime.Now;
                                            recuperatoricaloreDb.IDUtenteInserimento = idUtenteIns;
                                            recuperatoricaloreDb.IDUtenteUltimaModifica = idUtenteIns;
                                            recuperatoricaloreDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiRecuperatoriCalore.Add(recuperatoricaloreDb);
                                        }
                                    }
                                    #endregion

                                    #region Scambiatori di calore --> LIM_LibrettiImpiantiScambiatoriCalore
                                    i = 1;
                                    if (librettoImpianto.ScambiatoriCaloreSottostazione != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiScambiatoriCalore.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiScambiatoriCalore.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.ScambiatoriCaloreSottostazione)
                                        {
                                            var scambiatoricaloreDb = new LIM_LibrettiImpiantiScambiatoriCalore();
                                            scambiatoricaloreDb.LIM_LibrettiImpianti = libretto;
                                            scambiatoricaloreDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            scambiatoricaloreDb.Prefisso = "SC";
                                            scambiatoricaloreDb.CodiceProgressivo = i++;
                                            scambiatoricaloreDb.Fabbricante = item.Fabbricante;
                                            scambiatoricaloreDb.Modello = item.Modello;
                                            scambiatoricaloreDb.Matricola = item.Matricola;
                                            scambiatoricaloreDb.PotenzaTermicaNominaleTotaleKw = item.PotenzaTermicaNominaleTotaleKw;
                                            scambiatoricaloreDb.DataInstallazione = item.DataInstallazione;
                                            scambiatoricaloreDb.DataInserimento = DateTime.Now;
                                            scambiatoricaloreDb.DataUltimaModifica = DateTime.Now;
                                            scambiatoricaloreDb.IDUtenteInserimento = idUtenteIns;
                                            scambiatoricaloreDb.IDUtenteUltimaModifica = idUtenteIns;
                                            scambiatoricaloreDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiScambiatoriCalore.Add(scambiatoricaloreDb);
                                        }
                                    }
                                    #endregion

                                    #region Scambiatori di calore intermedi --> LIM_LibrettiImpiantiScambiatoriCaloreIntermedi
                                    i = 1;
                                    if (librettoImpianto.ScambiatoriCaloreIntermedio != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.ScambiatoriCaloreIntermedio)
                                        {
                                            var scambiatoricaloreintermediDb = new LIM_LibrettiImpiantiScambiatoriCaloreIntermedi();
                                            scambiatoricaloreintermediDb.LIM_LibrettiImpianti = libretto;
                                            scambiatoricaloreintermediDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            scambiatoricaloreintermediDb.Prefisso = "SC";
                                            scambiatoricaloreintermediDb.CodiceProgressivo = i++;
                                            scambiatoricaloreintermediDb.Fabbricante = item.Fabbricante;
                                            scambiatoricaloreintermediDb.Modello = item.Modello;
                                            scambiatoricaloreintermediDb.DataInstallazione = item.DataInstallazione;
                                            scambiatoricaloreintermediDb.DataInserimento = DateTime.Now;
                                            scambiatoricaloreintermediDb.DataUltimaModifica = DateTime.Now;
                                            scambiatoricaloreintermediDb.IDUtenteInserimento = idUtenteIns;
                                            scambiatoricaloreintermediDb.IDUtenteUltimaModifica = idUtenteIns;
                                            scambiatoricaloreintermediDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi.Add(scambiatoricaloreintermediDb);
                                        }
                                    }
                                    #endregion

                                    #region Raffreddatori a liquido --> LIM_LibrettiImpiantiRaffreddatoriLiquido
                                    i = 1;
                                    if (librettoImpianto.RaffreddatoriLiquido != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiRaffreddatoriLiquido.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiRaffreddatoriLiquido.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.RaffreddatoriLiquido)
                                        {
                                            var raffreddatoriliquidoDb = new LIM_LibrettiImpiantiRaffreddatoriLiquido();
                                            raffreddatoriliquidoDb.LIM_LibrettiImpianti = libretto;
                                            raffreddatoriliquidoDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            raffreddatoriliquidoDb.Prefisso = "RV";
                                            raffreddatoriliquidoDb.CodiceProgressivo = i++;
                                            raffreddatoriliquidoDb.Fabbricante = item.Fabbricante;
                                            raffreddatoriliquidoDb.Modello = item.Modello;
                                            raffreddatoriliquidoDb.Matricola = item.Matricola;
                                            raffreddatoriliquidoDb.VentilatoriNum = item.VentilatoriNum;
                                            raffreddatoriliquidoDb.IDTipologiaVentilatori = item.TipologiaVentilatori;
                                            raffreddatoriliquidoDb.TipologiaVentilatoriAltro = item.TipologiaVentilatoriAltro;
                                            raffreddatoriliquidoDb.DataInstallazione = item.DataInstallazione;
                                            raffreddatoriliquidoDb.DataInserimento = DateTime.Now;
                                            raffreddatoriliquidoDb.DataUltimaModifica = DateTime.Now;
                                            raffreddatoriliquidoDb.IDUtenteInserimento = idUtenteIns;
                                            raffreddatoriliquidoDb.IDUtenteUltimaModifica = idUtenteIns;
                                            raffreddatoriliquidoDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiRaffreddatoriLiquido.Add(raffreddatoriliquidoDb);
                                        }
                                    }
                                    #endregion

                                    #region Torri evaporative --> LIM_LibrettiImpiantiTorriEvaporative
                                    i = 1;
                                    if (librettoImpianto.TorriEvaporative != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiTorriEvaporative.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiTorriEvaporative.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.TorriEvaporative)
                                        {
                                            var torrievaporativeDb = new LIM_LibrettiImpiantiTorriEvaporative();
                                            torrievaporativeDb.LIM_LibrettiImpianti = libretto;
                                            torrievaporativeDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            torrievaporativeDb.Prefisso = "TE";
                                            torrievaporativeDb.CodiceProgressivo = i++;
                                            torrievaporativeDb.Fabbricante = item.Fabbricante;
                                            torrievaporativeDb.Modello = item.Modello;
                                            torrievaporativeDb.Matricola = item.Matricola;
                                            torrievaporativeDb.CapacitaNominaleLt = item.CapacitaNominaleLt;
                                            torrievaporativeDb.VentilatoriNum = item.VentilatoriNum;
                                            torrievaporativeDb.IDTipologiaVentilatori = item.TipologiaVentilatori;
                                            torrievaporativeDb.TipologiaVentilatoriAltro = item.TipologiaVentilatoriAltro;
                                            torrievaporativeDb.DataInserimento = DateTime.Now;
                                            torrievaporativeDb.DataUltimaModifica = DateTime.Now;
                                            torrievaporativeDb.IDUtenteInserimento = idUtenteIns;
                                            torrievaporativeDb.IDUtenteUltimaModifica = idUtenteIns;
                                            torrievaporativeDb.DataInstallazione = item.DataInstallazione;
                                            torrievaporativeDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiTorriEvaporative.Add(torrievaporativeDb);
                                        }
                                    }

                                    #endregion

                                    #region Unità trattamento aria --> LIM_LibrettiImpiantiUnitaTrattamentoAria
                                    i = 1;
                                    if (librettoImpianto.UnitaTrattamentoAria != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiUnitaTrattamentoAria.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiUnitaTrattamentoAria.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.UnitaTrattamentoAria)
                                        {
                                            var unitatrattamentoariaDb = new LIM_LibrettiImpiantiUnitaTrattamentoAria();
                                            unitatrattamentoariaDb.LIM_LibrettiImpianti = libretto;
                                            unitatrattamentoariaDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            unitatrattamentoariaDb.Prefisso = "UT";
                                            unitatrattamentoariaDb.CodiceProgressivo = i++;
                                            unitatrattamentoariaDb.Fabbricante = item.Fabbricante;
                                            unitatrattamentoariaDb.Modello = item.Modello;
                                            unitatrattamentoariaDb.Matricola = item.Matricola;
                                            unitatrattamentoariaDb.PortataVentilatoreMandataLts = item.PortataVentilatoreMandataLts;
                                            unitatrattamentoariaDb.PortataVentilatoreRipresaLts = item.PortataVentilatoreRipresaLts;
                                            unitatrattamentoariaDb.PotenzaVentilatoreMandataKw = item.PotenzaVentilatoreMandataKw;
                                            unitatrattamentoariaDb.PotenzaVentilatoreRipresaKw = item.PotenzaVentilatoreRipresaKw;

                                            unitatrattamentoariaDb.DataInstallazione = item.DataInstallazione;
                                            unitatrattamentoariaDb.DataInserimento = DateTime.Now;
                                            unitatrattamentoariaDb.DataUltimaModifica = DateTime.Now;
                                            unitatrattamentoariaDb.IDUtenteInserimento = idUtenteIns;
                                            unitatrattamentoariaDb.IDUtenteUltimaModifica = idUtenteIns;
                                            unitatrattamentoariaDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiUnitaTrattamentoAria.Add(unitatrattamentoariaDb);
                                        }
                                    }
                                    #endregion

                                    #region Valvole regolazione --> LIM_LibrettiImpiantiValvoleRegolazione
                                    i = 1;
                                    if (librettoImpianto.ValvoleRegolazione != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiValvoleRegolazione.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiValvoleRegolazione.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.ValvoleRegolazione)
                                        {
                                            var valvoleregolazioneDb = new LIM_LibrettiImpiantiValvoleRegolazione();
                                            valvoleregolazioneDb.LIM_LibrettiImpianti = libretto;
                                            valvoleregolazioneDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            valvoleregolazioneDb.Prefisso = "VR";
                                            valvoleregolazioneDb.CodiceProgressivo = i++;
                                            valvoleregolazioneDb.Fabbricante = item.Fabbricante;
                                            valvoleregolazioneDb.Modello = item.Modello;
                                            valvoleregolazioneDb.VieNum = item.VieNum;
                                            valvoleregolazioneDb.Servomotore = item.Servomotore;

                                            valvoleregolazioneDb.DataInstallazione = item.DataInstallazione;
                                            valvoleregolazioneDb.DataInserimento = DateTime.Now;
                                            valvoleregolazioneDb.DataUltimaModifica = DateTime.Now;
                                            valvoleregolazioneDb.IDUtenteInserimento = idUtenteIns;
                                            valvoleregolazioneDb.IDUtenteUltimaModifica = idUtenteIns;
                                            valvoleregolazioneDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiValvoleRegolazione.Add(valvoleregolazioneDb);
                                        }
                                    }
                                    #endregion

                                    #region Vasi di espansione --> LIM_LibrettiImpiantiVasiEspansione
                                    i = 1;
                                    if (librettoImpianto.VasiEspansione != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiVasiEspansione.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiVasiEspansione.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.VasiEspansione)
                                        {
                                            var vasiespansioneDb = new LIM_LibrettiImpiantiVasiEspansione();
                                            vasiespansioneDb.LIM_LibrettiImpianti = libretto;
                                            vasiespansioneDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            vasiespansioneDb.Prefisso = "VX";
                                            vasiespansioneDb.CodiceProgressivo = i++;
                                            vasiespansioneDb.CapacitaLt = item.CapacitaLt;
                                            vasiespansioneDb.fChiuso = item.fChiuso;
                                            vasiespansioneDb.PressionePrecaricaBar = item.PressionePrecaricaBar;

                                            vasiespansioneDb.DataInstallazione = item.DataInstallazione;
                                            vasiespansioneDb.DataInserimento = DateTime.Now;
                                            vasiespansioneDb.DataUltimaModifica = DateTime.Now;
                                            vasiespansioneDb.IDUtenteInserimento = idUtenteIns;
                                            vasiespansioneDb.IDUtenteUltimaModifica = idUtenteIns;
                                            vasiespansioneDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiVasiEspansione.Add(vasiespansioneDb);
                                        }
                                    }
                                    #endregion

                                    #region Gruppi Frigo --> LIM_LibrettiImpiantiMacchineFrigorifere
                                    i = 1;
                                    if (librettoImpianto.GruppiFrigo != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.GruppiFrigo)
                                        {
                                            var gruppifrigoDb = new LIM_LibrettiImpiantiMacchineFrigorifere();
                                            gruppifrigoDb.LIM_LibrettiImpianti = libretto;
                                            gruppifrigoDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            gruppifrigoDb.Prefisso = "GF";
                                            gruppifrigoDb.CodiceProgressivo = i++;
                                            gruppifrigoDb.Fabbricante = item.Fabbricante;
                                            gruppifrigoDb.Modello = item.Modello;
                                            gruppifrigoDb.Matricola = item.Matricola;
                                            gruppifrigoDb.FiltroFrigorigeno = item.FiltroFrigorigeno;
                                            gruppifrigoDb.IDTipologiaMacchineFrigorifere = item.TipologiaMacchineFrigorifere;
                                            gruppifrigoDb.Combustibile = item.Combustibile;
                                            gruppifrigoDb.IDSorgenteLatoEsterno = item.SorgenteLatoEsterno;
                                            gruppifrigoDb.IDFluidoLatoUtenze = item.FluidoLatoUtenze;
                                            gruppifrigoDb.NumCircuiti = item.NumCircuiti;
                                            gruppifrigoDb.CoefficienteRiscaldamento = item.CoefficienteRiscaldamento;
                                            gruppifrigoDb.CoefficienteRaffrescamento = item.CoefficienteRaffrescamento;
                                            gruppifrigoDb.PotenzaFrigoriferaNominaleKw = item.PotenzaFrigoriferaNominaleKw;
                                            gruppifrigoDb.PortataTermicaNominaleKw = item.PortataTermicaNominaleKw;
                                            gruppifrigoDb.PotenzaFrigoriferaAssorbitaNominaleKw = item.PotenzaFrigoriferaAssorbitaNominaleKw;
                                            gruppifrigoDb.PotenzaTermicaAssorbitaNominaleKw = item.PotenzaTermicaAssorbitaNominaleKw;

                                            gruppifrigoDb.DataInstallazione = item.DataInstallazione;
                                            gruppifrigoDb.DataInserimento = DateTime.Now;
                                            gruppifrigoDb.DataUltimaModifica = DateTime.Now;
                                            gruppifrigoDb.IDUtenteInserimento = idUtenteIns;
                                            gruppifrigoDb.IDUtenteUltimaModifica = idUtenteIns;
                                            gruppifrigoDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Add(gruppifrigoDb);
                                        }
                                    }
                                    #endregion

                                    #region Gruppi VMC --> LIM_LibrettiImpiantiImpiantiVMC
                                    i = 1;
                                    if (librettoImpianto.GruppiVMC != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiImpiantiVMC.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiImpiantiVMC.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.GruppiVMC)
                                        {
                                            var gruppiVMCDb = new LIM_LibrettiImpiantiImpiantiVMC();
                                            gruppiVMCDb.LIM_LibrettiImpianti = libretto;
                                            gruppiVMCDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                            gruppiVMCDb.Prefisso = "VM";
                                            gruppiVMCDb.CodiceProgressivo = i++;
                                            gruppiVMCDb.Fabbricante = item.Fabbricante;
                                            gruppiVMCDb.Modello = item.Modello;
                                            gruppiVMCDb.IDTipologiaImpiantoVMC = item.TipologiaImpiantoVMC;
                                            gruppiVMCDb.TipologiaImpiantoAltro = item.TipologiaImpiantoAltro;
                                            gruppiVMCDb.PortataAriaMaxMch = item.PortataAriaMaxMch;
                                            gruppiVMCDb.RendimentoRecuperoCop = item.RendimentoRecuperoCop;
                                            gruppiVMCDb.DataInstallazione = item.DataInstallazione;
                                            gruppiVMCDb.DataInserimento = DateTime.Now;
                                            gruppiVMCDb.DataUltimaModifica = DateTime.Now;
                                            gruppiVMCDb.IDUtenteInserimento = idUtenteIns;
                                            gruppiVMCDb.IDUtenteUltimaModifica = idUtenteIns;
                                            gruppiVMCDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiImpiantiVMC.Add(gruppiVMCDb);
                                        }
                                    }
                                    #endregion

                                    #region Sistemi Regolazione --> LIM_LibrettiImpiantiSistemiRegolazione
                                    i = 1;
                                    if (librettoImpianto.SistemiRegolazione != null)
                                    {
                                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                        {
                                            int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                            var datiAttuali = ctx.LIM_LibrettiImpiantiSistemiRegolazione.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                            foreach (var dati in datiAttuali)
                                            {
                                                ctx.LIM_LibrettiImpiantiSistemiRegolazione.Remove(dati);
                                            }
                                        }

                                        foreach (var item in librettoImpianto.SistemiRegolazione)
                                        {
                                            var sistemiregolazioneDb = new LIM_LibrettiImpiantiSistemiRegolazione();
                                            sistemiregolazioneDb.LIM_LibrettiImpianti = libretto;
                                            sistemiregolazioneDb.IDLibrettoImpiantoInserimento = 0;
                                            sistemiregolazioneDb.Prefisso = "VM";
                                            sistemiregolazioneDb.CodiceProgressivo = i++;
                                            sistemiregolazioneDb.Fabbricante = item.Fabbricante;
                                            sistemiregolazioneDb.Modello = item.Modello;

                                            sistemiregolazioneDb.PuntiRegolazioneNum = item.PuntiRegolazioneNum;
                                            sistemiregolazioneDb.LivelliTemperaturaNum = item.LivelliTemperaturaNum;

                                            sistemiregolazioneDb.DataInstallazione = item.DataInstallazione;
                                            sistemiregolazioneDb.DataInserimento = DateTime.Now;
                                            sistemiregolazioneDb.DataUltimaModifica = DateTime.Now;
                                            sistemiregolazioneDb.IDUtenteInserimento = idUtenteIns;
                                            sistemiregolazioneDb.IDUtenteUltimaModifica = idUtenteIns;
                                            sistemiregolazioneDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiSistemiRegolazione.Add(sistemiregolazioneDb);
                                        }
                                    }
                                    #endregion

                                    #region Terzo Responsabile
                                    if (librettoImpianto.TerzoResponsabile != null)
                                    {
                                        if (librettoImpianto.fTerzoResponsabile)
                                        {
                                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                            {
                                                int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                                var datiAttuali = ctx.LIM_LibrettiImpiantiResponsabili.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt && a.fAttivo == true).ToList();
                                                foreach (var dati in datiAttuali)
                                                {
                                                    ctx.LIM_LibrettiImpiantiResponsabili.Remove(dati);
                                                }
                                            }

                                            var terzoResponsabileDb = new LIM_LibrettiImpiantiResponsabili();
                                            terzoResponsabileDb.LIM_LibrettiImpianti = libretto;
                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.Nome))
                                            {
                                                terzoResponsabileDb.Nome = librettoImpianto.TerzoResponsabile.Nome;
                                            }
                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.Nome))
                                            {
                                                terzoResponsabileDb.Cognome = librettoImpianto.TerzoResponsabile.Cognome;
                                            }
                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.NomeAzienda))
                                            {
                                                terzoResponsabileDb.RagioneSociale = librettoImpianto.TerzoResponsabile.NomeAzienda;
                                            }
                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.PartitaIVA))
                                            {
                                                terzoResponsabileDb.PartitaIva = librettoImpianto.TerzoResponsabile.PartitaIVA;
                                            }
                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.NumeroIscrizioneCCIAA))
                                            {
                                                terzoResponsabileDb.NumeroCciaa = librettoImpianto.TerzoResponsabile.NumeroIscrizioneCCIAA;
                                            }
                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.Email))
                                            {
                                                terzoResponsabileDb.Email = librettoImpianto.TerzoResponsabile.Email;
                                            }
                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.EmailPec))
                                            {
                                                terzoResponsabileDb.EmailPec = librettoImpianto.TerzoResponsabile.EmailPec;
                                            }
                                            if (librettoImpianto.InizioAssunzioneIncarico != null)
                                            {
                                                terzoResponsabileDb.DataInizio = librettoImpianto.InizioAssunzioneIncarico;
                                            }
                                            if (librettoImpianto.InizioAssunzioneIncarico != null)
                                            {
                                                terzoResponsabileDb.DataFine = librettoImpianto.FineAssunzioneIncarico;
                                            }

                                            if (!string.IsNullOrEmpty(librettoImpianto.TerzoResponsabile.PartitaIVA))
                                            {
                                                var anagrafica = ctx.COM_AnagraficaSoggetti.FirstOrDefault(a => a.PartitaIVA == librettoImpianto.TerzoResponsabile.PartitaIVA.Trim());
                                                if (anagrafica != null)
                                                {
                                                    terzoResponsabileDb.IDSoggetto = anagrafica.IDSoggetto;
                                                }
                                            }

                                            terzoResponsabileDb.DataInserimento = DateTime.Now;
                                            terzoResponsabileDb.DataUltimaModifica = DateTime.Now;
                                            terzoResponsabileDb.IDUtenteInserimento = idUtenteIns;
                                            terzoResponsabileDb.IDUtenteUltimaModifica = idUtenteIns;
                                            terzoResponsabileDb.fAttivo = true;

                                            ctx.LIM_LibrettiImpiantiResponsabili.Add(terzoResponsabileDb);
                                        }
                                    }
                                    #endregion
                                }

                                #region Tipologia generatori --> LIM_LibrettiImpiantiTipologiaGeneratori
                                if (librettoImpianto.TipologiaGeneratori != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaGeneratori.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaGeneratori.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaGeneratori.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaGeneratori.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaGeneratori)
                                    {
                                        var tipologiageneratoriDb = new LIM_LibrettiImpiantiTipologiaGeneratori();
                                        tipologiageneratoriDb.LIM_LibrettiImpianti = libretto;
                                        tipologiageneratoriDb.IDTipologiaGeneratori = item;
                                        if (item == 1)
                                        {
                                            tipologiageneratoriDb.TipologiaGeneratoriAltro = librettoImpianto.TipologiaGeneratoriAltro;
                                        }

                                        ctx.LIM_LibrettiImpiantiTipologiaGeneratori.Add(tipologiageneratoriDb);
                                    }
                                }
                                #endregion

                                #region Addolcimento acqua --> LIM_LibrettiImpiantiAddolcimentoAcqua
                                if (librettoImpianto.TipologiaAddolcimentoAcqua != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiAddolcimentoAcqua.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiAddolcimentoAcqua.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiAddolcimentoAcqua.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiAddolcimentoAcqua.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaAddolcimentoAcqua)
                                    {
                                        var addolcimentoDb = new LIM_LibrettiImpiantiAddolcimentoAcqua();
                                        addolcimentoDb.LIM_LibrettiImpianti = libretto;
                                        addolcimentoDb.IDTipologiaAddolcimentoAcqua = item;
                                        if (item == 1)
                                        {
                                            addolcimentoDb.AddolcimentoAcquaAltro = librettoImpianto.TipologiaAddolcimentoAcquaAltro;
                                        }

                                        ctx.LIM_LibrettiImpiantiAddolcimentoAcqua.Add(addolcimentoDb);
                                    }
                                }
                                #endregion

                                #region Condizionamento chimico --> LIM_LibrettiImpiantiCondizionamentoChimico
                                if (librettoImpianto.TipologiaCondizionamentoChimico != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiCondizionamentoChimico.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiCondizionamentoChimico.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiCondizionamentoChimico.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiCondizionamentoChimico.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaCondizionamentoChimico)
                                    {
                                        var condizionamentoDb = new LIM_LibrettiImpiantiCondizionamentoChimico();
                                        condizionamentoDb.LIM_LibrettiImpianti = libretto;
                                        condizionamentoDb.IDTipologiaCondizionamentoChimico = item;
                                        if (item == 1)
                                        {
                                            condizionamentoDb.TipologiaCondizionamentoChimicoAltro = librettoImpianto.TipologiaCondizionamentoChimicoAltro;
                                        }

                                        ctx.LIM_LibrettiImpiantiCondizionamentoChimico.Add(condizionamentoDb);
                                    }
                                }
                                #endregion

                                #region Consumo energia elettrica --> LIM_LibrettiImpiantiConsumoEnergiaElettrica
                                if (librettoImpianto.ConsumiEnergiaElettrica != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoEnergiaElettrica.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoEnergiaElettrica.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoEnergiaElettrica.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoEnergiaElettrica.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.ConsumiEnergiaElettrica)
                                    {
                                        var consumienergiaelettricaDb = new LIM_LibrettiImpiantiConsumoEnergiaElettrica();
                                        consumienergiaelettricaDb.LIM_LibrettiImpianti = libretto;
                                        consumienergiaelettricaDb.DataEsercizioStart = item.DataEsercizioStart;
                                        consumienergiaelettricaDb.DataEsercizioEnd = item.DataEsercizioEnd;
                                        consumienergiaelettricaDb.LetturaIniziale = item.LetturaIniziale;
                                        consumienergiaelettricaDb.LetturaFinale = item.LetturaFinale;
                                        consumienergiaelettricaDb.ConsumoTotale = item.ConsumoTotale;

                                        ctx.LIM_LibrettiImpiantiConsumoEnergiaElettrica.Add(consumienergiaelettricaDb);
                                    }
                                }
                                #endregion

                                #region Consumo acqua --> LIM_LibrettiImpiantiConsumoAcqua
                                if (librettoImpianto.ConsumiAcqua != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoAcqua.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoAcqua.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoAcqua.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoAcqua.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.ConsumiAcqua)
                                    {
                                        var consumiacquaDb = new LIM_LibrettiImpiantiConsumoAcqua();
                                        consumiacquaDb.LIM_LibrettiImpianti = libretto;
                                        consumiacquaDb.DataEsercizioStart = item.DataEsercizioStart;
                                        consumiacquaDb.DataEsercizioEnd = item.DataEsercizioEnd;
                                        consumiacquaDb.LetturaIniziale = item.LetturaIniziale;
                                        consumiacquaDb.LetturaFinale = item.LetturaFinale;
                                        consumiacquaDb.ConsumoTotale = item.ConsumoTotale;
                                        consumiacquaDb.IDUnitaMisura = item.UnitaMisura;

                                        ctx.LIM_LibrettiImpiantiConsumoAcqua.Add(consumiacquaDb);
                                    }
                                }
                                #endregion

                                #region Consumo combustibile --> LIM_LibrettiImpiantiConsumoCombustibile
                                if (librettoImpianto.ConsumiCombustibile != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoCombustibile.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoCombustibile.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoCombustibile.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoCombustibile.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.ConsumiCombustibile)
                                    {
                                        var consumicombustibileDb = new LIM_LibrettiImpiantiConsumoCombustibile();
                                        consumicombustibileDb.LIM_LibrettiImpianti = libretto;
                                        consumicombustibileDb.DataEsercizioStart = item.DataEsercizioStart;
                                        consumicombustibileDb.DataEsercizioEnd = item.DataEsercizioEnd;
                                        consumicombustibileDb.Acquisti = item.Acquisti;
                                        consumicombustibileDb.LetturaIniziale = item.LetturaIniziale;
                                        consumicombustibileDb.LetturaFinale = item.LetturaFinale;
                                        consumicombustibileDb.Consumo = item.Consumo;
                                        consumicombustibileDb.IDTipologiaCombustibile = item.TipologiaCombustibile;
                                        consumicombustibileDb.CombustibileAltro = item.CombustibileAltro;
                                        consumicombustibileDb.IDUnitaMisura = item.UnitaMisura;

                                        ctx.LIM_LibrettiImpiantiConsumoCombustibile.Add(consumicombustibileDb);
                                    }
                                }
                                #endregion

                                #region Consumo prodotti chimici --> LIM_LibrettiImpiantiConsumoProdottiChimici
                                if (librettoImpianto.ConsumiProdottiChimici != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoProdottiChimici.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoProdottiChimici.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiConsumoProdottiChimici.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiConsumoProdottiChimici.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.ConsumiProdottiChimici)
                                    {
                                        var consumiprodottichimiciDb = new LIM_LibrettiImpiantiConsumoProdottiChimici();
                                        consumiprodottichimiciDb.LIM_LibrettiImpianti = libretto;
                                        consumiprodottichimiciDb.DataEsercizioStart = item.DataEsercizioStart;
                                        consumiprodottichimiciDb.DataEsercizioEnd = item.DataEsercizioEnd;
                                        consumiprodottichimiciDb.fCircuitoImpiantoTermico = item.fCircuitoImpiantoTermico;
                                        consumiprodottichimiciDb.fCircuitoAcs = item.fCircuitoAcs;
                                        consumiprodottichimiciDb.fAltriCircuiti = item.fAltriCircuiti;
                                        consumiprodottichimiciDb.NomeProdotto = item.NomeProdotto;
                                        consumiprodottichimiciDb.Consumo = item.Consumo;
                                        consumiprodottichimiciDb.IDUnitaMisura = item.UnitaMisura;

                                        ctx.LIM_LibrettiImpiantiConsumoProdottiChimici.Add(consumiprodottichimiciDb);
                                    }
                                }
                                #endregion

                                #region Tipologia sistema di distribuzione --> LIM_LibrettiImpiantiTipologiaSistemaDistribuzione
                                if (librettoImpianto.TipologiaSistemaDistribuzione != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaSistemaDistribuzione)
                                    {
                                        var sistemadistribuzioneDb = new LIM_LibrettiImpiantiTipologiaSistemaDistribuzione();
                                        sistemadistribuzioneDb.LIM_LibrettiImpianti = libretto;
                                        sistemadistribuzioneDb.IDTipologiaSistemaDistribuzione = item;
                                        if (item == 1)
                                        {
                                            sistemadistribuzioneDb.TipologiaSistemaDistribuzioneAltro = librettoImpianto.TipologiaSistemaDistribuzioneAltro;
                                        }

                                        ctx.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Add(sistemadistribuzioneDb);
                                    }
                                }
                                #endregion

                                #region Tipologia fluido vettore --> LIM_LibrettiImpiantiTipologiaFluidoVettore
                                if (librettoImpianto.TipologiaFluidoVettore != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaFluidoVettore.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaFluidoVettore.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaFluidoVettore.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaFluidoVettore.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaFluidoVettore)
                                    {
                                        var fluidovettoreDb = new LIM_LibrettiImpiantiTipologiaFluidoVettore();
                                        fluidovettoreDb.LIM_LibrettiImpianti = libretto;
                                        fluidovettoreDb.IDTipologiaFluidoVettore = item;
                                        if (item == 1)
                                        {
                                            fluidovettoreDb.TipologiaFluidoVettoreAltro = librettoImpianto.TipologiaFluidoVettoreAltro;
                                        }

                                        ctx.LIM_LibrettiImpiantiTipologiaFluidoVettore.Add(fluidovettoreDb);
                                    }
                                }
                                #endregion

                                #region Tipologia sistemi emissione --> LIM_LibrettiImpiantiTipologiaSistemiEmissione
                                if (librettoImpianto.TipologiaSistemiEmissione != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaSistemiEmissione.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaSistemiEmissione.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaSistemiEmissione.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaSistemiEmissione.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaSistemiEmissione)
                                    {
                                        var sistemiemissioneDb = new LIM_LibrettiImpiantiTipologiaSistemiEmissione();
                                        sistemiemissioneDb.LIM_LibrettiImpianti = libretto;
                                        sistemiemissioneDb.IDTipologiaSistemiEmissione = item;

                                        ctx.LIM_LibrettiImpiantiTipologiaSistemiEmissione.Add(sistemiemissioneDb);
                                    }
                                }
                                #endregion

                                #region Trattamento acqua Acs --> LIM_LibrettiImpiantiTrattamentoAcquaAcs
                                if (librettoImpianto.TipologiaTrattamentoAcquaAcs != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaTrattamentoAcquaAcs)
                                    {
                                        var trattamentoacquaacsDb = new LIM_LibrettiImpiantiTrattamentoAcquaAcs();
                                        trattamentoacquaacsDb.LIM_LibrettiImpianti = libretto;
                                        trattamentoacquaacsDb.IDTipologiaTrattamentoAcqua = item;

                                        ctx.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Add(trattamentoacquaacsDb);
                                    }
                                }
                                #endregion

                                #region Trattamento acqua Estiva --> LIM_LibrettiImpiantiTrattamentoAcquaEstiva
                                if (librettoImpianto.TipologiaTrattamentoAcquaEstiva != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaTrattamentoAcquaEstiva)
                                    {
                                        var trattamentoacquaestivaDb = new LIM_LibrettiImpiantiTrattamentoAcquaEstiva();
                                        trattamentoacquaestivaDb.LIM_LibrettiImpianti = libretto;
                                        trattamentoacquaestivaDb.IDTipologiaTrattamentoAcqua = item;

                                        ctx.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Add(trattamentoacquaestivaDb);
                                    }
                                }
                                #endregion

                                #region Trattamento acqua Invernale --> LIM_LibrettiImpiantiTrattamentoAcquaInvernale
                                if (librettoImpianto.TipologiaTrattamentoAcquaInvernale != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaTrattamentoAcquaInvernale)
                                    {
                                        var trattamentoacquainvernaleaDb = new LIM_LibrettiImpiantiTrattamentoAcquaInvernale();
                                        trattamentoacquainvernaleaDb.LIM_LibrettiImpianti = libretto;
                                        trattamentoacquainvernaleaDb.IDTipologiaTrattamentoAcqua = item;

                                        ctx.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Add(trattamentoacquainvernaleaDb);
                                    }
                                }
                                #endregion

                                #region Tipologia filtrazioni --> LIM_LibrettiImpiantiTipologiaFiltrazioni
                                if (librettoImpianto.TipologiaFiltrazione != null)
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaFiltrazioni.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaFiltrazioni.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiTipologiaFiltrazioni.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiTipologiaFiltrazioni.Remove(dati);
                                        }
                                    }

                                    foreach (var item in librettoImpianto.TipologiaFiltrazione)
                                    {
                                        var tipologiafiltrazioniDb = new LIM_LibrettiImpiantiTipologiaFiltrazioni();
                                        tipologiafiltrazioniDb.LIM_LibrettiImpianti = libretto;
                                        tipologiafiltrazioniDb.IDTipologiaFiltrazione = item;
                                        if (item == 1)
                                        {
                                            tipologiafiltrazioniDb.TipologiaFiltrazioneAltro = librettoImpianto.TipologiaFiltrazioneAltro;
                                        }

                                        ctx.LIM_LibrettiImpiantiTipologiaFiltrazioni.Add(tipologiafiltrazioniDb);
                                    }
                                }
                                #endregion

                                #region Descrizione Sistema Telematico --> LIM_LibrettiImpiantiDescrizioniSistemi
                                if (!string.IsNullOrEmpty(librettoImpianto.DescrizioneSistemaTelematico))
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt && a.IDTipoSistema == 1).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto && a.IDTipoSistema == 1).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Remove(dati);
                                        }
                                    }

                                    var descrizioniSistemaTelamaticoDb = new LIM_LibrettiImpiantiDescrizioniSistemi();
                                    descrizioniSistemaTelamaticoDb.LIM_LibrettiImpianti = libretto;
                                    descrizioniSistemaTelamaticoDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                    descrizioniSistemaTelamaticoDb.IDTipoSistema = 1;
                                    descrizioniSistemaTelamaticoDb.DescrizioneSistema = librettoImpianto.DescrizioneSistemaTelematico;
                                    descrizioniSistemaTelamaticoDb.DataInserimento = DateTime.Now;
                                    descrizioniSistemaTelamaticoDb.DataUltimaModifica = DateTime.Now;
                                    descrizioniSistemaTelamaticoDb.IDUtenteInserimento = idUtenteIns;
                                    descrizioniSistemaTelamaticoDb.IDUtenteUltimaModifica = idUtenteIns;
                                    descrizioniSistemaTelamaticoDb.fAttivo = true;

                                    ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Add(descrizioniSistemaTelamaticoDb);
                                }
                                #endregion

                                #region Descrizione Sistema Contabilizzazione --> LIM_LibrettiImpiantiDescrizioniSistemi
                                if (!string.IsNullOrEmpty(librettoImpianto.DescrizioneSistemaContabilizzazione))
                                {
                                    if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                    {
                                        int iDLibrettoImpiantoInt = int.Parse(getvaluateOperation[0].ToString());
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Where(a => a.IDLibrettoImpianto == iDLibrettoImpiantoInt && a.IDTipoSistema == 2).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Remove(dati);
                                        }
                                    }

                                    if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                    {
                                        var datiAttuali = ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Where(a => a.IDLibrettoImpianto == libretto.IDLibrettoImpianto && a.IDTipoSistema == 2).ToList();
                                        foreach (var dati in datiAttuali)
                                        {
                                            ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Remove(dati);
                                        }
                                    }

                                    var descrizioniSistemaContabilizzazioneDb = new LIM_LibrettiImpiantiDescrizioniSistemi();
                                    descrizioniSistemaContabilizzazioneDb.LIM_LibrettiImpianti = libretto;
                                    descrizioniSistemaContabilizzazioneDb.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                                    descrizioniSistemaContabilizzazioneDb.IDTipoSistema = 2;
                                    descrizioniSistemaContabilizzazioneDb.DescrizioneSistema = librettoImpianto.DescrizioneSistemaContabilizzazione;
                                    descrizioniSistemaContabilizzazioneDb.DataInserimento = DateTime.Now;
                                    descrizioniSistemaContabilizzazioneDb.DataUltimaModifica = DateTime.Now;
                                    descrizioniSistemaContabilizzazioneDb.IDUtenteInserimento = idUtenteIns;
                                    descrizioniSistemaContabilizzazioneDb.IDUtenteUltimaModifica = idUtenteIns;
                                    descrizioniSistemaContabilizzazioneDb.fAttivo = true;

                                    ctx.LIM_LibrettiImpiantiDescrizioniSistemi.Add(descrizioniSistemaContabilizzazioneDb);
                                }
                                #endregion


                                if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                                {
                                    ctx.LIM_LibrettiImpianti.Add(libretto);
                                }

                                ctx.SaveChanges();
                                dbContextTransaction.Commit();

                                if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                                {
                                    result = "Libretto Impianto inserito correttamente con codice targatura: " + librettoImpianto.CodiceTargaturaImpianto.CodiceTargatura;
                                }
                                else if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                                {
                                    result = "Libretto Impianto aggiornato correttamente con codice targatura: " + librettoImpianto.CodiceTargaturaImpianto.CodiceTargatura;
                                }
                                else if (int.Parse(getvaluateOperation[2].ToString()) == 3) //Revisione
                                {
                                    result = "Libretto Impianto revisionato correttamente con codice targatura: " + librettoImpianto.CodiceTargaturaImpianto.CodiceTargatura;
                                }

                                if (int.Parse(getvaluateOperation[2].ToString()) == 1 || int.Parse(getvaluateOperation[2].ToString()) == 2) //Inserimento o Aggiornamento
                                {
                                    ReplaceIDLibrettoImpiantoInserimento(ctx, libretto);
                                }
                            }
                            catch (DbEntityValidationException ex)
                            {
                                dbContextTransaction.Rollback();
                                Exception raise = ex;
                                foreach (var validationErrors in ex.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }

                                result = "Libretto impianto non inserito correttamente " + raise.Message;
                            }
                            catch (Exception e)
                            {
                                if (!string.IsNullOrEmpty(e.InnerException.InnerException.Message))
                                {
                                    result = "Libretto impianto non inserito correttamente " + e.InnerException.InnerException.Message;
                                }
                                else
                                {
                                    result = "Libretto impianto non inserito correttamente " + e.Message;
                                }
                            }
                            finally
                            {
                                ctx.Dispose();
                            }
                        }
                    }
                    #endregion
                }
            }
            else
            {
                result = "Libretto impianto non inserito correttamente perchè il codice catastale " + librettoImpianto.CodiceCatastaleComune + " non è valido";
            }

            return result;
        }

        public static void ReplaceIDLibrettoImpiantoInserimento(CriterDataModel ctx, LIM_LibrettiImpianti libretto)
        {
            libretto.LIM_LibrettiImpiantiAccumuli.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            foreach (var gruppitermici in libretto.LIM_LibrettiImpiantiGruppiTermici)
            {
                gruppitermici.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                foreach (var bruciatori in gruppitermici.LIM_LibrettiImpiantiBruciatori)
                {
                    bruciatori.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                }
                foreach (var recuperatori in gruppitermici.LIM_LibrettiImpiantiRecuperatori)
                {
                    recuperatori.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto;
                }
            }
            libretto.LIM_LibrettiImpiantiAltriGeneratori.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiCampiSolariTermici.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiCircuitiInterrati.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiCogeneratori.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiPompeCircolazione.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiRecuperatoriCalore.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiScambiatoriCalore.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiRaffreddatoriLiquido.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiTorriEvaporative.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiUnitaTrattamentoAria.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiValvoleRegolazione.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiVasiEspansione.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiMacchineFrigorifere.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiImpiantiVMC.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);
            libretto.LIM_LibrettiImpiantiDescrizioniSistemi.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpianto).ToList().ForEach(m => m.IDLibrettoImpiantoInserimento = libretto.IDLibrettoImpianto);

            ctx.SaveChanges();
        }

        public static string LoadRapportoControlloTecnico_GT(POMJ_RapportoControlloTecnico_GT rapportoControllo)
        {
            string result = "";
            object[] getvaluateOperation = new object[3];
            getvaluateOperation = EvaluateOperationRapportiFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, rapportoControllo.CodiceProgressivo, "GT", rapportoControllo.DataControllo, rapportoControllo.OraArrivo, rapportoControllo.OraPartenza, rapportoControllo.GuidRapportoTecnico);

            #region Rapporto di Controllo
            using (var ctx = new CriterDataModel())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var rapporto = new RCT_RapportoDiControlloTecnicoBase();

                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt);
                        }

                        object[] getVal = new object[2];
                        getVal = GetIDSoggettiFromCodiceSoggetto(rapportoControllo.CodiceSoggetto);

                        int iDLibrettoImpianto = (int)GetIDLibrettoImpiantoFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);

                        #region Testata Rapporto --> RCT_RapportoDiControlloTecnicoBase
                        if (!string.IsNullOrEmpty(getVal[0].ToString()) && !string.IsNullOrEmpty(getVal[1].ToString()))
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = Convert.ToInt32(getVal[0]);
                        }
                        else
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = null;
                        }

                        rapporto.IDTargaturaImpianto = (int)GetIDTargaturaImpiantoFromCodiceTargatura(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, Convert.ToInt32(getVal[1]), "RCT");
                        rapporto.PotenzaTermicaNominale = rapportoControllo.PotenzaTermicaNominale;
                        if (!string.IsNullOrEmpty(rapportoControllo.Indirizzo))
                        {
                            rapporto.Indirizzo = rapportoControllo.Indirizzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Civico))
                        {
                            rapporto.Civico = rapportoControllo.Civico;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Palazzo))
                        {
                            rapporto.Palazzo = rapportoControllo.Palazzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Scala))
                        {
                            rapporto.Scala = rapportoControllo.Scala;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Interno))
                        {
                            rapporto.Interno = rapportoControllo.Interno;
                        }
                        rapporto.IDCodiceCatastale = GetIDCodiceCatastaleFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);
                        rapporto.IDTipologiaRCT = 1;
                        rapporto.IDLibrettoImpianto = iDLibrettoImpianto;
                        rapporto.IDStatoRapportoDiControllo = 1;
                        rapporto.IDTipologiaControllo = rapportoControllo.TipologiaControllo;
                        rapporto.IDTipologiaResponsabile = rapportoControllo.TipologiaResponsabile;
                        if (rapportoControllo.ResponsabileImpianto.TipoSoggetto != null)
                        {
                            rapporto.IDTipoSoggetto = rapportoControllo.ResponsabileImpianto.TipoSoggetto;
                        }
                        else
                        {
                            rapporto.IDTipoSoggetto = 1;
                        }
                        rapporto.NomeResponsabile = rapportoControllo.ResponsabileImpianto.Nome;
                        rapporto.CognomeResponsabile = rapportoControllo.ResponsabileImpianto.Cognome;
                        rapporto.CodiceFiscaleResponsabile = rapportoControllo.ResponsabileImpianto.CodiceFiscale;
                        rapporto.RagioneSocialeResponsabile = rapportoControllo.ResponsabileImpianto.NomeAzienda;
                        rapporto.PartitaIVAResponsabile = rapportoControllo.ResponsabileImpianto.PartitaIVA;
                        rapporto.IndirizzoResponsabile = rapportoControllo.ResponsabileImpianto.Indirizzo;
                        rapporto.CivicoResponsabile = rapportoControllo.ResponsabileImpianto.Civico;
                        rapporto.ComuneResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);
                        //if (rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale != 0)
                        //{
                        rapporto.IDProvinciaResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);// rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale;
                        //}

                        if (rapportoControllo.TerzoResponsabile != null)
                        {
                            rapporto.RagioneSocialeTerzoResponsabile = rapportoControllo.TerzoResponsabile.NomeAzienda;
                            rapporto.PartitaIVATerzoResponsabile = rapportoControllo.TerzoResponsabile.PartitaIVA;
                            rapporto.IndirizzoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Indirizzo;
                            rapporto.CivicoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Civico;
                            rapporto.ComuneTerzoResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune);
                            //if (rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale != 0)
                            //{
                            rapporto.IDProvinciaTerzoResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune); //rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale;
                            //}
                        }

                        var azienda = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == rapporto.IDSoggetto);
                        if (azienda != null)
                        {
                            rapporto.RagioneSocialeImpresaManutentrice = azienda.NomeAzienda;
                            rapporto.PartitaIVAImpresaManutentrice = azienda.PartitaIVA;
                            rapporto.IndirizzoImpresaManutentrice = azienda.IndirizzoSedeLegale;
                            rapporto.CivicoImpresaManutentrice = azienda.NumeroCivicoSedeLegale;
                            rapporto.ComuneImpresaManutentrice = azienda.CittaSedeLegale;
                            rapporto.IDProvinciaImpresaManutentrice = azienda.IDProvinciaSedeLegale;
                        }


                        //rapporto.RagioneSocialeImpresaManutentrice = rapportoControllo.ImpresaManutentrice.NomeAzienda;
                        //rapporto.PartitaIVAImpresaManutentrice = rapportoControllo.ImpresaManutentrice.PartitaIVA;
                        //rapporto.IndirizzoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Indirizzo;
                        //rapporto.CivicoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Civico;
                        //rapporto.ComuneImpresaManutentrice = GetComuneFromCodiceCatastale(rapportoControllo.ImpresaManutentrice.CodiceCatastaleComune);
                        //if (rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale != 0)
                        //{
                        //    rapporto.IDProvinciaImpresaManutentrice = rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale;
                        //}



                        if (rapportoControllo.LocaleInstallazioneIdoneo != null)
                        {
                            rapporto.LocaleInstallazioneIdoneo = rapportoControllo.LocaleInstallazioneIdoneo;
                        }
                        else
                        {
                            rapporto.LocaleInstallazioneIdoneo = -1;
                        }
                        rapporto.PotenzaTermicaNominaleTotaleMax = rapportoControllo.PotenzaTermicaNominaleTotaleMax;
                        rapporto.fDichiarazioneConformita = rapportoControllo.fDichiarazioneConformita;
                        rapporto.fLibrettoImpiantoPresente = rapportoControllo.fLibrettoImpiantoPresente;
                        rapporto.fUsoManutenzioneGeneratore = rapportoControllo.fUsoManutenzioneGeneratore;
                        rapporto.fLibrettoImpiantoCompilato = rapportoControllo.fLibrettoImpiantoCompilato;
                        rapporto.DurezzaAcqua = rapportoControllo.DurezzaAcqua;
                        if (rapportoControllo.TrattamentoACS != null)
                        {
                            rapporto.TrattamentoACS = rapportoControllo.TrattamentoACS;
                        }
                        else
                        {
                            rapporto.TrattamentoACS = 0;
                        }
                        if (rapportoControllo.TrattamentoRiscaldamento != null)
                        {
                            rapporto.TrattamentoRiscaldamento = rapportoControllo.TrattamentoRiscaldamento;
                        }
                        else
                        {
                            rapporto.TrattamentoRiscaldamento = 0;
                        }
                        if (rapportoControllo.ApertureLibere != null)
                        {
                            rapporto.ApertureLibere = rapportoControllo.ApertureLibere;
                        }
                        else
                        {
                            rapporto.ApertureLibere = -1;
                        }
                        if (rapportoControllo.DimensioniApertureAdeguate != null)
                        {
                            rapporto.DimensioniApertureAdeguate = rapportoControllo.DimensioniApertureAdeguate;
                        }
                        else
                        {
                            rapporto.DimensioniApertureAdeguate = -1;
                        }
                        rapporto.LineeElettricheIdonee = null;
                        rapporto.CoibentazioniIdonee = -1;
                        rapporto.StatoCoibentazioniIdonee = null;
                        if (rapportoControllo.AssenzaPerditeCombustibile != null)
                        {
                            rapporto.AssenzaPerditeCombustibile = rapportoControllo.AssenzaPerditeCombustibile;
                        }
                        else
                        {
                            rapporto.AssenzaPerditeCombustibile = -1;
                        }
                        if (rapportoControllo.TenutaImpiantoIdraulico != null)
                        {
                            rapporto.TenutaImpiantoIdraulico = rapportoControllo.TenutaImpiantoIdraulico;
                        }
                        else
                        {
                            rapporto.TenutaImpiantoIdraulico = -1;
                        }
                        if (rapportoControllo.ScarichiIdonei != null)
                        {
                            rapporto.ScarichiIdonei = rapportoControllo.ScarichiIdonei;
                        }
                        else
                        {
                            rapporto.ScarichiIdonei = -1;
                        }

                        rapporto.Prefisso = "GT";
                        rapporto.CodiceProgressivo = rapportoControllo.CodiceProgressivo;
                        rapporto.DataInstallazione = rapportoControllo.DataInstallazione;
                        rapporto.Fabbricante = rapportoControllo.Fabbricante;
                        rapporto.Modello = rapportoControllo.Modello;
                        rapporto.Matricola = rapportoControllo.Matricola;
                        rapporto.fClimatizzazioneInvernale = rapportoControllo.fClimatizzazioneInvernale;
                        rapporto.fProduzioneACS = rapportoControllo.fProduzioneACS;
                        rapporto.fClimatizzazioneEstiva = false;
                        rapporto.IDTipologiaCombustibile = rapportoControllo.TipologiaCombustibile;
                        rapporto.AltroCombustibile = rapportoControllo.AltroCombustibile;
                        rapporto.IDTipologiaFluidoTermoVettore = null;
                        if (!string.IsNullOrEmpty(rapportoControllo.Osservazioni))
                        {
                            rapporto.Osservazioni = rapportoControllo.Osservazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Raccomandazioni))
                        {
                            rapporto.Raccomandazioni = rapportoControllo.Raccomandazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Prescrizioni))
                        {
                            rapporto.Prescrizioni = rapportoControllo.Prescrizioni;
                        }
                        rapporto.fImpiantoFunzionante = rapportoControllo.fImpiantoFunzionante;
                        if (rapportoControllo.DataManutenzioneConsigliata != null)
                        {
                            rapporto.DataManutenzioneConsigliata = rapportoControllo.DataManutenzioneConsigliata;
                        }
                        rapporto.DataControllo = rapportoControllo.DataControllo;
                        if (rapportoControllo.OraArrivo != null)
                        {
                            rapporto.OraArrivo = rapportoControllo.OraArrivo;
                        }
                        else
                        {
                            rapporto.OraArrivo = DateTime.Now;
                        }
                        if (rapportoControllo.OraPartenza != null)
                        {
                            rapporto.OraPartenza = rapportoControllo.OraPartenza;
                        }
                        else
                        {
                            rapporto.OraPartenza = DateTime.Now;
                        }
                        rapporto.IDTipologiaSistemaDistribuzione = rapportoControllo.TipologiaSistemaDistribuzione;
                        rapporto.AltroTipologiaSistemaDistribuzione = null;
                        if (rapportoControllo.Contabilizzazione != null && rapportoControllo.Contabilizzazione != -1)
                        {
                            rapporto.Contabilizzazione = rapportoControllo.Contabilizzazione;
                        }
                        else
                        {
                            rapporto.Contabilizzazione = -2;
                        }
                        rapporto.IDTipologiaContabilizzazione = rapportoControllo.TipologiaContabilizzazione;
                        if (rapportoControllo.Termoregolazione != null && rapportoControllo.Termoregolazione != -1)
                        {
                            rapporto.Termoregolazione = rapportoControllo.Termoregolazione;
                        }
                        else
                        {
                            rapporto.Termoregolazione = -2;
                        }
                        if (rapportoControllo.CorrettoFunzionamentoContabilizzazione != null
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != -1
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != 2)
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = rapportoControllo.CorrettoFunzionamentoContabilizzazione;
                        }
                        else
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = -2;
                        }
                        rapporto.JsonFormat = JsonConvert.SerializeObject(rapportoControllo);
                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            rapporto.GuidRapportoTecnico = Guid.NewGuid().ToString();
                            rapporto.DataInserimento = DateTime.Now;
                        }
                        rapporto.DataAnnullamento = null;

                        if (!string.IsNullOrEmpty(rapportoControllo.CriterAPIKey))
                        {
                            rapporto.KeyApi = rapportoControllo.CriterAPIKey;
                        }

                        #endregion

                        #region Dettaglio Dati GT --> RCT_RapportoDiControlloTecnicoGT
                        var rapportoGT = new RCT_RapportoDiControlloTecnicoGT();
                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapportoGT = ctx.RCT_RapportoDiControlloTecnicoGT.FirstOrDefault(a => a.Id == iDRapportoControlloTecnicoInt);
                        }

                        rapportoGT.RCT_RapportoDiControlloTecnicoBase = rapporto;
                        rapportoGT.TemperaturaFumi = rapportoControllo.TemperaturaFumi;
                        if (rapportoControllo.GeneratoriIdonei != null)
                        {
                            rapportoGT.GeneratoriIdonei = rapportoControllo.GeneratoriIdonei;
                        }
                        else
                        {
                            rapportoGT.GeneratoriIdonei = -1;
                        }
                        if (rapportoControllo.RegolazioneTemperaturaAmbiente != null)
                        {
                            rapportoGT.RegolazioneTemperaturaAmbiente = rapportoControllo.RegolazioneTemperaturaAmbiente;
                        }
                        else
                        {
                            rapportoGT.RegolazioneTemperaturaAmbiente = -1;
                        }
                        rapportoGT.EvacuazioneForzata = rapportoControllo.EvacuazioneForzata;
                        rapportoGT.EvacuazioneNaturale = rapportoControllo.EvacuazioneNaturale;
                        rapportoGT.DepressioneCanaleFumo = rapportoControllo.DepressioneCanaleFumo;
                        rapportoGT.TemperaraturaComburente = rapportoControllo.TemperaraturaComburente;
                        rapportoGT.O2 = rapportoControllo.O2;
                        rapportoGT.Co2 = rapportoControllo.Co2;
                        rapportoGT.RendimentoCombustione = rapportoControllo.RendimentoCombustione;
                        rapportoGT.RendimentoMinimo = rapportoControllo.RendimentoMinimo;
                        if (rapportoControllo.DispositiviComandoRegolazione != null)
                        {
                            rapportoGT.DispositiviComandoRegolazione = rapportoControllo.DispositiviComandoRegolazione;
                        }
                        else
                        {
                            rapportoGT.DispositiviComandoRegolazione = -1;
                        }
                        if (rapportoControllo.DispositiviSicurezza != null)
                        {
                            rapportoGT.DispositiviSicurezza = rapportoControllo.DispositiviSicurezza;
                        }
                        else
                        {
                            rapportoGT.DispositiviSicurezza = -1;
                        }
                        if (rapportoControllo.ValvolaSicurezzaSovrappressione != null)
                        {
                            rapportoGT.ValvolaSicurezzaSovrappressione = rapportoControllo.ValvolaSicurezzaSovrappressione;
                        }
                        else
                        {
                            rapportoGT.ValvolaSicurezzaSovrappressione = -1;
                        }
                        if (rapportoControllo.ScambiatoreFumiPulito != null)
                        {
                            rapportoGT.ScambiatoreFumiPulito = rapportoControllo.ScambiatoreFumiPulito;
                        }
                        else
                        {
                            rapportoGT.ScambiatoreFumiPulito = -1;
                        }
                        if (rapportoControllo.RiflussoProdottiCombustione != null)
                        {
                            rapportoGT.RiflussoProdottiCombustione = rapportoControllo.RiflussoProdottiCombustione;
                        }
                        else
                        {
                            rapportoGT.RiflussoProdottiCombustione = -1;
                        }
                        if (rapportoControllo.ConformitaUNI10389 != null)
                        {
                            rapportoGT.ConformitaUNI10389 = rapportoControllo.ConformitaUNI10389;
                        }
                        else
                        {
                            rapportoGT.ConformitaUNI10389 = -1;
                        }
                        rapportoGT.IdTipologiaGruppiTermici = rapportoControllo.TipologiaGruppiTermici;
                        rapportoGT.bacharach1 = rapportoControllo.bacharach1;
                        rapportoGT.bacharach2 = rapportoControllo.bacharach2;
                        rapportoGT.bacharach3 = rapportoControllo.bacharach3;
                        rapportoGT.CoCorretto = rapportoControllo.CoCorretto;
                        rapportoGT.ModuloTermico = rapportoControllo.ModuloTermico;
                        rapportoGT.PotenzaTermicaNominaleFocolare = rapportoControllo.PotenzaTermicaNominaleFocolare;
                        rapportoGT.IDLIM_LibrettiImpiantiGruppitermici = GetIDLibrettoImpiantoGruppoTermico(rapportoControllo.CodiceProgressivo, iDLibrettoImpianto);
                        rapportoGT.COFumiSecchi = rapportoControllo.COFumiSecchi;
                        rapportoGT.PortataCombustibile = rapportoControllo.PortataCombustibile;
                        rapportoGT.RispettaIndiceBacharach = rapportoControllo.RispettaIndiceBacharach;
                        rapportoGT.COFumiSecchiNoAria1000 = rapportoControllo.COFumiSecchiNoAria1000;
                        rapportoGT.RendimentoSupMinimo = rapportoControllo.RendimentoSupMinimo;
                        rapportoGT.IdTipologiaGeneratoriTermici = rapportoControllo.TipologiaGeneratoriTermici;
                        rapportoGT.PotenzaTermicaEffettiva = rapportoControllo.PotenzaTermicaEffettiva;

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoGT.Add(rapportoGT);
                        }
                        #endregion

                        #region CheckList --> RCT_RapportoDiControlloTecnicoBaseCheckList
                        if (rapportoControllo.CheckList != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.CheckList)
                            {
                                var checklistDb = new RCT_RapportoDiControlloTecnicoBaseCheckList();
                                checklistDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                checklistDb.IDCheckList = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Add(checklistDb);
                            }
                        }
                        #endregion

                        #region Trattamento in riscaldamento --> RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale
                        if (rapportoControllo.TipoTrattamentoRiscaldamento != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoRiscaldamento)
                            {
                                var trattamentoInvernaleDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale();
                                trattamentoInvernaleDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoInvernaleDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Add(trattamentoInvernaleDb);
                            }
                        }
                        #endregion

                        #region Trattamento in Acs --> RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs
                        if (rapportoControllo.TipoTrattamentoACS != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoACS)
                            {
                                var trattamentoAcsDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs();
                                trattamentoAcsDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoAcsDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Add(trattamentoAcsDb);
                            }
                        }
                        #endregion

                        #region Raccomandazioni/Prescrizioni
                        if (rapportoControllo.RaccomandazioniPrescrizioni != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiRaccomandazioniPrescrizioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                ctx.RCT_RaccomandazioniPrescrizioni.RemoveRange(datiRaccomandazioniPrescrizioniAttuali);
                            }

                            var listaRaccomandazioniPrescrizioni = rapportoControllo.RaccomandazioniPrescrizioni.Select(s => new RCT_RaccomandazioniPrescrizioni()
                            {
                                RCT_RapportoDiControlloTecnicoBase = rapporto,
                                IDTipologiaRaccomandazionePrescrizioneRct = s.IDCampoRct,
                                IDTipologiaRaccomandazione = (s.TipoNonConformita == "Raccomandazione") ? (int?)s.IDNonConformita : null,
                                IDTipologiaPrescrizione = (s.TipoNonConformita == "Prescrizione") ? (int?)s.IDNonConformita : null
                            });
                            ctx.RCT_RaccomandazioniPrescrizioni.AddRange(listaRaccomandazioniPrescrizioni);
                        }
                        #endregion

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        }
                        
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            result = "Rapporto di controllo Tecnico GT inserito correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                        else if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            result = "Rapporto di controllo Tecnico GT aggiornato correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbContextTransaction.Rollback();
                        Exception raise = ex;
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                raise = new InvalidOperationException(message, raise);
                            }
                        }

                        result = "Rapporto di controllo Tecnico GT non inserito correttamente " + raise.Message;
                    }
                    catch (Exception e)
                    {
                        if (!string.IsNullOrEmpty(e.InnerException.InnerException.Message))
                        {
                            result = "Rapporto di controllo Tecnico GT non inserito correttamente " + e.InnerException.InnerException.Message;
                        }
                        else
                        {
                            result = "Rapporto di controllo Tecnico GT non inserito correttamente " + e.Message;
                        }
                    }
                    finally
                    {
                        ctx.Dispose();
                    }

                }
            }
            #endregion

            return result;
        }

        public static string LoadRapportoControlloTecnico_GF(POMJ_RapportoControlloTecnico_GF rapportoControllo)
        {
            string result = "";

            object[] getvaluateOperation = new object[3];
            getvaluateOperation = EvaluateOperationRapportiFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, rapportoControllo.CodiceProgressivo, "GF", rapportoControllo.DataControllo, rapportoControllo.OraArrivo, rapportoControllo.OraPartenza, rapportoControllo.GuidRapportoTecnico);

            #region Rapporto di Controllo
            using (var ctx = new CriterDataModel())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var rapporto = new RCT_RapportoDiControlloTecnicoBase();
                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt);
                        }

                        object[] getVal = new object[2];
                        getVal = GetIDSoggettiFromCodiceSoggetto(rapportoControllo.CodiceSoggetto);
                        int iDLibrettoImpianto = (int)GetIDLibrettoImpiantoFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);

                        #region Testata Rapporto --> RCT_RapportoDiControlloTecnicoBase
                        if (!string.IsNullOrEmpty(getVal[0].ToString()) && !string.IsNullOrEmpty(getVal[1].ToString()))
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = Convert.ToInt32(getVal[0]);
                        }
                        else
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = null;
                        }
                        rapporto.IDTargaturaImpianto = (int)GetIDTargaturaImpiantoFromCodiceTargatura(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, Convert.ToInt32(getVal[1]), "RCT");
                        rapporto.PotenzaTermicaNominale = rapportoControllo.PotenzaTermicaNominale;
                        if (!string.IsNullOrEmpty(rapportoControllo.Indirizzo))
                        {
                            rapporto.Indirizzo = rapportoControllo.Indirizzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Civico))
                        {
                            rapporto.Civico = rapportoControllo.Civico;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Palazzo))
                        {
                            rapporto.Palazzo = rapportoControllo.Palazzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Scala))
                        {
                            rapporto.Scala = rapportoControllo.Scala;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Interno))
                        {
                            rapporto.Interno = rapportoControllo.Interno;
                        }
                        rapporto.IDCodiceCatastale = GetIDCodiceCatastaleFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);
                        rapporto.IDTipologiaRCT = 2;
                        rapporto.IDLibrettoImpianto = iDLibrettoImpianto;
                        rapporto.IDStatoRapportoDiControllo = 1;
                        rapporto.IDTipologiaControllo = rapportoControllo.TipologiaControllo;
                        rapporto.IDTipologiaResponsabile = rapportoControllo.TipologiaResponsabile;
                        if (rapportoControllo.ResponsabileImpianto.TipoSoggetto != null)
                        {
                            rapporto.IDTipoSoggetto = rapportoControllo.ResponsabileImpianto.TipoSoggetto;
                        }
                        else
                        {
                            rapporto.IDTipoSoggetto = 1;
                        }
                        rapporto.NomeResponsabile = rapportoControllo.ResponsabileImpianto.Nome;
                        rapporto.CognomeResponsabile = rapportoControllo.ResponsabileImpianto.Cognome;
                        rapporto.CodiceFiscaleResponsabile = rapportoControllo.ResponsabileImpianto.CodiceFiscale;
                        rapporto.RagioneSocialeResponsabile = rapportoControllo.ResponsabileImpianto.NomeAzienda;
                        rapporto.PartitaIVAResponsabile = rapportoControllo.ResponsabileImpianto.PartitaIVA;
                        rapporto.IndirizzoResponsabile = rapportoControllo.ResponsabileImpianto.Indirizzo;
                        rapporto.CivicoResponsabile = rapportoControllo.ResponsabileImpianto.Civico;
                        rapporto.ComuneResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);
                        //if (rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale != 0)
                        //{
                        rapporto.IDProvinciaResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);// rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale;
                        //}
                        if (rapportoControllo.TerzoResponsabile != null)
                        {
                            rapporto.RagioneSocialeTerzoResponsabile = rapportoControllo.TerzoResponsabile.NomeAzienda;
                            rapporto.PartitaIVATerzoResponsabile = rapportoControllo.TerzoResponsabile.PartitaIVA;
                            rapporto.IndirizzoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Indirizzo;
                            rapporto.CivicoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Civico;
                            rapporto.ComuneTerzoResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune);
                            //if (rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale != 0)
                            //{
                            rapporto.IDProvinciaTerzoResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune); //rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale;
                            //}
                        }

                        var azienda = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == rapporto.IDSoggetto);
                        if (azienda != null)
                        {
                            rapporto.RagioneSocialeImpresaManutentrice = azienda.NomeAzienda;
                            rapporto.PartitaIVAImpresaManutentrice = azienda.PartitaIVA;
                            rapporto.IndirizzoImpresaManutentrice = azienda.IndirizzoSedeLegale;
                            rapporto.CivicoImpresaManutentrice = azienda.NumeroCivicoSedeLegale;
                            rapporto.ComuneImpresaManutentrice = azienda.CittaSedeLegale;
                            rapporto.IDProvinciaImpresaManutentrice = azienda.IDProvinciaSedeLegale;
                        }

                        //rapporto.RagioneSocialeImpresaManutentrice = rapportoControllo.ImpresaManutentrice.NomeAzienda;
                        //rapporto.PartitaIVAImpresaManutentrice = rapportoControllo.ImpresaManutentrice.PartitaIVA;
                        //rapporto.IndirizzoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Indirizzo;
                        //rapporto.CivicoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Civico;
                        //rapporto.ComuneImpresaManutentrice = GetComuneFromCodiceCatastale(rapportoControllo.ImpresaManutentrice.CodiceCatastaleComune);
                        //if (rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale != 0)
                        //{
                        //    rapporto.IDProvinciaImpresaManutentrice = rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale;
                        //}


                        rapporto.LocaleInstallazioneIdoneo = rapportoControllo.LocaleInstallazioneIdoneo;
                        rapporto.PotenzaTermicaNominaleTotaleMax = rapportoControllo.PotenzaTermicaNominaleTotaleMax;
                        rapporto.fDichiarazioneConformita = rapportoControllo.fDichiarazioneConformita;
                        rapporto.fLibrettoImpiantoPresente = rapportoControllo.fLibrettoImpiantoPresente;
                        rapporto.fUsoManutenzioneGeneratore = rapportoControllo.fUsoManutenzioneGeneratore;
                        rapporto.fLibrettoImpiantoCompilato = rapportoControllo.fLibrettoImpiantoCompilato;
                        rapporto.DurezzaAcqua = rapportoControllo.DurezzaAcqua;
                        if (!string.IsNullOrEmpty(rapportoControllo.TrattamentoACS.ToString()))
                        {
                            rapporto.TrattamentoACS = rapportoControllo.TrattamentoACS;
                        }
                        else
                        {
                            rapporto.TrattamentoACS = 0;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.TrattamentoRiscaldamento.ToString()))
                        {
                            rapporto.TrattamentoRiscaldamento = rapportoControllo.TrattamentoRiscaldamento;
                        }
                        else
                        {
                            rapporto.TrattamentoRiscaldamento = 0;
                        }
                        rapporto.ApertureLibere = rapportoControllo.ApertureLibere;
                        rapporto.DimensioniApertureAdeguate = rapportoControllo.DimensioniApertureAdeguate;
                        rapporto.LineeElettricheIdonee = rapportoControllo.LineeElettricheIdonee;
                        rapporto.CoibentazioniIdonee = rapportoControllo.CoibentazioniIdonee;
                        rapporto.StatoCoibentazioniIdonee = -1;
                        rapporto.AssenzaPerditeCombustibile = -1;
                        rapporto.TenutaImpiantoIdraulico = -1;
                        rapporto.ScarichiIdonei = -1;
                        rapporto.Prefisso = "GF";
                        rapporto.CodiceProgressivo = rapportoControllo.CodiceProgressivo;
                        rapporto.DataInstallazione = rapportoControllo.DataInstallazione;
                        rapporto.Fabbricante = rapportoControllo.Fabbricante;
                        rapporto.Modello = rapportoControllo.Modello;
                        rapporto.Matricola = rapportoControllo.Matricola;
                        rapporto.fClimatizzazioneInvernale = rapportoControllo.fClimatizzazioneInvernale;
                        rapporto.fProduzioneACS = rapportoControllo.fProduzioneACS;
                        rapporto.fClimatizzazioneEstiva = rapportoControllo.fClimatizzazioneEstiva;
                        rapporto.IDTipologiaCombustibile = null;
                        rapporto.AltroCombustibile = null;
                        rapporto.IDTipologiaFluidoTermoVettore = null;
                        if (!string.IsNullOrEmpty(rapportoControllo.Osservazioni))
                        {
                            rapporto.Osservazioni = rapportoControllo.Osservazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Raccomandazioni))
                        {
                            rapporto.Raccomandazioni = rapportoControllo.Raccomandazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Prescrizioni))
                        {
                            rapporto.Prescrizioni = rapportoControllo.Prescrizioni;
                        }
                        rapporto.fImpiantoFunzionante = rapportoControllo.fImpiantoFunzionante;
                        if (rapportoControllo.DataManutenzioneConsigliata != null)
                        {
                            rapporto.DataManutenzioneConsigliata = rapportoControllo.DataManutenzioneConsigliata;
                        }
                        rapporto.DataControllo = rapportoControllo.DataControllo;
                        if (rapportoControllo.OraArrivo != null)
                        {
                            rapporto.OraArrivo = rapportoControllo.OraArrivo;
                        }
                        else
                        {
                            rapporto.OraArrivo = DateTime.Now;
                        }
                        if (rapportoControllo.OraPartenza != null)
                        {
                            rapporto.OraPartenza = rapportoControllo.OraPartenza;
                        }
                        else
                        {
                            rapporto.OraPartenza = DateTime.Now;
                        }
                        rapporto.IDTipologiaSistemaDistribuzione = rapportoControllo.TipologiaSistemaDistribuzione;
                        rapporto.AltroTipologiaSistemaDistribuzione = null;
                        if (rapportoControllo.Contabilizzazione != null && rapportoControllo.Contabilizzazione != -1)
                        {
                            rapporto.Contabilizzazione = rapportoControllo.Contabilizzazione;
                        }
                        else
                        {
                            rapporto.Contabilizzazione = -2;
                        }
                        rapporto.IDTipologiaContabilizzazione = rapportoControllo.TipologiaContabilizzazione;
                        if (rapportoControllo.Termoregolazione != null && rapportoControllo.Termoregolazione != -1)
                        {
                            rapporto.Termoregolazione = rapportoControllo.Termoregolazione;
                        }
                        else
                        {
                            rapporto.Termoregolazione = -2;
                        }
                        if (rapportoControllo.CorrettoFunzionamentoContabilizzazione != null
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != -1
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != 2)
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = rapportoControllo.CorrettoFunzionamentoContabilizzazione;
                        }
                        else
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = -2;
                        }
                        rapporto.JsonFormat = JsonConvert.SerializeObject(rapportoControllo);
                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            rapporto.GuidRapportoTecnico = Guid.NewGuid().ToString();
                            rapporto.DataInserimento = DateTime.Now;
                        }
                        rapporto.DataAnnullamento = null;
                        if (!string.IsNullOrEmpty(rapportoControllo.CriterAPIKey))
                        {
                            rapporto.KeyApi = rapportoControllo.CriterAPIKey;
                        }
                        #endregion

                        #region Dettaglio Dati GF --> RCT_RapportoDiControlloTecnicoGF
                        var rapportoGF = new RCT_RapportoDiControlloTecnicoGF();
                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapportoGF = ctx.RCT_RapportoDiControlloTecnicoGF.FirstOrDefault(a => a.Id == iDRapportoControlloTecnicoInt);
                        }
                        rapportoGF.RCT_RapportoDiControlloTecnicoBase = rapporto;
                        rapportoGF.TemperaturaSurriscaldamento = rapportoControllo.TemperaturaSurriscaldamento;
                        rapportoGF.TemperaturaSottoraffreddamento = rapportoControllo.TemperaturaSottoraffreddamento;
                        rapportoGF.TemperaturaCondensazione = rapportoControllo.TemperaturaCondensazione;
                        rapportoGF.TemperaturaEvaporazione = rapportoControllo.TemperaturaEvaporazione;
                        rapportoGF.TInglatoEst = rapportoControllo.TInglatoEst;
                        rapportoGF.TUscLatoEst = rapportoControllo.TUscLatoEst;
                        rapportoGF.TIngLatoUtenze = rapportoControllo.TIngLatoUtenze;
                        rapportoGF.TUscLatoUtenze = rapportoControllo.TUscLatoUtenze;
                        rapportoGF.AssenzaPerditeRefrigerante = rapportoControllo.AssenzaPerditeRefrigerante;
                        rapportoGF.LeakDetector = rapportoControllo.LeakDetector;
                        rapportoGF.ParametriTermodinamici = rapportoControllo.ParametriTermodinamici;
                        rapportoGF.ScambiatoriLiberi = rapportoControllo.ScambiatoriLiberi;
                        rapportoGF.Potenzafrigorifera = rapportoControllo.Potenzafrigorifera;
                        if (rapportoControllo.fRaffrescamento)
                        {
                            rapportoGF.ProvaRaffrescamento = true;
                            rapportoGF.ProvaRiscaldamento = false;
                        }
                        else
                        {
                            rapportoGF.ProvaRaffrescamento = false;
                            rapportoGF.ProvaRiscaldamento = true;
                        }
                        rapportoGF.IdLIM_LibrettiImpiantiMacchineFrigorifere = GetIDLibrettoImpiantoMacchineFrigorifere(rapportoControllo.CodiceProgressivo, iDLibrettoImpianto);
                        rapportoGF.IDTipologiemacchineFrigorifere = rapportoControllo.TipologiaMacchineFrigorifere;
                        rapportoGF.NCircuiti = rapportoControllo.NCircuiti;
                        if (rapportoControllo.FiltriPuliti != null)
                        {
                            rapportoGF.FiltriPuliti = rapportoControllo.FiltriPuliti;
                        }
                        else
                        {
                            rapportoGF.FiltriPuliti = 0;
                        }
                        rapportoGF.PotenzaAssorbita = rapportoControllo.PotenzaAssorbita;
                        rapportoGF.TUscitaFluido = rapportoControllo.TUscitaFluido;
                        rapportoGF.TBulboUmidoAria = rapportoControllo.TBulboUmidoAria;
                        rapportoGF.TIngressoLatoEsterno = rapportoControllo.TIngressoLatoEsterno;
                        rapportoGF.TUscitaLatoEsterno = rapportoControllo.TUscitaLatoEsterno;
                        rapportoGF.TIngressoLatoMacchina = rapportoControllo.TIngressoLatoMacchina;
                        rapportoGF.TUscitaLatoMacchina = rapportoControllo.TUscitaLatoMacchina;
                        rapportoGF.NCircuitiTotali = rapportoControllo.NCircuitiTotali;

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoGF.Add(rapportoGF);
                        }
                        #endregion

                        #region CheckList --> RCT_RapportoDiControlloTecnicoBaseCheckList
                        if (rapportoControllo.CheckList != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.CheckList)
                            {
                                var checklistDb = new RCT_RapportoDiControlloTecnicoBaseCheckList();
                                checklistDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                checklistDb.IDCheckList = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Add(checklistDb);
                            }
                        }
                        #endregion

                        #region Trattamento in riscaldamento --> RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale
                        if (rapportoControllo.TipoTrattamentoRiscaldamento != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoRiscaldamento)
                            {
                                var trattamentoInvernaleDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale();
                                trattamentoInvernaleDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoInvernaleDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Add(trattamentoInvernaleDb);
                            }
                        }
                        #endregion

                        #region Trattamento in Acs --> RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs
                        if (rapportoControllo.TipoTrattamentoACS != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoACS)
                            {
                                var trattamentoAcsDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs();
                                trattamentoAcsDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoAcsDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Add(trattamentoAcsDb);
                            }
                        }
                        #endregion

                        #region Raccomandazioni/Prescrizioni
                        if (rapportoControllo.RaccomandazioniPrescrizioni != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiRaccomandazioniPrescrizioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                ctx.RCT_RaccomandazioniPrescrizioni.RemoveRange(datiRaccomandazioniPrescrizioniAttuali);
                            }

                            var listaRaccomandazioniPrescrizioni = rapportoControllo.RaccomandazioniPrescrizioni.Select(s => new RCT_RaccomandazioniPrescrizioni()
                            {
                                RCT_RapportoDiControlloTecnicoBase = rapporto,
                                IDTipologiaRaccomandazionePrescrizioneRct = s.IDCampoRct,
                                IDTipologiaRaccomandazione = (s.TipoNonConformita == "Raccomandazione") ? (int?)s.IDNonConformita : null,
                                IDTipologiaPrescrizione = (s.TipoNonConformita == "Prescrizione") ? (int?)s.IDNonConformita : null
                            });
                            ctx.RCT_RaccomandazioniPrescrizioni.AddRange(listaRaccomandazioniPrescrizioni);
                        }
                        #endregion

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        }

                        ctx.SaveChanges();
                        dbContextTransaction.Commit();

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            result = "Rapporto di controllo Tecnico GF inserito correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                        else if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            result = "Rapporto di controllo Tecnico GF aggiornato correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbContextTransaction.Rollback();
                        Exception raise = ex;
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                raise = new InvalidOperationException(message, raise);
                            }
                        }

                        result = "Rapporto di controllo Tecnico GF non inserito correttamente " + raise.Message;
                    }
                    catch (Exception e)
                    {
                        if (!string.IsNullOrEmpty(e.InnerException.InnerException.Message))
                        {
                            result = "Rapporto di controllo Tecnico GF non inserito correttamente " + e.InnerException.InnerException.Message;
                        }
                        else
                        {
                            result = "Rapporto di controllo Tecnico GF non inserito correttamente " + e.Message;
                        }
                    }
                    finally
                    {
                        ctx.Dispose();
                    }
                }
            }
            #endregion

            return result;
        }

        public static string LoadRapportoControlloTecnico_SC(POMJ_RapportoControlloTecnico_SC rapportoControllo)
        {
            string result = "";

            object[] getvaluateOperation = new object[3];
            getvaluateOperation = EvaluateOperationRapportiFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, rapportoControllo.CodiceProgressivo, "SC", rapportoControllo.DataControllo, rapportoControllo.OraArrivo, rapportoControllo.OraPartenza, rapportoControllo.GuidRapportoTecnico);

            #region Rapporto di Controllo
            using (var ctx = new CriterDataModel())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var rapporto = new RCT_RapportoDiControlloTecnicoBase();
                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt);
                        }

                        object[] getVal = new object[2];
                        getVal = GetIDSoggettiFromCodiceSoggetto(rapportoControllo.CodiceSoggetto);

                        int iDLibrettoImpianto = (int)GetIDLibrettoImpiantoFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);

                        #region Testata Rapporto --> RCT_RapportoDiControlloTecnicoBase
                        if (!string.IsNullOrEmpty(getVal[0].ToString()) && !string.IsNullOrEmpty(getVal[1].ToString()))
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = Convert.ToInt32(getVal[0]);
                        }
                        else
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = null;
                        }
                        rapporto.IDTargaturaImpianto = (int)GetIDTargaturaImpiantoFromCodiceTargatura(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, Convert.ToInt32(getVal[1]), "RCT");
                        rapporto.PotenzaTermicaNominale = rapportoControllo.PotenzaTermicaNominale;
                        if (!string.IsNullOrEmpty(rapportoControllo.Indirizzo))
                        {
                            rapporto.Indirizzo = rapportoControllo.Indirizzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Civico))
                        {
                            rapporto.Civico = rapportoControllo.Civico;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Palazzo))
                        {
                            rapporto.Palazzo = rapportoControllo.Palazzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Scala))
                        {
                            rapporto.Scala = rapportoControllo.Scala;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Interno))
                        {
                            rapporto.Interno = rapportoControllo.Interno;
                        }
                        rapporto.IDCodiceCatastale = GetIDCodiceCatastaleFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);
                        rapporto.IDTipologiaRCT = 3;
                        rapporto.IDLibrettoImpianto = iDLibrettoImpianto;
                        rapporto.IDStatoRapportoDiControllo = 1;
                        rapporto.IDTipologiaControllo = rapportoControllo.TipologiaControllo;
                        rapporto.IDTipologiaResponsabile = rapportoControllo.TipologiaResponsabile;
                        if (rapportoControllo.ResponsabileImpianto.TipoSoggetto != null)
                        {
                            rapporto.IDTipoSoggetto = rapportoControllo.ResponsabileImpianto.TipoSoggetto;
                        }
                        else
                        {
                            rapporto.IDTipoSoggetto = 1;
                        }
                        rapporto.NomeResponsabile = rapportoControllo.ResponsabileImpianto.Nome;
                        rapporto.CognomeResponsabile = rapportoControllo.ResponsabileImpianto.Cognome;
                        rapporto.CodiceFiscaleResponsabile = rapportoControllo.ResponsabileImpianto.CodiceFiscale;
                        rapporto.RagioneSocialeResponsabile = rapportoControllo.ResponsabileImpianto.NomeAzienda;
                        rapporto.PartitaIVAResponsabile = rapportoControllo.ResponsabileImpianto.PartitaIVA;
                        rapporto.IndirizzoResponsabile = rapportoControllo.ResponsabileImpianto.Indirizzo;
                        rapporto.CivicoResponsabile = rapportoControllo.ResponsabileImpianto.Civico;
                        rapporto.ComuneResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);
                        //if (rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale != 0)
                        //{
                        rapporto.IDProvinciaResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);// rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale;
                        //}
                        if (rapportoControllo.TerzoResponsabile != null)
                        {
                            rapporto.RagioneSocialeTerzoResponsabile = rapportoControllo.TerzoResponsabile.NomeAzienda;
                            rapporto.PartitaIVATerzoResponsabile = rapportoControllo.TerzoResponsabile.PartitaIVA;
                            rapporto.IndirizzoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Indirizzo;
                            rapporto.CivicoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Civico;
                            rapporto.ComuneTerzoResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune);
                            //if (rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale != 0)
                            //{
                            rapporto.IDProvinciaTerzoResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune); //rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale;
                            //}
                        }

                        var azienda = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == rapporto.IDSoggetto);
                        if (azienda != null)
                        {
                            rapporto.RagioneSocialeImpresaManutentrice = azienda.NomeAzienda;
                            rapporto.PartitaIVAImpresaManutentrice = azienda.PartitaIVA;
                            rapporto.IndirizzoImpresaManutentrice = azienda.IndirizzoSedeLegale;
                            rapporto.CivicoImpresaManutentrice = azienda.NumeroCivicoSedeLegale;
                            rapporto.ComuneImpresaManutentrice = azienda.CittaSedeLegale;
                            rapporto.IDProvinciaImpresaManutentrice = azienda.IDProvinciaSedeLegale;
                        }

                        //rapporto.RagioneSocialeImpresaManutentrice = rapportoControllo.ImpresaManutentrice.NomeAzienda;
                        //rapporto.PartitaIVAImpresaManutentrice = rapportoControllo.ImpresaManutentrice.PartitaIVA;
                        //rapporto.IndirizzoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Indirizzo;
                        //rapporto.CivicoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Civico;
                        //rapporto.ComuneImpresaManutentrice = GetComuneFromCodiceCatastale(rapportoControllo.ImpresaManutentrice.CodiceCatastaleComune);
                        //if (rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale != 0)
                        //{
                        //    rapporto.IDProvinciaImpresaManutentrice = rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale;
                        //}


                        rapporto.LocaleInstallazioneIdoneo = rapportoControllo.LocaleInstallazioneIdoneo;
                        rapporto.PotenzaTermicaNominaleTotaleMax = rapportoControllo.PotenzaTermicaNominaleTotaleMax;
                        rapporto.fDichiarazioneConformita = rapportoControllo.fDichiarazioneConformita;
                        rapporto.fLibrettoImpiantoPresente = rapportoControllo.fLibrettoImpiantoPresente;
                        rapporto.fUsoManutenzioneGeneratore = rapportoControllo.fUsoManutenzioneGeneratore;
                        rapporto.fLibrettoImpiantoCompilato = rapportoControllo.fLibrettoImpiantoCompilato;
                        rapporto.DurezzaAcqua = rapportoControllo.DurezzaAcqua;

                        if (rapportoControllo.TrattamentoACS != null)
                        {
                            rapporto.TrattamentoACS = rapportoControllo.TrattamentoACS;
                        }
                        else
                        {
                            rapporto.TrattamentoACS = 0;
                        }

                        rapporto.TrattamentoRiscaldamento = rapportoControllo.TrattamentoRiscaldamento;
                        rapporto.ApertureLibere = -1;
                        rapporto.DimensioniApertureAdeguate = -1;
                        rapporto.LineeElettricheIdonee = rapportoControllo.LineeElettricheIdonee;
                        rapporto.StatoCoibentazioniIdonee = rapportoControllo.StatoCoibentazioniIdonee;
                        if (rapportoControllo.CoibentazioniIdonee != null)
                        {
                            rapporto.CoibentazioniIdonee = rapportoControllo.CoibentazioniIdonee;
                        }
                        else
                        {
                            rapporto.CoibentazioniIdonee = -1;
                        }
                        rapporto.AssenzaPerditeCombustibile = rapportoControllo.AssenzaPerditeCombustibile;
                        if (rapportoControllo.TenutaImpiantoIdraulico != null)
                        {
                            rapporto.TenutaImpiantoIdraulico = rapportoControllo.TenutaImpiantoIdraulico;
                        }
                        else
                        {
                            rapporto.TenutaImpiantoIdraulico = -1;
                        }
                        rapporto.ScarichiIdonei = -1;
                        rapporto.Prefisso = "SC";
                        rapporto.CodiceProgressivo = rapportoControllo.CodiceProgressivo;
                        rapporto.DataInstallazione = rapportoControllo.DataInstallazione;
                        rapporto.Fabbricante = rapportoControllo.Fabbricante;
                        rapporto.Modello = rapportoControllo.Modello;
                        rapporto.Matricola = rapportoControllo.Matricola;
                        rapporto.fClimatizzazioneInvernale = rapportoControllo.fClimatizzazioneInvernale;
                        rapporto.fProduzioneACS = rapportoControllo.fProduzioneACS;
                        rapporto.fClimatizzazioneEstiva = rapportoControllo.fClimatizzazioneEstiva;
                        rapporto.IDTipologiaCombustibile = null;
                        rapporto.AltroCombustibile = null;
                        rapporto.IDTipologiaFluidoTermoVettore = rapportoControllo.TipologiaFluidoTermoVettore;
                        if (!string.IsNullOrEmpty(rapportoControllo.Osservazioni))
                        {
                            rapporto.Osservazioni = rapportoControllo.Osservazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Raccomandazioni))
                        {
                            rapporto.Raccomandazioni = rapportoControllo.Raccomandazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Prescrizioni))
                        {
                            rapporto.Prescrizioni = rapportoControllo.Prescrizioni;
                        }
                        rapporto.fImpiantoFunzionante = rapportoControllo.fImpiantoFunzionante;
                        if (rapportoControllo.DataManutenzioneConsigliata != null)
                        {
                            rapporto.DataManutenzioneConsigliata = rapportoControllo.DataManutenzioneConsigliata;
                        }
                        rapporto.DataControllo = rapportoControllo.DataControllo;
                        if (rapportoControllo.OraArrivo != null)
                        {
                            rapporto.OraArrivo = rapportoControllo.OraArrivo;
                        }
                        else
                        {
                            rapporto.OraArrivo = DateTime.Now;
                        }
                        if (rapportoControllo.OraPartenza != null)
                        {
                            rapporto.OraPartenza = rapportoControllo.OraPartenza;
                        }
                        else
                        {
                            rapporto.OraPartenza = DateTime.Now;
                        }
                        rapporto.IDTipologiaSistemaDistribuzione = rapportoControllo.TipologiaSistemaDistribuzione;
                        rapporto.AltroTipologiaSistemaDistribuzione = null;
                        if (rapportoControllo.Contabilizzazione != null && rapportoControllo.Contabilizzazione != -1)
                        {
                            rapporto.Contabilizzazione = rapportoControllo.Contabilizzazione;
                        }
                        else
                        {
                            rapporto.Contabilizzazione = -2;
                        }
                        rapporto.IDTipologiaContabilizzazione = rapportoControllo.TipologiaContabilizzazione;
                        if (rapportoControllo.Termoregolazione != null && rapportoControllo.Termoregolazione != -1)
                        {
                            rapporto.Termoregolazione = rapportoControllo.Termoregolazione;
                        }
                        else
                        {
                            rapporto.Termoregolazione = -2;
                        }
                        if (rapportoControllo.CorrettoFunzionamentoContabilizzazione != null
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != -1
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != 2)
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = rapportoControllo.CorrettoFunzionamentoContabilizzazione;
                        }
                        else
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = -2;
                        }
                        rapporto.JsonFormat = JsonConvert.SerializeObject(rapportoControllo);
                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            rapporto.GuidRapportoTecnico = Guid.NewGuid().ToString();
                            rapporto.DataInserimento = DateTime.Now;
                        }
                        rapporto.DataAnnullamento = null;

                        if (!string.IsNullOrEmpty(rapportoControllo.CriterAPIKey))
                        {
                            rapporto.KeyApi = rapportoControllo.CriterAPIKey;
                        }

                        #endregion

                        #region Dettaglio Dati SC --> RCT_RapportoDiControlloTecnicoSC
                        var rapportoSC = new RCT_RapportoDiControlloTecnicoSC();
                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapportoSC = ctx.RCT_RapportoDiControlloTecnicoSC.FirstOrDefault(a => a.Id == iDRapportoControlloTecnicoInt);
                        }
                        rapportoSC.RCT_RapportoDiControlloTecnicoBase = rapporto;
                        if (rapportoControllo.TemperaturaEsterna != null)
                        {
                            rapportoSC.TemperaturaEsterna = rapportoControllo.TemperaturaEsterna;
                        }
                        else
                        {
                            rapportoSC.TemperaturaEsterna = 0;
                        }
                        if (rapportoControllo.TemperaturaMandataPrimario != null)
                        {
                            rapportoSC.TemperaturaMandataPrimario = rapportoControllo.TemperaturaMandataPrimario;
                        }
                        else
                        {
                            rapportoSC.TemperaturaMandataPrimario = 0;
                        }
                        if (rapportoControllo.TemperaturaRitornoPrimario != null)
                        {
                            rapportoSC.TemperaturaRitornoPrimario = rapportoControllo.TemperaturaRitornoPrimario;
                        }
                        else
                        {
                            rapportoSC.TemperaturaRitornoPrimario = 0;
                        }
                        if (rapportoControllo.PotenzaTermica != null)
                        {
                            rapportoSC.PotenzaTermica = rapportoControllo.PotenzaTermica;
                        }
                        else
                        {
                            rapportoSC.PotenzaTermica = 0;
                        }
                        if (rapportoControllo.PortataFluidoPrimario != null)
                        {
                            rapportoSC.PortataFluidoPrimario = rapportoControllo.PortataFluidoPrimario;
                        }
                        else
                        {
                            rapportoSC.PortataFluidoPrimario = 0;
                        }
                        if (rapportoControllo.TemperaturaMandataSecondario != null)
                        {
                            rapportoSC.TemperaturaMandataSecondario = rapportoControllo.TemperaturaMandataSecondario;
                        }
                        else
                        {
                            rapportoSC.TemperaturaMandataSecondario = 0;
                        }
                        if (rapportoControllo.TemperaturaRitornoSecondario != null)
                        {
                            rapportoSC.TemperaturaRitornoSecondario = rapportoControllo.TemperaturaRitornoSecondario;
                        }
                        else
                        {
                            rapportoSC.TemperaturaRitornoSecondario = 0;
                        }
                        if (rapportoControllo.PotenzaCompatibileProgetto != null)
                        {
                            rapportoSC.PotenzaCompatibileProgetto = rapportoControllo.PotenzaCompatibileProgetto;
                        }
                        else
                        {
                            rapportoSC.PotenzaCompatibileProgetto = 0;
                        }
                        if (rapportoControllo.AssenzaTrafilamenti != null)
                        {
                            rapportoSC.AssenzaTrafilamenti = rapportoControllo.AssenzaTrafilamenti;
                        }
                        else
                        {
                            rapportoSC.AssenzaTrafilamenti = -1;
                        }
                        rapportoSC.IdLIM_LibrettiImpiantiScambaitoriCalore = GetIDLibrettoImpiantoScambiatori(rapportoControllo.CodiceProgressivo, iDLibrettoImpianto);

                        rapportoSC.DispositiviComandoRegolazione = null;
                        rapportoSC.IDTipologiaFluidoTermoVettoreEntrata = rapportoControllo.TipologiaAlimentazione;
                        if (rapportoControllo.TipologiaAlimentazione == 1)
                        {
                            rapportoSC.AltroTipologiaFluidoTermoVettoreEntrata = rapportoControllo.AltroTipologiaFluidoTermoVettoreEntrata;
                        }
                        else
                        {
                            rapportoSC.AltroTipologiaFluidoTermoVettoreEntrata = null;
                        }
                        rapportoSC.IDTipologiaFluidoTermoVettoreUscita = rapportoControllo.TipologiaFluidoTermoVettore;
                        if (rapportoControllo.TipologiaFluidoTermoVettore == 1)
                        {
                            rapportoSC.AltroTipologiaFluidoTermoVettoreUscita = rapportoControllo.AltroTipologiaFluidoTermoVettoreUscita;
                        }
                        else
                        {
                            rapportoSC.AltroTipologiaFluidoTermoVettoreUscita = null;
                        }

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoSC.Add(rapportoSC);
                        }
                        #endregion

                        #region CheckList --> RCT_RapportoDiControlloTecnicoBaseCheckList
                        if (rapportoControllo.CheckList != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.CheckList)
                            {
                                var checklistDb = new RCT_RapportoDiControlloTecnicoBaseCheckList();
                                checklistDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                checklistDb.IDCheckList = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Add(checklistDb);
                            }
                        }
                        #endregion

                        #region Trattamento in riscaldamento --> RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale
                        if (rapportoControllo.TipoTrattamentoRiscaldamento != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoRiscaldamento)
                            {
                                var trattamentoInvernaleDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale();
                                trattamentoInvernaleDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoInvernaleDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Add(trattamentoInvernaleDb);
                            }
                        }
                        #endregion

                        #region Trattamento in Acs --> RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs
                        if (rapportoControllo.TipoTrattamentoACS != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoACS)
                            {
                                var trattamentoAcsDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs();
                                trattamentoAcsDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoAcsDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Add(trattamentoAcsDb);
                            }
                        }
                        #endregion

                        #region Raccomandazioni/Prescrizioni
                        if (rapportoControllo.RaccomandazioniPrescrizioni != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiRaccomandazioniPrescrizioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                ctx.RCT_RaccomandazioniPrescrizioni.RemoveRange(datiRaccomandazioniPrescrizioniAttuali);
                            }

                            var listaRaccomandazioniPrescrizioni = rapportoControllo.RaccomandazioniPrescrizioni.Select(s => new RCT_RaccomandazioniPrescrizioni()
                            {
                                RCT_RapportoDiControlloTecnicoBase = rapporto,
                                IDTipologiaRaccomandazionePrescrizioneRct = s.IDCampoRct,
                                IDTipologiaRaccomandazione = (s.TipoNonConformita == "Raccomandazione") ? (int?)s.IDNonConformita : null,
                                IDTipologiaPrescrizione = (s.TipoNonConformita == "Prescrizione") ? (int?)s.IDNonConformita : null
                            });
                            ctx.RCT_RaccomandazioniPrescrizioni.AddRange(listaRaccomandazioniPrescrizioni);
                        }
                        #endregion

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        }

                        ctx.SaveChanges();
                        dbContextTransaction.Commit();

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            result = "Rapporto di controllo Tecnico SC inserito correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                        else if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            result = "Rapporto di controllo Tecnico SC aggiornato correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbContextTransaction.Rollback();
                        Exception raise = ex;
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                raise = new InvalidOperationException(message, raise);
                            }
                        }

                        result = "Rapporto di controllo Tecnico SC non inserito correttamente " + raise.Message;
                    }
                    catch (Exception e)
                    {
                        if (!string.IsNullOrEmpty(e.InnerException.InnerException.Message))
                        {
                            result = "Rapporto di controllo Tecnico SC non inserito correttamente " + e.InnerException.InnerException.Message;
                        }
                        else
                        {
                            result = "Rapporto di controllo Tecnico SC non inserito correttamente " + e.Message;
                        }
                    }
                    finally
                    {
                        ctx.Dispose();
                    }
                }
            }
            #endregion

            return result;
        }

        public static string LoadRapportoControlloTecnico_CG(POMJ_RapportoControlloTecnico_CG rapportoControllo)
        {
            string result = "Rapporto di controllo Tecnico CG inserito correttamente";

            object[] getvaluateOperation = new object[3];
            getvaluateOperation = EvaluateOperationRapportiFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, rapportoControllo.CodiceProgressivo, "CG", rapportoControllo.DataControllo, rapportoControllo.OraArrivo, rapportoControllo.OraPartenza, rapportoControllo.GuidRapportoTecnico);

            #region Rapporto di Controllo
            using (var ctx = new CriterDataModel())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var rapporto = new RCT_RapportoDiControlloTecnicoBase();
                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt);
                        }

                        object[] getVal = new object[2];
                        getVal = GetIDSoggettiFromCodiceSoggetto(rapportoControllo.CodiceSoggetto);

                        int iDLibrettoImpianto = (int)GetIDLibrettoImpiantoFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);

                        #region Testata Rapporto --> RCT_RapportoDiControlloTecnicoBase
                        if (!string.IsNullOrEmpty(getVal[0].ToString()) && !string.IsNullOrEmpty(getVal[1].ToString()))
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = Convert.ToInt32(getVal[0]);
                        }
                        else
                        {
                            rapporto.IDSoggetto = Convert.ToInt32(getVal[1]);
                            rapporto.IDSoggettoDerived = null;
                        }
                        rapporto.IDTargaturaImpianto = (int)GetIDTargaturaImpiantoFromCodiceTargatura(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura, Convert.ToInt32(getVal[1]), "RCT");
                        rapporto.PotenzaTermicaNominale = null;
                        if (!string.IsNullOrEmpty(rapportoControllo.Indirizzo))
                        {
                            rapporto.Indirizzo = rapportoControllo.Indirizzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Civico))
                        {
                            rapporto.Civico = rapportoControllo.Civico;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Palazzo))
                        {
                            rapporto.Palazzo = rapportoControllo.Palazzo;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Scala))
                        {
                            rapporto.Scala = rapportoControllo.Scala;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Interno))
                        {
                            rapporto.Interno = rapportoControllo.Interno;
                        }
                        rapporto.IDCodiceCatastale = GetIDCodiceCatastaleFromCodiceTargaturaImpianto(rapportoControllo.CodiceTargaturaImpianto.CodiceTargatura);
                        rapporto.IDTipologiaRCT = 4;
                        rapporto.IDLibrettoImpianto = iDLibrettoImpianto;
                        rapporto.IDStatoRapportoDiControllo = 1;
                        rapporto.IDTipologiaControllo = rapportoControllo.TipologiaControllo;
                        rapporto.IDTipologiaResponsabile = rapportoControllo.TipologiaResponsabile;
                        if (rapportoControllo.ResponsabileImpianto.TipoSoggetto != null)
                        {
                            rapporto.IDTipoSoggetto = rapportoControllo.ResponsabileImpianto.TipoSoggetto;
                        }
                        else
                        {
                            rapporto.IDTipoSoggetto = 1;
                        }
                        rapporto.NomeResponsabile = rapportoControllo.ResponsabileImpianto.Nome;
                        rapporto.CognomeResponsabile = rapportoControllo.ResponsabileImpianto.Cognome;
                        rapporto.CodiceFiscaleResponsabile = rapportoControllo.ResponsabileImpianto.CodiceFiscale;
                        rapporto.RagioneSocialeResponsabile = rapportoControllo.ResponsabileImpianto.NomeAzienda;
                        rapporto.PartitaIVAResponsabile = rapportoControllo.ResponsabileImpianto.PartitaIVA;
                        rapporto.IndirizzoResponsabile = rapportoControllo.ResponsabileImpianto.Indirizzo;
                        rapporto.CivicoResponsabile = rapportoControllo.ResponsabileImpianto.Civico;
                        rapporto.ComuneResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);
                        //if (rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale != 0)
                        //{
                        rapporto.IDProvinciaResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.ResponsabileImpianto.CodiceCatastaleComune);// rapportoControllo.ResponsabileImpianto.ProvinciaSedeLegale;
                        //}
                        if (rapportoControllo.TerzoResponsabile != null)
                        {
                            rapporto.RagioneSocialeTerzoResponsabile = rapportoControllo.TerzoResponsabile.NomeAzienda;
                            rapporto.PartitaIVATerzoResponsabile = rapportoControllo.TerzoResponsabile.PartitaIVA;
                            rapporto.IndirizzoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Indirizzo;
                            rapporto.CivicoTerzoResponsabile = rapportoControllo.TerzoResponsabile.Civico;
                            rapporto.ComuneTerzoResponsabile = GetComuneFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune);
                            //if (rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale != 0)
                            //{
                            rapporto.IDProvinciaTerzoResponsabile = GetIDProvinciaFromCodiceCatastale(rapportoControllo.TerzoResponsabile.CodiceCatastaleComune); //rapportoControllo.TerzoResponsabile.ProvinciaSedeLegale;
                            //}
                        }

                        var azienda = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == rapporto.IDSoggetto);
                        if (azienda != null)
                        {
                            rapporto.RagioneSocialeImpresaManutentrice = azienda.NomeAzienda;
                            rapporto.PartitaIVAImpresaManutentrice = azienda.PartitaIVA;
                            rapporto.IndirizzoImpresaManutentrice = azienda.IndirizzoSedeLegale;
                            rapporto.CivicoImpresaManutentrice = azienda.NumeroCivicoSedeLegale;
                            rapporto.ComuneImpresaManutentrice = azienda.CittaSedeLegale;
                            rapporto.IDProvinciaImpresaManutentrice = azienda.IDProvinciaSedeLegale;
                        }

                        //rapporto.RagioneSocialeImpresaManutentrice = rapportoControllo.ImpresaManutentrice.NomeAzienda;
                        //rapporto.PartitaIVAImpresaManutentrice = rapportoControllo.ImpresaManutentrice.PartitaIVA;
                        //rapporto.IndirizzoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Indirizzo;
                        //rapporto.CivicoImpresaManutentrice = rapportoControllo.ImpresaManutentrice.Civico;
                        //rapporto.ComuneImpresaManutentrice = GetComuneFromCodiceCatastale(rapportoControllo.ImpresaManutentrice.CodiceCatastaleComune);
                        //if (rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale != 0)
                        //{
                        //    rapporto.IDProvinciaImpresaManutentrice = rapportoControllo.ImpresaManutentrice.ProvinciaSedeLegale;
                        //}
                        rapporto.LocaleInstallazioneIdoneo = rapportoControllo.LocaleInstallazioneIdoneo;
                        rapporto.PotenzaTermicaNominaleTotaleMax = rapportoControllo.PotenzaTermicaNominaleTotaleMax;
                        rapporto.fDichiarazioneConformita = rapportoControllo.fDichiarazioneConformita;
                        rapporto.fLibrettoImpiantoPresente = rapportoControllo.fLibrettoImpiantoPresente;
                        rapporto.fUsoManutenzioneGeneratore = rapportoControllo.fUsoManutenzioneGeneratore;
                        rapporto.fLibrettoImpiantoCompilato = rapportoControllo.fLibrettoImpiantoCompilato;
                        rapporto.DurezzaAcqua = rapportoControllo.DurezzaAcqua;
                        if (rapportoControllo.TrattamentoACS != null)
                        {
                            rapporto.TrattamentoACS = rapportoControllo.TrattamentoACS;
                        }
                        else
                        {
                            rapporto.TrattamentoACS = 0;
                        }
                        rapporto.TrattamentoRiscaldamento = rapportoControllo.TrattamentoRiscaldamento;
                        rapporto.ApertureLibere = rapportoControllo.ApertureLibere;
                        rapporto.DimensioniApertureAdeguate = rapportoControllo.DimensioniApertureAdeguate;
                        rapporto.LineeElettricheIdonee = rapportoControllo.LineeElettricheIdonee;
                        rapporto.CoibentazioniIdonee = -1;
                        rapporto.StatoCoibentazioniIdonee = null;
                        if (rapportoControllo.AssenzaPerditeCombustibile != null)
                        {
                            rapporto.AssenzaPerditeCombustibile = rapportoControllo.AssenzaPerditeCombustibile;
                        }
                        else
                        {
                            rapporto.AssenzaPerditeCombustibile = -1;
                        }
                        rapporto.TenutaImpiantoIdraulico = rapportoControllo.TenutaImpiantoIdraulico;
                        if (rapportoControllo.ScarichiIdonei != null)
                        {
                            rapporto.ScarichiIdonei = rapportoControllo.ScarichiIdonei;
                        }
                        else
                        {
                            rapporto.ScarichiIdonei = -1;
                        }
                        rapporto.Prefisso = "CG";
                        rapporto.CodiceProgressivo = rapportoControllo.CodiceProgressivo;
                        rapporto.DataInstallazione = rapportoControllo.DataInstallazione;
                        rapporto.Fabbricante = rapportoControllo.Fabbricante;
                        rapporto.Modello = rapportoControllo.Modello;
                        rapporto.Matricola = rapportoControllo.Matricola;
                        rapporto.fClimatizzazioneInvernale = rapportoControllo.fClimatizzazioneInvernale;
                        rapporto.fProduzioneACS = rapportoControllo.fProduzioneACS;
                        rapporto.fClimatizzazioneEstiva = rapportoControllo.fClimatizzazioneEstiva;
                        rapporto.IDTipologiaCombustibile = null;
                        rapporto.AltroCombustibile = null;
                        rapporto.IDTipologiaFluidoTermoVettore = rapportoControllo.TipologiaFluidoTermoVettore;
                        if (!string.IsNullOrEmpty(rapportoControllo.Osservazioni))
                        {
                            rapporto.Osservazioni = rapportoControllo.Osservazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Raccomandazioni))
                        {
                            rapporto.Raccomandazioni = rapportoControllo.Raccomandazioni;
                        }
                        if (!string.IsNullOrEmpty(rapportoControllo.Prescrizioni))
                        {
                            rapporto.Prescrizioni = rapportoControllo.Prescrizioni;
                        }
                        rapporto.fImpiantoFunzionante = rapportoControllo.fImpiantoFunzionante;
                        if (rapportoControllo.DataManutenzioneConsigliata != null)
                        {
                            rapporto.DataManutenzioneConsigliata = rapportoControllo.DataManutenzioneConsigliata;
                        }
                        rapporto.DataControllo = rapportoControllo.DataControllo;
                        if (rapportoControllo.OraArrivo != null)
                        {
                            rapporto.OraArrivo = rapportoControllo.OraArrivo;
                        }
                        else
                        {
                            rapporto.OraArrivo = DateTime.Now;
                        }
                        if (rapportoControllo.OraPartenza != null)
                        {
                            rapporto.OraPartenza = rapportoControllo.OraPartenza;
                        }
                        else
                        {
                            rapporto.OraPartenza = DateTime.Now;
                        }
                        rapporto.IDTipologiaSistemaDistribuzione = rapportoControllo.TipologiaSistemaDistribuzione;
                        rapporto.AltroTipologiaSistemaDistribuzione = null;
                        if (rapportoControllo.Contabilizzazione != null && rapportoControllo.Contabilizzazione != -1)
                        {
                            rapporto.Contabilizzazione = rapportoControllo.Contabilizzazione;
                        }
                        else
                        {
                            rapporto.Contabilizzazione = -2;
                        }
                        rapporto.IDTipologiaContabilizzazione = rapportoControllo.TipologiaContabilizzazione;
                        if (rapportoControllo.Termoregolazione != null && rapportoControllo.Termoregolazione != -1)
                        {
                            rapporto.Termoregolazione = rapportoControllo.Termoregolazione;
                        }
                        else
                        {
                            rapporto.Termoregolazione = -2;
                        }
                        if (rapportoControllo.CorrettoFunzionamentoContabilizzazione != null
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != -1
                            && rapportoControllo.CorrettoFunzionamentoContabilizzazione != 2)
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = rapportoControllo.CorrettoFunzionamentoContabilizzazione;
                        }
                        else
                        {
                            rapporto.CorrettoFunzionamentoContabilizzazione = -2;
                        }
                        rapporto.JsonFormat = JsonConvert.SerializeObject(rapportoControllo);
                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            rapporto.GuidRapportoTecnico = Guid.NewGuid().ToString();
                            rapporto.DataInserimento = DateTime.Now;
                        }
                        rapporto.DataAnnullamento = null;

                        if (!string.IsNullOrEmpty(rapportoControllo.CriterAPIKey))
                        {
                            rapporto.KeyApi = rapportoControllo.CriterAPIKey;
                        }

                        #endregion

                        #region Dettaglio Dati CG --> RCT_RapportoDiControlloTecnicoCG

                        var rapportoCG = new RCT_RapportoDiControlloTecnicoCG();
                        if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                            rapportoCG = ctx.RCT_RapportoDiControlloTecnicoCG.FirstOrDefault(a => a.Id == iDRapportoControlloTecnicoInt);
                        }
                        rapportoCG.RCT_RapportoDiControlloTecnicoBase = rapporto;
                        rapportoCG.CapsulaInsonorizzataIdonea = rapportoControllo.CapsulaInsonorizzataIdonea;
                        rapportoCG.TenutaCircuitoOlioIdonea = rapportoControllo.TenutaCircuitoOlioIdonea;
                        rapportoCG.FunzionalitàScambiatoreSeparazione = rapportoControllo.FunzionalitàScambiatoreSeparazione;
                        rapportoCG.TemperaturaAriaComburente = rapportoControllo.TemperaturaAriaComburente;
                        rapportoCG.TemperaturaAcquaIngresso = rapportoControllo.TemperaturaAcquaIngresso;
                        rapportoCG.TemperaturaAcquauscita = rapportoControllo.TemperaturaAcquauscita;
                        rapportoCG.PotenzaAiMorsetti = rapportoControllo.PotenzaAiMorsetti;
                        rapportoCG.TemperaturaAcquaMotore = rapportoControllo.TemperaturaAcquaMotore;
                        rapportoCG.TemperaturaFumiMonte = rapportoControllo.TemperaturaFumiMonte;
                        rapportoCG.TemperaturaFumiValle = rapportoControllo.TemperaturaFumiValle;
                        rapportoCG.PotenzaElettricaMorsetti = rapportoControllo.PotenzaElettricaMorsetti;
                        rapportoCG.PotenzaAssorbitaCombustibile = rapportoControllo.PotenzaAssorbitaCombustibile;
                        rapportoCG.PotenzaMassimoRecupero = rapportoControllo.PotenzaMassimoRecupero;
                        rapportoCG.PotenzaByPass = rapportoControllo.PotenzaByPass;
                        rapportoCG.EmissioneMonossido = rapportoControllo.EmissioneMonossido;
                        rapportoCG.IdLIM_LibrettiImpiantiCogeneratori = GetIDLibrettoImpiantoCogeneratori(rapportoControllo.CodiceProgressivo, iDLibrettoImpianto);
                        if (rapportoControllo.TipologiaFluidoTermoVettore != null)
                        {
                            rapportoCG.IDTipologiaFluidoTermoVettoreUscita = rapportoControllo.TipologiaFluidoTermoVettore;
                        }
                        if (rapportoControllo.TipologiaFluidoTermoVettore == 1)
                        {
                            rapportoCG.AltroTipologiaFluidoTermoVettoreUscita = rapportoControllo.AltroTipologiaFluidoTermoVettoreUscita;
                        }
                        else
                        {
                            rapportoCG.AltroTipologiaFluidoTermoVettoreUscita = null;
                        }
                        rapportoCG.SovrafrequenzaSogliaInterv1 = rapportoControllo.SovrafrequenzaSogliaInterv1;
                        rapportoCG.SovrafrequenzaSogliaInterv2 = rapportoControllo.SovrafrequenzaSogliaInterv2;
                        rapportoCG.SovrafrequenzaSogliaInterv3 = rapportoControllo.SovrafrequenzaSogliaInterv3;
                        rapportoCG.SovrafrequenzaTempoInterv1 = rapportoControllo.SovrafrequenzaTempoInterv1;
                        rapportoCG.SovrafrequenzaTempoInterv2 = rapportoControllo.SovrafrequenzaTempoInterv2;
                        rapportoCG.SovrafrequenzaTempoInterv3 = rapportoControllo.SovrafrequenzaTempoInterv3;
                        rapportoCG.SottofrequenzaSogliaInterv1 = rapportoControllo.SottofrequenzaSogliaInterv1;
                        rapportoCG.SottofrequenzaSogliaInterv2 = rapportoControllo.SottofrequenzaSogliaInterv2;
                        rapportoCG.SottofrequenzaSogliaInterv3 = rapportoControllo.SottofrequenzaSogliaInterv3;
                        rapportoCG.SottofrequenzaTempoInterv1 = rapportoControllo.SottofrequenzaTempoInterv1;
                        rapportoCG.SottofrequenzaTempoInterv2 = rapportoControllo.SottofrequenzaTempoInterv2;
                        rapportoCG.SottofrequenzaTempoInterv3 = rapportoControllo.SottofrequenzaTempoInterv3;
                        rapportoCG.SovratensioneSogliaInterv1 = rapportoControllo.SovratensioneSogliaInterv1;
                        rapportoCG.SovratensioneSogliaInterv2 = rapportoControllo.SovratensioneSogliaInterv2;
                        rapportoCG.SovratensioneSogliaInterv3 = rapportoControllo.SovratensioneSogliaInterv3;
                        rapportoCG.SovratensioneTempoInterv1 = rapportoControllo.SovratensioneTempoInterv1;
                        rapportoCG.SovratensioneTempoInterv2 = rapportoControllo.SovratensioneTempoInterv2;

                        rapportoCG.SovratensioneTempoInterv3 = rapportoControllo.SovratensioneTempoInterv3;
                        rapportoCG.SottotensioneSogliaInterv1 = rapportoControllo.SottotensioneSogliaInterv1;
                        rapportoCG.SottotensioneSogliaInterv2 = rapportoControllo.SottotensioneSogliaInterv2;
                        rapportoCG.SottotensioneSogliaInterv3 = rapportoControllo.SottotensioneSogliaInterv3;
                        rapportoCG.SottotensioneTempoInterv1 = rapportoControllo.SottotensioneTempoInterv1;

                        rapportoCG.SottotensioneTempoInterv2 = rapportoControllo.SottotensioneTempoInterv2;
                        rapportoCG.SottotensioneTempoInterv3 = rapportoControllo.SottotensioneTempoInterv3;
                        rapportoCG.IDTipologiaCogeneratore = null;

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoCG.Add(rapportoCG);
                        }
                        #endregion

                        #region CheckList --> RCT_RapportoDiControlloTecnicoBaseCheckList
                        if (rapportoControllo.CheckList != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.CheckList)
                            {
                                var checklistDb = new RCT_RapportoDiControlloTecnicoBaseCheckList();
                                checklistDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                checklistDb.IDCheckList = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Add(checklistDb);
                            }
                        }
                        #endregion

                        #region Trattamento in riscaldamento --> RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale
                        if (rapportoControllo.TipoTrattamentoRiscaldamento != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoRiscaldamento)
                            {
                                var trattamentoInvernaleDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale();
                                trattamentoInvernaleDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoInvernaleDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Add(trattamentoInvernaleDb);
                            }
                        }
                        #endregion

                        #region Trattamento in Acs --> RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs
                        if (rapportoControllo.TipoTrattamentoACS != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiAttuali = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                foreach (var dati in datiAttuali)
                                {
                                    ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Remove(dati);
                                }
                            }

                            foreach (var item in rapportoControllo.TipoTrattamentoACS)
                            {
                                var trattamentoAcsDb = new RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs();
                                trattamentoAcsDb.RCT_RapportoDiControlloTecnicoBase = rapporto;
                                trattamentoAcsDb.IDTipologiaTrattamentoAcqua = item;

                                ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Add(trattamentoAcsDb);
                            }
                        }
                        #endregion

                        #region Raccomandazioni/Prescrizioni
                        if (rapportoControllo.RaccomandazioniPrescrizioni != null)
                        {
                            if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                            {
                                int iDRapportoControlloTecnicoInt = int.Parse(getvaluateOperation[0].ToString());
                                var datiRaccomandazioniPrescrizioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnicoInt).ToList();
                                ctx.RCT_RaccomandazioniPrescrizioni.RemoveRange(datiRaccomandazioniPrescrizioniAttuali);
                            }

                            var listaRaccomandazioniPrescrizioni = rapportoControllo.RaccomandazioniPrescrizioni.Select(s => new RCT_RaccomandazioniPrescrizioni()
                            {
                                RCT_RapportoDiControlloTecnicoBase = rapporto,
                                IDTipologiaRaccomandazionePrescrizioneRct = s.IDCampoRct,
                                IDTipologiaRaccomandazione = (s.TipoNonConformita == "Raccomandazione") ? (int?)s.IDNonConformita : null,
                                IDTipologiaPrescrizione = (s.TipoNonConformita == "Prescrizione") ? (int?)s.IDNonConformita : null
                            });
                            ctx.RCT_RaccomandazioniPrescrizioni.AddRange(listaRaccomandazioniPrescrizioni);
                        }
                        #endregion

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        }

                        ctx.SaveChanges();
                        dbContextTransaction.Commit();

                        if (int.Parse(getvaluateOperation[2].ToString()) == 1) //Inserimento
                        {
                            result = "Rapporto di controllo Tecnico CG inserito correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                        else if (int.Parse(getvaluateOperation[2].ToString()) == 2) //Aggiornamento
                        {
                            result = "Rapporto di controllo Tecnico CG aggiornato correttamente con ID: " + rapporto.GuidRapportoTecnico;
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbContextTransaction.Rollback();
                        Exception raise = ex;
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                raise = new InvalidOperationException(message, raise);
                            }
                        }

                        result = "Rapporto di controllo Tecnico CG non inserito correttamente " + raise.Message;
                    }
                    catch (Exception e)
                    {
                        if (!string.IsNullOrEmpty(e.InnerException.InnerException.Message))
                        {
                            result = "Rapporto di controllo Tecnico CG non inserito correttamente " + e.InnerException.InnerException.Message;
                        }
                        else
                        {
                            result = "Rapporto di controllo Tecnico CG non inserito correttamente " + e.Message;
                        }
                    }
                    finally
                    {
                        ctx.Dispose();
                    }
                }
            }
            #endregion

            return result;
        }

        public static string GetUrlLibrettoImpianto(string iDLibrettoImpianto)
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["UploadLibrettiImpianti"];
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

            string reportName = ConfigurationManager.AppSettings["ReportNameLibrettiImpianti"];
            string urlPdf = ReportingServices.GetLibrettoImpiantoReport(iDLibrettoImpianto, reportName, reportUrl, reportPath, destinationFile, urlSite);

            return urlPdf;
        }

        public static string GetUrlRapportoControlloTecnico(string iDRapportoControlloTecnico)
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["UploadRapportiControllo"];
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

            string reportName = ConfigurationManager.AppSettings["ReportNameRapportiControllo"];
            string urlPdf = ReportingServices.GetRapportoControlloReport(iDRapportoControlloTecnico, reportName, reportUrl, reportPath, destinationFile, urlSite);

            return urlPdf;
        }

    }
}