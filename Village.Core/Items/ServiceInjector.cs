using Microsoft.Extensions.DependencyInjection;
using Village.Core.Items.Internal;

namespace Village.Core.Items
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                //.AddTransient<IMapLayer, MapLayer>()
                //.AddTransient<IMapTile, MapTile>()
                .AddSingleton<IItemController, ItemController>();
            serviceCollection.AddTransient<IController, ItemController>();
            return serviceCollection;
        }
    }
}
