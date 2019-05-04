using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;

namespace Village.Social.Jobs
{
    public enum JobType
    {
        UNSET = -1,
        Constant = 0,
        Repeat = 1,
        Single = 2
    }

    public class JobDef
    {
        public string JobName { get; set; }
        public PayLevel PayLevel { get; set; }
        public int MaxWorkerCount { get; set; }
        public JobType JobType { get; set; }
        public Dictionary<string, int> ConsumedResources { get; set; }
        public Dictionary<string, int> ProducedResources { get; set; }
        public SimpleTime TimeToComplete { get; set; }
        public EducationLevel RequiredEducationLevel { get; set; }
        public PayLevel BasePayLevel { get; set; }
        
    }
}
