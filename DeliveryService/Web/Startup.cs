﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Runtime.Loader;
using System.IO;
using System.Reflection;
using DeliveryApi;
using DeliveryCore;
using DeliveryActors;

namespace DeliveryService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment _hostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IContainer ApplicationContainer { get; private set; }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var assembliesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //var controllersAssembly = Assembly.LoadFrom(Path.Combine(assembliesPath, "Controllers"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //    .AddApplicationPart(controllersAssembly).AddControllersAsServices();
            services.AddSingleton<IHostingEnvironment>(_hostingEnvironment);

            services.AddSingleton<IDeliveryService>(new DeliveryCore.DeliveryService());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            var a = typeof(DeliveryActor);
            //var config = new ConfigurationBuilder();
            //config.AddJsonFile("autofac.json");
            //IModule module = new ConfigurationModule(config.Build());// ConfigurationModule(config.Build());

            var builder = new ContainerBuilder();
            builder.Populate(services);
            //builder.RegisterModule(module);

            AssemblyLoadContext.Default.Resolving += (AssemblyLoadContext context, AssemblyName assembly) =>
            {
                var curassemblyname = $"{assembly.Name}.dll";
                return context.LoadFromAssemblyPath(Path.Combine(assembliesPath, curassemblyname));
            };
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}