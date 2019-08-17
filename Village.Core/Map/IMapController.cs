using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map
{
    public interface IMapController
    {
        int MaxWidth { get; }
        int MaxHeight { get; }
        int MinWidth { get; }
        int MinHeight { get; }

        IMapRenderer Renderer { get; }

        void CreateEmptyLayer(string LayerName);
        IMapLayer GetLayer(string LayerName);
        void AddMapObject(IMapObject mapObject);
        void RemoveMapObject(IMapObject mapObject);

    }
}
