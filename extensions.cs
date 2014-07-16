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
        
        public static string ToGEOJson(this DataTable dt, string latColumn, string lngColumn)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder line;

            foreach (DataRow r in dt.Rows)
            {
                line = new StringBuilder();


                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != latColumn && col.ColumnName != lngColumn)
                    {
                        string cValue = r[col].ToString();
                        line.Append(",\"" + col.ColumnName + "\":\"" + cValue.Replace("\"","\\\"") + "\"");
                    }

                }

                result.Append(
                    ",{\"type\":\"Feature\",\"geometry\": {\"type\":\"Point\", \"coordinates\": [" + r[lngColumn].ToString() + "," + r[latColumn].ToString() + "]},\"properties\":{" +
                    line.ToString().Substring(1) + "}}");

            }

            string geojson = "{\"type\": \"FeatureCollection\",\"features\": [" +
                result.ToString().Substring(1) + "]}";

            return geojson;
        }
        
        public static string ToJsonTable(this DataTable dt) {
            StringBuilder sb = new StringBuilder();

            foreach (DataColumn column in dt.Columns)
            {
                sb.Append(",'" + column.ColumnName + "'");
            }

            List<object[]> data = new List<object[]>();
            foreach(DataRow row in dt.Rows) {
                data.Add(row.ItemArray);
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            string json = "{\"columns\":[" + sb.ToString().Substring(1) + "],\"data\":" + serializer.Serialize(data) + "}";

            return json;
        }
        
    }
}
