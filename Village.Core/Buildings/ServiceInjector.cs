using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Buildings.Internal;
using Village.Core.Map.Internal;

namespace Village.Core.Buildings
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IBuildingController, BuildingController>();

            return serviceCollection;
        }
    }
}
