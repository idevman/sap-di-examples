using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SAPDIExamples
{

    /// <summary>
    /// Persistence unit
    /// </summary>
    public class DBConnection : IDisposable
    {

        /// <summary>
        /// Server name for database connection
        /// </summary>
        public static string Server { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        public static string Database { get; set; }

        /// <summary>
        /// Database login as trusted connection
        /// </summary>
        public static bool UseTrusted { get; set; }

        /// <summary>
        /// Database user
        /// </summary>
        public static string User { get; set; }

        /// <summary>
        /// Database password
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// SQL connection to handle in persistence unit
        /// </summary>
        private SqlConnection SQLConnection { get; set; }

        /// <summary>
        /// SQL command to execute
        /// </summary>
        public SqlCommand SQLCommand { get; set; }

        /// <summary>
        /// Create a new instance of Persistence
        /// </summary>
        public DBConnection()
        {
            StringBuilder connectionStringBuilder = new StringBuilder();
            connectionStringBuilder.Append("Server=").Append(Server).Append(";");
            connectionStringBuilder.Append("Database=").Append(Database).Append(";");
            if (UseTrusted)
            {
                connectionStringBuilder.Append("Trusted_Connection=True");
            }
            else
            {
                connectionStringBuilder.Append("User Id=").Append(User).Append(";");
                connectionStringBuilder.Append("Password=").Append(Password).Append(";");
            }
            SQLConnection = new SqlConnection
            {
                ConnectionString = connectionStringBuilder.ToString()
            };
            SQLCommand = new SqlCommand
            {
                Connection = SQLConnection
            };
            SQLConnection.Open();
        }

        /// <summary>
        /// Fill data table
        /// </summary>
        /// <returns>Data table loaded</returns>
        public DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            using (DataSet dataSet = new DataSet())
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter
            {
                SelectCommand = SQLCommand
            })
            {
                sqlAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
            }
            SQLCommand = new SqlCommand
            {
                Connection = SQLCommand.Connection
            };
            return dataTable;
        }

        /// <summary>
        /// Dispose action
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose action
        /// </summary>
        protected virtual void Dispose(bool dispose)
        {
            if (SQLConnection.State != ConnectionState.Closed)
            {
                SQLConnection.Close();
            }
        }

    }

}
