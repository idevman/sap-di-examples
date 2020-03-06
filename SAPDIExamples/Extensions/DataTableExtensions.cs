using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SAPDIExamples.Extensions
{

    /// <summary>
    /// Provide extension data get data table to dictionary values
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Convert Datatable content into List of dictionary values
        /// </summary>
        /// <param name="table">Data table to load</param>
        /// <returns>Object list from data table</returns>
        public static List<Dictionary<string, string>> ToList(this DataTable table)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var row in table.AsEnumerable())
            {
                Dictionary<string, string> record = new Dictionary<string, string>();
                foreach (DataColumn column in table.Columns)
                {
                    var value = row[column.ColumnName];
                    if (!(value is DBNull))
                    {
                        record[column.ColumnName] = value.ToString();
                    }
                    else
                    {
                        record[column.ColumnName] = null;
                    }
                }
                list.Add(record);
            }
            return list;
        }

    }

}
