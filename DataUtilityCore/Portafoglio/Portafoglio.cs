using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using PayerLib;
using DataLayer;
using Z.EntityFramework.Plus;
using System.Globalization;

namespace DataUtilityCore.Portafoglio
{
    public enum Valuta
    {
        Euro = 1,
        Dollaro
    }

    public class Portafoglio
    {
        #region Costruttori

        public Portafoglio(int soggetto)
        {
            if (this._movimenti == null)
            {
                this._movimenti = new List<COM_RigaPortafoglio>();
            }

            this.IDSoggetto = soggetto;
        }

        #endregion

        #region Metodi Protected

        protected COM_RigaPortafoglio Variazione(string utente, DateTime dt, decimal importo, int quantita, object o)
        {
            COM_RigaPortafoglio r = new COM_RigaPortafoglio();
            r.IdPortafoglio = this.IDPortafoglio;
            r.Valuta = (int)Valuta.Euro;
            r.Importo = importo;
            r.Quantita = quantita;
            r.DataRegistrazione = dt;
            r.Utente = utente;

            if (o is COM_MovimentoBonifico)
            {
                COM_MovimentoBonifico b = (COM_MovimentoBonifico)o;
                r.COM_MovimentoBonifico = b;

                this._movimenti.Add(r);
            }
            else if (o is COM_PayerPaymentRequest)
            {
                COM_PayerPaymentRequest p = (COM_PayerPaymentRequest)o;
                r.COM_PayerPaymentRequest = p;


                this._movimenti.Add(r);
            }
            else if (o is COM_MovimentoCassa)
            {
                COM_MovimentoCassa c = (COM_MovimentoCassa)o;
                r.COM_MovimentoCassa = c;

                this._movimenti.Add(r);
            }
            else if (o is COM_MovimentoStorno)
            {
                COM_MovimentoStorno c = (COM_MovimentoStorno)o;
                r.COM_MovimentoStorno = c;

                this._movimenti.Add(r);
            }

            return r;
        }

        protected void Variazione(string utente, DateTime dt, decimal importo, int quantita, int o)
        {
            COM_RigaPortafoglio r = new COM_RigaPortafoglio();
            r.IdPortafoglio = this.IDPortafoglio;
            r.Valuta = (int)Valuta.Euro;
            r.Importo = importo;
            r.Quantita = quantita;
            r.DataRegistrazione = dt;
            r.Utente = utente;
            r.IdLottoBollinicalorePulito = o;

            this._movimenti.Add(r);
        }

        #endregion

        #region Properties

        public List<COM_RigaPortafoglio> _movimenti;

        public int IDPortafoglio { get; set; }

        public int IDSoggetto { get; set; }

        public decimal Totale
        {
            get
            {
                return
                    this._movimenti.Where(
                        c =>
                            c.IdPaymentRequest == null ||
                            (c.COM_PayerPaymentRequest.Esito.HasValue &&
                             c.COM_PayerPaymentRequest.Esito == (short)PayerEsitoTransazione.OK))
                        .Sum(x => x.Importo.Value);
            }
        }

        public bool HaCredito
        {
            get { return (this.Totale > 0); }
        }

        #endregion

        //public static decimal GetSaldoTotale(int IDSoggetto)
        //{
        //    using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
        //    {
        //        return ctx.COM_RigaPortafoglio.Where(c => c.COM_Portafoglio != null && c.COM_Portafoglio.IdSoggetto == IDSoggetto)
        //            .OrderByDescending(c => c.DataRegistrazione)
        //            .Include(c => c.COM_MovimentoCassa)
        //            .Include(c => c.COM_MovimentoBonifico)
        //            .Include(c => c.COM_PayerPaymentRequest)
        //            .Where(
        //                c =>
        //                    c.Importo.HasValue &&
        //                    c.Quantita.HasValue && (
        //                    c.IdPaymentRequest == null ||
        //                    (c.COM_PayerPaymentRequest.Esito.HasValue &&
        //                     c.COM_PayerPaymentRequest.Esito == (short)PayerEsitoTransazione.OK)))
        //            .Sum(x => x.Importo) ?? 0m;
        //    }
        //}

