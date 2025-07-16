using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ClosedXML.Excel;

namespace DataUtilityCore.Portafoglio
{
    public static class ExportContabileUtil
    {

        public static XLWorkbook ExportXlsRendicontazione(DateTime dataInizio, DateTime dataFine,
            string numeroAccreditamento)
        {

            var wb = new XLWorkbook();

            using (var ctx = new CriterDataModel())
            {
                //Tolgo il proxy altrimenti mi vengono nell'esportazione
                ctx.Configuration.ProxyCreationEnabled = false;

                // FOGLIO MOVIMENTI ------------------------------------------
                var wsMovimenti = wb.Worksheets.Add("MOVIMENTI PAYER");
                //var propMovimenti = typeof (V_COM_PayerFlussoRiversamento).GetProperties();
                ////Intestazioni
                //for (int i = 0; i < propMovimenti.Length; i++)
                //{
                //    //le righe e le colonne partono da 1
                //    var prop = propMovimenti[i];
                //    wsMovimenti.Cell(1, i + 1).Value = prop.Name;
                //}



                //var datiMovimentiQuery = ctx.V_COM_PayerFlussoRiversamento
                //    .Where(c =>  DbFunctions.TruncateTime(c.dataRegolamento) >= dataInizio.Date &&  DbFunctions.TruncateTime(c.dataRegolamento) <= dataFine.Date);

                var datiMovimentiQuery = ctx.COM_RigaPortafoglio
                  .Where(c => c.COM_PayerPaymentRequest != null && c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento != null && DbFunctions.TruncateTime(c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.dataRegolamento) >= dataInizio.Date && DbFunctions.TruncateTime(c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.dataRegolamento) <= dataFine.Date);


                if (!string.IsNullOrEmpty(numeroAccreditamento))
                {
                    datiMovimentiQuery = datiMovimentiQuery.Where(c => c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto == numeroAccreditamento);
                }
                var datiMovimenti = //datiMovimentiQuery.ToArray();
                    datiMovimentiQuery.Select(c => new
                    {
                        c.IDMovimento,
                        c.IdPaymentRequest,
                        c.Importo,
                        c.COM_PayerPaymentRequest.DataOraInserimento,
                        c.COM_PayerPaymentRequest.Esito,
                        c.COM_PayerPaymentRequest.IUV,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.Cognome,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.Nome,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.SYS_FormeGiuridiche.FormaGiuridica,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.NomeAzienda,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.identificativoFlusso,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.dataOraFlusso,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.identificativoUnivocoRegolamento,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.dataRegolamento,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.tipoIdentificativoUnivocoMittente,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.codiceIdentificativoUnivocoMittente,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.denominazioneMittente,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.importoTotalePagamenti,
                        c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.numeroTotalePagamenti
                    })
                    .OrderBy(c => c.dataRegolamento)
                    .ThenBy(c => c.DataOraInserimento)
                    .ToArray();

                if (datiMovimenti.Length > 0)
                {

                    //Intestazioni
                    var propMovimenti = datiMovimenti[0].GetType().GetProperties();
                    for (int i = 0; i < propMovimenti.Length; i++)
                    {
                        //le righe e le colonne partono da 1
                        var prop = propMovimenti[i];
                        wsMovimenti.Cell(1, i + 1).Value = prop.Name;
                    }
                    wsMovimenti.Row(1).Style.Font.Bold = true;

                    //dalla seconda riga, partono da 1
                    wsMovimenti.Cell(2, 1).InsertData(datiMovimenti);

                    //Importo
                    wsMovimenti.Column("C").Style.NumberFormat.Format = "€ #,##0.00";
                    //SUBTOTAL(9,C2:C374) -- ATTENZIONE QUI CI VA LA VIRGOLA anzichè il ;
                    var cellImportoTotale = wsMovimenti.Column("C").Cell(datiMovimenti.Length + 2);
                    cellImportoTotale.FormulaA1 = "=SUBTOTAL(9,C2:C" + (datiMovimenti.Length + 1).ToString() + ")";
                    cellImportoTotale.Style.Font.Bold = true;
                    //importoTotalePagamenti
                    wsMovimenti.Column("S").Style.NumberFormat.Format = "€  #,##0.00";
                    wsMovimenti.RangeUsed().SetAutoFilter();
                    wsMovimenti.ColumnsUsed().AdjustToContents();

                    //inserisco 2 righe sopra per mettere le descrizioni
                    var rowDescrizione = wsMovimenti.Row(1).InsertRowsAbove(2).First();
                    var desc = string.Format("Situazione dal {0:dd/MM/yyyy} al {1:dd/MM/yyyy}", dataInizio, dataFine);
                    if (!string.IsNullOrEmpty(numeroAccreditamento))
                    {
                        desc += " - N. Accr. " + numeroAccreditamento;
                    }
                    rowDescrizione.Cell(1).Value = desc;
                    rowDescrizione.Cell(1).Style.Font.Bold = true;

                }
                else
                {
                    wsMovimenti.Cell(1, 1).Value = "Nessun movimento trovato.";
                }


                // FOGLIO RIVERSAMENTI
                var wsRiversamenti = wb.Worksheets.Add("RIVERSAMENTI PAYER");

                var datiRiversamentiQuery = ctx.COM_PayerFlussoRiversamento
                    .Where(c => DbFunctions.TruncateTime(c.dataRegolamento) >= dataInizio.Date && DbFunctions.TruncateTime(c.dataRegolamento) <= dataFine.Date);

                if (!string.IsNullOrEmpty(numeroAccreditamento))
                {
                    datiRiversamentiQuery = datiRiversamentiQuery.Where(
                        c =>
                            c.COM_PayerPaymentRequest.Any(
                                p => p.COM_AnagraficaSoggetti.CodiceSoggetto == numeroAccreditamento));
                }
                var datiRiversamenti =
                    datiRiversamentiQuery.Select(c => new
                    {
                        c.Id,
                        c.identificativoFlusso,
                        c.dataOraFlusso,
                        c.identificativoUnivocoRegolamento,
                        c.dataRegolamento,
                        c.tipoIdentificativoUnivocoMittente,
                        c.codiceIdentificativoUnivocoMittente,
                        c.denominazioneMittente,
                        c.tipoIdentificativoUnivocoRicevente,
                        c.codiceIdentificativoUnivocoRicevente,
                        c.denominazioneRicevente,
                        c.importoTotalePagamenti,
                        c.numeroTotalePagamenti
                    })
                    .OrderBy(c => c.dataRegolamento)
                    .ToArray();

                if (datiRiversamenti.Length > 0)
                {
                    //Intestazioni
                    var propRiversamenti = datiRiversamenti[0].GetType().GetProperties();
                    for (int i = 0; i < propRiversamenti.Length; i++)
                    {
                        //le righe e le colonne partono da 1
                        var prop = propRiversamenti[i];
                        wsRiversamenti.Cell(1, i + 1).Value = prop.Name;
                    }

                    wsRiversamenti.Row(1).Style.Font.Bold = true;

                    //dalla seconda riga, partono da 1
                    wsRiversamenti.Cell(2, 1).InsertData(datiRiversamenti);

                    //Importo
                    wsRiversamenti.Column("L").Style.NumberFormat.Format = "€ #,##0.00";
                    //SUBTOTAL(9,C2:C374) -- ATTENZIONE QUI CI VA LA VIRGOLA anzichè il ;
                    var cellImportoTotale = wsRiversamenti.Column("L").Cell(datiRiversamenti.Length + 2);
                    cellImportoTotale.FormulaA1 = "=SUBTOTAL(9,L2:L" + (datiRiversamenti.Length + 1).ToString() + ")";
                    cellImportoTotale.Style.Font.Bold = true;
                    wsRiversamenti.RangeUsed().SetAutoFilter();
                    wsRiversamenti.ColumnsUsed().AdjustToContents();

                    //inserisco 2 righe sopra per mettere le descrizioni
                    var rowDescrizione = wsRiversamenti.Row(1).InsertRowsAbove(2).First();
                    var desc = string.Format("Situazione dal {0:dd/MM/yyyy} al {1:dd/MM/yyyy}", dataInizio, dataFine);
                    if (!string.IsNullOrEmpty(numeroAccreditamento))
                    {
                        desc += " - N. Accr. " + numeroAccreditamento;
                    }
                    rowDescrizione.Cell(1).Value = desc;
                    rowDescrizione.Cell(1).Style.Font.Bold = true;
                }
                else
                {
                    wsRiversamenti.Cell(1, 1).Value = "Nessun riversamento trovato";
                }



                // FOGLIO MOVIMENTI BONIFICO CASSA
                var wsRigaPortafoglio = wb.Worksheets.Add("Mov. BONIFICO-CASSA");

                var datiRigaPortafoglioQuery = ctx.COM_RigaPortafoglio
                    .Where(
                        c =>
                            (c.COM_MovimentoBonifico != null && DbFunctions.TruncateTime(c.COM_MovimentoBonifico.DataBonifico) >= dataInizio.Date &&
                              DbFunctions.TruncateTime(c.COM_MovimentoBonifico.DataBonifico) <= dataFine.Date)
                            ||
                            (c.COM_MovimentoCassa != null && DbFunctions.TruncateTime(c.COM_MovimentoCassa.DataVersamento) >= dataInizio.Date &&
                              DbFunctions.TruncateTime(c.COM_MovimentoCassa.DataVersamento) <= dataFine.Date));
                if (!string.IsNullOrEmpty(numeroAccreditamento))
                {
                    datiRigaPortafoglioQuery =
                        datiRigaPortafoglioQuery.Where(
                            c => c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto == numeroAccreditamento);
                }
                var datiRigaPortafoglio = datiRigaPortafoglioQuery
                    .Select(c => new
                    {
                        c.IDMovimento,
                        c.Importo,
                        DataValuta =
                            c.COM_MovimentoBonifico != null
                                ? c.COM_MovimentoBonifico.DataBonifico
                                : c.COM_MovimentoCassa.DataVersamento,
                        Tipologia = c.COM_MovimentoBonifico != null ? "Bonifico" : "Cassa",
                        UtenteInserimento = c.Utente,
                        c.DataRegistrazione,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.Cognome,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.Nome,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.SYS_FormeGiuridiche.FormaGiuridica,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.NomeAzienda,
                        c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto

                    })
                    .OrderBy(c => c.DataValuta)
                    .ToArray();

                if (datiRigaPortafoglio.Length > 0)
                {
                    //Intestazioni
                    var propRigaPortafoglio = datiRigaPortafoglio[0].GetType().GetProperties();
                    for (int i = 0; i < propRigaPortafoglio.Length; i++)
                    {
                        //le righe e le colonne partono da 1
                        var prop = propRigaPortafoglio[i];
                        wsRigaPortafoglio.Cell(1, i + 1).Value = prop.Name;
                    }

                    wsRigaPortafoglio.Row(1).Style.Font.Bold = true;

                    //dalla seconda riga, partono da 1
                    wsRigaPortafoglio.Cell(2, 1).InsertData(datiRigaPortafoglio);

                    //Importo
                    wsRigaPortafoglio.Column("B").Style.NumberFormat.Format = "€ #,##0.00";
                    //SUBTOTAL(9,C2:C374) -- ATTENZIONE QUI CI VA LA VIRGOLA anzichè il ;
                    var cellImportoTotale = wsRigaPortafoglio.Column("B").Cell(datiRigaPortafoglio.Length + 2);
                    cellImportoTotale.FormulaA1 = "=SUBTOTAL(9,B2:B" + (datiRigaPortafoglio.Length + 1).ToString() + ")";
                    cellImportoTotale.Style.Font.Bold = true;
                    wsRigaPortafoglio.RangeUsed().SetAutoFilter();
                    wsRigaPortafoglio.ColumnsUsed().AdjustToContents();


                    //inserisco 2 righe sopra per mettere le descrizioni
                    var rowDescrizione = wsRigaPortafoglio.Row(1).InsertRowsAbove(2).First();
                    var desc = string.Format("Situazione dal {0:dd/MM/yyyy} al {1:dd/MM/yyyy}", dataInizio, dataFine);
                    if (!string.IsNullOrEmpty(numeroAccreditamento))
                    {
                        desc += " - N. Accr. " + numeroAccreditamento;
                    }
                    rowDescrizione.Cell(1).Value = desc;
                    rowDescrizione.Cell(1).Style.Font.Bold = true;
                }
                else
                {
                    wsRigaPortafoglio.Cell(1, 1).Value = "Nessun movimento bonifico/cassa trovato";
                }
            }

            return wb;
        }

