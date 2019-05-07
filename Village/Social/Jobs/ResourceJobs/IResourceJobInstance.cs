using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Resources;

namespace Village.Social.Jobs.ResourceJobs
{
    public interface IResourceJobInstance : IJobInstance, IResourceUser
    {
        ResourceManager ResourceManager { get; }
        ResourceJobDef ResourceJobDef { get; }
    }
}
