using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bender.Extensions;
using Criter.Lookup;
using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;

public partial class WebUserControls_Ispezioni_UCIspezioneRapporto : System.Web.UI.UserControl
{
    protected string IDIspezione
    {
        get
        {
            if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on") 
            {
                #region Encrypt on
                QueryString qs = QueryString.FromCurrent();
                QueryString qsdec = Encryption.DecryptQueryString(qs);

                try
                {
                    if (qsdec.Count > 0)
                    {
                        if (qsdec[0] != null)
                        {
                            return (string)qsdec[0];
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
                #endregion
            }
            return "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DisabledRequiredFieldValidator(this);
    }

    protected void DisabledRequiredFieldValidator(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            if (control is RequiredFieldValidator)
            {
                RequiredFieldValidator validator = (RequiredFieldValidator)control;
                validator.Enabled = false;
            }

            // Ricorsione per controlli contenitore (come Panel, Table, ecc.)
            if (control.HasControls())
            {
                DisabledRequiredFieldValidator(control);
            }
        }
    }

    public void GetDatiRapportoAll(long iDIspezione)
    {      
            rbAll();
            GetDatiIspezioneRapporto(iDIspezione);
            GetDatiStrumentazioneUtilizzata(iDIspezione.ToString());
            //GetDatiProgetto(iDIspezione.ToString());       
    }

    public void GetDatiIspezioneRapporto(long iDIspezione)
    {
        using (CriterDataModel db = new CriterDataModel())
        {
            var rapportoIspezioneCurrent = db.VER_IspezioneRapporto.FirstOrDefault(i => i.IDIspezione == iDIspezione);
            var abilitazioni = db.V_VER_Ispezioni.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();

            if (rapportoIspezioneCurrent != null)
            {
                #region Dati rapporto di ispezione

                //if (rapportoIspezioneCurrent.IDTargaturaImpianto != null)
                //{
                    lblIDTargaturaImpianto.Text = rapportoIspezioneCurrent.IDTargaturaImpianto.ToString();
                    PnlViewCodiceTargatura.Visible = true;

                    var GetCodiceTargatura = db.LIM_TargatureImpianti.Where(c => c.IDTargaturaImpianto == rapportoIspezioneCurrent.IDTargaturaImpianto).FirstOrDefault();
                    lblCodiceTargatura.Text = GetCodiceTargatura.CodiceTargatura.ToString();
                //}

                lblDataPrimaInstalazioneImpianto.Text = string.Format("{0:dd/MM/yyyy}", rapportoIspezioneCurrent.DataPrimaInstallazioneImpianto);

                //DELEGATO

                rblNominatoDelega.SelectedValue = rapportoIspezioneCurrent.fDelegatoNominato ? "True" : "False";
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NomeDelegato))
                    txtNomeDelegato.Text = rapportoIspezioneCurrent.NomeDelegato;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CognomeDelegato))
                    txtCognomeDelegato.Text = rapportoIspezioneCurrent.CognomeDelegato;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CodiceFiscaleDelegato))
                    txtCFDelega.Text = rapportoIspezioneCurrent.CodiceFiscaleDelegato;
                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.fDelega.ToString()))
                //    rblDelegaIspezionePresente.SelectedValue = rapportoIspezioneCurrent.fDelega.ToString();
                rblDelegaIspezionePresente.SelectedValue = rapportoIspezioneCurrent.fDelega ? "True" : "False";

                // Operatore/Conduttore

                rblOCNominato.SelectedValue = rapportoIspezioneCurrent.fOperatoreConduttoreNominato ? "True" : "False";
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.fOperatoreConduttorePresente.ToString()))
                    rblOCNominatoPresente.SelectedValue = rapportoIspezioneCurrent.fOperatoreConduttorePresente.ToString();
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NomeOperatoreConduttore))
                    txtOCNome.Text = rapportoIspezioneCurrent.NomeOperatoreConduttore;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CognomeOperatoreConduttore))
                    txtOCCOgnome.Text = rapportoIspezioneCurrent.CognomeOperatoreConduttore;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CodiceFiscaleOperatoreConduttore))
                    txtOCCF.Text = rapportoIspezioneCurrent.CodiceFiscaleOperatoreConduttore;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.fOperatoreConduttoreAbilitato.ToString()))
                    rblOCAbilitato.SelectedValue = rapportoIspezioneCurrent.fOperatoreConduttoreAbilitato.ToString();
                if (rapportoIspezioneCurrent.IDTipoAbilitazione != null)
                    rblTipoAbilitazione.SelectedValue = rapportoIspezioneCurrent.IDTipoAbilitazione.ToString();
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NumeroPatentinoOperatoreConduttore))
                    txtOCPatentinoNumero.Text = rapportoIspezioneCurrent.NumeroPatentinoOperatoreConduttore;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.DataRilascioPatentinoOperatoreConduttore.ToString()))
                    txtDataRilascioPatentino.Text = String.Format("{0:dd/MM/yyyy}", rapportoIspezioneCurrent.DataRilascioPatentinoOperatoreConduttore);

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NumeroPOD))
                    lblPOD.Text = rapportoIspezioneCurrent.NumeroPOD;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NumeroPDR))
                    lblPDR.Text = rapportoIspezioneCurrent.NumeroPDR;

                rblImpiantoRegistratoCRITER.SelectedValue = rapportoIspezioneCurrent.fImpiantoRegistrato ? "True" : "False";
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PotenzaTermicaNominaleTotaleFocolare.ToString()))
                {
                    txtPotenzaTermicaNominaleTotaleFocalore.Text = rapportoIspezioneCurrent.PotenzaTermicaNominaleTotaleFocolare.ToString();
                }

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PotenzaTermicaNominaleTotaleUtile.ToString()))
                {
                    txtPotenzaTermicaNominaleTotaleUtile.Text = rapportoIspezioneCurrent.PotenzaTermicaNominaleTotaleUtile.ToString();
                }

                // Ubicazione 

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.IDCodiceCatastale.ToString()))
                {
                    var GetComune = db.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == rapportoIspezioneCurrent.IDCodiceCatastale).FirstOrDefault();
                    lblComuneUbicazione.Text = GetComune.CodiceCatastale + " - " + GetComune.Comune;
                }

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.IndirizzoUbicazione))
                    lblIndirizzoUbicazione.Text = rapportoIspezioneCurrent.IndirizzoUbicazione;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CivicoUbicazione))
                    lblCivicoUbicazione.Text = rapportoIspezioneCurrent.CivicoUbicazione;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PalazzoUbicazione))
                    lblPalazzoUbicazione.Text = rapportoIspezioneCurrent.PalazzoUbicazione;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.ScalaUbicazione))
                    lblScalaUbicazione.Text = rapportoIspezioneCurrent.ScalaUbicazione;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.InternoUbicazione))
                    lblInternoUbicazione.Text = rapportoIspezioneCurrent.InternoUbicazione;

                GetDatiCatastali(abilitazioni.IDLibrettoImpianto.ToString());

                // RESPONSABILE
                if (rapportoIspezioneCurrent.IDTipologiaResponsabile.HasValue)
                    rblTipologiaResponsabile.SelectedValue = rapportoIspezioneCurrent.IDTipologiaResponsabile.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NomeResponsabile))
                    txtNomeResponsabile.Text = rapportoIspezioneCurrent.NomeResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CognomeResponsabile))
                    txtCognomeResponsabile.Text = rapportoIspezioneCurrent.CognomeResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CodiceFiscaleResponsabile))
                    txtCFResponsabile.Text = rapportoIspezioneCurrent.CodiceFiscaleResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.RagioneSocialeResponsabile))
                    txtRagioneSocialeResponsabile.Text = rapportoIspezioneCurrent.RagioneSocialeResponsabile;

                cbComuneResponsabile.Value = rapportoIspezioneCurrent.IDCodiceCatastaleResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PartitaIVAResponsabile))
                    txtPIVAResponsabile.Text = rapportoIspezioneCurrent.PartitaIVAResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.IndirizzoResponsabile))
                    txtIndirizzoResponsabile.Text = rapportoIspezioneCurrent.IndirizzoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CivicoResponsabile))
                    txtCivicoResponsabile.Text = rapportoIspezioneCurrent.CivicoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TelefonoResponsabile))
                    txtTelefonoResponsabile.Text = rapportoIspezioneCurrent.TelefonoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailResponsabile))
                    txtEmailResponsabile.Text = rapportoIspezioneCurrent.EmailResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailResponsabile))
                    txtEmailResponsabile.Text = rapportoIspezioneCurrent.EmailResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailPECResponsabile))
                    txtEmailPECResponsabile.Text = rapportoIspezioneCurrent.EmailPECResponsabile;

                // TERZO RESPONSABILE 
                rblfTerzoResponsabile.SelectedValue = rapportoIspezioneCurrent.fTerzoResponsabile ? "True" : "False";

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.RagioneSocialeTerzoResponsabile))
                    txtRagioneSocialeTerzoResponsabile.Text = rapportoIspezioneCurrent.RagioneSocialeTerzoResponsabile;


                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PartitaIVATerzoResponsabile))
                    txtPartitaIVATerzoResponsabile.Text = rapportoIspezioneCurrent.PartitaIVATerzoResponsabile;

                cbComuneTerzoResponsabile.Value = rapportoIspezioneCurrent.IDCodiceCatastaleTerzoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.IndirizzoTerzoResponsabile))
                    txtIndirizzoTerzoResponsabile.Text = rapportoIspezioneCurrent.IndirizzoTerzoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CivicoTerzoResponsabile))
                    txtNumeroCivicoTerzoResponsabile.Text = rapportoIspezioneCurrent.CivicoTerzoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TelefonoTerzoResponsabile))
                    txtTelefonoTerzoResponsabile.Text = rapportoIspezioneCurrent.TelefonoTerzoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailTerzoResponsabile))
                    txtEmailTerzoResponsabile.Text = rapportoIspezioneCurrent.EmailTerzoResponsabile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailPECTerzoResponsabile))
                    txtEmailPECTerzoResponsabile.Text = rapportoIspezioneCurrent.EmailPECTerzoResponsabile;

                //if (rapportoIspezioneCurrent.fAbilitatoTerzoResponsabile != null)
                    rblAbilitazioneTerzoResponsabile.SelectedValue = rapportoIspezioneCurrent.fAbilitatoTerzoResponsabile ? "True" : "False";

                //if (rapportoIspezioneCurrent.fCertificatoTerzoResponsabile != null)
                rblCertificazioneTerzoResponsabile.SelectedValue = rapportoIspezioneCurrent.fCertificatoTerzoResponsabile.ToString();

                //if (rapportoIspezioneCurrent.fAttestatoTerzoResponsabile != null)
                    rblAttestatoTerzoResponsabile.SelectedValue = rapportoIspezioneCurrent.fAttestatoTerzoResponsabile.ToString();

                //if (rapportoIspezioneCurrent.fAttestatoIncaricoTerzoResponsabile != null)
                    rblAttestazioneIncaricoTerzoResponsabile.SelectedValue = rapportoIspezioneCurrent.fAttestatoIncaricoTerzoResponsabile.ToString();

                // IMPRESA MANUTENTRICE

                //if (rapportoIspezioneCurrent.fImpresaManutentrice != null)
                    rblImpresaManutentricePresente.SelectedValue = rapportoIspezioneCurrent.fImpresaManutentrice.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.RagioneSocialeImpresaManutentrice))
                    txtRagioneSocialeImpresaManutentrice.Text = rapportoIspezioneCurrent.RagioneSocialeImpresaManutentrice;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PartitaIVAImpresaManutentrice))
                    txtPartitaIVAImpresaManutentrice.Text = rapportoIspezioneCurrent.PartitaIVAImpresaManutentrice;

                cbComuneImpresaManutentrice.Value = rapportoIspezioneCurrent.IDCodiceCatastaleImpresaManutentrice;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.IndirizzoImpresaManutentrice))
                    txtIndirizzoImpresaManutentrice.Text = rapportoIspezioneCurrent.IndirizzoImpresaManutentrice;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CivicoImpresaManutentrice))
                    txtNumeroCivicoImpresaManutentrice.Text = rapportoIspezioneCurrent.CivicoImpresaManutentrice;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TelefonoImpresaManutentrice))
                    txtTelefonoImpresaManutentrice.Text = rapportoIspezioneCurrent.TelefonoImpresaManutentrice;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailImpresaManutentrice))
                    txtEmailImpresaManutentrice.Text = rapportoIspezioneCurrent.EmailImpresaManutentrice;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailPECImpresaManutentrice))
                    txtEmailPECImpresaManutentrice.Text = rapportoIspezioneCurrent.EmailPECImpresaManutentrice;

                //if (rapportoIspezioneCurrent.fAbilitataImpresaManutentrice != null)
                    rblAbilitazioneImpresaManutentrice.SelectedValue = rapportoIspezioneCurrent.fAbilitataImpresaManutentrice ? "True" : "False";// rapportoIspezioneCurrent.fAbilitataImpresaManutentrice.ToString();

                //if (rapportoIspezioneCurrent.fCertificataImpresaManutentrice != null)
                    rblCertificazioneImpresaManutentrice.SelectedValue = rapportoIspezioneCurrent.fCertificataImpresaManutentrice ? "True" : "False";

                //if (rapportoIspezioneCurrent.fAttestataImpresaManutentrice != null)
                rblAttestatoSOAImpresaManutentrice.SelectedValue = rapportoIspezioneCurrent.fAttestataImpresaManutentrice ? "True" : "False";

                // OPERATORE ULTIMO CONTROLLO

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NomeOperatore))
                    txtNomeOperatoreUltimoControllo.Text = rapportoIspezioneCurrent.NomeOperatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CognomeOperatore))
                    txtCognomeOperatoreUltimoControllo.Text = rapportoIspezioneCurrent.CognomeOperatore;

                cbNatoOperatoreUltimoControllo.Value = rapportoIspezioneCurrent.IDCodiceCatastaleNascitaOperatore;

                if (rapportoIspezioneCurrent.DataNascitaOperatore != null)
                    deDataNascitaOperatoreUltimoControllo.Date = DateTime.Parse(rapportoIspezioneCurrent.DataNascitaOperatore.ToString());

                cbComuneOperatoreUltimoControllo.Value = rapportoIspezioneCurrent.IDCodiceCatastaleOperatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.IndirizzoOperatore))
                    txtIndirizzoOperatoreUltimoControllo.Text = rapportoIspezioneCurrent.IndirizzoOperatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CivicoOperatore))
                    txtNumeroCivicoOperatoreUltimoControllo.Text = rapportoIspezioneCurrent.CivicoOperatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TelefonoOperatore))
                    txtTelefonoOperatoreUltimoControllo.Text = rapportoIspezioneCurrent.TelefonoOperatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.EmailOperatore))
                    txtEmailOperatoreUltimoControllo.Text = rapportoIspezioneCurrent.EmailOperatore;

                //if (rapportoIspezioneCurrent.fPatentatoOperatore != null)
                    rblPatentinoOperatoreUltimoControllo.SelectedValue = rapportoIspezioneCurrent.fPatentatoOperatore.ToString();

                // 3.
                if (rapportoIspezioneCurrent.IDDestinazioneUso != null)
                    rblDestinazioneUso.SelectedValue = rapportoIspezioneCurrent.IDDestinazioneUso.ToString();

                cbUnitaImmobiliariServite.Value = rapportoIspezioneCurrent.UnitaImmobiliariServite;
                cbServiziServitiImpianto.Value = rapportoIspezioneCurrent.ServiziServitiImpianto;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.VolumeLordoRiscaldato.ToString()))
                    txtVolumeLordoRiscaldato.Text = rapportoIspezioneCurrent.VolumeLordoRiscaldato.ToString();

                // 4.

                cbInstallazioneInternaEsterna.Value = rapportoIspezioneCurrent.IDTipoInstallazione;

                switch (rapportoIspezioneCurrent.LocaleInstallazioneIdoneo)
                {
                    case -1:
                        chkLocaleInstallazioneIdoneo.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkLocaleInstallazioneIdoneo.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkLocaleInstallazioneIdoneo.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.GeneratoriInstallazioneIdonei)
                {
                    case -1:
                        chkGeneratoriIdonei.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkGeneratoriIdonei.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkGeneratoriIdonei.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.ApertureLibere)
                {
                    case -1:
                        chkApertureLibere.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkApertureLibere.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkApertureLibere.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.DimensioniApertureAdeguate)
                {
                    case -1:
                        chkDimensioniApertureAdeguate.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkDimensioniApertureAdeguate.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkDimensioniApertureAdeguate.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.ScarichiIdonei)
                {
                    case -1:
                        chkScarichiIdonei.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkScarichiIdonei.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkScarichiIdonei.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.AssenzaPerditeCombustibile)
                {
                    case -1:
                        chkAssenzaPerditeCombustibile.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkAssenzaPerditeCombustibile.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkAssenzaPerditeCombustibile.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.TenutaImpiantoIdraulico)
                {
                    case -1:
                        chkTenutaImpianto.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkTenutaImpianto.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkTenutaImpianto.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TiraggioProvaStrumentalePA.ToString()))
                    txtTiraggioDelCamino.Text = rapportoIspezioneCurrent.TiraggioProvaStrumentalePA.ToString();

                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TiraggioProvaIndirettaCalcolata.ToString()))
                //    rblTiraggioDelCamino.SelectedValue = rapportoIspezioneCurrent.TiraggioProvaIndirettaCalcolata.ToString();

                // 6.
                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.fLibrettoImpiantoPresente.ToString()))
                    rblPresenteLibretto.SelectedValue = rapportoIspezioneCurrent.fLibrettoImpiantoPresente.ToString();

                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.fUsoManutenzioneGeneratore.ToString()))
                    rblLibrettoUsoManutenzione.SelectedValue = rapportoIspezioneCurrent.fUsoManutenzioneGeneratore.ToString();

                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.fLibrettoImpiantoCompilato.ToString()))
                    rblLibrettoCompilatoInTutteLeParte.SelectedValue = rapportoIspezioneCurrent.fLibrettoImpiantoCompilato.ToString();

                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.fDichiarazioneConformita.ToString()))
                    rblDichiarazione.SelectedValue = rapportoIspezioneCurrent.fDichiarazioneConformita.ToString();

                
                //if (rapportoIspezioneCurrent.IDCategoriaDocumentazione != null)
                //    rblCategoriaAnticendio.SelectedValue = rapportoIspezioneCurrent.IDCategoriaDocumentazione.ToString();

                //if (rapportoIspezioneCurrent.CodiceRischio != null)
                //    txtCodiceRischio.Text = rapportoIspezioneCurrent.CodiceRischio.ToString();

                //switch (rapportoIspezioneCurrent.ProgettoAntincendio)
                //{
                //    case -1:
                //        chkProggettoAntincendio.Value = EnumStatoSiNoNc.NonClassificabile;
                //        break;
                //    case 0:
                //        chkProggettoAntincendio.Value = EnumStatoSiNoNc.No;
                //        break;
                //    case 1:
                //        chkProggettoAntincendio.Value = EnumStatoSiNoNc.Si;
                //        break;
                //}
                //if (rapportoIspezioneCurrent.DataProgettoAntincendio != null)
                //    deProgettoAntincendio.Date = DateTime.Parse(rapportoIspezioneCurrent.DataProgettoAntincendio.ToString());

                switch (rapportoIspezioneCurrent.SCIAAntincendio)
                {
                    case -1:
                        chkSCIAAntincendio.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkSCIAAntincendio.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkSCIAAntincendio.Value = EnumStatoSiNoNc.Si;
                        break;
                }
                if (rapportoIspezioneCurrent.DataSCIAAntincendio != null)
                    deSCIAAntincendio.Date = DateTime.Parse(rapportoIspezioneCurrent.DataSCIAAntincendio.ToString());

                //switch (rapportoIspezioneCurrent.VerbaleSopralluogo)
                //{
                //    case -1:
                //        chkVerbaleSopralluogo.Value = EnumStatoSiNoNc.NonClassificabile;
                //        break;
                //    case 0:
                //        chkVerbaleSopralluogo.Value = EnumStatoSiNoNc.No;
                //        break;
                //    case 1:
                //        chkVerbaleSopralluogo.Value = EnumStatoSiNoNc.Si;
                //        break;
                //}
                //if (rapportoIspezioneCurrent.DataVerbaleSopralluogo != null)
                //    deVerbaleSopralluogo.Date = DateTime.Parse(rapportoIspezioneCurrent.DataVerbaleSopralluogo.ToString());

                //switch (rapportoIspezioneCurrent.UltimoRinnovoPeriodico)
                //{
                //    case -1:
                //        chkUltimoRinnovoPeriodico.Value = EnumStatoSiNoNc.NonClassificabile;
                //        break;
                //    case 0:
                //        chkUltimoRinnovoPeriodico.Value = EnumStatoSiNoNc.No;
                //        break;
                //    case 1:
                //        chkUltimoRinnovoPeriodico.Value = EnumStatoSiNoNc.Si;
                //        break;
                //}
                //if (rapportoIspezioneCurrent.DataUltimoRinnovoPeriodico != null)
                //    deUltimoRinnovoPeriodico.Date = DateTime.Parse(rapportoIspezioneCurrent.DataUltimoRinnovoPeriodico.ToString());

                rblPresenteProgettoImpianto.SelectedValue = rapportoIspezioneCurrent.fProgettoImpiantoPresente.ToString();


                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.Progettista))
                    txtProgettista.Text = rapportoIspezioneCurrent.Progettista;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.ProtocolloDepositoComune))
                    txtProtocolloDepositoComune.Text = rapportoIspezioneCurrent.ProtocolloDepositoComune;
                if (rapportoIspezioneCurrent.DataDepositoComune != null)
                    deDataDepositoComune.Date = DateTime.Parse(rapportoIspezioneCurrent.DataDepositoComune.ToString());
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PotenzaProgetto.ToString()))
                    txtPotenzaTermicaUtileProgetto.Text = rapportoIspezioneCurrent.PotenzaProgetto.ToString();

                //switch (rapportoIspezioneCurrent.AQEAttestato)
                //{
                //    case -1:
                //        chkAQE.Value = EnumStatoSiNoNc.NonClassificabile;
                //        break;
                //    case 0:
                //        chkAQE.Value = EnumStatoSiNoNc.No;
                //        break;
                //    case 1:
                //        chkAQE.Value = EnumStatoSiNoNc.Si;
                //        break;
                //}

                switch (rapportoIspezioneCurrent.DiagnosiEnergetica)
                {
                    case -1:
                        chkDiagnosiEnergetica.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkDiagnosiEnergetica.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkDiagnosiEnergetica.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.Perizia)
                {
                    case -1:
                        chkPerizia.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkPerizia.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkPerizia.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                switch (rapportoIspezioneCurrent.OmologazioneVerifiche)
                {
                    case -1:
                        chkOmologazioneVerifiche.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkOmologazioneVerifiche.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkOmologazioneVerifiche.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                if (rapportoIspezioneCurrent.CodiceProgressivo != null)
                    lblGruppoTermicoGT.Text = rapportoIspezioneCurrent.CodiceProgressivo.ToString();

                if (rapportoIspezioneCurrent.DataInstallazioneGeneratore != null)
                    deDataInstallazioneGeneratore.Date = DateTime.Parse(rapportoIspezioneCurrent.DataInstallazioneGeneratore.ToString());

                if (rapportoIspezioneCurrent.IDTipologiaFluidoTermoVettore != null)
                    cbFluidoTermoVettore.Value = rapportoIspezioneCurrent.IDTipologiaFluidoTermoVettore.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.AltroFluidoTermovettore))
                    txtAltroFluidoTermoVettore.Text = rapportoIspezioneCurrent.AltroFluidoTermovettore;

                rblEvacuazioneForzata.Checked = rapportoIspezioneCurrent.EvacuazioneForzata;
                rblEvacuazioneNaturale.Checked = rapportoIspezioneCurrent.EvacuazioneNaturale;

                if (rapportoIspezioneCurrent.IDTipologiaCombustibile != null)
                    cbCombustibile.Value = rapportoIspezioneCurrent.IDTipologiaCombustibile.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.AltroCombustibile))
                    txtAltroCombustibile.Text = rapportoIspezioneCurrent.AltroCombustibile;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CostruttoreCaldaia))
                    lblCustrutoreCaldaia.Text = rapportoIspezioneCurrent.CostruttoreCaldaia;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.ModelloGeneratore))
                    lblModelloGeneratore.Text = rapportoIspezioneCurrent.ModelloGeneratore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.MatricolaGeneratore))
                    lblMarticolaGeneratore.Text = rapportoIspezioneCurrent.MatricolaGeneratore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CostruttoreBruciatore))
                    txtCostruttoreBruciatore.Text = rapportoIspezioneCurrent.CostruttoreBruciatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.ModelloBruciatore))
                    txtModelloBruciatore.Text = rapportoIspezioneCurrent.ModelloBruciatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.MatricolaBruciatore))
                    txtMatricolaBruciatore.Text = rapportoIspezioneCurrent.MatricolaBruciatore;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PotenzaTermicaFocolareGeneratore.ToString()))
                    txtPotenzaTermicaFocolareDatiNominali.Text = rapportoIspezioneCurrent.PotenzaTermicaFocolareGeneratore.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PotenzaTermicaNominaleGeneratore.ToString()))
                    txtPotenzaTermicaUtileDatiNominali.Text = rapportoIspezioneCurrent.PotenzaTermicaNominaleGeneratore.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.LavoroBruciatoreDa.ToString()))
                    txtCampoLavoroBruciatoreDatiNominaliDa.Text = rapportoIspezioneCurrent.LavoroBruciatoreDa.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.LavoroBruciatoreA.ToString()))
                    txtCampoLavoroBruciatoreDatiNominaliA.Text = rapportoIspezioneCurrent.LavoroBruciatoreA.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PortataCombustibileValoriMisuratiM3H.ToString()))
                    txtPortataCombustibileValoriMisuratim3h.Text = rapportoIspezioneCurrent.PortataCombustibileValoriMisuratiM3H.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PortataCombustibileValoriMisuratiKG.ToString()))
                    txtPortataCombustibileValoriMisuratikgh.Text = rapportoIspezioneCurrent.PortataCombustibileValoriMisuratiKG.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PotenzaTermicaFocolareValoriMisurati.ToString()))
                    txtPotenzaTermicaFocolareValoriMisurati.Text = rapportoIspezioneCurrent.PotenzaTermicaFocolareValoriMisurati.ToString();

                if (rapportoIspezioneCurrent.IdTipologiaGruppiTermici != null)
                    cbTipologiaGruppoTermico.Value = rapportoIspezioneCurrent.IdTipologiaGruppiTermici.ToString();


                if (rapportoIspezioneCurrent.IdTipologiaGeneratoriTermici != null)
                    cbClassificazioneDPR66096.Value = rapportoIspezioneCurrent.IdTipologiaGeneratoriTermici.ToString();

                //switch (rapportoIspezioneCurrent.CorrettoDimensionamento)
                //{
                //    case -1:
                //        chkOmologazioneVerifiche.Value = EnumStatoSiNoNc.NonClassificabile;
                //        break;
                //    case 0:
                //        chkOmologazioneVerifiche.Value = EnumStatoSiNoNc.No;
                //        break;
                //    case 1:
                //        chkOmologazioneVerifiche.Value = EnumStatoSiNoNc.Si;
                //        break;
                //}

                if (rapportoIspezioneCurrent.CorrettoDimensionamento != null)
                    rblCorrettoDimensionamentoGeneratore.SelectedValue = rapportoIspezioneCurrent.CorrettoDimensionamento.ToString();

                if (rapportoIspezioneCurrent.TrattamentoRiscaldamento != null)
                    rblTrattamentoRiscaldamento.SelectedValue = rapportoIspezioneCurrent.TrattamentoRiscaldamento.ToString();
                if (rapportoIspezioneCurrent.TrattamentoACS != null)
                    rblTrattamentoProduzioneACS.SelectedValue = rapportoIspezioneCurrent.TrattamentoACS.ToString();

                GetDatiIspezioneRapportoTrattamentoAcquaInvernale(int.Parse(rapportoIspezioneCurrent.IDIspezione.ToString()));
                GetDatiIspezioneRapportoTrattamentoAcquaAcs(int.Parse(rapportoIspezioneCurrent.IDIspezione.ToString()));

                cbFrequenza.Value = rapportoIspezioneCurrent.IDFrequenzaManutenzione;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.AltroFrequenzaManutenzione))
                    txtAltroFrequenza.Text = rapportoIspezioneCurrent.AltroFrequenzaManutenzione;

                //if (rapportoIspezioneCurrent.fUltimaManutenzioneEffettuata != null)
                    rblUltimaManutenzionePrevistaEffettuata.SelectedValue = rapportoIspezioneCurrent.fUltimaManutenzioneEffettuata.ToString();

                if (rapportoIspezioneCurrent.DataUltimaManutenzione != null)
                    deDataUltimaManutenzione.Date = DateTime.Parse(rapportoIspezioneCurrent.DataUltimaManutenzione.ToString());

                cbFrequenzaEfficienzaEnergetica.Value = rapportoIspezioneCurrent.IDFrequenzaControllo;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.AltroFrequenzaControllo))
                    txtAltroFrequenzaEfficienzaEnergetica.Text = rapportoIspezioneCurrent.AltroFrequenzaControllo;

                cbUltimoControlloPrevistoEffettuato.Value = rapportoIspezioneCurrent.UltimoControlloEffettuato;

                if (rapportoIspezioneCurrent.DataUltimoControllo != null)
                    deDataUltimoControllo.Date = DateTime.Parse(rapportoIspezioneCurrent.DataUltimoControllo.ToString());

                cbRTCEEPresenteInCopia.Value = rapportoIspezioneCurrent.RaportoControlloPresente;

                //if (rapportoIspezioneCurrent.fRTCEEManutenzioneRegistaro != null)
                    rblRTCEERegistrato.SelectedValue = rapportoIspezioneCurrent.fRTCEEManutenzioneRegistaro.ToString();

                cbRTCEEinterventiPrevisti.Value = rapportoIspezioneCurrent.RealizzatiInterventiPrevisti;

                cbOsservazioniRCTEE.Checked = rapportoIspezioneCurrent.fOsservazioniRCTEE;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.OsservazioniRCTEE))
                    txtOsservazioniRCTEE.Text = rapportoIspezioneCurrent.OsservazioniRCTEE;

                cbRaccomandazioniRCTEE.Checked = rapportoIspezioneCurrent.fRaccomandazioniRCTEE;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.RaccomandazioniRCTEE))
                    txtRaccomandazioniRCTEE.Text = rapportoIspezioneCurrent.RaccomandazioniRCTEE;

                cbPrescrizioniRCTEE.Checked = rapportoIspezioneCurrent.fPrescrizioniRCTEE;
                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PrescrizioniRCTEE))
                    txtPrescrizioniRCTEE.Text = rapportoIspezioneCurrent.PrescrizioniRCTEE;


                //if (rapportoIspezioneCurrent.fCOFumiSecchiNoAria1000 != null)
                    rblMonossidoCarbonio.SelectedValue = rapportoIspezioneCurrent.fCOFumiSecchiNoAria1000.ToString();
                //if (rapportoIspezioneCurrent.fIndiceFumositaNdiBacharch != null)
                    rblIndiceFumosita.SelectedValue = rapportoIspezioneCurrent.fIndiceFumositaNdiBacharch.ToString();
                //if (rapportoIspezioneCurrent.fRendimentoSupMinimo != null)
                    rblRendimentoCombustibileMinimo.SelectedValue = rapportoIspezioneCurrent.fRendimentoSupMinimo.ToString();


                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.ModuloTermico))
                    txtModuloTermico.Text = rapportoIspezioneCurrent.ModuloTermico;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TemperaturaFumi.ToString()))
                    txtTemperaturaFumi.Text = rapportoIspezioneCurrent.TemperaturaFumi.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.TemperaraturaComburente.ToString()))
                    txtTemperaturaComburente.Text = rapportoIspezioneCurrent.TemperaraturaComburente.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.O2.ToString()))
                    txtO2.Text = rapportoIspezioneCurrent.O2.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.Co2.ToString()))
                    txtCO2.Text = rapportoIspezioneCurrent.Co2.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.bacharach1.ToString()))
                    txtBacharach1.Text = rapportoIspezioneCurrent.bacharach1.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.bacharach2.ToString()))
                    txtBacharach2.Text = rapportoIspezioneCurrent.bacharach2.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.bacharach3.ToString()))
                    txtBacharach3.Text = rapportoIspezioneCurrent.bacharach3.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.COFumiSecchi.ToString()))
                    txtCoFumiSecchi.Text = rapportoIspezioneCurrent.COFumiSecchi.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.CoCorretto.ToString()))
                    txtCoCorretto.Text = rapportoIspezioneCurrent.CoCorretto.ToString();

                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PortataCombustibile.ToString()))      
                //    txtPortataCombustibile.Text = rapportoIspezioneCurrent.PortataCombustibile.ToString();

                //if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.PotenzaTermicaEffettiva.ToString()))       
                //    txtPotenzaTermicaEffettiva.Text = rapportoIspezioneCurrent.PotenzaTermicaEffettiva.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.RendimentoCombustibile.ToString()))
                    txtRendimentoCombustione.Text = rapportoIspezioneCurrent.RendimentoCombustibile.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.RendimentoMinimoCombustibile.ToString()))
                    txtRendimentoMinimo.Text = rapportoIspezioneCurrent.RendimentoMinimoCombustibile.ToString();

                cbTipoDistribuzione.Value = rapportoIspezioneCurrent.IDTipologiaSistemaDistribuzione;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.AltroTipologiaSistemaDistribuzione))
                    txtAltroDistribuzione.Text = rapportoIspezioneCurrent.AltroTipologiaSistemaDistribuzione;

                cbUnitaImmobiliariContabilizzate.Value = rapportoIspezioneCurrent.UnitaImmobiliariContabilizzazione;
                cbTipologiaContabilizzazione.Value = rapportoIspezioneCurrent.IDTipologiaContabilizzazione;
                cbUnitaImmobiliariTermoregolazione.Value = rapportoIspezioneCurrent.UnitaImmobiliariTermoregolazione;
                cbTipologiaTermoregolazione.Value = rapportoIspezioneCurrent.IDTipologiaSistemaTermoregolazione;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.AltroSistemaTermoregolazione))
                    txtAltriSistemiTermoregolazione.Text = rapportoIspezioneCurrent.AltroSistemaTermoregolazione;

                cbFunzionamentoSistemaRegPrincipale.Value = rapportoIspezioneCurrent.CorrettoFunzionamentoRegolazione;
                cbFunzionamentoSistemaRegAbitative.Value = rapportoIspezioneCurrent.CorrettoFunzionamentoInterno;
                cbMotivazioneEsenzione.Value = rapportoIspezioneCurrent.MotivazioneEsenzione;
                cbPresenzaRelazioneTecnica.Value = rapportoIspezioneCurrent.PresenzaRelazioneTecnica;

                switch (rapportoIspezioneCurrent.PresenzaDocumentaleAdozione)
                {
                    case -1:
                        chkVerificaDocumentaleAdozione.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkVerificaDocumentaleAdozione.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkVerificaDocumentaleAdozione.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.NumeroRilevazioniEseguite.ToString()))
                    txtMisurazioniEseguite.Text = rapportoIspezioneCurrent.NumeroRilevazioniEseguite.ToString();

                switch (rapportoIspezioneCurrent.RispettoValoriNormativaVigente)
                {
                    case -1:
                        chkRispettoValoriVigente.Value = EnumStatoSiNoNc.NonClassificabile;
                        break;
                    case 0:
                        chkRispettoValoriVigente.Value = EnumStatoSiNoNc.No;
                        break;
                    case 1:
                        chkRispettoValoriVigente.Value = EnumStatoSiNoNc.Si;
                        break;
                }

                if (rapportoIspezioneCurrent.InterventiAtti != null)
                {
                    rblInterventiAttiMigliorare.SelectedValue = rapportoIspezioneCurrent.InterventiAtti.ToString();
                }
                if (rapportoIspezioneCurrent.StimaDimensionamentoGeneratore != null)
                {
                    rblStima.SelectedValue = rapportoIspezioneCurrent.StimaDimensionamentoGeneratore.ToString();
                }
                

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.Osservazioni))
                    txtOsservazioni.Text = rapportoIspezioneCurrent.Osservazioni;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.Raccomandazioni))
                    txtRaccomandazzioni.Text = rapportoIspezioneCurrent.Raccomandazioni;

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.Prescrizioni))
                    txtPrescrizioni.Text = rapportoIspezioneCurrent.Prescrizioni;

                //if (rapportoIspezioneCurrent.fImpiantoPuoFunzionare != null)
                    rblImpiantoPuoFunzionare.SelectedValue = rapportoIspezioneCurrent.fImpiantoPuoFunzionare.ToString();

                if (!string.IsNullOrEmpty(rapportoIspezioneCurrent.DichiarazioniResponsabileImpianto))
                    txtDichiarazioniResponsabileImpianto.Text = rapportoIspezioneCurrent.DichiarazioniResponsabileImpianto;

                #endregion
            }
        }
    }

    public void EnableIspezioneRapporto(int iDStatoIspezione)
    {
        switch (iDStatoIspezione)
        {
            case 1: //Ricerca Ispettore
                tblIspezioneRapporto.Enabled = false;
                break;
            case 2: //Ispezione da Pianificare
                tblIspezioneRapporto.Enabled = false;
                break;
            case 3://Ispezione Pianificata in attesa di conferma
                tblIspezioneRapporto.Enabled = false;
                break;
            case 4: //Ispezione Conclusa da Ispettore
                tblIspezioneRapporto.Enabled = false;
                pnlInsertStrumenti.Visible = false;
                break;
            case 5: //Ispezione Conclusa da Coordinatore con accertamento
                tblIspezioneRapporto.Enabled = false;
                pnlInsertStrumenti.Visible = false;
                break;
            case 6: //Ispezione Confermata da Inviare LAI
                tblIspezioneRapporto.Enabled = false;
                break;
            case 7://Ispezione Pianificata confermata
                tblIspezioneRapporto.Enabled = true;
                break;
            case 8://Ispezione Annullata
                tblIspezioneRapporto.Enabled = false;
                break;
            case 9://Ispezione Conclusa da Coordinatore senza accertamento
                tblIspezioneRapporto.Enabled = false;
                pnlInsertStrumenti.Visible = false;
                break;
        }
        
        //if (iDStatoIspezione != 3) // 3 - Ispezione Pianificata
        //{
        //    pnlInsertStrumenti.Visible = false;
        //    tblIspezioneRapporto.Enabled = false;
        //}
        //else
        //{
        //    pnlInsertStrumenti.Visible = true;
        //    tblIspezioneRapporto.Enabled = true;
        //}
    }

    public void SalvaDatiIspezioneRapporto(long iDIspezione)
    {
        using (CriterDataModel db = new CriterDataModel())
        {
            var saveDatiRapportoIspezione = db.VER_IspezioneRapporto.FirstOrDefault(i => i.IDIspezione == iDIspezione);

            DateTime ToFix = DateTime.Parse("01/01/0001");

            #region DATI

            saveDatiRapportoIspezione.fDelegatoNominato = bool.Parse(rblNominatoDelega.SelectedValue);

            if (!string.IsNullOrEmpty(txtNomeDelegato.Text))
            {
                saveDatiRapportoIspezione.NomeDelegato = txtNomeDelegato.Text;
            }
            else
            {
                saveDatiRapportoIspezione.NomeDelegato = null;
            }

            if (!string.IsNullOrEmpty(txtCognomeDelegato.Text))
            {
                saveDatiRapportoIspezione.CognomeDelegato = txtCognomeDelegato.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CognomeDelegato = null;
            }

            if (!string.IsNullOrEmpty(txtCFDelega.Text))
            {
                saveDatiRapportoIspezione.CodiceFiscaleDelegato = txtCFDelega.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CodiceFiscaleDelegato = null;
            }

            //if (!string.IsNullOrEmpty(rblDelegaIspezionePresente.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fDelega = null;
            //}
            saveDatiRapportoIspezione.fDelega = bool.Parse(rblDelegaIspezionePresente.SelectedValue);

            // Operatore/Conduttore

            saveDatiRapportoIspezione.fOperatoreConduttoreNominato = bool.Parse(rblOCNominato.SelectedValue);

            //if (!string.IsNullOrEmpty(rblOCNominatoPresente.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fOperatoreConduttorePresente = null;
            //}
            saveDatiRapportoIspezione.fOperatoreConduttorePresente = bool.Parse(rblOCNominatoPresente.SelectedValue);
            if (!string.IsNullOrEmpty(txtOCNome.Text))
            {
                saveDatiRapportoIspezione.NomeOperatoreConduttore = txtOCNome.Text;
            }
            else
            {
                saveDatiRapportoIspezione.NomeOperatoreConduttore = null;
            }

            if (!string.IsNullOrEmpty(txtOCCOgnome.Text))
            {
                saveDatiRapportoIspezione.CognomeOperatoreConduttore = txtOCCOgnome.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CognomeOperatoreConduttore = null;
            }

            if (!string.IsNullOrEmpty(txtOCCF.Text))
            {
                saveDatiRapportoIspezione.CodiceFiscaleOperatoreConduttore = txtOCCF.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CodiceFiscaleOperatoreConduttore = null;
            }

            //if (!string.IsNullOrEmpty(rblOCAbilitato.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fOperatoreConduttoreAbilitato = null;
            //}
            saveDatiRapportoIspezione.fOperatoreConduttoreAbilitato = bool.Parse(rblOCAbilitato.SelectedValue);
            if (!string.IsNullOrEmpty(rblTipoAbilitazione.SelectedValue))
            {
                saveDatiRapportoIspezione.IDTipoAbilitazione = int.Parse(rblTipoAbilitazione.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.IDTipoAbilitazione = null;
            }

            if (!string.IsNullOrEmpty(txtOCPatentinoNumero.Text))
            {
                saveDatiRapportoIspezione.NumeroPatentinoOperatoreConduttore = txtOCPatentinoNumero.Text;
            }
            else
            {
                saveDatiRapportoIspezione.NumeroPatentinoOperatoreConduttore = null;
            }

            if (!string.IsNullOrEmpty(txtDataRilascioPatentino.Text))
            {
                saveDatiRapportoIspezione.DataRilascioPatentinoOperatoreConduttore = DateTime.Parse(txtDataRilascioPatentino.Text);
            }
            else
            {
                saveDatiRapportoIspezione.DataRilascioPatentinoOperatoreConduttore = null;
            }

            saveDatiRapportoIspezione.fImpiantoRegistrato = bool.Parse(rblImpiantoRegistratoCRITER.SelectedValue);


            if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleFocalore.Text))
            {
                saveDatiRapportoIspezione.PotenzaTermicaNominaleTotaleFocolare = decimal.Parse(txtPotenzaTermicaNominaleTotaleFocalore.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PotenzaTermicaNominaleTotaleFocolare = null;
            }

            if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleUtile.Text))
            {
                saveDatiRapportoIspezione.PotenzaTermicaNominaleTotaleUtile = decimal.Parse(txtPotenzaTermicaNominaleTotaleUtile.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PotenzaTermicaNominaleTotaleUtile = null;
            }

            // RESPONSABILE

            if (!string.IsNullOrEmpty(rblTipologiaResponsabile.SelectedValue))
            {
                saveDatiRapportoIspezione.IDTipologiaResponsabile = int.Parse(rblTipologiaResponsabile.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.IDTipologiaResponsabile = null;
            }


            if (!string.IsNullOrEmpty(txtNomeResponsabile.Text))
            {
                saveDatiRapportoIspezione.NomeResponsabile = txtNomeResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.NomeResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtCognomeResponsabile.Text))
            {
                saveDatiRapportoIspezione.CognomeResponsabile = txtCognomeResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CognomeResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtCFResponsabile.Text))
            {
                saveDatiRapportoIspezione.CodiceFiscaleResponsabile = txtCFResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CodiceFiscaleResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtRagioneSocialeResponsabile.Text))
            {
                saveDatiRapportoIspezione.RagioneSocialeResponsabile = txtRagioneSocialeResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.RagioneSocialeResponsabile = null;
            }

            if (cbComuneResponsabile.Value != null)
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleResponsabile = int.Parse(cbComuneResponsabile.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtPIVAResponsabile.Text))
            {
                saveDatiRapportoIspezione.PartitaIVAResponsabile = txtPIVAResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.PartitaIVAResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtIndirizzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.IndirizzoResponsabile = txtIndirizzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.IndirizzoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtCivicoResponsabile.Text))
            {
                saveDatiRapportoIspezione.CivicoResponsabile = txtCivicoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CivicoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtTelefonoResponsabile.Text))
            {
                saveDatiRapportoIspezione.TelefonoResponsabile = txtTelefonoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.TelefonoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtEmailResponsabile.Text))
            {
                saveDatiRapportoIspezione.EmailResponsabile = txtEmailResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.EmailResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtEmailPECResponsabile.Text))
            {
                saveDatiRapportoIspezione.EmailPECResponsabile = txtEmailPECResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.EmailPECResponsabile = null;
            }

            // TERZO RESPONSABILE 
            saveDatiRapportoIspezione.fTerzoResponsabile = bool.Parse(rblfTerzoResponsabile.SelectedValue);

            if (!string.IsNullOrEmpty(txtRagioneSocialeTerzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.RagioneSocialeTerzoResponsabile = txtRagioneSocialeTerzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.RagioneSocialeTerzoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtPartitaIVATerzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.PartitaIVATerzoResponsabile = txtPartitaIVATerzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.PartitaIVATerzoResponsabile = null;
            }

            if (cbComuneTerzoResponsabile.Value != null)
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleTerzoResponsabile = int.Parse(cbComuneTerzoResponsabile.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleTerzoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtIndirizzoTerzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.IndirizzoTerzoResponsabile = txtIndirizzoTerzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.IndirizzoTerzoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtNumeroCivicoTerzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.CivicoTerzoResponsabile = txtNumeroCivicoTerzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CivicoTerzoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtTelefonoTerzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.TelefonoTerzoResponsabile = txtTelefonoTerzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.TelefonoTerzoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtEmailTerzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.EmailTerzoResponsabile = txtEmailTerzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.EmailTerzoResponsabile = null;
            }

            if (!string.IsNullOrEmpty(txtEmailPECTerzoResponsabile.Text))
            {
                saveDatiRapportoIspezione.EmailPECTerzoResponsabile = txtEmailPECTerzoResponsabile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.EmailPECTerzoResponsabile = null;
            }

            //if (!string.IsNullOrEmpty(rblAbilitazioneTerzoResponsabile.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fAbilitatoTerzoResponsabile = null;
            //}
            saveDatiRapportoIspezione.fAbilitatoTerzoResponsabile = bool.Parse(rblAbilitazioneTerzoResponsabile.SelectedValue);

            //if (!string.IsNullOrEmpty(rblCertificazioneTerzoResponsabile.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fCertificatoTerzoResponsabile = null;
            //}

            saveDatiRapportoIspezione.fCertificatoTerzoResponsabile = bool.Parse(rblCertificazioneTerzoResponsabile.SelectedValue);
            //if (!string.IsNullOrEmpty(rblAttestatoTerzoResponsabile.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fAttestatoTerzoResponsabile = null;
            //}
            saveDatiRapportoIspezione.fAttestatoTerzoResponsabile = bool.Parse(rblAttestatoTerzoResponsabile.SelectedValue);

            //if (!string.IsNullOrEmpty(rblAttestazioneIncaricoTerzoResponsabile.SelectedValue))
            //{
               
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fAttestatoIncaricoTerzoResponsabile = null;
            //}
            saveDatiRapportoIspezione.fAttestatoIncaricoTerzoResponsabile = bool.Parse(rblAttestazioneIncaricoTerzoResponsabile.SelectedValue);
            // IMPRESA MANUTENTRICE

            //if (!string.IsNullOrEmpty(rblImpresaManutentricePresente.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fImpresaManutentrice = null;
            //}
            saveDatiRapportoIspezione.fImpresaManutentrice = bool.Parse(rblImpresaManutentricePresente.SelectedValue);
            if (!string.IsNullOrEmpty(txtRagioneSocialeImpresaManutentrice.Text))
            {
                saveDatiRapportoIspezione.RagioneSocialeImpresaManutentrice = txtRagioneSocialeImpresaManutentrice.Text;
            }
            else
            {
                saveDatiRapportoIspezione.RagioneSocialeImpresaManutentrice = null;
            }

            if (!string.IsNullOrEmpty(txtPartitaIVAImpresaManutentrice.Text))
            {
                saveDatiRapportoIspezione.PartitaIVAImpresaManutentrice = txtPartitaIVAImpresaManutentrice.Text;
            }
            else
            {
                saveDatiRapportoIspezione.PartitaIVAImpresaManutentrice = null;
            }

            if (cbComuneImpresaManutentrice.Value != null)
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleImpresaManutentrice = int.Parse(cbComuneImpresaManutentrice.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleImpresaManutentrice = null;
            }

            if (!string.IsNullOrEmpty(txtIndirizzoImpresaManutentrice.Text))
            {
                saveDatiRapportoIspezione.IndirizzoImpresaManutentrice = txtIndirizzoImpresaManutentrice.Text;
            }
            else
            {
                saveDatiRapportoIspezione.IndirizzoImpresaManutentrice = null;
            }

            if (!string.IsNullOrEmpty(txtNumeroCivicoImpresaManutentrice.Text))
            {
                saveDatiRapportoIspezione.CivicoImpresaManutentrice = txtNumeroCivicoImpresaManutentrice.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CivicoImpresaManutentrice = null;
            }

            if (!string.IsNullOrEmpty(txtTelefonoImpresaManutentrice.Text))
            {
                saveDatiRapportoIspezione.TelefonoImpresaManutentrice = txtTelefonoImpresaManutentrice.Text;
            }
            else
            {
                saveDatiRapportoIspezione.TelefonoImpresaManutentrice = null;
            }

            if (!string.IsNullOrEmpty(txtEmailImpresaManutentrice.Text))
            {
                saveDatiRapportoIspezione.EmailImpresaManutentrice = txtEmailImpresaManutentrice.Text;
            }
            else
            {
                saveDatiRapportoIspezione.EmailImpresaManutentrice = null;
            }

            if (!string.IsNullOrEmpty(txtEmailPECImpresaManutentrice.Text))
            {
                saveDatiRapportoIspezione.EmailPECImpresaManutentrice = txtEmailPECImpresaManutentrice.Text;
            }
            else
            {
                saveDatiRapportoIspezione.EmailPECImpresaManutentrice = null;
            }

            if (!string.IsNullOrEmpty(rblAbilitazioneImpresaManutentrice.SelectedValue))
            {
                saveDatiRapportoIspezione.fAbilitataImpresaManutentrice = bool.Parse(rblAbilitazioneImpresaManutentrice.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.fAbilitataImpresaManutentrice = false;
            }


            if (!string.IsNullOrEmpty(rblCertificazioneImpresaManutentrice.SelectedValue))
            {
                saveDatiRapportoIspezione.fCertificataImpresaManutentrice = bool.Parse(rblCertificazioneImpresaManutentrice.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.fCertificataImpresaManutentrice = false;
            }

            if (!string.IsNullOrEmpty(rblAttestatoSOAImpresaManutentrice.SelectedValue))
            {
                saveDatiRapportoIspezione.fAttestataImpresaManutentrice = bool.Parse(rblAttestatoSOAImpresaManutentrice.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.fAttestataImpresaManutentrice = false;
            }


            // OPERATORE ULTIMO CONTROLLO

            if (!string.IsNullOrEmpty(txtNomeOperatoreUltimoControllo.Text))
            {
                saveDatiRapportoIspezione.NomeOperatore = txtNomeOperatoreUltimoControllo.Text;
            }
            else
            {
                saveDatiRapportoIspezione.NomeOperatore = null;
            }

            if (!string.IsNullOrEmpty(txtCognomeOperatoreUltimoControllo.Text))
            {
                saveDatiRapportoIspezione.CognomeOperatore = txtCognomeOperatoreUltimoControllo.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CognomeOperatore = null;
            }

            if (cbNatoOperatoreUltimoControllo.Value != null)
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleNascitaOperatore = int.Parse(cbNatoOperatoreUltimoControllo.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleNascitaOperatore = null;
            }

            if (deDataNascitaOperatoreUltimoControllo.Date != ToFix)
            {
                saveDatiRapportoIspezione.DataNascitaOperatore = deDataNascitaOperatoreUltimoControllo.Date;
            }
            else
            {
                saveDatiRapportoIspezione.DataNascitaOperatore = null;
            }

            if (cbComuneOperatoreUltimoControllo.Value != null)
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleOperatore = int.Parse(cbComuneOperatoreUltimoControllo.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDCodiceCatastaleOperatore = null;
            }

            if (!string.IsNullOrEmpty(txtIndirizzoOperatoreUltimoControllo.Text))
            {
                saveDatiRapportoIspezione.IndirizzoOperatore = txtIndirizzoOperatoreUltimoControllo.Text;
            }
            else
            {
                saveDatiRapportoIspezione.IndirizzoOperatore = null;
            }

            if (!string.IsNullOrEmpty(txtNumeroCivicoOperatoreUltimoControllo.Text))
            {
                saveDatiRapportoIspezione.CivicoOperatore = txtNumeroCivicoOperatoreUltimoControllo.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CivicoOperatore = null;
            }

            if (!string.IsNullOrEmpty(txtTelefonoOperatoreUltimoControllo.Text))
            {
                saveDatiRapportoIspezione.TelefonoOperatore = txtTelefonoOperatoreUltimoControllo.Text;
            }
            else
            {
                saveDatiRapportoIspezione.TelefonoOperatore = null;
            }

            if (!string.IsNullOrEmpty(txtEmailOperatoreUltimoControllo.Text))
            {
                saveDatiRapportoIspezione.EmailOperatore = txtEmailOperatoreUltimoControllo.Text;
            }
            else
            {
                saveDatiRapportoIspezione.EmailOperatore = null;
            }


            //if (!string.IsNullOrEmpty(rblPatentinoOperatoreUltimoControllo.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fPatentatoOperatore = null;
            //}
            saveDatiRapportoIspezione.fPatentatoOperatore = bool.Parse(rblPatentinoOperatoreUltimoControllo.SelectedValue);
            // 3.

            if (!string.IsNullOrEmpty(rblDestinazioneUso.SelectedValue))
            {
                saveDatiRapportoIspezione.IDDestinazioneUso = int.Parse(rblDestinazioneUso.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.IDDestinazioneUso = null;
            }

            if (cbUnitaImmobiliariServite.Value != null)
            {
                saveDatiRapportoIspezione.UnitaImmobiliariServite = int.Parse(cbUnitaImmobiliariServite.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.UnitaImmobiliariServite = null;
            }

            if (cbServiziServitiImpianto.Value != null)
            {
                saveDatiRapportoIspezione.ServiziServitiImpianto = int.Parse(cbServiziServitiImpianto.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.ServiziServitiImpianto = null;
            }

            if (!string.IsNullOrEmpty(txtVolumeLordoRiscaldato.Text))
            {
                saveDatiRapportoIspezione.VolumeLordoRiscaldato = decimal.Parse(txtVolumeLordoRiscaldato.Text);
            }
            else
            {
                saveDatiRapportoIspezione.VolumeLordoRiscaldato = null;
            }

            // 4.

            if (cbInstallazioneInternaEsterna.Value != null)
            {
                saveDatiRapportoIspezione.IDTipoInstallazione = int.Parse(cbInstallazioneInternaEsterna.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDTipoInstallazione = null;
            }

            switch (chkLocaleInstallazioneIdoneo.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.LocaleInstallazioneIdoneo = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.LocaleInstallazioneIdoneo = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.LocaleInstallazioneIdoneo = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.LocaleInstallazioneIdoneo = null;
                    break;
            }

            switch (chkGeneratoriIdonei.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.GeneratoriInstallazioneIdonei = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.GeneratoriInstallazioneIdonei = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.GeneratoriInstallazioneIdonei = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.GeneratoriInstallazioneIdonei = null;
                    break;
            }

            switch (chkApertureLibere.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.ApertureLibere = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.ApertureLibere = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.ApertureLibere = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.ApertureLibere = null;
                    break;
            }

            switch (chkDimensioniApertureAdeguate.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.DimensioniApertureAdeguate = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.DimensioniApertureAdeguate = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.DimensioniApertureAdeguate = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.DimensioniApertureAdeguate = null;
                    break;
            }

            switch (chkScarichiIdonei.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.ScarichiIdonei = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.ScarichiIdonei = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.ScarichiIdonei = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.ScarichiIdonei = null;
                    break;
            }

            switch (chkAssenzaPerditeCombustibile.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.AssenzaPerditeCombustibile = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.AssenzaPerditeCombustibile = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.AssenzaPerditeCombustibile = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.AssenzaPerditeCombustibile = null;
                    break;
            }

            switch (chkTenutaImpianto.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.TenutaImpiantoIdraulico = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.TenutaImpiantoIdraulico = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.TenutaImpiantoIdraulico = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.TenutaImpiantoIdraulico = null;
                    break;
            }

            if (!string.IsNullOrEmpty(txtTiraggioDelCamino.Text))
            {
                saveDatiRapportoIspezione.TiraggioProvaStrumentalePA = decimal.Parse(txtTiraggioDelCamino.Text);
            }
            else
            {
                saveDatiRapportoIspezione.TiraggioProvaStrumentalePA = null;
            }

            //if (!string.IsNullOrEmpty(rblTiraggioDelCamino.Text))
            //{
            //    saveDatiRapportoIspezione.TiraggioProvaIndirettaCalcolata = int.Parse(rblTiraggioDelCamino.SelectedValue);
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.TiraggioProvaIndirettaCalcolata = null;
            //}

            //if (!string.IsNullOrEmpty(rblPresenteLibretto.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fLibrettoImpiantoPresente = null;
            //}
            saveDatiRapportoIspezione.fLibrettoImpiantoPresente = bool.Parse(rblPresenteLibretto.SelectedValue);


            //if (!string.IsNullOrEmpty(rblLibrettoUsoManutenzione.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fUsoManutenzioneGeneratore = null;
            //}
            saveDatiRapportoIspezione.fUsoManutenzioneGeneratore = bool.Parse(rblLibrettoUsoManutenzione.SelectedValue);


            //if (!string.IsNullOrEmpty(rblLibrettoCompilatoInTutteLeParte.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fLibrettoImpiantoCompilato = null;
            //}
            saveDatiRapportoIspezione.fLibrettoImpiantoCompilato = bool.Parse(rblLibrettoCompilatoInTutteLeParte.SelectedValue);
            //if (!string.IsNullOrEmpty(rblDichiarazione.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fDichiarazioneConformita = null;
            //}
            saveDatiRapportoIspezione.fDichiarazioneConformita = bool.Parse(rblDichiarazione.SelectedValue);


            //if (!string.IsNullOrEmpty(rblDocumentazioneTecnicaAsseverata.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fDocumentazioneTecnicaAsservata = null;
            //}
            //saveDatiRapportoIspezione.fDocumentazioneTecnicaAsservata = bool.Parse(rblDocumentazioneTecnicaAsseverata.SelectedValue);
            //// 6.

            //if (!string.IsNullOrEmpty(rblCategoriaAnticendio.SelectedValue))
            //{
            //    saveDatiRapportoIspezione.IDCategoriaDocumentazione = int.Parse(rblCategoriaAnticendio.SelectedValue);
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.IDCategoriaDocumentazione = null;
            //}

            //if (!string.IsNullOrEmpty(txtCodiceRischio.Text))
            //{
            //    saveDatiRapportoIspezione.CodiceRischio = int.Parse(txtCodiceRischio.Text);
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.CodiceRischio = null;
            //}

            //switch (chkProggettoAntincendio.Value)
            //{
            //    case EnumStatoSiNoNc.NonClassificabile:
            //        saveDatiRapportoIspezione.ProgettoAntincendio = (int)EnumStatoSiNoNc.NonClassificabile;
            //        break;
            //    case EnumStatoSiNoNc.No:
            //        saveDatiRapportoIspezione.ProgettoAntincendio = (int)EnumStatoSiNoNc.No;
            //        break;
            //    case EnumStatoSiNoNc.Si:
            //        saveDatiRapportoIspezione.ProgettoAntincendio = (int)EnumStatoSiNoNc.Si;
            //        break;
            //    case null:
            //        saveDatiRapportoIspezione.ProgettoAntincendio = null;
            //        break;
            //}

            //if (deProgettoAntincendio.Date != ToFix)
            //{
            //    saveDatiRapportoIspezione.DataProgettoAntincendio = deProgettoAntincendio.Date;
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.DataProgettoAntincendio = null;
            //}

            switch (chkSCIAAntincendio.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.SCIAAntincendio = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.SCIAAntincendio = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.SCIAAntincendio = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.SCIAAntincendio = null;
                    break;
            }

            if (deSCIAAntincendio.Date != ToFix)
            {
                saveDatiRapportoIspezione.DataSCIAAntincendio = deSCIAAntincendio.Date;
            }
            else
            {
                saveDatiRapportoIspezione.DataSCIAAntincendio = null;
            }

            //switch (chkVerbaleSopralluogo.Value)
            //{
            //    case EnumStatoSiNoNc.NonClassificabile:
            //        saveDatiRapportoIspezione.VerbaleSopralluogo = (int)EnumStatoSiNoNc.NonClassificabile;
            //        break;
            //    case EnumStatoSiNoNc.No:
            //        saveDatiRapportoIspezione.VerbaleSopralluogo = (int)EnumStatoSiNoNc.No;
            //        break;
            //    case EnumStatoSiNoNc.Si:
            //        saveDatiRapportoIspezione.VerbaleSopralluogo = (int)EnumStatoSiNoNc.Si;
            //        break;
            //    case null:
            //        saveDatiRapportoIspezione.VerbaleSopralluogo = null;
            //        break;
            //}

            //if (deVerbaleSopralluogo.Date != ToFix)
            //{
            //    saveDatiRapportoIspezione.DataVerbaleSopralluogo = deVerbaleSopralluogo.Date;
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.DataVerbaleSopralluogo = null;
            //}

            //switch (chkUltimoRinnovoPeriodico.Value)
            //{
            //    case EnumStatoSiNoNc.NonClassificabile:
            //        saveDatiRapportoIspezione.UltimoRinnovoPeriodico = (int)EnumStatoSiNoNc.NonClassificabile;
            //        break;
            //    case EnumStatoSiNoNc.No:
            //        saveDatiRapportoIspezione.UltimoRinnovoPeriodico = (int)EnumStatoSiNoNc.No;
            //        break;
            //    case EnumStatoSiNoNc.Si:
            //        saveDatiRapportoIspezione.UltimoRinnovoPeriodico = (int)EnumStatoSiNoNc.Si;
            //        break;
            //    case null:
            //        saveDatiRapportoIspezione.UltimoRinnovoPeriodico = null;
            //        break;
            //}

            //if (deUltimoRinnovoPeriodico.Date != ToFix)
            //{
            //    saveDatiRapportoIspezione.DataUltimoRinnovoPeriodico = deUltimoRinnovoPeriodico.Date;
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.DataUltimoRinnovoPeriodico = null;
            //}

            //if (!string.IsNullOrEmpty(rblPresenteProgettoImpianto.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fProgettoImpiantoPresente = null;
            //}
            saveDatiRapportoIspezione.fProgettoImpiantoPresente = bool.Parse(rblPresenteProgettoImpianto.SelectedValue);
            if (!string.IsNullOrEmpty(txtProgettista.Text))
            {
                saveDatiRapportoIspezione.Progettista = txtProgettista.Text;
            }
            else
            {
                saveDatiRapportoIspezione.Progettista = null;
            }

            if (!string.IsNullOrEmpty(txtProtocolloDepositoComune.Text))
            {
                saveDatiRapportoIspezione.ProtocolloDepositoComune = txtProtocolloDepositoComune.Text;
            }
            else
            {
                saveDatiRapportoIspezione.ProtocolloDepositoComune = null;
            }

            if (deDataDepositoComune.Date != ToFix)
            {
                saveDatiRapportoIspezione.DataDepositoComune = deDataDepositoComune.Date;
            }
            else
            {
                saveDatiRapportoIspezione.DataDepositoComune = null;
            }

            if (!string.IsNullOrEmpty(txtPotenzaTermicaUtileProgetto.Text))
            {
                saveDatiRapportoIspezione.PotenzaProgetto = decimal.Parse(txtPotenzaTermicaUtileProgetto.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PotenzaProgetto = null;
            }

            //switch (chkAQE.Value)
            //{
            //    case EnumStatoSiNoNc.NonClassificabile:
            //        saveDatiRapportoIspezione.AQEAttestato = (int)EnumStatoSiNoNc.NonClassificabile;
            //        break;
            //    case EnumStatoSiNoNc.No:
            //        saveDatiRapportoIspezione.AQEAttestato = (int)EnumStatoSiNoNc.No;
            //        break;
            //    case EnumStatoSiNoNc.Si:
            //        saveDatiRapportoIspezione.AQEAttestato = (int)EnumStatoSiNoNc.Si;
            //        break;
            //    case null:
            //        saveDatiRapportoIspezione.AQEAttestato = null;
            //        break;
            //}

            switch (chkDiagnosiEnergetica.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.DiagnosiEnergetica = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.DiagnosiEnergetica = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.DiagnosiEnergetica = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.DiagnosiEnergetica = null;
                    break;
            }

            switch (chkPerizia.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.Perizia = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.Perizia = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.Perizia = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.Perizia = null;
                    break;
            }

            switch (chkOmologazioneVerifiche.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.OmologazioneVerifiche = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.OmologazioneVerifiche = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.OmologazioneVerifiche = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.OmologazioneVerifiche = null;
                    break;
            }

            if (deDataInstallazioneGeneratore.Date != ToFix)
            {
                saveDatiRapportoIspezione.DataInstallazioneGeneratore = deDataInstallazioneGeneratore.Date;
            }
            else
            {
                saveDatiRapportoIspezione.DataInstallazioneGeneratore = null;
            }

            if (cbFluidoTermoVettore.Value != null)
            {
                saveDatiRapportoIspezione.IDTipologiaFluidoTermoVettore = int.Parse(cbFluidoTermoVettore.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDTipologiaFluidoTermoVettore = null;
            }

            if (!string.IsNullOrEmpty(txtAltroFluidoTermoVettore.Text))
            {
                saveDatiRapportoIspezione.AltroFluidoTermovettore = txtAltroFluidoTermoVettore.Text;
            }
            else
            {
                saveDatiRapportoIspezione.AltroFluidoTermovettore = null;
            }

            saveDatiRapportoIspezione.EvacuazioneForzata = rblEvacuazioneForzata.Checked;
            saveDatiRapportoIspezione.EvacuazioneNaturale = rblEvacuazioneNaturale.Checked;

            if (cbCombustibile.Value != null)
            {
                saveDatiRapportoIspezione.IDTipologiaCombustibile = int.Parse(cbCombustibile.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDTipologiaCombustibile = null;
            }

            if (!string.IsNullOrEmpty(txtAltroCombustibile.Text))
            {
                saveDatiRapportoIspezione.AltroCombustibile = txtAltroCombustibile.Text;
            }
            else
            {
                saveDatiRapportoIspezione.AltroCombustibile = null;
            }

            if (!string.IsNullOrEmpty(txtCostruttoreBruciatore.Text))
            {
                saveDatiRapportoIspezione.CostruttoreBruciatore = txtCostruttoreBruciatore.Text;
            }
            else
            {
                saveDatiRapportoIspezione.CostruttoreBruciatore = null;
            }

            if (!string.IsNullOrEmpty(txtModelloBruciatore.Text))
            {
                saveDatiRapportoIspezione.ModelloBruciatore = txtModelloBruciatore.Text;
            }
            else
            {
                saveDatiRapportoIspezione.ModelloBruciatore = null;
            }

            if (!string.IsNullOrEmpty(txtMatricolaBruciatore.Text))
            {
                saveDatiRapportoIspezione.MatricolaBruciatore = txtMatricolaBruciatore.Text;
            }
            else
            {
                saveDatiRapportoIspezione.MatricolaBruciatore = null;
            }

            if (!string.IsNullOrEmpty(txtPotenzaTermicaFocolareDatiNominali.Text))
            {
                saveDatiRapportoIspezione.PotenzaTermicaFocolareGeneratore = decimal.Parse(txtPotenzaTermicaFocolareDatiNominali.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PotenzaTermicaFocolareGeneratore = null;
            }

            if (!string.IsNullOrEmpty(txtPotenzaTermicaUtileDatiNominali.Text))
            {
                saveDatiRapportoIspezione.PotenzaTermicaNominaleGeneratore = decimal.Parse(txtPotenzaTermicaUtileDatiNominali.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PotenzaTermicaNominaleGeneratore = null;
            }

            if (!string.IsNullOrEmpty(txtCampoLavoroBruciatoreDatiNominaliDa.Text))
            {
                saveDatiRapportoIspezione.LavoroBruciatoreDa = decimal.Parse(txtCampoLavoroBruciatoreDatiNominaliDa.Text);
            }
            else
            {
                saveDatiRapportoIspezione.LavoroBruciatoreDa = null;
            }

            if (!string.IsNullOrEmpty(txtCampoLavoroBruciatoreDatiNominaliA.Text))
            {
                saveDatiRapportoIspezione.LavoroBruciatoreA = decimal.Parse(txtCampoLavoroBruciatoreDatiNominaliA.Text);
            }
            else
            {
                saveDatiRapportoIspezione.LavoroBruciatoreA = null;
            }

            if (!string.IsNullOrEmpty(txtPortataCombustibileValoriMisuratim3h.Text))
            {
                saveDatiRapportoIspezione.PortataCombustibileValoriMisuratiM3H = decimal.Parse(txtPortataCombustibileValoriMisuratim3h.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PortataCombustibileValoriMisuratiM3H = null;
            }

            if (!string.IsNullOrEmpty(txtPortataCombustibileValoriMisuratikgh.Text))
            {
                saveDatiRapportoIspezione.PortataCombustibileValoriMisuratiKG = decimal.Parse(txtPortataCombustibileValoriMisuratikgh.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PortataCombustibileValoriMisuratiKG = null;
            }

            if (!string.IsNullOrEmpty(txtPotenzaTermicaFocolareValoriMisurati.Text))
            {
                saveDatiRapportoIspezione.PotenzaTermicaFocolareValoriMisurati = decimal.Parse(txtPotenzaTermicaFocolareValoriMisurati.Text);
            }
            else
            {
                saveDatiRapportoIspezione.PotenzaTermicaFocolareValoriMisurati = null;
            }

            if (cbTipologiaGruppoTermico.Value != null)
            {
                saveDatiRapportoIspezione.IdTipologiaGruppiTermici = int.Parse(cbTipologiaGruppoTermico.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IdTipologiaGruppiTermici = null;
            }

            if (cbClassificazioneDPR66096.Value != null)
            {
                saveDatiRapportoIspezione.IdTipologiaGeneratoriTermici = int.Parse(cbClassificazioneDPR66096.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IdTipologiaGeneratoriTermici = null;
            }

            if (!string.IsNullOrEmpty(rblCorrettoDimensionamentoGeneratore.SelectedValue))
            {
                saveDatiRapportoIspezione.CorrettoDimensionamento = int.Parse(rblCorrettoDimensionamentoGeneratore.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.CorrettoDimensionamento = null;
            }

            if (!string.IsNullOrEmpty(rblTrattamentoRiscaldamento.SelectedValue))
            {
                saveDatiRapportoIspezione.TrattamentoRiscaldamento = int.Parse(rblTrattamentoRiscaldamento.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.TrattamentoRiscaldamento = null;
            }

            if (!string.IsNullOrEmpty(rblTrattamentoProduzioneACS.SelectedValue))
            {
                saveDatiRapportoIspezione.TrattamentoACS = int.Parse(rblTrattamentoProduzioneACS.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.TrattamentoACS = null;
            }

            UpdateTipologiaTrattamentoAcquaInvernale(int.Parse(saveDatiRapportoIspezione.IDIspezione.ToString()));
            UpdateTipologiaTrattamentoAcquaAcs(int.Parse(saveDatiRapportoIspezione.IDIspezione.ToString()));
            
            if (cbFrequenza.Value != null)
            {
                saveDatiRapportoIspezione.IDFrequenzaManutenzione = int.Parse(cbFrequenza.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDFrequenzaManutenzione = null;
            }

            if (!string.IsNullOrEmpty(txtAltroFrequenza.Text))
            {
                saveDatiRapportoIspezione.AltroFrequenzaManutenzione = txtAltroFrequenza.Text;
            }
            else
            {
                saveDatiRapportoIspezione.AltroFrequenzaManutenzione = null;
            }

            //if (!string.IsNullOrEmpty(rblUltimaManutenzionePrevistaEffettuata.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fUltimaManutenzioneEffettuata = null;
            //}
            saveDatiRapportoIspezione.fUltimaManutenzioneEffettuata = bool.Parse(rblUltimaManutenzionePrevistaEffettuata.SelectedValue);
            if (deDataUltimaManutenzione.Date != ToFix)
            {
                saveDatiRapportoIspezione.DataUltimaManutenzione = deDataUltimaManutenzione.Date;
            }
            else
            {
                saveDatiRapportoIspezione.DataUltimaManutenzione = null;
            }

            if (cbFrequenzaEfficienzaEnergetica.Value != null)
            {
                saveDatiRapportoIspezione.IDFrequenzaControllo = int.Parse(cbFrequenzaEfficienzaEnergetica.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDFrequenzaControllo = null;
            }

            if (!string.IsNullOrEmpty(txtAltroFrequenzaEfficienzaEnergetica.Text))
            {
                saveDatiRapportoIspezione.AltroFrequenzaControllo = txtAltroFrequenzaEfficienzaEnergetica.Text;
            }
            else
            {
                saveDatiRapportoIspezione.AltroFrequenzaControllo = null;
            }

            if (cbUltimoControlloPrevistoEffettuato.Value != null)
            {
                saveDatiRapportoIspezione.UltimoControlloEffettuato = int.Parse(cbUltimoControlloPrevistoEffettuato.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.UltimoControlloEffettuato = null;
            }

            if (deDataUltimoControllo.Date != ToFix)
            {
                saveDatiRapportoIspezione.DataUltimoControllo = deDataUltimoControllo.Date;
            }
            else
            {
                saveDatiRapportoIspezione.DataUltimoControllo = null;
            }

            if (cbRTCEEPresenteInCopia.Value != null)
            {
                saveDatiRapportoIspezione.RaportoControlloPresente = int.Parse(cbRTCEEPresenteInCopia.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.RaportoControlloPresente = null;
            }

            //if (!string.IsNullOrEmpty(rblRTCEERegistrato.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fRTCEEManutenzioneRegistaro = null;
            //}
            saveDatiRapportoIspezione.fRTCEEManutenzioneRegistaro = bool.Parse(rblRTCEERegistrato.SelectedValue);
            if (cbRTCEEinterventiPrevisti.Value != null)
            {
                saveDatiRapportoIspezione.RealizzatiInterventiPrevisti = int.Parse(cbRTCEEinterventiPrevisti.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.RealizzatiInterventiPrevisti = null;
            }

            saveDatiRapportoIspezione.fOsservazioniRCTEE = cbOsservazioniRCTEE.Checked;

            if (!string.IsNullOrEmpty(txtOsservazioniRCTEE.Text))
            {
                saveDatiRapportoIspezione.OsservazioniRCTEE = txtOsservazioniRCTEE.Text;
            }
            else
            {
                saveDatiRapportoIspezione.OsservazioniRCTEE = null;
            }

            saveDatiRapportoIspezione.fRaccomandazioniRCTEE = cbRaccomandazioniRCTEE.Checked;

            if (!string.IsNullOrEmpty(txtRaccomandazioniRCTEE.Text))
            {
                saveDatiRapportoIspezione.RaccomandazioniRCTEE = txtRaccomandazioniRCTEE.Text;
            }
            else
            {
                saveDatiRapportoIspezione.RaccomandazioniRCTEE = null;
            }

            saveDatiRapportoIspezione.fPrescrizioniRCTEE = cbPrescrizioniRCTEE.Checked;

            if (!string.IsNullOrEmpty(txtPrescrizioniRCTEE.Text))
            {
                saveDatiRapportoIspezione.PrescrizioniRCTEE = txtPrescrizioniRCTEE.Text;
            }
            else
            {
                saveDatiRapportoIspezione.PrescrizioniRCTEE = null;
            }

            //if (!string.IsNullOrEmpty(rblMonossidoCarbonio.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fCOFumiSecchiNoAria1000 = null;
            //}
            saveDatiRapportoIspezione.fCOFumiSecchiNoAria1000 = bool.Parse(rblMonossidoCarbonio.SelectedValue);
            //if (!string.IsNullOrEmpty(rblIndiceFumosita.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fIndiceFumositaNdiBacharch = null;
            //}
            saveDatiRapportoIspezione.fIndiceFumositaNdiBacharch = bool.Parse(rblIndiceFumosita.SelectedValue);
            //if (!string.IsNullOrEmpty(rblRendimentoCombustibileMinimo.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fRendimentoSupMinimo = null;
            //}
            saveDatiRapportoIspezione.fRendimentoSupMinimo = bool.Parse(rblRendimentoCombustibileMinimo.SelectedValue);
            if (!string.IsNullOrEmpty(txtModuloTermico.Text))
            {
                saveDatiRapportoIspezione.ModuloTermico = txtModuloTermico.Text;
            }
            else
            {
                saveDatiRapportoIspezione.ModuloTermico = null;
            }
            if (!string.IsNullOrEmpty(txtTemperaturaFumi.Text))
            {
                saveDatiRapportoIspezione.TemperaturaFumi = decimal.Parse(txtTemperaturaFumi.Text);
            }
            else
            {
                saveDatiRapportoIspezione.TemperaturaFumi = null;
            }

            if (!string.IsNullOrEmpty(txtTemperaturaComburente.Text))
            {
                saveDatiRapportoIspezione.TemperaraturaComburente = decimal.Parse(txtTemperaturaComburente.Text);
            }
            else
            {
                saveDatiRapportoIspezione.TemperaraturaComburente = null;
            }

            if (!string.IsNullOrEmpty(txtO2.Text))
            {
                saveDatiRapportoIspezione.O2 = decimal.Parse(txtO2.Text);
            }
            else
            {
                saveDatiRapportoIspezione.O2 = null;
            }

            if (!string.IsNullOrEmpty(txtCO2.Text))
            {
                saveDatiRapportoIspezione.Co2 = decimal.Parse(txtCO2.Text);
            }
            else
            {
                saveDatiRapportoIspezione.Co2 = null;
            }

            if (!string.IsNullOrEmpty(txtBacharach1.Text))
            {
                saveDatiRapportoIspezione.bacharach1 = decimal.Parse(txtBacharach1.Text);
            }
            else
            {
                saveDatiRapportoIspezione.bacharach1 = null;
            }

            if (!string.IsNullOrEmpty(txtBacharach2.Text))
            {
                saveDatiRapportoIspezione.bacharach2 = decimal.Parse(txtBacharach2.Text);
            }
            else
            {
                saveDatiRapportoIspezione.bacharach2 = null;
            }

            if (!string.IsNullOrEmpty(txtBacharach3.Text))
            {
                saveDatiRapportoIspezione.bacharach3 = decimal.Parse(txtBacharach3.Text);
            }
            else
            {
                saveDatiRapportoIspezione.bacharach3 = null;
            }

            if (!string.IsNullOrEmpty(txtCoFumiSecchi.Text))
            {
                saveDatiRapportoIspezione.COFumiSecchi = decimal.Parse(txtCoFumiSecchi.Text);
            }
            else
            {
                saveDatiRapportoIspezione.COFumiSecchi = null;
            }
            if (!string.IsNullOrEmpty(txtCoCorretto.Text))
            {
                saveDatiRapportoIspezione.CoCorretto = decimal.Parse(txtCoCorretto.Text);
            }
            else
            {
                saveDatiRapportoIspezione.CoCorretto = null;
            }
            
            if (!string.IsNullOrEmpty(txtRendimentoCombustione.Text))
            {
                saveDatiRapportoIspezione.RendimentoCombustibile = decimal.Parse(txtRendimentoCombustione.Text);
            }
            else
            {
                saveDatiRapportoIspezione.RendimentoCombustibile = null;
            }
            if (!string.IsNullOrEmpty(txtRendimentoMinimo.Text))
            {
                saveDatiRapportoIspezione.RendimentoMinimoCombustibile = decimal.Parse(txtRendimentoMinimo.Text);
            }
            else
            {
                saveDatiRapportoIspezione.RendimentoMinimoCombustibile = null;
            }


            if (cbTipoDistribuzione.Value != null)
            {
                saveDatiRapportoIspezione.IDTipologiaSistemaDistribuzione = int.Parse(cbTipoDistribuzione.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDTipologiaSistemaDistribuzione = null;
            }

            if (!string.IsNullOrEmpty(txtAltroDistribuzione.Text))
            {
                saveDatiRapportoIspezione.AltroTipologiaSistemaDistribuzione = txtAltroDistribuzione.Text;
            }
            else
            {
                saveDatiRapportoIspezione.AltroTipologiaSistemaDistribuzione = null;
            }

            if (cbUnitaImmobiliariContabilizzate.Value!=null)
            {
                saveDatiRapportoIspezione.UnitaImmobiliariContabilizzazione = int.Parse(cbUnitaImmobiliariContabilizzate.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.UnitaImmobiliariContabilizzazione = null;
            }

            if (cbTipologiaContabilizzazione.Value != null)
            {
                saveDatiRapportoIspezione.IDTipologiaContabilizzazione = int.Parse(cbTipologiaContabilizzazione.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDTipologiaContabilizzazione = null;
            }

            if (cbUnitaImmobiliariTermoregolazione.Value != null)
            {
                saveDatiRapportoIspezione.UnitaImmobiliariTermoregolazione = int.Parse(cbUnitaImmobiliariTermoregolazione.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.UnitaImmobiliariTermoregolazione = null;
            }

            if (cbTipologiaTermoregolazione.Value != null)
            {
                saveDatiRapportoIspezione.IDTipologiaSistemaTermoregolazione = int.Parse(cbTipologiaTermoregolazione.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.IDTipologiaSistemaTermoregolazione = null;
            }

            if (!string.IsNullOrEmpty(txtAltriSistemiTermoregolazione.Text))
            {
                saveDatiRapportoIspezione.AltroSistemaTermoregolazione = txtAltriSistemiTermoregolazione.Text;
            }
            else
            {
                saveDatiRapportoIspezione.AltroSistemaTermoregolazione = null;
            }


            if (cbFunzionamentoSistemaRegPrincipale.Value!=null)
            {
                saveDatiRapportoIspezione.CorrettoFunzionamentoRegolazione = int.Parse(cbFunzionamentoSistemaRegPrincipale.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.CorrettoFunzionamentoRegolazione = null;
            }

            if (cbFunzionamentoSistemaRegAbitative.Value != null)
            {
                saveDatiRapportoIspezione.CorrettoFunzionamentoInterno = int.Parse(cbFunzionamentoSistemaRegAbitative.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.CorrettoFunzionamentoInterno = null;
            }

            if (cbMotivazioneEsenzione.Value != null)
            {
                saveDatiRapportoIspezione.MotivazioneEsenzione = int.Parse(cbMotivazioneEsenzione.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.MotivazioneEsenzione = null;
            }

            if (cbPresenzaRelazioneTecnica.Value != null)
            {
                saveDatiRapportoIspezione.PresenzaRelazioneTecnica = int.Parse(cbPresenzaRelazioneTecnica.Value.ToString());
            }
            else
            {
                saveDatiRapportoIspezione.PresenzaRelazioneTecnica = null;
            }

            switch (chkVerificaDocumentaleAdozione.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.PresenzaDocumentaleAdozione = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.PresenzaDocumentaleAdozione = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.PresenzaDocumentaleAdozione = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.PresenzaDocumentaleAdozione = null;
                    break;
            }

            if (!string.IsNullOrEmpty(txtMisurazioniEseguite.Text))
            {
                saveDatiRapportoIspezione.NumeroRilevazioniEseguite = int.Parse(txtMisurazioniEseguite.Text);
            }
            else
            {
                saveDatiRapportoIspezione.NumeroRilevazioniEseguite = null;
            }

            switch (chkRispettoValoriVigente.Value)
            {
                case EnumStatoSiNoNc.NonClassificabile:
                    saveDatiRapportoIspezione.RispettoValoriNormativaVigente = (int)EnumStatoSiNoNc.NonClassificabile;
                    break;
                case EnumStatoSiNoNc.No:
                    saveDatiRapportoIspezione.RispettoValoriNormativaVigente = (int)EnumStatoSiNoNc.No;
                    break;
                case EnumStatoSiNoNc.Si:
                    saveDatiRapportoIspezione.RispettoValoriNormativaVigente = (int)EnumStatoSiNoNc.Si;
                    break;
                case null:
                    saveDatiRapportoIspezione.RispettoValoriNormativaVigente = null;
                    break;
            }

            if (!string.IsNullOrEmpty(rblInterventiAttiMigliorare.SelectedValue))
            {
                saveDatiRapportoIspezione.InterventiAtti = int.Parse(rblInterventiAttiMigliorare.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.InterventiAtti = null;
            }

            if (!string.IsNullOrEmpty(rblStima.SelectedValue))
            {
                saveDatiRapportoIspezione.StimaDimensionamentoGeneratore = int.Parse(rblStima.SelectedValue);
            }
            else
            {
                saveDatiRapportoIspezione.StimaDimensionamentoGeneratore = null;
            }

            if (!string.IsNullOrEmpty(txtOsservazioni.Text))
            {
                saveDatiRapportoIspezione.Osservazioni = txtOsservazioni.Text;
            }
            else
            {
                saveDatiRapportoIspezione.Osservazioni = null;
            }

            if (!string.IsNullOrEmpty(txtRaccomandazzioni.Text))
            {
                saveDatiRapportoIspezione.Raccomandazioni = txtRaccomandazzioni.Text;
            }
            else
            {
                saveDatiRapportoIspezione.Raccomandazioni = null;
            }

            if (!string.IsNullOrEmpty(txtPrescrizioni.Text))
            {
                saveDatiRapportoIspezione.Prescrizioni = txtPrescrizioni.Text;
            }
            else
            {
                saveDatiRapportoIspezione.Prescrizioni = null;
            }

            //if (!string.IsNullOrEmpty(rblImpiantoPuoFunzionare.SelectedValue))
            //{
                
            //}
            //else
            //{
            //    saveDatiRapportoIspezione.fImpiantoPuoFunzionare = null;
            //}
            saveDatiRapportoIspezione.fImpiantoPuoFunzionare = bool.Parse(rblImpiantoPuoFunzionare.SelectedValue);
            if (!string.IsNullOrEmpty(txtDichiarazioniResponsabileImpianto.Text))
            {
                saveDatiRapportoIspezione.DichiarazioniResponsabileImpianto = txtDichiarazioniResponsabileImpianto.Text;
            }
            else
            {
                saveDatiRapportoIspezione.DichiarazioniResponsabileImpianto = null;
            }

            #endregion

            try
            {
                db.SaveChanges();
                UtilityVerifiche.SetFieldsRaccomandazioniPrescrizioni(iDIspezione);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
    
    #region Rapporto di ispezione

    #region Filtri

    public void FiltriAll()
    {
        if (!Page.IsPostBack)
        {
            FilterTipoTrattamentoProduzioneACS();
            FilterTrattamentoRiscaldamento();
            FilterAltroCombustibile();
            FilterFluidoTermoVettore();
            FilterDistribuzione();
            FilterFrequenza();
            FilterTipologiaTermoregolazione();
            FilterConRCTEE();
            FilterTipologiaContabilizzazione();
            FilterDelegatoIspezione();
            FilterOperatoreConduttoreIspezione();
            FilterImpresaManutentriceIspezione();
            FilterTerzoResponsabileIspezione();
            FilterResponsabileIspezione();
            FilterInstallazioneInternaEsterna();
            FilterUltimaManutenzione();
            FilterFrequenzaEfficienzaEnergetica();
            SetAllRaccomandazioniPrescrizioni();
            FiltroPotenzaTermicaFocaloreObblogatori();
            FilterDataUltimoControllo();
            FilterEvacuzioneFumi();

            //18102024 - Tolta la logica di oscuramento in base all'unita immobiliare rowSistemiTermoregolazioneContabilizzazione
            //FilterUnitaImmobiliari();
        }
        FilterObbligatorio();
    }

    public void FiltroPotenzaTermicaFocaloreObblogatori()
    {
        decimal potenzaTermicaFocalore = 0;
        if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleFocalore.Text))
            potenzaTermicaFocalore = decimal.Parse(txtPotenzaTermicaNominaleTotaleFocalore.Text);

        if(potenzaTermicaFocalore > 232)
        {
            rfvOCAbilitazione.Enabled = true;
            rfvOCNominato.Enabled = true;
        }
        else
        {
            rfvOCAbilitazione.Enabled = false;
            rfvOCNominato.Enabled = false;
        }
    }
 
    public void FilterTipoTrattamentoProduzioneACS()
    {
        if (rblTrattamentoProduzioneACS.SelectedIndex == 0)
        {
            rowTipoTrattamentoACS.Visible = true;
        }
        else
        {
            rowTipoTrattamentoACS.Visible = false;
            cblTipoTrattamentoProduzioneACS.ClearSelection();
        }
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione31");
    }

    public void FilterTrattamentoRiscaldamento()
    {
        if (rblTrattamentoRiscaldamento.SelectedIndex == 0)
        {
            rowTipoTrattamentoRiscaldamento.Visible = true;
        }
        else
        {
            rowTipoTrattamentoRiscaldamento.Visible = false;
            cblTipoTrattamentoRiscaldamento.ClearSelection();
        }
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione30");
    }

    public void FilterAltroCombustibile()
    {
        if(cbCombustibile.Value != null)
        {
            if (cbCombustibile.Value.ToString() == "1")
            {
                rowAltroCombustibile.Visible = true;
            }
            else
            {
                rowAltroCombustibile.Visible = false;
                txtAltroCombustibile.Text = "";
            }
        }
        else
        {
            rowAltroCombustibile.Visible = false;
            txtAltroCombustibile.Text = "";
        }
    }

    public void FilterFluidoTermoVettore()
    {
        if (cbFluidoTermoVettore.Value != null)
        {
            if (cbFluidoTermoVettore.Value.ToString() == "1")
            {
                rowAltroFluidoTermoVettore.Visible = true;
            }
            else
            {
                rowAltroFluidoTermoVettore.Visible = false;
                txtAltroFluidoTermoVettore.Text = "";
            }
        }
        else
        {
            rowAltroFluidoTermoVettore.Visible = false;
            txtAltroFluidoTermoVettore.Text = "";
        }
    }

    public void FilterDistribuzione()
    {
        if (cbTipoDistribuzione.Value != null)
        {
            if(cbTipoDistribuzione.Value.ToString() == "1")
            {
                rowAltroDistribuzione.Visible = true;
            }
            else
            {
                rowAltroDistribuzione.Visible = false;
                txtAltroDistribuzione.Text = "";
            }
        }
        else
        {
            rowAltroDistribuzione.Visible = false;
            txtAltroDistribuzione.Text = "";
        }
    }

    public void FilterTipologiaContabilizzazione()
    {
        //if (rblTipologiaContabilizzazione.SelectedIndex == 2)
        //{
        //    rowAltroTipoContabilizzazione.Visible = true;
        //}
        //else
        //{
        //    rowAltroTipoContabilizzazione.Visible = false;
        //    txtAltroTipoContabilizzazione.Text = "";
        //}

        //if (rblTipoContabilizzazioneInvernale.SelectedIndex == 2)
        //{
        //    rowAltroTipoContabilizzazioneInvernale.Visible = true;
        //}
        //else
        //{
        //    rowAltroTipoContabilizzazioneInvernale.Visible = false;
        //    txtAltroTipoContabilizzazioneInvernale.Text = "";
        //}

        //if (rblTipoContabilizzazioneEstiva.SelectedIndex == 2)
        //{
        //    rowAltroTipoContabilizzazioneEstiva.Visible = true;
        //}
        //else
        //{
        //    rowAltroTipoContabilizzazioneEstiva.Visible = false;
        //    txtAltroTipoContabilizzazioneEstiva.Text = "";
        //}

        //if (rblTipoContabilizzazioneACS.SelectedIndex == 2)
        //{
        //    rowAltroTipoContabilizzazioneACS.Visible = true;
        //}
        //else
        //{
        //    rowAltroTipoContabilizzazioneACS.Visible = false;
        //    txtAltroTipoContabilizzazioneACS.Text = "";
        //}
    }

    public void FilterTipologiaTermoregolazione()
    {
        if (cbTipologiaTermoregolazione.Value != null) // Altro Sistema
        {
            if(cbTipologiaTermoregolazione.Value.ToString() == "1")
            {
                rowAltroSistemiTermoregolazione.Visible = true;
            }
            else
            {
                rowAltroSistemiTermoregolazione.Visible = false;
                txtAltriSistemiTermoregolazione.Text = "";
            }         
        }
        else
        {
            rowAltroSistemiTermoregolazione.Visible = false;
            txtAltriSistemiTermoregolazione.Text = "";
        }
    }

    public void FilterOperatoreConduttoreIspezione()
    {
        if (rblOCNominato.SelectedValue == "True")
        {
            rowOCPresente.Visible = true;
            rowOCNome.Visible = true;
            rowOCCognome.Visible = true;
            rowOCCF.Visible = true;
            rowOCAbilitazione.Visible = true;
            rowTipoAbilitazione.Visible = true;
            rowOCNumeroPatentino.Visible = true;
            rowOCDataRilascio.Visible = true;
            if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleFocalore.Text))
            {
                decimal potenza = decimal.Parse(txtPotenzaTermicaNominaleTotaleFocalore.Text);

                if(potenza > 232)
                {
                    rfvOCNominatoPresente.Enabled = true;
                }
                else
                {
                    rfvOCNominatoPresente.Enabled = false;
                }
            }
            else
            {
                rfvOCNominatoPresente.Enabled = false;
            }
        }
        else
        {
            rowOCPresente.Visible = false;
            rowOCNome.Visible = false;
            rowOCCognome.Visible = false;
            rowOCCF.Visible = false;
            rowOCAbilitazione.Visible = false;
            rowTipoAbilitazione.Visible = false;
            rowOCNumeroPatentino.Visible = false;
            rowOCDataRilascio.Visible = false;
            txtOCNome.Text = "";
            txtOCCOgnome.Text = "";
            txtOCCF.Text = "";
            txtOCPatentinoNumero.Text = "";
            //rblOCAbilitato.SelectedValue = null;
            //rblTipoAbilitazione.SelectedValue = null;
            //rblOCNominatoPresente.SelectedValue = null;
            rfvOCNominatoPresente.Enabled = false;
        }
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione0");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione1");
    }

    public void FilterResponsabileIspezione()
    {
        if(rblImpiantoRegistratoCRITER.SelectedValue == "true")
        {
            // TODO
        }
        else
        {
            // TODO
        }
    }

    public void FilterTerzoResponsabileIspezione()
    {
        if (rblfTerzoResponsabile.SelectedValue == "True")
        {
            rowTRespoRagS.Visible = true;
            rowTRespPIVA.Visible = true;
            rowTRespComune.Visible = true;
            rowTRespIndirizzo.Visible = true;
            rowTRespCivico.Visible = true;
            //rowTRespProvincia.Visible = true;
            rowTRespTelefono.Visible = true;
            rowTRespEmail.Visible = true;
            rowTRespEmailPEC.Visible = true;
            rowTRespAbilitazione.Visible = true;
            rowTRespCert.Visible = true;
            rowTRespAttestato.Visible = true;
            rowTRespIncarico.Visible = true;            
        }
        else
        {
            rowTRespoRagS.Visible = false;
            rowTRespPIVA.Visible = false;
            rowTRespComune.Visible = false;
            rowTRespIndirizzo.Visible = false;
            rowTRespCivico.Visible = false;
            //rowTRespProvincia.Visible = false;
            rowTRespTelefono.Visible = false;
            rowTRespEmail.Visible = false;
            rowTRespEmailPEC.Visible = false;
            rowTRespAbilitazione.Visible = false;
            rowTRespCert.Visible = false;
            rowTRespAttestato.Visible = false;
            rowTRespIncarico.Visible = false;
            txtRagioneSocialeTerzoResponsabile.Text = "";
            //txtComuneTerzoResponsabile.Text = "";
            txtPartitaIVATerzoResponsabile.Text = ""; // AGGIUNGERE VERIFICA C.F.
            txtIndirizzoTerzoResponsabile.Text = "";
            txtNumeroCivicoTerzoResponsabile.Text = "";
            //txtProvinciaTerzoResponsabile.Text = "";
            txtTelefonoTerzoResponsabile.Text = "";
            txtEmailTerzoResponsabile.Text = "";
            txtEmailPECTerzoResponsabile.Text = "";

            //rblAbilitazioneTerzoResponsabile.SelectedValue = null;
            //rblCertificazioneTerzoResponsabile.SelectedValue = null;
            //rblAttestatoTerzoResponsabile.SelectedValue = null;
            //rblAttestazioneIncaricoTerzoResponsabile.SelectedValue = null;
        }
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione4");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione5");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione6");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione44");
    }

    public void FilterImpresaManutentriceIspezione()
    {
        if (rblImpresaManutentricePresente.SelectedValue == "True")
        {
            rowImpresaMRagS.Visible = true;
            rowImpresaMPI.Visible = true;
            rowImpresaMComune.Visible = true;
            rowImpresaMIndirizzo.Visible = true;
            rowImpresaMCivico.Visible = true;
            rowImpresaMTelefono.Visible = true;
            rowImpresaMEmail.Visible = true;
            rowImpresaMEmailPEC.Visible = true;
            rowImpresaMAbilitata.Visible = true;
            ImpresaMCertificata.Visible = true;
            rowImpresaMAttestatoSOA.Visible = true;
        }
        else
        {
            rowImpresaMRagS.Visible = false;
            rowImpresaMPI.Visible = false;
            rowImpresaMComune.Visible = false;
            rowImpresaMIndirizzo.Visible = false;
            rowImpresaMCivico.Visible = false;
            rowImpresaMTelefono.Visible = false;
            rowImpresaMEmail.Visible = false;
            rowImpresaMEmailPEC.Visible = false;
            rowImpresaMAbilitata.Visible = false;
            ImpresaMCertificata.Visible = false;
            rowImpresaMAttestatoSOA.Visible = false;
            txtRagioneSocialeImpresaManutentrice.Text = "";
            txtPartitaIVAImpresaManutentrice.Text = ""; // AGGIUNGERE VERIFICA P.I.
            //txtComuneImpresaManutentrice.Text = "";
            txtIndirizzoImpresaManutentrice.Text = "";
            txtNumeroCivicoImpresaManutentrice.Text = "";
            txtTelefonoImpresaManutentrice.Text = "";
            txtEmailImpresaManutentrice.Text = "";
            txtEmailPECImpresaManutentrice.Text = "";
            rblAbilitazioneImpresaManutentrice.SelectedValue = null;
            rblCertificazioneImpresaManutentrice.SelectedValue = null;
            rblAttestatoSOAImpresaManutentrice.SelectedValue = null;
        }
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione7");
    }

    public void FilterInstallazioneInternaEsterna()
    {
        if(cbInstallazioneInternaEsterna.Value != null)
        {
            if (cbInstallazioneInternaEsterna.Value.ToString() == "1") // Interna
            {
                chkGeneratoriIdonei.Value = null; // Esterna
                rowInstallazioneEsterna.Enabled = false;
                rowInstallazioneInterna.Enabled = true;
            }
            else
            {
                chkLocaleInstallazioneIdoneo.Value = null; // Interna
                rowInstallazioneInterna.Enabled = false;
                rowInstallazioneEsterna.Enabled = true;
            }
        }
        else
        {
            chkGeneratoriIdonei.Value = null; // Esterna
            rowInstallazioneEsterna.Enabled = false;
        }
                
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione45");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione46");
    }

    public void FilterUltimaManutenzione()
    {
        if(rblUltimaManutenzionePrevistaEffettuata.SelectedValue == "True")
        {
            rowDataUltimaManutenzione.Visible = true;
        }
        else
        {
            rowDataUltimaManutenzione.Visible = false;
            deDataUltimaManutenzione.Text = "";
        }
    }

    public void FilterDelegatoIspezione()
    {
        if (rblNominatoDelega.SelectedValue == "True")
        {
            rowDelegaNome.Visible = true;
            rowDelegaCognome.Visible = true;
            rowDelegaCF.Visible = true;
            rowDelega.Visible = true;
        }
        else
        {
            rowDelegaNome.Visible = false;
            rowDelegaCognome.Visible = false;
            rowDelegaCF.Visible = false;
            rowDelega.Visible = false;
            txtNomeDelegato.Text = "";
            txtCognomeDelegato.Text = "";
            txtCFDelega.Text = "";
        }
    }
        
    public void FilterFrequenza()
    {
        if(cbFrequenza.Value != null)
        {
            if (cbFrequenza.Value.ToString() == "1")
            {
                rowAltroFrequenza.Visible = true;
            }
            else
            {
                rowAltroFrequenza.Visible = false;
                txtAltroFrequenza.Text = "";
            }
        }
        else
        {
            rowAltroFrequenza.Visible = false;
            txtAltroFrequenza.Text = "";
        }      
    }

    public void FilterFrequenzaEfficienzaEnergetica()
    {
        if(cbFrequenzaEfficienzaEnergetica.Value != null)
        {
            if (cbFrequenzaEfficienzaEnergetica.Value.ToString() == "1")
            {
                rowAltroFrequenzaEfficienzaEnergetica.Visible = true;
            }
            else
            {
                rowAltroFrequenzaEfficienzaEnergetica.Visible = false;
                txtAltroFrequenzaEfficienzaEnergetica.Text = "";
            }
        }
        else
        {
            rowAltroFrequenzaEfficienzaEnergetica.Visible = false;
            txtAltroFrequenzaEfficienzaEnergetica.Text = "";
        }
    }

    public void FilterUnitaImmobiliari()
    {
        if (cbUnitaImmobiliariServite.Value != null)
        {
            if (cbUnitaImmobiliariServite.Value.ToString() == "3")
            {
                rowSistemiTermoregolazioneContabilizzazione0.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione1.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione2.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione3.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione4.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione5.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione6.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione7.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione8.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione9.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione10.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione11.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione12.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione13.Visible = true;
                rowSistemiTermoregolazioneContabilizzazione14.Visible = true;
            }
            else
            {
                rowSistemiTermoregolazioneContabilizzazione0.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione1.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione2.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione3.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione4.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione5.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione6.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione7.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione8.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione9.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione10.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione11.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione12.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione13.Visible = false;
                rowSistemiTermoregolazioneContabilizzazione14.Visible = false;
            }
        }
        else
        {
            rowSistemiTermoregolazioneContabilizzazione0.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione1.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione2.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione3.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione4.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione5.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione6.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione7.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione8.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione9.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione10.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione11.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione12.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione13.Visible = false;
            rowSistemiTermoregolazioneContabilizzazione14.Visible = false;
        }
    }

    #region Custom Validation

    public void ControllaCfDelega(Object sender, ServerValidateEventArgs e)
    {
        if (txtCFDelega.Text != "")
        {
            bool fCodFis = fCodFis = CodiceFiscale.ControlloFormale(txtCFDelega.Text);

            if (!fCodFis)
            {
                e.IsValid = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Codice fiscale : non valido')", true);
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaCfOC(Object sender, ServerValidateEventArgs e)
    {
        if (txtOCCF.Text != "")
        {
            bool fCodFis = fCodFis = CodiceFiscale.ControlloFormale(txtOCCF.Text);

            if (!fCodFis)
            {
                e.IsValid = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Codice fiscale : non valido')", true);
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaCfResponsabile(Object sender, ServerValidateEventArgs e)
    {
        if (txtCFResponsabile.Text != "")
        {
            bool fCodFis = fCodFis = CodiceFiscale.ControlloFormale(txtCFResponsabile.Text);

            if (!fCodFis)
            {
                e.IsValid = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Codice fiscale : non valido')", true);
            }
        }
        else
        {
            e.IsValid = true;
        }
    }
    
    #endregion


    public void FilterDataUltimoControllo()
    {
        if(cbUltimoControlloPrevistoEffettuato.Value != null)
        {
            if (cbUltimoControlloPrevistoEffettuato.Value.ToString() == "3")
            {
                rowUltimoControllo.Visible = false;
                deDataUltimoControllo.Text = "";
            }
            else
            {
                rowUltimoControllo.Visible = true;
            }
        }
        else
        {
            rowUltimoControllo.Visible = false;
            deDataUltimoControllo.Text = "";
        }
    }

    public void FilterConRCTEE()
    {
        if (cbOsservazioniRCTEE.Checked)
        {
            rowOsservazioni.Visible = true;
        }
        else
        {
            rowOsservazioni.Visible = false;
        }
        if (cbRaccomandazioniRCTEE.Checked)
        {
            rowRaccomandazioni.Visible = true;
        }
        else
        {
            rowRaccomandazioni.Visible = false;
        }
        if (cbPrescrizioniRCTEE.Checked)
        {
            rowPrescrizioni.Visible = true;
        }
        else
        {
            rowPrescrizioni.Visible = false;
        }
    }

    public void FilterObbligatorio()
    {
        //if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleUtile.Text))
        //{
        //    decimal PotenzaTermicaNominaleTotaleUtile = decimal.Parse(txtPotenzaTermicaNominaleTotaleUtile.Text);

        //    //default 
        //    rfvCategoria.Enabled = false;
            
        //}

        if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleFocalore.Text))
        {
            decimal PotenzaTermicaNominaleTotaleFocalore = decimal.Parse(txtPotenzaTermicaNominaleTotaleFocalore.Text);

            if(PotenzaTermicaNominaleTotaleFocalore > 232)
            {
                rfvPatentinoOperatoreUltimoControllo.Enabled = true;
            }
            else
            {
                rfvPatentinoOperatoreUltimoControllo.Enabled = false;
            }
        }

        //if(cbUnitaImmobiliariServite.Value != null)
        //{
        //    if(cbUnitaImmobiliariServite.Value.ToString() == "2")
        //    {
        //       //5.i , 5.l obbligatori
        //       //per default NC
        //    }
        //    else
        //    {

        //    }
        //}

        //if (!string.IsNullOrEmpty(txtTiraggioDelCamino.Text))
        //{
        //    decimal TiraggioDelCamino = decimal.Parse(txtTiraggioDelCamino.Text);

        //    if(TiraggioDelCamino > 1 && TiraggioDelCamino < 3)
        //    {
        //        revLTiraggioDelCamino.Enabled = true;
        //    }
        //    else
        //    {
        //        revLTiraggioDelCamino.Enabled = false;
        //    }
        //}
        //else
        //{
        //    revLTiraggioDelCamino.Enabled = false;
        //}

        if (!string.IsNullOrEmpty(txtPotenzaTermicaUtileProgetto.Text))
        {
            rblCorrettoDimensionamentoGeneratore.Enabled = true;
        }
        else
        {
            rblCorrettoDimensionamentoGeneratore.Enabled = false;
            if (!string.IsNullOrEmpty(rblCorrettoDimensionamentoGeneratore.SelectedValue))
                rblCorrettoDimensionamentoGeneratore.SelectedIndex = -1;
        }
    }

    public void FilterEvacuzioneFumi()
    {
        if (rblEvacuazioneNaturale.Checked)
        {
            txtTiraggioDelCamino.CssClass = "txtClass_o";
            rfvtxtTiraggioDelCamino.Enabled = true;
        }
        else
        {
            txtTiraggioDelCamino.CssClass = "txtClass";
            rfvtxtTiraggioDelCamino.Enabled = false;
        }
        //if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleUtile.Text))
        //{
        //    decimal PotenzaTermicaNominaleTotaleUtile = decimal.Parse(txtPotenzaTermicaNominaleTotaleUtile.Text);

        //    //default 
        //    rfvCategoria.Enabled = false;

        //}

        if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleFocalore.Text))
        {
            decimal PotenzaTermicaNominaleTotaleFocalore = decimal.Parse(txtPotenzaTermicaNominaleTotaleFocalore.Text);

            if (PotenzaTermicaNominaleTotaleFocalore > 232)
            {
                rfvPatentinoOperatoreUltimoControllo.Enabled = true;
            }
            else
            {
                rfvPatentinoOperatoreUltimoControllo.Enabled = false;
            }
        }

        //if(cbUnitaImmobiliariServite.Value != null)
        //{
        //    if(cbUnitaImmobiliariServite.Value.ToString() == "2")
        //    {
        //       //5.i , 5.l obbligatori
        //       //per default NC
        //    }
        //    else
        //    {

        //    }
        //}

        //if (!string.IsNullOrEmpty(txtTiraggioDelCamino.Text))
        //{
        //    decimal TiraggioDelCamino = decimal.Parse(txtTiraggioDelCamino.Text);

        //    if(TiraggioDelCamino > 1 && TiraggioDelCamino < 3)
        //    {
        //        revLTiraggioDelCamino.Enabled = true;
        //    }
        //    else
        //    {
        //        revLTiraggioDelCamino.Enabled = false;
        //    }
        //}
        //else
        //{
        //    revLTiraggioDelCamino.Enabled = false;
        //}

        if (!string.IsNullOrEmpty(txtPotenzaTermicaUtileProgetto.Text))
        {
            rblCorrettoDimensionamentoGeneratore.Enabled = true;
        }
        else
        {
            rblCorrettoDimensionamentoGeneratore.Enabled = false;
            if (!string.IsNullOrEmpty(rblCorrettoDimensionamentoGeneratore.SelectedValue))
                rblCorrettoDimensionamentoGeneratore.SelectedIndex = -1;
        }

        //if(deDataDepositoComune.Value != null)
        //{
        //    if(deDataDepositoComune.Date > DateTime.Parse("06/10/2005"))
        //    {
        //        //AQE obbligatorio
        //    }
        //    if (deDataDepositoComune.Date > DateTime.Parse("06/10/2011"))
        //    {
        //        //Perizia obbligatorio
        //    }
        //}

        //if (!string.IsNullOrEmpty(rblCategoriaAnticendio.SelectedValue))
        //{
        //    if (!string.IsNullOrEmpty(deVerbaleSopralluogo.ValidationSettings.ValidationGroup))
        //        deVerbaleSopralluogo.ValidationSettings.ValidationGroup = "";

        //    if (rblCategoriaAnticendio.SelectedValue == "2")
        //    {
        //        rowProgettoAntincendio.Visible = true;
        //    }
        //    else if(rblCategoriaAnticendio.SelectedValue == "3")            
        //    {
        //        rowProgettoAntincendio.Visible = true;
        //        deVerbaleSopralluogo.ValidationSettings.ValidationGroup = "vgIspezioneRapporto";
        //        //verbale di sopralluogo VV.FF e relativa data : Obbligatorio
        //    }
        //    else if(rblCategoriaAnticendio.SelectedValue == "1")
        //    {
        //        rowProgettoAntincendio.Visible = false;
        //        deProgettoAntincendio.Value = null;
        //        chkProggettoAntincendio.Value = null;
        //    }
        //}
        //else
        //{
        //    rowProgettoAntincendio.Visible = false;
        //    deProgettoAntincendio.Value = null;
        //    chkProggettoAntincendio.Value = null;
        //}
    }

    #endregion

    #region LoadDropDownList

    protected void rbAll()
    {
        rbDestinazioneUso(null);
        rbFluidoTermoVettore(null);
        rbTipologiaGruppoTermico(null);
        rbTipologiaResponsabile(null);
        rbTipoDistribuzione(null);
        rbTipologiaContabilizzazione(null);
        rbTipologiaGeneratoriTermici(null);
        comboCombustibile(null);
    }

    protected void rbDestinazioneUso(int? iDPresel)
    {
        rblDestinazioneUso.Items.Clear();

        rblDestinazioneUso.DataValueField = "IDDestinazioneUso";
        rblDestinazioneUso.DataTextField = "DestinazioneUso";
        rblDestinazioneUso.DataSource = LoadDropDownList.LoadDropDownList_SYS_DestinazioneUso(iDPresel);
        rblDestinazioneUso.DataBind();
    }

    protected void rbTipologiaResponsabile(int? idPresel)
    {
        rblTipologiaResponsabile.Items.Clear();
        rblTipologiaResponsabile.DataValueField = "IDTipologiaResponsabile";
        rblTipologiaResponsabile.DataTextField = "TipologiaResponsabile";
        rblTipologiaResponsabile.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaResponsabile(idPresel);
        rblTipologiaResponsabile.DataBind();
    }

    protected void comboCombustibile(int? iDPresel)
    {
        cbCombustibile.Items.Clear();

        cbCombustibile.ValueField = "IDTipologiaCombustibile";
        cbCombustibile.TextField = "TipologiaCombustibile";
        cbCombustibile.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaCombustibile(iDPresel);
        cbCombustibile.DataBind();
    }

    protected void rbFluidoTermoVettore(int? iDPresel)
    {
        cbFluidoTermoVettore.Items.Clear();
        cbFluidoTermoVettore.SelectedIndex = -1;
        cbFluidoTermoVettore.ValueField = "IDTipologiaFluidoTermoVettore";
        cbFluidoTermoVettore.TextField = "TipologiaFluidoTermoVettore";
        cbFluidoTermoVettore.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaFluidoTermoVettore(iDPresel);
        cbFluidoTermoVettore.DataBind();
    }

    protected void rbTipoDistribuzione(int? iDPresel)
    {
        cbTipoDistribuzione.Items.Clear();
        cbTipoDistribuzione.SelectedIndex = -1;
        cbTipoDistribuzione.ValueField = "IDTipologiaSistemaDistribuzione";
        cbTipoDistribuzione.TextField = "TipologiaSistemaDistribuzione";
        cbTipoDistribuzione.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaSistemaDistribuzione(iDPresel);
        cbTipoDistribuzione.DataBind();
    }

    protected void rbTipologiaGruppoTermico(int? iDPresel)
    {
        cbTipologiaGruppoTermico.Items.Clear();
        cbTipologiaGruppoTermico.SelectedIndex = -1;
        cbTipologiaGruppoTermico.ValueField = "IDTipologiaGruppiTermici";
        cbTipologiaGruppoTermico.TextField = "TipologiaGruppiTermici";
        cbTipologiaGruppoTermico.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaGruppiTermici(iDPresel);
        cbTipologiaGruppoTermico.DataBind();
    }

    protected void rbTipologiaContabilizzazione(int? iDPresel)
    {
        cbTipologiaContabilizzazione.Items.Clear();
        cbTipologiaContabilizzazione.SelectedIndex = -1;
        cbTipologiaContabilizzazione.ValueField = "IDTipologiaContabilizzazione";
        cbTipologiaContabilizzazione.TextField = "TipologiaContabilizzazione";
        cbTipologiaContabilizzazione.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaContabilizzazione(iDPresel);
        cbTipologiaContabilizzazione.DataBind();
    }

    protected void cbTipologiaTrattamentoAcqua(int? iDPresel, CheckBoxList cbTrattamentoAcqua)
    {
        cbTrattamentoAcqua.Items.Clear();

        cbTrattamentoAcqua.DataValueField = "IDTipologiaTrattamentoAcqua";
        cbTrattamentoAcqua.DataTextField = "TipologiaTrattamentoAcqua";
        cbTrattamentoAcqua.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaTrattamentoAcqua(iDPresel);
        cbTrattamentoAcqua.DataBind();
    }

    protected void cbTipologiaCheckList(int? iDPresel, int? iDTipoCheckList, CheckBoxList cblCheckList)
    {
        cblCheckList.Items.Clear();

        cblCheckList.DataValueField = "IDTipologiaCheckList";
        cblCheckList.DataTextField = "TipologiaCheckList";
        cblCheckList.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaIspezioneRapportoCheckList(iDPresel, iDTipoCheckList);
        cblCheckList.DataBind();
    }

    protected void rbTipologiaGeneratoriTermici(int? iDPresel)
    {
        cbClassificazioneDPR66096.Items.Clear();
        cbClassificazioneDPR66096.SelectedIndex = -1;
        cbClassificazioneDPR66096.ValueField = "IdTipologiaGeneratoriTermici";
        cbClassificazioneDPR66096.TextField = "Descrizione";
        cbClassificazioneDPR66096.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaGeneratoriTermici(iDPresel);
        cbClassificazioneDPR66096.DataBind();
    }

    #endregion

    #region DATI CATASTALI LIBRETTO
    public void GetDatiCatastali(string iDLibrettoImpianto)
    {
        dgDatiCatastali.DataSource = UtilityLibrettiImpianti.GetValoriDatiCatastali(int.Parse(iDLibrettoImpianto));
        dgDatiCatastali.DataBind();

        if (dgDatiCatastali.Columns.IsEmpty)
        {
            pnlDatiCatastaliView.Visible = false;
        }
        else
        {
            pnlDatiCatastaliView.Visible = true;
        }
    }

    #endregion

    #region TRATTAMENTO ACQUA

    public void UpdateTipologiaTrattamentoAcquaInvernale(int iDIspezione)
    {
        List<string> valoriTrattamentoAcqua = new List<string>();
        foreach (ListItem item in cblTipoTrattamentoRiscaldamento.Items)
        {
            if (item.Selected)
            {
                valoriTrattamentoAcqua.Add(item.Value);
            }
        }
        UtilityVerifiche.SaveInsertDeleteDatiTrattamentoAcquaInvernale(iDIspezione, valoriTrattamentoAcqua.ToArray<string>());
    }

    public void UpdateTipologiaTrattamentoAcquaAcs(int iDIspezione)
    {
        List<string> valoriTrattamentoAcqua = new List<string>();
        foreach (ListItem item in cblTipoTrattamentoProduzioneACS.Items)
        {
            if (item.Selected)
            {
                valoriTrattamentoAcqua.Add(item.Value);
            }
        }
        UtilityVerifiche.SaveInsertDeleteDatiTrattamentoAcquaAcs(iDIspezione, valoriTrattamentoAcqua.ToArray<string>());
    }

    public void GetDatiIspezioneRapportoTrattamentoAcquaInvernale(int iDIspezione)
    {
        cbTipologiaTrattamentoAcqua(null, cblTipoTrattamentoRiscaldamento);
        var result = UtilityVerifiche.GetValoriIspezioneRapportoTrattamentoAcquaInvernale(iDIspezione);

        foreach (var row in result)
        {
            cblTipoTrattamentoRiscaldamento.Items.FindByValue(row.IDTipologiaTrattamentoAcqua.ToString()).Selected = true;
        }
    }

    public void GetDatiIspezioneRapportoTrattamentoAcquaAcs(int iDIspezione)
    {
        cbTipologiaTrattamentoAcqua(null, cblTipoTrattamentoProduzioneACS);
        var result = UtilityVerifiche.GetValoriIspezioneRapportoTrattamentoAcquaAcs(iDIspezione);

        foreach (var row in result)
        {
            cblTipoTrattamentoProduzioneACS.Items.FindByValue(row.IDTipologiaTrattamentoAcqua.ToString()).Selected = true;
        }
    }

    #endregion

    #region DATI STRUMENTAZIONE UTILIZZATA

    public void GetDatiStrumentazioneUtilizzata(string iDIspezione)
    {
        DataGridStrumentazioneUtilizzata.DataSource = UtilityVerifiche.GetValoriDatiStrumento(int.Parse(iDIspezione));
        DataGridStrumentazioneUtilizzata.DataBind();

        if (DataGridStrumentazioneUtilizzata.Columns.IsEmpty)
        {
            pnlStrumentazioneUtilizzataView.Visible = false;
        }
        else
        {
            pnlStrumentazioneUtilizzataView.Visible = true;
        }
    }

    public void RowCommandStrumento(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDIspezioneStrumenti = int.Parse(e.CommandArgument.ToString());
                var datiStrumenti = new VER_IspezioneRapportoStrumenti();
                datiStrumenti = ctx.VER_IspezioneRapportoStrumenti.FirstOrDefault(c => c.IDIspezioneStrumenti == iDIspezioneStrumenti);

                ctx.VER_IspezioneRapportoStrumenti.Remove(datiStrumenti);
                ctx.SaveChanges();
            }
            GetDatiStrumentazioneUtilizzata(IDIspezione);
        }
        if (e.CommandName == "Update")
        {
            btnSaveDatiStrumentazioneUtilizzata.Text = "salva";
            VisibleDatiStrumento(false);
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int IDIspezioneDatiStrumentiInt = int.Parse(e.CommandArgument.ToString());
                var datiStrumenti = new VER_IspezioneRapportoStrumenti();
                datiStrumenti = ctx.VER_IspezioneRapportoStrumenti.FirstOrDefault(c => c.IDIspezioneStrumenti == IDIspezioneDatiStrumentiInt);

                lblIDIspezioneRapportoDatiStrumenti.Text = IDIspezioneDatiStrumentiInt.ToString();

                if (!string.IsNullOrEmpty(datiStrumenti.Tipologia))
                {
                    txtTipologiaUtilizzata.Text = datiStrumenti.Tipologia;
                }
                if (!string.IsNullOrEmpty(datiStrumenti.Matricola))
                {
                    txtMatricolaUtilizzata.Text = datiStrumenti.Matricola;
                }
                if (!string.IsNullOrEmpty(datiStrumenti.TipologiaMisurazione))
                {
                    txtTipologiaMisurazione.Text = datiStrumenti.TipologiaMisurazione;
                }
                if (!string.IsNullOrEmpty(datiStrumenti.CertificatoTaratura))
                {
                    txtCertificatoTaratura.Text = datiStrumenti.CertificatoTaratura;
                }
                if (!string.IsNullOrEmpty(datiStrumenti.DataScadenzaCertificato.ToString()))
                {
                    txtDataScadenzaCertificato.Text = string.Format("{0:dd/MM/yyyy}", datiStrumenti.DataScadenzaCertificato);
                }
            }
            VisibleDatiStrumento(true);
        }
    }

    protected void VisibleDatiStrumento(bool fVisible)
    {
        if (fVisible)
        {
            pnlDatiStrumentiInsert.Visible = true;
            lnkInsertDatiStrumenti.Visible = false;
            pnlStrumentazioneUtilizzataView.Visible = false;
        }
        else
        {
            pnlDatiStrumentiInsert.Visible = false;
            lnkInsertDatiStrumenti.Visible = true;
            pnlStrumentazioneUtilizzataView.Visible = true;
        }
    }

    protected void lnkInsertDatiStrumenti_Click(object sender, EventArgs e)
    {
        VisibleDatiStrumento(true);
        ResetDatiStrumento();
        btnSaveDatiStrumentazioneUtilizzata.Text = "inserisci";
    }

    protected void ResetDatiStrumento()
    {
        txtMatricolaUtilizzata.Text = "";
        txtTipologiaMisurazione.Text = "";
        txtCertificatoTaratura.Text = "";
        txtDataScadenzaCertificato.Text = "";
        lblIDIspezioneRapportoDatiStrumenti.Text = "";
    }

    protected void SaveDatiStrumenti(string iDIspezioneStrumenti)
    {
        if (string.IsNullOrEmpty(iDIspezioneStrumenti))
        {
            using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var datiStrumento = new VER_IspezioneRapportoStrumenti();
                        datiStrumento.IDIspezione = long.Parse(IDIspezione);

                        if (!string.IsNullOrEmpty(txtTipologiaUtilizzata.Text))
                        {
                            datiStrumento.Tipologia = txtTipologiaUtilizzata.Text;
                        }
                        if (!string.IsNullOrEmpty(txtMatricolaUtilizzata.Text))
                        {
                            datiStrumento.Matricola = txtMatricolaUtilizzata.Text;
                        }
                        if (!string.IsNullOrEmpty(txtTipologiaMisurazione.Text))
                        {
                            datiStrumento.TipologiaMisurazione = txtTipologiaMisurazione.Text;
                        }
                        if (!string.IsNullOrEmpty(txtCertificatoTaratura.Text))
                        {
                            datiStrumento.CertificatoTaratura = txtCertificatoTaratura.Text;
                        }
                        if (!string.IsNullOrEmpty(txtDataScadenzaCertificato.Text))
                        {
                            datiStrumento.DataScadenzaCertificato = DateTime.Parse(txtDataScadenzaCertificato.Text);
                        }

                        ctx.VER_IspezioneRapportoStrumenti.Add(datiStrumento);
                        ctx.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }
        else
        {
            using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        int iDIspezioneDatiStrumentoInt = int.Parse(iDIspezioneStrumenti);
                        var datiStrumento = new VER_IspezioneRapportoStrumenti();
                        datiStrumento = ctx.VER_IspezioneRapportoStrumenti.FirstOrDefault(c => c.IDIspezioneStrumenti == iDIspezioneDatiStrumentoInt);
                        datiStrumento.IDIspezione = long.Parse(IDIspezione);

                        if (!string.IsNullOrEmpty(txtTipologiaUtilizzata.Text))
                        {
                            datiStrumento.Tipologia = txtTipologiaUtilizzata.Text;
                        }
                        if (!string.IsNullOrEmpty(txtMatricolaUtilizzata.Text))
                        {
                            datiStrumento.Matricola = txtMatricolaUtilizzata.Text;
                        }
                        if (!string.IsNullOrEmpty(txtTipologiaMisurazione.Text))
                        {
                            datiStrumento.TipologiaMisurazione = txtTipologiaMisurazione.Text;
                        }
                        if (!string.IsNullOrEmpty(txtCertificatoTaratura.Text))
                        {
                            datiStrumento.CertificatoTaratura = txtCertificatoTaratura.Text;
                        }
                        if (!string.IsNullOrEmpty(txtDataScadenzaCertificato.Text))
                        {
                            datiStrumento.DataScadenzaCertificato = DateTime.Parse(txtDataScadenzaCertificato.Text);
                        }

                        ctx.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }
        btnSaveDatiStrumentazioneUtilizzata.Text = "inserisci";
    }

    protected void btnSaveDatiStrumentazioneUtilizzata_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SaveDatiStrumenti(lblIDIspezioneRapportoDatiStrumenti.Text);
            ResetDatiStrumento();
            VisibleDatiStrumento(false);
            GetDatiStrumentazioneUtilizzata(IDIspezione);
        }
    }

    protected void btnAnnullaDatiStrumentazioneUtilizzata_Click(object sender, EventArgs e)
    {
        VisibleDatiStrumento(false);
        GetDatiStrumentazioneUtilizzata(IDIspezione);
    }


    #endregion

    #region DATI PROGETTO

    //public void GetDatiProgetto(string iDIspezione)
    //{
    //    DataGridDatiProgetto.DataSource = UtilityVerifiche.GetValoriDatiProgetto(int.Parse(iDIspezione));
    //    DataGridDatiProgetto.DataBind();

    //    if (DataGridDatiProgetto.Columns.IsEmpty)
    //    {
    //        pnlDatiProgettoView.Visible = false;
    //    }
    //    else
    //    {
    //        pnlDatiProgettoView.Visible = true;
    //    }
    //}

    //public void RowCommandProgetto(object sender, CommandEventArgs e)
    //{
    //    if (e.CommandName == "Delete")
    //    {
    //        using (CriterDataModel ctx = new CriterDataModel())
    //        {
    //            int iDIspezioneProgetto = int.Parse(e.CommandArgument.ToString());
    //            var datiProgetto = new VER_IspezioneRapportoProgetti();
    //            datiProgetto = ctx.VER_IspezioneRapportoProgetti.FirstOrDefault(c => c.IDIspezioneProgetto == iDIspezioneProgetto);

    //            ctx.VER_IspezioneRapportoProgetti.Remove(datiProgetto);
    //            ctx.SaveChanges();
    //        }

    //        GetDatiProgetto(IDIspezione);
    //    }

    //    if (e.CommandName == "Update")
    //    {
    //        btnSaveDatiProgetto.Text = "salva";
    //        VisibleDatiProgetto(false);
    //        using (CriterDataModel ctx = new CriterDataModel())
    //        {
    //            int IDIspezioneDatiProgettoiInt = int.Parse(e.CommandArgument.ToString());
    //            var datiProgetto = new VER_IspezioneRapportoProgetti();
    //            datiProgetto = ctx.VER_IspezioneRapportoProgetti.FirstOrDefault(c => c.IDIspezioneProgetto == IDIspezioneDatiProgettoiInt);

    //            lblIDIspezioneProgetto.Text = IDIspezioneDatiProgettoiInt.ToString();
    //            if (!string.IsNullOrEmpty(datiProgetto.Progettista))
    //            {
    //                txtProgettista.Text = datiProgetto.Progettista;
    //            }
    //            if (!string.IsNullOrEmpty(datiProgetto.NumeroProgetto.ToString()))
    //            {
    //                txtNumeroProgetto.Text = datiProgetto.NumeroProgetto.ToString();
    //            }
    //            if (!string.IsNullOrEmpty(datiProgetto.Anno))
    //            {
    //                txtAnnoProgetto.Text = datiProgetto.Anno;
    //            }
    //            if (!string.IsNullOrEmpty(datiProgetto.PotenzaTermicaUtilePrevista.ToString()))
    //            {
    //                txtPotenzaProgetto.Text = datiProgetto.PotenzaTermicaUtilePrevista.ToString();
    //            }
    //        }
    //        VisibleDatiProgetto(true);
    //    }
    //}

    //protected void VisibleDatiProgetto(bool fVisible)
    //{
    //    if (fVisible)
    //    {
    //        pnlDatiProggetoInsert.Visible = true;
    //        lnkInsertDatiProgetto.Visible = false;
    //        pnlDatiProgettoView.Visible = false;
    //    }
    //    else
    //    {
    //        pnlDatiProggetoInsert.Visible = false;
    //        lnkInsertDatiProgetto.Visible = true;
    //        pnlDatiProgettoView.Visible = true;
    //    }
    //}

    //protected void lnkInsertDatiProgetto_Click(object sender, EventArgs e)
    //{
    //    btnSaveDatiProgetto.Text = "inserisci";
    //    VisibleDatiProgetto(true);
    //    ResetDatiProgetto();
    //}

    //protected void ResetDatiProgetto()
    //{
    //    txtProgettista.Text = "";
    //    txtNumeroProgetto.Text = "";
    //    txtAnnoProgetto.Text = "";
    //    txtPotenzaProgetto.Text = "";
    //    lblIDIspezioneProgetto.Text = "";
    //}

    //protected void SaveDatiProgetto(string iDIspezioneProgetto)
    //{
    //    if (string.IsNullOrEmpty(iDIspezioneProgetto))
    //    {
    //        using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
    //        {
    //            using (var dbContextTransaction = ctx.Database.BeginTransaction())
    //            {
    //                try
    //                {
    //                    var datiProgetto = new VER_IspezioneRapportoProgetti();
    //                    datiProgetto.IDIspezione = long.Parse(IDIspezione);

    //                    if (!string.IsNullOrEmpty(txtProgettista.Text))
    //                    {
    //                        datiProgetto.Progettista = txtProgettista.Text;
    //                    }
    //                    if (!string.IsNullOrEmpty(txtNumeroProgetto.Text))
    //                    {
    //                        datiProgetto.NumeroProgetto = int.Parse(txtNumeroProgetto.Text);
    //                    }
    //                    if (!string.IsNullOrEmpty(txtAnnoProgetto.Text))
    //                    {
    //                        datiProgetto.Anno = txtAnnoProgetto.Text;
    //                    }
    //                    if (!string.IsNullOrEmpty(txtPotenzaProgetto.Text))
    //                    {
    //                        datiProgetto.PotenzaTermicaUtilePrevista = decimal.Parse(txtPotenzaProgetto.Text);
    //                    }

    //                    ctx.VER_IspezioneRapportoProgetti.Add(datiProgetto);
    //                    ctx.SaveChanges();

    //                    dbContextTransaction.Commit();
    //                }
    //                catch (Exception)
    //                {
    //                    dbContextTransaction.Rollback();
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
    //        {
    //            using (var dbContextTransaction = ctx.Database.BeginTransaction())
    //            {
    //                try
    //                {
    //                    int iDIspezioneDatiProgettoInt = int.Parse(iDIspezioneProgetto);
    //                    var datiProgetto = new VER_IspezioneRapportoProgetti();
    //                    datiProgetto = ctx.VER_IspezioneRapportoProgetti.FirstOrDefault(c => c.IDIspezioneProgetto == iDIspezioneDatiProgettoInt);
    //                    datiProgetto.IDIspezione = long.Parse(IDIspezione);

    //                    if (!string.IsNullOrEmpty(txtProgettista.Text))
    //                    {
    //                        datiProgetto.Progettista = txtProgettista.Text;
    //                    }
    //                    if (!string.IsNullOrEmpty(txtNumeroProgetto.Text))
    //                    {
    //                        datiProgetto.NumeroProgetto = int.Parse(txtNumeroProgetto.Text);
    //                    }
    //                    if (!string.IsNullOrEmpty(txtAnnoProgetto.Text))
    //                    {
    //                        datiProgetto.Anno = txtAnnoProgetto.Text;
    //                    }
    //                    if (!string.IsNullOrEmpty(txtPotenzaProgetto.Text))
    //                    {
    //                        datiProgetto.PotenzaTermicaUtilePrevista = decimal.Parse(txtPotenzaProgetto.Text);
    //                    }

    //                    ctx.SaveChanges();

    //                    dbContextTransaction.Commit();
    //                }
    //                catch (Exception)
    //                {
    //                    dbContextTransaction.Rollback();
    //                }
    //            }
    //        }
    //    }
    //}

    //protected void btnSaveDatiProgetto_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        SaveDatiProgetto(lblIDIspezioneProgetto.Text);
    //        ResetDatiProgetto();
    //        VisibleDatiProgetto(false);
    //        GetDatiProgetto(IDIspezione);
    //        btnSaveDatiProgetto.Text = "inserisci";
    //    }
    //}

    //protected void btnAnnullaDatiProgetto_Click(object sender, EventArgs e)
    //{
    //    VisibleDatiProgetto(false);
    //    GetDatiProgetto(IDIspezione);
    //}

    #endregion

    #region DATI CHECK LIST

    public void GetDatiIspezioneRapportoCheckList(int iDIspezione, int iDTipoCheckList, CheckBoxList checkList)
    {
        cbTipologiaCheckList(null, iDTipoCheckList, checkList);
        var result = UtilityVerifiche.GetValoriIspezioneRapportoCheckList(iDIspezione, iDTipoCheckList);

        foreach (var row in result)
        {
            checkList.Items.FindByValue(row.IDTipologiaCheckList.ToString()).Selected = true;
        }
    }

    public void UpdateCheckList(int iDIspezione, int iDTipoCheckList, CheckBoxList checkList)
    {
        List<string> valoriCheckList = new List<string>();
        foreach (ListItem item in checkList.Items)
        {
            if (item.Selected)
            {
                valoriCheckList.Add(item.Value);
            }
        }
        UtilityVerifiche.SaveInsertDeleteDatiCheckList(iDIspezione, iDTipoCheckList, valoriCheckList.ToArray<string>());
    }
    #endregion

    #region CODICE CATASTALE / DATI CATASTALI (COMUNI)

    // RESPONSABILE
    protected void cbComuneResponsabile_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void cbComuneResponsabile_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void cbComuneResponsabile_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshcbComuneResponsabile();
    }

    protected void RefreshcbComuneResponsabile()
    {
        cbComuneResponsabile.SelectedIndex = -1;
        cbComuneResponsabile.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        cbComuneResponsabile.DataBind();
    }

    // TERZO RESPONSABILE

    protected void cbComuneTerzoResponsabile_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void cbComuneTerzoResponsabile_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void cbComuneTerzoResponsabile_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshcbComuneTerzoResponsabile();
    }

    protected void RefreshcbComuneTerzoResponsabile()
    {
        cbComuneTerzoResponsabile.SelectedIndex = -1;
        cbComuneTerzoResponsabile.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        cbComuneTerzoResponsabile.DataBind();
    }

    // IMPRESA MANUTENTRICE

    protected void cbComuneImpresaManutentrice_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void cbComuneImpresaManutentrice_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void cbComuneImpresaManutentrice_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshcbComuneImpresaManutentrice();
    }

    protected void RefreshcbComuneImpresaManutentrice()
    {
        cbComuneImpresaManutentrice.SelectedIndex = -1;
        cbComuneImpresaManutentrice.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        cbComuneImpresaManutentrice.DataBind();
    }

    // NATO - OPERATORE ULTIMO CONTROLLO

    protected void cbNatoOperatoreUltimoControllo_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void cbNatoOperatoreUltimoControllo_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void cbNatoOperatoreUltimoControllo_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshcbNatoOperatoreUltimoControllo();
    }

    protected void RefreshcbNatoOperatoreUltimoControllo()
    {
        cbNatoOperatoreUltimoControllo.SelectedIndex = -1;
        cbNatoOperatoreUltimoControllo.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        cbNatoOperatoreUltimoControllo.DataBind();
    }

    // COMUNE - OPERATORE ULTIMO CONTROLLO
    protected void cbComuneOperatoreUltimoControllo_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void cbComuneOperatoreUltimoControllo_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void cbComuneOperatoreUltimoControllo_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshcbComuneOperatoreUltimoControllo();
    }

    protected void RefreshcbComuneOperatoreUltimoControllo()
    {
        cbComuneOperatoreUltimoControllo.SelectedIndex = -1;
        cbComuneOperatoreUltimoControllo.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        cbComuneOperatoreUltimoControllo.DataBind();
    }

    #endregion

    #region CONTROLLO RACCOMANDAZIONI / PRESCRIZIONI ISPEZIONE

    public void SetAllRaccomandazioniPrescrizioni()
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione0");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione1");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione2");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione3");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione4");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione5");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione6");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione7");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione8");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione9");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione10");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione11");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione12");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione13");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione14");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione15");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione16");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione17");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione18");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione19");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione20");
        //SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione21");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione22");
        //SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione23");
        //SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione24");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione25");
        //SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione26");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione27");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione28");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione29");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione30");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione31");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione32");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione33");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione34");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione35");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione36");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione37");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione38");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione39");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione40");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione41");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione42");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione43");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione44");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione45");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione46");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione47");
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione48");
    }

    public void SetRaccomandazioniPrescrizioni(string idUserControl)
    {
        // Carico Valori per i filtri
        decimal potenzaTermicaFocalore = 0;
        if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleFocalore.Text))
            potenzaTermicaFocalore = decimal.Parse(txtPotenzaTermicaNominaleTotaleFocalore.Text);

        decimal provaStrumentalePA = 0;
        if (!string.IsNullOrEmpty(txtTiraggioDelCamino.Text))
            provaStrumentalePA = decimal.Parse(txtTiraggioDelCamino.Text);

        DateTime InizioRaccPres = DateTime.Parse("01/01/2020");
        bool inizioRaccPres = false;
        if (DateTime.Now >= InizioRaccPres)
            inizioRaccPres = true;

        // Filtri
        switch (idUserControl)
        {
            case "UCRaccomandazioniPrescrizioniIspezione0": // Operatore/Conduttore - Nominato               
                if (rblOCNominato.SelectedValue == "False" && potenzaTermicaFocalore > 232)
                {
                    UCRaccomandazioniPrescrizioniIspezione0.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione0.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione0.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione0.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione1": // Operatore/Conduttore - Abilitato
                if(rblOCAbilitato.SelectedValue == "False" && potenzaTermicaFocalore > 232 && rblOCNominato.SelectedValue == "True")
                {
                    UCRaccomandazioniPrescrizioniIspezione1.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione1.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione1.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione1.fActive = "0";
                }    
                break;
            case "UCRaccomandazioniPrescrizioniIspezione2":
                if (inizioRaccPres && lblPOD.Text == "00000000000")
                {
                    UCRaccomandazioniPrescrizioniIspezione2.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione2.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione2.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione2.fActive = "0";
                }            
                break;
            case "UCRaccomandazioniPrescrizioniIspezione48":
                if (inizioRaccPres && lblPDR.Text == "00000000000")
                {
                    UCRaccomandazioniPrescrizioniIspezione48.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione48.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione48.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione48.fActive = "0";
                }
                break;

            case "UCRaccomandazioniPrescrizioniIspezione3":
                if (inizioRaccPres && rblImpiantoRegistratoCRITER.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione3.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione3.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione3.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione3.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione4": // Terzo Responsabile - Abilitato
                if(rblfTerzoResponsabile.SelectedValue == "True" && rblAbilitazioneTerzoResponsabile.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione4.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione4.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione4.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione4.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione5": // Terzo Responsabile - Certificato ISO 9001
                if (rblfTerzoResponsabile.SelectedValue == "True" && rblCertificazioneTerzoResponsabile.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione5.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione5.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione5.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione5.fActive = "0";
                }                   
                break;
            case "UCRaccomandazioniPrescrizioniIspezione6": // Terzo Resposnabile - Attestato SOA (OG-011 o OS-28)
                if (rblfTerzoResponsabile.SelectedValue == "True" && rblAttestatoTerzoResponsabile.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione6.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione6.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione6.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione6.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione44": // Terzo Responsabile - Attestatazione accettazione incarico
                if (rblfTerzoResponsabile.SelectedValue == "True" && rblAttestazioneIncaricoTerzoResponsabile.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione44.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione44.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione44.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione44.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione7": // Impresa Manutentrice - Abilitazione lett.c ed e DM.37/08
                if(rblImpresaManutentricePresente.SelectedValue == "True" && rblAbilitazioneImpresaManutentrice.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione7.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione7.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione7.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione7.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione8": // Operatore Ultimo Controllo - Patentino conduzioni impianti termici
                if(rblPatentinoOperatoreUltimoControllo.SelectedValue == "False" && potenzaTermicaFocalore > 232)
                {
                    UCRaccomandazioniPrescrizioniIspezione8.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione8.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione8.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione8.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione45": // Per Installazione interna: locale idoneo
                if (chkLocaleInstallazioneIdoneo.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione45.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione45.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione45.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione45.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione46": // Per Installazione esterna: generatori idonei
                if (chkGeneratoriIdonei.Value == EnumStatoSiNoNc.No && rowInstallazioneEsterna.Enabled)
                {
                    UCRaccomandazioniPrescrizioniIspezione46.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione46.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione46.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione46.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione9": // Aperture di ventilazione/aerazione libere da ostruzioni
                if(chkApertureLibere.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione9.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione9.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione9.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione9.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione10": // Dichiarazione di conformità / rispondenza presente
                if(rblDichiarazione.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione10.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione10.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione10.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione10.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione11": // Aperture di ventilazione / aerazione di adeguate dimensioni
                if (chkDimensioniApertureAdeguate.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione11.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione11.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione11.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione11.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione12": // Canale da fumo o condotti di scarico idonei (esame visivo)
                if(chkScarichiIdonei.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione12.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione12.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione12.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione12.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione13": // Assenza perdita combustibile liquido (esame visivo)
                if(chkAssenzaPerditeCombustibile.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione13.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione13.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione13.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione13.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione14": // Idonea tenuta impianto gas combustibile e raccordi con il generatore (UNI 11137)
                if(chkTenutaImpianto.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione14.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione14.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione14.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione14.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione15": // Tiraggio del camino: prova strumentale diretta valore in Pa  (UNI 10845)              
                if(provaStrumentalePA < 1)
                {
                    UCRaccomandazioniPrescrizioniIspezione15.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione15.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione15.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione15.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione16": // Omologazione o verifiche per. DM 01/12/1975
                if (chkOmologazioneVerifiche.Value == EnumStatoSiNoNc.No) // NO
                {
                    UCRaccomandazioniPrescrizioniIspezione16.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione16.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione16.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione16.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione17": // Libretto di impianto presente
                if (rblPresenteLibretto.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione17.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione17.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione17.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione17.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione18": // Libretto uso/manutenzione presente
                if(rblLibrettoUsoManutenzione.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione18.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione18.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione18.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione18.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione19": // Libretto uso/manutenzione compilato correttamente in ogni parte
                if(rblLibrettoCompilatoInTutteLeParte.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione19.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione19.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione19.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione19.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione20": // Rispetto valori normativa vigente 
                if (chkRispettoValoriVigente.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione20.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione20.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione20.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione20.fActive = "0";
                }
                break;
            //case "UCRaccomandazioniPrescrizioniIspezione21": // Progetto antincendio e data di protocollo comando VV.FF:
            //    if (chkProggettoAntincendio.Value == EnumStatoSiNoNc.No)
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione21.Visible = true;
            //        UCRaccomandazioniPrescrizioniIspezione21.fActive = "1";
            //    }
            //    else
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione21.Visible = false;
            //        UCRaccomandazioniPrescrizioniIspezione21.fActive = "0";
            //    }
            //    break;
            case "UCRaccomandazioniPrescrizioniIspezione22": // SCIA  antincendio e data di protocollo comando VV.FF:
                if (chkSCIAAntincendio.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione22.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione22.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione22.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione22.fActive = "0";
                }
                break;
            //case "UCRaccomandazioniPrescrizioniIspezione23": // Verbale di sopralluogo VV.FF e relativa data
            //    if (chkVerbaleSopralluogo.Value == EnumStatoSiNoNc.No)
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione23.Visible = true;
            //        UCRaccomandazioniPrescrizioniIspezione23.fActive = "1";
            //    }
            //    else
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione23.Visible = false;
            //        UCRaccomandazioniPrescrizioniIspezione23.fActive = "0";
            //    }
            //    break;
            //case "UCRaccomandazioniPrescrizioniIspezione24": // Ultimo rinnovo periodico e relativa data               
            //    if (chkUltimoRinnovoPeriodico.Value == EnumStatoSiNoNc.No)
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione24.Visible = true;
            //        UCRaccomandazioniPrescrizioniIspezione24.fActive = "1";
            //    }
            //    else
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione24.Visible = false;
            //        UCRaccomandazioniPrescrizioniIspezione24.fActive = "0";
            //    }
            //    break;
            case "UCRaccomandazioniPrescrizioniIspezione25": // Progetto energetico dell'impianto presente
                if (rblPresenteProgettoImpianto.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione25.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione25.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione25.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione25.fActive = "0";
                }
                break;
            //case "UCRaccomandazioniPrescrizioniIspezione26": // AQE - attestato qualificazione energetica
            //    if (chkAQE.Value == EnumStatoSiNoNc.No)
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione26.Visible = true;
            //        UCRaccomandazioniPrescrizioniIspezione26.fActive = "1";
            //    }
            //    else
            //    {
            //        UCRaccomandazioniPrescrizioniIspezione26.Visible = false;
            //        UCRaccomandazioniPrescrizioniIspezione26.fActive = "0";
            //    }
            //    break;
            case "UCRaccomandazioniPrescrizioniIspezione27": // Diagnosi energetica per per sostituzione generatori con P > 100 kW e/o disconnessione da impianto centralizzato
                if (chkDiagnosiEnergetica.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione27.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione27.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione27.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione27.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione28": // Perizia di non aggravio di costi e/o sbilanciamento in caso di distacco dall’impianto centralizzato
                if (chkPerizia.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione28.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione28.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione28.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione28.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione29": // Corretto dimensionamento del generatore in riferimento al progetto
                if (rblCorrettoDimensionamentoGeneratore.SelectedValue == "0")
                {
                    UCRaccomandazioniPrescrizioniIspezione29.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione29.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione29.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione29.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione30": // Trattamento acqua in riscaldamento
                if (rblTrattamentoRiscaldamento.SelectedValue == "0")
                {
                    UCRaccomandazioniPrescrizioniIspezione30.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione30.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione30.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione30.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione31": // Trattamento acqua in produzione ACS
                if (rblTrattamentoProduzioneACS.SelectedValue == "0")
                {
                    UCRaccomandazioniPrescrizioniIspezione31.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione31.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione31.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione31.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione32": // Ultimo controllo obbligatorio effettuato
                if (cbUltimoControlloPrevistoEffettuato.Value != null)
                {
                    if (cbUltimoControlloPrevistoEffettuato.Value.ToString() == "2" || cbUltimoControlloPrevistoEffettuato.Value.ToString() == "3")
                    {
                        UCRaccomandazioniPrescrizioniIspezione32.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione32.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione32.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione32.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione32.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione32.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione33": // Sono stati realizzati gli interventi previsti
                if (cbRTCEEinterventiPrevisti.Value != null)
                {
                    if (cbRTCEEinterventiPrevisti.Value.ToString() == "2" || cbRTCEEinterventiPrevisti.Value.ToString() == "3")
                    {
                        UCRaccomandazioniPrescrizioniIspezione33.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione33.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione33.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione33.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione33.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione33.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione34": // CO fumi secchi regolare 
                if(rblMonossidoCarbonio.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione34.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione34.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione34.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione34.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione35": // Indice di Bacharach regolare
                if (rblIndiceFumosita.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione35.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione35.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione35.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione35.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione36": // Rendimento  combustione regolare
                if (rblRendimentoCombustibileMinimo.SelectedValue == "False")
                {
                    UCRaccomandazioniPrescrizioniIspezione36.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione36.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione36.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione36.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione37": // Unità immobiliari contabilizzate
                if (cbUnitaImmobiliariContabilizzate.Value != null)
                {
                    if (cbUnitaImmobiliariContabilizzate.Value.ToString() == "2")
                    {
                        UCRaccomandazioniPrescrizioniIspezione37.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione37.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione37.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione37.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione37.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione37.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione38": // Unità immobiliari dotate di sistemi di termoregolazione
                if (cbUnitaImmobiliariTermoregolazione.Value != null)
                {
                    if (cbUnitaImmobiliariTermoregolazione.Value.ToString() == "2")
                    {
                        UCRaccomandazioniPrescrizioniIspezione38.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione38.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione38.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione38.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione38.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione38.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione39": // Tipologia termoregolazione
                if (cbTipologiaTermoregolazione.Value != null)
                {
                    if (cbTipologiaTermoregolazione.Value.ToString() == "1")
                    {
                        UCRaccomandazioniPrescrizioniIspezione39.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione39.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione39.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione39.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione39.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione39.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione40": // Funzionamento sistema di regolazione principale
                if (cbFunzionamentoSistemaRegPrincipale.Value != null)
                {
                    if (cbFunzionamentoSistemaRegPrincipale.Value.ToString() == "2")
                    {
                        UCRaccomandazioniPrescrizioniIspezione40.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione40.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione40.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione40.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione40.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione40.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione41": // Funzionamento sistema di regolazione interno alle singole unità abitative
                if (cbFunzionamentoSistemaRegAbitative.Value != null)
                {
                    if (cbFunzionamentoSistemaRegAbitative.Value.ToString() == "2")
                    {
                        UCRaccomandazioniPrescrizioniIspezione41.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione41.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione41.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione41.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione41.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione41.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione42": // Presenza relazione tecnica di motivazione dell'esenzione
                if (cbPresenzaRelazioneTecnica.Value != null)
                {
                    if (cbPresenzaRelazioneTecnica.Value.ToString() == "2")
                    {
                        UCRaccomandazioniPrescrizioniIspezione42.Visible = true;
                        UCRaccomandazioniPrescrizioniIspezione42.fActive = "1";
                    }
                    else
                    {
                        UCRaccomandazioniPrescrizioniIspezione42.Visible = false;
                        UCRaccomandazioniPrescrizioniIspezione42.fActive = "0";
                    }
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione42.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione42.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione43": // Verifica della presenza riferimento documentale adozione
                if (chkVerificaDocumentaleAdozione.Value == EnumStatoSiNoNc.No)
                {
                    UCRaccomandazioniPrescrizioniIspezione43.Visible = true;
                    UCRaccomandazioniPrescrizioniIspezione43.fActive = "1";
                }
                else
                {
                    UCRaccomandazioniPrescrizioniIspezione43.Visible = false;
                    UCRaccomandazioniPrescrizioniIspezione43.fActive = "0";
                }
                break;
            case "UCRaccomandazioniPrescrizioniIspezione47": // DATI CATASTALI
                // TODO dgDatiCatastali - se sono vuoti o qualche dato è 0000000000 - visualizzare raccomandazioni
                //if (dgDatiCatastali.Columns.IsEmpty)
                //{
                //    UCRaccomandazioniPrescrizioniIspezione47.Visible = true;
                //    UCRaccomandazioniPrescrizioniIspezione47.fActive = "1";
                //}
                //else
                //{
                //    bool fRaccPres = false;

                //    if (inizioRaccPres)
                //    {
                //        for (int i = 0; i < dgDatiCatastali.VisibleRowCount; i++)
                //        {
                //            if (dgDatiCatastali.GetRowValues(i, "Foglio").ToString() == "00000000000" ||
                //                dgDatiCatastali.GetRowValues(i, "Mappale").ToString() == "00000000000" ||
                //                dgDatiCatastali.GetRowValues(i, "Subalterno").ToString() == "00000000000" ||
                //                dgDatiCatastali.GetRowValues(i, "Identificativo").ToString() == "00000000000")
                //            {
                //                fRaccPres = true;
                //                break;
                //            }
                //        }
                //    }
                    
                //    UCRaccomandazioniPrescrizioniIspezione47.Visible = fRaccPres;
                //    if (fRaccPres)
                //    {
                //        UCRaccomandazioniPrescrizioniIspezione47.fActive = "1";
                //    }
                //    else
                //    {
                //        UCRaccomandazioniPrescrizioniIspezione47.fActive = "0";
                //    }                    
                //}
                break;
        }
    }

    #endregion

    #region Value Changed

    protected void txtMisurazioniEseguite_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMisurazioniEseguite.Text) || txtMisurazioniEseguite.Text == "0")
        {
            rowRispettoValoriVigente.Visible = false;
            chkRispettoValoriVigente.Value = null;
        }
        else
        {
            rowRispettoValoriVigente.Visible = true;
        }
    }

    protected void cbUnitaImmobiliariServite_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FilterUnitaImmobiliari();
    }

    protected void rblNominatoDelega_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterDelegatoIspezione();
    }

    protected void cbCombustibile_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterAltroCombustibile();
    }

    protected void rblOCNominato_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterOperatoreConduttoreIspezione();
    }

    protected void rblOCNominatoPresente_SelectedIndexChanged(object sender, EventArgs e)
    {
        // TODO
    }

    protected void rblOCAbilitato_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione1");
    }

    protected void rblImpiantoRegistratoCRITER_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione3");
    }

    protected void rblfTerzoResponsabile_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterTerzoResponsabileIspezione();
    }

    protected void rblAbilitazioneTerzoResponsabile_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione4");
    }

    protected void rblCertificazioneTerzoResponsabile_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione5");
    }

    protected void rblAttestatoTerzoResponsabile_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione6");
    }

    protected void rblAttestazioneIncaricoTerzoResponsabile_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione44");
    }

    protected void rblImpresaManutentricePresente_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterImpresaManutentriceIspezione();
    }

    protected void rblAbilitazioneImpresaManutentrice_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione7");
    }

    protected void rblPatentinoOperatoreUltimoControllo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione8");
    }

    protected void cbInstallazioneInternaEsterna_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterInstallazioneInternaEsterna();
    }

    protected void chkLocaleInstallazioneIdoneo_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione45");
    }

    protected void chkGeneratoriIdonei_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione46");
    }

    protected void chkApertureLibere_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione9");
    }

    protected void chkDimensioniApertureAdeguate_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione11");
    }

    protected void chkScarichiIdonei_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione12");
    }

    protected void chkAssenzaPerditeCombustibile_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione13");
    }

    protected void chkTenutaImpianto_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione14");
    }

    protected void txtTiraggioDelCamino_TextChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione15");
    }

    //protected void rblTiraggioDelCamino_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione16");
    //}

    protected void rblPresenteLibretto_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione17");
    }

    protected void rblLibrettoUsoManutenzione_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione18");
    }

    protected void rblLibrettoCompilatoInTutteLeParte_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione19");
    }

    protected void rblDichiarazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione10");
    }
       

    //protected void chkProggettoAntincendio_CheckedChanged(object sender, EventArgs e)
    //{
    //    SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione21");
    //}

    protected void chkSCIAAntincendio_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione22");
    }

    //protected void chkVerbaleSopralluogo_CheckedChanged(object sender, EventArgs e)
    //{
    //    SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione23");
    //}

    //protected void chkUltimoRinnovoPeriodico_CheckedChanged(object sender, EventArgs e)
    //{
    //    SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione24");
    //}

    protected void rblPresenteProgettoImpianto_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione25");
        //FilterObbligatorioProgetto();
        //FilterObbligatorio();
    }

    //protected void chkAQE_CheckedChanged(object sender, EventArgs e)
    //{
    //    SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione26");
    //}

    protected void chkDiagnosiEnergetica_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione27");
    }

    protected void chkPerizia_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione28");
    }

    protected void rblCorrettoDimensionamentoGeneratore_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione29");
    }

    protected void chkOmologazioneVerifiche_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione16");
    }


    protected void rblTrattamentoRiscaldamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterTrattamentoRiscaldamento();
    }

    protected void rblTrattamentoProduzioneACS_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterTipoTrattamentoProduzioneACS();
    }
    protected void cbUltimoControlloPrevistoEffettuato_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione32");
        FilterDataUltimoControllo();
    }

    protected void cbRTCEEinterventiPrevisti_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione33");
    }

    protected void rblMonossidoCarbonio_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione34");
    }

    protected void rblIndiceFumosita_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione35");
    }

    protected void rblRendimentoCombustibileMinimo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione36");
    }

    protected void cbUnitaImmobiliariContabilizzate_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione37");
    }

    protected void cbUnitaImmobiliariTermoregolazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione38");
    }

    protected void cbTipologiaTermoregolazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione39");
        FilterTipologiaTermoregolazione();
    }

    protected void cbFunzionamentoSistemaRegPrincipale_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione40");
    }

    protected void cbFunzionamentoSistemaRegAbitative_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione41");
    }

    protected void cbPresenzaRelazioneTecnica_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione42");
    }

    protected void chkVerificaDocumentaleAdozione_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione43");
    }

    protected void cbFluidoTermoVettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterFluidoTermoVettore();
    }

    protected void cbFrequenza_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterFrequenza();
    }

    protected void rblUltimaManutenzionePrevistaEffettuata_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterUltimaManutenzione();
    }

    protected void cbFrequenzaEfficienzaEnergetica_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterFrequenzaEfficienzaEnergetica();
    }

    protected void cbTipoDistribuzione_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterDistribuzione();
    }

    protected void cbOsservazioniRCTEE_CheckedChanged(object sender, EventArgs e)
    {
        FilterConRCTEE();
    }

    protected void cbRaccomandazioniRCTEE_CheckedChanged(object sender, EventArgs e)
    {
        FilterConRCTEE();
    }

    protected void cbPrescrizioniRCTEE_CheckedChanged(object sender, EventArgs e)
    {
        FilterConRCTEE();
    }

    protected void rblEvacuazioneFumi_CheckedChanged(object sender, EventArgs e)
    {
        FilterEvacuzioneFumi();
    }

    protected void chkRispettoValoriVigente_CheckedChanged(object sender, EventArgs e)
    {
        SetRaccomandazioniPrescrizioni("UCRaccomandazioniPrescrizioniIspezione20");
    }

    #endregion

    #endregion

    protected void customValidatorRapportoIspezione(Object sender, ServerValidateEventArgs e)
    {
        string message = "Attenzione:<br/><br/>";

        //    #region Controlli Raccomadanzioni/Prescrizioni
        //    bool fUnitaImmbiliare = UtilityRapportiControllo.GetfUnitaImmobiliare(int.Parse(IDRapportoControlloTecnico));
        //    bool c01 = true;
        //    if ((rblDichiarazioneConformita.SelectedIndex != 0) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c01 = false;
        //        message += "Sezione B - Dichiarazione di conformità assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //    }

        //    bool c02 = true;
        //    if ((rblUsoManutenzioneGeneratore.SelectedIndex != 0) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c02 = false;
        //        message += "Sezione B - Libretti uso/manutenzione generatore assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        //    }

        //    bool c03 = true;
        //    if ((rblLibrettoImpiantoCompilato.SelectedIndex != 0) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c03 = false;
        //        message += "Sezione B - Libretto impianto non compilato in tutte le sue parti: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        //    }

        //    bool c04 = true;
        //    if ((rblTrattamentoRiscaldamento.SelectedIndex == 1) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c04 = false;
        //        message += "Sezione C - Trattamento in riscaldamento assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        //    }

        //    bool c05 = true;
        //    if ((rblTrattamentoAcs.SelectedIndex == 1) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c05 = false;
        //        message += "Sezione C - Trattamento Acs assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        //    }

        //    bool c06 = true;
        //    int LocaleInstallazioneIdoneo = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkLocaleInstallazioneIdoneo")).Value;
        //    if ((LocaleInstallazioneIdoneo == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //    {
        //        c06 = false;
        //        if (IDTipologiaRct == "1")
        //        {
        //            message += "Sezione D - Installazione interna in locale non idoneo: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
        //        }
        //        else if ((IDTipologiaRct == "2") || (IDTipologiaRct == "3") || (IDTipologiaRct == "4"))
        //        {
        //            message += "Sezione D - Locale di installazione non idoneo: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c06bis = true;
        //    if (IDTipologiaRct == "4")
        //    {
        //        int CapsulaInsonorizzazioneIdonea = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkCapsulaInsonorizzazioneIdonea")).Value;
        //        if ((CapsulaInsonorizzazioneIdonea == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c06bis = false;
        //            message += "Sezione D - Capsula insonorizzante non idonea (esame visivo): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c07 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int GeneratoriIdonei = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkGeneratoriIdonei")).Value;
        //        if ((GeneratoriIdonei == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c07 = false;
        //            message += "Sezione D - Installazione esterna generatori non idonei: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //        }
        //    }

        //    bool c07bis = true;
        //    if (IDTipologiaRct == "4")
        //    {
        //        int TenutaCircuitoOlioIdonea = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTenutaCircuitoOlioIdonea")).Value;
        //        if ((TenutaCircuitoOlioIdonea == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c07bis = false;
        //            message += "Sezione D - Tenuta circuito olio non idonea: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c08 = true;
        //    if ((IDTipologiaRct == "1") || (IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
        //    {
        //        int DimensioniApertureAdeguate = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDimensioniApertureAdeguate")).Value;
        //        if ((DimensioniApertureAdeguate == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c08 = false;
        //            if (IDTipologiaRct == "1")
        //            {
        //                message += "Sezione D - Non Adeguate dimensioni aperture di ventilazione/aerazione: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
        //            }
        //            else if ((IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
        //            {
        //                message += "Sezione D - Non Adeguate dimensioni aperture di ventilazione/aerazione: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //            }
        //        }
        //    }

        //    bool c08bis = true;
        //    if (IDTipologiaRct == "4")
        //    {
        //        int FunzionalitaScambiatoreSeparazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkFunzionalitaScambiatoreSeparazione")).Value;
        //        if ((FunzionalitaScambiatoreSeparazione == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c08bis = false;
        //            message += "Sezione D - Funzionalità dello scambiatore di calore di separazione fra unità cogenerativa e impianto edificio (se presente) non idonea: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c09 = true;
        //    if ((IDTipologiaRct == "1") || (IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
        //    {
        //        int ApertureLibere = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkApertureLibere")).Value;
        //        if ((ApertureLibere == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c09 = false;
        //            if (IDTipologiaRct == "1")
        //            {
        //                message += "Sezione D - Aperture di ventilazione/aerazione non libere da ostruzioni: è necessario indicare una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //            }
        //            else if ((IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
        //            {
        //                message += "Sezione D - Aperture di ventilazione/aerazione non libere da ostruzioni: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //            }
        //        }
        //    }

        //    bool c10 = true;
        //    if ((IDTipologiaRct == "1") || (IDTipologiaRct == "4"))
        //    {
        //        int ScarichiIdonei = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScarichiIdonei")).Value;
        //        if ((ScarichiIdonei == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c10 = false;
        //            if (IDTipologiaRct == "1")
        //            {
        //                message += "Sezione D - Canale da fumo o condotti di scarico non idonei (esame visivo): è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
        //            }
        //            else if (IDTipologiaRct == "4")
        //            {
        //                message += "Sezione D - Canale da fumo o condotti di scarico non idonei (esame visivo): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //            }
        //        }
        //    }

        //    bool c11 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int RegolazioneTemperaturaAmbiente = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkRegolazioneTemperaturaAmbiente")).Value;
        //        if ((RegolazioneTemperaturaAmbiente == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c11 = false;
        //            message += "Sezione D - Sistema di regolazione temperatura ambiente non funzionante: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //        }
        //    }

        //    bool c12 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int AssenzaPerditeCombustibile = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeCombustibile")).Value;
        //        if ((AssenzaPerditeCombustibile == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c12 = false;
        //            message += "Sezione D - Presenza di perdite di combustibile liquido: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //        }
        //    }

        //    bool c13 = true;
        //    if ((IDTipologiaRct == "1") || (IDTipologiaRct == "3") || (IDTipologiaRct == "4"))
        //    {
        //        int TenutaImpiantoIdraulico = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTenutaImpiantoIdraulico")).Value;
        //        if ((TenutaImpiantoIdraulico == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c13 = false;
        //            if (IDTipologiaRct == "1")
        //            {
        //                message += "Sezione D - Non idonea tenuta dell'impianto interno e raccordi con il generatore: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
        //            }
        //            else if (IDTipologiaRct == "3")
        //            {
        //                message += "Sezione D - Presenza di perdite circuito idraulico: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //            }
        //            else if (IDTipologiaRct == "4")
        //            {
        //                message += "Sezione D - Tenuta circuito alimentazione non idoena: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //            }
        //        }
        //    }

        //    bool c13bis = true;
        //    if ((IDTipologiaRct == "2") || (IDTipologiaRct == "3") || (IDTipologiaRct == "4"))
        //    {
        //        int LineeElettricheIdonee = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkLineeElettricheIdonee")).Value;
        //        if ((LineeElettricheIdonee == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c13bis = false;
        //            message += "Sezione D - Linee elettriche non idonee: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c14 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int DispositiviComandoRegolazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviComandoRegolazione")).Value;
        //        if ((DispositiviComandoRegolazione == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c14 = false;
        //            message += "Sezione E - Dispositivi di comando e regolazione non funzionanti correttamente: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
        //        }
        //    }

        //    bool c14bis = true;
        //    if ((IDTipologiaRct == "2") || (IDTipologiaRct == "3"))
        //    {
        //        int CoibentazioniIdonee = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkCoibentazioniIdonee")).Value;
        //        if ((CoibentazioniIdonee == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c14bis = false;
        //            message += "Sezione D - Coibentazioni idonee non idonee: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c15 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int DispositiviSicurezza = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviSicurezza")).Value;
        //        if ((DispositiviSicurezza == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c15 = false;
        //            message += "Sezione E - Dispositivi di sicurezza manomessi e/o cortocircuitati: è necessario indicare una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Prescrizione'<br /><br />";
        //        }
        //    }

        //    bool c15bis = true;
        //    if (IDTipologiaRct == "2")
        //    {
        //        int AssenzaPerditeRefrigerante = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeRefrigerante")).Value;
        //        if ((AssenzaPerditeRefrigerante == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c15bis = false;
        //            message += "Sezione E - Presenza di perdite di gas refrigerante: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c16 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int ValvolaSicurezzaSovrappressione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkValvolaSicurezzaSovrappressione")).Value;
        //        if ((ValvolaSicurezzaSovrappressione == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c16 = false;
        //            message += "Sezione E - Valvola di sicurezza alla sovrappressione a scarico libero assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //        }
        //    }

        //    bool c16bis = true;
        //    if (IDTipologiaRct == "2")
        //    {
        //        int FiltriPuliti = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkFiltriPuliti")).Value;
        //        if ((FiltriPuliti == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c16bis = false;
        //            message += "Sezione E - Filtri non puliti: è necessario indicare una Raccomandazione o una Prescrzione<br /><br />";
        //        }
        //    }

        //    bool c17 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int ScambiatoreFumiPulito = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoreFumiPulito")).Value;
        //        if ((ScambiatoreFumiPulito == 0) && (txtRaccomandazioni.Text == string.Empty))
        //        {
        //            c17 = false;
        //            message += "Sezione E - Non controllato e pulito lo scambiatore lato fumi: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //        }
        //    }

        //    bool c17bis = true;
        //    if (IDTipologiaRct == "2")
        //    {
        //        int LeakDetector = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkLeakDetector")).Value;
        //        if ((LeakDetector == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c17bis = false;
        //            message += "Sezione E - Assenza di apparecchiatura automatica rilevazione diretta fughe refrigerante (leak detector): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c18 = true;
        //    if (IDTipologiaRct == "1")
        //    {
        //        int RiflussoProdottiCombustione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkRiflussoProdottiCombustione")).Value;
        //        if ((RiflussoProdottiCombustione == 1) && (txtPrescrizioni.Text == string.Empty))
        //        {
        //            c18 = false;
        //            message += "Sezione E - Presenza riflusso dei prodotti della combustione: è necessario indicare una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Prescrizione'<br /><br />";
        //        }
        //    }

        //    bool c18bis = true;
        //    if (IDTipologiaRct == "2")
        //    {
        //        int ScambiatoriLiberi = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoriLiberi")).Value;
        //        if ((ScambiatoriLiberi == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c18bis = false;
        //            message += "Sezione E - Scambiatori di calore non puliti e non liberi da incrostazioni: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c18bisbis = true;
        //    if (IDTipologiaRct == "3")
        //    {
        //        int PotenzaCompatibileProgetto = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkPotenzaCompatibileProgetto")).Value;
        //        if ((PotenzaCompatibileProgetto == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c18bisbis = false;
        //            message += "Sezione E - Potenza non compatibile con i dati di progetto: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c19 = true;
        //    int Contabilizzazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkContabilizzazione")).Value;
        //    if ((Contabilizzazione == 0) && (!fUnitaImmbiliare) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c19 = false;
        //        message += "Sezione G - Contabilizzazione assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //    }

        //    bool c19bis = true;
        //    if (IDTipologiaRct == "2")
        //    {
        //        int ParametriTermodinamici = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkParametriTermodinamici")).Value;
        //        if ((ParametriTermodinamici == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c19bis = false;
        //            message += "Sezione E - Assenza di apparecchiatura automatica rilevazione indiretta fughe refrigerante (parametri termodinamici): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c19bisbis = true;
        //    if (IDTipologiaRct == "3")
        //    {
        //        int AssenzaTrafilamenti = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaTrafilamenti")).Value;
        //        if ((AssenzaTrafilamenti == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        //        {
        //            c19bisbis = false;
        //            message += "Sezione E - Dispositivi di regolazione e controllo non funzionanti e presenza di trafilamenti sulla valvola di regolazione: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
        //        }
        //    }

        //    bool c20 = true;
        //    int Termoregolazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTermoregolazione")).Value;
        //    if ((Termoregolazione == 0) && (!fUnitaImmbiliare) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c20 = false;
        //        message += "Sezione G - Termoregolazione assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //    }

        //    bool c21 = true;
        //    int CorrettoFunzionamentoContabilizzazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkCorrettoFunzionamentoContabilizzazione")).Value;
        //    if ((Termoregolazione == 0) && (!fUnitaImmbiliare) && (txtRaccomandazioni.Text == string.Empty))
        //    {
        //        c21 = false;
        //        message += "Sezione G - Non corretto funzionamento dei sistemi di contabilizzazione e termoregolazione: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        //    }

        //    #endregion

        //    #region Controlli Campi
        //    decimal DurezzaAcqua;
        //    try
        //    {
        //        DurezzaAcqua = decimal.Parse(txtDurezzaAcqua.Text);
        //    }
        //    catch (Exception)
        //    {
        //        DurezzaAcqua = -1;
        //    }

        //    RCT_UC_UCVerificaEnergeticaGT RCT_UC_UCVerificaEnergeticaGT = ((RCT_UC_UCVerificaEnergeticaGT)MainFormView.FindControl("UCVerificaEnergeticaGT"));
        //    decimal TemperaturaFumi;
        //    try
        //    {
        //        TemperaturaFumi = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtTemperaturaFumi")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        TemperaturaFumi = -1;
        //    }

        //    decimal TemperaturaComburente;
        //    try
        //    {
        //        TemperaturaComburente = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtTemperaturaComburente")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        TemperaturaComburente = -1;
        //    }

        //    decimal O2;
        //    try
        //    {
        //        O2 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtO2")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        O2 = -1;
        //    }

        //    decimal CO2;
        //    try
        //    {
        //        CO2 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCO2")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        CO2 = -1;
        //    }

        //    decimal CoFumiSecchi;
        //    try
        //    {
        //        CoFumiSecchi = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCoFumiSecchi")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        CoFumiSecchi = -1;
        //    }

        //    decimal CoCorretto;
        //    try
        //    {
        //        CoCorretto = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCoCorretto")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        CoCorretto = -1;
        //    }

        //    decimal PotenzaTermicaEffettiva;
        //    try
        //    {
        //        PotenzaTermicaEffettiva = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtPotenzaTermicaEffettiva")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        PotenzaTermicaEffettiva = -1;
        //    }

        //    decimal RendimentoCombustione;
        //    try
        //    {
        //        RendimentoCombustione = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtRendimentoCombustione")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        RendimentoCombustione = -1;
        //    }

        //    decimal RendimentoMinimo;
        //    try
        //    {
        //        RendimentoMinimo = decimal.Parse(((Label)RCT_UC_UCVerificaEnergeticaGT.FindControl("lblRendimentoMinimo")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        RendimentoMinimo = -1;
        //    }

        //    decimal Bacharach1;
        //    try
        //    {
        //        Bacharach1 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach1")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        Bacharach1 = -1;
        //    }

        //    decimal Bacharach2;
        //    try
        //    {
        //        Bacharach2 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach2")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        Bacharach2 = -1;
        //    }

        //    decimal Bacharach3;
        //    try
        //    {
        //        Bacharach3 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach3")).Text);
        //    }
        //    catch (Exception)
        //    {
        //        Bacharach3 = -1;
        //    }


        //    decimal PotenzaTermicaNominaleFocolare;
        //    try
        //    {
        //        PotenzaTermicaNominaleFocolare = decimal.Parse(lblPotenzaTermicaNominaleFocolare.Text);
        //    }
        //    catch (Exception)
        //    {
        //        PotenzaTermicaNominaleFocolare = -1;
        //    }

        //    bool Val01 = true;
        //    bool Val02 = true;
        //    bool Val03 = true;
        //    bool Val04 = true;
        //    bool Val05 = true;
        //    bool Val06 = true;
        //    bool Val07 = true;
        //    bool Val08 = true;
        //    bool Val09 = true;
        //    bool Val10 = true;
        //    bool Val10bis = true;
        //    bool Val11 = true;
        //    bool Val12 = true;
        //    bool Val13 = true;
        //    bool Val14 = true;
        //    bool Val15 = true;

        //    if (IDTipologiaRct == "1")
        //    {
        //        if (DurezzaAcqua != -1)
        //        {
        //            if (!((DurezzaAcqua >= 0) && (DurezzaAcqua <= 100.00m)))
        //            {
        //                Val01 = false;
        //                message += "Sezione C – Durezza totale dell'acqua: range valori ammessi 0 <= 100 °F (gradi francesi)<br /><br />";
        //            }
        //        }

        //        if (TemperaturaFumi != -1)
        //        {
        //            if (!((TemperaturaFumi >= 0) && (TemperaturaFumi <= 250.00m)))
        //            {
        //                Val02 = false;
        //                message += "Sezione E – Temperatura fumi: range valori ammessi 0 <= 250 °C<br /><br />";
        //            }
        //        }

        //        if (TemperaturaComburente != -1)
        //        {
        //            if (!((TemperaturaComburente >= 0) && (TemperaturaComburente <= 200.00m)))
        //            {
        //                Val03 = false;
        //                message += "Sezione E – Temperatura aria comburente: range valori ammessi 0 <= 200 °C<br /><br />";
        //            }
        //        }

        //        if (O2 != -1)
        //        {
        //            if (!((O2 >= 0) && (O2 <= 20.9m)))
        //            {
        //                Val04 = false;
        //                message += "Sezione E – O2: range valori ammessi 0 <= 20,9 %<br /><br />";
        //            }
        //        }

        //        if ((CO2 != -1) && (lblIDTipologiaCombustibile.Text != "1"))
        //        {
        //            switch (lblIDTipologiaCombustibile.Text)
        //            {
        //                case "2": //Gas naturale
        //                    if (!((CO2 >= 0) && (CO2 <= 12.00m)))
        //                    {
        //                        Val05 = false;
        //                        message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 12 %<br /><br />";
        //                    }
        //                    break;
        //                case "3": //Gpl
        //                    if (!((CO2 >= 0) && (CO2 <= 13.90m)))
        //                    {
        //                        Val05 = false;
        //                        message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 13,9 %<br /><br />";
        //                    }
        //                    break;
        //                case "4": //Gasolio
        //                    if (!((CO2 >= 0) && (CO2 <= 15.10m)))
        //                    {
        //                        Val05 = false;
        //                        message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 15,1 %<br /><br />";
        //                    }
        //                    break;
        //                case "5": //Olio combustibile
        //                    if (!((CO2 >= 0) && (CO2 <= 15.70m)))
        //                    {
        //                        Val05 = false;
        //                        message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 15,7 %<br /><br />";
        //                    }
        //                    break;
        //                case "6": //Pellet
        //                case "7": //Legna
        //                case "8": //Cippato
        //                case "9": //Bricchette
        //                    if (!((CO2 >= 0) && (CO2 <= 20.10m)))
        //                    {
        //                        Val05 = false;
        //                        message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 20,1 %<br /><br />";
        //                    }
        //                    break;
        //            }
        //        }

        //        if ((CoCorretto != -1) && (RendimentoCombustione != -1) && (RendimentoMinimo != -1) && (lblIDTipologiaCombustibile.Text != "1"))
        //        {
        //            //TODO: da controllare
        //            //int conformitaUni10389 = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkConformitàUNI10389")).Value;
        //            //switch (lblIDTipologiaCombustibile.Text)
        //            //{
        //            //    case "2": //Gas naturale
        //            //    case "3": //Gpl
        //            //        if (conformitaUni10389 == 1 && ((CoCorretto > 1000) || (RendimentoCombustione < RendimentoMinimo)))
        //            //        {
        //            //            Val06 = false;
        //            //            message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul No solo se non vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br /><br />";
        //            //        }
        //            //        else if (conformitaUni10389 == 0 && ((CoCorretto <= 1000) || (RendimentoCombustione >= RendimentoMinimo)))
        //            //        {
        //            //            Val06 = false;
        //            //            message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul Si solo se vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br /><br />";
        //            //        }
        //            //        break;
        //            //    case "4": //Gasolio
        //            //        var fCheckbacharach2 = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 2);
        //            //        if (conformitaUni10389 == 1)
        //            //        {
        //            //            if ((CoCorretto > 1000) || (RendimentoCombustione < RendimentoMinimo) || (!fCheckbacharach2))
        //            //            {
        //            //                Val06 = false;
        //            //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul No solo se non vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori del campo Bacharach devono essere ≤ 2<br /><br />";
        //            //            }
        //            //        }
        //            //        else if (conformitaUni10389 == 0)
        //            //        {
        //            //            if ((CoCorretto <= 1000) || (RendimentoCombustione >= RendimentoMinimo) || (fCheckbacharach2))
        //            //            {
        //            //                Val06 = false;
        //            //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul Si solo se vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori del campo Bacharach devono essere ≤ 2<br /><br />";
        //            //            }
        //            //        }
        //            //        break;
        //            //    case "5": //Olio combustibile
        //            //        var fCheckbacharach6 = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 6);
        //            //        if (conformitaUni10389 == 1)
        //            //        {
        //            //            if ((CoCorretto > 1000) || (RendimentoCombustione < RendimentoMinimo) || (!fCheckbacharach6))
        //            //            {
        //            //                Val06 = false;
        //            //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul No solo se non vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori di Bacharach devono essere ≤ 6<br /><br />";
        //            //            }
        //            //        }
        //            //        else if (conformitaUni10389 == 0)
        //            //        {
        //            //            if ((CoCorretto <= 1000) || (RendimentoCombustione >= RendimentoMinimo) || (fCheckbacharach6))
        //            //            {
        //            //                Val06 = false;
        //            //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul Si solo se vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori di Bacharach devono essere ≤ 6<br /><br />";
        //            //            }
        //            //        }
        //            //        break;
        //            //}
        //        }

        //        if ((CoCorretto != -1) && (CoFumiSecchi != -1))
        //        {
        //            if (!(CoCorretto >= CoFumiSecchi))
        //            {
        //                Val07 = false;
        //                message += "Sezione E – CO corretto (ppm): il valore inserito non può essere minore del valore di CO fumi secchi<br /><br />";
        //            }
        //        }

        //        if ((PotenzaTermicaEffettiva != -1) && (PotenzaTermicaNominaleFocolare != -1))
        //        {
        //            decimal PotenzaTermicaNominaleFocolare50 = (PotenzaTermicaNominaleFocolare * 0.5m);
        //            decimal PotenzaTermicaNominaleFocolare120 = (PotenzaTermicaNominaleFocolare * 1.20m);

        //            if (!((PotenzaTermicaEffettiva > PotenzaTermicaNominaleFocolare50) && (PotenzaTermicaEffettiva < PotenzaTermicaNominaleFocolare120)))
        //            {
        //                Val08 = false;
        //                message += "Sezione E – La Potenza termica effettiva deve avere un range di valori ammessi tra il 50% della Potenza termica max al focolare e il 120% della Potenza termica max al focolare<br /><br />";
        //            }
        //        }

        //        if (RendimentoCombustione != -1)
        //        {
        //            if (!((RendimentoCombustione >= 0) && (RendimentoCombustione <= 150.00m)))
        //            {
        //                Val09 = false;
        //                message += "Sezione E – Rendimento di combustione: range valori ammessi 0 <= 150<br /><br />";
        //            }
        //        }

        //        if ((Bacharach1 != -1) && (Bacharach2 != -1) && (Bacharach3 != -1))
        //        {
        //            var result = false;
        //            RCT_UC_Checkbox chkRispettaIndiceBacharach = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkRispettaIndiceBacharach"));
        //            bool fRispettaIndiceBacharach = UtilityApp.ToBoolean(chkRispettaIndiceBacharach.Value.ToString());

        //            switch (lblIDTipologiaCombustibile.Text)
        //            {
        //                case "4": //Gasolio
        //                    result = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 2);
        //                    if ((!result) && (fRispettaIndiceBacharach))
        //                    {
        //                        Val13 = false;
        //                        message += "Sezione E – Rispetta l'indice di Bacharach: il campo deve essere impostato sul Si solo se due dei tre valori di Bacharach inseriti sono ≤ 2  per il combustibile " + lblTipologiaCombustibile.Text + "<br /><br/>";
        //                    }
        //                    break;
        //                case "5": //Olio combustibile
        //                    result = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 6);
        //                    if ((!result) && (fRispettaIndiceBacharach))
        //                    {
        //                        Val13 = false;
        //                        message += "Sezione E – Rispetta l'indice di Bacharach: il campo deve essere impostato sul Si solo se due dei tre valori di Bacharach inseriti sono ≤ 6  per il combustibile " + lblTipologiaCombustibile.Text + "<br /><br/>";
        //                    }
        //                    break;
        //            }
        //        }

        //        if ((CoCorretto != -1))
        //        {
        //            RCT_UC_Checkbox chkCOFumiSecchiNoAria1000 = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkCOFumiSecchiNoAria1000"));
        //            bool fCOFumiSecchiNoAria1000 = UtilityApp.ToBoolean(chkCOFumiSecchiNoAria1000.Value.ToString());
        //            if (!(CoCorretto <= 1000) && (fCOFumiSecchiNoAria1000))
        //            {
        //                Val14 = false;
        //                message += "Sezione E – CO corretto <= 1.000 ppm v/v: in base al valore di CO corretto (ppm) il campo deve essere impostato su No<br /><br />";
        //            }
        //            else if (!(CoCorretto >= 1000) && (!fCOFumiSecchiNoAria1000))
        //            {
        //                Val14 = false;
        //                message += "Sezione E – CO corretto <= 1.000 ppm v/v: in base al valore di CO corretto (ppm) il campo deve essere impostato su Si<br /><br />";
        //            }
        //        }

        //        if ((RendimentoCombustione != -1) && (RendimentoMinimo != -1))
        //        {
        //            RCT_UC_Checkbox chkRendimentoSupMinimo = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkRendimentoSupMinimo"));
        //            bool fRendimentoSupMinimo = UtilityApp.ToBoolean(chkRendimentoSupMinimo.Value.ToString());
        //            if (!(RendimentoCombustione >= RendimentoMinimo) && (fRendimentoSupMinimo))
        //            {
        //                Val15 = false;
        //                message += "Sezione E – Rendimento >= rendimento minimo: in base al valore del rendimento minimo il campo deve essere impostato su No<br /><br />";
        //            }
        //            else if (!(RendimentoCombustione <= RendimentoMinimo) && (!fRendimentoSupMinimo))
        //            {
        //                Val15 = false;
        //                message += "Sezione E – Rendimento >= rendimento minimo: in base al valore del rendimento minimo il campo deve essere impostato su Si<br /><br />";
        //            }
        //        }

        //        if ((txtDataManutenzioneConsigliata.Text != "") && (txtDataControllo.Text != ""))
        //        {
        //            DateTime dtManutenzione = DateTime.Parse(txtDataManutenzioneConsigliata.Text);
        //            DateTime dtControllo = DateTime.Parse(txtDataControllo.Text);
        //            if (!(dtManutenzione > dtControllo))
        //            {
        //                Val10 = false;
        //                message += "La data intervento manutentivo raccomandata deve essere maggiore della data del presente controllo<br /><br />";
        //            }
        //            if (!(dtControllo <= DateTime.Now))
        //            {
        //                Val10bis = false;
        //                message += "La data del presente controllo non deve essere maggiore della data odierna<br /><br />";
        //            }
        //        }

        //        if ((txtOraArrivo.DateTime != null) && (txtOraPartenza.DateTime != null))
        //        {
        //            DateTime dtOraArrivo = txtOraArrivo.DateTime;
        //            DateTime dtOraPartenza = txtOraPartenza.DateTime;
        //            if (!(dtOraPartenza > dtOraArrivo))
        //            {
        //                Val11 = false;
        //                message += "L'orario di arrivo presso l'impianto deve essere inferiore all'orario di partenza presso l'impianto<br /><br />";
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(txtPrescrizioni.Text) && (bool.Parse(rblImpiantoFunzionante.SelectedItem.Value)))
        //        {
        //            Val12 = false;
        //            message += "In presenza di una prescrizione l’impianto non può funzionare. E’ necessario pertanto selezionare l’opzione “No” del relativo campo<br /><br />";
        //        }



        //    }
        //    #endregion

        //    #region Controlli NC
        //    bool Nc01 = true;
        //    int? fScarichiIdonei = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkScarichiIdonei")).Value;
        //    if (fScarichiIdonei != null)
        //    {
        //        if ((fScarichiIdonei == -1) && (IDTipologiaRct == "1"))
        //        {
        //            Nc01 = false;
        //            message += "Sezione D - Canale da fumo o condotti di scarico idonei (esame visivo): l'opzione Nc non può essere selezionata<br /><br />";
        //        }
        //    }

        //    bool Nc02 = true;
        //    int? fAssenzaPerditeCombustibile = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeCombustibile")).Value;
        //    if (fAssenzaPerditeCombustibile != null)
        //    {
        //        if ((fAssenzaPerditeCombustibile == -1) && (IDTipologiaRct == "1") && ((lblIDTipologiaCombustibile.Text == "4") || (lblIDTipologiaCombustibile.Text == "5")))
        //        {
        //            Nc02 = false;
        //            message += "Sezione D - Assenza di perdite di combustibile liquido: l'opzione Nc non può essere selezionata<br /><br />";
        //        }
        //    }

        //    bool Nc03 = true;
        //    int? fDispositiviComandoRegolazione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviComandoRegolazione")).Value;
        //    if (fDispositiviComandoRegolazione != null)
        //    {
        //        if ((fDispositiviComandoRegolazione == -1) && (IDTipologiaRct == "1"))
        //        {
        //            Nc03 = false;
        //            message += "Sezione E - Dispositivi di comando e regolazione funzionanti correttamente: l'opzione Nc non può essere selezionata<br /><br />";
        //        }
        //    }

        //    bool Nc04 = true;
        //    int? fDispositiviSicurezza = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviSicurezza")).Value;
        //    if (fDispositiviSicurezza != null)
        //    {
        //        if ((fDispositiviSicurezza == -1) && (IDTipologiaRct == "1"))
        //        {
        //            Nc04 = false;
        //            message += "Sezione E - Dispositivi di sicurezza non manomessi e/o cortocircuitati: l'opzione Nc non può essere selezionata<br /><br />";
        //        }
        //    }

        //    bool Nc05 = true;
        //    //int? fValvolaSicurezzaSovrappressione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkValvolaSicurezzaSovrappressione")).Value;
        //    //if (fValvolaSicurezzaSovrappressione != null)
        //    //{
        //    //    if ((fValvolaSicurezzaSovrappressione == -1) && (IDTipologiaRct == "1"))
        //    //    {
        //    //        Nc05 = false;
        //    //        message += "Sezione E - Valvola di sicurezza alla sovrappressione a scarico libero: l'opzione Nc non può essere selezionata<br /><br />";
        //    //    }
        //    //}

        //    bool Nc06 = true;
        //    int? fScambiatoreFumiPulito = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoreFumiPulito")).Value;
        //    if (fScambiatoreFumiPulito != null)
        //    {
        //        if ((fScambiatoreFumiPulito == -1) && (IDTipologiaRct == "1"))
        //        {
        //            Nc06 = false;
        //            message += "Sezione E - Controllato e pulito lo scambiatore lato fumi: l'opzione Nc non può essere selezionata<br /><br />";
        //        }
        //    }

        //    bool Nc07 = true;
        //    int? fRiflussoProdottiCombustione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkRiflussoProdottiCombustione")).Value;
        //    if (fRiflussoProdottiCombustione != null)
        //    {
        //        if ((fRiflussoProdottiCombustione == -1) && (IDTipologiaRct == "1"))
        //        {
        //            Nc07 = false;
        //            message += "Sezione E - Presenza riflusso dei prodotti della combustione: l'opzione Nc non può essere selezionata<br /><br />";
        //        }
        //    }

        //    bool Nc08 = true;
        //    int? fConformitàUNI10389 = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkConformitàUNI10389")).Value;
        //    if (fConformitàUNI10389 != null)
        //    {
        //        if ((fConformitàUNI10389 == -1) && (iDTipologiaControllo == "1") && ((lblIDTipologiaCombustibile.Text == "2") || (lblIDTipologiaCombustibile.Text == "3") || (lblIDTipologiaCombustibile.Text == "4") || (lblIDTipologiaCombustibile.Text == "5")))
        //        {
        //            Nc08 = false;
        //            message += "Sezione E - Risultati controllo, secondo UNI 10389-1, conformi alla legge: l'opzione Nc non può essere selezionata<br /><br />";
        //        }
        //    }

        //    #endregion

        //    if (c01 && c02 && c03 && c04 && c05 && c06 && c06bis && c07 && c07bis && c08 && c08bis && c09 && c10 && c11 && c12 && c13 && c13bis && c14 && c14bis && c15 && c15bis && c16 && c16bis && c17 && c17bis && c18 && c18bis && c18bisbis && c19 && c19bis && c19bisbis && c20 && c21
        //        & Val01 && Val02 && Val03 && Val04 && Val05 && Val06 && Val07 && Val08 && Val09 && Val10 && Val10bis && Val11 && Val12 && Val13 && Val14 && Val15
        //        & Nc01 && Nc02 && Nc03 && Nc04 && Nc05 && Nc06 && Nc07 && Nc08
        //        )
        //    {
        //        e.IsValid = true;
        //    }
        //    else
        //    {
        //        e.IsValid = false;
        //        cvRaccomandazioniPrescrizioni.ErrorMessage = message;
        //    }
        }


    protected void btnSaveDatiIspezioneRapporto_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SalvaDatiIspezioneRapporto(long.Parse(IDIspezione));
            GetDatiRapportoAll(long.Parse(IDIspezione));
        }
    }






    
}