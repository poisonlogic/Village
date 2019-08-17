using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Time.Internal
{
    internal class TimeUnitInst
    {
        public TimeUnitConfig Config { get; }
        public int Value { get; set; } 
        public int IntervalIndex { get; set; }
        public int IntervalLength => Config.Intervals[IntervalIndex];
        public IEnumerable<TimeUnitInst> Subscribers;
        public TimeUnitInst ParentUnit;
        public bool IsBase = true;

        public TimeUnitInst(TimeUnitConfig config)
        {
            this.Config = config ?? throw new ArgumentNullException(nameof(config));

            if (Config.Intervals.Length <= 0)
                throw new Exception($"Interval lengths are not defined for TimeUnit '{config.UnitName}'.");

            if (Config.Intervals.Length > 1 && Config.Intervals.Length != Config.IntervalLabels.Length)
                throw new Exception($"Intervals and Interval Label lengths do not match for TimeUnit '{Config.UnitName}'");

            if (!string.IsNullOrEmpty(config.SubscribeToUnit))
                IsBase = false;
        }

        public string GetLabel()
        {
            if(Config.IntervalLabels.Length > IntervalIndex)
            {
                return Config.IntervalLabels[IntervalIndex];
            }
            else
            {
                return Value.ToString();
            }
        }

        public bool Tick()
        {
            IntervalIndex++;
            if(IntervalIndex >= Config.Intervals.Length)
            {
                IntervalIndex = 0;
                Value++;

                foreach (var dep in Subscribers)
                    if (Value > dep.IntervalLength)
                        dep.Tick();

                if (ParentUnit != null && Value >= ParentUnit.IntervalLength)
                {
                    ParentUnit.Tick();
                    Value = 0;
                    return true;
                }
            }
            return false;

        }
    }
}
