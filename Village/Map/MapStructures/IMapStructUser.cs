using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Map.MapStructures
{
    public interface IMapStructUser
    {
        int Width { get; }
        int Height { get; }
        IEnumerable<Tile> AllTiles { get; }
        Tile GetTile(int x, int y);
        bool IsOpenToMapStructure(int x, int y);
    }
}
