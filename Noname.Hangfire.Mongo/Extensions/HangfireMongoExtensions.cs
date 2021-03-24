using Microsoft.Extensions.Hosting;
using System;

namespace Noname.Hangfire.Mongo.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class HangfireMongoExtensions
    {
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value> The name of the database. </value>
        public static string NameDatabase(string env, string nameDb)
        {
            if (!env.IsNullOrWhiteSpaceCustom() && env != "prod")
            {
                return $"{nameDb}_{env.Trim().ToLower()}";
            }
            return nameDb;
        }

        /// <summary>
        /// Determines whether this instance is production.
        /// </summary>
        /// <returns> <c> true </c> if this instance is production; otherwise, <c> false </c>. </returns>
        public static bool IsProduction()
        {
            return GetEnvironmentName() == "prod";
        }

        /// <summary>
        /// Gets the name of the environment.
        /// </summary>
        /// <returns> </returns>
        public static string GetEnvironmentName()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Trim()?.ToLower();
            if (environmentName == Environments.Production)
            {
                environmentName = "prod";
            }
            else if (environmentName == Environments.Staging)
            {
                environmentName = "staging";
            }
            else if (environmentName == Environments.Development)
            {
                environmentName = "development";
            }
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = "prod";
            }
            return environmentName;
        }

        /// <summary>
        /// Determines whether [is null or white space].
        /// </summary>
        /// <param name="str"> The string. </param>
        /// <returns> <c> true </c> if [is null or white space] [the specified string]; otherwise, <c> false </c>. </returns>
        public static bool IsNullOrWhiteSpaceCustom(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static bool IsDev()
        {
            if (IsDevelopment())
            {
                return true;
            }
            return GetEnvironmentName().ToUpper() == "dev".ToUpper();
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static bool IsDevelopment()
        {
            return GetEnvironmentName().ToUpper() == "development".ToUpper();
        }
    }
}