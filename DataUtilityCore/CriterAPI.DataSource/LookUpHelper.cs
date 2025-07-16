using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CriterAPI.DataSource
{
    public class LookUpHelper
    {
        private CriterDataModel db;

        public LookUpHelper()
        {
            db = new CriterDataModel();
        }

        public string CheckList(int ID)
        {
            var res = db.SYS_RCTCheckList.Where(t => t.IDCheckList == ID).FirstOrDefault();
            if (res != null)
                return res.TestoCheckList;
            else
                return "";
        }

        public string Classificabile_SiNo(int? ID)
        {
            var res = "";
            if (ID.HasValue)
            {

                switch (ID.Value)
                {
                    case 1:
                        res = "Si";
                        break;
                    case 0:
                        res = "No";
                        break;
                    case -1:
                        res = "Non Classificabile";
                        break;
                    default:
                        break;
                }
            }
            return res;
        }

        public string Presente_Assente(int? ID)
        {
            var res = "";
            if (ID.HasValue)
            {
                switch (ID.Value)
                {
                    case 1:
                        res = "Presente";
                        break;
                    case 2:
                        res = "Assente";
                        break;
                    case 3:
                        res = "Non Richiesto";
                        break;
                    default:
                        break;
                }
            }
            return res;
        }

        public string TipologiaControllo(int ID)
        {
            var res = db.SYS_TipologiaControllo.Where(t => t.IDTipologiaControllo == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaControllo;
            else
                return "";
        }

        public string StatoRapportoDiControllo(int ID)
        {
            var res = db.SYS_StatoRapportoDiControllo.Where(t => t.IDStatoRapportoDiControllo == ID).FirstOrDefault();
            if (res != null)
                return res.StatoRapportoDiControllo;
            else
                return "";
        }
        public string TipologiaResponsabile(int ID)
        {
            var res = db.SYS_TipologiaResponsabile.Where(t => t.IDTipologiaResponsabile == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaResponsabile;
            else
                return "";
        }

        public string TipoSoggetto(int? ID)
        {
            if (ID != null)
            {
                var res = db.SYS_TipoSoggetto.Where(t => t.IDTipoSoggetto == ID).FirstOrDefault();
                if (res != null)
                    return res.TipoSoggetto;
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        public string FormaGiuridica(int ID)
        {
            var res = db.SYS_FormeGiuridiche.Where(t => t.IDFormaGiuridica == ID).FirstOrDefault();
            if (res != null)
                return res.FormaGiuridica;
            else
                return "";
        }
        public string TitoloSoggetto(int ID)
        {
            var res = db.SYS_TitoliSoggetti.Where(t => t.IDTitoloSoggetto == ID).FirstOrDefault();
            if (res != null)
                return res.TitoloSoggetto;
            else
                return "";
        }

        public string AbilitazioniSoggetto(int ID)
        {
            var res = db.SYS_AbilitazioneSoggetto.Where(t => t.IDAbilitazioneSoggetto == ID).FirstOrDefault();
            if (res != null)
                return res.AbilitazioneSoggetto;
            else
                return "";
        }

        public string Province(int ID)
        {
            var res = db.SYS_Province.Where(t => t.IDProvincia == ID).FirstOrDefault();
            if (res != null)
                return res.SiglaProvincia;
            else
                return "";
        }

        public string RuoliSoggetti(int ID)
        {
            var res = db.SYS_RuoloSoggetto.Where(t => t.IDRuoloSoggetto == ID).FirstOrDefault();
            if (res != null)
                return res.RuoloSoggetto;
            else
                return "";
        }

        public string FunzioneSoggetto(int ID)
        {
            var res = db.SYS_FunzioniSoggetti.Where(t => t.IDFunzioneSoggetto == ID).FirstOrDefault();
            if (res != null)
                return res.FunzioneSoggetto;
            else
                return "";
        }

        public string TipoTrattamentoAcqua(int ID)
        {
            var res = db.SYS_TipologiaTrattamentoAcqua.Where(t => t.IDTipologiaTrattamentoAcqua == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaTrattamentoAcqua;
            else
                return "";
        }

        public string TipoTrattamentoACS(int ID)
        {
            var res = db.SYS_TipologiaTrattamentoAcqua.Where(t => t.IDTipologiaTrattamentoAcqua == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaTrattamentoAcqua;
            else
                return "";
        }

        public string TipoTrattamentoRiscaldamento(int ID)
        {
            var res = db.SYS_TipologiaTrattamentoAcqua.Where(t => t.IDTipologiaTrattamentoAcqua == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaTrattamentoAcqua;
            else
                return "";
        }
        public string TipologiaGruppiTermici(int ID)
        {
            var res = db.SYS_TipologiaGruppiTermici.Where(t => t.IDTipologiaGruppiTermici == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaGruppiTermici;
            else
                return "";
        }

        public string TipologiaGeneratoriTermici(int ID)
        {
            var res = db.SYS_TipologiaGeneratoriTermici.Where(t => t.IdTipologiaGeneratoriTermici == ID).FirstOrDefault();
            if (res != null)
                return res.Descrizione;
            else
                return "";
        }

        public string TipologiaCombustibile(int ID)
        {
            var res = db.SYS_TipologiaCombustibile.Where(t => t.IDTipologiaCombustibile == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaCombustibile;
            else
                return "";
        }

        public string TipologiaFluidoTermoVettore(int ID)
        {
            var res = db.SYS_TipologiaFluidoTermoVettore.Where(t => t.IDTipologiaFluidoTermoVettore == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaFluidoTermoVettore;
            else
                return "";
        }

        public string TipologiaMacchineFrigorifere(int ID)
        {
            var res = db.SYS_TipologiaMacchineFrigorifere.Where(t => t.IDTipologiaMacchineFrigorifere == ID).FirstOrDefault();
            if (res != null)
                return res.TipologiaMacchineFrigorifere;
            else
                return "";
        }
    }
}