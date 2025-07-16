using DataUtilityCore;
using System;
using System.Configuration;
using System.Linq;
using DevExpress.Web;
using System.Web;
using Microsoft.Reporting.WebForms;
using EncryptionQS;

public partial class COM_Reports : System.Web.UI.Page
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "COM_Reports.aspx");

        //Response.AddHeader("PRAGMA", "NO-CACHE");
        //Response.ExpiresAbsolute = DateTime.Now.AddMinutes(-1);
        //Response.Expires = 0;
        //Response.CacheControl = "no-cache";

        if (!Page.IsPostBack)
        {
            ddlReports();
            PagePermission();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                
                break;
            case "2": //Amministratore azienda
                
                break;
            case "3": //Operatore/Addetto
                
                break;
            case "10": //Responsabile tecnico
                
                break;
            case "11": //Software house
                
                break;
            case "13": //Cittadino
                
                break;
            case "16": //Ente locale
                ddlReport.SelectedIndex = 1;
                rowListReports.Visible = false;
                rowListReports1.Visible = false;
                rowListReports2.Visible = false;
                rowViewReport.Visible = false;
                rowDownloadEnti.Visible = false;

                //Procedura con link per scaricare excel
                ReportViewer1.ShowExportControls = false;
                rowDownloadEnti.Visible = true;
                SetUrlDownload(info.IDSoggetto.ToString());
                GetReport();               
                break;
            case "6": //Accertatore
            case "7": //Coordinatore
                
                break;

        }
    }

    public void SetUrlDownload(string iDSoggetto)
    {
        QueryString qs = new QueryString();
        qs.Add("IDSoggetto", iDSoggetto);
        
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = ConfigurationManager.AppSettings["UrlPortal"] + "EntiReportDownload.aspx";
        url += qsEncrypted.ToString();

        this.imgExportXlsEnti.Attributes.Add("onclick",
            "var win=dhtmlwindow.open('DownloadCsvEnti_" + iDSoggetto + "', 'iframe', '" +
            url +
            "', 'Scarica file csv', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
        this.imgExportXlsEnti.Attributes.Add("style", "cursor: pointer;");
    }

    protected void ddlReports()
    {
        var ls = LoadDropDownList.LoadDropDownList_Reports(null);
        ddlReport.ValueField = "IDReport";
        ddlReport.TextField = "ReportDescription";
        ddlReport.DataSource = ls;
        ddlReport.DataBind();
        

        ListEditItem myItem = new ListEditItem("-- Selezionare --", "0");
        ddlReport.Items.Insert(0, myItem);
        ddlReport.SelectedIndex = 0;
    }

    protected void COM_Reports_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetReport();
        }
    }

    protected void GetReport()
    {
        int? iDReport = int.Parse(ddlReport.Value.ToString());
        var ls = LoadDropDownList.LoadDropDownList_Reports(iDReport).FirstOrDefault();

        string reportName = ls.ReportName;

        string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
        string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
        string credentialsDomain = string.Empty;

        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
        {
            credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
        }

        ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);

        ReportViewer1.ApplyStyleSheetSkin(this);
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportRemoteURL"]);
        ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"] + reportName;

        if (info.IDRuolo == 16)
        {
            ReportParameter rp0 = new ReportParameter("IDSoggetto", info.IDSoggetto.ToString());
            ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { rp0 });
        }

        rowViewReport.Visible = true;

        ReportViewer1.ServerReport.Refresh();
    }

    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReportViewer1.Reset();

        if (ddlReport.Value.ToString() == "0")
        {
            rowViewReport.Visible = false;
        }
    }

}