using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Noname.Hangfire.Mongo.Filters;
using Noname.Hangfire.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Noname.Hangfire.Mongo.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class HangfireMongoBuilderExtensions
    {
        private static Dictionary<string, string> UserDashboardHangfire = new Dictionary<string, string>();

        /// <summary>
        /// Configures the hangfire storage.
        /// </summary>
        /// <param name="services"> The services. </param>
        /// <param name="configuration"> </param>
        /// <param name="prefix"> </param>
        /// <param name="environmentName"> </param>
        /// <param name="hangfireConnectionString"> </param>
        /// <param name="dbName"></param>
        /// <returns> </returns>
        public static void AddHangfireStorage(IServiceCollection services, IConfiguration configuration,
            string prefix, string environmentName, string hangfireConnectionString, string dbName)
        {
            configuration.GetSection(nameof(UserDashboardHangfire)).Bind(UserDashboardHangfire);
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
            {
                Attempts = 3
            });

            // Add framework services.
            if (!environmentName.IsNullOrWhiteSpaceCustom())
            {
                environmentName = environmentName.ToLower();
            }
            if (!HangfireMongoExtensions.IsProduction())
            {
                prefix = $"{prefix}_{Environment.UserName}".ToLower();
            }

            BackgroundJobHangfire.Prefix = prefix;
            BackgroundJobHangfire.DatabaseName = HangfireMongoExtensions.NameDatabase(environmentName, dbName);
            BackgroundJobHangfire.MongoClient = new MongoClient(hangfireConnectionString);
            services.AddHangfire(config =>
            {
                var storageOptions = new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new MigrateMongoMigrationStrategy(),
                        BackupStrategy = new NoneMongoBackupStrategy()
                    },
                    Prefix = prefix,
                    CheckConnection = false,
                    InvisibilityTimeout = TimeSpan.FromDays(2)
                };
                config.UseMongoStorage(BackgroundJobHangfire.MongoClient, BackgroundJobHangfire.DatabaseName, storageOptions);
            });
            services.AddScoped<BackgroundJobHangfire>();
        }

        /// <summary>
        /// Uses the hangfire.
        /// </summary>
        /// <param name="app"> The application. </param>
        /// <param name="workerCount"> </param>
        /// <param name="pathMatch"> </param>
        public static void UseHangfire(IApplicationBuilder app, int workerCount = 0, string pathMatch = "/job-hangfire")
        {
            var queuesHangfire = QueueCustom.GetQueues();
            queuesHangfire.AddRange(QueueCustom.GetQueues().Select(x => x.ToUpper()));
            var backgroundServerOptions = new BackgroundJobServerOptions
            {
                Queues = queuesHangfire.Distinct().ToArray()
            };
            if (workerCount > 0)
            {
                backgroundServerOptions.WorkerCount = workerCount;
            }
            app.UseHangfireServer(backgroundServerOptions);

            app.UseHangfireDashboard(pathMatch, new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter(UserDashboardHangfire) },
                IgnoreAntiforgeryToken = true,
                DisplayStorageConnectionString = false,
                DashboardTitle = $"Hangfire {BackgroundJobHangfire.Prefix}",
            });
            //Remove All Job
            RecurringJob.AddOrUpdate<BackgroundJobHangfire>(obj => obj.Delete(), Cron.Daily, queue: QueueCustom.Queue0);
        }
    }
}