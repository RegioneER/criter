using Bender.Extensions;
using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using DevExpress.Xpo.Logger;
using DevExpress.XtraGauges.Core.Model;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using EncryptionQS;
using iTextSharp.text;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LIM_LibrettiImpiantiSearchCatasto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "LIM_LibrettiImpiantiSearchCatasto.aspx");

        if (!Page.IsPostBack)
        {
            rbTipologiaGeneratori(null);
            VisibleHiddenTypeSearch(rblTipoRicercaLibretti.SelectedValue);
        }

        ASPxWebControl.RegisterBaseScript(Page);
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                cmbAziende.Visible = true;
                cmbAddetti.Visible = true;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                break;
            case "2": //Amministratore azienda
                cmbAziende.Value = getVal[0];
                cmbAziende.Visible = false;
                cmbAddetti.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
            case "3": //Operatore/Addetto
                cmbAziende.Visible = false;
                cmbAddetti.Visible = false;
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = true;
                cmbAziende.Value = getVal[2];
                cmbAddetti.Value = getVal[0];
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2]);
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                break;
            case "10": //Responsabile tecnico
                cmbAziende.Value = getVal[2];
                cmbAziende.Visible = false;
                cmbAddetti.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2]);
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
            case "8": //Ispettore
                rowAzienda.Visible = false;
                rowOperatore.Visible = false;
                rowPresaInCarico.Visible = false;
                break;
        }
    }

    #region RICERCA AZIENDE / ADDETTI
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetComboBoxFilterByIDAzienda();
    }

    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }
    
    protected void GetComboBoxFilterByIDAzienda()
    {
        if (cmbAziende.Value != null)
        {
            cmbAddetti.Text = "";
            cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", cmbAziende.Value.ToString(), string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
            cmbAddetti.DataBind();
        }
    }

    protected void RefreshAspxComboBox()
    {
        cmbAziende.SelectedIndex = -1;
        cmbAziende.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbAziende.DataBind();
        cmbAddetti.SelectedIndex = -1;
        cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "0", string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        cmbAddetti.DataBind();
    }
        
    #endregion

    #region LIBRETTI IMPIANTI
    
    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgPdf = (ImageButton) (e.Item.Cells[6].FindControl("ImgPdf"));
            imgPdf.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Libretto_" + e.Item.Cells[0].Text + "', 'iframe', 'LIM_LibrettiImpiantiViewer.aspx?IDLibrettoImpianto=" + e.Item.Cells[0].Text + "', 'Libretto_" + e.Item.Cells[0].Text + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Pdf")
        {

        }
        else if (e.CommandName == "View")
        {
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpianti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    public void GetLibrettiImpianti()
    {
        System.Threading.Thread.Sleep(500);
        string strSql = UtilityLibrettiImpianti.GetSqlValoriLibrettiSuCatastoFilter(txtCodiceTargatura.Text,
                                                                                    rblTipologieGeneratori.SelectedItem.Value,
                                                                                    RadComboBoxCodiciCatastali.Value,
                                                                                    txtIndirizzoImpianto.Text,
                                                                                    txtCivicoImpianto.Text,
                                                                                    txtMatricolaGeneratore.Text,
                                                                                    txtCodicePod.Text,
                                                                                    txtCodicePdr.Text,
                                                                                    txtCfPIvaResponsabile.Text
                                                                                    );
        int totRecords = UtilityApp.BindControl(false, this.DataGrid, strSql, string.Empty, 0, DataGrid.PageSize);
    }
    #endregion

    #region COMUNI
    protected void comboComune_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void comboComune_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void comboComune_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshComboComune();
    }

    protected void RefreshComboComune()
    {
        RadComboBoxCodiciCatastali.SelectedIndex = -1;
        RadComboBoxCodiciCatastali.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        RadComboBoxCodiciCatastali.TextField = "Codice";
        RadComboBoxCodiciCatastali.ValueField = "IDCodiceCatastale";
        RadComboBoxCodiciCatastali.DataBind();
    }
    #endregion

    #region TENDINE
    protected void rbTipologiaGeneratori(int? idPresel)
    {
        rblTipologieGeneratori.Items.Clear();
        rblTipologieGeneratori.DataValueField = "IDTipologiaRCT";
        rblTipologieGeneratori.DataTextField = "DescrizioneRCT";
        rblTipologieGeneratori.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaRapportoDiControllo(idPresel);
        rblTipologieGeneratori.DataBind();

       rblTipologieGeneratori.SelectedIndex = 0;
    }
    #endregion

    protected void rblTipoRicercaLibretti_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenTypeSearch(rblTipoRicercaLibretti.SelectedValue);
    }

    protected void VisibleHiddenTypeSearch(string IDTipoRicercaLibretti)
    {
        switch (IDTipoRicercaLibretti)
        {
            case "0":
                rowCodiceTargatura.Visible = true;
                rowTipologieGeneratori.Visible = false;
                rowComune.Visible = false;
                rowIndirizzoCivico.Visible = false;
                rowMatricolaGeneratore.Visible = false;
                rowCodicePod.Visible = false;
                rowCodicePdr.Visible = false;
                rowCfPIvaResponsabile.Visible = false;
                txtCfPIvaResponsabile.Text = string.Empty;
                break;
            case "1":
                rowCodiceTargatura.Visible = false;
                rowTipologieGeneratori.Visible = true;
                rowComune.Visible = true;
                rowIndirizzoCivico.Visible = true;
                rowMatricolaGeneratore.Visible = true;
                rowCodicePod.Visible = true;
                rowCodicePdr.Visible = true;
                rowCfPIvaResponsabile.Visible = true;
                break;
        }
        ResetFieldSearch(IDTipoRicercaLibretti);
    }

    protected void ResetFieldSearch(string IDTipoRicercaLibretti)
    {
        lblMessage.Text = string.Empty;
        SetVisibilityResult(false);
        switch (IDTipoRicercaLibretti)
        {
            case "0":
                RefreshComboComune();
                txtIndirizzoImpianto.Text = string.Empty;
                txtCivicoImpianto.Text = string.Empty;
                txtMatricolaGeneratore.Text = string.Empty;
                txtCodicePod.Text = string.Empty;
                txtCodicePdr.Text = string.Empty;
                txtCfPIvaResponsabile.Text = string.Empty;
                break;
            case "1":
                txtCodiceTargatura.Text = string.Empty;
                break;
        }
    }

    protected void ControllaPresenzaMinimiFieldSearch(object source, ServerValidateEventArgs args)
    {
        switch (int.Parse(rblTipoRicercaLibretti.SelectedItem.Value))
        {
            case 0:
                args.IsValid = true;
                break;
            case 1:
                int countIndirizzo = !string.IsNullOrEmpty(txtIndirizzoImpianto.Text) ? 1 : 0;
                int countCivicoIndirizzo = !string.IsNullOrEmpty(txtIndirizzoImpianto.Text) ? !string.IsNullOrEmpty(txtCivicoImpianto.Text) ? 1 : 0 : 0;  
                int countMatricolaGeneratore = !string.IsNullOrEmpty(txtMatricolaGeneratore.Text) ? 1 : 0;
                int countCodicePod = !string.IsNullOrEmpty(txtCodicePod.Text) ? 1 : 0;
                int countCodicePdr = !string.IsNullOrEmpty(txtCodicePdr.Text) ? 1 : 0;
                int countCfPIvaResponsabile = !string.IsNullOrEmpty(txtCfPIvaResponsabile.Text) ? 1 : 0;

                int totale = (countIndirizzo + countCivicoIndirizzo + countMatricolaGeneratore + countCodicePod + countCodicePdr + countCfPIvaResponsabile);
                if (totale >= 3)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                    rowGridLibretti.Visible = false;
                    rowGridHeaderLibretti.Visible = false;
                    rowAzienda.Visible = false;
                    rowOperatore.Visible = false;
                    rowPresaInCarico.Visible = false;
                }
                break;
        }        
    }

    protected void ControllaPodPdrFieldSearch(object source, ServerValidateEventArgs args)
    {
        string StringChecked = "00000000000000";
        bool checkPdr = false;
        bool checkPod = false;

        if (txtCodicePdr.Text != StringChecked)
        {
            checkPdr = true;
        }
        
        if (txtCodicePod.Text != StringChecked)
        {
            checkPod = true;
        }

        if (checkPdr && checkPod)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
            rowGridLibretti.Visible = false;
            rowGridHeaderLibretti.Visible = false;
            rowAzienda.Visible = false;
            rowOperatore.Visible = false;
            rowPresaInCarico.Visible = false;
        }
    }

    protected void LIM_LibrettiImpiantiSearch_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMessage.Text = string.Empty;
            rowMessage.Visible = false;
            GetLibrettiImpianti();

            if (DataGrid.Items.Count == 1) //Ho trovato il libretto che sto cercando
            {
                string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadLibrettiImpianti"] + @"\" + "LibrettoImpianto_" + DataGrid.Items[0].Cells[0].Text + ".pdf";
                if (!System.IO.File.Exists(pathPdfFile))
                {
                    #region Force generazione libretto
                    string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                    string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
                    string destinationFile = ConfigurationManager.AppSettings["UploadLibrettiImpianti"];
                    string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                    string reportName = ConfigurationManager.AppSettings["ReportNameLibrettiImpianti"];
                    
                    string urlPdf = ReportingServices.GetLibrettoImpiantoReport(DataGrid.Items[0].Cells[0].Text, reportName, reportUrl, reportPath, destinationFile, urlSite);
                    #endregion
                }

                #region Controlli su libretto trovato
                int? IDStatoLibrettoImpianto = null;
                string DataRevisione = string.Empty;
                for (int i = 0; (i <= (DataGrid.Items.Count - 1)); i++)
                {
                    IDStatoLibrettoImpianto = int.Parse(DataGrid.Items[i].Cells[4].Text);
                    if (!string.IsNullOrEmpty(DataGrid.Items[i].Cells[8].Text.Replace("&nbsp;", "")))
                    {
                        DateTime? dataRevisione = DateTime.Parse(DataGrid.Items[i].Cells[8].Text);
                        DataRevisione = String.Format("{0:dd/MM/yyyy}", dataRevisione);
                    }
                }

                switch (IDStatoLibrettoImpianto)
                {
                    case 1: //Libretto in bozza
                        SetVisibilityResult(false);
                        //rowMessage.Visible = true;
                        lblMessage.Text = "E’ presente un libretto in stato di bozza ed incompleto. Se il libretto è stato creato dall'impresa installatrice/manutentrice è opportuno che essa renda definitivo il libretto.  Fare presente al responsabile di impianto che il libretto non è stato accatastato in maniera definitiva.";
                        break;
                    case 2: //Libretto definitivo: attivo la presa in carico
                        //rowMessage.Visible = false;
                        //rowPresaInCarico.Visible = true;
                        //rowAzienda.Visible = true;
                        //rowOperatore.Visible = true;
                        //rowGridHeaderLibretti.Visible = true;
                        //rowGridLibretti.Visible = true;
                        SetVisibilityResult(true);
                        PagePermission();
                        break;
                    case 3: //Libretto revisionato
                        //rowMessage.Visible = true;
                        SetVisibilityResult(false);
                        lblMessage.Text = "E’ presente un libretto in stato di revisione. Il libretto sarà reso definitivo in maniera automatica entro "+ ConfigurationManager.AppSettings["DaysCancellazioneLibrettiBozzaRevisionati"] + " gg dalla messa in revisione ("+ DataRevisione + ") data oltre la quale potrà essere preso in carico.";
                        break;
                    case 4: //Libretto annullato
                        //rowMessage.Visible = true;
                        SetVisibilityResult(false);
                        lblMessage.Text = "E’ presente un libretto di impianto in stato annullato.";
                        break;
                }
                #endregion
            }
            else if (DataGrid.Items.Count > 1) //Devo dire di affinare la ricerca perchè ci sono troppi risultati
            {
                SetVisibilityResult(false);
                //rowMessage.Visible = true;
                lblMessage.Text = "Sono stati trovati troppi risultati. Affinare la ricerca inserendo ulteriori parametri di filtro!";
            }
            else if (DataGrid.Items.Count == 0) //Forse non ho trovato il libretto
            {
                switch (int.Parse(rblTipoRicercaLibretti.SelectedItem.Value))
                {
                    case 0:
                        SetVisibilityResult(false);
                        //Devo verificare se esiste il codice targatura ma non è legato a nessun libretto
                        bool EsisteCodiceTargaturaImpianto = UtilityTargaturaImpianti.CheckCodiceTargaturaImpianto(txtCodiceTargatura.Text);
                        if (EsisteCodiceTargaturaImpianto)
                        {
                            lblMessage.Text = "Il codice targatura è esistente ma non è stato associato a nessun Libretto presente sul CRITER. Fare presente al responsabile di impianto che il libretto non è stato accatastato.";
                        }
                        else
                        {
                            lblMessage.Text = "La ricerca non ha prodotto risultati.";
                        }
                        break;
                    case 1:
                        //Libretto non esiste proprio!!!!!
                        SetVisibilityResult(false);
                        lblMessage.Text = "La ricerca non ha prodotto risultati.";
                        break;
                }
            }



            //if (DataGrid.Items.Count > 0)
            //{
            //    rowPresaInCarico.Visible = true;
            //    rowAzienda.Visible = true;
            //    rowOperatore.Visible = true;
            //    rowGridHeaderLibretti.Visible = true;
            //    rowGridLibretti.Visible = true;

            //    PagePermission();
            //}
            //else
            //{
            //    rowPresaInCarico.Visible = false;
            //    rowAzienda.Visible = false;
            //    rowOperatore.Visible = false;
            //    rowGridHeaderLibretti.Visible = false;
            //    if (DataGrid.Items.Count == 0)
            //    {
            //        rowGridLibretti.Visible = true;
            //    }
            //    else
            //    {
            //        rowGridLibretti.Visible = false;
            //    }
            //}
        }
    }

    protected void SetVisibilityResult(bool fVisible)
    {
        rowMessage.Visible = !fVisible;
        rowPresaInCarico.Visible = fVisible;
        rowAzienda.Visible = fVisible;
        rowOperatore.Visible = fVisible;
        rowGridHeaderLibretti.Visible = fVisible;
        rowGridLibretti.Visible = fVisible;
    }

    protected void LIM_LibrettiImpiantiSearch_btnPresaInCarico_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int IDSoggetto = int.Parse(cmbAddetti.Value.ToString());
            int IDSoggettoDerived = int.Parse(cmbAziende.Value.ToString());
            for (int i = 0; (i <= (DataGrid.Items.Count - 1)); i++)
            {
                int IDLibrettoImpianto = int.Parse(DataGrid.Items[i].Cells[0].Text);

                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var libretto = ctx.LIM_LibrettiImpianti.Find(Convert.ToInt32(IDLibrettoImpianto));

                    LIM_LibrettiImpianti nuovaPresaInCaricoRevisione = UtilityLibrettiImpianti.PresaInCaricoRevisionaLibretto(libretto, IDSoggetto, IDSoggettoDerived);

                    string url = "LIM_LibrettiImpianti.aspx";
                    QueryString qs = new QueryString();
                    qs.Add("IDLibrettoImpianto", nuovaPresaInCaricoRevisione.IDLibrettoImpianto.ToString());
                    QueryString qsEncrypted = Encryption.EncryptQueryString(qs);
                                        
                    url += qsEncrypted.ToString();

                    Response.Redirect(url);
                }
            }
        }
    }


}