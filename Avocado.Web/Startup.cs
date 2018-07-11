using System.Text;
using Avocado.Domain.Entities;
using Avocado.Domain.Interfaces;
using Avocado.Domain.Services;
using Avocado.Infrastructure.Authentication;
using Avocado.Infrastructure.Context;
using Avocado.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Avocado.Web
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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();

            services.Configure<ContextOptions<AvocadoContext>>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("AvocadoContext");
            });

            services.Configure<ContextOptions<IdentityContext>>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("IdentityContext");
            });

            services.Configure<LoginOptions>(options =>
            {
                options.Issuer = Configuration["JwtIssuer"];
                options.MillisecondsUntilExpiration = long.Parse(Configuration["JwtExpireMilliseconds"]);
                options.Key = Configuration["JwtKey"];
            });

            services.AddDbContext<AvocadoContext>();
            services.AddDbContext<IdentityContext>();

            services.AddScoped<IRepository<Event>, ContextRepository<Event, AvocadoContext>>();
            services.AddScoped<IRepository<Invitee>, ContextRepository<Invitee, AvocadoContext>>();
            services.AddScoped<IRepository<Account>, ContextRepository<Account, AvocadoContext>>();
            services.AddScoped<IAccountAccessor,AccountAccessor>();
            services.AddScoped<EventService>();
            services.AddScoped<AuthorizationService>();

            services.AddScoped<IRepository<Login>, ContextRepository<Login, IdentityContext>>();
            services.AddScoped<LoginService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"]))
                    };
                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
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
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
