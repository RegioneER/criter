using System;
using java.io;
using java.security;
using javax.xml.parsers;
using NLog;
using Console = System.Console;
using IOException = System.IO.IOException;

namespace com.seda.payer.ext
{

	using Document = org.w3c.dom.Document;
	using SAXException = org.xml.sax.SAXException;

	using Messages = com.seda.payer.ext.util.Messages;
	using SedaExtException = com.seda.payer.ext.util.SedaExtException;
	using Utilities = com.seda.payer.ext.util.Utilities;
	using Base64 = com.sun.org.apache.xerces.@internal.impl.dv.util.Base64;
    using System.Xml;

	public class CoreCS
	{



//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected String creaBuffer(String bufferDati, String encryptIV, String encryptKey, String codicePortale, org.apache.log4j.Logger logger) throws com.seda.payer.ext.util.SedaExtException
		protected internal virtual string creaBuffer(string bufferDati, string encryptIV, string encryptKey, string codicePortale, Logger logger)
		{
			string buffer = null;

			try
			{
				/*TripleDESChryptoService cryptoService = new TripleDESChryptoService();
				cryptoService.setIv(encryptIV);
				cryptoService.setKeyValue(encryptKey);*/

				string sTagOrario = Utilities.TagOrario;
				string hash = Utilities.getMD5Hash(encryptIV + bufferDati + encryptKey + sTagOrario);
				string bufferDatiCrypt = Base64.encode( bufferDati.GetBytes()); //URLEncoder.encode(cryptoService.encryptBASE64(bufferDati), "UTF-8");

				/*cryptoService.destroy();
				cryptoService = null;*/

				buffer = "<Buffer>" + "<TagOrario>" + sTagOrario + "</TagOrario>" + "<CodicePortale>" + codicePortale + "</CodicePortale>" + "<BufferDati>" + bufferDatiCrypt + "</BufferDati>" + "<Hash>" + hash + "</Hash>" + "</Buffer>";

			}
			catch (UnsupportedEncodingException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.HASH_CREATION_ERROR.format(), e);
			}
			catch (NoSuchAlgorithmException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.HASH_CREATION_ERROR.format(), e);
			}

