using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPDIExamples
{

    /// <summary>
    /// Used to handle common utillities for SAP connection
    /// </summary>
    public class SAPConnection : IDisposable
    {

        /// <summary>
        /// SAP server
        /// </summary>
        public static string Server { get; set; }
        
        /// <summary>
        /// SAP user
        /// </summary>
        public static string User { get; set; }

        /// <summary>
        /// SAP password
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// Database type
        /// </summary>
        public static BoDataServerTypes DbServerType { get; set; }

        /// <summary>
        /// Company property
        /// </summary>
        public Company Company { get; private set; }

        /// <summary>
        /// Create a new SAP connection
        /// </summary>
        public SAPConnection()
        {
            Company = new Company
            {
                Server = Server,
                UserName = User,
                Password = Password,
                CompanyDB = DBConnection.Database,
                DbUserName = DBConnection.User,
                DbPassword = DBConnection.Password,
                UseTrusted = DBConnection.UseTrusted,
                DbServerType = DbServerType
            };
            int response = Company.Connect();
            if (response != 0)
            {
                Company.GetLastError(out response, out string error);
                throw new Exception("[" + response + "]: " + error);
            }
        }

        /// <summary>
        /// Check response code
        /// </summary>
        /// <param name="response">Response code</param>
        public void CheckResponse(int response)
        {
            if (response != 0)
            {
                Company.GetLastError(out response, out string error);
                throw new Exception("[" + response + "]: " + error);
            }
        }

        /// <summary>
        /// Close current connection
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Real dispose
        /// </summary>
        /// <param name="x">Disposing flag</param>
        protected virtual void Dispose(bool x)
        {
            if (Company.Connected)
            {
                Company.Disconnect();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

    }

}
