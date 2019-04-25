﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pivotal.Discovery.Client;
using Pivotal.Extensions.Configuration.ConfigServer;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Management.CloudFoundry;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Pivotal.NetCore.WebApi.Template
{
    using Pivotal.NetCore.WebApi.Template.Extensions;
    using Pivotal.NetCore.WebApi.Template.Validators;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public static IContainer Container { get; private set; } = new ContainerBuilder().Build();

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();

            if (this.Configuration.HasCloudFoundryServiceConfigurations())
            {
                services.AddConfiguration(this.Configuration);
                services.AddDiscoveryClient(this.Configuration);
                services.ConfigureCloudFoundryOptions(this.Configuration);
            }

            services.AddMediatR();

            services.AddActuatorsAndHealthContributors(Configuration);

            services.AddMvc().AddFluentValidation((fv) =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<ValuesRequestValidator>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Values API", Version = "v1" });
            });

            return new AutofacServiceProvider(Container = services.GetAutofacContainer());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Form API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseMvc();

            if (configuration.HasCloudFoundryServiceConfigurations())
            {
                app.UseDiscoveryClient();
                app.UseCloudFoundryActuators();
            }
        }
    }
}