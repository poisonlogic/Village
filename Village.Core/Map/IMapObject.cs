using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map
{
    public interface IMapObject
    {
        IEnumerable<MapSpot> GetFootPrint();
    }
}
