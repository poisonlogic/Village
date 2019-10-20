using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Jobs
{
    public interface ITask
    {
        bool CanStart();
        bool CanPause();
        bool CanCancel();

        void Start();
        void Pause();
        void UnPause();
        void Cancel();

        bool IsCompleted();

        void DoUpdate();
    }
}
