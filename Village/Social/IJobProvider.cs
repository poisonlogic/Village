using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social
{
    public interface IJobProvider
    {
        string Name { get; }
        Guid InstanceId { get; }
        IEnumerable<string> Tags { get; }

        bool HasOpenJobs { get; }
        IEnumerable<Job> GetOpenJobs();
        IEnumerable<Job> AllPossibleJobs { get; }
    }
}
