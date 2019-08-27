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
                    if (!units.ContainsKey(unit.Config.ParentUnit))
                        throw new Exception($"Failed to find parent '{unit.Config.ParentUnit}' for time unit '{unit.Config.UnitName}'.");

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

        public string Print(string format)
        {
            var tempString = format;
            foreach(var unit in _units)
            {
                tempString = tempString.Replace("[" + unit.Key + "]", unit.Value.GetLabel());
            }
            return tempString;
        }
    }
}
