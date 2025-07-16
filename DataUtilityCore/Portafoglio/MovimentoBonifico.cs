//// /////////////////////////////////////////////////////////////////////////////
//// Solution:	Sace
//// Project:		Sace
//// File: 		MovimentoBonifico.cs
//// Created: 	2016-01-27  11:19
//// Modified: 	2016-01-28  15:27
//// By: 			Gianluca
//// Company:		Simatica S.p.A.
//// /////////////////////////////////////////////////////////////////////////////


//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SiDataLayer.Portafoglio
//{
//    public class MovimentoBonifico
//    {
//        #region Costruttori

//        public MovimentoBonifico()
//        {
//        }

//        #endregion


//        #region DB

//        public int Save(SqlTransaction transaction)
//        {
//            SqlConnection conn = transaction.Connection;

//            SqlCommand TipoCommando = null;

//            Int32 id = 0;

//            try
//            {
//                if( this.IDMovimentoBonifico == 0 )
//                {
//                    TipoCommando =
//                        new SqlCommand(
//                            "INSERT INTO COM_MovimentoBonifico (DataBonifico,Causale) OUTPUT INSERTED.IDMovimentoBonifico  VALUES (" +
//                            "@DataBonifico," + "@Causale)", conn);

//                    TipoCommando.Parameters.Add(new SqlParameter("@DataBonifico", SqlDbType.DateTime));
//                    TipoCommando.Parameters["@DataBonifico"].Value = this.DataBonifico;

//                    TipoCommando.Parameters.Add(new SqlParameter("@Causale", SqlDbType.NVarChar, 300));
//                    TipoCommando.Parameters["@Causale"].Value = this.Causale;
//                }
//                else
//                {
//                    /* Non ha senso risalvare i dati sugli stessi dati se non possono e non devono cambiare
//                TipoCommando =
//                    new SqlCommand(
//                        "UPDATE COM_MovimentoBonifico SET " + "DataBonifico = @DataBonifico, " + "Causale = @Causale" +
//                        " OUTPUT INSERTED.IDMovimentoBonifico " + " WHERE IDMovimentoBonifico=@IDMovimentoBonifico",
//                        conn);

//                TipoCommando.Parameters.Add(new SqlParameter("@IDMovimentoBonifico", SqlDbType.Int, 4));
//                TipoCommando.Parameters["@IDMovimentoBonifico"].Value = this.IDMovimentoBonifico;

//                TipoCommando.Parameters.Add(new SqlParameter("@DataBonifico", SqlDbType.DateTime));
//                TipoCommando.Parameters["@DataBonifico"].Value = this.DataBonifico;

//                TipoCommando.Parameters.Add(new SqlParameter("@Causale", SqlDbType.NVarChar, 300));
//                TipoCommando.Parameters["@Causale"].Value = this.Causale;
//                 */
//                }

//                if( TipoCommando != null )
//                {
//                    TipoCommando.CommandType = CommandType.Text;
//                    TipoCommando.Transaction = transaction;
//                    //TipoCommando.ExecuteNonQuery();
//                    id = (Int32)TipoCommando.ExecuteScalar();

//                    if( this.IDMovimentoBonifico == 0 )
//                    {
//                        this.IDMovimentoBonifico = id;
//                    }
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

//            return id;
//        }

//        #endregion


//        #region Properties

//        public int IDMovimentoBonifico{ get; set; }


//        public DateTime DataBonifico{ get; set; }


//        public string Causale{ get; set; }

//        #endregion
//    }
//}