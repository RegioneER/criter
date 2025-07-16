using DataLayer;
using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public partial class LIM_LibrettiImpiantiDismissione : System.Web.UI.Page
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

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
        if (!IsPostBack)
        {
            PagePermission();
            GetTipiGruppiGeneratori(IDLibrettoImpianto);
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
            case "7": //Coordinatore Accertamenti

                break;
            case "2": //Amministratore azienda
            case "3": //Operatore/Addetto
            case "10": //Responsabile tecnico
            case "13": //Cittadino
                gridGT.Columns[5].Visible = false;
                gridGF.Columns[5].Visible = false;
                gridSC.Columns[5].Visible = false;
                gridCG.Columns[5].Visible = false;
                break;
        }
    }

    public void GetTipiGruppiGeneratori(string IDLibrettoImpianto)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            #region Generatori from Libretto
            if (!string.IsNullOrEmpty(IDLibrettoImpianto))
            {
                int IDLibrettoImpiantoInt = int.Parse(IDLibrettoImpianto);
                #region GT
                var gt = ctx.LIM_LibrettiImpiantiGruppiTermici.Select(c => new
                {
                    Tipologia = c.SYS_TipologiaGruppiTermici.TipologiaGruppiTermici,
                    DataInstallazione = c.DataInstallazione,
                    Fabbricante = c.Fabbricante,
                    Modello = c.Modello,
                    Matricola = c.Matricola,
                    AnalisiFumoPrevisteNr = c.AnalisiFumoPrevisteNr,
                    IDLibrettoImpiantoGruppoTermico = c.IDLibrettoImpiantoGruppoTermico,
                    IDLibrettoImpianto = c.IDLibrettoImpianto,
                    Prefisso = c.Prefisso,
                    CodiceProgressivo = c.CodiceProgressivo,
                    fAttivo = c.fAttivo,
                    fDismesso = c.fDismesso,
                    DataDismesso = c.DataDismesso,
                    IDUtenteDismesso = c.IDUtenteDismesso

                }).AsQueryable();

                gt = gt.Where(c => c.fAttivo == true);
                gt = gt.Where(c => c.IDLibrettoImpianto == IDLibrettoImpiantoInt);

                var dsGT = gt.ToList();
                if (dsGT.Count > 0)
                {
                    gridGT.DataSource = dsGT;
                    gridGT.DataBind();
                    rowGT.Visible = true;
                }
                else
                {
                    rowGT.Visible = false;
                }
                #endregion

                #region GF
                var gf = ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Select(c => new
                {
                    IDLibrettoImpiantoMacchinaFrigorifera = c.IDLibrettoImpiantoMacchinaFrigorifera,
                    Tipologia = c.SYS_TipologiaMacchineFrigorifere.TipologiaMacchineFrigorifere,
                    NumCircuiti = c.NumCircuiti,
                    DataInstallazione = c.DataInstallazione,
                    Fabbricante = c.Fabbricante,
                    Modello = c.Modello,
                    Matricola = c.Matricola,
                    IDLibrettoImpianto = c.IDLibrettoImpianto,
                    Prefisso = c.Prefisso,
                    CodiceProgressivo = c.CodiceProgressivo,
                    fAttivo = c.fAttivo,
                    fDismesso = c.fDismesso,
                    DataDismesso = c.DataDismesso,
                    IDUtenteDismesso = c.IDUtenteDismesso

                }).AsQueryable();

                gf = gf.Where(c => c.fAttivo == true);
                gf = gf.Where(c => c.IDLibrettoImpianto == IDLibrettoImpiantoInt);

                var dsGF = gf.ToList();
                if (dsGF.Count > 0)
                {
                    gridGF.DataSource = dsGF;
                    gridGF.DataBind();
                    rowGF.Visible = true;
                }
                else
                {
                    rowGF.Visible = false;
                }
                #endregion

                #region SC
                var sc = ctx.LIM_LibrettiImpiantiScambiatoriCalore.Select(c => new
                {
                    IDLibrettoImpiantoScambiatoreCalore = c.IDLibrettoImpiantoScambiatoreCalore,
                    DataInstallazione = c.DataInstallazione,
                    Fabbricante = c.Fabbricante,
                    Modello = c.Modello,
                    Matricola = c.Matricola,
                    IDLibrettoImpianto = c.IDLibrettoImpianto,
                    Prefisso = c.Prefisso,
                    CodiceProgressivo = c.CodiceProgressivo,
                    fAttivo = c.fAttivo,
                    fDismesso = c.fDismesso,
                    DataDismesso = c.DataDismesso,
                    IDUtenteDismesso = c.IDUtenteDismesso

                }).AsQueryable();

                sc = sc.Where(c => c.IDLibrettoImpianto == IDLibrettoImpiantoInt);
                sc = sc.Where(c => c.fAttivo == true);

                var dsSC = sc.ToList();
                if (dsSC.Count > 0)
                {
                    gridSC.DataSource = dsSC;
                    gridSC.DataBind();
                    rowSC.Visible = true;
                }
                else
                {
                    rowSC.Visible = false;
                }
                #endregion

                #region CG
                var cg = ctx.LIM_LibrettiImpiantiCogeneratori.Select(c => new
                {
                    IDLibrettoImpiantoCogeneratore = c.IDLibrettoImpiantoCogeneratore,
                    Tipologia = c.SYS_TipologiaCogeneratore.TipologiaCogeneratore,
                    DataInstallazione = c.DataInstallazione,
                    Fabbricante = c.Fabbricante,
                    Modello = c.Modello,
                    Matricola = c.Matricola,
                    IDLibrettoImpianto = c.IDLibrettoImpianto,
                    Prefisso = c.Prefisso,
                    CodiceProgressivo = c.CodiceProgressivo,
                    fAttivo = c.fAttivo,
                    fDismesso = c.fDismesso,
                    DataDismesso = c.DataDismesso,
                    IDUtenteDismesso = c.IDUtenteDismesso

                }).AsQueryable();

                cg = cg.Where(c => c.IDLibrettoImpianto == IDLibrettoImpiantoInt);
                cg = cg.Where(c => c.fAttivo == true);

                var dsCG = cg.ToList();
                if (dsCG.Count > 0)
                {
                    gridCG.DataSource = dsCG;
                    gridCG.DataBind();
                    rowCG.Visible = true;
                }
                else
                {
                    rowCG.Visible = false;
                }
                #endregion
                
                //if ((dsGT.Count == 1) && (dsGF.Count == 0) && (dsSC.Count == 0) && (dsCG.Count == 0))
                //{
                //    CheckBox chk = (CheckBox)gridGT.Items[0].Cells[5].FindControl("chkSelezione");
                //    chk.Checked = true;
                //}
                //if ((dsGT.Count == 0) && (dsGF.Count == 1) && (dsSC.Count == 0) && (dsCG.Count == 0))
                //{
                //    CheckBox chk = (CheckBox)gridGF.Items[0].Cells[5].FindControl("chkSelezione");
                //    chk.Checked = true;
                //}
                //if ((dsGT.Count == 0) && (dsGF.Count == 0) && (dsSC.Count == 1) && (dsCG.Count == 0))
                //{
                //    CheckBox chk = (CheckBox)gridSC.Items[0].Cells[5].FindControl("chkSelezione");
                //    chk.Checked = true;
                //}
                //if ((dsGT.Count == 0) && (dsGF.Count == 0) && (dsSC.Count == 0) && (dsCG.Count == 1))
                //{
                //    CheckBox chk = (CheckBox)gridCG.Items[0].Cells[5].FindControl("chkSelezione");
                //    chk.Checked = true;
                //}
            }
            else
            {
                rowNoResult.Visible = true;
            }
            #endregion
        }
    }

    public void SelectGT(string IDGeneratore, bool fChecked)
    {
        UtilityLibrettiImpianti.DismettiGeneratore(int.Parse(IDGeneratore), (int)info.IDUtente, fChecked, "GT");
    }

    public void SelectGF(string IDGeneratore, bool fChecked)
    {
        UtilityLibrettiImpianti.DismettiGeneratore(int.Parse(IDGeneratore), (int)info.IDUtente, fChecked, "GF");
    }

    public void SelectSC(string IDGeneratore, bool fChecked)
    {
        UtilityLibrettiImpianti.DismettiGeneratore(int.Parse(IDGeneratore), (int)info.IDUtente, fChecked, "SC");
    }

    public void SelectCG(string IDGeneratore, bool fChecked)
    {
        UtilityLibrettiImpianti.DismettiGeneratore(int.Parse(IDGeneratore), (int)info.IDUtente, fChecked, "CG");
    }

    protected void chkSelezioneGT_CheckedChanged(object sender, EventArgs e)
    {
        SelectGT(((CheckBox)sender).Attributes["IDGeneratore"], ((CheckBox)sender).Checked);
        GetTipiGruppiGeneratori(IDLibrettoImpianto);
    }

    protected void chkSelezioneGF_CheckedChanged(object sender, EventArgs e)
    {
        SelectGF(((CheckBox)sender).Attributes["IDGeneratore"], ((CheckBox)sender).Checked);
        GetTipiGruppiGeneratori(IDLibrettoImpianto);
    }

    protected void chkSelezioneSC_CheckedChanged(object sender, EventArgs e)
    {
        SelectSC(((CheckBox)sender).Attributes["IDGeneratore"], ((CheckBox)sender).Checked);
        GetTipiGruppiGeneratori(IDLibrettoImpianto);
    }

    protected void chkSelezioneCG_CheckedChanged(object sender, EventArgs e)
    {
        SelectCG(((CheckBox)sender).Attributes["IDGeneratore"], ((CheckBox)sender).Checked);
        GetTipiGruppiGeneratori(IDLibrettoImpianto);
    }
    
    protected void gridGT_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)))
        {
            CheckBox chkSelezioneGT = (CheckBox)e.Item.Cells[5].FindControl("chkSelezioneGT");
            chkSelezioneGT.Attributes.Add("IDGeneratore", e.Item.Cells[0].Text);

            bool fDismesso = bool.Parse(e.Item.Cells[6].Text);
            chkSelezioneGT.Checked = fDismesso;

            TableRow rowDismesso = (TableRow)e.Item.Cells[4].FindControl("rowDismesso");
            rowDismesso.Visible = fDismesso;

            string iDGeneratore = e.Item.Cells[0].Text;
            string iDLibrettoImpianto = e.Item.Cells[1].Text;
            string prefisso = e.Item.Cells[2].Text;

            #region Lettera Pdf Dismissione Generatore
            QueryString qs = new QueryString();
            qs.Add("IDGeneratore", iDGeneratore);
            qs.Add("IDLibrettoImpianto", iDLibrettoImpianto);
            qs.Add("Prefisso", prefisso);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = ConfigurationManager.AppSettings["UrlPortal"] + "LetteraDismissioneGeneratoreDownload.aspx";
            url += qsEncrypted.ToString();

            HyperLink imgExportPdfLetteraDismissione = (HyperLink)e.Item.Cells[9].FindControl("imgExportPdfLetteraDismissione");
            imgExportPdfLetteraDismissione.Attributes.Add("onclick",
                "var win=dhtmlwindow.open('LetteraDismissioneGeneratoreExport_" + iDGeneratore + "', 'iframe', '" + url +
                "', 'Scarica lettera dismissione generatore', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
            imgExportPdfLetteraDismissione.Attributes.Add("style", "cursor: pointer;");
            #endregion
        }
    }

    protected void gridGF_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)))
        {
            CheckBox chkSelezioneGF = (CheckBox)e.Item.Cells[5].FindControl("chkSelezioneGF");
            chkSelezioneGF.Attributes.Add("IDGeneratore", e.Item.Cells[0].Text);

            bool fDismesso = bool.Parse(e.Item.Cells[6].Text);
            chkSelezioneGF.Checked = fDismesso;

            TableRow rowDismesso = (TableRow)e.Item.Cells[4].FindControl("rowDismesso");
            rowDismesso.Visible = fDismesso;

            string iDGeneratore = e.Item.Cells[0].Text;
            string iDLibrettoImpianto = e.Item.Cells[1].Text;
            string prefisso = e.Item.Cells[2].Text;

            #region Lettera Pdf Dismissione Generatore
            QueryString qs = new QueryString();
            qs.Add("IDGeneratore", iDGeneratore);
            qs.Add("IDLibrettoImpianto", iDLibrettoImpianto);
            qs.Add("Prefisso", prefisso);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = ConfigurationManager.AppSettings["UrlPortal"] + "LetteraDismissioneGeneratoreDownload.aspx";
            url += qsEncrypted.ToString();

            HyperLink imgExportPdfLetteraDismissione = (HyperLink)e.Item.Cells[9].FindControl("imgExportPdfLetteraDismissione");
            imgExportPdfLetteraDismissione.Attributes.Add("onclick",
                "var win=dhtmlwindow.open('LetteraDismissioneGeneratoreExport_" + iDGeneratore + "', 'iframe', '" + url +
                "', 'Scarica lettera dismissione generatore', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
            imgExportPdfLetteraDismissione.Attributes.Add("style", "cursor: pointer;");
            #endregion

        }
    }

    protected void gridSC_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)))
        {
            CheckBox chkSelezioneSC = (CheckBox)e.Item.Cells[5].FindControl("chkSelezioneSC");
            chkSelezioneSC.Attributes.Add("IDGeneratore", e.Item.Cells[0].Text);

            bool fDismesso = bool.Parse(e.Item.Cells[6].Text);
            chkSelezioneSC.Checked = fDismesso;

            TableRow rowDismesso = (TableRow)e.Item.Cells[4].FindControl("rowDismesso");
            rowDismesso.Visible = fDismesso;

            string iDGeneratore = e.Item.Cells[0].Text;
            string iDLibrettoImpianto = e.Item.Cells[1].Text;
            string prefisso = e.Item.Cells[2].Text;

            #region Lettera Pdf Dismissione Generatore
            QueryString qs = new QueryString();
            qs.Add("IDGeneratore", iDGeneratore);
            qs.Add("IDLibrettoImpianto", iDLibrettoImpianto);
            qs.Add("Prefisso", prefisso);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = ConfigurationManager.AppSettings["UrlPortal"] + "LetteraDismissioneGeneratoreDownload.aspx";
            url += qsEncrypted.ToString();

            HyperLink imgExportPdfLetteraDismissione = (HyperLink)e.Item.Cells[9].FindControl("imgExportPdfLetteraDismissione");
            imgExportPdfLetteraDismissione.Attributes.Add("onclick",
                "var win=dhtmlwindow.open('LetteraDismissioneGeneratoreExport_" + iDGeneratore + "', 'iframe', '" + url +
                "', 'Scarica lettera dismissione generatore', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
            imgExportPdfLetteraDismissione.Attributes.Add("style", "cursor: pointer;");
            #endregion
        }
    }

    protected void gridCG_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)))
        {
            CheckBox chkSelezioneCG = (CheckBox)e.Item.Cells[5].FindControl("chkSelezioneCG");
            chkSelezioneCG.Attributes.Add("IDGeneratore", e.Item.Cells[0].Text);

            bool fDismesso = bool.Parse(e.Item.Cells[6].Text);
            chkSelezioneCG.Checked = fDismesso;

            TableRow rowDismesso = (TableRow)e.Item.Cells[4].FindControl("rowDismesso");
            rowDismesso.Visible = fDismesso;

            string iDGeneratore = e.Item.Cells[0].Text;
            string iDLibrettoImpianto = e.Item.Cells[1].Text;
            string prefisso = e.Item.Cells[2].Text;

            #region Lettera Pdf Dismissione Generatore
            QueryString qs = new QueryString();
            qs.Add("IDGeneratore", iDGeneratore);
            qs.Add("IDLibrettoImpianto", iDLibrettoImpianto);
            qs.Add("Prefisso", prefisso);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = ConfigurationManager.AppSettings["UrlPortal"] + "LetteraDismissioneGeneratoreDownload.aspx";
            url += qsEncrypted.ToString();

            HyperLink imgExportPdfLetteraDismissione = (HyperLink)e.Item.Cells[9].FindControl("imgExportPdfLetteraDismissione");
            imgExportPdfLetteraDismissione.Attributes.Add("onclick",
                "var win=dhtmlwindow.open('LetteraDismissioneGeneratoreExport_" + iDGeneratore + "', 'iframe', '" + url +
                "', 'Scarica lettera dismissione generatore', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
            imgExportPdfLetteraDismissione.Attributes.Add("style", "cursor: pointer;");
            #endregion

        }
    }
    

}