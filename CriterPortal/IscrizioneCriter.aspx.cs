using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IscrizioneCriter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool fSpidAbilitato = bool.Parse(ConfigurationManager.AppSettings["SpidAbilitato"]);
            if (fSpidAbilitato)
            {
                lbIscrizioneImpresaSpid.Visible = true;
            }
            else
            {
                lbIscrizioneImpresaSpid.Visible = false;
            }
        }
    }
    
    protected void lbIscrizioneImpresa_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDTipoSoggetto", "2");
        qs.Add("fSpid", "False");
        qs.Add("codicefiscale", "");
        qs.Add("key", "");
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Iscrizione.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void lbIscrizioneImpresaSpid_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["UrlRequestSpidAzienda"]);
    }

    protected void lbIscrizioneTerzoResponsabile_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDTipoSoggetto", "3");
        qs.Add("fSpid", "False");
        qs.Add("codicefiscale", "");
        qs.Add("key", "");
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Iscrizione.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void lbIscrizioneIspettore_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDTipoSoggetto", "7");
        qs.Add("fSpid", "False");
        qs.Add("codicefiscale", "");
        qs.Add("key", "");
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Iscrizione.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void lbIscrizioneDistributore_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDTipoSoggetto", "5");
        qs.Add("fSpid", "False");
        qs.Add("codicefiscale", "");
        qs.Add("key", "");
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Iscrizione.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void lbIscrizioneEnteLocale_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDTipoSoggetto", "9");
        qs.Add("fSpid", "False");
        qs.Add("codicefiscale", "");
        qs.Add("key", "");
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Iscrizione.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void lbAccesso_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("", "");
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Login.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }
    
    protected void lbAccessoImpresa_Click(object sender, EventArgs e)
    {
        //QueryString qs = new QueryString();
        //qs.Add("type", "azn");
        //QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Login.aspx?type=azn";
        //url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void lbAccessoCittadino_Click(object sender, EventArgs e)
    {
        //Response.Redirect(ConfigurationManager.AppSettings["UrlRequestSpidCittadino"]);
        //QueryString qs = new QueryString();
        //qs.Add("type", "ctz");
        //QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/Login.aspx?type=ctz";
        //url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

}