using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Web;
using System.Collections;
using Z.EntityFramework.Plus;
using System.Data.Entity.SqlServer;
using Bender.Extensions;
using System.Configuration;
using System.Data.Entity.Core.Objects.DataClasses;

namespace DataUtilityCore
{
    public class UtilityLibrettiImpianti
    {
        public static int SaveInsertDeleteDatiLibrettiImpianti(
                string operationType,
                string IDLibrettoImpianto,
                string IDSoggetto,
                string IDSoggettoDerived,
                string IDTargaturaImpianto,
                string IDStatoLibrettoImpianto,
                string IDTipologiaIntervento,
                string DataIntervento,
                string Indirizzo,
                string Civico,
                string Palazzo,
                string Scala,
                string Interno,
                string IDCodiceCatastale,
                string fUnitaImmobiliare,
                string IDDestinazioneUso,
                string VolumeLordoRiscaldato,
                string VolumeLordoRaffrescato,
                string NumeroAPE,
                string NumeroPDR,
                string NumeroPOD,
                string fAcs,
                string PotenzaAcs,
                string fClimatizzazioneEstiva,
                string PotenzaClimatizzazioneEstiva,
                string fClimatizzazioneInvernale,
                string PotenzaClimatizzazioneInvernale,
                string fClimatizzazioneAltro,
                string ClimatizzazioneAltro,
                string TipologiaFluidoVettoreAltro,
                string TipologiaGeneratoriAltro,
                string fPannelliSolariTermici,
                string SuperficieTotaleSolariTermici,
                string PotenzaSolariTermici,
                string fPannelliSolariTermiciAltro,
                string PannelliSolariTermiciAltro,
                string fPannelliSolariClimatizzazioneAcs,
                string fPannelliSolariClimatizzazioneEstiva,
                string fPannelliSolariClimatizzazioneInvernale,
                string ContenutoAcquaImpianto,
                string DurezzaTotaleAcquaImpianto,
                string fTrattamentoAcquaInvernale,
                string DurezzaTotaleAcquaImpiantoInvernale,
                string IDTipologiaProtezioneGelo,
                string fTrattamentoAcquaAcs,
                string DurezzaTotaleAcquaAcs,
                string PercentualeGlicole,
                string PhGlicole,
                string fTrattamentoAcquaEstiva,
                string IDTipologiaCircuitoRaffreddamento,
                string IDTipologiaAcquaAlimento,
                string fSistemaSpurgoAutomatico,
                string ConducibilitaAcquaIngresso,
                string ConducibilitaInizioSpurgo,
                string IDUtenteInserimento,
                string DataInserimento,
                string IDUtenteUltimaModifica,
                string DataUltimaModifica,
                string IDLibrettoImpiantoRevisione,
                string NumeroRevisione,
                string DataRevisione,
                string DataAnnullamento,
                string fAttivo)
        {
            int IDLibrettoImpiantoInserted = 0;

            SqlConnection conn = BuildConnection.GetConn();

            SqlCommand tipoComando = null;
            if (operationType == "INSERT")
            {
                tipoComando = new SqlCommand("INSERT INTO LIM_LibrettiImpianti ("
                    + "IDSoggetto, "
                    + "IDSoggettoDerived, "
                    + "IDTargaturaImpianto, "
                    + "IDStatoLibrettoImpianto, "
                    + "IDTipologiaIntervento, "
                    + "DataIntervento, "
                    + "Indirizzo, "
                    + "Civico, "
                    + "Palazzo, "
                    + "Scala, "
                    + "Interno, "
                    + "IDCodiceCatastale, "
                    + "fUnitaImmobiliare, "
                    + "IDDestinazioneUso, "
                    + "VolumeLordoRiscaldato, "
                    + "VolumeLordoRaffrescato, "
                    + "NumeroAPE, "
                    + "NumeroPDR, "
                    + "NumeroPOD, "
                    + "fAcs, "
                    + "PotenzaAcs, "
                    + "fClimatizzazioneEstiva, "
                    + "PotenzaClimatizzazioneEstiva, "
                    + "fClimatizzazioneInvernale, "
                    + "PotenzaClimatizzazioneInvernale, "
                    + "fClimatizzazioneAltro, "
                    + "ClimatizzazioneAltro, "
                    + "TipologiaFluidoVettoreAltro, "
                    + "TipologiaGeneratoriAltro, "
                    + "fPannelliSolariTermici, "
                    + "SuperficieTotaleSolariTermici, "
                    + "PotenzaSolariTermici, "
                    + "fPannelliSolariTermiciAltro, "
                    + "PannelliSolariTermiciAltro, "
                    + "fPannelliSolariClimatizzazioneAcs, "
                    + "fPannelliSolariClimatizzazioneEstiva, "
                    + "fPannelliSolariClimatizzazioneInvernale, "
                    + "ContenutoAcquaImpianto, "
                    + "DurezzaTotaleAcquaImpianto, "
                    + "fTrattamentoAcquaInvernale, "
                    + "DurezzaTotaleAcquaImpiantoInvernale, "
                    + "IDTipologiaProtezioneGelo, "
                    + "fTrattamentoAcquaAcs, "
                    + "DurezzaTotaleAcquaAcs, "
                    + "PercentualeGlicole, "
                    + "PhGlicole, "
                    + "fTrattamentoAcquaEstiva, "
                    + "IDTipologiaCircuitoRaffreddamento, "
                    + "IDTipologiaAcquaAlimento, "
                    + "fSistemaSpurgoAutomatico, "
                    + "ConducibilitaAcquaIngresso, "
                    + "ConducibilitaInizioSpurgo, "
                    + "IDUtenteInserimento, "
                    + "DataInserimento, "
                    + "IDUtenteUltimaModifica, "
                    + "DataUltimaModifica, "
                    + "IDLibrettoImpiantoRevisione, "
                    + "NumeroRevisione, "
                    + "DataRevisione, "
                    + "DataAnnullamento, "
                    + "fAttivo "
                    + "  ) OUTPUT INSERTED.IDLibrettoImpianto VALUES ( "
                    + "@IDSoggetto, "
                    + "@IDSoggettoDerived, "
                    + "@IDTargaturaImpianto, "
                    + "@IDStatoLibrettoImpianto, "
                    + "@IDTipologiaIntervento, "
                    + "@DataIntervento, "
                    + "@Indirizzo, "
                    + "@Civico, "
                    + "@Palazzo, "
                    + "@Scala, "
                    + "@Interno, "
                    + "@IDCodiceCatastale, "
                    + "@fUnitaImmobiliare, "
                    + "@IDDestinazioneUso, "
                    + "@VolumeLordoRiscaldato, "
                    + "@VolumeLordoRaffrescato, "
                    + "@NumeroAPE, "
                    + "@NumeroPDR, "
                    + "@NumeroPOD, "
                    + "@fAcs, "
                    + "@PotenzaAcs, "
                    + "@fClimatizzazioneEstiva, "
                    + "@PotenzaClimatizzazioneEstiva, "
                    + "@fClimatizzazioneInvernale, "
                    + "@PotenzaClimatizzazioneInvernale, "
                    + "@fClimatizzazioneAltro, "
                    + "@ClimatizzazioneAltro, "
                    + "@TipologiaFluidoVettoreAltro, "
                    + "@TipologiaGeneratoriAltro, "
                    + "@fPannelliSolariTermici, "
                    + "@SuperficieTotaleSolariTermici, "
                    + "@PotenzaSolariTermici, "
                    + "@fPannelliSolariTermiciAltro, "
                    + "@PannelliSolariTermiciAltro, "
                    + "@fPannelliSolariClimatizzazioneAcs, "
                    + "@fPannelliSolariClimatizzazioneEstiva, "
                    + "@fPannelliSolariClimatizzazioneInvernale, "
                    + "@ContenutoAcquaImpianto, "
                    + "@DurezzaTotaleAcquaImpianto, "
                    + "@fTrattamentoAcquaInvernale, "
                    + "@DurezzaTotaleAcquaImpiantoInvernale, "
                    + "@IDTipologiaProtezioneGelo, "
                    + "@fTrattamentoAcquaAcs, "
                    + "@DurezzaTotaleAcquaAcs, "
                    + "@PercentualeGlicole, "
                    + "@PhGlicole, "
                    + "@fTrattamentoAcquaEstiva, "
                    + "@IDTipologiaCircuitoRaffreddamento, "
                    + "@IDTipologiaAcquaAlimento, "
                    + "@fSistemaSpurgoAutomatico, "
                    + "@ConducibilitaAcquaIngresso, "
                    + "@ConducibilitaInizioSpurgo, "
                    + "@IDUtenteInserimento, "
                    + "@DataInserimento, "
                    + "@IDUtenteUltimaModifica, "
                    + "@DataUltimaModifica, "
                    + "@IDLibrettoImpiantoRevisione, "
                    + "@NumeroRevisione, "
                    + "@DataRevisione, "
                    + "@DataAnnullamento, "
                    + "@fAttivo "
                    + " ) ", conn);
            }
            else if (operationType == "UPDATE")
            {
                tipoComando = new SqlCommand("UPDATE LIM_LibrettiImpianti SET "
                    + "IDSoggetto=@IDSoggetto, "
                    + "IDSoggettoDerived=@IDSoggettoDerived, "
                    + "IDTargaturaImpianto=@IDTargaturaImpianto, "
                    + "IDStatoLibrettoImpianto=@IDStatoLibrettoImpianto, "
                    + "IDTipologiaIntervento=@IDTipologiaIntervento, "
                    + "DataIntervento=@DataIntervento, "
                    + "Indirizzo=@Indirizzo, "
                    + "Civico=@Civico, "
                    + "Palazzo=@Palazzo, "
                    + "Scala=@Scala, "
                    + "Interno=@Interno, "
                    + "IDCodiceCatastale=@IDCodiceCatastale, "
                    + "fUnitaImmobiliare=@fUnitaImmobiliare, "
                    + "IDDestinazioneUso=@IDDestinazioneUso, "
                    + "VolumeLordoRiscaldato=@VolumeLordoRiscaldato, "
                    + "VolumeLordoRaffrescato=@VolumeLordoRaffrescato, "
                    + "NumeroAPE=@NumeroAPE, "
                    + "NumeroPDR=@NumeroPDR, "
                    + "NumeroPOD=@NumeroPOD, "
                    + "fAcs=@fAcs, "
                    + "PotenzaAcs=@PotenzaAcs, "
                    + "fClimatizzazioneEstiva=@fClimatizzazioneEstiva, "
                    + "PotenzaClimatizzazioneEstiva=@PotenzaClimatizzazioneEstiva, "
                    + "fClimatizzazioneInvernale=@fClimatizzazioneInvernale, "
                    + "PotenzaClimatizzazioneInvernale=@PotenzaClimatizzazioneInvernale, "
                    + "fClimatizzazioneAltro=@fClimatizzazioneAltro, "
                    + "ClimatizzazioneAltro=@ClimatizzazioneAltro, "
                    + "TipologiaFluidoVettoreAltro=@TipologiaFluidoVettoreAltro, "
                    + "TipologiaGeneratoriAltro=@TipologiaGeneratoriAltro, "
                    + "fPannelliSolariTermici=@fPannelliSolariTermici, "
                    + "SuperficieTotaleSolariTermici=@SuperficieTotaleSolariTermici, "
                    + "PotenzaSolariTermici=@PotenzaSolariTermici, "
                    + "fPannelliSolariTermiciAltro=@fPannelliSolariTermiciAltro, "
                    + "PannelliSolariTermiciAltro=@PannelliSolariTermiciAltro, "
                    + "fPannelliSolariClimatizzazioneAcs=@fPannelliSolariClimatizzazioneAcs, "
                    + "fPannelliSolariClimatizzazioneEstiva=@fPannelliSolariClimatizzazioneEstiva, "
                    + "fPannelliSolariClimatizzazioneInvernale=@fPannelliSolariClimatizzazioneInvernale, "
                    + "ContenutoAcquaImpianto=@ContenutoAcquaImpianto, "
                    + "DurezzaTotaleAcquaImpianto=@DurezzaTotaleAcquaImpianto, "
                    + "fTrattamentoAcquaInvernale=@fTrattamentoAcquaInvernale, "
                    + "DurezzaTotaleAcquaImpiantoInvernale=@DurezzaTotaleAcquaImpiantoInvernale, "
                    + "IDTipologiaProtezioneGelo=@IDTipologiaProtezioneGelo, "
                    + "fTrattamentoAcquaAcs=@fTrattamentoAcquaAcs, "
                    + "DurezzaTotaleAcquaAcs=@DurezzaTotaleAcquaAcs, "
                    + "PercentualeGlicole=@PercentualeGlicole, "
                    + "PhGlicole=@PhGlicole, "
                    + "fTrattamentoAcquaEstiva=@fTrattamentoAcquaEstiva, "
                    + "IDTipologiaCircuitoRaffreddamento=@IDTipologiaCircuitoRaffreddamento, "
                    + "IDTipologiaAcquaAlimento=@IDTipologiaAcquaAlimento, "
                    + "fSistemaSpurgoAutomatico=@fSistemaSpurgoAutomatico, "
                    + "ConducibilitaAcquaIngresso=@ConducibilitaAcquaIngresso, "
                    + "ConducibilitaInizioSpurgo=@ConducibilitaInizioSpurgo, "
                    + "IDUtenteInserimento=@IDUtenteInserimento, "
                    + "DataInserimento=@DataInserimento, "
                    + "IDUtenteUltimaModifica=@IDUtenteUltimaModifica, "
                    + "DataUltimaModifica=@DataUltimaModifica, "
                    + "IDLibrettoImpiantoRevisione=@IDLibrettoImpiantoRevisione, "
                    + "NumeroRevisione=@NumeroRevisione, "
                    + "DataRevisione=@DataRevisione, "
                    + "DataAnnullamento=@DataAnnullamento, "
                    + "fAttivo=@fAttivo "
                  + " WHERE IDLibrettoImpianto=@IDLibrettoImpianto ", conn);
            }
            else if (operationType == "DELETE")
            {
                tipoComando = new SqlCommand("DELETE FROM LIM_LibrettiImpianti "
                  + " WHERE IDLibrettoImpianto=@IDLibrettoImpianto ", conn);
            }

            tipoComando.CommandType = CommandType.Text;

            if ((operationType == "UPDATE") || (operationType == "DELETE"))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDLibrettoImpianto", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDLibrettoImpianto"].Value = IDLibrettoImpianto;
            }

