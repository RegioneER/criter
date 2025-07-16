using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Pomiager.Service.Parer;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace DataUtilityCore
{
    public class UtilityParer
    {
        public static void SendToParer(MailMessage msg, string PecSendToName, string PecSendToSurname, Pomiager.Service.Parer.TypeEmailEnum typeEmailEnum, Pomiager.Service.Parer.TypePecToTypeEnum typePecToEnum)
        {
            if (bool.Parse(ParerConfig.ParerEnabled))
            {
                bool isSend = false;
                string responseParer = string.Empty;
                string requestParer = string.Empty;

                //QUI DEVO PRIMA INSERIRE LA RIGA SULLA TABELLA DEI LOG DI PARER 
                int IDParerLog = Pomiager.Service.Parer.UtilityDB.LogParerService(null, typeEmailEnum.ToString(), msg.Subject, msg.Body, null, null, false);
                string PecProtocollo = IDParerLog.ToString();

                string pecNameDocumentMain = PecProtocollo + ".XHTML";
                var pecStreamDocumentMain = Pomiager.Service.Parer.UtilityApp.GenerateStreamFromString(msg.Body);

                var parerParametersDto = new Pomiager.Service.Parer.dtoSendParameters();
                try
                {
                    parerParametersDto.PecGuid = PecProtocollo;
                    parerParametersDto.PecTo = msg.To.ToString();
                    parerParametersDto.PecToName = PecSendToName;
                    parerParametersDto.PecToSurname = PecSendToSurname;
                    parerParametersDto.PecToType = typePecToEnum.ToString();
                    parerParametersDto.PecFrom = msg.From.ToString();
                    parerParametersDto.PecBody = msg.Body.ToString();
                    parerParametersDto.PecManager = "ARUBA";
                    parerParametersDto.PecAttributes = typeEmailEnum.ToString();
                    parerParametersDto.PecObject = msg.Subject;
                    parerParametersDto.PecSendDate = DateTime.Now;
                    parerParametersDto.PecDocumentMain = new Pomiager.Service.Parer.PecDocumentMain()
                    {
                        IDDocument = Guid.NewGuid().ToString(),
                        FileName = pecNameDocumentMain, //Path.GetFileName(pathPec),
                        StreamDocument = pecStreamDocumentMain,//File.OpenRead(pathPec),
                        FileExtension = Path.GetExtension(pecNameDocumentMain).Replace(".", "")
                    };

                    parerParametersDto.PecAttachments = new List<Pomiager.Service.Parer.PecAttachment>();
                    foreach (var allegato in msg.Attachments.ToList())
                    {
                        Pomiager.Service.Parer.PecAttachment attachment = new Pomiager.Service.Parer.PecAttachment()
                        {
                            IDAttachment = Guid.NewGuid().ToString(),
                            FileName = allegato.Name,
                            StreamAttachment = allegato.ContentStream,
                            FileExtension = Path.GetExtension(allegato.Name).Replace(".", "")
                        };
                        parerParametersDto.PecAttachments.Add(attachment);
                    }

                    var response = Pomiager.Service.Parer.ParerService.SendToParer(parerParametersDto).GetAwaiter().GetResult();
                    isSend = response.Item1;
                    requestParer = response.Item2;
                    responseParer = response.Item3;
                }
                catch (Exception ex)
                {
                    isSend = false;
                    responseParer = ex.Message;
                }
                finally
                {
                    Pomiager.Service.Parer.UtilityDB.LogParerService(IDParerLog, typeEmailEnum.ToString(), null, null, requestParer, responseParer, isSend);

                    //libero tutti gli stream..
                    pecStreamDocumentMain.Close();
                    foreach (var attachmentStream in parerParametersDto.PecAttachments.ToList())
                    {
                        attachmentStream.StreamAttachment.Close();
                    }
                }
            }
        }


        public static DataTable GetDataParerLog(
                                        string esito,
                                        string ObjectPec,
                                        string DataInvioDal, string DataInvioAl
           )
        {
            using (SqlConnection con = new SqlConnection(BuildConnection.ConnectionString))
            {
                string select = "SELECT * FROM dbo.LogParer WHERE 1=1 ";

                if (!string.IsNullOrEmpty(ObjectPec))
                {
                    select = select + " AND (SubjectRequest LIKE '%" + ObjectPec + "%' OR BodyRequest LIKE '%" + ObjectPec + "%')";
                }
                if (!string.IsNullOrEmpty(DataInvioDal))
                {
                    select = select + " AND DateRequest >= '" + DateTime.Parse(DataInvioDal).ToString("yyyy-MM-dd") + "'";
                }
                if (!string.IsNullOrEmpty(DataInvioAl))
                {
                    select = select + " AND DateRequest <= '" + DateTime.Parse(DataInvioAl).AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (esito != "")
                {
                    select = select + " AND FResponse = " + esito;
                }

                select = select + " ORDER BY DateRequest DESC";

                using (SqlCommand cmd = new SqlCommand(select))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);


                            return dt;
                        }
                    }
                }
            }
        }
    }
}
