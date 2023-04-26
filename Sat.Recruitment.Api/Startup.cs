using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Builders;
using Sat.Recruitment.Api.Builders.Interfaces;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Repositories.Interfaces;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Validators;
using Sat.Recruitment.Api.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
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
            services.AddScoped<IUserBuilder, UserBuilder>();
            services.AddScoped<IRepository, TextFileRepository>();

            services.AddScoped<IEnumerable<IUserValidator>>(
                s => new List<IUserValidator>()
                {
                    new NameValidator(),
                    new AddressValidator(),
                    new EmailValidator(),
                    new PhoneValidator(),
                });

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ILogger<UsersController>, Logger<UsersController>>();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
