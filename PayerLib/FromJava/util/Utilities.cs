using System;
using java.io;
using java.security;
using java.text;
using javax.xml.parsers;
using javax.xml.xpath;

namespace com.seda.payer.ext.util
{


	using Document = org.w3c.dom.Document;
	using Element = org.w3c.dom.Element;
	using InputSource = org.xml.sax.InputSource;
	using SAXException = org.xml.sax.SAXException;

	public class Utilities
	{

		public static string TagOrario
		{
			get
			{
                //SimpleDateFormat dateFormatter = new SimpleDateFormat("yyyyMMddHHmm");
                //return dateFormatter.format(DateTime.Now);
                return DateTime.Now.ToString("yyyyMMddHHmm");
            }
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static String getMD5Hash(String text) throws java.security.NoSuchAlgorithmException, java.io.UnsupportedEncodingException
		public static string getMD5Hash(string text)
		{
			MessageDigest md;
			md = MessageDigest.getInstance("MD5");
			md.update(text.GetBytes("ISO-8859-1"), 0, text.Length);
			byte[] md5hash = md.digest();
            //return new string(Hex.encodeHex(md5hash));
		    return StringHelperClass.ByteArrayToString(md5hash);
		}


//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static org.w3c.dom.Document getXmlDocumentFromString(String xmlString) throws javax.xml.parsers.ParserConfigurationException, org.xml.sax.SAXException, java.io.IOException
		public static Document getXmlDocumentFromString(string xmlString)
		{
			DocumentBuilderFactory domFactory = DocumentBuilderFactory.newInstance();
			domFactory.setNamespaceAware(true); // never forget this!
			DocumentBuilder builder = domFactory.newDocumentBuilder();

			InputSource @is = new InputSource();
			@is.setCharacterStream (new StringReader(xmlString));
			Document doc = builder.parse(@is);

			return doc;
		}

		public static string getElementValue(string xpath_expr, Document doc)
		{
			XPathFactory factory = XPathFactory.newInstance();
			XPath xpath = factory.newXPath();
			try
			{
				Element elem = (Element)(xpath.evaluate(xpath_expr, doc, XPathConstants.NODE));
				if (elem != null)
				{
					return elem.getTextContent();
				}
			}
			catch (Exception)
			{
			}

			return "";
		}

		public static void setElementValue(Document doc, string xpath_expr, string sValue)
		{
			XPathFactory factory = XPathFactory.newInstance();
			XPath xpath = factory.newXPath();
			try
			{
				Element elem = (Element)(xpath.evaluate(xpath_expr, doc, XPathConstants.NODE));
				if (elem != null)
				{
					elem.setTextContent(sValue);
				}
			}
			catch (Exception)
			{
			}

		}
	}

}