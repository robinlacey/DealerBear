using System;
using GameBear.Consumers;
using GameBear.Gateways;
using GameBear.Gateways.Interface;
using GameBear.UseCases.IsExistingSession;
using GameBear.UseCases.IsExistingSession.Interface;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameBear
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddScoped<IIsExistingSession, IsExistingSession>();
            services.AddScoped<IsExistingSessionConsumer>();
            
            services.AddMassTransit(x =>
            {
                // add the consumer to the container
                x.AddConsumer<IsExistingSessionConsumer>();
            });
            
            string rabbitMQHost = $"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST")}";
            
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                IRabbitMqHost host = cfg.Host(new Uri(rabbitMQHost), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.ReceiveEndpoint(host, "IsExistingSession", e =>
                {
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(x => x.Interval(2, 100));

                    e.Consumer<IsExistingSessionConsumer>(provider);
                    EndpointConvention.Map<IIsSessionIDInUse>(e.InputAddress);
                });
            }));

            services.AddSingleton<IGameDataGateway, InMemoryGameDataGateway>();
            
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<IIsSessionIDInUse>());
            
            services.AddSingleton<IHostedService, BusService>();
            
            
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}