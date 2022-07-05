using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySchoolAPI.Data.Interfaces;
using MySchoolAPI.Data.Repository;
using MySchoolAPI.Uilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MySchoolAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string c = "ConnectionStrings:defaultConnection";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSignalR().AddMessagePackProtocol();

            //services.AddDbContext<IDbConnection>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddTransient<IStudentRepository>(s => new StudentRepository(Configuration[c]));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MySchoolAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySchoolAPI v1"));
            }

            // Cors
            #region [ CORS ]
            app.UseCors(options =>
            {

                options.WithOrigins("http://localhost:4200");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowCredentials();

            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HubAlert>("/hubAlerts");
            });
        }
    }
}
