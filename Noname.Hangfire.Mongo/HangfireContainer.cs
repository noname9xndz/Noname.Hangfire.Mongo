using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noname.Hangfire.Mongo.Extensions;
using Noname.Hangfire.Mongo.Models;

namespace Noname.Hangfire.Mongo
{
    public static class HangfireContainer
    {
        public static IServiceCollection AddCustomHangfireService(this IServiceCollection services, IConfiguration configuration, string hangfireConfigKey,string databaseName)
        {
            MongoHangfireStorageConfig hangfireConfig = new MongoHangfireStorageConfig();
            configuration.GetSection(hangfireConfigKey).Bind(hangfireConfig);
            HangfireMongoBuilderExtensions.AddHangfireStorage(services, configuration, 
                "ets-api", HangfireMongoExtensions.GetEnvironmentName(), 
                hangfireConfig.ConnectionString, databaseName);

            return services;
        }

        public static IApplicationBuilder AddHangfireApp(this IApplicationBuilder app)
        {
            HangfireMongoBuilderExtensions.UseHangfire(app);
            app.UseHangfireJob();
            return app;
        }
        
        


    }
}
