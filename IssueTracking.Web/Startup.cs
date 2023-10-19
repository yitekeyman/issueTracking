using System;
using System.IO;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
using IssueTracking.Domain.IssueTracking;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace IssueTracking.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
           
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            
            services.AddDistributedMemoryCache();
            services.AddDataProtection();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = ".ASPNetCoreSession";
                options.Cookie.Path = "/";
            });

            services.AddAntiforgery(opts =>
            {
                opts.Cookie.Name = ".ASPNetCoreSession";
                opts.Cookie.Path = "/";
            });

            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //services.AddHostedService<TimerScheduling>();
            services.AddApplicationInsightsTelemetry(Configuration);
//
            var connString = Configuration.GetConnectionString("hrms_context");
            services.AddEntityFrameworkNpgsql().AddDbContext<LIC_HRMSContext>(options => { options.UseNpgsql(connString); });
            services.Configure<LITOptions>(Configuration.GetSection("LITOptions"));
           
            InjectDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            var options=new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                  
                }
            });

            app.UseCors("AllowAll");
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseDefaultFiles(options);
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action=Index}/{id?}");
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    spa.Options.StartupTimeout = new TimeSpan(days: 0, hours: 0, minutes: 1, seconds: 30);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            /*
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            */
        }

        private void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<IUserActionService, UserActionService>();
            services.AddTransient<IIssueTrackingService, IssueTrackingService>();
            services.AddTransient<IIssueTrackingFacade, IssueTrackingFacade>();
        }
    }
}