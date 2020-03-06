using System.Collections.Generic;

namespace SAPDIExamples.Extensions
{

    /// <summary>
    /// Provide extensions for get specific records on list
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Convert Datatable content into List of dictionary values
        /// </summary>
        /// <param name="list">List to use</param>
        /// <typeparam name="T">Generic type of list</typeparam>
        /// <returns>First object or null</returns>
        public static T First<T>(this List<T> list) where T : class, new()
        {
            return list.Count > 0 ? list[0] : null;
        }

    }

}
