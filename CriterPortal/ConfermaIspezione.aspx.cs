using System;
using DataLayer;
using DataUtilityCore;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using EncryptionQS;

public partial class ConfermaIspezione : System.Web.UI.Page
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
            
            if (IDIspezioneVisita != null && IDIspettore != null)
            {
                if (UtilityVerifiche.IspettoreAccessoPaginaConfermaIspezione(iDIspezioneVisita, iDIspettore))
                {
                    TextOfButton();
                    GetDataVerifica(long.Parse(IDIspezioneVisita.ToString()), int.Parse(IDIspettore.ToString()));
                    rowInfoAccessoNegato.Visible = false;
                    rowInfoErroreRichiestaPagina.Visible = false;
                    rowDettaglioVerifica0.Visible = true;
                    rowDettaglioVerifica1.Visible = true;
                    rowDettaglioVerifica2.Visible = true;
                    rowDettaglioVerifica3.Visible = true;
                    rowDettaglioVerifica4.Visible = true;
                }
                else
                {
                    rowInfoAccessoNegato.Visible = true;
                    rowInfoErroreRichiestaPagina.Visible = false;
                    rowDettaglioVerifica0.Visible = false;
                    rowDettaglioVerifica1.Visible = false;
                    rowDettaglioVerifica2.Visible = false;
                    rowDettaglioVerifica3.Visible = false;
                    rowDettaglioVerifica4.Visible = false;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
                }
            }
            else
            {
                rowInfoAccessoNegato.Visible = false;
                rowInfoErroreRichiestaPagina.Visible = true;
                rowDettaglioVerifica0.Visible = false;
                rowDettaglioVerifica1.Visible = false;
                rowDettaglioVerifica2.Visible = false;
                rowDettaglioVerifica3.Visible = false;
                rowDettaglioVerifica4.Visible = false;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
            }
        }
    }

    public void GetDataVerifica(long iDIspezioneVisita, int iDIspettore)
    {
        using (var ctx = new CriterDataModel())
        {
            var ispettore = ctx.V_COM_AnagraficaSoggetti.AsQueryable().Where(c => c.IDSoggetto == iDIspettore).FirstOrDefault();
            lblIspettore.Text = "Pregiatissimo Ispettore " + ispettore.Nome + "&nbsp;" + ispettore.Cognome;
            lblIspettore1.Text = "Io sottoscritto " + ispettore.Nome + "&nbsp;" + ispettore.Cognome + "&nbsp;confermo:";

            lblIDVisitaIspettiva.Text = iDIspezioneVisita.ToString();
        }
        
        var ispezioni = UtilityVerifiche.GetIspezioni(iDIspezioneVisita);
        DataGrid.DataSource = ispezioni;
        DataGrid.DataBind();
    }

    public void TextOfButton()
    {
        if (rblSceltaAccettazione.SelectedItem.Value == "S")
        {
            btnConfermaVerifica.Text = "CONFERMI DI ACCETTARE LA VISITA ISPETTIVA";
            //btnConfermaVerifica.OnClientClick = "javascript:return(confirm('Confermi di accettare di effettuare l\'ispezione?'));";
            btnConfermaVerifica.OnClientClick = "javascript:return confermaAccettazione();";
        }
        else if (rblSceltaAccettazione.SelectedItem.Value == "N")
        {
            btnConfermaVerifica.Text = "CONFERMI DI NON ACCETTARE LA VISITA ISPETTIVA";
            //btnConfermaVerifica.OnClientClick = "javascript:return(confirm('Confermi di non accettare di effettuare l\'ispezione?'));";
            btnConfermaVerifica.OnClientClick = "javascript:return confermaRifiuto();";
        }
        else if (rblSceltaAccettazione.SelectedItem.Value == "NCI")
        {
            btnConfermaVerifica.Text = "CONFERMI DI NON ACCETTARE LA VISITA ISPETTIVA PER CONFLITTO DI INTERESSE";
            //btnConfermaVerifica.OnClientClick = "javascript:return(confirm('Confermi di non accettare di effettuare l\'ispezione?'));";
            btnConfermaVerifica.OnClientClick = "javascript:return confermaRifiutoConflittoInteressi();";
        }
    }

    public void TipoAccettazioneVerifica_OnSelectedIndexChanged(Object sender, EventArgs e)
    {
        TextOfButton();
    }

    public void SendConfermaVerifica_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var iDIspezioneVisita = long.Parse(IDIspezioneVisita);
                var iDIspettore = int.Parse(IDIspettore);

                var DatiGruppo = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == iDIspezioneVisita && c.IDIspettore == iDIspettore).FirstOrDefault();

                if (rblSceltaAccettazione.SelectedItem.Value == "S")
                {
                    rowInfoAccessoNegato.Visible = false;
                    rowInfoErroreRichiestaPagina.Visible = false;
                    rowDettaglioVerifica0.Visible = false;
                    rowDettaglioVerifica1.Visible = false;
                    rowDettaglioVerifica2.Visible = false;
                    rowDettaglioVerifica3.Visible = false;
                    rowDettaglioVerifica4.Visible = false;
                    rowInfoVisitaOk.Visible = true;
                    rowInfoVisitaRifiutata.Visible = false;

                    UtilityVerifiche.CambiaStatoPianificazioneIspezioneIspettore(DatiGruppo.IDIspezioneGruppoVerifica, 3, ctx, iDIspezioneVisita);
                }
                else if (rblSceltaAccettazione.SelectedItem.Value == "N")
                {
                    rowInfoAccessoNegato.Visible = false;
                    rowInfoErroreRichiestaPagina.Visible = false;
                    rowDettaglioVerifica0.Visible = false;
                    rowDettaglioVerifica1.Visible = false;
                    rowDettaglioVerifica2.Visible = false;
                    rowDettaglioVerifica3.Visible = false;
                    rowDettaglioVerifica4.Visible = false;
                    rowInfoVisitaOk.Visible = false;
                    rowInfoVisitaRifiutata.Visible = true;

                    UtilityVerifiche.CambiaStatoPianificazioneIspezioneIspettore(DatiGruppo.IDIspezioneGruppoVerifica, 4, ctx, iDIspezioneVisita);
                    UtilityVerifiche.ChiediDisponibilitaProssimoIspettore(iDIspezioneVisita);
                }
                else if (rblSceltaAccettazione.SelectedItem.Value == "NCI")
                {
                    rowInfoAccessoNegato.Visible = false;
                    rowInfoErroreRichiestaPagina.Visible = false;
                    rowDettaglioVerifica0.Visible = false;
                    rowDettaglioVerifica1.Visible = false;
                    rowDettaglioVerifica2.Visible = false;
                    rowDettaglioVerifica3.Visible = false;
                    rowDettaglioVerifica4.Visible = false;
                    rowInfoVisitaOk.Visible = false;
                    rowInfoVisitaRifiutata.Visible = true;

                    UtilityVerifiche.CambiaStatoPianificazioneIspezioneIspettore(DatiGruppo.IDIspezioneGruppoVerifica, 11, ctx, iDIspezioneVisita);
                    UtilityVerifiche.ChiediDisponibilitaProssimoIspettore(iDIspezioneVisita);
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},2000);", true);
        }
    }

}