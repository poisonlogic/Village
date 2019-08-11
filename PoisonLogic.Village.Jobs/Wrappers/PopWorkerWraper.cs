using PoisonLogic.Village.Population;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Jobs
{
    public class PopWorkerWraper : IJobWorker
    {
        private PopInstance _pop;
        private JobInstance _currentJob;

        public string Label => _pop.Label;

        public bool HasJob => _currentJob != null;

        public string JobId => _currentJob?.InstanceId ?? null;

        public JobManager GetManager()
        {
            return _currentJob.GetManager() as JobManager;
        }

        public PopWorkerWraper(PopInstance popInstance)
        {

        }
    }
}
