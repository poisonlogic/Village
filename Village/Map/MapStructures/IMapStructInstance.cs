using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Map.MapStructures
{
    public interface IMapStructInstance
    {
        string InstanceId { get; }
        string Label { get; }
        IEnumerable<string> Tags { get; }
        string Description { get; }
        int XAnchor { get; }
        int YAnchor { get; }
        MapStructDef Def { get; }

        IEnumerable<int[]> GetFootprint();
    }
}
