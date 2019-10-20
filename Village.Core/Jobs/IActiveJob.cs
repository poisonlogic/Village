using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Jobs
{
    public interface IActiveJob
    {
        bool CanBeInterupted { get; }
        bool IsComplete { get; }

    }
}
