using Application.Application;
using Application.Interface.Application;
using Domain.Service.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Data.Context;
using Microsoft.Practices.ServiceLocation;

namespace Infrastructure.IoC
{
    public class SimpleInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // App
            services.AddSingleton(typeof(IApplicationBase<>), typeof(ApplicationBase<>));
            services.AddSingleton<IUserApplication, UserApplication>();

            // Service
            services.AddSingleton(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddSingleton<IUserService, UserService>();

            // Repositories
            services.AddSingleton(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddScoped<AADbContext>();
        }
    }
}