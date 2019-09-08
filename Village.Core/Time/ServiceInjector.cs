using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Time.Internal;

namespace Village.Core.Time
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ITimeKeeper, TimeKeeper>();
            serviceCollection.AddTransient<IController, TimeKeeper>();
            return serviceCollection;
        }
    }
}