        public static XLWorkbook ExportXlsUtilizzoCredito(DateTime dataInizio, DateTime dataFine, string numeroAccreditamento)
        {
            var wb = new XLWorkbook();

            using (var ctx = new CriterDataModel())
            {
                //Tolgo il proxy altrimenti mi vengono nell'esportazione
                ctx.Configuration.ProxyCreationEnabled = false;
                //ctx.Database.Log += s => Debug.WriteLine(s);
                var wsRigaPortafoglio = wb.Worksheets.Add("Utilizzo Credito");

                var datiRigaPortafoglioBonificoQuery = ctx.COM_RigaPortafoglio
                    .Where(
                        c => (c.COM_MovimentoBonifico != null && DbFunctions.TruncateTime(c.COM_MovimentoBonifico.DataBonifico) >= dataInizio.Date &&
                              DbFunctions.TruncateTime(c.COM_MovimentoBonifico.DataBonifico) <= dataFine.Date));

                var datiRigaPortafoglioCassaQuery = ctx.COM_RigaPortafoglio
                 .Where(
                        c => c.COM_MovimentoCassa != null && DbFunctions.TruncateTime(c.COM_MovimentoCassa.DataVersamento) >= dataInizio.Date &&
                           DbFunctions.TruncateTime(c.COM_MovimentoCassa.DataVersamento) <= dataFine.Date);

                var datiRigaPortafoglioPayerQuery = ctx.COM_RigaPortafoglio
              .Where(
                     c => c.COM_PayerPaymentRequest != null && c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento != null && DbFunctions.TruncateTime(c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.dataRegolamento) >= dataInizio.Date &&
                      DbFunctions.TruncateTime(c.COM_PayerPaymentRequest.COM_PayerFlussoRiversamento.dataRegolamento) <= dataFine.Date);

                var datiRigaPortafoglioCertificatiQuery = ctx.COM_RigaPortafoglio
           .Where(c => c.RCT_LottiBolliniCalorePulito != null && DbFunctions.TruncateTime(c.DataRegistrazione) >= dataInizio.Date &&
                   DbFunctions.TruncateTime(c.DataRegistrazione) <= dataFine.Date);

                if (!string.IsNullOrEmpty(numeroAccreditamento))
                {
                    datiRigaPortafoglioBonificoQuery =
                        datiRigaPortafoglioBonificoQuery.Where(
                            c => c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto == numeroAccreditamento);

                    datiRigaPortafoglioCassaQuery =
                        datiRigaPortafoglioCassaQuery.Where(
                            c => c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto == numeroAccreditamento);

                    datiRigaPortafoglioPayerQuery =
                        datiRigaPortafoglioPayerQuery.Where(
                            c => c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto == numeroAccreditamento);

                    datiRigaPortafoglioCertificatiQuery =
                        datiRigaPortafoglioCertificatiQuery.Where(
                            c => c.COM_Portafoglio.COM_AnagraficaSoggetti.CodiceSoggetto == numeroAccreditamento);
                }

                var datiRigaPortafoglioObj = new
                {
                    ImportoBonifici = datiRigaPortafoglioBonificoQuery.Sum(c => c.Importo) ?? 0,
                    ImportoCassa = datiRigaPortafoglioCassaQuery.Sum(c => c.Importo) ?? 0,
                    ImportoPayer = datiRigaPortafoglioPayerQuery.Sum(c => c.Importo) ?? 0,
                    ImportoCertificati = -datiRigaPortafoglioCertificatiQuery.Sum(c => c.Importo) ?? 0, //lo positivizzo
                    NumeroCertificati = datiRigaPortafoglioCertificatiQuery.Count()
                };

                var datiRigaPortafoglioObjExt = new
                {
                    ImportoBonifici = datiRigaPortafoglioObj.ImportoBonifici,
                    ImportoCassa = datiRigaPortafoglioObj.ImportoCassa,
                    ImportoPayer = datiRigaPortafoglioObj.ImportoPayer,
                    ImportoCertificati = datiRigaPortafoglioObj.ImportoCertificati,
                    NumeroCertificati = datiRigaPortafoglioObj.NumeroCertificati,
                    //
                    CreditoNonUtilizzato = datiRigaPortafoglioObj.ImportoBonifici +
                                datiRigaPortafoglioObj.ImportoCassa +
                                datiRigaPortafoglioObj.ImportoPayer -
                                datiRigaPortafoglioObj.ImportoCertificati
                };


                var datiRigaPortafoglio = new[]
                {
                    datiRigaPortafoglioObjExt
                };



                //Intestazioni
                var propRigaPortafoglio = datiRigaPortafoglio[0].GetType().GetProperties();
                for (int i = 0; i < propRigaPortafoglio.Length; i++)
                {
                    //le righe e le colonne partono da 1
                    var prop = propRigaPortafoglio[i];
                    wsRigaPortafoglio.Cell(1, i + 1).Value = prop.Name;
                }

                wsRigaPortafoglio.Row(1).Style.Font.Bold = true;

                //dalla seconda riga, partono da 1
                wsRigaPortafoglio.Cell(2, 1).InsertData(datiRigaPortafoglio);

                //Importo
                wsRigaPortafoglio.Column("A").Style.NumberFormat.Format = "€ #,##0.00";
                wsRigaPortafoglio.Column("B").Style.NumberFormat.Format = "€ #,##0.00";
                wsRigaPortafoglio.Column("C").Style.NumberFormat.Format = "€ #,##0.00";
                wsRigaPortafoglio.Column("D").Style.NumberFormat.Format = "€ #,##0.00";
                wsRigaPortafoglio.Column("F").Style.NumberFormat.Format = "€ #,##0.00";
                ////SUBTOTAL(9,C2:C374) -- ATTENZIONE QUI CI VA LA VIRGOLA anzichè il ;
                //var cellImportoTotale = wsRigaPortafoglio.Column("B").Cell(datiRigaPortafoglio.Length + 2);
                //cellImportoTotale.FormulaA1 = "=SUBTOTAL(9,B2:B" + (datiRigaPortafoglio.Length + 1).ToString() + ")";
                //cellImportoTotale.Style.Font.Bold = true;
                //wsRigaPortafoglio.RangeUsed().SetAutoFilter();
                wsRigaPortafoglio.ColumnsUsed().AdjustToContents();

                //inserisco 2 righe sopra per mettere le descrizioni
                var rowDescrizione = wsRigaPortafoglio.Row(1).InsertRowsAbove(2).First();
                var desc = string.Format("Situazione al {0:dd/MM/yyyy}", dataFine);
                if (!string.IsNullOrEmpty(numeroAccreditamento))
                {
                    desc += " - N. Accr. " + numeroAccreditamento;
                }
                rowDescrizione.Cell(1).Value = desc;

                rowDescrizione.Cell(1).Style.Font.Bold = true;
            }

            return wb;
        }
    }
}
