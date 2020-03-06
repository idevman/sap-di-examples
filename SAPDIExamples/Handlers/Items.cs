using SAPbobsCOM;
using SAPDIExamples.Extensions;
using System.Collections.Generic;

namespace SAPDIExamples.Handlers
{

    /// <summary>
    /// Items handler
    /// </summary>
    public class Items
    {

        /// <summary>
        /// Load first 5 items
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Item list data</returns>
        public List<Dictionary<string, string>> LoadAll(DBConnection db)
        {
            db.SQLCommand.CommandText =
                @"SELECT TOP 5 [ItemCode], [ItemName]
                  FROM [OITM]
                  ORDER BY [ItemCode]";
            return db.CreateDataTable().ToList();
        }

        /// <summary>
        /// Load single record to load 
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Attributes for item loaded</returns>
        public Dictionary<string, string> LoadSingle(DBConnection db)
        {
            return LoadByKey(db, "A00001");
        }

        /// <summary>
        /// Load single record to load
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <param name="code">Item code to load</param>
        /// <returns>Attributes for item loaded</returns>
        public Dictionary<string, string> LoadByKey(DBConnection db, string code)
        {
            db.SQLCommand.CommandText =
                @"SELECT [ItemCode], [ItemName]
                  FROM [OITM]
                  WHERE [ItemCode] = @code";
            db.SQLCommand.Parameters.Add("@code", System.Data.SqlDbType.VarChar, 50).Value = code;
            return db.CreateDataTable().ToList().First();
        }


        /// <summary>
        /// Create a new item
        /// </summary>
        /// <param name="sap">SAP connection</param>
        /// <param name="code">Item code</param>
        /// <param name="name">Item name</param>
        /// <returns>Retrieve new object key</returns>
        public string Create(SAPConnection sap, string code, string name)
        {
            IItems partner = sap.Company.GetBusinessObject(BoObjectTypes.oItems);
            partner.ItemCode = code;
            partner.ItemName = name;

            sap.CheckResponse(partner.Add());
            return sap.Company.GetNewObjectKey();
        }

        /// <summary>
        /// Delete SAP record if exist
        /// </summary>
        /// <param name="sap">SAP connection</param>
        /// <param name="key">Key to delete</param>
        /// <returns></returns>
        public bool Delete(SAPConnection sap, string key)
        {
            IItems partner = sap.Company.GetBusinessObject(BoObjectTypes.oItems);
            if (partner.GetByKey(key))
            {
                sap.CheckResponse(partner.Remove());
                return true;
            }
            return false;
        }

    }

}