        public static decimal GetSaldoPortafoglio(int IDSoggetto)
        {
            decimal saldoPortafoglio = 0m;

            using (var ctx = new CriterDataModel())
            {
                var saldoPortafoglioDb = ctx.COM_Portafoglio.Where(a => a.IdSoggetto == IDSoggetto).FirstOrDefault();
                if (saldoPortafoglioDb != null)
                {
                    if (saldoPortafoglioDb.ResiduoPortafoglio != null)
                    {
                        saldoPortafoglio = (decimal)saldoPortafoglioDb.ResiduoPortafoglio;
                    }
                }
                
                

            //    decimal SaldoPortafoglioIncassa = ctx.COM_RigaPortafoglio.Where(c => c.COM_Portafoglio != null && c.COM_Portafoglio.IdSoggetto == IDSoggetto)
            //        .OrderByDescending(c => c.DataRegistrazione)
            //        .Include(c => c.COM_MovimentoCassa)
            //        .Include(c => c.COM_MovimentoBonifico)
            //        .Where(
            //            c =>
            //                c.Importo.HasValue &&
            //                c.Quantita.HasValue && (
            //                c.IdMovimentoBonifico.HasValue || c.IdMovimentoCassa.HasValue)
            //                )
            //        .Sum(x => x.Importo - (x.Quantita * 7)) ?? 0m;


                //    decimal SaldoPortafoglioStorno = ctx.COM_RigaPortafoglio.Where(c => c.COM_Portafoglio != null && c.COM_Portafoglio.IdSoggetto == IDSoggetto)
                //        .OrderByDescending(c => c.DataRegistrazione)
                //        .Include(c => c.COM_MovimentoStorno)
                //        .Where(
                //            c =>
                //                c.Importo.HasValue && (
                //                c.IdMovimentoStorno.HasValue)
                //                )
                //        .Sum(x => x.Importo) ?? 0m;


                //    saldoPortafoglio = SaldoPortafoglioIncassa + SaldoPortafoglioStorno;
                }
                return saldoPortafoglio;
        }

        #region Metodi Pubblici

        public bool CreditoMaggioreDi(decimal importo)
        {
            return (this.Totale >= importo);
        }

        public List<COM_RigaPortafoglio> GetListaMovimenti()
        {
            return this._movimenti;
        }

        public void Pagamento(string utente, DateTime dt, decimal importo, int quantita, int IDCertificato)
        {
            this.Variazione(utente, dt, -1 * importo, quantita, IDCertificato);
        }

        //public void Incasso(string utente, DateTime dt, decimal importo, string IUV, StatoTransazionePayER stato)
        //{
        //    MovimentoPayER p = new MovimentoPayER();
        //    p.IUV = IUV;
        //    p.Stato = stato;

        //    this.Variazione(utente, dt, importo, p);
        //}

        public COM_RigaPortafoglio Incasso(string utente, DateTime dt, decimal importo, int quantita, DateTime databonifico, string causale)
        {
            COM_MovimentoBonifico b = new COM_MovimentoBonifico();
            b.Causale = causale;
            b.DataBonifico = databonifico;

            return this.Variazione(utente, dt, importo, quantita, b);
        }

        public COM_RigaPortafoglio Incasso(string utente, DateTime dt, decimal importo, int quantita, DateTime dataversamento)
        {
            COM_MovimentoCassa c = new COM_MovimentoCassa();
            c.DataVersamento = dataversamento;

            return this.Variazione(utente, dt, importo, quantita, c);

        }

        /// <summary>
        /// L'importo è positivo, ci pensa questo metodo a renderlo negativo
        /// </summary>
        /// <param name="utente"></param>
        /// <param name="dt"></param>
        /// <param name="importo"></param>
        /// <param name="data"></param>
        /// <param name="descrizione"></param>
        /// <returns></returns>
        public COM_RigaPortafoglio Storno(string utente, DateTime dt, decimal importo, int quantita, DateTime data, string descrizione)
        {
            COM_MovimentoStorno c = new COM_MovimentoStorno();
            c.Data = data;
            c.Descrizione = descrizione;
            if (importo > 0)
                importo = -importo; //negativizzo l'importo, che dovrebbe arrivare positivo a questo metodo
            return this.Variazione(utente, dt, importo, quantita, c);

        }

        //Chiamata quando è arrivato il paymentData con l'esito definitivo, mi dirà se è OK oppure no.
        //metto nel portafoglio la transazione anche se ha avuto esito negativo per dare un feedback all'utente
        //public void IncassoPayer(string utente, DateTime dataRegistrazioneMovimentoIncasso,
        //    COM_PayerPaymentData paymentData)
        //{
        //    if (paymentData.COM_PayerPaymentRequest != null)
        //    {

        //        this.Variazione(utente, dataRegistrazioneMovimentoIncasso, paymentData.ImportoTransato.Value,
        //            paymentData.COM_PayerPaymentRequest);

        //    }
        //}

