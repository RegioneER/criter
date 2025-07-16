using Criter.Rapporti;
using Criter.Libretto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI;
using DataUtilityCore.Enum;
using CriterAPI;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

public partial class CriterApi_CriterApi : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetUrl();
            ValorizeProduzioneTest();
            ASPxRoundPanel1.Collapsed = true;
            ASPxRoundPanel2.Collapsed = true;
            ASPxRoundPanel3.Collapsed = true;
        }
    }

    public void GetUrl()
    {
        lnkApiDocumentation.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation";

        lblUrlCodiciTargatura.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/GetCodiciTargaturaByCodiceSoggetto";
        lblUrlLibretto.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/GetLibrettoImpiantoByCodice";
        lblUrlGT.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/GetRapportoTecnico_GT_ByID";
        lblUrlGF.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/GetRapportoTecnico_GF_ByID";
        lblUrlSC.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/GetRapportoTecnico_SC_ByID";
        lblUrlCG.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/GetRapportoTecnico_CG_ByID";

        lblUrlValidateLibretto.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/ValidationLibrettoImpianto";
        lblUrlValidateRapportoGT.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/ValidationRapportoControlloTecnico_GT";
        lblUrlValidateRapportoGF.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/ValidationRapportoControlloTecnico_GF";
        lblUrlValidateRapportoSC.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/ValidationRapportoControlloTecnico_SC";
        lblUrlValidateRapportoCG.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/ValidationRapportoControlloTecnico_CG";

        lnkLibretto.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/N_Criter_Libretto.htm";
        lnkGT.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_GT.htm";
        lnkGF.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_GF.htm";
        lnkSC.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_SC.htm";
        lnkCG.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_CG.htm";
        

        lblUrlUploadLibretto.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/LoadLibrettoImpianto";
        lblUrlUploadRapportoGT.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/LoadRapportoControlloTecnico_GT";
        lblUrlUploadRapportoGF.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/LoadRapportoControlloTecnico_GF";
        lblUrlUploadRapportoSC.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/LoadRapportoControlloTecnico_SC";
        lblUrlUploadRapportoCG.Text = ConfigurationManager.AppSettings["UrlPortal"] + " api/CriterService/LoadRapportoControlloTecnico_CG";
        
        lnkPomJ_Lib.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/N_Criter_Libretto.htm";
        lnkPomJ_RappGT.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_GT.htm";
        lnkPomJ_RappGF.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_GF.htm";
        lnkPomJ_RappSC.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_SC.htm";
        lnkPomJ_RappCG.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/html/T_Criter_Rapporti_POMJ_RapportoControlloTecnico_CG.htm";

        lnkGetCodiciTargatura.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetCodiciTargatura.png";
        lnkGetLibretto.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetLib.png";
        lnkGetGT.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetGT.png";
        lnkGetGF.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetGF.png";
        lnkGetSC.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetSC.png";
        lnkGetCG.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetCG.png";

        lnkEsempioLibretto.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiLib.png";
        lnkEsempioRappGT.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetGT.png";
        lnkEsempioRappGF.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetGF.png";
        lnkEsempioRappSC.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetSC.png";
        lnkEsempioRappCG.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + "ApiDocumentation/images/ApiGetCG.png";
    }

    public void ValorizeProduzioneTest()
    {
        if (ConfigurationManager.AppSettings["UrlPortal"] != "https://criter.regione.emilia-romagna.it/")
        {
            txtCodiciTargaturaCodiceSoggetto.Text = "000001-0001";
            txtCodiciTargaturaCodiceSoggetto.Enabled = false;

            txtGetCodiceSoggettoLibretto.Text = "000001-0001";
            txtGetCodiceSoggettoLibretto.Enabled = false;

            txtGetCodiceSoggettoGT.Text = "000001-0001";
            txtGetCodiceSoggettoGT.Enabled = false;

            txtGetCodiceSoggettoGF.Text = "000001-0001";
            txtGetCodiceSoggettoGF.Enabled = false;

            txtGetCodiceSoggettoSC.Text = "000001-0001";
            txtGetCodiceSoggettoSC.Enabled = false;

            txtGetCodiceSoggettoCG.Text = "000001-0001";
            txtGetCodiceSoggettoCG.Enabled = false;

            txtUploadCodiceSoggettoLibretto.Text = "000001-0001";
            txtUploadCodiceSoggettoLibretto.Enabled = false;

            txtUploadCodiceSoggettoRapportoGT.Text = "000001-0001";
            txtUploadCodiceSoggettoRapportoGT.Enabled = false;

            txtUploadCodiceSoggettoRapportoGF.Text = "000001-0001";
            txtUploadCodiceSoggettoRapportoGF.Enabled = false;

            txtUploadCodiceSoggettoRapportoSC.Text = "000001-0001";
            txtUploadCodiceSoggettoRapportoSC.Enabled = false;

            txtUploadCodiceSoggettoRapportoCG.Text = "000001-0001";
            txtUploadCodiceSoggettoRapportoCG.Enabled = false;

            txtCodiciTargaturaApiKeySoggetto.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtCodiciTargaturaApiKeySoggetto.Enabled = false;

            txtGetApiKeySoggettoLibretto.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtGetApiKeySoggettoLibretto.Enabled = false;

            txtGetApiKeySoggettoGT.Text = "27e88fa97f7e397d97860c9008245f4f92cbe3a4037d8d5cab85dc4e76094fb1";
            txtGetApiKeySoggettoGT.Enabled = false;

            txtGetApiKeySoggettoGF.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtGetApiKeySoggettoGF.Enabled = false;

            txtGetApiKeySoggettoSC.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtGetApiKeySoggettoSC.Enabled = false;

            txtGetApiKeySoggettoCG.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtGetApiKeySoggettoCG.Enabled = false;

            txtUploadApiKeySoggettoLibretto.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtUploadApiKeySoggettoLibretto.Enabled = false;

            txtUploadApiKeySoggettoRapportoGT.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtUploadApiKeySoggettoRapportoGT.Enabled = false;

            txtUploadApiKeySoggettoRapportoGF.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtUploadApiKeySoggettoRapportoGF.Enabled = false;

            txtUploadApiKeySoggettoRapportoSC.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtUploadApiKeySoggettoRapportoSC.Enabled = false;

            txtUploadApiKeySoggettoRapportoCG.Text = "5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc";
            txtUploadApiKeySoggettoRapportoCG.Enabled = false;
        }
    }

    public static async Task<object> GetJson(string apiUrl, APICriterIDParameter param, int? iDTipoRapporto)
    {
        object json = null;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
        var stringContent = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
        
        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                json = data.Result;
            }
        }

        return json;
    }

    #region Validation Method
    public static async Task<string> GetValidationLibrettoResult(string apiUrl, POMJ_LibrettoImpianto lib)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(lib), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> GetValidationRapportoGTResult(string apiUrl, POMJ_RapportoControlloTecnico_GT rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> GetValidationRapportoGFResult(string apiUrl, POMJ_RapportoControlloTecnico_GF rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> GetValidationRapportoCGResult(string apiUrl, POMJ_RapportoControlloTecnico_CG rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> GetValidationRapportoSCResult(string apiUrl, POMJ_RapportoControlloTecnico_SC rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    #endregion
        
    #region Upload Method
    public static async Task<string> LoadLibrettoResult(string apiUrl, POMJ_LibrettoImpianto lib)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(lib), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> LoadRapportoGTResult(string apiUrl, POMJ_RapportoControlloTecnico_GT rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> LoadRapportoGFResult(string apiUrl, POMJ_RapportoControlloTecnico_GF rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> LoadRapportoCGResult(string apiUrl, POMJ_RapportoControlloTecnico_CG rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    public static async Task<string> LoadRapportoSCResult(string apiUrl, POMJ_RapportoControlloTecnico_SC rap)
    {
        string result = string.Empty;

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlPortal"]);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var stringContent = new StringContent(JsonConvert.SerializeObject(rap), Encoding.UTF8, "application/json");

        using (client)
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
            if (response.IsSuccessStatusCode) //Qui entri se api online e richiesta formattata bene
            {
                var data = response.Content.ReadAsStringAsync();
                result = data.Result;
            }
            else
            {
                result = "Errore Server Criter:" + response.StatusCode + " " + response.ReasonPhrase;
            }
        }

        return result;
    }

    #endregion

    #region Get Json
    protected async void Btn_GetCodiciTargaturaImpianto_Click(object sender, EventArgs e)
    {
        string CodiceSoggetto = txtCodiciTargaturaCodiceSoggetto.Text;
        string CriterAPIKey = txtCodiciTargaturaApiKey.Text;
        string CriterAPIKeySoggetto = txtCodiciTargaturaApiKeySoggetto.Text;

        APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, ID = string.Empty, CodiceTargatura=string.Empty, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto };

        object json = await GetJson("api/CriterService/GetCodiciTargaturaByCodiceSoggetto", param, null);
        if (json != null)
        {
            this.Txt_Result_CodiciTargatura.Text = json.ToString();
        }
        else
        {
            this.Txt_Result_CodiciTargatura.Text = "Errore";
        }
    }

    protected async void Btn_GetLibrettoImpianto_ByCodice_Click(object sender, EventArgs e)
    {
        string CodiceLibretto = Txt_DefaultParam_GetLibrettoImpianto_ByCodice.Text;
        string CriterAPIKey = txtApiKeyLibretto.Text;
        string CodiceSoggetto = txtGetCodiceSoggettoLibretto.Text;
        string CriterAPIKeySoggetto = txtGetApiKeySoggettoLibretto.Text;

        APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, ID = string.Empty, CodiceTargatura = CodiceLibretto, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto };

        object json = await GetJson("api/CriterService/GetLibrettoImpiantoByCodice", param, null);
        if (json != null)
        {
            this.Txt_Result_GetLibrettoImpianto_ByCodice.Text = json.ToString();
        }
        else
        {
            this.Txt_Result_GetLibrettoImpianto_ByCodice.Text = "Errore";
        }
    }

    protected async void Btn_GetRapportoTecnico_GT_ByID_Click(object sender, EventArgs e)
    {
        string iDRapportoControlloTecnico = this.Txt_DefaultParam_GetRapportoTecnico_GT_ByID.Text != "" ? this.Txt_DefaultParam_GetRapportoTecnico_GT_ByID.Text : "FB4F4D45-A934-4C6F-8EAC-E4E47A8DB187";
        string CriterAPIKey = txtApiKeyGT.Text;
        string CodiceSoggetto = txtGetCodiceSoggettoGT.Text;
        string CriterAPIKeySoggetto = txtGetApiKeySoggettoGT.Text;

        APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto= CriterAPIKeySoggetto, ID = iDRapportoControlloTecnico, CodiceTargatura=string.Empty };

        object json = await GetJson("api/CriterService/GetRapportoTecnico_GT_ByID", param, (int) RCT_TipoRapportoDiControlloTecnico.GT);
        if (json != null)
        {
            this.Txt_Result_GetRapportoTecnico_GT_ByID.Text = json.ToString();
        }
        else
        {
            this.Txt_Result_GetRapportoTecnico_GT_ByID.Text = "Errore";
        }
    }

    protected async void Btn_GetRapportoTecnico_GF_ByID_Click(object sender, EventArgs e)
    {
        string iDRapportoControlloTecnico = this.Txt_DefaultParam_GetRapportoTecnico_GF_ByID.Text != "" ? this.Txt_DefaultParam_GetRapportoTecnico_GF_ByID.Text : "1B6437F6-9231-490F-9FD1-2BA98F544714";
        string CriterAPIKey = txtApiKeyGF.Text;
        string CodiceSoggetto = txtGetCodiceSoggettoGF.Text;
        string CriterAPIKeySoggetto = txtGetApiKeySoggettoGF.Text;

        APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto, ID = iDRapportoControlloTecnico, CodiceTargatura=string.Empty };
        
        object json = await GetJson("api/CriterService/GetRapportoTecnico_GF_ByID", param, (int) RCT_TipoRapportoDiControlloTecnico.GF);
        if (json != null)
        {
            this.Txt_Result_GetRapportoTecnico_GF_ByID.Text = json.ToString();
        }
        else
        {
            this.Txt_Result_GetRapportoTecnico_GF_ByID.Text = "Errore";
        }
    }

    protected async void Btn_GetRapportoTecnico_SC_ByID_Click(object sender, EventArgs e)
    {
        string iDRapportoControlloTecnico = this.Txt_DefaultParam_GetRapportoTecnico_SC_ByID.Text != "" ? this.Txt_DefaultParam_GetRapportoTecnico_SC_ByID.Text : "1B8016A6-9CB1-4D17-87C7-EB8D2BFFFA5E";

        string CriterAPIKey = txtApiKeySC.Text;
        string CodiceSoggetto = txtGetCodiceSoggettoSC.Text;
        string CriterAPIKeySoggetto = txtGetApiKeySoggettoSC.Text;

        APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto, ID = iDRapportoControlloTecnico, CodiceTargatura=string.Empty };

        object json = await GetJson("api/CriterService/GetRapportoTecnico_SC_ByID", param, (int) RCT_TipoRapportoDiControlloTecnico.SC);

        if (json != null)
        {
            this.Txt_Result_GetRapportoTecnico_SC_ByID.Text = json.ToString();
        }
        else
        {
            this.Txt_Result_GetRapportoTecnico_SC_ByID.Text = "Errore";
        }
    }

    protected async void Btn_GetRapportoTecnico_CG_ByID_Click(object sender, EventArgs e)
    {
        string iDRapportoControlloTecnico = this.Txt_DefaultParam_GetRapportoTecnico_CG_ByID.Text != "" ? this.Txt_DefaultParam_GetRapportoTecnico_CG_ByID.Text : "5EFF51AE-E9C9-401E-8581-3089D802CF90";
        string CriterAPIKey = txtApiKeyCG.Text;
        string CodiceSoggetto = txtGetCodiceSoggettoCG.Text;
        string CriterAPIKeySoggetto = txtGetApiKeySoggettoCG.Text;

        APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto, ID = iDRapportoControlloTecnico };
        
        object json = await GetJson("api/CriterService/GetRapportoTecnico_CG_ByID", param, (int) RCT_TipoRapportoDiControlloTecnico.CG);
        if (json != null)
        {
            this.Txt_Result_GetRapportoTecnico_CG_ByID.Text = json.ToString();
        }
        else
        {
            this.Txt_Result_GetRapportoTecnico_CG_ByID.Text = "Errore";
        }
    }

    #endregion

    #region Validazioni Json

    protected async void Btn_ValidateLibrettoImpianto_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                POMJ_LibrettoImpianto lib = JsonConvert.DeserializeObject<POMJ_LibrettoImpianto>(Txt_Validate_LibrettoImpianto.Text);
                lib.CriterAPIKey = txtValidateApiKeyLibretto.Text;

                string result = await GetValidationLibrettoResult("api/CriterService/ValidationLibrettoImpianto", lib);
                lblLibrettoValidateResult.Text = result;
                lblLibrettoValidateResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblLibrettoValidateResult.ForeColor = System.Drawing.Color.Red;
                lblLibrettoValidateResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }
    
    protected async void Btn_ValidateRapportoGT_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                POMJ_RapportoControlloTecnico_GT rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_GT>(Txt_Validate_RapportoGT.Text);
                rap.CriterAPIKey = txtValidateApiKeyRapportoGT.Text;

                string result = await GetValidationRapportoGTResult("api/CriterService/ValidationRapportoControlloTecnico_GT", rap);
                this.lblRapportoGTValidateResult.Text = result;
                this.lblRapportoGTValidateResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoGTValidateResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoGTValidateResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    protected async void Btn_ValidateRapportoGF_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                POMJ_RapportoControlloTecnico_GF rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_GF>(Txt_Validate_RapportoGF.Text);
                rap.CriterAPIKey = txtValidateApiKeyRapportoGF.Text;

                string result = await GetValidationRapportoGFResult("api/CriterService/ValidationRapportoControlloTecnico_GF", rap);
                this.lblRapportoGFValidateResult.Text = result;
                this.lblRapportoGFValidateResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoGFValidateResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoGFValidateResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    protected async void Btn_ValidateRapportoSC_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                POMJ_RapportoControlloTecnico_SC rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_SC>(Txt_Validate_RapportoSC.Text);
                rap.CriterAPIKey = txtValidateApiKeyRapportoSC.Text;

                string result = await GetValidationRapportoSCResult("api/CriterService/ValidationRapportoControlloTecnico_SC", rap);
                this.lblRapportoSCValidateResult.Text = result;
                this.lblRapportoSCValidateResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoSCValidateResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoSCValidateResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    protected async void Btn_ValidateRapportoCG_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                POMJ_RapportoControlloTecnico_CG rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_CG>(Txt_Validate_RapportoCG.Text);
                rap.CriterAPIKey = txtValidateApiKeyRapportoCG.Text;

                string result = await GetValidationRapportoCGResult("api/CriterService/ValidationRapportoControlloTecnico_CG", rap);
                this.lblRapportoCGValidateResult.Text = result;
                this.lblRapportoCGValidateResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoCGValidateResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoCGValidateResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    #endregion

    #region Upload Json
    protected async void Btn_UploadLibrettoImpianto_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string CriterAPIKey = txtUploadApiKeyLibretto.Text;
                string CodiceSoggetto = txtUploadCodiceSoggettoLibretto.Text;
                string CriterAPIKeySoggetto = txtUploadApiKeySoggettoLibretto.Text;

                APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, ID = string.Empty, CodiceTargatura=string.Empty, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto };
                
                POMJ_LibrettoImpianto lib = JsonConvert.DeserializeObject<POMJ_LibrettoImpianto>(Txt_Upload_LibrettoImpianto.Text);
                lib.CriterAPIKey = CriterAPIKey;
                lib.CodiceSoggetto = CodiceSoggetto;
                lib.CriterAPIKeySoggetto = CriterAPIKeySoggetto;

                string result = await LoadLibrettoResult("api/CriterService/LoadLibrettoImpianto", lib);
                this.lblLibrettoUploadResult.Text = result;
                this.lblLibrettoUploadResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblLibrettoUploadResult.ForeColor = System.Drawing.Color.Red;
                lblLibrettoUploadResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    protected async void Btn_UploadRapportoGT_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string CriterAPIKey = txtUploadApiKeyRapportoGT.Text;
                string CodiceSoggetto = txtUploadCodiceSoggettoRapportoGT.Text;
                string CriterAPIKeySoggetto = txtUploadApiKeySoggettoRapportoGT.Text;

                APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, ID = string.Empty, CodiceTargatura = string.Empty, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto };
                
                POMJ_RapportoControlloTecnico_GT rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_GT>(Txt_Upload_RapportoGT.Text);
                rap.CriterAPIKey = CriterAPIKey;
                rap.CodiceSoggetto = CodiceSoggetto;
                rap.CriterAPIKeySoggetto = CriterAPIKeySoggetto;

                string result = await LoadRapportoGTResult("api/CriterService/LoadRapportoControlloTecnico_GT", rap);
                this.lblRapportoGTUploadResult.Text = result;
                this.lblRapportoGTUploadResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoGTUploadResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoGTUploadResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    protected async void Btn_UploadRapportoGF_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string CriterAPIKey = txtUploadApiKeyRapportoGF.Text;
                string CodiceSoggetto = txtUploadCodiceSoggettoRapportoGF.Text;
                string CriterAPIKeySoggetto = txtUploadApiKeySoggettoRapportoGF.Text;

                APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, ID = string.Empty, CodiceTargatura = string.Empty, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto };
                
                POMJ_RapportoControlloTecnico_GF rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_GF>(Txt_Upload_RapportoGF.Text);
                rap.CriterAPIKey = CriterAPIKey;
                rap.CodiceSoggetto = CodiceSoggetto;
                rap.CriterAPIKeySoggetto = CriterAPIKeySoggetto;

                string result = await LoadRapportoGFResult("api/CriterService/LoadRapportoControlloTecnico_GF", rap);
                this.lblRapportoGFUploadResult.Text = result;
                this.lblRapportoGFUploadResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoGFUploadResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoGFUploadResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    protected async void Btn_UploadRapportoSC_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string CriterAPIKey = txtUploadApiKeyRapportoSC.Text;
                string CodiceSoggetto = txtUploadCodiceSoggettoRapportoSC.Text;
                string CriterAPIKeySoggetto = txtUploadApiKeySoggettoRapportoSC.Text;

                APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, ID = string.Empty, CodiceTargatura = string.Empty, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto };
                
                POMJ_RapportoControlloTecnico_SC rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_SC>(Txt_Upload_RapportoSC.Text);
                rap.CriterAPIKey = CriterAPIKey;
                rap.CodiceSoggetto = CodiceSoggetto;
                rap.CriterAPIKeySoggetto = CriterAPIKeySoggetto;

                string result = await LoadRapportoSCResult("api/CriterService/LoadRapportoControlloTecnico_SC", rap);
                this.lblRapportoSCUploadResult.Text = result;
                this.lblRapportoSCUploadResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoSCUploadResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoSCUploadResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    protected async void Btn_UploadRapportoCG_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string CriterAPIKey = txtUploadApiKeyRapportoCG.Text;
                string CodiceSoggetto = txtUploadCodiceSoggettoRapportoCG.Text;
                string CriterAPIKeySoggetto = txtUploadApiKeySoggettoRapportoCG.Text;

                APICriterIDParameter param = new APICriterIDParameter() { CriterAPIKey = CriterAPIKey, ID = string.Empty, CodiceTargatura = string.Empty, CodiceSoggetto = CodiceSoggetto, CriterAPIKeySoggetto = CriterAPIKeySoggetto };

                POMJ_RapportoControlloTecnico_CG rap = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_CG>(Txt_Upload_RapportoCG.Text);
                rap.CriterAPIKey = CriterAPIKey;
                rap.CodiceSoggetto = CodiceSoggetto;
                rap.CriterAPIKeySoggetto = CriterAPIKeySoggetto;

                string result = await LoadRapportoCGResult("api/CriterService/LoadRapportoControlloTecnico_CG", rap);
                lblRapportoCGUploadResult.Text = result;
                lblRapportoCGUploadResult.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                lblRapportoCGUploadResult.ForeColor = System.Drawing.Color.Red;
                lblRapportoCGUploadResult.Text = "Json non valido:" + ex.Message;
            }
        }
    }

    #endregion

}