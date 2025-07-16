using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Pomiager.Service.Parer.ResponseDto
{
	[XmlRoot(ElementName = "EsitoGenerale")]
	public class EsitoGenerale
	{

		[XmlElement(ElementName = "CodiceEsito")]
		public string CodiceEsito { get; set; }

		[XmlElement(ElementName = "CodiceErrore")]
		public string CodiceErrore { get; set; }

		[XmlElement(ElementName = "MessaggioErrore")]
		public string MessaggioErrore { get; set; }
	}

	[XmlRoot(ElementName = "EsitoChiamataWS")]
	public class EsitoChiamataWS
	{

		[XmlElement(ElementName = "VersioneWSCorretta")]
		public string VersioneWSCorretta { get; set; }

		[XmlElement(ElementName = "CredenzialiOperatore")]
		public string CredenzialiOperatore { get; set; }

		[XmlElement(ElementName = "FileAttesiRicevuti")]
		public string FileAttesiRicevuti { get; set; }
	}

	[XmlRoot(ElementName = "EsitoXSD")]
	public class EsitoXSD
	{

		[XmlElement(ElementName = "CodiceEsito")]
		public string CodiceEsito { get; set; }

		[XmlElement(ElementName = "ControlloStrutturaXML")]
		public string ControlloStrutturaXML { get; set; }

		[XmlElement(ElementName = "UnivocitaIDComponenti")]
		public string UnivocitaIDComponenti { get; set; }

		[XmlElement(ElementName = "UnivocitaIDDocumenti")]
		public string UnivocitaIDDocumenti { get; set; }

		[XmlElement(ElementName = "CorrispondenzaAllegatiDichiarati")]
		public string CorrispondenzaAllegatiDichiarati { get; set; }

		[XmlElement(ElementName = "CorrispondenzaAnnessiDichiarati")]
		public string CorrispondenzaAnnessiDichiarati { get; set; }

		[XmlElement(ElementName = "CorrispondenzaAnnotazioniDichiarate")]
		public string CorrispondenzaAnnotazioniDichiarate { get; set; }
	}

	[XmlRoot(ElementName = "Configurazione")]
	public class Configurazione
	{

		[XmlElement(ElementName = "TipoConservazione")]
		public string TipoConservazione { get; set; }

		[XmlElement(ElementName = "ForzaAccettazione")]
		public bool ForzaAccettazione { get; set; }

		[XmlElement(ElementName = "ForzaConservazione")]
		public bool ForzaConservazione { get; set; }

		[XmlElement(ElementName = "ForzaCollegamento")]
		public bool ForzaCollegamento { get; set; }

		[XmlElement(ElementName = "ForzaHash")]
		public bool ForzaHash { get; set; }

		[XmlElement(ElementName = "ForzaFormatoNumero")]
		public bool ForzaFormatoNumero { get; set; }

		[XmlElement(ElementName = "ForzaFormatoFile")]
		public bool ForzaFormatoFile { get; set; }

		[XmlElement(ElementName = "AbilitaControlloCrittografico")]
		public bool AbilitaControlloCrittografico { get; set; }

		[XmlElement(ElementName = "AbilitaControlloTrust")]
		public bool AbilitaControlloTrust { get; set; }

		[XmlElement(ElementName = "AbilitaControlloCertificato")]
		public bool AbilitaControlloCertificato { get; set; }

		[XmlElement(ElementName = "AbilitaControlloRevoca")]
		public bool AbilitaControlloRevoca { get; set; }

		[XmlElement(ElementName = "AbilitaControlloFormato")]
		public bool AbilitaControlloFormato { get; set; }

		[XmlElement(ElementName = "AbilitaControlloCollegamento")]
		public bool AbilitaControlloCollegamento { get; set; }

		[XmlElement(ElementName = "AbilitaControlloHash")]
		public bool AbilitaControlloHash { get; set; }

		[XmlElement(ElementName = "AbilitaControlloFormatoNumero")]
		public bool AbilitaControlloFormatoNumero { get; set; }

		[XmlElement(ElementName = "AccettaFirmaSconosciuta")]
		public bool AccettaFirmaSconosciuta { get; set; }

		[XmlElement(ElementName = "AccettaFirmaNonConforme")]
		public bool AccettaFirmaNonConforme { get; set; }

		[XmlElement(ElementName = "AccettaFirmaNoDelibera45")]
		public bool AccettaFirmaNoDelibera45 { get; set; }

		[XmlElement(ElementName = "AccettaMarcaSconosciuta")]
		public bool AccettaMarcaSconosciuta { get; set; }

		[XmlElement(ElementName = "AccettaControlloCrittograficoNegativo")]
		public bool AccettaControlloCrittograficoNegativo { get; set; }

		[XmlElement(ElementName = "AccettaControlloTrustNegativo")]
		public bool AccettaControlloTrustNegativo { get; set; }

		[XmlElement(ElementName = "AccettaControlloCertificatoScaduto")]
		public bool AccettaControlloCertificatoScaduto { get; set; }

		[XmlElement(ElementName = "AccettaControlloCertificatoNoValido")]
		public bool AccettaControlloCertificatoNoValido { get; set; }

		[XmlElement(ElementName = "AccettaControlloCertificatoNoFirma")]
		public bool AccettaControlloCertificatoNoFirma { get; set; }

		[XmlElement(ElementName = "AccettaControlloCRLNegativo")]
		public bool AccettaControlloCRLNegativo { get; set; }

		[XmlElement(ElementName = "AccettaControlloCRLScaduta")]
		public bool AccettaControlloCRLScaduta { get; set; }

		[XmlElement(ElementName = "AccettaControlloCRLNoValida")]
		public bool AccettaControlloCRLNoValida { get; set; }

		[XmlElement(ElementName = "AccettaControlloCRLNoScaric")]
		public bool AccettaControlloCRLNoScaric { get; set; }

		[XmlElement(ElementName = "AccettaControlloOCSPNegativo")]
		public bool AccettaControlloOCSPNegativo { get; set; }

		[XmlElement(ElementName = "AccettaControlloOCSPNoValido")]
		public bool AccettaControlloOCSPNoValido { get; set; }

		[XmlElement(ElementName = "AccettaControlloOCSPNoScaric")]
		public bool AccettaControlloOCSPNoScaric { get; set; }

		[XmlElement(ElementName = "AccettaControlloFormatoNegativo")]
		public bool AccettaControlloFormatoNegativo { get; set; }

		[XmlElement(ElementName = "AccettaControlloHashNegativo")]
		public bool AccettaControlloHashNegativo { get; set; }

		[XmlElement(ElementName = "AccettaControlloFormatoNumeroNegativo")]
		public bool AccettaControlloFormatoNumeroNegativo { get; set; }

		[XmlElement(ElementName = "AccettaControlloCollegamentiNegativo")]
		public bool AccettaControlloCollegamentiNegativo { get; set; }

		[XmlElement(ElementName = "DataObbligatorio")]
		public bool DataObbligatorio { get; set; }

		[XmlElement(ElementName = "OggettoObbligatorio")]
		public bool OggettoObbligatorio { get; set; }

		[XmlElement(ElementName = "AbilitaVerificaFirma")]
		public bool AbilitaVerificaFirma { get; set; }

		[XmlElement(ElementName = "AbilitaVerificaFirmaSoloCrypto")]
		public bool AbilitaVerificaFirmaSoloCrypto { get; set; }

		[XmlElement(ElementName = "AbilitaConservazioneNonFirmati")]
		public bool AbilitaConservazioneNonFirmati { get; set; }

		[XmlElement(ElementName = "AccettaConservazioneNonFirmatiNegativa")]
		public bool AccettaConservazioneNonFirmatiNegativa { get; set; }

		[XmlElement(ElementName = "ForzaControlloRevoca")]
		public bool ForzaControlloRevoca { get; set; }

		[XmlElement(ElementName = "ForzaControlloTrust")]
		public bool ForzaControlloTrust { get; set; }

		[XmlElement(ElementName = "ForzaControlloCertificato")]
		public bool ForzaControlloCertificato { get; set; }

		[XmlElement(ElementName = "ForzaControlloCrittografico")]
		public bool ForzaControlloCrittografico { get; set; }

		[XmlElement(ElementName = "ForzaControlloNonConformita")]
		public bool ForzaControlloNonConformita { get; set; }
	}

	[XmlRoot(ElementName = "Versatore")]
	public class Versatore
	{

		[XmlElement(ElementName = "Ambiente")]
		public string Ambiente { get; set; }

		[XmlElement(ElementName = "Ente")]
		public string Ente { get; set; }

		[XmlElement(ElementName = "Struttura")]
		public string Struttura { get; set; }

		[XmlElement(ElementName = "UserID")]
		public string UserID { get; set; }

		[XmlElement(ElementName = "IndirizzoIp")]
		public string IndirizzoIp { get; set; }

		[XmlElement(ElementName = "SistemaVersante")]
		public string SistemaVersante { get; set; }
	}

	[XmlRoot(ElementName = "Chiave")]
	public class Chiave
	{

		[XmlElement(ElementName = "Numero")]
		public int Numero { get; set; }

		[XmlElement(ElementName = "Anno")]
		public int Anno { get; set; }

		[XmlElement(ElementName = "TipoRegistro")]
		public string TipoRegistro { get; set; }
	}

	[XmlRoot(ElementName = "EsitoUnitaDocumentaria")]
	public class EsitoUnitaDocumentaria
	{

		[XmlElement(ElementName = "CodiceEsito")]
		public string CodiceEsito { get; set; }

		[XmlElement(ElementName = "IdentificazioneVersatore")]
		public string IdentificazioneVersatore { get; set; }

		[XmlElement(ElementName = "UnivocitaChiave")]
		public string UnivocitaChiave { get; set; }

		[XmlElement(ElementName = "VerificaTipologiaUD")]
		public string VerificaTipologiaUD { get; set; }

		[XmlElement(ElementName = "CorrispondenzaDatiSpecifici")]
		public string CorrispondenzaDatiSpecifici { get; set; }

		[XmlElement(ElementName = "CorrispondenzaProfiloNormativo")]
		public string CorrispondenzaProfiloNormativo { get; set; }

		[XmlElement(ElementName = "PresenzaUDCollegate")]
		public string PresenzaUDCollegate { get; set; }
	}

	[XmlRoot(ElementName = "EsitoDocumento")]
	public class EsitoDocumento
	{

		[XmlElement(ElementName = "CodiceEsito")]
		public string CodiceEsito { get; set; }

		[XmlElement(ElementName = "VerificaTipoDocumento")]
		public string VerificaTipoDocumento { get; set; }

		[XmlElement(ElementName = "CorrispondenzaDatiSpecifici")]
		public string CorrispondenzaDatiSpecifici { get; set; }

		[XmlElement(ElementName = "CorrispondenzaDatiFiscali")]
		public string CorrispondenzaDatiFiscali { get; set; }

		[XmlElement(ElementName = "VerificaTipoStruttura")]
		public string VerificaTipoStruttura { get; set; }

		[XmlElement(ElementName = "UnivocitaOrdinePresentazione")]
		public string UnivocitaOrdinePresentazione { get; set; }
	}

	[XmlRoot(ElementName = "EsitoComponente")]
	public class EsitoComponente
	{

		[XmlElement(ElementName = "CodiceEsito")]
		public string CodiceEsito { get; set; }

		[XmlElement(ElementName = "VerificaTipoComponente")]
		public string VerificaTipoComponente { get; set; }

		[XmlElement(ElementName = "CorrispondenzaDatiSpecifici")]
		public string CorrispondenzaDatiSpecifici { get; set; }

		[XmlElement(ElementName = "VerificaTipoSupportoComponente")]
		public string VerificaTipoSupportoComponente { get; set; }

		[XmlElement(ElementName = "VerificaNomeComponente")]
		public string VerificaNomeComponente { get; set; }

		[XmlElement(ElementName = "VerificaAmmissibilitaFormato")]
		public string VerificaAmmissibilitaFormato { get; set; }

		[XmlElement(ElementName = "VerificaRiconoscimentoFormato")]
		public string VerificaRiconoscimentoFormato { get; set; }

		[XmlElement(ElementName = "MessaggioRiconoscimentoFormato")]
		public string MessaggioRiconoscimentoFormato { get; set; }
	}

	[XmlRoot(ElementName = "Componente")]
	public class Componente
	{

		[XmlElement(ElementName = "OrdinePresentazione")]
		public int OrdinePresentazione { get; set; }

		[XmlElement(ElementName = "TipoComponente")]
		public string TipoComponente { get; set; }

		[XmlElement(ElementName = "URN")]
		public string URN { get; set; }

		[XmlElement(ElementName = "Hash")]
		public string Hash { get; set; }

		[XmlElement(ElementName = "AlgoritmoHash")]
		public string AlgoritmoHash { get; set; }

		[XmlElement(ElementName = "Encoding")]
		public string Encoding { get; set; }

		[XmlElement(ElementName = "FormatoRappresentazione")]
		public string FormatoRappresentazione { get; set; }

		[XmlElement(ElementName = "FormatoRappresentazioneEsteso")]
		public string FormatoRappresentazioneEsteso { get; set; }

		[XmlElement(ElementName = "IdoneitaFormato")]
		public string IdoneitaFormato { get; set; }

		[XmlElement(ElementName = "DimensioneFile")]
		public int DimensioneFile { get; set; }

		[XmlElement(ElementName = "FirmatoDigitalmente")]
		public bool FirmatoDigitalmente { get; set; }

		[XmlElement(ElementName = "NomeComponente")]
		public string NomeComponente { get; set; }

		[XmlElement(ElementName = "HashVersato")]
		public string HashVersato { get; set; }

		[XmlElement(ElementName = "EsitoComponente")]
		public EsitoComponente EsitoComponente { get; set; }
	}

	[XmlRoot(ElementName = "Componenti")]
	public class Componenti
	{

		[XmlElement(ElementName = "Componente")]
		public Componente Componente { get; set; }
	}

	[XmlRoot(ElementName = "DocumentoPrincipale")]
	public class DocumentoPrincipale
	{

		[XmlElement(ElementName = "ChiaveDoc")]
		public string ChiaveDoc { get; set; }

		[XmlElement(ElementName = "IDDocumento")]
		public int IDDocumento { get; set; }

		[XmlElement(ElementName = "TipoDocumento")]
		public string TipoDocumento { get; set; }

		[XmlElement(ElementName = "FirmatoDigitalmente")]
		public bool FirmatoDigitalmente { get; set; }

		[XmlElement(ElementName = "EsitoDocumento")]
		public EsitoDocumento EsitoDocumento { get; set; }

		[XmlElement(ElementName = "Componenti")]
		public Componenti Componenti { get; set; }
	}

	[XmlRoot(ElementName = "Allegato")]
	public class Allegato
	{

		[XmlElement(ElementName = "ChiaveDoc")]
		public string ChiaveDoc { get; set; }

		[XmlElement(ElementName = "IDDocumento")]
		public string IDDocumento { get; set; }

		[XmlElement(ElementName = "TipoDocumento")]
		public string TipoDocumento { get; set; }

		[XmlElement(ElementName = "FirmatoDigitalmente")]
		public bool FirmatoDigitalmente { get; set; }

		[XmlElement(ElementName = "EsitoDocumento")]
		public EsitoDocumento EsitoDocumento { get; set; }

		[XmlElement(ElementName = "Componenti")]
		public Componenti Componenti { get; set; }
	}

	[XmlRoot(ElementName = "Allegati")]
	public class Allegati
	{

		[XmlElement(ElementName = "Allegato")]
		public Allegato Allegato { get; set; }
	}

	[XmlRoot(ElementName = "UnitaDocumentaria")]
	public class UnitaDocumentaria
	{

		[XmlElement(ElementName = "Versatore")]
		public Versatore Versatore { get; set; }

		[XmlElement(ElementName = "Chiave")]
		public Chiave Chiave { get; set; }

		[XmlElement(ElementName = "DataVersamento")]
		public DateTime DataVersamento { get; set; }

		[XmlElement(ElementName = "StatoConservazione")]
		public string StatoConservazione { get; set; }

		[XmlElement(ElementName = "FirmatoDigitalmente")]
		public bool FirmatoDigitalmente { get; set; }

		[XmlElement(ElementName = "EsitoUnitaDocumentaria")]
		public EsitoUnitaDocumentaria EsitoUnitaDocumentaria { get; set; }

		[XmlElement(ElementName = "DocumentoPrincipale")]
		public DocumentoPrincipale DocumentoPrincipale { get; set; }

		[XmlElement(ElementName = "Allegati")]
		public Allegati Allegati { get; set; }
	}

	[XmlRoot(ElementName = "EsitoVersamento")]
	public class EsitoVersamento
	{

		[XmlElement(ElementName = "Versione")]
		public string Versione { get; set; }

		[XmlElement(ElementName = "VersioneXMLChiamata")]
		public string VersioneXMLChiamata { get; set; }

		[XmlElement(ElementName = "URNSIP")]
		public string URNSIP { get; set; }

		[XmlElement(ElementName = "DataVersamento")]
		public DateTime DataVersamento { get; set; }

		[XmlElement(ElementName = "EsitoGenerale")]
		public EsitoGenerale EsitoGenerale { get; set; }

		[XmlElement(ElementName = "EsitoChiamataWS")]
		public EsitoChiamataWS EsitoChiamataWS { get; set; }

		[XmlElement(ElementName = "EsitoXSD")]
		public EsitoXSD EsitoXSD { get; set; }

		[XmlElement(ElementName = "Configurazione")]
		public Configurazione Configurazione { get; set; }

		[XmlElement(ElementName = "UnitaDocumentaria")]
		public UnitaDocumentaria UnitaDocumentaria { get; set; }

		[XmlElement(ElementName = "RapportoVersamento")]
		public string RapportoVersamento { get; set; }
	}





}
