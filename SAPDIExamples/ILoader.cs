using System.Collections.Generic;

namespace SAPDIExamples
{

    /// <summary>
    /// Used to handle loader standar
    /// </summary>
    public interface ILoader
    {

        /// <summary>
        /// Loader constant to handle
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>List of records to handle</returns>
        List<Dictionary<string, string>> LoadAll(DBConnection db);

        /// <summary>
        /// Load single data for specific record
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns>Record properties loaded</returns>
        Dictionary<string, string> LoadSingle(DBConnection db);


    }

}
