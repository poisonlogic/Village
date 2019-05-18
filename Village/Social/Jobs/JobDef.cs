using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;
using Village.Core.DIMCUP;

namespace Village.Social.Jobs
{
    public enum JobType
    {
        UNSET = -1,
        Constant = 0,
        Repeat = 1,
        Single = 2
    }

    public class JobDef : IDimcupRunnableDef
    {
        public string DefName { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public JobType JobType { get; set; }
        public int MaxWorkerCount { get; set; }
        public PayLevel PayLevel { get; set; }
        public SimpleTime TimeToComplete { get; set; }
        public IEnumerable<string> RequiredTags { get; set; }
        public IEnumerable<string> ForbidenTags { get; set; }
    }
}
