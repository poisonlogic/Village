using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public enum RunnableInstanceState
    {
        Errored = -1,
        New = 1,
        Waiting = 2,
        Running = 4,
        Paused = 5,
        Canceling = 6,
        Finished = 7,
        Dead = 8
    }

    public interface IDimRunnableInstance<T> : IDimInstance<T> where T : IDimRunnableDef
    {
        RunnableInstanceState RunState { get; }
        bool IsActive { get; }
        bool IsReadyToStart();
        bool TryStart();

        bool TryPause();
        bool TryUnPause();

        void FlagForCancel();
        bool TryCancel();

        bool IsReadyToFinish();
        bool TryFinish();
        bool TryCleanUp();
    }
}
