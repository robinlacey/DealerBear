using System;
using DealerBear.Consumers;
using DealerBear.Consumers.Player.Requests;
using DealerBear.Consumers.Services.AdHoc;
using DealerBear.Consumers.Services.Response;
using DealerBear.Gateway;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CheckIfGameInProgress;
using DealerBear.UseCases.CheckIfGameInProgress.Interface;
using DealerBear.UseCases.CreateNewGame;
using DealerBear.UseCases.CreateNewGame.Interface;
using DealerBear.UseCases.GameSessionFound;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GameSessionNotFound;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBear.UseCases.GenerateSeed;
using DealerBear.UseCases.GenerateSeed.Interface;
using DealerBear.UseCases.GetGameInProgress;
using DealerBear.UseCases.GetGameInProgress.Interface;
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
                    h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"));
                    h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"));
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
            // Player 
            SetEndpointForGameRequest(cfg, host, provider);
            SetEndpointForGameResponse(cfg, host, provider);
            
            // Internal
            SetEndpointForRequestGameSessionNotFound(cfg, host, provider);
            SetEndpointForRequestGameSessionFound(cfg, host, provider);
            SetEndpointForPackNumberUpdating(cfg, host, provider);
       
        }

        private static void AddRequestClients(IServiceCollection services)
        {
            // Player 
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IGameRequest>());
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IGameResponse>());
            
            // Internal
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IGameSessionNotFoundRequest>());
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IRequestGameSessionFound>());
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IRequestPackVersionNumberUpdated>());
       
        }

        private static void SetEndpointForGameRequest(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "RequestGameData", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));
                e.Consumer<RequestGameConsumer>(provider);
                EndpointConvention.Map<IGameRequest>(e.InputAddress);
            });
        }

        private static void SetEndpointForGameResponse(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "GameDataResponse", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));
                e.Consumer<RequestGameConsumer>(provider);
                EndpointConvention.Map<IGameResponse>(e.InputAddress);
            });
        }
        private static void SetEndpointForPackNumberUpdating(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "PackNumberUpdated", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));
                e.Consumer<PackVersionUpdatedConsumer>(provider);
                EndpointConvention.Map<IRequestPackVersionNumberUpdated>(e.InputAddress);
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
                e.Consumer<GameSessionNotFoundConsumer>(provider);
                EndpointConvention.Map<IGameSessionNotFoundRequest>(e.InputAddress);
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
                e.Consumer<GameSessionFoundConsumer>(provider);
                EndpointConvention.Map<IRequestGameSessionFound>(e.InputAddress);
            });
        }

        private static void AddConsumers(IServiceCollection services)
        {
            services.AddScoped<RequestGameConsumer>();
            services.AddScoped<GameSessionFoundConsumer>();
            services.AddScoped<GameSessionNotFoundConsumer>();

            services.AddMassTransit(x =>
            {
                // add the consumer to the container
                x.AddConsumer<RequestGameConsumer>();
                x.AddConsumer<GameSessionFoundConsumer>();
                x.AddConsumer<GameSessionNotFoundConsumer>();
            });
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<ICheckIfGameInProgress, CheckIfGameInProgress>();
            services.AddScoped<IGameSessionFound, GameSessionFound>();
            services.AddScoped<IGameSessionNotFound, GameSessionNotFound>();
            services.AddScoped<IGetGameInProgress, GetGameInProgress>();
            services.AddScoped<ICreateNewGame, CreateNewGame>();
            services.AddScoped<IGenerateSeed, GenerateSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}