        public COM_PayerPaymentRequest IncassoPayer(string utente, DateTime dataInserimentoPaymentRequest, decimal importo, int quantita)
        {
            //creo la richiesta sul db
            COM_PayerPaymentRequest dbPaymentRequest = new COM_PayerPaymentRequest();
            dbPaymentRequest.IdSoggetto = this.IDSoggetto;
            dbPaymentRequest.Importo = importo;
            dbPaymentRequest.DataOraInserimento = DateTime.Now;
            dbPaymentRequest.NumeroOperazione = Guid.NewGuid();
            //all'inizio l'esito è Null
            this.Variazione(utente, dataInserimentoPaymentRequest, importo, quantita, dbPaymentRequest);
            return dbPaymentRequest;
        }

        #endregion

        #region DB

        public static int LoadMovimenti(SqlConnection conn, Portafoglio p)
        {
            using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
            {
                p._movimenti =
                    ctx.COM_RigaPortafoglio.Where(c => c.IdPortafoglio == p.IDPortafoglio)
                        .OrderByDescending(c => c.DataRegistrazione)
                        .Include(c => c.COM_MovimentoCassa)
                        .Include(c => c.COM_MovimentoBonifico)
                        .Include(c => c.COM_MovimentoStorno)
                        .Include(c => c.COM_PayerPaymentRequest)
                        .Include(c => c.RCT_LottiBolliniCalorePulito)
                        .ToList();
                return p._movimenti.Count;
            }

        }

        public static Portafoglio Load(int IDSoggetto)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnectionString);

            SqlDataReader reader = null;
            Portafoglio ret = null;

            try
            {
                string select = "SELECT TOP 1 * FROM COM_Portafoglio WHERE IDSoggetto = @IDSoggetto";
                SqlCommand cmd = new SqlCommand(select, conn);
                cmd.Parameters.Add("@IDSoggetto", SqlDbType.Int, 4).Value = IDSoggetto;

                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.Default);
                if (reader.Read())
                {
                    ret = new Portafoglio(IDSoggetto);

                    if (reader["IDPortafoglio"] != DBNull.Value)
                    {
                        ret.IDPortafoglio = Convert.ToInt32(reader["IDPortafoglio"]);

                        reader.Close();

                        LoadMovimenti(conn, ret);
                    }


                    return ret;
                }
                else
                {
                    ret = new Portafoglio(IDSoggetto);
                    ret.IDPortafoglio = 0;

                    return ret;
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                conn.Close();
            }

