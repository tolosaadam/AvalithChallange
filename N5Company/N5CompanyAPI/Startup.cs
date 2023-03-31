using AutoMapper;
using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N5Company.Business;
using N5Company.Business.Interfaces;
using N5Company.MapperProfiles;
using N5Company.Repositories;
using N5Company.Repositories.Interfaces;
using N5Company.Repositories.Repositories;
using System.Reflection;
using N5Company.Queries.GetPermissions;
using System.Net.NetworkInformation;
using N5Company.Entities.Models;
using N5Company.Commands.UpdatePermission;
using N5Company.Queries.GetPermission;
using Nest;

namespace N5CompanyAPI
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
            #region Database
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("N5Company");
            });


            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                // Agregar datos
                dbContext.Permissions.Add(new Permission { Id = 1, EmployeeForename = "Diego", EmployeeSurname = "Casanova", PermissionDate = new DateTime(), PermissionTypeId = 1 });
                dbContext.Permissions.Add(new Permission { Id = 2, EmployeeForename = "Adam", EmployeeSurname = "Tolosa", PermissionDate = new DateTime(), PermissionTypeId = 2 });
                dbContext.PermissionTypes.Add(new PermissionType { Id = 1, Description = "Description 1" });
                dbContext.PermissionTypes.Add(new PermissionType { Id = 2, Description = "Description 2" });
                dbContext.SaveChanges();
            }
            #endregion

            #region Elastic Search
            var elasticsearchUrl = Configuration.GetValue<string>("ElasticSearch:Url");
            var elasticsearchIndex = Configuration.GetValue<string>("ElasticSearch:Index");

            var settings = new ConnectionSettings(new Uri(elasticsearchUrl))
                .DefaultIndex(elasticsearchIndex);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            #endregion

            services.AddControllers();

            services.AddScoped<IElasticSearchBusiness<Permission>, ElasticSearchBusiness<Permission>>();
            services.AddScoped<IPermissionBusiness, PermissionBusiness>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetPermissionQuery).GetTypeInfo().Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllPermissionsQuery).GetTypeInfo().Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdatePermissionCommand).GetTypeInfo().Assembly);
            });
            #endregion

            #region Auto Mapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new PermissionProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
