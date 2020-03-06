using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPDIExamples;
using SAPDIExamples.BusinessPartners;
using System.Collections.Generic;

namespace Test.BusinessPartners
{
    /// <summary>
    /// Used to test data loaded for business partners
    /// </summary>
    [TestClass]
    public class BusinessPartnersLoaderTest : BaseTest
    {

        /// <summary>
        /// Initialize instance each test
        /// </summary>
        [TestInitialize]
        public void Init() {
            Loader = new BusinessPartnersLoader();
        }

        /// <summary>
        /// Check data is correct against customer C111
        /// </summary>
        [TestMethod]
        public void ShouldLoadC111BusinessPartner()
        {
            using (DBConnection db = new DBConnection()) {
                Dictionary<string, string> record = Loader.LoadSingle(db);
                AssertBusinessPartner(record, "C1111", "Cliente de Mostrador", "C");
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
                List<Dictionary<string, string>> records = Loader.LoadAll(db);
                Assert.IsNotNull(records);
                Assert.AreEqual(5, records.Count);

                AssertBusinessPartner(records[0], "C1111", "Cliente de Mostrador", "C");
                AssertBusinessPartner(records[1], "C20000", "Maxi-Teq", "C");
                AssertBusinessPartner(records[2], "C23900", "Parameter Technology", "C");
                AssertBusinessPartner(records[3], "C30000", "Microchips", "C");
                AssertBusinessPartner(records[4], "C40000", "Earthshaker Corporation", "C");
            }
        }


        /// <summary>
        /// Test business partner data
        /// </summary>
        /// <param name="record">Record to test</param>
        /// <param name="cardCode">Card code to test</param>
        /// <param name="cardName">Card name to test</param>
        /// <param name="cardType">Card type to test</param>
        private void AssertBusinessPartner(
            Dictionary<string, string> record,
            string cardCode,
            string cardName,
            string cardType)
        {
            Assert.IsNotNull(record);
            Assert.IsTrue(record.ContainsKey("CardCode"));
            Assert.IsTrue(record.ContainsKey("CardName"));
            Assert.IsTrue(record.ContainsKey("CardType"));

            Assert.AreEqual(cardCode, record["CardCode"]);
            Assert.AreEqual(cardName, record["CardName"]);
            Assert.AreEqual(cardType, record["CardType"]);
        }

    }

}
