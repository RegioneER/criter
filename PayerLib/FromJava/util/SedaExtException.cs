using System;

/// 
namespace com.seda.payer.ext.util
{

	public class SedaExtException : Exception
	{

		/// 
		private const long serialVersionUID = -8559375647370938983L;

		/// <param name="message"> </param>
		public SedaExtException(string message) : base("com.seda.payer.ext - " + message)
		{
		}

		///// <param name="cause"> </param>
		//public SedaExtException(Exception cause) : base(cause)
		//{
		//}

		/// <param name="message"> </param>
		/// <param name="cause"> </param>
		public SedaExtException(string message, Exception cause) : base("com.seda.payer.ext - " + message, cause)
		{
		}

	}

}