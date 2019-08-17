using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map
{
    public interface IMapRenderer
    {
        void DrawMap(IMapController controller);
        void DrawLayer(IMapLayer layer);

    }
}
