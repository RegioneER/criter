using System;
using System.Collections.Generic;
using System.Linq;
using Bender.Extensions;
using DataLayer;
using DataUtilityCore.Enum;
using System.Web;
using System.Configuration;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.BarCode;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace DataUtilityCore
{
    public static class UtilityBollini
    {
        
        public static Tuple<bool, decimal, string> GetImportoRichiesto(long iDRapportoControlloTecnico, string potenzaStr, object dataControllo, int iDTipologiaRCT)
        {
            bool fEsito = false;
            string message = string.Empty;

            decimal importoNecessario = 0;
            decimal potenza;
            if (potenzaStr.IsNullOrEmpty())
            {
                potenza = 0;
            }
            else
            {
                potenza = decimal.Parse(potenzaStr);
            }

            DateTime dataControlloCheck;
            //bool chkDate = DateTime.TryParse(dataControllo.ToString(), out dataControlloCheck);

            var dataControlloValid = UtilityApp.CheckValidDatetimeWithMinValue(dataControllo.ToString());

            if (dataControlloValid !=null)
            {
                #region Fasce Contributive calcolabili
                using (var ctx = new CriterDataModel())
                {
                    RCT_RapportoDiControlloTecnicoBase rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);
                    int IDTipologiaRct = 1;
                    if (rapporto != null)
                    {
                        IDTipologiaRct = rapporto.IDTipologiaRCT;
                    }
                    else
                    {
                        IDTipologiaRct = iDTipologiaRCT;
                    }
                    DateTime dataVariazioneCostoBollini = DateTime.Parse(ConfigurationManager.AppSettings["DataVariazioneCostoBollini"]);

                    switch (IDTipologiaRct)
                    {
                        case (int)RCT_TipoRapportoDiControlloTecnico.GT:
                            if (rapporto != null)
                            {
                                if (!ctx.SYS_TipologiaCombustibile.Find(rapporto.IDTipologiaCombustibile).Biomassa)
                                {
                                    importoNecessario = (decimal)ctx.SYS_FasceContributive.First(f => f.IDTipologiaRCT == 1
                                                                                                && potenza < f.PotenzaMassima).CostoBollini;
                                }
                            }
                            else
                            {
                                importoNecessario = (decimal)ctx.SYS_FasceContributive.First(f => f.IDTipologiaRCT == 1
                                                                                             && potenza < f.PotenzaMassima).CostoBollini;
                            }

                            if (dataControlloValid < dataVariazioneCostoBollini)
                            {
                                //Il bollino in origine costava 7 euro quindi per tutti i rapporti prima della vazione il costo non è di 1.75 ma sempre di 7 euro
                                importoNecessario = importoNecessario * 4;
                            }
                            break;
                        case (int)RCT_TipoRapportoDiControlloTecnico.SC:
                            break;
                        case (int)RCT_TipoRapportoDiControlloTecnico.CG:
                            importoNecessario = (decimal)ctx.SYS_FasceContributive.Where(f => f.IDTipologiaRCT == 4
                                                                                        && potenza < f.PotenzaMassima)
                                                                                        .OrderBy(f => f.PotenzaMassima)
                                                                                        .First().CostoBollini;

                            if (dataControlloValid < dataVariazioneCostoBollini)
                            {
                                //Il bollino in origine costava 7 euro quindi per tutti i rapporti prima della vazione il costo non è di 1.75 ma sempre di 7 euro
                                importoNecessario = importoNecessario * 4;
                            }
                            break;
                        case (int)RCT_TipoRapportoDiControlloTecnico.GF:
                            break;
                    }
                }
                #endregion

                fEsito = true;
                message = string.Format("Il costo per emettere questo rapporto di controllo è pari a {0} €.", string.Format("{0:#.00}", importoNecessario));
            }
            else
            {
                fEsito = false;
                importoNecessario = -1;
                message = string.Format("Per calcolare il costo di emissione del rapporto di controllo è necessario inserire la data del controllo!");
            }


            var tupleResult = Tuple.Create(fEsito, importoNecessario, message);
            return tupleResult;
        }



        //public static decimal GetImportoRichiestoOld(long iDRapportoControlloTecnico, string potenzaStr, object dataControllo, int iDTipologiaRCT)
        //{
        //    decimal importoNecessario = 0;
        //    decimal potenza;
        //    if (potenzaStr.IsNullOrEmpty())
        //    {
        //        potenza = 0;
        //    }
        //    else
        //    {
        //        potenza = decimal.Parse(potenzaStr);
        //    }

        //    using (var ctx = new CriterDataModel())
        //    {
        //        RCT_RapportoDiControlloTecnicoBase rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);
        //        int IDTipologiaRct = 0;
        //        if (rapporto != null)
        //        {
        //            IDTipologiaRct = rapporto.IDTipologiaRCT;
        //        }
        //        else
        //        {
        //            IDTipologiaRct = iDTipologiaRCT;
        //        }

        //        switch (IDTipologiaRct)
        //        {
        //            case (int)RCT_TipoRapportoDiControlloTecnico.GT:
        //                if (rapporto != null)
        //                {
        //                    if (!ctx.SYS_TipologiaCombustibile.Find(rapporto.IDTipologiaCombustibile).Biomassa)
        //                    {
        //                        importoNecessario = (decimal)ctx.SYS_FasceContributive.First(f => f.IDTipologiaRCT == 1
        //                                                                                    && potenza < f.PotenzaMassima).CostoBollini;
        //                    }
        //                }
        //                else
        //                { 
        //                    importoNecessario = (decimal)ctx.SYS_FasceContributive.First(f => f.IDTipologiaRCT == 1
        //                                                                                    && potenza < f.PotenzaMassima).CostoBollini;
        //                }
        //                break;
        //            case (int)RCT_TipoRapportoDiControlloTecnico.SC:
        //                break;
        //            case (int)RCT_TipoRapportoDiControlloTecnico.CG:
        //                importoNecessario = (decimal)ctx.SYS_FasceContributive.Where(f => f.IDTipologiaRCT == 4
        //                                                                            && potenza < f.PotenzaMassima)
        //                                                                            .OrderBy(f => f.PotenzaMassima)
        //                                                                            .First().CostoBollini;
        //                break;
        //            case (int)RCT_TipoRapportoDiControlloTecnico.GF:
        //                break;
        //        }
        //    }

        //    return importoNecessario;
        //}

        //public static decimal GetImportoRichiesto(int iDTipologiaRCT, string potenzaStr)
        //{
        //    decimal importoNecessario = 0;
        //    decimal potenza;
        //    if (potenzaStr.IsNullOrEmpty())
        //    {
        //        potenza = 0;
        //    }
        //    else
        //    {
        //        potenza = decimal.Parse(potenzaStr);
        //    }

        //    using (var ctx = new CriterDataModel())
        //    {
        //        switch (iDTipologiaRCT)
        //        {
        //            case (int)RCT_TipoRapportoDiControlloTecnico.GT:
        //                importoNecessario = (decimal)ctx.SYS_FasceContributive.First(f => f.IDTipologiaRCT == iDTipologiaRCT
        //                                                                            && potenza < f.PotenzaMassima).CostoBollini;
        //                break;
        //            case (int)RCT_TipoRapportoDiControlloTecnico.SC:
        //                break;
        //            case (int)RCT_TipoRapportoDiControlloTecnico.CG:
        //                importoNecessario = (decimal)ctx.SYS_FasceContributive.Where(f => f.IDTipologiaRCT == iDTipologiaRCT
        //                                                                            && potenza < f.PotenzaMassima)
        //                                                                            .OrderBy(f => f.PotenzaMassima)
        //                                                                            .First().CostoBollini;
        //                break;
        //            case (int)RCT_TipoRapportoDiControlloTecnico.GF:
        //                break;
        //        }
        //    }

        //    return importoNecessario;
        //}

        public static void AssegnaBolliniInteroImpiantoSuRct(int iDSoggetto, 
                                                             int iDSoggettoDerived, 
                                                             List<long> valuesRapporti, 
                                                             List<long> valuesBolliniSelezionati /*,
                                                             int numeroBolliniDaUtilizzare*/)
        {
            using (var ctx = new CriterDataModel())
            {
                int numeroRapporti = valuesRapporti.Count();
                //var bolliniDaUtilizzare = UtilityBollini.GetBolliniUtilizzabili(iDSoggetto, iDSoggettoDerived, true).Take(numeroBolliniDaUtilizzare);
                List<RCT_BollinoCalorePulito> bolliniDaUtilizzare = ctx.RCT_BollinoCalorePulito.Where(c => valuesBolliniSelezionati.Contains(c.IDBollinoCalorePulito)).ToList();
                
                //int j = 0;
                //var splits = from item in bolliniDaUtilizzare
                //             group item by j++ % 3 into part
                //             select part.AsEnumerable();

                //foreach (var item in splits.ToList())
                //{
                //    foreach (var ss in item)
                //    {
                //        var aa = ss.IDBollinoCalorePulito;
                //    }
                //}

                var groupsBollini = bolliniDaUtilizzare.Select((i, index) => new
                {
                    i,
                    index
                }).GroupBy(group => group.index % numeroRapporti, element => element.i);

                List<List<long>> listeBollini = new List<List<long>>();
                foreach (var group in groupsBollini)
                {
                    List<long> subList = new List<long>();
                    foreach (var bollino in group)
                    {
                        subList.Add(bollino.IDBollinoCalorePulito);
                    }
                    listeBollini.Add(subList);
                }

                int j = 0;
                foreach (var iDRapportoControlloTecnico in valuesRapporti)
                {
                    try
                    {
                        foreach (var idBollino in listeBollini[j])
                        {
                            RCT_BollinoCalorePulito bollinoCorrente = ctx.RCT_BollinoCalorePulito.Where(b => b.IDBollinoCalorePulito == idBollino).FirstOrDefault();
                            bollinoCorrente.IDRapportoControlloTecnico = iDRapportoControlloTecnico;
                            bollinoCorrente.DataOraUtilizzo = DateTime.Now;
                            bollinoCorrente.IDSoggettoUtilizzatore = iDSoggetto;
                            ctx.SaveChanges();
                        }
                        j++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        //public static string GetBolliniInteroImpiantoSuRct(int iDSoggetto, int iDSoggettoDerived, List<long> valuesGeneratori, int numeroBolliniDaUtilizzare)
        //{
        //    string tabella = string.Empty;
        //    using (var ctx = new CriterDataModel())
        //    {
        //        int numeroRapporti = valuesGeneratori.Count();
        //        var bolliniDaUtilizzare = UtilityBollini.GetBolliniUtilizzabili(iDSoggetto, iDSoggettoDerived, true).Take(numeroBolliniDaUtilizzare);

        //        var groupsBollini = bolliniDaUtilizzare.Select((i, index) => new
        //        {
        //            i,
        //            index
        //        }).GroupBy(group => group.index % numeroRapporti, element => element.i);

        //        List<List<long>> listeBollini = new List<List<long>>();
        //        foreach (var group in groupsBollini)
        //        {
        //            List<long> subList = new List<long>();
        //            foreach (var bollino in group)
        //            {
        //                subList.Add(bollino.IDBollinoCalorePulito);
        //            }
        //            listeBollini.Add(subList);
        //        }

        //        int j = 0;
        //        tabella += "<table>";
        //        tabella += "<tr>";
        //        tabella += "<td width='150'>";
        //        tabella += "<b>Generatori</b>";
        //        tabella += "</td>";
        //        tabella += "<td width='450'>";
        //        tabella += "<b>Bollini</b>";
        //        tabella += "</td>";
        //        tabella += "</tr>";
                
        //        foreach (var iDLibrettoImpiantoGruppoTermico in valuesGeneratori)
        //        {
        //            LIM_LibrettiImpiantiGruppiTermici generatore = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(b => b.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();

        //            tabella += "<tr>";
        //            tabella += "<td>";
        //            tabella += "Generatore " + generatore.Prefisso + " " + generatore.CodiceProgressivo;
        //            tabella += "</td>";
                    
        //            tabella += "<td>";
        //            try
        //            {
        //                foreach (var idBollino in listeBollini[j])
        //                {
        //                    RCT_BollinoCalorePulito bollinoCorrente = ctx.RCT_BollinoCalorePulito.Where(b => b.IDBollinoCalorePulito == idBollino).FirstOrDefault();
        //                    tabella += bollinoCorrente.CodiceBollino + "<br/>";
        //                }
        //                j++;
        //            }
        //            catch (Exception ex)
        //            {
                        
        //            }
                                        
        //            tabella += "</td>";
        //            tabella += "</tr>";
        //        }
                
        //        tabella += "</table>";
        //    }

        //    return tabella;
        //}

        public static bool VerificaBollini(CriterDataModel ctx, RCT_RapportoDiControlloTecnicoBase rapporto, decimal importoBollini)
        {
            var result = GetImportoRichiesto(rapporto.IDRapportoControlloTecnico, rapporto.PotenzaTermicaNominale.ToString(), rapporto.DataControllo, rapporto.IDTipologiaRCT);
            return importoBollini == result.Item2;
        }

        public static bool VerificaBollini(CriterDataModel ctx, int iDRapportoControlloTecnico, decimal importoBollini)
        {
            RCT_RapportoDiControlloTecnicoBase rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);

            if (rapporto != null)
            {
                if (rapporto.IDTipologiaControllo == 2) //Funzionale non ha bollini
                    return true;
                else
                    return VerificaBollini(ctx, rapporto, importoBollini);
            }

            else
                return false;
        }

        public static void SaveInsertDeleteDatiAssegnazioneBollini(
                int iDSoggettoDerived,
                long[] valoriSelected)
        {
            using (var ctx = new CriterDataModel())
            {
                var bolliniAttuali = ctx.RCT_BollinoCalorePulito.Where(i => i.IDSoggettoDerived == iDSoggettoDerived
                                                                  && i.IDRapportoControlloTecnico == null
                                                                  && i.fBollinoUtilizzato == false && i.fAttivo == true).ToList();

                var bollino = new RCT_BollinoCalorePulito();

                foreach (var bollini in bolliniAttuali)
                {
                    if (!valoriSelected.Contains(bollini.IDBollinoCalorePulito))
                    {
                        bollino = ctx.RCT_BollinoCalorePulito.FirstOrDefault(a => a.IDBollinoCalorePulito == bollini.IDBollinoCalorePulito);
                        bollino.IDSoggettoDerived = null;
                    }
                }
                for (int i = 0; i < valoriSelected.Length; i++)
                {
                    long iDBollinoCalorePulito = valoriSelected[i];
                    bollino = ctx.RCT_BollinoCalorePulito.FirstOrDefault(a => a.IDBollinoCalorePulito == iDBollinoCalorePulito);
                    bollino.IDSoggettoDerived = iDSoggettoDerived;
                }

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }

        
        public static List<RCT_BollinoCalorePulito> GetBolliniUtilizzabili(int? iDSoggetto, int? iDSoggettoDerived, bool fAssegnatiSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                if (iDSoggetto != null)
                {
                    bool fBolliniAssegnati = false;
                    if (fAssegnatiSoggetto)
                    {
                        #region Verifico se sono stati assegnati dei bollini all'operatore
                        
                        var bolliniAssegnati = ctx.RCT_BollinoCalorePulito.Where(s => (s.fBollinoUtilizzato == false && s.fAttivo == true)
                                                                          && (s.IDRapportoControlloTecnico == null)
                                                                          && (s.RCT_LottiBolliniCalorePulito.IDSoggetto == iDSoggetto)
                                                                          && (s.IDSoggettoDerived == iDSoggettoDerived)
                                    ).ToList();

                        if (bolliniAssegnati.Count > 0)
                        {
                            fBolliniAssegnati = true;
                        }
                        #endregion
                    }
                    
                    var query = ctx.RCT_BollinoCalorePulito.Where(c => c.fBollinoUtilizzato == false && c.fAttivo == true);
                    query = query.Where(c => c.IDRapportoControlloTecnico ==  null);
                    query = query.Where(c => c.RCT_LottiBolliniCalorePulito.IDSoggetto == iDSoggetto);
                    
                    if (fAssegnatiSoggetto)
                    {
                        if (fBolliniAssegnati)
                        {
                            //Bollini assegnati ad operatori quindi li faccio vedere
                            query = query.Where(c => c.IDSoggettoDerived == iDSoggettoDerived);
                        }
                        else
                        {
                            //Bollini NON assegnati ad operatori quindi li faccio vedere TUTTI
                            query = query.Where(c => c.IDSoggettoDerived == null);
                        }
                    }
                    else
                    {
                        //Bollini sia assegnati ad operatori che non
                        query = query.Where(c => c.IDSoggettoDerived == iDSoggettoDerived || c.IDSoggettoDerived == null);
                    }

                    return query.OrderByDescending(c => c.CostoBollino).ThenBy(c => c.CodiceBollino).ToList();
                }
            }
            
            return new List<RCT_BollinoCalorePulito>();
        }

        public static decimal GetImportoBollini(List<RCT_BollinoCalorePulito> bollini)
        {
            decimal ret = 0;

            foreach (var bollino in bollini)
            {
                ret += (decimal)bollino.CostoBollino;
            }

            return ret;
        }

        public static List<RCT_BollinoCalorePulito> GetBolliniUtilizzati(int? iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                if (iDSoggetto != null)
                {
                    var query = ctx.RCT_BollinoCalorePulito.Where(c => c.fBollinoUtilizzato == true);
                    query = query.Where(c => c.IDRapportoControlloTecnico != null);
                    query = query.Where(c => c.RCT_LottiBolliniCalorePulito.IDSoggetto == iDSoggetto);
                    
                    return query.OrderBy(c => c.CodiceBollino.ToString()).ToList();
                }
            }

            return new List<RCT_BollinoCalorePulito>();
        }

        public static List<RCT_BollinoCalorePulito> GetRapportoControlloBolliniAssociati(long iDRapportoControlloTecnico)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.RCT_BollinoCalorePulito.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico).OrderBy(c => c.CodiceBollino.ToString()).ToList();

                return result;
            }
        }

        //public static decimal GetBolliniFromSaldo(int iDSoggetto)
        //{
        //    decimal ImportoBollino = decimal.Parse(ConfigurationManager.AppSettings["CostoBollino"], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat);
        //    decimal NumeroBollini = Math.Floor(Portafoglio.Portafoglio.GetSaldoTotale(iDSoggetto) / ImportoBollino);

        //    return NumeroBollini;
        //}
                
        public static List<int?> AcquisisciBollini(int iDSoggetto, Guid iDMovimento)
        {
            List<int?> lottiList = new List<int?>();

            using (var ctx = new CriterDataModel())
            {
                var movimento = ctx.COM_RigaPortafoglio.Where(c => c.IDMovimento == iDMovimento).FirstOrDefault();
                
                int? NumeroBollini = movimento.Quantita;
                if (NumeroBollini != null)
                {
                    using (var dbtransaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            RCT_LottiBolliniCalorePulito lotto = new RCT_LottiBolliniCalorePulito();
                            lotto.QuantitaBollini = int.Parse(NumeroBollini.ToString());
                            lotto.IDSoggetto = iDSoggetto;
                            lotto.DataAcquisto = DateTime.UtcNow;
                            ctx.RCT_LottiBolliniCalorePulito.Add(lotto);
                            for (int i = 0; i < NumeroBollini; i++)
                            {
                                RCT_BollinoCalorePulito bollino = new RCT_BollinoCalorePulito();
                                bollino.RCT_LottiBolliniCalorePulito = lotto;
                                bollino.CodiceBollino = Guid.NewGuid();
                                bollino.IDSoggettoDerived = null;
                                bollino.IDSoggettoUtilizzatore = null;
                                bollino.fBollinoUtilizzato = false;
                                bollino.fAttivo = true;
                                bollino.CostoBollino = decimal.Parse(ConfigurationManager.AppSettings["CostoBollino"], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat);
                                GetBarCodeUrl(bollino.CodiceBollino.ToString(), bollino.CodiceBollino.ToString());
                                ctx.RCT_BollinoCalorePulito.Add(bollino);
                            }
                            //COM_RigaPortafoglio movimento = new COM_RigaPortafoglio();
                            //movimento.IDMovimento = Guid.NewGuid();
                            movimento.DataRegistrazione = DateTime.Now;
                            movimento.IdLottoBollinicalorePulito = lotto.IdLottobolliniCalorePulito;
                            //movimento.Utente = HttpContext.Current.User.Identity.Name.ToString();
                            movimento.IdPortafoglio = ctx.COM_Portafoglio.Where(p => p.IdSoggetto == iDSoggetto).Select(p => p.Idportafoglio).FirstOrDefault();
                            //movimento.Importo = 0 - (NumeroBollini * ImportoBollino);
                            //ctx.COM_RigaPortafoglio.Add(movimento);
                            ctx.SaveChanges();
                            dbtransaction.Commit();

                            lottiList.Add(lotto.IdLottobolliniCalorePulito);
                        }
                        catch (Exception e)
                        {
                            dbtransaction.Rollback();
                        }
                    }
                }
            }

            return lottiList;
        }

        public static void UtilizzaBollinisuRCT(long IDRapportoControlloTecnico)
        {
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    //foreach (var bollino in ctx.RCT_BollinoCalorePulito.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).ToList())
                    //{
                    //    bollino.fBollinoUtilizzato = true;
                    //}
                    //ctx.SaveChanges();

                    ctx.RCT_BollinoCalorePulito.Where(c => c.IDRapportoControlloTecnico == IDRapportoControlloTecnico).Update(c => new RCT_BollinoCalorePulito()
                    {
                        fBollinoUtilizzato = true
                    });
                }
            }
            catch (Exception ex)
            {

            }            
        }

        public static void GetBarCodeUrl(string barcodenumber, string filename)
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
            string newfile = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["UploadBolliniCalorePulito"].ToString() + "/" + filename + ".png");
            XRBarCode barcode = new XRBarCode();
            barcode.Symbology = new QRCodeGenerator();
            barcode.Text = barcodenumber;
            barcode.Width = 500;
            barcode.Height = 500;
            barcode.ShowText = false;
            barcode.AutoModule = true;
            barcode.BackColor = System.Drawing.Color.White;
            //barcode.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5f2f2");
            //((Code128Generator)barcode.Symbology).CharacterSet = Code128Charset.CharsetB;
            ((QRCodeGenerator) barcode.Symbology).CompactionMode = QRCodeCompactionMode.Byte;
            ((QRCodeGenerator) barcode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator) barcode.Symbology).Version = QRCodeVersion.Version1;
            XtraReport report = new XtraReport();
            report.Bands.Add(new DetailBand());
            report.Bands[0].Controls.Add(barcode);
            report.CreateDocument();
            report.ExportToImage(newfile, System.Drawing.Imaging.ImageFormat.Png);
            //return "~/" + ConfigurationManager.AppSettings["UploadTargatureImpianti"].ToString() + "/" + filename + ".png";
        }

        public static string GetSqlValoriBolliniFilter(object iDSoggetto, object iDSoggettoDerived, object codiceLotto, object statoBolliniCalorePulito, object codiceBollino, object statoBolliniCalorePulitoNonAttivi, object costoBollino)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_RCT_BollinoCalorePulito ");
            strSql.Append(" WHERE 1=1");
            
            if ((iDSoggetto != "") && (iDSoggetto != "-1") && (iDSoggetto != null))
            {
                strSql.Append(" AND IDSoggetto=" + iDSoggetto);
            }

            if ((iDSoggettoDerived != "") && (iDSoggettoDerived != "-1") && (iDSoggettoDerived != null))
            {
                strSql.Append(" AND IDSoggettoDerived=" + iDSoggettoDerived);
            }

            if ((codiceLotto.ToString() != "") && (codiceLotto.ToString() != "0"))
            {
                strSql.Append(" AND IdLottobolliniCalorePulito = ");
                strSql.Append("'");
                strSql.Append(codiceLotto);
                strSql.Append("'");
            }

            if ((codiceBollino != null) && (codiceBollino.ToString() != ""))
            {
                Guid guidOutput;
                bool isValid = Guid.TryParse(codiceBollino.ToString(), out guidOutput);
                if (isValid)
                {
                    strSql.Append(" AND CodiceBollino = ");
                    strSql.Append("'");
                    strSql.Append(codiceBollino);
                    strSql.Append("'");
                }
                else
                {
                    strSql.Append(" AND CodiceBollino = ");
                    strSql.Append("'");
                    strSql.Append("00000000-0000-0000-0000-000000000000");
                    strSql.Append("'");
                }               
            }

            switch (statoBolliniCalorePulito.ToString())
            {
                case "0": //Visualizza tutti i codici bollini

                    break;
                case "1": //Visualizza i codici bollini liberi
                    strSql.Append(" AND IDRapportoControlloTecnico IS NULL ");
                    break;
                case "2": //Visualizza i codici bollini legati ad un rapporto di controllo
                    strSql.Append(" AND IDRapportoControlloTecnico IS NOT NULL ");
                    break;
            }

            switch (statoBolliniCalorePulitoNonAttivi.ToString())
            {
                case "0": //Visualizza tutti i codici bollini

                    break;
                case "1": //Visualizza i codici bollini attivi
                    strSql.Append(" AND fAttivo=1 ");
                    break;
                case "2": //Visualizza i codici bollini non attivi
                    strSql.Append(" AND fAttivo=0 ");
                    break;
            }

            if (costoBollino != null)
            {
                strSql.Append(" AND CostoBollino=");
                strSql.Append(costoBollino);
            }

            strSql.Append(" ORDER BY IDBollinoCalorePulito DESC");
            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public static async Task<Tuple<bool, string, string>> ConvertiBolliniCalorePulito(List<long> bolliniDaConvertire, int iDSoggetto)
        {
            bool fEsito = false;
            string response = string.Empty;
            string iDMovimento = string.Empty;

            Portafoglio.Portafoglio portafoglio = Portafoglio.Portafoglio.Load(iDSoggetto);
            if (portafoglio != null)
            {
                using (var ctx = new CriterDataModel())
                {
                    var soggetto = ctx.V_COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == iDSoggetto);
                    string impresa = soggetto.NomeAzienda + " " + soggetto.IndirizzoSoggetto;

                    DateTime dt = DateTime.Now;
                    int quantitaBollini = bolliniDaConvertire.Count * 4;
                    decimal importo = 0; //Nella conversione dei bollini l'importo è pari zero in quanto si tratta di bollini già pagati
                    string causale = "Conversione Bollini Calore Pulito";

                    var rp = portafoglio.Incasso(impresa, dt, importo, quantitaBollini, dt, causale);
                    portafoglio.Save();

                    if (rp != null)
                    {
                        List<int?> lotti = UtilityBollini.AcquisisciBollini(iDSoggetto, rp.IDMovimento);
                        if (lotti.Count > 0)
                        {
                            //Disattivo tutti i Bollini passati
                            ctx.RCT_BollinoCalorePulito.Where(c => bolliniDaConvertire.Contains(c.IDBollinoCalorePulito)).Update(c => new RCT_BollinoCalorePulito()
                            {
                                fAttivo = false,
                                DataDisattivazione = DateTime.Now
                            });

                            iDMovimento = rp.IDMovimento.ToString();

                            //Salvo la lista dei bollini disattivati e lo collego e lo collego alla movimentazione 
                            var bolliniDisattivati = bolliniDaConvertire.Select(bollino => new RCT_BollinoCalorePulitoConvertito()
                            { 
                                 IDBollinoCalorePulito = bollino,
                                 IDMovimento = rp.IDMovimento
                            }).ToList();

                            ctx.RCT_BollinoCalorePulitoConvertito.AddRange(bolliniDisattivati);
                            ctx.SaveChanges();

                            fEsito = true;
                            response = "Conversione dei bollini calore pulito avvenuta con successo!";
                        }
                        else
                        {
                            fEsito = false;
                            response = "Si è verificato un errore nell'acquisizione dei bollini!";
                        }
                    }
                    else
                    {
                        fEsito = false;
                        response = "Si è verificato un errore nella transazione del portafoglio utente!";
                    }                                    
                }
            }

            var tupleResult = Tuple.Create(fEsito, response, iDMovimento);
            return tupleResult;
        }

    }
}
