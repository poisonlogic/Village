﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map.Internal;

namespace Village.Core.Map
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                //.AddTransient<IMapLayer, MapLayer>()
                //.AddTransient<IMapTile, MapTile>()
                .AddSingleton<IMapController, MapController>();

            serviceCollection.AddTransient<IController, MapController>();
            return serviceCollection;
        }
    }
}
