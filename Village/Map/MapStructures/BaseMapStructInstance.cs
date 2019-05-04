using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Map.MapStructures
{
    public class BaseMapStructInstance : IMapStructInstance
    {
        private List<int[]> _cachedFootprint;

        public string InstanceId { get; }
        public MapStructDef Def { get; }
        public IEnumerable<string> Tags { get { return Def.Tags; } }
        public string Lable { get; private set; }
        public string Description { get { return Def.Description; } }
        public int XAnchor { get; private set; }
        public int YAnchor { get; private set; }
        public int Width { get { return Def.Width; } }
        public int Height { get { return Def.Height; } }
        public bool HasCutouts { get { return ((Cutouts?.Count() ?? -1) > 0); } }
        public IEnumerable<int[]> Cutouts { get { return Def.Cutouts; } }

        public BaseMapStructInstance(MapStructDef def, int x, int y)
        {
            InstanceId = Guid.NewGuid().ToString();
            Def = def;
            Lable = def.Label;
            XAnchor = x;
            YAnchor = y;
        }

        public IEnumerable<int[]> GetFootprint()
        {
            if (_cachedFootprint != null)
                return _cachedFootprint;

            _cachedFootprint = new List<int[]>();
            for(int x = 0; x < this.Width; x++)
                for(int y = 0; y < this.Height; y++)
                {
                    if(HasCutouts && Cutouts.Where(cutout => cutout[0] == x && cutout[1] == y).Any())
                    {
                        // This tile has been cut out
                    }
                    else
                    {
                        _cachedFootprint.Add(new int[] { x + XAnchor, y + YAnchor });
                    }
                }


            return _cachedFootprint;
        }

        public void RecachFootprint()
        {
            this._cachedFootprint = null;
            GetFootprint();
        }

    }
}
