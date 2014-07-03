using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Kartessian
{
    public static class tools
    {
    
        public static string AnalyzeJson(string json)
        {

            Dictionary<string, string> result = new Dictionary<string, string>();

            var root = JToken.Parse(json);

            switch (root.Type)
            {
                case JTokenType.Object:
                    return "{\"type\":\"object\",\"items\":" + AnalyzeObject((JObject)root) + "}";
                case JTokenType.Array:
                    return  "{\"type\":\"array\",\"items\":" + AnalyzeArray((JArray)root) + "}";
                default:
                    return "{\"type\":\"" + root.Type.ToString().ToLower() + "\"}";
            }


        }

        public static string AnalyzeObject(JObject token)
        {
            List<string> result = new List<string>();

            foreach (var item in token)
            {
                switch (item.Value.Type)
                {
                    case JTokenType.Object:
                        result.Add("{\"name\":\"" + item.Key + "\",\"type\":\"object\",\"items\":" + AnalyzeObject((JObject)item.Value) + "}");
                        break;
                    case JTokenType.Array:
                        result.Add("{\"name\":\"" + item.Key + "\",\"type\":\"array\",\"items\":" + AnalyzeArray((JArray)item.Value) + "}");
                        break;
                    default:
                        result.Add("{\"name\":\"" + item.Key + "\",\"type\":\"" + item.Value.Type.ToString().ToLower() + "\"}");
                        break;
                }
            }

            return "[" + string.Join(",", result.ToArray()) + "]";
        }

        public static string AnalyzeArray(JArray token)
        {
            List<string> result = new List<string>();

            var item = token[0];
                switch (item.Type)
                {
                    case JTokenType.Object:
                        result.Add("{\"type\":\"object\",\"items\":" + AnalyzeObject((JObject)item) + "}");
                        break;
                    case JTokenType.Array:
                        result.Add("{\"type\":\"array\",\"items\":" + AnalyzeObject((JObject)item) + "}");
                        break;
                    default:
                        result.Add("{\"type\":\"" + item.Type.ToString().ToLower() + "\"}");
                        break;
                }

            return "[" + string.Join(",", result.ToArray()) + "]";
        }
    }
}
