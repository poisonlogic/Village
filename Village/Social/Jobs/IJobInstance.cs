using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;
using Village.Core;
using Village.Core.DIMCUP;

namespace Village.Social.Jobs
{
    public enum JobState
    {
        UNSET = 0,
        CantRun = 1,
        Pending = 2,
        Running = 3,
        NowDone = 4
    }

    public interface IJobInstance<TDef> : IDimcupRunnableInstance<TDef> where TDef : JobDef
    {
        JobDef JobDef { get; }
        IEnumerable<IJobWorker<TDef>> Workers { get; }
        IJobProvider<TDef> JobProvider { get; }
        bool Disabled { get; }
        JobState JobState { get; }
        SimpleTime StartedAt();
        SimpleTime WillFinishAt();
        int Priority { get; }

        bool HasOpenPosition();
        bool TryAddWorker(IJobWorker<TDef> worker);
        bool CanAddWorker(IJobWorker<TDef> worker);
    }
}
