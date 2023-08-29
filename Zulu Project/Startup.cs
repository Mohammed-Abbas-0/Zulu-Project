using AspCoreApI_Project.AppContext;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zulu_Project.Mapper;
using Zulu_Project.Repositories;
using Zulu_Project.Repositories.IRepositories;

namespace Zulu_Project
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
            // Service To Connect With Db
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // Auto Mapper
            services.AddAutoMapper(typeof(ObjectMapper));
            // Scopped With Repository
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Company_V1", new OpenApiInfo {
                    Title = "Zulu_Project",
                    Version = "v1",
                    Description = "Zulu Version 1 (Company)",
                    Contact = new OpenApiContact() {
                        Email = "mohammed-abbas0@outlook.com",
                        Name = "Mohammed Abbas",
                        Url = new Uri("https://www.linkedin.com/in/mohammed-abbas-8b5b76184/")

                    }


            });
                c.SwaggerDoc("Branch_V1", new OpenApiInfo
                {
                    Title = "Zulu_Project",
                    Version = "v1",
                    Description = "Zulu Version 1 (Branch)",
                    Contact = new OpenApiContact()
                    {
                        Email = "mohammed-abbas0@outlook.com",
                        Name = "Mohammed Abbas",
                        Url = new Uri("https://www.linkedin.com/in/mohammed-abbas-8b5b76184/")

                    }
                });
            });
            services.AddApiVersioning(idx=> {
                idx.AssumeDefaultVersionWhenUnspecified = true;
                idx.DefaultApiVersion = new ApiVersion(1,0);
                idx.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //  Swagger خاص بالانترفيس الخاص ب 
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/Company_V1/swagger.json", "Zulu_Project Company v1");
                    c.SwaggerEndpoint("/swagger/Branch_V1/swagger.json", "Zulu_Project Branch v1");

                }); 
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
