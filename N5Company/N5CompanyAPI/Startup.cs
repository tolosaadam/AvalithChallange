using AutoMapper;
using System;
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
using N5Company.Entities.Models;
using N5Company.Commands.UpdatePermission;
using Nest;
using N5Company.Commands.CreatePermission;
using Microsoft.OpenApi.Models;

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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    x.MigrationsAssembly("N5Company.Repositories");
                });
            });
            #endregion

            #region Elastic Search
            var elasticsearchUrl = Configuration.GetValue<string>("ElasticSearch:Url");
            var elasticsearchIndex = Configuration.GetValue<string>("ElasticSearch:Index");

            var settings = new ConnectionSettings(new Uri(elasticsearchUrl))
                .DefaultIndex(elasticsearchIndex);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "N5CompanyAPI", Version = "v1" });
            });

            services.AddControllers();

            services.AddScoped<IElasticSearchBusiness<Permission>, ElasticSearchBusiness<Permission>>();
            services.AddScoped<IPermissionBusiness, PermissionBusiness>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreatePermissionCommand).GetTypeInfo().Assembly);
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "N5CompanyAPI v1"));
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
