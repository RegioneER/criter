using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using System.Data.Entity.Spatial;
using System.Configuration;

namespace DataUtilityCore
{
    public class UtilitySoggetti
    {
        public static int SaveInsertDeleteDatiSoggetti(
                string operationType,
                int? iDSoggetto,
                string codiceSoggetto,
                int? iDSoggettoDerived,
                int? iDTipoSoggetto,
                string nomeAzienda,
                int? iDFormaGiuridica,
                int? iDPaeseSedeLegale,
                string indirizzoSedeLegale,
                string capSedeLegale,
                string cittaSedeLegale,
                string numeroCivicoSedeLegale,
                int? iDProvinciaSedeLegale,
                int? iDTitoloSoggetto,
                int? iDFunzioneSoggetto,
                string nome,
                string cognome,
                int? iDPaeseNascita,
                DateTime? dataNascita,
                string cittaNascita,
                int? iDProvinciaNascita,
                int? iDPaeseResidenza,
                string cittaResidenza,
                int? iDProvinciaResidenza,
                string capResidenza,
                string indirizzoResidenza,
                string NumeroCivicoResidenza,
                string codicefiscale,
                string telefono,
                string fax,
                string email,
                string emailPec,
                string partitaIva,
                string codiceFiscaleAzienda,
                string sitoWeb,
                string numeroIscrizioneAlboImprese,
                int? iDProvinciaIscrizioneAlboImprese,
                bool? fIscrizioneRegistroGasFluorurati,
                string numeroIscrizioneRegistroGasFluorurati,
                DateTime? dataIscrizione,
                bool? fPubblicazioneAlbo,
                bool? fPrivacy,
                int? iDUtenteInserimento,
                DateTime? dataInserimento,
                int? iDUtenteUltimaModifica,
                DateTime? dataUltimaModifica,
                bool? fAttivo,
                object[] coordinate,
                bool? fIscrizione,

                bool? fDomicilioUgualeResidenza,
                int? iDPaeseDomicilio,
                string CittaDomicilio,
                int? IDProvinciaDomicilio,
                string CapDomicilio,
                string IndirizzoDomicilio,
                string NumeroCivicoDomicilio,
                string Cellulare,
                string TelefonoOrganizzazione,
                string EmailOrganizzazione,
                string EmailPecOrganizzazione,
                string PartitaIVAOrganizzazione,
                int? IDTipologiaQualificaIspettore,
                bool? fIdoneoIspezione,
                bool? fCondanna,
                int? IDTipologiaTitoloStudio,
                string TitoloStudioConseguitoPresso,
                DateTime? DataTitoloStudio,
                string StageAzienda,
                DateTime? DataStageAziendaDal,
                DateTime? DataStageAziendaAl,
                string CorsoFormazione,
                string CorsoFormazioneOrganizzatore,
                bool? fRequisitoOrganizzativo,
                bool? fDisponibilitaApparecchiature,
                bool? fDisponibilitaControlli,
                bool? fOrganizzazioneEsterna,
                string OrganismoIspezioneNumero,
                string OrganismoIspezione,
                bool? fConsensoRequisitiDichiarati,
                //int? IDStatoAccreditamento,
                //int? IDStatoSottoAccreditamento,

                int? IDTipologiaOrdineCollegio,
                int? IDProvinciaOrdineCollegio,
                string SezioneOrdineCollegio,
                string NumeroOrdineCollegio,
                DateTime? DataOrdineCollegio
            )
        {
            int? IDSoggettoInsert = null;

            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var soggetto = new COM_AnagraficaSoggetti();

            if (operationType == "delete")
            {
                soggetto = db.COM_AnagraficaSoggetti.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);
                soggetto.fAttivo = true;
            }
            else
            {
                if (operationType == "update")
                {
                    soggetto = db.COM_AnagraficaSoggetti.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);
                }

                if (!string.IsNullOrEmpty(codiceSoggetto))
                {
                    soggetto.CodiceSoggetto = codiceSoggetto;
                }
                else
                {
                    soggetto.CodiceSoggetto = null;
                }
                if (!string.IsNullOrEmpty(iDSoggettoDerived.ToString()))
                {
                    soggetto.IDSoggettoDerived = iDSoggettoDerived;
                }
                else
                {
                    soggetto.IDSoggettoDerived = null;
                }
                if (!string.IsNullOrEmpty(iDTipoSoggetto.ToString()))
                {
                    soggetto.IDTipoSoggetto = iDTipoSoggetto;
                }
                else
                {
                    soggetto.IDTipoSoggetto = null;
                }
                if (!string.IsNullOrEmpty(nomeAzienda))
                {
                    soggetto.NomeAzienda = nomeAzienda;
                }
                else
                {
                    soggetto.NomeAzienda = null;
                }
                if (!string.IsNullOrEmpty(iDFormaGiuridica.ToString()))
                {
                    soggetto.IDFormaGiuridica = iDFormaGiuridica;
                }
                else
                {
                    soggetto.IDFormaGiuridica = null;
                }
                if ((!string.IsNullOrEmpty(iDPaeseSedeLegale.ToString())) && (iDPaeseSedeLegale.ToString() != "0"))
                {
                    soggetto.IDPaeseSedeLegale = iDPaeseSedeLegale;
                }
                else
                {
                    soggetto.IDPaeseSedeLegale = null;
                }
                if (!string.IsNullOrEmpty(indirizzoSedeLegale))
                {
                    soggetto.IndirizzoSedeLegale = indirizzoSedeLegale;
                }
                else
                {
                    soggetto.IndirizzoSedeLegale = null;
                }
                if (!string.IsNullOrEmpty(capSedeLegale))
                {
                    soggetto.CapSedeLegale = capSedeLegale;
                }
                else
                {
                    soggetto.CapSedeLegale = null;
                }
                if (!string.IsNullOrEmpty(cittaSedeLegale))
                {
                    soggetto.CittaSedeLegale = cittaSedeLegale;
                }
                else
                {
                    soggetto.CittaSedeLegale = null;
                }
                if (!string.IsNullOrEmpty(numeroCivicoSedeLegale))
                {
                    soggetto.NumeroCivicoSedeLegale = numeroCivicoSedeLegale;
                }
                else
                {
                    soggetto.NumeroCivicoSedeLegale = null;
                }
                if ((!string.IsNullOrEmpty(iDProvinciaSedeLegale.ToString())) && (iDProvinciaSedeLegale.ToString() != "0"))
                {
                    soggetto.IDProvinciaSedeLegale = iDProvinciaSedeLegale;
                }
                else
                {
                    soggetto.IDProvinciaSedeLegale = null;
                }
                if ((!string.IsNullOrEmpty(iDTitoloSoggetto.ToString())) && ((iDTitoloSoggetto.ToString() != "0")))
                {
                    soggetto.IDTitoloSoggetto = iDTitoloSoggetto;
                }
                else
                {
                    soggetto.IDTitoloSoggetto = null;
                }
                if ((!string.IsNullOrEmpty(iDFunzioneSoggetto.ToString())) && ((iDFunzioneSoggetto.ToString() != "0")))
                {
                    soggetto.IDFunzioneSoggetto = iDFunzioneSoggetto;
                }
                else
                {
                    soggetto.IDFunzioneSoggetto = null;
                }
                if (!string.IsNullOrEmpty(nome))
                {
                    soggetto.Nome = nome;
                }
                else
                {
                    soggetto.Nome = null;
                }
                if (!string.IsNullOrEmpty(cognome))
                {
                    soggetto.Cognome = cognome;
                }
                else
                {
                    soggetto.Cognome = null;
                }
                if (!string.IsNullOrEmpty(iDPaeseNascita.ToString()))
                {
                    soggetto.IDPaeseNascita = iDPaeseNascita;
                }
                else
                {
                    soggetto.IDPaeseNascita = null;
                }
                if (dataNascita != null)
                {
                    soggetto.DataNascita = (DateTime)dataNascita;
                }
                else
                {
                    soggetto.DataNascita = null;
                }
                if (!string.IsNullOrEmpty(cittaNascita))
                {
                    soggetto.CittaNascita = cittaNascita;
                }
                else
                {
                    soggetto.CittaNascita = null;
                }
                if (!string.IsNullOrEmpty(iDProvinciaNascita.ToString()) && (iDProvinciaNascita.ToString() != "0"))
                {
                    soggetto.IDProvinciaNascita = iDProvinciaNascita;
                }
                else
                {
                    soggetto.IDProvinciaNascita = null;
                }

                if ((!string.IsNullOrEmpty(iDPaeseResidenza.ToString())) && (iDPaeseResidenza.ToString() != "0"))
                {
                    soggetto.IDPaeseResidenza = iDPaeseResidenza;
                }
                else
                {
                    soggetto.IDPaeseResidenza = null;
                }

