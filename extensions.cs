using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Kartessian
{
    public static class extensions
    {
        
        public static string ToJson(this DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(rows);
        }

        public static string ToJavaScriptArray(this DataTable dt)
        {
            List<object> rows = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Count > 1)
                {
                    List<object> row = new List<object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(dr[col]);
                    }
                    rows.Add(row);
                }
                else
                {
                    rows.Add(dr[0]);
                }
            }
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(rows);
        }
    }
}
