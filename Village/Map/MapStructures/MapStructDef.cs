using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Map.MapStructures
{
    [Serializable]
    public class MapStructDef
    {
        [JsonProperty("draftName")]
        public string DraftName { get; }
        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; }
        [JsonProperty("label")]
        public string Label { get; }
        [JsonProperty("description")]
        public string Description { get; }
        [JsonProperty("width")]
        public int Width { get; }
        [JsonProperty("height")]
        public int Height { get; }
        [JsonProperty("cutouts")]
        public IEnumerable<int[]> Cutouts { get; }

        public MapStructDef(string draftName, IEnumerable<string> tags, string label, string description, int width, int height, IEnumerable<int[]> cutouts)
        {
            DraftName = draftName;
            Tags = tags;
            Label = label;
            Description = description;
            Width = width;
            Height = height;
            Cutouts = cutouts;
        }
    }
}
