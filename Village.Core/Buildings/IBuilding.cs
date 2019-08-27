using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;
using Village.Core.Map.MapStructure;

namespace Village.Core.Buildings
{
    public interface IBuilding : IMapStructure
    {
        string Label { get; }

        void Update();

    }
}
