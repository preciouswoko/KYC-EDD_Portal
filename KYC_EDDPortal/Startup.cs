using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KYC_EDDPortal.Data;
using KYC_EDDPortal.IServices;
using KYC_EDDPortal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC_EDDPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Cookies";
                options.DefaultChallengeScheme = "Cookies";
                options.DefaultForbidScheme = "Cookies";
            }).AddCookie("Cookies", options =>
            {
                options.LoginPath = "/Auth/Login";
            });
            // loggerFactory.AddConsole(LogLevel.Debug);
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");

            services.AddResponseCaching();
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<DapperDbContext>();

            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IOracleDataService, OracleDataService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<IAumsService, AumsService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddSingleton<IUtilityService, UtilityService>();
            services.AddHttpClient();
            services.AddMemoryCache();

            services.AddResponseCaching();
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                template: "{controller=Auth}/{action=Login}/{id?}");

                // template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