                if (!string.IsNullOrEmpty(cittaResidenza))
                {
                    soggetto.CittaResidenza = cittaResidenza;
                }
                else
                {
                    soggetto.CittaResidenza = null;
                }
                if ((!string.IsNullOrEmpty(iDProvinciaResidenza.ToString())) && (iDProvinciaResidenza.ToString() != "0"))
                {
                    soggetto.IDProvinciaResidenza = iDProvinciaResidenza;
                }
                else
                {
                    soggetto.IDProvinciaResidenza = null;
                }
                if (!string.IsNullOrEmpty(capResidenza))
                {
                    soggetto.CapResidenza = capResidenza;
                }
                else
                {
                    soggetto.CapResidenza = null;
                }
                if (!string.IsNullOrEmpty(indirizzoResidenza))
                {
                    soggetto.IndirizzoResidenza = indirizzoResidenza;
                }
                else
                {
                    soggetto.IndirizzoResidenza = null;
                }
                if (!string.IsNullOrEmpty(NumeroCivicoResidenza))
                {
                    soggetto.NumeroCivicoResidenza = NumeroCivicoResidenza;
                }
                else
                {
                    soggetto.NumeroCivicoResidenza = null;
                }
                if (!string.IsNullOrEmpty(codicefiscale))
                {
                    soggetto.CodiceFiscale = codicefiscale;
                }
                else
                {
                    soggetto.CodiceFiscale = null;
                }
                if (!string.IsNullOrEmpty(telefono))
                {
                    soggetto.Telefono = telefono;
                }
                else
                {
                    soggetto.Telefono = null;
                }
                if (!string.IsNullOrEmpty(fax))
                {
                    soggetto.Fax = fax;
                }
                else
                {
                    soggetto.Fax = null;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    soggetto.Email = email;
                }
                else
                {
                    soggetto.Email = null;
                }
                if (!string.IsNullOrEmpty(emailPec))
                {
                    soggetto.EmailPec = emailPec;
                }
                else
                {
                    soggetto.EmailPec = null;
                }
                if (!string.IsNullOrEmpty(partitaIva))
                {
                    soggetto.PartitaIVA = partitaIva;
                }
                else
                {
                    soggetto.PartitaIVA = null;
                }
                if (!string.IsNullOrEmpty(codiceFiscaleAzienda))
                {
                    soggetto.CodiceFiscaleAzienda = codiceFiscaleAzienda;
                }
                else
                {
                    soggetto.CodiceFiscaleAzienda = null;
                }
                if (!string.IsNullOrEmpty(sitoWeb))
                {
                    soggetto.SitoWeb = sitoWeb;
                }
                else
                {
                    soggetto.SitoWeb = null;
                }
                if (!string.IsNullOrEmpty(numeroIscrizioneAlboImprese))
                {
                    soggetto.NumeroIscrizioneAlboImprese = numeroIscrizioneAlboImprese;
                }
                else
                {
                    soggetto.NumeroIscrizioneAlboImprese = null;
                }
                if (!string.IsNullOrEmpty(iDProvinciaIscrizioneAlboImprese.ToString()))
                {
                    soggetto.IDProvinciaIscrizioneAlboImprese = iDProvinciaIscrizioneAlboImprese;
                }
                else
                {
                    soggetto.IDProvinciaIscrizioneAlboImprese = null;
                }
                if (fIscrizioneRegistroGasFluorurati != null)
                {
                    soggetto.fIscrizioneRegistroGasFluorurati = (bool)fIscrizioneRegistroGasFluorurati;
                }
                if (!string.IsNullOrEmpty(numeroIscrizioneRegistroGasFluorurati))
                {
                    soggetto.NumeroIscrizioneRegistroGasFluorurati = numeroIscrizioneRegistroGasFluorurati;
                }
                else
                {
                    soggetto.NumeroIscrizioneRegistroGasFluorurati = null;
                }
                if (dataIscrizione != null)
                {
                    soggetto.DataIscrizione = (DateTime)dataIscrizione;
                }
                else
                {
                    soggetto.DataIscrizione = null;
                }
                if (fPubblicazioneAlbo != null)
                {
                    soggetto.fPubblicazioneAlbo = (bool)fPubblicazioneAlbo;
                }
                if (fPrivacy != null)
                {
                    soggetto.fPrivacy = (bool)fPrivacy;
                }
                if (!string.IsNullOrEmpty(iDUtenteInserimento.ToString()))
                {
                    soggetto.IDUtenteInserimento = iDUtenteInserimento;
                }
                else
                {
                    soggetto.IDUtenteInserimento = null;
                }
                if (dataInserimento != null)
                {
                    soggetto.DataInserimento = (DateTime)dataInserimento;
                }
                else
                {
                    soggetto.DataInserimento = null;
                }
                if (!string.IsNullOrEmpty(iDUtenteUltimaModifica.ToString()))
                {
                    soggetto.IDUtenteUltimaModifica = iDUtenteUltimaModifica;
                }
                else
                {
                    soggetto.IDUtenteUltimaModifica = null;
                }
                if (dataUltimaModifica != null)
                {
                    soggetto.DataUltimaModifica = (DateTime)dataUltimaModifica;
                }
                else
                {
                    soggetto.DataUltimaModifica = null;
                }
                if (fAttivo != null)
                {
                    soggetto.fAttivo = (bool)fAttivo;
                }
                if (coordinate != null)
                {
                    if (coordinate[0] != null && coordinate[1] != null)
                    {
                        soggetto.Coordinate = DbGeography.PointFromText(string.Format("POINT({0} {1})", coordinate[1].ToString().Replace(",", "."), coordinate[0].ToString().Replace(",", ".")), 4326);
                    }
                }
                else
                {
                    soggetto.Coordinate = null;
                }
                if (fIscrizione != null)
                {
                    soggetto.fIscrizione = (bool)fIscrizione;
                }
                //NUOVI CAMPI PER GLI ISPETTORI
                if (fDomicilioUgualeResidenza != null)
                {
                    soggetto.fDomicilioUgualeResidenza = (bool)fDomicilioUgualeResidenza;
                }
                if (!string.IsNullOrEmpty(CittaDomicilio))
                {
                    soggetto.CittaDomicilio = CittaDomicilio;
                }
                else
                {
                    soggetto.CittaDomicilio = null;
                }

                if ((!string.IsNullOrEmpty(iDPaeseDomicilio.ToString())) && (iDPaeseDomicilio.ToString() != "0"))
                {
                    soggetto.IDPaeseDomicilio = iDPaeseDomicilio;
                }
                else
                {
                    soggetto.IDPaeseDomicilio = null;
                }