            if (IDSoggetto != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDSoggetto", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDSoggetto"].Value = IDSoggetto;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDSoggetto", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDSoggetto"].Value = DBNull.Value;
            }

            if (IDSoggettoDerived != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDSoggettoDerived", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDSoggettoDerived"].Value = IDSoggettoDerived;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDSoggettoDerived", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDSoggettoDerived"].Value = DBNull.Value;
            }

            if ((IDTargaturaImpianto != "0") && (IDTargaturaImpianto != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTargaturaImpianto", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTargaturaImpianto"].Value = IDTargaturaImpianto;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTargaturaImpianto", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTargaturaImpianto"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@IDStatoLibrettoImpianto", SqlDbType.Int, 4));
            tipoComando.Parameters["@IDStatoLibrettoImpianto"].Value = IDStatoLibrettoImpianto;

            if ((IDTipologiaIntervento != "0") && (IDTipologiaIntervento != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaIntervento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaIntervento"].Value = IDTipologiaIntervento;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaIntervento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaIntervento"].Value = DBNull.Value;
            }

            if (DataIntervento != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataIntervento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataIntervento"].Value = DataIntervento;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataIntervento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataIntervento"].Value = DBNull.Value;
            }

            if (Indirizzo != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Indirizzo", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@Indirizzo"].Value = Indirizzo;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Indirizzo", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@Indirizzo"].Value = DBNull.Value;
            }

            if (Civico != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Civico", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Civico"].Value = Civico;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Civico", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Civico"].Value = DBNull.Value;
            }

            if (Palazzo != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Palazzo", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Palazzo"].Value = Palazzo;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Palazzo", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Palazzo"].Value = DBNull.Value;
            }

            if (Scala != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Scala", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Scala"].Value = Scala;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Scala", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Scala"].Value = DBNull.Value;
            }

            if (Interno != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Interno", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Interno"].Value = Interno;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Interno", SqlDbType.NVarChar, 10));
                tipoComando.Parameters["@Interno"].Value = DBNull.Value;
            }

            if ((IDCodiceCatastale != "0") && (IDCodiceCatastale != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDCodiceCatastale", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDCodiceCatastale"].Value = IDCodiceCatastale;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDCodiceCatastale", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDCodiceCatastale"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fUnitaImmobiliare", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fUnitaImmobiliare"].Value = bool.Parse(fUnitaImmobiliare.ToString());

            if ((IDDestinazioneUso != "0") && (IDDestinazioneUso != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDDestinazioneUso", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDDestinazioneUso"].Value = IDDestinazioneUso;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDDestinazioneUso", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDDestinazioneUso"].Value = DBNull.Value;
            }

            if (VolumeLordoRiscaldato != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@VolumeLordoRiscaldato", SqlDbType.Decimal));
                tipoComando.Parameters["@VolumeLordoRiscaldato"].Value = VolumeLordoRiscaldato;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@VolumeLordoRiscaldato", SqlDbType.Decimal));
                tipoComando.Parameters["@VolumeLordoRiscaldato"].Value = DBNull.Value;
            }

            if (VolumeLordoRaffrescato != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@VolumeLordoRaffrescato", SqlDbType.Decimal));
                tipoComando.Parameters["@VolumeLordoRaffrescato"].Value = VolumeLordoRaffrescato;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@VolumeLordoRaffrescato", SqlDbType.Decimal));
                tipoComando.Parameters["@VolumeLordoRaffrescato"].Value = DBNull.Value;
            }

            if (NumeroAPE != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroAPE", SqlDbType.NVarChar, 17));
                tipoComando.Parameters["@NumeroAPE"].Value = NumeroAPE;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroAPE", SqlDbType.NVarChar, 17));
                tipoComando.Parameters["@NumeroAPE"].Value = DBNull.Value;
            }

            if (NumeroPDR != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroPDR", SqlDbType.NVarChar, 14));
                tipoComando.Parameters["@NumeroPDR"].Value = NumeroPDR;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroPDR", SqlDbType.NVarChar, 14));
                tipoComando.Parameters["@NumeroPDR"].Value = DBNull.Value;
            }

            if (NumeroPOD != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroPOD", SqlDbType.NVarChar, 14));
                tipoComando.Parameters["@NumeroPOD"].Value = NumeroPOD;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroPOD", SqlDbType.NVarChar, 14));
                tipoComando.Parameters["@NumeroPOD"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fAcs", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fAcs"].Value = bool.Parse(fAcs.ToString());

            if (PotenzaAcs != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaAcs", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaAcs"].Value = PotenzaAcs;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaAcs", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaAcs"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fClimatizzazioneEstiva", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fClimatizzazioneEstiva"].Value = bool.Parse(fClimatizzazioneEstiva.ToString());

            if (PotenzaClimatizzazioneEstiva != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaClimatizzazioneEstiva", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaClimatizzazioneEstiva"].Value = PotenzaClimatizzazioneEstiva;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaClimatizzazioneEstiva", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaClimatizzazioneEstiva"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fClimatizzazioneInvernale", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fClimatizzazioneInvernale"].Value = bool.Parse(fClimatizzazioneInvernale.ToString());

            if (PotenzaClimatizzazioneInvernale != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaClimatizzazioneInvernale", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaClimatizzazioneInvernale"].Value = PotenzaClimatizzazioneInvernale;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaClimatizzazioneInvernale", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaClimatizzazioneInvernale"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fClimatizzazioneAltro", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fClimatizzazioneAltro"].Value = bool.Parse(fClimatizzazioneAltro.ToString());

            if (ClimatizzazioneAltro != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@ClimatizzazioneAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@ClimatizzazioneAltro"].Value = ClimatizzazioneAltro;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@ClimatizzazioneAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@ClimatizzazioneAltro"].Value = DBNull.Value;
            }

            if (TipologiaFluidoVettoreAltro != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@TipologiaFluidoVettoreAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@TipologiaFluidoVettoreAltro"].Value = TipologiaFluidoVettoreAltro;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@TipologiaFluidoVettoreAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@TipologiaFluidoVettoreAltro"].Value = DBNull.Value;
            }

            if (TipologiaGeneratoriAltro != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@TipologiaGeneratoriAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@TipologiaGeneratoriAltro"].Value = TipologiaGeneratoriAltro;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@TipologiaGeneratoriAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@TipologiaGeneratoriAltro"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fPannelliSolariTermici", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fPannelliSolariTermici"].Value = bool.Parse(fPannelliSolariTermici.ToString());

            if (SuperficieTotaleSolariTermici != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@SuperficieTotaleSolariTermici", SqlDbType.Decimal));
                tipoComando.Parameters["@SuperficieTotaleSolariTermici"].Value = SuperficieTotaleSolariTermici;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@SuperficieTotaleSolariTermici", SqlDbType.Decimal));
                tipoComando.Parameters["@SuperficieTotaleSolariTermici"].Value = DBNull.Value;
            }

            if (PotenzaSolariTermici != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaSolariTermici", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaSolariTermici"].Value = PotenzaSolariTermici;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@PotenzaSolariTermici", SqlDbType.Decimal));
                tipoComando.Parameters["@PotenzaSolariTermici"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fPannelliSolariTermiciAltro", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fPannelliSolariTermiciAltro"].Value = bool.Parse(fPannelliSolariTermiciAltro.ToString());

            if (PannelliSolariTermiciAltro != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@PannelliSolariTermiciAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@PannelliSolariTermiciAltro"].Value = PannelliSolariTermiciAltro;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@PannelliSolariTermiciAltro", SqlDbType.NVarChar, 200));
                tipoComando.Parameters["@PannelliSolariTermiciAltro"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fPannelliSolariClimatizzazioneAcs", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fPannelliSolariClimatizzazioneAcs"].Value = bool.Parse(fPannelliSolariClimatizzazioneAcs.ToString());

            tipoComando.Parameters.Add(new SqlParameter("@fPannelliSolariClimatizzazioneEstiva", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fPannelliSolariClimatizzazioneEstiva"].Value = bool.Parse(fPannelliSolariClimatizzazioneEstiva.ToString());

            tipoComando.Parameters.Add(new SqlParameter("@fPannelliSolariClimatizzazioneInvernale", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fPannelliSolariClimatizzazioneInvernale"].Value = bool.Parse(fPannelliSolariClimatizzazioneInvernale.ToString());

            if (ContenutoAcquaImpianto != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@ContenutoAcquaImpianto", SqlDbType.Decimal));
                tipoComando.Parameters["@ContenutoAcquaImpianto"].Value = ContenutoAcquaImpianto;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@ContenutoAcquaImpianto", SqlDbType.Decimal));
                tipoComando.Parameters["@ContenutoAcquaImpianto"].Value = DBNull.Value;
            }

            if (DurezzaTotaleAcquaImpianto != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DurezzaTotaleAcquaImpianto", SqlDbType.Decimal));
                tipoComando.Parameters["@DurezzaTotaleAcquaImpianto"].Value = DurezzaTotaleAcquaImpianto;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@DurezzaTotaleAcquaImpianto", SqlDbType.Decimal));
                tipoComando.Parameters["@DurezzaTotaleAcquaImpianto"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fTrattamentoAcquaInvernale", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fTrattamentoAcquaInvernale"].Value = bool.Parse(fTrattamentoAcquaInvernale.ToString());

            if (DurezzaTotaleAcquaImpiantoInvernale != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DurezzaTotaleAcquaImpiantoInvernale", SqlDbType.Decimal));
                tipoComando.Parameters["@DurezzaTotaleAcquaImpiantoInvernale"].Value = DurezzaTotaleAcquaImpiantoInvernale;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@DurezzaTotaleAcquaImpiantoInvernale", SqlDbType.Decimal));
                tipoComando.Parameters["@DurezzaTotaleAcquaImpiantoInvernale"].Value = DBNull.Value;
            }

            if ((IDTipologiaProtezioneGelo != "0") && (IDTipologiaProtezioneGelo != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaProtezioneGelo", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaProtezioneGelo"].Value = IDTipologiaProtezioneGelo;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaProtezioneGelo", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaProtezioneGelo"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fTrattamentoAcquaAcs", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fTrattamentoAcquaAcs"].Value = bool.Parse(fTrattamentoAcquaAcs.ToString());

            if (DurezzaTotaleAcquaAcs != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DurezzaTotaleAcquaAcs", SqlDbType.Decimal));
                tipoComando.Parameters["@DurezzaTotaleAcquaAcs"].Value = DurezzaTotaleAcquaAcs;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@DurezzaTotaleAcquaAcs", SqlDbType.Decimal));
                tipoComando.Parameters["@DurezzaTotaleAcquaAcs"].Value = DBNull.Value;
            }

            if (PercentualeGlicole != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@PercentualeGlicole", SqlDbType.Decimal));
                tipoComando.Parameters["@PercentualeGlicole"].Value = PercentualeGlicole;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@PercentualeGlicole", SqlDbType.Decimal));
                tipoComando.Parameters["@PercentualeGlicole"].Value = DBNull.Value;
            }

            if (PhGlicole != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@PhGlicole", SqlDbType.Decimal));
                tipoComando.Parameters["@PhGlicole"].Value = PhGlicole;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@PhGlicole", SqlDbType.Decimal));
                tipoComando.Parameters["@PhGlicole"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fTrattamentoAcquaEstiva", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fTrattamentoAcquaEstiva"].Value = bool.Parse(fTrattamentoAcquaEstiva.ToString());

            if ((IDTipologiaCircuitoRaffreddamento != "0") && (IDTipologiaCircuitoRaffreddamento != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaCircuitoRaffreddamento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaCircuitoRaffreddamento"].Value = IDTipologiaCircuitoRaffreddamento;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaCircuitoRaffreddamento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaCircuitoRaffreddamento"].Value = DBNull.Value;
            }

            if ((IDTipologiaAcquaAlimento != "0") && (IDTipologiaAcquaAlimento != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaAcquaAlimento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaAcquaAlimento"].Value = IDTipologiaAcquaAlimento;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDTipologiaAcquaAlimento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDTipologiaAcquaAlimento"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fSistemaSpurgoAutomatico", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fSistemaSpurgoAutomatico"].Value = bool.Parse(fSistemaSpurgoAutomatico.ToString());

            if (ConducibilitaAcquaIngresso != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@ConducibilitaAcquaIngresso", SqlDbType.Decimal));
                tipoComando.Parameters["@ConducibilitaAcquaIngresso"].Value = ConducibilitaAcquaIngresso;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@ConducibilitaAcquaIngresso", SqlDbType.Decimal));
                tipoComando.Parameters["@ConducibilitaAcquaIngresso"].Value = DBNull.Value;
            }

            if (ConducibilitaInizioSpurgo != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@ConducibilitaInizioSpurgo", SqlDbType.Decimal));
                tipoComando.Parameters["@ConducibilitaInizioSpurgo"].Value = ConducibilitaInizioSpurgo;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@ConducibilitaInizioSpurgo", SqlDbType.Decimal));
                tipoComando.Parameters["@ConducibilitaInizioSpurgo"].Value = DBNull.Value;
            }

            if (IDUtenteInserimento != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteInserimento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteInserimento"].Value = IDUtenteInserimento;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteInserimento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteInserimento"].Value = DBNull.Value;
            }

            if (operationType == "INSERT")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataInserimento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataInserimento"].Value = DateTime.Now;
            }
            else if (operationType == "UPDATE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataInserimento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataInserimento"].Value = DataInserimento;
            }
            else if (operationType == "DELETE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataInserimento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataInserimento"].Value = DBNull.Value;
            }

            if (IDUtenteUltimaModifica != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteUltimaModifica", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteUltimaModifica"].Value = IDUtenteUltimaModifica;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteUltimaModifica", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteUltimaModifica"].Value = DBNull.Value;
            }

            if (operationType == "INSERT")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataUltimaModifica", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataUltimaModifica"].Value = DateTime.Now;
            }
            else if (operationType == "UPDATE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataUltimaModifica", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataUltimaModifica"].Value = DateTime.Now;
            }
            else if (operationType == "DELETE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataUltimaModifica", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataUltimaModifica"].Value = DBNull.Value;
            }

            if (IDLibrettoImpiantoRevisione != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDLibrettoImpiantoRevisione", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDLibrettoImpiantoRevisione"].Value = IDLibrettoImpiantoRevisione;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDLibrettoImpiantoRevisione", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDLibrettoImpiantoRevisione"].Value = DBNull.Value;
            }

            if (NumeroRevisione != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroRevisione", SqlDbType.Int, 4));
                tipoComando.Parameters["@NumeroRevisione"].Value = NumeroRevisione;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@NumeroRevisione", SqlDbType.Int, 4));
                tipoComando.Parameters["@NumeroRevisione"].Value = DBNull.Value;
            }

            if (DataRevisione != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataRevisione", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataRevisione"].Value = DataRevisione;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataRevisione", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataRevisione"].Value = DBNull.Value;
            }

            if (DataAnnullamento != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataAnnullamento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataAnnullamento"].Value = DataAnnullamento;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataAnnullamento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataAnnullamento"].Value = DBNull.Value;
            }

            tipoComando.Parameters.Add(new SqlParameter("@fAttivo", SqlDbType.Bit, 1));
            tipoComando.Parameters["@fAttivo"].Value = bool.Parse(fAttivo.ToString());

            if (operationType == "INSERT")
            {
                conn.Open();
                IDLibrettoImpiantoInserted = (int)tipoComando.ExecuteScalar();
            }
            else if (operationType == "UPDATE")
            {
                tipoComando.Connection.Open();
                tipoComando.ExecuteNonQuery();
                tipoComando.Connection.Close();

                IDLibrettoImpiantoInserted = int.Parse(IDLibrettoImpianto.ToString());
            }
            else if (operationType == "DELETE")
            {
                tipoComando.Connection.Open();
                tipoComando.ExecuteNonQuery();
                tipoComando.Connection.Close();

                IDLibrettoImpiantoInserted = int.Parse(IDLibrettoImpianto.ToString());
            }

            return IDLibrettoImpiantoInserted;
        }

        public static int SaveCodiceCatastaleLibrettiImpianti(
                string operationType,
                string iDLibrettoImpianto,
                string iDCodiceCatastale
                )
        {
            int iDLibrettoImpiantoInserted = 0;

            SqlConnection conn = BuildConnection.GetConn();

            SqlCommand tipoComando = null;
            if (operationType == "UPDATE")
            {
                tipoComando = new SqlCommand("UPDATE LIM_LibrettiImpianti SET "
                    + "IDCodiceCatastale=@IDCodiceCatastale "
                    + " WHERE IDLibrettoImpianto=@IDLibrettoImpianto ", conn);
            }

            tipoComando.CommandType = CommandType.Text;

            tipoComando.Parameters.Add(new SqlParameter("@IDLibrettoImpianto", SqlDbType.Int, 4));
            tipoComando.Parameters["@IDLibrettoImpianto"].Value = iDLibrettoImpianto;

            if ((iDCodiceCatastale != "0") && (iDCodiceCatastale != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDCodiceCatastale", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDCodiceCatastale"].Value = iDCodiceCatastale;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDCodiceCatastale", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDCodiceCatastale"].Value = DBNull.Value;
            }

            if (operationType == "UPDATE")
            {
                tipoComando.Connection.Open();
                tipoComando.ExecuteNonQuery();
                tipoComando.Connection.Close();

                iDLibrettoImpiantoInserted = int.Parse(iDLibrettoImpianto.ToString());
            }

            return iDLibrettoImpiantoInserted;
        }

        public static int SaveInsertDeleteDatiCatastaliLibrettiImpianti(
                string operationType,
                string IDLibrettoImpiantoDatiCatastali,
                string IDLibrettoImpianto,
                string IDCodiceCatastaleSezione,
                string Foglio,
                string Mappale,
                string Subalterno,
                string Identificativo,
                string IDUtenteInserimento,
                string DataInserimento,
                string IDUtenteUltimaModifica,
                string DataUltimaModifica
                )
        {
            int IDLibrettoImpiantoDatiCatastaliInserted = 0;

            SqlConnection conn = BuildConnection.GetConn();

            SqlCommand tipoComando = null;
            if (operationType == "INSERT")
            {
                tipoComando = new SqlCommand("INSERT INTO LIM_LibrettiImpiantiDatiCatastali ("
                    + "IDLibrettoImpianto, "
                    + "IDCodiceCatastaleSezione, "
                    + "Foglio, "
                    + "Mappale, "
                    + "Subalterno, "
                    + "Identificativo, "
                    + "IDUtenteInserimento, "
                    + "DataInserimento, "
                    + "IDUtenteUltimaModifica, "
                    + "DataUltimaModifica "
                    + "  ) OUTPUT INSERTED.IDLibrettoImpiantoDatiCatastali VALUES ( "
                    + "@IDLibrettoImpianto, "
                    + "@IDCodiceCatastaleSezione, "
                    + "@Foglio, "
                    + "@Mappale, "
                    + "@Subalterno, "
                    + "@Identificativo, "
                    + "@IDUtenteInserimento, "
                    + "@DataInserimento, "
                    + "@IDUtenteUltimaModifica, "
                    + "@DataUltimaModifica "
                    + " ) ", conn);
            }
            else if (operationType == "UPDATE")
            {
                tipoComando = new SqlCommand("UPDATE LIM_LibrettiImpiantiDatiCatastali SET "
                    + "IDLibrettoImpianto=@IDLibrettoImpianto, "
                    + "IDCodiceCatastaleSezione=@IDCodiceCatastaleSezione, "
                    + "Foglio=@Foglio, "
                    + "Mappale=@Mappale, "
                    + "Subalterno=@Subalterno, "
                    + "Identificativo=@Identificativo, "
                    + "IDUtenteInserimento=@IDUtenteInserimento, "
                    + "DataInserimento=@DataInserimento, "
                    + "IDUtenteUltimaModifica=@IDUtenteUltimaModifica, "
                    + "DataUltimaModifica=@DataUltimaModifica "
                    + " WHERE IDLibrettoImpiantoDatiCatastali=@IDLibrettoImpiantoDatiCatastali ", conn);
            }
            else if (operationType == "DELETE")
            {
                tipoComando = new SqlCommand("DELETE FROM LIM_LibrettiImpiantiDatiCatastali "
                  + " WHERE IDLibrettoImpiantoDatiCatastali=@IDLibrettoImpiantoDatiCatastali ", conn);
            }

            tipoComando.CommandType = CommandType.Text;

            if ((operationType == "UPDATE") || (operationType == "DELETE"))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDLibrettoImpiantoDatiCatastali", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDLibrettoImpiantoDatiCatastali"].Value = IDLibrettoImpiantoDatiCatastali;
            }

            if (IDLibrettoImpianto != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDLibrettoImpianto", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDLibrettoImpianto"].Value = IDLibrettoImpianto;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDLibrettoImpianto", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDLibrettoImpianto"].Value = DBNull.Value;
            }

            if ((IDCodiceCatastaleSezione != "0") && (IDCodiceCatastaleSezione != ""))
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDCodiceCatastaleSezione", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDCodiceCatastaleSezione"].Value = IDCodiceCatastaleSezione;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDCodiceCatastaleSezione", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDCodiceCatastaleSezione"].Value = DBNull.Value;
            }

            if (Foglio != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Foglio", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Foglio"].Value = Foglio;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Foglio", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Foglio"].Value = DBNull.Value;
            }

            if (Mappale != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Mappale", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Mappale"].Value = Mappale;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Mappale", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Mappale"].Value = DBNull.Value;
            }

            if (Subalterno != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Subalterno", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Subalterno"].Value = Subalterno;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Subalterno", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Subalterno"].Value = DBNull.Value;
            }

            if (Identificativo != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@Identificativo", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Identificativo"].Value = Identificativo;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@Identificativo", SqlDbType.NVarChar, 50));
                tipoComando.Parameters["@Identificativo"].Value = DBNull.Value;
            }

            if (IDUtenteInserimento != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteInserimento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteInserimento"].Value = IDUtenteInserimento;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteInserimento", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteInserimento"].Value = DBNull.Value;
            }

            if (operationType == "INSERT")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataInserimento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataInserimento"].Value = DateTime.Now;
            }
            else if (operationType == "UPDATE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataInserimento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataInserimento"].Value = DataInserimento;
            }
            else if (operationType == "DELETE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataInserimento", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataInserimento"].Value = DBNull.Value;
            }

            if (IDUtenteUltimaModifica != "")
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteUltimaModifica", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteUltimaModifica"].Value = IDUtenteUltimaModifica;
            }
            else
            {
                tipoComando.Parameters.Add(new SqlParameter("@IDUtenteUltimaModifica", SqlDbType.Int, 4));
                tipoComando.Parameters["@IDUtenteUltimaModifica"].Value = DBNull.Value;
            }

            if (operationType == "INSERT")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataUltimaModifica", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataUltimaModifica"].Value = DateTime.Now;
            }
            else if (operationType == "UPDATE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataUltimaModifica", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataUltimaModifica"].Value = DateTime.Now;
            }
            else if (operationType == "DELETE")
            {
                tipoComando.Parameters.Add(new SqlParameter("@DataUltimaModifica", SqlDbType.DateTime, 8));
                tipoComando.Parameters["@DataUltimaModifica"].Value = DBNull.Value;
            }

            if (operationType == "INSERT")
            {
                conn.Open();
                IDLibrettoImpiantoDatiCatastaliInserted = (int)tipoComando.ExecuteScalar();
            }
            else if (operationType == "UPDATE")
            {
                tipoComando.Connection.Open();
                tipoComando.ExecuteNonQuery();
                tipoComando.Connection.Close();

                IDLibrettoImpiantoDatiCatastaliInserted = int.Parse(IDLibrettoImpiantoDatiCatastali.ToString());
            }
            else if (operationType == "DELETE")
            {
                tipoComando.Connection.Open();
                tipoComando.ExecuteNonQuery();
                tipoComando.Connection.Close();

                IDLibrettoImpiantoDatiCatastaliInserted = int.Parse(IDLibrettoImpiantoDatiCatastali.ToString());
            }

            return IDLibrettoImpiantoDatiCatastaliInserted;
        }

        public static void SaveInsertDeleteDatiTipologiaFluidoVettore(
                int iDLibrettoImpianto,
                string tipologiaFluidoVettoreAltro,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var vettoriAttuali = db.LIM_LibrettiImpiantiTipologiaFluidoVettore.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var vettori in vettoriAttuali)
            {
                if (!valoriSelected.Contains(vettori.IDTipologiaFluidoVettore.ToString())
                    || valoriSelected.Contains("1"))
                {
                    db.LIM_LibrettiImpiantiTipologiaFluidoVettore.Remove(vettori);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!vettoriAttuali.Any(o => o.IDTipologiaFluidoVettore.ToString() == valoriSelected[i])
                    || vettoriAttuali.Any(o => o.IDTipologiaFluidoVettore.ToString() == "1"))
                {
                    if (valoriSelected[i] == "1")
                    {
                        string tipologiaFluidoVettoreAltrox = "";
                        if (!string.IsNullOrEmpty(tipologiaFluidoVettoreAltro))
                        {
                            tipologiaFluidoVettoreAltrox = tipologiaFluidoVettoreAltro;
                        }

                        db.LIM_LibrettiImpiantiTipologiaFluidoVettore.Add(new LIM_LibrettiImpiantiTipologiaFluidoVettore() { TipologiaFluidoVettoreAltro = tipologiaFluidoVettoreAltrox, IDTipologiaFluidoVettore = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                    else
                    {
                        db.LIM_LibrettiImpiantiTipologiaFluidoVettore.Add(new LIM_LibrettiImpiantiTipologiaFluidoVettore() { IDTipologiaFluidoVettore = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTipologiaGeneratori(
                int iDLibrettoImpianto,
                string tipologiaGeneratoriAltro,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var generatoriAttuali = db.LIM_LibrettiImpiantiTipologiaGeneratori.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var generatori in generatoriAttuali)
            {
                if (!valoriSelected.Contains(generatori.IDTipologiaGeneratori.ToString())
                    || valoriSelected.Contains("1"))
                {
                    db.LIM_LibrettiImpiantiTipologiaGeneratori.Remove(generatori);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!generatoriAttuali.Any(o => o.IDTipologiaGeneratori.ToString() == valoriSelected[i])
                    || generatoriAttuali.Any(o => o.IDTipologiaGeneratori.ToString() == "1"))
                {
                    if (valoriSelected[i] == "1")
                    {
                        string tipologiaGeneratoriAltrox = "";
                        if (!string.IsNullOrEmpty(tipologiaGeneratoriAltro))
                        {
                            tipologiaGeneratoriAltrox = tipologiaGeneratoriAltro;
                        }

                        db.LIM_LibrettiImpiantiTipologiaGeneratori.Add(new LIM_LibrettiImpiantiTipologiaGeneratori() { TipologiaGeneratoriAltro = tipologiaGeneratoriAltrox, IDTipologiaGeneratori = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                    else
                    {
                        db.LIM_LibrettiImpiantiTipologiaGeneratori.Add(new LIM_LibrettiImpiantiTipologiaGeneratori() { IDTipologiaGeneratori = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTrattamentoAcquaInvernale(
                int iDLibrettoImpianto,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var trattamentiAttuali = db.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var trattamenti in trattamentiAttuali)
            {
                if (!valoriSelected.Contains(trattamenti.IDTipologiaTrattamentoAcqua.ToString()))
                {
                    db.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Remove(trattamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!trattamentiAttuali.Any(o => o.IDTipologiaTrattamentoAcqua.ToString() == valoriSelected[i]))
                {
                    db.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Add(new LIM_LibrettiImpiantiTrattamentoAcquaInvernale() { IDTipologiaTrattamentoAcqua = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTrattamentoAcquaAcs(
                int iDLibrettoImpianto,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var trattamentiAttuali = db.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var trattamenti in trattamentiAttuali)
            {
                if (!valoriSelected.Contains(trattamenti.IDTipologiaTrattamentoAcqua.ToString()))
                {
                    db.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Remove(trattamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!trattamentiAttuali.Any(o => o.IDTipologiaTrattamentoAcqua.ToString() == valoriSelected[i]))
                {
                    db.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Add(new LIM_LibrettiImpiantiTrattamentoAcquaAcs() { IDTipologiaTrattamentoAcqua = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTrattamentoAcquaEstiva(
                int iDLibrettoImpianto,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var trattamentiAttuali = db.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var trattamenti in trattamentiAttuali)
            {
                if (!valoriSelected.Contains(trattamenti.IDTipologiaTrattamentoAcqua.ToString()))
                {
                    db.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Remove(trattamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!trattamentiAttuali.Any(o => o.IDTipologiaTrattamentoAcqua.ToString() == valoriSelected[i]))
                {
                    db.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Add(new LIM_LibrettiImpiantiTrattamentoAcquaEstiva() { IDTipologiaTrattamentoAcqua = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTipologiaFiltrazioni(
                int iDLibrettoImpianto,
                string tipologiaFiltrazioneAltro,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var filtrazioniAttuali = db.LIM_LibrettiImpiantiTipologiaFiltrazioni.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var filtrazioni in filtrazioniAttuali)
            {
                if (!valoriSelected.Contains(filtrazioni.IDTipologiaFiltrazione.ToString())
                    || valoriSelected.Contains("1"))
                {
                    db.LIM_LibrettiImpiantiTipologiaFiltrazioni.Remove(filtrazioni);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!filtrazioniAttuali.Any(o => o.IDTipologiaFiltrazione.ToString() == valoriSelected[i])
                    || filtrazioniAttuali.Any(o => o.IDTipologiaFiltrazione.ToString() == "1"))
                {
                    if (valoriSelected[i] == "1")
                    {
                        string tipologiaFiltrazioneAltrox = "";
                        if (!string.IsNullOrEmpty(tipologiaFiltrazioneAltro))
                        {
                            tipologiaFiltrazioneAltrox = tipologiaFiltrazioneAltro;
                        }

                        db.LIM_LibrettiImpiantiTipologiaFiltrazioni.Add(new LIM_LibrettiImpiantiTipologiaFiltrazioni() { TipologiaFiltrazioneAltro = tipologiaFiltrazioneAltrox, IDTipologiaFiltrazione = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                    else
                    {
                        db.LIM_LibrettiImpiantiTipologiaFiltrazioni.Add(new LIM_LibrettiImpiantiTipologiaFiltrazioni() { IDTipologiaFiltrazione = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTipologiaAddolcimentoAcqua(
               int iDLibrettoImpianto,
               string tipologiaAddolcimentoAcquaAltro,
               string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var addolcimentiAttuali = db.LIM_LibrettiImpiantiAddolcimentoAcqua.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var addolcimenti in addolcimentiAttuali)
            {
                if (!valoriSelected.Contains(addolcimenti.IDTipologiaAddolcimentoAcqua.ToString())
                    || valoriSelected.Contains("1"))
                {
                    db.LIM_LibrettiImpiantiAddolcimentoAcqua.Remove(addolcimenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!addolcimentiAttuali.Any(o => o.IDTipologiaAddolcimentoAcqua.ToString() == valoriSelected[i])
                    || addolcimentiAttuali.Any(o => o.IDTipologiaAddolcimentoAcqua.ToString() == "1"))
                {
                    if (valoriSelected[i] == "1")
                    {
                        string tipologiaAddolcimentoAcquaAltrox = "";
                        if (!string.IsNullOrEmpty(tipologiaAddolcimentoAcquaAltro))
                        {
                            tipologiaAddolcimentoAcquaAltrox = tipologiaAddolcimentoAcquaAltro;
                        }

                        db.LIM_LibrettiImpiantiAddolcimentoAcqua.Add(new LIM_LibrettiImpiantiAddolcimentoAcqua() { AddolcimentoAcquaAltro = tipologiaAddolcimentoAcquaAltrox, IDTipologiaAddolcimentoAcqua = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                    else
                    {
                        db.LIM_LibrettiImpiantiAddolcimentoAcqua.Add(new LIM_LibrettiImpiantiAddolcimentoAcqua() { IDTipologiaAddolcimentoAcqua = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTipologiaCondizionamentoChimico(
               int iDLibrettoImpianto,
               string tipologiaCondizionamentoChimicoAltro,
               string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var condizionamentiAttuali = db.LIM_LibrettiImpiantiCondizionamentoChimico.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var condizionamenti in condizionamentiAttuali)
            {
                if (!valoriSelected.Contains(condizionamenti.IDTipologiaCondizionamentoChimico.ToString())
                    || valoriSelected.Contains("1"))
                {
                    db.LIM_LibrettiImpiantiCondizionamentoChimico.Remove(condizionamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!condizionamentiAttuali.Any(o => o.IDTipologiaCondizionamentoChimico.ToString() == valoriSelected[i])
                    || condizionamentiAttuali.Any(o => o.IDTipologiaCondizionamentoChimico.ToString() == "1"))
                {
                    if (valoriSelected[i] == "1")
                    {
                        string tipologiaCondizionamentoChimicoAltrox = "";
                        if (!string.IsNullOrEmpty(tipologiaCondizionamentoChimicoAltro))
                        {
                            tipologiaCondizionamentoChimicoAltrox = tipologiaCondizionamentoChimicoAltro;
                        }
                        db.LIM_LibrettiImpiantiCondizionamentoChimico.Add(new LIM_LibrettiImpiantiCondizionamentoChimico() { TipologiaCondizionamentoChimicoAltro = tipologiaCondizionamentoChimicoAltrox, IDTipologiaCondizionamentoChimico = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                    else
                    {
                        db.LIM_LibrettiImpiantiCondizionamentoChimico.Add(new LIM_LibrettiImpiantiCondizionamentoChimico() { IDTipologiaCondizionamentoChimico = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTipologiaSistemaDistribuzione(
                int iDLibrettoImpianto,
                string tipologiaSistemaDistribuzioneAltro,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var distribuzioniAttuali = db.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Where(i => i.IDLibrettoImpianto == iDLibrettoImpianto).ToList();

            foreach (var distribuzioni in distribuzioniAttuali)
            {
                if (!valoriSelected.Contains(distribuzioni.IDTipologiaSistemaDistribuzione.ToString())
                    || valoriSelected.Contains("1"))
                {
                    db.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Remove(distribuzioni);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!distribuzioniAttuali.Any(o => o.IDTipologiaSistemaDistribuzione.ToString() == valoriSelected[i])
                    || distribuzioniAttuali.Any(o => o.IDTipologiaSistemaDistribuzione.ToString() == "1"))
                {
                    if (valoriSelected[i] == "1")
                    {
                        string tipologiaSistemaDistribuzioneAltrox = "";
                        if (!string.IsNullOrEmpty(tipologiaSistemaDistribuzioneAltro))
                        {
                            tipologiaSistemaDistribuzioneAltrox = tipologiaSistemaDistribuzioneAltro;
                        }

                        db.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Add(new LIM_LibrettiImpiantiTipologiaSistemaDistribuzione() { TipologiaSistemaDistribuzioneAltro = tipologiaSistemaDistribuzioneAltrox, IDTipologiaSistemaDistribuzione = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                    else
                    {
                        db.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Add(new LIM_LibrettiImpiantiTipologiaSistemaDistribuzione() { IDTipologiaSistemaDistribuzione = int.Parse(valoriSelected[i]), IDLibrettoImpianto = iDLibrettoImpianto });
                    }
                }
            }

            db.SaveChanges();
        }

        public static string GetSqlValoriLibrettiFilter(object iDSoggettoAzienda,
                                                        object iDSoggettoManutentore,
                                                        object codiceTargaturaImpianto,
                                                        object iDCodiceCastale,
                                                        object foglio,
                                                        object mappale,
                                                        object subalterno,
                                                        object identificativo,
                                                        object iDStatoLibrettoImpianto,
                                                        string responsabile,
                                                        string CfPIvaResponsabile,
                                                        string codicePod,
                                                        string codicePdr,
                                                        object DataRegistrazioneDal, object DataRegistrazioneAl,
                                                        string keyApi,
                                                        bool fGeneratoriDismessi,
                                                        string Indirizzo,
                                                        string Civico,
                                                        object MatricolaGeneratore,
                                                        object IDTipoGeneratore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_LIM_LibrettiImpianti ");
            strSql.Append(" WHERE fattivo=1 ");

            if ((iDSoggettoAzienda != "") && (iDSoggettoAzienda != "-1") && (iDSoggettoAzienda != null))
            {
                strSql.Append(" AND IDSoggettoAzienda=" + iDSoggettoAzienda);
            }

            if ((iDSoggettoManutentore != "") && (iDSoggettoManutentore != "-1") && (iDSoggettoManutentore != null))
            {
                strSql.Append(" AND IDSoggettoManutentore=" + iDSoggettoManutentore);
            }

            if (!string.IsNullOrEmpty(keyApi))
            {
                strSql.Append(" AND keyApi = ");
                strSql.Append("'");
                strSql.Append(keyApi);
                strSql.Append("'");
            }

            if (codiceTargaturaImpianto.ToString() != "")
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(codiceTargaturaImpianto);
                strSql.Append("'");
            }

            //Caso Particolare
            if ((codicePod.ToString() != "") || (codicePdr.ToString() != ""))
            {
                if (codicePod.ToString() == codicePdr.ToString())
                {
                    strSql.Append(" AND (NumeroPOD = ");
                    strSql.Append("'");
                    strSql.Append(codicePod);
                    strSql.Append("'");
                    strSql.Append(" OR NumeroPDR = ");
                    strSql.Append("'");
                    strSql.Append(codicePdr);
                    strSql.Append("')");
                }
                else
                {
                    if (codicePod.ToString() != "")
                    {
                        strSql.Append(" AND NumeroPOD = ");
                        strSql.Append("'");
                        strSql.Append(codicePod);
                        strSql.Append("'");
                    }

                    if (codicePdr.ToString() != "")
                    {
                        strSql.Append(" AND NumeroPDR = ");
                        strSql.Append("'");
                        strSql.Append(codicePdr);
                        strSql.Append("'");
                    }
                }
            }

            if (iDCodiceCastale != null)
            {
                strSql.Append(" AND IDCodiceCatastale=" + iDCodiceCastale + "");
            }

            if ((foglio.ToString() != "") || (mappale.ToString() != "") || (subalterno.ToString() != "") || (identificativo.ToString() != ""))
            {
                strSql.Append(" AND IDLibrettoImpianto IN ");
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

            if (iDStatoLibrettoImpianto.ToString() != "0")
            {
                strSql.Append(" AND IDStatoLibrettoImpianto=");
                strSql.Append(iDStatoLibrettoImpianto);
            }

            if (!string.IsNullOrEmpty(responsabile))
            {
                strSql.Append(string.Format(" AND ([NomeResponsabile] + ' '  + [CognomeResponsabile] LIKE '%{0}%' OR [RagioneSocialeResponsabile] LIKE '%{1}%') ", responsabile.Replace("'", ""), responsabile.Replace("'", "")));
            }
            if (!string.IsNullOrEmpty(CfPIvaResponsabile))
            {
                strSql.Append(string.Format(" AND (CodiceFiscaleResponsabile LIKE '%{0}%' OR PartitaIvaResponsabile LIKE '%{0}%') ", CfPIvaResponsabile));
            }
            if (!string.IsNullOrEmpty(Indirizzo))
            {
                strSql.Append(string.Format(" AND (Indirizzo LIKE '%{0}%') ", Indirizzo));
            }
            if (!string.IsNullOrEmpty(Civico))
            {
                strSql.Append(string.Format(" AND (Civico LIKE '%{0}%') ", Civico));
            }

            if ((DataRegistrazioneDal != null) && (DataRegistrazioneDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInserimento, 126) >= '");
                DateTime dataInserimentoDa = DateTime.Parse(DataRegistrazioneDal.ToString());
                string newDataInserimentoDa = dataInserimentoDa.ToString("yyyy") + "-" + dataInserimentoDa.ToString("MM") + "-" + dataInserimentoDa.ToString("dd");
                strSql.Append(newDataInserimentoDa);
                strSql.Append("'");
            }

            if ((DataRegistrazioneAl != null) && (DataRegistrazioneAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInserimento, 126) <= '");
                DateTime dataInserimentoAl = DateTime.Parse(DataRegistrazioneAl.ToString());
                string newdataInserimentoeAl = dataInserimentoAl.ToString("yyyy") + "-" + dataInserimentoAl.ToString("MM") + "-" + dataInserimentoAl.ToString("dd");
                strSql.Append(newdataInserimentoeAl);
                strSql.Append("'");
            }

            if (fGeneratoriDismessi)
            {
                strSql.Append(" AND IDLibrettoImpianto IN ");
                strSql.Append("( ");
                strSql.Append("SELECT DISTINCT IDLibrettoImpianto FROM ");
                strSql.Append("( ");
                strSql.Append("SELECT IDLibrettoImpianto FROM LIM_LibrettiImpiantiGruppiTermici WHERE fDismesso = 1 ");
                strSql.Append("UNION ");
                strSql.Append("SELECT IDLibrettoImpianto FROM LIM_LibrettiImpiantiMacchineFrigorifere WHERE fDismesso = 1 ");
                strSql.Append("UNION ");
                strSql.Append("SELECT IDLibrettoImpianto FROM LIM_LibrettiImpiantiCogeneratori WHERE fDismesso = 1 ");
                strSql.Append("UNION ");
                strSql.Append("SELECT IDLibrettoImpianto FROM LIM_LibrettiImpiantiScambiatoriCalore WHERE fDismesso = 1 ");
                strSql.Append(") AS generatori");
                strSql.Append(" )");
            }

            if (!string.IsNullOrEmpty(MatricolaGeneratore.ToString()))
            {
                string MatricolaFilter = string.Empty;

                switch (IDTipoGeneratore)
                {
                    case "1":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiGruppiTermici.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiGruppiTermici ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiGruppiTermici.fAttivo = 1) AND (LIM_LibrettiImpiantiGruppiTermici.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                    case "2":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiMacchineFrigorifere.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiMacchineFrigorifere ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiMacchineFrigorifere.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiMacchineFrigorifere.fAttivo = 1) AND (LIM_LibrettiImpiantiMacchineFrigorifere.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                    case "3":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiScambiatoriCalore.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiScambiatoriCalore ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiScambiatoriCalore.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiScambiatoriCalore.fAttivo = 1) AND (LIM_LibrettiImpiantiScambiatoriCalore.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                    case "4":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiCogeneratori.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiCogeneratori ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiCogeneratori.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiCogeneratori.fAttivo = 1) AND (LIM_LibrettiImpiantiCogeneratori.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                }
            }

            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            if (info.IDRuolo == 16)
            {
                strSql.Append(" AND IDCodiceCatastale IN ");
                strSql.Append("( SELECT IDCodiceCatastale FROM COM_CodiciCatastaliCompetenza WHERE IDSoggetto= " + info.IDSoggetto + ")");
            }

            strSql.Append(" ORDER BY DataInserimento DESC");
            return strSql.ToString();
        }

        public static string GetSqlValoriLibrettiSuCatastoFilter(object CodiceTargaturaImpianto,
                                                                 object TipoGeneratore,
                                                                 object IDCodiceCastale,
                                                                 object Indirizzo,
                                                                 object Civico,
                                                                 object MatricolaGeneratore,
                                                                 object CodicePod,
                                                                 object CodicePdr,
                                                                 object CfPIvaResponsabile
                                                                )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 100 "); //Prestazioni
            strSql.Append(" * ");
            strSql.Append(" FROM V_LIM_LibrettiImpianti ");
            strSql.Append(" WHERE fattivo=1 ");

            if (!string.IsNullOrEmpty(CodiceTargaturaImpianto.ToString()))
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(CodiceTargaturaImpianto);
                strSql.Append("'");
            }

            if (!string.IsNullOrEmpty(MatricolaGeneratore.ToString()))
            {
                string MatricolaFilter = string.Empty;
               
                switch (TipoGeneratore)
                {
                    case "1":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiGruppiTermici.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiGruppiTermici ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiGruppiTermici.fAttivo = 1) AND (LIM_LibrettiImpiantiGruppiTermici.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                    case "2":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiMacchineFrigorifere.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiMacchineFrigorifere ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiMacchineFrigorifere.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiMacchineFrigorifere.fAttivo = 1) AND (LIM_LibrettiImpiantiMacchineFrigorifere.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                    case "3":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiScambiatoriCalore.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiScambiatoriCalore ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiScambiatoriCalore.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiScambiatoriCalore.fAttivo = 1) AND (LIM_LibrettiImpiantiScambiatoriCalore.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                    case "4":
                        MatricolaFilter = string.Format(" AND (LIM_LibrettiImpiantiCogeneratori.Matricola LIKE '%{0}%') ", MatricolaGeneratore);
                        strSql.Append(" AND IDLibrettoImpianto IN ");
                        strSql.Append("( ");
                        strSql.Append("SELECT DISTINCT LIM_LibrettiImpianti.IDLibrettoImpianto FROM LIM_LibrettiImpianti INNER JOIN LIM_LibrettiImpiantiCogeneratori ON LIM_LibrettiImpianti.IDLibrettoImpianto = LIM_LibrettiImpiantiCogeneratori.IDLibrettoImpianto WHERE (LIM_LibrettiImpianti.fAttivo = 1) AND (LIM_LibrettiImpiantiCogeneratori.fAttivo = 1) AND (LIM_LibrettiImpiantiCogeneratori.fDismesso = 0) AND (LIM_LibrettiImpianti.IDStatoLibrettoImpianto = 2) " + MatricolaFilter);
                        strSql.Append(" )");
                        break;
                }
            }

            if (IDCodiceCastale !=null)
            {
                strSql.Append(" AND (IDCodiceCatastale=" + IDCodiceCastale + ")");
            }

            if (!string.IsNullOrEmpty(CodicePod.ToString()))
            {
                strSql.Append(" AND (NumeroPOD = ");
                strSql.Append("'");
                strSql.Append(CodicePod);
                strSql.Append("')");
            }

            if (!string.IsNullOrEmpty(CodicePdr.ToString()))
            {
                strSql.Append(" AND (NumeroPDR = ");
                strSql.Append("'");
                strSql.Append(CodicePdr);
                strSql.Append("')");
            }
                                    
            if (!string.IsNullOrEmpty(CfPIvaResponsabile.ToString()))
            {
                strSql.Append(string.Format(" AND (CodiceFiscaleResponsabile = '{0}' OR PartitaIvaResponsabile = '{0}') ", CfPIvaResponsabile));
            }
            if (!string.IsNullOrEmpty(Indirizzo.ToString()))
            {
                strSql.Append(string.Format(" AND (Indirizzo LIKE '%{0}%') ", Indirizzo.ToString().Replace("'", "''")));
            }
            if (!string.IsNullOrEmpty(Civico.ToString()))
            {
                strSql.Append(string.Format(" AND (Civico LIKE '%{0}%') ", Civico.ToString().Replace("'", "''")));
            }

            strSql.Append(" ORDER BY DataInserimento DESC");
            return strSql.ToString();
        }


        public static LIM_LibrettiImpianti ClonaLibretto(LIM_LibrettiImpianti libretto)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var clone = ctx.LIM_LibrettiImpianti
                            //.AsNoTracking()
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiAccumuli)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiAddolcimentoAcqua)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiAltriGeneratori)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiCampiSolariTermici)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiCircuitiInterrati)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiCogeneratori)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiCondizionamentoChimico)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiConsumoAcqua)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiConsumoCombustibile)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiConsumoEnergiaElettrica)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiConsumoProdottiChimici)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiDatiCatastali)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiDescrizioniSistemi)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiGruppiTermici)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiGruppiTermici.Select(x => x.LIM_LibrettiImpiantiRecuperatori))
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiGruppiTermici.Select(x => x.LIM_LibrettiImpiantiBruciatori))
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiImpiantiVMC)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiMacchineFrigorifere)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiPompeCircolazione)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiRaffreddatoriLiquido)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiRecuperatoriCalore)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiResponsabili)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiScambiatoriCalore)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiSistemiRegolazione)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTipologiaFiltrazioni)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTipologiaFluidoVettore)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTipologiaGeneratori)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTipologiaSistemiEmissione)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTorriEvaporative)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTrattamentoAcquaAcs)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTrattamentoAcquaEstiva)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiTrattamentoAcquaInvernale)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiUnitaTrattamentoAria)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiValvoleRegolazione)
                            .IncludeOptimized(l => l.LIM_LibrettiImpiantiVasiEspansione)
                            .Single(l => l.IDLibrettoImpianto == libretto.IDLibrettoImpianto);

                clone.LIM_LibrettiImpiantiAccumuli.ToList().ForEach(i => i.IDLibrettoImpiantoAccumulo = 0);
                clone.LIM_LibrettiImpiantiAddolcimentoAcqua.ToList().ForEach(i => i.IDLibrettoImpiantoAddolcimentoAcqua = 0);
                clone.LIM_LibrettiImpiantiAltriGeneratori.ToList().ForEach(i => i.IDLibrettoImpiantoAltroGeneratore = 0);
                clone.LIM_LibrettiImpiantiCampiSolariTermici.ToList().ForEach(i => i.IDLibrettoImpiantoCampoSolareTermico = 0);
                clone.LIM_LibrettiImpiantiCircuitiInterrati.ToList().ForEach(i => i.IDLibrettoImpiantoCircuitoInterrato = 0);
                clone.LIM_LibrettiImpiantiCogeneratori.ToList().ForEach(i => i.IDLibrettoImpiantoCogeneratore = 0);
                clone.LIM_LibrettiImpiantiConsumoAcqua.ToList().ForEach(i => i.IDLibrettiImpiantiConsumoAcqua = 0);
                clone.LIM_LibrettiImpiantiConsumoCombustibile.ToList().ForEach(i => i.IDLibrettiImpiantiConsumoCombustibile = 0);
                clone.LIM_LibrettiImpiantiConsumoEnergiaElettrica.ToList().ForEach(i => i.IDLibrettiImpiantiConsumoEnergiaElettrica = 0);
                clone.LIM_LibrettiImpiantiConsumoProdottiChimici.ToList().ForEach(i => i.IDLibrettiImpiantiConsumoProdottiChimici = 0);
                clone.LIM_LibrettiImpiantiDatiCatastali.ToList().ForEach(i => i.IDLibrettoImpiantoDatiCatastali = 0);
                clone.LIM_LibrettiImpiantiDescrizioniSistemi.ToList().ForEach(i => i.IDLibrettoImpiantoDescrizioneSistema = 0);
                clone.LIM_LibrettiImpiantiGruppiTermici.ToList().ForEach(i => i.IDLibrettoImpiantoGruppoTermico = 0);
                clone.LIM_LibrettiImpiantiGruppiTermici.Select(i => i.LIM_LibrettiImpiantiRecuperatori).ToList();
                clone.LIM_LibrettiImpiantiGruppiTermici.Select(i => i.LIM_LibrettiImpiantiBruciatori).ToList();
                clone.LIM_LibrettiImpiantiImpiantiVMC.ToList().ForEach(i => i.IDLibrettoImpiantoImpiantiVMC = 0);
                clone.LIM_LibrettiImpiantiMacchineFrigorifere.ToList().ForEach(i => i.IDLibrettoImpiantoMacchinaFrigorifera = 0);
                clone.LIM_LibrettiImpiantiPompeCircolazione.ToList().ForEach(i => i.IDLibrettoImpiantoPompaCircolazione = 0);
                clone.LIM_LibrettiImpiantiRaffreddatoriLiquido.ToList().ForEach(i => i.IDLibrettoImpiantoRaffreddatoreLiquido = 0);
                clone.LIM_LibrettiImpiantiRecuperatoriCalore.ToList().ForEach(i => i.IDLibrettoImpiantoRecuperatoreCalore = 0);
                clone.LIM_LibrettiImpiantiResponsabili.ToList().ForEach(i => i.IDLibrettoImpiantoResponsabili = 0);
                clone.LIM_LibrettiImpiantiScambiatoriCalore.ToList().ForEach(i => i.IDLibrettoImpiantoScambiatoreCalore = 0);
                clone.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi.ToList().ForEach(i => i.IDLibrettoImpiantoScambiatoreCaloreIntermedio = 0);
                clone.LIM_LibrettiImpiantiSistemiRegolazione.ToList().ForEach(i => i.IDLibrettoImpiantoSistemaRegolazione = 0);
                clone.LIM_LibrettiImpiantiTipologiaFiltrazioni.ToList().ForEach(i => i.IDLibrettoImpiantoTipologiaFiltrazione = 0);
                clone.LIM_LibrettiImpiantiTipologiaFluidoVettore.ToList().ForEach(i => i.IDLibrettoImpiantoTipologiaFluidoVettore = 0);
                clone.LIM_LibrettiImpiantiTipologiaGeneratori.ToList().ForEach(i => i.IDLibrettoImpiantoTipologiaGeneratori = 0);
                clone.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.ToList().ForEach(i => i.IDLibrettoImpiantoTipologiaSistemaDistrubuzione = 0);
                clone.LIM_LibrettiImpiantiTipologiaSistemiEmissione.ToList().ForEach(i => i.IDLibrettoImpiantoTipologiaSistemiEmissione = 0);
                clone.LIM_LibrettiImpiantiTorriEvaporative.ToList().ForEach(i => i.IDLibrettoImpiantoTorreEvaporativa = 0);
                clone.LIM_LibrettiImpiantiTrattamentoAcquaAcs.ToList().ForEach(i => i.IDLibrettoImpiantoTrattamentoAcquaAcs = 0);
                clone.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.ToList().ForEach(i => i.IDLibrettoImpiantoTrattamentoAcquaEstiva = 0);
                clone.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.ToList().ForEach(i => i.IDLibrettoImpiantoTrattamentoAcquaInvernale = 0);
                clone.LIM_LibrettiImpiantiUnitaTrattamentoAria.ToList().ForEach(i => i.IDLibrettoImpiantoUnitaTrattamentoAria = 0);
                clone.LIM_LibrettiImpiantiValvoleRegolazione.ToList().ForEach(i => i.IDLibrettoImpiantoValvolaRegolazione = 0);
                clone.LIM_LibrettiImpiantiVasiEspansione.ToList().ForEach(i => i.IDLibrettoImpiantoVasoEspansione = 0);
                clone.IDLibrettoImpianto = 0;

                return clone;
            }
        }

        public static LIM_LibrettiImpianti RevisionaLibretto(LIM_LibrettiImpianti libretto)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                //•	Duplico testata e righe figlie
                LIM_LibrettiImpianti nuovaRevisione = ClonaLibretto(libretto);

                //•	Sul nuovo record imposto lo stato 3 (revisionato)
                nuovaRevisione.IDStatoLibrettoImpianto = 3;
                //•	Sul nuovo record imposto l'id della revisione originale
                nuovaRevisione.IDLibrettoImpiantoRevisione = libretto.IDLibrettoImpianto;
                //•	Sul nuovo record imposto il numero revisione (progressivo)
                nuovaRevisione.NumeroRevisione = ctx.LIM_LibrettiImpianti.Count(l => l.IDTargaturaImpianto == libretto.IDTargaturaImpianto && l.NumeroRevisione != null) + 1;
                //•	Sul nuovo record imposto la data di revisione (data di pressione pulsante)
                nuovaRevisione.DataRevisione = DateTime.Now;
                nuovaRevisione.IDTargaturaImpianto = libretto.IDTargaturaImpianto;
                //•	Sullo stato 3 posso editare come in bozza
                ctx.LIM_LibrettiImpianti.Add(nuovaRevisione);
                ctx.SaveChanges();

                SetDisableLibretto(libretto, ctx);

                return nuovaRevisione;
            }
        }

        public static List<V_LIM_LibrettiImpianti> StoricoRevisioniLibretti(int iDTargaturaImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var revisioni = ctx.V_LIM_LibrettiImpianti.Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.fAttivo == false).ToList();

                return revisioni;
            }
        }

        public static void RipristinaRevisione(int IDLibretto)
        {
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    var libretto = new LIM_LibrettiImpianti();
                    libretto = ctx.LIM_LibrettiImpianti.FirstOrDefault(c => c.IDLibrettoImpianto == IDLibretto);
                    int? maxRevisione = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpiantoRevisione).Select(p => p.NumeroRevisione).DefaultIfEmpty(0).Max();

                    var librettoDaRipristinare = new LIM_LibrettiImpianti();
                    librettoDaRipristinare = ctx.LIM_LibrettiImpianti.FirstOrDefault(c => c.IDLibrettoImpianto == libretto.IDLibrettoImpiantoRevisione && c.NumeroRevisione == maxRevisione);
                    librettoDaRipristinare.fAttivo = true;
                    ctx.SaveChanges();

                    ctx.LIM_LibrettiImpianti.Remove(libretto);
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public static void RipristinaRevisioneManuale(int iDLibretto, int iDTargaturaImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                foreach (var libretto in ctx.LIM_LibrettiImpianti.Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto).ToList())
                {
                    libretto.fAttivo = false;
                }
                ctx.SaveChanges();

                var librettoDaRipristinare = ctx.LIM_LibrettiImpianti.FirstOrDefault(c => c.IDLibrettoImpianto == iDLibretto);
                librettoDaRipristinare.fAttivo = true;
                ctx.SaveChanges();
            }
        }

        public static void SetDisableLibretto(LIM_LibrettiImpianti libretto, CriterDataModel ctx)
        {
            var lib = new LIM_LibrettiImpianti();
            lib = ctx.LIM_LibrettiImpianti.FirstOrDefault(i => i.IDLibrettoImpianto == libretto.IDLibrettoImpianto);
            //•	Sul record originale imposto il flag fAttivo a false
            lib.fAttivo = false;
            ctx.SaveChanges();
        }

        public static void AnnullaLibretto(int iDLibretto)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var libretto = ctx.LIM_LibrettiImpianti.FirstOrDefault(c => c.IDLibrettoImpianto == iDLibretto);
                libretto.IDStatoLibrettoImpianto = 4;
                libretto.DataAnnullamento = DateTime.Now;
                ctx.SaveChanges();

                UtilityRapportiControllo.AnnullaRapportoControlloMassive((int)libretto.IDTargaturaImpianto);
            }
        }

        public static int? GetLastLibrettoImpianto(int iDTargaturaImpianto)
        {
            int? iDLibrettoImpiantoAttivo = null;
            using (var ctx = new CriterDataModel())
            {
                var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.fAttivo == true).FirstOrDefault();
                if (libretto != null)
                {
                    iDLibrettoImpiantoAttivo = libretto.IDLibrettoImpianto;
                }

            }

            return iDLibrettoImpiantoAttivo;
        }

        public static LIM_LibrettiImpianti PresaInCaricoRevisionaLibretto(LIM_LibrettiImpianti libretto, int iDSoggetto, int iDSoggettoDerived)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                //•	Duplico testata e righe figlie
                LIM_LibrettiImpianti nuovaRevisione = ClonaLibretto(libretto);
                //•	Sul nuovo record imposto lo stato 3 (revisionato)
                nuovaRevisione.IDStatoLibrettoImpianto = 3;
                //•	Cambio azienda e soggetto
                nuovaRevisione.IDSoggetto = iDSoggetto;
                nuovaRevisione.IDSoggettoDerived = iDSoggettoDerived;
                nuovaRevisione.KeyApi = null;
                nuovaRevisione.JsonFormat = null;
                //•	Sul nuovo record imposto l'id della versione originale
                nuovaRevisione.IDLibrettoImpiantoRevisione = libretto.IDLibrettoImpianto;
                //•	Sul nuovo record imposto il numero revisione (progressivo)
                nuovaRevisione.NumeroRevisione = ctx.LIM_LibrettiImpianti.Count(l => l.IDLibrettoImpiantoRevisione == nuovaRevisione.IDLibrettoImpiantoRevisione) + 1;
                //•	Sul nuovo record imposto la data di revisione (data di pressione pulsante)
                nuovaRevisione.DataRevisione = DateTime.Now;
                //•	Sullo stato 3 posso editare come in bozza
                ctx.LIM_LibrettiImpianti.Add(nuovaRevisione);
                ctx.SaveChanges();

                var targatura = ctx.LIM_TargatureImpianti.Where(a => a.IDTargaturaImpianto == nuovaRevisione.IDTargaturaImpianto).FirstOrDefault();
                if (targatura != null)
                {
                    targatura.IDSoggetto = nuovaRevisione.IDSoggettoDerived;
                    targatura.IDSoggettoDerived = null;
                    ctx.SaveChanges();
                }

                SetDisableLibretto(libretto, ctx);

                return nuovaRevisione;
            }
        }

        public static string GetSqlValoriLibrettiResponsabiliFilter(object iDSoggettoAzienda, object iDSoggettoManutentore, object codiceTargaturaImpianto, object iDCodiceCastale, object foglio, object mappale, object subalterno, object identificativo, object iDStatoLibrettoImpianto, string responsabile, string CfPIvaResponsabile, string codicePod, string codicePdr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_LIM_LibrettiImpianti ");
            strSql.Append(" WHERE fattivo=1 ");

            if ((iDSoggettoAzienda != "") && (iDSoggettoAzienda != "-1") && (iDSoggettoAzienda != null))
            {
                strSql.Append(" AND IDLibrettoImpianto IN (SELECT DISTINCT IDLibrettoImpianto FROM LIM_LibrettiImpiantiResponsabili WHERE fAttivo=1 AND DataDichiarazioneDismissioneIncarico IS NULL AND IDSoggetto=" + iDSoggettoAzienda + ") ");
            }
            else
            {
                strSql.Append(" AND IDLibrettoImpianto IN (SELECT DISTINCT IDLibrettoImpianto FROM LIM_LibrettiImpiantiResponsabili WHERE fAttivo=1 AND DataDichiarazioneDismissioneIncarico IS NULL)");
            }

            if (codiceTargaturaImpianto.ToString() != "")
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(codiceTargaturaImpianto);
                strSql.Append("'");
            }

            if (codicePod.ToString() != "")
            {
                strSql.Append(" AND NumeroPOD = ");
                strSql.Append("'");
                strSql.Append(codicePod);
                strSql.Append("'");
            }

            if (codicePod.ToString() != "")
            {
                strSql.Append(" AND NumeroPDR = ");
                strSql.Append("'");
                strSql.Append(codicePdr);
                strSql.Append("'");
            }

            if ((iDCodiceCastale != null) || (foglio.ToString() != "") || (mappale.ToString() != "") || (subalterno.ToString() != "") || (identificativo.ToString() != ""))
            {
                strSql.Append(" AND IDLibrettoImpianto IN ");
                string sqlDatiCatastali = "";
                if (iDCodiceCastale != "")
                {
                    strSql.Append(" AND IDCodiceCastale=" + iDCodiceCastale + "");
                }
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

            if (iDStatoLibrettoImpianto.ToString() != "0")
            {
                strSql.Append(" AND IDStatoLibrettoImpianto=");
                strSql.Append(iDStatoLibrettoImpianto);
            }

            if (!string.IsNullOrEmpty(responsabile))
            {
                strSql.Append(string.Format(" AND ([NomeResponsabile] + ' '  + [CognomeResponsabile] LIKE '%{0}%' OR [RagioneSocialeResponsabile] LIKE '%{1}%') ", responsabile, responsabile));
            }

            if (!string.IsNullOrEmpty(CfPIvaResponsabile))
            {
                strSql.Append(string.Format(" AND (CodiceFiscaleResponsabile LIKE '%{0}%' OR PartitaIvaResponsabile LIKE '%{0}%') ", CfPIvaResponsabile));
            }

            strSql.Append(" ORDER BY DataInserimento DESC");
            return strSql.ToString();
        }

        public static void AssunzioneIncaricoTerzoResponsabile(int iDLibrettoImpianto, int iDSoggetto, DateTime dtInizio, DateTime dtFine)
        {
            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var librettoResponsabili = new LIM_LibrettiImpiantiResponsabili();

                var responsabile = ctx.COM_AnagraficaSoggetti.Where(s => (s.IDSoggetto == iDSoggetto)
                            ).OrderBy(s => s.IDSoggetto).FirstOrDefault();

                librettoResponsabili.IDLibrettoImpianto = iDLibrettoImpianto;
                librettoResponsabili.IDSoggetto = iDSoggetto;
                librettoResponsabili.Nome = responsabile.Nome;
                librettoResponsabili.Cognome = responsabile.Cognome;
                librettoResponsabili.CodiceFiscale = responsabile.CodiceFiscale;
                librettoResponsabili.RagioneSociale = responsabile.NomeAzienda;
                librettoResponsabili.PartitaIva = responsabile.PartitaIVA;
                librettoResponsabili.Email = responsabile.Email;
                librettoResponsabili.EmailPec = responsabile.EmailPec;
                librettoResponsabili.NumeroCciaa = responsabile.NumeroIscrizioneAlboImprese;
                librettoResponsabili.IDProvinciaCciaa = responsabile.IDProvinciaIscrizioneAlboImprese;
                librettoResponsabili.DataInizio = dtInizio;
                librettoResponsabili.DataFine = dtFine;
                librettoResponsabili.fAttivo = true;
                librettoResponsabili.DataInserimento = DateTime.Now;
                librettoResponsabili.IDUtenteInserimento = info.IDUtente;
                librettoResponsabili.DataUltimaModifica = DateTime.Now;
                librettoResponsabili.IDUtenteUltimaModifica = info.IDUtente;
                ctx.LIM_LibrettiImpiantiResponsabili.Add(librettoResponsabili);
                ctx.SaveChanges();

                if (librettoResponsabili.IDLibrettoImpianto > 0)
                {
                    var libretto = ctx.LIM_LibrettiImpianti.Find(librettoResponsabili.IDLibrettoImpianto);

                    if (libretto != null)
                    {
                        libretto.fTerzoResponsabile = true;
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public static void RecovaIncaricoTerzoResponsabile(int IDLibrettoImpianto)
        {
            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var terzoResponsabile = ctx.LIM_LibrettiImpiantiResponsabili.Where(s => s.fAttivo == true && s.IDLibrettoImpianto == IDLibrettoImpianto).OrderBy(s => s.IDLibrettoImpiantoResponsabili).FirstOrDefault();

                if (terzoResponsabile != null)
                {
                    terzoResponsabile.DataDichiarazioneDismissioneIncarico = DateTime.Now;
                    terzoResponsabile.IDUtenteDichiarazioneDismissioneIncarico = info.IDUtente;
                    terzoResponsabile.fAttivo = false;
                    ctx.SaveChanges();

                    var libretto = ctx.LIM_LibrettiImpianti.Find(terzoResponsabile.IDLibrettoImpianto);

                    if (libretto != null)
                    {
                        libretto.fTerzoResponsabile = false;
                        ctx.SaveChanges();
                    }
                }

            }
        }

        public static List<V_LIM_LibrettiImpiantiDatiCatastali> GetValoriDatiCatastali(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.V_LIM_LibrettiImpiantiDatiCatastali.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDLibrettoImpiantoDatiCatastali).ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiTipologiaFluidoVettore> GetValoriLibrettoImpiantoTipologiaFluidoVettore(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiTipologiaFluidoVettore.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaFluidoVettore).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiTipologiaGeneratori> GetValoriLibrettoImpiantoTipologiaGeneratori(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiTipologiaGeneratori.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaGeneratori).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiTrattamentoAcquaInvernale> GetValoriLibrettoImpiantoTrattamentoAcquaInvernale(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiTrattamentoAcquaInvernale.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaTrattamentoAcqua).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiTrattamentoAcquaAcs> GetValoriLibrettoImpiantoTrattamentoAcquaAcs(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiTrattamentoAcquaAcs.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaTrattamentoAcqua).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiTrattamentoAcquaEstiva> GetValoriLibrettoImpiantoTrattamentoAcquaEstiva(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiTrattamentoAcquaEstiva.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaTrattamentoAcqua).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiTipologiaFiltrazioni> GetValoriLibrettoImpiantoTipologiaFiltrazioni(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiTipologiaFiltrazioni.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaFiltrazione).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiAddolcimentoAcqua> GetValoriLibrettoImpiantoTipologiaAddolcimentoAcqua(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiAddolcimentoAcqua.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaAddolcimentoAcqua).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiCondizionamentoChimico> GetValoriLibrettoImpiantoTipologiaCondizionamentoChimico(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiCondizionamentoChimico.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaCondizionamentoChimico).Distinct().ToList();

                return result;
            }
        }

        public static List<LIM_LibrettiImpiantiTipologiaSistemaDistribuzione> GetValoriLibrettoImpiantoTipologiaSistemaDistribuzione(int iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).OrderBy(s => s.IDTipologiaSistemaDistribuzione).Distinct().ToList();

                return result;
            }
        }

        public static object[] CheckComuneFoglioParticellaSub(string IdCodiceCatastaleComune, string Foglio, string Mappa, string Sub, string Identificativo, string IDSezione, string iDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                object[] outVal = new object[2];
                outVal[0] = false;          //fEsiste
                outVal[1] = string.Empty;   //codice targatura

                var query = ctx.LIM_LibrettiImpiantiDatiCatastali.AsQueryable();
                query = query.Where(d => d.LIM_LibrettiImpianti.fAttivo == true);
                query = query.Where(d => d.LIM_LibrettiImpianti.IDStatoLibrettoImpianto != 4);
                if (IdCodiceCatastaleComune != "")
                {
                    int IdCodiceCatastale = Int32.Parse(IdCodiceCatastaleComune);
                    query = query.Where(d => d.LIM_LibrettiImpianti.IDCodiceCatastale == IdCodiceCatastale);
                }
                if (Foglio != "")
                {
                    query = query.Where(d => d.Foglio == Foglio);
                }
                if (Mappa != "")
                {
                    query = query.Where(d => d.Mappale == Mappa);
                }
                if (Sub != "")
                {
                    query = query.Where(d => d.Subalterno == Sub);
                }
                if (Identificativo != "")
                {
                    query = query.Where(d => d.Identificativo == Identificativo);
                }
                if (IDSezione != "")
                {
                    int IdSezione = int.Parse(IDSezione);
                    query = query.Where(d => d.IDCodiceCatastaleSezione == IdSezione);
                }

                if (!string.IsNullOrEmpty(iDLibrettoImpianto))
                {
                    int iDLibrettoImpiantoInt = int.Parse(iDLibrettoImpianto);
                    query = query.Where(d => d.LIM_LibrettiImpianti.IDLibrettoImpianto != iDLibrettoImpiantoInt);
                }

                if (query.Any())
                {
                    if (string.IsNullOrEmpty(iDLibrettoImpianto))
                    {
                        var queryTargature = query.FirstOrDefault().LIM_LibrettiImpianti.LIM_TargatureImpianti;
                        if (queryTargature != null)
                        {
                            outVal[0] = true;
                            outVal[1] = queryTargature.CodiceTargatura;
                        }
                    }
                }

                return outVal;
            }
        }

        public static bool ChangefAttivo(int iDLibrettoImpianto, bool fAttivo)
        {
            bool fAttivoLibretto = false;
            using (var ctx = new CriterDataModel())
            {
                var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
                if (fAttivo)
                {
                    libretto.fAttivo = false;
                    libretto.IDTargaturaImpianto = null;
                    fAttivoLibretto = false;
                }
                else
                {
                    libretto.fAttivo = true;
                    fAttivoLibretto = true;
                }

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                return fAttivoLibretto;
            }
        }

        public static bool DeleteLibrettoImpianto(int IDLibrettoImpianto)
        {
            bool fAttivoLibretto = false;
            
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == IDLibrettoImpianto).FirstOrDefault();
                    ctx.LIM_LibrettiImpianti.Remove(libretto);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return fAttivoLibretto;
        }

        public static void DismettiGeneratore(int iDGeneratore, int iDUtente, bool fDismesso, string tipoGeneratore)
        {
            using (var ctx = new CriterDataModel())
            {
                switch (tipoGeneratore)
                {
                    case "GT":
                        var gt = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpiantoGruppoTermico == iDGeneratore).FirstOrDefault();
                        if (fDismesso)
                        {
                            gt.fDismesso = true;
                            gt.DataDismesso = DateTime.Now;
                            gt.IDUtenteDismesso = iDUtente;
                        }
                        else
                        {
                            gt.fDismesso = false;
                            gt.DataDismesso = null;
                            gt.IDUtenteDismesso = null;
                        }
                        break;
                    case "GF":
                        var gf = ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Where(c => c.IDLibrettoImpiantoMacchinaFrigorifera == iDGeneratore).FirstOrDefault();
                        if (fDismesso)
                        {
                            gf.fDismesso = true;
                            gf.DataDismesso = DateTime.Now;
                            gf.IDUtenteDismesso = iDUtente;
                        }
                        else
                        {
                            gf.fDismesso = false;
                            gf.DataDismesso = null;
                            gf.IDUtenteDismesso = null;
                        }
                        break;
                    case "SC":
                        var sc = ctx.LIM_LibrettiImpiantiScambiatoriCalore.Where(c => c.IDLibrettoImpiantoScambiatoreCalore == iDGeneratore).FirstOrDefault();
                        if (fDismesso)
                        {
                            sc.fDismesso = true;
                            sc.DataDismesso = DateTime.Now;
                            sc.IDUtenteDismesso = iDUtente;
                        }
                        else
                        {
                            sc.fDismesso = false;
                            sc.DataDismesso = null;
                            sc.IDUtenteDismesso = null;
                        }
                        break;
                    case "CG":
                        var cg = ctx.LIM_LibrettiImpiantiCogeneratori.Where(c => c.IDLibrettoImpiantoCogeneratore == iDGeneratore).FirstOrDefault();
                        if (fDismesso)
                        {
                            cg.fDismesso = true;
                            cg.DataDismesso = DateTime.Now;
                            cg.IDUtenteDismesso = iDUtente;
                        }
                        else
                        {
                            cg.fDismesso = false;
                            cg.DataDismesso = null;
                            cg.IDUtenteDismesso = null;
                        }
                        break;
                }

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }

        #region Verifiche Periodiche
        protected static string FormatDecimal(decimal? x)
        {
            return (x.HasValue) ? x.Value.ToString("#.##") : "...";
        }

        public static List<int> ElencaGeneratori<T>(int iDLibrettoImpianto) where T : class, new()
        {
            using (var ctx = new CriterDataModel())
            {
                if (typeof(T) == typeof(LIM_LibrettiImpiantiGruppiTermici))
                {
                    return ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => (c.IDLibrettoImpianto == iDLibrettoImpianto || c.IDLibrettoImpiantoInserimento == iDLibrettoImpianto) && c.fAttivo == true).Select(c => c.IDLibrettoImpiantoGruppoTermico).ToList();
                }

                if (typeof(T) == typeof(LIM_LibrettiImpiantiMacchineFrigorifere))
                {
                    return ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Where(c => (c.IDLibrettoImpianto == iDLibrettoImpianto || c.IDLibrettoImpiantoInserimento == iDLibrettoImpianto) && c.fAttivo == true).Select(c => c.IDLibrettoImpiantoMacchinaFrigorifera).ToList();
                }

                if (typeof(T) == typeof(LIM_LibrettiImpiantiScambiatoriCalore))
                {
                    return ctx.LIM_LibrettiImpiantiScambiatoriCalore.Where(c => (c.IDLibrettoImpianto == iDLibrettoImpianto || c.IDLibrettoImpiantoInserimento == iDLibrettoImpianto) && c.fAttivo == true).Select(c => c.IDLibrettoImpiantoScambiatoreCalore).ToList();
                }

                if (typeof(T) == typeof(LIM_LibrettiImpiantiCogeneratori))
                {
                    return ctx.LIM_LibrettiImpiantiCogeneratori.Where(c => (c.IDLibrettoImpianto == iDLibrettoImpianto || c.IDLibrettoImpiantoInserimento == iDLibrettoImpianto) && c.fAttivo == true).Select(c => c.IDLibrettoImpiantoCogeneratore).ToList();
                }
            }

            return null;
        }

        public static DataTable dtVerificheGT(int iDTargaturaImpianto, int IDGeneratore)
        {
            DataTable dt = new DataTable("DataTableDS");

            dt.Columns.Add("Id");
            dt.Columns.Add("DataControllo");
            dt.Columns.Add("Numero modulo");
            dt.Columns.Add("Portata termica effettiva (kW)");
            dt.Columns.Add("Temperatura fumi (°C)");
            dt.Columns.Add("Temperaratura aria comburente (°C)");
            dt.Columns.Add("O2 (%)");
            dt.Columns.Add("CO2 (%)");
            dt.Columns.Add("Indice di Bachahach");
            dt.Columns.Add("CO fumi secchi (ppm v/v)");
            dt.Columns.Add("Portata combustibile (m3/h oppure kg/h))");
            dt.Columns.Add("CO nei fumi secchi e senz'aria (ppm v/v)");
            dt.Columns.Add("Rendimento di combustione (%)");
            dt.Columns.Add("Rispetta l'indice di Bacharach");
            dt.Columns.Add("CO fumi secchi e senz'aria <= 1000 ppm v/v");
            dt.Columns.Add("Rendimento minimo di legge (%)");
            dt.Columns.Add("Rendimendo supera il minimo");

            using (var ctx = new CriterDataModel())
            {
                List<DateTime> dtList = new List<DateTime>();

                var collection = ctx.V_LIM_VerifichePeriodicheGT.ToList().Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.IDLIM_LibrettiImpiantiGruppitermici == IDGeneratore && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null);

                foreach (V_LIM_VerifichePeriodicheGT v in collection)
                {
                    if (!dtList.Contains(v.DataControllo.Value))
                    {
                        dtList.Add(v.DataControllo.Value);

                        string s1 = v.bacharach1 + " / " + v.bacharach2 + " / " + v.bacharach3;

                        dt.Rows.Add(new object[]
                        {
                            v.Id,
                            v.DataControllo.Value.ToShortDateString(),
                            v.ModuloTermico,
                            v.PotenzaTermicaEffettiva,
                            v.TemperaturaFumi,
                            v.TemperaraturaComburente,
                            v.O2,
                            v.Co2,
                            s1,
                            v.COFumiSecchi,
                            v.PortataCombustibile,
                            v.CoCorretto,
                            v.RendimentoCombustione,
                            (v.RispettaIndiceBacharach) ? "SI" : "NO",
                            (v.COFumiSecchiNoAria1000) ? "SI" : "NO",
                            v.RendimentoMinimo,
                            (v.RendimentoSupMinimo) ? "SI" : "NO"
                        });
                    }
                }
            }

            return dt;
        }

        public static IEnumerable ListVerificheGT(int iDTargaturaImpianto, string prefisso)
        {
            using (var ctx = new CriterDataModel())
            {
                var rapporto = from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                               join RCT_RapportoDiControlloTecnicoGT in ctx.RCT_RapportoDiControlloTecnicoGT
                               on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico }
                               equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoGT.Id }
                               where
                                 RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto == iDTargaturaImpianto &&
                                 RCT_RapportoDiControlloTecnicoBase.Prefisso == prefisso &&
                                 RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo == 2 &&
                                 RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo == 1 &&
                                 RCT_RapportoDiControlloTecnicoBase.DataControllo != null
                               select new
                               {
                                   Id = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico,
                                   RCT_RapportoDiControlloTecnicoBase.IDLibrettoImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggetto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggettoDerived,
                                   RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo,
                                   RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo,
                                   RCT_RapportoDiControlloTecnicoBase.DataControllo,
                                   RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaEffettiva,
                                   RCT_RapportoDiControlloTecnicoGT.ModuloTermico,
                                   RCT_RapportoDiControlloTecnicoGT.TemperaturaFumi,
                                   RCT_RapportoDiControlloTecnicoGT.TemperaraturaComburente,
                                   RCT_RapportoDiControlloTecnicoGT.O2,
                                   RCT_RapportoDiControlloTecnicoGT.Co2,
                                   RCT_RapportoDiControlloTecnicoGT.bacharach1,
                                   RCT_RapportoDiControlloTecnicoGT.bacharach2,
                                   RCT_RapportoDiControlloTecnicoGT.bacharach3,
                                   RCT_RapportoDiControlloTecnicoGT.COFumiSecchi,
                                   RCT_RapportoDiControlloTecnicoGT.CoCorretto,
                                   RCT_RapportoDiControlloTecnicoGT.PortataCombustibile,
                                   RCT_RapportoDiControlloTecnicoGT.RendimentoCombustione,
                                   RCT_RapportoDiControlloTecnicoGT.RendimentoMinimo,
                                   RCT_RapportoDiControlloTecnicoGT.RispettaIndiceBacharach,
                                   RCT_RapportoDiControlloTecnicoGT.COFumiSecchiNoAria1000,
                                   RCT_RapportoDiControlloTecnicoGT.RendimentoSupMinimo,
                                   RCT_RapportoDiControlloTecnicoGT.IDLIM_LibrettiImpiantiGruppitermici,
                                   RCT_RapportoDiControlloTecnicoBase.Prefisso,
                                   RCT_RapportoDiControlloTecnicoBase.CodiceProgressivo
                               };

                //var collection = ctx.V_LIM_VerifichePeriodicheGT.Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.Prefisso == prefisso && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null).ToList();
                //return collection;
                return rapporto.ToList();
            }
        }

        public static DataTable dtVerificheGF(int iDTargaturaImpianto, int IDGeneratore)
        {
            DataTable dt = new DataTable("DataTableDS");
            dt.Columns.Add("Id");
            dt.Columns.Add("DataControllo");
            dt.Columns.Add("Numreo circuito");
            dt.Columns.Add("Assenza perdite refrigerante");
            dt.Columns.Add("Modalità di funzionamento");
            dt.Columns.Add("ProvaRiscaldamento");
            dt.Columns.Add("Surriscaldamento (K)");
            dt.Columns.Add("Sottoraffreddamento (K)");
            dt.Columns.Add("T Condensazione (°C)");
            dt.Columns.Add("T Evaporazione (°C)");
            dt.Columns.Add("T sorgente ingresso lato esterno (°C)");
            dt.Columns.Add("T sorgente uscita lato esterno (°C)");
            dt.Columns.Add("T ingresso fluido utenze (°C)");
            dt.Columns.Add("T uscita fluido utenze (°C)");
            dt.Columns.Add("T uscita fluido (°C)");
            dt.Columns.Add("T bulbo umido aria (°C)");
            dt.Columns.Add("T ingresso fluido sorgente esterna (°C)");
            dt.Columns.Add("T uscita fluido sorgente esterna (°C)");
            dt.Columns.Add("T ingresso fluido alla macchina (°C)");
            dt.Columns.Add("T uscita fluido alla macchina (°C)");
            dt.Columns.Add("Potenza assorbita (kW)");
            dt.Columns.Add("Filtri puliti");

            using (var ctx = new CriterDataModel())
            {
                List<DateTime> dtList = new List<DateTime>();

                var collection = ctx.V_LIM_VerifichePeriodicheGF.ToList().Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.IdLIM_LibrettiImpiantiMacchineFrigorifere == IDGeneratore && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null);

                foreach (V_LIM_VerifichePeriodicheGF v in collection)
                {
                    if (!dtList.Contains(v.DataControllo.Value))
                    {
                        dtList.Add(v.DataControllo.Value);

                        string s1 = "-";
                        if (v.AssenzaPerditeRefrigerante.HasValue)
                        {
                            s1 = (v.AssenzaPerditeRefrigerante.Value == 1) ? "SI" : "NO";
                        }

                        string s2 = (v.ProvaRaffrescamento) ? "Raff" : "Risc";

                        string s3 = "-";
                        if (v.FiltriPuliti.HasValue)
                        {
                            s3 = (v.FiltriPuliti.Value == 1) ? "SI" : "NO";
                        }

                        dt.Rows.Add(new object[]
                        {
                            v.Id,
                            v.DataControllo.Value.ToShortDateString(),
                            v.NCircuiti,
                            s1,
                            s2,
                            v.TemperaturaSurriscaldamento,
                            v.TemperaturaSottoraffreddamento,
                            v.TemperaturaCondensazione,
                            v.TemperaturaEvaporazione,
                            v.TInglatoEst,
                            v.TUscLatoEst,
                            v.TIngLatoUtenze,
                            v.TUscLatoUtenze,
                            v.TUscitaFluido,
                            v.TBulboUmidoAria,
                            v.TIngressoLatoEsterno,
                            v.TUscitaLatoEsterno,
                            v.TIngressoLatoMacchina,
                            v.TUscitaLatoMacchina,
                            v.PotenzaAssorbita,
                            s3
                        });
                    }
                }
            }

            return dt;
        }

        public static IEnumerable ListVerificheGF(int iDTargaturaImpianto, string prefisso)
        {
            using (var ctx = new CriterDataModel())
            {
                var rapporto = from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                               join RCT_RapportoDiControlloTecnicoGF in ctx.RCT_RapportoDiControlloTecnicoGF on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoGF.Id }
                               where
                                 RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto == iDTargaturaImpianto &&
                                 RCT_RapportoDiControlloTecnicoBase.Prefisso == prefisso &&
                                 RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo == 2 &&
                                 RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo == 1 &&
                                 RCT_RapportoDiControlloTecnicoBase.DataControllo != null
                               select new
                               {
                                   Id = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico,
                                   RCT_RapportoDiControlloTecnicoBase.IDLibrettoImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggetto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggettoDerived,
                                   RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo,
                                   RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo,
                                   RCT_RapportoDiControlloTecnicoBase.DataControllo,
                                   RCT_RapportoDiControlloTecnicoGF.NCircuiti,
                                   RCT_RapportoDiControlloTecnicoGF.AssenzaPerditeRefrigerante,
                                   RCT_RapportoDiControlloTecnicoGF.ProvaRaffrescamento,
                                   RCT_RapportoDiControlloTecnicoGF.ProvaRiscaldamento,
                                   RCT_RapportoDiControlloTecnicoGF.TemperaturaSurriscaldamento,
                                   RCT_RapportoDiControlloTecnicoGF.TemperaturaSottoraffreddamento,
                                   RCT_RapportoDiControlloTecnicoGF.TemperaturaCondensazione,
                                   RCT_RapportoDiControlloTecnicoGF.TemperaturaEvaporazione,
                                   RCT_RapportoDiControlloTecnicoGF.TInglatoEst,
                                   RCT_RapportoDiControlloTecnicoGF.TUscLatoEst,
                                   RCT_RapportoDiControlloTecnicoGF.TIngLatoUtenze,
                                   RCT_RapportoDiControlloTecnicoGF.TUscLatoUtenze,
                                   RCT_RapportoDiControlloTecnicoGF.TUscitaFluido,
                                   RCT_RapportoDiControlloTecnicoGF.TBulboUmidoAria,
                                   RCT_RapportoDiControlloTecnicoGF.TIngressoLatoEsterno,
                                   RCT_RapportoDiControlloTecnicoGF.TUscitaLatoEsterno,
                                   RCT_RapportoDiControlloTecnicoGF.TIngressoLatoMacchina,
                                   RCT_RapportoDiControlloTecnicoGF.TUscitaLatoMacchina,
                                   RCT_RapportoDiControlloTecnicoGF.PotenzaAssorbita,
                                   RCT_RapportoDiControlloTecnicoGF.FiltriPuliti,
                                   RCT_RapportoDiControlloTecnicoGF.IdLIM_LibrettiImpiantiMacchineFrigorifere,
                                   RCT_RapportoDiControlloTecnicoBase.Prefisso,
                                   RCT_RapportoDiControlloTecnicoBase.CodiceProgressivo
                               };

                return rapporto.ToList();

                //var collection = ctx.V_LIM_VerifichePeriodicheGF.Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.Prefisso == prefisso && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null).ToList();
                //return collection;
            }
        }

        public static DataTable dtVerificheCG(int iDTargaturaImpianto, int IDGeneratore)
        {
            DataTable dt = new DataTable("DataTableDS");

            dt.Columns.Add("Id");
            dt.Columns.Add("DataControllo");
            dt.Columns.Add("Temperatura aria comburente (°C)");
            dt.Columns.Add("Temperatura acqua in uscita (°C)");
            dt.Columns.Add("Temperatura acqua in ingresso (°C)");
            dt.Columns.Add("Temperatura acqua motore (molo m.c.i) (°C)");
            dt.Columns.Add("Temperatura fumi a valle dello scambiatore di fumi (°C)");
            dt.Columns.Add("Temperatura fumi a monte dello scambiatore di fumi (°C)");
            dt.Columns.Add("Potenza elettrica ai morsetti (kW)");
            dt.Columns.Add("Emissioni di monossido di carbonio CO (mg/Nm3 riportati al 5% di O2 nei fumi)");
            dt.Columns.Add("Sovrafrequenza: soglia intervento (Hz)");
            //dt.Columns.Add("SovrafrequenzaSogliaInterv2");
            //dt.Columns.Add("SovrafrequenzaSogliaInterv3");
            dt.Columns.Add("Sovrafrequenza: tempo intervento (s)");
            //dt.Columns.Add("SovrafrequenzaTempoInterv2");
            //dt.Columns.Add("SovrafrequenzaTempoInterv3");
            dt.Columns.Add("Sottofrequenza: soglia intervento (Hz)");
            //dt.Columns.Add("SottofrequenzaSogliaInterv2");
            //dt.Columns.Add("SottofrequenzaSogliaInterv3");
            dt.Columns.Add("Sottofrequenza: tempo intervento (s)");
            //dt.Columns.Add("SottofrequenzaTempoInterv2");
            //dt.Columns.Add("SottofrequenzaTempoInterv3");
            dt.Columns.Add("Sovratensione: soglia intervento (V)");
            //dt.Columns.Add("SovratensioneSogliaInterv2");
            //dt.Columns.Add("SovratensioneSogliaInterv3");
            dt.Columns.Add("Sovratensione: tempo intervento (s)");
            //dt.Columns.Add("SovratensioneTempoInterv2");
            //dt.Columns.Add("SovratensioneTempoInterv3");
            dt.Columns.Add("Sottotensione: soglia intervento (V)");
            //dt.Columns.Add("SottotensioneSogliaInterv2");
            //dt.Columns.Add("SottotensioneSogliaInterv3");
            dt.Columns.Add("Sottotensione: tempo intervento (s)");
            //dt.Columns.Add("SottotensioneTempoInterv2");
            //dt.Columns.Add("SottotensioneTempoInterv3");

            using (var ctx = new CriterDataModel())
            {
                List<DateTime> dtList = new List<DateTime>();

                var collection = ctx.V_LIM_VerifichePeriodicheCG.ToList().Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.IdLIM_LibrettiImpiantiCogeneratori == IDGeneratore && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null); //

                foreach (V_LIM_VerifichePeriodicheCG v in collection)
                {
                    if (!dtList.Contains(v.DataControllo.Value))
                    {
                        dtList.Add(v.DataControllo.Value);

                        string s1 = FormatDecimal(v.SovrafrequenzaSogliaInterv1) + " / " + FormatDecimal(v.SovrafrequenzaSogliaInterv2) + " / " + FormatDecimal(v.SovrafrequenzaSogliaInterv3);
                        string s2 = FormatDecimal(v.SovrafrequenzaTempoInterv1) + " / " + FormatDecimal(v.SovrafrequenzaTempoInterv2) + " / " + FormatDecimal(v.SovrafrequenzaTempoInterv3);
                        string s3 = FormatDecimal(v.SottofrequenzaSogliaInterv1) + " / " + FormatDecimal(v.SottofrequenzaSogliaInterv2) + " / " + FormatDecimal(v.SottofrequenzaSogliaInterv3);
                        string s4 = FormatDecimal(v.SottofrequenzaTempoInterv1) + " / " + FormatDecimal(v.SottofrequenzaTempoInterv2) + " / " + FormatDecimal(v.SottofrequenzaTempoInterv3);
                        string s5 = FormatDecimal(v.SovratensioneSogliaInterv1) + " / " + FormatDecimal(v.SovratensioneSogliaInterv2) + " / " + FormatDecimal(v.SovratensioneSogliaInterv3);
                        string s6 = FormatDecimal(v.SovratensioneTempoInterv1) + " / " + FormatDecimal(v.SovratensioneTempoInterv2) + " / " + FormatDecimal(v.SovratensioneTempoInterv3);
                        string s7 = FormatDecimal(v.SottotensioneSogliaInterv1) + " / " + FormatDecimal(v.SottotensioneSogliaInterv2) + " / " + FormatDecimal(v.SottotensioneSogliaInterv3);
                        string s8 = FormatDecimal(v.SottotensioneTempoInterv1) + " / " + FormatDecimal(v.SottotensioneTempoInterv2) + " / " + FormatDecimal(v.SottotensioneTempoInterv3);


                        dt.Rows.Add(new object[]
                        {
                            v.Id,
                            v.DataControllo.Value.ToShortDateString(),
                            v.TemperaturaAriaComburente,
                            v.TemperaturaAcquauscita,
                            v.TemperaturaAcquaIngresso,
                            v.TemperaturaAcquaMotore,
                            v.TemperaturaFumiValle,
                            v.TemperaturaFumiMonte,
                            v.PotenzaAiMorsetti,
                            v.EmissioneMonossido,
                            s1,
                            s2,
                            s3,
                            s4,
                            s5,
                            s6,
                            s7,
                            s8

                            //v.SovrafrequenzaSogliaInterv1,
                            //v.SovrafrequenzaSogliaInterv2,
                            //v.SovrafrequenzaSogliaInterv3,
                            //v.SovrafrequenzaTempoInterv1,
                            //v.SovrafrequenzaTempoInterv2,
                            //v.SovrafrequenzaTempoInterv3,
                            //v.SottofrequenzaSogliaInterv1,
                            //v.SottofrequenzaSogliaInterv2,
                            //v.SottofrequenzaSogliaInterv3,
                            //v.SottofrequenzaTempoInterv1,
                            //v.SottofrequenzaTempoInterv2,
                            //v.SottofrequenzaTempoInterv3,
                            //v.SovratensioneSogliaInterv1,
                            //v.SovratensioneSogliaInterv2,
                            //v.SovratensioneSogliaInterv3,
                            //v.SovratensioneTempoInterv1,
                            //v.SovratensioneTempoInterv2,
                            //v.SovratensioneTempoInterv3,
                            //v.SottotensioneSogliaInterv1,
                            //v.SottotensioneSogliaInterv2,
                            //v.SottotensioneSogliaInterv3,
                            //v.SottotensioneTempoInterv1,
                            //v.SottotensioneTempoInterv2,
                            //v.SottotensioneTempoInterv3
                        });

                    }
                }
            }

            return dt;
        }

        public static IEnumerable ListVerificheCG(int iDTargaturaImpianto, string prefisso)
        {
            using (var ctx = new CriterDataModel())
            {
                var rapporto = from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                               join RCT_RapportoDiControlloTecnicoCG in ctx.RCT_RapportoDiControlloTecnicoCG on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoCG.Id }
                               where
                                 RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto == iDTargaturaImpianto &&
                                 RCT_RapportoDiControlloTecnicoBase.Prefisso == prefisso &&
                                 RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo == 2 &&
                                 RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo == 1 &&
                                 RCT_RapportoDiControlloTecnicoBase.DataControllo != null
                               select new
                               {
                                   Id = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico,
                                   RCT_RapportoDiControlloTecnicoBase.IDLibrettoImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggetto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggettoDerived,
                                   RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo,
                                   RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo,
                                   RCT_RapportoDiControlloTecnicoBase.DataControllo,
                                   RCT_RapportoDiControlloTecnicoCG.TemperaturaAriaComburente,
                                   RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquauscita,
                                   RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquaIngresso,
                                   RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquaMotore,
                                   RCT_RapportoDiControlloTecnicoCG.TemperaturaFumiValle,
                                   RCT_RapportoDiControlloTecnicoCG.TemperaturaFumiMonte,
                                   RCT_RapportoDiControlloTecnicoCG.PotenzaAiMorsetti,
                                   RCT_RapportoDiControlloTecnicoCG.EmissioneMonossido,
                                   RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv1,
                                   RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv2,
                                   RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv3,
                                   RCT_RapportoDiControlloTecnicoCG.IdLIM_LibrettiImpiantiCogeneratori,
                                   RCT_RapportoDiControlloTecnicoBase.Prefisso,
                                   RCT_RapportoDiControlloTecnicoBase.CodiceProgressivo
                               };

                return rapporto.ToList();

                //var collection = ctx.V_LIM_VerifichePeriodicheCG.Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.Prefisso == prefisso && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null).ToList();
                //return collection;
            }
        }

        public static DataTable dtVerificheSC(int iDTargaturaImpianto, int IDGeneratore)
        {
            DataTable dt = new DataTable("DataTableDS");

            dt.Columns.Add("Id");
            dt.Columns.Add("DataControllo");
            dt.Columns.Add("Temperatura esterna (°C)");
            dt.Columns.Add("Temperatura mandata primario (°C)");
            dt.Columns.Add("Temperatura ritorno primario (°C)");
            dt.Columns.Add("Temperatura mandata secondario (°C)");
            dt.Columns.Add("Temperatura ritorno secondario (°C)");
            dt.Columns.Add("Portata fluido primario (m3/h)");
            dt.Columns.Add("Potenza termica nominale totale (kW)");
            dt.Columns.Add("Potenza compatibile con i dati di progetto");
            dt.Columns.Add("Stato delle coibentazioni idoneo");
            dt.Columns.Add("Dispositifi di regolazione e controllo");

            using (var ctx = new CriterDataModel())
            {
                List<DateTime> dtList = new List<DateTime>();

                var collection = ctx.V_LIM_VerifichePeriodicheSC.ToList().Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.IdLIM_LibrettiImpiantiScambaitoriCalore == IDGeneratore && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null);

                foreach (V_LIM_VerifichePeriodicheSC v in collection)
                {
                    if (!dtList.Contains(v.DataControllo.Value))
                    {
                        dtList.Add(v.DataControllo.Value);

                        string s1 = "NC";
                        if (v.PotenzaCompatibileProgetto.HasValue)
                        {
                            s1 = (v.PotenzaCompatibileProgetto.Value == 1) ? "SI" : "NO";
                        }

                        string s2 = "NC";
                        if (v.CoibentazioniIdonee.HasValue)
                        {
                            s2 = (v.CoibentazioniIdonee.Value == 1) ? "SI" : "NO";
                        }

                        string s3 = "NC";
                        if (v.AssenzaTrafilamenti.HasValue)
                        {
                            s3 = (v.AssenzaTrafilamenti.Value == 1) ? "SI" : "NO";
                        }

                        dt.Rows.Add(new object[]
                        {
                            v.Id,
                            v.DataControllo.Value.ToShortDateString(),
                            v.TemperaturaEsterna,
                            v.TemperaturaMandataPrimario,
                            v.TemperaturaRitornoPrimario,
                            v.TemperaturaMandataSecondario,
                            v.TemperaturaRitornoSecondario,
                            v.PortataFluidoPrimario,
                            v.PotenzaTermica,
                            s1,
                            s2,
                            s3
                        });
                    }
                }
            }

            return dt;
        }

        public static IEnumerable ListVerificheSC(int iDTargaturaImpianto, string prefisso)
        {
            using (var ctx = new CriterDataModel())
            {
                var rapporto = from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                               join RCT_RapportoDiControlloTecnicoSC in ctx.RCT_RapportoDiControlloTecnicoSC on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoSC.Id }
                               where
                                 RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto == iDTargaturaImpianto &&
                                 RCT_RapportoDiControlloTecnicoBase.Prefisso == prefisso &&
                                 RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo == 2 &&
                                 RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo == 1 &&
                                 RCT_RapportoDiControlloTecnicoBase.DataControllo != null
                               select new
                               {
                                   Id = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico,
                                   RCT_RapportoDiControlloTecnicoBase.IDLibrettoImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDTargaturaImpianto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggetto,
                                   RCT_RapportoDiControlloTecnicoBase.IDSoggettoDerived,
                                   RCT_RapportoDiControlloTecnicoBase.IDTipologiaControllo,
                                   RCT_RapportoDiControlloTecnicoBase.IDStatoRapportoDiControllo,
                                   RCT_RapportoDiControlloTecnicoBase.DataControllo,
                                   RCT_RapportoDiControlloTecnicoSC.TemperaturaEsterna,
                                   RCT_RapportoDiControlloTecnicoSC.TemperaturaMandataPrimario,
                                   RCT_RapportoDiControlloTecnicoSC.TemperaturaRitornoPrimario,
                                   RCT_RapportoDiControlloTecnicoSC.TemperaturaMandataSecondario,
                                   RCT_RapportoDiControlloTecnicoSC.TemperaturaRitornoSecondario,
                                   RCT_RapportoDiControlloTecnicoSC.PortataFluidoPrimario,
                                   RCT_RapportoDiControlloTecnicoSC.PotenzaTermica,
                                   RCT_RapportoDiControlloTecnicoSC.PotenzaCompatibileProgetto,
                                   RCT_RapportoDiControlloTecnicoBase.CoibentazioniIdonee,
                                   RCT_RapportoDiControlloTecnicoSC.AssenzaTrafilamenti,
                                   RCT_RapportoDiControlloTecnicoSC.IdLIM_LibrettiImpiantiScambaitoriCalore,
                                   RCT_RapportoDiControlloTecnicoBase.Prefisso,
                                   RCT_RapportoDiControlloTecnicoBase.CodiceProgressivo
                               };

                return rapporto.ToList();

                //var collection = ctx.V_LIM_VerifichePeriodicheSC.Where(c => c.IDTargaturaImpianto == iDTargaturaImpianto && c.Prefisso == prefisso && c.IDStatoRapportoDiControllo == 2 && c.IDTipologiaControllo == 1 && c.DataControllo != null).ToList();
                //return collection;
            }
        }

        public static IEnumerable ListVerificheInterventiControlloEfficienzaEnergetica(int iDTargaturaImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var rapportiEfficienzaEnergetica = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c =>
                                                       !string.IsNullOrEmpty(c.PartitaIVAImpresaManutentrice)
                                                       && c.LIM_LibrettiImpianti != null
                                                       && c.IDTargaturaImpianto == iDTargaturaImpianto
                                                       && c.IDTipologiaControllo == 1
                                                       && c.IDStatoRapportoDiControllo == 2)
                                                       .Select(c => new
                                                       {
                                                           c.IDRapportoControlloTecnico,
                                                           c.DataControllo,
                                                           c.RagioneSocialeImpresaManutentrice,
                                                           c.Raccomandazioni,
                                                           c.Prescrizioni,
                                                           NumeroIscrizioneAlboImprese = ctx.COM_AnagraficaSoggetti.FirstOrDefault(aa => aa.PartitaIVA == c.PartitaIVAImpresaManutentrice).NumeroIscrizioneAlboImprese
                                                       }).OrderBy(c => c.DataControllo).ToList();


                return rapportiEfficienzaEnergetica;
            }
        }

        public static IEnumerable ListVerificheIspettiveLibretto(int IDTargaturaImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var ListStatiIspezione = new List<int>() { 5, 9, 10, 11 };

                var ispezioni = ctx.V_VER_Ispezioni.AsNoTracking().Where(c =>
                                                       c.IDTargaturaImpianto == IDTargaturaImpianto
                                                       && ListStatiIspezione.Contains(c.IDStatoIspezione))
                                                       .Select(c => new
                                                       {
                                                           c.IDTargaturaImpianto,   
                                                           c.IDIspezione,
                                                           c.IDIspezioneVisita,
                                                           c.IDIspezioneVisitaInfo,
                                                           c.CodiceIspezione,
                                                           c.DataIspezione,
                                                           c.Ispettore,
                                                           c.Prefisso,
                                                           c.CodiceProgressivo
                                                       }).OrderBy(c => c.DataIspezione).ToList();

                return ispezioni;
            }
        }

        #endregion

        public static void UpdateCap(int iDComune, string cap)
        {
            using (var ctx = new CriterDataModel())
            {
                var GetComune = ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == iDComune && c.Cap == null && c.fAttivo == true).FirstOrDefault();

                if (GetComune != null)
                {
                    GetComune.Cap = cap;
                    ctx.SaveChanges();
                }
            }
        }

        public static decimal? GetPotenzaTermicaUtileNominaleImpiantoFromIDLibrettoImpianto(int IDLibrettoImpianto)
        {
            decimal? potenzaTermicaUtileNominale = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var libretto = (from LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti
                                join LIM_LibrettiImpiantiGruppiTermici in ctx.LIM_LibrettiImpiantiGruppiTermici on LIM_LibrettiImpianti.IDLibrettoImpianto equals LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto
                                where
                                LIM_LibrettiImpiantiGruppiTermici.fAttivo == true
                                group new { LIM_LibrettiImpianti, LIM_LibrettiImpiantiGruppiTermici } by new
                                {
                                    LIM_LibrettiImpianti.IDLibrettoImpianto
                                } into g
                                where g.Key.IDLibrettoImpianto == IDLibrettoImpianto
                                select new
                                {
                                    g.Key.IDLibrettoImpianto,
                                    potenza = g.Sum(p => ((System.Decimal?)p.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw ?? (System.Decimal?)0))
                                }

                            ).FirstOrDefault();


                if (libretto != null)
                {
                    potenzaTermicaUtileNominale = libretto.potenza;
                }

                return potenzaTermicaUtileNominale;
            }
        }

        public static void DisabledExpiredTerziResponsabili()
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var terziResponsabili = (from LIM_LibrettiImpiantiResponsabili in ctx.LIM_LibrettiImpiantiResponsabili
                                         join LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti on LIM_LibrettiImpiantiResponsabili.IDLibrettoImpianto equals LIM_LibrettiImpianti.IDLibrettoImpianto
                                         where
                                           LIM_LibrettiImpiantiResponsabili.DataFine == null &&
                                           LIM_LibrettiImpiantiResponsabili.fAttivo == true &&
                                           LIM_LibrettiImpianti.fAttivo == true ||
                                           LIM_LibrettiImpiantiResponsabili.DataInizio == null &&
                                           LIM_LibrettiImpiantiResponsabili.fAttivo == true &&
                                           LIM_LibrettiImpianti.fAttivo == true ||
                                           LIM_LibrettiImpiantiResponsabili.DataFine < SqlFunctions.GetDate() &&
                                           LIM_LibrettiImpiantiResponsabili.fAttivo == true &&
                                           LIM_LibrettiImpianti.fAttivo == true
                                         select new
                                         {
                                             LIM_LibrettiImpiantiResponsabili.IDLibrettoImpiantoResponsabili,
                                             LIM_LibrettiImpiantiResponsabili.IDLibrettoImpianto,
                                             LIM_LibrettiImpiantiResponsabili.DataInizio,
                                             LIM_LibrettiImpiantiResponsabili.DataFine
                                         }
                                        ).ToList();


                if (terziResponsabili != null)
                {
                    foreach (var tr in terziResponsabili)
                    {
                        var terzoResponsabile = ctx.LIM_LibrettiImpiantiResponsabili.Where(c => c.IDLibrettoImpiantoResponsabili == tr.IDLibrettoImpiantoResponsabili).FirstOrDefault();
                        terzoResponsabile.fAttivo = false;
                        terzoResponsabile.DataDichiarazioneDismissioneIncarico = DateTime.Now;
                        terzoResponsabile.IDUtenteDichiarazioneDismissioneIncarico = 5805;
                        ctx.SaveChanges();

                        var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == tr.IDLibrettoImpianto).FirstOrDefault();
                        libretto.fTerzoResponsabile = false;
                        ctx.SaveChanges();
                    }
                }

            }
        }

        public static object[] GetDatiTerzoResponsabile(CriterDataModel ctx, int iDLibrettoImpianto)
        {
            object[] outVal = new object[7];

            try
            {
                var terzoResponsabile = ctx.V_LIM_LibrettiImpiantiTerziResponsabili.Where(a => a.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
                if (terzoResponsabile != null)
                {
                    if (terzoResponsabile.IDSoggetto != null)
                    {
                        outVal[0] = terzoResponsabile.RagioneSocialeTerzoResponsabile;
                        outVal[1] = terzoResponsabile.PartitaIvaTerzoResponsabile;
                        outVal[2] = terzoResponsabile.IndirizzoTerzoResponsabile;
                        outVal[3] = terzoResponsabile.CivicoTerzoResponsabile;
                        outVal[4] = terzoResponsabile.CittaTerzoResponsabile;
                        outVal[5] = terzoResponsabile.IDProvinciaTerzoResponsabile;
                        outVal[6] = terzoResponsabile.CapTerzoResponsabile;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outVal;
        }

        public static V_LIM_LibrettiImpiantiTerziResponsabili GetDatiTerzoResponsabile(int IDLibrettoImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.V_LIM_LibrettiImpiantiTerziResponsabili.Where(a => a.IDLibrettoImpianto == IDLibrettoImpianto).FirstOrDefault();

                return result;
            }
        }


    }
}