using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPDIExamples;
using SAPDIExamples.Handlers;
using System;
using System.Collections.Generic;

namespace Test.HandlerTests
{
    /// <summary>
    /// Used to test data loaded for sales
    /// </summary>
    [TestClass]
    public class SalesLoaderTest : BaseTest
    {

        /// <summary>
        /// Handler to use in tests
        /// </summary>
        protected SalesOrders Handler { get; set; }

        /// <summary>
        /// Initialize instance each test
        /// </summary>
        [TestInitialize]
        public void Init() {
            Handler = new SalesOrders();
        }

        /// <summary>
        /// Check data is correct against sales 1
        /// </summary>
        [TestMethod]
        public void ShouldLoad1Sales()
        {
            using (DBConnection db = new DBConnection()) {
                Dictionary<string, string> record = Handler.LoadSingle(db);

                List<Dictionary<string, string>> items = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>()
                };

                items[0]["LineNum"] = "0";
                items[0]["LineStatus"] = "C";
                items[0]["ItemCode"] = "A00001";
                items[0]["Dscription"] = "IBM Infoprint 1312";
                items[0]["Quantity"] = "5.000000";
                items[0]["Price"] = "107460.050000";
                items[0]["LineTotal"] = "537300.250000";

                items[1]["LineNum"] = "1";
                items[1]["LineStatus"] = "C";
                items[1]["ItemCode"] = "A00002";
                items[1]["Dscription"] = "IBM Infoprint 1222";
                items[1]["Quantity"] = "5.000000";
                items[1]["Price"] = "53730.030000";
                items[1]["LineTotal"] = "268650.150000";

                items[2]["LineNum"] = "2";
                items[2]["LineStatus"] = "C";
                items[2]["ItemCode"] = "A00003";
                items[2]["Dscription"] = "IBM Infoprint 1226";
                items[2]["Quantity"] = "5.000000";
                items[2]["Price"] = "80594.930000";
                items[2]["LineTotal"] = "402974.650000";

                items[3]["LineNum"] = "3";
                items[3]["LineStatus"] = "C";
                items[3]["ItemCode"] = "A00004";
                items[3]["Dscription"] = "HP Color Laser Jet 5";
                items[3]["Quantity"] = "5.000000";
                items[3]["Price"] = "134324.960000";
                items[3]["LineTotal"] = "671624.800000";

                items[4]["LineNum"] = "4";
                items[4]["LineStatus"] = "C";
                items[4]["ItemCode"] = "A00005";
                items[4]["Dscription"] = "HP Color Laser Jet 4";
                items[4]["Quantity"] = "5.000000";
                items[4]["Price"] = "107460.050000";
                items[4]["LineTotal"] = "537300.250000";

                AssertSales(record, 1, "I", "C", "10/01/2006 12:00:00 a. m.", "C20000", "Norm Thompson", items);
            }
        }

        /// <summary>
        /// Test load first 5 business partners
        /// </summary>
        [TestMethod]
        public void ShouldLoad2BusinessPartners()
        {
            using (DBConnection db = new DBConnection())
            {
                List<Dictionary<string, string>> records = Handler.LoadAll(db);
                Assert.IsNotNull(records);
                Assert.AreEqual(2, records.Count);

                List<Dictionary<string, string>> items = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>()
                };

                items[0]["LineNum"] = "0";
                items[0]["LineStatus"] = "C";
                items[0]["ItemCode"] = "A00001";
                items[0]["Dscription"] = "IBM Infoprint 1312";
                items[0]["Quantity"] = "5.000000";
                items[0]["Price"] = "107460.050000";
                items[0]["LineTotal"] = "537300.250000";

                items[1]["LineNum"] = "1";
                items[1]["LineStatus"] = "C";
                items[1]["ItemCode"] = "A00002";
                items[1]["Dscription"] = "IBM Infoprint 1222";
                items[1]["Quantity"] = "5.000000";
                items[1]["Price"] = "53730.030000";
                items[1]["LineTotal"] = "268650.150000";

                items[2]["LineNum"] = "2";
                items[2]["LineStatus"] = "C";
                items[2]["ItemCode"] = "A00003";
                items[2]["Dscription"] = "IBM Infoprint 1226";
                items[2]["Quantity"] = "5.000000";
                items[2]["Price"] = "80594.930000";
                items[2]["LineTotal"] = "402974.650000";

                items[3]["LineNum"] = "3";
                items[3]["LineStatus"] = "C";
                items[3]["ItemCode"] = "A00004";
                items[3]["Dscription"] = "HP Color Laser Jet 5";
                items[3]["Quantity"] = "5.000000";
                items[3]["Price"] = "134324.960000";
                items[3]["LineTotal"] = "671624.800000";

                items[4]["LineNum"] = "4";
                items[4]["LineStatus"] = "C";
                items[4]["ItemCode"] = "A00005";
                items[4]["Dscription"] = "HP Color Laser Jet 4";
                items[4]["Quantity"] = "5.000000";
                items[4]["Price"] = "107460.050000";
                items[4]["LineTotal"] = "537300.250000";

                AssertSales(records[0], 1, "I", "C", "10/01/2006 12:00:00 a. m.", "C20000", "Norm Thompson", items);

                items = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>()
                };

