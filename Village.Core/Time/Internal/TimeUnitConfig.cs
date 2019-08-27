using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Time.Internal
{
    public class TimeUnitConfig
    {
        public string UnitName;
        public string ParentUnit;
        public string SubscribeToUnit;
        public string Label;
        public int[] Intervals; // Number of full cycles that the lower unit should make before this unit ticks
        public string[] IntervalLabels;
        public string StringFormating;
        public bool AddOne;
    }
}
