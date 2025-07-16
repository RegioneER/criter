using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataUtilityCore
{
    public class BuildConnection
    {
        static string m_strConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        
        public static SqlCommand GetSprocCmd(string strSprocName, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(strSprocName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static SqlConnection GetConn()
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