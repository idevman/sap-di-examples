using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPDIExamples;
using SAPDIExamples.Handlers;
using System.Collections.Generic;

namespace Test.HandlerTests
{
    /// <summary>
    /// Used to test data loaded for employee
    /// </summary>
    [TestClass]
    public class EmployeeLoaderTest : BaseTest
    {

        /// <summary>
        /// Handler to use in tests
        /// </summary>
        protected Employees Handler { get; set; }

        /// <summary>
        /// Initialize instance each test
        /// </summary>
        [TestInitialize]
        public void Init() {
            Handler = new Employees();
        }

        /// <summary>
        /// Test employee creation
        /// </summary>
        [TestMethod]
        public void ShouldCreateBusinessPartner()
        {
            using (SAPConnection sap = new SAPConnection())
            using (DBConnection db = new DBConnection())
            {
                List<Dictionary<string, string>> existing =  Handler.LoadByPrefix(db, "SF-");
                Assert.IsNotNull(existing);
                foreach (Dictionary<string, string> i in existing)
                {
                    Assert.IsTrue(i.ContainsKey("empID"));
                    int existingKey = int.Parse(i["empID"]);
                    Assert.IsTrue(existingKey > 0);
                    Assert.IsTrue(Handler.Delete(sap, existingKey));
                }
                existing = Handler.LoadByPrefix(db, "SF-");
                Assert.IsNotNull(existing);
                Assert.AreEqual(0, existing.Count);

                int key1 = Handler.Create(sap, "SF-John", "Smith", true);
                Assert.IsTrue(key1 > 0);

                int key2 = Handler.Create(sap, "SF-Tom", "Lance", true);
                Assert.IsTrue(key2 > 0);

                Dictionary<string, string> john = Handler.LoadByEmpId(db, key1);
                AssertEmployee(john, "SF-John", "Smith", true);

                Dictionary<string, string> tom = Handler.LoadByEmpId(db, key2);
                AssertEmployee(tom, "SF-Tom", "Lance", true);

                existing = Handler.LoadByPrefix(db, "SF-");
                Assert.IsNotNull(existing);
                Assert.AreEqual(2, existing.Count);

                Assert.IsTrue(Handler.Delete(sap, key1));
                Assert.IsTrue(Handler.Delete(sap, key2));
            }
        }


        /// <summary>
        /// Test business partner data
        /// </summary>
        /// <param name="record">Record to test</param>
        /// <param name="firstName">First name to test</param>
        /// <param name="lastName">Last name to test</param>
        /// <param name="active">Check if actuve</param>
        private void AssertEmployee(
            Dictionary<string, string> record,
            string firstName,
            string lastName,
            bool active)
        {
            Assert.IsNotNull(record);
            Assert.IsTrue(record.ContainsKey("firstName"));
            Assert.IsTrue(record.ContainsKey("lastName"));
            Assert.IsTrue(record.ContainsKey("Active"));

            Assert.AreEqual(firstName, record["firstName"]);
            Assert.AreEqual(lastName, record["lastName"]);
            Assert.AreEqual(active ? "Y" : "F", record["Active"]);
        }

    }

}
