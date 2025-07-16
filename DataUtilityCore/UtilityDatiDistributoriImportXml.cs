 using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DataLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace DataUtilityCore
{
    public class UtilityDatiDistributoriImportXml
    {
        public static UteXmlComunicazione LeggiFileXML(string path)
        {
            using (var reader = new XmlTextReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UteXmlComunicazione));

                UteXmlComunicazione xmlobj = (UteXmlComunicazione)serializer.Deserialize(reader);

                return xmlobj;
            }
        }

        public static UteXmlComunicazione LeggiStreamXML(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UteXmlComunicazione));

            UteXmlComunicazione xmlobj = (UteXmlComunicazione)serializer.Deserialize(stream);

            return xmlobj;
        }

        public static bool ImportComunicazioneUtenzeEnergetiche(Stream stream, object iDSoggetto, string fileName, int annoRiferimento, out List<string> errors)
        {
            errors = new List<string>();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UteXmlComunicazione));
                UteXmlComunicazione xmlobj = (UteXmlComunicazione)serializer.Deserialize(stream);

                return ImportComunicazioneUtenzeEnergetiche(xmlobj, iDSoggetto, fileName, annoRiferimento, out errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.InnerException.Message);
            }
            return false;
        }

        public static bool ImportComunicazioneUtenzeEnergetiche(string fileContent, object iDSoggetto, string fileName, int annoRiferimento, out List<string> errors)
        {
            errors = new List<string>();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UteXmlComunicazione));
                UteXmlComunicazione xmlobj;
                using (TextReader reader = new StringReader(fileContent))
                {
                    xmlobj = (UteXmlComunicazione)serializer.Deserialize(reader);
                }
                return ImportComunicazioneUtenzeEnergetiche(xmlobj, iDSoggetto, fileName, annoRiferimento, out errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            return false;
        }

        public static bool ImportComunicazioneUtenzeEnergetiche(UteXmlComunicazione xmlobj, object iDSoggetto, string fileName, int annoRiferimento, out List<string> errors)
        {
            errors = new List<string>();
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    using (var dbContextTransaction = ctx.Database.BeginTransaction())
                    {
                        var comunicazione = new DataLayer.UTE_Comunicazioni();
                        //comunicazione.IdImportazione = idImportazione; //leggo all'importazione
                        ctx.UTE_Comunicazioni.Add(comunicazione);
                        comunicazione.DataOraElaborazione = DateTime.Now;
                        comunicazione.IDSoggetto = int.Parse(iDSoggetto.ToString());
                        comunicazione.NomeFile = fileName;
                        comunicazione.NumeroUtenze = xmlobj.UteXmlDatiFornituraCliente.Count();
                        comunicazione.AnnoRiferimento = annoRiferimento;
                        int countOk = 0;
                        foreach (var xmlDatiFornitura in xmlobj.UteXmlDatiFornituraCliente)
                        {
                            //Controllo che l'anno di riferimento selezionato sia uguale all'anno del file xml
                            if (xmlDatiFornitura.UteXmlDatiFornitura.UteXmlPeriodoFornitura.annoRiferimento == annoRiferimento)
                            {
                                countOk++;
                                var datiFornitura = new DataLayer.UTE_DatiFornituraCliente();
                                ctx.UTE_DatiFornituraCliente.Add(datiFornitura);
                                datiFornitura.UTE_Comunicazioni = comunicazione;

                                //Dati Cliente
                                if (xmlDatiFornitura.UteXmlDatiCliente.pfPg.ToString() == "PF")
                                {
                                    datiFornitura.PfPg = 0;
                                }
                                else if (xmlDatiFornitura.UteXmlDatiCliente.pfPg.ToString() == "PG")
                                {
                                    datiFornitura.PfPg = 1;
                                }
                                //datiFornitura.PfPg = int.Parse(xmlDatiFornitura.UteXmlDatiCliente.pfPg.ToString());
                                datiFornitura.CfPiva = xmlDatiFornitura.UteXmlDatiCliente.cfPiva;
                                datiFornitura.Cognome = xmlDatiFornitura.UteXmlDatiCliente.cognome;
                                datiFornitura.Nome = xmlDatiFornitura.UteXmlDatiCliente.nome;

                                //Dati Fornitura

                                //Periodo Fornitura
                                datiFornitura.AnnoRiferimento = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlPeriodoFornitura.annoRiferimento;

                                //Update xml 18-12-2017 
                                //datiFornitura.NumeroMesiFatturazione = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlPeriodoFornitura.numeroMesiFatturazione;

                                //Localizzazione Fornitura
                                datiFornitura.Cap = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.UteXmlUbicazione.cap;
                                datiFornitura.Civico = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.UteXmlUbicazione.civico;
                                datiFornitura.CodiceISTATComune = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.UteXmlUbicazione.codiceISTATComune;
                                datiFornitura.Indirizzo = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.UteXmlUbicazione.indirizzo;
                                datiFornitura.Toponimo = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.UteXmlUbicazione.toponimo;

                                //if (xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.CodiceAssenzaDatiCatastali != null)
                                //{
                                    if (xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.CodiceAssenzaDatiCatastali.HasValue)
                                    {
                                        datiFornitura.CodiceAssenzaDatiCatastali = (int?)xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura.CodiceAssenzaDatiCatastali;
                                    }
                                    else
                                    {
                                        //carico i dati catastali
                                        foreach (
                                            var xmlDatoCatastale in
                                                xmlDatiFornitura.UteXmlDatiFornitura.UteXmlLocalizzazioneFornitura
                                                    .EstremiCatastali)
                                        {
                                            var datoCatastale = new DataLayer.UTE_DatiCatastali();
                                            ctx.UTE_DatiCatastali.Add(datoCatastale);

                                            datoCatastale.UTE_DatiFornituraCliente = datiFornitura;

                                            datoCatastale.Foglio = xmlDatoCatastale.foglio;
                                            datoCatastale.Mappale = xmlDatoCatastale.particella;
                                            datoCatastale.Subalterno = xmlDatoCatastale.subalterno;
                                            datoCatastale.Sezione = xmlDatoCatastale.sezione;

                                        }
                                    }
                                //}
                                
                                //Contratto fornitura
                                datiFornitura.CategoriaUtilizzo = (int)xmlDatiFornitura.UteXmlDatiFornitura.UteXmlContrattoFornitura.categoriaUtilizzo;
                                datiFornitura.CodicePdrPod = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlContrattoFornitura.codicePdrPod;
                                datiFornitura.Combustibile = (int)xmlDatiFornitura.UteXmlDatiFornitura.UteXmlContrattoFornitura.combustibile;
                                datiFornitura.TipoContratto = (int)xmlDatiFornitura.UteXmlDatiFornitura.UteXmlContrattoFornitura.tipoContratto;
                                
                                //Consumi Fornitura
                                datiFornitura.ConsumoAnnuo = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlConsumiFornitura.consumoAnnuo;
                                datiFornitura.UnitaMisuraConsumo = (int)xmlDatiFornitura.UteXmlDatiFornitura.UteXmlConsumiFornitura.unitaMisuraConsumo;

                                // Update xml 18-12-2017
                                //datiFornitura.VolumetriaRiscaldata = xmlDatiFornitura.UteXmlDatiFornitura.UteXmlConsumiFornitura.volumetriaRiscaldata;
                            }
                        }
                        if (countOk > 0)
                        {
                            ctx.SaveChanges();
                            dbContextTransaction.Commit();
                            return true;
                        }
                        else
                        {
                            errors.Add("Nessuna utenza importata perchè l'anno di riferimento selezionato non risulta uguale a nessun anno di riferimento");
                            return false;
                        }             
                    }
                }
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                foreach (var error in dbEntityValidationException.EntityValidationErrors)
                {
                    foreach (var dbValidationError in error.ValidationErrors)
                    {
                        errors.Add(dbValidationError.ErrorMessage);
                        //Debug.WriteLine(dbValidationError.PropertyName + " " + dbValidationError.ErrorMessage);
                    }
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                errors.Add(dbUpdateException.Message);
                var innerEx = dbUpdateException.InnerException;
                while (innerEx != null)
                {
                    errors.Add(innerEx.Message);
                    innerEx = innerEx.InnerException;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            return false;
        }

        public static void DeleteDatiDistributori(int iD)
        {
            using (var ctx = new CriterDataModel())
            {
                var DistributoriCombustibile = ctx.UTE_Comunicazioni.Where(c => c.Id == iD).FirstOrDefault();

                ctx.UTE_Comunicazioni.Remove(DistributoriCombustibile);

                //ctx.SaveChanges();

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    public class UTE_Comunicazioni
    {
        [Key]
        public int Id { get; set; }

        public int IDSoggetto { get; set; }

        public string NomeFile { get; set; }

        public DateTime DataOraElaborazione { get; set; }

        public int AnnoRiferimento { get; set; }

        ////Messaggio errore in caso di esito negativo
        //public string MessaggioEsito { get; set; }

        ////Se l'importazione è stata eseguita correttamente
        //public bool EsitoPositivo { get; set; }

        //public int IdImportazione { get; set; }

        //[ForeignKey("IdImportazione")]
        //public UTE_Importazioni Importazione { get; set; }

        public ICollection<UTE_DatiFornituraCliente> DatiFornituraClienti { get; set; }
    }

    public class UTE_DatiCatastali
    {
        [Key]
        public long Id { get; set; }

        public long IdDatiFornituraCliente { get; set; }

        [ForeignKey("IdDatiFornituraCliente")]
        public UTE_DatiFornituraCliente DatiFornituraCliente { get; set; }

        [StringLength(50)]
        public string Sezione { get; set; }

        [StringLength(50)]
        public string Foglio { get; set; }

        [StringLength(50)]
        public string Mappale { get; set; }

        [StringLength(50)]
        public string Subalterno { get; set; }

        //[StringLength(50)]
        //public string Identificativo { get; set; }
    }

    public class UTE_DatiFornituraCliente
    {
        [Key]
        public long Id { get; set; }

        public int IdComunicazione { get; set; }

        [ForeignKey("IdComunicazione")]

        public UTE_Comunicazioni Comunicazione { get; set; }

        #region Dati Cliente

        public UteXmlDatiClientePfPg PfPg { get; set; }

        public string Cognome { get; set; }

        public string Nome { get; set; }

        /// <remarks/>
        public string CfPiva { get; set; }

        #endregion

        #region Dati Fornitura

        #region Periodo Fornitura

        public int AnnoRiferimento { get; set; }

        // Update xml 18-12-2017
        //public int NumeroMesiFatturazione { get; set; }

        #endregion

        #region Localizzazione Fornitura

        public string Toponimo { get; set; }

        public string Indirizzo { get; set; }

        public string Civico { get; set; }

        public string Cap { get; set; }

        public string CodiceISTATComune { get; set; }

        public ICollection<UTE_DatiCatastali> DatiCatastali { get; set; }

        public UteXmlLocalizzazioneFornituraCodiceAssenzaDatiCatastali? CodiceAssenzaDatiCatastali { get; set; }

        #endregion

        #region Contratto Fornitura

        public string CodicePdrPod { get; set; }

        /// <remarks/>
        public UteXmlContrattoFornituraTipoContratto TipoContratto { get; set; }

        /// <remarks/>
        public UteXmlContrattoFornituraCategoriaUtilizzo CategoriaUtilizzo { get; set; }

        /// <remarks/>
        public UteXmlContrattoFornituraCombustibile Combustibile { get; set; }

        #endregion

        #region Consumi Fornitura

        public decimal ConsumoAnnuo { get; set; }

        public UteXmlConsumiFornituraUnitaMisuraConsumo UnitaMisuraConsumo { get; set; }

       // Update xml 18-12-2017
       // public System.Nullable<decimal> VolumetriaRiscaldata { get; set; }

        #endregion

        #endregion
    }
    #region Importazioni
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlComunicazione
    {
        private UteXmlDatiFornituraCliente[] _UteXmlDatiFornituraClienteField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("datiFornituraCliente")]
        public UteXmlDatiFornituraCliente[] UteXmlDatiFornituraCliente
        {
            get { return this._UteXmlDatiFornituraClienteField; }
            set { this._UteXmlDatiFornituraClienteField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlDatiFornituraCliente
    {
        private UteXmlDatiCliente _UteXmlDatiClienteField;

        private UteXmlDatiFornitura _UteXmlDatiFornituraField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("datiCliente")]
        public UteXmlDatiCliente UteXmlDatiCliente
        {
            get { return this._UteXmlDatiClienteField; }
            set { this._UteXmlDatiClienteField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("datiFornitura")]
        public UteXmlDatiFornitura UteXmlDatiFornitura
        {
            get { return this._UteXmlDatiFornituraField; }
            set { this._UteXmlDatiFornituraField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlDatiCliente
    {
        private UteXmlDatiClientePfPg pfPgField;

        private string cognomeField;

        private string nomeField;

        private string cfPivaField;

        /// <remarks/>
        public UteXmlDatiClientePfPg pfPg
        {
            get { return this.pfPgField; }
            set { this.pfPgField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string cognome
        {
            get { return this.cognomeField; }
            set { this.cognomeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string nome
        {
            get { return this.nomeField; }
            set { this.nomeField = value; }
        }

        /// <remarks/>
        public string cfPiva
        {
            get { return this.cfPivaField; }
            set { this.cfPivaField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    public enum UteXmlDatiClientePfPg
    {
        // <remarks/>
        PF,
        // <remarks/>
        PG,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlDatiFornitura
    {
        private UteXmlPeriodoFornitura _uteXmlPeriodoFornituraField;

        private UteXmlLocalizzazioneFornitura _uteXmlLocalizzazioneFornituraField;

        private UteXmlContrattoFornitura _uteXmlContrattoFornituraField;

        private UteXmlConsumiFornitura _uteXmlConsumiFornituraField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("periodoFornitura")]
        public UteXmlPeriodoFornitura UteXmlPeriodoFornitura
        {
            get { return this._uteXmlPeriodoFornituraField; }
            set { this._uteXmlPeriodoFornituraField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("localizzazioneFornitura")]

        public UteXmlLocalizzazioneFornitura UteXmlLocalizzazioneFornitura
        {
            get { return this._uteXmlLocalizzazioneFornituraField; }
            set { this._uteXmlLocalizzazioneFornituraField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contrattoFornitura")]
        public UteXmlContrattoFornitura UteXmlContrattoFornitura
        {
            get { return this._uteXmlContrattoFornituraField; }
            set { this._uteXmlContrattoFornituraField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("consumiFornitura")]
        public UteXmlConsumiFornitura UteXmlConsumiFornitura
        {
            get { return this._uteXmlConsumiFornituraField; }
            set { this._uteXmlConsumiFornituraField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlPeriodoFornitura
    {
        private int annoRiferimentoField;

        // Update xml 18-12-2017
        //private int numeroMesiFatturazioneField;

        /// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute(DataType = "gYear")]
        public int annoRiferimento
        {
            get { return this.annoRiferimentoField; }
            set { this.annoRiferimentoField = value; }
        }

        /// <remarks/>
        //
        //  Update xml 18-12-2017 
        //
        //[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        //public int numeroMesiFatturazione
        //{
        //    get { return this.numeroMesiFatturazioneField; }
        //    set { this.numeroMesiFatturazioneField = value; }
        //}
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlLocalizzazioneFornitura
    {
        private UteXmlUbicazione _uteXmlUbicazioneField;
        private UteXmlEstremiCatastali[] _estremiCatastali;
        private UteXmlLocalizzazioneFornituraCodiceAssenzaDatiCatastali? _codiceAssenzaDatiCatastali;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ubicazione")]

        public UteXmlUbicazione UteXmlUbicazione
        {
            get { return this._uteXmlUbicazioneField; }
            set { this._uteXmlUbicazioneField = value; }
        }

        /// <remarks/>

        [System.Xml.Serialization.XmlElementAttribute("estremiCatastali", typeof(UteXmlEstremiCatastali))]
        public UteXmlEstremiCatastali[] EstremiCatastali
        {
            get { return this._estremiCatastali; }
            set { this._estremiCatastali = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute("codiceAssenzaDatiCatastali",
            typeof(UteXmlLocalizzazioneFornituraCodiceAssenzaDatiCatastali))]
        public UteXmlLocalizzazioneFornituraCodiceAssenzaDatiCatastali? CodiceAssenzaDatiCatastali
        {
            get { return this._codiceAssenzaDatiCatastali; }
            set { this._codiceAssenzaDatiCatastali = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlUbicazione
    {
        private string toponimoField;

        private string indirizzoField;

        private string civicoField;

        private string capField;

        private string codiceISTATComuneField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string toponimo
        {
            get { return this.toponimoField; }
            set { this.toponimoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string indirizzo
        {
            get { return this.indirizzoField; }
            set { this.indirizzoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string civico
        {
            get { return this.civicoField; }
            set { this.civicoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string cap
        {
            get { return this.capField; }
            set { this.capField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string codiceISTATComune
        {
            get { return this.codiceISTATComuneField; }
            set { this.codiceISTATComuneField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    public enum UteXmlLocalizzazioneFornituraCodiceAssenzaDatiCatastali
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlEstremiCatastali
    {
        private string sezioneField;

        private string foglioField;

        private string particellaField;

        private string subalternoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string sezione
        {
            get { return this.sezioneField; }
            set { this.sezioneField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string foglio
        {
            get { return this.foglioField; }
            set { this.foglioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string particella
        {
            get { return this.particellaField; }
            set { this.particellaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string subalterno
        {
            get { return this.subalternoField; }
            set { this.subalternoField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlContrattoFornitura
    {
        private string codicePdrPodField;

        private UteXmlContrattoFornituraTipoContratto tipoContrattoField;

        private UteXmlContrattoFornituraCategoriaUtilizzo categoriaUtilizzoField;

        private UteXmlContrattoFornituraCombustibile combustibileField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string codicePdrPod
        {
            get { return this.codicePdrPodField; }
            set { this.codicePdrPodField = value; }
        }

        /// <remarks/>
        public UteXmlContrattoFornituraTipoContratto tipoContratto
        {
            get { return this.tipoContrattoField; }
            set { this.tipoContrattoField = value; }
        }

        /// <remarks/>
        public UteXmlContrattoFornituraCategoriaUtilizzo categoriaUtilizzo
        {
            get { return this.categoriaUtilizzoField; }
            set { this.categoriaUtilizzoField = value; }
        }

        /// <remarks/>
        public UteXmlContrattoFornituraCombustibile combustibile
        {
            get { return this.combustibileField; }
            set { this.combustibileField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    public enum UteXmlContrattoFornituraTipoContratto
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Domestico = 0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Condominiale = 1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        AltriUtilizzi = 2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        ServizioPubblico = 3,

        ///// <remarks/>
        //[System.Xml.Serialization.XmlEnumAttribute("4")] Item4,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    public enum UteXmlContrattoFornituraCategoriaUtilizzo
    {
        /// <remarks/>
        C1 = 1,

        /// <remarks/>
        C2 = 2,

        /// <remarks/>
        C3 = 3,

        /// <remarks/>
        C4 = 4,

        /// <remarks/>
        C5 = 5,

        /// <remarks/>
        C6 = 6,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    public enum UteXmlContrattoFornituraCombustibile
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        GasNaturale = 1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Gpl = 2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Gasolio = 3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        OlioCombustibile = 4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Pellet = 5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Tronchetti = 6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Cippato = 7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        Carbone = 8,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        AltraBiomassaSolida = 9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        BiomassaLiquida = 10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        BiomassaGassosa = 11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        EnergiaTermica = 12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        EnergiaElettrica = 13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        ALtro = 99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    public partial class UteXmlConsumiFornitura
    {
        private decimal consumoAnnuoField;

        //private UteXmlConsumoMensile _uteXmlConsumoMensileField;

        //private UteXmlConsumoGiornaliero _uteXmlConsumoGiornalieroField;

        private UteXmlConsumiFornituraUnitaMisuraConsumo unitaMisuraConsumoField;


        //private System.Nullable<decimal> volumetriaRiscaldataField;
        //private bool volumetriaRiscaldataFieldSpecified;

        /// <remarks/>
        public decimal consumoAnnuo
        {
            get { return this.consumoAnnuoField; }
            set { this.consumoAnnuoField = value; }
        }

        //SIMATICA: su criter non interessa il consumo mensile e giornaliero, ma solo annuale
        ///// <remarks/>
        //public UteXmlConsumoMensile UteXmlConsumoMensile
        //{
        //    get { return this._uteXmlConsumoMensileField; }
        //    set { this._uteXmlConsumoMensileField = value; }
        //}

        ///// <remarks/>
        //public UteXmlConsumoGiornaliero UteXmlConsumoGiornaliero
        //{
        //    get { return this._uteXmlConsumoGiornalieroField; }
        //    set { this._uteXmlConsumoGiornalieroField = value; }
        //}

        /// <remarks/>
        public UteXmlConsumiFornituraUnitaMisuraConsumo unitaMisuraConsumo
        {
            get { return this.unitaMisuraConsumoField; }
            set { this.unitaMisuraConsumoField = value; }
        }

        /// <remarks/>
        // 
        // Start Update xml 18-12-2017
        //
        //[System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        //public System.Nullable<decimal> volumetriaRiscaldata
        //{
        //    get { return this.volumetriaRiscaldataField; }
        //    set { this.volumetriaRiscaldataField = value; }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool volumetriaRiscaldataSpecified
        //{
        //    get { return this.volumetriaRiscaldataFieldSpecified; }
        //    set { this.volumetriaRiscaldataFieldSpecified = value; }
        //}
        //
        // End Update xml 18-12-2017
    }

    ///// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    //    Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    //[System.Xml.Serialization.XmlRootAttribute(
    //    Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    //public partial class UteXmlConsumoMensile
    //{

    //    private decimal consumoMensile1Field;

    //    private string meseRiferimentoField;

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlElementAttribute("UteXmlConsumoMensile")]
    //    public decimal consumoMensile1
    //    {
    //        get { return this.consumoMensile1Field; }
    //        set { this.consumoMensile1Field = value; }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    //    public string meseRiferimento
    //    {
    //        get { return this.meseRiferimentoField; }
    //        set { this.meseRiferimentoField = value; }
    //    }
    //}

    ///// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    //    Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    //[System.Xml.Serialization.XmlRootAttribute(
    //    Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data", IsNullable = false)]
    //public partial class UteXmlConsumoGiornaliero
    //{

    //    private decimal consumoGiornaliero1Field;

    //    private System.DateTime giornoRiferimentoField;

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlElementAttribute("UteXmlConsumoGiornaliero")]
    //    public decimal consumoGiornaliero1
    //    {
    //        get { return this.consumoGiornaliero1Field; }
    //        set { this.consumoGiornaliero1Field = value; }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    //    public System.DateTime giornoRiferimento
    //    {
    //        get { return this.giornoRiferimentoField; }
    //        set { this.giornoRiferimentoField = value; }
    //    }
    //}

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.csi.it/sigit/sigitwebn/xml/importmassivo/utenzedistributori/data")]
    public enum UteXmlConsumiFornituraUnitaMisuraConsumo
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Litri = 1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Kg,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        M3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        KWh,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        NonDefinito,
    }
    #endregion
}