using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Jobs
{
    public class JobManager
    {
        private class JobRecord
        {
            public IJobInstance JobInstance;
            public List<IJobWorker> Workers;
            public IJobProvider JobProvider;
        }

        //private Dictionary<string, >
    }
}
