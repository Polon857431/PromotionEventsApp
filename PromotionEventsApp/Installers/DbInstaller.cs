using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromotionEventsApp.DAL;
using PromotionEventsApp.Installers.Abstract;
using PromotionEventsApp.Models.Entities;

namespace PromotionEventsApp.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

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
        }
    }
}
