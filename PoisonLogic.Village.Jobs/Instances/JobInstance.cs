using System.Collections.Generic;
using System.Linq;
using PoisonLogic.Dim;

namespace PoisonLogic.Village.Jobs
{
    public enum JobState
    {
        UNSET = 0,
        CantRun = 1,
        Pending = 2,
        Running = 3,
        NowDone = 4
    }

    public abstract class JobInstance : DimBaseInstance<JobDef>
    {
        public JobDef JobDef { get; }
        public IEnumerable<IJobWorker> Workers { get; }
        //public IJobProvider<JobDef> JobProvider { get; }
        public bool Disabled { get; }
        public JobState JobState { get; }
        public long StartedAt { get; }
        public long WillFinishAt { get; }
        public int Priority { get; }

        public abstract bool HasOpenPosition();
        public abstract bool CanAddWorker(IJobWorker worker);
        public abstract bool TryAddWorker(IJobWorker worker);

        public override string SerializeSaveData()
        {
            throw new System.NotImplementedException();
        }
    }
}
