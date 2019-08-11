using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;

namespace Village.Social.Jobs
{
    public interface IJobWorker<TDef> : IDimUser<TDef> where TDef : JobDef
    {
        string Label { get; }

        bool HasJob { get; }
        string JobId { get; }
        bool TryTransferToNewJob(IJobInstance<TDef> job);
        void FireFromJob();

    }
}
