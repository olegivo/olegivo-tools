using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Oleg_ivo.PrismExtensions.Extensions
{
    public static class SqlBulkCopyExtensions
    {
        public static void AddColumnMappings(this SqlBulkCopy bcp, DataTable dataTable)
        {
            bcp.AddColumnMappings(dataTable.Columns.Cast<DataColumn>().Select(col => col.ColumnName));
        }

        public static void AddColumnMappings(this SqlBulkCopy bcp, IEnumerable<string> columnNames)
        {
            foreach (var name in columnNames)
                bcp.ColumnMappings.Add(name, name);
        }

        public static void AddColumnMappings(this SqlBulkCopy bcp, IEnumerable<KeyValuePair<string, string>> columnMappings)
        {
            foreach (var columnMapping in columnMappings)
                bcp.ColumnMappings.Add(columnMapping.Key, columnMapping.Value);
        }
    }
}