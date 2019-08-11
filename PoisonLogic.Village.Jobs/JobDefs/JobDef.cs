using System.Collections.Generic;
using PoisonLogic.Dim;

namespace PoisonLogic.Village.Jobs
{
    public enum JobType
    {
        UNSET = -1,
        Constant = 0,
        Repeat = 1,
        Single = 2
    }

    public class JobDef : DimDef
    {

        public JobType JobType { get; set; }
        public int MaxWorkerCount { get; set; }
        public long TimeToComplete { get; set; }
        public IEnumerable<string> RequiredTags { get; set; }
        public IEnumerable<string> ForbidenTags { get; set; }
    }
}
