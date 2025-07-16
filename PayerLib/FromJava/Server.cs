using NLog;

namespace com.seda.payer.ext
{

	

	using Messages = com.seda.payer.ext.util.Messages;
	using SedaExtException = com.seda.payer.ext.util.SedaExtException;
	using SedaExtLogger = com.seda.payer.ext.util.SedaExtLogger;

	public class Server
	{

		private CoreCS coreCS;
		private string _encryptIV;
		private string _encryptKey;
		private string _codicePortale;

		private static Logger _logger = null;

		/// <summary>
		/// Costruttore dell'oggetto Server con logger di default </summary>
		/// <param name="encryptIV"> Chiave primaria per la generazione dell'Hash </param>
		/// <param name="encryptKey"> Chiave secondaria per la generazione dell'Hash </param>
		/// <param name="codicePortale"> Codice identificativo del portale esterno da utilizzare nei buffer di scambio </param>
		/// <exception cref="SedaExtException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Server(String encryptIV, String encryptKey, String codicePortale) throws com.seda.payer.ext.util.SedaExtException
		public Server(string encryptIV, string encryptKey, string codicePortale)
		{
			_logger = SedaExtLogger.getLogger("com.seda.payer.ext.Server");
			initClass(encryptIV, encryptKey, codicePortale);
		}

