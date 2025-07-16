using DataUtilityCore;
using DataUtilityCore.Enum;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_IspezioniPianificazione : System.Web.UI.Page
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
            return "";
        }
    }
        
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDatiVerifiche(long.Parse(IDIspezioneVisita));
        }
    }
    
    protected void GetDatiVerifiche(long iDIspezioneVisita)
    {
        var ispezioni = UtilityVerifiche.GetIspezioni(iDIspezioneVisita);
        gridIspezioniNellaVisita.DataSource = ispezioni;
        gridIspezioniNellaVisita.DataBind();
    }

    protected void gridIspezioniNellaVisita_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ImageButton ImgPianificazione = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgPianificazione") as ImageButton;
            string keyValue = gridIspezioniNellaVisita.GetRowValues(e.VisibleIndex, "IDIspezione").ToString();
            string IDIspezioneVisita = gridIspezioniNellaVisita.GetRowValues(e.VisibleIndex, "IDIspezioneVisita").ToString();
            string CodiceIspezione = gridIspezioniNellaVisita.GetRowValues(e.VisibleIndex, "CodiceIspezione").ToString();
            string fIspezioneNonSvolta = gridIspezioniNellaVisita.GetRowValues(e.VisibleIndex, "fIspezioneNonSvolta").ToString();
            string fIspezioneNonSvolta2 = gridIspezioniNellaVisita.GetRowValues(e.VisibleIndex, "fIspezioneNonSvolta2").ToString();
            string fIspezioneRipianificata = gridIspezioniNellaVisita.GetRowValues(e.VisibleIndex, "fIspezioneRipianificata").ToString();

            ImgPianificazione.Attributes.Add("onclick", "OpenPopupWindows(this, " + keyValue + "); return false;");

            ImageButton ImgConferma = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgConferma") as ImageButton;
            ImageButton ImgConfermaRipianificazione = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgConfermaRipianificazione") as ImageButton;
            ImageButton ImgPianificazioneDocuments = gridIspezioniNellaVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgPianificazioneDocuments") as ImageButton;
            ImgPianificazioneDocuments.Attributes.Add("onclick", "var winPianificazione=dhtmlwindow.open('ConfermaPianificazione_" + keyValue + "', 'iframe', 'VER_PianificazioneViewer.aspx?IDIspezione=" + keyValue + "&IDIspezioneVisita=" + IDIspezioneVisita + "&CodiceIspezione=" + CodiceIspezione + "&fIspezioneNonSvolta=" + fIspezioneNonSvolta + "&fIspezioneNonSvolta2=" + fIspezioneNonSvolta2 + "&fIspezioneRipianificata="+ fIspezioneRipianificata + "', 'ConfermaPianificazione_" + keyValue + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            ImgPianificazioneDocuments.Attributes.Add("onmouseout", "this.style.cursor='pointer'");

            int iDStatoIspezione = (int)e.GetValue("IDStatoIspezione");

            if (userInfo.IDRuolo == 1 || userInfo.IDRuolo == 7 || userInfo.IDRuolo == 12) //Amministratore Criter/Coordinatori/Amministrazione
            {
                ImgPianificazioneDocuments.Visible = true;
                if (iDStatoIspezione == 2 || iDStatoIspezione == 3) //Ispezione da pianificare
                {
                    ImgPianificazione.Visible = true;
                } 
                if (iDStatoIspezione == 3) //Ispezione da pianificare
                {
                    ImgConferma.Visible = true;
                }
                if (iDStatoIspezione == 7)
                {
                    ImgConfermaRipianificazione.Visible = true;
                }
            }
            else if (userInfo.IDRuolo == 8 || userInfo.IDRuolo == 14) //Ispettori
            {
                ImgConferma.Visible = false;

                if (iDStatoIspezione == 2 || iDStatoIspezione == 3) //Ispezione da pianificare
                {
                    ImgPianificazione.Visible = true;
                }
            }
        }
    }

    protected void imgConfirm_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        long iDIspezione = long.Parse(commandArgs[0].ToString());
        long iDIspezioneVisita = long.Parse(commandArgs[1].ToString());
        bool fIspezioneRipianificata = bool.Parse(commandArgs[2].ToString());
        string CodiceIspezione = commandArgs[3].ToString();
        bool fTerzoResponsabile = bool.Parse(commandArgs[4].ToString());

        if (e.CommandName == "ConfermaPianificazione")
        {
            
            UtilityVerifiche.CambiaStatoIspezioneMassivo(iDIspezioneVisita,
                                                         7,
                                                         iDIspezione,
                                                         null,
                                                         null,
                                                         null
                                                         );

            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));

            //Rigenero per sicurezza il documento di pianificazione ispezione

            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + CodiceIspezione;
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
            string reportName = string.Empty;
            string urlPdf = string.Empty;

            //Commentato perchè forse non serve
            //UtilityVerifiche.SetfRipianificazioneIspezione(iDIspezione, false);
            if (!fIspezioneRipianificata)
            {
                //Rigenero per sicurezza il documento di pianificazione ispezione
                reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneConferma"];
                urlPdf = ReportingServices.GetIspezionePianificazioneConfermaReport(iDIspezione.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
                if (fTerzoResponsabile)
                {
                    EmailNotify.SendMailPecPerTerzoResponsabileComunicazioneIspezione(iDIspezione);
                }
                else
                {
                    UtilityPosteItaliane.SendToPosteItaliane(iDIspezione, (int)EnumTypeofRaccomandata.TypePianificazioneIspezioneConferma);
                    EmailNotify.SendMailPecPerImpresaComunicazioneIspezione(iDIspezione);
                }
            }
            else if (fIspezioneRipianificata)
            {
                //Rigenero per sicurezza il documento di ripianificazione ispezione
                reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneRipianificazione"];
                urlPdf = ReportingServices.GetIspezionePianificazioneRipianificazioneReport(iDIspezione.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

                if (fTerzoResponsabile)
                {
                    EmailNotify.SendMailPecPerTerzoResponsabileComunicazioneIspezione(iDIspezione);
                }
                //14092023: Commentato
                //else
                //{
                //    UtilityPosteItaliane.SendToPosteItaliane(iDIspezione, (int)EnumTypeofRaccomandata.TypePianificazioneIspezioneRipianificazione);
                //}                
            }
        }
        else if (e.CommandName == "ConfermaRipianificazione")
        {
            //Storicizzare prima le vecchie date di ispezione
            UtilityVerifiche.StoricizzaDataIspezionePrecedente(iDIspezione);

            UtilityVerifiche.CambiaStatoIspezioneMassivo(iDIspezioneVisita,
                                                         2,
                                                         iDIspezione,
                                                         null,
                                                         null,
                                                         null
                                                         );

            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            UtilityVerifiche.SetfRipianificazioneIspezione(iDIspezione, true);           
        }
        else if (e.CommandName == "EditPianificazione")
        {
            QueryString qs = new QueryString();
            qs.Add("IDIspezione", iDIspezione.ToString());
            qs.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_IspezioniPianificazioneDetails.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }

        GetDatiVerifiche(iDIspezioneVisita);
    }
    
    protected void btnIspezioni_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDIspezioneVisita", IDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_IspezioniSearch.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

}