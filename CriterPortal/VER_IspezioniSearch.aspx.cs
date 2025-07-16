using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using DataLayer;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DataUtilityCore.Enum;
using Pomiager.Service.Parer.RequestDto;
using System.Collections.Generic;

public partial class VER_IspezioniSearch : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected string IDIspezioneVisita
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "VER_IspezioniSearch.aspx");

        if (!Page.IsPostBack)
        {
            LoadAllDropDownlist();
            PagePermission();
            
            if (!string.IsNullOrEmpty(IDIspezioneVisita))
            {
                txtIDISpezioneVisita.Text = IDIspezioneVisita;
                //GetIspezioni();
            }
        }
        GetIspezioni();
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
            case "12": //Amministrazione
                cmbIspettore.Visible = true;
                lblSoggetto.Visible = false;
                rowProgrammaIspezione.Visible = true;
                rowCriticitaFilter.Visible = true;
                ASPxComboBox2.Value = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

                rblStatoIspezione.SelectedIndex = 1;
                break;
            case "6": //Accertatore

                break;
            case "7": //Coordinatore
                cmbIspettore.Visible = true;
                lblSoggetto.Visible = false;
                rowProgrammaIspezione.Visible = true;
                rowCriticitaFilter.Visible = true;
                ASPxComboBox2.Value = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

                rblStatoIspezione.SelectedIndex = 1;
                break;
            case "8": //Ispettore
                cmbIspettore.Value = getVal[0];
                cmbIspettore.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                rowProgrammaIspezione.Visible = true;
                rowCriticitaFilter.Visible = false;

                ASPxComboBox2.Value = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();
                break;
            case "14": //Coordinatore/Ispettore
                cmbIspettore.Value = getVal[0];
                cmbIspettore.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;

                rowProgrammaIspezione.Visible = true;
                rowCriticitaFilter.Visible = true;
                ASPxComboBox2.Value = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

                rblStatoIspezione.SelectedIndex = 1;
                break;
            case "9": //Segreteria Verifiche

                break;
        }
    }

    protected void LoadAllDropDownlist()
    {
        rbStatoIspezione(null);
    }

    protected void rbStatoIspezione(int? idPresel)
    {
        rblStatoIspezione.Items.Clear();
        rblStatoIspezione.DataValueField = "IDStatoIspezione";
        rblStatoIspezione.DataTextField = "StatoIspezione";
        //rblStatoIspezione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoIspezione(idPresel);
        var iDRuolo = userInfo.IDRuolo;
        //if (iDRuolo == 1 || iDRuolo == 7 || iDRuolo == 14)
        //{
        //    rblStatoIspezione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoIspezione(idPresel);
        //}
        //else if (iDRuolo == 8)
        //{
        //    var removeList = new List<int>() { 1 };

        //    rblStatoIspezione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoIspezione(idPresel);//.RemoveAll(r => removeList.Any(a => a == r.IDStatoIspezione));
        //}

        if (iDRuolo != 8)
        {
            rblStatoIspezione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoIspezione(idPresel);
        }
        else
        {
            var removeList = new List<int>() { 10, 11 };
            rblStatoIspezione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoIspezione(idPresel).Where(r => !removeList.Contains(r.IDStatoIspezione));

        }
        rblStatoIspezione.DataBind();

        ListItem myItem = new ListItem("Tutti gli stati", "0");
        rblStatoIspezione.Items.Insert(0, myItem);

        rblStatoIspezione.SelectedIndex = 0;
    }

    #region RICERCA ISPETTORE

    protected void cmbIspettore_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void cmbIspettore_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "7", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void cmbIspettore_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBoxIspettori();
    }

    protected void RefreshAspxComboBoxIspettori()
    {
        cmbIspettore.SelectedIndex = -1;
        cmbIspettore.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbIspettore.DataBind();
    }

    #endregion

    #region RICERCA AZIENDE
    //protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    //{
    //    ASPxComboBox comboBox = (ASPxComboBox)source;
    //    comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
    //    comboBox.DataBind();
    //}

    //protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    //{
    //    long value = 0;
    //    if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
    //        return;
    //    ASPxComboBox comboBox = (ASPxComboBox)source;

    //    comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
    //    comboBox.DataBind();
    //}

    //protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    //{
    //    RefreshAspxComboBox();
    //}

    //protected void RefreshAspxComboBox()
    //{
    //    ASPxComboBox1.SelectedIndex = -1;
    //    ASPxComboBox1.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
    //    ASPxComboBox1.DataBind();
    //}

    #endregion

    #region RICERCA PROGRAMMA ISPEZIONI
    protected void ASPxComboBox2_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox2_VER_ProgrammaIspezioni(string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox2_VER_ProgrammaIspezioniRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox2();
    }

    protected void RefreshAspxComboBox2()
    {
        ASPxComboBox2.SelectedIndex = -1;
        ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox2_VER_ProgrammaIspezioni(string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        ASPxComboBox2.DataBind();
    }

    #endregion

    #region ISPEZIONI
    protected void Ispezioni_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            QueryString qs = new QueryString();
            qs.Add("IDIspezione", commandArgs[0]);
            qs.Add("IDIspezioneVisita", commandArgs[1]);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Ispezioni.aspx";
            url += qsEncrypted.ToString();
            //Response.Redirect(url);

            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }
        else if (e.CommandName == "AnnullaIspezione")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            long iDIspezione = long.Parse(commandArgs[0]);
            long iDIspezioneVisita = long.Parse(commandArgs[1]);

            UtilityVerifiche.CambiaStatoIspezioneMassivo(iDIspezioneVisita,
                                                         8,
                                                         iDIspezione,
                                                         null,
                                                         null,
                                                         null
                                                         );

            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));

            bool usaPec = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);
            EmailNotify.SendMailPerIspettore_AnnullaIspezione(iDIspezioneVisita, usaPec);

            UtilityPosteItaliane.SendToPosteItaliane(iDIspezione, (int)EnumTypeofRaccomandata.TypePianificazioneIspezioneAnnulla);
            GetIspezioni();
        }
        else if (e.CommandName == "FirmaLetteraIncarico")
        {
            long iDIspezioneVisita = long.Parse(e.CommandArgument.ToString());
            string securityCode = UtilityVerifiche.GetSecurityCodeFromGruppoVerifica(iDIspezioneVisita);
            string url = "ConfermaIspezioneIncarico.aspx" + securityCode;

            Response.Redirect(url);
        }
        else if (e.CommandName == "PianificazioneVisita")
        {
            QueryString qs = new QueryString();
            qs.Add("IDIspezioneVisita", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_IspezioniPianificazione.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    protected void gridIspezioniNellaVisita_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ImageButton imgView = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "imgView") as ImageButton;
            ImageButton imgAnnullaIspezione = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "imgAnnullaIspezione") as ImageButton;
            ImageButton imgFirmaLetteraIncarico = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "imgFirmaLetteraIncarico") as ImageButton;
            ImageButton imgPianificazione = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "imgPianificazione") as ImageButton;

            int iDStatoIspezione = (int)e.GetValue("IDStatoIspezione");
            int iDIspezioneVisita = int.Parse(e.GetValue("IDIspezioneVisita").ToString());
            if (userInfo.IDRuolo == 1 || userInfo.IDRuolo == 7 || userInfo.IDRuolo == 12) //Amministratore / Coordinatori / Amministrazione
            {
                imgFirmaLetteraIncarico.Visible = false;
                //imgAlertPianificazione.Visible = true;

                //bool fAlertPianificazione = UtilityVerifiche.AlertPianificazioneIspettori(iDIspezioneVisita);
                //imgAlertPianificazione.ImageUrl = UtilityApp.ToImage(fAlertPianificazione);

                switch (iDStatoIspezione)
                {
                   case 1: //Ricerca ispettore
                   case 2: //Ispezione da pianificare
                   case 3: //Ispezione pianificata in attesa di conferma
                   case 4: //Ispezione Conclusa da Ispettore
                   case 5: //Ispezione Conclusa da Coordinatore con accertamento
                   case 6: //Ispezione Confermata da Inviare LAI
                   case 9: //Ispezione Conclusa da Coordinatore senza accertamento
                   case 10: //Ispezione Conclusa da Coordinatore con doppio mancato accesso (avviso non recapitato)
                   case 11: //Ispezione Conclusa da Coordinatore con utente sconosciuto
                        imgView.Visible = true;
                        break;
                    case 7: //Ispezione pianificata e confermata
                        imgView.Visible = true;
                        imgAnnullaIspezione.Visible = true;
                        break;
                    case 8: //Ispezione annullata
                        imgView.Visible = false;
                        imgAnnullaIspezione.Visible = false;
                        break;                   
                }
            }
            else if (userInfo.IDRuolo == 8 || userInfo.IDRuolo == 14) //Ispettori
            {
                //imgAlertPianificazione.Visible = false;
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var datiIspettore = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == iDIspezioneVisita && c.fInGruppoVerifica == true).FirstOrDefault();
                    if (datiIspettore != null)
                    {
                        if (datiIspettore.fIncaricoFirmato)
                        {
                            imgFirmaLetteraIncarico.Visible = false;
                            imgPianificazione.Visible = true;
                        }
                        else
                        {
                            imgFirmaLetteraIncarico.Visible = true;
                            imgPianificazione.Visible = false;
                        }
                    }
                    else
                    {
                        imgFirmaLetteraIncarico.Visible = false;
                        imgPianificazione.Visible = false;
                    }
                }
                

                switch (iDStatoIspezione)
                {
                    case 1: //Ricerca ispettore
                    case 2: //Ispezione da pianificare
                    case 3: //Ispezione pianificata in attesa di conferma
                    case 6: //Ispezione Confermata da Inviare LAI
                        imgView.Visible = false;
                        break;
                    case 4: //Ispezione Conclusa da Ispettore
                    case 5: //Ispezione Conclusa da Coordinatore con accertamento
                    case 7: //Ispezione pianificata e confermata
                    case 9: //Ispezione Conclusa da Coordinatore senza accertamento
                        imgView.Visible = true;
                        break;
                    case 8: //Ispezione annullata
                        imgView.Visible = false;
                        imgAnnullaIspezione.Visible = false;
                        break;
                }
            }
        }
    }
    #endregion

    public void GetIspezioni()
    {
        List<object> valoriCriticita = new List<object>();
        foreach (ListItem item in cblCriticita.Items)
        {
            if (item.Selected)
            {
                valoriCriticita.Add(item.Value);
            }
        }


        var ispezioniList = UtilityVerifiche.GetListIspezioni(cmbIspettore.Value,
                                                              txtIDISpezioneVisita.Text,
                                                              rblStatoIspezione.SelectedItem.Value,
                                                              txtDataIspezioneDa.Text,
                                                              txtDataIspezioneAl.Text,
                                                              txtCodiceTargatura.Text,
                                                              txtCodiceIspezione.Text,
                                                              rblIspezioniNonSvolte.SelectedItem.Value,
                                                              ASPxComboBox2.Value,
                                                              valoriCriticita.ToArray<object>()
                                                              );

        int count = ispezioniList.Count();
        if (count > 0)
        {
            lblCount.Visible = true;
            lblCount.Text = count + " ISPEZIONI CORRISPONDENTI AI PARAMETRI IMPOSTATI";
        }
        else
        {
            lblCount.Visible = false;
            lblCount.Text = string.Empty;
        }


        gridIspezioniNellaVisita.DataSource = ispezioniList;
        gridIspezioniNellaVisita.DataBind();
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetIspezioni();
        }
    }


}