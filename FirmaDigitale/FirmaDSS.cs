using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Security.Cryptography.Pkcs;
using FirmaLib.WSFirma;

namespace FirmaLib
{
    public class FirmaDSS
    {
        public String valPolicyDefault = ConfigurationManager.AppSettings["Infocert.firma.policy"];
        public String valPolicyVerify = ConfigurationManager.AppSettings["Infocert.verifica.policy"];

        protected FirmaDSS()
        {
        }

        public static FirmaDSS NewInstance
        {
            get
            {
                return new FirmaDSS();
            }
        }

        /// <summary>
        /// Function to get byte array from a file
        /// </summary>
        /// <param name="_FileName">File name to get byte array</param>
        /// <returns>Byte Array</returns>
        public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;
            try
            {
                // Open file for reading
                using (System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    // attach filestream to binary reader
                    using (System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream))
                    {
                        // get total byte length of the file
                        long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                        // read entire file into buffer
                        _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                        _BinaryReader.Close();
                    }
                    _FileStream.Close();
                }
            }

            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            return _Buffer;
        }
        
        public byte[] Decode_p7m(byte[] file)
        {
            try
            {
                SignedCms signedCms = new SignedCms();
                signedCms.Decode(file);
                signedCms.CheckSignature(false);
                return signedCms.ContentInfo.Content;
            }
            catch
            {
                return file;
            }
        }

        public byte[] Decodep7m(byte[] p7mFile)
        {
            SignedCms signedCms = new SignedCms();
            try
            {
                signedCms.Decode(p7mFile);
            }
            catch
            {
                string Sp7mFile = System.Text.Encoding.UTF8.GetString(p7mFile);
                signedCms.Decode(Convert.FromBase64String(Sp7mFile));
            }

            return signedCms.ContentInfo.Content;
        }

        public object[] GetInfoSignerCertificate(byte[] p7mFile)
        {
            object[] outVal = new object[8];
            outVal[0] = null; //dnQualifier=12288613
            outVal[1] = null; //SN=BALDUCCI
            outVal[2] = null; //G=GIOVANNI
            outVal[3] = null; //SERIALNUMBER=IT:BLDGNN62M29E289X
            outVal[4] = null; //CN=BALDUCCI GIOVANNI
            outVal[5] = null; //CN=ArubaPEC S.p.A. NG CA 3
            outVal[6] = null; //OU=Certification AuthorityC
            outVal[7] = null; //SerialNumber

            SignedCms signedCms = new SignedCms();
            try
            {
                signedCms.Decode(p7mFile);
            }
            catch
            {
                string Sp7mFile = System.Text.Encoding.UTF8.GetString(p7mFile);
                signedCms.Decode(Convert.FromBase64String(Sp7mFile));
            }
            
            outVal[0] = signedCms.Certificates[0].Subject.Split(new char[] { ',' })[0].Replace("dnQualifier=", "");
            outVal[1] = signedCms.Certificates[0].Subject.Split(new char[] { ',' })[1].Replace("SN=", "");
            outVal[2] = signedCms.Certificates[0].Subject.Split(new char[] { ',' })[2].Replace("G=", "");
            outVal[3] = signedCms.Certificates[0].Subject.Split(new char[] { ',' })[3].Replace("SERIALNUMBER=IT:", "");
            outVal[4] = signedCms.Certificates[0].Subject.Split(new char[] { ',' })[4].Replace("CN=", "");
            outVal[5] = signedCms.Certificates[0].Issuer.Split(new char[] { ',' })[0].Replace("CN=", "");
            outVal[6] = signedCms.Certificates[0].Issuer.Split(new char[] { ',' })[1].Replace("OU=", "");
            outVal[7] = signedCms.Certificates[0].SerialNumber;
            
            return outVal;
        }

        public bool CheckInfoSignerCodiceFiscale(byte[] p7mFile, string CodiceFiscaleSoggetto)
        {
            bool SameCodiceFiscale = false;

            SignedCms signedCms = new SignedCms();
            try
            {
                signedCms.Decode(p7mFile);
            }
            catch
            {
                string Sp7mFile = System.Text.Encoding.UTF8.GetString(p7mFile);
                signedCms.Decode(Convert.FromBase64String(Sp7mFile));
            }

            foreach (var certificate in signedCms.Certificates) 
            {
                if (certificate.Subject.ToString().Replace("IT:", "").ToUpper().Contains(CodiceFiscaleSoggetto.ToUpper()))
                {
                    SameCodiceFiscale = true;
                }
            }

            return SameCodiceFiscale;
        }

        public beanDssCreaTransazioneVerifica AvviaTransazioneVerifica(long sessionId, String[] filenames, byte[][] fileb64)
        {
            WSFirma.WSFirmaClient wsFirma = new WSFirma.WSFirmaClient("WSFirmaPort");
            beanDssCreaTransazioneVerifica result = wsFirma.creaTransazioneVerifica(valPolicyVerify, sessionId, null, null, filenames, fileb64);
            return result;            
        }
        public beanDssCreaTransazioneFirma AvviaTransazione(long sessionId, String[] filenames, byte[][] fileb64)
        {
            WSFirma.WSFirmaClient wsFirma = new WSFirma.WSFirmaClient("WSFirmaPort");
            beanDssCreaTransazioneFirma result = wsFirma.creaTransazioneFirma(valPolicyDefault, sessionId, null, null, filenames, fileb64);
            return result;
        }
        public beanDssCompletaTransazioneFirma RisultatoAvviaTransazione(long sessionId)
        {
            WSFirma.WSFirmaClient wsFirma = new WSFirma.WSFirmaClient("WSFirmaPort");
            beanDssCompletaTransazioneFirma bean = wsFirma.completaTransazioneFirma(sessionId);
            return bean;
        }
        public beanDssControllaTransazioneFirma ControllaFileFirmato(long sessionId, byte[] fileb64)
        {
            WSFirma.WSFirmaClient wsFirma = new WSFirma.WSFirmaClient("WSFirmaPort");
            beanDssControllaTransazioneFirma bean = wsFirma.controllaTransazioneFirma(valPolicyDefault, sessionId, fileb64);
            return bean;
        }
        public bool CleanUpTransazione(long sessionId)
        {
            WSFirma.WSFirmaClient wsFirma = new WSFirma.WSFirmaClient("WSFirmaPort");
            return wsFirma.cleanUp(sessionId);
        }
        public long GetEpochTime()
        {
            DateTime dtCurTime = DateTime.Now;
            DateTime dtEpochStartTime = Convert.ToDateTime("1/1/1970 8:00:00 AM");
            TimeSpan ts = dtCurTime.Subtract(dtEpochStartTime);

            long epochtime;
            epochtime = ((((((ts.Days * 24) + ts.Hours) * 60) + ts.Minutes) * 60) + ts.Seconds);
            return epochtime;
        }

    }
}
