using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPDIExamples;

namespace Test
{

    /// <summary>
    /// Used to handle generic functionallity agains all tests
    /// </summary>
    [TestClass]
    public class BaseTest
    {

        /// <summary>
        /// Used to handle loaders
        /// </summary>
        protected ILoader Loader { get; set; }

        /// <summary>
        /// Initialize connections
        /// </summary>
        public BaseTest()
        {
            DBConnection.Server = "SAP";
            DBConnection.Database = "SBODemoMX";
            DBConnection.User = "sa";
            DBConnection.Password = "F41c0n12#";
        }

    }

}
