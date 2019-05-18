using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Map.MapStructures;

namespace Village.Buildings
{
    public class JobBuildingDef : MapStructDef
    {
        public IEnumerable<string> ProvidedJobNames;

        public JobBuildingDef(string draftName, IEnumerable<string> tags, string label, string description, int width, int height, IEnumerable<int[]> cutouts) : base(draftName, tags, label, description, width, height, cutouts)
        {

        }
    }
}
