using DataLayer;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;

public partial class LIM_LibrettiImpiantiSearchRevisioni : System.Web.UI.Page
{
    protected string IDLibrettoImpianto
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
                            return (string) qsdec[0];
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

    protected string IDTargaturaImpianto
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
            PagePermission();
            GetLibrettiImpianti(IDTargaturaImpianto);
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                DataGrid.Columns[9].Visible = true;
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
                
                break;
            case "6": //Accertatore
            case "7": //Coordinatore
                
                break;

        }
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgView = (ImageButton) (e.Item.Cells[7].FindControl("ImgView"));
            ImageButton imgPdf = (ImageButton) (e.Item.Cells[8].FindControl("ImgPdf"));

            string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadLibrettiImpianti"] + @"\" + "LibrettoImpianto_" + e.Item.Cells[0].Text.ToString() + ".pdf";
            if (System.IO.File.Exists(pathPdfFile))
            {
                imgPdf.Visible = true;
                imgPdf.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Libretto_" + e.Item.Cells[0].Text + "', 'iframe', 'LIM_LibrettiImpiantiViewer.aspx?IDLibrettoImpianto=" + e.Item.Cells[0].Text + "', 'Libretto_" + e.Item.Cells[0].Text + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            }
            else
            {
                imgPdf.Visible = false;
            }
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpianti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
        else if (e.CommandName == "RestoreRevision")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int iDLibrettoImpianto = int.Parse(commandArgs[0].ToString());
            int iDTargaturaImpianto = int.Parse(commandArgs[1].ToString());
            
            UtilityLibrettiImpianti.RipristinaRevisioneManuale(iDLibrettoImpianto, iDTargaturaImpianto);

            GetLibrettiImpianti(IDTargaturaImpianto);
        }
        else if (e.CommandName == "Pdf")
        {

        }
    }

    public void GetLibrettiImpianti(string IDTargaturaImpianto)
    {
        int iDTargaturaImpianto = int.Parse(IDTargaturaImpianto);
        DataGrid.DataSource = UtilityLibrettiImpianti.StoricoRevisioniLibretti(iDTargaturaImpianto);
        DataGrid.DataBind();
    }

    protected void LIM_LibrettiImpiantiSearch_btnProcess_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            //QueryString qs = new QueryString();
            //qs.Add("IDLibrettoImpianto", "");
            //QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpiantiSearch.aspx";
            //url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

}