                if (!string.IsNullOrEmpty(IDProvinciaDomicilio.ToString()) && (IDProvinciaDomicilio.ToString() != "0"))
                {
                    soggetto.IDProvinciaDomicilio = IDProvinciaDomicilio;
                }
                else
                {
                    soggetto.IDProvinciaDomicilio = null;
                }
                if (!string.IsNullOrEmpty(CapDomicilio))
                {
                    soggetto.CapDomicilio = CapDomicilio;
                }
                else
                {
                    soggetto.CapDomicilio = null;
                }
                if (!string.IsNullOrEmpty(IndirizzoDomicilio))
                {
                    soggetto.IndirizzoDomicilio = IndirizzoDomicilio;
                }
                else
                {
                    soggetto.IndirizzoDomicilio = null;
                }
                if (!string.IsNullOrEmpty(NumeroCivicoDomicilio))
                {
                    soggetto.NumeroCivicoDomicilio = NumeroCivicoDomicilio;
                }
                else
                {
                    soggetto.NumeroCivicoDomicilio = null;
                }
                if (!string.IsNullOrEmpty(Cellulare))
                {
                    soggetto.Cellulare = Cellulare;
                }
                else
                {
                    soggetto.Cellulare = null;
                }
                if (!string.IsNullOrEmpty(TelefonoOrganizzazione))
                {
                    soggetto.TelefonoOrganizzazione = TelefonoOrganizzazione;
                }
                else
                {
                    soggetto.TelefonoOrganizzazione = null;
                }
                if (!string.IsNullOrEmpty(EmailOrganizzazione))
                {
                    soggetto.EmailOrganizzazione = EmailOrganizzazione;
                }
                else
                {
                    soggetto.EmailOrganizzazione = null;
                }
                if (!string.IsNullOrEmpty(EmailPecOrganizzazione))
                {
                    soggetto.EmailPecOrganizzazione = EmailPecOrganizzazione;
                }
                else
                {
                    soggetto.EmailPecOrganizzazione = null;
                }
                if (!string.IsNullOrEmpty(PartitaIVAOrganizzazione))
                {
                    soggetto.PartitaIVAOrganizzazione = PartitaIVAOrganizzazione;
                }
                else
                {
                    soggetto.PartitaIVAOrganizzazione = null;
                }
                if (!string.IsNullOrEmpty(IDTipologiaQualificaIspettore.ToString()) && (IDTipologiaQualificaIspettore.ToString() != "0"))
                {
                    soggetto.IDTipologiaQualificaIspettore = IDTipologiaQualificaIspettore;
                }
                else
                {
                    soggetto.IDTipologiaQualificaIspettore = null;
                }
                if (fIdoneoIspezione != null)
                {
                    soggetto.fIdoneoIspezione = (bool)fIdoneoIspezione;
                }
                if (fCondanna != null)
                {
                    soggetto.fCondanna = (bool)fCondanna;
                }
                if (!string.IsNullOrEmpty(IDTipologiaTitoloStudio.ToString()) && (IDTipologiaTitoloStudio.ToString() != "0"))
                {
                    soggetto.IDTipologiaTitoloStudio = IDTipologiaTitoloStudio;
                }
                else
                {
                    soggetto.IDTipologiaTitoloStudio = null;
                }
                if (!string.IsNullOrEmpty(TitoloStudioConseguitoPresso))
                {
                    soggetto.TitoloStudioConseguitoPresso = TitoloStudioConseguitoPresso;
                }
                else
                {
                    soggetto.TitoloStudioConseguitoPresso = null;
                }
                if (DataTitoloStudio != null)
                {
                    soggetto.DataTitoloStudio = (DateTime)DataTitoloStudio;
                }
                else
                {
                    soggetto.DataTitoloStudio = null;
                }
                if (!string.IsNullOrEmpty(StageAzienda))
                {
                    soggetto.StageAzienda = StageAzienda;
                }
                else
                {
                    soggetto.StageAzienda = null;
                }
                if (DataStageAziendaDal != null)
                {
                    soggetto.DataStageAziendaDal = (DateTime)DataStageAziendaDal;
                }
                else
                {
                    soggetto.DataStageAziendaDal = null;
                }
                if (DataStageAziendaAl != null)
                {
                    soggetto.DataStageAziendaAl = (DateTime)DataStageAziendaAl;
                }
                else
                {
                    soggetto.DataStageAziendaAl = null;
                }
                if (!string.IsNullOrEmpty(CorsoFormazione))
                {
                    soggetto.CorsoFormazione = CorsoFormazione;
                }
                else
                {
                    soggetto.CorsoFormazione = null;
                }
                if (!string.IsNullOrEmpty(CorsoFormazioneOrganizzatore))
                {
                    soggetto.CorsoFormazioneOrganizzatore = CorsoFormazioneOrganizzatore;
                }
                else
                {
                    soggetto.CorsoFormazioneOrganizzatore = null;
                }
                if (fRequisitoOrganizzativo != null)
                {
                    soggetto.fRequisitoOrganizzativo = (bool)fRequisitoOrganizzativo;
                }
                if (fDisponibilitaApparecchiature != null)
                {
                    soggetto.fDisponibilitaApparecchiature = (bool)fDisponibilitaApparecchiature;
                }
                if (fDisponibilitaControlli != null)
                {
                    soggetto.fDisponibilitaControlli = (bool)fDisponibilitaControlli;
                }
                if (fOrganizzazioneEsterna != null)
                {
                    soggetto.fOrganizzazioneEsterna = (bool)fOrganizzazioneEsterna;
                }
                if (!string.IsNullOrEmpty(OrganismoIspezioneNumero))
                {
                    soggetto.OrganismoIspezioneNumero = OrganismoIspezioneNumero;
                }
                else
                {
                    soggetto.OrganismoIspezioneNumero = null;
                }
                if (!string.IsNullOrEmpty(OrganismoIspezione))
                {
                    soggetto.OrganismoIspezione = OrganismoIspezione;
                }
                else
                {
                    soggetto.OrganismoIspezione = null;
                }
                if (fConsensoRequisitiDichiarati != null)
                {
                    soggetto.fConsensoRequisitiDichiarati = (bool)fConsensoRequisitiDichiarati;
                }

                //if ((!string.IsNullOrEmpty(IDStatoAccreditamento.ToString())) && ((IDStatoAccreditamento.ToString() != "0")))
                //{
                //    soggetto.IDStatoAccreditamento = IDStatoAccreditamento;
                //}
                //else
                //{
                //    soggetto.IDStatoAccreditamento = null;
                //}

                //if ((!string.IsNullOrEmpty(IDStatoSottoAccreditamento.ToString())) && ((IDStatoSottoAccreditamento.ToString() != "0")))
                //{
                //    soggetto.IDStatoSottoAccreditamento = IDStatoSottoAccreditamento;
                //}
                //else
                //{
                //    soggetto.IDStatoSottoAccreditamento = null;
                //}

                if (IDTipologiaOrdineCollegio != null)
                {
                    soggetto.IDTipologiaOrdineCollegio = IDTipologiaOrdineCollegio;
                }
                else
                {
                    soggetto.IDTipologiaOrdineCollegio = null;
                }
                if (!string.IsNullOrEmpty(IDProvinciaOrdineCollegio.ToString()) && (IDProvinciaOrdineCollegio.ToString() != "0"))
                {
                    soggetto.IDProvinciaOrdineCollegio = IDProvinciaOrdineCollegio;
                }
                else
                {
                    soggetto.IDProvinciaOrdineCollegio = null;
                }
                if (!string.IsNullOrEmpty(SezioneOrdineCollegio))
                {
                    soggetto.SezioneOrdineCollegio = SezioneOrdineCollegio;
                }
                else
                {
                    soggetto.SezioneOrdineCollegio = null;
                }
                if (!string.IsNullOrEmpty(NumeroOrdineCollegio))
                {
                    soggetto.NumeroOrdineCollegio = NumeroOrdineCollegio;
                }
                else
                {
                    soggetto.NumeroOrdineCollegio = null;
                }
                if (DataOrdineCollegio != null)
                {
                    soggetto.DataOrdineCollegio = (DateTime)DataOrdineCollegio;
                }
                else
                {
                    soggetto.DataOrdineCollegio = null;
                }


                if (operationType == "insert")
                {
                    db.COM_AnagraficaSoggetti.Add(soggetto);
                }
            }

            try
            {
                db.SaveChanges();
            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception ex)
            {
                throw;
            }

            if (operationType == "insert")
            {
                IDSoggettoInsert = soggetto.IDSoggetto;
            }
            else if (operationType == "update")
            {
                IDSoggettoInsert = iDSoggetto;
            }
            else if (operationType == "delete")
            {
                IDSoggettoInsert = iDSoggetto;
            }

