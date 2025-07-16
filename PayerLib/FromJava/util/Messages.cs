using System;
using System.Collections.Generic;
using java.text;
using java.util;
using PayerLib.Properties;

namespace com.seda.payer.ext.util
{

	/// <summary>
	/// @author mmontisano
	/// </summary>
	public sealed class Messages
	{
		public static readonly Messages EMPTY_PARAMETER = new Messages("EMPTY_PARAMETER", InnerEnum.EMPTY_PARAMETER);
		public static readonly Messages INVALID_PARAMETER_VALUE = new Messages("INVALID_PARAMETER_VALUE", InnerEnum.INVALID_PARAMETER_VALUE);
		public static readonly Messages ERROR_XML_NODE = new Messages("ERROR_XML_NODE", InnerEnum.ERROR_XML_NODE);
		public static readonly Messages XML_EXCEPTION = new Messages("XML_EXCEPTION", InnerEnum.XML_EXCEPTION);
		public static readonly Messages HASH_CREATION_ERROR = new Messages("HASH_CREATION_ERROR", InnerEnum.HASH_CREATION_ERROR);
		public static readonly Messages HASH_VERIFY_ERROR = new Messages("HASH_VERIFY_ERROR", InnerEnum.HASH_VERIFY_ERROR);
		public static readonly Messages TIME_WINDOW_EXPIRED = new Messages("TIME_WINDOW_EXPIRED", InnerEnum.TIME_WINDOW_EXPIRED);

		public static readonly Messages INITIALIZATION_SUCCESS = new Messages("INITIALIZATION_SUCCESS", InnerEnum.INITIALIZATION_SUCCESS);
		public static readonly Messages METHOD_START_PARAMETER = new Messages("METHOD_START_PARAMETER", InnerEnum.METHOD_START_PARAMETER);
		public static readonly Messages METHOD_START_2_PARAMETERS = new Messages("METHOD_START_2_PARAMETERS", InnerEnum.METHOD_START_2_PARAMETERS);
		public static readonly Messages BUFFER_CREATED = new Messages("BUFFER_CREATED", InnerEnum.BUFFER_CREATED);
		public static readonly Messages TIME_WINDOW_VERIFIED = new Messages("TIME_WINDOW_VERIFIED", InnerEnum.TIME_WINDOW_VERIFIED);
		public static readonly Messages HASH_VERIFIED = new Messages("HASH_VERIFIED", InnerEnum.HASH_VERIFIED);
		public static readonly Messages DATA_BUFFER = new Messages("DATA_BUFFER", InnerEnum.DATA_BUFFER);

		private static readonly IList<Messages> valueList = new List<Messages>();

		static Messages()
		{
			valueList.Add(EMPTY_PARAMETER);
			valueList.Add(INVALID_PARAMETER_VALUE);
			valueList.Add(ERROR_XML_NODE);
			valueList.Add(XML_EXCEPTION);
			valueList.Add(HASH_CREATION_ERROR);
			valueList.Add(HASH_VERIFY_ERROR);
			valueList.Add(TIME_WINDOW_EXPIRED);
			valueList.Add(INITIALIZATION_SUCCESS);
			valueList.Add(METHOD_START_PARAMETER);
			valueList.Add(METHOD_START_2_PARAMETERS);
			valueList.Add(BUFFER_CREATED);
			valueList.Add(TIME_WINDOW_VERIFIED);
			valueList.Add(HASH_VERIFIED);
			valueList.Add(DATA_BUFFER);
		}

		public enum InnerEnum
		{
			EMPTY_PARAMETER,
			INVALID_PARAMETER_VALUE,
			ERROR_XML_NODE,
			XML_EXCEPTION,
			HASH_CREATION_ERROR,
			HASH_VERIFY_ERROR,
			TIME_WINDOW_EXPIRED,
			INITIALIZATION_SUCCESS,
			METHOD_START_PARAMETER,
			METHOD_START_2_PARAMETERS,
			BUFFER_CREATED,
			TIME_WINDOW_VERIFIED,
			HASH_VERIFIED,
			DATA_BUFFER
		}

		private readonly string nameValue;
		private readonly int ordinalValue;
		private readonly InnerEnum innerEnumValue;
		private static int nextOrdinal = 0;

		private Messages(string name, InnerEnum innerEnum)
		{
			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		private static java.util.ResourceBundle rb;

		public string format(params object[] args)
		{
            //lock (typeof(Messages))
            //{
            //				if (rb == null)
            //				{
            ////JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
            //					rb = ResourceBundle.getBundle(typeof(Messages).FullName);
            //				}
            //return MessageFormat.format(rb.getString(name()),args);
            //TODO verificare
            //return MessageFormat.format(rb.getString(nameValue), args);


            //}

            return String.Format(Settings.Default[nameValue].ToString(), args);
        }

		public static IList<Messages> values()
		{
			return valueList;
		}

		public InnerEnum InnerEnumValue()
		{
			return innerEnumValue;
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static Messages valueOf(string name)
		{
			foreach (Messages enumInstance in Messages.values())
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}
}