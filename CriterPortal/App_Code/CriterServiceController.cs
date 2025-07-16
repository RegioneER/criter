using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DataUtilityCore;
using Criter.Libretto;
using Criter.Rapporti;
using CriterAPI;
using CriterAPI.DataSource;
using CriterAPI.Validators;
using CriterAPI.Entity;
using CriterAPI.Filters;
using Criter.Anagrafica;
using Criter.Lookup;
using Criter.Login;

public class CriterServiceController : System.Web.Http.ApiController
{
    EFCriterDataSource ds;
    public CriterServiceController()
    {
        ds = new CriterAPI.DataSource.EFCriterDataSource();
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetLoginCriter(APICriterIDParameter param)
    {
        string username = param.username;
        string password = param.password;
        string ApiKey = param.CriterAPIKey;

        if (APIKeyHelper.IsKeyValid(ApiKey))
        {
            POMJ_Login user = ds.GetLoginCriter(username, password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Errore nella richiesta della Login a Criter!");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    /************************************************************************************************
    * 
    *                                  Lookup Criter
    *                                  
    ************************************************************************************************/

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetLookupCriter(APICriterIDParameter param)
    {
        string CriterAPIKey = param.CriterAPIKey;
        string SysTableName = param.SysTableName;
        bool OnlyActive = bool.Parse(param.OnlyActive);

        if (APIKeyHelper.IsKeyValid(CriterAPIKey))
        {
            List<object> lookup = ds.GetLookUp(SysTableName, OnlyActive);
            return Ok(lookup);
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetRaccomandazioniPrescrizioni(APICriterIDParameter param)
    {
        string CriterAPIKey = param.CriterAPIKey;
        
        if (APIKeyHelper.IsKeyValid(CriterAPIKey))
        {
            List<RaccomandazioniPrescrizioni> raccomandazioniPrescrizioniList = ds.GetRaccomandazioniPrescrizioni();
            return Ok(raccomandazioniPrescrizioniList);
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetIDLibrettoImpiantoByCodiceTargatura([FromBody] string codiceTargatura)
    {
        int? iDLibrettoImpianto = CriterEntity.GetIDLibrettoImpiantoByCodiceTargatura(codiceTargatura);
                
        return Ok(iDLibrettoImpianto.ToString());
    }
    
    [System.Web.Http.HttpPost]
    public IHttpActionResult GetUrlPdfLibretto([FromBody] string iDLibrettoImpianto)
    {
        string url = CriterEntity.GetUrlLibrettoImpianto(iDLibrettoImpianto);

        return Ok(url);
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetIDRapportoControlloTecnicoByGuid([FromBody] string guidRapporto)
    {
        long? iDRapportoControlloTecnico = CriterEntity.GetIDRapportoControlloTecnicoByGuid(guidRapporto);

        return Ok(iDRapportoControlloTecnico.ToString());
    }


    [System.Web.Http.HttpPost]
    public IHttpActionResult GetUrlPdfRct([FromBody] string iDRapportoControlloTecnico)
    {
        string url = CriterEntity.GetUrlRapportoControlloTecnico(iDRapportoControlloTecnico);

        return Ok(url);
    }

    /************************************************************************************************
    * 
    *                                  Libretti Impianto
    *                                  
    ************************************************************************************************/

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetAnagraficaImpresaByPiva([FromBody] string Piva)
    {
        POMJ_AnagraficaSoggetti anagrafica = ds.GetAnagraficaImpresaByPIva(Piva);
        if (anagrafica != null)
        {
            return Ok(anagrafica);
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Errore nella richiesta dell'anagrafica Impresa");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetAnagraficaManutentoriByIDSoggetto([FromBody] int IDSoggetto)
    {
        List<POMJ_AnagraficaSoggetti> manutentori = ds.GetAnagraficaManutentoriByIDAzienda(IDSoggetto);
        if (manutentori.Count > 0)
        {
            return Ok(manutentori);
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Errore nella richiesta delle anagrafiche Manutentori");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetApiKeyImpresaByPIva([FromBody] string Piva)
    {
        string KeyApi = ds.GetApiKeyImpresaByPIva(Piva);
        if (!string.IsNullOrEmpty(KeyApi))
        {
            return Ok(KeyApi);
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Errore nella richiesta dell'Api Key Impresa");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetCodiciTargaturaByCodiceSoggetto(APICriterIDParameter param)
    {
        string codiceSoggetto = param.CodiceSoggetto;
        string CriterAPIKeySoggetto = param.CriterAPIKeySoggetto;
        string CriterAPIKey = param.CriterAPIKey;

        if (APIKeyHelper.IsKeyValid(CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(codiceSoggetto, CriterAPIKeySoggetto))
            {
                List<POMJ_TargaturaImpianto> targature = ds.GeCodiciTargaturaByCodiceSoggetto(codiceSoggetto);
                return Ok(targature);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }    
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetIDStatoLibrettoByCodiceTargatura([FromBody] string codiceTargatura)
    {
        int? iDStato = CriterEntity.GetIDStatoLibrettoImpiantoByCodiceTargatura(codiceTargatura);

        //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, fDenifitivo);
        return Ok(iDStato.ToString());
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetLibrettoImpiantoByCodice(APICriterIDParameter param)
    {
        string codiceSoggetto = param.CodiceSoggetto;
        string CriterAPIKeySoggetto = param.CriterAPIKeySoggetto;
        string CriterAPIKey = param.CriterAPIKey;
        string CodiceTargatura = param.CodiceTargatura;

        if (APIKeyHelper.IsKeyValid(CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(codiceSoggetto, CriterAPIKeySoggetto))
            {
                POMJ_LibrettoImpianto lib = ds.GetLibrettoByCodiceImpianto(CodiceTargatura);
                return Ok(lib);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetLibrettoImpiantoByPodPdr(APICriterIDParameter param)
    {
        string codiceSoggetto = param.CodiceSoggetto;
        string CriterAPIKeySoggetto = param.CriterAPIKeySoggetto;
        string CriterAPIKey = param.CriterAPIKey;
        string CodicePod = param.CodicePod;
        string CodicePdr = param.CodicePdr;

        if (APIKeyHelper.IsKeyValid(CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(codiceSoggetto, CriterAPIKeySoggetto))
            {
                POMJ_LibrettoImpianto lib = ds.GetLibrettoByPodPdr(CodicePod, CodicePdr);
                return Ok(lib);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    [ValidateModel]
    public HttpResponseMessage ValidationLibrettoImpianto(POMJ_LibrettoImpianto librettoImpianto)
    {
        if (APIKeyHelper.IsKeyValid(librettoImpianto.CriterAPIKey))
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Key not valid");
        }
    }
    
    [System.Web.Http.HttpPost]
    [ValidateModel]
    public IHttpActionResult LoadLibrettoImpianto(POMJ_LibrettoImpianto librettoImpianto)
    {
        if (APIKeyHelper.IsKeyValid(librettoImpianto.CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(librettoImpianto.CodiceSoggetto, librettoImpianto.CriterAPIKeySoggetto))
            {
                //string codiceTargatura = librettoImpianto.CodiceTargaturaImpianto.CodiceTargatura;
                //if (APIKeyHelper.IsCodiceTargaturaImpiantoValid(codiceTargatura))
                //{
                string returnLibretto = CriterEntity.LoadLibrettoImpianto(librettoImpianto);
                if (returnLibretto.Contains("Libretto Impianto inserito correttamente")
                    ||
                    returnLibretto.Contains("Libretto Impianto aggiornato correttamente")
                    ||
                    returnLibretto.Contains("Libretto Impianto revisionato correttamente"))
                {
                    return Ok(returnLibretto);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, returnLibretto);
                }
                //    return Ok(CriterEntity.LoadLibrettoImpianto(librettoImpianto));
                //}
                //else
                //{
                //    return Content(HttpStatusCode.BadRequest, "Codice targatura già associato ad un libretto");
                //}
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            } 
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    /************************************************************************************************
    * 
    *                                  Rapporti Tecnici Di Controllo
    *                                  
    ************************************************************************************************/

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetIDStatoRapportoByGuid([FromBody] string guidRapporto)
    {
        int? iDStato = CriterEntity.GetIDStatoRapportoDiControlloByGuid(guidRapporto);
                
        return Ok(iDStato.ToString());
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult LoadRapportoControlloTecnico_GT(POMJ_RapportoControlloTecnico_GT rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(rapportoTecnico.CodiceSoggetto, rapportoTecnico.CriterAPIKeySoggetto))
            {
                if (APIKeyHelper.IsCodiceTargaturaImpiantoLibrettoDefinitivoValid(rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura))
                {
                    string returnRapporto = CriterEntity.LoadRapportoControlloTecnico_GT(rapportoTecnico);
                    if (returnRapporto.Contains("Rapporto di controllo Tecnico GT inserito correttamente con ID")
                    ||
                    returnRapporto.Contains("Rapporto di controllo Tecnico GT aggiornato correttamente con ID")
                    )
                    {
                        return Ok(returnRapporto);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, returnRapporto);
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Codice targatura impianto non valido perchè associato ad un libretto in bozza");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    [ValidateModel]
    public HttpResponseMessage ValidationRapportoControlloTecnico_GT(POMJ_RapportoControlloTecnico_GT rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Key not valid");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult LoadRapportoControlloTecnico_GF(POMJ_RapportoControlloTecnico_GF rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(rapportoTecnico.CodiceSoggetto, rapportoTecnico.CriterAPIKeySoggetto))
            {
                if (APIKeyHelper.IsCodiceTargaturaImpiantoLibrettoDefinitivoValid(rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura))
                {
                    //string validate = CriterValidator.ValidateRapportoControlloTecnico_GF(rapportoTecnico);
                    //return Ok(CriterEntity.LoadRapportoControlloTecnico_GF(rapportoTecnico));

                    string returnRapporto = CriterEntity.LoadRapportoControlloTecnico_GF(rapportoTecnico);
                    if (returnRapporto.Contains("Rapporto di controllo Tecnico GF inserito correttamente con ID")
                    ||
                    returnRapporto.Contains("Rapporto di controllo Tecnico GF aggiornato correttamente con ID")
                    )
                    {
                        return Ok(returnRapporto);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, returnRapporto);
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Codice targatura impianto non valido perchè associato ad un libretto in bozza");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    [ValidateModel]
    public HttpResponseMessage ValidationRapportoControlloTecnico_GF(POMJ_RapportoControlloTecnico_GF rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Key not valid");
        }
    }


    [System.Web.Http.HttpPost]
    public IHttpActionResult LoadRapportoControlloTecnico_SC(POMJ_RapportoControlloTecnico_SC rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(rapportoTecnico.CodiceSoggetto, rapportoTecnico.CriterAPIKeySoggetto))
            {
                if (APIKeyHelper.IsCodiceTargaturaImpiantoLibrettoDefinitivoValid(rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura))
                {
                    //string validate = CriterValidator.ValidateRapportoControlloTecnico_SC(rapportoTecnico);
                    //return Ok(CriterEntity.LoadRapportoControlloTecnico_SC(rapportoTecnico));
                    string returnRapporto = CriterEntity.LoadRapportoControlloTecnico_SC(rapportoTecnico);
                    if (returnRapporto.Contains("Rapporto di controllo Tecnico SC inserito correttamente con ID")
                    ||
                    returnRapporto.Contains("Rapporto di controllo Tecnico SC aggiornato correttamente con ID")
                    )
                    {
                        return Ok(returnRapporto);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, returnRapporto);
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Codice targatura impianto non valido perchè associato ad un libretto in bozza");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    [ValidateModel]
    public HttpResponseMessage ValidationRapportoControlloTecnico_SC(POMJ_RapportoControlloTecnico_SC rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Key not valid");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult LoadRapportoControlloTecnico_CG(POMJ_RapportoControlloTecnico_CG rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(rapportoTecnico.CodiceSoggetto, rapportoTecnico.CriterAPIKeySoggetto))
            {
                if (APIKeyHelper.IsCodiceTargaturaImpiantoLibrettoDefinitivoValid(rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura))
                {
                    //string validate = CriterValidator.ValidateRapportoControlloTecnico_CG(rapportoTecnico);
                    //return Ok(CriterEntity.LoadRapportoControlloTecnico_CG(rapportoTecnico));
                    string returnRapporto = CriterEntity.LoadRapportoControlloTecnico_CG(rapportoTecnico);
                    if (returnRapporto.Contains("Rapporto di controllo Tecnico CG inserito correttamente con ID")
                    ||
                    returnRapporto.Contains("Rapporto di controllo Tecnico CG aggiornato correttamente con ID")
                    )
                    {
                        return Ok(returnRapporto);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, returnRapporto);
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Codice targatura impianto non valido perchè associato ad un libretto in bozza");
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    [ValidateModel]
    public HttpResponseMessage ValidationRapportoControlloTecnico_CG(POMJ_RapportoControlloTecnico_CG rapportoTecnico)
    {
        if (APIKeyHelper.IsKeyValid(rapportoTecnico.CriterAPIKey))
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Key not valid");
        }       
    }
        
    [System.Web.Http.HttpPost]
    public IHttpActionResult GetRapportoTecnico_GT_ByID(APICriterIDParameter param)
    {
        string codiceSoggetto = param.CodiceSoggetto;
        string CriterAPIKeySoggetto = param.CriterAPIKeySoggetto;
        string CriterAPIKey = param.CriterAPIKey;
        string IDRapportoTecnico = param.ID;

        if (APIKeyHelper.IsKeyValid(CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(codiceSoggetto, CriterAPIKeySoggetto))
            {
                POMJ_RapportoControlloTecnico_GT rapportotecnico_GT = ds.GetRapportoTecnico_GT_ByGuid(IDRapportoTecnico);
                return Ok(rapportotecnico_GT);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetRapportoTecnico_GF_ByID(APICriterIDParameter param)
    {
        string codiceSoggetto = param.CodiceSoggetto;
        string CriterAPIKeySoggetto = param.CriterAPIKeySoggetto;
        string CriterAPIKey = param.CriterAPIKey;
        string IDRapportoTecnico = param.ID;

        if (APIKeyHelper.IsKeyValid(param.CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(codiceSoggetto, CriterAPIKeySoggetto))
            {
                POMJ_RapportoControlloTecnico_GF rapportotecnico_GF = ds.GetRapportoTecnico_GF_ByGuid(IDRapportoTecnico);
                return Ok(rapportotecnico_GF);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetRapportoTecnico_CG_ByID(APICriterIDParameter param)
    {
        string codiceSoggetto = param.CodiceSoggetto;
        string CriterAPIKeySoggetto = param.CriterAPIKeySoggetto;
        string CriterAPIKey = param.CriterAPIKey;
        string IDRapportoTecnico = param.ID;

        if (APIKeyHelper.IsKeyValid(CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(codiceSoggetto, CriterAPIKeySoggetto))
            {
                POMJ_RapportoControlloTecnico_CG rapportotecnico_CG = ds.GetRapportoTecnico_CG_ByGuid(IDRapportoTecnico);
                return Ok(rapportotecnico_CG);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }
    
    [System.Web.Http.HttpPost]
    public IHttpActionResult GetRapportoTecnico_SC_ByID(APICriterIDParameter param)
    {
        string codiceSoggetto = param.CodiceSoggetto;
        string CriterAPIKeySoggetto = param.CriterAPIKeySoggetto;
        string CriterAPIKey = param.CriterAPIKey;
        string IDRapportoTecnico = param.ID;

        if (APIKeyHelper.IsKeyValid(param.CriterAPIKey))
        {
            if (APIKeyHelper.IsCodiceSoggettoValid(codiceSoggetto, CriterAPIKeySoggetto))
            {
                POMJ_RapportoControlloTecnico_SC rapportotecnico_SC = ds.GetRapportoTecnico_SC_ByGuid(IDRapportoTecnico);
                return Ok(rapportotecnico_SC);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Codice soggetto o Api Key soggetto non validi");
            }
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetLibrettiImpiantiByParameters([FromBody] DataUtilityCore.SaceDto.RequestLibrettoImpiantoDto dto)
    {
        // Leggi l'API Key dall'header
        var apiKey = Request.Headers.Contains("x-api-key")
                     ? Request.Headers.GetValues("x-api-key").FirstOrDefault()
                     : null;

        // Se l'API Key è presente e valida
        if (!string.IsNullOrEmpty(apiKey) && APIKeyHelper.IsKeyValid(apiKey))
        {
            // Chiamata alla funzione che gestisce la logica di ricerca
            DataUtilityCore.SaceDto.ResponseCriterDto<List<DataUtilityCore.SaceDto.ResponseLibrettiImpiantiDto>> libretti = ds.GetLibrettiImpiantiByParameters(dto);
            return Ok(libretti);
        }
        else
        {
            // Se l'API Key non è valida o è mancante
            return Content(HttpStatusCode.BadRequest, "Api Key non valida o vuota!");
        }
    }

    [System.Web.Http.HttpPost]
    public IHttpActionResult GetPodPdrByAddress([FromBody] DataUtilityCore.SaceDto.RequestPodPdrDto dto)
    {
        // Leggi l'API Key dall'header
        var apiKey = Request.Headers.Contains("x-api-key")
                     ? Request.Headers.GetValues("x-api-key").FirstOrDefault()
                     : null;

        // Se l'API Key è presente e valida
        if (!string.IsNullOrEmpty(apiKey) && APIKeyHelper.IsKeyValid(apiKey))
        {
            DataUtilityCore.SaceDto.ResponseCriterDto<List<DataUtilityCore.SaceDto.ResponsePodPdrDto>> utenze = ds.GetPodPdrByAddress(dto);
            return Ok(utenze);
        }
        else
        {
            return Content(HttpStatusCode.BadRequest, "Api Key non valida o vuota!");
        }
    }

}