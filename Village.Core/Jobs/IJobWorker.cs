using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Jobs
{
    public interface IJobWorker
    {
        string Id { get; }
        MapSpot Position { get; }
        bool MoveToSpot(MapSpot pos);
    }
}
