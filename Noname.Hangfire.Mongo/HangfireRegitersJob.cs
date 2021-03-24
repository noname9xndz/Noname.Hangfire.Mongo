using Hangfire;
using Microsoft.AspNetCore.Builder;
using Noname.Hangfire.Mongo.BackgroundJobs;
using System;

namespace Noname.Hangfire.Mongo
{
    public static class HangfireRegitersJob
    {
        /// <summary>
        /// Uses the hangfire Api.
        /// </summary>
        /// <param name="app"> The application. </param>
        public static IApplicationBuilder UseHangfireJob(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<CommonJob>(obj => obj.TestJob(), Cron.Daily(), TimeZoneInfo.Local);

            return app;
        }
    }
}