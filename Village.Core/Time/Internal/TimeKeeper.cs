using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Village.Core.Time.Internal
{
    internal class TimeKeeper : ITimeKeeper
    {
        private TimeConfig _config;
        private Dictionary<string, TimeUnitInst> _units;
        private List<TimeUnitInst> _baseUnits;

        public TimeKeeper()
        {
            _config = ConfigLoader.LoadConfig<TimeConfig>("Village.Core.Time.Internal.TimeConfig.json");

            _units = BuildTimeUnits(_config);
        }

        private Dictionary<string, TimeUnitInst> BuildTimeUnits(TimeConfig config)
        {
            var units = new Dictionary<string, TimeUnitInst>();
            foreach (var configUnit in _config.TimeUnits)
                units.Add(configUnit.UnitName, new TimeUnitInst(configUnit));

            foreach (var unit in units.Values)
            {
                unit.Subscribers = units.Values.Where(x => x.Config.SubscribeToUnit.Equals(unit.Config.UnitName));
                if (!string.IsNullOrEmpty(unit.Config.ParentUnit))
                {
                    unit.ParentUnit = units[unit.Config.ParentUnit];
                    units[unit.Config.ParentUnit].IsBase = false;
                }

                if (unit.Subscribers.Where(x => x.Config.UnitName.Equals(unit.Config.ParentUnit)).Any())
                    throw new Exception($"TimeUnit '{unit.Config.ParentUnit}' is both parent and subscriber to '{unit.Config.UnitName}'. This is not allowed.");
            }
            return units;
        }

        public void Tick()
        {
            foreach (var sub in _units.Values.Where(x => x.IsBase))
                sub.Tick();
        }

        public string Print()
        {
            var sb = new StringBuilder();
            sb.Append($"{_units["SEC"].GetLabel()}\t:\t{_units["MIN"].GetLabel()}\t:\t{_units["HOUR"].GetLabel()}\t:\t");
            sb.Append($"{_units["DAY"].GetLabel()}\t:\t{_units["WEEK"].GetLabel()}\t:\t{_units["SEAS"].GetLabel()}");
            return sb.ToString();
        }
    }
}
