using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using System.Collections.Generic;

public partial class VER_Accertamenti : Page
{
    protected string IDAccertamento
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

    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDatiAccertamento(long.Parse(IDAccertamento));
            GetDatiAccertamentoStorico(long.Parse(IDAccertamento));
            GetDatiRaccomandate(long.Parse(IDAccertamento));
        }
    }

    public void GetDatiRaccomandate(long iDAccertamento)
    {
        UCRaccomandate.IDAccertamento = iDAccertamento.ToString();
    }

    #region DropDownList
    protected void ddTipologiaDistributore(DropDownList ddlTipologiaDistributore, int? iDPresel, int? iDCodiceCatastale)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaDistributori(iDPresel, iDCodiceCatastale);
        ddlTipologiaDistributore.DataValueField = "IDDistributore";
        ddlTipologiaDistributore.DataTextField = "Distributore";
        ddlTipologiaDistributore.DataSource = ls;
        ddlTipologiaDistributore.DataBind();

        ListItem myItem = new ListItem("NESSUNO", "0");
        ddlTipologiaDistributore.Items.Insert(0, myItem);

        if (ls.Count == 1)
        {
            ddlTipologiaDistributore.SelectedIndex = 1;
            ddlTipologiaDistributore.Enabled = false;
        }
        else
        {
            ddlTipologiaDistributore.SelectedIndex = 0;
        }
    }

    protected void rbTipologiaRisoluzioneAccertamento(RadioButtonList rblTipologiaRisoluzioneAccertamento, int? IDPresel, bool fImmediata)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaRisoluzioneAccertamento(IDPresel, fImmediata);
        rblTipologiaRisoluzioneAccertamento.DataValueField = "IDTipologiaRisoluzioneAccertamento";
        rblTipologiaRisoluzioneAccertamento.DataTextField = "TipologiaRisoluzioneAccertamento";
        rblTipologiaRisoluzioneAccertamento.DataSource = ls;
        rblTipologiaRisoluzioneAccertamento.DataBind();

        rblTipologiaRisoluzioneAccertamento.SelectedIndex = 0;
    }

    protected void ddTipologiaEventoAccertamento(DropDownList ddlTipologiaEventoAccertamento, int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaEventoAccertamento(IDPresel);
        ddlTipologiaEventoAccertamento.DataValueField = "IDTipologiaEventoAccertamento";
        ddlTipologiaEventoAccertamento.DataTextField = "TipologiaEventoAccertamento";
        ddlTipologiaEventoAccertamento.DataSource = ls;
        ddlTipologiaEventoAccertamento.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlTipologiaEventoAccertamento.Items.Insert(0, myItem);
        ddlTipologiaEventoAccertamento.SelectedIndex = 0;
    }

    protected void rbTipologiaImpiantoFunzionanteAccertamento(RadioButtonList rblTipologiaImpiantoFunzionanteAccertamento, int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaImpiantoFunzionanteAccertamento(IDPresel);
        rblTipologiaImpiantoFunzionanteAccertamento.DataValueField = "IDTipologiaImpiantoFunzionanteAccertamento";
        rblTipologiaImpiantoFunzionanteAccertamento.DataTextField = "TipologiaImpiantoFunzionanteAccertamento";
        rblTipologiaImpiantoFunzionanteAccertamento.DataSource = ls;
        rblTipologiaImpiantoFunzionanteAccertamento.DataBind();

        rblTipologiaImpiantoFunzionanteAccertamento.SelectedIndex = 0;
    }
    #endregion

    public void GetDatiAccertamento(long iDAccertamento)
    {
        using (var ctx = new CriterDataModel())
        {
            var accertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
            if (accertamento != null)
            {
                lblProgrammaAccertamento.Text = accertamento.ProgrammaAccertamento;
                lblIDRapportoControllo.Text = accertamento.IDRapportoDiControlloTecnicoBase.ToString();
                lblIDAccertatore.Text = accertamento.IDUtenteAccertatore.ToString();
                lblIDTipoAccertamento.Text = accertamento.IDTipoAccertamento.ToString();
                VisibleHiddenTipoAccertamento(accertamento.IDAccertamento.ToString(), accertamento.IDTipoAccertamento.ToString());

                lblIDIspezione.Text = accertamento.IDIspezione.ToString();
                if (accertamento.IDTipoAccertamento == 2)
                {
                    var ispezione = ctx.V_VER_Ispezioni.Where(a => a.IDIspezione == accertamento.IDIspezione).FirstOrDefault();
                    if (ispezione != null)
                    {
                        var ispettore = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == ispezione.IDIspettore).FirstOrDefault();

                        lblTipoIspezione.Text = ispezione.TipoIspezione;
                        lblIspettore.Text = "<b>Ispettore:</b>&nbsp;" + ispettore.Nome + " " + ispettore.Cognome + "<br/><b>Telefono:</b>&nbsp;"+ ispettore.Telefono + "<br/><b>Email:</b>&nbsp;" + ispettore.Email + "<br/><b>Pec:</b>&nbsp;" + ispettore.EmailPec;

                        if (ispezione.IDAccertamento != null)
                        {
                            rowDatiIspezioneAccertamentoOriginale.Visible = true;

                            QueryString qs = new QueryString();
                            qs.Add("IDAccertamento", ispezione.IDAccertamento.ToString());
                            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                            string url = "VER_Accertamenti.aspx";
                            url += qsEncrypted.ToString();

                            lnkAccertamentoRct.NavigateUrl = url;
                        }
                        else
                        {
                            rowDatiIspezioneAccertamentoOriginale.Visible = false;
                        }
                        
                    }
                }

                lblIDCoordinatore.Text = accertamento.IDUtenteCoordinatore.ToString();
                lblIDCodiceCatastale.Text = accertamento.IDCodiceCatastale.ToString();
                lblCodiceAccertamento.Text = accertamento.CodiceAccertamento;
                lblDataAccertamento.Text = string.Format("{0:dd/MM/yyyy}", accertamento.DataRilevazione);
                lblAccertatore.Text = accertamento.Accertatore;
                if (accertamento.IDUtenteCoordinatore != null)
                {
                    rowCoordinatore.Visible = true;
                    lblCoordinatore.Text = accertamento.Coordinatore;
                }
                else
                {
                    rowCoordinatore.Visible = false;
                }

                lblIDAgenteAccertatore.Text = accertamento.IDUtenteAgenteAccertatore.ToString();
                if (accertamento.IDUtenteAgenteAccertatore != null)
                {
                    rowAgenteAccertatore.Visible = true;
                    lblAgenteAccertatore.Text = accertamento.AgenteAccertatore;
                }
                else
                {
                    rowAgenteAccertatore.Visible = false;
                }

                txtNote.Text = accertamento.Note;
                lblStatoAccertamento.Text = accertamento.StatoAccertamento;
                lblIDStatoAccertamento.Text = accertamento.IDStatoAccertamento.ToString();

                lblfEmailConfermaAccertamento.Text = accertamento.fEmailConfermaAccertamento.ToString();
                lblDataInvioEmail.Text = accertamento.DataInvioEmail.ToString();
                if (!string.IsNullOrEmpty(accertamento.TestoEmail))
                {
                    txtTestoEmail.Text = accertamento.TestoEmail;
                }
                
                if (accertamento.fEmailConfermaAccertamento)
                {
                    rowEmailConfermaAccertamento.Visible = true;
                    lblEmailConfermaAccertamento.Text = "Email inviata per conferma accertamento in data " + string.Format("{0:dd/MM/yyyy hh:mm}", accertamento.DataInvioEmail);
                }
                else
                {
                    rowEmailConfermaAccertamento.Visible = false;
                }

                if (accertamento.RispostaEmail != null)
                {
                    rowEmailRispostaAccertamento.Visible = true;
                    lblEmailRispostaAccertamento.Text = accertamento.RispostaEmail;
                }
                else
                {
                    rowEmailRispostaAccertamento.Visible = false;
                }

                lblImpresaManutenzione.Text = accertamento.CodiceSoggetto + "&nbsp;-&nbsp;" + accertamento.NomeAzienda;
                lblImpresaManutenzioneIndirizzo.Text = accertamento.IndirizzoAzienda;
                lblImpresaManutenzioneTelefono.Text = accertamento.Telefono;
                lblImpresaManutenzioneEmail.Text = accertamento.Email;
                lblImpresaManutenzioneEmailPec.Text = accertamento.EmailPec;
                
                lblDatiImpiantoCodiceTargatura.Text = accertamento.CodiceTargatura;

                //string ragioneSocialeResponsabile = string.Empty;
                //if (!string.IsNullOrEmpty(accertamento.RagioneSocialeResponsabile))
                //{
                //    ragioneSocialeResponsabile = "Ragione Sociale: " + accertamento.RagioneSocialeResponsabile + "<br/>";
                //}
                //lblResponsabile.Text = ragioneSocialeResponsabile +  accertamento.NomeResponsabile + "&nbsp;-&nbsp;" + accertamento.CognomeResponsabile;

                if (accertamento.fTerzoResponsabile != null)
                {
                    if ((bool)accertamento.fTerzoResponsabile)
                    {
                        rowTerzoResponsabile.Visible = true;
                        lblTerzoResponsabile.Text = accertamento.RagioneSocialeTerzoResponsabile + " " + accertamento.IndirizzoTerzoResponsabile + " " + accertamento.CivicoTerzoResponsabile + " " + accertamento.ComuneTerzoResponsabile;
                    }
                    else
                    {
                        rowTerzoResponsabile.Visible = false;
                    }
                }
                else
                {
                    rowTerzoResponsabile.Visible = false;
                }

                //03052021: Disattivato per nuova logica Cap
                //lblResponsabileIndirizzo.Text = accertamento.IndirizzoResponsabile + "&nbsp;" + accertamento.CapResponsabile + "&nbsp;" + accertamento.ComuneResponsabile + "&nbsp;(" + accertamento.ProvinciaResponsabile + ")&nbsp; - Italia";
                //lblResponsabileIDComune.Text = accertamento.IDComuneResponsabile.ToString();
                //if (String.IsNullOrEmpty(accertamento.CapResponsabile))
                //{
                //    string strHTML = "<br /> Cap:";
                //    lblResponsabileCap.Text = Server.HtmlDecode(strHTML);
                //    lblResponsabileCap.Visible = true;
                //    txtResponsabileCap.Visible = true;
                //    btnUpdCapResponsabile.Visible = true;
                //}
                //else
                //{
                //    lblResponsabileCap.Visible = false;
                //    txtResponsabileCap.Visible = false;
                //    btnUpdCapResponsabile.Visible = false;
                //}
                UCGoogleAutosuggest.IDLibrettoImpianto = accertamento.IDLibrettoImpianto.ToString();

                int? iDLibreImpiantoAttivo = UtilityLibrettiImpianti.GetLastLibrettoImpianto(accertamento.IDTargaturaImpianto);
                if (iDLibreImpiantoAttivo != null)
                {
                    QueryString qsLibretto = new QueryString();
                    qsLibretto.Add("IDLibrettoImpianto", iDLibreImpiantoAttivo.ToString());
                    QueryString qsEncryptedLibretto = Encryption.EncryptQueryString(qsLibretto);

                    string urlLibretto = "LIM_LibrettiImpianti.aspx";
                    urlLibretto += qsEncryptedLibretto.ToString();
                    btnViewLibrettoImpianto.NavigateUrl = urlLibretto;
                }
                else
                {
                    btnViewLibrettoImpianto.NavigateUrl = "#";
                }

                lblPod.Text = ctx.LIM_LibrettiImpianti.Where(a => a.IDLibrettoImpianto == iDLibreImpiantoAttivo).FirstOrDefault().NumeroPOD;
                lblPdr.Text = ctx.LIM_LibrettiImpianti.Where(a => a.IDLibrettoImpianto == iDLibreImpiantoAttivo).FirstOrDefault().NumeroPDR;

                lblPotenzaTermicaNominale.Text = accertamento.PotenzaTermicaNominale.ToString();
                lblTipologiaCombustibile.Text = accertamento.TipologiaCombustibile;
                lblIDTipologiaCombustibile.Text = accertamento.IDTipologiaCombustibile.ToString();

                GetRapportiControllo(accertamento.IDTargaturaImpianto, accertamento.Prefisso, accertamento.CodiceProgressivo);

                //lblComune.Text = ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == accertamento.IDCodiceCatastale).FirstOrDefault().Comune;
                lblIndirizzo.Text = accertamento.Indirizzo + " " + accertamento.Civico + " " + ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == accertamento.IDCodiceCatastale).FirstOrDefault().Comune + " (" + accertamento.SiglaProvincia + ")";


                lblEmailPecComune.Text = string.Format("{0}{1}", "<b>Email Pec Sindaco</b>:&nbsp;", accertamento.EmailPecComune);
                ddTipologiaDistributore(ddlTipologiaDistributore, null, accertamento.IDCodiceCatastale);
                if (accertamento.IDDistributore != null)
                {
                    ddlTipologiaDistributore.SelectedValue = accertamento.IDDistributore.ToString();
                }
                else
                {
                    ddlTipologiaDistributore.SelectedValue = "0";
                }

                if (!string.IsNullOrEmpty(accertamento.Osservazioni))
                {
                    imgEsitoControlloOsservazioni.ImageUrl = "~/images/No.png";
                }
                else
                {
                    imgEsitoControlloOsservazioni.ImageUrl = "~/images/Si.png";
                }

                if (!string.IsNullOrEmpty(accertamento.Prescrizioni))
                {
                    
                    imgEsitoControlloPrescrizioni.ImageUrl = "~/images/No.png";
                }
                else
                {
                    imgEsitoControlloPrescrizioni.ImageUrl = "~/images/Si.png";
                }

                if (!string.IsNullOrEmpty(accertamento.Raccomandazioni))
                {
                    imgEsitoControlloRaccomandazioni.ImageUrl = "~/images/No.png";
                }
                else
                {
                    imgEsitoControlloRaccomandazioni.ImageUrl = "~/images/Si.png";
                }

                if (accertamento.GiorniRealizzazioneInterventi != null)
                {
                    txtGiorniRealizzazioneInterventi.Text = accertamento.GiorniRealizzazioneInterventi.ToString();
                }

                if (accertamento.IDIspezione != null)
                {
                    long iDIspezioneVisita = UtilityVerifiche.GetIDIspezioneVisitaFromVerifica(long.Parse(accertamento.IDIspezione.ToString()));

                    QueryString qsIspezione = new QueryString();
                    qsIspezione.Add("IDIspezione", accertamento.IDIspezione.ToString());
                    qsIspezione.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
                    QueryString qsEncryptedIspezione = Encryption.EncryptQueryString(qsIspezione);

                    string urlIspezione = "VER_Ispezioni.aspx";
                    urlIspezione += qsEncryptedIspezione.ToString();
                    btnViewIspezione.NavigateUrl = urlIspezione;
                }

                UCChangeResponsabileLibretto.IDLibrettoImpianto = accertamento.IDLibrettoImpianto.ToString();
                UCChangeResponsabileLibretto.IDTargaturaImpianto = accertamento.IDTargaturaImpianto.ToString();
                UCChangeResponsabileLibretto.IDAccertamento = accertamento.IDAccertamento.ToString();

                #region Osservazioni
                GetNonConformita(accertamento.IDAccertamento, "OSS");
                #endregion

                #region Raccomandazioni
                GetNonConformita(accertamento.IDAccertamento, "RACC");
                #endregion

                #region Prescrizioni
                GetNonConformita(accertamento.IDAccertamento, "PRES");
                #endregion

                #region Impianto non funzionante
                GetNonConformita(accertamento.IDAccertamento, "INF");
                #endregion

                LogicStatiAccertamento(iDAccertamento, accertamento.IDStatoAccertamento.ToString(), accertamento.fEmailConfermaAccertamento, accertamento.DataInvioEmail);
                SetVisibleHiddenImpiantoFunzionanteConferma();
            }
        }
    }

    public void VisibleHiddenTipoAccertamento(string iDAccertamento, string iDTipoAccertamento)
    {
        switch (iDTipoAccertamento)
        {
            case "1":
                //rowSanzioni.Visible = false;

                lblTitoloDatiRCTIspezione.Text = "Dati Rapporto di Controllo Tecnico";
                VER_Accertamenti_lblRapportoControlloTecnico.Text = "Dati Rapporto di Controllo";
                btnViewIspezione.Visible = false;

                lblTitoloDatiNonConformita.Text = "Non conformità Rapporto di Controllo Tecnico";

                rowTipoIspezione.Visible = false;
                rowDatiIspettore.Visible = false;
                break;
            case "2":
                //if (info.IDRuolo == 6)
                //{
                //    rowSanzioni.Visible = false;
                //}
                //else
                //{
                //    rowSanzioni.Visible = true;
                //    GetSanzioniUserControll(iDAccertamento, iDTipoAccertamento);
                //}
                lblTitoloDatiRCTIspezione.Text = "Dati Ispezione";
                VER_Accertamenti_lblRapportoControlloTecnico.Text = "Dati Ispezione";
                dgRapporti.Visible = false;
                btnViewIspezione.Visible = true;

                lblTitoloDatiNonConformita.Text = "Non conformità Rapporto di Ispezione";

                rowTestoConfermaEmailIntestazione.Visible = false;
                rowTestoConfermaEmail.Visible = false;
                rowTipoIspezione.Visible = true;
                rowDatiIspettore.Visible = true;
                
                break;
        }
    }

    protected void GetNonConformita(long iDAccertamento, string tipo)
    {
        int? iDProceduraRacc = null;
        int? iDProceduraPres = null;

        using (var ctx = new CriterDataModel())
        {
            switch (tipo)
            {
                case "OSS":
                    var osservazioni = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento && c.Tipo == tipo).ToList();
                    dgOsservazioni.DataSource = osservazioni;
                    dgOsservazioni.DataBind();
                    break;
                case "RACC":
                    var raccomandazioni = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento && c.Tipo == tipo).ToList();
                    dgRaccomandazioni.DataSource = raccomandazioni;
                    dgRaccomandazioni.DataBind();
                                        
                    bool fVisiblePdfRaccomandazioni = false;
                    for (int i = 0; i < dgRaccomandazioni.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)dgRaccomandazioni.Items[i].Cells[5].FindControl("chkRaccomandazioneConferma");
                        if (chk.Checked)
                        {
                            fVisiblePdfRaccomandazioni = true;
                            break;
                        }
                    }
                    
                    if (fVisiblePdfRaccomandazioni)
                    {
                        //Correttivo del 22/12/2020
                        if (raccomandazioni.Where(a => a.IDProceduraAccertamento != null).Any())
                        {
                            iDProceduraRacc = raccomandazioni.Where(a => a.IDProceduraAccertamento != null).Distinct().FirstOrDefault().IDProceduraAccertamento.Value;
                            var procedura = ctx.SYS_ProceduraAccertamento.Where(c => c.IDProceduraAccertamento == iDProceduraRacc).FirstOrDefault();
                            if (procedura != null)
                            {
                                lblProceduraRaccomandazioni.Visible = true;
                                lblProceduraRaccomandazioni.Text = procedura.ProceduraAccertamento;
                                ImgPdfRaccomandazioni.Visible = true;
                                ImgPdfRaccomandazioni.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Accertamento_" + IDAccertamento + "', 'iframe', 'VER_AccertamentiViewer.aspx?IDAccertamento=" + IDAccertamento + "&IDProceduraAccertamento=" + procedura.IDProceduraAccertamento.ToString() + "&CodiceAccertamento=" + lblCodiceAccertamento.Text + "&IDTipoAccertamento="+ lblIDTipoAccertamento.Text +"', 'Accertamento_" + IDAccertamento + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
                                ImgPdfRaccomandazioni.Attributes.Add("onmouseout", "this.style.cursor='pointer'");
                            }
                            else
                            {
                                ImgPdfRaccomandazioni.Visible = false;
                            }
                        }
                        else
                        {
                            var accertamento = ctx.VER_Accertamento.Where(a => a.IDAccertamento == iDAccertamento).FirstOrDefault();

                            iDProceduraRacc = UtilityVerifiche.GetProcedureAccertamento(ctx, accertamento.IDRapportoDiControlloTecnicoBase, accertamento.IDIspezione, (int)accertamento.IDTipoAccertamento, tipo);

                            //Salvo la procedura
                            var proceduraSave = raccomandazioni.FirstOrDefault();
                            proceduraSave.IDProceduraAccertamento = iDProceduraRacc;
                            ctx.SaveChanges();

                            var procedura = ctx.SYS_ProceduraAccertamento.Where(c => c.IDProceduraAccertamento == iDProceduraRacc).FirstOrDefault();
                            if (procedura != null)
                            {
                                lblProceduraRaccomandazioni.Visible = true;
                                lblProceduraRaccomandazioni.Text = procedura.ProceduraAccertamento;
                                ImgPdfRaccomandazioni.Visible = true;
                                ImgPdfRaccomandazioni.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Accertamento_" + IDAccertamento + "', 'iframe', 'VER_AccertamentiViewer.aspx?IDAccertamento=" + IDAccertamento + "&IDProceduraAccertamento=" + procedura.IDProceduraAccertamento.ToString() + "&CodiceAccertamento=" + lblCodiceAccertamento.Text + "&IDTipoAccertamento=" + lblIDTipoAccertamento.Text + "', 'Accertamento_" + IDAccertamento + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
                                ImgPdfRaccomandazioni.Attributes.Add("onmouseout", "this.style.cursor='pointer'");
                            }
                            else
                            {
                                ImgPdfRaccomandazioni.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        ImgPdfRaccomandazioni.Visible = false;
                        lblProceduraRaccomandazioni.Visible = false;
                    }
                    break;
                case "INF":
                    var impiantoNonFunzionate = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento && c.Tipo == tipo).FirstOrDefault();
                    if (impiantoNonFunzionate != null)
                    {
                        lblIDNonConformita.Text = impiantoNonFunzionate.IDNonConformita.ToString();
                        //rblImpiantoFunzionanteConferma.Items.FindByValue(impiantoNonFunzionate.fImpiantoFunzionanteConferma.ToString()).Selected = true;
                        //chkImpiantoFunzionanteConferma.Checked = impiantoNonFunzionate.fImpiantoFunzionanteConferma;
                        rblImpiantoFunzionanteConferma.SelectedValue = impiantoNonFunzionate.fImpiantoFunzionanteConferma.ToString();
                        lblImpiantoNonFUnzionate.Text = "(" + UtilityApp.ToSiNo(impiantoNonFunzionate.fImpiantoFunzionanteRct.ToString()) + ")";
                        if (impiantoNonFunzionate.fImpiantoFunzionanteRct)
                        {
                            imgEsitoControlloImpiantoNonFunzionante.ImageUrl = "~/images/Si.png";
                            //txtNoteImpiantoFunzionante.Visible = false;
                        }
                        else
                        {
                            imgEsitoControlloImpiantoNonFunzionante.ImageUrl = "~/images/No.png";
                        }

                        txtNoteImpiantoFunzionante.Text = impiantoNonFunzionate.ImpiantoFunzionante;

                        rbTipologiaImpiantoFunzionanteAccertamento(rblTipologiaImpiantoFunzionanteAccertamento, null);
                        if (!string.IsNullOrEmpty(impiantoNonFunzionate.IDTipologiaImpiantoFunzionanteAccertamento.ToString()))
                        {
                            rblTipologiaImpiantoFunzionanteAccertamento.SelectedValue = impiantoNonFunzionate.IDTipologiaImpiantoFunzionanteAccertamento.ToString();
                        }

                        rbTipologiaRisoluzioneAccertamento(rblTipologiaRisoluzioneImpiantoFunzionante, null, true);
                        rblTipologiaRisoluzioneImpiantoFunzionante.SelectedValue = impiantoNonFunzionate.IDTipologiaRisoluzioneAccertamento.ToString();

                        if (impiantoNonFunzionate.Giorni != null)
                        {
                            txtGiorniImpiantoFunzionante.Text = impiantoNonFunzionate.Giorni.ToString();
                        }


                        SetVisibleHiddenTipologiaImpiantoFunzionanteAccertamento(impiantoNonFunzionate.fImpiantoFunzionanteConferma);
                        //SetVisibleHiddenNoteImpiantoFunzionante(impiantoNonFunzionate.IDTipologiaImpiantoFunzionanteAccertamento.ToString());
                    }
                    else
                    {
                        //CICCIO: Correttivo del 01/02/2021 - Far in modo che sulla tabella delle NC ci sia sempre la riga INF
                        var nonConformitaImpiantoNonFunzionante = new VER_AccertamentoNonConformita();
                        nonConformitaImpiantoNonFunzionante.IDAccertamento = iDAccertamento;
                        nonConformitaImpiantoNonFunzionante.Tipo = "INF";
                        nonConformitaImpiantoNonFunzionante.IDProceduraAccertamento = null;
                        nonConformitaImpiantoNonFunzionante.fRaccomandazioneConferma = false;
                        nonConformitaImpiantoNonFunzionante.RaccomandazioneRct = null;
                        nonConformitaImpiantoNonFunzionante.Raccomandazione = null;

                        nonConformitaImpiantoNonFunzionante.PrescrizioneRct = null;
                        nonConformitaImpiantoNonFunzionante.fPrescrizioneConferma = false;
                        nonConformitaImpiantoNonFunzionante.Prescrizione = null;
                        nonConformitaImpiantoNonFunzionante.OsservazioneRct = null;
                        nonConformitaImpiantoNonFunzionante.fOsservazioneConferma = false;
                        nonConformitaImpiantoNonFunzionante.Osservazione = null;
                        nonConformitaImpiantoNonFunzionante.fImpiantoFunzionanteRct = false;
                        nonConformitaImpiantoNonFunzionante.fImpiantoFunzionanteConferma = true;
                        nonConformitaImpiantoNonFunzionante.ImpiantoFunzionante = null;
                        nonConformitaImpiantoNonFunzionante.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaImpiantoNonFunzionante.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaImpiantoNonFunzionante.IDTipologiaEventoAccertamento = null;
                        nonConformitaImpiantoNonFunzionante.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaImpiantoNonFunzionante);
                        ctx.SaveChanges();

                    }
                    break;
                case "PRES":
                    var prescrizioni = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento && c.Tipo == tipo).ToList();
                    dgPrescrizioni.DataSource = prescrizioni;
                    dgPrescrizioni.DataBind();

                    bool fVisiblePdfPrescrizioni = false;
                    for (int i = 0; i < dgPrescrizioni.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)dgPrescrizioni.Items[i].Cells[3].FindControl("chkPrescrizioneConferma");
                        if (chk.Checked)
                        {
                            fVisiblePdfPrescrizioni = true;
                            break;
                        }
                    }

                    if (fVisiblePdfPrescrizioni)
                    {
                        //Correttivo del 22/12/2020
                        if (prescrizioni.Where(a => a.IDProceduraAccertamento != null).Any())
                        {
                            iDProceduraPres = prescrizioni.Where(a => a.IDProceduraAccertamento != null).Distinct().FirstOrDefault().IDProceduraAccertamento.Value;
                            var procedura = ctx.SYS_ProceduraAccertamento.Where(c => c.IDProceduraAccertamento == iDProceduraPres).FirstOrDefault();
                            if (procedura != null)
                            {
                                lblProceduraPrescrizioni.Visible = true;
                                lblProceduraPrescrizioni.Text = procedura.ProceduraAccertamento;
                                ImgPdfPrescrizioni.Visible = true;
                                ImgPdfPrescrizioni.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Accertamento_" + IDAccertamento + "', 'iframe', 'VER_AccertamentiViewer.aspx?IDAccertamento=" + IDAccertamento + "&IDProceduraAccertamento=" + procedura.IDProceduraAccertamento + "&CodiceAccertamento=" + lblCodiceAccertamento.Text + "&IDTipoAccertamento=" + lblIDTipoAccertamento.Text + "', 'Accertamento_" + IDAccertamento + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
                                ImgPdfPrescrizioni.Attributes.Add("onmouseout", "this.style.cursor='pointer'");
                            }
                            else
                            {
                                ImgPdfPrescrizioni.Visible = false;
                            }
                        }
                        else
                        {
                            var accertamento = ctx.VER_Accertamento.Where(a => a.IDAccertamento == iDAccertamento).FirstOrDefault();

                            iDProceduraPres = UtilityVerifiche.GetProcedureAccertamento(ctx, accertamento.IDRapportoDiControlloTecnicoBase, accertamento.IDIspezione, (int)accertamento.IDTipoAccertamento, tipo);

                            //Salvo la procedura
                            var proceduraSave = prescrizioni.FirstOrDefault();
                            proceduraSave.IDProceduraAccertamento = iDProceduraPres;
                            ctx.SaveChanges();

                            var procedura = ctx.SYS_ProceduraAccertamento.Where(c => c.IDProceduraAccertamento == iDProceduraPres).FirstOrDefault();
                            if (procedura != null)
                            {
                                lblProceduraPrescrizioni.Visible = true;
                                lblProceduraPrescrizioni.Text = procedura.ProceduraAccertamento;
                                ImgPdfPrescrizioni.Visible = true;
                                ImgPdfPrescrizioni.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Accertamento_" + IDAccertamento + "', 'iframe', 'VER_AccertamentiViewer.aspx?IDAccertamento=" + IDAccertamento + "&IDProceduraAccertamento=" + procedura.IDProceduraAccertamento + "&CodiceAccertamento=" + lblCodiceAccertamento.Text + "&IDTipoAccertamento=" + lblIDTipoAccertamento.Text + "', 'Accertamento_" + IDAccertamento + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
                                ImgPdfPrescrizioni.Attributes.Add("onmouseout", "this.style.cursor='pointer'");
                            }
                            else
                            {
                                ImgPdfPrescrizioni.Visible = false;
                            }
                        }
                        
                        //Visualizzazione combo distributori
                        if (((iDProceduraPres == 1) || (iDProceduraPres == 2) || (iDProceduraPres == 5) || (iDProceduraPres == 6)) && ((lblIDTipologiaCombustibile.Text =="2" || lblIDTipologiaCombustibile.Text == "3")))
                        {
                            rowDistributore.Visible = true;
                            rowDistributore0.Visible = true;
                        }
                        else
                        {
                            rowDistributore.Visible = false;
                            rowDistributore0.Visible = false;
                        }
                    }
                    else
                    {
                        ImgPdfPrescrizioni.Visible = false;
                        lblProceduraPrescrizioni.Visible = false;
                        rowDistributore.Visible = false;
                        rowDistributore0.Visible = false;
                    }
                    break;
            }
        }
    }

    public void GetDatiAccertamentoStorico(long iDAccertamento)
    {
        var accertamentoStorico = UtilityVerifiche.GetAccertamentoStorico(iDAccertamento, true);
        if (accertamentoStorico != null)
        {
            DataGrid.DataSource = accertamentoStorico;
            DataGrid.DataBind();
        }
    }

    public void LogicStatiAccertamento(long iDAccertamento, string iDStatoAccertamento, bool fEmailConfermaAccertamento, DateTime? dataInvioEmail)
    {
        switch (iDStatoAccertamento)
        {
            case "1": //In attesa di accertamento

                break;
            case "2": //Assegnato ad accertatore
            case "7"://Accertamento sospeso in attesa di conferma
                if (info.IDRuolo == 1)
                {
                    btnSaveAccertamento.Visible = true;
                    if (fEmailConfermaAccertamento)
                    {
                        DateTime dtInvioEmail = DateTime.Parse(dataInvioEmail.ToString());
                        if (DateTime.Now >= dtInvioEmail.AddHours(24))
                        {
                            btnInviaACoordinatore.Visible = true;
                        }
                        else
                        {
                            btnInviaACoordinatore.Visible = false;
                        }
                        btnInviaEmailConfermaAccertamento.Visible = false;
                    }
                    else
                    {
                        btnInviaACoordinatore.Visible = true;
                        btnInviaEmailConfermaAccertamento.Visible = true;
                    }
                    btnNewOsservazione.Visible = true;
                    btnNewRaccomandazione.Visible = true;
                    btnNewPrescrizione.Visible = true;
                    rowSavePartial.Visible = true;
                }
                else if(info.IDRuolo == 6 || info.IDRuolo ==7 || info.IDRuolo == 14 || info.IDRuolo == 17) // Aggiunta || info.IDRuolo == 17 - Agente ad accertamento
                {
                    if (fEmailConfermaAccertamento)
                    {
                        DateTime dtInvioEmail = DateTime.Parse(dataInvioEmail.ToString());
                        if (DateTime.Now >= dtInvioEmail.AddHours(24))
                        {
                            btnInviaACoordinatore.Visible = true;
                        }
                        else
                        {
                            btnInviaACoordinatore.Visible = false;
                            EnabledDisabledControl(false);
                        }
                        btnInviaEmailConfermaAccertamento.Visible = false;
                    }
                    else
                    {
                        if (info.IDRuolo == 6)
                        {
                            btnInviaACoordinatore.Visible = true;
                        }
                        if (info.IDRuolo == 7)
                        {
                            btnRimandaAdAccertatore.Visible = false;
                            btnAccertamentoNonInviato.Visible = false;
                            btnAccertamentoRigettato.Visible = false;
                            btnInviaEmailConfermaAccertamento.Visible = false;
                            btnInviaAdAgenteAccertatore.Visible = false;
                        }
                    }
                    btnSaveAccertamento.Visible = true;
                    btnNewOsservazione.Visible = true;
                    btnNewRaccomandazione.Visible = true;
                    btnNewPrescrizione.Visible = true;
                    rowSavePartial.Visible = true;
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;
                }
                rowGiorniRealizzazioneInterventi.Visible = false;
                break;
            case "3": //In attesa di verifica da parte del Coordinatore
                if (info.IDRuolo == 1)
                {
                    btnSaveAccertamento.Visible = true;
                }
                else if ((info.IDRuolo == 6) || (info.IDRuolo == 17))
                {
                    EnabledDisabledControl(false);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = false;
                    btnNewOsservazione.Visible = false;
                    btnNewRaccomandazione.Visible = false;
                    btnNewPrescrizione.Visible = false;
                    rowSavePartial.Visible = false;
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;
                }
                rowGiorniRealizzazioneInterventi.Visible = false;
                break;
            case "4": //Assegnato a coordinatore
                if ((info.IDRuolo == 6) || (info.IDRuolo == 17)) // Accertatore / Agente Accertatore
                {
                    EnabledDisabledControl(false);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = false;
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;

                    btnNewOsservazione.Visible = false;
                    btnNewRaccomandazione.Visible = false;
                    btnNewPrescrizione.Visible = false;
                    rowSavePartial.Visible = false;
                }
                else if ((info.IDRuolo == 1)) // Amministratore Criter
                {
                    EnabledDisabledControl(true);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = true;
                    btnNewOsservazione.Visible = true;
                    btnNewRaccomandazione.Visible = true;
                    btnNewPrescrizione.Visible = true;
                    rowSavePartial.Visible = true;
                    //btnInviaInFirma.Visible = true; // 16/10/2020 solo il Agente ad Accertamento può inviare in firma
                    btnInviaInFirma.Visible = false; // - cosa devo fare per admin ?
                    btnInviaAdAgenteAccertatore.Visible = true;
                    btnRimandaACoordinatore.Visible = false;
                    btnRimandaAdAgenteAccertatore.Visible = false;
                }
                else if((info.IDRuolo == 7) || (info.IDRuolo == 14)) // 7 = Coordinatore ,  14 = Coordinatore/Ispettore
                {
                    EnabledDisabledControl(true);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = true;
                    btnNewOsservazione.Visible = true;
                    btnNewRaccomandazione.Visible = true;
                    btnNewPrescrizione.Visible = true;
                    rowSavePartial.Visible = true;
                    //btnInviaInFirma.Visible = true; // 16/10/2020 solo il Agente ad Accertamento può inviare in firma / e admin?
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;
                    btnInviaAdAgenteAccertatore.Visible = true;
                    btnRimandaAdAgenteAccertatore.Visible = false;
                    if ((info.IDRuolo == 7) || (info.IDRuolo == 14))
                    {
                        btnRimandaAdAccertatore.Visible = true;
                        btnAccertamentoNonInviato.Visible = true;
                        btnAccertamentoRigettato.Visible = true;
                        if (info.IDRuolo == 7)
                        {
                            if (!fEmailConfermaAccertamento)
                            {
                                btnInviaEmailConfermaAccertamento.Visible = true;
                            }
                        }
                    }
                }               
                rowGiorniRealizzazioneInterventi.Visible = true;
                break;
            case "5": //Accertamento concluso
                if ((info.IDRuolo == 1) || (info.IDRuolo == 6) || (info.IDRuolo == 7) || (info.IDRuolo == 14) || (info.IDRuolo == 17))
                {
                    EnabledDisabledControl(false);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = false;
                    btnNewOsservazione.Visible = false;
                    btnNewRaccomandazione.Visible = false;
                    btnNewPrescrizione.Visible = false;
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;
                    btnInviaAdAgenteAccertatore.Visible = false;
                    btnRimandaAdAgenteAccertatore.Visible = false;
                }
                rowGiorniRealizzazioneInterventi.Visible = true;
                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;
            case "6": //Accertamento in attesa di firma
                if (info.IDRuolo == 1)
                {
                    EnabledDisabledControl(false);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = false;
                    btnNewOsservazione.Visible = false;
                    btnNewRaccomandazione.Visible = false;
                    btnNewPrescrizione.Visible = false;
                    rowSavePartial.Visible = false;
                    btnInviaInFirma.Visible = false;
                    //btnRimandaACoordinatore.Visible = true;
                    btnRimandaACoordinatore.Visible = false;
                    btnRimandaAdAgenteAccertatore.Visible = true;
                }
                else if ((info.IDRuolo == 6) || (info.IDRuolo == 7) || (info.IDRuolo == 14) || (info.IDRuolo == 17))
                {
                    EnabledDisabledControl(false);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = false;
                    btnNewOsservazione.Visible = false;
                    btnNewRaccomandazione.Visible = false;
                    btnNewPrescrizione.Visible = false;
                    rowSavePartial.Visible = false;
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;
                }
                rowGiorniRealizzazioneInterventi.Visible = true;
                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;
            case "8": //In attesa di verifica da parte dell'Agente Accertatore
                if (info.IDRuolo == 1)
                {
                    btnSaveAccertamento.Visible = true;
                    btnRimandaACoordinatore.Visible = true;
                }
                else if ((info.IDRuolo == 6) || (info.IDRuolo == 7) || (info.IDRuolo == 14) || (info.IDRuolo == 17))
                {
                    EnabledDisabledControl(false);
                    btnInviaACoordinatore.Visible = false;
                    btnInviaAdAgenteAccertatore.Visible = false;
                    btnRimandaAdAgenteAccertatore.Visible = false;
                    btnSaveAccertamento.Visible = false;
                    btnNewOsservazione.Visible = false;
                    btnNewRaccomandazione.Visible = false;
                    btnNewPrescrizione.Visible = false;
                    rowSavePartial.Visible = false;
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;

                    btnRimandaAdAccertatore.Visible = false;
                    btnAccertamentoNonInviato.Visible = false;
                    btnAccertamentoRigettato.Visible = false;
                    btnInviaEmailConfermaAccertamento.Visible = false;
                    btnInviaAdAgenteAccertatore.Visible = false;
                    btnInviaEmailConfermaAccertamento.Visible = false;

                }
                rowGiorniRealizzazioneInterventi.Visible = false;
                break;
            case "9": //Assegnato ad Agente Accertatore
                if ((info.IDRuolo == 6) || (info.IDRuolo == 7) || (info.IDRuolo == 14)) // Accertatore,Coordinatore,Coordinatore/Ispettore
                {
                    EnabledDisabledControl(false);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = false;
                    btnInviaInFirma.Visible = false;
                    btnRimandaACoordinatore.Visible = false;
                    btnRimandaAdAgenteAccertatore.Visible = false;
                    btnInviaAdAgenteAccertatore.Visible = false;
                    //Coordinatore non riesce modificare nulla dopo che è stato assegnato ad agente accertatore
                }
                else if (info.IDRuolo == 1) // Amministratore Criter
                {
                    EnabledDisabledControl(true);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = true;
                    btnNewOsservazione.Visible = true;
                    btnNewRaccomandazione.Visible = true;
                    btnNewPrescrizione.Visible = true;
                    rowSavePartial.Visible = true;
                    btnInviaInFirma.Visible = true; // 16/10/2020 solo il Agente ad Accertamento può inviare in firma / lascio true
                    btnInviaAdAgenteAccertatore.Visible = false;
                    btnRimandaACoordinatore.Visible = true;
                    btnRimandaAdAgenteAccertatore.Visible = false;
                }
                else if (info.IDRuolo == 17) // Agente Accertatore
                {
                    EnabledDisabledControl(true);
                    btnInviaACoordinatore.Visible = false;
                    btnSaveAccertamento.Visible = true;
                    btnNewOsservazione.Visible = true;
                    btnNewRaccomandazione.Visible = true;
                    btnNewPrescrizione.Visible = true;
                    rowSavePartial.Visible = true;
                    btnInviaInFirma.Visible = true; // 16/10/2020 solo il Agente ad Accertamento può inviare in firma
                    btnRimandaACoordinatore.Visible = true;
                    btnInviaAdAgenteAccertatore.Visible = false;
                    btnRimandaAdAgenteAccertatore.Visible = false;
                }
                rowGiorniRealizzazioneInterventi.Visible = true;
                break;
            case "10": //Accertamento non inviato
            case "11": //Accertamento rigettato
                EnabledDisabledControl(true);
                btnInviaACoordinatore.Visible = false;
                btnSaveAccertamento.Visible = false;
                btnNewOsservazione.Visible = false;
                btnNewRaccomandazione.Visible = false;
                btnNewPrescrizione.Visible = false;
                btnInviaInFirma.Visible = false;
                btnRimandaACoordinatore.Visible = true;
                btnRimandaAdAgenteAccertatore.Visible = false;
                btnRimandaAdAccertatore.Visible = false;
                btnAccertamentoNonInviato.Visible = false;
                btnAccertamentoRigettato.Visible = false;
                btnInviaEmailConfermaAccertamento.Visible = false;
                btnInviaAdAgenteAccertatore.Visible = false;
                break;
        }
    }

    protected void EnabledDisabledControl(bool fEnabled)
    {
        txtNote.Enabled = fEnabled;
        grdPrincipale.SettingsDataSecurity.AllowInsert = grdPrincipale.SettingsDataSecurity.AllowEdit = grdPrincipale.SettingsDataSecurity.AllowDelete = fEnabled;

        dgOsservazioni.Enabled = fEnabled;
        dgRaccomandazioni.Enabled = fEnabled;
        dgPrescrizioni.Enabled = fEnabled;
        //rblImpiantoFunzionanteConferma.Enabled = fEnabled;
        rblTipologiaImpiantoFunzionanteAccertamento.Enabled = fEnabled;
        txtNoteImpiantoFunzionante.Enabled = fEnabled;
        ddlTipologiaDistributore.Enabled = fEnabled;
        txtGiorniRealizzazioneInterventi.Enabled = fEnabled;
        txtTestoEmail.Enabled = fEnabled;
        txtGiorniImpiantoFunzionante.Enabled = fEnabled;
    }

    #region Sanzioni
    //protected void GetSanzioniUserControll(string iDAccertamento, string iDTipoAccertamento)
    //{
    //    UCSanzione.IDAccertamento = iDAccertamento;
    //    UCSanzione.TipoPageSanzione = "2";
    //    //UCSanzione.UserControlButtonClicked += new
    //    //            EventHandler(UCSanzione_UserControlButtonClicked);
    //}
    #endregion

    #region Accertamenti Note
    private CriterDataModel _CurrentDataContext;

    public CriterDataModel CurrentDataContext
    {
        get
        {
            if (_CurrentDataContext == null)
            {
                _CurrentDataContext = DataLayer.Common.ApplicationContext.Current.Context;
            }
            return _CurrentDataContext;
        }
    }

    protected bool IsDraftElementoGridPrincipale(int itemId)
    {
        return CurrentDataContext.VER_AccertamentoNote.Find(itemId).IDAccertamento == long.Parse(IDAccertamento);
    }

    protected void DetailGrid_DataBound(object sender, EventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        //if (!Enabled)
        //{
        //    EnableGridEditing(gridView, Enabled);
        //}

        //2016-07-28 espansione di tutte le righe di dettaglio
        gridView.DetailRows.ExpandAllRows();
        //2016-07-28 espansione di tutte le righe di dettaglio
    }

    protected void DetailGrid_DetailRowGetButtonVisibility(object sender, DevExpress.Web.ASPxGridViewDetailRowButtonEventArgs e)
    {
        if (IsDraftElementoGridPrincipale(Convert.ToInt32(e.KeyValue)))
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }

    protected void grdPrincipale_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        e.NewValues["IDAccertamento"] = IDAccertamento;
        e.NewValues["IDUtente"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        //e.NewValues["Data"] = DateTime.Now;
        //e.NewValues["Nota"] = "sasassas";
    }

    protected void grdPrincipale_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        //e.NewValues["IDUtenteUltimaModifica"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        //e.NewValues["DataUltimaModifica"] = DateTime.Now;
    }

    protected void grdPrincipale_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsGridPrincipale.WhereParameters["IDAccertamento"].DefaultValue = IDAccertamento.ToString();
    }

    protected void grdPrincipale_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected void grdPrincipale_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex));
        }
    }

    protected int GetGridKeyValue(ASPxGridView gridView, int visibleIndex)
    {
        return Convert.ToInt32(gridView.GetRowValues(visibleIndex, gridView.KeyFieldName));
    }

    protected void grdPrincipale_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (!gridView.IsNewRowEditing)
        {

        }
    }

    protected void grdPrincipale_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        //var dataEsercizioStart = int.Parse(e.OldValues["DataEsercizioStart"].ToString());
        //var dataEsercizioEnd = int.Parse(e.NewValues["DataEsercizioEnd"].ToString());

        //if (!dataEsercizioStart.Equals(dataEsercizioEnd))
        //{
        //    var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

        //    var gridObject = CurrentDataContext.LIM_LibrettiImpiantiConsumoAcqua.Find(idRiga);


        //    CurrentDataContext.SaveChanges();
        //    gridView.DataBind();

        //}
    }

    protected void grdPrincipale_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var idRiga = Convert.ToInt32(e.EditingKeyValue);

        e.Cancel = !IsDraftElementoGridPrincipale(idRiga);
    }

    protected void grdPrincipale_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {

    }

    #endregion

    #region Osservazioni
    protected void dgOsservazioni_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            CheckBox chkOsservazioneConferma = (CheckBox)e.Item.Cells[5].FindControl("chkOsservazioneConferma");
            TextBox txtOsservazione = (TextBox)e.Item.Cells[5].FindControl("txtOsservazione");
            RadioButtonList rblTipologiaRisoluzioneOsservazione = ((RadioButtonList)(e.Item.Cells[6].FindControl("rblTipologiaRisoluzioneOsservazione")));
            rbTipologiaRisoluzioneAccertamento(rblTipologiaRisoluzioneOsservazione, null, false);
            rblTipologiaRisoluzioneOsservazione.SelectedValue = e.Item.Cells[3].Text;

            Panel pnlTipologiaRisoluzioneOsservazioneGiorni = ((Panel)(e.Item.Cells[6].FindControl("pnlTipologiaRisoluzioneOsservazioneGiorni")));
            TextBox txtGiorni = ((TextBox)(e.Item.Cells[6].FindControl("txtGiorni")));

            Panel pnlTipologiaRisoluzioneOsservazioneEvento = ((Panel)(e.Item.Cells[6].FindControl("pnlTipologiaRisoluzioneOsservazioneEvento")));
            DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(e.Item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));
            ddTipologiaEventoAccertamento(ddlTipologiaEventoAccertamento, null);
            if (e.Item.Cells[4].Text != "")
            {
                ddlTipologiaEventoAccertamento.SelectedValue = e.Item.Cells[4].Text;
            }

            SetVisibleOsservazione(chkOsservazioneConferma,
                                  txtOsservazione,
                                  rblTipologiaRisoluzioneOsservazione,
                                  pnlTipologiaRisoluzioneOsservazioneGiorni,
                                  txtGiorni,
                                  pnlTipologiaRisoluzioneOsservazioneEvento,
                                  ddlTipologiaEventoAccertamento);
        }
    }

    protected void chkOsservazioneConferma_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkOsservazioneConferma = (CheckBox)sender;
        TableCell cella = chkOsservazioneConferma.Parent as TableCell;
        DataGridItem item = cella.Parent as DataGridItem;

        Label lblOsservazioneRct = ((Label)(item.Cells[5].FindControl("lblOsservazioneRct")));
        TextBox txtOsservazione = ((TextBox)(item.Cells[5].FindControl("txtOsservazione")));
        txtOsservazione.Text = lblOsservazioneRct.Text;

        RadioButtonList rblTipologiaRisoluzioneOsservazione = ((RadioButtonList)(item.Cells[6].FindControl("rblTipologiaRisoluzioneOsservazione")));

        Panel pnlTipologiaRisoluzioneOsservazioneGiorni = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneOsservazioneGiorni")));
        TextBox txtGiorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni")));

        Panel pnlTipologiaRisoluzioneOsservazioneEvento = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneOsservazioneEvento")));
        DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));

        SetVisibleOsservazione(chkOsservazioneConferma,
                                  txtOsservazione,
                                  rblTipologiaRisoluzioneOsservazione,
                                  pnlTipologiaRisoluzioneOsservazioneGiorni,
                                  txtGiorni,
                                  pnlTipologiaRisoluzioneOsservazioneEvento,
                                  ddlTipologiaEventoAccertamento);
    }

    protected void rblTipologiaRisoluzioneOsservazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rblTipologiaRisoluzioneOsservazione = (RadioButtonList)sender;
        TableCell cella = rblTipologiaRisoluzioneOsservazione.Parent as TableCell;
        DataGridItem item = cella.Parent as DataGridItem;

        TextBox txtOsservazione = ((TextBox)(item.Cells[5].FindControl("txtOsservazione")));
        CheckBox chkOsservazioneConferma = ((CheckBox)(item.Cells[5].FindControl("chkOsservazioneConferma")));

        Panel pnlTipologiaRisoluzioneOsservazioneGiorni = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneOsservazioneGiorni")));
        TextBox txtGiorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni")));

        Panel pnlTipologiaRisoluzioneOsservazioneEvento = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneOsservazioneEvento")));
        DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));

        SetVisibleOsservazione(chkOsservazioneConferma,
                                  txtOsservazione,
                                  rblTipologiaRisoluzioneOsservazione,
                                  pnlTipologiaRisoluzioneOsservazioneGiorni,
                                  txtGiorni,
                                  pnlTipologiaRisoluzioneOsservazioneEvento,
                                  ddlTipologiaEventoAccertamento);

    }

    protected void SetVisibleOsservazione(CheckBox chkOsservazioneConferma,
                                             TextBox txtOsservazione,
                                             RadioButtonList rblTipologiaRisoluzioneOsservazione,
                                             Panel pnlTipologiaRisoluzioneOsservazioneGiorni,
                                             TextBox txtGiorni,
                                             Panel pnlTipologiaRisoluzioneOsservazioneEvento,
                                             DropDownList ddlTipologiaEventoAccertamento)
    {
        if (chkOsservazioneConferma.Checked)
        {
            txtOsservazione.Visible = true;
            rblTipologiaRisoluzioneOsservazione.Visible = true;
            if (rblTipologiaRisoluzioneOsservazione.SelectedItem.Value == "1")
            {
                pnlTipologiaRisoluzioneOsservazioneGiorni.Visible = true;
                pnlTipologiaRisoluzioneOsservazioneEvento.Visible = false;
                ddlTipologiaEventoAccertamento.SelectedIndex = 0;
                rblTipologiaRisoluzioneOsservazione.SelectedIndex = 0;
            }
            else
            {
                pnlTipologiaRisoluzioneOsservazioneGiorni.Visible = false;
                pnlTipologiaRisoluzioneOsservazioneEvento.Visible = true;
                txtGiorni.Text = string.Empty;
            }
        }
        else
        {
            txtOsservazione.Visible = false;
            txtOsservazione.Text = string.Empty;
            rblTipologiaRisoluzioneOsservazione.Visible = false;
            rblTipologiaRisoluzioneOsservazione.SelectedIndex = 0;
            pnlTipologiaRisoluzioneOsservazioneGiorni.Visible = false;
            txtGiorni.Text = string.Empty;
            pnlTipologiaRisoluzioneOsservazioneEvento.Visible = false;
            ddlTipologiaEventoAccertamento.SelectedIndex = 0;
        }
    }

    protected void btnNewOsservazione_Click(object sender, EventArgs e)
    {
        long iDAccertamento = long.Parse(IDAccertamento);
        //long iDRapportoControlloTecnico = long.Parse(lblIDRapportoControllo.Text);
        SaveNonConformita();

        GetDatiAccertamento(iDAccertamento);
    }
    #endregion
    
    #region Raccomandazioni

    protected void dgRaccomandazioni_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            CheckBox chkRaccomandazioneConferma = (CheckBox)e.Item.Cells[5].FindControl("chkRaccomandazioneConferma");
            TextBox txtRaccomandazione = (TextBox)e.Item.Cells[5].FindControl("txtRaccomandazione");
            RadioButtonList rblTipologiaRisoluzioneRaccomandazione = ((RadioButtonList)(e.Item.Cells[6].FindControl("rblTipologiaRisoluzioneRaccomandazione")));
            rbTipologiaRisoluzioneAccertamento(rblTipologiaRisoluzioneRaccomandazione, null, false);
            rblTipologiaRisoluzioneRaccomandazione.SelectedValue = e.Item.Cells[3].Text;

            Panel pnlTipologiaRisoluzioneRaccomandazioneGiorni = ((Panel)(e.Item.Cells[6].FindControl("pnlTipologiaRisoluzioneRaccomandazioneGiorni")));
            TextBox txtGiorni = ((TextBox)(e.Item.Cells[6].FindControl("txtGiorni")));

            Panel pnlTipologiaRisoluzioneRaccomandazioneEvento = ((Panel)(e.Item.Cells[6].FindControl("pnlTipologiaRisoluzioneRaccomandazioneEvento")));
            DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(e.Item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));
            ddTipologiaEventoAccertamento(ddlTipologiaEventoAccertamento, null);
            if (e.Item.Cells[4].Text != "")
            {
                ddlTipologiaEventoAccertamento.SelectedValue = e.Item.Cells[4].Text;
            }
            
            SetVisibleRaccomandazione(chkRaccomandazioneConferma,
                                  txtRaccomandazione,
                                  rblTipologiaRisoluzioneRaccomandazione,
                                  pnlTipologiaRisoluzioneRaccomandazioneGiorni,
                                  txtGiorni,
                                  pnlTipologiaRisoluzioneRaccomandazioneEvento,
                                  ddlTipologiaEventoAccertamento);
        }
    }

    protected void chkRaccomandazioneConferma_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkRaccomandazioneConferma = (CheckBox)sender;
        TableCell cella = chkRaccomandazioneConferma.Parent as TableCell;
        DataGridItem item = cella.Parent as DataGridItem;

        Label lblRaccomandazioneRct = ((Label)(item.Cells[5].FindControl("lblRaccomandazioneRct")));
        TextBox txtRaccomandazione = ((TextBox)(item.Cells[5].FindControl("txtRaccomandazione")));
        txtRaccomandazione.Text = lblRaccomandazioneRct.Text;

        RadioButtonList rblTipologiaRisoluzioneRaccomandazione = ((RadioButtonList)(item.Cells[6].FindControl("rblTipologiaRisoluzioneRaccomandazione")));
        
        Panel pnlTipologiaRisoluzioneRaccomandazioneGiorni = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneRaccomandazioneGiorni")));
        TextBox txtGiorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni")));

        Panel pnlTipologiaRisoluzioneRaccomandazioneEvento = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneRaccomandazioneEvento")));
        DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));

        SetVisibleRaccomandazione(chkRaccomandazioneConferma, 
                                  txtRaccomandazione, 
                                  rblTipologiaRisoluzioneRaccomandazione,
                                  pnlTipologiaRisoluzioneRaccomandazioneGiorni,
                                  txtGiorni,
                                  pnlTipologiaRisoluzioneRaccomandazioneEvento,
                                  ddlTipologiaEventoAccertamento);
    }

    protected void rblTipologiaRisoluzioneRaccomandazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rblTipologiaRisoluzioneRaccomandazione = (RadioButtonList)sender;
        TableCell cella = rblTipologiaRisoluzioneRaccomandazione.Parent as TableCell;
        DataGridItem item = cella.Parent as DataGridItem;

        TextBox txtRaccomandazione = ((TextBox)(item.Cells[5].FindControl("txtRaccomandazione")));
        CheckBox chkRaccomandazioneConferma = ((CheckBox)(item.Cells[5].FindControl("chkRaccomandazioneConferma")));

        Panel pnlTipologiaRisoluzioneRaccomandazioneGiorni = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneRaccomandazioneGiorni")));
        TextBox txtGiorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni")));

        Panel pnlTipologiaRisoluzioneRaccomandazioneEvento = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzioneRaccomandazioneEvento")));
        DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));

        SetVisibleRaccomandazione(chkRaccomandazioneConferma,
                                  txtRaccomandazione,
                                  rblTipologiaRisoluzioneRaccomandazione,
                                  pnlTipologiaRisoluzioneRaccomandazioneGiorni,
                                  txtGiorni,
                                  pnlTipologiaRisoluzioneRaccomandazioneEvento,
                                  ddlTipologiaEventoAccertamento);

    }

    protected void SetVisibleRaccomandazione(CheckBox chkRaccomandazioneConferma, 
                                             TextBox txtRaccomandazione, 
                                             RadioButtonList rblTipologiaRisoluzioneRaccomandazione,
                                             Panel pnlTipologiaRisoluzioneRaccomandazioneGiorni,
                                             TextBox txtGiorni,
                                             Panel pnlTipologiaRisoluzioneRaccomandazioneEvento,
                                             DropDownList ddlTipologiaEventoAccertamento)
    {
        if (chkRaccomandazioneConferma.Checked)
        {
            txtRaccomandazione.Visible = true;
            rblTipologiaRisoluzioneRaccomandazione.Visible = true;
            if (rblTipologiaRisoluzioneRaccomandazione.SelectedItem.Value == "1")
            {
                pnlTipologiaRisoluzioneRaccomandazioneGiorni.Visible = true;
                pnlTipologiaRisoluzioneRaccomandazioneEvento.Visible = false;
                ddlTipologiaEventoAccertamento.SelectedIndex = 0;
                rblTipologiaRisoluzioneRaccomandazione.SelectedIndex = 0;
            }
            else
            {
                pnlTipologiaRisoluzioneRaccomandazioneGiorni.Visible = false;
                pnlTipologiaRisoluzioneRaccomandazioneEvento.Visible = true;
                txtGiorni.Text = string.Empty;
            }
        }
        else
        {
            txtRaccomandazione.Visible = false;
            txtRaccomandazione.Text = string.Empty;
            rblTipologiaRisoluzioneRaccomandazione.Visible = false;
            rblTipologiaRisoluzioneRaccomandazione.SelectedIndex = 0;
            pnlTipologiaRisoluzioneRaccomandazioneGiorni.Visible = false;
            txtGiorni.Text = string.Empty;
            pnlTipologiaRisoluzioneRaccomandazioneEvento.Visible = false;
            ddlTipologiaEventoAccertamento.SelectedIndex = 0;           
        }
    }

    protected void btnNewRaccomandazione_Click(object sender, EventArgs e)
    {
        long iDAccertamento = long.Parse(IDAccertamento);
        long? iDRapportoControlloTecnico = null;
        if (!string.IsNullOrEmpty(lblIDRapportoControllo.Text))
        {
            iDRapportoControlloTecnico = long.Parse(lblIDRapportoControllo.Text);
        }
        
        int iDTipoAccertamento = int.Parse(lblIDTipoAccertamento.Text);
        long? iDIspezione = null;
        if (!string.IsNullOrEmpty(lblIDIspezione.Text))
        {
            iDIspezione = long.Parse(lblIDIspezione.Text);
        }

        UtilityVerifiche.SetNonConformitaLibere(iDAccertamento, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, "RACC");
        //UtilityVerifiche.SetDocumentiAccertamento(iDAccertamento);
        SaveNonConformita();
        GetDatiAccertamento(iDAccertamento);
    }

    #endregion

    #region Prescrizioni
    protected void dgPrescrizioni_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            CheckBox chkPrescrizioneConferma = (CheckBox)e.Item.Cells[3].FindControl("chkPrescrizioneConferma");
            TextBox txtPrescrizione = (TextBox)e.Item.Cells[3].FindControl("txtPrescrizione");
            //RadioButtonList rblTipologiaRisoluzionePrescrizione = ((RadioButtonList)(e.Item.Cells[6].FindControl("rblTipologiaRisoluzionePrescrizione")));
            //rbTipologiaRisoluzioneAccertamento(rblTipologiaRisoluzionePrescrizione, null, true);
            //rblTipologiaRisoluzionePrescrizione.SelectedValue = e.Item.Cells[3].Text;

            //Panel pnlTipologiaRisoluzionePrescrizioneGiorni = ((Panel)(e.Item.Cells[6].FindControl("pnlTipologiaRisoluzionePrescrizioneGiorni")));
            //TextBox txtGiorni = ((TextBox)(e.Item.Cells[6].FindControl("txtGiorni")));

            //Panel pnlTipologiaRisoluzionePrescrizioneEvento = ((Panel)(e.Item.Cells[6].FindControl("pnlTipologiaRisoluzionePrescrizioneEvento")));
            //DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(e.Item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));
            //ddTipologiaEventoAccertamento(ddlTipologiaEventoAccertamento, null);
            //if (e.Item.Cells[4].Text != "")
            //{
            //    ddlTipologiaEventoAccertamento.SelectedValue = e.Item.Cells[4].Text;
            //}

            SetVisiblePrescrizione(chkPrescrizioneConferma,
                                  txtPrescrizione);

            //SetTipologiaRisoluzionePrescrizioneSameValue(rblTipologiaRisoluzionePrescrizione);
        }
    }

    protected void chkPrescrizioneConferma_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkPrescrizioneConferma = (CheckBox)sender;
        TableCell cella = chkPrescrizioneConferma.Parent as TableCell;
        DataGridItem item = cella.Parent as DataGridItem;

        Label lblPrescrizioneRct = ((Label)(item.Cells[3].FindControl("lblPrescrizioneRct")));
        TextBox txtPrescrizione = ((TextBox)(item.Cells[3].FindControl("txtPrescrizione")));
        txtPrescrizione.Text = lblPrescrizioneRct.Text;

        //RadioButtonList rblTipologiaRisoluzionePrescrizione = ((RadioButtonList)(item.Cells[6].FindControl("rblTipologiaRisoluzionePrescrizione")));

        //Panel pnlTipologiaRisoluzionePrescrizioneGiorni = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzionePrescrizioneGiorni")));
        //TextBox txtGiorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni")));

        //Panel pnlTipologiaRisoluzionePrescrizioneEvento = ((Panel)(item.Cells[6].FindControl("pnlTipologiaRisoluzionePrescrizioneEvento")));
        //DropDownList ddlTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento")));

        SetVisiblePrescrizione(chkPrescrizioneConferma,
                                  txtPrescrizione);
    }
    
    protected void SetVisiblePrescrizione(CheckBox chkPrescrizioneConferma, TextBox txtPrescrizione)
    {
        if (chkPrescrizioneConferma.Checked)
        {
            //rblImpiantoFunzionante.Visible = true;
            txtPrescrizione.Visible = true;
            //rblTipologiaRisoluzionePrescrizione.Visible = true;
            //if (rblTipologiaRisoluzionePrescrizione.SelectedItem.Value == "1")
            //{
            //    pnlTipologiaRisoluzionePrescrizioneGiorni.Visible = true;
            //    pnlTipologiaRisoluzionePrescrizioneEvento.Visible = false;
            //    ddlTipologiaEventoAccertamento.SelectedIndex = 0;
            //    rblTipologiaRisoluzionePrescrizione.SelectedIndex = 0;
            //}
            //else if (rblTipologiaRisoluzionePrescrizione.SelectedItem.Value == "2")
            //{
            //    pnlTipologiaRisoluzionePrescrizioneGiorni.Visible = false;
            //    pnlTipologiaRisoluzionePrescrizioneEvento.Visible = true;
            //    txtGiorni.Text = string.Empty;
            //}
        }
        else
        {
            //rblImpiantoFunzionante.Visible = false;
            txtPrescrizione.Visible = false;
            txtPrescrizione.Text = string.Empty;
            //rblTipologiaRisoluzionePrescrizione.Visible = false;
            //rblTipologiaRisoluzionePrescrizione.SelectedIndex = 0;
            //pnlTipologiaRisoluzionePrescrizioneGiorni.Visible = false;
            //txtGiorni.Text = string.Empty;
            //pnlTipologiaRisoluzionePrescrizioneEvento.Visible = false;
            //ddlTipologiaEventoAccertamento.SelectedIndex = 0;
        }

        SetVisibleHiddenImpiantoFunzionanteConferma();
    }
    
    protected void btnNewPrescrizione_Click(object sender, EventArgs e)
    {
        long iDAccertamento = long.Parse(IDAccertamento);
        long? iDRapportoControlloTecnico = null;
        if (!string.IsNullOrEmpty(lblIDRapportoControllo.Text))
        {
            iDRapportoControlloTecnico = long.Parse(lblIDRapportoControllo.Text);
        }
        int iDTipoAccertamento = int.Parse(lblIDTipoAccertamento.Text);
        long? iDIspezione = null;
        if (!string.IsNullOrEmpty(lblIDIspezione.Text))
        {
            iDIspezione = long.Parse(lblIDIspezione.Text);
        }

        UtilityVerifiche.SetNonConformitaLibere(iDAccertamento, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, "PRES");
        //UtilityVerifiche.SetDocumentiAccertamento(iDAccertamento);
        SaveNonConformita();

        GetDatiAccertamento(iDAccertamento);
    }
    #endregion

    #region Impianto non Funzionante

    protected void SetVisibleHiddenImpiantoFunzionanteConferma()
    {
        bool fVisibleImpiantoFunzionanteConferma = false;
        for (int i = 0; i < dgPrescrizioni.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)dgPrescrizioni.Items[i].Cells[3].FindControl("chkPrescrizioneConferma");
            if (chk.Checked)
            {
                fVisibleImpiantoFunzionanteConferma = true;
                break;
            }
        }

        if (fVisibleImpiantoFunzionanteConferma)
        {
            rblImpiantoFunzionanteConferma.Visible = true;
            rblTipologiaRisoluzioneImpiantoFunzionante.Visible = true;
            txtGiorniImpiantoFunzionante.Visible = true;
            rblTipologiaImpiantoFunzionanteAccertamento.Visible = true;
            //txtNoteImpiantoFunzionante.Visible = true;
            //rfvtxtNoteImpiantoFunzionante.Enabled = true;

            //TODO: Fix 01-02-2021 Non veniva mostrata una parte di schermata relativo alla sezione INF
            //SetVisibleHiddenTipologiaImpiantoFunzionanteAccertamento(bool.Parse(rblImpiantoFunzionanteConferma.SelectedValue));
            //SetVisibleHiddenNoteImpiantoFunzionante(rblTipologiaImpiantoFunzionanteAccertamento.SelectedItem.Value);
        }
        else
        {
            rblImpiantoFunzionanteConferma.Visible = false;
            rblTipologiaRisoluzioneImpiantoFunzionante.Visible = false;
            //rblImpiantoFunzionanteConferma.SelectedValue = "False";
            rblTipologiaImpiantoFunzionanteAccertamento.SelectedIndex = 0;
            rblTipologiaImpiantoFunzionanteAccertamento.Visible = false;
            txtGiorniImpiantoFunzionante.Visible = false;
            //txtNoteImpiantoFunzionante.Visible = false;
            //rfvtxtNoteImpiantoFunzionante.Enabled = false;
        }
    }
    
    protected void rblImpiantoFunzionanteConferma_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenTipologiaImpiantoFunzionanteAccertamento(bool.Parse(rblImpiantoFunzionanteConferma.SelectedValue));
        //SetVisibleHiddenNoteImpiantoFunzionante(rblTipologiaImpiantoFunzionanteAccertamento.SelectedItem.Value);
    }

    protected void SetVisibleHiddenTipologiaImpiantoFunzionanteAccertamento(bool fConfermaimpiantoFunzionante)
    {
        //TODO: ricordasi di abilitarlo
        //if (fConfermaimpiantoFunzionante)
        //{
        //    //rblTipologiaRisoluzioneImpiantoFunzionante.SelectedValue = "3";
        //    //pnlTipologiaRisoluzionePrescrizioneGiorni.Visible = false;
        //    //txtGiorniImpiantoFunzionante.Text = string.Empty;
        //}
        //else
        //{
            rblTipologiaRisoluzioneImpiantoFunzionante.SelectedValue = "1";
            pnlTipologiaRisoluzionePrescrizioneGiorni.Visible = true;
        //}
    }
        
    #endregion

    #region Funzionalità
    protected void SaveNonConformita()
    {
        #region Osservazioni
        for (int i = 0; i < dgOsservazioni.Items.Count; i++)
        {
            DataGridItem item = dgOsservazioni.Items[i];

            long IDNonConformita = long.Parse(item.Cells[0].Text);
            long IDAccertamentoOss = long.Parse(item.Cells[1].Text);
            string Tipo = item.Cells[2].Text;

            bool fOsservazioneConferma = ((CheckBox)(item.Cells[5].FindControl("chkOsservazioneConferma"))).Checked;
            string Osservazione = ((TextBox)(item.Cells[5].FindControl("txtOsservazione"))).Text;
            string IDTipologiaRisoluzioneAccertamento = ((RadioButtonList)(item.Cells[6].FindControl("rblTipologiaRisoluzioneOsservazione"))).SelectedValue;
            string IDTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento"))).SelectedValue;
            string Giorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni"))).Text;

            UtilityVerifiche.SaveNonConformitaAccertamento(IDNonConformita,
                                                           IDAccertamentoOss,
                                                           Tipo,
                                                           false,
                                                           string.Empty,
                                                           false,
                                                           string.Empty,
                                                           fOsservazioneConferma,
                                                           Osservazione,
                                                           false,
                                                           string.Empty,
                                                           null,
                                                           UtilityApp.ParseNullableInt(IDTipologiaRisoluzioneAccertamento),
                                                           UtilityApp.ParseNullableInt(IDTipologiaEventoAccertamento),
                                                           UtilityApp.ParseNullableInt(Giorni)
                                                          );
        }
        #endregion

        #region Raccomandazioni
        for (int i = 0; i < dgRaccomandazioni.Items.Count; i++)
        {
            DataGridItem item = dgRaccomandazioni.Items[i];

            long IDNonConformita = long.Parse(item.Cells[0].Text);
            long IDAccertamentoRacc = long.Parse(item.Cells[1].Text);
            string Tipo = item.Cells[2].Text;

            bool fRaccomandazioneConferma = ((CheckBox)(item.Cells[5].FindControl("chkRaccomandazioneConferma"))).Checked;
            string Raccomandazione = ((TextBox)(item.Cells[5].FindControl("txtRaccomandazione"))).Text;
            string IDTipologiaRisoluzioneAccertamento = ((RadioButtonList)(item.Cells[6].FindControl("rblTipologiaRisoluzioneRaccomandazione"))).SelectedValue;
            string IDTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento"))).SelectedValue;
            string Giorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni"))).Text;

            UtilityVerifiche.SaveNonConformitaAccertamento(IDNonConformita,
                                                           IDAccertamentoRacc,
                                                           Tipo,
                                                           fRaccomandazioneConferma,
                                                           Raccomandazione,
                                                           false,
                                                           string.Empty,
                                                           false,
                                                           string.Empty,
                                                           false,
                                                           string.Empty,
                                                           null,
                                                           UtilityApp.ParseNullableInt(IDTipologiaRisoluzioneAccertamento),
                                                           UtilityApp.ParseNullableInt(IDTipologiaEventoAccertamento),
                                                           UtilityApp.ParseNullableInt(Giorni)
                                                          );
        }
        #endregion

        #region Prescrizioni
        for (int i = 0; i < dgPrescrizioni.Items.Count; i++)
        {
            DataGridItem item = dgPrescrizioni.Items[i];

            long IDNonConformita = long.Parse(item.Cells[0].Text);
            long IDAccertamentoPres = long.Parse(item.Cells[1].Text);
            string Tipo = item.Cells[2].Text;

            bool fPrescrizioneConferma = ((CheckBox)(item.Cells[3].FindControl("chkPrescrizioneConferma"))).Checked;
            string Prescrizione = ((TextBox)(item.Cells[3].FindControl("txtPrescrizione"))).Text;
            //string IDTipologiaRisoluzioneAccertamento = ((RadioButtonList)(item.Cells[6].FindControl("rblTipologiaRisoluzionePrescrizione"))).SelectedValue;
            //string IDTipologiaEventoAccertamento = ((DropDownList)(item.Cells[6].FindControl("ddlTipologiaEventoAccertamento"))).SelectedValue;
            //string Giorni = ((TextBox)(item.Cells[6].FindControl("txtGiorni"))).Text;

            UtilityVerifiche.SaveNonConformitaAccertamento(IDNonConformita,
                                                           IDAccertamentoPres,
                                                           Tipo,
                                                           false,
                                                           string.Empty,
                                                           fPrescrizioneConferma,
                                                           Prescrizione,
                                                           false,
                                                           string.Empty,
                                                           false,
                                                           string.Empty,
                                                           null,
                                                           null,
                                                           null,
                                                           null
                                                          );
        }

        
        #endregion

        #region Impianto Non funzionante
        long IDAccertamentoInf = long.Parse(IDAccertamento);
        if (!string.IsNullOrEmpty(lblIDNonConformita.Text))
        {
            UtilityVerifiche.SaveNonConformitaAccertamento(long.Parse(lblIDNonConformita.Text),
                                                       IDAccertamentoInf,
                                                       "INF",
                                                       false,
                                                       string.Empty,
                                                       false,
                                                       string.Empty,
                                                       false,
                                                       string.Empty,
                                                       bool.Parse(rblImpiantoFunzionanteConferma.SelectedValue),
                                                       txtNoteImpiantoFunzionante.Text,
                                                       UtilityApp.ParseNullableInt(rblTipologiaImpiantoFunzionanteAccertamento.SelectedItem.Value),
                                                       UtilityApp.ParseNullableInt(rblTipologiaRisoluzioneImpiantoFunzionante.SelectedItem.Value),
                                                       null,
                                                       UtilityApp.ParseNullableInt(txtGiorniImpiantoFunzionante.Text)
                                                       );
        }        
        #endregion

        UtilityVerifiche.SetDocumentiAccertamento(long.Parse(IDAccertamento));
    }

    protected void btnInviaACoordinatore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 3, iDUtenteAccertatore, null, null);
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
        }
    }
    
    protected void btnRimandaACoordinatore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            int iDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 4, iDUtenteAccertatore, iDUtenteCoordinatore, null);
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
        }
    }
    
    protected void btnRimandaAdAccertatore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            int iDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 2, iDUtenteAccertatore, null, null);
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
        }
    }

    protected void btnAccertamentoNonInviato_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            int iDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 10, iDUtenteAccertatore, iDUtenteCoordinatore, null);
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
        }
    }

    protected void btnAccertamentoRigettato_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            int iDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 11, iDUtenteAccertatore, iDUtenteCoordinatore, null);
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
        }
    }

    protected void btnInviaInFirma_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            UtilityVerifiche.SaveAccertamento(iDAccertamento,
                                              UtilityApp.ParseNullableInt(ddlTipologiaDistributore.SelectedItem.Value),
                                              txtNote.Text,
                                              bool.Parse(lblfEmailConfermaAccertamento.Text),
                                              UtilityApp.ParseNullableDatetime(lblDataInvioEmail.Text),
                                              txtTestoEmail.Text,
                                              UtilityApp.ParseNullableInt(txtGiorniRealizzazioneInterventi.Text),
                                              lblEmailRispostaAccertamento.Text
                                             );
            SaveNonConformita();
            
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            int iDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
            int iDUtenteAgenteAccertatore = int.Parse(lblIDAgenteAccertatore.Text);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 6, iDUtenteAccertatore, iDUtenteCoordinatore, iDUtenteAgenteAccertatore);
            
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
            GenerateAutomaticReport(iDAccertamento, lblIDTipoAccertamento.Text);
        }
    }

    protected void btnSaveAccertamento_Click(object sender, EventArgs e)
    {
        List<IValidator> errored = this.Validators.Cast<IValidator>().Where(v => !v.IsValid).ToList();


        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            UtilityVerifiche.SaveAccertamento(iDAccertamento,
                                              UtilityApp.ParseNullableInt(ddlTipologiaDistributore.SelectedItem.Value),
                                              txtNote.Text,
                                              bool.Parse(lblfEmailConfermaAccertamento.Text),
                                              UtilityApp.ParseNullableDatetime(lblDataInvioEmail.Text),
                                              txtTestoEmail.Text,
                                              UtilityApp.ParseNullableInt(txtGiorniRealizzazioneInterventi.Text),
                                              lblEmailRispostaAccertamento.Text
                                             );

            SaveNonConformita();
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
            GenerateAutomaticReport(iDAccertamento, lblIDTipoAccertamento.Text);
        }
    }

    public static void GenerateAutomaticReport(long iDAccertamento, string iDTipoAccertamento)
    {
        var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iDAccertamento);
        foreach (var documento in documenti)
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + documento.CodiceAccertamento;
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

            string reportName = string.Empty;
            switch (iDTipoAccertamento)
            {
                case "1":
                    #region Rapporto di controllo tecnico
                    reportName = ConfigurationManager.AppSettings["ReportNameAccertamento"];
                    #endregion
                    break;
                case "2":
                    #region Rapporto di Ispezione
                    reportName = ConfigurationManager.AppSettings["ReportNameAccertamentoIspezione"];
                    #endregion
                    break;
            }
                        
            string urlPdf = ReportingServices.GetAccertamentiReport(documento.IDAccertamento.ToString(), documento.IDProceduraAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
        }
    }

    protected void btnSavePartial_Click(object sender, EventArgs e)
    {
        long iDAccertamento = long.Parse(IDAccertamento);
        UtilityVerifiche.SaveAccertamento(iDAccertamento,
                                          UtilityApp.ParseNullableInt(ddlTipologiaDistributore.SelectedItem.Value),
                                          txtNote.Text,
                                          bool.Parse(lblfEmailConfermaAccertamento.Text),
                                          UtilityApp.ParseNullableDatetime(lblDataInvioEmail.Text),
                                          txtTestoEmail.Text,
                                          UtilityApp.ParseNullableInt(txtGiorniRealizzazioneInterventi.Text),
                                          lblEmailRispostaAccertamento.Text
                                         );

        SaveNonConformita();
        GetDatiAccertamento(iDAccertamento);
        GetDatiAccertamentoStorico(iDAccertamento);
        GenerateAutomaticReport(iDAccertamento, lblIDTipoAccertamento.Text);
    }

    protected void btnInviaEmailConfermaAccertamento_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 7, iDUtenteAccertatore, null, null);
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            UtilityVerifiche.SaveAccertamento(iDAccertamento,
                                              UtilityApp.ParseNullableInt(ddlTipologiaDistributore.SelectedItem.Value),
                                              txtNote.Text,
                                              true,
                                              DateTime.Now,
                                              txtTestoEmail.Text,
                                              UtilityApp.ParseNullableInt(txtGiorniRealizzazioneInterventi.Text),
                                              lblEmailRispostaAccertamento.Text
                                             );
            
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
            EmailNotify.SendConfermaAccertamento(iDAccertamento);
        }
    }

    #endregion

    #region Rapporti
    public void dgRapporti_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            if (lblIDRapportoControllo.Text == e.Item.Cells[0].Text)
            {
                e.Item.BackColor = System.Drawing.Color.Orange;
            }

            HyperLink lnkRapportoControllo = (HyperLink)(e.Item.Cells[4].FindControl("lnkRapportoControllo"));

            QueryString qsRapporto = new QueryString();
            qsRapporto.Add("IDRapportoControlloTecnico", e.Item.Cells[0].Text);
            qsRapporto.Add("IDTipologiaRCT", e.Item.Cells[1].Text);
            qsRapporto.Add("IDSoggetto", e.Item.Cells[2].Text);
            qsRapporto.Add("IDSoggettoDerived", e.Item.Cells[3].Text);
            QueryString qsEncryptedRapporto = Encryption.EncryptQueryString(qsRapporto);

            string urlRapporto = "RCT_RapportoDiControlloTecnico.aspx";
            urlRapporto += qsEncryptedRapporto.ToString();
            lnkRapportoControllo.NavigateUrl = urlRapporto;
        }
    }

    public void GetRapportiControllo(int iDTargaturaImpianto, string Prefisso, int CodiceProgressivo)
    {
        var rapporti = UtilityRapportiControllo.GetValoriRapportoControllo(iDTargaturaImpianto, Prefisso, CodiceProgressivo);
        dgRapporti.DataSource = rapporti;
        dgRapporti.DataBind();
    }

    #endregion

    //protected void btnUpdCapResponsabile_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        int iDComuneResponsabile = int.Parse(lblResponsabileIDComune.Text);
    //        UtilityLibrettiImpianti.UpdateCap(iDComuneResponsabile, txtResponsabileCap.Text);
    //        GetDatiAccertamento(long.Parse(IDAccertamento));
    //    }
    //}

    protected void btnInviaAdAgenteAccertatore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            int IDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
            //UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 3, iDUtenteAccertatore, null);
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 8, iDUtenteAccertatore, IDUtenteCoordinatore, null); // 8 - In attesa di verifica da parte dell'Agente Accertatore
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
        }
    }

    protected void btnRimandaAdAgenteAccertatore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            int iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
            int iDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
            int iDUtenteAgenteAccertatore = int.Parse(lblIDAgenteAccertatore.Text);
            
            UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 9, iDUtenteAccertatore, iDUtenteCoordinatore, iDUtenteAgenteAccertatore); // 9 - Assegnato ad Agente Accertatore
            int iDUtente = int.Parse(info.IDUtente.ToString());
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            GetDatiAccertamento(iDAccertamento);
            GetDatiAccertamentoStorico(iDAccertamento);
        }
    }
}