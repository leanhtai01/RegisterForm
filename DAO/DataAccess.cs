using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DataAccess
    {
        private string connectionString =
            "Data Source=DESKTOP-UHGFQ2E;Initial Catalog=QLSV;" +
            "Integrated Security=True";
        private SqlConnection sqlConnection;

        public DataAccess()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public SqlConnection OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed ||
                sqlConnection.State == ConnectionState.Broken)
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlDataReader ExecuteReader(string queryString)
        {
            SqlDataReader sqlDataReader = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, OpenConnection());

                sqlDataReader = sqlCommand.ExecuteReader();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Exception: {e.StackTrace.ToString()}");
            }

            return sqlDataReader;
        }

        public object ExecuteScalar(string queryString)
        {
            object returnedObject = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, OpenConnection());

                returnedObject = sqlCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Exception: {e.StackTrace.ToString()}");
            }

            CloseConnection();

            return returnedObject;
        }

        public object ExecuteScalar(string queryString, SqlParameter[] sqlParameters)
        {
            object returnedObject = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, OpenConnection());
                sqlCommand.Parameters.AddRange(sqlParameters);
                returnedObject = sqlCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Exception: {e.StackTrace.ToString()}");
            }

            CloseConnection();

            return returnedObject;
        }

        public bool ExecuteNonQuery(string queryString)
        {
            bool isSuccess = true;

            try
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, OpenConnection());

                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                isSuccess = false;
                Console.WriteLine($"Exception: {e.StackTrace.ToString()}");
            }

            CloseConnection();

            return isSuccess;
        }

        public bool ExecuteNonQuery(string queryString, SqlParameter[] sqlParameters)
        {
            bool isSuccess = true;

            try
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, OpenConnection());
                sqlCommand.Parameters.AddRange(sqlParameters);
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                isSuccess = false;
                Console.WriteLine($"Exception: {e.StackTrace.ToString()}");
            }

            CloseConnection();

            return isSuccess;
        }

        public DataTable GetTable(string queryString)
        {
            DataTable dataTable = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, OpenConnection());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Exception: {e.StackTrace.ToString()}");
            }

            CloseConnection();

            return dataTable;
        }
    }
}
