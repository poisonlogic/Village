using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;
using Village.Core;
using Village.Social.Jobs;

namespace Village.Social
{
    public enum JobState
    {
        UNSET = 0,
        CantRun = 1,
        Pending = 2,
        Running = 3,
        NowDone = 4
    }

    public interface IJobInstance
    {
        JobDef Job { get; }
        IEnumerable<IJobWorker> Workers { get; }
        IPlaceOfWork Building { get; }
        bool Disabled { get; }
        bool Running { get; }
        JobState JobState { get; }
        SimpleTime StartedAt();
        SimpleTime WillFinishAt();

        bool TryAddWorker(IJobWorker worker);
        bool CanAddWorker(IJobWorker worker);

        bool TryStart();
        bool TryCancel();
        bool TryFinsh();
    }
}
