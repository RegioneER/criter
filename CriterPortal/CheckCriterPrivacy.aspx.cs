using DataUtilityCore;
using System;
using System.Configuration;
using System.Drawing;
using System.Web;
using System.Web.UI;

public partial class CheckCriterPrivacy : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int nTentativi = int.Parse(ConfigurationManager.AppSettings["NTentativiPrivacy"]);
            lblCountRimanenti.Text = (nTentativi - UtilitySoggetti.GetCountRimanentiPrivacy(userInfo.IDSoggetto)).ToString();
            imgExportDocument.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"].ToString() + "template/SUBRESPONSABILE_manutentore_CRITER.pdf";
            imgExportDocument1.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"].ToString() + "template/SUBRESPONSABILE_manutentore_CRITER.pdf";
            
            if (userInfo.fSpid)
            {
                rowSpid.Visible = true;
                rowFirmaDigitale.Visible = false;
            }
            else
            {
                rowSpid.Visible = false;
                rowFirmaDigitale.Visible = true;
            }
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LIM_LibrettiImpiantiSearch.aspx");
    }
    
    protected void btnFirma_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (userInfo.fSpid)
            {
                #region SPID
                int? iDSoggetto = userInfo.IDSoggetto;
                UtilitySoggetti.ChangefPrivacySoggetto(iDSoggetto);

                lblCheckP7m.Text = "Documento firmato correttamente";

                string jScript = "setTimeout('Redirect()',2000); function Redirect() { location.href = 'Default.aspx'; }";
                string jScriptKey = "KeyScript";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), jScriptKey, jScript, true);
                #endregion
            }
            else
            {
                #region FIRMA DITALE
                if (UploadFileP7m.HasFile && UploadFileP7m.PostedFile != null)
                {
                    int? iDSoggetto = userInfo.IDSoggetto;

                    string FileP7m = UploadFileP7m.FileName;
                    string PathP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\";
                    string PathAndFileP7m = PathP7mFile + "Privacy_" + iDSoggetto + ".p7m";
                    UploadFileP7m.SaveAs(PathAndFileP7m);
                    UtilitySoggetti.ChangefPrivacySoggetto(iDSoggetto);

                    lblCheckP7m.Text = "Documento firmato correttamente";

                    string jScript = "setTimeout('Redirect()',2000); function Redirect() { location.href = 'Default.aspx'; }";
                    string jScriptKey = "KeyScript";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), jScriptKey, jScript, true);
                }
                else
                {
                    lblCheckP7m.ForeColor = ColorTranslator.FromHtml("#800000");
                    lblCheckP7m.Text = "<br/>Attenzione: bisogna selezionare il file firmato digitalmente in formato .p7m da caricare!";
                }
                #endregion
            }
        }
    }
    
}