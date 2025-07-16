using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomiager.Service.Parer
{
    public class BuildConnection
    {
        static string m_strConnection = ConfigurationManager.ConnectionStrings["DBConnection"] !=null 
                                        ? ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString 
                                        : "Data Source=PCTERRANOVADEV;Initial Catalog=SaceDB;Persist Security Info=True;User ID=sa_sace;Password=ciccio;Pooling=False;Connection Reset=False";

        public static SqlConnection GetDBConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static string ConnectionString
        {
            get
            {
                return m_strConnection;
            }
            set
            {
                m_strConnection = value;
            }
        }

    }
}
