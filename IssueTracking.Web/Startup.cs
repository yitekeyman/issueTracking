using System;
using System.IO;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
using IssueTracking.Domain.IssueTracking;
using IssueTracking.Domain.Reports;
using IssueTracking.Domain.UnscrConsolidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IssueTracking.Web
{
    public class Startup
    {
      
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => 
            {
                options.EnableEndpointRouting = false; // Disable endpoint routing
            });

            services.AddSpaStaticFiles(config => config.RootPath = "ClientApp/dist");
            services.AddDistributedMemoryCache();
            services.AddDataProtection();
            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.Name = ".ASPNetCoreSession";
                options.Cookie.Path = "/";
            });
            services.AddAntiforgery(opts => 
            {
                opts.Cookie.Name = ".ASPNetCoreSession";
                opts.Cookie.Path = "/";
            });
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = 
                        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<LIC_HRMSContext>(options => options.UseNpgsql(Configuration.GetConnectionString("hrms_context")));
            services.Configure<LITOptions>(Configuration.GetSection("LITOptions"));
            InjectDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseCors("AllowAll");
            app.UseMvc(routes => 
            {
                routes.MapRoute("default", "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => 
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                    spa.UseAngularCliServer("start");
            });
        }

        private void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<IUserActionService, UserActionService>();
            services.AddTransient<IIssueTrackingService, IssueTrackingService>();
            services.AddTransient<IIssueTrackingFacade, IssueTrackingFacade>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IReportFacade, ReportFacade>();
            services.AddTransient<IUNSCRConsolidationFacade, UnscrConsolidationFacade>();
            services.AddTransient<IUNSCRConsolidationServices, UnscrConsolidationServices>();
        }
    }
}