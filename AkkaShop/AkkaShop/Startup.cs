using Akka.Actor;
using Akka.Configuration;
using AkkaShop.Hubs;
using DeliveryActors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationActors;
using System;

namespace AkkaShop
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // add actor system
            var system = ActorSystem.Create("ShopSystem", ShopActorSettings.config);
            services.AddSingleton(_ => system);
            // actor delegates
            services.AddSingleton<NotificationActorProvider>(provider =>
            {
                var actorSystem = provider.GetService<ActorSystem>();
                var notificationActor = actorSystem.ActorOf<NotificationActor>("NotificationActor");
                return () => notificationActor;
            });

            services.AddSingleton<DeliveryActorProvider>(provider =>
            {
                var actorSystem = provider.GetService<ActorSystem>();
                var deliveryActorSelection = actorSystem.ActorSelection(ShopActorSettings.DeliveryActorUrl);
                var deliveryActor = deliveryActorSelection.ResolveOne(TimeSpan.FromSeconds(5)).Result;
                return () => deliveryActor;
            });

            services.AddSingleton<DeliveryHub>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<DeliveryHub>("/deliver");
            });

            applicationLifetime.ApplicationStarted.Register(() =>
            {
                app.ApplicationServices.GetService<ActorSystem>();
            });

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                app.ApplicationServices.GetService<ActorSystem>().Terminate().Wait();
            });
        }
    }
}
