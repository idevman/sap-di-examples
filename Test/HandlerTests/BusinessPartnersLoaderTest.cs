using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPDIExamples;
using SAPDIExamples.Handlers;
using System.Collections.Generic;

namespace Test.HandlerTests
{
    /// <summary>
    /// Used to test data loaded for business partners
    /// </summary>
    [TestClass]
    public class BusinessPartnersLoaderTest : BaseTest
    {

        /// <summary>
        /// Handler to use in tests
        /// </summary>
        protected BusinessPartners Handler { get; set; }

        /// <summary>
        /// Initialize instance each test
        /// </summary>
        [TestInitialize]
        public void Init() {
            Handler = new BusinessPartners();
        }

        /// <summary>
        /// Check data is correct against customer C111
        /// </summary>
        [TestMethod]
        public void ShouldLoadC111BusinessPartner()
        {
            using (DBConnection db = new DBConnection()) {
                Dictionary<string, string> record = Handler.LoadSingle(db);
                AssertBusinessPartner(record, "C1111", "Cliente de Mostrador", "1234567890123", "C");
            }
        }

        /// <summary>
        /// Test load first 5 business partners
        /// </summary>
        [TestMethod]
        public void ShouldLoad5BusinessPartners()
        {
            using (DBConnection db = new DBConnection())
            {
                List<Dictionary<string, string>> records = Handler.LoadAll(db);
                Assert.IsNotNull(records);
                Assert.AreEqual(5, records.Count);

                AssertBusinessPartner(records[0], "C1111", "Cliente de Mostrador", "1234567890123", "C");
                AssertBusinessPartner(records[1], "C20000", "Maxi-Teq", "NOT111111DEF", "C");
                AssertBusinessPartner(records[2], "C23900", "Parameter Technology", "PAT222222ERW", "C");
                AssertBusinessPartner(records[3], "C30000", "Microchips", "MCH333333AXF", "C");
                AssertBusinessPartner(records[4], "C40000", "Earthshaker Corporation", "EAC444444TYH", "C");
            }
        }

        /// <summary>
        /// Test business partner creation
        /// </summary>
        [TestMethod]
        public void ShouldCreateBusinessPartner()
        {
            using (SAPConnection sap = new SAPConnection())
            using (DBConnection db = new DBConnection())
            {
                Handler.Delete(sap, "SF01");
                string key = Handler.Create(sap,
                    "SF01",
                    "Silifalcon",
                    "SILF160106PP",
                    SAPbobsCOM.BoCardTypes.cCustomer);
                Assert.AreEqual("SF01", key);

                Dictionary<string, string> record = Handler.LoadByCode(db, "SF01");
                AssertBusinessPartner(record, "SF01", "Silifalcon", "SILF160106PP", "C");

                Assert.IsTrue(Handler.Delete(sap, "SF01"));
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
        private void AssertBusinessPartner(
            Dictionary<string, string> record,
            string cardCode,
            string cardName,
            string licTradeNumber,
            string cardType)
        {
            Assert.IsNotNull(record);
            Assert.IsTrue(record.ContainsKey("CardCode"));
            Assert.IsTrue(record.ContainsKey("CardName"));
            Assert.IsTrue(record.ContainsKey("LicTradNum"));
            Assert.IsTrue(record.ContainsKey("CardType"));

            Assert.AreEqual(cardCode, record["CardCode"]);
            Assert.AreEqual(cardName, record["CardName"]);
            Assert.AreEqual(licTradeNumber, record["LicTradNum"]);
            Assert.AreEqual(cardType, record["CardType"]);
        }

    }

}
