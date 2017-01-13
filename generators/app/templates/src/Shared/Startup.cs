using System.IO;
using Kasbah.DataAccess;
using Kasbah.Media;
using Kasbah.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace <%= namespace %>
{
    public static class Registration
    {
        public static IServiceCollection Add<%= namespace %>Web(this IServiceCollection services)
        {
            services.AddSingleton(new Kasbah.DataAccess.ElasticSearch.ElasticSearchDataAccessProviderSettings
            {
                Node = "http://localhost:32769",
                IndexPrefix = "<%= alias %>"
            });
            services.AddTransient<IDataAccessProvider, Kasbah.DataAccess.ElasticSearch.ElasticSearchDataAccessProvider>();

            services.AddSingleton(new Kasbah.Media.LocalStorageMediaProviderSettings
            {
                ContentRoot = Path.Combine(Directory.GetCurrentDirectory(), "../_media")
            });
            services.AddTransient<IMediaStorageProvider, Kasbah.Media.LocalStorageMediaProvider>();

            services.AddTransient<IKasbahWebRegistration, WebRegistration>();

            return services;
        }

        public static IApplicationBuilder Use<%= namespace %>Web(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
