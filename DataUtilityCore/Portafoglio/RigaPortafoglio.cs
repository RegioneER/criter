//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Xml.Serialization;

//namespace DataUtilityCore.Portafoglio
//{
//    public enum Valuta
//    {
//        Euro = 1,

//        Dollaro
//    }


//    public class RigaPortafoglio
//    {
//        #region Costruttori

//        public RigaPortafoglio()
//        {
//        }

//        #endregion


//        #region DB

//        public void Save(SqlTransaction transaction)
//        {
//            SqlConnection conn = transaction.Connection;

//            SqlCommand TipoCommando = null;

//            Guid id;
//            try
//            {
//                if( this.IDMovimento == Guid.Empty )
//                {
//                    id = Guid.NewGuid();

//                    TipoCommando =
//                        new SqlCommand(
//                            "INSERT INTO COM_RigaPortafoglio (IDMovimento,IDPortafoglio,Utente,DataRegistrazione,Valuta,Importo,IDRapporto,IDMovimentoPayER,IDMovimentoBonifico,IDMovimentoCassa) VALUES (" +
//                            "@IDMovimento," + "@IDPortafoglio," + "@Utente," + "@DataRegistrazione," + "@Valuta," +
//                            "@Importo," + "@IDRapporto," + "@IDMovimentoPayER," + "@IDMovimentoBonifico," + "@IDMovimentoCassa)", conn);


//                    TipoCommando.Parameters.Add(new SqlParameter("@IDMovimento", SqlDbType.UniqueIdentifier));
//                    TipoCommando.Parameters["@IDMovimento"].Value = id;


//                    TipoCommando.Parameters.Add(new SqlParameter("@IDPortafoglio", SqlDbType.Int, 4));
//                    TipoCommando.Parameters["@IDPortafoglio"].Value = this.IDPortafoglio.IDPortafoglio;

//                    TipoCommando.Parameters.Add(new SqlParameter("@Utente", SqlDbType.NVarChar, 100));
//                    TipoCommando.Parameters["@Utente"].Value = this.Utente;

//                    TipoCommando.Parameters.Add(new SqlParameter("@DataRegistrazione", SqlDbType.DateTime));
//                    TipoCommando.Parameters["@DataRegistrazione"].Value = this.DataRegistrazione;

//                    TipoCommando.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.Int, 4));
//                    TipoCommando.Parameters["@Valuta"].Value = this.Valuta;

//                    TipoCommando.Parameters.Add(new SqlParameter("@Importo", SqlDbType.Decimal));
//                    TipoCommando.Parameters["@Importo"].Value = this.Importo;
//                }
//                else
//                {
//                    id = this.IDMovimento;

//                    /* Non ha senso risalvare i dati sugli stessi dati se non possono e non devono cambiare
//                TipoCommando =
//                    new SqlCommand(
//                        "UPDATE COM_RigaPortafoglio SET " + "IDMovimento = @IDMovimento, " +
//                        "IDPortafoglio = @IDPortafoglio, " + "Utente = @Utente, " +
//                        "DataRegistrazione = @DataRegistrazione, " + "Valuta = @Valuta, " + "Importo = @Importo, " +
//                        "IDRapporto = @IDRapporto, " + "IDMovimentoPayER = @IDMovimentoPayER, " +
//                        "IDMovimentoBonifico = @IDMovimentoBonifico " + " WHERE IDMovimento=@IDMovimento", conn);

//                TipoCommando.Parameters.Add(new SqlParameter("@IDMovimento", SqlDbType.UniqueIdentifier));
//                TipoCommando.Parameters["@IDMovimento"].Value = this.IDMovimento;

//                TipoCommando.Parameters.Add(new SqlParameter("@IDPortafoglio", SqlDbType.Int,4));
//                TipoCommando.Parameters["@IDPortafoglio"].Value = this.IDPortafoglio.IDPortafoglio;

//                TipoCommando.Parameters.Add(new SqlParameter("@Utente", SqlDbType.NVarChar, 100));
//                TipoCommando.Parameters["@Utente"].Value = this.Utente;

//                TipoCommando.Parameters.Add(new SqlParameter("@DataRegistrazione", SqlDbType.DateTime));
//                TipoCommando.Parameters["@DataRegistrazione"].Value = this.DataRegistrazione;

//                TipoCommando.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.Int, 4));
//                TipoCommando.Parameters["@Valuta"].Value = this.Valuta;

//                TipoCommando.Parameters.Add(new SqlParameter("@Importo", SqlDbType.Decimal));
//                TipoCommando.Parameters["@Importo"].Value = this.Importo;
//                */
//                }


//                if( TipoCommando != null )
//                {
//                    TipoCommando.Parameters.Add(new SqlParameter("@IDRapporto", SqlDbType.Int, 4));
//                    TipoCommando.Parameters.Add(new SqlParameter("@IDMovimentoPayER", SqlDbType.UniqueIdentifier));
//                    TipoCommando.Parameters.Add(new SqlParameter("@IDMovimentoBonifico", SqlDbType.Int, 4));
//                    TipoCommando.Parameters.Add(new SqlParameter("@IDMovimentoCassa", SqlDbType.Int, 4));
//                    TipoCommando.Parameters["@IDRapporto"].Value = DBNull.Value;
//                    TipoCommando.Parameters["@IDMovimentoPayER"].Value = DBNull.Value;
//                    TipoCommando.Parameters["@IDMovimentoBonifico"].Value = DBNull.Value;
//                    TipoCommando.Parameters["@IDMovimentoCassa"].Value = DBNull.Value;

//                    if( this.IDRapporto != 0 )
//                    {
//                        TipoCommando.Parameters["@IDRapporto"].Value = this.IDRapporto;
//                    }

//                    //if( this.IDMovimentoPayER != null )
//                    //{
//                    //    this.IDMovimentoPayER.Save(transaction, id);
//                    //    TipoCommando.Parameters["@IDMovimentoPayER"].Value = id;
//                    //}

//                    if( this.IDMovimentoBonifico != null )
//                    {
//                        int idb = this.IDMovimentoBonifico.Save(transaction);
//                        TipoCommando.Parameters["@IDMovimentoBonifico"].Value = idb;
//                    }

//                    if(this.IDMovimentoCassa != null)
//                    {
//                        int idb = this.IDMovimentoCassa.Save(transaction);
//                        TipoCommando.Parameters["@IDMovimentoCassa"].Value = idb;
//                    }


//                    TipoCommando.CommandType = CommandType.Text;
//                    TipoCommando.Transaction = transaction;
//                    TipoCommando.ExecuteNonQuery();
//                }
//            }
//            catch( Exception e )
//            {
//                //if(transaction != null)
//                //    transaction.Rollback();

//                throw;
//            }
//            finally
//            {
//            }
//        }

//        #endregion


//        #region Properties

//        [XmlIgnore]
//        public Portafoglio IDPortafoglio{ get; set; }


//        public Guid IDMovimento{ get; set; }


//        public string Utente{ get; set; }


//        public DateTime DataRegistrazione{ get; set; }


//        public decimal Importo{ get; set; }


//        public Valuta Valuta{ get; set; }


//        public int IDRapporto{ get; set; }


//       // public MovimentoPayER IDMovimentoPayER{ get; set; }


//        public MovimentoBonifico IDMovimentoBonifico{ get; set; }


//        public MovimentoCassa IDMovimentoCassa{get;set;}


//        public string NPDatiCert{ get; set; }


//        #endregion
//    }
//}