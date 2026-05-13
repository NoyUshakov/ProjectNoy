using System.Data;
using Microsoft.Data.SqlClient;
namespace ProjectNoy.Model
{
    public class Helper
    {
        
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\ProjectNoy\ProjectNoy\App_Data\Users.mdf;Integrated Security=True";

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