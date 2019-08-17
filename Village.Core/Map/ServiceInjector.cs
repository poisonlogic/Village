using Microsoft.Extensions.DependencyInjection;
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
                .AddTransient<IMapController, MapController>();

            return serviceCollection;
        }
    }
}
