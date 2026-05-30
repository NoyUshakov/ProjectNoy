using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjectNoy.Model;
using System.Data;
using System.IO;
using System;

namespace ProjectNoy.Model
{
    public class Helper
    {
        private string conString = "connection string";

        public Helper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            conString = configuration.GetConnectionString("UsersDB");
        }

        public DataTable RetrieveTable(string SQLStr, string table)
        // Gets A table from the data base acording to the SELECT Command in SQLStr;
        // Returns DataTable with the Table.
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(SQLStr, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds, table);

            return ds.Tables[table];
        }

        public int Insert(User user, string table)
        // The Method recieve a user objects and insert it to the Database as new row. 
        // if the user is already taken the method will return -1.
        {
            SqlConnection con = new SqlConnection(conString);

            string SQLStr = $"SELECT * FROM {table} WHERE Username Like '{user.Username}'";
            SqlCommand cmd = new SqlCommand(SQLStr, con);
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            if (ds.Tables[table].Rows.Count > 0)
            {
                return -1;
            }

            DataRow dr = ds.Tables[table].NewRow();
            dr["FirstName"] = user.FirstName; // תוקן ל-N גדולה
            dr["LastName"] = user.LastName;   // תוקן ל-N גדולה
            dr["Username"] = user.Username;
            dr["Password"] = user.Password;
            dr["Email"] = user.Email;
            dr["Phone"] = user.Phone;
            dr["Birthday"] = user.Birthday.ToString();
            dr["Admin"] = false;

            ds.Tables[table].Rows.Add(dr);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int n = adapter.Update(ds, table);
            return n;
        }

        public int ExecuteNonQuery(string SQL)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(SQL, con);

            con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();

            return n;
        }

        public int Delete(int id, string table)
        {
            if (id == 0)
            {
                return -1;
            }
            string SQL = $"DELETE FROM {table} WHERE ID = {id}";
            int n = ExecuteNonQuery(SQL);
            return n;
        }

        public int Update(User user, string table)
        {
            string SQL = $"UPDATE {table} " +
                $"SET Username='{user.Username}', Password = '{user.Password}', " +
                $"FirstName  = '{user.FirstName}', LastName = '{user.LastName}', " +
                $"Email = '{user.Email}', Phone = '{user.Phone}',  Admin = '{user.Admin}', " +
                $"Birthday = '{user.Birthday:MM-dd-yyyy HH:mm:ss}' " +
                $"WHERE Id = {user.ID}";
            int n = ExecuteNonQuery(SQL);
            return n;
        }

        // מתודת גישור עבור קובץ ה-Update.cshtml.cs שלך
        public int UpdateUser(User user, string table = "Users")
        {
            return Update(user, table);
        }

        // מתודה חדשה לשליפת משתמש בודד לפי ה-ID שלו
        public User GetUserById(int id, string table = "Users")
        {
            string sql = $"SELECT * FROM {table} WHERE Id = {id}";
            DataTable dt = RetrieveTable(sql, table);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                User user = new User();
                user.ID = Convert.ToInt32(dr["Id"]);
                user.Username = dr["Username"].ToString();
                user.Password = dr["Password"].ToString();
                user.FirstName = dr["FirstName"].ToString();
                user.LastName = dr["LastName"].ToString();
                user.Email = dr["Email"].ToString();
                user.Phone = dr["Phone"].ToString();
                user.Birthday = dr["Birthday"].ToString(); // מציב ישירות כטקסט
                user.Admin = Convert.ToBoolean(dr["Admin"]);

                return user;
            }
            return null;
        }

        public int Update_disconnected(User user, string table)
        {
            SqlConnection con = new SqlConnection(conString);

            string SQLStr = $"SELECT * FROM {table} WHERE Id = {user.ID}";
            SqlCommand cmd = new SqlCommand(SQLStr, con);
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            if (ds.Tables[table].Rows.Count == 0)
            {
                return -1;
            }

            DataRow dr = ds.Tables[table].Rows[0];

            dr["FirstName"] = user.FirstName; // תוקן ל-N גדולה
            dr["LastName"] = user.LastName;   // תוקן ל-N גדולה
            dr["Username"] = user.Username;
            dr["Password"] = user.Password;
            dr["Email"] = user.Email;
            dr["Phone"] = user.Phone;
            dr["Birthday"] = user.Birthday.ToString();
            dr["Admin"] = user.Admin;

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int n = adapter.Update(ds, table);
            return n;
        }

        public int Delete_disconnected(int id, string table)
        {
            SqlConnection con = new SqlConnection(conString);

            string SQLStr = $"SELECT * FROM {table} WHERE Id = {id}";
            SqlCommand cmd = new SqlCommand(SQLStr, con);
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            ds.Tables[table].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int n = adapter.Update(ds, table);
            return n;
        }

        public object GetScalar(string SQL)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(SQL, con);

            con.Open();
            object scalar = cmd.ExecuteScalar();
            con.Close();

            return scalar;
        }

        public SqlDataReader GetDataReader(string SQL)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(SQL, con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}