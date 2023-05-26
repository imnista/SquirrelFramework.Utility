using System.Collections.Specialized;
using System.Text;

namespace SquirrelFramework.Utility.Common.Http
{
    public class QueryString : Dictionary<string, string>
    {
        public NameValueCollection ParseQueryStringAsCollection(string queryString)
        {
            return System.Web.HttpUtility.ParseQueryString(queryString);
        }

        public QueryString? ParseQueryString(string queryString)
        {
            var collection = System.Web.HttpUtility.ParseQueryString(queryString);
            return collection.Cast<string>().ToDictionary(k => k, k => collection[k]) as QueryString;
        }

        public string Generate()
        {
            var removeList = (from pair in this where string.IsNullOrEmpty(pair.Value) select pair.Key).ToList();
            foreach (var item in removeList)
            {
                Remove(item);
            }

            var result = new StringBuilder();
            result.Append('?');
            foreach (var pair in this)
            {
                result.Append(pair.Key + "=");
                result.Append(pair.Value + (pair.Equals(this.Last()) ? "" : "&"));
            }
            return result.ToString();
        }

        public override string ToString()
        {
            return Generate();
        }
    }
}