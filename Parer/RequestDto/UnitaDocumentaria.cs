using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pomiager.Service.Parer.RequestDto
{
	[XmlRoot(ElementName = "UnitaDocumentaria")]
	public class UnitaDocumentaria
	{
		[XmlElement(ElementName = "Intestazione")]
		public Intestazione Intestazione { get; set; }

		[XmlElement(ElementName = "Configurazione")]
		public Configurazione Configurazione { get; set; }

		[XmlElement(ElementName = "ProfiloUnitaDocumentaria")]
		public ProfiloUnitaDocumentaria ProfiloUnitaDocumentaria { get; set; }

		[XmlElement(ElementName = "ProfiloNormativo")]
		public ProfiloNormativo ProfiloNormativo { get; set; }

		[XmlElement(ElementName = "DatiSpecifici")]
		public DatiSpecifici DatiSpecifici { get; set; }

		[XmlElement(ElementName = "NumeroAllegati")]
		public int NumeroAllegati { get; set; }

		[XmlElement(ElementName = "NumeroAnnessi")]
		public int NumeroAnnessi { get; set; }

		[XmlElement(ElementName = "NumeroAnnotazioni")]
		public int NumeroAnnotazioni { get; set; }

		[XmlElement(ElementName = "DocumentoPrincipale")]
		public DocumentoPrincipale DocumentoPrincipale { get; set; }

		[XmlElement(ElementName = "Allegati")]
		public Allegati_ Allegati { get; set; }
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
	}

	[XmlRoot(ElementName = "Chiave")]
	public class Chiave
	{

		[XmlElement(ElementName = "Numero")]
		public string Numero { get; set; }

		[XmlElement(ElementName = "Anno")]
		public string Anno { get; set; }

		[XmlElement(ElementName = "TipoRegistro")]
		public string TipoRegistro { get; set; }
	}

	[XmlRoot(ElementName = "Intestazione")]
	public class Intestazione
	{
		[XmlElement(ElementName = "Versione")]
		public string Versione { get; set; }

		[XmlElement(ElementName = "Versatore")]
		public Versatore Versatore { get; set; }

		[XmlElement(ElementName = "Chiave")]
		public Chiave Chiave { get; set; }

		[XmlElement(ElementName = "TipologiaUnitaDocumentaria")]
		public string TipologiaUnitaDocumentaria { get; set; }
	}

	[XmlRoot(ElementName = "Configurazione")]
	public class Configurazione
	{
		[XmlElement(ElementName = "TipoConservazione")]
		public string TipoConservazione { get; set; }

		[XmlElement(ElementName = "ForzaConservazione")]
		public bool ForzaConservazione { get; set; }

		//[XmlElement(ElementName = "ForzaAccettazione")]
		//public bool ForzaAccettazione { get; set; }

		[XmlElement(ElementName = "ForzaCollegamento")]
		public bool ForzaCollegamento { get; set; }

		[XmlElement(ElementName = "ForzaHash")]
		public bool ForzaHash { get; set; }

		[XmlElement(ElementName = "ForzaFormatoNumero")]
		public bool ForzaFormatoNumero { get; set; }

		[XmlElement(ElementName = "ForzaFormatoFile")]
		public bool ForzaFormatoFile { get; set; }

		[XmlElement(ElementName = "SimulaSalvataggioDatiInDB")]
		public bool SimulaSalvataggioDatiInDB { get; set; }		
	}

	[XmlRoot(ElementName = "ProfiloUnitaDocumentaria")]
	public class ProfiloUnitaDocumentaria
	{

		[XmlElement(ElementName = "Oggetto")]
		public string Oggetto { get; set; }

		[XmlElement(ElementName = "Data")]
		public string Data { get; set; }
	}

	[XmlRoot(ElementName = "ProfiloNormativo")]
	public class ProfiloNormativo
	{
		[XmlAttribute(AttributeName = "versione")]
		public string Versione { get; set; }

		//Inietto il profilo normativo Agid
		[XmlElement(ElementName = "DocumentoAmministrativoInformatico")]
		public DocumentoAmministrativoInformatico ProfiloNormativoAgid { get; set; }
	}

	[XmlRoot(ElementName = "DatiSpecifici")]
	public class DatiSpecifici
	{
		[XmlElement(ElementName = "VersioneDatiSpecifici")]
		public string VersioneDatiSpecifici { get; set; }

		[XmlElement(ElementName = "IndirizzoPECMittente")]
		public string IndirizzoPECMittente { get; set; }

		[XmlElement(ElementName = "IndirizzoPECDestinatario")]
		public string IndirizzoPECDestinatario { get; set; }

		[XmlElement(ElementName = "GestorePEC")]
		public string GestorePEC { get; set; }

		[XmlElement(ElementName = "CodiceAttivita")]
		public string CodiceAttivita { get; set; }

		[XmlElement(ElementName = "DataRicezione")]
		public string DataRicezione { get; set; }

		[XmlElement(ElementName = "DataInvio")]
		public string DataInvio { get; set; }

		[XmlElement(ElementName = "Movimento")]
		public string Movimento { get; set; }
	}

	[XmlRoot(ElementName = "Componente")]
	public class Componente
	{
		[XmlElement(ElementName = "ID")]
		public string ID { get; set; }

		[XmlElement(ElementName = "OrdinePresentazione")]
		public int OrdinePresentazione { get; set; }

		[XmlElement(ElementName = "TipoComponente")]
		public string TipoComponente { get; set; }

		[XmlElement(ElementName = "TipoSupportoComponente")]
		public string TipoSupportoComponente { get; set; }

		[XmlElement(ElementName = "NomeComponente")]
		public string NomeComponente { get; set; }

		[XmlElement(ElementName = "FormatoFileVersato")]
		public string FormatoFileVersato { get; set; }

        [XmlElement(ElementName = "HashVersato")]
		public byte[] HashVersato { get; set; }

		
		//[XmlElement(ElementName = "UrnVersato")]
		//public string UrnVersato { get; set; }

		//[XmlElement(ElementName = "IDComponenteVersato")]
		//public string IDComponenteVersato { get; set; }

		//[XmlElement(ElementName = "UtilizzoDataFirmaPerRifTemp")]
		//public bool UtilizzoDataFirmaPerRifTemp { get; set; }

		//[XmlElement(ElementName = "RiferimentoTemporale")]
		//public string RiferimentoTemporale { get; set; }

		//[XmlElement(ElementName = "DescrizioneRiferimentoTemporale")]
		//public string DescrizioneRiferimentoTemporale { get; set; }
	}
		
	[XmlRoot(ElementName = "Componenti")]
	public class Componenti
	{
		[XmlElement(ElementName = "Componente")]
		public Componente Componente { get; set; }
	}

	[XmlRoot(ElementName = "StrutturaOriginale")]
	public class StrutturaOriginale
	{
		[XmlElement(ElementName = "TipoStruttura")]
		public string TipoStruttura { get; set; }

		[XmlElement(ElementName = "Componenti")]
		public Componenti Componenti { get; set; }
	}

	[XmlRoot(ElementName = "ProfiloDocumento")]
	public class ProfiloDocumento
	{
		[XmlElement(ElementName = "Descrizione")]
		public string Descrizione { get; set; }

		[XmlElement(ElementName = "Autore")]
		public string Autore { get; set; }
	}

	[XmlRoot(ElementName = "Allegato")]
	public class Allegato
	{
		[XmlElement(ElementName = "IDDocumento")]
		public string IDDocumento { get; set; }

		[XmlElement(ElementName = "TipoDocumento")]
		public string TipoDocumento { get; set; }

		[XmlElement(ElementName = "ProfiloDocumento")]
		public ProfiloDocumento ProfiloDocumento { get; set; }

		[XmlElement(ElementName = "StrutturaOriginale")]
		public StrutturaOriginale StrutturaOriginale { get; set; }
	}

    [XmlRoot(ElementName = "Allegati")]
    public class Allegati_
    {
        [XmlElement(ElementName = "Allegato")]
        public List<Allegato> Allegato { get; set; }
    }

    [XmlRoot(ElementName = "DocumentoPrincipale")]
	public class DocumentoPrincipale
	{

		[XmlElement(ElementName = "IDDocumento")]
		public string IDDocumento { get; set; }

		[XmlElement(ElementName = "TipoDocumento")]
		public string TipoDocumento { get; set; }

		[XmlElement(ElementName = "ProfiloDocumento")]
		public ProfiloDocumento ProfiloDocumento { get; set; }

		[XmlElement(ElementName = "StrutturaOriginale")]
		public StrutturaOriginale StrutturaOriginale { get; set; }
	}

}