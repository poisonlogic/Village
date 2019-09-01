using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Time
{
    public interface ITime
    {
        long Ticks { get; }
        void TickUnit(string unitName);
        void SetValue(string unitName, int value);
        int GetValue(string unitName);
        string Print(string formating);
        Dictionary<string, int> ProjectTime(Dictionary<string, int> values);
        int CompairTime(ITime time);
        void AddTime(Dictionary<string, int> add);
    }
}
