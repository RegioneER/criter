using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Xml;
using System.Web;

namespace Pomiager.Service.Parer
{
    public class UtilityDB
    {
        public static int LogParerService(object IDLogParer, string TypeRequest, string SubjectRequest, string BodyRequest, string Request, string Response, bool FResponse)
        {
            int IDLogParerDB = 0;

            using (var dbCon = BuildConnection.GetDBConnection())
            {
                string sql = string.Empty;
                bool finsert = (IDLogParer == null); 

                dbCon.Open();
                var cmd = dbCon.CreateCommand();
                                
                if (finsert)
                {
                    sql = "INSERT INTO LogParer (TypeRequest,DateRequest,SubjectRequest,BodyRequest) ";
                    sql += "VALUES (@TypeRequest,@DateRequest,@SubjectRequest,@BodyRequest);";
                    sql += "SELECT CAST(SCOPE_IDENTITY() AS VARCHAR);";
                }
                else
                {
                    sql = "UPDATE LogParer SET Request=@Request,Response=@Response,FResponse=@FResponse WHERE IDLogParer=@IDLogParer";
                }
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text;

                if (finsert)
                {
                    cmd.Parameters.AddWithValue("@SubjectRequest", ParamValue(SubjectRequest));
                    cmd.Parameters.AddWithValue("@BodyRequest", ParamValue(BodyRequest));
                    cmd.Parameters.AddWithValue("@TypeRequest", ParamValue(TypeRequest));
                    cmd.Parameters.AddWithValue("@DateRequest", DateTime.Now);                   
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IDLogParer", IDLogParer == null ? DBNull.Value : IDLogParer);
                    cmd.Parameters.AddWithValue("@Request", ParamValue(Request));
                    cmd.Parameters.AddWithValue("@Response", ParamValue(Response));
                    cmd.Parameters.AddWithValue("@FResponse", FResponse);
                }
                if (finsert)
                {
                    IDLogParerDB = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    cmd.ExecuteScalar();
                    IDLogParerDB = (int)IDLogParer;
                }
                                
                cmd.Dispose();
            }

            return IDLogParerDB;
        }

        private static object ParamValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return DBNull.Value;
            return value;
        }
    }

    public class UtilityXml
    {
        public static string GetXml<T>(T dataToSerialize)
        {
            string xml = string.Empty;

            try
            {
                //var stringwriter = new StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                //serializer.Serialize(stringwriter, dataToSerialize);
                //xml = stringwriter.ToString();

                using (StringWriter writer = new Utf8StringWriter())
                {
                    serializer.Serialize(writer, dataToSerialize);
                    xml = writer.ToString();
                }
            }
            catch (Exception ex)
            {
                var error = "PARER - " + ParerConfig.ParerSystemSender + " Errore di deserializzazione xml: " + ex.Message;
            }

            return xml;
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static bool SaveXmlToFileSystem(string xml)
        {
            bool fCreated = false;

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);

                string guid = xmlDocument.SelectSingleNode("/Intestazione/Chiave/@Numero").InnerText;

                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                xmlDocument.Save(path + "/" + guid + ".xml");

                fCreated = true;
            }
            catch (Exception ex)
            {
                var error = "PARER - " + ParerConfig.ParerSystemSender + " Errore di salvataggio xml: " + ex.Message;
            }

            return fCreated;
        }
    }

    public class UtilityHash
    {
        public static byte[] HashFiles(Stream fileStream)
        {
            var sha256 = SHA256.Create();
            //Stream fileStream = file.StreamAttachment;

            fileStream.Position = 0;

            try
            {
                // Compute the hash of the fileStream.
                byte[] hashValue = sha256.ComputeHash(fileStream);

                return hashValue;
            }
            catch (IOException e)
            {
                Console.WriteLine($"I/O Exception: {e.Message}");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Access Exception: {e.Message}");
            }

            return null;
        }

        public static string GenerateHash(string input)
        {
            var sha256 = SHA256.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static bool CompareHashs(string plainTextInput, string hashedInput)
        {
            string newHashedPin = GenerateHash(plainTextInput);
            return newHashedPin.Equals(hashedInput);
        }

    }

    public class UtilityApp
    {
        public static MemoryStream GenerateStreamFromString(string s)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(s));
        }

        public static string GetMimeType(string FileName)
        {
            string contentType = MimeMapping.GetMimeMapping(FileName);
            return contentType ?? "application/octet-stream";
        }
    }

}