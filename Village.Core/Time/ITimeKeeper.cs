using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Time
{
    public interface ITimeKeeper : IController
    {
        void Tick();
        string Print(string format);
        ITime Time { get; }
        Dictionary<string, int> ProjectTime(Dictionary<string, int> values);
        void AddTime(Dictionary<string, int> values);
        int IsItTime(Dictionary<string, int> values);
    }
}
