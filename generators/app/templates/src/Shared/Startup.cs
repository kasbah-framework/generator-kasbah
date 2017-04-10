﻿using System;
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
            services.AddSingleton(new Kasbah.DataAccess.Npgsql.NpgsqlSettings
            {
                ConnectionString = Environment.GetEnvironmentVariable("KASBAH_DB")
            });
            services.AddTransient<Kasbah.Content.IContentProvider, Kasbah.DataAccess.Npgsql.ContentProvider>();

            // TODO: You'll probably want to change this...
            services.AddSingleton(new Kasbah.Media.LocalStorageMediaProviderSettings
            {
                ContentRoot = Path.Combine(Directory.GetCurrentDirectory(), "../_media")
            });
            services.AddTransient<IMediaStorageProvider, Kasbah.Media.LocalStorageMediaProvider>();
            // ... to maybe use S3?
            /*
            services.AddSingleton(new Kasbah.Media.S3.S3MediaProviderSettings
            {
                Region = "...",
                BucketName = "...",
                AwsAccessKeyId = "...",
                AwsSecretAccessKey = "..."
            });
            services.AddTransient<IMediaStorageProvider, Kasbah.Media.S3.S3MediaProvider>();
            */

            services.AddTransient<IKasbahWebRegistration, WebRegistration>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("KASBAH_CACHE");
                options.InstanceName = "kasbah_<%= alias %>";
            });

            return services;
        }

        public static IApplicationBuilder Use<%= namespace %>Web(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
