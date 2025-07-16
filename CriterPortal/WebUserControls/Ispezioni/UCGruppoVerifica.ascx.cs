using EncryptionQS;
using System;
using DataLayer;
using DataUtilityCore;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class WebUserControls_Ispezioni_UCGruppoVerifica : System.Web.UI.UserControl
{
    protected string IDIspezione
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

    protected string IDStatoIspezione
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
                        if (qsdec[2] != null)
                        {
                            return (string)qsdec[2];
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

    //UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetGruppoVerifica(long.Parse(IDIspezione), long.Parse(IDIspezioneVisita));
        }
    }

    public void VisibleHiddenRimandaIspezioneInRicerca(long iDIspezione, long iDIspezioneVisita)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
            if (ispezione.IDStatoIspezione == 1) //Ricerca ispettore
            {
                btnCancellaIspezione.Visible = true;
                bool fgruppoExist = UtilityVerifiche.GetExistGruppoVerifica(iDIspezioneVisita);
                if (!fgruppoExist)
                {
                    btnCreaGruppoVerificaAssente.Visible = true;
                }
            }
            if (ispezione.IDStatoIspezione == 2) //Ispezione da Pianificare
            {
                var checkAlmenoUnaIspezionePianificata = ctx.VER_Ispezione.Where(a => (a.IDStatoIspezione == 3 //Ispezione Pianificata in attesa di conferma
                                                                                    || a.IDStatoIspezione == 4 //Ispezione Conclusa da Ispettore
                                                                                    || a.IDStatoIspezione == 5 //Ispezione Conclusa da Coordinatore con accertamento
                                                                                    || a.IDStatoIspezione == 7 //Ispezione Pianificata confermata
                                                                                    || a.IDStatoIspezione == 8 //Ispezione Annullata
                                                                                    || a.IDStatoIspezione == 9 //Ispezione Conclusa da Coordinatore senza accertamento
                                                                                 ) && a.IDIspezioneVisita == ispezione.IDIspezioneVisita).Any();
                if (!checkAlmenoUnaIspezionePianificata)
                {
                    btnRimandaInRicercaIspettoreNoPianificazione.Visible = true;
                }
            }
            else if (ispezione.IDStatoIspezione == 3) //Ispezione Pianificata in attesa di conferma
            {
                btnRimandaInRicercaIspettoreNoCorrettaPianificazione.Visible = true;
            }
            else if (ispezione.IDStatoIspezione == 6) //Ispezione Confermata da Inviare LAI
            {
                btnRimandaInRicercaIspettoreNoFirmaLAI.Visible = true;
            }
        }
    }

    public void GetGruppoVerifica(long iDIspezione, long iDIspezioneVisita)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            VisibleHiddenRimandaIspezioneInRicerca(iDIspezione, iDIspezioneVisita);


            var ispettori = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();
            var InGruppo = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == iDIspezioneVisita && c.fIspettorePrincipale == true && c.fInGruppoVerifica == true).ToList();

            bool statoIspezioneUnoODue = true;
           
            var statoIspezione = ctx.VER_Ispezione.Where(i => i.IDIspezioneVisita == iDIspezioneVisita).ToList();

            foreach (var item in statoIspezione)
            {
                if (item.IDStatoIspezione == 4 || item.IDStatoIspezione == 5 || item.IDStatoIspezione == 3)
                {
                    statoIspezioneUnoODue = false;
                }
            }
          
            if (InGruppo.Count() > 0 || !statoIspezioneUnoODue)
            {
                GridGruppoVerifica.Columns[8].Visible = false;
            }
            else
            {
                GridGruppoVerifica.Columns[8].Visible = true;
            }
            

            GridGruppoVerifica.DataSource = ispettori;
            GridGruppoVerifica.DataBind();
        }
    }

    protected void imgfInGruppoVerifica_Init(object sender, EventArgs e)
    {
        GridViewDataItemTemplateContainer container = ((ASPxImage)sender).NamingContainer as GridViewDataItemTemplateContainer;
        int currentIndex = container.VisibleIndex;
        bool isfAttivo = Convert.ToBoolean(container.Grid.GetRowValues(currentIndex, "fInGruppoVerifica"));

        if (isfAttivo)
        {
            ((ASPxImage)sender).ImageUrl = "~/images/si.png";
            
        }
        else
        {
            ((ASPxImage)sender).ImageUrl = "~/images/no.png";
        }
    }

    protected void GridGruppoVerifica_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                long iDIspettore = long.Parse(e.GetValue("IDIspettore").ToString());
                long iDIspezioneGruppoVerifica = long.Parse(e.GetValue("IDIspezioneGruppoVerifica").ToString());
                //long iDIspezione = long.Parse(IDIspezione);

                var GetDatiIspettorePianificazione = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspettore == iDIspettore && c.IDIspezioneGruppoVerifica == iDIspezioneGruppoVerifica).FirstOrDefault();
                //var GetDatiIspezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();

                #region COLUMN 3 - DATI ISPETTORE

                var GetDatiIspettore = ctx.V_COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDIspettore).FirstOrDefault();

                Label lblTitoloIspettore = GridGruppoVerifica.FindRowCellTemplateControl(e.VisibleIndex, null, "lblTitoloIspettore") as Label;
                lblTitoloIspettore.Text = GetDatiIspettore.Soggetto;

                Label lblIndirizzoIspettore = GridGruppoVerifica.FindRowCellTemplateControl(e.VisibleIndex, null, "lblIndirizzoIspettore") as Label;
                lblIndirizzoIspettore.Text = GetDatiIspettore.IndirizzoSoggetto;

                Label lblTelefonoIspettore = GridGruppoVerifica.FindRowCellTemplateControl(e.VisibleIndex, null, "lblTelefonoIspettore") as Label;
                lblTelefonoIspettore.Text = "Telefono:" + "&nbsp;" + GetDatiIspettore.Telefono;

                Label lblCellulareIspettore = GridGruppoVerifica.FindRowCellTemplateControl(e.VisibleIndex, null, "lblCellulareIspettore") as Label;
                lblCellulareIspettore.Text = "Cellulare:" + "&nbsp;" + GetDatiIspettore.Cellulare;

                Label lblEmailIspettore = GridGruppoVerifica.FindRowCellTemplateControl(e.VisibleIndex, null, "lblEmailIspettore") as Label;
                lblEmailIspettore.Text = "Email:" + "&nbsp;" + GetDatiIspettore.Email;

                #endregion

                #region COLUMN 6 - DATI PIANIFICAZIONE

                int iDStatoPianificazioneIspettore = int.Parse(e.GetValue("IDStatoPianificazioneIspettore").ToString());

                var GetDatiPianificazione = ctx.SYS_StatoPianificazioneIspettore.Where(c => c.IDStatoPianificazioneIspettore == iDStatoPianificazioneIspettore && c.fAttivo == true).FirstOrDefault();

                string TitoloPianificazione = GetDatiPianificazione.StatoPianificazioneIspettore;

                switch (iDStatoPianificazioneIspettore)
                {
                    case 1: // Pianificazione in sospeso
                        TitoloPianificazione += "";
                        break;
                    case 2: // Pianificazione in attesa di conferma da parte dell'ispettore
                        TitoloPianificazione += "&nbsp;" + "inviata il <b>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", GetDatiIspettorePianificazione.DataInvioPianificazione) + "</b>";
                        break;
                    case 3: // Pianificazione accettata 
                        TitoloPianificazione += "&nbsp;" + "il <b>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", GetDatiIspettorePianificazione.DataAccettazione) + "</b>"; // CONTROLLARE
                        break;
                    case 4: // Pianificazione rifiutata
                        TitoloPianificazione += "&nbsp;" + "il <b>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", GetDatiIspettorePianificazione.DataRifiuto) + "</b>";
                        break;
                    case 5: // Pianificazione automaticamente annullata dal sistema trascorse 24 ore
                        TitoloPianificazione += "";
                        break;
                    case 6: // Pianificazione annullata da Coordinatore
                        TitoloPianificazione += "";
                        break;
                    case 7: // Pianificazione assegnata da Coordinatore
                        TitoloPianificazione += "";
                        break;
                }

                Label lblStatoPianificazioneIspettore = GridGruppoVerifica.FindRowCellTemplateControl(e.VisibleIndex, null, "lblStatoPianificazioneIspettore") as Label;
                lblStatoPianificazioneIspettore.Text = TitoloPianificazione;

                #endregion

            }
        }
    }

    protected void imgSetIspettore_Command(object sender, CommandEventArgs e)
    {
        if(e.CommandName == "Set")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });       
            long iDIspezioneGruppoVerifica = long.Parse(commandArgs[0].ToString());
            int iDIspettore = int.Parse(commandArgs[1].ToString());            

            UtilityVerifiche.IspezioneAssegnaManualmenteAdIspettore(iDIspezioneGruppoVerifica, long.Parse(IDIspezioneVisita), iDIspettore);
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            QueryString qs = new QueryString();
            qs.Add("IDIspezione", IDIspezione);
            qs.Add("IDIspezioneVisita", IDIspezioneVisita);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Ispezioni.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    protected void btnRimandaInRicercaIspettoreNoFirmaLAI_Click(object sender, EventArgs e)
    {
        UtilityVerifiche.IspezioneRimandaInRicercaIspettoreNoFirmaLAI(long.Parse(IDIspezioneVisita));
        
        QueryString qs = new QueryString();
        qs.Add("IDIspezione", IDIspezione);
        qs.Add("IDIspezioneVisita", IDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_Ispezioni.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void btnRimandaInRicercaIspettoreNoPianificazione_Click(object sender, EventArgs e)
    {
        UtilityVerifiche.IspezioneRimandaInRicercaIspettoreNoPianificazione(long.Parse(IDIspezioneVisita));

        QueryString qs = new QueryString();
        qs.Add("IDIspezione", IDIspezione);
        qs.Add("IDIspezioneVisita", IDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_Ispezioni.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }
        
    public void GetRicreaGruppoDiVerifica(long iDIspezione)
    {
        //using (CriterDataModel ctx = new CriterDataModel())
        //{
        //    var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
        //    if (ispezione.IDStatoIspezione == 2) //Ispezione da Pianificare
        //    {
        //        btnRimandaInRicercaIspettoreNoPianificazione.Visible = true;
        //    }
        //    else if (ispezione.IDStatoIspezione == 6) //Ispezione Confermata da Inviare LAI
        //    {
        //        btnRimandaInRicercaIspettoreNoFirmaLAI.Visible = true;
        //    }
        //}
    }

    protected void btnRimandaInRicercaIspettoreNoCorrettaPianificazione_Click(object sender, EventArgs e)
    {
        UtilityVerifiche.IspezioneRimandaInRicercaIspettoreNoCorrettaPianificazione(long.Parse(IDIspezioneVisita));

        QueryString qs = new QueryString();
        qs.Add("IDIspezione", IDIspezione);
        qs.Add("IDIspezioneVisita", IDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_Ispezioni.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void btnCreaGruppoVerificaAssente_Click(object sender, EventArgs e)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);
            long iDIspezione = long.Parse(IDIspezione);
            UtilityVerifiche.RicercaIspettoriPerVerifica(ctx, iDIspezioneVisita);
            GetGruppoVerifica(iDIspezione, iDIspezioneVisita);
        }
    }

    protected void btnCancellaIspezione_Click(object sender, EventArgs e)
    {
        UtilityVerifiche.DeleteIspezione(long.Parse(IDIspezione));
        QueryString qs = new QueryString();
        qs.Add("IDIspezioneVisita", IDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_IspezioniSearch.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }
}