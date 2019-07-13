using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using PromotionEventsApp.DAL;
using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories;
using PromotionEventsApp.Repositories.Abstract;
using PromotionEventsApp.Services;
using PromotionEventsApp.Services.Abstract;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using PromotionEventsApp.Profiles;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using PromotionEventsApp.Helpers;

namespace PromotionEventsApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(x =>
            {
                x.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }).AddSessionStateTempDataProvider(); ;
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("ConnectionString")));

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });


            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<AppDbContext>();

            var appSettingsSection = Configuration.GetSection("JWTConfiguration");
            services.Configure<JWTConfiguration>(appSettingsSection);

            // configure jwt authentication
            var jwtConfiguration = appSettingsSection.Get<JWTConfiguration>();
            var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            
            services.AddSession();

            services.AddAutoMapper(
                typeof(EventToEventViewModel).Assembly,
                typeof(EventViewModelToEvent).Assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ISpotRepository, SpotRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ISpotService, SpotService>();
            services.AddScoped<IRankRepository, RankRepository>();
            services.AddScoped<IRankingService, RankingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            //services.AddScoped<IEventRepository, EventRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
  
            app.Use(async (context, next) =>
            {
                var jwToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(jwToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + jwToken);
                }
                await next();
            });
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
