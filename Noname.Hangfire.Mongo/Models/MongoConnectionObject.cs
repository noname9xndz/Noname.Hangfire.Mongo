using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Noname.Hangfire.Mongo.Models
{
    /// <summary>
    /// </summary>
    public class MongoConnectionObject
    {
        /// <summary>
        /// Gets or sets the connection string. mongodb://[UserName]:[PassWord]@[Hosts[0],..,Hosts[n]]/[DatabaseName]?[QueryString(Options)]
        /// </summary>
        /// <value> The connection string. </value>
        public string ConnectionString
        {
            get
            {
                string connectionString = $"mongodb://";
                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(PassWord))
                {
                    connectionString = $"{connectionString}{UserName}:{PassWord}@";
                }
                else
                {
                    connectionString = $"{connectionString}";
                }

                if (Hosts != null && Hosts.Count > 0)
                {
                    Hosts = Hosts.Distinct().ToList();
                    string urlHost = "";
                    foreach (string host in Hosts)
                    {
                        urlHost += $"{host},";
                    }
                    urlHost = urlHost.TrimEnd(',');
                    connectionString = $"{connectionString}{urlHost}/admin";
                }
                if (Options != null)
                {
                    connectionString = $"{connectionString}?{ConvertObjectToStringParamUrl(Options)}";
                }
                return connectionString;
            }
        }

        /// <summary>
        /// Gets or sets the hosts.
        /// </summary>
        /// <value> The hosts. </value>
        public List<string> Hosts { set; get; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value> The options. </value>
        public ConnectionStringOptions Options { set; get; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value> The password. </value>
        public string PassWord { set; get; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value> The name of the user. </value>
        public string UserName { set; get; }

        private string ConvertObjectToStringParamUrl(object paramsObject)
        {
            if (paramsObject != null)
            {
                var data = from p in paramsObject.GetType().GetProperties()
                           where p.GetValue(paramsObject, null) != null
                           select new
                           {
                               p.Name,
                               Value = p.GetValue(paramsObject, null).ToString()
                           };
                IEnumerable<string> properties = from p in data
                                                 where !string.IsNullOrWhiteSpace(p.Value)
                                                 select $"{p.Name}={HttpUtility.UrlEncode(p.Value)}";
                string queryString = string.Join("&", properties.ToArray());
                return queryString;
            }
            return "";
        }
    }
}
