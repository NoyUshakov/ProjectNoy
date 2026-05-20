using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjectNoy.Model;
using System;
using System.Data;
using System.IO;

namespace ProjectNoy.Model
{
    public class Helper
    {
        private string conString;

        public Helper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            conString = configuration.GetConnectionString("UsersDB");

            // Safety Check: Avoid uninitialized connection string exceptions downstream
            if (string.IsNullOrEmpty(conString))
            {
                throw new InvalidOperationException(
                    "Error: The connection string 'UsersDB' could not be found. " +
                    "Ensure 'appsettings.json' has a 'ConnectionStrings' section containing 'UsersDB'.");
            }
        }

        public DataTable RetrieveTable(string SQLStr, string table)
        {
            // 'using' statements automatically close and dispose connections safely
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLStr, con))
                {
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        ad.Fill(ds, table);
                        return ds.Tables[table];
                    }
                }
            }
        }

        public int Insert(User user, string table)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                // Warning: Direct string interpolation is vulnerable to SQL injection. 
                // Consider moving to parameterized queries later!
                string SQLStr = $"SELECT * FROM {table} WHERE Username LIKE '{user.Username}'";
                using (SqlCommand cmd = new SqlCommand(SQLStr, con))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, table);

                    if (ds.Tables[table].Rows.Count > 0)
                    {
                        return -1;
                    }

                    DataRow dr = ds.Tables[table].NewRow();
                    dr["Firstname"] = user.FirstName;
                    dr["Lastname"] = user.LastName;
                    dr["Username"] = user.Username;
                    dr["Password"] = user.Password;
                    dr["Email"] = user.Email;
                    dr["Phone"] = user.Phone;
                    dr["Birthday"] = user.Birthday.ToString();
                    dr["Admin"] = false;

                    ds.Tables[table].Rows.Add(dr);

                    using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                    {
                        int n = adapter.Update(ds, table);
                        return n;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string SQL)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    con.Open();
                    int n = cmd.ExecuteNonQuery();
                    return n; // No need to call con.Close() manually; 'using' handles it.
                }
            }
        }

        public int Delete(int id, string table)
        {
            if (id == 0) return -1;
            string SQL = $"DELETE FROM {table} WHERE ID = {id}";
            return ExecuteNonQuery(SQL);
        }

        public int Update(User user, string table)
        {
            string SQL = $"UPDATE {table} " +
                $"SET Username='{user.Username}', Password = '{user.Password}', " +
                $"FirstName  = '{user.FirstName}', LastName = '{user.LastName}', " +
                $"Email = '{user.Email}', Phone = '{user.Phone}',  Admin = '{user.Admin}', " +
                $"Birthday = '{user.Birthday:MM-dd-yyyy HH:mm:ss}' " +
                $"WHERE Id = {user.ID}";
            return ExecuteNonQuery(SQL);
        }

        public int Update_disconnected(User user, string table)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string SQLStr = $"SELECT * FROM {table} WHERE Id = {user.ID}";
                using (SqlCommand cmd = new SqlCommand(SQLStr, con))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, table);

                    if (ds.Tables[table].Rows.Count == 0)
                    {
                        return -1;
                    }

                    DataRow dr = ds.Tables[table].Rows[0];
                    dr["Firstname"] = user.FirstName;
                    dr["Lastname"] = user.LastName;
                    dr["Username"] = user.Username;
                    dr["Password"] = user.Password;
                    dr["Email"] = user.Email;
                    dr["Phone"] = user.Phone;
                    dr["Birthday"] = user.Birthday.ToString();
                    dr["Admin"] = user.Admin;

                    using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                    {
                        int n = adapter.Update(ds, table);
                        return n;
                    }
                }
            }
        }

        public int Delete_disconnected(int id, string table)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string SQLStr = $"SELECT * FROM {table} WHERE Id = {id}";
                using (SqlCommand cmd = new SqlCommand(SQLStr, con))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, table);

                    if (ds.Tables[table].Rows.Count == 0) return -1;

                    ds.Tables[table].Rows[0].Delete();

                    using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                    {
                        int n = adapter.Update(ds, table);
                        return n;
                    }
                }
            }
        }

        public object GetScalar(string SQL)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public SqlDataReader GetDataReader(string SQL)
        {
            // Do not use a 'using' statement on this specific connection, 
            // because CommandBehavior.CloseConnection transfers connection management ownership to the reader.
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(SQL, con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}