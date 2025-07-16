using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;

public partial class WebUserControls_Ispezioni_UCIspezioneDocumenti : System.Web.UI.UserControl
{
    public string IDIspezione
    {
        get { return lblIDIspezione.Text; }
        set
        {
            lblIDIspezione.Text = value;
        }
    }

    public string IDIspezioneVisita
    {
        get { return lblIDIspezioneVisita.Text; }
        set
        {
            lblIDIspezioneVisita.Text = value;
        }
    }

    public string CodiceIspezione
    {
        get { return lblCodiceIspezione.Text; }
        set
        {
            lblCodiceIspezione.Text = value;
        }
    }


    public override void DataBind()
    {
        base.DataBind();
        GetDatiDocumenti();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetDatiDocumenti();
    }

    public void EnableIspezioneDocumenti(int iDStatoIspezione)
    {
        switch (iDStatoIspezione)
        {
            case 1: //Ricerca Ispettore
                
                break;
            case 2: //Ispezione da Pianificare
                
                break;
            case 3://Ispezione Pianificata in attesa di conferma
                
                break;
            case 4: //Ispezione Conclusa da Ispettore
                UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                var iDRuolo = userInfo.IDRuolo;
                if (iDRuolo == 8 || iDRuolo == 14)
                {
                    gridIspezioniDocumenti.Columns[8].Visible = false;
                }
                break;
            case 5: //Ispezione Conclusa da Coordinatore con accertamento
                
                break;
            case 6: //Ispezione Confermata da Inviare LAI
                
                break;
            case 7://Ispezione Pianificata confermata
                
                break;
            case 8://Ispezione Annullata
                
                break;
            case 9://Ispezione Conclusa da Coordinatore senza accertamento
                
                break;
        }
    }


    protected void GetDatiDocumenti()
    {
        int iDIspezione = int.Parse(IDIspezione);

        var documenti = UtilityVerifiche.GetIspezioniDocumenti(iDIspezione);
        gridIspezioniDocumenti.DataSource = documenti;
        gridIspezioniDocumenti.DataBind();
    }

