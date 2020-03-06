using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPDIExamples;
using SAPDIExamples.Handlers;
using System.Collections.Generic;

namespace Test.HandlerTests
{
    /// <summary>
    /// Used to test data loaded for items
    /// </summary>
    [TestClass]
    public class ItemsLoaderTest : BaseTest
    {

        /// <summary>
        /// Handler to use in tests
        /// </summary>
        protected Items Handler { get; set; }

        /// <summary>
        /// Initialize instance each test
        /// </summary>
        [TestInitialize]
        public void Init() {
            Handler = new Items();
        }

        /// <summary>
        /// Check data is correct against item A00001
        /// </summary>
        [TestMethod]
        public void ShouldLoadA00001Item()
        {
            using (DBConnection db = new DBConnection()) {
                Dictionary<string, string> record = Handler.LoadSingle(db);
                AssertItem(record, "A00001", "J.B. Officeprint 1420");
            }
        }

        /// <summary>
        /// Test load first 5 business partners
        /// </summary>
        [TestMethod]
        public void ShouldLoad5Items()
        {
            using (DBConnection db = new DBConnection())
            {
                List<Dictionary<string, string>> records = Handler.LoadAll(db);
                Assert.IsNotNull(records);
                Assert.AreEqual(5, records.Count);

                AssertItem(records[0], "A00001", "J.B. Officeprint 1420");
                AssertItem(records[1], "A00002", "J.B. Officeprint 1111");
                AssertItem(records[2], "A00003", "J.B. Officeprint 1186");
                AssertItem(records[3], "A00004", "Rainbow Color Printer 5.0");
                AssertItem(records[4], "A00005", "Rainbow Color Printer 7.5");
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
                string key = Handler.Create(sap, "SF01", "Awesome product");
                Assert.AreEqual("SF01", key);

                Dictionary<string, string> record = Handler.LoadByKey(db, "SF01");
                AssertItem(record, "SF01", "Awesome product");

                Assert.IsTrue(Handler.Delete(sap, "SF01"));
            }
        }


        /// <summary>
        /// Test business partner data
        /// </summary>
        /// <param name="record">Record to test</param>
        /// <param name="code">Item code</param>
        /// <param name="name">Item name</param>
        private void AssertItem(
            Dictionary<string, string> record,
            string code,
            string name)
        {
            Assert.IsNotNull(record);
            Assert.IsTrue(record.ContainsKey("ItemCode"));
            Assert.IsTrue(record.ContainsKey("ItemName"));

            Assert.AreEqual(code, record["ItemCode"]);
            Assert.AreEqual(name, record["ItemName"]);
        }

    }

}
