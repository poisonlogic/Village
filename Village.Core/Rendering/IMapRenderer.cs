using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Rendering
{
    public interface IMapRenderer
    {
        void DrawMap(IMapController controller, object args);
        void DrawLayer(IMapLayer layer, object args);

    }
}
