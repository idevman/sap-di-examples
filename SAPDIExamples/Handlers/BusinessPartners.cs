using SAPbobsCOM;
using SAPDIExamples.Extensions;
using System;
using System.Collections.Generic;

namespace SAPDIExamples.Handlers
{

    /// <summary>
    /// Business partner handler
    /// </summary>
    public class BusinessPartners
    {

        /// <summary>
        /// Load first 5 business partners
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Business partners list data</returns>
        public List<Dictionary<string, string>> LoadAll(DBConnection db)
        {
            db.SQLCommand.CommandText =
                @"SELECT TOP 5 [CardCode], [CardName], [CardType], [LicTradNum]
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
            return LoadByCode(db, "C1111");
        }

        /// <summary>
        /// Load single record to load
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <param name="code">Code to load</param>
        /// <returns>Attributes for C1111 customer</returns>
        public Dictionary<string, string> LoadByCode(DBConnection db, string code)
        {
            db.SQLCommand.CommandText =
                @"SELECT [CardCode], [CardName], [CardType], [LicTradNum]
                  FROM [OCRD]
                  WHERE [CardCode] = @code";
            db.SQLCommand.Parameters.Add("@code", System.Data.SqlDbType.VarChar, 15).Value = code;
            return db.CreateDataTable().ToList().First();
        }


        /// <summary>
        /// Create a new business partner
        /// </summary>
        /// <param name="sap">SAP connection</param>
        /// <param name="cardCode">Card code</param>
        /// <param name="cardName">Card name</param>
        /// <param name="cardType">Card type</param>
        /// <param name="licTradeNumber">Lic trade number</param>
        /// <returns>Retrieve new object key</returns>
        public string Create(
            SAPConnection sap,
            string cardCode,
            string cardName,
            string licTradeNumber,
            BoCardTypes cardType)
        {
            IBusinessPartners partner = sap.Company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            partner.CardCode = cardCode;
            partner.CardName = cardName;
            partner.CardType = cardType;
            partner.FederalTaxID = licTradeNumber;

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
            IBusinessPartners partner = sap.Company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            if (partner.GetByKey(key))
            {
                sap.CheckResponse(partner.Remove());
                return true;
            }
            return false;
        }

    }

}
