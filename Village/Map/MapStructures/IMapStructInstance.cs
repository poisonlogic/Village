using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.DIMCUP;

namespace Village.Map.MapStructures
{
    public interface IMapStructInstance<TDef> : IDimInstance<TDef> where TDef : MapStructDef
    {
        string Label { get; }
        string Description { get; }
        int XAnchor { get; }
        int YAnchor { get; }
        MapStructDef MapStructDef { get; }

        IEnumerable<int[]> GetFootprint();
    }
}
