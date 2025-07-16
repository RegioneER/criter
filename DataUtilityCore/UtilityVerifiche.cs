using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Bender.Collections;
using Bender.Extensions;
using DataLayer;
using DevExpress.Office.NumberConverters;
using EncryptionQS;
using Ionic.Zip;
using KellermanSoftware.CompareNetObjects;
using Z.EntityFramework.Plus;

namespace DataUtilityCore
{
    public static class UtilityVerifiche
    {
        #region Accertamenti
        public static bool SottoponiAdAccertamento(long? iDRapportoControlloTecnico, int iDTipoAccertamento, long? iDIspezione)
        {
            bool fAccertamento = false;
            if (iDTipoAccertamento == 1)
            {
                #region Rapporto in Accertamento
                using (var ctx = new CriterDataModel())
                {
                    var rapportoInAccertamento = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                                                                 & c.IDStatoRapportoDiControllo == 2
                                                                                 & (//c.Osservazioni != null || 
                                                                                    c.Prescrizioni != null ||
                                                                                    c.Raccomandazioni != null ||
                                                                                    c.fImpiantoFunzionante == false)
                                                                                   //Esclusioni 
                                                                                   //& c.fDichiarazioneConformita == true
                                                                                   //& c.fUsoManutenzioneGeneratore == true
                                                                                   //& c.fLibrettoImpiantoCompilato == true
                                                                                   ).FirstOrDefault();

                    if (rapportoInAccertamento != null)
                    {
                        #region NUOVA LOGICA DI SOVRASCRITTURA ACCERTAMENTO
                        //Se esiste un accertamento in stato "Assegnato ad accertatore" con lo stesso codice targatura, GT, Progressivo GT e data controllo >= all'RCT originale legato 
                        //all'accertamento allora non creo il nuovo accertamento ma cambio il riferimento al nuovo RCT, cancello tutte le NC e inserisco quelle nuove

                        var accertamentoInEssere = ctx.VER_Accertamento.Where(a => a.IDStatoAccertamento == 2 &&
                                                                                   a.IDTargaturaImpianto == rapportoInAccertamento.IDTargaturaImpianto &&
                                                                                   a.Prefisso == rapportoInAccertamento.Prefisso &&
                                                                                   a.CodiceProgressivo == rapportoInAccertamento.CodiceProgressivo &&
                                                                                   a.IDTipoAccertamento == iDTipoAccertamento
                                                                                   ).FirstOrDefault();

                        if (accertamentoInEssere != null)
                        {
                            //La dataControlloRCT Nuovo RCT >= dataControlloRCT Vecchio RCT non faccio nulla altrimenti aggiorno l'accertamento
                            if (rapportoInAccertamento.DataControllo >= accertamentoInEssere.RCT_RapportoDiControlloTecnicoBase.DataControllo)
                            {
                                //CANCELLO LE NC DELL'ACCERTAMENTO ORIGINALE
                                var accertamentoNC = ctx.VER_AccertamentoNonConformita.Where(a => a.IDAccertamento == accertamentoInEssere.IDAccertamento).ToList();
                                ctx.VER_AccertamentoNonConformita.RemoveRange(accertamentoNC);
                                ctx.SaveChanges();

                                //INSERISCO LE NUOVE NC
                                SetNonConformitaAccertamento(ctx, accertamentoInEssere.IDAccertamento, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento);

                                //AGGIORNO L'ACCERTAMENTO CON IL RIFERIMENTO DEL NUOVO RCT ED EVENTUALMENTE DEL NUOVO LIBRETTO
                                accertamentoInEssere.IDRapportoDiControlloTecnicoBase = rapportoInAccertamento.IDRapportoControlloTecnico;
                                accertamentoInEssere.IDLibrettoImpianto = rapportoInAccertamento.IDLibrettoImpianto;

                                ctx.SaveChanges();

                                StoricizzaStatoAccertamento(ctx, accertamentoInEssere, 5805);
                                fAccertamento = true;
                            }
                            else
                            {
                                //NON FACCIO NULLA OVVERO RIMANE IL VECCHIO ACCERTAMENTO
                            }
                        }
                        else
                        {
                            //INSERISCO UN NUOVO ACCERTAMENTO
                            try
                            {
                                object[] getValAccertamentoTerzoResponsabile = new object[7];
                                getValAccertamentoTerzoResponsabile = UtilityLibrettiImpianti.GetDatiTerzoResponsabile(ctx, rapportoInAccertamento.IDLibrettoImpianto);

                                var accertamento = new VER_Accertamento();
                                accertamento.IDTipoAccertamento = iDTipoAccertamento;
                                accertamento.IDIspezione = iDIspezione;
                                accertamento.IDRapportoDiControlloTecnicoBase = iDRapportoControlloTecnico;
                                accertamento.IDTargaturaImpianto = rapportoInAccertamento.IDTargaturaImpianto;
                                accertamento.IDSoggetto = rapportoInAccertamento.IDSoggetto;
                                accertamento.IDLibrettoImpianto = rapportoInAccertamento.IDLibrettoImpianto;
                                accertamento.IDTipologiaCombustibile = rapportoInAccertamento.IDTipologiaCombustibile;
                                accertamento.PotenzaTermicaNominale = rapportoInAccertamento.PotenzaTermicaNominale;

                                accertamento.RagioneSocialeTerzoResponsabile = rapportoInAccertamento.RagioneSocialeTerzoResponsabile;
                                accertamento.PartitaIVATerzoResponsabile = rapportoInAccertamento.PartitaIVATerzoResponsabile;
                                accertamento.IndirizzoTerzoResponsabile = rapportoInAccertamento.IndirizzoTerzoResponsabile;
                                accertamento.CivicoTerzoResponsabile = rapportoInAccertamento.CivicoTerzoResponsabile;
                                accertamento.ComuneTerzoResponsabile = rapportoInAccertamento.ComuneTerzoResponsabile;
                                accertamento.IDProvinciaTerzoResponsabile = rapportoInAccertamento.IDProvinciaTerzoResponsabile;
                                if (getValAccertamentoTerzoResponsabile[6] != null)
                                {
                                    accertamento.CapTerzoResponsabile = getValAccertamentoTerzoResponsabile[6].ToString();
                                }

                                accertamento.Osservazioni = rapportoInAccertamento.Osservazioni;
                                accertamento.Raccomandazioni = rapportoInAccertamento.Raccomandazioni;
                                accertamento.Prescrizioni = rapportoInAccertamento.Prescrizioni;
                                accertamento.fImpiantoFunzionante = rapportoInAccertamento.fImpiantoFunzionante;
                                accertamento.DataControllo = rapportoInAccertamento.DataControllo;

                                accertamento.Prefisso = rapportoInAccertamento.Prefisso;
                                accertamento.CodiceProgressivo = rapportoInAccertamento.CodiceProgressivo;
                                accertamento.GiorniRealizzazioneInterventi = null;
                                accertamento.IDStatoAccertamento = 1;
                                accertamento.DataRilevazione = DateTime.Now;
                                accertamento.IDCodiceCatastale = (int)rapportoInAccertamento.IDCodiceCatastale;
                                accertamento.IDDistributore = null;
                                accertamento.IDUtenteAccertatore = null;
                                accertamento.IDUtenteCoordinatore = null;
                                accertamento.Note = null;
                                accertamento.GuidAccertamento = Guid.NewGuid().ToString();
                                accertamento.fAttivo = true;
                                accertamento.fEmailConfermaAccertamento = false;
                                accertamento.DataInvioEmail = null;
                                accertamento.RispostaEmail = null;

                                ctx.VER_Accertamento.Add(accertamento);
                                ctx.SaveChanges();

                                //Dopo aver salvato posso generare il codice perchè mi baso sull'identity...
                                accertamento.CodiceAccertamento = CalcolaCodiceAccertamento(accertamento);
                                ctx.SaveChanges();
                                accertamento.TestoEmail = GetTestoEmail(ctx, accertamento.IDAccertamento);
                                ctx.SaveChanges();

                                accertamento.PunteggioNCAccertamento = CalcolaPunteggioNCAccertamento(ctx, iDRapportoControlloTecnico, accertamento.Prefisso);
                                ctx.SaveChanges();

                                long iDRapportoControlloLong = long.Parse(accertamento.IDRapportoDiControlloTecnicoBase.ToString());

                                SetNonConformitaAccertamento(ctx, accertamento.IDAccertamento, iDRapportoControlloLong, iDIspezione, iDTipoAccertamento);
                                StoricizzaStatoAccertamento(ctx, accertamento, 5805);
                                fAccertamento = true;

                                string pathAccertamento = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + accertamento.CodiceAccertamento + @"\";
                                UtilityFileSystem.CreateDirectoryIfNotExists(pathAccertamento);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
            else
            {
                #region Ispezione in Accertamento
                using (var ctx = new CriterDataModel())
                {
                    var ispezioneInAccertamento = (from isp in ctx.V_VER_Ispezioni
                                                   join rapp in ctx.VER_IspezioneRapporto on isp.IDIspezione equals rapp.IDIspezione
                                                   where (rapp.IDIspezione == iDIspezione
                                                         //& (//c.Osservazioni != null || 
                                                         //   rapp.Prescrizioni != null ||
                                                         //   rapp.Raccomandazioni != null
                                                         //   //||c.fImpiantoFunzionante == false
                                                         //  )
                                                         )
                                                   select new
                                                   {
                                                       iDSoggetto = isp.IDSoggetto,
                                                       iDTargaturaImpianto = rapp.IDTargaturaImpianto,
                                                       prefisso = rapp.Prefisso,
                                                       codiceProgressivo = rapp.CodiceProgressivo,
                                                       iDCodiceCatastale = rapp.IDCodiceCatastale,
                                                       iDRapportoControlloTecnico = rapp.IDRapportoDiControllo,
                                                       iDLibrettoImpianto = isp.IDLibrettoImpianto,
                                                       iDTipologiaCombustibile = isp.IDTipologiaCombustibile,
                                                       Osservazioni = rapp.Osservazioni,
                                                       Raccomandazioni = rapp.Raccomandazioni,
                                                       Prescrizioni = rapp.Prescrizioni,
                                                       fImpiantoFunzionante = rapp.fImpiantoPuoFunzionare,
                                                       DataControllo = isp.DataIspezione,
                                                       PotenzaTermicaNominale = rapp.PotenzaTermicaNominaleGeneratore
                                                   }
                                                  ).FirstOrDefault();

                    if (ispezioneInAccertamento != null)
                    {
                        try
                        {
                            object[] getValAccertamentoTerzoResponsabile = new object[7];
                            getValAccertamentoTerzoResponsabile = UtilityLibrettiImpianti.GetDatiTerzoResponsabile(ctx, ispezioneInAccertamento.iDLibrettoImpianto);


                            var accertamento = new VER_Accertamento();
                            accertamento.IDTipoAccertamento = iDTipoAccertamento;
                            accertamento.IDRapportoDiControlloTecnicoBase = ispezioneInAccertamento.iDRapportoControlloTecnico;
                            accertamento.IDTargaturaImpianto = ispezioneInAccertamento.iDTargaturaImpianto;
                            accertamento.IDSoggetto = ispezioneInAccertamento.iDSoggetto;
                            accertamento.IDLibrettoImpianto = ispezioneInAccertamento.iDLibrettoImpianto;
                            accertamento.IDTipologiaCombustibile = ispezioneInAccertamento.iDTipologiaCombustibile;
                            accertamento.PotenzaTermicaNominale = ispezioneInAccertamento.PotenzaTermicaNominale;

                            if (getValAccertamentoTerzoResponsabile[0] != null)
                            {
                                accertamento.RagioneSocialeTerzoResponsabile = getValAccertamentoTerzoResponsabile[0].ToString();
                            }
                            if (getValAccertamentoTerzoResponsabile[1] != null)
                            {
                                accertamento.PartitaIVATerzoResponsabile = getValAccertamentoTerzoResponsabile[1].ToString();
                            }
                            if (getValAccertamentoTerzoResponsabile[2] != null)
                            {
                                accertamento.IndirizzoTerzoResponsabile = getValAccertamentoTerzoResponsabile[2].ToString();
                            }
                            if (getValAccertamentoTerzoResponsabile[3] != null)
                            {
                                accertamento.CivicoTerzoResponsabile = getValAccertamentoTerzoResponsabile[3].ToString(); ;
                            }
                            if (getValAccertamentoTerzoResponsabile[4] != null)
                            {
                                accertamento.ComuneTerzoResponsabile = getValAccertamentoTerzoResponsabile[4].ToString();
                            }
                            if (getValAccertamentoTerzoResponsabile[5] != null)
                            {
                                accertamento.IDProvinciaTerzoResponsabile = int.Parse(getValAccertamentoTerzoResponsabile[5].ToString());
                            }
                            if (getValAccertamentoTerzoResponsabile[6] != null)
                            {
                                accertamento.CapTerzoResponsabile = getValAccertamentoTerzoResponsabile[6].ToString();
                            }

                            accertamento.Osservazioni = ispezioneInAccertamento.Osservazioni;
                            accertamento.Raccomandazioni = ispezioneInAccertamento.Raccomandazioni;
                            accertamento.Prescrizioni = ispezioneInAccertamento.Prescrizioni;
                            accertamento.fImpiantoFunzionante = ispezioneInAccertamento.fImpiantoFunzionante;
                            accertamento.DataControllo = ispezioneInAccertamento.DataControllo;

                            accertamento.Prefisso = ispezioneInAccertamento.prefisso;
                            accertamento.CodiceProgressivo = (int)ispezioneInAccertamento.codiceProgressivo;
                            accertamento.GiorniRealizzazioneInterventi = null;
                            accertamento.IDStatoAccertamento = 1;
                            accertamento.DataRilevazione = DateTime.Now;
                            accertamento.IDCodiceCatastale = (int)ispezioneInAccertamento.iDCodiceCatastale;
                            accertamento.IDDistributore = null;
                            accertamento.IDUtenteAccertatore = null;
                            accertamento.IDUtenteCoordinatore = null;
                            accertamento.Note = null;
                            accertamento.GuidAccertamento = Guid.NewGuid().ToString();
                            accertamento.fAttivo = true;
                            accertamento.fEmailConfermaAccertamento = false;
                            accertamento.DataInvioEmail = null;
                            accertamento.RispostaEmail = null;
                            accertamento.IDIspezione = iDIspezione;

                            ctx.VER_Accertamento.Add(accertamento);
                            ctx.SaveChanges();

                            //Dopo aver salvato posso generare il codice perchè mi baso sull'identity...
                            accertamento.CodiceAccertamento = CalcolaCodiceAccertamento(accertamento);
                            ctx.SaveChanges();
                            accertamento.TestoEmail = GetTestoEmail(ctx, accertamento.IDAccertamento);
                            ctx.SaveChanges();

                            SetNonConformitaAccertamento(ctx, accertamento.IDAccertamento, accertamento.IDRapportoDiControlloTecnicoBase, iDIspezione, iDTipoAccertamento);
                            StoricizzaStatoAccertamento(ctx, accertamento, 5805);
                            fAccertamento = true;

                            string pathAccertamento = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + accertamento.CodiceAccertamento + @"\";
                            UtilityFileSystem.CreateDirectoryIfNotExists(pathAccertamento);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                #endregion
            }

            return fAccertamento;
        }

        public static int? CalcolaPunteggioNCAccertamento(CriterDataModel ctx, long? iDRapportoControlloTecnico, string tipoRct)
        {
            int? punteggio = null;
            switch (tipoRct)
            {
                case "GT":
                    var RctGT = (from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                                 join RCT_RapportoDiControlloTecnicoGT in ctx.RCT_RapportoDiControlloTecnicoGT on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoGT.Id }
                                 where RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                 select new
                                 {
                                     RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo,
                                     RCT_RapportoDiControlloTecnicoBase.fDichiarazioneConformita,
                                     RCT_RapportoDiControlloTecnicoBase.fUsoManutenzioneGeneratore,
                                     RCT_RapportoDiControlloTecnicoBase.fLibrettoImpiantoCompilato,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoRiscaldamento,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoACS,
                                     RCT_RapportoDiControlloTecnicoBase.LocaleInstallazioneIdoneo,
                                     RCT_RapportoDiControlloTecnicoGT.GeneratoriIdonei,
                                     RCT_RapportoDiControlloTecnicoBase.DimensioniApertureAdeguate,
                                     RCT_RapportoDiControlloTecnicoBase.ApertureLibere,
                                     RCT_RapportoDiControlloTecnicoBase.ScarichiIdonei,
                                     RCT_RapportoDiControlloTecnicoBase.LineeElettricheIdonee,
                                     RCT_RapportoDiControlloTecnicoGT.RegolazioneTemperaturaAmbiente,
                                     RCT_RapportoDiControlloTecnicoBase.AssenzaPerditeCombustibile,
                                     RCT_RapportoDiControlloTecnicoBase.TenutaImpiantoIdraulico,
                                     RCT_RapportoDiControlloTecnicoGT.DepressioneCanaleFumo,
                                     RCT_RapportoDiControlloTecnicoGT.DispositiviComandoRegolazione,
                                     RCT_RapportoDiControlloTecnicoGT.DispositiviSicurezza,
                                     RCT_RapportoDiControlloTecnicoGT.ValvolaSicurezzaSovrappressione,
                                     RCT_RapportoDiControlloTecnicoGT.ScambiatoreFumiPulito,
                                     RCT_RapportoDiControlloTecnicoGT.RiflussoProdottiCombustione,
                                     RCT_RapportoDiControlloTecnicoGT.ConformitaUNI10389,
                                     RCT_RapportoDiControlloTecnicoGT.CoCorretto,
                                     RCT_RapportoDiControlloTecnicoGT.RendimentoCombustione,
                                     RCT_RapportoDiControlloTecnicoGT.RendimentoMinimo,
                                     RCT_RapportoDiControlloTecnicoGT.RispettaIndiceBacharach,
                                     RCT_RapportoDiControlloTecnicoGT.COFumiSecchiNoAria1000,
                                     RCT_RapportoDiControlloTecnicoGT.RendimentoSupMinimo,
                                     RCT_RapportoDiControlloTecnicoBase.CoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.StatoCoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.Contabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Termoregolazione,
                                     RCT_RapportoDiControlloTecnicoBase.CorrettoFunzionamentoContabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Prescrizioni,
                                     RCT_RapportoDiControlloTecnicoBase.Raccomandazioni,
                                     RCT_RapportoDiControlloTecnicoBase.fImpiantoFunzionante,
                                     RCT_RapportoDiControlloTecnicoBase.IDTipologiaCombustibile
                                 }).FirstOrDefault();

                    if (RctGT.fImpiantoFunzionante == false)
                    {
                        punteggio = 1;
                    }
                    else if (!string.IsNullOrEmpty(RctGT.Prescrizioni))
                    {
                        punteggio = 2;
                    }
                    else if (RctGT.RiflussoProdottiCombustione == 1)
                    {
                        punteggio = 3;
                    }
                    else if (RctGT.DepressioneCanaleFumo < 3 && RctGT.IDTipologiaControllo == 1)
                    {
                        punteggio = 3;
                    }
                    else if (RctGT.TenutaImpiantoIdraulico == 0)
                    {
                        punteggio = 3;
                    }
                    //else if (RctGT.AssenzaPerditeCombustibile == 0)
                    else if (RctGT.AssenzaPerditeCombustibile == 0 && (RctGT.IDTipologiaCombustibile == 4 || RctGT.IDTipologiaCombustibile == 5))
                    {
                        punteggio = 3;
                    }
                    else if (RctGT.ApertureLibere == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctGT.COFumiSecchiNoAria1000 == false)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.CoCorretto >= 1000)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.ScambiatoreFumiPulito == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.ValvolaSicurezzaSovrappressione == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.DispositiviSicurezza == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.DispositiviComandoRegolazione == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.ScarichiIdonei == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.DimensioniApertureAdeguate == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.GeneratoriIdonei == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGT.LocaleInstallazioneIdoneo == 0)
                    {
                        punteggio = 4;
                    }
                    else if (!string.IsNullOrEmpty(RctGT.Raccomandazioni))
                    {
                        punteggio = 5;
                    }
                    else if (RctGT.CorrettoFunzionamentoContabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.CorrettoFunzionamentoContabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.Termoregolazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.Contabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.RendimentoSupMinimo == false)
                    {
                        punteggio = 6;
                    }
                    //else if (RctGT.RispettaIndiceBacharach == false)
                    else if (RctGT.RispettaIndiceBacharach == false && RctGT.IDTipologiaControllo == 1 && (RctGT.IDTipologiaCombustibile == 4 || RctGT.IDTipologiaCombustibile == 5))
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.RendimentoCombustione < RctGT.RendimentoMinimo)
                    {
                        punteggio = 6;
                    }
                    //else if (RctGT.ConformitaUNI10389 == 0)
                    else if (RctGT.ConformitaUNI10389 == 0 && RctGT.IDTipologiaControllo == 1)
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.RegolazioneTemperaturaAmbiente == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.RegolazioneTemperaturaAmbiente == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGT.TrattamentoACS == 0)
                    {
                        punteggio = 7;
                    }
                    else if (RctGT.TrattamentoRiscaldamento == 0)
                    {
                        punteggio = 7;
                    }
                    else if (RctGT.fLibrettoImpiantoCompilato == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctGT.fUsoManutenzioneGeneratore == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctGT.fDichiarazioneConformita == false)
                    {
                        punteggio = 8;
                    }
                    break;
                case "GF":
                    var RctGF = (from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                                 join RCT_RapportoDiControlloTecnicoGF in ctx.RCT_RapportoDiControlloTecnicoGF on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoGF.Id }
                                 where RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                 select new
                                 {
                                     RCT_RapportoDiControlloTecnicoBase.fDichiarazioneConformita,
                                     RCT_RapportoDiControlloTecnicoBase.fUsoManutenzioneGeneratore,
                                     RCT_RapportoDiControlloTecnicoBase.fLibrettoImpiantoCompilato,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoRiscaldamento,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoACS,
                                     RCT_RapportoDiControlloTecnicoBase.LocaleInstallazioneIdoneo,
                                     RCT_RapportoDiControlloTecnicoBase.DimensioniApertureAdeguate,
                                     RCT_RapportoDiControlloTecnicoBase.ApertureLibere,
                                     RCT_RapportoDiControlloTecnicoBase.ScarichiIdonei,
                                     RCT_RapportoDiControlloTecnicoBase.LineeElettricheIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.AssenzaPerditeCombustibile,
                                     RCT_RapportoDiControlloTecnicoBase.TenutaImpiantoIdraulico,
                                     RCT_RapportoDiControlloTecnicoBase.CoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.StatoCoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoGF.AssenzaPerditeRefrigerante,
                                     RCT_RapportoDiControlloTecnicoGF.FiltriPuliti,
                                     RCT_RapportoDiControlloTecnicoGF.LeakDetector,
                                     RCT_RapportoDiControlloTecnicoGF.ScambiatoriLiberi,
                                     RCT_RapportoDiControlloTecnicoGF.ParametriTermodinamici,
                                     RCT_RapportoDiControlloTecnicoBase.Contabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Termoregolazione,
                                     RCT_RapportoDiControlloTecnicoBase.CorrettoFunzionamentoContabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Prescrizioni,
                                     RCT_RapportoDiControlloTecnicoBase.Raccomandazioni,
                                     RCT_RapportoDiControlloTecnicoBase.fImpiantoFunzionante
                                 }).FirstOrDefault();

                    if (RctGF.fImpiantoFunzionante == false)
                    {
                        punteggio = 1;
                    }
                    else if (!string.IsNullOrEmpty(RctGF.Prescrizioni))
                    {
                        punteggio = 2;
                    }
                    else if (RctGF.AssenzaPerditeRefrigerante == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctGF.CoibentazioniIdonee == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctGF.LineeElettricheIdonee == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctGF.ApertureLibere == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctGF.DimensioniApertureAdeguate == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctGF.LocaleInstallazioneIdoneo == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctGF.ParametriTermodinamici == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGF.ScambiatoriLiberi == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGF.LeakDetector == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctGF.FiltriPuliti == 0)
                    {
                        punteggio = 4;
                    }
                    else if (!string.IsNullOrEmpty(RctGF.Raccomandazioni))
                    {
                        punteggio = 5;
                    }
                    else if (RctGF.CorrettoFunzionamentoContabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGF.Termoregolazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGF.Contabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctGF.TrattamentoRiscaldamento == 0)
                    {
                        punteggio = 7;
                    }
                    else if (RctGF.fLibrettoImpiantoCompilato == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctGF.fUsoManutenzioneGeneratore == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctGF.fDichiarazioneConformita == false)
                    {
                        punteggio = 8;
                    }
                    break;
                case "SC":
                    var RctSC = (from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                                 join RCT_RapportoDiControlloTecnicoSC in ctx.RCT_RapportoDiControlloTecnicoSC on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoSC.Id }
                                 where RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                 select new
                                 {
                                     RCT_RapportoDiControlloTecnicoBase.fDichiarazioneConformita,
                                     RCT_RapportoDiControlloTecnicoBase.fUsoManutenzioneGeneratore,
                                     RCT_RapportoDiControlloTecnicoBase.fLibrettoImpiantoCompilato,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoRiscaldamento,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoACS,
                                     RCT_RapportoDiControlloTecnicoBase.LocaleInstallazioneIdoneo,
                                     RCT_RapportoDiControlloTecnicoBase.DimensioniApertureAdeguate,
                                     RCT_RapportoDiControlloTecnicoBase.ApertureLibere,
                                     RCT_RapportoDiControlloTecnicoBase.ScarichiIdonei,
                                     RCT_RapportoDiControlloTecnicoBase.LineeElettricheIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.AssenzaPerditeCombustibile,
                                     RCT_RapportoDiControlloTecnicoBase.TenutaImpiantoIdraulico,
                                     RCT_RapportoDiControlloTecnicoSC.DispositiviComandoRegolazione,
                                     RCT_RapportoDiControlloTecnicoBase.CoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.StatoCoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoSC.PotenzaCompatibileProgetto,
                                     RCT_RapportoDiControlloTecnicoSC.AssenzaTrafilamenti,
                                     RCT_RapportoDiControlloTecnicoBase.Contabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Termoregolazione,
                                     RCT_RapportoDiControlloTecnicoBase.CorrettoFunzionamentoContabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Prescrizioni,
                                     RCT_RapportoDiControlloTecnicoBase.Raccomandazioni,
                                     RCT_RapportoDiControlloTecnicoBase.fImpiantoFunzionante
                                 }).FirstOrDefault();

                    if (RctSC.fImpiantoFunzionante == false)
                    {
                        punteggio = 1;
                    }
                    else if (!string.IsNullOrEmpty(RctSC.Prescrizioni))
                    {
                        punteggio = 2;
                    }
                    else if (RctSC.TenutaImpiantoIdraulico == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctSC.StatoCoibentazioniIdonee == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctSC.LineeElettricheIdonee == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctSC.LocaleInstallazioneIdoneo == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctSC.AssenzaTrafilamenti == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctSC.StatoCoibentazioniIdonee == 0)
                    {
                        punteggio = 4;
                    }
                    else if (RctSC.PotenzaCompatibileProgetto == 0)
                    {
                        punteggio = 4;
                    }
                    else if (!string.IsNullOrEmpty(RctSC.Raccomandazioni))
                    {
                        punteggio = 5;
                    }
                    else if (RctSC.CorrettoFunzionamentoContabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctSC.Termoregolazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctSC.Contabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctSC.TrattamentoACS == 0)
                    {
                        punteggio = 7;
                    }
                    else if (RctSC.TrattamentoRiscaldamento == 0)
                    {
                        punteggio = 7;
                    }
                    else if (RctSC.fLibrettoImpiantoCompilato == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctSC.fUsoManutenzioneGeneratore == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctSC.fDichiarazioneConformita == false)
                    {
                        punteggio = 8;
                    }
                    break;
                case "CG":
                    var RctCG = (from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                                 join RCT_RapportoDiControlloTecnicoCG in ctx.RCT_RapportoDiControlloTecnicoCG on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoCG.Id }
                                 where RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                 select new
                                 {
                                     RCT_RapportoDiControlloTecnicoBase.fDichiarazioneConformita,
                                     RCT_RapportoDiControlloTecnicoBase.fUsoManutenzioneGeneratore,
                                     RCT_RapportoDiControlloTecnicoBase.fLibrettoImpiantoCompilato,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoRiscaldamento,
                                     RCT_RapportoDiControlloTecnicoBase.TrattamentoACS,
                                     RCT_RapportoDiControlloTecnicoBase.LocaleInstallazioneIdoneo,
                                     RCT_RapportoDiControlloTecnicoBase.DimensioniApertureAdeguate,
                                     RCT_RapportoDiControlloTecnicoBase.ApertureLibere,
                                     RCT_RapportoDiControlloTecnicoBase.ScarichiIdonei,
                                     RCT_RapportoDiControlloTecnicoBase.LineeElettricheIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.AssenzaPerditeCombustibile,
                                     RCT_RapportoDiControlloTecnicoBase.TenutaImpiantoIdraulico,
                                     RCT_RapportoDiControlloTecnicoCG.CapsulaInsonorizzataIdonea,
                                     RCT_RapportoDiControlloTecnicoCG.TenutaCircuitoOlioIdonea,
                                     RCT_RapportoDiControlloTecnicoBase.CoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoBase.StatoCoibentazioniIdonee,
                                     RCT_RapportoDiControlloTecnicoCG.FunzionalitàScambiatoreSeparazione,
                                     RCT_RapportoDiControlloTecnicoBase.Contabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Termoregolazione,
                                     RCT_RapportoDiControlloTecnicoBase.CorrettoFunzionamentoContabilizzazione,
                                     RCT_RapportoDiControlloTecnicoBase.Prescrizioni,
                                     RCT_RapportoDiControlloTecnicoBase.Raccomandazioni,
                                     RCT_RapportoDiControlloTecnicoBase.fImpiantoFunzionante
                                 }).FirstOrDefault();

                    if (RctCG.fImpiantoFunzionante == false)
                    {
                        punteggio = 1;
                    }
                    else if (!string.IsNullOrEmpty(RctCG.Prescrizioni))
                    {
                        punteggio = 2;
                    }
                    else if (RctCG.FunzionalitàScambiatoreSeparazione == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.AssenzaPerditeCombustibile == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.TenutaCircuitoOlioIdonea == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.TenutaImpiantoIdraulico == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.CapsulaInsonorizzataIdonea == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.ScarichiIdonei == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.LineeElettricheIdonee == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.ApertureLibere == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.DimensioniApertureAdeguate == 0)
                    {
                        punteggio = 3;
                    }
                    else if (RctCG.LocaleInstallazioneIdoneo == 0)
                    {
                        punteggio = 3;
                    }
                    else if (!string.IsNullOrEmpty(RctCG.Raccomandazioni))
                    {
                        punteggio = 5;
                    }
                    else if (RctCG.CorrettoFunzionamentoContabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctCG.Termoregolazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctCG.Contabilizzazione == 0)
                    {
                        punteggio = 6;
                    }
                    else if (RctCG.TrattamentoACS == 0)
                    {
                        punteggio = 7;
                    }
                    else if (RctCG.TrattamentoRiscaldamento == 0)
                    {
                        punteggio = 7;
                    }
                    else if (RctCG.fLibrettoImpiantoCompilato == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctCG.fUsoManutenzioneGeneratore == false)
                    {
                        punteggio = 8;
                    }
                    else if (RctCG.fDichiarazioneConformita == false)
                    {
                        punteggio = 8;
                    }
                    break;
            }

            return punteggio;
        }

        public static string GetTestoEmail(CriterDataModel ctx, long iDAccertamento)
        {
            string email = string.Empty;

            var accertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();

            if (accertamento != null)
            {
                string url = ConfigurationManager.AppSettings["UrlPortal"] + "ConfermaAccertamento.aspx?guidAccertamento=" + accertamento.GuidAccertamento;

                email += "Pregiatissimo " + accertamento.NomeLegaleRappresentante.ToString() + " " + accertamento.CognomeLegaleRappresentante.ToString() + " legale rappresentante della Ditta " + accertamento.NomeAzienda.ToString() + "\n";
                email += "Vi comunichiamo che è stato effettuato l’accertamento n. " + accertamento.CodiceAccertamento.ToString() + " del " + string.Format("{0:dd/MM/yyyy}", accertamento.DataRilevazione) + " sul rapporto di controllo del " + string.Format("{0:dd/MM/yyyy}", accertamento.DataControllo) + " da Voi registrato con riferimento all’impianto targa " + accertamento.CodiceTargatura.ToString() + " installato nell’unità immobiliare di Via " + accertamento.Indirizzo.ToString() + ", " + accertamento.Comune.ToString() + " (" + accertamento.SiglaProvincia.ToString() + ") di cui risulta Responsabile " + accertamento.NomeResponsabile.ToString() + " " + accertamento.CognomeResponsabile.ToString() + ", codice fiscale " + accertamento.CodiceFiscaleResponsabile.ToString() + ".\n";
                email += "Vi comunichiamo altresì che ai sensi delle disposizioni del Regolamento Regionale n. 1 del 3 aprile 2017 e s.m. verrà inviata al Responsabile di Impianto una “segnalazione di non conformità” riportante le seguenti indicazioni, definite sulla base dei contenuti del Rapporto di Controllo sopra indicato e dei successivi contatti intercorsi\n\n";
                email += "- PRESCRIZIONI\n";
                email += "- RACCOMANDAZIONI\n\n";
                email += "Salvo Vostre diverse indicazioni che Vi chiediamo di comunicarci entro e non oltre le prossime 24 ore, termine oltre il quale consideriamo asseverate tali indicazioni.\n";
                email += "Nel caso sia necessario comunicarci indicazioni diverse da quelle sopra riportate, cliccate " + url + "\n\n";
                email += "Cordiali saluti.\n";
                email += "Organismo Regionale di Accreditamento ed Ispezione";
            }

            return email;
        }

        public static void SetNonConformitaAccertamento(CriterDataModel ctx, long iDAccertamento, long? iDRapportoControlloTecnico, long? iDIspezione, int iDTipoAccertamento)
        {
            int? iDProceduraAccertamento = null;

            if (iDTipoAccertamento == 1)
            {
                #region Accertamento da Rapporto di Controllo
                int iDRapportoControlloTecnicoInt = int.Parse(iDRapportoControlloTecnico.ToString());
                var rapporto = (from a in ctx.RCT_RapportoDiControlloTecnicoBase
                                where a.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                select a
                        ).FirstOrDefault();

                #region Raccomandazioni
                var raccomandazioniRct = UtilityRapportiControllo.GetValoriRCTRaccomandazioniPrescrizioni(iDRapportoControlloTecnicoInt, null, "Raccomandazioni");
                if (raccomandazioniRct.Count > 0)
                {
                    iDProceduraAccertamento = GetProcedureAccertamento(ctx, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, "RACC");
                }
                else
                {
                    iDProceduraAccertamento = null;
                }

                string raccomandazioniLibere = rapporto.Raccomandazioni;
                foreach (var row in raccomandazioniRct)
                {
                    var raccomandazioni = ctx.SYS_RCTTipologiaRaccomandazione.Where(c => c.IDTipologiaRaccomandazione == row.IDTipologiaRaccomandazione).FirstOrDefault();
                    if (!string.IsNullOrEmpty(raccomandazioniLibere))
                    {
                        raccomandazioniLibere = raccomandazioniLibere.Replace(raccomandazioni.Raccomandazione, "");
                    }

                    try
                    {
                        var nonConformitaRaccomandazioni = new VER_AccertamentoNonConformita();
                        nonConformitaRaccomandazioni.IDAccertamento = iDAccertamento;
                        nonConformitaRaccomandazioni.Tipo = "RACC";
                        nonConformitaRaccomandazioni.IDProceduraAccertamento = iDProceduraAccertamento;
                        nonConformitaRaccomandazioni.fRaccomandazioneConferma = false;
                        nonConformitaRaccomandazioni.RaccomandazioneRct = raccomandazioni.Raccomandazione;
                        nonConformitaRaccomandazioni.Raccomandazione = null;

                        nonConformitaRaccomandazioni.PrescrizioneRct = null;
                        nonConformitaRaccomandazioni.fPrescrizioneConferma = false;
                        nonConformitaRaccomandazioni.Prescrizione = null;
                        nonConformitaRaccomandazioni.OsservazioneRct = null;
                        nonConformitaRaccomandazioni.fOsservazioneConferma = false;
                        nonConformitaRaccomandazioni.Osservazione = null;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteRct = false;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaRaccomandazioni.ImpiantoFunzionante = null;
                        nonConformitaRaccomandazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaRaccomandazioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaRaccomandazioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaRaccomandazioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaRaccomandazioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }

                #region Raccomandazione libera
                if (!string.IsNullOrEmpty(raccomandazioniLibere))
                {
                    try
                    {
                        var nonConformitaRaccomandazioni = new VER_AccertamentoNonConformita();
                        nonConformitaRaccomandazioni.IDAccertamento = iDAccertamento;
                        nonConformitaRaccomandazioni.Tipo = "RACC";
                        nonConformitaRaccomandazioni.IDProceduraAccertamento = iDProceduraAccertamento;
                        nonConformitaRaccomandazioni.fRaccomandazioneConferma = false;
                        nonConformitaRaccomandazioni.RaccomandazioneRct = raccomandazioniLibere;
                        nonConformitaRaccomandazioni.Raccomandazione = null;

                        nonConformitaRaccomandazioni.PrescrizioneRct = null;
                        nonConformitaRaccomandazioni.fPrescrizioneConferma = false;
                        nonConformitaRaccomandazioni.Prescrizione = null;
                        nonConformitaRaccomandazioni.OsservazioneRct = null;
                        nonConformitaRaccomandazioni.fOsservazioneConferma = false;
                        nonConformitaRaccomandazioni.Osservazione = null;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteRct = false;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaRaccomandazioni.ImpiantoFunzionante = null;
                        nonConformitaRaccomandazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaRaccomandazioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaRaccomandazioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaRaccomandazioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaRaccomandazioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
                #endregion

                #endregion

                #region Osservazioni
                if (!string.IsNullOrEmpty(rapporto.Osservazioni))
                {
                    try
                    {
                        var nonConformitaOsservazioni = new VER_AccertamentoNonConformita();
                        nonConformitaOsservazioni.IDAccertamento = iDAccertamento;
                        nonConformitaOsservazioni.Tipo = "OSS";
                        nonConformitaOsservazioni.IDProceduraAccertamento = null;
                        nonConformitaOsservazioni.fRaccomandazioneConferma = false;
                        nonConformitaOsservazioni.RaccomandazioneRct = null;
                        nonConformitaOsservazioni.Raccomandazione = null;

                        nonConformitaOsservazioni.PrescrizioneRct = null;
                        nonConformitaOsservazioni.fPrescrizioneConferma = false;
                        nonConformitaOsservazioni.Prescrizione = null;
                        nonConformitaOsservazioni.OsservazioneRct = rapporto.Osservazioni;
                        nonConformitaOsservazioni.fOsservazioneConferma = false;
                        nonConformitaOsservazioni.Osservazione = null;
                        nonConformitaOsservazioni.fImpiantoFunzionanteRct = false;
                        nonConformitaOsservazioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaOsservazioni.ImpiantoFunzionante = null;
                        nonConformitaOsservazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaOsservazioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaOsservazioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaOsservazioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaOsservazioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
                #endregion

                #region Prescrizioni
                var prescrizioniRct = UtilityRapportiControllo.GetValoriRCTRaccomandazioniPrescrizioni(iDRapportoControlloTecnicoInt, null, "Prescrizioni");
                if (prescrizioniRct.Count > 0)
                {
                    iDProceduraAccertamento = GetProcedureAccertamento(ctx, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, "PRES");
                }
                else
                {
                    iDProceduraAccertamento = null;
                }

                foreach (var row in prescrizioniRct)
                {
                    var prescrizioni = ctx.SYS_RCTTipologiaPrescrizione.Where(c => c.IDTipologiaPrescrizione == row.IDTipologiaPrescrizione).FirstOrDefault();
                    try
                    {
                        var nonConformitaPrescrizioni = new VER_AccertamentoNonConformita();
                        nonConformitaPrescrizioni.IDAccertamento = iDAccertamento;
                        nonConformitaPrescrizioni.Tipo = "PRES";
                        nonConformitaPrescrizioni.IDProceduraAccertamento = iDProceduraAccertamento;
                        nonConformitaPrescrizioni.fRaccomandazioneConferma = false;
                        nonConformitaPrescrizioni.RaccomandazioneRct = null;
                        nonConformitaPrescrizioni.Raccomandazione = null;

                        nonConformitaPrescrizioni.PrescrizioneRct = prescrizioni.Prescrizione;
                        nonConformitaPrescrizioni.fPrescrizioneConferma = false;
                        nonConformitaPrescrizioni.Prescrizione = null;
                        nonConformitaPrescrizioni.OsservazioneRct = null;
                        nonConformitaPrescrizioni.fOsservazioneConferma = false;
                        nonConformitaPrescrizioni.Osservazione = null;
                        nonConformitaPrescrizioni.fImpiantoFunzionanteRct = false;
                        nonConformitaPrescrizioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaPrescrizioni.ImpiantoFunzionante = null;
                        nonConformitaPrescrizioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaPrescrizioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaPrescrizioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaPrescrizioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaPrescrizioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
                #endregion

                #region Impianto non funzionante
                try
                {
                    var nonConformitaImpiantoNonFunzionante = new VER_AccertamentoNonConformita();
                    nonConformitaImpiantoNonFunzionante.IDAccertamento = iDAccertamento;
                    nonConformitaImpiantoNonFunzionante.Tipo = "INF";
                    nonConformitaImpiantoNonFunzionante.IDProceduraAccertamento = iDProceduraAccertamento;
                    nonConformitaImpiantoNonFunzionante.fRaccomandazioneConferma = false;
                    nonConformitaImpiantoNonFunzionante.RaccomandazioneRct = null;
                    nonConformitaImpiantoNonFunzionante.Raccomandazione = null;

                    nonConformitaImpiantoNonFunzionante.PrescrizioneRct = null;
                    nonConformitaImpiantoNonFunzionante.fPrescrizioneConferma = false;
                    nonConformitaImpiantoNonFunzionante.Prescrizione = null;
                    nonConformitaImpiantoNonFunzionante.OsservazioneRct = null;
                    nonConformitaImpiantoNonFunzionante.fOsservazioneConferma = false;
                    nonConformitaImpiantoNonFunzionante.Osservazione = null;
                    nonConformitaImpiantoNonFunzionante.fImpiantoFunzionanteRct = rapporto.fImpiantoFunzionante;
                    nonConformitaImpiantoNonFunzionante.fImpiantoFunzionanteConferma = true;
                    nonConformitaImpiantoNonFunzionante.ImpiantoFunzionante = null;
                    nonConformitaImpiantoNonFunzionante.IDTipologiaImpiantoFunzionanteAccertamento = null;
                    nonConformitaImpiantoNonFunzionante.IDTipologiaRisoluzioneAccertamento = 1;
                    nonConformitaImpiantoNonFunzionante.IDTipologiaEventoAccertamento = null;
                    nonConformitaImpiantoNonFunzionante.Giorni = null;

                    ctx.VER_AccertamentoNonConformita.Add(nonConformitaImpiantoNonFunzionante);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {

                }
                #endregion
                #endregion               
            }
            else
            {
                #region Accertamento da Rapporto di Ispezione
                var rapportoIspezione = ctx.VER_IspezioneRapporto.Where(a => a.IDIspezione == iDIspezione).FirstOrDefault();

                #region Raccomandazioni
                var raccomandazioniIspezione = GetValoriIspezioneRaccomandazioniPrescrizioni(rapportoIspezione.IDIspezione, null, "Raccomandazioni");
                if (raccomandazioniIspezione.Count > 0)
                {
                    iDProceduraAccertamento = GetProcedureAccertamento(ctx, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, "RACC");
                }
                else
                {
                    iDProceduraAccertamento = null;
                }

                string raccomandazioniLibere = rapportoIspezione.Raccomandazioni;
                foreach (var row in raccomandazioniIspezione)
                {
                    var raccomandazioni = ctx.SYS_RCTTipologiaRaccomandazione.Where(c => c.IDTipologiaRaccomandazione == row.IDTipologiaRaccomandazioneIspezione).FirstOrDefault();
                    if (!string.IsNullOrEmpty(raccomandazioniLibere))
                    {
                        raccomandazioniLibere = raccomandazioniLibere.Replace(raccomandazioni.Raccomandazione, "");
                    }

                    try
                    {
                        var nonConformitaRaccomandazioni = new VER_AccertamentoNonConformita();
                        nonConformitaRaccomandazioni.IDAccertamento = iDAccertamento;
                        nonConformitaRaccomandazioni.Tipo = "RACC";
                        nonConformitaRaccomandazioni.IDProceduraAccertamento = iDProceduraAccertamento;
                        nonConformitaRaccomandazioni.fRaccomandazioneConferma = false;
                        nonConformitaRaccomandazioni.RaccomandazioneRct = raccomandazioni.Raccomandazione;
                        nonConformitaRaccomandazioni.Raccomandazione = null;

                        nonConformitaRaccomandazioni.PrescrizioneRct = null;
                        nonConformitaRaccomandazioni.fPrescrizioneConferma = false;
                        nonConformitaRaccomandazioni.Prescrizione = null;
                        nonConformitaRaccomandazioni.OsservazioneRct = null;
                        nonConformitaRaccomandazioni.fOsservazioneConferma = false;
                        nonConformitaRaccomandazioni.Osservazione = null;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteRct = false;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaRaccomandazioni.ImpiantoFunzionante = null;
                        nonConformitaRaccomandazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaRaccomandazioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaRaccomandazioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaRaccomandazioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaRaccomandazioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }

                #region Raccomandazione libera
                if (!string.IsNullOrEmpty(raccomandazioniLibere))
                {
                    try
                    {
                        var nonConformitaRaccomandazioni = new VER_AccertamentoNonConformita();
                        nonConformitaRaccomandazioni.IDAccertamento = iDAccertamento;
                        nonConformitaRaccomandazioni.Tipo = "RACC";
                        nonConformitaRaccomandazioni.IDProceduraAccertamento = iDProceduraAccertamento;
                        nonConformitaRaccomandazioni.fRaccomandazioneConferma = false;
                        nonConformitaRaccomandazioni.RaccomandazioneRct = raccomandazioniLibere;
                        nonConformitaRaccomandazioni.Raccomandazione = null;

                        nonConformitaRaccomandazioni.PrescrizioneRct = null;
                        nonConformitaRaccomandazioni.fPrescrizioneConferma = false;
                        nonConformitaRaccomandazioni.Prescrizione = null;
                        nonConformitaRaccomandazioni.OsservazioneRct = null;
                        nonConformitaRaccomandazioni.fOsservazioneConferma = false;
                        nonConformitaRaccomandazioni.Osservazione = null;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteRct = false;
                        nonConformitaRaccomandazioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaRaccomandazioni.ImpiantoFunzionante = null;
                        nonConformitaRaccomandazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaRaccomandazioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaRaccomandazioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaRaccomandazioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaRaccomandazioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
                #endregion

                #endregion

                #region Osservazioni
                if (!string.IsNullOrEmpty(rapportoIspezione.Osservazioni))
                {
                    try
                    {
                        var nonConformitaOsservazioni = new VER_AccertamentoNonConformita();
                        nonConformitaOsservazioni.IDAccertamento = iDAccertamento;
                        nonConformitaOsservazioni.Tipo = "OSS";
                        nonConformitaOsservazioni.IDProceduraAccertamento = null;
                        nonConformitaOsservazioni.fRaccomandazioneConferma = false;
                        nonConformitaOsservazioni.RaccomandazioneRct = null;
                        nonConformitaOsservazioni.Raccomandazione = null;

                        nonConformitaOsservazioni.PrescrizioneRct = null;
                        nonConformitaOsservazioni.fPrescrizioneConferma = false;
                        nonConformitaOsservazioni.Prescrizione = null;
                        nonConformitaOsservazioni.OsservazioneRct = rapportoIspezione.Osservazioni;
                        nonConformitaOsservazioni.fOsservazioneConferma = false;
                        nonConformitaOsservazioni.Osservazione = null;
                        nonConformitaOsservazioni.fImpiantoFunzionanteRct = false;
                        nonConformitaOsservazioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaOsservazioni.ImpiantoFunzionante = null;
                        nonConformitaOsservazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaOsservazioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaOsservazioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaOsservazioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaOsservazioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
                #endregion

                #region Prescrizioni
                var prescrizioniIspezione = GetValoriIspezioneRaccomandazioniPrescrizioni(rapportoIspezione.IDIspezione, null, "Prescrizioni");
                if (prescrizioniIspezione.Count > 0)
                {
                    iDProceduraAccertamento = GetProcedureAccertamento(ctx, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, "PRES");
                }
                else
                {
                    iDProceduraAccertamento = null;
                }

                foreach (var row in prescrizioniIspezione)
                {
                    var prescrizioni = ctx.SYS_RCTTipologiaPrescrizione.Where(c => c.IDTipologiaPrescrizione == row.IDTipologiaPrescrizioneIspezione).FirstOrDefault();
                    try
                    {
                        var nonConformitaPrescrizioni = new VER_AccertamentoNonConformita();
                        nonConformitaPrescrizioni.IDAccertamento = iDAccertamento;
                        nonConformitaPrescrizioni.Tipo = "PRES";
                        nonConformitaPrescrizioni.IDProceduraAccertamento = iDProceduraAccertamento;
                        nonConformitaPrescrizioni.fRaccomandazioneConferma = false;
                        nonConformitaPrescrizioni.RaccomandazioneRct = null;
                        nonConformitaPrescrizioni.Raccomandazione = null;

                        nonConformitaPrescrizioni.PrescrizioneRct = prescrizioni.Prescrizione;
                        nonConformitaPrescrizioni.fPrescrizioneConferma = false;
                        nonConformitaPrescrizioni.Prescrizione = null;
                        nonConformitaPrescrizioni.OsservazioneRct = null;
                        nonConformitaPrescrizioni.fOsservazioneConferma = false;
                        nonConformitaPrescrizioni.Osservazione = null;
                        nonConformitaPrescrizioni.fImpiantoFunzionanteRct = false;
                        nonConformitaPrescrizioni.fImpiantoFunzionanteConferma = false;
                        nonConformitaPrescrizioni.ImpiantoFunzionante = null;
                        nonConformitaPrescrizioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                        nonConformitaPrescrizioni.IDTipologiaRisoluzioneAccertamento = 1;
                        nonConformitaPrescrizioni.IDTipologiaEventoAccertamento = null;
                        nonConformitaPrescrizioni.Giorni = null;

                        ctx.VER_AccertamentoNonConformita.Add(nonConformitaPrescrizioni);
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
                #endregion

                #region Impianto non funzionante
                try
                {
                    var nonConformitaImpiantoNonFunzionante = new VER_AccertamentoNonConformita();
                    nonConformitaImpiantoNonFunzionante.IDAccertamento = iDAccertamento;
                    nonConformitaImpiantoNonFunzionante.Tipo = "INF";
                    nonConformitaImpiantoNonFunzionante.IDProceduraAccertamento = iDProceduraAccertamento;
                    nonConformitaImpiantoNonFunzionante.fRaccomandazioneConferma = false;
                    nonConformitaImpiantoNonFunzionante.RaccomandazioneRct = null;
                    nonConformitaImpiantoNonFunzionante.Raccomandazione = null;

                    nonConformitaImpiantoNonFunzionante.PrescrizioneRct = null;
                    nonConformitaImpiantoNonFunzionante.fPrescrizioneConferma = false;
                    nonConformitaImpiantoNonFunzionante.Prescrizione = null;
                    nonConformitaImpiantoNonFunzionante.OsservazioneRct = null;
                    nonConformitaImpiantoNonFunzionante.fOsservazioneConferma = false;
                    nonConformitaImpiantoNonFunzionante.Osservazione = null;
                    nonConformitaImpiantoNonFunzionante.fImpiantoFunzionanteRct = rapportoIspezione.fImpiantoPuoFunzionare;
                    nonConformitaImpiantoNonFunzionante.fImpiantoFunzionanteConferma = true;
                    nonConformitaImpiantoNonFunzionante.ImpiantoFunzionante = null;
                    nonConformitaImpiantoNonFunzionante.IDTipologiaImpiantoFunzionanteAccertamento = null;
                    nonConformitaImpiantoNonFunzionante.IDTipologiaRisoluzioneAccertamento = 1;
                    nonConformitaImpiantoNonFunzionante.IDTipologiaEventoAccertamento = null;
                    nonConformitaImpiantoNonFunzionante.Giorni = null;

                    ctx.VER_AccertamentoNonConformita.Add(nonConformitaImpiantoNonFunzionante);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {

                }
                #endregion
                #endregion
            }


        }

        public static void SetNonConformitaLibere(long iDAccertamento, long? iDRapportoControlloTecnico, long? iDIspezione, int iDTipoAccertamento, string tipo)
        {
            int? iDProceduraAccertamento = null;
            using (var ctx = new CriterDataModel())
            {
                switch (tipo)
                {
                    case "OSS":
                        #region Riga libera
                        try
                        {
                            var nonConformitaOsservazioni = new VER_AccertamentoNonConformita();
                            nonConformitaOsservazioni.IDAccertamento = iDAccertamento;
                            nonConformitaOsservazioni.Tipo = "OSS";
                            nonConformitaOsservazioni.IDProceduraAccertamento = null;
                            nonConformitaOsservazioni.fRaccomandazioneConferma = false;
                            nonConformitaOsservazioni.RaccomandazioneRct = null;
                            nonConformitaOsservazioni.Raccomandazione = null;

                            nonConformitaOsservazioni.PrescrizioneRct = null;
                            nonConformitaOsservazioni.fPrescrizioneConferma = false;
                            nonConformitaOsservazioni.Prescrizione = null;
                            nonConformitaOsservazioni.OsservazioneRct = null;
                            nonConformitaOsservazioni.fOsservazioneConferma = true;
                            nonConformitaOsservazioni.Osservazione = null;
                            nonConformitaOsservazioni.fImpiantoFunzionanteRct = false;
                            nonConformitaOsservazioni.fImpiantoFunzionanteConferma = false;
                            nonConformitaOsservazioni.ImpiantoFunzionante = null;
                            nonConformitaOsservazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                            nonConformitaOsservazioni.IDTipologiaRisoluzioneAccertamento = 1;
                            nonConformitaOsservazioni.IDTipologiaEventoAccertamento = null;
                            nonConformitaOsservazioni.Giorni = null;

                            ctx.VER_AccertamentoNonConformita.Add(nonConformitaOsservazioni);
                            ctx.SaveChanges();
                        }
                        catch (Exception e)
                        {

                        }
                        #endregion
                        break;
                    case "RACC":
                        iDProceduraAccertamento = GetProcedureAccertamento(ctx, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, tipo);
                        #region Riga Libera
                        try
                        {
                            var nonConformitaRaccomandazioni = new VER_AccertamentoNonConformita();
                            nonConformitaRaccomandazioni.IDAccertamento = iDAccertamento;
                            nonConformitaRaccomandazioni.Tipo = tipo;
                            nonConformitaRaccomandazioni.IDProceduraAccertamento = iDProceduraAccertamento;
                            nonConformitaRaccomandazioni.fRaccomandazioneConferma = true;
                            nonConformitaRaccomandazioni.RaccomandazioneRct = null;
                            nonConformitaRaccomandazioni.Raccomandazione = null;

                            nonConformitaRaccomandazioni.PrescrizioneRct = null;
                            nonConformitaRaccomandazioni.fPrescrizioneConferma = false;
                            nonConformitaRaccomandazioni.Prescrizione = null;
                            nonConformitaRaccomandazioni.OsservazioneRct = null;
                            nonConformitaRaccomandazioni.fOsservazioneConferma = false;
                            nonConformitaRaccomandazioni.Osservazione = null;
                            nonConformitaRaccomandazioni.fImpiantoFunzionanteRct = false;
                            nonConformitaRaccomandazioni.fImpiantoFunzionanteConferma = false;
                            nonConformitaRaccomandazioni.ImpiantoFunzionante = null;
                            nonConformitaRaccomandazioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                            nonConformitaRaccomandazioni.IDTipologiaRisoluzioneAccertamento = 1;
                            nonConformitaRaccomandazioni.IDTipologiaEventoAccertamento = null;
                            nonConformitaRaccomandazioni.Giorni = null;

                            ctx.VER_AccertamentoNonConformita.Add(nonConformitaRaccomandazioni);
                            ctx.SaveChanges();
                        }
                        catch (Exception e)
                        {

                        }
                        #endregion
                        break;
                    case "INF":

                        break;
                    case "PRES":
                        iDProceduraAccertamento = GetProcedureAccertamento(ctx, iDRapportoControlloTecnico, iDIspezione, iDTipoAccertamento, tipo);
                        #region Riga Libera
                        try
                        {
                            var nonConformitaPrescrizioni = new VER_AccertamentoNonConformita();
                            nonConformitaPrescrizioni.IDAccertamento = iDAccertamento;
                            nonConformitaPrescrizioni.Tipo = tipo;
                            nonConformitaPrescrizioni.IDProceduraAccertamento = iDProceduraAccertamento;
                            nonConformitaPrescrizioni.fRaccomandazioneConferma = false;
                            nonConformitaPrescrizioni.RaccomandazioneRct = null;
                            nonConformitaPrescrizioni.Raccomandazione = null;

                            nonConformitaPrescrizioni.PrescrizioneRct = null;
                            nonConformitaPrescrizioni.fPrescrizioneConferma = true;
                            nonConformitaPrescrizioni.Prescrizione = null;
                            nonConformitaPrescrizioni.OsservazioneRct = null;
                            nonConformitaPrescrizioni.fOsservazioneConferma = false;
                            nonConformitaPrescrizioni.Osservazione = null;
                            nonConformitaPrescrizioni.fImpiantoFunzionanteRct = false;
                            nonConformitaPrescrizioni.fImpiantoFunzionanteConferma = false;
                            nonConformitaPrescrizioni.ImpiantoFunzionante = null;
                            nonConformitaPrescrizioni.IDTipologiaImpiantoFunzionanteAccertamento = null;
                            nonConformitaPrescrizioni.IDTipologiaRisoluzioneAccertamento = 1;
                            nonConformitaPrescrizioni.IDTipologiaEventoAccertamento = null;
                            nonConformitaPrescrizioni.Giorni = null;

                            ctx.VER_AccertamentoNonConformita.Add(nonConformitaPrescrizioni);
                            ctx.SaveChanges();
                        }
                        catch (Exception e)
                        {

                        }
                        #endregion

                        #region Update non Conformità INF
                        try
                        {
                            var nonConformitaInf = new VER_AccertamentoNonConformita();
                            nonConformitaInf = ctx.VER_AccertamentoNonConformita.FirstOrDefault(a => a.IDAccertamento == iDAccertamento && a.Tipo == "INF");
                            nonConformitaInf.IDProceduraAccertamento = iDProceduraAccertamento;
                            ctx.SaveChanges();
                        }
                        catch (Exception e)
                        {

                        }
                        #endregion
                        break;
                }
            }
        }

        public static void SetDocumentiAccertamento(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var nonConformitaRaccomandazioni = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento
                                                                            && c.Tipo == "RACC"
                                                                            && c.fRaccomandazioneConferma == true
                                                                            && c.IDProceduraAccertamento != null
                                                                            ).Distinct().FirstOrDefault();

                var nonConformitaPrescrizioni = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento
                                                                                && c.Tipo == "PRES"
                                                                                && c.fPrescrizioneConferma == true
                                                                                && c.IDProceduraAccertamento != null
                                                                                ).Distinct().FirstOrDefault();

                //Cancello prima tutto
                var documento = ctx.VER_AccertamentoDocumento.Where(c => c.IDAccertamento == iDAccertamento).ToList();
                ctx.VER_AccertamentoDocumento.RemoveRange(documento);
                ctx.SaveChanges();

                if (nonConformitaRaccomandazioni != null)
                {
                    if (nonConformitaRaccomandazioni.IDProceduraAccertamento != null)
                    {
                        //Inserisco una nuova riga
                        var proceduraRaccomandazione = new VER_AccertamentoDocumento();
                        proceduraRaccomandazione.IDAccertamento = iDAccertamento;
                        proceduraRaccomandazione.IDProceduraAccertamento = (int)nonConformitaRaccomandazioni.IDProceduraAccertamento;
                        ctx.VER_AccertamentoDocumento.Add(proceduraRaccomandazione);
                        ctx.SaveChanges();
                    }
                }
                if (nonConformitaPrescrizioni != null)
                {
                    if (nonConformitaPrescrizioni.IDProceduraAccertamento != null)
                    {
                        //Inserisco una nuova riga
                        var proceduraPrescrizione = new VER_AccertamentoDocumento();
                        proceduraPrescrizione.IDAccertamento = iDAccertamento;
                        proceduraPrescrizione.IDProceduraAccertamento = (int)nonConformitaPrescrizioni.IDProceduraAccertamento;
                        ctx.VER_AccertamentoDocumento.Add(proceduraPrescrizione);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public static int? GetProcedureAccertamento(CriterDataModel ctx, long? iDRapportoControlloTecnico, long? iDIspezione, int iDTipoAccertamento, string tipo)
        {
            int? iDProceduraAccertamento = null;

            if (iDTipoAccertamento == 1)
            {
                #region Procedure accertamento sui Rapporti
                var rapportoInAccertamento = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                if (tipo == "RACC")
                {
                    if ((rapportoInAccertamento.IDTipologiaCombustibile == 2 || rapportoInAccertamento.IDTipologiaCombustibile == 3)//Gas naturale o Gpl
                        && rapportoInAccertamento.PotenzaTermicaNominale < 100)
                    {
                        iDProceduraAccertamento = 3; //PRC-AC-03
                    }
                    else if (
                                ((rapportoInAccertamento.IDTipologiaCombustibile == 2 || rapportoInAccertamento.IDTipologiaCombustibile == 3)//Gas naturale o Gpl
                                && rapportoInAccertamento.PotenzaTermicaNominale > 100)
                                ||
                                ((rapportoInAccertamento.IDTipologiaCombustibile == 1 || rapportoInAccertamento.IDTipologiaCombustibile == 4 || rapportoInAccertamento.IDTipologiaCombustibile == 5 || rapportoInAccertamento.IDTipologiaCombustibile == 6 || rapportoInAccertamento.IDTipologiaCombustibile == 7 || rapportoInAccertamento.IDTipologiaCombustibile == 8) //Gasolio, Olio combustibile, Pellet, Legna, Cippato, Altro
                                && rapportoInAccertamento.PotenzaTermicaNominale > 0)
                            )
                    {
                        iDProceduraAccertamento = 4; //PRC-AC-04
                    }
                }
                else if (tipo == "PRES")
                {
                    if (
                        (rapportoInAccertamento.IDTipologiaCombustibile == 2 || rapportoInAccertamento.IDTipologiaCombustibile == 3)
                        && rapportoInAccertamento.PotenzaTermicaNominale < 100) //Gas naturale o Gpl
                    {
                        iDProceduraAccertamento = 1; //PRC-AC-01
                    }
                    else if (
                                ((rapportoInAccertamento.IDTipologiaCombustibile == 2 || rapportoInAccertamento.IDTipologiaCombustibile == 3)
                                && rapportoInAccertamento.PotenzaTermicaNominale > 100)
                                ||
                                ((rapportoInAccertamento.IDTipologiaCombustibile == 1 || rapportoInAccertamento.IDTipologiaCombustibile == 4 || rapportoInAccertamento.IDTipologiaCombustibile == 5 || rapportoInAccertamento.IDTipologiaCombustibile == 6 || rapportoInAccertamento.IDTipologiaCombustibile == 7 || rapportoInAccertamento.IDTipologiaCombustibile == 8) //Gasolio, Olio combustibile, Pellet, Legna, Cippato, Altro
                                && rapportoInAccertamento.PotenzaTermicaNominale > 0)
                            )
                    {
                        iDProceduraAccertamento = 2; //PRC-AC-02
                    }
                }
                #endregion
            }
            else
            {
                #region Procedure Accertamento sulle Ispezioni
                var rapportoIspezioneInAccertamento = ctx.VER_IspezioneRapporto.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                if (tipo == "RACC")
                {
                    if ((rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 2 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 3)//Gas naturale o Gpl
                        && rapportoIspezioneInAccertamento.PotenzaTermicaNominaleGeneratore < 100)
                    {
                        iDProceduraAccertamento = 7; //PRC-VA-03
                    }
                    else if (
                                ((rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 2 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 3)//Gas naturale o Gpl
                                && rapportoIspezioneInAccertamento.PotenzaTermicaNominaleGeneratore > 100)
                                ||
                                ((rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 1 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 4 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 5 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 6 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 7 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 8) //Gasolio, Olio combustibile, Pellet, Legna, Cippato, Altro
                                && rapportoIspezioneInAccertamento.PotenzaTermicaNominaleGeneratore > 0)
                            )
                    {
                        iDProceduraAccertamento = 8; //PRC-VA-04
                    }
                }
                else if (tipo == "PRES")
                {
                    if (
                        (rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 2 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 3)
                        && rapportoIspezioneInAccertamento.PotenzaTermicaNominaleGeneratore < 100) //Gas naturale o Gpl
                    {
                        iDProceduraAccertamento = 5; //PRC-VA-01
                    }
                    else if (
                                ((rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 2 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 3)
                                && rapportoIspezioneInAccertamento.PotenzaTermicaNominaleGeneratore > 100)
                                ||
                                ((rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 1 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 4 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 5 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 6 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 7 || rapportoIspezioneInAccertamento.IDTipologiaCombustibile == 8) //Gasolio, Olio combustibile, Pellet, Legna, Cippato, Altro
                                && rapportoIspezioneInAccertamento.PotenzaTermicaNominaleGeneratore > 0)
                            )
                    {
                        iDProceduraAccertamento = 6; //PRC-VA-02
                    }
                }
                #endregion
            }

            return iDProceduraAccertamento;
        }

        private static string CalcolaCodiceAccertamento(VER_Accertamento Accertamento)
        {
            //Pad di 10 caratteri
            return string.Format("{0:0000000000}", Accertamento.IDAccertamento);
        }

        public static void StoricizzaStatoAccertamento(CriterDataModel ctx, VER_Accertamento accertamento, int iDUtente)
        {
            VER_AccertamentoStato accertamentoStato = new VER_AccertamentoStato();
            accertamentoStato.IDAccertamento = accertamento.IDAccertamento;
            accertamentoStato.Data = DateTime.Now;
            accertamentoStato.IDStatoAccertamento = accertamento.IDStatoAccertamento;
            accertamentoStato.IDStatoAccertamentoIntervento = accertamento.IDStatoAccertamentoIntervento;
            accertamentoStato.IDUtenteAccertatore = accertamento.IDUtenteAccertatore;
            accertamentoStato.IDUtenteCoordinatore = accertamento.IDUtenteCoordinatore;
            accertamentoStato.IDUtenteUltimaModifica = iDUtente;
            ctx.VER_AccertamentoStato.Add(accertamentoStato);
            ctx.SaveChanges();
        }

        public static void StoricizzaStatoAccertamento(long iDAccertamento, int iDUtente)
        {
            using (var ctx = new CriterDataModel())
            {
                VER_Accertamento accertamento = ctx.VER_Accertamento.Find(iDAccertamento);
                if (accertamento != null)
                {
                    StoricizzaStatoAccertamento(ctx, accertamento, iDUtente);
                }
            }
        }

        public static void CambiaStatoAccertamento(long iDAccertamento, int iDStatoAccertamento, int? iDUtenteAccertatore, int? iDUtenteCoordinatore, int? iDUtenteAgenteAccertatore)
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                accertamento.IDStatoAccertamento = iDStatoAccertamento;
                accertamento.IDUtenteAccertatore = iDUtenteAccertatore;
                accertamento.IDUtenteCoordinatore = iDUtenteCoordinatore;
                accertamento.IDUtenteAgenteAccertatore = iDUtenteAgenteAccertatore;
                ctx.SaveChanges();
            }
        }

        public static void SaveAccertamento(long iDAccertamento,
                                            int? iDDistributore,
                                            string note,
                                            bool fEmailConfermaAccertamento,
                                            DateTime? DataInvioEmail,
                                            string TestoEmail,
                                            int? giorniRealizzazioneInterventi,
                                            string RispostaEmail
                                            )
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();

                if (!string.IsNullOrEmpty(note))
                {
                    accertamento.Note = note;
                }

                if ((iDDistributore != null) && (iDDistributore != 0))
                {
                    accertamento.IDDistributore = iDDistributore;
                }
                else
                {
                    accertamento.IDDistributore = null;
                }

                accertamento.fEmailConfermaAccertamento = fEmailConfermaAccertamento;
                if (DataInvioEmail != null)
                {
                    accertamento.DataInvioEmail = DataInvioEmail;
                }
                if (!string.IsNullOrEmpty(TestoEmail))
                {
                    accertamento.TestoEmail = TestoEmail;
                }
                if (!string.IsNullOrEmpty(RispostaEmail))
                {
                    accertamento.RispostaEmail = RispostaEmail;
                }
                if (giorniRealizzazioneInterventi != null)
                {
                    accertamento.GiorniRealizzazioneInterventi = giorniRealizzazioneInterventi;
                }

                ctx.SaveChanges();
            }
        }

        public static void SaveNonConformitaAccertamento(long iDNonConformita,
                                                         long iDAccertamento,
                                                         string tipo,
                                                         bool fRaccomandazioneConferma,
                                                         string Raccomandazione,
                                                         bool fPrescrizioneConferma,
                                                         string Prescrizione,
                                                         bool fOsservazioneConferma,
                                                         string Osservazione,
                                                         bool fImpiantoFunzionanteConferma,
                                                         string ImpiantoFunzionante,
                                                         int? iDTipologiaImpiantoFunzionanteAccertamento,
                                                         int? iDTipologiaRisoluzioneAccertamento,
                                                         int? iDTipologiaEventoAccertamento,
                                                         int? giorni
                                                         )
        {
            using (var ctx = new CriterDataModel())
            {
                switch (tipo)
                {
                    case "RACC":
                        var raccomandazione = ctx.VER_AccertamentoNonConformita.Where(c => c.IDNonConformita == iDNonConformita).FirstOrDefault();
                        if (raccomandazione != null)
                        {
                            raccomandazione.fRaccomandazioneConferma = fRaccomandazioneConferma;
                            if (!string.IsNullOrEmpty(Raccomandazione))
                            {
                                raccomandazione.Raccomandazione = Raccomandazione;
                            }
                            else
                            {
                                raccomandazione.Raccomandazione = null;
                            }
                            if (iDTipologiaRisoluzioneAccertamento != null)
                            {
                                raccomandazione.IDTipologiaRisoluzioneAccertamento = iDTipologiaRisoluzioneAccertamento;
                            }
                            else
                            {
                                raccomandazione.IDTipologiaRisoluzioneAccertamento = null;
                            }
                            if ((!string.IsNullOrEmpty(iDTipologiaEventoAccertamento.ToString())) && (iDTipologiaEventoAccertamento.ToString() != "0"))
                            {
                                raccomandazione.IDTipologiaEventoAccertamento = iDTipologiaEventoAccertamento;
                            }
                            else
                            {
                                raccomandazione.IDTipologiaEventoAccertamento = null;
                            }
                            if (giorni != null)
                            {
                                raccomandazione.Giorni = giorni;
                            }
                            else
                            {
                                raccomandazione.Giorni = null;
                            }
                        }
                        break;
                    case "PRES":
                        var prescrizione = ctx.VER_AccertamentoNonConformita.Where(c => c.IDNonConformita == iDNonConformita).FirstOrDefault();
                        if (prescrizione != null)
                        {
                            prescrizione.fPrescrizioneConferma = fPrescrizioneConferma;
                            if (!string.IsNullOrEmpty(Prescrizione))
                            {
                                prescrizione.Prescrizione = Prescrizione;
                            }
                            else
                            {
                                prescrizione.Prescrizione = null;
                            }
                            if (iDTipologiaRisoluzioneAccertamento != null)
                            {
                                prescrizione.IDTipologiaRisoluzioneAccertamento = iDTipologiaRisoluzioneAccertamento;
                            }
                            else
                            {
                                prescrizione.IDTipologiaRisoluzioneAccertamento = null;
                            }
                            if ((!string.IsNullOrEmpty(iDTipologiaEventoAccertamento.ToString())) && (iDTipologiaEventoAccertamento.ToString() != "0"))
                            {
                                prescrizione.IDTipologiaEventoAccertamento = iDTipologiaEventoAccertamento;
                            }
                            else
                            {
                                prescrizione.IDTipologiaEventoAccertamento = null;
                            }
                            if (giorni != null)
                            {
                                prescrizione.Giorni = giorni;
                            }
                            else
                            {
                                prescrizione.Giorni = null;
                            }
                        }
                        break;
                    case "OSS":
                        var osservazione = ctx.VER_AccertamentoNonConformita.Where(c => c.IDNonConformita == iDNonConformita).FirstOrDefault();
                        if (osservazione != null)
                        {
                            osservazione.fOsservazioneConferma = fOsservazioneConferma;
                            if (!string.IsNullOrEmpty(Osservazione))
                            {
                                osservazione.Osservazione = Osservazione;
                            }
                            else
                            {
                                osservazione.Osservazione = null;
                            }
                            if (iDTipologiaRisoluzioneAccertamento != null)
                            {
                                osservazione.IDTipologiaRisoluzioneAccertamento = iDTipologiaRisoluzioneAccertamento;
                            }
                            else
                            {
                                osservazione.IDTipologiaRisoluzioneAccertamento = null;
                            }
                            if ((!string.IsNullOrEmpty(iDTipologiaEventoAccertamento.ToString())) && (iDTipologiaEventoAccertamento.ToString() != "0"))
                            {
                                osservazione.IDTipologiaEventoAccertamento = iDTipologiaEventoAccertamento;
                            }
                            else
                            {
                                osservazione.IDTipologiaEventoAccertamento = null;
                            }
                            if (giorni != null)
                            {
                                osservazione.Giorni = giorni;
                            }
                            else
                            {
                                osservazione.Giorni = null;
                            }
                        }
                        break;
                    case "INF":
                        var impiantonf = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento && c.Tipo == tipo).FirstOrDefault();
                        if (impiantonf != null)
                        {
                            impiantonf.fImpiantoFunzionanteConferma = fImpiantoFunzionanteConferma;
                            if (!string.IsNullOrEmpty(ImpiantoFunzionante))
                            {
                                impiantonf.ImpiantoFunzionante = ImpiantoFunzionante;
                            }
                            else
                            {
                                impiantonf.ImpiantoFunzionante = null;
                            }
                            if (iDTipologiaImpiantoFunzionanteAccertamento != null)
                            {
                                impiantonf.IDTipologiaImpiantoFunzionanteAccertamento = iDTipologiaImpiantoFunzionanteAccertamento;
                            }
                            if (iDTipologiaRisoluzioneAccertamento != null)
                            {
                                impiantonf.IDTipologiaRisoluzioneAccertamento = iDTipologiaRisoluzioneAccertamento;
                            }
                            //if ((!string.IsNullOrEmpty(iDTipologiaEventoAccertamento.ToString())) && (iDTipologiaEventoAccertamento.ToString() != "0"))
                            //{
                            //    impiantonf.IDTipologiaEventoAccertamento = iDTipologiaEventoAccertamento;
                            //}
                            impiantonf.Giorni = giorni;
                        }
                        break;
                }
                ctx.SaveChanges();
            }
        }

        public static void GetZipAccertamentiDaFirmare(string iDAccertamenti)
        {
            string[] array = iDAccertamenti.Split(',');

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BufferOutput = false;  // for large files
            string Zipfilename = string.Format("AccertamentiDaFirmare-{0}.zip", DateTime.Now.ToString("dd-MM-yyyy-HH_mm"));
            HttpContext.Current.Response.ContentType = "application/zip";
            //Response.AddHeader("content-disposition", "filename=" + filename);
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=\"" + Zipfilename + "\"");
            string pathPdf = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\";

            using (ZipFile zip = new ZipFile())
            {
                using (var ctx = new CriterDataModel())
                {
                    foreach (string iDAccertamento in array)
                    {
                        var documenti = GetDocumentiAccertamenti(long.Parse(iDAccertamento));
                        foreach (var documento in documenti)
                        {
                            string file = pathPdf + documento.CodiceAccertamento + @"\" + documento.IDAccertamento + "_" + documento.IDProceduraAccertamento + ".pdf";
                            FileInfo filePdf = new FileInfo(file);
                            if (File.Exists(file))
                            {
                                zip.AddFile(file, "");
                            }
                        }
                    }
                }
                zip.Save(HttpContext.Current.Response.OutputStream);
            }
        }

        public static void GetZipSanzioniDaFirmare(string iDAccertamenti)
        {
            string[] array = iDAccertamenti.Split(',');

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BufferOutput = false;  // for large files
            string Zipfilename = string.Format("SanzioniDaFirmare-{0}.zip", DateTime.Now.ToString("dd-MM-yyyy-HH_mm"));
            HttpContext.Current.Response.ContentType = "application/zip";
            //Response.AddHeader("content-disposition", "filename=" + filename);
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=\"" + Zipfilename + "\"");
            string pathPdf = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadSanzioni"] + @"\";

            using (ZipFile zip = new ZipFile())
            {
                using (var ctx = new CriterDataModel())
                {
                    foreach (string iDAccertamento in array)
                    {
                        int iDAccertamentoInt = int.Parse(iDAccertamento);
                        var sanzione = ctx.VER_Accertamento.Where(a => a.IDAccertamento == iDAccertamentoInt).FirstOrDefault();

                        string file = pathPdf + sanzione.CodiceAccertamento + @"\" + ConfigurationManager.AppSettings["ReportNameSanzione"] + "_" + sanzione.IDAccertamento + ".pdf";
                        FileInfo filePdf = new FileInfo(file);
                        if (File.Exists(file))
                        {
                            zip.AddFile(file, "");
                        }

                    }
                }
                zip.Save(HttpContext.Current.Response.OutputStream);
            }
        }

        public static string GetSqlValoriAccertamentiFilter(
            object iDTipoAccertamento,
            object iDSoggettoAzienda,
            object IDStatoAccertamento,
            object[] valoriProcedureSelected,
            object DataRilevazioneDal, object DataRilevazioneAl,
            object CodiceAccertamento,
            object codiceTargaturaImpianto,
            object iDAccertatore,
            object iDCoordinatore,
            object iDProgrammaAccertamento,
            object iDStatoAccertamentoSanzione
            )
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT dbo.V_VER_Accertamenti.IDAccertamento, dbo.V_VER_Accertamenti.IDRapportoDiControlloTecnicoBase, dbo.V_VER_Accertamenti.IDStatoAccertamento, dbo.V_VER_Accertamenti.CodiceAccertamento, ");
            strSql.Append("             dbo.V_VER_Accertamenti.DataRilevazione, dbo.V_VER_Accertamenti.IDSoggetto, dbo.V_VER_Accertamenti.IDUtenteAccertatore, dbo.V_VER_Accertamenti.Accertatore, dbo.V_VER_Accertamenti.IDUtenteCoordinatore, ");
            strSql.Append("             dbo.V_VER_Accertamenti.Coordinatore, dbo.V_VER_Accertamenti.Note, dbo.V_VER_Accertamenti.fAttivo, dbo.V_VER_Accertamenti.CodiceSoggetto, dbo.V_VER_Accertamenti.NomeAzienda, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IndirizzoAzienda, dbo.V_VER_Accertamenti.Telefono, dbo.V_VER_Accertamenti.Email, dbo.V_VER_Accertamenti.EmailPec, dbo.V_VER_Accertamenti.PartitaIVA, dbo.V_VER_Accertamenti.CodiceTargatura, ");
            strSql.Append("             dbo.V_VER_Accertamenti.StatoAccertamento, dbo.V_VER_Accertamenti.IDCodiceCatastale, dbo.V_VER_Accertamenti.Comune, dbo.V_VER_Accertamenti.SiglaProvincia, dbo.V_VER_Accertamenti.EmailPecComune, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IDDistributore, dbo.V_VER_Accertamenti.Distributore, dbo.V_VER_Accertamenti.EmailPecDistributore, dbo.V_VER_Accertamenti.GuidAccertamento, dbo.V_VER_Accertamenti.TipologiaCombustibile, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IDTargaturaImpianto, dbo.V_VER_Accertamenti.Prefisso, dbo.V_VER_Accertamenti.CodiceProgressivo, dbo.V_VER_Accertamenti.GiorniRealizzazioneInterventi, ");
            strSql.Append("             dbo.V_VER_Accertamenti.fEmailConfermaAccertamento, dbo.V_VER_Accertamenti.DataInvioEmail, dbo.V_VER_Accertamenti.TestoEmail, dbo.V_VER_Accertamenti.NomeLegaleRappresentante, ");
            strSql.Append("             dbo.V_VER_Accertamenti.CognomeLegaleRappresentante, dbo.V_VER_Accertamenti.RispostaEmail, dbo.V_VER_Accertamenti.IDStatoAccertamentoIntervento, dbo.V_VER_Accertamenti.StatoAccertamentoIntervento, ");
            strSql.Append("             dbo.V_VER_Accertamenti.DataInvioRaccomandata, dbo.V_VER_Accertamenti.DataRicevimentoRaccomandata, dbo.V_VER_Accertamenti.DataScadenzaIntervento, dbo.V_VER_Accertamenti.NoteInterventi, ");
            strSql.Append("             dbo.V_VER_Accertamenti.ProvinciaResponsabile, dbo.V_VER_Accertamenti.ProvinciaTerzoResponsabile, dbo.V_VER_Accertamenti.IDComuneResponsabile, dbo.V_VER_Accertamenti.CapResponsabile, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IDTipoAccertamento, dbo.V_VER_Accertamenti.TipoAccertamento, dbo.V_VER_Accertamenti.IDIspezione, dbo.V_VER_Accertamenti.PunteggioNCAccertamento, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IDUtenteAgenteAccertatore, dbo.V_VER_Accertamenti.AgenteAccertatore, dbo.V_VER_Accertamenti.IDProgrammaAccertamento, dbo.V_VER_Accertamenti.ProgrammaAccertamento, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IDLibrettoImpianto, dbo.V_VER_Accertamenti.IDTipologiaCombustibile, dbo.V_VER_Accertamenti.Indirizzo, dbo.V_VER_Accertamenti.Civico, dbo.V_VER_Accertamenti.Palazzo, ");
            strSql.Append("             dbo.V_VER_Accertamenti.Scala, dbo.V_VER_Accertamenti.Interno, dbo.V_VER_Accertamenti.NomeResponsabile, dbo.V_VER_Accertamenti.CognomeResponsabile, dbo.V_VER_Accertamenti.CodiceFiscaleResponsabile, ");
            strSql.Append("             dbo.V_VER_Accertamenti.RagioneSocialeResponsabile, dbo.V_VER_Accertamenti.IndirizzoResponsabile, dbo.V_VER_Accertamenti.CivicoResponsabile, dbo.V_VER_Accertamenti.ComuneResponsabile, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IDProvinciaResponsabile, dbo.V_VER_Accertamenti.PotenzaTermicaNominale, dbo.V_VER_Accertamenti.RagioneSocialeTerzoResponsabile, dbo.V_VER_Accertamenti.IndirizzoTerzoResponsabile, ");
            strSql.Append("             dbo.V_VER_Accertamenti.CivicoTerzoResponsabile, dbo.V_VER_Accertamenti.ComuneTerzoResponsabile, dbo.V_VER_Accertamenti.Osservazioni, dbo.V_VER_Accertamenti.Raccomandazioni, ");
            strSql.Append("             dbo.V_VER_Accertamenti.Prescrizioni, dbo.V_VER_Accertamenti.fImpiantoFunzionante, dbo.V_VER_Accertamenti.DataControllo, dbo.V_VER_Accertamenti.PartitaIVATerzoResponsabile, ");
            strSql.Append("             dbo.V_VER_Accertamenti.PartitaIvaResponsabile, dbo.V_VER_Accertamenti.IDStatoAccertamentoSanzione, dbo.V_VER_Accertamenti.DataInvioSanzione, dbo.V_VER_Accertamenti.DataRicezioneSanzione, ");
            strSql.Append("             dbo.V_VER_Accertamenti.StatoAccertamentoSanzione, dbo.V_VER_Accertamenti.DataScadenzaSanzione, dbo.V_VER_Accertamenti.DataRicevutaPagamento, dbo.V_VER_Accertamenti.NoteSanzione, ");
            strSql.Append("             dbo.V_VER_Accertamenti.IndirizzoNormalizzatoResponsabile, dbo.COM_AnagraficaSoggetti.Nome + N' ' + dbo.COM_AnagraficaSoggetti.Cognome AS Ispettore, dbo.VER_Ispezione.CodiceIspezione ");
            strSql.Append("FROM         dbo.VER_Ispezione INNER JOIN ");
            strSql.Append("             dbo.COM_AnagraficaSoggetti ON dbo.VER_Ispezione.IDIspettore = dbo.COM_AnagraficaSoggetti.IDSoggetto RIGHT OUTER JOIN ");
            strSql.Append("             dbo.V_VER_Accertamenti ON dbo.VER_Ispezione.IDIspezione = dbo.V_VER_Accertamenti.IDIspezione ");


            //strSql.Append("SELECT ");
            //strSql.Append(" * ");
            //strSql.Append(" FROM V_VER_Accertamenti ");


            strSql.Append(" WHERE IDTipoAccertamento=" + iDTipoAccertamento.ToString() + " ");

            if ((iDSoggettoAzienda != "") && (iDSoggettoAzienda != "-1") && (iDSoggettoAzienda != null))
            {
                strSql.Append(" AND V_VER_Accertamenti.IDSoggetto=" + iDSoggettoAzienda);
            }

            if (iDAccertatore != null)
            {
                //strSql.Append(" AND (iDUtenteAccertatore=");
                //strSql.Append(iDAccertatore.ToString());
                //strSql.Append(" OR iDUtenteAccertatore IS NULL) ");
                strSql.Append(" AND (iDUtenteAccertatore=");
                strSql.Append(iDAccertatore.ToString());
                //strSql.Append(" OR iDUtenteAccertatore IS NULL) "); // 16/10/2020 accertatore può vedere solo suoi accertamenti
                strSql.Append(" ) ");
            }

            if (iDCoordinatore != null)
            {
                strSql.Append(" AND IDUtenteCoordinatore=");
                strSql.Append(iDCoordinatore.ToString());
            }

            if (IDStatoAccertamento.ToString() != "0")
            {
                strSql.Append(" AND IDStatoAccertamento=");
                strSql.Append(IDStatoAccertamento);
            }

            //if (iDStatoAccertamentoSanzione.ToString() != "0")
            //{
            //    strSql.Append(" AND IDStatoAccertamentoSanzione=");
            //    strSql.Append(iDStatoAccertamentoSanzione);
            //}

            if (codiceTargaturaImpianto.ToString() != "")
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(codiceTargaturaImpianto);
                strSql.Append("'");
            }

            if (CodiceAccertamento.ToString() != "")
            {
                strSql.Append(" AND CodiceAccertamento = ");
                strSql.Append("'");
                strSql.Append(CodiceAccertamento);
                strSql.Append("'");
            }

            if ((DataRilevazioneDal != null) && (DataRilevazioneDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataRilevazione, 126) >= '");
                DateTime dataRilevazioneDa = DateTime.Parse(DataRilevazioneDal.ToString());
                string newDataRilevazioneDa = dataRilevazioneDa.ToString("yyyy") + "-" + dataRilevazioneDa.ToString("MM") + "-" + dataRilevazioneDa.ToString("dd");
                strSql.Append(newDataRilevazioneDa);
                strSql.Append("'");
            }

            if ((DataRilevazioneAl != null) && (DataRilevazioneAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataRilevazione, 126) <= '");
                DateTime dataRilevazioneAl = DateTime.Parse(DataRilevazioneAl.ToString());
                string newdataRilevazioneAl = dataRilevazioneAl.ToString("yyyy") + "-" + dataRilevazioneAl.ToString("MM") + "-" + dataRilevazioneAl.ToString("dd");
                strSql.Append(newdataRilevazioneAl);
                strSql.Append("'");
            }

            if ((iDProgrammaAccertamento != "") && (iDProgrammaAccertamento != "-1") && (iDProgrammaAccertamento != null))
            {
                strSql.Append(" AND IDProgrammaAccertamento=" + iDProgrammaAccertamento);
            }

            if (valoriProcedureSelected.Count() > 0)
            {
                var filterProcedure = string.Join(",", valoriProcedureSelected);
                strSql.Append(" AND IDAccertamento IN (SELECT DISTINCT IDAccertamento FROM VER_AccertamentoNonConformita WHERE IDProceduraAccertamento IN(" + filterProcedure + "))");
            }

            strSql.Append(" ORDER BY DataRilevazione DESC");

            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public static List<V_VER_AccertamentoDocumento> GetDocumentiAccertamenti(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var documenti = ctx.V_VER_AccertamentoDocumento.Where(c => c.IDAccertamento == iDAccertamento).ToList();

                return documenti;
            }
        }

        public static List<V_VER_AccertamentiStato> GetAccertamentoStorico(long iDAccertamento, bool fAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var query = ctx.V_VER_AccertamentiStato.AsQueryable();
                query = query.Where(c => c.IDAccertamento == iDAccertamento);
                if (fAccertamento)
                {
                    query = query.Where(c => c.IDStatoAccertamento != null && c.IDStatoAccertamentoIntervento == null);
                }
                else
                {
                    query = query.Where(c => c.IDStatoAccertamentoIntervento != null);
                }

                return query.OrderByDescending(c => c.Data).ToList();
            }
        }

        public static void SaveInsertDeleteDatiAccertamentiFirmaDigitale(long iDAccertamento,
                                                                     int iDSoggetto,
                                                                     string documento,
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
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var firma = new VER_AccertamentoFirmaDigitale();
                firma.IDAccertamento = iDAccertamento;
                firma.IDSoggetto = iDSoggetto;
                if (!string.IsNullOrEmpty(documento))
                {
                    firma.Documento = documento;
                }
                else
                {
                    firma.Documento = null;
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
                ctx.VER_AccertamentoFirmaDigitale.Add(firma);

                try
                {
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        public static void ControllaAccertamentiSospesi()
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamentiSospesi = ctx.VER_Accertamento.Where(c => c.IDStatoAccertamento == 7);

                if (accertamentiSospesi.Any())
                {
                    foreach (var a in accertamentiSospesi)
                    {
                        if (a.DataInvioEmail.Value.AddHours(24) >= DateTime.Now)
                        {
                            CambiaStatoAccertamento(a.IDAccertamento, 2, a.IDUtenteAccertatore, null, null);
                            StoricizzaStatoAccertamento(a.IDAccertamento, 5805);
                        }
                    }
                }
            }
        }

        public static bool ControllaAccettamentiInCorso(int iDTipoAccertamento, string iDAccertamento, string iDTargaturaImpianto, string prefisso, string codiceProgressivo)
        {
            bool fExist = false;
            using (var ctx = new CriterDataModel())
            {
                long iDAccertamentoLong = long.Parse(iDAccertamento);
                int iDTargaturaImpiantoInt = int.Parse(iDTargaturaImpianto);
                int codiceProgressivoInt = int.Parse(codiceProgressivo);

                fExist = (from a in ctx.V_VER_Accertamenti
                          where (a.IDTargaturaImpianto == iDTargaturaImpiantoInt
                          && a.CodiceProgressivo == codiceProgressivoInt
                          && a.Prefisso == prefisso
                          && a.IDStatoAccertamento != 1
                          && a.IDAccertamento != iDAccertamentoLong
                          && a.IDTipoAccertamento == iDTipoAccertamento
                          )
                          select new
                          {
                              iDAccertamento = a.IDAccertamento
                          }
                        ).Any();
            }

            return fExist;
        }

        #endregion

        #region Programma Accertamenti 

        public static List<VER_ProgrammaAccertamento> GetListProgrammaAccertamenti(int iDTipoAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.VER_ProgrammaAccertamento.Where(a => a.IDTipoAccertamento == iDTipoAccertamento).OrderByDescending(c => c.fAttivo).ToList();

                return result;
            }
        }



        public static int GetIDProgrammaAccertamentoAttivo(int iDTipoAccertamento)
        {
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    return
                        ctx.VER_ProgrammaAccertamento.Where(
                            c => c.fAttivo == true && c.IDTipoAccertamento == iDTipoAccertamento
                            )
                            .OrderBy(c => c.DataInizio)
                            .Select(c => c.IDProgrammaAccertamento)
                            .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int SaveInsertDeleteDatiProgrammaAccertamento(
                                    string operationType,
                                    int? iDProgrammaAccertamento,
                                    int iDTipoAccertamento,
                                    string descrizione,
                                    DateTime? dataInizio,
                                    DateTime? dataFine,
                                    bool fAttivo)
        {
            int? iDProgrammaAccertamentoInsert = null;

            using (var ctx = new CriterDataModel())
            {
                var programmaAccertamento = new VER_ProgrammaAccertamento();


                if (operationType == "Delete" && iDProgrammaAccertamento != null)
                {
                    var toRemove = ctx.VER_ProgrammaAccertamento.FirstOrDefault(c => c.IDProgrammaAccertamento == iDProgrammaAccertamento);
                    ctx.VER_ProgrammaAccertamento.Remove(toRemove);
                    iDProgrammaAccertamentoInsert = 0;
                }
                else
                {
                    if (operationType == "update")
                    {
                        programmaAccertamento = ctx.VER_ProgrammaAccertamento.FirstOrDefault(i => i.IDProgrammaAccertamento == iDProgrammaAccertamento);
                    }
                    if (!string.IsNullOrEmpty(descrizione))
                    {
                        programmaAccertamento.Descrizione = descrizione;
                    }
                    else
                    {
                        programmaAccertamento.Descrizione = null;
                    }
                    if (dataInizio != null)
                    {
                        programmaAccertamento.DataInizio = (DateTime)dataInizio;
                    }

                    programmaAccertamento.IDTipoAccertamento = iDTipoAccertamento;
                    programmaAccertamento.DataFine = (DateTime)dataFine;
                    programmaAccertamento.fAttivo = fAttivo;

                    if (operationType == "insert")
                    {
                        ctx.VER_ProgrammaAccertamento.Add(programmaAccertamento);
                    }
                }

                try
                {
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

                if (operationType == "insert")
                {
                    iDProgrammaAccertamentoInsert = programmaAccertamento.IDProgrammaAccertamento;
                }
                else if (operationType == "update")
                {
                    iDProgrammaAccertamentoInsert = iDProgrammaAccertamento;
                }
            }

            return (int)iDProgrammaAccertamentoInsert;
        }

        public static void InsertDeleteAccertamentoNelProgramma(string operationType, long? IdAccertamentoProgramma, long IdAccertamento, int? IdUtente, int IdProgrammaAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                if (operationType == "Insert" && IdAccertamentoProgramma == null)
                {
                    var accertamentoProgramma = new VER_AccertamentoProgramma();

                    accertamentoProgramma.IDAccertamento = IdAccertamento;
                    accertamentoProgramma.IDUtenteInserimento = (int)IdUtente;
                    accertamentoProgramma.DataInserimento = DateTime.Now;
                    accertamentoProgramma.IDProgrammaAccertamento = IdProgrammaAccertamento;

                    ctx.VER_AccertamentoProgramma.Add(accertamentoProgramma);
                }
                else if (operationType == "Delete" && IdAccertamentoProgramma != null)
                {
                    var toremove = ctx.VER_AccertamentoProgramma.FirstOrDefault(c => c.IDAccertamentoProgramma == IdAccertamentoProgramma && c.IDProgrammaAccertamento == IdProgrammaAccertamento);
                    ctx.VER_AccertamentoProgramma.Remove(toremove);
                }
                ctx.SaveChanges();
            }
        }

        public static void CreaDeletePacchettoAccertamento(string operationType, int iDTipoAccertamento, long? IdAccertamentoPacchetto)
        {
            using (var ctx = new CriterDataModel())
            {
                var pacchettoAccertamento = new DataLayer.VER_AccertamentoVisita();

                if (operationType == "Crea")
                {
                    var CountPacchettiAttivi = ctx.VER_AccertamentoVisita.ToList();
                    string numeroPacchetto = "";

                    if (CountPacchettiAttivi == null || CountPacchettiAttivi.Count() == 0)
                    {
                        numeroPacchetto = "1";
                    }
                    else
                    {
                        numeroPacchetto = (CountPacchettiAttivi.Count() + 1).ToString();
                    }

                    pacchettoAccertamento.DescrizioneAccertamentoVisita = "Pacchetto" + " - " + numeroPacchetto;
                    pacchettoAccertamento.fPacchettoAssegnato = false;
                    pacchettoAccertamento.DataCreazione = DateTime.Now;
                    pacchettoAccertamento.IDProgrammaAccertamento = GetIDProgrammaAccertamentoAttivo(iDTipoAccertamento);

                    ctx.VER_AccertamentoVisita.Add(pacchettoAccertamento);
                }
                else if (operationType == "Delete" && IdAccertamentoPacchetto != null && !fPacchettoAssegnato((long)IdAccertamentoPacchetto))
                {
                    var AccertamentiDaCancellare = ctx.VER_AccertamentoVisitaInfo.Where(c => c.IDAccertamentoVisita == IdAccertamentoPacchetto).ToList();

                    foreach (var acc in AccertamentiDaCancellare)
                    {
                        var toremove = ctx.VER_AccertamentoVisitaInfo.FirstOrDefault(c => c.IDAccertamentoProgramma == acc.IDAccertamentoProgramma);
                        ctx.VER_AccertamentoVisitaInfo.Remove(toremove);
                    }

                    var pacchetto = ctx.VER_AccertamentoVisita.FirstOrDefault(c => c.IDAccertamentoVisita == IdAccertamentoPacchetto);
                    ctx.VER_AccertamentoVisita.Remove(pacchetto);
                }

                ctx.SaveChanges();
            }
        }

        public static bool fPacchettoAssegnato(long IdAccertamentoPacchetto)
        {
            using (var ctx = new CriterDataModel())
            {
                return ctx.VER_AccertamentoVisita.FirstOrDefault(c => c.IDAccertamentoVisita == IdAccertamentoPacchetto).fPacchettoAssegnato;
            }
        }

        public static void InsertDeleteAccertamentiNelPacchetto(string operationType, long IdAccertamentoPacchetto, long IdAccertamentoProgramma, int? IdUtente, long IdAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var pacchettoInfo = new DataLayer.VER_AccertamentoVisitaInfo();

                if (operationType == "Insert")
                {
                    pacchettoInfo.IDAccertamentoVisita = IdAccertamentoPacchetto;
                    pacchettoInfo.IDAccertamentoProgramma = IdAccertamentoProgramma;
                    pacchettoInfo.IDAccertamento = IdAccertamento;
                    ctx.VER_AccertamentoVisitaInfo.Add(pacchettoInfo);
                }
                else if (operationType == "Delete")
                {
                    pacchettoInfo = ctx.VER_AccertamentoVisitaInfo.FirstOrDefault(c => c.IDAccertamentoVisita == IdAccertamentoPacchetto && c.IDAccertamentoProgramma == IdAccertamentoProgramma && c.IDAccertamento == IdAccertamento);
                    ctx.VER_AccertamentoVisitaInfo.Remove(pacchettoInfo);
                }
                ctx.SaveChanges();
            }
        }

        public static void AssegnaPacchettoDeiAccertamenti(long IdAccertamentoPacchetto, int? IdUtente, int IdAccertatore)
        {
            using (var ctx = new CriterDataModel())
            {
                var pacchetto = ctx.VER_AccertamentoVisita.FirstOrDefault(c => c.IDAccertamentoVisita == IdAccertamentoPacchetto);
                if (pacchetto != null)
                {
                    pacchetto.fPacchettoAssegnato = true;
                    pacchetto.DataAssegnazione = DateTime.Now;
                    pacchetto.IDUtenteAssegnazione = (int)IdUtente;
                    pacchetto.IDAccertatore = IdAccertatore;
                    foreach (var item in pacchetto.VER_AccertamentoVisitaInfo.ToList())
                    {
                        CambiaStatoAccertamento(item.VER_AccertamentoProgramma.IDAccertamento, 2, IdAccertatore, null, null);
                        StoricizzaStatoAccertamento(item.VER_AccertamentoProgramma.IDAccertamento, IdAccertatore);
                        SetProgrammaAccertamentoInAccertamento(item.VER_AccertamentoProgramma.IDProgrammaAccertamento, item.VER_AccertamentoProgramma.IDAccertamento);
                    }
                    ctx.SaveChanges();
                }
            }
        }

        public static void SetProgrammaAccertamentoInAccertamento(int iDProgrammaAccertamento, long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = ctx.VER_Accertamento.Where(a => a.IDAccertamento == iDAccertamento).FirstOrDefault();
                if (accertamento != null)
                {
                    accertamento.IDProgrammaAccertamento = iDProgrammaAccertamento;
                    ctx.SaveChanges();
                }
            }
        }

        public static string GetSqlValoriProgrammaAccertamentiFilter(
           object iDTipoAccertamento,
           object iDSoggettoAzienda,
           object IDStatoAccertamento,
           bool fCriticita,
           object DataRilevazioneDal, object DataRilevazioneAl,
           object CodiceAccertamento,
           object codiceTargaturaImpianto,
           object[] valoriCriticitaSelected,
           object DataIspezioneDal, object DataIspezioneAl,
           object CodiceIspezione
           )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_VER_AccertamentiProgramma ");
            //strSql.Append(" FROM V_VER_ProgrammaAccertamento_TEST "); // TODO Union per punteggio
            strSql.Append(" WHERE IDTipoAccertamento=" + iDTipoAccertamento.ToString() + " ");

            if ((iDSoggettoAzienda != "") && (iDSoggettoAzienda != "-1") && (iDSoggettoAzienda != null))
            {
                strSql.Append(" AND IDSoggetto=" + iDSoggettoAzienda);
            }

            if (IDStatoAccertamento.ToString() != "0")
            {
                strSql.Append(" AND IDStatoAccertamento=");
                strSql.Append(IDStatoAccertamento);
            }

            if (codiceTargaturaImpianto.ToString() != "")
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(codiceTargaturaImpianto);
                strSql.Append("'");
            }

            if (CodiceAccertamento.ToString() != "")
            {
                strSql.Append(" AND CodiceAccertamento = ");
                strSql.Append("'");
                strSql.Append(CodiceAccertamento);
                strSql.Append("'");
            }

            if ((DataRilevazioneDal != null) && (DataRilevazioneDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataRilevazione, 126) >= '");
                DateTime dataRilevazioneDa = DateTime.Parse(DataRilevazioneDal.ToString());
                string newDataRilevazioneDa = dataRilevazioneDa.ToString("yyyy") + "-" + dataRilevazioneDa.ToString("MM") + "-" + dataRilevazioneDa.ToString("dd");
                strSql.Append(newDataRilevazioneDa);
                strSql.Append("'");
            }

            if ((DataRilevazioneAl != null) && (DataRilevazioneAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataRilevazione, 126) <= '");
                DateTime dataRilevazioneAl = DateTime.Parse(DataRilevazioneAl.ToString());
                string newdataRilevazioneAl = dataRilevazioneAl.ToString("yyyy") + "-" + dataRilevazioneAl.ToString("MM") + "-" + dataRilevazioneAl.ToString("dd");
                strSql.Append(newdataRilevazioneAl);
                strSql.Append("'");
            }


            if (CodiceIspezione.ToString() != "")
            {
                strSql.Append(" AND CodiceIspezione = ");
                strSql.Append("'");
                strSql.Append(CodiceIspezione);
                strSql.Append("'");
            }

            if ((DataIspezioneDal != null) && (DataIspezioneDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataIspezione, 126) >= '");
                DateTime dataIspezioneDa = DateTime.Parse(DataIspezioneDal.ToString());
                string newDataIspezioneDa = dataIspezioneDa.ToString("yyyy") + "-" + dataIspezioneDa.ToString("MM") + "-" + dataIspezioneDa.ToString("dd");
                strSql.Append(newDataIspezioneDa);
                strSql.Append("'");
            }

            if ((DataIspezioneAl != null) && (DataIspezioneAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataIspezione, 126) <= '");
                DateTime dataIspezioneAl = DateTime.Parse(DataIspezioneAl.ToString());
                string newdataIspezioneAl = dataIspezioneAl.ToString("yyyy") + "-" + dataIspezioneAl.ToString("MM") + "-" + dataIspezioneAl.ToString("dd");
                strSql.Append(newdataIspezioneAl);
                strSql.Append("'");
            }


            if (fCriticita)
            {
                strSql.Append(" AND PunteggioNCAccertamento IS NOT NULL");

                //strSql.Append(" AND ( ");
                //strSql.Append(" fDichiarazioneConformita=0 OR ");
                //strSql.Append(" fUsoManutenzioneGeneratore=0 OR ");
                //strSql.Append(" fLibrettoImpiantoCompilato=0 OR ");
                //strSql.Append(" TrattamentoRiscaldamento=0 OR ");
                //strSql.Append(" TrattamentoACS=0 OR ");
                //strSql.Append(" LocaleInstallazioneIdoneo=0 OR ");
                //strSql.Append(" GeneratoriIdonei=0 OR ");
                //strSql.Append(" DimensioniApertureAdeguate=0 OR ");
                //strSql.Append(" ApertureLibere=0 OR ");
                //strSql.Append(" ScarichiIdonei=0 OR ");
                //strSql.Append(" LineeElettricheIdonee=0 OR ");
                //strSql.Append(" RegolazioneTemperaturaAmbiente=0 OR ");
                //strSql.Append(" AssenzaPerditeCombustibile=0 OR ");
                //strSql.Append(" TenutaImpiantoIdraulico=0 OR ");
                //strSql.Append(" DepressioneCanaleFumo<3 OR ");
                //strSql.Append(" CapsulaInsonorizzataIdonea=0 OR");
                //strSql.Append(" TenutaCircuitoOlioIdonea=0 OR ");
                //strSql.Append(" DispositiviComandoRegolazione=0 OR ");
                //strSql.Append(" DispositiviSicurezza=0 OR ");
                //strSql.Append(" ValvolaSicurezzaSovrappressione=0 OR ");
                //strSql.Append(" ScambiatoreFumiPulito=0 OR ");
                //strSql.Append(" RiflussoProdottiCombustione=1 OR ");
                //strSql.Append(" ConformitaUNI10389=0 OR ");
                //strSql.Append(" CoCorretto>1000 OR ");
                //strSql.Append(" RendimentoCombustione<RendimentoMinimo OR ");
                //strSql.Append(" RispettaIndiceBacharach=0 OR ");
                //strSql.Append(" COFumiSecchiNoAria1000=0 OR ");
                //strSql.Append(" RendimentoSupMinimo=0 OR ");
                //strSql.Append(" CoibentazioniIdonee=0 OR ");
                //strSql.Append(" StatoCoibentazioniIdonee=0 OR ");
                //strSql.Append(" AssenzaPerditeRefrigerante=0 OR ");
                //strSql.Append(" FiltriPuliti=0 OR ");
                //strSql.Append(" LeakDetector=0 OR ");
                //strSql.Append(" ScambiatoriLiberi=0 OR ");
                //strSql.Append(" ParametriTermodinamici=0 OR ");
                //strSql.Append(" PotenzaCompatibileProgetto=0 OR ");
                //strSql.Append(" Assenzatrafilamenti=0 OR ");
                //strSql.Append(" FunzionalitàScambiatoreSeparazione=0 OR ");
                //strSql.Append(" Contabilizzazione=0 OR ");
                //strSql.Append(" Termoregolazione=0 OR ");
                //strSql.Append(" CorrettoFunzionamentoContabilizzazione=0 ");

                //strSql.Append(" Prescrizioni<>NULL OR ");
                //strSql.Append(" Raccomandazioni<>NULL OR ");
                //strSql.Append(" fImpiantoFunzionante=0 ");
                //strSql.Append(" ) ");

                if (valoriCriticitaSelected != null)
                {
                    for (int i = 0; i < valoriCriticitaSelected.Length; i++)
                    {
                        strSql.Append(" AND PunteggioNCAccertamento=" + valoriCriticitaSelected[i].ToString());
                    }
                }
            }

            strSql.Append(" AND IDAccertamento NOT IN (SELECT IDAccertamento FROM VER_AccertamentoProgramma) ");

            if (fCriticita)
            {
                strSql.Append("ORDER BY PunteggioNCAccertamento,DataRilevazione DESC");
            }
            else
            {
                strSql.Append(" ORDER BY DataRilevazione DESC");
            }


            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        #endregion

        #region Interventi
        public static void SottoponiAdIntervento(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                //Verifico se esistono non conformità confermate
                var nonconformita = ctx.VER_AccertamentoNonConformita.Where(c => c.IDAccertamento == iDAccertamento
                                                                            && (c.fRaccomandazioneConferma == true || c.fPrescrizioneConferma == true)
                                                                            );

                if (nonconformita.Any())
                {
                    try
                    {
                        var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento
                                                                            & c.IDStatoAccertamento == 5
                                                                            ).FirstOrDefault();

                        //accertamento.DataInvioRaccomandata = DateTime.Now;
                        accertamento.DataRicevimentoRaccomandata = null;
                        accertamento.DataScadenzaIntervento = null;
                        accertamento.IDStatoAccertamentoIntervento = 1;
                        accertamento.NoteInterventi = null;
                        ctx.SaveChanges();

                        StoricizzaStatoAccertamento(ctx, accertamento, 5805);

                        nonconformita.ToList().ForEach(b =>
                        {
                            b.fRealizzazioneIntervento = false;
                            b.DataRealizzazioneIntervento = null;
                            b.IDTipologiaInterventoAccertamento = 1;
                            b.NoteIntervento = null;
                            b.IDUtenteIntervento = null;
                        });
                        ctx.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        public static void SetDataScadenzaInterventoNexive(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                //Prendo il documento con data max visto che le raccomdante le dovrebbe ricevere insieme
                var documentiAccertamenti = ctx.VER_AccertamentoDocumento.Where(c => c.IDAccertamento == iDAccertamento && c.DataRecapito != null).OrderByDescending(c => c.DataRecapito).FirstOrDefault();
                var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                if (documentiAccertamenti != null)
                {
                    accertamento.DataRicevimentoRaccomandata = documentiAccertamenti.DataRecapito;
                    DateTime datascadenza = (DateTime)documentiAccertamenti.DataRecapito;
                    accertamento.DataScadenzaIntervento = datascadenza.AddDays((double)accertamento.GiorniRealizzazioneInterventi);
                    ctx.SaveChanges();
                }
            }
        }

        public static void SetDataScadenzaAccertamento(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                if (accertamento != null)
                {
                    accertamento.DataScadenzaIntervento = DateTime.Now.AddDays((double)accertamento.GiorniRealizzazioneInterventi);
                    ctx.SaveChanges();
                }
            }
        }

        public static string GetSqlValoriInterventiFilter(
            object iDTipoAccertamento,
            object iDSoggettoAzienda,
            object IDStatoAccertamentoIntervento,
            object DataInvioRaccomandataDal, object DataInvioRaccomandataAl,
            object DataRicevimentoRaccomandataDal, object DataRicevimentoRaccomandataAl,
            object DataScadenzaInterventoDal, object DataScadenzaInterventoAl,
            object CodiceAccertamento,
            object codiceTargaturaImpianto,
            object[] valoriCausaleSelected,
            object iDStatoAccertamentoSanzione
            )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_VER_Accertamenti ");
            strSql.Append(" WHERE IDTipoAccertamento=" + iDTipoAccertamento + " AND IDStatoAccertamento=5 AND IDAccertamento IN (SELECT DISTINCT IDAccertamento FROM VER_AccertamentoNonConformita WHERE (fRaccomandazioneConferma=1 OR fPrescrizioneConferma=1)) ");

            if ((iDSoggettoAzienda != "") && (iDSoggettoAzienda != "-1") && (iDSoggettoAzienda != null))
            {
                strSql.Append(" AND IDSoggetto=" + iDSoggettoAzienda);
            }

            if (IDStatoAccertamentoIntervento.ToString() != "0")
            {
                strSql.Append(" AND IDStatoAccertamentoIntervento=");
                strSql.Append(IDStatoAccertamentoIntervento);
            }

            if (iDStatoAccertamentoSanzione.ToString() != "0")
            {
                strSql.Append(" AND IDStatoAccertamentoSanzione=");
                strSql.Append(iDStatoAccertamentoSanzione);
            }

            if (codiceTargaturaImpianto.ToString() != "")
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(codiceTargaturaImpianto);
                strSql.Append("'");
            }

            if (CodiceAccertamento.ToString() != "")
            {
                strSql.Append(" AND CodiceAccertamento = ");
                strSql.Append("'");
                strSql.Append(CodiceAccertamento);
                strSql.Append("'");
            }

            if ((DataInvioRaccomandataDal != null) && (DataInvioRaccomandataDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInvioRaccomandata, 126) >= '");
                DateTime dataInvioRaccomandataDa = DateTime.Parse(DataInvioRaccomandataDal.ToString());
                string newDataInvioRaccomandataDa = dataInvioRaccomandataDa.ToString("yyyy") + "-" + dataInvioRaccomandataDa.ToString("MM") + "-" + dataInvioRaccomandataDa.ToString("dd");
                strSql.Append(newDataInvioRaccomandataDa);
                strSql.Append("'");
            }

            if ((DataInvioRaccomandataAl != null) && (DataInvioRaccomandataAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInvioRaccomandata, 126) <= '");
                DateTime dataInvioRaccomandataAl = DateTime.Parse(DataInvioRaccomandataAl.ToString());
                string newdataInvioRaccomandataAl = dataInvioRaccomandataAl.ToString("yyyy") + "-" + dataInvioRaccomandataAl.ToString("MM") + "-" + dataInvioRaccomandataAl.ToString("dd");
                strSql.Append(newdataInvioRaccomandataAl);
                strSql.Append("'");
            }

            if ((DataRicevimentoRaccomandataDal != null) && (DataRicevimentoRaccomandataDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataRicevimentoRaccomandata, 126) >= '");
                DateTime dataRicevimentoRaccomandataDa = DateTime.Parse(DataRicevimentoRaccomandataDal.ToString());
                string newDataRicevimentoRaccomandataDa = dataRicevimentoRaccomandataDa.ToString("yyyy") + "-" + dataRicevimentoRaccomandataDa.ToString("MM") + "-" + dataRicevimentoRaccomandataDa.ToString("dd");
                strSql.Append(newDataRicevimentoRaccomandataDa);
                strSql.Append("'");
            }

            if ((DataRicevimentoRaccomandataAl != null) && (DataRicevimentoRaccomandataAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataRicevimentoRaccomandata, 126) <= '");
                DateTime dataRicevimentoRaccomandataAl = DateTime.Parse(DataRicevimentoRaccomandataAl.ToString());
                string newdataRicevimentoRaccomandataAl = dataRicevimentoRaccomandataAl.ToString("yyyy") + "-" + dataRicevimentoRaccomandataAl.ToString("MM") + "-" + dataRicevimentoRaccomandataAl.ToString("dd");
                strSql.Append(newdataRicevimentoRaccomandataAl);
                strSql.Append("'");
            }

            if ((DataScadenzaInterventoDal != null) && (DataScadenzaInterventoDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataScadenzaIntervento, 126) >= '");
                DateTime dataScadenzaInterventoDa = DateTime.Parse(DataScadenzaInterventoDal.ToString());
                string newDataScadenzaInterventoDa = dataScadenzaInterventoDa.ToString("yyyy") + "-" + dataScadenzaInterventoDa.ToString("MM") + "-" + dataScadenzaInterventoDa.ToString("dd");
                strSql.Append(newDataScadenzaInterventoDa);
                strSql.Append("'");
            }

            if ((DataScadenzaInterventoAl != null) && (DataScadenzaInterventoAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataScadenzaIntervento, 126) <= '");
                DateTime dataScadenzaInterventoAl = DateTime.Parse(DataScadenzaInterventoAl.ToString());
                string newdataScadenzaInterventoAl = dataScadenzaInterventoAl.ToString("yyyy") + "-" + dataScadenzaInterventoAl.ToString("MM") + "-" + dataScadenzaInterventoAl.ToString("dd");
                strSql.Append(newdataScadenzaInterventoAl);
                strSql.Append("'");
            }

            //

            if (valoriCausaleSelected != null)
            {
                for (int i = 0; i < valoriCausaleSelected.Length; i++)
                {
                    strSql.Append("AND IDAccertamento IN(SELECT IDAccertamento FROM VER_AccertamentoDocumento WHERE 1=1 AND ( IDAccertamento IS NULL  ");

                    if (valoriCausaleSelected[i].ToString() == "1") //Giacenza - Assente inizio giacenza 
                    {
                        strSql.Append(" OR IDCausale = 1 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "2") //Reso - Calamità naturali
                    {
                        strSql.Append(" OR IDCausale = 2 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "3") //Reso - Casella bancaria 
                    {
                        strSql.Append(" OR IDCausale = 3 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "4") //Postalizzata o reso - Casella postale 
                    {
                        strSql.Append(" OR IDCausale = 4 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "5") //Reso - Cessata attività 
                    {
                        strSql.Append(" OR IDCausale = 5 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "6") //Reso - Chiuso per ferie 
                    {
                        strSql.Append(" OR IDCausale = 6 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "7") //Recapitata - Codice cliente illeggibile  
                    {
                        strSql.Append(" OR IDCausale = 7 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "8") //Reso - Compiuta giacenza  
                    {
                        strSql.Append(" OR IDCausale = 8 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "9") //Reso - Deceduto 
                    {
                        strSql.Append(" OR IDCausale = 9 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "10") //Reso - Fattorino: mancato appuntamento 
                    {
                        strSql.Append(" OR IDCausale = 10 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "11") //Reso - Fermo posta festività  
                    {
                        strSql.Append(" OR IDCausale = 11 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "12") //Reso - Indirizzo errato  
                    {
                        strSql.Append(" OR IDCausale = 12 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "13") //Reso - Indirizzo insufficiente  
                    {
                        strSql.Append(" OR IDCausale = 13 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "14") //Reso - Non postalizzata: restituita  
                    {
                        strSql.Append(" OR IDCausale = 14 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "15") //Recapitata - Non certificata  
                    {
                        strSql.Append(" OR IDCausale = 15 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "16") //Reso - Respinto 
                    {
                        strSql.Append(" OR IDCausale = 16 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "17") //Recapitata - Ritirata dal destinatario
                    {
                        strSql.Append(" OR IDCausale = 17 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "18") //Recapitata - Ritiro digitale 
                    {
                        strSql.Append(" OR IDCausale = 18 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "19") //Reso - Sconosciuto 
                    {
                        strSql.Append(" OR IDCausale = 19 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "20") //Reso - Stabile demolito 
                    {
                        strSql.Append(" OR IDCausale = 20 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "21") //Reso - Stabile inaccessibile 
                    {
                        strSql.Append(" OR IDCausale = 21 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "22") //Reso - Trasferito 
                    {
                        strSql.Append(" OR IDCausale = 22 ");
                    }
                    else if (valoriCausaleSelected[i].ToString() == "23") //Postalizzata o reso - Zone non servite posta  
                    {
                        strSql.Append(" OR IDCausale = 23 ");
                    }

                    strSql.Append(" ))");
                }
            }

            strSql.Append(" ORDER BY DataScadenzaIntervento DESC");

            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public static void SaveNonConformitaIntervento(long iDNonConformita,
                                                       bool fRealizzazioneIntervento,
                                                       DateTime? dataRealizzazioneIntervento,
                                                       int? iDTipologiaInterventoAccertamento,
                                                       string noteIntervento,
                                                       int? iDUtenteIntervento
                                                      )
        {
            using (var ctx = new CriterDataModel())
            {
                var nc = ctx.VER_AccertamentoNonConformita.Where(c => c.IDNonConformita == iDNonConformita).FirstOrDefault();
                nc.fRealizzazioneIntervento = fRealizzazioneIntervento;
                if (dataRealizzazioneIntervento != null)
                {
                    nc.DataRealizzazioneIntervento = dataRealizzazioneIntervento;
                }
                else
                {
                    nc.DataRealizzazioneIntervento = null;
                }
                if (iDTipologiaInterventoAccertamento != null)
                {
                    nc.IDTipologiaInterventoAccertamento = iDTipologiaInterventoAccertamento;
                }
                if (!string.IsNullOrEmpty(noteIntervento))
                {
                    nc.NoteIntervento = noteIntervento;
                }
                else
                {
                    nc.NoteIntervento = null;
                }
                if (iDUtenteIntervento != null)
                {
                    nc.IDUtenteIntervento = iDUtenteIntervento;
                }
                else
                {
                    nc.IDUtenteIntervento = null;
                }

                ctx.SaveChanges();
            }
        }

        public static void CambiaStatoIntervento(long iDAccertamento, int iDStatoAccertamentoIntervento)
        {
            using (var ctx = new CriterDataModel())
            {
                var intervento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                intervento.IDStatoAccertamentoIntervento = iDStatoAccertamentoIntervento;

                try
                {
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        public static void SaveIntervento(long iDAccertamento,
                                          string noteInterventi
                                          )
        {
            using (var ctx = new CriterDataModel())
            {
                var intervento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();

                if (!string.IsNullOrEmpty(noteInterventi))
                {
                    intervento.NoteInterventi = noteInterventi;
                }
                else
                {
                    intervento.NoteInterventi = null;
                }

                ctx.SaveChanges();
            }
        }

        public static void ControllaInterventiScaduti()
        {
            using (var ctx = new CriterDataModel())
            {
                var interventiScaduti = ctx.VER_Accertamento.Where(c => (c.IDStatoAccertamentoIntervento == 1
                                                                         || c.IDStatoAccertamentoIntervento == 3
                                                                        )
                                                                        && c.DataScadenzaIntervento < DateTime.Now
                                                                        ).ToList();

                if (interventiScaduti != null)
                {
                    foreach (var intervento in interventiScaduti)
                    {
                        //Per le procedure AC-01 o AC-03 cambio lo stato degli interventi in scadenza e poi
                        //sarà il coordinatore a decidere se mandare in ispezione oppure no
                        CambiaStatoIntervento(intervento.IDAccertamento, 8);


                        //var documenti = GetDocumentiAccertamenti(item.IDAccertamento);
                        //foreach (var documento in documenti)
                        //{
                        //    if (documento.IDProceduraAccertamento == 1 || documento.IDProceduraAccertamento == 3)
                        //    {
                        //        //Per le procedure AC-01 o AC-03 cambio lo stato degli interventi in scadenza e poi
                        //        //sarà il coordinatore a decidere se mandare in ispezione oppure no
                        //        CambiaStatoIntervento(item.IDAccertamento, 8);
                        //        break;
                        //    }
                        //    else if (documento.IDProceduraAccertamento == 2 || documento.IDProceduraAccertamento == 4)
                        //    {
                        //        //Per le procedure AC-02 o AC-04 cambio lo stato e metto in Sanzione
                        //        CambiaStatoIntervento(item.IDAccertamento, 7);

                        //        //TODO: Parte una raccomandata Nexive

                        //        break;
                        //    }
                        //}
                    }
                }
            }
        }

        public static void ControllaInterventiPresenzaNuovoRCT(int iDTargaturaImpianto, string prefisso, int codiceProgressivo, DateTime dataControllo)
        {
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    var rapporti = (from a in ctx.VER_Accertamento
                                    where (a.IDTargaturaImpianto == iDTargaturaImpianto
                                            && a.CodiceProgressivo == codiceProgressivo
                                            && a.Prefisso == prefisso
                                            && (a.IDStatoAccertamentoIntervento == 1 || a.IDStatoAccertamentoIntervento == 3)
                                    //&& a.DataRicevimentoRaccomandata >= dataControllo && a.DataScadenzaIntervento <= dataControllo
                                    )
                                    select new
                                    {
                                        iDAccertamento = a.IDAccertamento
                                    }
                                 ).ToList();

                    if (rapporti != null)
                    {
                        foreach (var r in rapporti)
                        {
                            CambiaStatoIntervento(r.iDAccertamento, 2);
                            //EmailNotify.SendMailInterventiPresenzaNuovoRCT(r.iDAccertamento);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static List<VER_AccertamentoNonConformita> GetNonConformitaInterventi(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var nonconformita = ctx.VER_AccertamentoNonConformita.AsQueryable();
                nonconformita = nonconformita.Where(a => a.IDAccertamento == iDAccertamento && (a.fRaccomandazioneConferma == true || a.fPrescrizioneConferma == true));

                return nonconformita.ToList();
            }
        }

        public static void CambiaStatoInterventoFromNonConformita(long iDAccertamento, int iDStatoAccertamentoIntervento)
        {
            switch (iDStatoAccertamentoIntervento)
            {
                case 1: //Interventi in attesa di realizzazione

                    break;
                case 2: //Interventi realizzati in attesa di conferma

                    break;
                case 3://Interventi parzialmente realizzati

                    break;
                case 4: //Interventi correttamente realizzati
                    break;
                case 5: //Interventi non realizzati - Ispezione

                    break;
                case 6: //Interventi non realizzati - Notifica verbale di accertamento con diffida

                    break;
                case 7: //Interventi non realizzati - Notifica verbale di accertamento con sanzione

                    break;
            }


            //Logica cambio stato intervento
            int ncTotali = GetNonConformitaInterventi(iDAccertamento).Count;
            int ncRisolte = GetNonConformitaInterventi(iDAccertamento).Where(a => a.fRealizzazioneIntervento == true).ToList().Count;
            int ncNonRisolte = GetNonConformitaInterventi(iDAccertamento).Where(a => a.fRealizzazioneIntervento == false).ToList().Count;

            if (ncRisolte == ncTotali)
            {
                //Tutte le nc sono risolte
                CambiaStatoIntervento(iDAccertamento, 4);
            }
            else if (ncNonRisolte == ncTotali)
            {
                //Nessuna nc è stata risolta
                CambiaStatoIntervento(iDAccertamento, 1);
            }
            else if (ncRisolte < ncTotali)
            {
                //Le nc sono risolte parzialmente
                CambiaStatoIntervento(iDAccertamento, 3);
            }
        }

        public static void SetDataRicevimentoRaccomandataIntervento(long iDAccertamento, string dataRecapito)
        {
            using (var ctx = new CriterDataModel())
            {
                DateTime dataRecapitoI;
                if (DateTime.TryParse(dataRecapito, out dataRecapitoI))
                {
                    var intervento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                    intervento.DataRicevimentoRaccomandata = dataRecapitoI;
                    ctx.SaveChanges();
                }
            }
        }

        public static void SetDataScadenzaIntervento(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var intervento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                intervento.DataScadenzaIntervento = DateTime.Parse(intervento.DataRicevimentoRaccomandata.ToString()).AddDays((double)intervento.GiorniRealizzazioneInterventi);
                ctx.SaveChanges();
            }
        }






        #endregion

        #region Programma Ispezioni

        public static string GetSqlValoriLibrettiNelProgrammaIspezione(object iDProgrammaIspezione,
                                                                       object iDCodiceCatastale,
                                                                       object foglio,
                                                                       object mappale,
                                                                       object subalterno,
                                                                       object identificativo,
                                                                       object iDCombustibile,
                                                                       object PotenzaDa,
                                                                       object PotenzaA)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_LIM_LibrettiImpianti ");
            strSql.Append(" WHERE fAttivo=1 ");
            strSql.Append(" AND IDLibrettoImpianto IN ");
            strSql.Append(" ( SELECT IDLibrettoImpianto FROM VER_ProgrammaIspezioneInfo WHERE fInVisitaIspettiva=0 AND IDProgrammaIspezione=" + iDProgrammaIspezione + ")");

            strSql.Append(" ORDER BY IDLibrettoImpianto DESC");



            return strSql.ToString();
        }

        public static string GetSqlValoriAccertamentiProgrammaIspezioneFilter(
                                                object iDCodiceCatastale,
                                                object foglio,
                                                object mappale,
                                                object subalterno,
                                                object identificativo,
                                                string codicePod,
                                                string codicePdr,
                                                string PotenzaDa,
                                                string PotenzaA,
                                                object combustibile,
                                                object tipologiaGruppoTermico,
                                                object DataInstallazioneDa, object DataInstallazioneA,
                                                object DataInserimentoDa, object DataInserimentoA,
                                                string codiceTargatura
            )
        {
            StringBuilder strSql = new StringBuilder();

            //strSql.Append(" SELECT dbo.V_VER_Accertamenti.IDAccertamento, dbo.V_VER_Accertamenti.IDRapportoDiControlloTecnicoBase, ");
            //strSql.Append(" dbo.V_VER_Accertamenti.IDStatoAccertamento, dbo.V_VER_Accertamenti.CodiceAccertamento,  ");
            //strSql.Append(" dbo.V_VER_Accertamenti.fAttivo, dbo.V_VER_Accertamenti.IDLibrettoImpianto, dbo.V_VER_Accertamenti.CodiceTargatura, ");
            //strSql.Append(" dbo.V_VER_Accertamenti.IDStatoAccertamentoIntervento, dbo.RCT_RapportoDiControlloTecnicoGT.IDLIM_LibrettiImpiantiGruppitermici, ");
            //strSql.Append(" dbo.V_LIM_LibrettiImpianti.DataInserimento, dbo.LIM_LibrettiImpiantiGruppiTermici.DataInstallazione,  ");
            //strSql.Append(" dbo.V_VER_Accertamenti.IDCodiceCatastale, dbo.V_VER_Accertamenti.Indirizzo, dbo.V_VER_Accertamenti.Civico, ");
            //strSql.Append(" dbo.V_VER_Accertamenti.Comune, dbo.V_VER_Accertamenti.SiglaProvincia, dbo.V_VER_Accertamenti.PotenzaTermicaNominale, ");
            //strSql.Append(" dbo.V_VER_Accertamenti.CodiceProgressivo, dbo.V_VER_Accertamenti.Prefisso, dbo.V_VER_Accertamenti.DataScadenzaIntervento, ");
            //strSql.Append(" dbo.V_VER_Accertamenti.StatoAccertamento, dbo.V_VER_Accertamenti.TipologiaCombustibile, dbo.V_VER_Accertamenti.IDTargaturaImpianto, ");
            //strSql.Append(" dbo.V_VER_Accertamenti.StatoAccertamentoIntervento, dbo.V_VER_Accertamenti.IDTipologiaCombustibile, ");
            //strSql.Append(" dbo.V_LIM_LibrettiImpianti.NumeroPDR, dbo.V_LIM_LibrettiImpianti.NumeroPOD ");
            //strSql.Append(" FROM dbo.V_VER_Accertamenti INNER JOIN ");
            //strSql.Append(" dbo.RCT_RapportoDiControlloTecnicoGT ON dbo.V_VER_Accertamenti.IDRapportoDiControlloTecnicoBase = dbo.RCT_RapportoDiControlloTecnicoGT.Id INNER JOIN ");
            //strSql.Append(" dbo.LIM_LibrettiImpiantiGruppiTermici ON dbo.RCT_RapportoDiControlloTecnicoGT.IDLIM_LibrettiImpiantiGruppitermici = dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico INNER JOIN ");
            //strSql.Append(" dbo.V_LIM_LibrettiImpianti ON dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto = dbo.V_LIM_LibrettiImpianti.IDLibrettoImpianto ");
            //strSql.Append(" WHERE(dbo.V_VER_Accertamenti.IDStatoAccertamento = 5) AND(dbo.V_VER_Accertamenti.IDStatoAccertamentoIntervento = 9) AND(dbo.V_VER_Accertamenti.fAttivo = 1) ");
            ////strSql.Append(" AND ( dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico NOT IN ");
            ////strSql.Append("( SELECT IDLibrettoImpiantoGruppoTermico FROM VER_ProgrammaIspezioneInfo WHERE (dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico = IDLibrettoImpiantoGruppoTermico )))");

            strSql.Append(" SELECT V_VER_Accertamenti.IDAccertamento, V_VER_Accertamenti.IDRapportoDiControlloTecnicoBase, V_VER_Accertamenti.IDStatoAccertamento, V_VER_Accertamenti.CodiceAccertamento,  ");
            strSql.Append(" V_VER_Accertamenti.fAttivo, V_VER_Accertamenti.IDLibrettoImpianto, V_VER_Accertamenti.CodiceTargatura, V_VER_Accertamenti.IDStatoAccertamentoIntervento, V_LIM_LibrettiImpianti.DataInserimento,  ");
            strSql.Append(" LIM_LibrettiImpiantiGruppiTermici.DataInstallazione, V_VER_Accertamenti.IDCodiceCatastale, V_VER_Accertamenti.Indirizzo, V_VER_Accertamenti.Civico, V_VER_Accertamenti.Comune, V_VER_Accertamenti.SiglaProvincia, V_VER_Accertamenti.Prefisso, V_VER_Accertamenti.CodiceProgressivo, V_VER_Accertamenti.DataScadenzaIntervento, V_VER_Accertamenti.PotenzaTermicaNominale, ");
            strSql.Append(" V_VER_Accertamenti.StatoAccertamento, V_VER_Accertamenti.TipologiaCombustibile, V_VER_Accertamenti.IDTargaturaImpianto, V_VER_Accertamenti.StatoAccertamentoIntervento, ");
            strSql.Append(" V_VER_Accertamenti.IDTipologiaCombustibile, V_LIM_LibrettiImpianti.NumeroPDR, V_LIM_LibrettiImpianti.NumeroPOD, LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico AS IDLIM_LibrettiImpiantiGruppitermici ");
            strSql.Append(" FROM LIM_LibrettiImpiantiGruppiTermici INNER JOIN V_VER_Accertamenti ON LIM_LibrettiImpiantiGruppiTermici.CodiceProgressivo = V_VER_Accertamenti.CodiceProgressivo AND ");
            strSql.Append(" LIM_LibrettiImpiantiGruppiTermici.Prefisso = V_VER_Accertamenti.Prefisso AND LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto = V_VER_Accertamenti.IDLibrettoImpianto INNER JOIN ");
            strSql.Append(" V_LIM_LibrettiImpianti ON LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto = V_LIM_LibrettiImpianti.IDLibrettoImpianto ");
            strSql.Append(" WHERE  (V_VER_Accertamenti.IDStatoAccertamento = 5) AND (V_VER_Accertamenti.IDStatoAccertamentoIntervento = 9) AND (V_VER_Accertamenti.fAttivo = 1) AND (LIM_LibrettiImpiantiGruppiTermici.fAttivo = 1) ");
            
            strSql.Append(" AND (dbo.V_VER_Accertamenti.IDTargaturaImpianto NOT IN");
            strSql.Append("     (SELECT IDTargaturaImpianto FROM dbo.VER_ProgrammaIspezioneInfo WHERE dbo.V_VER_Accertamenti.IDTargaturaImpianto = IDTargaturaImpianto))");

            if (codiceTargatura != "")
            {
                strSql.Append(" AND ( dbo.V_VER_Accertamenti.CodiceTargatura LIKE N'%" + codiceTargatura + "%') ");
            }

            if ((DataInstallazioneDa != null) && (DataInstallazioneDa.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.LIM_LibrettiImpiantiGruppiTermici.DataInstallazione, 126) >= '");
                DateTime dataInstallazioneDa = DateTime.Parse(DataInstallazioneDa.ToString());
                string newDataInstallazioneDa = dataInstallazioneDa.ToString("yyyy") + "-" + dataInstallazioneDa.ToString("MM") + "-" + dataInstallazioneDa.ToString("dd");
                strSql.Append(newDataInstallazioneDa);
                strSql.Append("'");
            }

            if ((DataInstallazioneA != null) && (DataInstallazioneA.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.LIM_LibrettiImpiantiGruppiTermici.DataInstallazione, 126) <= '");
                DateTime dataInstallazioneA = DateTime.Parse(DataInstallazioneA.ToString());
                string newdataInstallazioneA = dataInstallazioneA.ToString("yyyy") + "-" + dataInstallazioneA.ToString("MM") + "-" + dataInstallazioneA.ToString("dd");
                strSql.Append(newdataInstallazioneA);
                strSql.Append("'");
            }

            if ((DataInserimentoDa != null) && (DataInserimentoDa.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.V_LIM_LibrettiImpianti.DataInserimento, 126) >= '");
                DateTime dataInserimentoDa = DateTime.Parse(DataInserimentoDa.ToString());
                string newDataInserimentoDa = dataInserimentoDa.ToString("yyyy") + "-" + dataInserimentoDa.ToString("MM") + "-" + dataInserimentoDa.ToString("dd");
                strSql.Append(newDataInserimentoDa);
                strSql.Append("'");
            }

            if ((DataInserimentoA != null) && (DataInserimentoA.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.V_LIM_LibrettiImpianti.DataInserimento, 126) <= '");
                DateTime dataInserimentoA = DateTime.Parse(DataInserimentoA.ToString());
                string newdataInserimentoA = dataInserimentoA.ToString("yyyy") + "-" + dataInserimentoA.ToString("MM") + "-" + dataInserimentoA.ToString("dd");
                strSql.Append(newdataInserimentoA);
                strSql.Append("'");
            }

            if (combustibile != null && combustibile.ToString() != "")
            {
                strSql.Append(" AND dbo.V_VER_Accertamenti.IDTipologiaCombustibile =" + combustibile + " ");
            }

            if (tipologiaGruppoTermico != null && tipologiaGruppoTermico.ToString() != "")
            {
                strSql.Append(" AND dbo.LIM_LibrettiImpiantiGruppiTermici.IDTipologiaGruppiTermici =" + tipologiaGruppoTermico + " ");
            }

            if ((PotenzaDa.ToString() != "") || (PotenzaA.ToString() != ""))
            {
                if (PotenzaDa.ToString() != "" && PotenzaA.ToString() != "")
                {
                    strSql.Append(" AND ( dbo.V_VER_Accertamenti.PotenzaTermicaNominale >= " + PotenzaDa + " AND dbo.V_VER_Accertamenti.PotenzaTermicaNominale <= " + PotenzaA + " )");
                }
                else
                {
                    if (PotenzaDa.ToString() != "")
                    {
                        strSql.Append(" AND dbo.V_VER_Accertamenti.PotenzaTermicaNominale >= " + PotenzaDa);
                    }

                    if (PotenzaA.ToString() != "")
                    {
                        strSql.Append(" AND dbo.V_VER_Accertamenti.PotenzaTermicaNominale <= " + PotenzaA);
                    }
                }
            }

            //Caso Particolare
            if ((codicePod.ToString() != "") || (codicePdr.ToString() != ""))
            {
                if (codicePod.ToString() == codicePdr.ToString())
                {
                    strSql.Append(" AND (dbo.V_LIM_LibrettiImpianti.NumeroPOD = ");
                    strSql.Append("'");
                    strSql.Append(codicePod);
                    strSql.Append("'");
                    strSql.Append(" OR dbo.V_LIM_LibrettiImpianti.NumeroPDR = ");
                    strSql.Append("'");
                    strSql.Append(codicePdr);
                    strSql.Append("')");
                }
                else
                {
                    if (codicePod.ToString() != "")
                    {
                        strSql.Append(" AND dbo.V_LIM_LibrettiImpianti.NumeroPOD = ");
                        strSql.Append("'");
                        strSql.Append(codicePod);
                        strSql.Append("'");
                    }

                    if (codicePdr.ToString() != "")
                    {
                        strSql.Append(" AND dbo.V_LIM_LibrettiImpianti.NumeroPDR = ");
                        strSql.Append("'");
                        strSql.Append(codicePdr);
                        strSql.Append("'");
                    }
                }
            }

            if (iDCodiceCatastale != null)
            {
                strSql.Append(" AND dbo.V_VER_Accertamenti.IDCodiceCatastale=" + iDCodiceCatastale + "");
            }

            if ((foglio.ToString() != "") || (mappale.ToString() != "") || (subalterno.ToString() != "") || (identificativo.ToString() != ""))
            {
                strSql.Append(" AND dbo.V_VER_Accertamenti.IDLibrettoImpianto IN ");
                string sqlDatiCatastali = "";

                if (foglio.ToString() != "")
                {
                    sqlDatiCatastali += "AND Foglio = '" + foglio.ToString() + "'";
                }
                if (mappale.ToString() != "")
                {
                    sqlDatiCatastali += "AND Mappale = '" + mappale.ToString() + "'";
                }
                if (subalterno.ToString() != "")
                {
                    sqlDatiCatastali += "AND Subalterno = '" + subalterno.ToString() + "'";
                }

                strSql.Append("( SELECT IDLibrettoImpianto FROM LIM_LibrettiImpiantiDatiCatastali WHERE 1=1 " + sqlDatiCatastali + ")");
            }

            strSql.Append(" ORDER BY NEWID()");
            return strSql.ToString();
        }


        public static string GetSqlValoriLibrettiProgrammaIspezioneFilter(
                                                object iDCodiceCatastale,
                                                object foglio,
                                                object mappale,
                                                object subalterno,
                                                object identificativo,
                                                string codicePod,
                                                string codicePdr,
                                                string PotenzaDa,
                                                string PotenzaA,
                                                object combustibile,
                                                object tipologiaGruppoTermico,
                                                object DataInstallazioneDa, object DataInstallazioneA,
                                                object DataInserimentoDa, object DataInserimentoA,
                                                string codiceTargatura
        )
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico, ");
            strSql.Append("dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto, ");
            strSql.Append("dbo.LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile, ");
            strSql.Append("dbo.SYS_TipologiaCombustibile.TipologiaCombustibile, ");
            strSql.Append("dbo.LIM_LibrettiImpiantiGruppiTermici.CombustibileAltro, ");
            strSql.Append("dbo.LIM_LibrettiImpianti.IDTargaturaImpianto, ");
            strSql.Append("dbo.LIM_TargatureImpianti.CodiceTargatura, ");
            strSql.Append("dbo.LIM_LibrettiImpianti.NumeroRevisione, ");
            strSql.Append("dbo.LIM_LibrettiImpianti.IDCodiceCatastale, ");
            strSql.Append("dbo.SYS_CodiciCatastali.CodiceCatastale + N' - ' + dbo.SYS_CodiciCatastali.Comune AS CodiceCatastale,");
            strSql.Append("dbo.LIM_LibrettiImpiantiGruppiTermici.Prefisso, dbo.LIM_LibrettiImpiantiGruppiTermici.CodiceProgressivo,  ");
            strSql.Append("dbo.LIM_LibrettiImpianti.Indirizzo, dbo.LIM_LibrettiImpianti.Civico, ");
            strSql.Append("dbo.LIM_LibrettiImpianti.DataInserimento, ");
            strSql.Append("dbo.LIM_LibrettiImpiantiGruppiTermici.DataInstallazione, ");
            strSql.Append("dbo.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw, ");
            strSql.Append("dbo.LIM_LibrettiImpianti.NumeroPDR, dbo.LIM_LibrettiImpianti.NumeroPOD");
            strSql.Append(" FROM dbo.LIM_LibrettiImpianti");
            strSql.Append(" INNER JOIN dbo.LIM_LibrettiImpiantiGruppiTermici ON dbo.LIM_LibrettiImpianti.IDLibrettoImpianto = dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto");
            strSql.Append(" INNER JOIN dbo.LIM_TargatureImpianti ON dbo.LIM_LibrettiImpianti.IDTargaturaImpianto = dbo.LIM_TargatureImpianti.IDTargaturaImpianto");
            strSql.Append(" INNER JOIN dbo.SYS_CodiciCatastali ON dbo.LIM_LibrettiImpianti.IDCodiceCatastale = dbo.SYS_CodiciCatastali.IDCodiceCatastale");
            strSql.Append(" INNER JOIN dbo.SYS_TipologiaCombustibile ON dbo.LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile = dbo.SYS_TipologiaCombustibile.IDTipologiaCombustibile");
            strSql.Append(" WHERE");
            strSql.Append(" (dbo.LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2)");
            strSql.Append(" AND (dbo.LIM_LibrettiImpianti.fAttivo = 1)");
            strSql.Append(" AND (dbo.LIM_LibrettiImpiantiGruppiTermici.fAttivo = 1)");
            strSql.Append(" AND (dbo.LIM_LibrettiImpiantiGruppiTermici.fDismesso = 0)");
            //strSql.Append(" AND (dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico NOT IN");
            //strSql.Append("     (SELECT IDLibrettoImpiantoGruppoTermico FROM dbo.VER_ProgrammaIspezioneInfo WHERE dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico = IDLibrettoImpiantoGruppoTermico))");
            strSql.Append(" AND (dbo.LIM_LibrettiImpianti.IDTargaturaImpianto NOT IN");
            strSql.Append("     (SELECT DISTINCT IDTargaturaImpianto FROM dbo.VER_ProgrammaIspezioneInfo WHERE  dbo.LIM_LibrettiImpianti.IDTargaturaImpianto = IDTargaturaImpianto))"); //MOFICATO IL 30/07/2021 (YEAR(DataInserimento) + 3 < GETDATE()) AND

            if (codiceTargatura != "")
            {
                strSql.Append(" AND ( dbo.LIM_TargatureImpianti.CodiceTargatura LIKE N'%" + codiceTargatura + "%') ");
            }

            if ((DataInstallazioneDa != null) && (DataInstallazioneDa.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.LIM_LibrettiImpiantiGruppiTermici.DataInstallazione, 126) >= '");
                DateTime dataInstallazioneDa = DateTime.Parse(DataInstallazioneDa.ToString());
                string newDataInstallazioneDa = dataInstallazioneDa.ToString("yyyy") + "-" + dataInstallazioneDa.ToString("MM") + "-" + dataInstallazioneDa.ToString("dd");
                strSql.Append(newDataInstallazioneDa);
                strSql.Append("'");
            }

            if ((DataInstallazioneA != null) && (DataInstallazioneA.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.LIM_LibrettiImpiantiGruppiTermici.DataInstallazione, 126) <= '");
                DateTime dataInstallazioneA = DateTime.Parse(DataInstallazioneA.ToString());
                string newdataInstallazioneA = dataInstallazioneA.ToString("yyyy") + "-" + dataInstallazioneA.ToString("MM") + "-" + dataInstallazioneA.ToString("dd");
                strSql.Append(newdataInstallazioneA);
                strSql.Append("'");
            }

            if ((DataInserimentoDa != null) && (DataInserimentoDa.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.LIM_LibrettiImpianti.DataInserimento, 126) >= '");
                DateTime dataInserimentoDa = DateTime.Parse(DataInserimentoDa.ToString());
                string newDataInserimentoDa = dataInserimentoDa.ToString("yyyy") + "-" + dataInserimentoDa.ToString("MM") + "-" + dataInserimentoDa.ToString("dd");
                strSql.Append(newDataInserimentoDa);
                strSql.Append("'");
            }

            if ((DataInserimentoA != null) && (DataInserimentoA.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), dbo.LIM_LibrettiImpianti.DataInserimento, 126) <= '");
                DateTime dataInserimentoA = DateTime.Parse(DataInserimentoA.ToString());
                string newdataInserimentoA = dataInserimentoA.ToString("yyyy") + "-" + dataInserimentoA.ToString("MM") + "-" + dataInserimentoA.ToString("dd");
                strSql.Append(newdataInserimentoA);
                strSql.Append("'");
            }

            if (combustibile != null && combustibile.ToString() != "")
            {
                strSql.Append(" AND dbo.LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile =" + combustibile + " ");
            }

            if (tipologiaGruppoTermico != null && tipologiaGruppoTermico.ToString() != "")
            {
                strSql.Append(" AND dbo.LIM_LibrettiImpiantiGruppiTermici.IDTipologiaGruppiTermici =" + tipologiaGruppoTermico + " ");
            }

            if ((PotenzaDa.ToString() != "") || (PotenzaA.ToString() != ""))
            {
                if (PotenzaDa.ToString() != "" && PotenzaA.ToString() != "")
                {
                    strSql.Append(" AND ( dbo.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw >= " + PotenzaDa + " AND dbo.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw < " + PotenzaA + " )");
                }
                else
                {
                    if (PotenzaDa.ToString() != "")
                    {
                        strSql.Append(" AND dbo.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw >= " + PotenzaDa);
                    }

                    if (PotenzaA.ToString() != "")
                    {
                        strSql.Append(" AND dbo.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw < " + PotenzaA);
                    }
                }
            }

            //Caso Particolare
            if ((codicePod.ToString() != "") || (codicePdr.ToString() != ""))
            {
                if (codicePod.ToString() == codicePdr.ToString())
                {
                    strSql.Append(" AND (dbo.LIM_LibrettiImpianti.NumeroPOD = ");
                    strSql.Append("'");
                    strSql.Append(codicePod);
                    strSql.Append("'");
                    strSql.Append(" OR dbo.LIM_LibrettiImpianti.NumeroPDR = ");
                    strSql.Append("'");
                    strSql.Append(codicePdr);
                    strSql.Append("')");
                }
                else
                {
                    if (codicePod.ToString() != "")
                    {
                        strSql.Append(" AND dbo.LIM_LibrettiImpianti.NumeroPOD = ");
                        strSql.Append("'");
                        strSql.Append(codicePod);
                        strSql.Append("'");
                    }

                    if (codicePdr.ToString() != "")
                    {
                        strSql.Append(" AND dbo.LIM_LibrettiImpianti.NumeroPDR = ");
                        strSql.Append("'");
                        strSql.Append(codicePdr);
                        strSql.Append("'");
                    }
                }
            }

            if (iDCodiceCatastale != null)
            {
                strSql.Append(" AND dbo.LIM_LibrettiImpianti.IDCodiceCatastale=" + iDCodiceCatastale + "");
            }

            if ((foglio.ToString() != "") || (mappale.ToString() != "") || (subalterno.ToString() != "") || (identificativo.ToString() != ""))
            {
                strSql.Append(" AND dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto IN ");
                string sqlDatiCatastali = "";

                if (foglio.ToString() != "")
                {
                    sqlDatiCatastali += "AND Foglio = '" + foglio.ToString() + "'";
                }
                if (mappale.ToString() != "")
                {
                    sqlDatiCatastali += "AND Mappale = '" + mappale.ToString() + "'";
                }
                if (subalterno.ToString() != "")
                {
                    sqlDatiCatastali += "AND Subalterno = '" + subalterno.ToString() + "'";
                }

                strSql.Append("( SELECT dbo.LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto FROM LIM_LibrettiImpiantiDatiCatastali WHERE 1=1 " + sqlDatiCatastali + ")");
            }

            strSql.Append(" ORDER BY IDTargaturaImpianto");
            return strSql.ToString();
        }

        public static int GetIDProgrammaIspezioneAttivo()
        {
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    //Prendo tutti i programmi di Ispezioni abilitati in cui la data corrente ha superato la data di inizio, che non sono ancora scadute o che non hanno una scadenza
                    return
                        ctx.VER_ProgrammaIspezione.Where(
                            c => c.fAttivo == true //&& c.DataInizio <= DateTime.Now && c.DataFine >= DateTime.Now
                            )
                            .OrderBy(c => c.DataInizio)
                            .Select(c => c.IDProgrammaIspezione)
                            .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int SaveInsertDeleteDatiProgrammaIspezione(
                                string operationType,
                                int? iDProgrammaIspezione,
                                string descrizione,
                                DateTime dataInizio,
                                DateTime? dataFine,
                                bool fAttivo)
        {
            int? iDProgrammaIspezioneInsert = null;

            //var db = DataLayer.Common.ApplicationContext.Current.Context;
            using (var ctx = new CriterDataModel())
            {
                var programmaIspezione = new VER_ProgrammaIspezione();

                if (operationType == "update")
                {
                    programmaIspezione = ctx.VER_ProgrammaIspezione.FirstOrDefault(i => i.IDProgrammaIspezione == iDProgrammaIspezione);
                }
                if (!string.IsNullOrEmpty(descrizione))
                {
                    programmaIspezione.Descrizione = descrizione;
                }
                else
                {
                    programmaIspezione.Descrizione = null;
                }
                if (dataInizio != null)
                {
                    programmaIspezione.DataInizio = dataInizio;
                }
                programmaIspezione.DataFine = dataFine;
                programmaIspezione.fAttivo = fAttivo;

                if (operationType == "insert")
                {
                    ctx.VER_ProgrammaIspezione.Add(programmaIspezione);
                }


                try
                {
                    ctx.SaveChanges();
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
                    iDProgrammaIspezioneInsert = programmaIspezione.IDProgrammaIspezione;
                }
                else if (operationType == "update")
                {
                    iDProgrammaIspezioneInsert = iDProgrammaIspezione;
                }
            }

            return (int)iDProgrammaIspezioneInsert;
        }

        public static void InsertDeleteGeneratoreNelProgrammaIspezioneAttivo(string operationType, int iDProgrammaIspezione, int iDLibrettoImpianto, int iDTargaturaImpianto, long? iDAccertamento, int iDLibrettoImpiantoGruppoTermico)
        {
            using (var ctx = new CriterDataModel())
            {
                if (operationType == "Insert")
                {
                    var generatoreSuProgrammaIspezione = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto
                                                                                                   //&& c.IDProgrammaIspezione == iDProgrammaIspezione
                                                                                                   //&& c.fInVisitaIspettiva == false
                                                                                                   && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();

                    if (generatoreSuProgrammaIspezione == null)
                    {
                        var programmaInfo = new VER_ProgrammaIspezioneInfo();
                        if (iDAccertamento == null)
                        {
                            programmaInfo.IDProgrammaIspezione = iDProgrammaIspezione;
                            programmaInfo.IDAccertamento = null;
                            programmaInfo.IDLibrettoImpianto = iDLibrettoImpianto;
                            programmaInfo.IDTargaturaImpianto = iDTargaturaImpianto;
                            programmaInfo.IDTipoIspezione = 2;
                            programmaInfo.fInVisitaIspettiva = false;
                            programmaInfo.IDLibrettoImpiantoGruppoTermico = iDLibrettoImpiantoGruppoTermico;
                            programmaInfo.DataInserimento = DateTime.Now;
                        }
                        else
                        {
                            programmaInfo.IDProgrammaIspezione = iDProgrammaIspezione;
                            programmaInfo.IDAccertamento = iDAccertamento;
                            programmaInfo.IDLibrettoImpianto = iDLibrettoImpianto;
                            programmaInfo.IDTargaturaImpianto = iDTargaturaImpianto;
                            programmaInfo.IDTipoIspezione = 1;
                            programmaInfo.fInVisitaIspettiva = false;
                            programmaInfo.IDLibrettoImpiantoGruppoTermico = iDLibrettoImpiantoGruppoTermico;
                            programmaInfo.DataInserimento = DateTime.Now;
                        }

                        ctx.VER_ProgrammaIspezioneInfo.Add(programmaInfo);
                        ctx.SaveChanges();
                        InsertUpdateCoordinateLibretto(iDLibrettoImpianto);

                        //Logica per inserire in automatico a fronte di un libretto tutti i generatori facenti parte dei quel libretto
                        //if (iDAccertamento == null)
                        //{
                        //Importo solo generatori per tipologia di gruppi termici modulare o singolo
                        var ListiDTipologiaGruppiTermici = new List<int>() { 1, 2 };

                        var generatoriAltriLibretto = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto
                                                                                               && c.fAttivo == true
                                                                                               && c.fDismesso == false
                                                                                               && c.IDLibrettoImpiantoGruppoTermico != iDLibrettoImpiantoGruppoTermico
                                                                                               && ListiDTipologiaGruppiTermici.Contains(c.IDTipologiaGruppiTermici)
                                                                                               ).ToList();

                        //ERA LENTO!!!

                        //foreach (var generatore in generatoriLibretto)
                        //{
                        //    var programmaIspezioneInfo = new VER_ProgrammaIspezioneInfo();
                        //    programmaIspezioneInfo.IDProgrammaIspezione = iDProgrammaIspezione;
                        //    programmaIspezioneInfo.IDAccertamento = null;
                        //    programmaIspezioneInfo.IDLibrettoImpianto = iDLibrettoImpianto;
                        //    programmaIspezioneInfo.IDTipoIspezione = 2;
                        //    programmaIspezioneInfo.fInVisitaIspettiva = false;
                        //    programmaIspezioneInfo.IDLibrettoImpiantoGruppoTermico = generatore.IDLibrettoImpiantoGruppoTermico;
                        //    programmaIspezioneInfo.IDTargaturaImpianto = iDTargaturaImpianto;
                        //    programmaIspezioneInfo.DataInserimento = DateTime.Now;
                        //    ctx.VER_ProgrammaIspezioneInfo.Add(programmaIspezioneInfo);
                        //    ctx.SaveChanges();
                        //}

                        var generatoriLibretto = generatoriAltriLibretto.Select(s => new VER_ProgrammaIspezioneInfo()
                        {
                            IDProgrammaIspezione = iDProgrammaIspezione,
                            IDAccertamento = null,
                            IDLibrettoImpianto = iDLibrettoImpianto,
                            IDTipoIspezione = 2,
                            fInVisitaIspettiva = false,
                            IDLibrettoImpiantoGruppoTermico = s.IDLibrettoImpiantoGruppoTermico,
                            IDTargaturaImpianto = iDTargaturaImpianto,
                            DataInserimento = DateTime.Now
                        });

                        ctx.VER_ProgrammaIspezioneInfo.AddRange(generatoriLibretto);
                        ctx.SaveChanges();
                        //}
                    }
                }
                else if (operationType == "Delete")
                {
                    var Libretto = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.IDProgrammaIspezione == iDProgrammaIspezione && c.fInVisitaIspettiva == false && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                    ctx.VER_ProgrammaIspezioneInfo.Remove(Libretto);
                    ctx.SaveChanges();
                }
            }
        }

        public static void InsertUpdateCoordinateLibretto(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.Coordinate == null).FirstOrDefault();

                if (libretto != null)
                {
                    object[] coordinate = new object[2];

                    coordinate = UtilityApp.GetGeocodingAddress(libretto.Indirizzo + " ," + libretto.Civico,
                                                    libretto.SYS_CodiciCatastali.Cap,
                                                    libretto.SYS_CodiciCatastali.Comune, // città
                                                    "Italia"); // paese

                    if (coordinate != null)
                    {
                        if (coordinate[0] != null && coordinate[1] != null)
                        {
                            libretto.Coordinate = DbGeography.PointFromText(string.Format("POINT({0} {1})", coordinate[1].ToString().Replace(",", "."), coordinate[0].ToString().Replace(",", ".")), 4326);
                        }
                    }
                    else
                    {
                        libretto.Coordinate = null;
                    }

                    ctx.SaveChanges();
                }
            }
        }

        public static void InsertDeleteGeneratoreNelleVisiteIspettive(string operationType, long? iDIspezioneVisitaInfo, long iDIspezioneVisita, int iDLibrettoImpianto, int iDProgrammaIspezione, int iDLibrettoImpiantoGruppoTermico, int? iDAccertamento, object noteGeneratore)
        {
            using (var ctx = new CriterDataModel())
            {
                var programmaInfo = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.IDProgrammaIspezione == iDProgrammaIspezione && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();

                var ispezioneVisitaInfo = new VER_IspezioneVisitaInfo();

                if (operationType == "Insert")
                {
                    programmaInfo.fInVisitaIspettiva = true;

                    if (iDAccertamento == null)
                    {
                        //var IDRapportoControlloTecnicoPerLibretto = ctx.RCT_RapportoDiControlloTecnicoBase.OrderByDescending(c => c.DataControllo).Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.DataControllo != null).FirstOrDefault();

                        ispezioneVisitaInfo.IDIspezioneVisita = iDIspezioneVisita;
                        ispezioneVisitaInfo.IDAccertamento = null;
                        ispezioneVisitaInfo.IDLibrettoImpianto = iDLibrettoImpianto;
                        ispezioneVisitaInfo.IDTipoIspezione = 2;
                        //ispezioneVisitaInfo.IDRapportoControlloTecnico = IDRapportoControlloTecnicoPerLibretto.IDRapportoControlloTecnico;
                        ispezioneVisitaInfo.IDLibrettoImpiantoGruppoTermico = iDLibrettoImpiantoGruppoTermico;
                        ispezioneVisitaInfo.fInIspezione = false;
                        ispezioneVisitaInfo.NoteProgrammaIspezione = programmaInfo.NoteProgrammaIspezione;
                    }
                    else
                    {
                        //long? iDAccertamento = GetIDAccertamento(iDLibrettoImpianto, iDProgrammaIspezione);
                        //var IDRapportoControlloTecnicoPerAccertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();

                        ispezioneVisitaInfo.IDIspezioneVisita = iDIspezioneVisita;
                        ispezioneVisitaInfo.IDAccertamento = iDAccertamento;
                        ispezioneVisitaInfo.IDLibrettoImpianto = iDLibrettoImpianto;
                        ispezioneVisitaInfo.IDTipoIspezione = 1;
                        //ispezioneVisitaInfo.IDRapportoControlloTecnico = IDRapportoControlloTecnicoPerAccertamento.IDRapportoDiControlloTecnicoBase;
                        ispezioneVisitaInfo.IDLibrettoImpiantoGruppoTermico = iDLibrettoImpiantoGruppoTermico;
                        ispezioneVisitaInfo.fInIspezione = false;
                        ispezioneVisitaInfo.NoteProgrammaIspezione = programmaInfo.NoteProgrammaIspezione;
                    }

                    ctx.VER_IspezioneVisitaInfo.Add(ispezioneVisitaInfo);
                }
                else if (operationType == "Delete")
                {
                    int? iDTaragaturaImpianto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault().IDTargaturaImpianto;
                    int iDLibrettoImpiantoNuovo = ctx.LIM_LibrettiImpianti.Where(c => c.IDTargaturaImpianto == iDTaragaturaImpianto && c.fAttivo == true && c.IDStatoLibrettoImpianto == 2).FirstOrDefault().IDLibrettoImpianto;
                    var generatoreOld = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                    int iDLibrettoImpiantoGruppoTermicoNuovo = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpianto == iDLibrettoImpiantoNuovo && c.Prefisso == generatoreOld.Prefisso && c.CodiceProgressivo == generatoreOld.CodiceProgressivo).FirstOrDefault().IDLibrettoImpiantoGruppoTermico;

                    
                    var programmaInfoDelete = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDLibrettoImpianto == iDLibrettoImpiantoNuovo && c.IDProgrammaIspezione == iDProgrammaIspezione && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermicoNuovo).FirstOrDefault();
                    if (programmaInfoDelete != null)
                    {
                        programmaInfoDelete.fInVisitaIspettiva = false;
                    }
                    //FIX 30/09/2024: Poteva accadere che programmaInfoDelete era null perchè in VER_ProgrammaIspezioneInfo iDLibrettoImpiantoNuovo e iDLibrettoImpiantoGruppoTermicoNuovo non
                    //corrispondono quindi non aggiornerebbe il flag fInVisitaIspettiva e quindi il generatore verrebbe cancellato dalla visita ma non più visibile nella lista
                    //dei generatori nel programma ispezione. Allora in questo caso devo prendere in VER_ProgrammaIspezioneInfo  iDLibrettoImpianto e iDLibrettoImpiantoGruppoTermico che passo al metodo
                    else
                    {
                        var programmaInfoDeleteOldLibretto = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.IDProgrammaIspezione == iDProgrammaIspezione && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                        if (programmaInfoDeleteOldLibretto != null)
                        {
                            programmaInfoDeleteOldLibretto.fInVisitaIspettiva = false;
                        }
                    }

                    var generatore = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.IDIspezioneVisita == iDIspezioneVisita && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                    ctx.VER_IspezioneVisitaInfo.Remove(generatore);
                }
                else if (operationType == "AddNoteGeneratore")
                {
                    if (noteGeneratore != null)
                    {
                        var generatore = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisitaInfo == iDIspezioneVisitaInfo && c.IDIspezioneVisita == iDIspezioneVisita && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                        generatore.NoteIspezioneVisita = noteGeneratore.ToString();
                    }
                }

                ctx.SaveChanges();
            }
        }

        public static void CreaDeleteVisitaIspetiva(string operationType, int iDProgrammaIspezioneAttivo, long? iDVisitaIspettiva)
        {
            using (var ctx = new CriterDataModel())
            {
                var visitaIspettiva = new VER_IspezioneVisita();

                if (operationType == "Crea")
                {
                    var CountVisiteIspettive = ctx.VER_IspezioneVisita.Where(c => c.IDProgrammaIspezione == iDProgrammaIspezioneAttivo).ToList();
                    string NumeroVisita = "";

                    if (CountVisiteIspettive == null || CountVisiteIspettive.Count() == 0)
                    {
                        NumeroVisita = "1";
                    }
                    else
                    {
                        NumeroVisita = (CountVisiteIspettive.Count() + 1).ToString();
                    }

                    visitaIspettiva.IDProgrammaIspezione = iDProgrammaIspezioneAttivo;
                    visitaIspettiva.DescrizioneVisita = "Visita" + " - " + NumeroVisita;

                    ctx.VER_IspezioneVisita.Add(visitaIspettiva);
                }
                else if (operationType == "Delete")
                {
                    var librettiDaCancellare = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDVisitaIspettiva).ToList();

                    foreach (var lim in librettiDaCancellare)
                    {
                        var programmaIspezioneInfo = new VER_ProgrammaIspezioneInfo();
                        programmaIspezioneInfo = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDLibrettoImpianto == lim.IDLibrettoImpianto && c.fInVisitaIspettiva == true).FirstOrDefault();
                        programmaIspezioneInfo.fInVisitaIspettiva = false;
                    }

                    var visita = ctx.VER_IspezioneVisita.Where(c => c.IDProgrammaIspezione == iDProgrammaIspezioneAttivo && c.IDIspezioneVisita == iDVisitaIspettiva).FirstOrDefault();
                    ctx.VER_IspezioneVisita.Remove(visita);
                }

                ctx.SaveChanges();
            }
        }

        public static List<long> GetListVisisteByCodiceTargatura(string codiceTargatura)
        {
            using (var ctx = new CriterDataModel())
            {

                var listOfiDIspezioneVisita = (from VER_IspezioneVisitaInfo in ctx.VER_IspezioneVisitaInfo
                                               join LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti on VER_IspezioneVisitaInfo.IDLibrettoImpianto equals LIM_LibrettiImpianti.IDLibrettoImpianto
                                               join LIM_TargatureImpianti in ctx.LIM_TargatureImpianti on new { IDTargaturaImpianto = (int)LIM_LibrettiImpianti.IDTargaturaImpianto } equals new { IDTargaturaImpianto = LIM_TargatureImpianti.IDTargaturaImpianto }
                                               where
                                                 LIM_TargatureImpianti.CodiceTargatura == codiceTargatura
                                               select new
                                               {
                                                   iDIspezioneVisita = VER_IspezioneVisitaInfo.IDIspezioneVisita
                                               }).Distinct().ToList();

                List<long> list = new List<long>();
                foreach (var iDVisita in listOfiDIspezioneVisita)
                {
                    list.Add(iDVisita.iDIspezioneVisita);
                }

                return list;

                //return ctx.V_VER_IspezioniVisite.Where(
                //    c => c.CodiceTargatura == codiceTargatura
                //    )
                //    .Select(c => c.IDIspezioneVisita).Distinct()
                //    .ToList();
            }
        }

        #region Regole Controlli Old
        //public static object[] GetGeneratoriProgrammaIspezioneRegoleControlli(bool fR1, int? percentageR1,
        //                                                                  bool fR2, int? percentageR2,
        //                                                                  bool fR3, int? percentageR3,
        //                                                                  bool fR4, int? percentageR4,
        //                                                                  bool fR5, int? percentageR5)
        //{
        //    object[] outVal = new object[7];
        //    outVal[0] = 0;
        //    outVal[1] = 0;
        //    outVal[2] = 0;
        //    outVal[3] = 0;
        //    outVal[4] = 0;
        //    outVal[5] = 0;
        //    outVal[6] = 0;

        //    using (var ctx = new CriterDataModel())
        //    {
        //        var listaGeneratori = ctx.V_VER_ProgrammaIspezioneRegoleControlli.FromCache(DateTime.Now.AddMinutes(5)).ToList();

        //        int iDProgrammaIspezione = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

        //        var itemsIDLibrettoImpiantoGruppoTermico = ctx.VER_ProgrammaIspezioneInfo.Where(a => a.IDProgrammaIspezione == iDProgrammaIspezione).Select(a => a.IDLibrettoImpiantoGruppoTermico).Distinct().ToArray();
        //        var listaGeneratoriDepurata = listaGeneratori.Where(x => !itemsIDLibrettoImpiantoGruppoTermico.Contains(x.IDLibrettoImpiantoGruppoTermico)).ToList();

        //        outVal[0] = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r1")).Count();
        //        outVal[1] = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r2")).Count();
        //        outVal[2] = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r3")).Count();
        //        outVal[3] = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r4")).Count();
        //        outVal[4] = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r5")).Count();

        //        List<V_VER_ProgrammaIspezioneRegoleControlli> listaGeneratoriR1 = null;
        //        List<V_VER_ProgrammaIspezioneRegoleControlli> listaGeneratoriR2 = null;
        //        List<V_VER_ProgrammaIspezioneRegoleControlli> listaGeneratoriR3 = null;
        //        List<V_VER_ProgrammaIspezioneRegoleControlli> listaGeneratoriR4 = null;
        //        List<V_VER_ProgrammaIspezioneRegoleControlli> listaGeneratoriR5 = null;

        //        if (fR1 && percentageR1 != null)
        //        {
        //            int percentageR1Cal = int.Parse(percentageR1.ToString());
        //            listaGeneratoriR1 = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r1")).ToList();
        //            listaGeneratoriR1 = listaGeneratoriR1.Take(listaGeneratoriR1.Count * percentageR1Cal / 100).ToList();
        //        }
        //        if (fR2 && percentageR2 != null)
        //        {
        //            int percentageR2Cal = int.Parse(percentageR2.ToString());
        //            listaGeneratoriR2 = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r2")).ToList();
        //            listaGeneratoriR2 = listaGeneratoriR2.Take(listaGeneratoriR2.Count * percentageR2Cal / 100).ToList();
        //        }
        //        if (fR3 && percentageR3 != null)
        //        {
        //            int percentageR3Cal = int.Parse(percentageR3.ToString());
        //            listaGeneratoriR3 = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r3")).ToList();
        //            listaGeneratoriR3 = listaGeneratoriR3.Take(listaGeneratoriR3.Count * percentageR3Cal / 100).ToList();
        //        }
        //        if (fR4 && percentageR4 != null)
        //        {
        //            int percentageR4Cal = int.Parse(percentageR4.ToString());
        //            listaGeneratoriR4 = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r4")).ToList();
        //            listaGeneratoriR4 = listaGeneratoriR4.Take(listaGeneratoriR4.Count * percentageR4Cal / 100).ToList();
        //        }
        //        if (fR5 && percentageR5 != null)
        //        {
        //            int percentageR5Cal = int.Parse(percentageR5.ToString());
        //            listaGeneratoriR5 = listaGeneratoriDepurata.Where(a => a.RegolaControllo.StartsWith("r5")).ToList();
        //            listaGeneratoriR5 = listaGeneratoriR5.Take(listaGeneratoriR5.Count * percentageR5Cal / 100).ToList();
        //        }

        //        List<V_VER_ProgrammaIspezioneRegoleControlli> generatori = new List<V_VER_ProgrammaIspezioneRegoleControlli>();
        //        if (listaGeneratoriR1 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR1);
        //        }
        //        if (listaGeneratoriR2 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR2);
        //        }
        //        if (listaGeneratoriR3 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR3);
        //        }
        //        if (listaGeneratoriR4 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR4);
        //        }
        //        if (listaGeneratoriR5 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR5);
        //        }

        //        if (generatori != null)
        //        {
        //            outVal[5] = generatori.ToList().Count();
        //            outVal[6] = generatori.ToList().Sum(a => a.ImportoIspezione);

        //            UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
        //            string keyCache = "RegoleControlliProgrammaIspezione_" + userInfo.IDUtente;

        //            var listCache = new MyListCache();
        //            listCache.ClearList(keyCache);
        //            listCache.MyList(keyCache).Add(generatori);
        //        }
        //    }

        //    return outVal;
        //}

        //public static void InsertGeneratoriProgrammaIspezioneRegoleControlli(int iDProgrammaIspezione)
        //{
        //    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
        //    string keyCache = "RegoleControlliProgrammaIspezione_" + userInfo.IDUtente;

        //    var listCache = new MyListCache();
        //    foreach (var objectCache in listCache.MyList(keyCache))
        //    {
        //        var generatoriList = (List<V_VER_ProgrammaIspezioneRegoleControlli>)objectCache;
        //        //using (var ctx = new CriterDataModel())
        //        //{
        //        //    //Bulk Insert
        //        //    //ctx.BulkInsert(generatoriList, options => options.BatchSize = 100);

        //        //}
        //        foreach (var generatore in generatoriList)
        //        {
        //            InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Insert", iDProgrammaIspezione, generatore.IDLibrettoImpianto, (int)generatore.IDTargaturaImpianto, null, generatore.IDLibrettoImpiantoGruppoTermico);
        //        }

        //    }
        //}
        #endregion

        #region Regole Controlli

        public class GeneratoriRegoleControlliDTo
        {
            public string RegolaControllo { get; set; }
            public int IDLibrettoImpianto { get; set; }
            public int IDLibrettoImpiantoGruppoTermico { get; set; }
            public int? IDTargaturaImpianto { get; set; }
            public decimal ImportoIspezione { get; set; }
            public decimal? PotenzaTermicaUtileNominaleKw { get; set; }
            public int? IDTipologiaCombustibile { get; set; }
            public int IDTipologiaGruppiTermici { get; set; }
            public DateTime DataInserimento { get; set; }

            public int IDProgrammaIspezione { get; set; }
            //public int? IDAccertamento { get; set; }
            //public int IDTipoIspezione { get; set; }
            //public bool fInVisitaIspettiva { get; set; }

        }

        public static object[] GetGeneratoriProgrammaIspezioneRegoleControlli(
           bool fR1, int? percentageR1,
           bool fR2, int? percentageR2,
           bool fR3, int? percentageR3,
           bool fR4, int? percentageR4,
           bool fR5, int? percentageR5)
        {
            object[] outVal = new object[7];
            outVal[0] = 0;
            outVal[1] = 0;
            outVal[2] = 0;
            outVal[3] = 0;
            outVal[4] = 0;
            outVal[5] = 0;
            outVal[6] = 0;

            int iDProgrammaIspezione = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

            using (var ctx = new CriterDataModel())
            {
                var listGeneratoriCache = new MyListCache();

                UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                string keyCacheGeneratori = "GeneratoriCache_" + userInfo.IDUtente;

                bool fInCache = listGeneratoriCache.MyList(keyCacheGeneratori).Count > 0;

                if (!fInCache)
                {
                    var listaGeneratoriFromCache = ctx.VER_RegoleControlli.AsNoTracking().ToList(); //TODO: .OrderBy(a => Guid.NewGuid())
                    listGeneratoriCache.MyList(keyCacheGeneratori).Add(listaGeneratoriFromCache);
                }


                foreach (var objectCache in listGeneratoriCache.MyList(keyCacheGeneratori))
                {
                    var generatoriCacheList = (List<VER_RegoleControlli>)objectCache;

                    #region MyRegion
                    var listaGeneratori = generatoriCacheList.Select(s => new GeneratoriRegoleControlliDTo()
                    {
                        RegolaControllo = s.RegolaControllo,
                        IDLibrettoImpianto = s.IDLibrettoImpianto,
                        IDLibrettoImpiantoGruppoTermico = s.IDLibrettoImpiantoGruppoTermico,
                        IDTargaturaImpianto = s.IDTargaturaImpianto,
                        ImportoIspezione = s.ImportoIspezione,
                        IDProgrammaIspezione = iDProgrammaIspezione,
                        PotenzaTermicaUtileNominaleKw = s.PotenzaTermicaUtileNominaleKw,
                        IDTipologiaCombustibile = s.IDTipologiaCombustibile,
                        IDTipologiaGruppiTermici = s.IDTipologiaGruppiTermici,
                        DataInserimento = DateTime.Now
                    });

                    outVal[0] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r1")).Count();
                    outVal[1] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r2")).Count();
                    outVal[2] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r3")).Count();
                    outVal[3] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r4")).Count();
                    outVal[4] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r5")).Count();

                    List<GeneratoriRegoleControlliDTo> listaGeneratoriR1 = null;
                    List<GeneratoriRegoleControlliDTo> listaGeneratoriR2 = null;
                    List<GeneratoriRegoleControlliDTo> listaGeneratoriR3 = null;
                    List<GeneratoriRegoleControlliDTo> listaGeneratoriR4 = null;
                    List<GeneratoriRegoleControlliDTo> listaGeneratoriR5 = null;

                    if (fR1 && percentageR1 != null)
                    {
                        int percentageR1Cal = int.Parse(percentageR1.ToString());
                        listaGeneratoriR1 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r1")).ToList();
                        listaGeneratoriR1 = listaGeneratoriR1.Take(listaGeneratoriR1.Count * percentageR1Cal / 100).ToList();
                    }
                    if (fR2 && percentageR2 != null)
                    {
                        int percentageR2Cal = int.Parse(percentageR2.ToString());
                        listaGeneratoriR2 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r2")).ToList();
                        listaGeneratoriR2 = listaGeneratoriR2.Take(listaGeneratoriR2.Count * percentageR2Cal / 100).ToList();
                    }
                    if (fR3 && percentageR3 != null)
                    {
                        int percentageR3Cal = int.Parse(percentageR3.ToString());
                        listaGeneratoriR3 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r3")).ToList();
                        listaGeneratoriR3 = listaGeneratoriR3.Take(listaGeneratoriR3.Count * percentageR3Cal / 100).ToList();
                    }
                    if (fR4 && percentageR4 != null)
                    {
                        int percentageR4Cal = int.Parse(percentageR4.ToString());
                        listaGeneratoriR4 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r4")).ToList();
                        listaGeneratoriR4 = listaGeneratoriR4.Take(listaGeneratoriR4.Count * percentageR4Cal / 100).ToList();
                    }
                    if (fR5 && percentageR5 != null)
                    {
                        int percentageR5Cal = int.Parse(percentageR5.ToString());
                        listaGeneratoriR5 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r5")).ToList();
                        listaGeneratoriR5 = listaGeneratoriR5.Take(listaGeneratoriR5.Count * percentageR5Cal / 100).ToList();
                    }

                    List<GeneratoriRegoleControlliDTo> generatori = new List<GeneratoriRegoleControlliDTo>();
                    if (listaGeneratoriR1 != null)
                    {
                        generatori.AddRange(listaGeneratoriR1);
                    }
                    if (listaGeneratoriR2 != null)
                    {
                        generatori.AddRange(listaGeneratoriR2);
                    }
                    if (listaGeneratoriR3 != null)
                    {
                        generatori.AddRange(listaGeneratoriR3);
                    }
                    if (listaGeneratoriR4 != null)
                    {
                        generatori.AddRange(listaGeneratoriR4);
                    }
                    if (listaGeneratoriR5 != null)
                    {
                        generatori.AddRange(listaGeneratoriR5);
                    }

                    if (generatori != null && generatori.Count() > 0)
                    {
                        //List<decimal?> importoAbilitato = new List<decimal?>() { 100m, 130m, 160m, 200m };
                        //var checkAnomaliaImporto = generatori.Where(c => !importoAbilitato.Contains(c.ImportoIspezione)).ToList();

                        outVal[5] = generatori.ToList().Count();
                        outVal[6] = generatori.ToList().Sum(a => a.ImportoIspezione);

                        //UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                        string keyCache = "RegoleControlliProgrammaIspezione_" + userInfo.IDUtente;

                        var listCache = new MyListCache();
                        listCache.ClearList(keyCache);
                        listCache.MyList(keyCache).Add(generatori);
                    }
                    #endregion

                }

            }

            return outVal;
        }


        //public static object[] GetGeneratoriProgrammaIspezioneRegoleControlliOld(
        //    bool fR1, int? percentageR1, decimal PotenzaTermicaUtileNominaleKwR1, List<int?> CombustibileR1, List<int> GruppiTermiciR1,
        //    bool fR2, int? percentageR2, decimal PotenzaTermicaUtileNominaleKwR2, List<int?> CombustibileR2, List<int> GruppiTermiciR2,
        //    bool fR3, int? percentageR3, decimal PotenzaTermicaUtileNominaleKwR3, List<int?> CombustibileR3, List<int> GruppiTermiciR3,
        //    bool fR4, int? percentageR4, decimal Da_PotenzaTermicaUtileNominaleKwR4, decimal A_PotenzaTermicaUtileNominaleKwR4, List<int?> CombustibileR4, List<int> GruppiTermiciR4,
        //    bool fR5, int? percentageR5, decimal PotenzaTermicaUtileNominaleKwR5, List<int?> CombustibileR5, List<int> GruppiTermiciR5)
        //{
        //    object[] outVal = new object[7];
        //    outVal[0] = 0;
        //    outVal[1] = 0;
        //    outVal[2] = 0;
        //    outVal[3] = 0;
        //    outVal[4] = 0;
        //    outVal[5] = 0;
        //    outVal[6] = 0;

        //    using (var ctx = new CriterDataModel())
        //    {
        //        #region UNION QUERY REGOLI CONTROLLI

        //        var unionQuery =
        //            (from r1 in ctx.LIM_LibrettiImpianti
        //             join gr1 in ctx.LIM_LibrettiImpiantiGruppiTermici on r1.IDLibrettoImpianto equals gr1.IDLibrettoImpianto
        //             where gr1.fAttivo == true && gr1.fDismesso == false && r1.fAttivo == true && r1.IDStatoLibrettoImpianto == 2
        //             && gr1.DataInstallazione.HasValue && gr1.DataInstallazione.Value.Year < System.Data.Entity.DbFunctions.AddYears(DateTime.Now, -15).Value.Year
        //             && gr1.IDTipologiaCombustibile.HasValue && gr1.PotenzaTermicaUtileNominaleKw.HasValue
        //             && !ctx.VER_ProgrammaIspezioneInfo.Any(o => o.IDLibrettoImpiantoGruppoTermico == gr1.IDLibrettoImpiantoGruppoTermico)

        //             //&& gr1.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR1 && CombustibileR1.Contains(gr1.IDTipologiaCombustibile)
        //             //&& GruppiTermiciR1.Contains(gr1.IDTipologiaGruppiTermici)
        //             select new
        //             {
        //                 RegolaControllo = "r1",
        //                 IDLibrettoImpianto = r1.IDLibrettoImpianto,
        //                 IDLibrettoImpiantoGruppoTermico = gr1.IDLibrettoImpiantoGruppoTermico,
        //                 IDTargaturaImpianto = r1.IDTargaturaImpianto,
        //                 ImportoIspezione =
        //                 gr1.PotenzaTermicaUtileNominaleKw == null ? 0m :
        //                 gr1.PotenzaTermicaUtileNominaleKw <= 35.00m ? 100m :
        //                 gr1.PotenzaTermicaUtileNominaleKw >= 35.1m && gr1.PotenzaTermicaUtileNominaleKw <= 100.00m ? 130m :
        //                 gr1.PotenzaTermicaUtileNominaleKw >= 100.1m && gr1.PotenzaTermicaUtileNominaleKw <= 350.00m ? 160m :
        //                 gr1.PotenzaTermicaUtileNominaleKw > 350.1m ? 200m : 0m,
        //                 PotenzaTermicaUtileNominaleKw = gr1.PotenzaTermicaUtileNominaleKw,
        //                 IDTipologiaCombustibile = gr1.IDTipologiaCombustibile,
        //                 IDTipologiaGruppiTermici = gr1.IDTipologiaGruppiTermici
        //             })
        //        // Dati Default R1
        //        // PotenzaTermicaUtileNominaleKw >= 10
        //        // IDTipologiaCombustibile  IN (2, 3, 4, 5, 12, 15) -- Gas naturale, Gpl extra rete, Gpl, Gasolio, Kerosene, Olio Combustibile
        //        // IDTipologiaGruppiTermici IN (1, 2) -- Gruppi termici singoli, Gruppi termici modulari
        //        .Union(from r2 in ctx.LIM_LibrettiImpianti
        //               join gr2 in ctx.LIM_LibrettiImpiantiGruppiTermici on r2.IDLibrettoImpianto equals gr2.IDLibrettoImpianto
        //               where gr2.fAttivo == true && gr2.fDismesso == false && r2.fAttivo == true && r2.IDStatoLibrettoImpianto == 2
        //               && gr2.IDTipologiaCombustibile.HasValue && gr2.PotenzaTermicaUtileNominaleKw.HasValue
        //               && !ctx.VER_ProgrammaIspezioneInfo.Any(o => o.IDLibrettoImpiantoGruppoTermico == gr2.IDLibrettoImpiantoGruppoTermico)
        //               //&& gr2.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR2 && CombustibileR2.Contains(gr2.IDTipologiaCombustibile)
        //               //&& GruppiTermiciR2.Contains(gr2.IDTipologiaGruppiTermici)
        //               select new
        //               {
        //                   RegolaControllo = "r2",
        //                   IDLibrettoImpianto = r2.IDLibrettoImpianto,
        //                   IDLibrettoImpiantoGruppoTermico = gr2.IDLibrettoImpiantoGruppoTermico,
        //                   IDTargaturaImpianto = r2.IDTargaturaImpianto,
        //                   ImportoIspezione =
        //                   gr2.PotenzaTermicaUtileNominaleKw == null ? 0m :
        //                   gr2.PotenzaTermicaUtileNominaleKw <= 35.00m ? 100m :
        //                   gr2.PotenzaTermicaUtileNominaleKw >= 35.1m && gr2.PotenzaTermicaUtileNominaleKw <= 100.00m ? 130m :
        //                   gr2.PotenzaTermicaUtileNominaleKw >= 100.1m && gr2.PotenzaTermicaUtileNominaleKw <= 350.00m ? 160m :
        //                   gr2.PotenzaTermicaUtileNominaleKw > 350.1m ? 200m : 0m,
        //                   PotenzaTermicaUtileNominaleKw = gr2.PotenzaTermicaUtileNominaleKw,
        //                   IDTipologiaCombustibile = gr2.IDTipologiaCombustibile,
        //                   IDTipologiaGruppiTermici = gr2.IDTipologiaGruppiTermici
        //               })
        //           // Dati Default R2
        //           // PotenzaTermicaUtileNominaleKw >= 100
        //           // IDTipologiaCombustibile  IN (2, 3, 15) -- Gas naturale, Gpl extra rete, Gpl
        //           // IDTipologiaGruppiTermici IN (1, 2) -- Gruppi termici singoli, Gruppi termici modulari
        //           .Union(from r3 in ctx.LIM_LibrettiImpianti
        //                  join gr3 in ctx.LIM_LibrettiImpiantiGruppiTermici on r3.IDLibrettoImpianto equals gr3.IDLibrettoImpianto
        //                  where gr3.fAttivo == true && gr3.fDismesso == false && r3.fAttivo == true && r3.IDStatoLibrettoImpianto == 2
        //                  && gr3.IDTipologiaCombustibile.HasValue && gr3.PotenzaTermicaUtileNominaleKw.HasValue
        //                  && !ctx.VER_ProgrammaIspezioneInfo.Any(o => o.IDLibrettoImpiantoGruppoTermico == gr3.IDLibrettoImpiantoGruppoTermico)
        //                  //&& gr3.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR3 && CombustibileR3.Contains(gr3.IDTipologiaCombustibile)
        //                  //&& GruppiTermiciR3.Contains(gr3.IDTipologiaGruppiTermici)
        //                  select new
        //                  {
        //                      RegolaControllo = "r3",
        //                      IDLibrettoImpianto = r3.IDLibrettoImpianto,
        //                      IDLibrettoImpiantoGruppoTermico = gr3.IDLibrettoImpiantoGruppoTermico,
        //                      IDTargaturaImpianto = r3.IDTargaturaImpianto,
        //                      ImportoIspezione =
        //                      gr3.PotenzaTermicaUtileNominaleKw == null ? 0m :
        //                      gr3.PotenzaTermicaUtileNominaleKw <= 35.00m ? 100m :
        //                      gr3.PotenzaTermicaUtileNominaleKw >= 35.1m && gr3.PotenzaTermicaUtileNominaleKw <= 100.00m ? 130m :
        //                      gr3.PotenzaTermicaUtileNominaleKw >= 100.1m && gr3.PotenzaTermicaUtileNominaleKw <= 350.00m ? 160m :
        //                      gr3.PotenzaTermicaUtileNominaleKw > 350.1m ? 200m : 0m,
        //                      PotenzaTermicaUtileNominaleKw = gr3.PotenzaTermicaUtileNominaleKw,
        //                      IDTipologiaCombustibile = gr3.IDTipologiaCombustibile,
        //                      IDTipologiaGruppiTermici = gr3.IDTipologiaGruppiTermici
        //                  }
        //           )
        //           // Dati Default R3
        //           // PotenzaTermicaUtileNominaleKw >= 100
        //           // IDTipologiaCombustibile  IN (4, 5, 12) -- Olio combustibile, Gasolio, Kerosene
        //           // IDTipologiaGruppiTermici IN (1, 2) -- Gruppi termici singoli, Gruppi termici modulari
        //           .Union(from r4 in ctx.LIM_LibrettiImpianti
        //                  join gr4 in ctx.LIM_LibrettiImpiantiGruppiTermici on r4.IDLibrettoImpianto equals gr4.IDLibrettoImpianto
        //                  where gr4.fAttivo == true && gr4.fDismesso == false && r4.fAttivo == true && r4.IDStatoLibrettoImpianto == 2
        //                  && gr4.IDTipologiaCombustibile.HasValue && gr4.PotenzaTermicaUtileNominaleKw.HasValue
        //                  && !ctx.VER_ProgrammaIspezioneInfo.Any(o => o.IDLibrettoImpiantoGruppoTermico == gr4.IDLibrettoImpiantoGruppoTermico)
        //                  //&& gr4.PotenzaTermicaUtileNominaleKw >= Da_PotenzaTermicaUtileNominaleKwR4
        //                  //&& gr4.PotenzaTermicaUtileNominaleKw <= A_PotenzaTermicaUtileNominaleKwR4
        //                  //&& CombustibileR4.Contains(gr4.IDTipologiaCombustibile) && GruppiTermiciR4.Contains(gr4.IDTipologiaGruppiTermici)
        //                  select new
        //                  {
        //                      RegolaControllo = "r4",
        //                      IDLibrettoImpianto = r4.IDLibrettoImpianto,
        //                      IDLibrettoImpiantoGruppoTermico = gr4.IDLibrettoImpiantoGruppoTermico,
        //                      IDTargaturaImpianto = r4.IDTargaturaImpianto,
        //                      ImportoIspezione =
        //                      gr4.PotenzaTermicaUtileNominaleKw == null ? 0m :
        //                      gr4.PotenzaTermicaUtileNominaleKw <= 35.00m ? 100m :
        //                      gr4.PotenzaTermicaUtileNominaleKw >= 35.1m && gr4.PotenzaTermicaUtileNominaleKw <= 100.00m ? 130m :
        //                      gr4.PotenzaTermicaUtileNominaleKw >= 100.1m && gr4.PotenzaTermicaUtileNominaleKw <= 350.00m ? 160m :
        //                      gr4.PotenzaTermicaUtileNominaleKw > 350.1m ? 200m : 0m,
        //                      PotenzaTermicaUtileNominaleKw = gr4.PotenzaTermicaUtileNominaleKw,
        //                      IDTipologiaCombustibile = gr4.IDTipologiaCombustibile,
        //                      IDTipologiaGruppiTermici = gr4.IDTipologiaGruppiTermici
        //                  }
        //           )
        //            // Dati Default R4
        //            // PotenzaTermicaUtileNominaleKw - BETWEEN 20 AND 100
        //            // IDTipologiaCombustibile  IN (4, 5, 12) -- Olio combustibile, Gasolio, Kerosene
        //            // IDTipologiaGruppiTermici IN (1, 2) -- Gruppi termici singoli, Gruppi termici modulari
        //            .Union(from r5 in ctx.LIM_LibrettiImpianti
        //                   join gr5 in ctx.LIM_LibrettiImpiantiGruppiTermici on r5.IDLibrettoImpianto equals gr5.IDLibrettoImpianto
        //                   join rtGT in ctx.RCT_RapportoDiControlloTecnicoGT on gr5.IDLibrettoImpiantoGruppoTermico equals rtGT.IDLIM_LibrettiImpiantiGruppitermici
        //                   join rt in ctx.RCT_RapportoDiControlloTecnicoBase on rtGT.Id equals rt.IDRapportoControlloTecnico
        //                   where rt.IDTargaturaImpianto == r5.IDTargaturaImpianto && rt.IDStatoRapportoDiControllo == 2 && gr5.fAttivo == true && gr5.fDismesso == false && r5.fAttivo == true && r5.IDStatoLibrettoImpianto == 2
        //                   && System.Data.Entity.DbFunctions.AddMonths(rt.DataControllo, 30) < DateTime.Now
        //                   && gr5.IDTipologiaCombustibile.HasValue && gr5.PotenzaTermicaUtileNominaleKw.HasValue
        //                   && !ctx.VER_ProgrammaIspezioneInfo.Any(o => o.IDLibrettoImpiantoGruppoTermico == gr5.IDLibrettoImpiantoGruppoTermico)
        //                   //&& gr5.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR5 && CombustibileR5.Contains(gr5.IDTipologiaCombustibile)
        //                   //&& GruppiTermiciR5.Contains(gr5.IDTipologiaGruppiTermici)
        //                   select new
        //                   {
        //                       RegolaControllo = "r5",
        //                       IDLibrettoImpianto = r5.IDLibrettoImpianto,
        //                       IDLibrettoImpiantoGruppoTermico = gr5.IDLibrettoImpiantoGruppoTermico,
        //                       IDTargaturaImpianto = r5.IDTargaturaImpianto,
        //                       ImportoIspezione =
        //                       gr5.PotenzaTermicaUtileNominaleKw == null ? 0m :
        //                       gr5.PotenzaTermicaUtileNominaleKw <= 35.00m ? 100m :
        //                       gr5.PotenzaTermicaUtileNominaleKw >= 35.1m && gr5.PotenzaTermicaUtileNominaleKw <= 100.00m ? 130m :
        //                       gr5.PotenzaTermicaUtileNominaleKw >= 100.1m && gr5.PotenzaTermicaUtileNominaleKw <= 350.00m ? 160m :
        //                       gr5.PotenzaTermicaUtileNominaleKw > 350.1m ? 200m : 0m,
        //                       PotenzaTermicaUtileNominaleKw = gr5.PotenzaTermicaUtileNominaleKw,
        //                       IDTipologiaCombustibile = gr5.IDTipologiaCombustibile,
        //                       IDTipologiaGruppiTermici = gr5.IDTipologiaGruppiTermici
        //                   })
        //            // Dati Default R5
        //            // PotenzaTermicaUtileNominaleKw >= 10
        //            // IDTipologiaCombustibile  IN (2, 3, 4, 5, 12, 15) -- Gas naturale, Gpl extra rete, Gpl, Gasolio, Kerosene, Olio Combustibile
        //            // IDTipologiaGruppiTermici IN (1, 2) -- Gruppi termici singoli, Gruppi termici modulari
        //            .Distinct();
        //        //.Select(c => new GeneratoriRegoleControlliDTo()
        //        //{
        //        //    RegolaControllo = c.RegolaControllo,
        //        //    IDLibrettoImpianto = c.IDLibrettoImpianto,
        //        //    IDLibrettoImpiantoGruppoTermico = c.IDLibrettoImpiantoGruppoTermico,
        //        //    IDTargaturaImpianto = c.IDTargaturaImpianto,
        //        //    ImportoIspezione = c.ImportoIspezione,
        //        //    PotenzaTermicaUtileNominaleKw = c.PotenzaTermicaUtileNominaleKw,
        //        //    IDTipologiaCombustibile = c.IDTipologiaCombustibile,
        //        //    IDTipologiaGruppiTermici = c.IDTipologiaGruppiTermici
        //        //});

        //        #endregion

        //        var listaGeneratoriFromCache = unionQuery.FromCache(DateTime.Now.AddMinutes(10)).ToList();

        //        // devo filtrare e grouppare la lista from cache
        //        var listaGeneratori = listaGeneratoriFromCache.Where(r =>
        //                                                          (r.RegolaControllo == "r1"
        //                                                          && r.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR1
        //                                                          && CombustibileR1.Contains(r.IDTipologiaCombustibile)
        //                                                          && GruppiTermiciR1.Contains(r.IDTipologiaGruppiTermici)) ||
        //                                                          (r.RegolaControllo == "r2"
        //                                                          && r.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR2
        //                                                          && CombustibileR2.Contains(r.IDTipologiaCombustibile)
        //                                                          && GruppiTermiciR2.Contains(r.IDTipologiaGruppiTermici)) ||
        //                                                          (r.RegolaControllo == "r3"
        //                                                          && r.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR3
        //                                                          && CombustibileR3.Contains(r.IDTipologiaCombustibile)
        //                                                          && GruppiTermiciR3.Contains(r.IDTipologiaGruppiTermici)) ||
        //                                                          (r.RegolaControllo == "r4"
        //                                                          && r.PotenzaTermicaUtileNominaleKw >= Da_PotenzaTermicaUtileNominaleKwR4
        //                                                          && r.PotenzaTermicaUtileNominaleKw <= A_PotenzaTermicaUtileNominaleKwR4
        //                                                          && CombustibileR4.Contains(r.IDTipologiaCombustibile)
        //                                                          && GruppiTermiciR4.Contains(r.IDTipologiaGruppiTermici)) ||
        //                                                          (r.RegolaControllo == "r5"
        //                                                          && r.PotenzaTermicaUtileNominaleKw >= PotenzaTermicaUtileNominaleKwR5
        //                                                          && CombustibileR5.Contains(r.IDTipologiaCombustibile)
        //                                                          && GruppiTermiciR5.Contains(r.IDTipologiaGruppiTermici))
        //                                                          ).GroupBy(g => g.IDLibrettoImpiantoGruppoTermico).ToList().Select(s => new GeneratoriRegoleControlliDTo()
        //                                                          {
        //                                                              RegolaControllo = string.Join(",", s.OrderBy(i => i.RegolaControllo).Select(i => i.RegolaControllo)),
        //                                                              IDLibrettoImpianto = s.First().IDLibrettoImpianto,
        //                                                              IDLibrettoImpiantoGruppoTermico = s.Key,
        //                                                              IDTargaturaImpianto = s.First().IDTargaturaImpianto,
        //                                                              ImportoIspezione = s.First().ImportoIspezione
        //                                                          });


        //        //int iDProgrammaIspezione = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

        //        //var itemsIDLibrettoImpiantoGruppoTermico = ctx.VER_ProgrammaIspezioneInfo.Where(a => a.IDProgrammaIspezione == iDProgrammaIspezione).Select(a => a.IDLibrettoImpiantoGruppoTermico).Distinct().ToArray();
        //        //var listaGeneratoriDepurata = listaGeneratori.Where(x => !itemsIDLibrettoImpiantoGruppoTermico.Contains(x.IDLibrettoImpiantoGruppoTermico)).ToList();

        //        outVal[0] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r1")).Count();
        //        outVal[1] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r2")).Count();
        //        outVal[2] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r3")).Count();
        //        outVal[3] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r4")).Count();
        //        outVal[4] = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r5")).Count();

        //        List<GeneratoriRegoleControlliDTo> listaGeneratoriR1 = null;
        //        List<GeneratoriRegoleControlliDTo> listaGeneratoriR2 = null;
        //        List<GeneratoriRegoleControlliDTo> listaGeneratoriR3 = null;
        //        List<GeneratoriRegoleControlliDTo> listaGeneratoriR4 = null;
        //        List<GeneratoriRegoleControlliDTo> listaGeneratoriR5 = null;

        //        if (fR1 && percentageR1 != null)
        //        {
        //            int percentageR1Cal = int.Parse(percentageR1.ToString());
        //            listaGeneratoriR1 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r1")).ToList();
        //            listaGeneratoriR1 = listaGeneratoriR1.Take(listaGeneratoriR1.Count * percentageR1Cal / 100).ToList();
        //        }
        //        if (fR2 && percentageR2 != null)
        //        {
        //            int percentageR2Cal = int.Parse(percentageR2.ToString());
        //            listaGeneratoriR2 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r2")).ToList();
        //            listaGeneratoriR2 = listaGeneratoriR2.Take(listaGeneratoriR2.Count * percentageR2Cal / 100).ToList();
        //        }
        //        if (fR3 && percentageR3 != null)
        //        {
        //            int percentageR3Cal = int.Parse(percentageR3.ToString());
        //            listaGeneratoriR3 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r3")).ToList();
        //            listaGeneratoriR3 = listaGeneratoriR3.Take(listaGeneratoriR3.Count * percentageR3Cal / 100).ToList();
        //        }
        //        if (fR4 && percentageR4 != null)
        //        {
        //            int percentageR4Cal = int.Parse(percentageR4.ToString());
        //            listaGeneratoriR4 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r4")).ToList();
        //            listaGeneratoriR4 = listaGeneratoriR4.Take(listaGeneratoriR4.Count * percentageR4Cal / 100).ToList();
        //        }
        //        if (fR5 && percentageR5 != null)
        //        {
        //            int percentageR5Cal = int.Parse(percentageR5.ToString());
        //            listaGeneratoriR5 = listaGeneratori.Where(a => a.RegolaControllo.StartsWith("r5")).ToList();
        //            listaGeneratoriR5 = listaGeneratoriR5.Take(listaGeneratoriR5.Count * percentageR5Cal / 100).ToList();
        //        }

        //        List<GeneratoriRegoleControlliDTo> generatori = new List<GeneratoriRegoleControlliDTo>();
        //        if (listaGeneratoriR1 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR1);
        //        }
        //        if (listaGeneratoriR2 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR2);
        //        }
        //        if (listaGeneratoriR3 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR3);
        //        }
        //        if (listaGeneratoriR4 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR4);
        //        }
        //        if (listaGeneratoriR5 != null)
        //        {
        //            generatori.AddRange(listaGeneratoriR5);
        //        }

        //        if (generatori != null && generatori.Count() > 0)
        //        {
        //            //List<decimal?> importoAbilitato = new List<decimal?>() { 100m, 130m, 160m, 200m };
        //            //var checkAnomaliaImporto = generatori.Where(c => !importoAbilitato.Contains(c.ImportoIspezione)).ToList();

        //            outVal[5] = generatori.ToList().Count();
        //            outVal[6] = generatori.ToList().Sum(a => a.ImportoIspezione);

        //            UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
        //            string keyCache = "RegoleControlliProgrammaIspezione_" + userInfo.IDUtente;

        //            var listCache = new MyListCache();
        //            listCache.ClearList(keyCache);
        //            listCache.MyList(keyCache).Add(generatori);
        //        }
        //    }

        //    return outVal;
        //}

        public static void InsertGeneratoriProgrammaIspezioneRegoleControlli(int iDProgrammaIspezione)
        {
            UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            string keyCache = "RegoleControlliProgrammaIspezione_" + userInfo.IDUtente;

            var listCache = new MyListCache();
            foreach (var objectCache in listCache.MyList(keyCache))
            {
                var generatoriList = (List<GeneratoriRegoleControlliDTo>)objectCache;

                //TODO: VECCHIA VERSIONE TROPPO LENTA!!!!!!
                //foreach (var generatore in generatoriList)
                //{
                //    InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Insert", iDProgrammaIspezione, generatore.IDLibrettoImpianto, (int)generatore.IDTargaturaImpianto, null, generatore.IDLibrettoImpiantoGruppoTermico);
                //    DeleteDataRegoleControlli(generatore.IDLibrettoImpianto, generatore.IDLibrettoImpiantoGruppoTermico);
                //}

                //NUOVO
                //UtilityApp.BulkInsert(BuildConnection.ConnectionString, "VER_ProgrammaIspezioneInfo", generatoriList);



                using (var ctx = new CriterDataModel())
                {
                    ctx.Database.CommandTimeout = 360;

                    var generatoriSelezionati = generatoriList.Select(s => new VER_ProgrammaIspezioneInfo()
                    {
                        IDProgrammaIspezione = s.IDProgrammaIspezione,
                        IDAccertamento = null,
                        IDLibrettoImpianto = s.IDLibrettoImpianto,
                        IDTipoIspezione = 2,
                        fInVisitaIspettiva = false,
                        IDLibrettoImpiantoGruppoTermico = s.IDLibrettoImpiantoGruppoTermico,
                        IDTargaturaImpianto = s.IDTargaturaImpianto,
                        DataInserimento = DateTime.Now
                    }).ToList();

                    ctx.VER_ProgrammaIspezioneInfo.AddRange(generatoriSelezionati);
                    ctx.SaveChanges();

                    //Cancellazione dei generatori inseriti nelle regole controlli
                    var ListIDLibrettoImpianto = new HashSet<int>(generatoriList.Select(p => p.IDLibrettoImpianto));
                    var ListIDLibrettoImpiantoGruppoTermico = new HashSet<int>(generatoriList.Select(p => p.IDLibrettoImpiantoGruppoTermico));

                    var generatoriToRemove = ctx.VER_RegoleControlli.Where(c => ListIDLibrettoImpiantoGruppoTermico.Contains(c.IDLibrettoImpiantoGruppoTermico)).ToList();
                    ctx.VER_RegoleControlli.RemoveRange(generatoriToRemove);
                    ctx.SaveChanges();

                    //Importo solo generatori per tipologia di gruppi termici modulare o singolo
                    var ListiDTipologiaGruppiTermici = new List<int>() { 1, 2 };

                    //Logica per inserire in automatico a fronte di un libretto tutti i generatori facenti parte dei quel libretto
                    var generatoriAltriLibretto = (from LIM_LibrettiImpiantiGruppiTermici in ctx.LIM_LibrettiImpiantiGruppiTermici
                                                   join LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti on LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto equals LIM_LibrettiImpianti.IDLibrettoImpianto
                                                   where
                                                     LIM_LibrettiImpianti.fAttivo == true &&
                                                     LIM_LibrettiImpiantiGruppiTermici.fAttivo == true &&
                                                     LIM_LibrettiImpiantiGruppiTermici.fDismesso == false &&
                                                     LIM_LibrettiImpianti.IDStatoLibrettoImpianto == 2 &&
                                                     ListiDTipologiaGruppiTermici.Contains(LIM_LibrettiImpiantiGruppiTermici.IDTipologiaGruppiTermici) &&
                                                     ListIDLibrettoImpianto.Contains(LIM_LibrettiImpianti.IDLibrettoImpianto) &&
                                                     !(ListIDLibrettoImpiantoGruppoTermico.Contains(LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico))
                                                   select new
                                                   {
                                                       LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico,
                                                       LIM_LibrettiImpianti.IDTargaturaImpianto,
                                                       LIM_LibrettiImpianti.IDLibrettoImpianto
                                                   }).ToList();

                    var generatoriLibretto = generatoriAltriLibretto.Select(s => new VER_ProgrammaIspezioneInfo()
                    {
                        IDProgrammaIspezione = iDProgrammaIspezione,
                        IDAccertamento = null,
                        IDLibrettoImpianto = s.IDLibrettoImpianto,
                        IDTipoIspezione = 2,
                        fInVisitaIspettiva = false,
                        IDLibrettoImpiantoGruppoTermico = s.IDLibrettoImpiantoGruppoTermico,
                        IDTargaturaImpianto = s.IDTargaturaImpianto,
                        DataInserimento = DateTime.Now
                    });

                    ctx.VER_ProgrammaIspezioneInfo.AddRange(generatoriLibretto);
                    ctx.SaveChanges();

                    //Cancello la cache
                    string keyCacheGeneratori = "GeneratoriCache_" + userInfo.IDUtente;
                    listCache.ClearList(keyCacheGeneratori);
                }
            }
        }

        public static void SetRegoleControlli(decimal PotenzaTermicaUtileNominaleKwR1, List<int?> CombustibileR1, List<int> GruppiTermiciR1,
                                              decimal PotenzaTermicaUtileNominaleKwR2, List<int?> CombustibileR2, List<int> GruppiTermiciR2,
                                              decimal PotenzaTermicaUtileNominaleKwR3, List<int?> CombustibileR3, List<int> GruppiTermiciR3,
                                              decimal Da_PotenzaTermicaUtileNominaleKwR4, decimal A_PotenzaTermicaUtileNominaleKwR4, List<int?> CombustibileR4, List<int> GruppiTermiciR4,
                                              decimal PotenzaTermicaUtileNominaleKwR5, List<int?> CombustibileR5, List<int> GruppiTermiciR5)
        {
            string CombustibileR1String = string.Join(",", CombustibileR1);
            string GruppiTermiciR1String = string.Join(",", GruppiTermiciR1);
            string CombustibileR2String = string.Join(",", CombustibileR2);
            string GruppiTermiciR2String = string.Join(",", GruppiTermiciR2);
            string CombustibileR3String = string.Join(",", CombustibileR3);
            string GruppiTermiciR3String = string.Join(",", GruppiTermiciR3);
            string CombustibileR4String = string.Join(",", CombustibileR4);
            string GruppiTermiciR4String = string.Join(",", GruppiTermiciR4);
            string CombustibileR5String = string.Join(",", CombustibileR5);
            string GruppiTermiciR5String = string.Join(",", GruppiTermiciR5);

            using (var ctx = new CriterDataModel())
            {
                ctx.Database.CommandTimeout = 360;

                var sp = ctx.sp_SetRegoleControlli((int)PotenzaTermicaUtileNominaleKwR1, CombustibileR1String, GruppiTermiciR1String,
                                                   (int)PotenzaTermicaUtileNominaleKwR2, CombustibileR2String, GruppiTermiciR2String,
                                                   (int)PotenzaTermicaUtileNominaleKwR3, CombustibileR3String, GruppiTermiciR3String,
                                                   (int)Da_PotenzaTermicaUtileNominaleKwR4, (int)A_PotenzaTermicaUtileNominaleKwR4, CombustibileR4String, GruppiTermiciR4String,
                                                   (int)PotenzaTermicaUtileNominaleKwR5, CombustibileR5String, GruppiTermiciR5String
                                                  );
            }
        }

        public static DateTime? GetDataRegoleControlli()
        {
            DateTime? dataAnalisi = null;

            using (var ctx = new CriterDataModel())
            {
                var regoleControlli = ctx.VER_RegoleControlli.Take(1).FirstOrDefault();
                if (regoleControlli != null)
                {
                    dataAnalisi = regoleControlli.DataInserimento;
                }
            }

            return dataAnalisi;
        }

        public static void DeleteDataRegoleControlli(int iDTaragaturaImpianto, int iDLibrettoImpianto, int iDLibrettoImpiantoGruppoTermico)
        {
            using (var ctx = new CriterDataModel())
            {
                var regolaControllo = ctx.VER_RegoleControlli.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                ctx.VER_RegoleControlli.Remove(regolaControllo);
                ctx.SaveChanges();
            }
        }

        #endregion

        public static bool fVisitaInIspezione(int iDIspezioneVisita)
        {
            bool visitaInIspezione = false;
            using (var ctx = new CriterDataModel())
            {
                var LibrettoNelProgramma = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                foreach (var f in LibrettoNelProgramma)
                {
                    if (f.fInIspezione)
                    {
                        visitaInIspezione = true;
                    }
                }

            }
            return visitaInIspezione;
        }

        public static bool GetExistGruppoVerifica(long iDIspezioneVisita)
        {
            bool fExist = false;
            using (var ctx = new CriterDataModel())
            {
                fExist = ctx.VER_IspezioneGruppoVerifica.Where(a => a.IDIspezioneVisita == iDIspezioneVisita).Any();
            }

            return fExist;
        }


        //public static long? GetIDAccertamento(int iDLibrettoImpianto, int iDProgrammaIspezione)
        //{
        //    try
        //    {
        //        using (var ctx = new CriterDataModel())
        //        {
        //            return
        //                ctx.VER_ProgrammaIspezioneInfo.Where(
        //                    c => c.IDLibrettoImpianto == iDLibrettoImpianto && c.IDProgrammaIspezione == iDProgrammaIspezione && c.fInVisitaIspettiva == false && c.IDAccertamento != null
        //                    )
        //                    .Select(c => c.IDAccertamento)
        //                    .FirstOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public static string GetSqlValoriAccertamentiInAttesaIspezione(object iDProgrammaIspezione)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT ");
        //    strSql.Append(" * ");
        //    strSql.Append(" FROM V_VER_Accertamenti ");
        //    strSql.Append(" WHERE IDStatoAccertamento=5  AND IDStatoAccertamentoIntervento=9");


        //    if (iDProgrammaIspezione != null)
        //    {
        //        strSql.Append(" AND IDLibrettoImpianto NOT IN ");
        //        strSql.Append("( SELECT IDLibrettoImpianto FROM VER_ProgrammaIspezioneInfo WHERE 1=1)");
        //    }

        //    strSql.Append(" ORDER BY DataScadenzaIntervento DESC");

        //    return strSql.ToString();
        //}
        #endregion

        #region Ispezioni
        public static void CreaIspezione(long iDIspezioneVisita, int IDIspettore, int iDUtente)
        {
            //var t = Task.Run(() =>
            //{
                using (var ctx = new CriterDataModel())
            {
                var visiteIspettive = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                string pathVisita = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + iDIspezioneVisita;
                UtilityFileSystem.CreateDirectoryIfNotExists(pathVisita);

                foreach (var visita in visiteIspettive)
                {
                    var ispezione = new VER_Ispezione();

                    int? iDSoggetto = null;
                    long? iDRapportoDiControllo = null;

                    if (visita.IDAccertamento != null)
                    {
                        iDSoggetto = ctx.VER_Accertamento.Find(visita.IDAccertamento).IDSoggetto;
                        iDRapportoDiControllo = ctx.VER_Accertamento.Find(visita.IDAccertamento).IDRapportoDiControlloTecnicoBase;

                        CambiaStatoIntervento(long.Parse(visita.IDAccertamento.ToString()), 5);
                        StoricizzaStatoAccertamento(long.Parse(visita.IDAccertamento.ToString()), iDUtente);
                    }
                    else
                    {
                        iDSoggetto = ctx.LIM_LibrettiImpianti.Find(visita.IDLibrettoImpianto).IDSoggettoDerived;

                        var RCT = ctx.RCT_RapportoDiControlloTecnicoBase.OrderByDescending(r => r.DataControllo).FirstOrDefault(
                               r => r.RCT_RapportoDiControlloTecnicoGT.IDLIM_LibrettiImpiantiGruppitermici == visita.IDLibrettoImpiantoGruppoTermico
                               && r.IDTipologiaRCT == 1 && r.IDStatoRapportoDiControllo == 2 && r.DataControllo != null);

                        if (RCT != null)
                        {
                            iDRapportoDiControllo = RCT.IDRapportoControlloTecnico;
                        }
                        else
                        {
                            //Scialuppa di salvataggio RCT
                            int? iDTargaturaImpianto = ctx.LIM_LibrettiImpianti.Find(visita.IDLibrettoImpianto).IDTargaturaImpianto;
                            var generatore = ctx.LIM_LibrettiImpiantiGruppiTermici.FirstOrDefault(g => g.IDLibrettoImpiantoGruppoTermico == visita.IDLibrettoImpiantoGruppoTermico);
                            var rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.OrderByDescending(r => r.DataControllo).FirstOrDefault(
                                            r => r.IDTargaturaImpianto == iDTargaturaImpianto
                                            && r.CodiceProgressivo == generatore.CodiceProgressivo
                                            && r.Prefisso == generatore.Prefisso
                                            && r.IDTipologiaRCT == 1
                                            && r.IDStatoRapportoDiControllo == 2
                                            && r.DataControllo != null);

                            if (rapporto != null)
                            {
                                iDRapportoDiControllo = rapporto.IDRapportoControlloTecnico;
                            }
                        }
                    }

                    ispezione.IDIspezioneVisitaInfo = visita.IDIspezioneVisitaInfo;
                    ispezione.IDIspezioneVisita = iDIspezioneVisita;
                    ispezione.DataIspezione = null;
                    ispezione.IDSoggetto = (int)iDSoggetto;
                    ispezione.IDStatoIspezione = 1;
                    ispezione.fAttivo = true;
                    ispezione.CompensoIspezione = GetCompensoIspezione(visita.IDLibrettoImpianto);

                    ctx.VER_Ispezione.Add(ispezione);
                    ctx.SaveChanges();

                    ispezione.CodiceIspezione = CalcolaCodiceIspezione(ctx.VER_Ispezione.OrderByDescending(c => c.IDIspezione).FirstOrDefault());
                    ctx.SaveChanges();

                    string pathIspezione = pathVisita + @"\" + ispezione.CodiceIspezione + @"\";
                    UtilityFileSystem.CreateDirectoryIfNotExists(pathIspezione);

                    StoricizzaStatoIspezione(ctx, ispezione, 5805);

                    #region Documenti Ispezione
                    SetDocumentiIspezione(ispezione.IDIspezione);
                    #endregion

                    #region Crea Ispezione Rapporto
                    bool fRapportoCreato = CreaIspezioneRapporto(iDIspezioneVisita, ispezione.IDIspezione, visita.IDLibrettoImpianto, visita.IDLibrettoImpiantoGruppoTermico, iDRapportoDiControllo);
                    #endregion

                    #region Calcola compenso ispezione
                    //var ispezioneCompenso = ctx.VER_Ispezione.Where(a => a.IDIspezione == ispezione.IDIspezione).FirstOrDefault();
                    //decimal? compenso = GetCompensoIspezione(v.IDLibrettoImpianto);
                    //ispezioneCompenso.CompensoIspezione = compenso;
                    //ctx.SaveChanges();
                    #endregion

                    var ispezioneVisitaInfo = new VER_IspezioneVisitaInfo();
                    ispezioneVisitaInfo = ctx.VER_IspezioneVisitaInfo.FirstOrDefault(a => a.IDIspezioneVisitaInfo == visita.IDIspezioneVisitaInfo);
                    ispezioneVisitaInfo.fInIspezione = fRapportoCreato;
                    ctx.SaveChanges();
                }

                //trovo il prossimo momento utile per l'invio delle notifiche agli ispettori
                //in quanto si vuole evitare che partano notifiche fuori dagli orari tipicamente lavorativi
                var dueDate = IndividuaMomentoIdealePerRichiestaIspettore(DateTime.Now);
                UtilityScheduler.Schedule(() => CreaGruppoDiVerifica(iDIspezioneVisita), dueDate);
            }
            //});
        }

        public static decimal? GetCompensoIspezione(int iDLibrettoImpianto)
        {
            decimal? compenso = 180m;
            //decimal? potenza = UtilityLibrettiImpianti.GetPotenzaTermicaUtileNominaleImpiantoFromIDLibrettoImpianto(iDLibrettoImpianto);

            //if (potenza != null)
            //{
            //    if (potenza < 35.00m)
            //    {
            //        compenso = 120m;
            //    }
            //    else if (potenza >= 35.00m && potenza < 100.00m)
            //    {
            //        compenso = 180m;
            //    }
            //    else if (potenza >= 100.00m && potenza < 350.00m)
            //    {
            //        compenso = 200m;
            //    }
            //    else if (potenza >= 350.00m)
            //    {
            //        compenso = 300m;
            //    }
            //}

            return compenso;
        }

        private static string CalcolaCodiceIspezione(VER_Ispezione Ispezione)
        {
            //Pad di 10 caratteri
            return string.Format("{0:0000000000}", Ispezione.IDIspezione);
        }

        #region Ispezione Rapporto
        public static bool CreaIspezioneRapporto(long iDIspezioneVisita, long iDIspezione, int iDLibrettoImpianto, int? iDLibrettoImpiantoGruppoTermico, long? iDRapportoDiControllo)
        {
            bool fRapportoCreato = false;

            using (var ctx = new CriterDataModel())
            {
                try
                {
                    #region Crea Rapporto
                    var rapportoIspezione = new VER_IspezioneRapporto();

                    var DatiLibrettoImpianto = ctx.LIM_LibrettiImpianti.FirstOrDefault(l => l.IDLibrettoImpianto == iDLibrettoImpianto);
                    var DatiLibrettoImpiantoGruppoTermico = ctx.LIM_LibrettiImpiantiGruppiTermici.FirstOrDefault(g => g.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico);


                    #region DATI DEFAULT

                    //DATI GENERALI
                    rapportoIspezione.IDIspezione = iDIspezione;
                    rapportoIspezione.IDRapportoDiControllo = iDRapportoDiControllo;
                    if (DatiLibrettoImpianto.IDTargaturaImpianto != null)
                    {
                        rapportoIspezione.IDTargaturaImpianto = DatiLibrettoImpianto.IDTargaturaImpianto.Value;
                    }
                    if (DatiLibrettoImpiantoGruppoTermico != null)
                    {
                        if (DatiLibrettoImpiantoGruppoTermico.DataInstallazione != null)
                        {
                            DateTime? dateValue = UtilityApp.CheckValidDatetimeWithMinValue(DatiLibrettoImpiantoGruppoTermico.DataInstallazione.ToString());
                            rapportoIspezione.DataPrimaInstallazioneImpianto = dateValue;
                            //if (DatiLibrettoImpiantoGruppoTermico.DataInstallazione.ToString() == "01/01/0001 00:00:00")
                            //{
                            //    rapportoIspezione.DataPrimaInstallazioneImpianto = DateTime.Parse("01/01/2001 00:00:00");
                            //}
                            //else
                            //{
                            //    rapportoIspezione.DataPrimaInstallazioneImpianto = Convert.ToDateTime(DatiLibrettoImpiantoGruppoTermico.DataInstallazione.ToString());
                            //}
                        }
                        else
                        {
                            rapportoIspezione.DataPrimaInstallazioneImpianto = null;
                        }
                    }

                    //decimal totaleUtile = 0;
                    //decimal PotenzaNominale = 0;
                    decimal totaleFocalore = 0;
                    var TotaleUtile = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).ToList();
                    foreach (var item in TotaleUtile)
                    {
                        if (item.PotenzaTermicaUtileNominaleKw != null && item.RendimentoTermicoUtilePc != null)
                        {
                            decimal potenzaNominale = decimal.Parse(item.PotenzaTermicaUtileNominaleKw.ToString());
                            if (decimal.Parse(item.RendimentoTermicoUtilePc.ToString()) != 0)
                            {
                                totaleFocalore = totaleFocalore + (potenzaNominale * 100 / decimal.Parse(item.RendimentoTermicoUtilePc.ToString()));
                            }
                        }
                    }
                    rapportoIspezione.PotenzaTermicaNominaleTotaleUtile = UtilityLibrettiImpianti.GetPotenzaTermicaUtileNominaleImpiantoFromIDLibrettoImpianto(iDLibrettoImpianto);

                    if (totaleFocalore != 0)
                    {
                        rapportoIspezione.PotenzaTermicaNominaleTotaleFocolare = totaleFocalore;
                    }
                    else
                    {
                        rapportoIspezione.PotenzaTermicaNominaleTotaleFocolare = null;
                    }


                    // UBUCAZIONE
                    rapportoIspezione.IndirizzoUbicazione = DatiLibrettoImpianto.Indirizzo;
                    rapportoIspezione.CivicoUbicazione = DatiLibrettoImpianto.Civico;
                    rapportoIspezione.PalazzoUbicazione = DatiLibrettoImpianto.Palazzo;
                    rapportoIspezione.ScalaUbicazione = DatiLibrettoImpianto.Scala;
                    rapportoIspezione.InternoUbicazione = DatiLibrettoImpianto.Interno;
                    rapportoIspezione.IDCodiceCatastale = DatiLibrettoImpianto.IDCodiceCatastale;

                    // RESPONSABILE            
                    rapportoIspezione.IDTipologiaResponsabile = DatiLibrettoImpianto.IDTipologiaResponsabile;
                    rapportoIspezione.IDTipoSoggetto = DatiLibrettoImpianto.IDTipoSoggetto;
                    rapportoIspezione.NomeResponsabile = DatiLibrettoImpianto.NomeResponsabile;
                    rapportoIspezione.CognomeResponsabile = DatiLibrettoImpianto.CognomeResponsabile;
                    rapportoIspezione.CodiceFiscaleResponsabile = DatiLibrettoImpianto.CodiceFiscaleResponsabile;
                    rapportoIspezione.RagioneSocialeResponsabile = DatiLibrettoImpianto.RagioneSocialeResponsabile;
                    rapportoIspezione.PartitaIVAResponsabile = DatiLibrettoImpianto.PartitaIvaResponsabile;
                    rapportoIspezione.IndirizzoResponsabile = DatiLibrettoImpianto.IndirizzoResponsabile;
                    rapportoIspezione.CivicoResponsabile = DatiLibrettoImpianto.CivicoResponsabile;
                    var comuneResponsabile = ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == DatiLibrettoImpianto.IDComuneResponsabile && c.fAttivo == true).FirstOrDefault();
                    if (comuneResponsabile != null)
                    {
                        rapportoIspezione.IDCodiceCatastaleResponsabile = comuneResponsabile.IDCodiceCatastale;
                    }
                    else
                    {
                        rapportoIspezione.IDCodiceCatastaleResponsabile = null;
                    }

                    rapportoIspezione.TelefonoResponsabile = null;
                    rapportoIspezione.EmailResponsabile = DatiLibrettoImpianto.EmailResponsabile;
                    rapportoIspezione.EmailPECResponsabile = DatiLibrettoImpianto.EmailPecResponsabile;
                    rapportoIspezione.fTerzoResponsabile = DatiLibrettoImpianto.fTerzoResponsabile;
                    rapportoIspezione.fDelega = false;
                    // TERZO RESPONSABILE
                    if (DatiLibrettoImpianto != null)
                    {
                        if (DatiLibrettoImpianto.fTerzoResponsabile)
                        {
                            DateTime oggi = DateTime.Now.Date;
                            var terzoResponsabile = ctx.LIM_LibrettiImpiantiResponsabili.Where(c => c.IDLibrettoImpianto == DatiLibrettoImpianto.IDLibrettoImpianto && c.fAttivo == true && c.DataInizio.HasValue && c.DataInizio.Value <= oggi && (!c.DataFine.HasValue || c.DataFine.Value > oggi)).OrderByDescending(c => c.DataInizio).FirstOrDefault();
                            if (terzoResponsabile != null)
                            {
                                rapportoIspezione.RagioneSocialeTerzoResponsabile = terzoResponsabile.RagioneSociale;
                                rapportoIspezione.PartitaIVATerzoResponsabile = terzoResponsabile.PartitaIva;

                                if (terzoResponsabile.COM_AnagraficaSoggetti != null)
                                {
                                    rapportoIspezione.IndirizzoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.IndirizzoSedeLegale;
                                    rapportoIspezione.CivicoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.NumeroCivicoSedeLegale;
                                    var comuneTerzoResponsabile = ctx.SYS_CodiciCatastali.Where(c => c.Comune == terzoResponsabile.COM_AnagraficaSoggetti.CittaSedeLegale).FirstOrDefault();
                                    if (comuneTerzoResponsabile != null)
                                    {
                                        rapportoIspezione.IDCodiceCatastaleTerzoResponsabile = comuneTerzoResponsabile.IDCodiceCatastale;
                                    }

                                    rapportoIspezione.TelefonoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.Telefono;
                                    rapportoIspezione.EmailTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.Email;
                                    rapportoIspezione.EmailPECTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.EmailPec;
                                    rapportoIspezione.fAbilitatoTerzoResponsabile = false;
                                    rapportoIspezione.fCertificatoTerzoResponsabile = false;
                                    rapportoIspezione.fAttestatoTerzoResponsabile = false;
                                    rapportoIspezione.fAttestatoIncaricoTerzoResponsabile = false;
                                }
                            }
                        }
                    }
                    // IMPRESA MANUTENTRICE
                    if (DatiLibrettoImpianto != null)
                    {
                        var azienda = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == DatiLibrettoImpianto.IDSoggettoDerived);
                        if (azienda != null)
                        {
                            rapportoIspezione.fImpresaManutentrice = true;
                            rapportoIspezione.RagioneSocialeImpresaManutentrice = azienda.NomeAzienda;
                            rapportoIspezione.PartitaIVAImpresaManutentrice = azienda.PartitaIVA;
                            rapportoIspezione.IndirizzoImpresaManutentrice = azienda.IndirizzoSedeLegale;
                            rapportoIspezione.CivicoImpresaManutentrice = azienda.NumeroCivicoSedeLegale;
                            var comuneImpresaManutentrice = ctx.SYS_CodiciCatastali.Where(c => c.Comune == azienda.CittaSedeLegale).FirstOrDefault();
                            if (comuneImpresaManutentrice != null)
                            {
                                rapportoIspezione.IDCodiceCatastaleImpresaManutentrice = comuneImpresaManutentrice.IDCodiceCatastale;
                            }
                            else
                            {
                                rapportoIspezione.IDCodiceCatastaleImpresaManutentrice = null;
                            }

                            rapportoIspezione.TelefonoImpresaManutentrice = azienda.Telefono;
                            rapportoIspezione.EmailImpresaManutentrice = azienda.Email;
                            rapportoIspezione.EmailPECImpresaManutentrice = azienda.EmailPec;
                            rapportoIspezione.fAbilitataImpresaManutentrice = true;
                            rapportoIspezione.fCertificataImpresaManutentrice = false;
                            rapportoIspezione.fAttestataImpresaManutentrice = false;
                        }
                    }
                    // DELEGATO
                    rapportoIspezione.fDelegatoNominato = false;
                    rapportoIspezione.NomeDelegato = null;
                    rapportoIspezione.CognomeDelegato = null;
                    rapportoIspezione.CodiceFiscaleDelegato = null;
                    // OPERATORE/CONDUTTORE
                    rapportoIspezione.fOperatoreConduttoreNominato = false;
                    rapportoIspezione.fOperatoreConduttorePresente = false;
                    rapportoIspezione.fOperatoreConduttoreAbilitato = false;
                    rapportoIspezione.NomeOperatoreConduttore = null;
                    rapportoIspezione.CognomeOperatoreConduttore = null;
                    rapportoIspezione.CodiceFiscaleOperatoreConduttore = null;
                    rapportoIspezione.IDTipoAbilitazione = null;
                    rapportoIspezione.NumeroPatentinoOperatoreConduttore = null;
                    rapportoIspezione.DataRilascioPatentinoOperatoreConduttore = null;
                    // DATI GENERALI IMPIANTO
                    rapportoIspezione.IDDestinazioneUso = DatiLibrettoImpianto.IDDestinazioneUso;
                    rapportoIspezione.NumeroPOD = DatiLibrettoImpianto.NumeroPOD;
                    rapportoIspezione.NumeroPDR = DatiLibrettoImpianto.NumeroPDR;
                    rapportoIspezione.fImpiantoRegistrato = true;
                    rapportoIspezione.fLibrettoImpiantoPresente = true;
                    rapportoIspezione.VolumeLordoRiscaldato = DatiLibrettoImpianto.VolumeLordoRiscaldato;
                    rapportoIspezione.UnitaImmobiliariServite = null;
                    rapportoIspezione.ServiziServitiImpianto = null;
                    //CONTROLLO DELL'IMPIANTO
                    rapportoIspezione.IDTipoInstallazione = null;
                    rapportoIspezione.LocaleInstallazioneIdoneo = null;
                    rapportoIspezione.GeneratoriInstallazioneIdonei = null;
                    rapportoIspezione.ApertureLibere = null;
                    rapportoIspezione.DimensioniApertureAdeguate = null;
                    rapportoIspezione.ScarichiIdonei = null;
                    rapportoIspezione.AssenzaPerditeCombustibile = null;
                    rapportoIspezione.RendimentoMinimoCombustibile = null;
                    rapportoIspezione.TenutaImpiantoIdraulico = null;
                    rapportoIspezione.TiraggioProvaStrumentalePA = null;
                    //rapportoIspezione.TiraggioProvaIndirettaCalcolata = null;
                    rapportoIspezione.fUsoManutenzioneGeneratore = false;
                    rapportoIspezione.fLibrettoImpiantoCompilato = true;
                    rapportoIspezione.fDichiarazioneConformita = false;
                    //rapportoIspezione.IDCategoriaDocumentazione = null;
                    //rapportoIspezione.ProgettoAntincendio = null;
                    //rapportoIspezione.DataProgettoAntincendio = null;
                    rapportoIspezione.SCIAAntincendio = null;
                    rapportoIspezione.DataSCIAAntincendio = null;
                    //rapportoIspezione.VerbaleSopralluogo = null;
                    //rapportoIspezione.DataVerbaleSopralluogo = null;
                    //rapportoIspezione.UltimoRinnovoPeriodico = null;
                    //rapportoIspezione.DataUltimoRinnovoPeriodico = null;
                    rapportoIspezione.Progettista = null;
                    rapportoIspezione.ProtocolloDepositoComune = null;
                    rapportoIspezione.DataDepositoComune = null;
                    //rapportoIspezione.AQEAttestato = null;
                    rapportoIspezione.DiagnosiEnergetica = null;
                    rapportoIspezione.Perizia = null;
                    rapportoIspezione.fProgettoImpiantoPresente = true;
                    rapportoIspezione.PotenzaProgetto = null;
                    rapportoIspezione.OmologazioneVerifiche = null;
                    rapportoIspezione.CorrettoDimensionamento = (int)EnumStatoSiNoNc.NonClassificabile;

                    //DATI GENERATORE
                    rapportoIspezione.Prefisso = DatiLibrettoImpiantoGruppoTermico.Prefisso;
                    rapportoIspezione.CodiceProgressivo = DatiLibrettoImpiantoGruppoTermico.CodiceProgressivo;
                    if (DatiLibrettoImpiantoGruppoTermico != null)
                    {
                        if (DatiLibrettoImpiantoGruppoTermico.DataInstallazione != null)
                        {
                            DateTime? dateValue = UtilityApp.CheckValidDatetimeWithMinValue(DatiLibrettoImpiantoGruppoTermico.DataInstallazione.ToString());
                            rapportoIspezione.DataInstallazioneGeneratore = dateValue;

                            //if (DatiLibrettoImpiantoGruppoTermico.DataInstallazione.ToString() == "01/01/0001 00:00:00")
                            //{
                            //    rapportoIspezione.DataInstallazioneGeneratore = DateTime.Parse("01/01/2001 00:00:00");
                            //}
                            //else
                            //{
                            //    rapportoIspezione.DataInstallazioneGeneratore = Convert.ToDateTime(DatiLibrettoImpiantoGruppoTermico.DataInstallazione.ToString());
                            //}
                        }
                        else
                        {
                            rapportoIspezione.DataInstallazioneGeneratore = null;
                        }
                    }

                    rapportoIspezione.IDTipologiaFluidoTermoVettore = DatiLibrettoImpiantoGruppoTermico.IDTipologiaFluidoTermoVettore;
                    rapportoIspezione.AltroFluidoTermovettore = DatiLibrettoImpiantoGruppoTermico.FluidoTermovettoreAltro;
                    rapportoIspezione.EvacuazioneForzata = false;
                    rapportoIspezione.EvacuazioneNaturale = false;
                    rapportoIspezione.CostruttoreCaldaia = DatiLibrettoImpiantoGruppoTermico.Fabbricante;
                    rapportoIspezione.ModelloGeneratore = DatiLibrettoImpiantoGruppoTermico.Modello;
                    rapportoIspezione.MatricolaGeneratore = DatiLibrettoImpiantoGruppoTermico.Matricola;
                    rapportoIspezione.CostruttoreBruciatore = null;
                    rapportoIspezione.ModelloBruciatore = null;
                    rapportoIspezione.MatricolaBruciatore = null;
                    rapportoIspezione.PotenzaTermicaNominaleGeneratore = DatiLibrettoImpiantoGruppoTermico.PotenzaTermicaUtileNominaleKw;
                    if (DatiLibrettoImpiantoGruppoTermico.PotenzaTermicaUtileNominaleKw != null && DatiLibrettoImpiantoGruppoTermico.RendimentoTermicoUtilePc != null)
                    {
                        decimal totPotenzaTermicaFocolare = 0;
                        if (decimal.Parse(DatiLibrettoImpiantoGruppoTermico.RendimentoTermicoUtilePc.ToString()) != 0)
                        {
                            totPotenzaTermicaFocolare = decimal.Parse(DatiLibrettoImpiantoGruppoTermico.PotenzaTermicaUtileNominaleKw.ToString()) * 100 / decimal.Parse(DatiLibrettoImpiantoGruppoTermico.RendimentoTermicoUtilePc.ToString());
                        }
                        if (totPotenzaTermicaFocolare != 0)
                        {
                            rapportoIspezione.PotenzaTermicaFocolareGeneratore = totPotenzaTermicaFocolare;
                        }
                        else
                        {
                            rapportoIspezione.PotenzaTermicaFocolareGeneratore = null;
                        }
                    }
                    else
                    {
                        rapportoIspezione.PotenzaTermicaFocolareGeneratore = null;
                    }
                    rapportoIspezione.LavoroBruciatoreDa = null;
                    rapportoIspezione.LavoroBruciatoreA = null;
                    rapportoIspezione.PortataCombustibileValoriMisuratiM3H = null;
                    rapportoIspezione.PortataCombustibileValoriMisuratiKG = null;
                    rapportoIspezione.PotenzaTermicaFocolareValoriMisurati = null;
                    rapportoIspezione.IdTipologiaGruppiTermici = DatiLibrettoImpiantoGruppoTermico.IDTipologiaGruppiTermici;
                    rapportoIspezione.IdTipologiaGeneratoriTermici = null;
                    rapportoIspezione.IDTipologiaCombustibile = DatiLibrettoImpiantoGruppoTermico.IDTipologiaCombustibile;
                    rapportoIspezione.AltroCombustibile = DatiLibrettoImpiantoGruppoTermico.CombustibileAltro;
                    rapportoIspezione.TrattamentoRiscaldamento = 0;
                    rapportoIspezione.TrattamentoACS = 0;
                    rapportoIspezione.IDFrequenzaManutenzione = null;
                    rapportoIspezione.AltroFrequenzaManutenzione = null;
                    rapportoIspezione.IDFrequenzaControllo = null;
                    rapportoIspezione.AltroFrequenzaControllo = null;
                    rapportoIspezione.fUltimaManutenzioneEffettuata = true;
                    rapportoIspezione.DataUltimaManutenzione = null;

                    rapportoIspezione.UltimoControlloEffettuato = null;
                    rapportoIspezione.DataUltimoControllo = null;
                    rapportoIspezione.RaportoControlloPresente = null;
                    rapportoIspezione.RTCEEControllo = null;
                    rapportoIspezione.fRTCEEManutenzioneRegistaro = true;

                    rapportoIspezione.fOsservazioniRCTEE = false;
                    rapportoIspezione.OsservazioniRCTEE = null;
                    rapportoIspezione.fRaccomandazioniRCTEE = false;
                    rapportoIspezione.RaccomandazioniRCTEE = null;
                    rapportoIspezione.fPrescrizioniRCTEE = false;
                    rapportoIspezione.PrescrizioniRCTEE = null;
                    rapportoIspezione.RealizzatiInterventiPrevisti = null;
                    rapportoIspezione.ModuloTermico = null;
                    rapportoIspezione.IDTipologiaSistemaDistribuzione = null;

                    rapportoIspezione.UnitaImmobiliariContabilizzazione = null;
                    rapportoIspezione.IDTipologiaContabilizzazione = null;
                    rapportoIspezione.UnitaImmobiliariTermoregolazione = null;
                    rapportoIspezione.IDTipologiaSistemaTermoregolazione = null;

                    rapportoIspezione.CorrettoFunzionamentoRegolazione = null;
                    rapportoIspezione.CorrettoFunzionamentoInterno = null;
                    rapportoIspezione.MotivazioneEsenzione = null;
                    rapportoIspezione.PresenzaRelazioneTecnica = null;
                    rapportoIspezione.PresenzaDocumentaleAdozione = null;
                    rapportoIspezione.NumeroRilevazioniEseguite = null;
                    rapportoIspezione.RispettoValoriNormativaVigente = null;
                    rapportoIspezione.InterventiAtti = null;
                    rapportoIspezione.StimaDimensionamentoGeneratore = null;
                    rapportoIspezione.fImpiantoPuoFunzionare = true;

                    if (iDRapportoDiControllo != null)
                    {
                        var DatiRapportoControllo = ctx.RCT_RapportoDiControlloTecnicoBase.Where(r => r.IDRapportoControlloTecnico == iDRapportoDiControllo).FirstOrDefault();

                        //rapportoIspezione.PotenzaTermicaNominaleTotaleFocolare = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaNominaleFocolare;
                        //rapportoIspezione.PotenzaTermicaNominaleTotaleUtile = DatiRapportoControllo.PotenzaTermicaNominale;
                        //rapportoIspezione.EvacuazioneForzata = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.EvacuazioneForzata;
                        //rapportoIspezione.EvacuazioneNaturale = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.EvacuazioneNaturale;
                        if (DatiRapportoControllo.Osservazioni != null)
                        {
                            rapportoIspezione.fOsservazioniRCTEE = true;
                            rapportoIspezione.OsservazioniRCTEE = DatiRapportoControllo.Osservazioni;
                        }
                        if (DatiRapportoControllo.Raccomandazioni != null)
                        {
                            rapportoIspezione.fRaccomandazioniRCTEE = true;
                            rapportoIspezione.RaccomandazioniRCTEE = DatiRapportoControllo.Raccomandazioni;
                        }
                        if (DatiRapportoControllo.Prescrizioni != null)
                        {
                            rapportoIspezione.fPrescrizioniRCTEE = true;
                            rapportoIspezione.PrescrizioniRCTEE = DatiRapportoControllo.Prescrizioni;
                        }
                        //if (DatiRapportoControllo.LocaleInstallazioneIdoneo != null)
                        //{
                        //    rapportoIspezione.LocaleInstallazioneIdoneo = int.Parse(DatiRapportoControllo.LocaleInstallazioneIdoneo.ToString());
                        //}
                        //if (DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.GeneratoriIdonei != null)
                        //{
                        //    rapportoIspezione.GeneratoriInstallazioneIdonei = int.Parse(DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.GeneratoriIdonei.ToString());
                        //}
                        //if (DatiRapportoControllo.ApertureLibere != null)
                        //{
                        //    rapportoIspezione.ApertureLibere = int.Parse(DatiRapportoControllo.ApertureLibere.ToString());
                        //}
                        //if (DatiRapportoControllo.DimensioniApertureAdeguate != null)
                        //{
                        //    rapportoIspezione.DimensioniApertureAdeguate = int.Parse(DatiRapportoControllo.DimensioniApertureAdeguate.ToString());
                        //}
                        //if (DatiRapportoControllo.ScarichiIdonei != null)
                        //{
                        //    rapportoIspezione.ScarichiIdonei = int.Parse(DatiRapportoControllo.ScarichiIdonei.ToString());
                        //}
                        //if (DatiRapportoControllo.AssenzaPerditeCombustibile != null)
                        //{
                        //    rapportoIspezione.AssenzaPerditeCombustibile = int.Parse(DatiRapportoControllo.AssenzaPerditeCombustibile.ToString());
                        //}
                        //if (DatiRapportoControllo.TenutaImpiantoIdraulico != null)
                        //{
                        //    rapportoIspezione.TenutaImpiantoIdraulico = int.Parse(DatiRapportoControllo.TenutaImpiantoIdraulico.ToString());
                        //}
                        //rapportoIspezione.IdTipologiaGruppiTermici = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGruppiTermici;
                        //rapportoIspezione.IdTipologiaGeneratoriTermici = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGeneratoriTermici;
                        //rapportoIspezione.RendimentoMinimoCombustibile = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.RendimentoMinimo;
                    }

                    #endregion

                    ctx.VER_IspezioneRapporto.Add(rapportoIspezione);

                    try
                    {
                        ctx.SaveChanges();
                        fRapportoCreato = true;
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    #endregion
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }



            //catch (Exception ex)
            //{
            //    Logger.LogNote(TipoEvento.ErroreApplicativo,
            //                   TipoOggetto.Pagina,
            //                   "iDIspezioneVisita:" + iDIspezioneVisita + "  iDIspezione:" + iDIspezione + "  iDLibrettoImpianto:" + iDLibrettoImpianto + "  iDLibrettoImpiantoGruppoTermico:" + iDLibrettoImpiantoGruppoTermico + "  iDRapportoDiControllo:" + iDRapportoDiControllo + "  Errore:" + ex.Message);
            //}

            return fRapportoCreato;
        }


        //public static void CreaIspezioneRapporto(CriterDataModel ctx, long iDIspezione, int iDLibrettoImpianto, int? iDLibrettoImpiantoGruppoTermico, long? iDRapportoDiControllo)
        //{
        //    var rapportoIspezionePresente = ctx.VER_IspezioneRapporto.FirstOrDefault(i => i.IDIspezione == iDIspezione);

        //    try
        //    {
        //        #region Crea Rapporto
        //        if (rapportoIspezionePresente == null)
        //        {
        //            var rapportoIspezione = new VER_IspezioneRapporto();

        //            var DatiLibrettoImpianto = ctx.LIM_LibrettiImpianti.FirstOrDefault(l => l.IDLibrettoImpianto == iDLibrettoImpianto);
        //            var DatiLibrettoImpiantoGruppoTermico = ctx.LIM_LibrettiImpiantiGruppiTermici.FirstOrDefault(g => g.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico);


        //            #region DATI DEFAULT

        //            //DATI GENERALI
        //            rapportoIspezione.IDIspezione = iDIspezione;
        //            rapportoIspezione.IDRapportoDiControllo = iDRapportoDiControllo;
        //            if (DatiLibrettoImpianto.IDTargaturaImpianto != null) rapportoIspezione.IDTargaturaImpianto = int.Parse(DatiLibrettoImpianto.IDTargaturaImpianto.ToString());
        //            // DELEGATO
        //            rapportoIspezione.fDelegatoNominato = false;
        //            //rapportoIspezione.NomeDelegato = null;
        //            //rapportoIspezione.CognomeDelegato = null;
        //            //rapportoIspezione.CodiceFiscaleDelegato = null;
        //            //rapportoIspezione.fDelega = null;
        //            // OPERATORE/CONDUTTORE
        //            rapportoIspezione.fOperatoreConduttoreNominato = false;
        //            rapportoIspezione.fOperatoreConduttorePresente = false;
        //            //rapportoIspezione.NomeOperatoreConduttore = null;
        //            //rapportoIspezione.CognomeOperatoreConduttore = null;
        //            //rapportoIspezione.CodiceFiscaleOperatoreConduttore = null;
        //            //rapportoIspezione.fOperatoreConduttoreAbilitato = null;
        //            //rapportoIspezione.IDTipoAbilitazione = null;
        //            //rapportoIspezione.NumeroPatentinoOperatoreConduttore = null;
        //            //rapportoIspezione.DataRilascioPatentinoOperatoreConduttore = null;
        //            // DATI GENERALI IMPIANTO
        //            rapportoIspezione.NumeroPOD = DatiLibrettoImpianto.NumeroPOD;
        //            rapportoIspezione.NumeroPDR = DatiLibrettoImpianto.NumeroPDR;
        //            rapportoIspezione.fImpiantoRegistrato = true; // DEFAULT

        //            if (DatiLibrettoImpiantoGruppoTermico != null)
        //            {
        //                rapportoIspezione.DataPrimaInstallazioneImpianto = DatiLibrettoImpiantoGruppoTermico.DataInstallazione;
        //            }

        //            decimal totaleUtile = 0;
        //            decimal PotenzaNominale = 0;
        //            decimal totaleFocalore = 0;

        //            var TotaleUtile = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).ToList();
        //            foreach (var item in TotaleUtile)
        //            {
        //                if (item.PotenzaTermicaUtileNominaleKw != null)
        //                {
        //                    PotenzaNominale = decimal.Parse(item.PotenzaTermicaUtileNominaleKw.ToString());
        //                    totaleUtile = totaleUtile + PotenzaNominale;

        //                    if (item.RendimentoTermicoUtilePc != null)
        //                    {
        //                        totaleFocalore = totaleFocalore + (PotenzaNominale / decimal.Parse(item.RendimentoTermicoUtilePc.ToString()));
        //                    }
        //                }
        //            }

        //            rapportoIspezione.PotenzaTermicaNominaleTotaleUtile = totaleUtile;
        //            rapportoIspezione.PotenzaTermicaNominaleTotaleFocolare = totaleFocalore;
        //            // UBUCAZIONE
        //            rapportoIspezione.IDCodiceCatastale = DatiLibrettoImpianto.IDCodiceCatastale;
        //            rapportoIspezione.IndirizzoUbicazione = DatiLibrettoImpianto.Indirizzo;
        //            rapportoIspezione.CivicoUbicazione = DatiLibrettoImpianto.Civico;
        //            rapportoIspezione.PalazzoUbicazione = DatiLibrettoImpianto.Palazzo;
        //            rapportoIspezione.ScalaUbicazione = DatiLibrettoImpianto.Scala;
        //            rapportoIspezione.InternoUbicazione = DatiLibrettoImpianto.Interno;
        //            // RESPONSABILE            
        //            rapportoIspezione.IDTipologiaResponsabile = DatiLibrettoImpianto.IDTipologiaResponsabile;
        //            rapportoIspezione.IDTipoSoggetto = DatiLibrettoImpianto.IDTipoSoggetto;
        //            rapportoIspezione.NomeResponsabile = DatiLibrettoImpianto.NomeResponsabile;
        //            rapportoIspezione.CognomeResponsabile = DatiLibrettoImpianto.CognomeResponsabile;
        //            rapportoIspezione.CodiceFiscaleResponsabile = DatiLibrettoImpianto.CodiceFiscaleResponsabile;
        //            rapportoIspezione.RagioneSocialeResponsabile = DatiLibrettoImpianto.RagioneSocialeResponsabile;
        //            rapportoIspezione.PartitaIVAResponsabile = DatiLibrettoImpianto.PartitaIvaResponsabile;
        //            var comuneResponsabile = ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == DatiLibrettoImpianto.IDComuneResponsabile && c.fAttivo == true).FirstOrDefault();
        //            if (comuneResponsabile != null)
        //            {
        //                rapportoIspezione.IDCodiceCatastaleResponsabile = comuneResponsabile.IDCodiceCatastale;
        //            }
        //            else
        //            {
        //                rapportoIspezione.IDCodiceCatastaleResponsabile = null;
        //            }
        //            rapportoIspezione.IndirizzoResponsabile = DatiLibrettoImpianto.IndirizzoResponsabile;
        //            rapportoIspezione.CivicoResponsabile = DatiLibrettoImpianto.CivicoResponsabile;

        //            rapportoIspezione.TelefonoResponsabile = null;
        //            rapportoIspezione.EmailResponsabile = DatiLibrettoImpianto.EmailResponsabile;
        //            rapportoIspezione.EmailPECResponsabile = DatiLibrettoImpianto.EmailPecResponsabile;
        //            // TERZO RESPONSABILE
        //            rapportoIspezione.fTerzoResponsabile = DatiLibrettoImpianto.fTerzoResponsabile;
        //            if (rapportoIspezione.fTerzoResponsabile == true)
        //            {
        //                DateTime oggi = DateTime.Now.Date;
        //                var terzoResponsabile = ctx.LIM_LibrettiImpiantiResponsabili.Where(c => c.IDLibrettoImpianto == DatiLibrettoImpianto.IDLibrettoImpianto && c.fAttivo == true && c.DataInizio.HasValue && c.DataInizio.Value <= oggi && (!c.DataFine.HasValue || c.DataFine.Value > oggi)).OrderByDescending(c => c.DataInizio).FirstOrDefault();
        //                if (terzoResponsabile != null)
        //                {
        //                    rapportoIspezione.RagioneSocialeTerzoResponsabile = terzoResponsabile.RagioneSociale;
        //                    rapportoIspezione.PartitaIVATerzoResponsabile = terzoResponsabile.PartitaIva;

        //                    //var comuneTerzoResponsabile = ctx.COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == terzoResponsabile.IDSoggetto).FirstOrDefault();  //ctx.SYS_CodiciCatastali.Where(c => c.IDProvincia == terzoResponsabile.COM_AnagraficaSoggetti.SYS_Province.IDProvincia).FirstOrDefault();
        //                    //if (comuneTerzoResponsabile != null)
        //                    //{
        //                    if (terzoResponsabile.COM_AnagraficaSoggetti != null)
        //                    {
        //                        var comuneTerzoResponsabile = ctx.SYS_CodiciCatastali.Where(c => c.Comune == terzoResponsabile.COM_AnagraficaSoggetti.CittaSedeLegale).FirstOrDefault();
        //                        if (comuneTerzoResponsabile != null)
        //                        {
        //                            rapportoIspezione.IDCodiceCatastaleTerzoResponsabile = comuneTerzoResponsabile.IDCodiceCatastale;
        //                        }

        //                        rapportoIspezione.IndirizzoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.IndirizzoSedeLegale;
        //                        rapportoIspezione.CivicoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.NumeroCivicoSedeLegale;
        //                        rapportoIspezione.TelefonoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.Telefono;
        //                        rapportoIspezione.EmailTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.Email;
        //                        rapportoIspezione.EmailPECTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.EmailPec;
        //                        //rapportoIspezione.fAbilitatoTerzoResponsabile = null;
        //                        //rapportoIspezione.fCertificatoTerzoResponsabile = null;
        //                        //rapportoIspezione.fAttestatoTerzoResponsabile = null;
        //                        //rapportoIspezione.fAttestatoIncaricoTerzoResponsabile = null;
        //                    }
        //                }
        //            }
        //            // IMPRESA MANUTENTRICE
        //            rapportoIspezione.fImpresaManutentrice = false;

        //            var azienda = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == DatiLibrettoImpianto.IDSoggettoDerived);
        //            if (azienda != null)
        //            {
        //                rapportoIspezione.fImpresaManutentrice = true;
        //                rapportoIspezione.RagioneSocialeImpresaManutentrice = azienda.NomeAzienda;
        //                rapportoIspezione.PartitaIVAImpresaManutentrice = azienda.PartitaIVA;

        //                var comuneImpresaManutentrice = ctx.SYS_CodiciCatastali.Where(c => c.Comune == azienda.CittaSedeLegale).FirstOrDefault();
        //                if (comuneImpresaManutentrice != null)
        //                {
        //                    rapportoIspezione.IDCodiceCatastaleImpresaManutentrice = comuneImpresaManutentrice.IDCodiceCatastale;
        //                }

        //                rapportoIspezione.IndirizzoImpresaManutentrice = azienda.IndirizzoSedeLegale;
        //                rapportoIspezione.CivicoImpresaManutentrice = azienda.NumeroCivicoSedeLegale;
        //                rapportoIspezione.TelefonoImpresaManutentrice = azienda.Telefono;
        //                rapportoIspezione.EmailImpresaManutentrice = azienda.Email;
        //                rapportoIspezione.EmailPECImpresaManutentrice = azienda.EmailPec;
        //                //rapportoIspezione.fAbilitataImpresaManutentrice = null;
        //                //rapportoIspezione.fCertificataImpresaManutentrice = null;
        //                //rapportoIspezione.fAttestataImpresaManutentrice = null;
        //            }
        //            // OPERATORE ULTIMO CONTROLLO 
        //            // NULL
        //            // 3. DESTINAZIONE D'USO DELL'EDIFICIO O DELL'UNITA' IMMOBILIARE
        //            rapportoIspezione.IDDestinazioneUso = DatiLibrettoImpianto.IDDestinazioneUso;
        //            //rapportoIspezione.UnitaImmobiliariServite = null;
        //            //rapportoIspezione.ServiziServitiImpianto = null;
        //            rapportoIspezione.VolumeLordoRiscaldato = DatiLibrettoImpianto.VolumeLordoRiscaldato;
        //            // 4. CONTROLLO DELL'IMPIANTO
        //            //rapportoIspezione.IDTipoInstallazione = null;
        //            rapportoIspezione.LocaleInstallazioneIdoneo = null;
        //            rapportoIspezione.GeneratoriInstallazioneIdonei = null;
        //            rapportoIspezione.ApertureLibere = null;
        //            rapportoIspezione.DimensioniApertureAdeguate = null;
        //            rapportoIspezione.ScarichiIdonei = null;
        //            rapportoIspezione.AssenzaPerditeCombustibile = null;
        //            rapportoIspezione.TenutaImpiantoIdraulico = null;
        //            //rapportoIspezione.TiraggioProvaStrumentalePA = null;
        //            //rapportoIspezione.TiraggioProvaIndirettaCalcolata = null;
        //            //rapportoIspezione.fLibrettoImpiantoPresente = null;
        //            //rapportoIspezione.fUsoManutenzioneGeneratore = null;
        //            //rapportoIspezione.fLibrettoImpiantoCompilato = null;
        //            //rapportoIspezione.fDichiarazioneConformita = null;
        //            //rapportoIspezione.IDCategoriaDocumentazione = null;
        //            rapportoIspezione.ProgettoAntincendio = null;
        //            //rapportoIspezione.DataProgettoAntincendio = null;
        //            rapportoIspezione.SCIAAntincendio = null;
        //            rapportoIspezione.DataSCIAAntincendio = null;
        //            rapportoIspezione.VerbaleSopralluogo = null;
        //            //rapportoIspezione.DataVerbaleSopralluogo = null;
        //            rapportoIspezione.UltimoRinnovoPeriodico = null;
        //            //rapportoIspezione.DataUltimoRinnovoPeriodico = null;
        //            //rapportoIspezione.fProgettoImpiantoPresente = null;
        //            //rapportoIspezione.Progettista = null;
        //            //rapportoIspezione.ProtocolloDepositoComune = null;
        //            //rapportoIspezione.DataDepositoComune = null;
        //            //rapportoIspezione.PotenzaProgetto = null;
        //            rapportoIspezione.AQEAttestato = null;
        //            rapportoIspezione.DiagnosiEnergetica = null;
        //            rapportoIspezione.Perizia = null;
        //            rapportoIspezione.OmologazioneVerifiche = null;
        //            // 6.1 DATI GENERATORE
        //            rapportoIspezione.Prefisso = DatiLibrettoImpiantoGruppoTermico.Prefisso;
        //            rapportoIspezione.CodiceProgressivo = DatiLibrettoImpiantoGruppoTermico.CodiceProgressivo;
        //            rapportoIspezione.DataInstallazioneGeneratore = DatiLibrettoImpiantoGruppoTermico.DataInstallazione; // CONTROLALRE
        //            rapportoIspezione.IDTipologiaFluidoTermoVettore = DatiLibrettoImpiantoGruppoTermico.IDTipologiaFluidoTermoVettore;
        //            rapportoIspezione.AltroFluidoTermovettore = DatiLibrettoImpiantoGruppoTermico.FluidoTermovettoreAltro;
        //            rapportoIspezione.EvacuazioneForzata = false; // TODO
        //            rapportoIspezione.EvacuazioneNaturale = false; // TODO
        //            rapportoIspezione.CostruttoreCaldaia = DatiLibrettoImpiantoGruppoTermico.Fabbricante;
        //            rapportoIspezione.ModelloGeneratore = DatiLibrettoImpiantoGruppoTermico.Modello;
        //            rapportoIspezione.MatricolaGeneratore = DatiLibrettoImpiantoGruppoTermico.Matricola;
        //            //rapportoIspezione.CostruttoreBruciatore = null;
        //            //rapportoIspezione.ModelloBruciatore = null;
        //            //rapportoIspezione.MatricolaBruciatore = null;
        //            rapportoIspezione.PotenzaTermicaNominaleGeneratore = DatiLibrettoImpiantoGruppoTermico.PotenzaTermicaUtileNominaleKw;
        //            if (DatiLibrettoImpiantoGruppoTermico.PotenzaTermicaUtileNominaleKw != null && DatiLibrettoImpiantoGruppoTermico.RendimentoTermicoUtilePc != null)
        //                rapportoIspezione.PotenzaTermicaFocolareGeneratore = decimal.Parse(DatiLibrettoImpiantoGruppoTermico.PotenzaTermicaUtileNominaleKw.ToString()) / decimal.Parse(DatiLibrettoImpiantoGruppoTermico.RendimentoTermicoUtilePc.ToString());

        //            //rapportoIspezione.PotenzaTermicaFocolareGeneratore = null;
        //            //rapportoIspezione.LavoroBruciatoreDa = null;
        //            //rapportoIspezione.LavoroBruciatoreA = null;
        //            //rapportoIspezione.PortataCombustibileValoriMisuratiM3H = null;
        //            //rapportoIspezione.PortataCombustibileValoriMisuratiKG = null;
        //            //rapportoIspezione.PotenzaTermicaFocolareValoriMisurati = null;
        //            rapportoIspezione.IdTipologiaGruppiTermici = DatiLibrettoImpiantoGruppoTermico.IDTipologiaGruppiTermici;
        //            //rapportoIspezione.IdTipologiaGeneratoriTermici = null;
        //            rapportoIspezione.CorrettoDimensionamento = (int)EnumStatoSiNoNc.NonClassificabile;
        //            rapportoIspezione.IDTipologiaCombustibile = DatiLibrettoImpiantoGruppoTermico.IDTipologiaCombustibile;
        //            rapportoIspezione.AltroCombustibile = DatiLibrettoImpiantoGruppoTermico.CombustibileAltro;
        //            rapportoIspezione.TrattamentoRiscaldamento = 0; // DEFAULT VALUE 0 = ASSENTE
        //            rapportoIspezione.TrattamentoACS = 0;  // DEFAULT VALUE 0 = ASSENTE
        //                                                   //rapportoIspezione.IDFrequenzaManutenzione = null;
        //                                                   //rapportoIspezione.AltroFrequenzaManutenzione = null;
        //                                                   //rapportoIspezione.fUltimaManutenzioneEffettuata = null;
        //                                                   //rapportoIspezione.DataUltimaManutenzione = null;
        //                                                   //rapportoIspezione.IDFrequenzaControllo = null;
        //                                                   //rapportoIspezione.AltroFrequenzaControllo = null;
        //                                                   //rapportoIspezione.UltimoControlloEffettuato = null;
        //                                                   //rapportoIspezione.DataUltimoControllo = null;
        //                                                   //rapportoIspezione.RaportoControlloPresente = null;
        //                                                   //rapportoIspezione.RTCEEControllo = null;
        //                                                   //rapportoIspezione.fRTCEEManutenzioneRegistaro = null;
        //            rapportoIspezione.fOsservazioniRCTEE = false;
        //            //rapportoIspezione.OsservazioniRCTEE = null;
        //            rapportoIspezione.fRaccomandazioniRCTEE = false;
        //            //rapportoIspezione.RaccomandazioniRCTEE = null;
        //            rapportoIspezione.fPrescrizioniRCTEE = false;
        //            //rapportoIspezione.PrescrizioniRCTEE = null;
        //            //rapportoIspezione.RealizzatiInterventiPrevisti = null;
        //            //rapportoIspezione.ModuloTermico = null;
        //            //rapportoIspezione.IDTipologiaSistemaDistribuzione = null;
        //            rapportoIspezione.UnitaImmobiliariContabilizzazione = null; // NC DEFAULT
        //            rapportoIspezione.IDTipologiaContabilizzazione = null; // NC DEFAULT
        //            rapportoIspezione.UnitaImmobiliariTermoregolazione = null; // NC DEFAULT
        //            rapportoIspezione.IDTipologiaSistemaTermoregolazione = null; // NC DEFAULT
        //            rapportoIspezione.CorrettoFunzionamentoRegolazione = null; // NC DEFAULT
        //            rapportoIspezione.CorrettoFunzionamentoInterno = null; // NC DEFAULT
        //            rapportoIspezione.MotivazioneEsenzione = null; // NC DEFAULT
        //            rapportoIspezione.PresenzaRelazioneTecnica = null; // NC DEFAULT
        //            rapportoIspezione.PresenzaDocumentaleAdozione = null;
        //            //rapportoIspezione.NumeroRilevazioniEseguite = null;
        //            //rapportoIspezione.RispettoValoriNormativaVigente = null;
        //            rapportoIspezione.InterventiAtti = null; // NC DEFAULT
        //            rapportoIspezione.StimaDimensionamentoGeneratore = null; // NC DEFAULT
        //                                                                     //rapportoIspezione.fImpiantoPuoFunzionare = null;

        //            if (iDRapportoDiControllo != null)
        //            {
        //                var DatiRapportoControllo = ctx.RCT_RapportoDiControlloTecnicoBase.Where(r => r.IDRapportoControlloTecnico == iDRapportoDiControllo).FirstOrDefault();

        //                rapportoIspezione.PotenzaTermicaNominaleTotaleFocolare = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaNominaleFocolare;
        //                rapportoIspezione.PotenzaTermicaNominaleTotaleUtile = DatiRapportoControllo.PotenzaTermicaNominale; ; // CONTROLLARE 
        //                rapportoIspezione.EvacuazioneForzata = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.EvacuazioneForzata;
        //                rapportoIspezione.EvacuazioneNaturale = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.EvacuazioneNaturale;
        //                if (DatiRapportoControllo.Osservazioni != null)
        //                {
        //                    rapportoIspezione.fOsservazioniRCTEE = true;
        //                    rapportoIspezione.OsservazioniRCTEE = DatiRapportoControllo.Osservazioni;
        //                }
        //                if (DatiRapportoControllo.Raccomandazioni != null)
        //                {
        //                    rapportoIspezione.fRaccomandazioniRCTEE = true;
        //                    rapportoIspezione.RaccomandazioniRCTEE = DatiRapportoControllo.Raccomandazioni;
        //                }
        //                if (DatiRapportoControllo.Prescrizioni != null)
        //                {
        //                    rapportoIspezione.fPrescrizioniRCTEE = true;
        //                    rapportoIspezione.PrescrizioniRCTEE = DatiRapportoControllo.Prescrizioni;
        //                }
        //                if (DatiRapportoControllo.LocaleInstallazioneIdoneo != null)
        //                {
        //                    rapportoIspezione.LocaleInstallazioneIdoneo = int.Parse(DatiRapportoControllo.LocaleInstallazioneIdoneo.ToString());
        //                }
        //                if (DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.GeneratoriIdonei != null)
        //                {
        //                    rapportoIspezione.GeneratoriInstallazioneIdonei = int.Parse(DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.GeneratoriIdonei.ToString());
        //                }
        //                if (DatiRapportoControllo.ApertureLibere != null)
        //                {
        //                    rapportoIspezione.ApertureLibere = int.Parse(DatiRapportoControllo.ApertureLibere.ToString());
        //                }
        //                if (DatiRapportoControllo.DimensioniApertureAdeguate != null)
        //                {
        //                    rapportoIspezione.DimensioniApertureAdeguate = int.Parse(DatiRapportoControllo.DimensioniApertureAdeguate.ToString());
        //                }
        //                if (DatiRapportoControllo.ScarichiIdonei != null)
        //                {
        //                    rapportoIspezione.ScarichiIdonei = int.Parse(DatiRapportoControllo.ScarichiIdonei.ToString());
        //                }
        //                if (DatiRapportoControllo.AssenzaPerditeCombustibile != null)
        //                {
        //                    rapportoIspezione.AssenzaPerditeCombustibile = int.Parse(DatiRapportoControllo.AssenzaPerditeCombustibile.ToString());
        //                }
        //                if (DatiRapportoControllo.TenutaImpiantoIdraulico != null)
        //                {
        //                    rapportoIspezione.TenutaImpiantoIdraulico = int.Parse(DatiRapportoControllo.TenutaImpiantoIdraulico.ToString());
        //                }
        //                rapportoIspezione.IdTipologiaGruppiTermici = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGruppiTermici;
        //                rapportoIspezione.IdTipologiaGeneratoriTermici = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGeneratoriTermici;
        //                rapportoIspezione.RendimentoMinimoCombustibile = DatiRapportoControllo.RCT_RapportoDiControlloTecnicoGT.RendimentoMinimo;
        //            }

        //            #endregion

        //            ctx.VER_IspezioneRapporto.Add(rapportoIspezione);

        //            try
        //            {
        //                ctx.SaveChanges();
        //            }
        //            catch (System.Data.Entity.Validation.DbEntityValidationException e)
        //            {
        //                foreach (var eve in e.EntityValidationErrors)
        //                {
        //                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //                    foreach (var ve in eve.ValidationErrors)
        //                    {
        //                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                            ve.PropertyName, ve.ErrorMessage);
        //                    }
        //                }
        //                throw;
        //            }
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {

        //    }        
        //}

        #region TRATTAMENTO ACQUA

        public static void SaveInsertDeleteDatiTrattamentoAcquaInvernale(
                int iDIspezione,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var trattamentiAttuali = db.VER_IspezioneRapportoTrattamentoInvernale.Where(i => i.IDIspezione == iDIspezione).ToList();

            foreach (var trattamenti in trattamentiAttuali)
            {
                if (!valoriSelected.Contains(trattamenti.IDTipologiaTrattamentoAcqua.ToString()))
                {
                    db.VER_IspezioneRapportoTrattamentoInvernale.Remove(trattamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!trattamentiAttuali.Any(o => o.IDTipologiaTrattamentoAcqua.ToString() == valoriSelected[i]))
                {
                    db.VER_IspezioneRapportoTrattamentoInvernale.Add(new VER_IspezioneRapportoTrattamentoInvernale() { IDTipologiaTrattamentoAcqua = int.Parse(valoriSelected[i]), IDIspezione = iDIspezione });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTrattamentoAcquaAcs(
               int iDIspezione,
               string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var trattamentiAttuali = db.VER_IspezioneRapportoTrattamentoAcs.Where(i => i.IDIspezione == iDIspezione).ToList();

            foreach (var trattamenti in trattamentiAttuali)
            {
                if (!valoriSelected.Contains(trattamenti.IDTipologiaTrattamentoAcqua.ToString()))
                {
                    db.VER_IspezioneRapportoTrattamentoAcs.Remove(trattamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!trattamentiAttuali.Any(o => o.IDTipologiaTrattamentoAcqua.ToString() == valoriSelected[i]))
                {
                    db.VER_IspezioneRapportoTrattamentoAcs.Add(new VER_IspezioneRapportoTrattamentoAcs() { IDTipologiaTrattamentoAcqua = int.Parse(valoriSelected[i]), IDIspezione = iDIspezione });
                }
            }

            db.SaveChanges();
        }

        public static List<VER_IspezioneRapportoTrattamentoAcs> GetValoriIspezioneRapportoTrattamentoAcquaAcs(int iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.VER_IspezioneRapportoTrattamentoAcs.Where(a => a.IDIspezione == iDIspezione).OrderBy(s => s.IDTipologiaTrattamentoAcqua).ToList();

                return result;
            }
        }

        public static List<VER_IspezioneRapportoTrattamentoInvernale> GetValoriIspezioneRapportoTrattamentoAcquaInvernale(int iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.VER_IspezioneRapportoTrattamentoInvernale.Where(a => a.IDIspezione == iDIspezione).OrderBy(s => s.IDTipologiaTrattamentoAcqua).ToList();

                return result;
            }
        }

        #endregion

        #region CHECK LIST

        public static void SaveInsertDeleteDatiCheckList(
                int iDIspezione,
                int iDTipoCheckList,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var checklistAttuali = db.VER_IspezioneRapportoCheckList.Where(i => i.IDIspezione == iDIspezione && i.IDTipoCheckList == iDTipoCheckList).ToList();

            foreach (var checkList in checklistAttuali)
            {
                if (!valoriSelected.Contains(checkList.IDTipologiaCheckList.ToString()))
                {
                    db.VER_IspezioneRapportoCheckList.Remove(checkList);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!checklistAttuali.Any(o => o.IDTipologiaCheckList.ToString() == valoriSelected[i]))
                {
                    db.VER_IspezioneRapportoCheckList.Add(new VER_IspezioneRapportoCheckList() { IDTipologiaCheckList = int.Parse(valoriSelected[i]), IDIspezione = iDIspezione, IDTipoCheckList = iDTipoCheckList });
                }
            }

            db.SaveChanges();
        }

        public static List<VER_IspezioneRapportoCheckList> GetValoriIspezioneRapportoCheckList(int iDIspezione, int iDTipoCheckList)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.VER_IspezioneRapportoCheckList.Where(a => a.IDIspezione == iDIspezione && a.IDTipoCheckList == iDTipoCheckList).OrderBy(s => s.IDTipologiaCheckList).ToList();

                return result;
            }
        }


        #endregion

        #region Raccomandazioni/Prescrizioni
        public static List<VER_IspezioneRaccomandazioniPrescrizioni> GetValoriIspezioneRaccomandazioniPrescrizioni(long iDIspezione, int? iDTipologiaRaccomandazionePrescrizioneIspezione, string type)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            var result = db.VER_IspezioneRaccomandazioniPrescrizioni.AsQueryable();
            if (type == "Raccomandazioni")
            {
                result = result.Where(a => a.IDIspezione == iDIspezione && a.IDTipologiaRaccomandazioneIspezione != null);
            }
            else if (type == "Prescrizioni")
            {
                result = result.Where(a => a.IDIspezione == iDIspezione && a.IDTipologiaPrescrizioneIspezione != null);
            }

            if (iDTipologiaRaccomandazionePrescrizioneIspezione != null)
            {
                result = result.Where(a => a.IDTipologiaRaccomandazionePrescrizioneIspezione == iDTipologiaRaccomandazionePrescrizioneIspezione);
            }

            return result.OrderBy(s => s.IDRaccomandazioniPrescrizioniIspezione).ToList();
        }

        public static void SaveInsertDeleteDatiRaccomandazioniPrescrizioni(
                long iDIspezione,
                int iDTipologiaRaccomandazionePrescrizioneIspezione,
                object[] valoriSelected, string type)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;

            if (type == "Raccomandazioni")
            {
                var raccomandazioniAttuali = db.VER_IspezioneRaccomandazioniPrescrizioni.Where(i => i.IDIspezione == iDIspezione & i.IDTipologiaRaccomandazionePrescrizioneIspezione == iDTipologiaRaccomandazionePrescrizioneIspezione & i.IDTipologiaRaccomandazioneIspezione != null).ToList();

                foreach (var raccomandazioni in raccomandazioniAttuali)
                {
                    if (!valoriSelected.Contains(raccomandazioni.IDTipologiaRaccomandazioneIspezione.ToString()))
                    {
                        db.VER_IspezioneRaccomandazioniPrescrizioni.Remove(raccomandazioni);
                    }
                }
                for (int i = 0; i < valoriSelected.Length; i++)
                {
                    if (!raccomandazioniAttuali.Any(o => o.IDTipologiaRaccomandazioneIspezione.ToString() == valoriSelected[i].ToString()))
                    {
                        db.VER_IspezioneRaccomandazioniPrescrizioni.Add(new VER_IspezioneRaccomandazioniPrescrizioni() { IDTipologiaRaccomandazioneIspezione = int.Parse(valoriSelected[i].ToString()), IDIspezione = iDIspezione, IDTipologiaRaccomandazionePrescrizioneIspezione = iDTipologiaRaccomandazionePrescrizioneIspezione });
                    }
                }
            }
            else if (type == "Prescrizioni")
            {
                var prescrizioniAttuali = db.VER_IspezioneRaccomandazioniPrescrizioni.Where(i => i.IDIspezione == iDIspezione & i.IDTipologiaRaccomandazionePrescrizioneIspezione == iDTipologiaRaccomandazionePrescrizioneIspezione & i.IDTipologiaPrescrizioneIspezione != null).ToList();

                foreach (var prescrizioni in prescrizioniAttuali)
                {
                    if (!valoriSelected.Contains(prescrizioni.IDTipologiaPrescrizioneIspezione.ToString()))
                    {
                        db.VER_IspezioneRaccomandazioniPrescrizioni.Remove(prescrizioni);
                    }
                }
                for (int i = 0; i < valoriSelected.Length; i++)
                {
                    if (!prescrizioniAttuali.Any(o => o.IDTipologiaPrescrizioneIspezione.ToString() == valoriSelected[i].ToString()))
                    {
                        db.VER_IspezioneRaccomandazioniPrescrizioni.Add(new VER_IspezioneRaccomandazioniPrescrizioni() { IDTipologiaPrescrizioneIspezione = int.Parse(valoriSelected[i].ToString()), IDIspezione = iDIspezione, IDTipologiaRaccomandazionePrescrizioneIspezione = iDTipologiaRaccomandazionePrescrizioneIspezione });
                    }
                }
            }

            db.SaveChanges();
        }

        public static void SetFieldsRaccomandazioniPrescrizioni(long iDIspezione)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var raccomandazioni = (from a in ctx.VER_IspezioneRaccomandazioniPrescrizioni
                                       join c in ctx.SYS_RCTTipologiaRaccomandazione on a.IDTipologiaRaccomandazioneIspezione equals c.IDTipologiaRaccomandazione
                                       where a.IDIspezione == iDIspezione
                                       select c.Raccomandazione
                                      ).ToList();

                var prescrizioni = (from a in ctx.VER_IspezioneRaccomandazioniPrescrizioni
                                    join c in ctx.SYS_RCTTipologiaPrescrizione on a.IDTipologiaPrescrizioneIspezione equals c.IDTipologiaPrescrizione
                                    where a.IDIspezione == iDIspezione
                                    select c.Prescrizione
                                   ).ToList();

                var rapporto = (from a in ctx.VER_IspezioneRapporto
                                where a.IDIspezione == iDIspezione
                                select a
                                   ).FirstOrDefault();

                string tempRaccomandazioni = string.Empty;
                foreach (var r in raccomandazioni)
                {
                    tempRaccomandazioni += r + "\n\n";
                }

                string tempPrescrizioni = string.Empty;
                foreach (var p in prescrizioni)
                {
                    tempPrescrizioni += p + "\n\n";
                }

                string raccomandazioniLibere = string.Empty;
                if (!string.IsNullOrEmpty(rapporto.Raccomandazioni) && (!string.IsNullOrEmpty(tempRaccomandazioni)))
                {
                    string raccomandazioniLiberetemp = rapporto.Raccomandazioni;
                    foreach (var r in raccomandazioni)
                    {
                        raccomandazioniLiberetemp = raccomandazioniLiberetemp.Replace(r, "");
                    }
                    raccomandazioniLibere = raccomandazioniLiberetemp.Replace("\n", "");
                }

                string prescrizioniLibere = string.Empty;
                if (!string.IsNullOrEmpty(rapporto.Prescrizioni) && (!string.IsNullOrEmpty(tempPrescrizioni)))
                {
                    string prescrizioniLiberetemp = rapporto.Prescrizioni;
                    foreach (var p in prescrizioni)
                    {
                        prescrizioniLiberetemp = prescrizioniLiberetemp.Replace(p, "");
                    }
                    prescrizioniLibere = prescrizioniLiberetemp.Replace("\n", "");
                }

                var rapportoIspezione = new VER_IspezioneRapporto();
                rapportoIspezione = ctx.VER_IspezioneRapporto.FirstOrDefault(c => c.IDIspezione == iDIspezione);
                if (!string.IsNullOrEmpty(raccomandazioniLibere.Replace("\n", "")))
                {
                    rapportoIspezione.Raccomandazioni = raccomandazioniLibere + "\n\n" + tempRaccomandazioni;
                }
                else
                {
                    if (!string.IsNullOrEmpty(tempRaccomandazioni))
                    {
                        rapportoIspezione.Raccomandazioni = tempRaccomandazioni;
                    }
                    //else
                    //{
                    //    rapportoRP.Raccomandazioni = null;
                    //}
                }
                if (!string.IsNullOrEmpty(prescrizioniLibere.Replace("\n", "")))
                {
                    rapportoIspezione.Prescrizioni = prescrizioniLibere + "\n\n" + tempPrescrizioni;
                }
                else
                {
                    if (!string.IsNullOrEmpty(tempPrescrizioni))
                    {
                        rapportoIspezione.Prescrizioni = tempPrescrizioni;
                    }
                    //else
                    //{
                    //    rapportoRP.Prescrizioni = null;
                    //}
                }

                ctx.SaveChanges();
            }
        }

        #endregion

        public static List<VER_IspezioneRapportoStrumenti> GetValoriDatiStrumento(int iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.VER_IspezioneRapportoStrumenti.Where(a => a.IDIspezione == iDIspezione).OrderBy(s => s.IDIspezioneStrumenti).ToList();

                return result;
            }
        }

        #endregion

        public static void StoricizzaStatoIspezione(CriterDataModel ctx, VER_Ispezione ispezione, int iDUtente)
        {
            VER_IspezioneStato ispezioneStato = new VER_IspezioneStato();
            ispezioneStato.IDIspezione = ispezione.IDIspezione;
            ispezioneStato.Data = DateTime.Now;
            ispezioneStato.DataIspezione = ispezione.DataIspezione;
            ispezioneStato.IDOrarioDa = ispezione.IDOrarioDa;
            ispezioneStato.IDOrarioA = ispezione.IDOrarioA;
            ispezioneStato.IDStatoIspezione = ispezione.IDStatoIspezione;
            ispezioneStato.IDUtenteUltimaModifica = iDUtente;
            ctx.VER_IspezioneStato.Add(ispezioneStato);
            ctx.SaveChanges();
        }

        public static void StoricizzaStatoIspezione(long iDIspezione, int iDUtente)
        {
            using (var ctx = new CriterDataModel())
            {
                VER_Ispezione ispezione = ctx.VER_Ispezione.Find(iDIspezione);
                if (ispezione != null)
                {
                    StoricizzaStatoIspezione(ctx, ispezione, iDUtente);
                }
            }
        }

        public static void SetDocumentiIspezione(long iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var documenti = ctx.SYS_TipoDocumentoIspezione.Where(c => c.fAttivo == true).ToList();
                foreach (var doc in documenti)
                {
                    var ispezioneDocumenti = new VER_IspezioneDocumento();
                    ispezioneDocumenti.IDIspezione = iDIspezione;
                    ispezioneDocumenti.IDTipoDocumentoIspezione = doc.IDTipoDocumentoIspezione;
                    ispezioneDocumenti.fDocumentoObbligatorio = doc.fDocumentoObbligatorio;
                    ispezioneDocumenti.NomeDocumento = null;
                    ispezioneDocumenti.fDocumentoCompilato = false;
                    ctx.VER_IspezioneDocumento.Add(ispezioneDocumenti);
                    ctx.SaveChanges();
                }
            }
        }

        public static void fDocumentoIspezioneCompilato(long iDIspezioneDocumento, string fileName)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var documentoIspezione = ctx.VER_IspezioneDocumento.Where(i => i.IDIspezioneDocumento == iDIspezioneDocumento).FirstOrDefault();
                documentoIspezione.fDocumentoCompilato = true;
                documentoIspezione.NomeDocumento = fileName;
                ctx.SaveChanges();
            }
        }


        //public static bool fStatoIspezioneNonConclusa(long iDIspezioneVisita)
        //{
        //    bool statoIspezioneNonConclusa = true;

        //    using (CriterDataModel ctx = new CriterDataModel())
        //    {
        //        var statoIspezione = ctx.VER_Ispezione.Where(i => i.IDIspezioneVisita == iDIspezioneVisita).ToList();

        //        foreach (var item in statoIspezione)
        //        {
        //            if (item.IDStatoIspezione == 4 || item.IDStatoIspezione == 5)
        //            {
        //                statoIspezioneNonConclusa = false;
        //            }
        //        }
        //    }

        //    return statoIspezioneNonConclusa;
        //}

        public static void CambiaStatoIspezione(long iDIspezione, int iDStatoIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                ispezione.IDStatoIspezione = iDStatoIspezione;

                ctx.SaveChanges();
            }
        }

        #region Gruppo di Verifica
        public static void AnnullaRichiesteVerificaScadute(long iDIspezioneVisita, long iDIspezioneGruppoVerifica)
        {
            using (var ctx = new CriterDataModel())
            {
                //annullare eventuali richieste ad ispettori che non hanno risposto in tempo
                var daAnnullare = ctx.VER_IspezioneGruppoVerifica.Where(c =>
                    c.IDIspezioneVisita == iDIspezioneVisita
                    && c.IDStatoPianificazioneIspettore == 2
                    && c.IDIspezioneGruppoVerifica != iDIspezioneGruppoVerifica
                    && c.DataInvioPianificazione != null).ToList();

                foreach (var ispettoreGruppoVerifica in daAnnullare)
                {
                    var ispettoreGv = ctx.VER_IspezioneGruppoVerifica.Find(ispettoreGruppoVerifica.IDIspezioneGruppoVerifica);
                    ispettoreGv.IDStatoPianificazioneIspettore = 5;
                    ispettoreGv.DataRifiuto = DateTime.Now;
                    ispettoreGv.fIspettorePrincipale = false;
                    ispettoreGv.fInGruppoVerifica = false;
                    ctx.SaveChanges();

                    //HelpAnnullaRichiestaVerificaScadute(item.IDIspezioneVisita, int.Parse(item.IDIspezioneGruppoVerifica.ToString()));
                    //CambiaStatoPianificazioneIspezioneIspettore(item.IDIspezioneGruppoVerifica, 5, ctx, item.IDIspezioneVisita);
                }
            }
        }

        //public static void HelpAnnullaRichiestaVerificaScadute(long iDIspezioneVisita, int iDIspezioneGruppoVerifica)
        //{
        //    using (var ctx = new CriterDataModel())
        //    {
        //        var ListaIspezioneNellaVisita = ctx.VER_Ispezione.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

        //        bool Exit = true;

        //        foreach (var item in ListaIspezioneNellaVisita)
        //        {
        //            if (item.IDStatoIspezione != 1)
        //            {
        //                Exit = false;
        //            }
        //        }

        //        if (Exit)
        //        {

        //            //RicercaProssimoIspettorePerIspezione(item.IDIspezione.Value);
        //        }
        //    }
        //}

        public static void NotificaLettereIncaricoIspettori()
        {
            bool usaPec = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);

            using (var ctx = new CriterDataModel())
            {
                //Spedisco le mail agli ispettori che hanno accettato, se per qualche motivo non era partita prima..
                var emailIncaricoDaSpedire = ctx.VER_IspezioneGruppoVerifica.Where(c =>
                    (c.IDStatoPianificazioneIspettore == 3 ||
                     c.IDStatoPianificazioneIspettore == 7) &&
                    c.fInGruppoVerifica == true &&
                    c.fEmailIncaricoInviata == false).ToList(); //ToList Altrimenti EF va in errore nel foreach

                foreach (var item in emailIncaricoDaSpedire)
                {
                    //TODO devo anche mandare la lettera di incarico se per caso qualcuno non l'ha ricevuta per qualche motivo..
                    if (EmailNotify.SendMailPerIspettore_PresaInCaricoIspezione(ctx, item, usaPec))
                    {
                        item.fEmailIncaricoInviata = true;
                        item.fIncaricoFirmato = false;
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public static void SetIspettoreInVisita(int iDIspettore, long iDIspezioneVisita)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezioni = ctx.VER_Ispezione.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                foreach (var item in ispezioni)
                {
                    var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == item.IDIspezione).FirstOrDefault();

                    ispezione.IDIspettore = iDIspettore;
                    ispezione.IDContrattoIspettore = UtilitySoggetti.GetContrattoContrattoIspettoreAttivo(iDIspettore);
                }
                ctx.SaveChanges();
            }
        }

        public static void IspezioneAssegnaManualmenteAdIspettore(long iDIspezioneGruppoVerifica, long iDIspezioneVisita, int iDIspettore)
        {
            using (var ctx = new CriterDataModel())
            {
                CambiaStatoPianificazioneIspezioneIspettore(iDIspezioneGruppoVerifica, 7, ctx, iDIspezioneVisita);
                //var ispezioneIspettore = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                //if (ispezioneIspettore != null)
                //{
                //    //devo però annullare tutti gli altri, qualora ci fossero 

                //    foreach (var Annullare in ispezioneIspettore)
                //    {
                //        //Annullare.fInGruppoVerifica = false;
                //        //Annullare.fIspettorePrincipale = false;

                //        if (Annullare.IDStatoPianificazioneIspettore == 1 // Pianificazione in sospeso
                //            || Annullare.IDStatoPianificazioneIspettore == 2)  // Pianificazione in attesa di conferma da parte dell'ispettore
                //                                                               //|| Annullare.IDStatoPianificazioneIspettore == 3  // Pianificazione accettata
                //                                                               //|| Annullare.IDStatoPianificazioneIspettore == 7) // Pianificazione assegnata da Coordinatore
                //        {
                //            CambiaStatoPianificazioneIspezioneIspettore(Annullare.IDIspezioneGruppoVerifica, 6, ctx, iDIspezioneVisita);  // 6 - Pianificazione annullata da Coordinatore
                //        }
                //    }
                //    ctx.SaveChanges();

                //    CambiaStatoPianificazioneIspezioneIspettore(iDIspezioneGruppoVerifica, 7, ctx, iDIspezioneVisita);
                //}
            }
        }

        public static void IspezioneRimandaInRicercaIspettoreNoFirmaLAI(long iDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                var gruppoVerifica = ctx.VER_IspezioneGruppoVerifica.Where(a => a.IDIspezioneVisita == iDIspezioneVisita && a.IDStatoPianificazioneIspettore == 3).FirstOrDefault();
                CambiaStatoPianificazioneIspezioneIspettore(gruppoVerifica.IDIspezioneGruppoVerifica, 8, ctx, iDIspezioneVisita);
            }
        }

        public static void IspezioneRimandaInRicercaIspettoreNoPianificazione(long iDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                var gruppoVerifica = ctx.VER_IspezioneGruppoVerifica.Where(a => a.IDIspezioneVisita == iDIspezioneVisita && a.IDStatoPianificazioneIspettore == 3).FirstOrDefault();
                CambiaStatoPianificazioneIspezioneIspettore(gruppoVerifica.IDIspezioneGruppoVerifica, 9, ctx, iDIspezioneVisita);
            }
        }

        public static void IspezioneRimandaInRicercaIspettoreNoCorrettaPianificazione(long iDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                var gruppoVerifica = ctx.VER_IspezioneGruppoVerifica.Where(a => a.IDIspezioneVisita == iDIspezioneVisita && a.IDStatoPianificazioneIspettore == 3).FirstOrDefault();
                CambiaStatoPianificazioneIspezioneIspettore(gruppoVerifica.IDIspezioneGruppoVerifica, 10, ctx, iDIspezioneVisita);
            }
        }


        public static void CambiaStatoIspezioneMassivo(long iDIspezioneVisita, int iDStatoIspezione, long? iDIspezione, DateTime? dataIspezione, int? iDOrarioDa, int? iDOrarioA)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var statoIspezione = ctx.VER_Ispezione.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).AsQueryable();
                if (iDIspezione != null)
                {
                    statoIspezione = statoIspezione.Where(c => c.IDIspezione == iDIspezione);
                }

                foreach (var ispezione in statoIspezione.ToList())
                {
                    CambiaStatoIspezione(ispezione.IDIspezione, iDStatoIspezione);

                    //Serve per portare indietro l'ispezione in ricerca ispettore nel caso in cui ad esempio non firma la LAI
                    if (iDStatoIspezione == 1)
                    {
                        ispezione.IDContrattoIspettore = null;
                        ispezione.IDIspettore = null;
                        ispezione.DataIspezione = null;
                        ispezione.IDOrarioDa = null;
                        ispezione.IDOrarioA = null;
                    }
                    else
                    if (iDStatoIspezione == 3)
                    {
                        ispezione.DataIspezione = dataIspezione;
                        ispezione.IDOrarioDa = iDOrarioDa;
                        ispezione.IDOrarioA = iDOrarioA;
                    }
                    else if (iDStatoIspezione == 2)
                    {
                        ispezione.DataIspezione = null;
                        ispezione.IDOrarioDa = null;
                        ispezione.IDOrarioA = null;
                    }
                }
                ctx.SaveChanges();
            }
        }

        public static DateTime IndividuaMomentoIdealePerRichiestaIspettore(DateTime dataPrevista)
        {
            DateTime dataRiferimento = dataPrevista;
            if (ConfigurationManager.AppSettings["fDataIdealeIspezione"] == "on")
            {
                int minHour = 8;
                int maxHour = 18;
                //se sono già passate le 18 il momento valido è il giorno successivo
                if (dataRiferimento.TimeOfDay.TotalHours > maxHour)
                {
                    dataRiferimento = dataRiferimento.Date.AddDays(1);
                }
                //se non sono ancora le 8 il momento valido diventano le 8 dello stesso giorno
                if (dataRiferimento.TimeOfDay.TotalHours < minHour)
                {
                    dataRiferimento = dataRiferimento.Date.AddHours(minHour);
                }
                //sabato e domenica non devono partire notifiche, vanno schedulate per il lunedì mattina 
                if (dataRiferimento.DayOfWeek == DayOfWeek.Saturday)
                {
                    dataRiferimento = dataRiferimento.Date.AddDays(2).AddHours(minHour);
                }
                if (dataRiferimento.DayOfWeek == DayOfWeek.Sunday)
                {
                    dataRiferimento = dataRiferimento.Date.AddDays(1).AddHours(minHour);
                }
            }

            return dataRiferimento;
        }

        

        private static bool IsIspettoreInAttesaDiRispostaSuAltreVerifiche(VER_IspezioneGruppoVerifica ispettore)
        {
            bool IspettoreInAttessa = false;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var CountIspettore = ispettore.COM_AnagraficaSoggetti.VER_IspezioneGruppoVerifica.Where(c =>
                c.IDIspezioneVisita != ispettore.IDIspezioneVisita
                //&& c.fInGruppoVerifica
                && c.IDStatoPianificazioneIspettore == 2).ToList();

                foreach (var item in CountIspettore)
                {
                    var datiIspettore = ctx.VER_Ispezione.Where(c => c.IDIspezioneVisita == item.IDIspezioneVisita && c.IDStatoIspezione == 1).ToList();

                    if (datiIspettore.Count() > 0)
                    {
                        IspettoreInAttessa = true;
                    }
                }
            }
            return IspettoreInAttessa;

            //var elencoStatiVerificheIspettore = new List<int>() { 1 };

            //return ispettore.COM_AnagraficaSoggetti.VER_IspezioneGruppoVerifica.Any(c =>
            //    c.IDIspezioneVisita != ispettore.IDIspezioneVisita
            //    && c.fInGruppoVerifica
            //    //&& elencoStatiVerificheIspettore.Contains(c.VER_Ispezione.IDStatoIspezione)
            //    && c.IDStatoPianificazioneIspettore == 2);
        }

        private static List<VER_IspezioneGruppoVerifica> GetIspettoriConMenoVerifiche(List<VER_IspezioneGruppoVerifica> listaIspettori, int numeroIspettori)
        {
            var toReturn = new List<VER_IspezioneGruppoVerifica>();

            foreach (var ispettore in listaIspettori.OrderBy(i => i.NumeroVerificheEffettuate).ToList())
            {
                if (IsIspettoreInAttesaDiRispostaSuAltreVerifiche(ispettore))
                    continue;

                toReturn.Add(ispettore);

                if (toReturn.Count >= numeroIspettori)
                    break;
            }
            return toReturn;
        }

        #region Info Distanza Ispettore/Impianto
        private static Tuple<bool, int, int> GetInfoPercorsoIspettoreImpianto(COM_AnagraficaSoggetti ispettore, long iDIspezioneVisita)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var indirizzoIspettore = string.Empty;
                var cittaIspettore = string.Empty;

                if (ispettore.fDomicilioUgualeResidenza)
                {
                    indirizzoIspettore = string.Format("{0}, {1} ({2}), {3}, {4}",
                                                        ispettore.IndirizzoDomicilio,
                                                        ispettore.CittaDomicilio,
                                                        ispettore.SYS_Province3 != null ? ispettore.SYS_Province3.SiglaProvincia : string.Empty,
                                                        ispettore.CapDomicilio,
                                                        ispettore.SYS_Paesi2 != null ? ispettore.SYS_Paesi2.Paese : string.Empty);
                }
                else
                {
                    indirizzoIspettore = string.Format("{0}, {1} ({2}), {3}, {4}",
                                                        ispettore.IndirizzoResidenza,
                                                        ispettore.CittaResidenza,
                                                        ispettore.SYS_Province2 != null ? ispettore.SYS_Province2.SiglaProvincia : string.Empty,
                                                        ispettore.CapResidenza,
                                                        ispettore.SYS_Paesi1 != null ? ispettore.SYS_Paesi1.Paese : string.Empty);
                }



                //if (ispettore.fDomicilioUgualeResidenza)
                //{
                //    indirizzoIspettore = string.Format("{0}, {1} ({2}), {3}, Italia",
                //                                        ispettore.IndirizzoDomicilio,
                //                                        ispettore.CittaDomicilio,
                //                                        ispettore.SYS_Province3.SiglaProvincia,
                //                                        ispettore.CapDomicilio);
                //}
                //else
                //{
                //    indirizzoIspettore = string.Format("{0}, {1} ({2}), {3}, Italia",
                //                                        ispettore.IndirizzoResidenza,
                //                                        ispettore.CittaResidenza,
                //                                        ispettore.SYS_Province2 != null ? ispettore.SYS_Province2.SiglaProvincia : string.Empty,
                //                                        ispettore.CapResidenza);
                //}




                var ListaLibrettiNellaVisita = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                string indirizzoImpianto = "";
                string cittaImpianto = "";
                bool Start = true;

                foreach (var item in ListaLibrettiNellaVisita)
                {
                    var DatiLibretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == item.IDLibrettoImpianto).FirstOrDefault();

                    if (Start)
                    {
                        indirizzoImpianto = DatiLibretto.Indirizzo + ", " + DatiLibretto.Civico + " " + DatiLibretto.SYS_CodiciCatastali.Cap + " " + DatiLibretto.SYS_CodiciCatastali.Comune + " IT";
                        cittaImpianto = DatiLibretto.Indirizzo + ", " + DatiLibretto.Civico + " " + DatiLibretto.SYS_CodiciCatastali.Cap + " " + DatiLibretto.SYS_CodiciCatastali.Comune + " IT"; // CONTROLLARE

                        Start = false;
                    }
                    else
                    {
                        indirizzoImpianto += "|" + DatiLibretto.Indirizzo + ", " + DatiLibretto.Civico + " " + DatiLibretto.SYS_CodiciCatastali.Cap + " " + DatiLibretto.SYS_CodiciCatastali.Comune + " IT";
                        cittaImpianto += "|" + DatiLibretto.Indirizzo + ", " + DatiLibretto.Civico + " " + DatiLibretto.SYS_CodiciCatastali.Cap + " " + DatiLibretto.SYS_CodiciCatastali.Comune + " IT";
                    }
                }

                var ret = TryGetInfoPercorso(indirizzoIspettore, indirizzoImpianto);

                //se non risolvo il percorso con gli indirizzi completi provo usando solo la città dove si trova l'edificio, visto che il dato dell'edificio è il più suscettibile di errore
                if (!ret.Item1)
                {
                    ret = TryGetInfoPercorso(indirizzoIspettore, cittaImpianto);
                }

                //se non risolvo nemmeno in quel modo trovo la distanza tra le due città, e questa dovrebbe sempre risolverla
                if (!ret.Item1)
                {
                    ret = TryGetInfoPercorso(cittaIspettore, cittaImpianto);
                }

                return ret;
            }
        }

        private static Tuple<bool, int, int> TryGetInfoPercorso(string from, string to)
        {
            bool succeeded = true;

            var infoPercorso = Google.GoogleMaps.CalcolaDistanzaPercorso(from, to);

            double? distanza = 10000;
            double? tempoPercorrenza = 0;
            bool start = true;
            int counT = 0;

            try
            {
                foreach (var item in infoPercorso.Path.destination_addresses)
                {
                    if (start)
                    {
                        distanza = infoPercorso.Path.rows[0].elements[counT].distance.value / 1000;
                        tempoPercorrenza = infoPercorso.Path.rows[0].elements[counT].duration.value / 60;
                        counT++;
                        start = false;
                    }
                    else
                    {
                        distanza = distanza + (infoPercorso.Path.rows[0].elements[counT].distance.value / 1000);
                        tempoPercorrenza = tempoPercorrenza + (infoPercorso.Path.rows[0].elements[counT].duration.value / 60);
                        counT++;
                    }
                }

                succeeded = true;
            }
            catch
            {
                distanza = 10000;
                tempoPercorrenza = 0;
                succeeded = false;
            }

            return Tuple.Create(succeeded, (int)Math.Round(distanza.Value), (int)Math.Round(tempoPercorrenza.Value));
        }

        #endregion

        public static void CambiaStatoPianificazioneIspezioneIspettore(long IDIspezioneGruppoVerifica, int IDStatoPianificazioneIspettore, CriterDataModel ctx, long IDIspezioneVisita)
        {
            var ispettoreGruppoVerifica = ctx.VER_IspezioneGruppoVerifica.Find(IDIspezioneGruppoVerifica);
            if (ispettoreGruppoVerifica != null)
            {
                ispettoreGruppoVerifica.IDStatoPianificazioneIspettore = IDStatoPianificazioneIspettore;
                switch (IDStatoPianificazioneIspettore)
                {
                    case 1: // Pianificazione in sospeso
                        break;
                    case 2: //Pianificazione in attesa di conferma da parte dell'ispettore                       
                        ispettoreGruppoVerifica.DataInvioPianificazione = DateTime.Now;
                        //devo annullare su una visita ispettiva le ispezioni che sono scadute
                        AnnullaRichiesteVerificaScadute(IDIspezioneVisita, IDIspezioneGruppoVerifica);
                        break;
                    case 3: //Pianificazione accettata
                        ispettoreGruppoVerifica.DataAccettazione = DateTime.Now;
                        ispettoreGruppoVerifica.fInGruppoVerifica = true;
                        ispettoreGruppoVerifica.fIspettorePrincipale = true;
                        SetIspettoreInVisita(ispettoreGruppoVerifica.IDIspettore, IDIspezioneVisita);
                        CambiaStatoIspezioneMassivo(IDIspezioneVisita, 6, null, null, null, null);

                        var Annullare = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita).ToList();
                        foreach (var item in Annullare)
                        {
                            if (item.IDStatoPianificazioneIspettore == 1 // Pianificazione in sospeso
                            || item.IDStatoPianificazioneIspettore == 2)  // Pianificazione in attesa di conferma da parte dell'ispettore
                            {
                                CambiaStatoPianificazioneIspezioneIspettore(item.IDIspezioneGruppoVerifica, 4, ctx, IDIspezioneVisita);
                            }
                        }

                        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
                        string destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita;
                        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                        string reportName = ConfigurationManager.AppSettings["ReportNameLetteraIncarico"];
                        string urlPdf = ReportingServices.GetIspezioneLetteraIncaricoReport(IDIspezioneVisita.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

                        bool usaPec = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);
                        if (EmailNotify.SendMailPerIspettore_PresaInCaricoIspezione(ctx, ispettoreGruppoVerifica, usaPec))
                        {
                            ispettoreGruppoVerifica.fEmailIncaricoInviata = true;
                            ctx.SaveChanges();
                        }
                        break;
                    case 4: // Pianificazione rifiutata
                        ispettoreGruppoVerifica.DataRifiuto = DateTime.Now;
                        ispettoreGruppoVerifica.fIspettorePrincipale = false;
                        ispettoreGruppoVerifica.fInGruppoVerifica = false;
                        break;
                    case 5: //Pianificazione automaticamente annullata dal sistema trascorse 24 ore
                        ispettoreGruppoVerifica.DataRifiuto = DateTime.Now;
                        ispettoreGruppoVerifica.fIspettorePrincipale = false;
                        ispettoreGruppoVerifica.fInGruppoVerifica = false;
                        break;
                    case 6: //Pianificazione annullata da Coordinatore
                        ispettoreGruppoVerifica.fIspettorePrincipale = false;
                        ispettoreGruppoVerifica.fInGruppoVerifica = false;
                        break;
                    case 7: //Pianificazione assegnata da Coordinatore
                        ispettoreGruppoVerifica.DataAccettazione = DateTime.Now;
                        ispettoreGruppoVerifica.fIspettorePrincipale = true;
                        ispettoreGruppoVerifica.fInGruppoVerifica = true;
                        ctx.SaveChanges();

                        SetIspettoreInVisita(ispettoreGruppoVerifica.IDIspettore, IDIspezioneVisita);
                        CambiaStatoIspezioneMassivo(IDIspezioneVisita, 6, null, null, null, null);

                        var Annullare1 = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita).ToList();
                        foreach (var item in Annullare1)
                        {
                            if (item.IDStatoPianificazioneIspettore == 1 // Pianificazione in sospeso
                            || item.IDStatoPianificazioneIspettore == 2)  // Pianificazione in attesa di conferma da parte dell'ispettore
                            {
                                CambiaStatoPianificazioneIspezioneIspettore(item.IDIspezioneGruppoVerifica, 6, ctx, IDIspezioneVisita);
                            }
                        }

                        //INVIO MAIL con lettera di incarico e altro materiale..
                        string reportPathAssegnatoCoordinatore = ConfigurationManager.AppSettings["ReportPath"];
                        string reportUrlAssegnatoCoordinatore = ConfigurationManager.AppSettings["ReportRemoteURL"];
                        string destinationFileAssegnatoCoordinatore = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita;
                        string urlSiteAssegnatoCoordinatore = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                        string reportNameAssegnatoCoordinatore = ConfigurationManager.AppSettings["ReportNameLetteraIncarico"];
                        string urlPdfAssegnatoCoordinatore = ReportingServices.GetIspezioneLetteraIncaricoReport(IDIspezioneVisita.ToString(), reportNameAssegnatoCoordinatore, reportUrlAssegnatoCoordinatore, reportPathAssegnatoCoordinatore, destinationFileAssegnatoCoordinatore, urlSiteAssegnatoCoordinatore);

                        bool usaPecEmail = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);
                        if (EmailNotify.SendMailPerIspettore_PresaInCaricoIspezione(ctx, ispettoreGruppoVerifica, usaPecEmail))
                        {
                            ispettoreGruppoVerifica.fEmailIncaricoInviata = true;
                            ctx.SaveChanges();
                        }
                        break;
                    case 8: //Pianificazione annullata per inadempienza dell'ispettore a firmare la LAI
                    case 9: //Pianificazione annullata per inadempienza dell'ispettore a pianificare
                    case 10: //Pianificazione annullata per non corretta pianificazione dell'ispettore
                        ispettoreGruppoVerifica.DataInvioPianificazione = null;
                        ispettoreGruppoVerifica.DataAccettazione = null;
                        ispettoreGruppoVerifica.fInGruppoVerifica = false;
                        ispettoreGruppoVerifica.fIspettorePrincipale = false;
                        ispettoreGruppoVerifica.fIncaricoFirmato = false;
                        ispettoreGruppoVerifica.fEmailIncaricoInviata = false;
                        ctx.SaveChanges();

                        CambiaStatoIspezioneMassivo(IDIspezioneVisita, 1, null, null, null, null);

                        var annulla = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita).ToList();
                        foreach (var item in annulla)
                        {
                            if (item.IDStatoPianificazioneIspettore == 4 // Pianificazione rifiutata
                            || item.IDStatoPianificazioneIspettore == 6)  // Pianificazione annullata da Coordinatore
                            {
                                CambiaStatoPianificazioneIspezioneIspettore(item.IDIspezioneGruppoVerifica, 1, ctx, IDIspezioneVisita);
                            }
                        }

                        UtilityVerifiche.ChiediDisponibilitaProssimoIspettore(IDIspezioneVisita);
                        break;
                    case 11: // Pianificazione rifiutata per conflitto di interessi
                        ispettoreGruppoVerifica.DataRifiuto = DateTime.Now;
                        ispettoreGruppoVerifica.fIspettorePrincipale = false;
                        ispettoreGruppoVerifica.fInGruppoVerifica = false;
                        break;
                }
                ctx.SaveChanges();
            }
        }

        private static long? GetProssimoIspettorePerIspezione(long iDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                //assegno la verifica al prossimo ispettore solo se questa è in stato ricerca ispettore
                if (IsIDStatoIspezioneUno(iDIspezioneVisita))
                {
                    var ispettoreCandidatoQuery = ctx.VER_IspezioneGruppoVerifica.Where(
                        c =>
                            c.IDIspezioneVisita == iDIspezioneVisita
                            //Solo ispettori RGVI (non esiste più il concetto di ruolo)
                            //&& c.IDRuolo == 1
                            //Solo ispettori attivi
                            ///&& c.COM_AnagraficaSoggetti.fAttivo == true
                            //Solo ispettori accreditati
                            //&& c.COM_AnagraficaSoggetti.COM_AnagraficaSoggettiAccreditamento.IDStatoAccreditamento == 4
                            //l'ispettore non è stato ancora contattato per questa ispezione (In Sospeso)
                            && c.IDStatoPianificazioneIspettore == 1
                            );

                    var ispettoreCandidato = ispettoreCandidatoQuery.OrderBy(c => c.Distanza).FirstOrDefault();

                    if (ispettoreCandidato != null)
                    {
                        return ispettoreCandidato.IDIspezioneGruppoVerifica;
                    }
                }
                return null;
            }
        }

        public static string ChiediDisponibilitaProssimoIspettore(long iDIspezioneVisita, int delay = 0)
        {
            long? iDIspezioneGruppoVerifica = GetProssimoIspettorePerIspezione(iDIspezioneVisita);

            if (iDIspezioneGruppoVerifica.HasValue)
            {
                //calcolo il momento previsto di ricerca ispettore ed eventualmente lo sposto avanti in modo da skippare gli orari festivi
                var dueDate = IndividuaMomentoIdealePerRichiestaIspettore(DateTime.Now.AddMinutes(delay));

                return UtilityScheduler.Schedule(() => ChiediDisponibilitaIspettore(iDIspezioneGruppoVerifica.Value), dueDate);
            }
            return null;
        }

        public static void ChiediDisponibilitaIspettore(long iDIspezioneGruppoVerifica)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispettoreCandidato = ctx.VER_IspezioneGruppoVerifica.FirstOrDefault(c => c.IDIspezioneGruppoVerifica == iDIspezioneGruppoVerifica);

                if (ispettoreCandidato != null)
                {
                    if (ispettoreCandidato.IDStatoPianificazioneIspettore == 1 //in sospeso
                        && IsIDStatoIspezioneUno(ispettoreCandidato.IDIspezioneVisita)
                        && HelpChiediDisponibilitaProssimoIspettore(ispettoreCandidato.IDIspezioneVisita))
                    {
                        //invio la mail per la notifica
                        bool usaPec = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);
                        EmailNotify.SendMailPerIspettore_RichiestaDisponibilitaPerIspezione(ispettoreCandidato, usaPec);
                        //Cambio lo stato della pianificazione a 'email inviata'
                        CambiaStatoPianificazioneIspezioneIspettore(iDIspezioneGruppoVerifica, 2, ctx, ispettoreCandidato.IDIspezioneVisita);

                        int intervalloProssimoIspettoreMinuti = 0;
                        if (!int.TryParse(ConfigurationManager.AppSettings["IntervalloProssimoIspettoreMinuti"], out intervalloProssimoIspettoreMinuti))
                        {
                            intervalloProssimoIspettoreMinuti = 120;
                        }

                        ChiediDisponibilitaProssimoIspettore(ispettoreCandidato.IDIspezioneVisita, intervalloProssimoIspettoreMinuti);
                    }
                }
            }
        }

        public static bool IsIDStatoIspezioneUno(long iDIspezioneVisita)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                bool Si = true;

                var statoIspezione = ctx.VER_Ispezione.Where(i => i.IDIspezioneVisita == iDIspezioneVisita).ToList();

                foreach (var item in statoIspezione)
                {
                    if (item.IDStatoIspezione != 1)
                    {
                        Si = false;
                    }
                }

                return Si;
            }
        }

        public static bool HelpChiediDisponibilitaProssimoIspettore(long iDIspezioneVisita)
        {
            bool ChiediProssimo = true;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var getDatiGruppo = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                foreach (var item in getDatiGruppo)
                {
                    if (item.IDStatoPianificazioneIspettore == 3 || item.IDStatoPianificazioneIspettore == 7)
                    {
                        ChiediProssimo = false;
                    }
                }
            }
            return ChiediProssimo;
        }



        public static void RicercaIspettoriPerVerificaOld(CriterDataModel ctx, long iDIspezioneVisita)
        {
            //int numeroIspettoriFasciaA = 2;
            int lunghezzaPeriodoConteggioVerifiche = 3;
            var punteggio = 0;

            //Calcola per ogni ispettore i dati che mi serviranno per poter poi decidere quale chiamare (es. distanza) per ogni ispettore
            var listaIspettoriAbilitati = ctx.COM_AnagraficaSoggetti.Where(c => c.fAttivo == true
                                                                               && c.IDTipoSoggetto == 7
                                                                               && c.COM_AnagraficaSoggettiAccreditamento.IDStatoAccreditamento == 4
                                                                               && c.COM_AnagraficaSoggettiAccreditamento.fAttivo == true
                                                                               && (c.COM_ContrattoIspettore.FirstOrDefault().fAttivo == true
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().IDStatoContratto == 2
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().NumeroIspezioniMax > c.VER_Ispezione.Count(a => a.IDIspettore == c.IDSoggetto && a.IDContrattoIspettore == c.COM_ContrattoIspettore.FirstOrDefault().IDContrattoIspettore)
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().DataInizioContratto <= DateTime.Now
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().DataFineContratto >= DateTime.Now
                                                                               )
                                                                          ).ToList();


            var listaIspezioneIspettore = new List<VER_IspezioneGruppoVerifica>(listaIspettoriAbilitati.Count);

            foreach (var ispettore in listaIspettoriAbilitati)
            {
                //tutte le ispezioni in cui ha partecipato, che poi si sono concluse o in corso
                int numeroVerificheEffettuate = GetNumeroVerificheIspettoreUltimoPeriodo(ispettore, lunghezzaPeriodoConteggioVerifiche);

                var infoPercorso = GetInfoPercorsoIspettoreImpianto(ispettore, iDIspezioneVisita);

                var gruppoverifica = new VER_IspezioneGruppoVerifica();
                gruppoverifica.COM_AnagraficaSoggetti = ispettore;
                gruppoverifica.IDIspezioneVisita = iDIspezioneVisita;
                gruppoverifica.NumeroVerificheEffettuate = numeroVerificheEffettuate;
                gruppoverifica.PunteggioGraduatoria = 0;
                if (infoPercorso.Item1)
                {
                    //percorso individuato
                    gruppoverifica.Distanza = infoPercorso.Item2;
                    gruppoverifica.TempoPercorrenza = infoPercorso.Item3;
                }
                else
                {
                    //Se Google non trova l'indirizzo metto distanza 1000
                    //Se l'indirizzo dell'edificio non viene trovato tutti gli ispettori prendono il max
                    gruppoverifica.Distanza = 1000;
                    gruppoverifica.TempoPercorrenza = 1000;
                }

                //Ricerca dell'ispettore
                gruppoverifica.IDStatoPianificazioneIspettore = 1;

                string securityCode = string.Empty;
                QueryString qs = new QueryString();
                qs.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
                qs.Add("IDIspettore", ispettore.IDSoggetto.ToString());
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                securityCode = qsEncrypted.ToString();
                gruppoverifica.SecurityCode = securityCode;

                listaIspezioneIspettore.Add(gruppoverifica);
            }

            var listaIspezioneIspettoreOrdinata = listaIspezioneIspettore.OrderBy(a => a.Distanza).ToList();

            //Secondo giro per calcolare il punteggio in graduatoria
            //la fascia A è ordinata per Numero visite ultimo anno
            //var ispettoriFasciaA = GetIspettoriConMenoVerifiche(listaIspezioneIspettoreOrdinata, numeroIspettoriFasciaA);
            //var punteggio = 1;
            //foreach (var ispettore in ispettoriFasciaA)
            //{
            //    ispettore.PunteggioGraduatoria = punteggio++;
            //}
            //punteggio = 10;
            //la fascia B è ordinata per distanza
            //var elencoPerDistanza = listaIspezioneIspettoreOrdinata.Where(i => ispettoriFasciaA.All(ia => ia.IDIspettore != i.IDIspettore)).OrderBy(i => i.Distanza);
            foreach (var ispettore in listaIspezioneIspettoreOrdinata)
            {
                ispettore.PunteggioGraduatoria = punteggio++;
            }

            ctx.VER_IspezioneGruppoVerifica.AddRange(listaIspezioneIspettoreOrdinata); //La lista deve essere salvata con l'ordine definitivo
            ctx.SaveChanges();
        }

        public static int GetNumeroVerificheIspettoreUltimoPeriodo(COM_AnagraficaSoggetti ispettore, int lunghezzaPeriodo)
        {
            //var elencoStatiVerificheIspettore = new List<int>() { 2, 3, 4, 5 };
            var dataLimite = DateTime.Today.AddMonths(-1 * lunghezzaPeriodo);

            int NrVerificheIspettore = 0;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var CountIspettore = ispettore.VER_IspezioneGruppoVerifica.Where(c => c.fInGruppoVerifica && c.DataAccettazione > dataLimite).ToList();

                foreach (var item in CountIspettore)
                {
                    var datiIspettore = ctx.VER_Ispezione.Where(c => c.IDIspezioneVisita == item.IDIspezioneVisita && c.IDIspettore == item.IDIspettore && c.IDStatoIspezione != 1).ToList();

                    if (datiIspettore.Count() > 0)
                    {
                        NrVerificheIspettore++;
                    }
                    //NrVerificheIspettore = datiIspettore.Count();
                }
            }
            return NrVerificheIspettore;

            //return ispettore.VER_IspezioneGruppoVerifica.Count(c => c.fInGruppoVerifica //&& elencoStatiVerificheIspettore.Contains(c.VER_Ispezione.IDStatoIspezione) 
            //&& c.DataAccettazione > dataLimite);
        }

        public static void RicercaIspettoriPerVerifica(CriterDataModel ctx, long iDIspezioneVisita)
        {
            int numeroIspettoriFasciaA = int.Parse(ConfigurationManager.AppSettings["NumeroIspettoriFasciaA"]);
            int lunghezzaPeriodoConteggioVerifiche = int.Parse(ConfigurationManager.AppSettings["LunghezzaPeriodoConteggioVerificheMesi"]);
            var punteggio = 0;

            //Calcola per ogni ispettore i dati che mi serviranno per poter poi decidere quale chiamare (es. distanza) per ogni ispettore
            var listaIspettoriAbilitati = ctx.COM_AnagraficaSoggetti.Where(c => c.fAttivo == true
                                                                               && c.IDTipoSoggetto == 7
                                                                               && c.COM_AnagraficaSoggettiAccreditamento.IDStatoAccreditamento == 4
                                                                               && c.COM_AnagraficaSoggettiAccreditamento.fAttivo == true
                                                                               && (c.COM_ContrattoIspettore.FirstOrDefault().fAttivo == true
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().IDStatoContratto == 2
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().NumeroIspezioniMax > c.VER_Ispezione.Count(a => a.IDIspettore == c.IDSoggetto && a.IDContrattoIspettore == c.COM_ContrattoIspettore.FirstOrDefault().IDContrattoIspettore)
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().DataInizioContratto <= DateTime.Now
                                                                               && c.COM_ContrattoIspettore.FirstOrDefault().DataFineContratto >= DateTime.Now
                                                                               )
                                                                          ).ToList();


            var listaIspezioneIspettore = new List<VER_IspezioneGruppoVerifica>(listaIspettoriAbilitati.Count);

            foreach (var ispettore in listaIspettoriAbilitati)
            {
                //tutte le ispezioni in cui ha partecipato, che poi si sono concluse o in corso
                int numeroVerificheEffettuate = GetNumeroVerificheIspettoreUltimoPeriodo(ispettore, lunghezzaPeriodoConteggioVerifiche);

                var infoPercorso = GetInfoPercorsoIspettoreImpianto(ispettore, iDIspezioneVisita);

                var gruppoverifica = new VER_IspezioneGruppoVerifica();
                gruppoverifica.COM_AnagraficaSoggetti = ispettore;
                gruppoverifica.IDIspezioneVisita = iDIspezioneVisita;
                gruppoverifica.NumeroVerificheEffettuate = numeroVerificheEffettuate;
                gruppoverifica.PunteggioGraduatoria = 0;
                if (infoPercorso.Item1)
                {
                    //percorso individuato
                    gruppoverifica.Distanza = infoPercorso.Item2;
                    gruppoverifica.TempoPercorrenza = infoPercorso.Item3;
                }
                else
                {
                    //Se Google non trova l'indirizzo metto distanza 1000
                    //Se l'indirizzo dell'edificio non viene trovato tutti gli ispettori prendono il max
                    gruppoverifica.Distanza = 1000;
                    gruppoverifica.TempoPercorrenza = 1000;
                }

                //Ricerca dell'ispettore
                gruppoverifica.IDStatoPianificazioneIspettore = 1;

                string securityCode = string.Empty;
                QueryString qs = new QueryString();
                qs.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
                qs.Add("IDIspettore", ispettore.IDSoggetto.ToString());
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                securityCode = qsEncrypted.ToString();
                gruppoverifica.SecurityCode = securityCode;

                listaIspezioneIspettore.Add(gruppoverifica);
            }

            //Secondo giro per calcolare il punteggio in graduatoria la fascia A è ordinata per Numero visite ultimo anno
            var elencoIspettoriFasciaAConMenoVerifiche = GetIspettoriConMenoVerifiche(listaIspezioneIspettore, numeroIspettoriFasciaA);
            punteggio = 1;
            foreach (var ispettore in elencoIspettoriFasciaAConMenoVerifiche)
            {
                ispettore.PunteggioGraduatoria = punteggio++;
            }
            
            //la fascia B è ordinata per distanza
            var elencoIspettoriFasciaBPerDistanza = listaIspezioneIspettore.Except(elencoIspettoriFasciaAConMenoVerifiche).OrderBy(i => i.Distanza).ToList();
            foreach (var ispettore in elencoIspettoriFasciaBPerDistanza)
            {
                ispettore.PunteggioGraduatoria = GetPunteggioIspettore(ispettore.NumeroVerificheEffettuate, ispettore.Distanza);
            }

            var gruppoVerificaOrdinatoPerPunteggio = elencoIspettoriFasciaAConMenoVerifiche.Concat(elencoIspettoriFasciaBPerDistanza).OrderBy(c => c.PunteggioGraduatoria);
            ctx.VER_IspezioneGruppoVerifica.AddRange(gruppoVerificaOrdinatoPerPunteggio); //La lista deve essere salvata con l'ordine definitivo
            ctx.SaveChanges();
        }

        public static int? GetPunteggioIspettore(int? numeroIspezioniIspettore, int? distanzaIspettoreCertificatore)
        {
            int? punteggio = (numeroIspezioniIspettore * 15) + (distanzaIspettoreCertificatore * 2);

            return punteggio;
        }


        public static void CreaGruppoDiVerifica(long iDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                RicercaIspettoriPerVerifica(ctx, iDIspezioneVisita);
                ChiediDisponibilitaProssimoIspettore(iDIspezioneVisita);
            }
        }
        #endregion
        public static List<V_VER_IspezioniStato> GetIspezioneStorico(long iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var query = ctx.V_VER_IspezioniStato.AsQueryable();
                query = query.Where(c => c.IDIspezione == iDIspezione);

                return query.OrderByDescending(c => c.Data).ToList();
            }
        }

        public static void SaveIspezione(long iDIspezione,
                                         DateTime? dataIspezione,
                                         string note,
                                         DateTime? dataIspezioneRipianificazione,
                                         bool fIspezioneNonSvolta,
                                         bool fIspezioneNonSvolta2,
                                         bool IsPagamentoMai1,
                                         bool IsPagamentoMai2,
                                         string notaCoordinatore,
                                         bool IsIspezioneSvolta,
                                         int? IDSvolgimentoIspezione,
                                         string AltroSvolgimentoIspezione
                                         )
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                if (dataIspezione != null)
                {
                    ispezione.DataIspezione = dataIspezione;
                }
                if (!string.IsNullOrEmpty(note))
                {
                    ispezione.Note = note;
                }
                if (dataIspezioneRipianificazione != null)
                {
                    ispezione.DataIspezioneRipianificazione = dataIspezioneRipianificazione;
                }
                ispezione.fIspezioneNonSvolta = fIspezioneNonSvolta;
                ispezione.fIspezioneNonSvolta2 = fIspezioneNonSvolta2;
                ispezione.IsPagamentoMai1 = IsPagamentoMai1;
                ispezione.IsPagamentoMai2 = IsPagamentoMai2;
                //if (!string.IsNullOrEmpty(descrizioneIspezione))
                //{
                //    ispezione.DescrizioneIspezione = descrizioneIspezione;
                //}
                if (!string.IsNullOrEmpty(notaCoordinatore))
                {
                    ispezione.NotaCoordinatore = notaCoordinatore;
                }
                ispezione.IsIspezioneSvolta = IsIspezioneSvolta;
                ispezione.IDSvolgimentoIspezione = IDSvolgimentoIspezione != 0 ? IDSvolgimentoIspezione : null;
                if (!string.IsNullOrEmpty(AltroSvolgimentoIspezione))
                {
                    ispezione.AltroSvolgimentoIspezione = AltroSvolgimentoIspezione;
                }

                ctx.SaveChanges();
            }
        }

        public static void SaveDataIspezioneRewrite(long iDIspezione, DateTime? dataIspezioneRipianificazione, int? iDOrarioDa, int? iDOrarioA)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                if (ispezione != null)
                {
                    ispezione.DataIspezione = dataIspezioneRipianificazione;
                    ispezione.IDOrarioDa = iDOrarioDa;
                    ispezione.IDOrarioA = iDOrarioA;

                    ctx.SaveChanges();
                }
            }
        }

        public static void SaveIspezioneDettaglioPecTerzoResponsabile(long IDIspezione, bool IsAvvisoPecTerzoResponsabile, string DettagliAvvisoPecTerzoResponsabile)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == IDIspezione).FirstOrDefault();
                if (ispezione != null)
                {
                    ispezione.IsAvvisoPecTerzoResponsabile = IsAvvisoPecTerzoResponsabile;
                    ispezione.DettagliAvvisoPecTerzoResponsabile = DettagliAvvisoPecTerzoResponsabile;
                    
                    ctx.SaveChanges();
                }
            }
        }

        //public static string GetSqlValoriIspezioniFilter(
        //    object iDIspettore,
        //    object iDIspezioneVisita,
        //    object iDStatoIspezione,
        //    object DataIspezioneDal, object DataIspezioneAl,
        //    object codiceTargaturaImpianto,
        //    object codiceIspezione,
        //    object IspezioniNonSvolte,
        //    object iDProgrammaIspezione
        //    )
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT ");
        //    strSql.Append(" * ");
        //    strSql.Append(" FROM VER_IspezioneVisita ");
        //    strSql.Append(" WHERE IDIspezioneVisita IN ( SELECT DISTINCT IDIspezioneVisita FROM V_VER_Ispezioni WHERE fAttivo=1");

        //    if (!string.IsNullOrEmpty(iDIspezioneVisita.ToString()))
        //    {
        //        strSql.Append(" AND IDIspezioneVisita=" + iDIspezioneVisita.ToString());
        //    }

        //    if ((iDIspettore != "") && (iDIspettore != "-1") && (iDIspettore != null))
        //    {
        //        strSql.Append(" AND IDIspettore=" + iDIspettore);
        //    }

        //    if ((iDProgrammaIspezione != "") && (iDProgrammaIspezione != "-1") && (iDProgrammaIspezione != null))
        //    {
        //        strSql.Append(" AND IDProgrammaIspezione=" + iDProgrammaIspezione);
        //    }

        //    if (iDStatoIspezione.ToString() != "0")
        //    {
        //        strSql.Append(" AND IDStatoIspezione=");
        //        strSql.Append(iDStatoIspezione);
        //    }

        //    if (codiceTargaturaImpianto.ToString() != "")
        //    {
        //        strSql.Append(" AND CodiceTargatura = ");
        //        strSql.Append("'");
        //        strSql.Append(codiceTargaturaImpianto);
        //        strSql.Append("'");
        //    }

        //    if (codiceIspezione.ToString() != "")
        //    {
        //        strSql.Append(" AND CodiceIspezione = ");
        //        strSql.Append("'");
        //        strSql.Append(codiceIspezione);
        //        strSql.Append("'");
        //    }

        //    if ((DataIspezioneDal != null) && (DataIspezioneDal.ToString() != ""))
        //    {
        //        strSql.Append(" AND convert(varchar(10), DataIspezione, 126) >= '");
        //        DateTime dataIspezioneDa = DateTime.Parse(DataIspezioneDal.ToString());
        //        string newDataIspezioneDa = dataIspezioneDa.ToString("yyyy") + "-" + dataIspezioneDa.ToString("MM") + "-" + dataIspezioneDa.ToString("dd");
        //        strSql.Append(newDataIspezioneDa);
        //        strSql.Append("'");
        //    }

        //    if ((DataIspezioneAl != null) && (DataIspezioneAl.ToString() != ""))
        //    {
        //        strSql.Append(" AND convert(varchar(10), DataIspezione, 126) <= '");
        //        DateTime dataIspezioneAl = DateTime.Parse(DataIspezioneAl.ToString());
        //        string newdataIspezioneAl = dataIspezioneAl.ToString("yyyy") + "-" + dataIspezioneAl.ToString("MM") + "-" + dataIspezioneAl.ToString("dd");
        //        strSql.Append(newdataIspezioneAl);
        //        strSql.Append("'");
        //    }

        //    switch (IspezioniNonSvolte)
        //    {
        //        case 0: //Tutte le ispezioni

        //            break;
        //        case 1: //Ispezioni non svolte la prima volta
        //            strSql.Append(" AND fIspezioneNonSvolta = 1");
        //            break;
        //        case 2://Ispezioni non svolte la seconda volta
        //            strSql.Append(" AND fIspezioneNonSvolta2 = 1");
        //            break;
        //    }

        //    strSql.Append(" ) ORDER BY IDIspezioneVisita DESC");

        //    return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        //}

        public static List<V_VER_Ispezioni> GetListIspezioni(object iDIspettore,
                                                             object iDIspezioneVisita,
                                                             object iDStatoIspezione,
                                                             object DataIspezioneDal, object DataIspezioneAl,
                                                             object codiceTargaturaImpianto,
                                                             object codiceIspezione,
                                                             object IspezioniNonSvolte,
                                                             object iDProgrammaIspezione,
                                                             object[] valoriCriticitaSelected)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezioni = ctx.V_VER_Ispezioni.AsNoTracking().AsQueryable();
                if ((iDIspettore != "") && (iDIspettore != "-1") && (iDIspettore != null))
                {
                    int iDIspettoreParse = int.Parse(iDIspettore.ToString());
                    ispezioni = ispezioni.Where(a => a.IDIspettore == iDIspettoreParse);
                }
                if (!string.IsNullOrEmpty(iDIspezioneVisita.ToString()))
                {
                    long iDIspezioneVisitaParse = long.Parse(iDIspezioneVisita.ToString());
                    ispezioni = ispezioni.Where(a => a.IDIspezioneVisita == iDIspezioneVisitaParse);
                }
                if (iDStatoIspezione.ToString() != "0")
                {
                    long iDStatoIspezioneParse = long.Parse(iDStatoIspezione.ToString());
                    ispezioni = ispezioni.Where(a => a.IDStatoIspezione == iDStatoIspezioneParse);
                }
                if (codiceTargaturaImpianto.ToString() != "")
                {
                    string codiceTargaturaImpiantoParse = codiceTargaturaImpianto.ToString();
                    ispezioni = ispezioni.Where(a => a.CodiceTargatura == codiceTargaturaImpiantoParse);
                }
                if (codiceIspezione.ToString() != "")
                {
                    string codiceIspezioneParse = codiceIspezione.ToString();
                    ispezioni = ispezioni.Where(a => a.CodiceIspezione == codiceIspezioneParse);
                }
                if ((iDProgrammaIspezione != "") && (iDProgrammaIspezione != "-1") && (iDProgrammaIspezione != null))
                {
                    int iDProgrammaIspezioneParse = int.Parse(iDProgrammaIspezione.ToString());
                    ispezioni = ispezioni.Where(a => a.IDProgrammaIspezione == iDProgrammaIspezioneParse);
                }
                switch (IspezioniNonSvolte.ToString())
                {
                    case "0": //Tutte le ispezioni

                        break;
                    case "1": //Ispezioni non svolte la prima volta
                        ispezioni = ispezioni.Where(a => a.fIspezioneNonSvolta == true);
                        break;
                    case "2"://Ispezioni non svolte la seconda volta
                        ispezioni = ispezioni.Where(a => a.fIspezioneNonSvolta2 == true);
                        break;
                }

                if ((DataIspezioneDal != null) && (DataIspezioneDal.ToString() != ""))
                {
                    DateTime dataIspezioneDa = DateTime.Parse(DataIspezioneDal.ToString());
                    ispezioni = ispezioni.Where(a => a.DataIspezione >= dataIspezioneDa);
                }

                if ((DataIspezioneAl != null) && (DataIspezioneAl.ToString() != ""))
                {
                    DateTime dataIspezioneAl = DateTime.Parse(DataIspezioneAl.ToString());
                    ispezioni = ispezioni.Where(a => a.DataIspezione <= dataIspezioneAl);
                }

                if (valoriCriticitaSelected != null)
                {
                    for (int i = 0; i < valoriCriticitaSelected.Length; i++)
                    {
                        if (valoriCriticitaSelected[i].ToString() == "0") //Osservazioni
                        {
                            ispezioni = ispezioni.Where(a => a.Osservazioni != null);
                        }
                        else if (valoriCriticitaSelected[i].ToString() == "1") //Raccomandazioni
                        {
                            ispezioni = ispezioni.Where(a => a.Raccomandazioni != null);
                        }
                        else if (valoriCriticitaSelected[i].ToString() == "2") //Prescrizioni
                        {
                            ispezioni = ispezioni.Where(a => a.Prescrizioni != null);
                        }
                        else if (valoriCriticitaSelected[i].ToString() == "3") //Impianto può funzionare
                        {
                            ispezioni = ispezioni.Where(a => a.fImpiantoPuoFunzionare == false);
                        }
                    }
                }

                return ispezioni.OrderBy(c => c.CodiceIspezione.ToString()).ToList();
            }
        }

        public static List<V_VER_Ispezioni> GetIspezioni(long iDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.V_VER_Ispezioni.Where(a => a.IDIspezioneVisita == iDIspezioneVisita).OrderBy(c => c.CodiceIspezione.ToString()).ToList();

                return result;
            }
        }

        public static List<V_VER_Ispezioni> GetIspezioniPrecedenti(int? IDTargaturaImpianto, long IDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.V_VER_Ispezioni.Where(a => a.IDTargaturaImpianto == IDTargaturaImpianto
                                                            && (a.IDStatoIspezione == 10 //Ispezione Conclusa da Coordinatore con doppio mancato accesso (avviso non recapitato)
                                                                || a.IDStatoIspezione == 11 //Ispezione Conclusa da Coordinatore con utente sconosciuto
                                                                || a.IDStatoIspezione == 5 //Ispezione Conclusa da Coordinatore con accertamento
                                                                || a.IDStatoIspezione == 9 //Ispezione Conclusa da Coordinatore senza accertamento
                                                                )

                && a.IDIspezioneVisita != IDIspezioneVisita).OrderBy(c => c.CodiceIspezione.ToString()).ToList();

                return result;
            }
        }

        public static void SaveNotificaIspettoreAperturaIspezione(long IDIspezione, string NotificaAdIspettore, int IDUtente)
        {
            using (var ctx = new CriterDataModel())
            {
                var notifica = new VER_IspezioneNotificaRiaperturaIspezione();
                notifica.IDIspezione = IDIspezione;
                notifica.NotificaAdIspettore = !string.IsNullOrEmpty(NotificaAdIspettore) ? NotificaAdIspettore : null;
                notifica.DataNotifica = DateTime.Now;
                notifica.IDUtenteUltimaModifica = IDUtente;
                ctx.VER_IspezioneNotificaRiaperturaIspezione.Add(notifica);
                ctx.SaveChanges();
            }
        }

        public static List<V_VER_IspezioneNotificaRiaperturaIspezione> GetIspezioneNotificaRiaperturaIspezione(int iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var notifica = ctx.V_VER_IspezioneNotificaRiaperturaIspezione.AsNoTracking().AsQueryable();
                notifica = notifica.Where(a => a.IDIspezione == iDIspezione);

                return notifica.OrderBy(c => c.DataNotifica).ToList();
            }
        }

        public static List<V_VER_IspezioneDocumento> GetIspezioniDocumenti(int iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var documenti = ctx.V_VER_IspezioneDocumento.AsNoTracking().AsQueryable();
                documenti = documenti.Where(a => a.IDIspezione == iDIspezione);
                documenti = documenti.Where(a => a.fAttivo == true);

                return documenti.OrderBy(c => c.IDTipoDocumentoIspezione.ToString()).ToList();
            }
        }

        public static void SetObbligatorietaIspezioneDocumenti(long iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(a => a.IDIspezione == iDIspezione).FirstOrDefault();
                bool fIspezioneNonSvolta = ispezione.fIspezioneNonSvolta;
                bool fIspezioneNonSvolta2 = ispezione.fIspezioneNonSvolta2;
                bool IsIspezioneSvolta = ispezione.IsIspezioneSvolta;
                int? IDSvolgimentoIspezione = ispezione.IDSvolgimentoIspezione;

                var documentiIspezione = ctx.VER_IspezioneDocumento.Where(a => a.IDIspezione == iDIspezione).ToList();

                #region Documento Comunicazione di mancata ispezione 1
                var documentoMancataIspezione1 = documentiIspezione.Where(a => a.IDTipoDocumentoIspezione == 6).FirstOrDefault();
                if (fIspezioneNonSvolta) //Doppia MAI
                {
                    documentoMancataIspezione1.fDocumentoObbligatorio = true;
                }
                else
                {
                    documentoMancataIspezione1.fDocumentoObbligatorio = false;
                }
                ctx.SaveChanges();
                #endregion

                #region Documento Comunicazione di mancata ispezione 2
                var documentoMancataIspezione2 = documentiIspezione.Where(a => a.IDTipoDocumentoIspezione == 7).FirstOrDefault();
                if (fIspezioneNonSvolta2)
                {
                    documentoMancataIspezione2.fDocumentoObbligatorio = true;
                }
                else
                {
                    documentoMancataIspezione2.fDocumentoObbligatorio = false;
                }
                ctx.SaveChanges();
                #endregion

                #region Documento Dichiarazione disattivazione impianto/generatore
                var documentDichiarazioneDisattivazioneGeneratore = documentiIspezione.Where(a => a.IDTipoDocumentoIspezione == 10).FirstOrDefault();
                if (!IsIspezioneSvolta && IDSvolgimentoIspezione==5) //Generatore Disattivato
                {
                    documentDichiarazioneDisattivazioneGeneratore.fDocumentoObbligatorio = true;
                }
                else
                {
                    documentDichiarazioneDisattivazioneGeneratore.fDocumentoObbligatorio = false;
                }
                ctx.SaveChanges();
                #endregion 

                #region Documento Relazione miglioramento energetico
                var documentRelazioneMiglioramentoEnergetico = documentiIspezione.Where(a => a.IDTipoDocumentoIspezione == 4).FirstOrDefault();
                if (IsIspezioneSvolta || (!IsIspezioneSvolta && IDSvolgimentoIspezione == 5)) //Generatore Disattivato
                {
                    documentRelazioneMiglioramentoEnergetico.fDocumentoObbligatorio = true;
                }
                else
                {
                    documentRelazioneMiglioramentoEnergetico.fDocumentoObbligatorio = false;
                }
                ctx.SaveChanges();
                #endregion

                #region Rapporto Ispezione
                var documentRapportoIspezione = documentiIspezione.Where(a => a.IDTipoDocumentoIspezione == 8).FirstOrDefault();
                if (IsIspezioneSvolta || (!IsIspezioneSvolta && IDSvolgimentoIspezione == 7)) //Doppia MAI
                {
                    documentRapportoIspezione.fDocumentoObbligatorio = true;
                }
                else
                {
                    documentRapportoIspezione.fDocumentoObbligatorio = false;
                }
                ctx.SaveChanges();
                #endregion
            }
        }

        public static bool IspettoreAccessoPaginaConfermaIspezione(long? IDIspezioneVisita, long? IDIspettore)
        {
            bool puoEntrare = true;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var datiIspettore = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita && c.IDIspettore == IDIspettore).FirstOrDefault();

                if (datiIspettore.IDStatoPianificazioneIspettore != 2)
                {
                    puoEntrare = false;
                }
            }

            return puoEntrare;
        }

        public static bool IspettoreAccessoPaginaConfermaIncarico(long? IDIspezioneVisita, long? IDIspettore)
        {
            bool puoEntrare = true;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var datiIspettore = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita && c.IDIspettore == IDIspettore).FirstOrDefault();

                if (datiIspettore.fIncaricoFirmato)
                {
                    puoEntrare = false;
                }
            }

            return puoEntrare;
        }

        public static void LetteraIncaricoFirmata(long IDIspezioneVisita)
        {
            using (var ctx = new CriterDataModel())
            {
                var datiIspettore = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita && c.fInGruppoVerifica == true).FirstOrDefault();
                datiIspettore.fIncaricoFirmato = true;
                datiIspettore.DataFirmaIncarico = DateTime.Now;
                ctx.SaveChanges();
            }

            CambiaStatoIspezioneMassivo(IDIspezioneVisita, 2, null, null, null, null);
        }

        public static DateTime? GetDataFirmaIncarico(long IDIspezioneVisita)
        {
            DateTime? DataFirmaIncarico = null;

            using (var ctx = new CriterDataModel())
            {
                var datiIspettore = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita && c.fInGruppoVerifica == true).FirstOrDefault();
                if (datiIspettore != null)
                {
                    DataFirmaIncarico = datiIspettore.DataFirmaIncarico;
                }                  
            }

            return DataFirmaIncarico;
        }

        public static string GetSecurityCodeFromGruppoVerifica(long IDIspezioneVisita)
        {
            string securityCode = string.Empty;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var datiIspettore = ctx.VER_IspezioneGruppoVerifica.Where(c => c.IDIspezioneVisita == IDIspezioneVisita && c.fInGruppoVerifica == true).FirstOrDefault();
                securityCode = datiIspettore.SecurityCode;
            }

            return securityCode;
        }

        public static long GetIDIspezioneVisitaFromVerifica(long iDIspezione)
        {
            long iDIspezioneVisita = 0;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                if (ispezione != null)
                {
                    iDIspezioneVisita = ispezione.IDIspezioneVisita;
                }
            }

            return iDIspezioneVisita;
        }

        public static string GetCodiceIspezioneFromIDIspezione(long iDIspezione)
        {
            string codiceIspezione = string.Empty;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                if (ispezione != null)
                {
                    codiceIspezione = ispezione.CodiceIspezione;
                }
            }

            return codiceIspezione;
        }

        public static void SetFieldAccessoriIspezione(long iDIspezione, string Osservatore, bool fIspezionePagamento, decimal? costoIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                if (!string.IsNullOrEmpty(Osservatore))
                {
                    ispezione.Osservatore = Osservatore;
                }
                else
                {
                    ispezione.Osservatore = null;
                }
                ispezione.fIspezionePagamento = fIspezionePagamento;

                if (costoIspezione != null)
                {
                    ispezione.CostoIspezione = costoIspezione;
                }
                else
                {
                    ispezione.CostoIspezione = null;
                }

                ctx.SaveChanges();
            }
        }

        public static void SetfRipianificazioneIspezione(long iDIspezione, bool fIspezioneRipianificazione)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                ispezione.fIspezioneRipianificata = fIspezioneRipianificazione;
                ctx.SaveChanges();
            }
        }

        public static void StoricizzaDataIspezionePrecedente(long iDIspezione)
        {
            using (var ctx = new CriterDataModel())
            {
                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
                ispezione.DataIspezionePrecedente = ispezione.DataIspezione;
                ispezione.IDOrarioDaPrecedente = ispezione.IDOrarioDa;
                ispezione.IDOrarioAPrecedente = ispezione.IDOrarioA;
                ctx.SaveChanges();
            }
        }


        public static void RefreshLibrettiAttiviInVisiteIspettive()
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                #region Aggiornamento Libretti con fAttivo=false e generatori in visite ispettive
                var librettiNonAttiviInVisite = (from LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti
                                                 join VER_IspezioneVisitaInfo in ctx.VER_IspezioneVisitaInfo on LIM_LibrettiImpianti.IDLibrettoImpianto equals VER_IspezioneVisitaInfo.IDLibrettoImpianto
                                                 join LIM_LibrettiImpiantiGruppiTermici in ctx.LIM_LibrettiImpiantiGruppiTermici on new { iDLibrettoImpiantoGruppoTermico = (int)VER_IspezioneVisitaInfo.IDLibrettoImpiantoGruppoTermico } equals new { iDLibrettoImpiantoGruppoTermico = LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico }
                                                 where
                                                   VER_IspezioneVisitaInfo.fInIspezione == false &&
                                                   LIM_LibrettiImpianti.fAttivo == false
                                                 select new
                                                 {
                                                     VER_IspezioneVisitaInfo.IDIspezioneVisitaInfo,
                                                     LIM_LibrettiImpianti.IDTargaturaImpianto,
                                                     VER_IspezioneVisitaInfo.IDLibrettoImpianto,
                                                     iDLibrettoImpiantoGruppoTermico = VER_IspezioneVisitaInfo.IDLibrettoImpiantoGruppoTermico,
                                                     LIM_LibrettiImpiantiGruppiTermici.Prefisso,
                                                     LIM_LibrettiImpiantiGruppiTermici.CodiceProgressivo
                                                 }
                                        ).ToList();

                if (librettiNonAttiviInVisite != null)
                {
                    foreach (var libNonAttivi in librettiNonAttiviInVisite)
                    {
                        var librettoAttivo = ctx.LIM_LibrettiImpianti.Where(c => c.IDTargaturaImpianto == libNonAttivi.IDTargaturaImpianto && c.fAttivo == true).FirstOrDefault();
                        if (librettoAttivo != null)
                        {
                            switch (librettoAttivo.IDStatoLibrettoImpianto)
                            {
                                case 1: //Libretto in bozza
                                case 3: //Libretto revisionato
                                    //Non aggiorno aspetto al prossimo giro che il libretto diventa definitivo
                                    break;
                                case 2: //Libretto definitivo
                                    var gruppoTermicoAttivo = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpianto == librettoAttivo.IDLibrettoImpianto
                                                                                                       && c.Prefisso == libNonAttivi.Prefisso
                                                                                                       && c.CodiceProgressivo == libNonAttivi.CodiceProgressivo
                                                                                                       && c.fAttivo == true).FirstOrDefault();

                                    if (gruppoTermicoAttivo != null)
                                    {
                                        var visita = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisitaInfo == libNonAttivi.IDIspezioneVisitaInfo).FirstOrDefault();

                                        if (!gruppoTermicoAttivo.fDismesso)
                                        {
                                            //Ricavo i dati del generatore nella visita
                                            var gruppoTermicoNonAttivo = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpiantoGruppoTermico == libNonAttivi.iDLibrettoImpiantoGruppoTermico).FirstOrDefault();

                                            if (gruppoTermicoNonAttivo != null)
                                            {
                                                //https://documentation.help/Compare-Net-Objects/
                                                //Devo verificare se sono cambiati i dati del generatore
                                                CompareLogic gtCompare = new CompareLogic();
                                                gtCompare.Config.CompareChildren = false;
                                                gtCompare.Config.MaxDifferences = 100;

                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.IDUtenteInserimento");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.DataInserimento");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoInserimento");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.IDUtenteUltimaModifica");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.DataUltimaModifica");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.CombustibileAltro");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.FluidoTermovettoreAltro");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.AnalisiFumoPrevisteNr");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.DataDismissione");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.fAttivo");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.fDismesso");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.DataDismesso");
                                                gtCompare.Config.MembersToIgnore.Add("LIM_LibrettiImpiantiGruppiTermici.IDUtenteDismesso");

                                                ComparisonResult resultCompare = gtCompare.Compare(gruppoTermicoNonAttivo, gruppoTermicoAttivo);

                                                if (resultCompare.AreEqual)
                                                {
                                                    //Se sono uaguali allora FINALMENTE aggiorno il libretto e il generatore in visite ispettive
                                                    visita.IDLibrettoImpianto = librettoAttivo.IDLibrettoImpianto;
                                                    visita.IDLibrettoImpiantoGruppoTermico = gruppoTermicoAttivo.IDLibrettoImpiantoGruppoTermico;
                                                }
                                                else
                                                {
                                                    //Se non sono uguali allora do indicazione della variazione 
                                                    //facendo decidere manualmente se cancellare o meno
                                                    //il generatore dalla visita
                                                    visita.fGeneratoreModificato = true;
                                                    visita.GeneratoreModificatoDettagli = resultCompare.DifferencesString;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Invio Email per dismissione generatore
                                            var visitaIspettivaDismissione = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisitaInfo == libNonAttivi.IDIspezioneVisitaInfo).FirstOrDefault();
                                            var gruppoTermicoInVisitaDismissione = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpiantoGruppoTermico == libNonAttivi.iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                                            string motivoRimozioneDismissione = "DismissioneGeneratore";
                                            EmailNotify.SendMailPerBackOffice_NotificaRimozioneGeneratoreDaVisita(visitaIspettivaDismissione, gruppoTermicoInVisitaDismissione, motivoRimozioneDismissione);

                                            //ctx.VER_IspezioneVisitaInfo.Remove(visita);
                                        }
                                        ctx.SaveChanges();
                                    }
                                    break;
                                case 4: //Libretto annullato
                                    //Invio email di annullamento libretto 
                                    var visitaIspettivaAnnullamento = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisitaInfo == libNonAttivi.IDIspezioneVisitaInfo).FirstOrDefault();
                                    var gruppoTermicoInVisitaAnnullamento = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpiantoGruppoTermico == libNonAttivi.iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                                    string motivoRimozioneAnnullamento = "AnnullamentoLibretto";
                                    EmailNotify.SendMailPerBackOffice_NotificaRimozioneGeneratoreDaVisita(visitaIspettivaAnnullamento, gruppoTermicoInVisitaAnnullamento, motivoRimozioneAnnullamento);
                                    //ctx.VER_IspezioneVisitaInfo.Remove(visitaIspettiva);
                                    //ctx.SaveChanges();
                                    break;
                            }
                        }
                    }
                }
                #endregion

                #region Aggiornamento Libretti con fAttivo=false e generatori in programma ispezione
                var librettiNonAttiviInProgrammaIspezione = (from VER_ProgrammaIspezioneInfo in ctx.VER_ProgrammaIspezioneInfo
                                                             join LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti on new { iDLibrettoImpianto = (int)VER_ProgrammaIspezioneInfo.IDLibrettoImpianto } equals new { iDLibrettoImpianto = LIM_LibrettiImpianti.IDLibrettoImpianto }
                                                             join LIM_LibrettiImpiantiGruppiTermici in ctx.LIM_LibrettiImpiantiGruppiTermici on new { iDLibrettoImpiantoGruppoTermico = (int)VER_ProgrammaIspezioneInfo.IDLibrettoImpiantoGruppoTermico } equals new { iDLibrettoImpiantoGruppoTermico = LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico }
                                                             where
                                                               LIM_LibrettiImpianti.fAttivo == false
                                                             select new
                                                             {
                                                                 VER_ProgrammaIspezioneInfo.IDProgrammaIspezioneInfo,
                                                                 VER_ProgrammaIspezioneInfo.IDTargaturaImpianto,
                                                                 iDLibrettoImpianto = VER_ProgrammaIspezioneInfo.IDLibrettoImpianto,
                                                                 LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico,
                                                                 LIM_LibrettiImpiantiGruppiTermici.Prefisso,
                                                                 LIM_LibrettiImpiantiGruppiTermici.CodiceProgressivo
                                                             }
                                                             ).ToList();

                if (librettiNonAttiviInProgrammaIspezione != null)
                {
                    foreach (var libNonAttivi in librettiNonAttiviInProgrammaIspezione)
                    {
                        var librettoAttivo = ctx.LIM_LibrettiImpianti.Where(c => c.IDTargaturaImpianto == libNonAttivi.IDTargaturaImpianto && c.fAttivo == true).FirstOrDefault();
                        if (librettoAttivo != null)
                        {
                            switch (librettoAttivo.IDStatoLibrettoImpianto)
                            {
                                case 1: //Libretto in bozza
                                case 3: //Libretto revisionato
                                    //Non aggiorno aspetto al prossimo giro che il libretto diventa definitivo
                                    break;
                                case 2: //Libretto definitivo
                                    var gruppoTermicoAttivo = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpianto == librettoAttivo.IDLibrettoImpianto
                                                                                                       && c.Prefisso == libNonAttivi.Prefisso
                                                                                                       && c.CodiceProgressivo == libNonAttivi.CodiceProgressivo
                                                                                                       && c.fAttivo == true).FirstOrDefault();

                                    if (gruppoTermicoAttivo != null)
                                    {
                                        var programmaIspezione = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDProgrammaIspezioneInfo == libNonAttivi.IDProgrammaIspezioneInfo).FirstOrDefault();

                                        if (!gruppoTermicoAttivo.fDismesso)
                                        {
                                            //Aggiorno il libretto e il generatore in programma ispezione
                                            programmaIspezione.IDLibrettoImpianto = librettoAttivo.IDLibrettoImpianto;
                                            programmaIspezione.IDLibrettoImpiantoGruppoTermico = gruppoTermicoAttivo.IDLibrettoImpiantoGruppoTermico;
                                        }
                                        else
                                        {
                                            //Invio già notifica di dismissione generatore
                                            //ctx.VER_ProgrammaIspezioneInfo.Remove(programmaIspezione);
                                        }
                                        ctx.SaveChanges();
                                    }
                                    break;
                                case 4: //Libretto annullato
                                        //Invio già notifica di annullamento generatore

                                    //OLD
                                    //Lo rimuovo dal programma ispezione
                                    //var programmaIsp = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDProgrammaIspezioneInfo == libNonAttivi.IDProgrammaIspezioneInfo).FirstOrDefault();
                                    //ctx.VER_ProgrammaIspezioneInfo.Remove(programmaIsp);
                                    //ctx.SaveChanges();
                                    break;
                            }
                        }
                    }
                }
                #endregion

                #region Elimino gli eventuali libretti/generatori duplicati
                //try
                //{

                //    var generatoriDuplicati = (from VER_ProgrammaIspezioneInfo in ctx.VER_ProgrammaIspezioneInfo
                //                               group VER_ProgrammaIspezioneInfo by new
                //                               {
                //                                   VER_ProgrammaIspezioneInfo.IDTargaturaImpianto,
                //                                   VER_ProgrammaIspezioneInfo.IDLibrettoImpiantoGruppoTermico,
                //                                   VER_ProgrammaIspezioneInfo.IDLibrettoImpianto,
                //                                   VER_ProgrammaIspezioneInfo.IDAccertamento,
                //                                   VER_ProgrammaIspezioneInfo.IDTipoIspezione
                //                               } into g
                //                               where g.Count() > 1
                //                               select new
                //                               {
                //                                   g.Key.IDTargaturaImpianto,
                //                                   g.Key.IDLibrettoImpiantoGruppoTermico,
                //                                   g.Key.IDLibrettoImpianto,
                //                                   g.Key.IDAccertamento,
                //                                   g.Key.IDTipoIspezione
                //                               }).ToList();

                //    foreach (var item in generatoriDuplicati)
                //    {

                //        var programmaIspezioneInfo = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDTargaturaImpianto == item.IDTargaturaImpianto &&
                //                                                                           c.IDLibrettoImpianto == item.IDLibrettoImpianto &&
                //                                                                           c.IDLibrettoImpiantoGruppoTermico == item.IDLibrettoImpiantoGruppoTermico
                //                                                                           ).FirstOrDefault();
                //        ctx.VER_ProgrammaIspezioneInfo.Remove(programmaIspezioneInfo);
                //        ctx.SaveChanges();
                //    }
                //}
                //catch (Exception ex)
                //{

                //}
                #endregion

            }
        }

        public static string GetDescriptionOfLibrettoModificatoInVisiteIspettive(object generatoreModificatoDettagli)
        {
            string description = string.Empty;
            if (generatoreModificatoDettagli != null)
            {
                description = generatoreModificatoDettagli.ToString();
            }
            else
            {
                description = "Nessuna modifica al generatore";
            }

            return description;
        }

        public static bool AlertPianificazioneIspettori(long iDIspezioneVisita)
        {
            bool fAlertPianificazione = true;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var datiIspettore = (from VER_Ispezione in ctx.VER_Ispezione
                                     join VER_IspezioneGruppoVerifica in ctx.VER_IspezioneGruppoVerifica
                                           on new { VER_Ispezione.IDIspezioneVisita, IDIspettore = (int)VER_Ispezione.IDIspettore }
                                       equals new { VER_IspezioneGruppoVerifica.IDIspezioneVisita, IDIspettore = VER_IspezioneGruppoVerifica.IDIspettore }
                                     where
                                       VER_IspezioneGruppoVerifica.IDStatoPianificazioneIspettore == 3 &&
                                       VER_Ispezione.IDStatoIspezione == 6
                                     select new
                                     {
                                         VER_IspezioneGruppoVerifica.DataFirmaIncarico,
                                         VER_IspezioneGruppoVerifica.IDIspezioneGruppoVerifica
                                     }).Distinct().FirstOrDefault();

                if (datiIspettore != null)
                {
                    if (datiIspettore.DataFirmaIncarico != null)
                    {
                        double intervalloPianificazione = double.Parse(ConfigurationManager.AppSettings["IntervalloNoPianificazioneIspezioneMinuti"]);
                        var data = DateTime.Parse(datiIspettore.DataFirmaIncarico.ToString()).AddMinutes(intervalloPianificazione);
                        if (DateTime.Now > data)
                        {
                            fAlertPianificazione = false;
                        }
                    }
                }
            }

            return fAlertPianificazione;
        }

        public static void RimandaInRicercaIspettoreNoFirmaLAI()
        {
            //Viene rimesso in ricerca ispettore tutte le ispezioni in stato "Ispezione Confermata da Inviare LAI" la cui data odierna > di data accettazione + 24 ore
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezioni = (from VER_Ispezione in ctx.VER_Ispezione
                                 join VER_IspezioneGruppoVerifica in ctx.VER_IspezioneGruppoVerifica on VER_Ispezione.IDIspezioneVisita equals VER_IspezioneGruppoVerifica.IDIspezioneVisita
                                 where
                                   VER_Ispezione.IDStatoIspezione == 6 &&
                                   VER_IspezioneGruppoVerifica.IDStatoPianificazioneIspettore == 3
                                 select new
                                 {
                                     VER_Ispezione.IDIspezioneVisita,
                                     VER_IspezioneGruppoVerifica.DataAccettazione
                                 }).Distinct().ToList();

                foreach (var ispezione in ispezioni)
                {
                    bool visitaCheck = ctx.VER_Ispezione.Where(a => a.IDIspezioneVisita == ispezione.IDIspezioneVisita && a.IDStatoIspezione !=6).Any();
                    if (!visitaCheck)
                    {
                        double intervalloNoFirmaLai = double.Parse(ConfigurationManager.AppSettings["IntervalloNoFirmaLaiIspezioneMinuti"]);
                        var data = DateTime.Parse(ispezione.DataAccettazione.ToString()).AddMinutes(intervalloNoFirmaLai);
                        if (DateTime.Now > data)
                        {
                            UtilityVerifiche.IspezioneRimandaInRicercaIspettoreNoFirmaLAI(ispezione.IDIspezioneVisita);
                        }
                    }                    
                }
            }
        }

        public static void RimandaInRicercaIspettoreNoPianificazione()
        {
            //Viene rimesso in ricerca ispettore tutte le ispezioni in stato "Ispezione da Pianificare" la cui data odierna > di data firma incarico + 72 ore
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezioni = (from VER_Ispezione in ctx.VER_Ispezione
                                 join VER_IspezioneGruppoVerifica in ctx.VER_IspezioneGruppoVerifica on VER_Ispezione.IDIspezioneVisita equals VER_IspezioneGruppoVerifica.IDIspezioneVisita
                                 where
                                   VER_Ispezione.IDStatoIspezione == 2 &&
                                   VER_IspezioneGruppoVerifica.IDStatoPianificazioneIspettore == 3
                                 select new
                                 {
                                     VER_Ispezione.IDIspezioneVisita,
                                     VER_IspezioneGruppoVerifica.DataFirmaIncarico
                                 }).Distinct().ToList();

                foreach (var ispezione in ispezioni)
                {
                    if (ispezione.DataFirmaIncarico != null) //Devo gestire il pregresso dove non avevo la data firma Incarico
                    {
                        //Fix 26-09-2022: Se di tutte quelle da pianificare l'ispettore ha almeno pianificato una ispezione (oppure una delle ispezioni del pacchetto ha uno stato diverso da 2=Da Pianificare) non deve essere rimesso in ricerca ispettore
                        var checkAlmenoUnaIspezionePianificata = ctx.VER_Ispezione.Where(a => (a.IDStatoIspezione == 3 //Ispezione Pianificata in attesa di conferma
                                                                                            || a.IDStatoIspezione == 4 //Ispezione Conclusa da Ispettore
                                                                                            || a.IDStatoIspezione == 5 //Ispezione Conclusa da Coordinatore con accertamento
                                                                                            || a.IDStatoIspezione == 7 //Ispezione Pianificata confermata
                                                                                            || a.IDStatoIspezione == 8 //Ispezione Annullata
                                                                                            || a.IDStatoIspezione == 9 //Ispezione Conclusa da Coordinatore senza accertamento
                                                                                            )        
                                                                                              && a.IDIspezioneVisita == ispezione.IDIspezioneVisita).Any();
                        if (!checkAlmenoUnaIspezionePianificata)
                        {
                            double intervalloNoPianificazione = double.Parse(ConfigurationManager.AppSettings["IntervalloNoPianificazioneIspezioneMinuti"]);
                            var data = DateTime.Parse(ispezione.DataFirmaIncarico.ToString()).AddMinutes(intervalloNoPianificazione);
                            if (DateTime.Now > data)
                            {
                                UtilityVerifiche.IspezioneRimandaInRicercaIspettoreNoPianificazione(ispezione.IDIspezioneVisita);
                            }
                        }
                    }
                }

            }
        }

        public static void DeleteIspezione(long iDIspezione)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var ispezioneInfo = ctx.V_VER_Ispezioni.Where(a => a.IDIspezione == iDIspezione).FirstOrDefault();
                long iDIspezioneVisitaInfo = ispezioneInfo.IDIspezioneVisitaInfo;
                long iDIspezioneVisita = ispezioneInfo.IDIspezioneVisita;
                int iDLibrettoImpianto = ispezioneInfo.IDLibrettoImpianto;
                int iDProgrammaIspezione = ispezioneInfo.IDProgrammaIspezione;
                int iDLibrettoImpiantoGruppoTermico = (int)ispezioneInfo.IDLibrettoImpiantoGruppoTermico;

                var ispezione = ctx.VER_Ispezione.Where(a => a.IDIspezione == iDIspezione).FirstOrDefault();
                ctx.VER_Ispezione.Remove(ispezione);
                ctx.SaveChanges();
                
                UtilityVerifiche.InsertDeleteGeneratoreNelleVisiteIspettive("Delete", iDIspezioneVisitaInfo, iDIspezioneVisita, iDLibrettoImpianto, iDProgrammaIspezione, iDLibrettoImpiantoGruppoTermico, null, null);
                
            }
        }

        public static void NotificaIspettoreMancataChiusuraIspezione()
        {
            //Se ci sono delle ispezioni in stato “Ispezione Pianificata confermata” il sistema dopo 48 h
            //dalla data di pianificazione ispezione invia una notifica via pec all’ispettore
            double DaysNotificaMancataChiusuraIspezione = Convert.ToDouble(ConfigurationManager.AppSettings["DaysNotificaMancataChiusuraIspezione"]);
            bool usaPec = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);

            using (CriterDataModel ctx = new CriterDataModel())
            {
                string sqlQueryIspezioniNotifiche = "SELECT * FROM VER_Ispezione WHERE (IDStatoIspezione = 7) AND (CONVERT(varchar(10), DATEADD(day, " + DaysNotificaMancataChiusuraIspezione + ", DataIspezione), 120) = CONVERT(varchar(10), GETDATE(), 120)) ";
                var ispezioni = ctx.VER_Ispezione.SqlQuery(sqlQueryIspezioniNotifiche).ToList();

                foreach (var ispezione in ispezioni)
                {
                    EmailNotify.SendMailPerIspettore_MancataChiusuraIspezione(ispezione.IDIspezione, usaPec);
                }
            }
        }

        #endregion

        #region Ispezioni in Accertamento
        public static bool SottoponiIspezioneAdAccertamento(long? iDRapportoControlloTecnico, int iDTipoAccertamento, long? iDIspezione)
        {
            bool fAccertamento = SottoponiAdAccertamento(iDRapportoControlloTecnico, iDTipoAccertamento, iDIspezione);

            return fAccertamento;
        }
        #endregion

        #region Sanzioni
        public static string GetSqlValoriSanzioniFilter(
           object IDIspettore,
           object CodiceIspezione,
           object CodiceAccertamento,
           object CodiceTargaturaImpianto,
           object iDStatoAccertamentoSanzione
           )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" V_VER_Accertamenti.*, VER_Ispezione.IDIspettore, VER_Ispezione.CodiceIspezione, dbo.COM_AnagraficaSoggetti.Nome + ' ' + dbo.COM_AnagraficaSoggetti.Cognome AS Ispettore ");
            strSql.Append(" FROM COM_AnagraficaSoggetti RIGHT OUTER JOIN ");
            strSql.Append(" VER_Ispezione ON COM_AnagraficaSoggetti.IDSoggetto = VER_Ispezione.IDIspettore RIGHT OUTER JOIN ");
            strSql.Append(" V_VER_Accertamenti ON VER_Ispezione.IDIspezione = V_VER_Accertamenti.IDIspezione ");
            strSql.Append(" WHERE IDStatoAccertamentoIntervento=7 ");
            if (iDStatoAccertamentoSanzione.ToString() != "0")
            {
                strSql.Append("AND IDStatoAccertamentoSanzione=" + iDStatoAccertamentoSanzione.ToString() + " ");
            }
            //else
            //{
            //    strSql.Append("AND IDStatoAccertamentoSanzione IS NOT NULL ");
            //}

            if ((IDIspettore != "") && (IDIspettore != "-1") && (IDIspettore != null))
            {
                strSql.Append("AND IDIspettore = " + IDIspettore + "");
            }

            if (!string.IsNullOrEmpty(CodiceAccertamento.ToString()))
            {
                strSql.Append(" AND CodiceAccertamento = ");
                strSql.Append("'");
                strSql.Append(CodiceAccertamento);
                strSql.Append("'");
            }

            if (!string.IsNullOrEmpty(CodiceIspezione.ToString()))
            {
                strSql.Append(" AND CodiceIspezione = ");
                strSql.Append("'");
                strSql.Append(CodiceIspezione);
                strSql.Append("'");
            }


            if (!string.IsNullOrEmpty(CodiceTargaturaImpianto.ToString()))
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(CodiceTargaturaImpianto);
                strSql.Append("'");
            }

            strSql.Append(" ORDER BY IDAccertamento DESC");

            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public static void SaveSanzione(long iDAccertamento,
                                        DateTime? dataRicevutaPagamento,
                                        string noteSanzione,
                                        bool isSanzioneRevocata,
                                        string MotivoSanzioneRevocata
                                        )
        {
            using (var ctx = new CriterDataModel())
            {
                var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                if (!string.IsNullOrEmpty(noteSanzione))
                {
                    sanzione.NoteSanzione = noteSanzione;
                }
                else
                {
                    sanzione.NoteSanzione = null;
                }
                sanzione.DataRicevutaPagamento = dataRicevutaPagamento;
                
                sanzione.IsSanzioneRevocata = isSanzioneRevocata;
                if (!string.IsNullOrEmpty(MotivoSanzioneRevocata))
                {
                    sanzione.MotivoSanzioneRevocata = MotivoSanzioneRevocata;
                }
                else
                {
                    sanzione.MotivoSanzioneRevocata = null;
                }

                ctx.SaveChanges();
            }
        }

        public static string SetCodiceSanzione(long IDAccertamento)
        {
            string CodiceSanzione = string.Empty;

            using (var ctx = new CriterDataModel())
            {
                //int ProgressivoCodiceSanzione = ctx.VER_Accertamento.Where(c => c.IDStatoAccertamentoIntervento == 7 && CodiceSanzione !=null && c.IDAccertamento != IDAccertamento).Select(p => (int?)p.CodiceSanzione)).DefaultIfEmpty(0).Max();
                List<int> ListOfCode = new List<int>() { 0 };
                var sanzioni = ctx.VER_Accertamento.Where(c => c.IDStatoAccertamentoIntervento == 7 && c.CodiceSanzione != null && c.IDAccertamento != IDAccertamento).Select(p => p.CodiceSanzione).ToList();
                foreach (var sanzione in sanzioni)
                {
                    int r = 0;
                    if (int.TryParse(sanzione, out r))
                    {
                        ListOfCode.Add(r);
                    }                    
                }

                int ProgressivoCodiceSanzione = ListOfCode.Max() + 1;
                CodiceSanzione = string.Format("{0:00000}", ProgressivoCodiceSanzione);

                var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == IDAccertamento).FirstOrDefault();
                accertamento.CodiceSanzione = CodiceSanzione;
                ctx.SaveChanges();
            }

            return CodiceSanzione;
        }


        public static void CambiaStatoSanzione(long iDAccertamento, int iDStatoAccertamentoSanzione)
        {
            using (var ctx = new CriterDataModel())
            {
                var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                sanzione.IDStatoAccertamentoSanzione = iDStatoAccertamentoSanzione;

                try
                {
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

                }
            }
        }

        public static string[] GetProvenienzaSanzione(long iDAccertamento)
        {
            string[] outVal = new string[2];
            using (var ctx = new CriterDataModel())
            {
                var accertamento = ctx.VER_Accertamento.Where(a => a.IDAccertamento == iDAccertamento).FirstOrDefault();
                if (accertamento != null)
                {
                    if (accertamento.IDTipoAccertamento == 1)
                    {
                        outVal[0] = "VER_Interventi.aspx";
                        outVal[1] = "SANZIONE PROVENIENTE DA GESTIONE INTERVENTI";
                    }
                    else if (accertamento.IDTipoAccertamento == 2)
                    {
                        if (accertamento.IDStatoAccertamentoIntervento != null)
                        {
                            outVal[0] = "VER_Interventi.aspx";
                            outVal[1] = "SANZIONE PROVENIENTE DA GESTIONE INTERVENTI";
                        }
                        else
                        {
                            outVal[0] = "VER_Accertamenti.aspx";
                            outVal[1] = "SANZIONE PROVENIENTE DA GESTIONE ACCERTAMENTI";
                        }
                    }
                }
            }

            return outVal;
        }

        public static void SetDataInvioSanzione(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                sanzione.DataInvioSanzione = DateTime.Now;
                ctx.SaveChanges();
            }
        }

        public static void SetDataRicevimentoRaccomandataSanzione(long iDAccertamento, string dataRecapito)
        {
            using (var ctx = new CriterDataModel())
            {
                DateTime dataRecapitoI;
                if (DateTime.TryParse(dataRecapito, out dataRecapitoI))
                {
                    var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                    sanzione.DataRicezioneSanzione = dataRecapitoI;
                    ctx.SaveChanges();
                }
            }
        }

        public static void SetDataScadenzaSanzione(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                sanzione.DataScadenzaSanzione = DateTime.Parse(sanzione.DataRicezioneSanzione.ToString()).AddDays(int.Parse(ConfigurationManager.AppSettings["DaysScadenzaSanzione"]));  // UtilityApp.AddWorkingDays(DateTime.Parse(sanzione.DataRicezioneSanzione.ToString()), int.Parse(ConfigurationManager.AppSettings["DaysScadenzaSanzione"]));
                ctx.SaveChanges();
            }
        }

        public static void SetDataScadenzaPagamentoRidottoSanzione(long iDAccertamento)
        {
            using (var ctx = new CriterDataModel())
            {
                var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
                sanzione.DataScadenzaPagamentoRidottoSanzione = DateTime.Parse(sanzione.DataRicezioneSanzione.ToString()).AddDays(int.Parse(ConfigurationManager.AppSettings["DaysScadenzaPagamentoRidottoSanzione"]));// UtilityApp.AddWorkingDays(DateTime.Parse(sanzione.DataRicezioneSanzione.ToString()), int.Parse(ConfigurationManager.AppSettings["DaysScadenzaPagamentoRidottoSanzione"]));
                ctx.SaveChanges();
            }
        }

        #endregion

        #region Questionari Ispezioni
        public static void CreateIfNotExistQuestionariIspezioni(long IDIspezione, int IDUtente)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var check = ctx.VER_IspezioneQuestinarioQualita.Where(a => a.IDIspezione == IDIspezione).FirstOrDefault();

                if (check == null)
                {
                    var questionario = new VER_IspezioneQuestinarioQualita();
                    questionario.IDIspezione = IDIspezione;
                    questionario.IndiceTempisticheConclusioneVerifica = null;
                    questionario.IndiceCompletezzaRvi = null;
                    questionario.IndiceReclami = null;
                    questionario.IspezioneNonEffettuataOption = null;
                    questionario.IsIspezioneEffettuataNoAnalisi = false;
                    questionario.IsIspezioneEffettuataRitardoConsegnaRvi = false;
                    questionario.IsIspezioneEffettuataRitardoComunicazione = false;
                    questionario.IsIspezioneEffettuataRitardoAppuntamento = false;
                    questionario.IsIspezioneEffettuataMancataDocumentazione = false;
                    questionario.IsIspezioneNonEffettuataMAI1 = false;
                    questionario.IsIspezioneNonEffettuataMAI2 = false;
                    questionario.CostoFinale = null;
                    questionario.TrimestreFatturazione = null;
                    questionario.IsDefinitivo = false;
                    questionario.DataUltimaModifica = DateTime.Now;
                    questionario.IDUtenteUltimaModifica = IDUtente;

                    ctx.VER_IspezioneQuestinarioQualita.Add(questionario);
                    ctx.SaveChanges();
                }
            }
        }


        public static void UpdateQuestionariIspezioni(long IDIspezione, 
                                                      int? IndiceTempisticheConclusioneVerifica, 
                                                      int? IndiceCompletezzaRvi,
                                                      int? IndiceReclami,
                                                      int? IspezioneNonEffettuataOption,
                                                      bool IsIspezioneEffettuataNoAnalisi,
                                                      bool IsIspezioneEffettuataRitardoConsegnaRvi,
                                                      bool IsIspezioneEffettuataRitardoComunicazione,
                                                      bool IsIspezioneEffettuataRitardoAppuntamento,
                                                      bool IsIspezioneEffettuataMancataDocumentazione,
                                                      bool IsIspezioneNonEffettuataMAI1,
                                                      bool IsIspezioneNonEffettuataMAI2,
                                                      decimal? CostoFinale,
                                                      int? TrimestreFatturazione,
                                                      int IDUtente
                                                     )
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var questionarioUpdate = ctx.VER_IspezioneQuestinarioQualita.Where(a => a.IDIspezione == IDIspezione).FirstOrDefault();

                if (questionarioUpdate != null)
                {
                    questionarioUpdate.IndiceTempisticheConclusioneVerifica = IndiceTempisticheConclusioneVerifica;
                    questionarioUpdate.IndiceCompletezzaRvi = IndiceCompletezzaRvi;
                    questionarioUpdate.IndiceReclami = IndiceReclami;
                    questionarioUpdate.IspezioneNonEffettuataOption = IspezioneNonEffettuataOption;
                    questionarioUpdate.IsIspezioneEffettuataNoAnalisi = IsIspezioneEffettuataNoAnalisi;
                    questionarioUpdate.IsIspezioneEffettuataRitardoConsegnaRvi = IsIspezioneEffettuataRitardoConsegnaRvi;
                    questionarioUpdate.IsIspezioneEffettuataRitardoComunicazione = IsIspezioneEffettuataRitardoComunicazione;
                    questionarioUpdate.IsIspezioneEffettuataRitardoAppuntamento = IsIspezioneEffettuataRitardoAppuntamento;
                    questionarioUpdate.IsIspezioneEffettuataMancataDocumentazione = IsIspezioneEffettuataMancataDocumentazione;
                    questionarioUpdate.IsIspezioneNonEffettuataMAI1 = IsIspezioneNonEffettuataMAI1;
                    questionarioUpdate.IsIspezioneNonEffettuataMAI2 = IsIspezioneNonEffettuataMAI2;
                    questionarioUpdate.CostoFinale = CostoFinale;
                    questionarioUpdate.TrimestreFatturazione = TrimestreFatturazione;
                    questionarioUpdate.DataUltimaModifica = DateTime.Now;
                    questionarioUpdate.IDUtenteUltimaModifica = IDUtente;

                    ctx.SaveChanges();
                }                
            }
        }

        public static void SetQuestionarioBozzaDefinitivo(long IDIspezione, bool IsDefinitivo)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var questionarioUpdate = ctx.VER_IspezioneQuestinarioQualita.Where(a => a.IDIspezione == IDIspezione).FirstOrDefault();
                if (questionarioUpdate != null)
                {
                    questionarioUpdate.IsDefinitivo = IsDefinitivo;
                    ctx.SaveChanges();
                }
            }
        }
        
        #endregion

    }
}