using System;
using DealerBear.Consumers;
using DealerBear.Gateway;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GameSessionFound;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GameSessionNotFound;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBear.UseCases.GenerateSeed;
using DealerBear.UseCases.GenerateSeed.Interface;
using DealerBear.UseCases.GetCurrentGameState;
using DealerBear.UseCases.GetCurrentGameState.Interface;
using DealerBear.UseCases.RequestGameData;
using DealerBear.UseCases.RequestGameData.Interface;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DealerBear
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddUseCases(services);
            AddConsumers(services);

            string rabbitMQHost = $"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST")}";

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                IRabbitMqHost host = cfg.Host(new Uri(rabbitMQHost), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                SetEndPoints(cfg, host, provider);
            }));

            AddGateways(services);

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            AddRequestClients(services);

            services.AddSingleton<IHostedService, BusService>();
        }

        private static void AddGateways(IServiceCollection services)
        {
            services.AddSingleton<IAwaitingResponseGateway, InMemoryAwaitingResponseGateway>();
            services.AddSingleton<IPackVersionGateway, InMemoryPackVersionGateway>();
        }

        private static void SetEndPoints(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            SetEndpointForGameRequest(cfg, host, provider);
            SetEndpointForRequestGameSessionNotFound(cfg, host, provider);
            SetEndpointForRequestGameSessionFound(cfg, host, provider);
            SetEndpointForPackNumberUpdating(cfg, host, provider);
        }

        private static void AddRequestClients(IServiceCollection services)
        {
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IGameRequest>());
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IRequestGameSessionNotFound>());
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IRequestGameSessionFound>());
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IRequestPackNumberUpdated>());
        }

        private static void SetEndpointForGameRequest(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "RequestGameData", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));
                e.Consumer<RequestGameDataConsumer>(provider);
                EndpointConvention.Map<IGameRequest>(e.InputAddress);
            });
        }


        private static void SetEndpointForPackNumberUpdating(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "PackNumberUpdated", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));
                e.Consumer<RequestPackVersionUpdatedConsumer>(provider);
                EndpointConvention.Map<IRequestPackNumberUpdated>(e.InputAddress);
            });
        }

        private static void SetEndpointForRequestGameSessionNotFound(IRabbitMqBusFactoryConfigurator cfg,
            IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "RequestGameSessionNotFound", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));
                e.Consumer<RequestGameSessionNotFoundConsumer>(provider);
                EndpointConvention.Map<IRequestGameSessionNotFound>(e.InputAddress);
            });
        }

        private static void SetEndpointForRequestGameSessionFound(IRabbitMqBusFactoryConfigurator cfg,
            IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "RequestGameSessionFound", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));
                e.Consumer<RequestGameSessionFoundConsumer>(provider);
                EndpointConvention.Map<IRequestGameSessionFound>(e.InputAddress);
            });
        }

        private static void AddConsumers(IServiceCollection services)
        {
            services.AddScoped<RequestGameDataConsumer>();
            services.AddScoped<RequestGameSessionFoundConsumer>();
            services.AddScoped<RequestGameSessionNotFoundConsumer>();

            services.AddMassTransit(x =>
            {
                // add the consumer to the container
                x.AddConsumer<RequestGameDataConsumer>();
                x.AddConsumer<RequestGameSessionFoundConsumer>();
                x.AddConsumer<RequestGameSessionNotFoundConsumer>();
            });
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRequestGameData, RequestGameData>();
            services.AddScoped<IGameSessionFound, GameSessionFound>();
            services.AddScoped<IGameSessionNotFound, GameSessionNotFound>();
            services.AddScoped<IGetCurrentGameState, GetCurrentGameState>();
            services.AddScoped<ICreateGameState, CreateGameState>();
            services.AddScoped<IGenerateSeed, GenerateSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}