            return (int)IDSoggettoInsert;
        }

        public static void SaveInsertDeleteDatiProvinceCompetenza(
                int iDSoggetto,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var provinceAttuali = db.COM_ProvinceCompetenza.Where(i => i.IDSoggetto == iDSoggetto).ToList();

            foreach (var province in provinceAttuali)
            {
                if (!valoriSelected.Contains(province.IDProvincia.ToString()))
                {
                    db.COM_ProvinceCompetenza.Remove(province);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!provinceAttuali.Any(o => o.IDProvincia.ToString() == valoriSelected[i]))
                {
                    db.COM_ProvinceCompetenza.Add(new COM_ProvinceCompetenza() { IDProvincia = int.Parse(valoriSelected[i]), IDSoggetto = iDSoggetto });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiRuoliSoggetto(
                int iDSoggetto,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var ruoliAttuali = db.COM_RuoliSoggetti.Where(i => i.IDSoggetto == iDSoggetto).ToList();

            foreach (var ruoli in ruoliAttuali)
            {
                if (!valoriSelected.Contains(ruoli.IDRuoloSoggetto.ToString()))
                {
                    db.COM_RuoliSoggetti.Remove(ruoli);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!ruoliAttuali.Any(o => o.IDRuoloSoggetto.ToString() == valoriSelected[i]))
                {
                    db.COM_RuoliSoggetti.Add(new COM_RuoliSoggetti() { IDRuoloSoggetto = int.Parse(valoriSelected[i]), IDSoggetto = iDSoggetto });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiClassificazioniImpiantoSoggetto(
                int iDSoggetto,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var classificazioniAttuali = db.COM_ClassificazioniImpianto.Where(i => i.IDSoggetto == iDSoggetto).ToList();

            foreach (var classificazioni in classificazioniAttuali)
            {
                if (!valoriSelected.Contains(classificazioni.IDClassificazioneImpianto.ToString()))
                {
                    db.COM_ClassificazioniImpianto.Remove(classificazioni);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!classificazioniAttuali.Any(o => o.IDClassificazioneImpianto.ToString() == valoriSelected[i]))
                {
                    db.COM_ClassificazioniImpianto.Add(new COM_ClassificazioniImpianto() { IDClassificazioneImpianto = int.Parse(valoriSelected[i]), IDSoggetto = iDSoggetto });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiAbilitazioniSoggetto(
                int iDSoggetto,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var abilitazioniAttuali = db.COM_AbilitazioniSoggetto.Where(i => i.IDSoggetto == iDSoggetto).ToList();

            foreach (var abilitazioni in abilitazioniAttuali)
            {
                if (!valoriSelected.Contains(abilitazioni.IDAbilitazioneSoggetto.ToString()))
                {
                    db.COM_AbilitazioniSoggetto.Remove(abilitazioni);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!abilitazioniAttuali.Any(o => o.IDAbilitazioneSoggetto.ToString() == valoriSelected[i]))
                {
                    db.COM_AbilitazioniSoggetto.Add(new COM_AbilitazioniSoggetto() { IDAbilitazioneSoggetto = int.Parse(valoriSelected[i]), IDSoggetto = iDSoggetto });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTipologiaDistributoriCombustibile(
                int iDSoggetto,
                string tipologiaDistributoriCombustibileAltro,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var tipiAttuali = db.COM_TipiDistributoriCombustibile.Where(i => i.IDSoggetto == iDSoggetto).ToList();

            foreach (var vettori in tipiAttuali)
            {
                if (!valoriSelected.Contains(vettori.IDTipologiaDistributoriCombustibile.ToString()))
                {
                    db.COM_TipiDistributoriCombustibile.Remove(vettori);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!tipiAttuali.Any(o => o.IDTipologiaDistributoriCombustibile.ToString() == valoriSelected[i]))
                {
                    if (valoriSelected[i] == "1")
                    {
                        string tipologiaDistributoriCombustibileAltrox = "";
                        if (!string.IsNullOrEmpty(tipologiaDistributoriCombustibileAltro))
                        {
                            tipologiaDistributoriCombustibileAltrox = tipologiaDistributoriCombustibileAltro;
                        }

                        db.COM_TipiDistributoriCombustibile.Add(new COM_TipiDistributoriCombustibile() { TipologiaDistributoriCombustibileAltro = tipologiaDistributoriCombustibileAltrox, IDTipologiaDistributoriCombustibile = int.Parse(valoriSelected[i]), IDSoggetto = iDSoggetto });
                    }
                    else
                    {
                        db.COM_TipiDistributoriCombustibile.Add(new COM_TipiDistributoriCombustibile() { IDTipologiaDistributoriCombustibile = int.Parse(valoriSelected[i]), IDSoggetto = iDSoggetto });
                    }
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiComuniCompetenza(
                int iDSoggetto,
                List<object> valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var comuniAttuali = db.COM_CodiciCatastaliCompetenza.Where(i => i.IDSoggetto == iDSoggetto).ToList();

            foreach (var comuni in comuniAttuali)
            {
                if (!valoriSelected.Contains(comuni.IDCodiceCatastale))
                {
                    db.COM_CodiciCatastaliCompetenza.Remove(comuni);
                }
            }

            foreach (var item in valoriSelected)
            {
                int iDCodiceCatastale = int.Parse(item.ToString());

                if (!comuniAttuali.Any(o => o.IDCodiceCatastale == iDCodiceCatastale))
                {
                    db.COM_CodiciCatastaliCompetenza.Add(new COM_CodiciCatastaliCompetenza() { IDCodiceCatastale = iDCodiceCatastale, IDSoggetto = iDSoggetto });
                }
            }

            db.SaveChanges();
        }

        public static List<COM_RuoliSoggetti> GetValoriUsersRuoloSoggetto(int iDSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            var result = db.COM_RuoliSoggetti.Where(a => a.IDSoggetto == iDSoggetto).OrderBy(s => s.IDRuoliSoggetti).ToList();

            return result;
        }

        public static List<COM_ProvinceCompetenza> GetValoriUsersProvinceCompetenza(int iDSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            var result = db.COM_ProvinceCompetenza.Where(a => a.IDSoggetto == iDSoggetto).OrderBy(s => s.IDProvincia).ToList();

            return result;
        }

        public static List<COM_CodiciCatastaliCompetenza> GetValoriUsersComuniCompetenza(int iDSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            var result = db.COM_CodiciCatastaliCompetenza.Where(a => a.IDSoggetto == iDSoggetto).OrderBy(s => s.IDCodiceCatastale).ToList();

            return result;
        }

        public static List<COM_ClassificazioniImpianto> GetValoriUsersClassificazioniImpianto(int iDSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            var result = db.COM_ClassificazioniImpianto.Where(a => a.IDSoggetto == iDSoggetto).OrderBy(s => s.IDClassificazioneImpianto).ToList();

            return result;
        }

        public static List<COM_AbilitazioniSoggetto> GetValoriUsersAbilitazioniSoggetto(int iDSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            var result = db.COM_AbilitazioniSoggetto.Where(a => a.IDSoggetto == iDSoggetto).OrderBy(s => s.IDAbilitazioneSoggetto).ToList();

            return result;
        }

        public static List<COM_TipiDistributoriCombustibile> GetValoriUsersTipiDistributoriCombustibile(int iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.COM_TipiDistributoriCombustibile.Where(a => a.IDSoggetto == iDSoggetto).OrderBy(s => s.IDTipologiaDistributoriCombustibile).Distinct().ToList();

                return result;
            }
        }

        public static bool CheckfEmail(string email, int? iDSoggetto, int iDTipoSoggetto)
        {
            bool fExist = false;
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var soggetto = db.COM_AnagraficaSoggetti.Where(a => (a.fAttivo == true)
                                         && (a.Email == email)
                                         && ((a.IDSoggetto != iDSoggetto) || (a.IDSoggetto == null))
                                         && (a.IDTipoSoggetto == iDTipoSoggetto)
                                        ).OrderBy(a => a.IDSoggetto).ToList();

            if (soggetto.Count > 0)
            {
                fExist = true;
            }

            return fExist;
        }

        public static bool CheckfEmailPec(string emailpec, int? iDSoggetto, int iDTipoSoggetto)
        {
            bool fExist = false;
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var soggetto = db.COM_AnagraficaSoggetti.Where(a => (a.fAttivo == true)
                                         && (a.EmailPec == emailpec)
                                         && ((a.IDSoggetto != iDSoggetto) || (a.IDSoggetto == null))
                                         && (a.IDTipoSoggetto == iDTipoSoggetto)
                                        ).OrderBy(a => a.IDSoggetto).ToList();

            if (soggetto.Count > 0)
            {
                fExist = true;
            }

            return fExist;
        }

        public static bool CheckfPartitaIva(string partitaIva, int? iDSoggetto, int iDTipoSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            bool fExistPiva = false;
            bool fExist = true;

            if (partitaIva != "")
            {
                var soggetto = db.COM_AnagraficaSoggetti.Where(a => (a.fAttivo == true)
                                         && (a.PartitaIVA == partitaIva)
                                         && ((a.IDSoggetto != iDSoggetto) || (a.IDSoggetto == null))
                                         && (a.IDTipoSoggetto == iDTipoSoggetto)
                                        ).OrderBy(a => a.IDSoggetto).ToList();

                if (soggetto.Count > 0)
                {
                    fExistPiva = true;
                }
            }

            if (!fExistPiva)
            {
                fExist = false;
            }

            return fExist;
        }

        public static bool CheckfCodiceFiscale(string codicefiscale, int? iDSoggetto, int iDTipoSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            bool fExistCodiceFiscale = false;
            bool fExist = true;

            if (codicefiscale != "")
            {
                var soggetto = db.COM_AnagraficaSoggetti.Where(a => (a.fAttivo == true)
                                         && (a.CodiceFiscale == codicefiscale)
                                         && ((a.IDSoggetto != iDSoggetto) || (a.IDSoggetto == null))
                                         && (a.IDTipoSoggetto == iDTipoSoggetto)
                                        ).OrderBy(a => a.IDSoggetto).ToList();

                if (soggetto.Count > 0)
                {
                    fExistCodiceFiscale = true;
                }
            }

            if (!fExistCodiceFiscale)
            {
                fExist = false;
            }

            return fExist;
        }

        //public static bool CheckfCodiceFiscalePartitaIva(string partitaIva, string codiceFiscale, int? iDSoggetto, int iDTipoSoggetto)
        //{
        //    var db = DataLayer.Common.ApplicationContext.Current.Context;

        //    bool fExistPiva = false;
        //    bool fExistCf = false;
        //    bool fExist = true;

        //    if (partitaIva != "")
        //    {
        //        var soggetto = db.COM_AnagraficaSoggetti.Where(a => (a.fAttivo == true)
        //                                 && (a.PartitaIVA == partitaIva)
        //                                 && ((a.IDSoggetto != iDSoggetto) || (a.IDSoggetto == null))
        //                                 && (a.IDTipoSoggetto == iDTipoSoggetto)
        //                                ).OrderBy(a => a.IDSoggetto).ToList();

        //        if (soggetto.Count > 0)
        //        {
        //            fExistPiva = true;
        //        }
        //    }

        //    if (codiceFiscale != "")
        //    {
        //        var soggetto = db.COM_AnagraficaSoggetti.Where(a => (a.fAttivo == true)
        //                                 && (a.CodiceFiscale == codiceFiscale)
        //                                 && (a.IDTipoSoggetto == iDTipoSoggetto)
        //                                ).OrderBy(a => a.IDSoggetto).ToList();

        //        if (soggetto.Count > 0)
        //        {
        //            fExistCf = true;
        //        }
        //    }

        //    if ((!fExistPiva) && (!fExistCf))
        //    {
        //        fExist = false;
        //    }

        //    return fExist;
        //}

        public static void SetCodiceSoggetto(int? iDSoggetto, int? iDTipoSoggetto)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var sp = db.sp_SetCodiceSoggetto(iDSoggetto, iDTipoSoggetto);
        }

        public static string GetSqlValoriSoggettiFilter(object iDSoggettoAzienda, object iDTipoSoggetto, object soggetto, object iDFormaGiuridica, object codiceSoggetto, 
                                                        object codiceFiscale, object partitaIva, object email, 
                                                        object dataInizioIscrizione, object dataFineIscrizione, 
                                                        bool attivo, bool fIscrizione, object iDStatoAccreditamento, object fAttivoAccreditamento,
                                                        object dataInizioAccreditamento, object dataFineAccreditamento,
                                                        object dataInizioRinnovo, object dataFineRinnovo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_COM_AnagraficaSoggetti ");
            strSql.Append(" WHERE 1=1 ");

            if (fAttivoAccreditamento != null)
            {
                bool fAttivoAccreditamentoBool = bool.Parse(fAttivoAccreditamento.ToString());
                if (fAttivoAccreditamentoBool)
                {
                    strSql.Append(" AND fAttivoAccreditamento=1");
                }
                else
                {
                    strSql.Append(" AND fAttivoAccreditamento=0");
                }
            }

            if (attivo)
            {
                strSql.Append(" AND fAttivo=1");
            }
            else
            {
                strSql.Append(" AND fAttivo=0");
            }

            if ((iDSoggettoAzienda != "") && (iDSoggettoAzienda != "-1") && (iDSoggettoAzienda != null))
            {
                strSql.Append(" AND IDSoggettoDerived=" + iDSoggettoAzienda);
            }

            if (iDTipoSoggetto.ToString() != "")
            {
                strSql.Append(" AND iDTipoSoggetto=");
                strSql.Append(iDTipoSoggetto);
            }

            if (soggetto.ToString() != "")
            {
                strSql.Append(" AND Soggetto LIKE '%");
                strSql.Append(soggetto.ToString().Replace("'", "''").TrimStart().TrimEnd() + "%'");
            }

            if (iDFormaGiuridica.ToString() != "0")
            {
                strSql.Append(" AND iDFormaGiuridica=");
                strSql.Append(iDFormaGiuridica);
            }

            if (iDStatoAccreditamento.ToString() != "0")
            {
                strSql.Append(" AND iDStatoAccreditamento=");
                strSql.Append(iDStatoAccreditamento);
            }

            if (codiceSoggetto.ToString() != "")
            {
                strSql.Append(" AND codiceSoggetto = ");
                strSql.Append("'");
                strSql.Append(codiceSoggetto);
                strSql.Append("'");
            }

            if (codiceFiscale.ToString().Replace(" ", "").Trim() != "")
            {
                strSql.Append(" AND codiceFiscale = ");
                strSql.Append("'");
                strSql.Append(codiceFiscale.ToString().Replace("'", "''").Trim());
                strSql.Append("'");
            }

            if (partitaIva.ToString().Replace(" ", "").Trim() != "")
            {
                strSql.Append(" AND partitaIva = ");
                strSql.Append("'");
                strSql.Append(partitaIva.ToString().Replace("'", "''").Trim());
                strSql.Append("'");
            }

            if (email.ToString() != "")
            {
                strSql.Append(" AND Email LIKE '%");
                strSql.Append(email.ToString().Replace("'", "''").TrimStart().TrimEnd() + "%'");
            }

            if (dataInizioIscrizione.ToString() != "")
            {
                strSql.Append(" AND CONVERT(varchar(10), DataIscrizione, 126) >= '");
                DateTime dataIscrizioneDA = DateTime.Parse(dataInizioIscrizione.ToString());
                string newDataIscrizioneDA = dataIscrizioneDA.ToString("yyyy") + "-" + dataIscrizioneDA.ToString("MM") + "-" + dataIscrizioneDA.ToString("dd");
                strSql.Append(newDataIscrizioneDA);
                strSql.Append("'");
            }

            if (dataFineIscrizione.ToString() != "")
            {
                strSql.Append(" AND CONVERT(varchar(10), DataIscrizione, 126) <= '");
                System.DateTime dataIscrizioneA = DateTime.Parse(dataFineIscrizione.ToString());
                string newdataIscrizioneA = dataIscrizioneA.ToString("yyyy") + "-" + dataIscrizioneA.ToString("MM") + "-" + dataIscrizioneA.ToString("dd");
                strSql.Append(newdataIscrizioneA);
                strSql.Append("'");
            }


            if (dataInizioAccreditamento.ToString() != "")
            {
                strSql.Append(" AND CONVERT(varchar(10), DataAccreditamento, 126) >= '");
                DateTime dataAccreditamentoDA = DateTime.Parse(dataInizioAccreditamento.ToString());
                string newDataAccreditamentoDA = dataAccreditamentoDA.ToString("yyyy") + "-" + dataAccreditamentoDA.ToString("MM") + "-" + dataAccreditamentoDA.ToString("dd");
                strSql.Append(newDataAccreditamentoDA);
                strSql.Append("'");
            }

            if (dataFineAccreditamento.ToString() != "")
            {
                strSql.Append(" AND CONVERT(varchar(10), DataAccreditamento, 126) <= '");
                System.DateTime dataAccreditamentoA = DateTime.Parse(dataFineAccreditamento.ToString());
                string newdataAccreditamentoA = dataAccreditamentoA.ToString("yyyy") + "-" + dataAccreditamentoA.ToString("MM") + "-" + dataAccreditamentoA.ToString("dd");
                strSql.Append(newdataAccreditamentoA);
                strSql.Append("'");
            }


            if (dataInizioRinnovo.ToString() != "")
            {
                strSql.Append(" AND CONVERT(varchar(10), DataRinnovo, 126) >= '");
                DateTime dataRinnovoDA = DateTime.Parse(dataInizioRinnovo.ToString());
                string newDataRinnovoDA = dataRinnovoDA.ToString("yyyy") + "-" + dataRinnovoDA.ToString("MM") + "-" + dataRinnovoDA.ToString("dd");
                strSql.Append(newDataRinnovoDA);
                strSql.Append("'");
            }

            if (dataFineRinnovo.ToString() != "")
            {
                strSql.Append(" AND CONVERT(varchar(10), DataRinnovo, 126) <= '");
                System.DateTime dataRinnovoA = DateTime.Parse(dataFineRinnovo.ToString());
                string newdataRinnovoA = dataRinnovoA.ToString("yyyy") + "-" + dataRinnovoA.ToString("MM") + "-" + dataRinnovoA.ToString("dd");
                strSql.Append(newdataRinnovoA);
                strSql.Append("'");
            }


            if (fIscrizione)
            {
                strSql.Append(" AND fIscrizione=1");
            }
            else
            {
                strSql.Append(" AND fIscrizione=0");
            }

            strSql.Append(" ORDER BY DataInserimento DESC");
            return strSql.ToString();
        }

        public static int? SaveInsertDeleteDatiSoggettiFirmaDigitale(int iDSoggetto,
                                                                     int TipoFirma,
                                                                     string SchedaIscrizione,
                                                                     DateTime dataFirma,
                                                                     string IpClient,
                                                                     string SignerQualifier,
                                                                     string SignerName,
                                                                     string SignerSurname,
                                                                     string SignerIdentifier,
                                                                     string SignerFullName,
                                                                     string SignerAuthority,
                                                                     string SignerCertificationAuthority,
                                                                     string SignerSerialNumber)
        {
            int? iDSoggettoInsert = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var firma = new COM_AnagraficaSoggettiFirmaDigitale();
                firma.IDSoggetto = iDSoggetto;
                firma.TipoFirma = TipoFirma;
                if (!string.IsNullOrEmpty(SchedaIscrizione))
                {
                    firma.SchedaIscrizione = SchedaIscrizione;
                }
                else
                {
                    firma.SchedaIscrizione = null;
                }
                firma.DataFirma = dataFirma;

                if (!string.IsNullOrEmpty(IpClient))
                {
                    firma.IPClient = IpClient;
                }
                else
                {
                    firma.IPClient = null;
                }
                if (!string.IsNullOrEmpty(SignerQualifier))
                {
                    firma.SignerQualifier = SignerQualifier;
                }
                else
                {
                    firma.SignerQualifier = null;
                }
                if (!string.IsNullOrEmpty(SignerName))
                {
                    firma.SignerName = SignerName;
                }
                else
                {
                    firma.SignerName = null;
                }
                if (!string.IsNullOrEmpty(SignerSurname))
                {
                    firma.SignerSurname = SignerSurname;
                }
                else
                {
                    firma.SignerSurname = null;
                }
                if (!string.IsNullOrEmpty(SignerIdentifier))
                {
                    firma.SignerIdentifier = SignerIdentifier;
                }
                else
                {
                    firma.SignerIdentifier = SignerIdentifier;
                }
                if (!string.IsNullOrEmpty(SignerFullName))
                {
                    firma.SignerFullName = SignerFullName;
                }
                else
                {
                    firma.SignerFullName = null;
                }
                if (!string.IsNullOrEmpty(SignerAuthority))
                {
                    firma.SignerAuthority = SignerAuthority;
                }
                else
                {
                    firma.SignerAuthority = SignerAuthority;
                }
                if (!string.IsNullOrEmpty(SignerCertificationAuthority))
                {
                    firma.SignerCertificationAuthority = SignerCertificationAuthority;
                }
                else
                {
                    firma.SignerCertificationAuthority = null;
                }
                if (!string.IsNullOrEmpty(SignerSerialNumber))
                {
                    firma.SignerSerialNumber = SignerSerialNumber;
                }
                else
                {
                    firma.SignerSerialNumber = null;
                }
                ctx.COM_AnagraficaSoggettiFirmaDigitale.Add(firma);

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                return iDSoggettoInsert = firma.IDSoggetto;
            }
        }

        public static int? SetIscrizioneEffettuata(int? iDSoggetto)
        {
            int? iDSoggettoInsert = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var soggetto = new COM_AnagraficaSoggetti();
                soggetto = ctx.COM_AnagraficaSoggetti.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);
                soggetto.fIscrizione = true;
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                return iDSoggettoInsert = soggetto.IDSoggetto;
            }
        }

        public static bool ChangefAttivo(int iDSoggetto, bool fAttivo)
        {
            bool fAttivoSoggetto = false;
            using (var ctx = new CriterDataModel())
            {
                var soggetto = ctx.COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();

                if (fAttivo)
                {
                    soggetto.fAttivo = false;
                    fAttivoSoggetto = false;
                }
                else
                {
                    soggetto.fAttivo = true;
                    fAttivoSoggetto = true;
                }

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                return fAttivoSoggetto;
            }
        }

        public static void InsertDatiAlboSoggetti(int iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var soggetto = ctx.COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();
                if (soggetto != null)
                {
                    var albo = new COM_AnagraficaSoggettiAlbo();
                    albo.IDSoggetto = soggetto.IDSoggetto;
                    albo.Impresa = soggetto.NomeAzienda;
                    albo.Indirizzo = soggetto.IndirizzoSedeLegale;
                    albo.Cap = soggetto.CapSedeLegale;
                    albo.Citta = soggetto.CittaSedeLegale;
                    albo.IDProvincia = soggetto.IDProvinciaSedeLegale;
                    albo.AmministratoreDelegato = soggetto.Nome + " " + soggetto.Cognome;
                    albo.Telefono = soggetto.Telefono;
                    albo.Fax = soggetto.Fax;
                    albo.Email = soggetto.Email;
                    albo.EmailPec = soggetto.EmailPec;
                    albo.SitoWeb = soggetto.SitoWeb;
                    albo.PartitaIVA = soggetto.PartitaIVA;
                    albo.fAmministratoreDelegato = false;
                    albo.fTelefono = false;
                    albo.fFax = false;
                    albo.fEmail = false;
                    albo.fEmailPec = false;
                    albo.fSitoWeb = false;
                    albo.fPartitaIVA = false;

                    ctx.COM_AnagraficaSoggettiAlbo.Add(albo);

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

        public static int SaveInsertDeleteDatiAlboSoggetti(
                int? iDSoggetto,
                string impresa,
                string indirizzo,
                string cap,
                string citta,
                int? iDProvincia,
                string amministratoreDelegato,
                string telefono,
                string fax,
                string email,
                string emailPec,
                string sitoWeb,
                string partitaIva,
                bool fAmministratoreDelegato,
                bool fTelefono,
                bool fFax,
                bool fEmail,
                bool fEmailPec,
                bool fSitoWeb,
                bool fPartitaIVA
            )
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var albo = new COM_AnagraficaSoggettiAlbo();

            albo = db.COM_AnagraficaSoggettiAlbo.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);

            if (!string.IsNullOrEmpty(impresa))
            {
                albo.Impresa = impresa;
            }
            else
            {
                albo.Impresa = null;
            }
            if (!string.IsNullOrEmpty(indirizzo))
            {
                albo.Indirizzo = indirizzo;
            }
            else
            {
                albo.Indirizzo = null;
            }
            if (!string.IsNullOrEmpty(cap))
            {
                albo.Cap = cap;
            }
            else
            {
                albo.Cap = null;
            }
            if (!string.IsNullOrEmpty(citta))
            {
                albo.Citta = citta;
            }
            else
            {
                albo.Citta = null;
            }
            if ((!string.IsNullOrEmpty(iDProvincia.ToString())) && (iDProvincia.ToString() != "0"))
            {
                albo.IDProvincia = iDProvincia;
            }
            else
            {
                albo.IDProvincia = null;
            }
            if (!string.IsNullOrEmpty(amministratoreDelegato))
            {
                albo.AmministratoreDelegato = amministratoreDelegato;
            }
            else
            {
                albo.AmministratoreDelegato = null;
            }
            if (!string.IsNullOrEmpty(telefono))
            {
                albo.Telefono = telefono;
            }
            else
            {
                albo.Telefono = null;
            }
            if (!string.IsNullOrEmpty(fax))
            {
                albo.Fax = fax;
            }
            else
            {
                albo.Fax = null;
            }
            if (!string.IsNullOrEmpty(email))
            {
                albo.Email = email;
            }
            else
            {
                albo.Email = null;
            }
            if (!string.IsNullOrEmpty(emailPec))
            {
                albo.EmailPec = emailPec;
            }
            else
            {
                albo.EmailPec = null;
            }
            if (!string.IsNullOrEmpty(sitoWeb))
            {
                albo.SitoWeb = sitoWeb;
            }
            else
            {
                albo.SitoWeb = null;
            }
            if (!string.IsNullOrEmpty(partitaIva))
            {
                albo.PartitaIVA = partitaIva;
            }
            else
            {
                albo.PartitaIVA = null;
            }
            albo.fAmministratoreDelegato = fAmministratoreDelegato;
            albo.fTelefono = fTelefono;
            albo.fFax = fFax;
            albo.fEmail = fEmail;
            albo.fEmailPec = fEmailPec;
            albo.fSitoWeb = fSitoWeb;
            albo.fPartitaIVA = fPartitaIVA;

            try
            {
                db.SaveChanges();
            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception ex)
            {
                throw;
            }

            return (int)iDSoggetto;
        }

        #region Contratti Ispettore

        public static int SaveInsertDeleteDatiContrattoIspettore(
                        string operationType,
                        int? IDContrattoIspettore,
                        int iDIspettore,
                        int iDStatoContratto,
                        int numeroIspezioniMax,
                        DateTime? dataInizioContratto,
                        DateTime? dataFineContratto,
                        bool? attivo
                    )
        {
            int? IDContrattoIspettoreInsert = null;

            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var contratto = new COM_ContrattoIspettore();

            if (operationType == "update")
            {
                contratto = db.COM_ContrattoIspettore.FirstOrDefault(i => i.IDContrattoIspettore == IDContrattoIspettore);
            }

            if (numeroIspezioniMax != 0)
            {
                contratto.NumeroIspezioniMax = numeroIspezioniMax;
            }

            if (iDIspettore != 0)
            {
                contratto.IDIspettore = iDIspettore;
            }

            if (!string.IsNullOrEmpty(iDStatoContratto.ToString()))
            {
                contratto.IDStatoContratto = iDStatoContratto;
            }

            if (dataInizioContratto != null)
            {
                contratto.DataInizioContratto = (DateTime)dataInizioContratto;
            }

            if (dataFineContratto != null)
            {
                contratto.DataFineContratto = (DateTime)dataFineContratto;
            }


            if (attivo != null)
            {
                contratto.fAttivo = (bool)attivo;
            }

            if (operationType == "insert")
            {
                db.COM_ContrattoIspettore.Add(contratto);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            if (operationType == "insert")
            {
                IDContrattoIspettoreInsert = contratto.IDContrattoIspettore;
            }
            else if (operationType == "update")
            {
                IDContrattoIspettoreInsert = IDContrattoIspettore;
            }


            return (int)IDContrattoIspettoreInsert;
        }

        public static bool CheckContrattoExit(int? iDIspettore)
        {
            bool fExist = true;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                fExist = ctx.COM_ContrattoIspettore.Where(a => a.IDIspettore == iDIspettore && a.fAttivo == true).Any();
            }

            return fExist;
        }

        public static int? GetContrattoContrattoIspettoreAttivo(int iDIspettore)
        {
            int? iDContrattoIspettore = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var contratto = ctx.COM_ContrattoIspettore.Where(a => a.IDIspettore == iDIspettore && a.fAttivo == true).FirstOrDefault();

                if (contratto != null)
                {
                    iDContrattoIspettore = contratto.IDContrattoIspettore;
                }
            }

            return iDContrattoIspettore;
        }

        public static string GetSqlValoriContrattoIspettoreFilter(object iDIspettore,
            object numeroIspezioniMax,
            object iDStatoContratto,
            object DataInizioContrattoDA,
            object DataInizioContrattoAL,
            object DataFineContrattoDA,
            object DataFineContrattoAl,
            bool fAttivo
            )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_COM_ContrattoIspettore ");
            strSql.Append(" WHERE 1=1 ");

            if ((iDIspettore != "") && (iDIspettore != "-1") && (iDIspettore != null))
            {
                strSql.Append(" AND IDIspettore=" + iDIspettore);
            }

            if ((numeroIspezioniMax != "") && (numeroIspezioniMax != "-1") && (numeroIspezioniMax != null))
            {
                strSql.Append(" AND NumeroIspezioniMax=" + numeroIspezioniMax);
            }

            if (iDStatoContratto.ToString() != "0")
            {
                strSql.Append(" AND IDStatoContratto=");
                strSql.Append(iDStatoContratto);
            }

            if ((DataInizioContrattoDA != null) && (DataInizioContrattoDA.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInizioContratto, 126) >= '");
                DateTime dataInizioContrattoDa = DateTime.Parse(DataInizioContrattoDA.ToString());
                string newDataInizioContrattoDa = dataInizioContrattoDa.ToString("yyyy") + "-" + dataInizioContrattoDa.ToString("MM") + "-" + dataInizioContrattoDa.ToString("dd");
                strSql.Append(newDataInizioContrattoDa);
                strSql.Append("'");
            }

            if ((DataInizioContrattoAL != null) && (DataInizioContrattoAL.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInizioContratto, 126) <= '");
                DateTime dataInizioContrattoAl = DateTime.Parse(DataInizioContrattoAL.ToString());
                string newdataInizioContrattoAl = dataInizioContrattoAl.ToString("yyyy") + "-" + dataInizioContrattoAl.ToString("MM") + "-" + dataInizioContrattoAl.ToString("dd");
                strSql.Append(newdataInizioContrattoAl);
                strSql.Append("'");
            }

            if ((DataFineContrattoDA != null) && (DataFineContrattoDA.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataFineContratto, 126) >= '");
                DateTime dataFineContrattoDa = DateTime.Parse(DataFineContrattoDA.ToString());
                string newDataFineContrattoDa = dataFineContrattoDa.ToString("yyyy") + "-" + dataFineContrattoDa.ToString("MM") + "-" + dataFineContrattoDa.ToString("dd");
                strSql.Append(newDataFineContrattoDa);
                strSql.Append("'");
            }

            if ((DataFineContrattoAl != null) && (DataFineContrattoAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataFineContratto, 126) <= '");
                DateTime dataFineContrattoAl = DateTime.Parse(DataFineContrattoAl.ToString());
                string newdataFineContrattoAl = dataFineContrattoAl.ToString("yyyy") + "-" + dataFineContrattoAl.ToString("MM") + "-" + dataFineContrattoAl.ToString("dd");
                strSql.Append(newdataFineContrattoAl);
                strSql.Append("'");
            }

            if (fAttivo)
            {
                strSql.Append(" AND fAttivo=1");
            }
            else
            {
                strSql.Append(" AND fAttivo=0");
            }

            strSql.Append(" ORDER BY DataInizioContratto DESC");

            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public static int SaveFirmaIspettore(
                string operationType,
                int? iDContrattoIspettore,
                byte[] imageData
            )
        {
            int? IDContrattoIspettoreInsert = null;

            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var contratto = new COM_ContrattoIspettore();

            if (operationType == "update" || operationType == "delete")
            {
                contratto = db.COM_ContrattoIspettore.FirstOrDefault(i => i.IDContrattoIspettore == iDContrattoIspettore);
            }

            if (imageData != null)
            {
                contratto.Firma = imageData;
            }
            else
            {
                contratto.Firma = null;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            if (operationType == "update" || operationType == "delete")
            {
                IDContrattoIspettoreInsert = iDContrattoIspettore;
            }


            return (int)IDContrattoIspettoreInsert;
        }


        #endregion

        #region Accreditamento Ispettori

        public static int SaveInsertDeleteDatiAccreditamento(
                string operationType,
                int iDSoggetto,
                int iDStatoAccreditamento,
                DateTime? dataAccreditamento,
                DateTime? dataRinnovo,
                DateTime? dataAnnullamento,
                string motivazioneAnnullamento,
                DateTime? dataSospensioneDa,
                DateTime? dataSospensioneA,
                string motivazioneSospensione
            )
        {
            int? IDIspettoreInsert = null;

            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var ispettore = new COM_AnagraficaSoggettiAccreditamento();

            if (operationType == "update")
            {
                ispettore = db.COM_AnagraficaSoggettiAccreditamento.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);
            }

            ispettore.IDSoggetto = iDSoggetto;
            ispettore.IDStatoAccreditamento = iDStatoAccreditamento;

            if (dataAccreditamento != null)
            {
                ispettore.DataAccreditamento = (DateTime)dataAccreditamento;
            }
            else
            {
                ispettore.DataAccreditamento = null;
            }

            if (dataRinnovo != null)
            {
                ispettore.DataRinnovo = (DateTime)dataRinnovo;
            }
            else
            {
                ispettore.DataRinnovo = null;
            }

            if (dataAnnullamento != null)
            {
                ispettore.DataAnnullamento = (DateTime)dataAnnullamento;
            }
            else
            {
                ispettore.DataAnnullamento = null;
            }

            if (!string.IsNullOrEmpty(motivazioneAnnullamento))
            {
                ispettore.MotivazioneAnnullamento = motivazioneAnnullamento;
            }
            else
            {
                ispettore.MotivazioneAnnullamento = null;
            }

            if (dataSospensioneDa != null)
            {
                ispettore.DataSospensioneDa = (DateTime)dataSospensioneDa;
            }
            else
            {
                ispettore.DataSospensioneDa = null;
            }

            if (dataSospensioneA != null)
            {
                ispettore.DataSospensioneA = (DateTime)dataSospensioneA;
            }
            else
            {
                ispettore.DataSospensioneA = null;
            }

            if (!string.IsNullOrEmpty(motivazioneSospensione))
            {
                ispettore.MotivazioneSospensione = motivazioneSospensione;
            }
            else
            {
                ispettore.MotivazioneSospensione = null;
            }

            if (operationType == "insert")
            {
                db.COM_AnagraficaSoggettiAccreditamento.Add(ispettore);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            if (operationType == "insert")
            {
                IDIspettoreInsert = ispettore.IDSoggetto;
            }
            else if (operationType == "update")
            {
                IDIspettoreInsert = iDSoggetto;
            }

            return (int)IDIspettoreInsert;
        }

        public static void StoricizzaStatoAccreditamento(CriterDataModel ctx, COM_AnagraficaSoggettiAccreditamento accreditamento, int iDUtente)
        {
            COM_AnagraficaSoggettiAccreditamentoStato accreditamentoStato = new COM_AnagraficaSoggettiAccreditamentoStato();
            accreditamentoStato.IDSoggetto = accreditamento.IDSoggetto;
            accreditamentoStato.Data = DateTime.Now;
            accreditamentoStato.IDStatoAccreditamento = accreditamento.IDStatoAccreditamento;
            accreditamentoStato.IDUtenteUltimaModifica = iDUtente;
            accreditamentoStato.DataAnnullamento = accreditamento.DataAnnullamento;
            accreditamentoStato.MotivazioneAnnullamento = accreditamento.MotivazioneAnnullamento;
            accreditamentoStato.DataSospensioneDa = accreditamento.DataSospensioneDa;
            accreditamentoStato.DataSospensioneA = accreditamento.DataSospensioneA;
            accreditamentoStato.MotivazioneSospensione = accreditamento.MotivazioneSospensione;
            ctx.COM_AnagraficaSoggettiAccreditamentoStato.Add(accreditamentoStato);
            ctx.SaveChanges();
        }

        public static void StoricizzaStatoAccreditamento(int iDSoggetto, int iDUtente)
        {
            using (var ctx = new CriterDataModel())
            {
                COM_AnagraficaSoggettiAccreditamento accreditamento = ctx.COM_AnagraficaSoggettiAccreditamento.Find(iDSoggetto);
                if (accreditamento != null)
                {
                    StoricizzaStatoAccreditamento(ctx, accreditamento, iDUtente);
                }
            }
        }

        public static List<V_COM_AnagraficaSoggettiAccreditamentoStato> GetAccreditamentoStorico(int iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var query = ctx.V_COM_AnagraficaSoggettiAccreditamentoStato.AsQueryable();
                query = query.Where(c => c.IDSoggetto == iDSoggetto);
                return query.OrderByDescending(c => c.Data).ToList();
            }
        }

        public static bool ChangefAttivoAccreditamento(int iDSoggetto, bool fAttivo)
        {
            bool fAttivoSoggetto = false;
            using (var ctx = new CriterDataModel())
            {
                var soggetto = ctx.COM_AnagraficaSoggettiAccreditamento.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();

                if (fAttivo)
                {
                    soggetto.fAttivo = false;
                    fAttivoSoggetto = false;
                }
                else
                {
                    soggetto.fAttivo = true;
                    fAttivoSoggetto = true;
                }

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                return fAttivoSoggetto;
            }
        }

        public static DateTime? GetDataRinnovoAccreditamentoIspettori(DateTime? data)
        {
            DateTime? dataRinnovo = null;
            if (data != null)
            {
                DateTime dt = DateTime.Parse(data.ToString()).AddYears(4);
                dataRinnovo = (DateTime?)dt;
            }
            return dataRinnovo;
        }

        //public static void CambiaStatoAccreditamento(int iDSoggetto, int iDStatoAccreditamento)
        //{
        //    using (var ctx = new CriterDataModel())
        //    {
        //        var accreditamento = ctx.COM_AnagraficaSoggettiAccreditamento.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();
        //        accreditamento.IDStatoAccreditamento = iDStatoAccreditamento;
        //        switch (iDStatoAccreditamento)
        //        {
        //            case 1: //Interventi in attesa di realizzazione

        //                break;
        //            case 2: //Interventi realizzati in attesa di conferma

        //                break;
        //            case 3://Interventi parzialmente realizzati

        //                break;
        //            case 4: //Interventi correttamente realizzati
        //                break;
        //            case 5: //Interventi non realizzati - Ispezione

        //                break;
        //            case 6: //Interventi non realizzati - Notifica verbale di accertamento con diffida

        //                break;
        //            case 7: //Interventi non realizzati - Notifica verbale di accertamento con sanzione

        //                break;
        //        }



        //        ctx.SaveChanges();
        //    }
        //}



        //public static int CambiaStatoQualificaIspettore(
        //        int? iDIspettore, // IDSoggetto
        //        int? iDStatoQualifica// IDStatoAccertamento
        //)
        //{
        //    var db = DataLayer.Common.ApplicationContext.Current.Context;
        //    var ispettore = new COM_AnagraficaSoggetti();

        //    int? IDIspettoreCambia = null;

        //    ispettore = db.COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDIspettore).FirstOrDefault();

        //    if (!string.IsNullOrEmpty(iDStatoQualifica.ToString()))
        //    {
        //        ispettore.IDStatoAccreditamento = iDStatoQualifica;
        //    }

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    IDIspettoreCambia = iDIspettore;

        //    return (int)IDIspettoreCambia;
        //}

        //public static string GetSqlValoriQualificaIspettoreFilter(object iDIspettore,
        //    object iDStatoQualifica,
        //    object DataInizioDA,
        //    object DataInizioAL,
        //    object DataFineDA,
        //    object DataFineAl,
        //    bool fAttivo
        //    )
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT ");
        //    strSql.Append(" * ");
        //    strSql.Append(" FROM V_COM_QualificaIspettore ");
        //    strSql.Append(" WHERE 1=1 ");

        //    if ((iDIspettore != "") && (iDIspettore != "-1") && (iDIspettore != null))
        //    {
        //        strSql.Append(" AND IDIspettore=" + iDIspettore);
        //    }

        //    if (iDStatoQualifica.ToString() != "0")
        //    {
        //        strSql.Append(" AND IDStatoAccreditamento=");
        //        strSql.Append(iDStatoQualifica);
        //    }

        //    if ((DataInizioDA != null) && (DataInizioDA.ToString() != ""))
        //    {
        //        strSql.Append(" AND convert(varchar(10), DataInizioQualifica, 126) >= '");
        //        DateTime dataInizioDa = DateTime.Parse(DataInizioDA.ToString());
        //        string newDataInizioDa = dataInizioDa.ToString("yyyy") + "-" + dataInizioDa.ToString("MM") + "-" + dataInizioDa.ToString("dd");
        //        strSql.Append(newDataInizioDa);
        //        strSql.Append("'");
        //    }

        //    if ((DataInizioAL != null) && (DataInizioAL.ToString() != ""))
        //    {
        //        strSql.Append(" AND convert(varchar(10), DataInizioQualifica, 126) <= '");
        //        DateTime dataInizioAl = DateTime.Parse(DataInizioAL.ToString());
        //        string newdataInizioAl = dataInizioAl.ToString("yyyy") + "-" + dataInizioAl.ToString("MM") + "-" + dataInizioAl.ToString("dd");
        //        strSql.Append(newdataInizioAl);
        //        strSql.Append("'");
        //    }

        //    if ((DataFineDA != null) && (DataFineDA.ToString() != ""))
        //    {
        //        strSql.Append(" AND convert(varchar(10), DataFineQualifica, 126) >= '");
        //        DateTime dataFineDa = DateTime.Parse(DataFineDA.ToString());
        //        string newDataFineDa = dataFineDa.ToString("yyyy") + "-" + dataFineDa.ToString("MM") + "-" + dataFineDa.ToString("dd");
        //        strSql.Append(newDataFineDa);
        //        strSql.Append("'");
        //    }

        //    if ((DataFineAl != null) && (DataFineAl.ToString() != ""))
        //    {
        //        strSql.Append(" AND convert(varchar(10), DataFineQualifica, 126) <= '");
        //        DateTime dataFineAl = DateTime.Parse(DataFineAl.ToString());
        //        string newdataFineAl = dataFineAl.ToString("yyyy") + "-" + dataFineAl.ToString("MM") + "-" + dataFineAl.ToString("dd");
        //        strSql.Append(newdataFineAl);
        //        strSql.Append("'");
        //    }

        //    if (fAttivo)
        //    {
        //        strSql.Append(" AND fAttivo=1");
        //    }
        //    else
        //    {
        //        strSql.Append(" AND fAttivo=0");
        //    }

        //    strSql.Append(" ORDER BY DataInizioQualifica DESC");

        //    return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        //}

        //public static bool CheckQualificaExit(int? iDIspettore)
        //{
        //    bool fExist = false;

        //    using (CriterDataModel ctx = new CriterDataModel())
        //    {
        //        fExist = ctx.COM_QualificaIspettore.Where(a => a.IDIspettore == iDIspettore).Any();
        //    }

        //    return fExist;
        //}

        //public static bool CheckQualificaAttiva(int? iDIspettore)
        //{
        //    bool fExist = true;

        //    using (CriterDataModel ctx = new CriterDataModel())
        //    {
        //        fExist = ctx.V_COM_QualificaIspettore.Where(a => a.IDIspettore == iDIspettore && a.IDStatoAccreditamento == 4).Any();
        //    }

        //    return fExist;
        //}


        #endregion

        #region Modulo Privacy
        public static bool GetSetPrivacySoggetti(int iDSoggetto)
        {
            bool fPrivacy = false;

            int nTentativi = int.Parse(ConfigurationManager.AppSettings["NTentativiPrivacy"]);

            using (var ctx = new CriterDataModel())
            {
                COM_AnagraficaSoggetti soggetto = ctx.COM_AnagraficaSoggetti.Find(iDSoggetto);
                if (soggetto != null)
                {
                    if (soggetto.fPrivacyNew)
                    {
                        fPrivacy = true;
                    }
                    else
                    {
                        var privacy = soggetto.COM_AnagraficaSoggettiPrivacyCount.Where(a => a.IDSoggetto == iDSoggetto);
                        if (privacy != null)
                        {
                            if (privacy.Count() < nTentativi)
                            {
                                if (!privacy.Where(a => a.DataInserimento.Date == DateTime.Today).Any())
                                {
                                    COM_AnagraficaSoggettiPrivacyCount privacyCount = new COM_AnagraficaSoggettiPrivacyCount();
                                    privacyCount.IDSoggetto = iDSoggetto;
                                    privacyCount.DataInserimento = DateTime.Now;
                                    ctx.COM_AnagraficaSoggettiPrivacyCount.Add(privacyCount);
                                    ctx.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            COM_AnagraficaSoggettiPrivacyCount privacyCount = new COM_AnagraficaSoggettiPrivacyCount();
                            privacyCount.IDSoggetto = iDSoggetto;
                            privacyCount.DataInserimento = DateTime.Now;
                            ctx.COM_AnagraficaSoggettiPrivacyCount.Add(privacyCount);
                            ctx.SaveChanges();
                        }
                    }
                }

                return fPrivacy;
            }            
        }

        public static void ChangefPrivacySoggetto(int? iDSoggetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var soggetto = ctx.COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();
                soggetto.fPrivacyNew = true;
                ctx.SaveChanges();
            }
        }

        public static bool fPrivacyBloccaNuovoRct(int? iDSoggetto, string type)
        {
            bool fBloccaRct = false;
            int nTentativi = int.Parse(ConfigurationManager.AppSettings["NTentativiPrivacy"]);

            using (var ctx = new CriterDataModel())
            {
                if (type == "Impresa")
                {
                    var soggetto = ctx.COM_AnagraficaSoggetti.Find(iDSoggetto);
                    if (soggetto != null)
                    {
                        if (!soggetto.fPrivacyNew)
                        {
                            var tentativiPrivacy = ctx.COM_AnagraficaSoggettiPrivacyCount.Where(a => a.IDSoggetto == iDSoggetto).ToList();

                            if (tentativiPrivacy != null)
                            {
                                if (tentativiPrivacy.Count() == nTentativi)
                                {
                                    fBloccaRct = true;
                                }
                            }
                        }
                    }
                }
                else if (type == "Operatore")
                {
                    var operatore = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == iDSoggetto).FirstOrDefault();
                    var impresa = ctx.COM_AnagraficaSoggetti.Find(operatore.IDSoggettoDerived);
                    if (!impresa.fPrivacyNew)
                    {
                        var tentativiPrivacy = ctx.COM_AnagraficaSoggettiPrivacyCount.Where(a => a.IDSoggetto == impresa.IDSoggetto).ToList();

                        if (tentativiPrivacy != null)
                        {
                            if (tentativiPrivacy.Count() == nTentativi)
                            {
                                fBloccaRct = true;
                            }
                        }
                    }
                }
            }

            return fBloccaRct;
        }

        public static int GetCountRimanentiPrivacy(int? iDSoggetto)
        {
            int count = 0;
            using (var ctx = new CriterDataModel())
            {
                var soggetto = ctx.COM_AnagraficaSoggetti.Find(iDSoggetto);
                if (soggetto != null)
                {
                    if (!soggetto.fPrivacyNew)
                    {
                        var tentativiPrivacy = ctx.COM_AnagraficaSoggettiPrivacyCount.Where(a => a.IDSoggetto == iDSoggetto);
                        if (tentativiPrivacy != null)
                        {
                            count = tentativiPrivacy.Count();
                        }
                    }
                }
            }

            return count;
        }

        #endregion

    }
}