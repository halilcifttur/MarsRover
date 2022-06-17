using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace MarsRovers.App.Infrastructure;

public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        static ServiceProviderFactory()
        {
            var serviceProvider = new ServiceCollection()
               .AddMediatR(Assembly.GetExecutingAssembly());
            var container = new Container();
            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.WithDefaultConventions();
                });
                config.Populate(serviceProvider);
            });

             ServiceProvider= container.GetInstance<IServiceProvider>();
        }
    }