		/// <summary>
		/// Costruttore dell'oggetto Server con logger custom </summary>
		/// <param name="encryptIV"> Chiave primaria per la generazione dell'Hash </param>
		/// <param name="encryptKey"> Chiave secondaria per la generazione dell'Hash </param>
		/// <param name="codicePortale"> Codice identificativo del portale esterno da utilizzare nei buffer di scambio </param>
		/// <param name="logger"> Istanza esterna di un oggetto log4j per il logging delle informazioni </param>
		/// <exception cref="SedaExtException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Server(String encryptIV, String encryptKey, String codicePortale, org.apache.log4j.Logger logger) throws com.seda.payer.ext.util.SedaExtException
		public Server(string encryptIV, string encryptKey, string codicePortale, Logger logger)
		{
			if (logger == null)
			{
				throw new SedaExtException(Messages.INVALID_PARAMETER_VALUE.format("logger"));
			}

			_logger = logger;
			initClass(encryptIV, encryptKey, codicePortale);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void initClass(String encryptIV, String encryptKey, String codicePortale) throws com.seda.payer.ext.util.SedaExtException
		private void initClass(string encryptIV, string encryptKey, string codicePortale)
		{
			//controllo parametri input
			if (string.ReferenceEquals(encryptIV, null) || encryptIV.Equals(""))
			{
				throw new SedaExtException(Messages.EMPTY_PARAMETER.format("encryptIV"));
			}
			if (string.ReferenceEquals(encryptKey, null) || encryptKey.Equals(""))
			{
				throw new SedaExtException(Messages.EMPTY_PARAMETER.format("encryptKey"));
			}
			if (string.ReferenceEquals(codicePortale, null) || codicePortale.Equals(""))
			{
				throw new SedaExtException(Messages.EMPTY_PARAMETER.format("codicePortale"));
			}

			coreCS = new CoreCS();

			_encryptIV = encryptIV;
			_encryptKey = encryptKey;
			_codicePortale = codicePortale;

			_logger.Info(Messages.INITIALIZATION_SUCCESS.format(encryptIV, encryptKey, codicePortale));
		}

		/// <summary>
		/// Effettua i controlli di valid� dei dati ricevuti ed estrae l'xml del &lt;PaymentRequest&gt; dal &lt;Buffer&gt; </summary>
		/// <param name="buffer"> Stringa xml del &lt;Buffer&gt; ricevuto </param>
		/// <param name="window_minutes"> Finestra tenporale entro la quale ritenere valido il messaggio ricevuto </param>
		/// <returns> Stringa xml del &lt;PaymentRequest&gt; </returns>
		/// <exception cref="SedaExtException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String getPaymentRequest(String buffer, int window_minutes) throws com.seda.payer.ext.util.SedaExtException
		public virtual string getPaymentRequest(string buffer, int window_minutes)
		{
			_logger.Info(Messages.METHOD_START_2_PARAMETERS.format("getPaymentRequest", "buffer", buffer, "window_minutes", window_minutes));

			//controllo parametri input
			if (string.ReferenceEquals(buffer, null) || buffer.Equals(""))
			{
				throw new SedaExtException(Messages.EMPTY_PARAMETER.format("buffer"));
			}

			return coreCS.decodeBuffer(buffer, window_minutes, _encryptIV, _encryptKey, _logger);
		}

		/// <summary>
		/// Effettua i controlli di valid� dei dati ricevuti ed estrae il Request ID dal &lt;Buffer&gt; </summary>
		/// <param name="buffer"> Stringa xml del &lt;Buffer&gt; ricevuto </param>
		/// <param name="window_minutes"> Finestra tenporale entro la quale ritenere valido il messaggio ricevuto </param>
		/// <returns> Request ID </returns>
		/// <exception cref="SedaExtException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String getRID(String buffer, int window_minutes) throws com.seda.payer.ext.util.SedaExtException
		public virtual string getRID(string buffer, int window_minutes)
		{
			_logger.Info(Messages.METHOD_START_2_PARAMETERS.format("getRID", "buffer", buffer, "window_minutes", window_minutes));

			//controllo parametri input
			if (string.ReferenceEquals(buffer, null) || buffer.Equals(""))
			{
				throw new SedaExtException(Messages.EMPTY_PARAMETER.format("buffer"));
			}

			return coreCS.decodeBuffer(buffer, window_minutes, _encryptIV, _encryptKey, _logger);
		}

		/// <summary>
		/// Effettua i controlli di valid� dei dati ricevuti ed estrae il Payment ID dal &lt;Buffer&gt; </summary>
		/// <param name="buffer"> Stringa xml del &lt;Buffer&gt; ricevuto </param>
		/// <param name="window_minutes"> Finestra tenporale entro la quale ritenere valido il messaggio ricevuto </param>
		/// <returns> Payment ID </returns>
		/// <exception cref="SedaExtException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String getPID(String buffer, int window_minutes) throws com.seda.payer.ext.util.SedaExtException
		public virtual string getPID(string buffer, int window_minutes)
		{
			_logger.Info(Messages.METHOD_START_2_PARAMETERS.format("getPID", "buffer", buffer, "window_minutes", window_minutes));

			//controllo parametri input
			if (string.ReferenceEquals(buffer, null) || buffer.Equals(""))
			{
				throw new SedaExtException(Messages.EMPTY_PARAMETER.format("buffer"));
			}

			return coreCS.decodeBuffer(buffer, window_minutes, _encryptIV, _encryptKey, _logger);
		}

		/// <summary>
		/// Costruisce l'xml del &lt;Buffer&gt; da utilizzare per gli scambi S2S e redirect con il Client </summary>
		/// <param name="bufferDati"> Stringa xml del &lt;PaymentData&gt; che verr� codificato e innestato all'interno del tag &lt;BufferDati&gt; del buffer finale </param>
		/// <returns> Stringa xml del &lt;Buffer&gt; costruito </returns>
		/// <exception cref="SedaExtException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String getBufferPaymentData(String bufferDati) throws com.seda.payer.ext.util.SedaExtException
		public virtual string getBufferPaymentData(string bufferDati)
		{
			_logger.Info(Messages.METHOD_START_PARAMETER.format("getBufferPaymentData", "bufferDati", bufferDati));

			//controllo parametri input
			if (string.ReferenceEquals(bufferDati, null) || bufferDati.Equals(""))
			{
				throw new SedaExtException(Messages.EMPTY_PARAMETER.format("bufferDati"));
			}

			return coreCS.creaBuffer(bufferDati, _encryptIV, _encryptKey, _codicePortale, _logger);
		}
	}

}