using System.Data;
using System.Data.SqlClient;

namespace ProjectNoy.Model // השם של הפרויקט שלך + התיקייה
{
    public class Helper
    {
        public DataTable RetrieveTable(string SQLStr, string table)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(SQLStr, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds, table);
            return ds.Tables[table];
        }
    }
}