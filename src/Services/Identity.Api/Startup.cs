using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Api.Messaging.Consumers;
using Identity.Api.Models;
using Identity.Api.Services;
using Identity.Api.Services.Interfaces;
using MassTransit;
using MassTransit.AutofacIntegration;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(sp =>
            {
                var configuration = new ConfigurationOptions { ResolveDns = true };
                configuration.EndPoints.Add(Configuration["RedisHost"]);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddTransient<IIdentityRepository, IdentityRepository>();
            var builder = new ContainerBuilder();

            // register a specific consumer
            builder.RegisterType<ApplicantAppliedEventConsumer>();

            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri("rabbitmq://rabbitmq/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    // https://stackoverflow.com/questions/39573721/disable-round-robin-pattern-and-use-fanout-on-masstransit
                    cfg.ReceiveEndpoint("JobsFinder" + Guid.NewGuid().ToString(), e =>
                    {
                        //e.LoadFrom(context);
                        //e.Consumer<ApplicantAppliedConsumer>();
                    });
                });

                return busControl;
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            builder.Populate(services);
            ApplicationContainer = builder.Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider, IHostApplicationLifetime lifetime)
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

            var identityRepository = serviceProvider.GetService<IIdentityRepository>();
            await identityRepository.UpdateUser(new User { Id = "1", Email = "wilson.vargas@processimlabs.com", Name = "Wilson Vargas" });

            var bus = ApplicationContainer.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
