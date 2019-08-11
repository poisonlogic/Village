using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;

namespace Village.Map.MapStructures
{
    public interface IMapStructProvider<TDef> : IDimProvider<TDef> where TDef : MapStructDef
    {
        int Width { get; }
        int Height { get; }
        IEnumerable<Tile> AllTiles { get; }
        Tile GetTile(int x, int y);
        bool IsOpenToMapStructure(int x, int y);
    }
}
