using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Resources;
using Village.Social.Population;

namespace Village.Social.Jobs.ResourceJobs
{
    public interface IResourceJobInstance : IPopInstance, IResourceUser
    {
        ResourceManager ResourceManager { get; }
        ResourceJobDef ResourceJobDef { get; }
    }
}
