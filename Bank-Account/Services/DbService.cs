using System.Data;
using System.Data.SqlClient;


namespace Bank_Account.Services
{
    public class DbService
    {
        private readonly string _connectionString;

        public DbService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Dbcon");
        }

        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            return con;
        }

        public int ExecuteScaler(string query , SqlParameter[] parameters)
        {
            using (SqlConnection con = GetConnection())

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void ExecuteNonQuery(string query , SqlParameter[] parameters)
        {
            using (SqlConnection con = GetConnection())

            using (SqlCommand cmd = new SqlCommand(query, con))
            { 
                if(parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                cmd.ExecuteNonQuery();
            }

        }


        public SqlDataReader ExecuteReader(string query, SqlParameter[] parameters)
        {
            SqlConnection con = GetConnection();

            SqlCommand cmd = new SqlCommand(query, con);

            
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
           
        }


    }
}
