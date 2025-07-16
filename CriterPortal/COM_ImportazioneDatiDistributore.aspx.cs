using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class COM_ImportazioneDatiDistributore : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
            cmbAziende.Focus();
            ddLoadAnni();          
        }

        if (userInfo.IDRuolo == 1)
        {
            GetDatiDistributori(null);
        }
        else
        {
            GetDatiDistributori(cmbAziende.Value);
        }
    }

    protected void ddLoadAnni()
    {
        ListEditItem li1 = new ListEditItem(DateTime.Now.AddYears(-2).ToString("yyyy"));
        cmbAnni.Items.Add(li1);
        ListEditItem li2 = new ListEditItem(DateTime.Now.AddYears(-1).ToString("yyyy"));
        cmbAnni.Items.Add(li2);
        ListEditItem Now = new ListEditItem(DateTime.Now.ToString("yyyy"));
        cmbAnni.Items.Add(Now);
        ListEditItem li3 = new ListEditItem(DateTime.Now.AddYears(+1).ToString("yyyy"));
        cmbAnni.Items.Add(li3);
        ListEditItem li4 = new ListEditItem(DateTime.Now.AddYears(+2).ToString("yyyy"));
        cmbAnni.Items.Add(li4);
        ListEditItem li5 = new ListEditItem(DateTime.Now.AddYears(+3).ToString("yyyy"));
        cmbAnni.Items.Add(li5);
        ListEditItem li6 = new ListEditItem(DateTime.Now.AddYears(+4).ToString("yyyy"));
        cmbAnni.Items.Add(li6);
        ListEditItem li7 = new ListEditItem(DateTime.Now.AddYears(+5).ToString("yyyy"));
        cmbAnni.Items.Add(li7);
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                cmbAziende.Visible = true;
                lblSoggetto.Visible = false;
                Grid.Columns[5].Visible = true;
                break;
            case "4": //Distributore di combustibile
                cmbAziende.Value = getVal[0];
                cmbAziende.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                Grid.Columns[5].Visible = false;
                break;
        }
    }

    #region AZIENDA

    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "5", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "5", e.Value.ToString(), "");
        comboBox.DataBind();
    }
    
    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        cmbAziende.SelectedIndex = -1;
        cmbAziende.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "5", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbAziende.DataBind();
    }
    #endregion

    protected void btnElaboraFileXml_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (UploadFileXml.HasFile && UploadFileXml.PostedFile != null)
            {
                List<string> errors = new List<string>();
                if (!UtilityDatiDistributoriImportXml.ImportComunicazioneUtenzeEnergetiche(UploadFileXml.PostedFile.InputStream, cmbAziende.Value, UploadFileXml.PostedFile.FileName, int.Parse(cmbAnni.Value.ToString()), out errors))
                {
                    lblEsitoImportazione.Visible = true;
                    lblEsitoImportazione.Text = "Attenzione, il file NON è stato importato a causa dei seguenti errori di importazione: <br/>";

                    string errori = string.Empty;
                    foreach (var errore in errors)
                    {
                        errori += "<br/>" + errore;
                    }
                    lblEsitoImportazione.Text = errori.Replace("<", "").Replace(">", "");
                    lblEsitoImportazione.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblEsitoImportazione.Visible = true;
                    lblEsitoImportazione.Text = "Importazione file xml effettuata con successo.";
                    lblEsitoImportazione.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                lblEsitoImportazione.Visible = true;
                lblEsitoImportazione.Text = "Attenzione è necessario selezionare un file xml da importare";
                lblEsitoImportazione.ForeColor = System.Drawing.Color.Red;
            }

            if (userInfo.IDRuolo == 1)
            {
                GetDatiDistributori(null);
            }
            else
            {
                GetDatiDistributori(cmbAziende.Value);
            }
        }
    }

    public void GetDatiDistributori(object iDSoggetto)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var datiDistributore = (from a in ctx.UTE_Comunicazioni
                                    join l in ctx.COM_AnagraficaSoggetti on a.IDSoggetto equals l.IDSoggetto
                                    select new
                                    {
                                        ID = a.Id,
                                        IDSoggetto = a.IDSoggetto,
                                        NomeAzienda = l.NomeAzienda,
                                        NomeFile = a.NomeFile,
                                        DataOraElaborazione = a.DataOraElaborazione,
                                        NumeroUtenze = a.NumeroUtenze,
                                        AnnoRiferimento = a.AnnoRiferimento
                                    }).AsQueryable();

            if (iDSoggetto !=null)
            {
                int iDSoggettoInt = int.Parse(iDSoggetto.ToString());
                datiDistributore = datiDistributore.Where(u => u.IDSoggetto == iDSoggettoInt);
            }
            
            Grid.DataSource = datiDistributore.ToList();
            Grid.DataBind();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        GridViewDataItemTemplateContainer c = (GridViewDataItemTemplateContainer)ib.NamingContainer;
        UtilityDatiDistributoriImportXml.DeleteDatiDistributori(int.Parse(c.KeyValue.ToString()));

        if (userInfo.IDRuolo == 1)
        {
            GetDatiDistributori(null);
        }
        else
        {
            GetDatiDistributori(cmbAziende.Value);
        }
    }

    protected void Grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
       
    }
}