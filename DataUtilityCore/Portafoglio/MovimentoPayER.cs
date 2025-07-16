//// /////////////////////////////////////////////////////////////////////////////
//// Solution:	Sace
//// Project:		Sace
//// File: 		MovimentoPayER.cs
//// Created: 	2016-01-27  11:34
//// Modified: 	2016-01-28  15:26
//// By: 			Gianluca
//// Company:		Simatica S.p.A.
//// /////////////////////////////////////////////////////////////////////////////

//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SiDataLayer.Portafoglio
//{
//    public enum StatoTransazionePayER
//    {
//        OK = 0,

//        KO,
//    }


//    //public class MovimentoPayER
//    //{
//    //    #region Costruttori

//    //    public MovimentoPayER()
//    //    {
//    //    }

//    //    #endregion


//    //    #region DB

//    //    public void Save(SqlTransaction transaction, Guid id)
//    //    {
//    //        SqlConnection conn = transaction.Connection;

//    //        SqlCommand TipoCommando = null;

//    //        try
//    //        {
//    //            if( this.IDMovimentoPayER == Guid.Empty )
//    //            {
//    //                TipoCommando =
//    //                    new SqlCommand(
//    //                        "INSERT INTO COM_MovimentoPayER (IDMovimentoPayER,Stato,IUV) VALUES (" + "@IDMovimentoPayER," +
//    //                        "@Stato," + "@IUV)", conn);


//    //                TipoCommando.Parameters.Add(new SqlParameter("@IDMovimentoPayER", SqlDbType.UniqueIdentifier));
//    //                this.IDMovimentoPayER = id;
//    //                TipoCommando.Parameters["@IDMovimentoPayER"].Value = this.IDMovimentoPayER;


//    //                TipoCommando.Parameters.Add(new SqlParameter("@Stato", SqlDbType.Int, 4));
//    //                TipoCommando.Parameters["@Stato"].Value = this.Stato;


//    //                TipoCommando.Parameters.Add(new SqlParameter("@IUV", SqlDbType.NVarChar, 50));
//    //                TipoCommando.Parameters["@IUV"].Value = this.IUV;
//    //            }
//    //            else
//    //            {
//    //                /* Non ha senso risalvare i dati sugli stessi dati se non possono e non devono cambiare
//    //            TipoCommando =
//    //                new SqlCommand(
//    //                    "UPDATE COM_MovimentoPayER SET " + "IDMovimentoPayER = @IDMovimentoPayER, " + "Stato = @Stato, " +
//    //                    "IUV = @IUV" + " WHERE IDMovimentoPayER=@IDMovimentoPayER", conn);

//    //            TipoCommando.Parameters.Add(new SqlParameter("@IDMovimentoPayER", SqlDbType.UniqueIdentifier));
//    //            TipoCommando.Parameters["@IDMovimentoPayER"].Value = this.IDMovimentoPayER;

//    //            TipoCommando.Parameters.Add(new SqlParameter("@Stato", SqlDbType.Int, 4));
//    //            TipoCommando.Parameters["@Stato"].Value = this.Stato;

//    //            TipoCommando.Parameters.Add(new SqlParameter("@IUV", SqlDbType.NVarChar, 50));
//    //            TipoCommando.Parameters["@IUV"].Value = this.IUV;
//    //            */
//    //            }

//    //            if( TipoCommando != null )
//    //            {
//    //                TipoCommando.CommandType = CommandType.Text;
//    //                TipoCommando.Transaction = transaction;
//    //                TipoCommando.ExecuteNonQuery();
//    //            }
//    //        }
//    //        catch( Exception e )
//    //        {
//    //            //if(transaction != null)
//    //            //    transaction.Rollback();

//    //            throw;
//    //        }
//    //        finally
//    //        {
//    //        }
//    //    }

//    //    #endregion


//    //    #region Properties

//    //    public Guid IDMovimentoPayER{ get; set; }


//    //    public string IUV{ get; set; }


//    //    public StatoTransazionePayER Stato{ get; set; }

//    //    #endregion
//    //}
//}