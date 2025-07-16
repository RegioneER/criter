//// /////////////////////////////////////////////////////////////////////////////
//// Solution:	WinPortafoglio
//// Project:		WinPortafoglio
//// File: 		MovimentoCassa.cs
//// Created: 	2016-02-12  09:33
//// Modified: 	2016-02-12  09:39
//// By: 			Gianluca
//// Company:		Simatica S.p.A.
//// /////////////////////////////////////////////////////////////////////////////


//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SiDataLayer.Portafoglio
//{
//    public class MovimentoCassa
//    {
//        #region Costruttori

//        public MovimentoCassa()
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
//                if(this.IDMovimentoCassa == 0)
//                {
//                    TipoCommando =
//                        new SqlCommand(
//                            "INSERT INTO COM_MovimentoCassa(DataVersamento) OUTPUT INSERTED.IDMovimentoCassa  VALUES (" +
//                            "@DataVersamento)", conn);

//                    TipoCommando.Parameters.Add(new SqlParameter("@DataVersamento", SqlDbType.DateTime));
//                    TipoCommando.Parameters["@DataVersamento"].Value = this.DataVersamento;
//                }
//                else
//                {
//                    /* Non ha senso risalvare i dati sugli stessi dati se non possono e non devono cambiare
//                TipoCommando =
//                    new SqlCommand(
//                        "UPDATE COM_MovimentoCassa SET " + "DataVersamento = @DataVersamento" +
//                        " OUTPUT INSERTED.IDMovimentoCassa " + " WHERE IDMovimentoCassa=@IDMovimentoCassa",
//                        conn);

//                TipoCommando.Parameters.Add(new SqlParameter("@IDMovimentoCassa", SqlDbType.Int, 4));
//                TipoCommando.Parameters["@IDMovimentoCassa"].Value = this.IDMovimentoBonifico;

//                TipoCommando.Parameters.Add(new SqlParameter("@DataVersamento", SqlDbType.DateTime));
//                TipoCommando.Parameters["@DataVersamento"].Value = this.DataBonifico;
//                 */
//                }

//                if( TipoCommando != null )
//                {
//                    TipoCommando.CommandType = CommandType.Text;
//                    TipoCommando.Transaction = transaction;
//                    //TipoCommando.ExecuteNonQuery();
//                    id = (Int32)TipoCommando.ExecuteScalar();

//                    if(this.IDMovimentoCassa == 0)
//                    {
//                        this.IDMovimentoCassa = id;
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

//        public int IDMovimentoCassa{ get; set; }


//        public DateTime DataVersamento{ get; set; }

//        #endregion
//    }
//}