			logger.Info(Messages.BUFFER_CREATED.format(buffer));
			return buffer;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected String decodeBuffer(String buffer, int window_minutes, String encryptIV, String encryptKey, org.apache.log4j.Logger logger) throws com.seda.payer.ext.util.SedaExtException
		protected internal virtual string decodeBuffer(string buffer, int window_minutes, string encryptIV, string encryptKey, Logger logger)
		{
			try
			{
				//verifica dati buffer
                //Document doc = Utilities.getXmlDocumentFromString(buffer);
                //string sTagOrario = Utilities.getElementValue("/Buffer/TagOrario", doc);
                //string sBufferDatiCrypt = Utilities.getElementValue("/Buffer/BufferDati", doc);
                //string sHashRicevuto = Utilities.getElementValue("/Buffer/Hash", doc);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(buffer);

                var sTagOrario = doc.SelectSingleNode("//Buffer/TagOrario");
                var sBufferDatiCrypt = doc.SelectSingleNode("//Buffer/BufferDati");
                var sHashRicevuto = doc.SelectSingleNode("//Buffer/Hash");

                if (sTagOrario == null || string.IsNullOrWhiteSpace(sTagOrario.InnerText) )
				{
					throw new SedaExtException(Messages.ERROR_XML_NODE.format("TagOrario"));
				}
                if (sBufferDatiCrypt == null || string.IsNullOrWhiteSpace(sBufferDatiCrypt.InnerText))
				{
					throw new SedaExtException(Messages.ERROR_XML_NODE.format("BufferDati"));
				}
                if (sHashRicevuto == null || string.IsNullOrWhiteSpace(sHashRicevuto.InnerText))
				{
					throw new SedaExtException(Messages.ERROR_XML_NODE.format("Hash"));
				}

				//verifica finestra temporale
                verificaFinestraTemporale(sTagOrario.InnerText, window_minutes);
				logger.Info(Messages.TIME_WINDOW_VERIFIED.format());

				//decodifica buffer Base64
                string bufferDati = decodificaBuffer(sBufferDatiCrypt.InnerText);

				//verifica hash
                verificaHash(sHashRicevuto.InnerText, bufferDati, encryptIV, encryptKey, sTagOrario.InnerText);
				logger.Info(Messages.HASH_VERIFIED.format());

				logger.Info(Messages.DATA_BUFFER.format(bufferDati));
				return bufferDati;

			}
			catch (ParserConfigurationException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.XML_EXCEPTION.format(), e);
			}
			catch (SAXException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.XML_EXCEPTION.format(), e);
			}
			catch (IOException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.XML_EXCEPTION.format(), e);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void verificaFinestraTemporale(String sTagOrario, int window_minutes) throws com.seda.payer.ext.util.SedaExtException
		private void verificaFinestraTemporale(string sTagOrario, int window_minutes)
		{
			long longTagOrario = 0L;
            DateTime calReceived = DateTime.MinValue;

            try
			{
				
				string sAnno = sTagOrario.Substring(0, 4);
				string sMese = sTagOrario.Substring(4, 2);
				string sGiorno = sTagOrario.Substring(6, 2);
				string sOra = sTagOrario.Substring(8, 2);
				string sMinuti = sTagOrario.Substring(10, 2);

			    calReceived = new DateTime(int.Parse(sAnno), int.Parse(sMese) ,
			        int.Parse(sGiorno), int.Parse(sOra), int.Parse(sMinuti), 0);

    //            calReceived.set(DateTime.YEAR, int.Parse(sAnno));
				//calReceived.set(DateTime.MONTH, int.Parse(sMese) - 1);
				//calReceived.set(DateTime.DATE, int.Parse(sGiorno));
				//calReceived.set(DateTime.HOUR_OF_DAY, int.Parse(sOra));
				//calReceived.set(DateTime.MINUTE, int.Parse(sMinuti));

				longTagOrario = calReceived.Ticks;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.INVALID_PARAMETER_VALUE.format("TagOrario"), e);
			}

            //DateTime calNow = DateTime.Now;

            //long longActualDate = calNow.Ticks;
            //long lMinutiDiff = Math.Abs((longActualDate - longTagOrario) / (long)60000);

            if (calReceived.AddMinutes(window_minutes) < DateTime.Now)
			{
				throw new SedaExtException(Messages.TIME_WINDOW_EXPIRED.format());
			}
		}

		private string decodificaBuffer(string sBufferDatiCrypt)
		{
            //string bufferDati = new string(Base64.decode(sBufferDatiCrypt));
            string bufferDati = StringHelperClass.NewString(Base64.decode(sBufferDatiCrypt));
            return bufferDati;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void verificaHash(String sHashRicevuto, String bufferDati, String encryptIV, String encryptKey, String sTagOrario) throws com.seda.payer.ext.util.SedaExtException
		private void verificaHash(string sHashRicevuto, string bufferDati, string encryptIV, string encryptKey, string sTagOrario)
		{
			string hashCalcolato = null;
			try
			{
				hashCalcolato = Utilities.getMD5Hash(encryptIV + bufferDati + encryptKey + sTagOrario);

			}
			catch (NoSuchAlgorithmException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.HASH_CREATION_ERROR.format(), e);
			}
			catch (UnsupportedEncodingException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				throw new SedaExtException(Messages.HASH_CREATION_ERROR.format(), e);
			}
			if (string.ReferenceEquals(hashCalcolato, null) || hashCalcolato.Equals(""))
			{
				throw new SedaExtException(Messages.HASH_CREATION_ERROR.format());
			}

			if (!hashCalcolato.Equals(sHashRicevuto, StringComparison.CurrentCultureIgnoreCase))
			{
				throw new SedaExtException(Messages.HASH_CREATION_ERROR.format(sHashRicevuto, hashCalcolato));
			}
		}
	}

}