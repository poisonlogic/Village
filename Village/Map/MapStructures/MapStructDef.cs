using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;
using Village.Core.DIMCUP;

namespace Village.Map.MapStructures
{
    public class MapStructDef : BaseDimDef
    {
        public string Label { get; }
        public string Description { get; }
        public int Width { get; }
        public int Height { get; }
        public IEnumerable<int[]> Cutouts { get; }

        public SpriteDetails SpriteDetails { get; set; }

        public MapStructDef(string draftName, IEnumerable<string> tags, string label, string description, int width, int height, IEnumerable<int[]> cutouts)
        {
            Label = label;
            Description = description;
            Width = width;
            Height = height;
            Cutouts = cutouts;
        }
    }
}
