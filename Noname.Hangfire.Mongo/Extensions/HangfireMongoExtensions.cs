using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Noname.Hangfire.Mongo.Filters;
using Noname.Hangfire.Mongo.Models;

namespace Noname.Hangfire.Mongo.Extensions
{
    public static class HangfireMongoExtensions
    {
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value> The name of the database. </value>
        public static string NameDatabase(string env,string nameDb)
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
        public static bool isDev()
        {
            if (isDevelopment())
            {
                return true;
            }
            return GetEnvironmentName().ToUpper() == "dev".ToUpper();
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static bool isDevelopment()
        {
            return GetEnvironmentName().ToUpper() == "development".ToUpper();
        }
    }
}
