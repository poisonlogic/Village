using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Time
{
    public interface ITimeKeeper
    {
        void Tick();
        string Print();
    }
}
