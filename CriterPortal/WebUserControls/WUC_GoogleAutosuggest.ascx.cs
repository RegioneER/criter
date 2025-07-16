using DataLayer;
using DataUtilityCore.Google;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControls_WUC_GoogleAutosuggest : System.Web.UI.UserControl
{
    public string IDLibrettoImpianto
    {
        get { return lblIDLibrettoImpianto.Text; }
        set
        {
            lblIDLibrettoImpianto.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(IDLibrettoImpianto))
            {
                GetLogicNormalizzeCivico(int.Parse(IDLibrettoImpianto));
                GetLogicNormalizzeAddress(int.Parse(IDLibrettoImpianto));
            }
        }
    }

    public static object[] GetZipCodeGoogleFromAddress(string address, string city)
    {
        object[] outVal = new object[2];
        outVal[0] = null; //Zip Code
        outVal[1] = null; //Address

        if (!string.IsNullOrEmpty(address))
        {
            WebClient wc = new WebClient();
            wc.Proxy = WebRequest.DefaultWebProxy;
            wc.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                byte[] data = wc.DownloadData(String.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", address, ConfigurationManager.AppSettings["GoogleApiKey"]));
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string jsonData = UTF8Encoding.UTF8.GetString(data);

                GoogleAddress googleComponent = JsonConvert.DeserializeObject<GoogleAddress>(jsonData);
                if (googleComponent.status == "OK")
                {
                    if (googleComponent.results.Count == 1) //Google mi da un risultato univoco allora sono sicuro che il cap sia quello giusto
                    {
                        outVal[0] = (from x in googleComponent.results[0].address_components.AsQueryable()
                                   where x.types.Contains("postal_code")
                                   select x.long_name).FirstOrDefault();

                        outVal[1] = (from x in googleComponent.results[0].address_components.AsQueryable()
                                     where x.types.Contains("route")
                                     select x.long_name).FirstOrDefault();
                    }
                    else if (googleComponent.results.Count > 1)
                    {
                        for (int i = 0; i < googleComponent.results.Count; i++)
                        {
                            string cittaGoogle = (from x in googleComponent.results[i].address_components.AsQueryable()
                                       where x.types.Contains("administrative_area_level_3")
                                       select x.long_name).FirstOrDefault();

                            if (cittaGoogle != null)
                            {
                                if (cittaGoogle.ToLower().Contains(city.ToLower()))
                                {
                                    outVal[0] = (from x in googleComponent.results[i].address_components.AsQueryable()
                                                 where x.types.Contains("postal_code")
                                                 select x.long_name).FirstOrDefault();

                                    outVal[1] = (from x in googleComponent.results[i].address_components.AsQueryable()
                                                 where x.types.Contains("route")
                                                 select x.long_name).FirstOrDefault();

                                    break;
                                }
                            }
                            
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        // handle the 404 here
                    }
                }
                else if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    //handle name resolution failure
                }
            }
        }

        return outVal;
    }

    //protected void GetLogicNormalizze(int iDLibrettoImpianto)
    //{
    //    using (var ctx = new CriterDataModel())
    //    {
    //        var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
    //        if (libretto != null)
    //        {
                
    //        }
    //    }
    //}


    protected void GetLogicNormalizzeAddress(int iDLibrettoImpianto)
    {
        using (var ctx = new CriterDataModel())
        {
            var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
            if (libretto != null)
            {
                string address = string.Empty;

                if (!string.IsNullOrEmpty(libretto.CapResponsabile) && !string.IsNullOrEmpty(libretto.IndirizzoNormalizzatoResponsabile))
                {
                    lblFeedBackAddress.ForeColor = System.Drawing.Color.Green;
                    lblFeedBackAddress.Text = "1 - Indirizzo correttamente normalizzato per la corrispondenza postale";

                    address = string.Format("{0} {1}, {2} {3} ({4}), {5}",
                                        libretto.IndirizzoNormalizzatoResponsabile,
                                        libretto.CivicoNormalizzatoResponsabile,
                                        libretto.CapResponsabile,
                                        libretto.SYS_CodiciCatastali1.Comune,
                                        libretto.SYS_CodiciCatastali1.SYS_Province.SiglaProvincia,
                                        "Italia");

                    lblAddress.Text = address;
                    GetLogicNormalizzeCap(true);
                }
                else
                {
                    if (libretto.SYS_CodiciCatastali1 != null) //Fatto questo controllo perchè ci sono libretti antichi che non avevano il comune del responsabile
                    {
                        address = string.Format("{0} {1}, {2} ({3}), {4}",
                                            libretto.IndirizzoResponsabile,
                                            libretto.CivicoResponsabile,
                                            libretto.SYS_CodiciCatastali1.Comune,
                                            libretto.SYS_CodiciCatastali1.SYS_Province.SiglaProvincia,
                                            "Italia");

                        //Step 1: Verifico se effettivamente l'indirizzo è scritto correttamente
                        object[] getVal = new object[2];
                        getVal = GetZipCodeGoogleFromAddress(address, libretto.SYS_CodiciCatastali1.Comune);

                        if (getVal[0] != null && getVal[1] != null)
                        {
                            libretto.IndirizzoNormalizzatoResponsabile = getVal[1].ToString();
                            libretto.CapResponsabile = getVal[0].ToString();
                            ctx.SaveChanges();

                            address = string.Format("{0} {1}, {2} {3} ({4}), {5}",
                                            libretto.IndirizzoNormalizzatoResponsabile,
                                            libretto.CivicoResponsabile,
                                            libretto.CapResponsabile,
                                            libretto.SYS_CodiciCatastali1.Comune,
                                            libretto.SYS_CodiciCatastali1.SYS_Province.SiglaProvincia,
                                            "Italia");

                            lblAddress.Text = address;

                            lblFeedBackAddress.ForeColor = System.Drawing.Color.Green;
                            lblFeedBackAddress.Text = "1 - Indirizzo correttamente normalizzato per la corrispondenza postale";

                            GetLogicNormalizzeCap(true);
                        }
                        else
                        {
                            address = string.Format("{0} {1}, {2} ({3}), {4}",
                                            libretto.IndirizzoResponsabile,
                                            libretto.CivicoResponsabile,
                                            libretto.SYS_CodiciCatastali1.Comune,
                                            libretto.SYS_CodiciCatastali1.SYS_Province.SiglaProvincia,
                                            "Italia");

                            lblAddress.Text = address;

                            btnNormalizzaIndirizzo.Visible = true;
                            lblFeedBackAddress.ForeColor = System.Drawing.Color.Red;
                            lblFeedBackAddress.Text = "Attenzione: indirizzo non normalizzato per la corrispondenza postale. Procedi a correggerlo manualmente cliccando sul bottone 'NORMALIZZA INDIRIZZO POSTALE'";

                            GetLogicNormalizzeCap(false);
                        }
                    }
                    else
                    {
                        //Non riesco in nessun modo a validare l'indirizzo perchè nel libretto il responsabile non ha il comune
                        address = string.Format("{0} {1}, {2} ({3}), {4}",
                                            libretto.IndirizzoResponsabile,
                                            libretto.CivicoResponsabile,
                                            string.Empty,
                                            string.Empty,
                                            "Italia");

                        lblAddress.Text = address;

                        btnNormalizzaIndirizzo.Visible = false;
                        btnNormalizzaCap.Visible = false;
                        lblFeedBackAddress.ForeColor = System.Drawing.Color.Red;
                        lblFeedBackAddress.Text = "Attenzione: indirizzo non normalizzabile. E' assente sul libretto il comune del responsabile!";
                    }
                }
            }
        }
    }

    public void GetLogicNormalizzeCivico(int iDLibrettoImpianto)
    {
        using (var ctx = new CriterDataModel())
        {
            var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
            if (libretto != null)
            {
                //Copio il civico del libretto per normalizzarlo se necessario 
                if (string.IsNullOrEmpty(libretto.CivicoNormalizzatoResponsabile))
                {
                    libretto.CivicoNormalizzatoResponsabile = libretto.CivicoResponsabile;
                    ctx.SaveChanges();
                }

                if (libretto.CivicoNormalizzatoResponsabile.Length <=5)
                {
                    lblFeedBackCivico.ForeColor = System.Drawing.Color.Green;
                    lblFeedBackCivico.Text = "2 - Civico '"+ libretto.CivicoNormalizzatoResponsabile + "' correttamente normalizzato per la corrispondenza postale";

                    rowCivico.Visible = false;
                }
                else
                {
                    btnNormalizzaCivico.Visible = true;
                    lblFeedBackCivico.ForeColor = System.Drawing.Color.Red;
                    lblFeedBackCivico.Text = "Attenzione: il civico indirizzo "+ libretto.CivicoNormalizzatoResponsabile +" non è conforme ai 5 caratteri ammissibili per la corrispondenza postale. Procedi a correggerlo manualmente cliccando sul bottone 'NORMALIZZA CIVICO POSTALE'";

                    rowCivico.Visible = true;
                }
            }
        }
    }

    public void GetLogicNormalizzeCap(bool fNormalizeCap)
    {
        rowCap.Visible = fNormalizeCap;
    }



    protected void btnNormalizzaIndirizzo_Click(object sender, EventArgs e)
    {
        VisibleHiddenNormalizzaAddress(true);
    }

    protected void btnNormalizzaCivico_Click(object sender, EventArgs e)
    {
        VisibleHiddenNormalizzaCivico(true);
    }

    protected void VisibleHiddenNormalizzaAddress(bool fVisible)
    {
        if (fVisible)
        {
            tb.Text = string.Empty;
            lblNormalizeAddress.Text = string.Empty;
            lblPostalCode.Text = string.Empty;
            lblLocality.Text = string.Empty;
        }

        //btnNormalizzaIndirizzo.Visible = fVisible;
        //rowAddress.Visible = !fVisible;

        rowNormalizzeAddress.Visible = fVisible;
    }

    protected void VisibleHiddenNormalizzaCivico(bool fVisible)
    {
        rowCivico.Visible = !fVisible;
        rowNormalizzeCivico.Visible = fVisible;
    }

    protected void VisibleHiddenNormalizzaCap(bool fVisible)
    {
        rowCap.Visible = !fVisible;
        rowNormalizzeCap.Visible = fVisible;
    }

    protected void btnSaveAddress_Click(object sender, EventArgs e)
    {
        using (var ctx = new CriterDataModel())
        {
            int iDLibrettoImpianto = int.Parse(lblIDLibrettoImpianto.Text);
            var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
            if (libretto != null)
            {
                libretto.IndirizzoNormalizzatoResponsabile = lblNormalizeAddress.Text != string.Empty ? lblNormalizeAddress.Text : null;
                libretto.CapResponsabile = lblPostalCode.Text != string.Empty ? lblPostalCode.Text : null;
                ctx.SaveChanges();
            }
        }

        VisibleHiddenNormalizzaAddress(false);
        GetLogicNormalizzeAddress(int.Parse(lblIDLibrettoImpianto.Text));       
    }

    protected void btnAnnullaAddress_Click(object sender, EventArgs e)
    {
        VisibleHiddenNormalizzaAddress(false);
        GetLogicNormalizzeAddress(int.Parse(IDLibrettoImpianto));
    }

    protected void btnSaveCivico_Click(object sender, EventArgs e)
    {
        using (var ctx = new CriterDataModel())
        {
            int iDLibrettoImpianto = int.Parse(lblIDLibrettoImpianto.Text);
            var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
            if (libretto != null)
            {
                libretto.CivicoNormalizzatoResponsabile = txtCivico.Text;
                ctx.SaveChanges();
            }
        }

        VisibleHiddenNormalizzaCivico(false);
        GetLogicNormalizzeCivico(int.Parse(lblIDLibrettoImpianto.Text));
    }

    protected void btnAnnullaCivico_Click(object sender, EventArgs e)
    {
        VisibleHiddenNormalizzaCivico(false);
        GetLogicNormalizzeCivico(int.Parse(IDLibrettoImpianto));
    }


    protected void btnNormalizzaCap_Click(object sender, EventArgs e)
    {
        VisibleHiddenNormalizzaCap(true);
    }



    protected void btnSaveCap_Click(object sender, EventArgs e)
    {
        using (var ctx = new CriterDataModel())
        {
            int iDLibrettoImpianto = int.Parse(lblIDLibrettoImpianto.Text);
            var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
            if (libretto != null)
            {
                libretto.CapResponsabile = txtCap.Text;
                ctx.SaveChanges();
            }
        }

        VisibleHiddenNormalizzaCap(false);
        GetLogicNormalizzeAddress(int.Parse(lblIDLibrettoImpianto.Text));
    }

    protected void btnAnnullaCap_Click(object sender, EventArgs e)
    {
        VisibleHiddenNormalizzaCap(false);
        GetLogicNormalizzeAddress(int.Parse(IDLibrettoImpianto));
    }

    
}