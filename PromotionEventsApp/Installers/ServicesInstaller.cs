using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromotionEventsApp.Installers.Abstract;
using PromotionEventsApp.Profiles;
using PromotionEventsApp.Repositories;
using PromotionEventsApp.Repositories.Abstract;
using PromotionEventsApp.Services;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
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
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMobileService, MobileService>();
        }
    }
}
