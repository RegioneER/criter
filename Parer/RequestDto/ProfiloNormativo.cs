using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pomiager.Service.Parer.RequestDto
{
    [XmlRoot(ElementName = "DocumentoAmministrativoInformatico")]
    public class DocumentoAmministrativoInformatico
    {
        [XmlElement(ElementName = "IdDoc")]
        public IdDoc IdDoc { get; set; }
        [XmlElement(ElementName = "ModalitaDiFormazione")]
        public string ModalitaDiFormazione { get; set; }
        [XmlElement(ElementName = "TipologiaDocumentale")]
        public string TipologiaDocumentale { get; set; }
        [XmlElement(ElementName = "DatiDiRegistrazione")]
        public DatiDiRegistrazione DatiDiRegistrazione { get; set; }
        [XmlElement(ElementName = "Soggetti")]
        public Soggetti Soggetti { get; set; }
        [XmlElement(ElementName = "ChiaveDescrittiva")]
        public ChiaveDescrittiva ChiaveDescrittiva { get; set; }
        [XmlElement(ElementName = "Allegati")]
        public Allegati Allegati { get; set; }
        [XmlElement(ElementName = "Classificazione")]
        public Classificazione Classificazione { get; set; }
        [XmlElement(ElementName = "Riservato")]
        public bool Riservato { get; set; }
        [XmlElement(ElementName = "IdentificativoDelFormato")]
        public IdentificativoDelFormato IdentificativoDelFormato { get; set; }
        [XmlElement(ElementName = "Verifica")]
        public Verifica Verifica { get; set; }
        [XmlElement(ElementName = "Agg")]
        public Agg Agg { get; set; }
        [XmlElement(ElementName = "IdIdentificativoDocumentoPrimario")]
        public IdIdentificativoDocumentoPrimario IdIdentificativoDocumentoPrimario { get; set; }
        [XmlElement(ElementName = "NomeDelDocumento")]
        public string NomeDelDocumento { get; set; }
        [XmlElement(ElementName = "VersioneDelDocumento")]
        public string VersioneDelDocumento { get; set; }
        [XmlElement(ElementName = "TracciatureModificheDocumento")]
        public TracciatureModificheDocumento TracciatureModificheDocumento { get; set; }
        [XmlElement(ElementName = "TempoDiConservazione")]
        public string TempoDiConservazione { get; set; }
        [XmlElement(ElementName = "Note")]
        public string Note { get; set; }
    }



    [XmlRoot(ElementName = "ImprontaCrittograficaDelDocumento")]
    public class ImprontaCrittograficaDelDocumento
    {
        [XmlElement(ElementName = "Impronta")]
        public byte[] Impronta { get; set; }
        [XmlElement(ElementName = "Algoritmo")]
        public string Algoritmo { get; set; }
    }

    [XmlRoot(ElementName = "IdDoc")]
    public class IdDoc
    {
        [XmlElement(ElementName = "ImprontaCrittograficaDelDocumento")]
        public ImprontaCrittograficaDelDocumento ImprontaCrittograficaDelDocumento { get; set; }
        [XmlElement(ElementName = "Identificativo")]
        public string Identificativo { get; set; }
        [XmlElement(ElementName = "Segnatura")]
        public string Segnatura { get; set; }
    }

    [XmlRoot(ElementName = "Repertorio_Registro")]
    public class Repertorio_Registro
    {
        [XmlElement(ElementName = "TipoRegistro")]
        public string TipoRegistro { get; set; }
        [XmlElement(ElementName = "DataRegistrazioneDocumento")]
        public string DataRegistrazioneDocumento { get; set; }
        [XmlElement(ElementName = "OraRegistrazioneDocumento")]
        public string OraRegistrazioneDocumento { get; set; }
        [XmlElement(ElementName = "NumeroRegistrazioneDocumento")]
        public string NumeroRegistrazioneDocumento { get; set; }
        [XmlElement(ElementName = "CodiceRegistro")]
        public string CodiceRegistro { get; set; }
    }

    [XmlRoot(ElementName = "TipoRegistro")]
    public class TipoRegistro
    {
        [XmlElement(ElementName = "Repertorio_Registro")]
        public Repertorio_Registro Repertorio_Registro { get; set; }
    }

    [XmlRoot(ElementName = "DatiDiRegistrazione")]
    public class DatiDiRegistrazione
    {
        [XmlElement(ElementName = "TipologiaDiFlusso")]
        public string TipologiaDiFlusso { get; set; }
        [XmlElement(ElementName = "TipoRegistro")]
        public TipoRegistro TipoRegistro { get; set; }
    }

    [XmlRoot(ElementName = "IPAAmm")]
    public class IPAAmm
    {
        [XmlElement(ElementName = "Denominazione")]
        public string Denominazione { get; set; }
        [XmlElement(ElementName = "CodiceIPA")]
        public string CodiceIPA { get; set; }
    }

    [XmlRoot(ElementName = "IPAAOO")]
    public class IPAAOO
    {
        [XmlElement(ElementName = "Denominazione")]
        public string Denominazione { get; set; }
        [XmlElement(ElementName = "CodiceIPA")]
        public string CodiceIPA { get; set; }
    }

    [XmlRoot(ElementName = "IPAUOR")]
    public class IPAUOR
    {
        [XmlElement(ElementName = "Denominazione")]
        public string Denominazione { get; set; }
        [XmlElement(ElementName = "CodiceIPA")]
        public string CodiceIPA { get; set; }
    }

    [XmlRoot(ElementName = "PAI")]
    public class PAI
    {
        [XmlElement(ElementName = "IPAAmm")]
        public IPAAmm IPAAmm { get; set; }
        [XmlElement(ElementName = "IPAAOO")]
        public IPAAOO IPAAOO { get; set; }
        [XmlElement(ElementName = "IPAUOR")]
        public IPAUOR IPAUOR { get; set; }
        [XmlElement(ElementName = "IndirizziDigitaliDiRiferimento")]
        public List<string> IndirizziDigitaliDiRiferimento { get; set; }
    }

    [XmlRoot(ElementName = "PF")]
    public class PF
    {
        [XmlElement(ElementName = "Cognome")]
        public string Cognome { get; set; }
        [XmlElement(ElementName = "Nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "IPAAmm")]
        public IPAAmm IPAAmm { get; set; }
        [XmlElement(ElementName = "IPAAOO")]
        public IPAAOO IPAAOO { get; set; }
        [XmlElement(ElementName = "IPAUOR")]
        public IPAUOR IPAUOR { get; set; }
        [XmlElement(ElementName = "IndirizziDigitaliDiRiferimento")]
        public List<string> IndirizziDigitaliDiRiferimento { get; set; }
    }

    [XmlRoot(ElementName = "PG")]
    public class PG
    {
        [XmlElement(ElementName = "DenominazioneOrganizzazione")]
        public string DenominazioneOrganizzazione { get; set; }
        [XmlElement(ElementName = "CodiceFiscale_PartitaIva")]
        public string CodiceFiscale_PartitaIva { get; set; }
        [XmlElement(ElementName = "IndirizziDigitaliDiRiferimento")]
        public List<string> IndirizziDigitaliDiRiferimento { get; set; }
    }


    [XmlRoot(ElementName = "AmministrazioneCheEffettuaLaRegistrazione")]
    public class AmministrazioneCheEffettuaLaRegistrazione
    {
        [XmlElement(ElementName = "TipoRuolo")]
        public string TipoRuolo { get; set; }
        [XmlElement(ElementName = "PAI")]
        public PAI PAI { get; set; }
    }

    [XmlRoot(ElementName = "Mittente")]
    public class Mittente
    {
        [XmlElement(ElementName = "TipoRuolo")]
        public string TipoRuolo { get; set; }
        [XmlElement(ElementName = "PG")]
        public PG PG { get; set; }
    }

    [XmlRoot(ElementName = "Destinatario")]
    public class Destinatario
    {
        [XmlElement(ElementName = "TipoRuolo")]
        public string TipoRuolo { get; set; }
        [XmlElement(ElementName = "PF")]
        public PF PF { get; set; }

        [XmlElement(ElementName = "PG")]
        public PG PG { get; set; }
    }


    [XmlRoot(ElementName = "Ruolo")]
    public class Ruolo
    {
        [XmlElement(ElementName = "AmministrazioneCheEffettuaLaRegistrazione")]
        public AmministrazioneCheEffettuaLaRegistrazione AmministrazioneCheEffettuaLaRegistrazione { get; set; }

        [XmlElement(ElementName = "Mittente")]
        public Mittente Mittente { get; set; }

        [XmlElement(ElementName = "Destinatario")]
        public Destinatario Destinatario { get; set; }
    }

    [XmlRoot(ElementName = "Soggetti")]
    public class Soggetti
    {
        [XmlElement(ElementName = "Ruolo")]
        public List<Ruolo> Ruolo { get; set; }
    }

    [XmlRoot(ElementName = "ChiaveDescrittiva")]
    public class ChiaveDescrittiva
    {
        [XmlElement(ElementName = "Oggetto")]
        public string Oggetto { get; set; }
        [XmlElement(ElementName = "ParoleChiave")]
        public List<string> ParoleChiave { get; set; }
    }

    [XmlRoot(ElementName = "IndiceAllegati")]
    public class IndiceAllegati
    {
        [XmlElement(ElementName = "IdDoc")]
        public IdDoc IdDoc { get; set; }
        [XmlElement(ElementName = "Descrizione")]
        public string Descrizione { get; set; }
    }

    [XmlRoot(ElementName = "Allegati")]
    public class Allegati
    {
        [XmlElement(ElementName = "NumeroAllegati")]
        public string NumeroAllegati { get; set; }
        [XmlElement(ElementName = "IndiceAllegati")]
        public List<IndiceAllegati> IndiceAllegati { get; set; }
    }

    [XmlRoot(ElementName = "Classificazione")]
    public class Classificazione
    {
        [XmlElement(ElementName = "IndiceDiClassificazione")]
        public string IndiceDiClassificazione { get; set; }
        [XmlElement(ElementName = "Descrizione")]
        public string Descrizione { get; set; }
        [XmlElement(ElementName = "PianoDiClassificazione")]
        public string PianoDiClassificazione { get; set; }
    }

    [XmlRoot(ElementName = "ProdottoSoftware")]
    public class ProdottoSoftware
    {
        [XmlElement(ElementName = "NomeProdotto")]
        public string NomeProdotto { get; set; }
        [XmlElement(ElementName = "VersioneProdotto")]
        public string VersioneProdotto { get; set; }
        [XmlElement(ElementName = "Produttore")]
        public string Produttore { get; set; }
    }

    [XmlRoot(ElementName = "IdentificativoDelFormato")]
    public class IdentificativoDelFormato
    {
        [XmlElement(ElementName = "Formato")]
        public string Formato { get; set; }
        [XmlElement(ElementName = "ProdottoSoftware")]
        public ProdottoSoftware ProdottoSoftware { get; set; }
    }

    [XmlRoot(ElementName = "Verifica")]
    public class Verifica
    {
        [XmlElement(ElementName = "FirmatoDigitalmente")]
        public bool FirmatoDigitalmente { get; set; }
        [XmlElement(ElementName = "SigillatoElettronicamente")]
        public bool SigillatoElettronicamente { get; set; }
        [XmlElement(ElementName = "MarcaturaTemporale")]
        public bool MarcaturaTemporale { get; set; }
        [XmlElement(ElementName = "ConformitaCopieImmagineSuSupportoInformatico")]
        public bool ConformitaCopieImmagineSuSupportoInformatico { get; set; }
    }

    [XmlRoot(ElementName = "TipoAgg")]
    public class TipoAgg
    {
        [XmlElement(ElementName = "TipoAggregazione")]
        public string TipoAggregazione { get; set; }
        [XmlElement(ElementName = "IdAggregazione")]
        public string IdAggregazione { get; set; }
    }

    [XmlRoot(ElementName = "Agg")]
    public class Agg
    {
        [XmlElement(ElementName = "TipoAgg")]
        public List<TipoAgg> TipoAgg { get; set; }
    }

    [XmlRoot(ElementName = "IdIdentificativoDocumentoPrimario")]
    public class IdIdentificativoDocumentoPrimario
    {
        [XmlElement(ElementName = "ImprontaCrittograficaDelDocumento")]
        public ImprontaCrittograficaDelDocumento ImprontaCrittograficaDelDocumento { get; set; }
        [XmlElement(ElementName = "Identificativo")]
        public string Identificativo { get; set; }
        [XmlElement(ElementName = "Segnatura")]
        public string Segnatura { get; set; }
    }

    [XmlRoot(ElementName = "SoggettoAutoreDellaModifica")]
    public class SoggettoAutoreDellaModifica
    {
        [XmlElement(ElementName = "Cognome")]
        public string Cognome { get; set; }
        [XmlElement(ElementName = "Nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "CodiceFiscale")]
        public string CodiceFiscale { get; set; }
        [XmlElement(ElementName = "IPAAmm")]
        public IPAAmm IPAAmm { get; set; }
        [XmlElement(ElementName = "IPAAOO")]
        public IPAAOO IPAAOO { get; set; }
        [XmlElement(ElementName = "IPAUOR")]
        public IPAUOR IPAUOR { get; set; }
        [XmlElement(ElementName = "IndirizziDigitaliDiRiferimento")]
        public List<string> IndirizziDigitaliDiRiferimento { get; set; }
    }

    [XmlRoot(ElementName = "IdDocVersionePrecedente")]
    public class IdDocVersionePrecedente
    {
        [XmlElement(ElementName = "ImprontaCrittograficaDelDocumento")]
        public ImprontaCrittograficaDelDocumento ImprontaCrittograficaDelDocumento { get; set; }
        [XmlElement(ElementName = "Identificativo")]
        public string Identificativo { get; set; }
        [XmlElement(ElementName = "Segnatura")]
        public string Segnatura { get; set; }
    }

    [XmlRoot(ElementName = "TracciatureModificheDocumento")]
    public class TracciatureModificheDocumento
    {
        [XmlElement(ElementName = "TipoModifica")]
        public string TipoModifica { get; set; }
        [XmlElement(ElementName = "SoggettoAutoreDellaModifica")]
        public SoggettoAutoreDellaModifica SoggettoAutoreDellaModifica { get; set; }
        [XmlElement(ElementName = "DataModifica")]
        public string DataModifica { get; set; }
        [XmlElement(ElementName = "OraModifica")]
        public string OraModifica { get; set; }
        [XmlElement(ElementName = "IdDocVersionePrecedente")]
        public IdDocVersionePrecedente IdDocVersionePrecedente { get; set; }
    }


}

