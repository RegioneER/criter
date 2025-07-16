/// 

using java.io;
using java.util;
using NLog;
using Console = System.Console;

namespace com.seda.payer.ext.util
{


    //using Hierarchy = org.apache.log4j.Hierarchy;
    //using Level = org.apache.log4j.Level;
    //using Logger = org.apache.log4j.Logger;
    //using PropertyConfigurator = org.apache.log4j.PropertyConfigurator;
    //using RootLogger = org.apache.log4j.spi.RootLogger;
   

    public class SedaExtLogger
    {
      

        /*public static final String BASE_LOG_MESSAGES="com.seda.j2ee5.maf.util.LogMessages";
		public static final String MDC_CTX="ctx"; 
		public static final String MDC_APP="app";*/

        //private static Hierarchy hierarchy;
        //private static ResourceBundle bundle=ResourceBundle.getBundle(BASE_LOG_MESSAGES);	

        static SedaExtLogger()
        {
           // Properties log4jConfiguration = new Properties();
            try
            {
                //log4jConfiguration.load(typeof(SedaExtLogger).getResourceAsStream("log4j.properties"));
                //hierarchy = new Hierarchy(new RootLogger(Level.DEBUG));
                //// Load log4j property from external properties configuration object
                //(new PropertyConfigurator()).doConfigure(log4jConfiguration, hierarchy);

            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        public static Logger getLogger(string name)
        {
 
            var    logger = LogManager.GetLogger(name);

            return logger;

        }
    }

}