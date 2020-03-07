using SAPbobsCOM;
using SAPDIExamples.Extensions;
using System;
using System.Collections.Generic;

namespace SAPDIExamples.Handlers
{

    /// <summary>
    /// Sales order handler
    /// </summary>
    public class SalesOrders
    {

        /// <summary>
        /// Load first 2 sales orders
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Sales orders list data</returns>
        public List<Dictionary<string, string>> LoadAll(DBConnection db)
        {
            db.SQLCommand.CommandText =
                @"SELECT TOP 2 [DocEntry], [DocNum], [DocType], [DocStatus], [DocDate],
                    [CardCode], [CardName]
                  FROM [ORDR]
                  ORDER BY [DocEntry]";
            List<Dictionary<string, string>> response = db.CreateDataTable().ToList();
            foreach (Dictionary<string, string> i in response)
            {
                db.SQLCommand.CommandText =
                    @"SELECT [LineNum], [LineStatus], [ItemCode], [Dscription],
                        [Quantity], [Price], [LineTotal]
                      FROM [RDR1]
                      WHERE [DocEntry] = @docEntry
                      ORDER BY [LineNum]";
                db.SQLCommand.Parameters.Add("@docEntry", System.Data.SqlDbType.Int).Value = int.Parse(i["DocEntry"]);
                List<Dictionary<string, string>> items = db.CreateDataTable().ToList();
                foreach (Dictionary<string, string> j in items)
                {
                    i["items." + j["LineNum"] + ".LineNum"] = j["LineNum"];
                    i["items." + j["LineNum"] + ".LineStatus"] = j["LineStatus"];
                    i["items." + j["LineNum"] + ".ItemCode"] = j["ItemCode"];
                    i["items." + j["LineNum"] + ".Dscription"] = j["Dscription"];
                    i["items." + j["LineNum"] + ".Quantity"] = j["Quantity"];
                    i["items." + j["LineNum"] + ".Price"] = j["Price"];
                    i["items." + j["LineNum"] + ".LineTotal"] = j["LineTotal"];
                }
            }
            return response;
        }

        /// <summary>
        /// Load single record to load
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Attributes for 1 doc entry sales</returns>
        public Dictionary<string, string> LoadSingle(DBConnection db)
        {
            return LoadByCode(db, 1);
        }

        /// <summary>
        /// Load single record to load
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <param name="code">Code to load</param>
        /// <returns>Attributes for C1111 customer</returns>
        public Dictionary<string, string> LoadByCode(DBConnection db, int docEntry)
        {
            db.SQLCommand.CommandText =
                @"SELECT [DocEntry], [DocNum], [DocType], [DocStatus], [DocDate],
                    [CardCode], [CardName]
                  FROM [ORDR]
                  WHERE [DocEntry] = @docEntry
                  ORDER BY [DocEntry]";
            db.SQLCommand.Parameters.Add("@docEntry", System.Data.SqlDbType.Int).Value = docEntry;
            Dictionary<string, string> response = db.CreateDataTable().ToList().First();
            db.SQLCommand.CommandText =
                @"SELECT [LineNum], [LineStatus], [ItemCode], [Dscription],
                    [Quantity], [Price], [LineTotal]
                    FROM [RDR1]
                    WHERE [DocEntry] = @docEntry
                    ORDER BY [LineNum]";
            db.SQLCommand.Parameters.Add("@docEntry", System.Data.SqlDbType.Int).Value = docEntry;
            List<Dictionary<string, string>> items = db.CreateDataTable().ToList();
            foreach (Dictionary<string, string> j in items)
            {
                response["items." + j["LineNum"] + ".LineNum"] = j["LineNum"];
                response["items." + j["LineNum"] + ".LineStatus"] = j["LineStatus"];
                response["items." + j["LineNum"] + ".ItemCode"] = j["ItemCode"];
                response["items." + j["LineNum"] + ".Dscription"] = j["Dscription"];
                response["items." + j["LineNum"] + ".Quantity"] = j["Quantity"];
                response["items." + j["LineNum"] + ".Price"] = j["Price"];
                response["items." + j["LineNum"] + ".LineTotal"] = j["LineTotal"];
            }
            return response;
        }


        /// <summary>
        /// Create a new business partner
        /// </summary>
        /// <param name="sap">SAP connection</param>
        /// <param name="docDate">Document date</param>
        /// <param name="cardCode">Card name</param>
        /// <param name="items">Sales order items</param>
        /// <returns>Retrieve new object key</returns>
        public int Create(
            SAPConnection sap,
            DateTime docDate,
            string cardCode,
            List<Dictionary<string, string>> items
            )
        {
            IDocuments document = sap.Company.GetBusinessObject(BoObjectTypes.oOrders);
            document.DocDate = docDate;
            document.DocDueDate = DateTime.Now;
            document.CardCode = cardCode;

            for (int i = 0; i < items.Count; i++)
            {
                Dictionary<string, string> current = items[i];
                document.Lines.ItemCode = current["ItemCode"];
                document.Lines.Quantity = double.Parse(current["Quantity"]);
                document.Lines.UnitPrice = double.Parse(current["Price"]);

                if (i < items.Count - 1)
                {
                    document.Lines.Add();
                }
            }

            sap.CheckResponse(document.Add());
            return int.Parse(sap.Company.GetNewObjectKey());
        }

        /// <summary>
        /// Delete SAP record if exist
        /// </summary>
        /// <param name="sap">SAP connection</param>
        /// <param name="docEntry">Doc entry to delete</param>
        /// <returns></returns>
        public bool Delete(SAPConnection sap, int docEntry)
        {
            IDocuments document = sap.Company.GetBusinessObject(BoObjectTypes.oOrders);
            if (document.GetByKey(docEntry))
            {
                sap.CheckResponse(document.Cancel());
                return true;
            }
            return false;
        }

    }

}
