using System;
using System.Collections.Generic;
using System.Text;
using Village.Map;
using Village.Map.MapStructures;
using Village.Social.Jobs;

namespace Village.Buildings
{
    public class BaseJobBuilding : BaseMapStructInstance, IJobProvider
    {
        public BaseJobBuilding(MapStructDef def, int x, int y) : base(def, x, y)
        {

        }
    }
}