    protected void gridIspezioniDocumenti_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string keyValue = gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "IDIspezioneDocumento").ToString();
            string nomeDocumentoIspezione = gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "NomeDocumentoIspezione").ToString();
            string estensione = gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "Estensione").ToString();
            bool fdocumentoCompilato = bool.Parse(gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "fDocumentoCompilato").ToString());
            bool fDocumentoObbligatorio = bool.Parse(gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "fDocumentoObbligatorio").ToString());
            string iDTipoDocumentoIspezione = gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "IDTipoDocumentoIspezione").ToString();
            string TipoDocumentoIspezione = gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "TipoDocumentoIspezione").ToString();

            ImageButton imgDocument = gridIspezioniDocumenti.FindRowCellTemplateControl(e.VisibleIndex, null, "imgDocument") as ImageButton;
            if (nomeDocumentoIspezione != "")
            {
                imgDocument.ToolTip = "Scarica documento " + TipoDocumentoIspezione;
                imgDocument.Attributes.Add("onclick", "var winDocumentoIspezione=dhtmlwindow.open('IspezioniDocumenti_" + keyValue + "', 'iframe', 'VER_IspezioniViewer.aspx?Type=Generate&nomeDocumentoIspezione=" + nomeDocumentoIspezione + "&estensione=" + estensione + "&CodiceIspezione=" + CodiceIspezione + "&IDIspezione=" + IDIspezione + "&IDIspezioneVisita=" + IDIspezioneVisita + "', 'IspezioniDocumenti_" + keyValue + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            }
            else
            {
                imgDocument.Visible = false;
            }

            ImageButton imgDocumentRapportoIspezioneVuoto = gridIspezioniDocumenti.FindRowCellTemplateControl(e.VisibleIndex, null, "imgDocumentRapportoIspezioneVuoto") as ImageButton;
            imgDocumentRapportoIspezioneVuoto.Attributes.Add("onclick", "var winDocumentoIspezione=dhtmlwindow.open('IspezioniRapportoIspezioneVuoto_" + keyValue + "', 'iframe', 'VER_IspezioniViewer.aspx?Type=Download&nomeDocumentoIspezione=RapportoIspezioneVuoto_"+ IDIspezione + ".pdf&estensione=.pdf&CodiceIspezione=" + CodiceIspezione + "&IDIspezione=" + IDIspezione + "&IDIspezioneVisita=" + IDIspezioneVisita + "', 'IspezioniRapportoIspezioneVuoto_" + keyValue + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            if (iDTipoDocumentoIspezione == "8")
            {
                imgDocumentRapportoIspezioneVuoto.Visible = true;
            }

            Image imgDocumentoCompilato = gridIspezioniDocumenti.FindRowCellTemplateControl(e.VisibleIndex, null, "imgDocumentoCompilato") as Image;
            imgDocumentoCompilato.ImageUrl = UtilityApp.ToImage(fdocumentoCompilato);

            Label lblDocumentoObbligatorio = gridIspezioniDocumenti.FindRowCellTemplateControl(e.VisibleIndex, null, "lblDocumentoObbligatorio") as Label;
            lblDocumentoObbligatorio.Text = UtilityApp.BooleanFlagToString(fDocumentoObbligatorio);

            var nomeDocumentoCaricato = gridIspezioniDocumenti.GetRowValues(e.VisibleIndex, "NomeDocumento");
            var linkButtonDownloadDocumento = gridIspezioniDocumenti.FindRowCellTemplateControl(e.VisibleIndex, null, "linkButtonDownloadDocumento") as System.Web.UI.HtmlControls.HtmlAnchor;
            if (nomeDocumentoCaricato !=null)
            {
                linkButtonDownloadDocumento.Visible = true;
                if (linkButtonDownloadDocumento != null)
                {
                    var estensioneDocumento = System.IO.Path.GetExtension(nomeDocumentoCaricato.ToString());
                    string linkDownload = "VER_IspezioniViewer.aspx?Type=Download&nomeDocumentoIspezione=" + nomeDocumentoCaricato + "&estensione=" + estensioneDocumento + "&CodiceIspezione=" + CodiceIspezione + "&IDIspezione=" + IDIspezione + "&IDIspezioneVisita=" + IDIspezioneVisita + "";
                    linkButtonDownloadDocumento.Attributes.Add("onclick", "OpenInNewTab('" + linkDownload + "'); return false");
                }
            }
            else
            {
                linkButtonDownloadDocumento.Visible = false;
            }
        }
    }

    #region Upload documents
    protected void uploadDocument_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        if (e.IsValid)
        {
            ASPxUploadControl uploadDocument = sender as ASPxUploadControl;
            GridViewDataItemTemplateContainer container = uploadDocument.NamingContainer as GridViewDataItemTemplateContainer;
            var iDIspezioneDocumento = long.Parse(container.Grid.GetRowValues(container.VisibleIndex, "IDIspezioneDocumento").ToString());

            string path = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + "\\" + IDIspezioneVisita + "\\" + lblCodiceIspezione.Text + "\\";
            string fileName =  e.UploadedFile.FileName;
            
            e.UploadedFile.SaveAs(path + fileName, true);

            UtilityVerifiche.fDocumentoIspezioneCompilato(iDIspezioneDocumento, fileName);

            e.CallbackData = fileName;            
        }
    }

    public bool CheckAllCompiledDocument(string iDIspezione)
    {
        bool fAllCompiled = false;

        for (int i = 0; i < gridIspezioniDocumenti.VisibleRowCount; i++)
        {
            var test = gridIspezioniDocumenti.GetRowValues(i, "TipoDocumentoIspezione");

            var fDocumentoCompilato = gridIspezioniDocumenti.GetRowValues(i, "fDocumentoCompilato");
            var fDocumentoObbligatorio = gridIspezioniDocumenti.GetRowValues(i, "fDocumentoObbligatorio");

            if (fDocumentoObbligatorio != null && fDocumentoCompilato != null)
            {
                if (!bool.Parse(fDocumentoCompilato.ToString()) && bool.Parse(fDocumentoObbligatorio.ToString()))
                {
                    fAllCompiled = false;
                    break;
                }
                else
                {
                    fAllCompiled = true;
                }                
            }           
        }

        return fAllCompiled;
    }



    protected void ASPxHyperLink_Load(object sender, EventArgs e)
    {
        ASPxHyperLink hpl = (ASPxHyperLink)sender;
        GridViewDataItemTemplateContainer c = (GridViewDataItemTemplateContainer)hpl.NamingContainer;
        //if (!String.IsNullOrWhiteSpace(FileList[c.VisibleIndex].FileName) && !String.IsNullOrWhiteSpace(FileList[c.VisibleIndex].Url))
        //{
        //    hpl.Text = FileList[c.VisibleIndex].FileName;
        //    hpl.NavigateUrl = FileList[c.VisibleIndex].Url;
        //}
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        
    }
    protected void ASPxGridView1_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        
    }
    protected void ASPxCallback1_Callback(object source, CallbackEventArgs e)
    {
        //string fileName = e.Parameter;
        //foreach (MySavedObjects myObj in FileList)
        //{
        //    if (myObj.FileName == fileName)
        //        myObj.FileName = myObj.Url = string.Empty;
        //}
        //File.Delete(Server.MapPath("~/Documents/" + fileName));
        //e.Result = "ok";
    }




    #endregion

    protected void uploadDocument_Init(object sender, EventArgs e)
    {
        ASPxUploadControl uploadDocument = sender as ASPxUploadControl;
        GridViewDataItemTemplateContainer container = uploadDocument.NamingContainer as GridViewDataItemTemplateContainer;

        var iDIspezioneDocumento = Convert.ToDecimal(container.Grid.GetRowValues(container.VisibleIndex, "IDIspezioneDocumento"));


    }
}