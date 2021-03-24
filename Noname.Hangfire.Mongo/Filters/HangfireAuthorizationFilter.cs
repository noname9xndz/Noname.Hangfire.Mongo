using Hangfire.Dashboard;
using Noname.Hangfire.Mongo.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Noname.Hangfire.Mongo.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly Dictionary<string, string> _userDashboardHangfire = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HangfireAuthorizationFilter"/> class.
        /// </summary>
        /// <param name="userAndPwd"></param>
        public HangfireAuthorizationFilter(Dictionary<string, string> userAndPwd)
        {
            if (userAndPwd != null)
            {
                _userDashboardHangfire = userAndPwd;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context)
        {
            if (HangfireMongoExtensions.IsDev()) return true;
            return AuthorizeBasic(context);
        }

        /// <summary>
        /// Authorizes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public bool AuthorizeBasic(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            string header = httpContext.Request.Headers["Authorization"];
            if (header.IsNullOrWhiteSpaceCustom() == false)
            {
                AuthenticationHeaderValue authValues = AuthenticationHeaderValue.Parse(header);
                if ("Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
                {
                    string parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
                    var parts = parameter.Split(':');
                    if (parts.Length > 1)
                    {
                        string username = parts[0];
                        string password = parts[1];
                        return CheckPassword(username, password);
                    }
                }
            }
            return AddLoginChallenge(context);
        }

        private bool AddLoginChallenge(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"Hangfire-Dashboard\"");
            return false;
        }

        private bool CheckPassword(string username, string password)
        {
            if (!username.IsNullOrWhiteSpaceCustom() && !password.IsNullOrWhiteSpaceCustom() && _userDashboardHangfire.ContainsKey(username))
            {
                return password == _userDashboardHangfire[username];
            }
            return false;
        }
    }
}