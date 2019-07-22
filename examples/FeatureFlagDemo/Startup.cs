﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using FeatureFlagDemo.FeatureManagement;
using FeatureFlagDemo.FeatureManagement.FeatureFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureAssigners;
using Microsoft.FeatureManagement.FeatureFilters;

namespace FeatureFlagDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", false, true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddFeatureManagement()
                    .AddFeatureFilter<BrowserFilter>()
                    .AddFeatureFilter<TimeWindowFilter>()
                    .AddFeatureFilter<PercentageFilter>()
                    .AddFeatureAssigner<PercentageAssigner>()
                    .UseDisabledFeaturesHandler(new FeatureNotEnabledDisabledHandler());

            services.AddMvc(o =>
            {
                o.Filters.AddForFeature<ThirdPartyActionFilter>(nameof(MyFeatures.EnhancedPipeline));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMiddlewareForFeature<ThirdPartyMiddleware>(nameof(MyFeatures.EnhancedPipeline));

            app.UseMvc(routes =>
            {
                //
                // Use a route that requires a feature to be enabled
                //routes.MapRouteForFeature("Beta", "betaDefault", "{controller=Beta}/{action=Index}/{id?}", null, null, null);

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
