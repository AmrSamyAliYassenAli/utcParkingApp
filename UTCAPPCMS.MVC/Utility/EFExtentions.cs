using Microsoft.Extensions.DependencyInjection;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.DAL.Repository.Repos;
using Microsoft.EntityFrameworkCore;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.MVC.Helpers;
using UTCAPPCMS.DAL.Repository.Interfaces.Api;
using UTCAPPCMS.DAL.Repository.Repos.Api;

namespace UTCAPPCMS.MVC.Utility
{
    public static class EFExtentions
    {
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UTCAPPCMS_DBContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOdWork<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IdefaultRepository), typeof(defaultRepository));
            services.AddScoped<QRCodeService>();
            services.AddScoped(typeof(IAppRepository), typeof(AppRepository));
            services.AddScoped(typeof(ISubscriptionRepos), typeof(SubscriptionRepos));
            
            services.AddScoped(typeof(ISubscriptionDurationsRops), typeof(SubscriptionDurationsRops));
            services.AddScoped(typeof(ICustomerRepos), typeof(CustomerRepository));

            services.AddScoped(typeof(IUserManagmentRepos), typeof(UserManagmentRepos));
            services.AddScoped(typeof(IGroupPrivilageRepos), typeof(GroupPrivilageRepos));
            services.AddScoped(typeof(ITariffRepos), typeof(TariffRepos));
            services.AddScoped(typeof(ISystemLogger), typeof(SystemLogger));
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Place Info Service API",
                    Version = "v2",
                    Description = "Sample service for Learner",
                });
            });
            return services;
        }
    }
}
