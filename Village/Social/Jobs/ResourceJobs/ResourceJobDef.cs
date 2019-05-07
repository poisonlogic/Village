using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core;

namespace Village.Social.Jobs.ResourceJobs
{
    public class ResourceJobDef : JobDef
    {
        public Dictionary<string, int> ResourceExchanges { get; set; }
        
    }
}
