using DataLayer;
using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;

public partial class ConfermaIspezioneIncarico : System.Web.UI.Page
{
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
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["IDSoggetto"] != null)
                    {
                        return (string)Request.QueryString["IDSoggetto"];
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

    protected string IDIspettore
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
                        if (qsdec[1] != null)
                        {
                            return (string)qsdec[1];
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
        if (!Page.IsPostBack)
        {
            long? iDIspezioneVisita = long.Parse(IDIspezioneVisita);
            long? iDIspettore = int.Parse(IDIspettore);

            if (IDIspezioneVisita != null && IDIspettore != null && !UtilityVerifiche.IspettoreAccessoPaginaConfermaIncarico(iDIspezioneVisita, iDIspettore))
            {
                rowInfoIncaricoOk.Visible = false;
                rowInfoIncaricoGiaConfermato.Visible = true;
                rowFirmaDigitale.Visible = false;
                rowFirmaDigitale1.Visible = false;
                rowFirmaDigitale2.Visible = false;
                rowDettaglioIspezioni.Visible = false;
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
            }
            else
            {
                rowInfoIncaricoOk.Visible = false;
                rowInfoIncaricoGiaConfermato.Visible = false;
                rowFirmaDigitale.Visible = true;
                rowFirmaDigitale1.Visible = true;
                rowFirmaDigitale2.Visible = true;
                rowDettaglioIspezioni.Visible = true;
                GetDataIspezioni((long)iDIspezioneVisita, (long)iDIspettore);

                SetUrlDownload(IDIspezioneVisita.ToString(), IDIspettore.ToString());
            }
        }
    }

    public void GetDataIspezioni(long iDIspezioneVisita, long iDIspettore)
    {
        using (var ctx = new CriterDataModel())
        {
            var ispettore = ctx.V_COM_AnagraficaSoggetti.AsQueryable().Where(c => c.IDSoggetto == iDIspettore).FirstOrDefault();
            lblIspettore.Text = "Pregiatissimo Ispettore " + ispettore.Nome + "&nbsp;" + ispettore.Cognome;
        }

        var ispezioni = UtilityVerifiche.GetIspezioni(iDIspezioneVisita);
        DataGrid.DataSource = ispezioni;
        DataGrid.DataBind();
    }

    public void SetUrlDownload(string IDIspezioneVisita, string IDIspettore)
    {
        QueryString qs = new QueryString();
        qs.Add("IDIspezioneVisita", IDIspezioneVisita.ToString());
        qs.Add("IDIspettore", IDIspettore.ToString());
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = ConfigurationManager.AppSettings["UrlPortal"] + "LetteraIncaricoDownload.aspx";
        url += qsEncrypted.ToString();

        this.imgExportPdfLetteraIncarico.Attributes.Add("onclick",
            "var win=dhtmlwindow.open('LetteraIncaricoExport_" + IDIspezioneVisita.ToString() + "', 'iframe', '" +
            url +
            "', 'Scarica Lettera di Incarico', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
        this.imgExportPdfLetteraIncarico.Attributes.Add("style", "cursor: pointer;");
    }

    protected void TypeFirmaIncarico(long IDIspezioneVisita, long IDIspettore)
    {
        #region FIRMA
        if (UploadFileP7m.HasFile && UploadFileP7m.PostedFile != null)
        {
            #region Firma
            string FileP7m = UploadFileP7m.FileName;
            string PathP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\";
            UtilityFileSystem.CreateDirectoryIfNotExists(PathP7mFile);
            UploadFileP7m.SaveAs(PathP7mFile + FileP7m);

            UtilityVerifiche.LetteraIncaricoFirmata(IDIspezioneVisita);

            rowInfoIncaricoOk.Visible = true;
            rowFirmaDigitale.Visible = false;
            rowFirmaDigitale1.Visible = false;
            rowFirmaDigitale2.Visible = false;
            rowDettaglioIspezioni.Visible = false;

            QueryString qs = new QueryString();
            qs.Add("IDIspezioneVisita", IDIspezioneVisita.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_IspezioniSearch.aspx";
            url += qsEncrypted.ToString();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + url + "';},4000);", true);
            #endregion
        }
        else
        {
            lblCheckP7m.ForeColor = System.Drawing.ColorTranslator.FromHtml("#800000");
            lblCheckP7m.Text = "Attenzione: bisogna selezionare il file firmato digitalmente in formato .p7m da caricare!";
        }
        #endregion
    }

    protected void btnConfermaIncarico_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);
            long iDIspettore = long.Parse(IDIspettore);

            TypeFirmaIncarico(iDIspezioneVisita, iDIspettore);
        }
    }

}