                items[0]["LineNum"] = "0";
                items[0]["LineStatus"] = "C";
                items[0]["ItemCode"] = "A00006";
                items[0]["Dscription"] = "HP 600 Series Inc";
                items[0]["Quantity"] = "5.000000";
                items[0]["Price"] = "153514.360000";
                items[0]["LineTotal"] = "767571.800000";

                items[1]["LineNum"] = "1";
                items[1]["LineStatus"] = "C";
                items[1]["ItemCode"] = "B10000";
                items[1]["Dscription"] = "Printer Label";
                items[1]["Quantity"] = "200.000000";
                items[1]["Price"] = "383.900000";
                items[1]["LineTotal"] = "76780.000000";

                AssertSales(records[1], 2, "I", "C", "15/01/2006 12:00:00 a. m.", "C30000", "Microchips", items);
            }
        }

        /// <summary>
        /// Test business partner creation
        /// </summary>
        [TestMethod]
        public void ShouldCreateSales()
        {
            using (SAPConnection sap = new SAPConnection())
            using (DBConnection db = new DBConnection())
            {
                List<Dictionary<string, string>> items = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>()
                };

                items[0]["ItemCode"] = "B10000";
                items[0]["Quantity"] = "200.000000";
                items[0]["Price"] = "400.000000";

                int key = Handler.Create(sap, new DateTime(2020, 1, 1), "C1111", items);
                Assert.IsTrue(key > 0);

                Dictionary<string, string> record = Handler.LoadByCode(db, key);

                items = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>()
                };

                items[0]["LineNum"] = "0";
                items[0]["LineStatus"] = "O";
                items[0]["ItemCode"] = "B10000";
                items[0]["Dscription"] = "Printer Label";
                items[0]["Quantity"] = "200.000000";
                items[0]["Price"] = "400.000000";
                items[0]["LineTotal"] = "80000.000000";


                AssertSales(record, key, "I", "O", "01/01/2020 12:00:00 a. m.", "C1111", "Cliente de Mostrador", items);

                Assert.IsTrue(Handler.Delete(sap, key));
            }
        }


        /// <summary>
        /// Test business partner data
        /// </summary>
        /// <param name="record">Record to test</param>
        /// <param name="cardCode">Card code to test</param>
        /// <param name="cardName">Card name to test</param>
        /// <param name="licTradeNumber">Lic trade number</param>
        /// <param name="cardType">Card type to test</param>
        private void AssertSales(
            Dictionary<string, string> record,
            int docEntry,
            string docType,
            string docStatus,
            string docDate,
            string cardCode,
            string cardName,
            List<Dictionary<string, string>> items)
        {
            Assert.IsNotNull(record);
            Assert.IsTrue(record.ContainsKey("DocEntry"));
            Assert.IsTrue(record.ContainsKey("DocNum"));
            Assert.IsTrue(record.ContainsKey("DocType"));
            Assert.IsTrue(record.ContainsKey("DocStatus"));
            Assert.IsTrue(record.ContainsKey("DocDate"));
            Assert.IsTrue(record.ContainsKey("CardCode"));
            Assert.IsTrue(record.ContainsKey("CardName"));

            Assert.AreEqual(docEntry, int.Parse(record["DocEntry"]));
            Assert.IsTrue(int.Parse(record["DocNum"]) > 0);
            Assert.AreEqual(docType, record["DocType"]);
            Assert.AreEqual(docStatus, record["DocStatus"]);
            Assert.AreEqual(docDate, record["DocDate"]);
            Assert.AreEqual(cardCode, record["CardCode"]);
            Assert.AreEqual(cardName, record["CardName"]);

            foreach (Dictionary<string, string> i in items)
            {
                Assert.IsTrue(record.ContainsKey("items." + i["LineNum"] + ".LineNum"));
                Assert.IsTrue(record.ContainsKey("items." + i["LineNum"] + ".LineStatus"));
                Assert.IsTrue(record.ContainsKey("items." + i["LineNum"] + ".ItemCode"));
                Assert.IsTrue(record.ContainsKey("items." + i["LineNum"] + ".Dscription"));
                Assert.IsTrue(record.ContainsKey("items." + i["LineNum"] + ".Quantity"));
                Assert.IsTrue(record.ContainsKey("items." + i["LineNum"] + ".Price"));
                Assert.IsTrue(record.ContainsKey("items." + i["LineNum"] + ".LineTotal"));

                Assert.AreEqual(i["LineNum"], record["items." + i["LineNum"] + ".LineNum"]);
                Assert.AreEqual(i["LineStatus"], record["items." + i["LineNum"] + ".LineStatus"]);
                Assert.AreEqual(i["ItemCode"], record["items." + i["LineNum"] + ".ItemCode"]);
                Assert.AreEqual(i["Dscription"], record["items." + i["LineNum"] + ".Dscription"]);
                Assert.AreEqual(i["Quantity"], record["items." + i["LineNum"] + ".Quantity"]);
                Assert.AreEqual(i["Price"], record["items." + i["LineNum"] + ".Price"]);
                Assert.AreEqual(i["LineTotal"], record["items." + i["LineNum"] + ".LineTotal"]);
            }
        }

    }

}