            return ret;
        }

        public void Save()
        {
            using (var ctx = new CriterDataModel())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        //Se il portafoglio non esiste, lo creo
                        COM_Portafoglio comPortafoglio;
                        if (IDPortafoglio == 0)
                        {
                            comPortafoglio = new COM_Portafoglio();
                            comPortafoglio.IdSoggetto = this.IDSoggetto;
                            ctx.COM_Portafoglio.Add(comPortafoglio);
                        }
                        else
                            comPortafoglio = ctx.COM_Portafoglio.Find(IDPortafoglio);

                        //Ho il portafoglio, verifico le righe

                        foreach (
                            var m in
                                _movimenti.Where(c => c.IDMovimento == Guid.Empty).OrderBy(s => s.DataRegistrazione))
                        {

                            //è una riga da salvare
                            m.IDMovimento = Guid.NewGuid();
                            ctx.COM_RigaPortafoglio.Add(m);
                            ctx.SaveChanges();

                        }

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        #endregion

        #region Serializzazione

        public Portafoglio()
        {
            if (this._movimenti == null)
            {
                this._movimenti = new List<COM_RigaPortafoglio>();
            }
        }


        public static bool Dump<T>(T value, ref string serializeXml)
        {
            if (value == null)
            {
                return false;
            }
            try
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
                StringWriter stringWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(stringWriter);

                xmlserializer.Serialize(writer, value);

                serializeXml = stringWriter.ToString();

                writer.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        public static COM_PayerPaymentData SalvaPaymentDataSuDb(PayerPaymentData paymentData)
        {
            using (var ctx = new CriterDataModel())
            {
                var guidNumeroOperazione = Guid.Parse(paymentData.NumeroOperazione);

                var dbPaymentRequest =
                    ctx.COM_PayerPaymentRequest.FirstOrDefault(c => c.NumeroOperazione == guidNumeroOperazione);

                if (dbPaymentRequest != null)
                {
                    var dbPaymentData = new COM_PayerPaymentData();
                    dbPaymentData.DataRicezioneNotifica = DateTime.Now;
                    dbPaymentData.Autorizzazione = paymentData.Autorizzazione;
                    dbPaymentData.CircuitoAutorizzativo = paymentData.CircuitoAutorizzativo;

                    dbPaymentData.DataOraOrdine = PayerUtil.ConvertiData(paymentData.DataOraOrdine);

                    if (paymentData.DataOraTransazione != "10000101000000")
                        //loro mandano comunque una stringa, che non corrisponde a DateTime.MinValue
                        dbPaymentData.DataOraTransazione = PayerUtil.ConvertiData(paymentData.DataOraTransazione);
                    dbPaymentData.Esito = (short)PayerUtil.ParseEnum<PayerEsitoTransazione>(paymentData.Esito);

                    dbPaymentData.IDOrdine = PayerUtil.ConvertiGuid(paymentData.IDOrdine);
                    dbPaymentData.IDTransazione = paymentData.IDTransazione;
                    dbPaymentData.IdPaymentRequest = dbPaymentRequest.idPaymentRequest;
                    dbPaymentData.ImportoCommissioni = PayerUtil.ConvertiImporto(paymentData.ImportoCommissioni);
                    dbPaymentData.ImportoCommissioniEnte = PayerUtil.ConvertiImporto(paymentData.ImportoCommissioniEnte);
                    dbPaymentData.ImportoTransato = PayerUtil.ConvertiImporto(paymentData.ImportoTransato);
                    dbPaymentData.SistemaPagamento = paymentData.SistemaPagamento;
                    ctx.COM_PayerPaymentData.Add(dbPaymentData);

                    //sovrascrivo l'esito della payment request solo se non ha ancora un esito oppure era pending, gli altri esiti li considero già definitivi
                    //if (!dbPaymentRequest.Esito.HasValue || dbPaymentRequest.Esito == (short) PayerEsitoTransazione.OP)
                    //2016-04-28: cambio comunque l'esito, perchè abbiamo scoperto che in realtà un KO può non essere definitivo 

                    List<int?> lottiList = new List<int?>();
                    if (!dbPaymentRequest.Esito.HasValue || dbPaymentRequest.Esito.Value != (short)PayerEsitoTransazione.OK)
                    {
                        dbPaymentRequest.Esito = dbPaymentData.Esito;

                        //Se sono arrivato ad un esito OK, invio la notifica con la ricevuta via mail
                        if (dbPaymentRequest.Esito == (short)PayerEsitoTransazione.OK)
                        {
                            //dovrei avere al max una riga portafoglio!
                            EmailNotify.SendRicevutaPagamentoAmministrazione(dbPaymentRequest.COM_RigaPortafoglio.First().IDMovimento);

                            //Acquisto automatico dei bollini e creazione lotto
                            lottiList = UtilityBollini.AcquisisciBollini(dbPaymentRequest.IdSoggetto, dbPaymentRequest.COM_RigaPortafoglio.First().IDMovimento);
                        }
                    }

                    ctx.SaveChanges();

                    //Qui provo a fixare la storia dei lotti duplicati (FALLITO)
                    //FixLottiDuplicati(ctx, lottiList, dbPaymentRequest.IdSoggetto);

                    return dbPaymentData;
                }
                else
                {
                    //Non dovrebbe mai essere possibile ricevere un payment data senza trovare la payment request abbinata
                    throw new Exception(string.Format("Nessuna Payment Request trovata per NumeroOperazione = {0} ", paymentData.NumeroOperazione));
                }
            }
        }

        public static void FixLottiDuplicati(string iDSoggetto)
        {
            List<int?> lottiList = new List<int?>();

            string sqlSelect = "SELECT * FROM [dbo].[RCT_LottiBolliniCalorePulito] "
                               + " WHERE "
                               + " IdLottobolliniCalorePulito NOT IN (SELECT IdLottobolliniCalorePulito FROM [dbo].[COM_RigaPortafoglio] WHERE IdLottoBollinicalorePulito IS NOT NULL) "
                               + " AND YEAR(DataAcquisto)='" + DateTime.Now.Year + "' AND IDSoggetto=" + iDSoggetto;

            SqlDataReader dr = UtilityApp.GetDR(sqlSelect);
            while (dr.Read())
            {
                if (dr["IdLottobolliniCalorePulito"] != null)
                {
                    lottiList.Add(int.Parse(dr["IdLottobolliniCalorePulito"].ToString()));
                }
            }

            //Se nei movimenti del soggetto esiste un lotto non presente allora devo cancellare questo lotto
            using (var ctx = new CriterDataModel())
            {
                if (lottiList.Count > 0)
                {
                    foreach (var lotto in lottiList)
                    {
                        if (lotto.HasValue)
                        {
                            try
                            {
                                //Cancello i bollini e il lotto non collegati alla riga del portafoglio del soggetto
                                var bolliniDaCancellare = ctx.RCT_BollinoCalorePulito.Where(c => c.IdLottoBolliniCalorePulito == lotto.Value && c.IDRapportoControlloTecnico == null).ToList();
                                ctx.RCT_BollinoCalorePulito.RemoveRange(bolliniDaCancellare);
                                ctx.SaveChanges();

                                if (bolliniDaCancellare.Count > 0)
                                {
                                    ctx.RCT_LottiBolliniCalorePulito.RemoveRange(ctx.RCT_LottiBolliniCalorePulito.Where(c => c.IdLottobolliniCalorePulito == lotto.Value));
                                    ctx.SaveChanges();
                                }

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
        }

        public static bool AggiornaCreditoResiduo(Guid iDMovimento)
        {
            bool fCreditoAggiornato = false;
            decimal ImportoBollino = decimal.Parse(ConfigurationManager.AppSettings["CostoBollino"], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat);
            decimal creditoResiduo = 0;

            using (var ctx = new CriterDataModel())
            {
                var movimento = ctx.COM_RigaPortafoglio.Where(c => c.IDMovimento == iDMovimento).FirstOrDefault();
                var residuo = Math.Abs((decimal)movimento.Importo) % ImportoBollino;

                var portafoglio = ctx.COM_Portafoglio.Where(a => a.Idportafoglio == movimento.IdPortafoglio).FirstOrDefault();
                if (portafoglio.ResiduoPortafoglio != null)
                {
                    decimal residuoAttuale = (decimal)portafoglio.ResiduoPortafoglio;
                    if (movimento.Importo < 0)
                    {
                        residuoAttuale += (decimal)movimento.Importo;
                    }
                    creditoResiduo = residuoAttuale + residuo;

                    portafoglio.ResiduoPortafoglio = creditoResiduo;
                    ctx.SaveChanges();
                }
            }

            return fCreditoAggiornato;
        }


        public static COM_PayerPaymentRequest GetDbPaymentRequest(Guid numeroOperazione)
        {
            using (var ctx = new CriterDataModel())
            {

                //verifico se è stato pagato
                return ctx.COM_PayerPaymentRequest.FirstOrDefault(
                    c =>
                        c.NumeroOperazione == numeroOperazione);
            }
        }

        public static PayerEsitoTransazione? GetEsitoRichiesta(Guid numeroOperazione)
        {
            using (var ctx = new CriterDataModel())
            {
                //verifico se è stato pagato
                var paymentRequest = ctx.COM_PayerPaymentRequest.FirstOrDefault(
                    c =>
                        c.NumeroOperazione == numeroOperazione);
                if (paymentRequest != null)
                    return PayerUtil.EsitoToEnum(paymentRequest.Esito);

            }
            return null;
        }

        //public static bool VerificaCreditoPerPagamentoCertificato(int IDSoggetto, int IDLotto, out string message)
        //{
        //    message = String.Empty;



        //    using (var ctx = new CriterDataModel())
        //    {
        //        //prima dovrei verificare se il rapporto è da pagare..
        //        //var saltaRichiestaPagamento = ctx.COM_RigaPortafoglio.Where(c => c.IdLottoBollinicalorePulito == IDLotto).Select(c => c.SaltaRichiestaPagamento).FirstOrDefault();


        //        //if (saltaRichiestaPagamento)
        //        //    return true;

        //        var giaPagato = ctx.COM_RigaPortafoglio.Any(c => c.IdLottoBollinicalorePulito == IDLotto && c.COM_Portafoglio.IdSoggetto == IDSoggetto);
        //        //
        //        if (giaPagato)
        //            return true;



        //    }

        //    var contributo = UtilityConfig.CostoBollino;

        //    var credito = Portafoglio.GetSaldoTotale(IDSoggetto);

        //    if (credito < contributo)
        //    {
        //        string urlPortafoglio = UtilityConfig.UrlPortafoglioUtente;
        //        //Devo ricaricare
        //        message = string.Format("E’ necessario caricare credito nel portafoglio per poter procedere con la firma digitale dell'attestato. Clicca sul pulsante \"Ricarica portafoglio\" per procedere al caricamento credito. Il pagamento richiesto per ogni rapporto è pari a {0:0.00} euro.<br/><a href=\"{1}\">Ricarica portafoglio</a>", contributo, urlPortafoglio);
        //        return false;
        //    }

        //    return true; //Tutto ok
        //}

    }
}