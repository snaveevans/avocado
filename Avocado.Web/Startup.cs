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
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Avocado.Web.Swagger;
using Microsoft.AspNetCore.Identity;
using Avocado.Infrastructure.Identity;
using Avocado.Infrastructure;
using Avocado.Infrastructure.Providers;
using Avocado.Web.Models;

namespace Avocado.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddIdentityCore<Account>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Avocado API", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = @"Please enter JWT with Bearer, ""Bearer {jwt}""",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.OperationFilter<AuthResponsesOperationFilter>();
                c.DocumentFilter<EnumDocumentFilter>();
                // Set the comments path for the Swagger JSON and UI.
                var domainInfo = Assembly.GetAssembly(typeof(Event));
                var domainXmlFile = $"{domainInfo.GetName().Name}.xml";
                var domainXmlPath = Path.Combine(AppContext.BaseDirectory, domainXmlFile);
                c.IncludeXmlComments(domainXmlPath);
                var infrastructureInfo = Assembly.GetAssembly(typeof(Login));
                var infrastructureXmlFile = $"{infrastructureInfo.GetName().Name}.xml";
                var infrastructureXmlPath = Path.Combine(AppContext.BaseDirectory, infrastructureXmlFile);
                c.IncludeXmlComments(infrastructureXmlPath);
                var webXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var webXmlPath = Path.Combine(AppContext.BaseDirectory, webXmlFile);
                c.IncludeXmlComments(webXmlPath);
            });

            services.Configure<ContextOptions<AvocadoContext>>(options =>
            {
                options.ConnectionString = _configuration.GetConnectionString("AvocadoContext");
            });

            // Get options from app settings
            var jwtAppSettingOptions = _configuration.GetSection(nameof(JwtIssuerOptions));
            var secret = jwtAppSettingOptions[nameof(JwtIssuerOptions.Secret)];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var validFor = long.Parse(jwtAppSettingOptions[nameof(JwtIssuerOptions.ValidFor)]);
            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.ValidFor = TimeSpan.FromMilliseconds(validFor);
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.Configure<GoogleOptions>(options =>
            {
                options.FirebaseJson = _configuration["FirebaseJson"];
            });

            var firebaseConfig = _configuration.GetSection(nameof(FirebaseConfig));
            services.Configure<FirebaseConfig>(config =>
            {
                config.ApiKey = firebaseConfig[nameof(FirebaseConfig.ApiKey)];
                config.AuthDomain = firebaseConfig[nameof(FirebaseConfig.AuthDomain)];
                config.DatabaseURL = firebaseConfig[nameof(FirebaseConfig.DatabaseURL)];
                config.ProjectId = firebaseConfig[nameof(FirebaseConfig.ProjectId)];
            });

            services.AddDbContext<AvocadoContext>();

            services.AddScoped<IRepository<Event>, ContextRepository<Event, AvocadoContext>>();
            services.AddScoped<IRepository<Member>, ContextRepository<Member, AvocadoContext>>();
            services.AddScoped<IRepository<Account>, ContextRepository<Account, AvocadoContext>>();
            services.AddScoped<IAccountAccessor, AccountAccessor>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<EventService>();
            services.AddScoped<MemberService>();
            services.AddScoped<AuthorizationService>();
            services.AddScoped<IRepository<Login>, ContextRepository<Login, AvocadoContext>>();
            services.AddScoped<IProvider, GoogleProvider>();

            services.AddTransient<IUserStore<Account>, AvocadoUserStore>();
            services.AddTransient<IUserLoginStore<Account>, AvocadoUserStore>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Avocado API v1");
                c.DisplayOperationId();
            });

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
                    // https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/react?view=aspnetcore-2.1&tabs=netcore-cli#run-the-cra-server-independently
                    // The line below separates the SPA and API into two processes so the SPA doesn't have to rebuild every API rebuild - switch when needed
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    // spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
