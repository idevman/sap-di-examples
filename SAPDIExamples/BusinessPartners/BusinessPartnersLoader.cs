using SAPDIExamples.Extensions;
using System;
using System.Collections.Generic;

namespace SAPDIExamples.BusinessPartners
{

    /// <summary>
    /// Business partner loader
    /// </summary>
    public class BusinessPartnersLoader : ILoader
    {

        /// <summary>
        /// Load first 5 business partners
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Business partners list data</returns>
        public List<Dictionary<string, string>> LoadAll(DBConnection db)
        {
            db.SQLCommand.CommandText =
                @"SELECT TOP 5 [CardCode], [CardName], [CardType]
                  FROM [OCRD]
                  ORDER BY [CardCode]";
            return db.CreateDataTable().ToList();
        }

        /// <summary>
        /// Load single record to load
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Attributes for C1111 customer</returns>
        public Dictionary<string, string> LoadSingle(DBConnection db)
        {
            db.SQLCommand.CommandText = 
                @"SELECT [CardCode], [CardName], [CardType]
                  FROM [OCRD]
                  WHERE [CardCode] = 'C1111'";
            return db.CreateDataTable().ToList().First();
        }

    }

}
