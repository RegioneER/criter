using DataLayer;
using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ANAG_AnagraficaAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetData();
        }
    }

    public void GetData()
    {
        UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
        
        using (var ctx = new CriterDataModel())
        {
            var utente = ctx.V_COM_Utenti.FirstOrDefault(a => a.IDUtente == info.IDUtente);
            imgSpid.ImageUrl = UtilityApp.ToImageSpid(utente.fSpid);
            if (utente.fSpid)
            {
                lblTipoAccesso.Text = "Autenticazione tramite Sistema Spid";
            }
            else
            {
                if (utente.IDRuolo == 2)
                {
                    lblTipoAccesso.Text = "Autenticazione tramite Dispositivo di Firma Digitale";
                }
                else
                {
                    lblTipoAccesso.Text = "Autenticazione Custom";
                }
            }
            lblCodiceSoggetto.Text = utente.CodiceSoggetto;
            lblUsername.Text = utente.Username;
            lblPassword.Text = "**************************";
            if (utente.DataUltimaModificaPassword != null)
            {
                lblDataUltimaModificaPassword.Text = String.Format("{0:dd/MM/yyyy}", utente.DataUltimaModificaPassword);
            }
            if (utente.DataScadenzaPassword != null)
            {
                lblDataScadenzaPassword.Text = String.Format("{0:dd/MM/yyyy}", utente.DataScadenzaPassword);
            }
            if (utente.DataUltimoAccesso != null)
            {
                lblDataUltimoAccesso.Text = String.Format("{0:dd/MM/yyyy}", utente.DataUltimoAccesso);
            }
            lblApiKey.Text = utente.KeyApi;
        }
    }

}