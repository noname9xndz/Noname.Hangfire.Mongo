using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Mongo.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Noname.Hangfire.Mongo.Models
{
    public class BackgroundJobHangfire
    {
        /// <summary>
        ///
        /// </summary>

        /// <summary>
        ///
        /// </summary>
        public static string DatabaseName;

        /// <summary>
        ///
        /// </summary>

        public static MongoClient MongoClient;

        /// <summary>
        ///
        /// </summary>
        public static string Prefix = "hangfire";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>

        public static HangfireDbContext GetDbContext()
        {
            return new HangfireDbContext(MongoClient, DatabaseName, Prefix);
        }

        /// <summary>
        ///
        /// </summary>
        public object Delete()
        {
            try
            {
                return BackgroundJobHangfire.TryDelete();
            }
            catch
            {
                return null;
            }
        }

        private static object TryDelete()
        {
            try
            {
                var result1 = TryDeleteJobSucceeded();
                return new { result1 };
            }
            catch
            {
                return null;
            }
        }

        private static DeleteResult TryDeleteJobSucceeded()
        {
            try
            {
                var col = MongoClient.GetDatabase(DatabaseName).GetCollection<BsonDocument>($"{Prefix}.jobGraph");
                var filter = new BsonDocument("StateName", new BsonDocument("$eq", "Succeeded"));
                var result = col.DeleteMany(filter);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
