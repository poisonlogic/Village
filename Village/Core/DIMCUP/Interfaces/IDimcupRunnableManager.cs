using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimRunnableManager<TDef> : IDimManager<TDef> where TDef : IDimRunnableDef
    {
        IEnumerable<IDimInstance<TDef>> AllRunningInstances { get; }
        void Update();
        void CheckForStart();
        void CheckForFinished();
        void PauseAll();
        void UnpauseAll();

    }
}
