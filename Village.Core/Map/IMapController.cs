using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map.MapStructure;
using Village.Core.Rendering;

namespace Village.Core.Map
{
    public interface IMapController : IController
    {
        int MaxWidth { get; }
        int MaxHeight { get; }
        int MinWidth { get; }
        int MinHeight { get; }

        IMapRenderer Renderer { get; }

        void CreateEmptyLayer(string LayerName);
        IMapLayer GetLayer(string LayerName);
        IEnumerable<IMapStructure> GetMapStructsAt(string LayerName, MapSpot mapSpot);
        IEnumerable<IMapStructure> GetMapStructsAt(string LayerName, int x, int y);
        bool CanAddMapStructure(string LayerName, MapStructDef mapStruct, MapSpot anchor, MapRotation rotation);
        void AddMapStructure(IMapStructure mapStructure);
        void RemoveMapStruct(IMapStructure mapStruct);

    }
}
