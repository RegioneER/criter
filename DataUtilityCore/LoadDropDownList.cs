using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using System.Web;
using System.Configuration;
using System;

namespace DataUtilityCore
{
    public class LoadDropDownList
    {
        #region Anagrafiche
        public static List<SYS_TipoSoggetto> LoadDropDownList_SYS_TipoSoggetto(int? idPresel, bool fIscrizione)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            string sqlfilter = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipoSoggetto = " + idPresel.Value;
            }
            if (fIscrizione)
            {
                sqlfilter = " AND fIscrizione=1";
            }
            else
            {
                sqlfilter = " AND fIscrizione=0";
            }

            return db.SYS_TipoSoggetto.SqlQuery("SELECT IDTipoSoggetto,TipoSoggetto, TipoSoggettoImage, fAttivo, fIscrizione FROM SYS_TipoSoggetto WHERE fAttivo=1 " + sqlfilter + sqlCond).ToList();
        }

        public static List<SYS_RuoloSoggetto> LoadDropDownList_SYS_RuoloSoggetto(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDRuoloSoggetto = " + idPresel.Value;
            }

            return db.SYS_RuoloSoggetto.SqlQuery("SELECT IDRuoloSoggetto,RuoloSoggetto, fAttivo FROM SYS_RuoloSoggetto WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaDistributoriCombustibile> LoadDropDownList_SYS_TipologiaDistributoriCombustibile(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaDistributoriCombustibile = " + idPresel.Value;
            }

            return db.SYS_TipologiaDistributoriCombustibile.SqlQuery("SELECT IDTipologiaDistributoriCombustibile,TipologiaDistributoriCombustibile, fAttivo FROM SYS_TipologiaDistributoriCombustibile WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_ClassificazioneImpianto> LoadDropDownList_SYS_ClassificazioneImpianto(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDClassificazioneImpianto = " + idPresel.Value;
            }

            return db.SYS_ClassificazioneImpianto.SqlQuery("SELECT IDClassificazioneImpianto,ClassificazioneImpianto, fAttivo FROM SYS_ClassificazioneImpianto WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_FormeGiuridiche> LoadDropDownList_SYS_FormeGiuridiche(int? idPresel, int? iDTipoSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDFormaGiuridica = " + idPresel.Value;
            }

            string sqlCond0 = "";
            if (iDTipoSoggetto != null)
            {
                sqlCond0 = " AND IDTipoSoggetto = " + iDTipoSoggetto.Value;
            }

            return db.SYS_FormeGiuridiche.SqlQuery("SELECT IDFormaGiuridica,FormaGiuridica,IDTipoSoggetto,fAttivo FROM SYS_FormeGiuridiche WHERE fAttivo=1" + sqlCond + sqlCond0).ToList();
        }

        public static List<SYS_TitoliSoggetti> LoadDropDownList_SYS_TitoliSoggetti(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTitoloSoggetto = " + idPresel.Value;
            }

            return db.SYS_TitoliSoggetti.SqlQuery("SELECT IDTitoloSoggetto,TitoloSoggetto, fAttivo FROM SYS_TitoliSoggetti WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_FunzioniSoggetti> LoadDropDownList_SYS_FunzioniSoggetti(int? idPresel, int? iDTipoSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDFunzioneSoggetto = " + idPresel.Value;
            }

            string sqlCond0 = "";
            if (iDTipoSoggetto != null)
            {
                sqlCond0 = " AND IDTipoSoggetto = " + iDTipoSoggetto.Value;
            }

            return db.SYS_FunzioniSoggetti.SqlQuery("SELECT IDFunzioneSoggetto,FunzioneSoggetto,IDTipoSoggetto,fAttivo FROM SYS_FunzioniSoggetti WHERE fAttivo=1" + sqlCond + sqlCond0).ToList();
        }

        public static List<SYS_AbilitazioneSoggetto> LoadDropDownList_SYS_AbilitazioneSoggetto(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDAbilitazioneSoggetto = " + idPresel.Value;
            }

            return db.SYS_AbilitazioneSoggetto.SqlQuery("SELECT IDAbilitazioneSoggetto,AbilitazioneSoggetto, fAttivo FROM SYS_AbilitazioneSoggetto WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_Paesi> LoadDropDownList_SYS_Paesi(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDPaese = " + idPresel.Value;
            }

            return db.SYS_Paesi.SqlQuery("SELECT IDPaese,Paese, CodiceIso, fAttivo FROM SYS_Paesi WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_Regioni> LoadDropDownList_SYS_Regioni(int? idPresel, int? iDProvincia)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDRegione = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (iDProvincia != null)
            {
                sqlCond1 = " AND IDProvincia = " + iDProvincia;
            }

            return db.SYS_Regioni.SqlQuery("SELECT IDRegione,Regione,IDProvincia fAttivo FROM V_SYS_ProvinceRegioni WHERE fAttivo=1" + sqlCond + sqlCond1).ToList();
        }

        public static List<V_SYS_ProvinceRegioni> LoadDropDownList_SYS_Province(int? idPresel, bool fProvinciaCompetenza)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDProvincia = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (fProvinciaCompetenza)
            {
                sqlCond1 = " AND fProvinciaCompetenza= 1";
            }

            return db.V_SYS_ProvinceRegioni.SqlQuery("SELECT * FROM V_SYS_ProvinceRegioni WHERE fAttivo=1" + sqlCond + sqlCond1 + "ORDER BY Provincia").ToList();
        }

        public static SqlDataReader ASPxComboBox_COM_AnagraficaSoggetti(bool fTerzoResponsabile, string IDTipoSoggetto, string IDSoggetto, string IDAzienda, string filter, string startIndex, string endIndex)
        {
            string sqlCond = "";
            switch (IDTipoSoggetto)
            {
                case "1": //Persone
                    sqlCond = " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND IDSoggetto IN (" + SecurityManager.GetUserIDSoggettiCollegateDelAzienda(IDAzienda) + ") AND "; //CodiceSoggetto IS NOT NULL AND
                    break;
                case "2": //Azienda
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND CodiceSoggetto IS NOT NULL AND ";
                    if (fTerzoResponsabile)
                    {
                        sqlCond += " IDSoggetto IN (select distinct IDSoggetto from COM_RuoliSoggetti where IDRuoloSoggetto=3) AND ";
                    }
                    break;
                case "2,3": //Azienda/Terzo Responsabile
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND CodiceSoggetto IS NOT NULL AND ";
                    if (fTerzoResponsabile)
                    {
                        sqlCond += " IDSoggetto IN (select distinct IDSoggetto from COM_RuoliSoggetti where IDRuoloSoggetto=3) AND ";
                    }
                    break;
                case "5": //Distributori di Combustibile
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND ";
                    break;
                case "7": //Ispettori
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND ";
                    break;
            }

            //Caso speciale 09092024: Vogliono solo nel caso di ispettore che venga visualizzato nelle dropdownlist prima il cognome e poi il nome
            string sqlText = string.Empty;
            if (IDTipoSoggetto == "7")
            {
                sqlText = @"SELECT [IDSoggetto], [Soggetto], [IndirizzoSoggetto], [CodiceSoggetto] FROM (select [IDSoggetto], [Cognome] + ' ' + [Nome] As [Soggetto], [IndirizzoSoggetto], [CodiceSoggetto], row_number() over(order by t.[CodiceSoggetto]) as [rn] from [V_COM_AnagraficaSoggetti] as t where " + sqlCond + " ([Soggetto] LIKE '" + filter + "' OR [CodiceSoggetto] LIKE '" + filter + "')) as st where st.[rn] between " + startIndex + " and " + endIndex + " ORDER BY Soggetto";
            }
            else
            {
                sqlText = @"SELECT [IDSoggetto], [Soggetto], [IndirizzoSoggetto], [CodiceSoggetto] FROM (select [IDSoggetto], [Soggetto], [IndirizzoSoggetto], [CodiceSoggetto], row_number() over(order by t.[CodiceSoggetto]) as [rn] from [V_COM_AnagraficaSoggetti] as t where " + sqlCond + " ([Soggetto] LIKE '" + filter + "' OR [CodiceSoggetto] LIKE '" + filter + "')) as st where st.[rn] between " + startIndex + " and " + endIndex + "";
            }
            
            SqlDataReader dr = UtilityApp.GetDR(UtilityApp.SanitizeInput(sqlText, SanitizeType.Select));
            return dr;
        }

        public static SqlDataReader ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(bool fTerzoResponsabile, string IDTipoSoggetto, string IDSoggetto, string IDAzienda)
        {
            string sqlCond = "";
            switch (IDTipoSoggetto)
            {
                case "1": //Persone
                    sqlCond = " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND IDSoggetto IN (" + SecurityManager.GetUserIDSoggettiCollegateDelAzienda(IDAzienda) + ") AND "; //CodiceSoggetto IS NOT NULL AND
                    break;
                case "2": //Azienda
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND CodiceSoggetto IS NOT NULL AND ";
                    if (fTerzoResponsabile)
                    {
                        sqlCond += " IDSoggetto IN (select distinct IDSoggetto from COM_RuoliSoggetti where IDRuoloSoggetto=3) AND ";
                    }
                    break;
                case "2,3": //Azienda/Terzo Resposanbile
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND CodiceSoggetto IS NOT NULL AND ";
                    if (fTerzoResponsabile)
                    {
                        sqlCond += " IDSoggetto IN (select distinct IDSoggetto from COM_RuoliSoggetti where IDRuoloSoggetto=3) AND ";
                    }
                    break;
                case "5": //Distributori di Combustibile
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND ";
                    break;
                case "7": //Ispettori
                    sqlCond += " fattivo = 1 AND fIscrizione=1 AND IDTipoSoggetto IN (" + IDTipoSoggetto + ") AND ";
                    break;
            }

            string sqlText = string.Empty;
            if (IDTipoSoggetto == "7")
            {
                sqlText = @"SELECT [IDSoggetto], [Cognome] + ' ' + [Nome] As [Soggetto], [IndirizzoSoggetto], [CodiceSoggetto] FROM V_COM_AnagraficaSoggetti WHERE " + sqlCond + " IDSoggetto = " + IDSoggetto + " ORDER BY Soggetto";
            }
            else
            {
                sqlText = @"SELECT [IDSoggetto], [Soggetto], [IndirizzoSoggetto], [CodiceSoggetto] FROM V_COM_AnagraficaSoggetti WHERE " + sqlCond + " IDSoggetto = " + IDSoggetto + " ORDER BY Soggetto";
            }
                        
            SqlDataReader dr = UtilityApp.GetDR(UtilityApp.SanitizeInput(sqlText, SanitizeType.Select));
            return dr;
        }

        public static List<SYS_TipologiaQualificaIspettore> LoadDropDownList_SYS_TipologiaQualificaIspettore(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaQualificaIspettore = " + idPresel.Value;
            }

            return db.SYS_TipologiaQualificaIspettore.SqlQuery("SELECT IDTipologiaQualificaIspettore,TipologiaQualificaIspettore, fAttivo FROM SYS_TipologiaQualificaIspettore WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaOrdineCollegio> LoadDropDownList_SYS_TipologiaOrdineCollegio(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaOrdineCollegio = " + idPresel.Value;
            }

            return db.SYS_TipologiaOrdineCollegio.SqlQuery("SELECT IDTipologiaOrdineCollegio,TipologiaOrdineCollegio, fAttivo FROM SYS_TipologiaOrdineCollegio WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaTitoloStudio> LoadDropDownList_SYS_TipologiaTitoloStudio(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaTitoloStudio = " + idPresel.Value;
            }

            return db.SYS_TipologiaTitoloStudio.SqlQuery("SELECT IDTipologiaTitoloStudio,TipologiaTitoloStudio, fAttivo FROM SYS_TipologiaTitoloStudio WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_StatoAccreditamento> LoadDropDownList_SYS_StatoAccreditamento(int? idPresel, int iDTipoSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDStatoAccreditamento = " + idPresel.Value;
            }
            string sqlCond1 = " AND IDTipoSoggetto=" + iDTipoSoggetto;

            return db.SYS_StatoAccreditamento.SqlQuery("SELECT IDStatoAccreditamento,StatoAccreditamento,ImageUrlStatoAccreditamento,IDTipoSoggetto,fAttivo FROM SYS_StatoAccreditamento WHERE (fAttivo=1" + sqlCond + ")" + sqlCond1).ToList();
        }

        public static List<SYS_StatoContratto> LoadDropDownList_SYS_StatoContratto(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDStatoContratto = " + idPresel.Value;
            }
            
            return db.SYS_StatoContratto.SqlQuery("SELECT IDStatoContratto,StatoContratto, fAttivo FROM SYS_StatoContratto WHERE fAttivo=1" + sqlCond).ToList();
        }

        #endregion

        #region Targature Impianti
        public static SqlDataReader LoadDropDownList_SYS_CodiciLottoTargaturaImpianti(object iDPresel, object iDSoggetto, object iDSoggettoDerived)
        {
            string sqlCond = "";
            if (iDPresel != "")
            {
                sqlCond = " OR CodiceLotto = '" + iDPresel + "'";
            }
            string sqlFilter = "";
            if ((iDSoggetto != "") && (iDSoggetto != "-1") && (iDSoggetto != null))
            {
                sqlFilter = " AND IDSoggetto=" + iDSoggetto;
            }

            if ((iDSoggettoDerived != "") && (iDSoggettoDerived != "-1") && (iDSoggettoDerived != null))
            {
                sqlFilter = " AND IDSoggettoDerived=" + iDSoggettoDerived;
            }

            string sqlText = "SELECT DISTINCT CodiceLotto + '-' + Cast(Anno as nvarchar) As CodiceLotto, DescrizioneLotto FROM [dbo].[V_LIM_TargatureImpianti] WHERE (1=1 " + sqlFilter + ") " + sqlCond + " ORDER BY CodiceLotto";
            SqlDataReader dr = UtilityApp.GetDR(UtilityApp.SanitizeInput(sqlText, SanitizeType.Select));
            return dr;
        }
                
        public static List<V_LIM_TargatureImpianti> LoadDropDownList_LIM_TargatureImpianti(object iDPresel, object iDSoggetto, object iDSoggettoDerived)
        {
            bool fCodiciTargaturaAssegnati = false;
            if (iDSoggettoDerived != null)
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    int? iDSoggettoDerivedfilter = int.Parse(iDSoggettoDerived.ToString());

                    var targature = ctx.V_LIM_TargatureImpianti.Where(s => (s.IDSoggettoDerived == iDSoggettoDerivedfilter)
                                                                        && (s.IDLibrettoImpianto == null)
                                                                        

                                ).OrderBy(s => s.CodiceTargatura).ToList();

                    if (targature.Count > 0)
                    {
                        fCodiciTargaturaAssegnati = true;
                    }
                }
            }
            

            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (iDPresel != null)
            {
                sqlCond = " OR IDTargaturaImpianto = '" + iDPresel + "'";
            }
            string sqlFilter = "";
            if ((iDSoggetto != "") && (iDSoggetto != "-1") && (iDSoggetto != null))
            {
                sqlFilter = " AND IDSoggetto=" + iDSoggetto;
            }
            //Se l'operatore ha dei codici assegnati allora filtro solo i suoi, altrimenti glieli faccio vedere tutti
            if (fCodiciTargaturaAssegnati)
            {
                sqlFilter += " AND IDSoggettoDerived=" + iDSoggettoDerived;
            }
            
            return db.V_LIM_TargatureImpianti.SqlQuery("SELECT * FROM [dbo].[V_LIM_TargatureImpianti] WHERE (IDLibrettoImpianto IS NULL " + sqlFilter + ") " + sqlCond + " ORDER BY CodiceTargatura").ToList();
        }

        #endregion

        #region Bollini
        public static SqlDataReader LoadDropDownList_RCT_LottiBolliniCalorePulito(object iDPresel, object iDSoggetto, bool IsfilterDataAcquisto)
        {
            string sqlCond = "";
            if (iDPresel != null)
            {
                sqlCond = " OR IdLottobolliniCalorePulito = '" + iDPresel + "'";
            }
            string sqlFilter = "";
            if ((iDSoggetto != "") && (iDSoggetto != "-1") && (iDSoggetto != null))
            {
                sqlFilter = " AND IDSoggetto=" + iDSoggetto;
            }

            if (IsfilterDataAcquisto)
            {
                string dataVariazioneCostoBollini = ConfigurationManager.AppSettings["DataVariazioneCostoBollini"];
                sqlFilter += " AND DataAcquisto <'" + dataVariazioneCostoBollini + "'";
            }

            string sqlText = "SELECT IdLottobolliniCalorePulito, 'Lotto bollini calore pulito codice n. ' + CAST(dbo.RCT_LottiBolliniCalorePulito.IDLottoBolliniCalorePulito as nvarchar(10)) + ' del ' + CONVERT(nvarchar(10), dbo.RCT_LottiBolliniCalorePulito.DataAcquisto, 103) As DescrizioneLotto FROM [dbo].[RCT_LottiBolliniCalorePulito] WHERE (1=1 " + sqlFilter + ") " + sqlCond + " ORDER BY DataAcquisto";
            SqlDataReader dr = UtilityApp.GetDR(sqlText);
            return dr;
        }
        #endregion

        #region Utenze
        public static List<SYS_UserRole> LoadDropDownList_SYS_UserRole(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDRole = " + idPresel.Value;
            }

            return db.SYS_UserRole.SqlQuery("SELECT IDRole,Role,Description,fActive FROM SYS_UserRole WHERE fActive=1" + sqlCond).ToList();
        }
        
        #endregion

        #region Libretto Impianti
        public static List<SYS_StatoLibrettoImpianto> LoadDropDownList_SYS_StatoLibrettoImpianto(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_StatoLibrettoImpianto.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDStatoLibrettoImpianto == idPresel.Value)
                                                         ).ToList();
        }
        
        public static List<V_SYS_CodiciCatastali> LoadDropDownList_V_SYS_CodiciCatastali(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.V_SYS_CodiciCatastali.Where(s => (idPresel == null || s.IDCodiceCatastale == idPresel.Value)
                                                         ).ToList();
        }
        
        public static List<SYS_TipologiaIntervento> LoadDropDownList_SYS_TipologiaIntervento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaIntervento = " + idPresel.Value;
            }

            return db.SYS_TipologiaIntervento.SqlQuery("SELECT IDTipologiaIntervento,TipologiaIntervento, fAttivo FROM SYS_TipologiaIntervento WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static SqlDataReader ASPxComboBox_SYS_CodiciCatastali(string iDCodiceCatastale, string filter, string startIndex, string endIndex, bool soloER)
        {
            string sqlText = "";
            if (!soloER)
            {
                sqlText = @"SELECT [IDCodiceCatastale], [Comune] FROM (select [IDCodiceCatastale], [Comune], row_number() over(order by t.[CodiceCatastale]) as [rn] from [V_SYS_CodiciCatastali] as t where fAttivo=1 AND (([Comune]) LIKE '" + filter + "')) as st where st.[rn] between " + startIndex + " and " + endIndex + "";
            }
            else
            {
                UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                string filterComuniEnti = string.Empty;
                if (info.IDRuolo == 16)
                {
                    filterComuniEnti = " AND [IDCodiceCatastale] IN ( SELECT [IDCodiceCatastale] FROM [COM_CodiciCatastaliCompetenza] WHERE IDSoggetto= " + info.IDSoggetto + ")";
                }

                sqlText = @"SELECT [IDCodiceCatastale], [Comune] FROM (select [IDCodiceCatastale], [Comune], row_number() over(order by t.[CodiceCatastale]) as [rn] from [V_SYS_CodiciCatastali] as t where fAttivo=1 "+ filterComuniEnti + " AND IDProvincia IN (8,20,23,37,46,40,50,51,53) AND (([Comune]) LIKE '" + filter + "')) as st where st.[rn] between " + startIndex + " and " + endIndex + "";
            }
            
            SqlDataReader dr = UtilityApp.GetDR(sqlText);
            return dr;
        }

        public static SqlDataReader ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(string iDCodiceCatastale)
        {
            string sqlText = @"SELECT [IDCodiceCatastale], [Comune] FROM V_SYS_CodiciCatastali WHERE fAttivo=1 and IDCodiceCatastale = " + iDCodiceCatastale + " ORDER BY Comune";
            SqlDataReader dr = UtilityApp.GetDR(sqlText);
            return dr;
        }


        public static List<SYS_CodiciCatastaliSezioni> LoadDropDownList_SYS_CodiciCatastaliSezioni(int? idPresel, int iDCodiceCatastale)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_CodiciCatastaliSezioni.Where(s => s.fAttivo
                                                            && (s.IDCodiceCatastale == iDCodiceCatastale)
                                                            && (idPresel == null || s.IDCodiceCatastaleSezione == idPresel.Value)
                                                         ).ToList();
        }
        
        public static List<SYS_DestinazioneUso> LoadDropDownList_SYS_DestinazioneUso(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDDestinazioneUso = " + idPresel.Value;
            }

            return db.SYS_DestinazioneUso.SqlQuery("SELECT IDDestinazioneUso,DestinazioneUso,CodiceDestinazioneUso, fAttivo FROM SYS_DestinazioneUso WHERE fAttivo=1" + sqlCond).ToList();
        }
        
        public static List<SYS_TipologiaFluidoVettore> LoadDropDownList_SYS_TipologiaFluidoVettore(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaFluidoVettore.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaFluidoVettore == idPresel.Value)
                                                         ).OrderByDescending(s => s.IDTipologiaFluidoVettore).ToList();
        }

        public static List<SYS_TipologiaGeneratori> LoadDropDownList_SYS_TipologiaGeneratori(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaGeneratori.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaGeneratori == idPresel.Value)
                                                         ).OrderByDescending(s => s.IDTipologiaGeneratori).ToList();
        }
        
        public static List<SYS_TipologiaSistemiEmissione> LoadDropDownList_SYS_TipologiaSistemiEmissione(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaSistemiEmissione.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaSistemiEmissione == idPresel.Value)
                                                         ).ToList();
        }

        public static List<SYS_TipologiaSistemaDistribuzione> LoadDropDownList_SYS_TipologiaSistemaDistribuzione(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaSistemaDistribuzione.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaSistemaDistribuzione == idPresel.Value)
                                                         ).OrderBy(c => c.IDTipologiaSistemaDistribuzione).ToList();
        }

        public static List<SYS_TipologiaTermostatoZona> LoadDropDownList_SYS_TipologiaTermostatoZona(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaTermostatoZona.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaTermostatoZona == idPresel.Value)
                                                         ).ToList();
        }

        public static List<SYS_TipologiaSistemaContabilizzazione> LoadDropDownList_SYS_TipologiaSistemaContabilizzazione(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaSistemaContabilizzazione.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaSistemaContabilizzazione == idPresel.Value)
                                                         ).ToList();
        }

        public static List<SYS_TipologiaResponsabile> LoadDropDownList_SYS_TipologiaResponsabile(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaResponsabile.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaResponsabile == idPresel.Value)
                                                         ).ToList();
        }

        public static List<SYS_TipologiaTrattamentoAcqua> LoadDropDownList_SYS_TipologiaTrattamentoAcqua(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaTrattamentoAcqua.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaTrattamentoAcqua == idPresel.Value)
                                                         ).ToList();
        }
        
        public static List<SYS_TipologiaProtezioneGelo> LoadDropDownList_SYS_TipologiaProtezioneGelo(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaProtezioneGelo = " + idPresel.Value;
            }

            return db.SYS_TipologiaProtezioneGelo.SqlQuery("SELECT IDTipologiaProtezioneGelo,TipologiaProtezioneGelo, fAttivo FROM SYS_TipologiaProtezioneGelo WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaCircuitoRaffreddamento> LoadDropDownList_SYS_TipologiaCircuitoRaffreddamento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaCircuitoRaffreddamento = " + idPresel.Value;
            }

            return db.SYS_TipologiaCircuitoRaffreddamento.SqlQuery("SELECT IDTipologiaCircuitoRaffreddamento,TipologiaCircuitoRaffreddamento, fAttivo FROM SYS_TipologiaCircuitoRaffreddamento WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaAcquaAlimento> LoadDropDownList_SYS_TipologiaAcquaAlimento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaAcquaAlimento = " + idPresel.Value;
            }

            return db.SYS_TipologiaAcquaAlimento.SqlQuery("SELECT IDTipologiaAcquaAlimento,TipologiaAcquaAlimento, fAttivo FROM SYS_TipologiaAcquaAlimento WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaFiltrazione> LoadDropDownList_SYS_TipologiaFiltrazione(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaFiltrazione = " + idPresel.Value;
            }

            return db.SYS_TipologiaFiltrazione.SqlQuery("SELECT IDTipologiaFiltrazione,TipologiaFiltrazione, fAttivo FROM SYS_TipologiaFiltrazione WHERE fAttivo=1" + sqlCond).ToList();
        }       

        public static List<SYS_TipologiaAddolcimentoAcqua> LoadDropDownList_SYS_TipologiaAddolcimentoAcqua(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaAddolcimentoAcqua = " + idPresel.Value;
            }

            return db.SYS_TipologiaAddolcimentoAcqua.SqlQuery("SELECT IDTipologiaAddolcimentoAcqua,TipologiaAddolcimentoAcqua, fAttivo FROM SYS_TipologiaAddolcimentoAcqua WHERE fAttivo=1" + sqlCond).ToList();
        }
        
        public static List<SYS_TipologiaCondizionamentoChimico> LoadDropDownList_SYS_TipologiaCondizionamentoChimico(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaCondizionamentoChimico = " + idPresel.Value;
            }

            return db.SYS_TipologiaCondizionamentoChimico.SqlQuery("SELECT IDTipologiaCondizionamentoChimico,TipologiaCondizionamentoChimico, fAttivo FROM SYS_TipologiaCondizionamentoChimico WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaCombustibile> LoadDropDownList_SYS_TipologiaCombustibile(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaCombustibile = " + idPresel.Value;
            }

            return db.SYS_TipologiaCombustibile.SqlQuery("SELECT IDTipologiaCombustibile,TipologiaCombustibile,Biomassa,LimiteFisicoCO2,Liquido,Gas, fAttivo FROM SYS_TipologiaCombustibile WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaContabilizzazione> LoadDropDownList_SYS_TipologiaContabilizzazione(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaContabilizzazione = " + idPresel.Value;
            }

            return db.SYS_TipologiaContabilizzazione.SqlQuery("SELECT IDTipologiaContabilizzazione,TipologiaContabilizzazione, fAttivo FROM SYS_TipologiaContabilizzazione WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaGeneratoriTermici> LoadDropDownList_SYS_TipologiaGeneratoriTermici(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IdTipologiaGeneratoriTermici = " + idPresel.Value;
            }

            return db.SYS_TipologiaGeneratoriTermici.SqlQuery("SELECT IdTipologiaGeneratoriTermici,Descrizione, fAttivo FROM SYS_TipologiaGeneratoriTermici WHERE fAttivo=1" + sqlCond).ToList();
        }
        
        public static List<SYS_TipologiaFluidoTermoVettore> LoadDropDownList_SYS_TipologiaFluidoTermoVettore(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaFluidoTermoVettore = " + idPresel.Value;
            }

            return db.SYS_TipologiaFluidoTermoVettore.SqlQuery("SELECT IDTipologiaFluidoTermoVettore,TipologiaFluidoTermoVettore, fAttivo FROM SYS_TipologiaFluidoTermoVettore WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_TipologiaGruppiTermici> LoadDropDownList_SYS_TipologiaGruppiTermici(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaGruppiTermici = " + idPresel.Value;
            }

            return db.SYS_TipologiaGruppiTermici.SqlQuery("SELECT IDTipologiaGruppiTermici,TipologiaGruppiTermici, fAttivo FROM SYS_TipologiaGruppiTermici WHERE fAttivo=1" + sqlCond).ToList();
        }
        #endregion

        #region Rapporti di Controllo
        public static List<SYS_StatoRapportoDiControllo> LoadDropDownList_SYS_StatoRapportoDiControllo(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_StatoRapportoDiControllo.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDStatoRapportoDiControllo == idPresel.Value)
                                                         ).ToList();
        }

        public static List<SYS_TipologiaRapportoDiControllo> LoadDropDownList_SYS_TipologiaRapportoDiControllo(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaRapportoDiControllo.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaRCT == idPresel.Value)
                                                         ).ToList();
        }

        public static List<SYS_TipologiaControllo> LoadDropDownList_SYS_TipologiaControllo(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_TipologiaControllo.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDTipologiaControllo == idPresel.Value)
                                                         ).ToList();
        }

        public static List<V_SYS_CheckList> LoadDropDownList_V_SYS_CheckList(int? idPresel, int? iDTipologiaRct)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDCheckList = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (iDTipologiaRct != null)
            {
                sqlCond1 = " AND iDTipologiaRct = " + iDTipologiaRct;
            }

            return db.V_SYS_CheckList.SqlQuery("SELECT IDCheckList,TestoCheckList,iDTipologiaRct, fAttivo FROM V_SYS_CheckList WHERE fAttivo=1" + sqlCond + sqlCond1).ToList();
        }
        #endregion

        #region  Raccomandazioni/Prescrizioni Rct

        public static List<SYS_RCTTipologiaRaccomandazione> LoadDropDownList_SYS_RCTTipologiaRaccomandazione(int? idPresel, int? iDTipologiaRaccomandazionePrescrizioneRct)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaRaccomandazione = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (iDTipologiaRaccomandazionePrescrizioneRct != null)
            {
                sqlCond1 = " AND iDTipologiaRaccomandazionePrescrizioneRct = " + iDTipologiaRaccomandazionePrescrizioneRct;
            }

            return db.SYS_RCTTipologiaRaccomandazione.SqlQuery("SELECT IDTipologiaRaccomandazione,Raccomandazione,iDTipologiaRaccomandazionePrescrizioneRct, fAttivo FROM SYS_RCTTipologiaRaccomandazione WHERE fAttivo=1" + sqlCond + sqlCond1).ToList();
        }

        public static List<SYS_RCTTipologiaPrescrizione> LoadDropDownList_SYS_RCTTipologiaPrescrizione(int? idPresel, int? iDTipologiaRaccomandazionePrescrizioneRct)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaPrescrizione = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (iDTipologiaRaccomandazionePrescrizioneRct != null)
            {
                sqlCond1 = " AND iDTipologiaRaccomandazionePrescrizioneRct = " + iDTipologiaRaccomandazionePrescrizioneRct;
            }

            return db.SYS_RCTTipologiaPrescrizione.SqlQuery("SELECT IDTipologiaPrescrizione,Prescrizione,iDTipologiaRaccomandazionePrescrizioneRct, fAttivo FROM SYS_RCTTipologiaPrescrizione WHERE fAttivo=1" + sqlCond + sqlCond1).ToList();
        }

        #endregion

        #region Reports
        public static List<COM_Reports> LoadDropDownList_Reports(int? iDReport)
        {
            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (iDReport != null)
            {
                sqlCond = " AND COM_Reports.IDReport = " + iDReport;
            }

            return db.COM_Reports.SqlQuery("SELECT COM_Reports.IDReport, REPLACE(COM_Reports.ReportName, '.rdl', '') AS ReportName, COM_Reports.ReportDescription, COM_Reports.fAttivo, COM_ReportsAccess.IDRole FROM COM_Reports INNER JOIN COM_ReportsAccess ON COM_Reports.IDReport = COM_ReportsAccess.IDReport WHERE (COM_ReportsAccess.IDRole = "+ info.IDRuolo +") AND (COM_Reports.fAttivo = 1) " + sqlCond + " ORDER BY ReportName").ToList();
        }
        #endregion

        #region Accertamenti/Interventi/Ispezioni

        public static SqlDataReader ASPxComboBox2_VER_ProgrammaAccertamento(string filter, string startIndex, string endIndex, int iDTipoAccertamento)
        {
            string sqlCond = "AND IDTipoAccertamento=" + iDTipoAccertamento;
            
            string sqlText = @"SELECT [IDProgrammaAccertamento], [Descrizione] FROM (select [IDProgrammaAccertamento], [Descrizione], row_number() over(order by t.[IDProgrammaAccertamento]) as [rn] from [VER_ProgrammaAccertamento] as t where ([Descrizione] LIKE '" + filter + "' " + sqlCond + ")) as st where st.[rn] between " + startIndex + " and " + endIndex + "";
            SqlDataReader dr = UtilityApp.GetDR(UtilityApp.SanitizeInput(sqlText, SanitizeType.Select));
            return dr;
        }

        public static SqlDataReader ASPxComboBox2_VER_ProgrammaAccertamentoRequestedByValue(string IDProgrammaAccertamento, int iDTipoAccertamento)
        {
            string sqlCond = "AND IDTipoAccertamento=" + iDTipoAccertamento;

            string sqlText = @"SELECT [IDProgrammaAccertamento], [Descrizione] FROM VER_ProgrammaAccertamento WHERE IDProgrammaAccertamento = " + IDProgrammaAccertamento + " " + sqlCond + " ORDER BY fAttivo";
            SqlDataReader dr = UtilityApp.GetDR(UtilityApp.SanitizeInput(sqlText, SanitizeType.Select));
            return dr;
        }

        public static List<SYS_StatoAccertamento> LoadDropDownList_SYS_StatoAccertamento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDStatoAccertamento = " + idPresel.Value;
            }
            string sqlFilter = string.Empty;
            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

            switch (info.IDRuolo)
            {
                case 1: //Amministratore Criter
                    sqlFilter = string.Empty;
                    break;
                case 6: //Accertatore
                    sqlFilter = " AND IDStatoAccertamento IN (2,3)";
                    break;
                case 7: //Coordinatore
                    sqlFilter = string.Empty;
                    break;
                case 8: //Ispettore
                    sqlFilter = string.Empty;
                    break;
                case 9: //Segreteria Verifiche
                    sqlFilter = string.Empty;
                    break;
                case 17: //Agente Accertatore
                    sqlFilter = string.Empty;
                    break;
            }

            return db.SYS_StatoAccertamento.SqlQuery("SELECT IDStatoAccertamento,StatoAccertamento, fAttivo FROM SYS_StatoAccertamento WHERE fAttivo=1" + sqlCond + sqlFilter + " Order by IDStatoAccertamento").ToList();
        }

        public static List<SYS_TipologiaDistributori> LoadDropDownList_SYS_TipologiaDistributori(int? idPresel, int? IDCodiceCatastale)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDDistributore = " + idPresel.Value;
            }
            string sqlCond1 = "";
            if (IDCodiceCatastale != null)
            {
                sqlCond1 = " AND IDCodiceCatastale = " + IDCodiceCatastale;
            }

            return db.SYS_TipologiaDistributori.SqlQuery("SELECT IDDistributore,IDCodiceCatastale,Distributore,Indirizzo,PartitaIva,EmailPec,fAttivo FROM SYS_TipologiaDistributori WHERE fAttivo=1" + sqlCond + sqlCond1 + " Order by IDDistributore").ToList();
        }

        public static List<SYS_ProceduraAccertamento> LoadDropDownList_SYS_ProceduraAccertamento(int? idPresel, int? iDTipoAccertamento)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDProceduraAccertamento = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (iDTipoAccertamento != null)
            {
                sqlCond1 = " IDTipoAccertamento = " + iDTipoAccertamento;
            }

            return db.SYS_ProceduraAccertamento.SqlQuery("SELECT IDProceduraAccertamento,ProceduraAccertamento,IDTipoAccertamento,fAttivo FROM SYS_ProceduraAccertamento WHERE (" + sqlCond1+") AND fAttivo=1 " + sqlCond + " Order by IDProceduraAccertamento").ToList();
        }

        public static List<SYS_TipologiaRisoluzioneAccertamento> LoadDropDownList_SYS_TipologiaRisoluzioneAccertamento(int? idPresel, bool fImmediata)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaRisoluzioneAccertamento = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (fImmediata)
            {
                sqlCond1 = " AND IDTipologiaRisoluzioneAccertamento IN (1,3)";
            }
            else
            {
                sqlCond1 = " AND IDTipologiaRisoluzioneAccertamento IN (1,2)";
            }
            return db.SYS_TipologiaRisoluzioneAccertamento.SqlQuery("SELECT IDTipologiaRisoluzioneAccertamento,TipologiaRisoluzioneAccertamento, fAttivo FROM SYS_TipologiaRisoluzioneAccertamento WHERE fAttivo=1" + sqlCond + sqlCond1 + " Order by IDTipologiaRisoluzioneAccertamento").ToList();
        }

        public static List<SYS_TipologiaEventoAccertamento> LoadDropDownList_SYS_TipologiaEventoAccertamento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaEventoAccertamento = " + idPresel.Value;
            }

            return db.SYS_TipologiaEventoAccertamento.SqlQuery("SELECT IDTipologiaEventoAccertamento,TipologiaEventoAccertamento,TestoTemplateDocumento, fAttivo FROM SYS_TipologiaEventoAccertamento WHERE fAttivo=1" + sqlCond + " Order by IDTipologiaEventoAccertamento").ToList();
        }

        public static List<SYS_TipologiaImpiantoFunzionanteAccertamento> LoadDropDownList_SYS_TipologiaImpiantoFunzionanteAccertamento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaImpiantoFunzionanteAccertamento = " + idPresel.Value;
            }

            return db.SYS_TipologiaImpiantoFunzionanteAccertamento.SqlQuery("SELECT IDTipologiaImpiantoFunzionanteAccertamento,TipologiaImpiantoFunzionanteAccertamento, fAttivo FROM SYS_TipologiaImpiantoFunzionanteAccertamento WHERE fAttivo=1" + sqlCond + " Order by IDTipologiaImpiantoFunzionanteAccertamento").ToList();
        }

        public static List<SYS_StatoAccertamentoIntervento> LoadDropDownList_SYS_StatoAccertamentoIntervento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDStatoAccertamentoIntervento = " + idPresel.Value;
            }

            return db.SYS_StatoAccertamentoIntervento.SqlQuery("SELECT IDStatoAccertamentoIntervento,StatoAccertamentoIntervento,fAttivo FROM SYS_StatoAccertamentoIntervento WHERE fAttivo=1" + sqlCond + " Order by IDStatoAccertamentoIntervento").ToList();
        }

        public static List<SYS_TipologiaInterventoAccertamento> LoadDropDownList_SYS_TipologiaInterventoAccertamento(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaInterventoAccertamento = " + idPresel.Value;
            }

            return db.SYS_TipologiaInterventoAccertamento.SqlQuery("SELECT IDTipologiaInterventoAccertamento,TipologiaInterventoAccertamento,fAttivo FROM SYS_TipologiaInterventoAccertamento WHERE fAttivo=1" + sqlCond + " Order by IDTipologiaInterventoAccertamento").ToList();
        }
        
        public static List<SYS_StatoIspezione> LoadDropDownList_SYS_StatoIspezione(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDStatoIspezione = " + idPresel.Value;
            }

            return db.SYS_StatoIspezione.SqlQuery("SELECT IDStatoIspezione,StatoIspezione,fAttivo FROM SYS_StatoIspezione WHERE fAttivo=1" + sqlCond + " Order by IDStatoIspezione").ToList();
        }

        public static List<SYS_TipologiaIspezioneRapportoCheckList> LoadDropDownList_SYS_TipologiaIspezioneRapportoCheckList(int? idPresel, int? iDTipoCheckList)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDTipologiaCheckList = " + idPresel.Value;
            }

            string sqlCond1 = "";
            if (iDTipoCheckList != null)
            {
                sqlCond1 = " AND IDTipoCheckList = " + iDTipoCheckList;
            }

            return db.SYS_TipologiaIspezioneRapportoCheckList.SqlQuery("SELECT IDTipologiaCheckList,TipologiaCheckList,IDTipoCheckList, fAttivo FROM SYS_TipologiaIspezioneRapportoCheckList WHERE fAttivo=1" + sqlCond + sqlCond1).ToList();
        }

        public static List<SYS_CausaliRaccomandate> LoadDropDownList_SYS_CausaliRaccomandate(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDCausale = " + idPresel.Value;
            }

            return db.SYS_CausaliRaccomandate.SqlQuery("SELECT IDCausale,Causale,DescrizioneCausale,EsitoRaccomandata, fAttivo FROM SYS_CausaliRaccomandate WHERE fAttivo=1" + sqlCond).ToList();
        }

        public static List<SYS_Orario> LoadDropDownList_SYS_Orario(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDOrario = " + idPresel.Value;
            }

            return db.SYS_Orario.SqlQuery("SELECT IDOrario,Orario,fAttivo FROM SYS_Orario WHERE fAttivo=1" + sqlCond + " Order by IDOrario").ToList();
        }


        public static SqlDataReader ASPxComboBox2_VER_ProgrammaIspezioni(string filter, string startIndex, string endIndex)
        {
            string sqlText = @"SELECT [IDProgrammaIspezione], [Descrizione] FROM (select [IDProgrammaIspezione], [Descrizione], row_number() over(order by t.[IDProgrammaIspezione]) as [rn] from [VER_ProgrammaIspezione] as t where ([Descrizione] LIKE '" + filter + "')) as st where st.[rn] between " + startIndex + " and " + endIndex + "";
            SqlDataReader dr = UtilityApp.GetDR(UtilityApp.SanitizeInput(sqlText, SanitizeType.Select));
            return dr;
        }

        public static SqlDataReader ASPxComboBox2_VER_ProgrammaIspezioniRequestedByValue(string IDProgrammaIspezione)
        {
            string sqlText = @"SELECT [IDProgrammaIspezione], [Descrizione] FROM VER_ProgrammaIspezione WHERE IDProgrammaIspezione = " + IDProgrammaIspezione + " ORDER BY fAttivo";
            SqlDataReader dr = UtilityApp.GetDR(UtilityApp.SanitizeInput(sqlText, SanitizeType.Select));
            return dr;
        }

        public static List<SYS_SvolgimentoIspezione> LoadDropDownList_SYS_SvolgimentoIspezione(int? idPresel, bool IsIspezioneSvolta)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDSvolgimentoIspezione = " + idPresel.Value;
            }

            string sqlCond1 = "";
            string filter = IsIspezioneSvolta ? "1" : "0";
            sqlCond1 = " AND IsIspezioneSvolta = " + filter;

            return db.SYS_SvolgimentoIspezione.SqlQuery("SELECT IDSvolgimentoIspezione,SvolgimentoIspezione,IsIspezioneSvolta, fAttivo FROM SYS_SvolgimentoIspezione WHERE (fAttivo=1" + sqlCond + ")" + sqlCond1).ToList();
        }

        #endregion

        #region Questionari
        public static List<SYS_StatoQuestionario> LoadDropDownList_SYS_StatoQuestionario(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            return db.SYS_StatoQuestionario.Where(s => s.fAttivo
                                                            && (idPresel == null || s.IDStatoQuestionario == idPresel.Value)
                                                         ).ToList();
        }
        #endregion

        #region Sanzioni
        public static List<SYS_StatoAccertamentoSanzione> LoadDropDownList_SYS_StatoAccertamentoSanzione(int? idPresel)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            string sqlCond = "";
            if (idPresel != null)
            {
                sqlCond = " OR IDStatoAccertamentoSanzione = " + idPresel.Value;
            }

            return db.SYS_StatoAccertamentoSanzione.SqlQuery("SELECT IDStatoAccertamentoSanzione,StatoAccertamentoSanzione,fAttivo FROM SYS_StatoAccertamentoSanzione WHERE fAttivo=1" + sqlCond + " Order by IDStatoAccertamentoSanzione").ToList();
        }
        #endregion

    }
}