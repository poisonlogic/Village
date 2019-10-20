using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.Jobs;
using Village.Core.Map;

namespace Village.DesktopApp.Classes
{
    public class FakeJobWorker : IJobWorker
    {
        public MapSpot Position => throw new NotImplementedException();

        public bool MoveToSpot(MapSpot pos)
        {
            throw new NotImplementedException();
        }
    }
}
