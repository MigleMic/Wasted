using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wasted.Data;
using Serilog;
using Microsoft.AspNetCore.Components.Authorization;

namespace Wasted
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<ProductService>();
            services.AddSingleton<RecipeCalcService>();
            services.AddSingleton<TipsService>();
            services.AddSingleton<RegistrationService>();
            services.AddSingleton<ExportService>();
            services.AddSingleton<JsonFileService>();
            services.AddSingleton<HttpHelper>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddSingleton<AuthenticationResult>();
            services.AddSingleton<Admins>();
            services.AddSingleton<AdminService>();
            services.AddSingleton<FridgeService>();
            services.AddSingleton<AddRecipeService>();
            services.AddSingleton<CalendarService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
