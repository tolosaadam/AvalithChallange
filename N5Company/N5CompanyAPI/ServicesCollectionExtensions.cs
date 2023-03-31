using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using N5Company.Business.Interfaces;
using N5Company.Business;
using N5Company.Entities.Models;
using N5Company.Repositories.Interfaces;
using N5Company.Repositories.Repositories;
using N5Company.Repositories;
using N5Company.MapperProfiles;
using N5Company.Commands.CreatePermission;
using N5Company.Commands.UpdatePermission;
using N5Company.Queries.GetPermissions;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using Microsoft.EntityFrameworkCore;

namespace N5CompanyAPI
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddAndConfigureScopes(this IServiceCollection services)
        {
            services.AddScoped<IElasticSearchBusiness<Permission>, ElasticSearchBusiness<Permission>>();
            services.AddScoped<IPermissionBusiness, PermissionBusiness>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddAndConfigureMapperProfiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new PermissionProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddAndConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreatePermissionCommand).GetTypeInfo().Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllPermissionsQuery).GetTypeInfo().Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdatePermissionCommand).GetTypeInfo().Assembly);
            });

            return services;
        }

        public static IServiceCollection AddAndConfigureElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:Url"]));
            var defaultIndex = configuration["ElasticSearch:IndexName"];
            if (!string.IsNullOrEmpty(defaultIndex))
                settings = settings.DefaultIndex(defaultIndex);
            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            services.AddSingleton(typeof(IElasticSearchBusiness<Permission>), typeof(ElasticSearchBusiness<Permission>));

            return services;
        }

        public static IServiceCollection AddAndConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    x.MigrationsAssembly("N5Company.Repositories");
                });
            });

            return services;
        }
    }
}
