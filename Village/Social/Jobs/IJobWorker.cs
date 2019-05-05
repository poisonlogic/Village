using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Jobs
{
    public interface IJobWorker
    {
        string InstanceId { get; }
        string Label { get; }
        IEnumerable<string> Tags { get; }

        string GetCurrentJobId();
        void SetCurrentJobId(string jobid);
        EducationLevel EducationLevel { get; }
    }
}
