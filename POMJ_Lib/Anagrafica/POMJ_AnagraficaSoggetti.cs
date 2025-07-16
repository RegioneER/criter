using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Criter.Anagrafica
{
    /// <summary>
    /// Contiene le indicazioni anagrafiche per qualunque tipo di soggetto
    /// che sia una persona fisica o un soggetto giuridico o altro
    /// </summary>
    public class POMJ_AnagraficaSoggetti
    {
        #region Anagrafica
        public int IDSoggetto { get; set; }

        public string CodiceSoggetto { get; set; }

        public int? IDSoggettoDerived { get; set; }
                
        public int? IDFormaGiuridica { get; set; }

        public int? IDPaeseSedeLegale { get; set; }

        public int? IDProvinciaSedeLegale { get; set; }

        public string Fax { get; set; }

        public string Telefono { get; set; }

        public string SitoWeb { get; set; }

        public int? IDProvinciaIscrizioneAlboImprese { get; set; }
        
        public bool fIscrizioneRegistroGasFluorurati { get; set; }
        #endregion

        public string NumeroIscrizioneRegistroGasFluorurati { get; set; }

        public bool fAttivo { get; set; }

        /// <summary>
        /// Tipo soggetto - <font color='red'><b>obbligatorio</b></font>
        /// <list type="bullet">
        /// <item>
        /// <description>1 Persona fisica</description>
        /// </item>
        /// <item>
        /// <description>2 Persona giuridica</description>
        /// </item>
        /// </list>
        /// </summary>
        [Range(1, 2, ErrorMessage = "Tipo soggetto valori consentiti compresi da 1 a 2")]
        public int? TipoSoggetto { get; set; }
        /// <summary>
        /// Nome dell'azienda
        /// </summary>
        public string NomeAzienda { get; set; }
        /// <summary>
        /// Indirizzo della sede legale
        /// </summary>
        public string IndirizzoSedeLegale { get; set; }
        /// <summary>
        /// CAP della sede legale
        /// </summary>
        public string CapSedeLegale { get; set; }
        /// <summary>
        /// Città della sede legale
        /// </summary>
        public string CittaSedeLegale { get; set; }
        /// <summary>
        /// Numero civico
        /// </summary>
        public string NumeroCivicoSedeLegale { get; set; }
        /// <summary>
        /// Vedi <a href="http://criter.pomiager.com/ApiDocumentation/Codici/CodiciProvince.txt">questo File</a> per i codici delle Province
        /// </summary>
        public int? ProvinciaSedeLegale { get; set; }
        /// <summary>
        /// Nome (in caso di persona fisica)
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cognome (in caso di persona fisica)
        /// </summary>
        public string Cognome { get; set; }
        /// <summary>
        /// Indirizzo (in caso di persona fisica)
        /// </summary>
        public string Indirizzo { get; set; }
        /// <summary>
        /// Numero civico (in caso di persona fisica)
        /// </summary>
        public string Civico { get; set; }
        /// <summary>
        /// Codice catastale comune
        /// Vedi <a href="http://criter.pomiager.com/ApiDocumentation/Codici/CodiciComuni.txt">questo File</a> per i codici catastali dei comuni
        /// </summary>
        public string CodiceCatastaleComune { get; set; }
        /// <summary>
        /// Email del soggetto
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Email PEC del soggetto
        /// </summary>
        public string EmailPec { get; set; }
        /// <summary>
        /// Partita IVA
        /// </summary>
        public string PartitaIVA { get; set; }
        /// <summary>
        /// Codice Fiscale
        /// </summary>
        public string CodiceFiscale { get; set; }
        /// <summary>
        /// NUmero di Iscrizione alla Camera di Commercio 
        /// </summary>
        public string NumeroIscrizioneCCIAA { get; set; }
        /// <summary>
        /// Provincia della Camera di Commercio
        /// Vedi <a href="http://criter.pomiager.com/ApiDocumentation/Codici/CodiciProvince.txt">questo File</a> per i codici delle province
        /// </summary>
        public string ProvinciaIscrizioneCCIAA { get; set; }
    }
}