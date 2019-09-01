using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Core.Time.Internal
{
    internal class BaseTime : ITime
    {
        private Dictionary<string, int> _values;
        private Dictionary<string, TimeUnitConfig> _units;
        private Dictionary<string, string> _cachedStrings;

        public long Ticks => 0;
        public string BaseUnit { get; }
        public Dictionary<string, int> Values => _values;

        public BaseTime(IEnumerable<TimeUnitConfig> timeConfigs)
        {
            if (timeConfigs == null)
                throw new ArgumentNullException(nameof(timeConfigs));

            _units = new Dictionary<string, TimeUnitConfig>();
            _values = new Dictionary<string, int>();
            _cachedStrings = new Dictionary<string, string>();
            foreach (var config in timeConfigs)
            {
                _units.Add(config.UnitName, config);
                _values.Add(config.UnitName, 0);
                _cachedStrings.Add(config.UnitName, null);
            }

            var baseUnit = _units.Values.Where(x => string.IsNullOrEmpty(x.ChildUnit) && string.IsNullOrEmpty(x.SubscribeToUnit)).ToList();
            if (!baseUnit.Any())
                throw new Exception("No base unit found for ITime. One unit must have no child or subscribed unit.");
            else if(baseUnit.Count() > 1)
                throw new Exception("Multiple base unit found for ITime. Only one unit must have no child or subscribed unit.");

            BaseUnit = baseUnit.Single().UnitName;

        }

        public string Print(string format)
        {
            var tempString = format;
            foreach (var unit in _units)
            {
                tempString = tempString.Replace("[" + unit.Key + "]", PrintUnit(unit.Key));
            }
            return tempString;
        }

        private string PrintUnit(string unitName)
        {
            var unit = GetUnit(unitName);
            var indexValue = unit.HasDynamicIntervals ? _values[unitName] % unit.Intervals.Length : _values[unitName];

            if (string.IsNullOrEmpty(_cachedStrings[unitName]))
            {
                if (unit.IntervalLabels.Length > 0)
                {
                    _cachedStrings[unitName] = unit.IntervalLabels[indexValue];
                }
                else
                {
                    var tempVal = indexValue + (unit.AddOne ? 1 : 0);
                    if (string.IsNullOrEmpty(unit.StringFormating))
                        _cachedStrings[unitName] = tempVal.ToString();
                    else
                        _cachedStrings[unitName] = tempVal.ToString(unit.StringFormating);
                }
            }

            return _cachedStrings[unitName];
        }


        /// <summary>
        /// 0 if equal. 1 if this is greater than other, -1 if this is less than other
        /// </summary>
        /// <param name="otherValues"></param>
        /// <returns></returns>
        public int CompairTime(ITime time)
        {
            var realTime = time as BaseTime;
            return CompairTime(realTime.Values);
        }

        /// <summary>
        /// 0 if equal. 1 if this is greater than other, -1 if this is less than other
        /// </summary>
        /// <param name="otherValues"></param>
        /// <returns></returns>
        public int CompairTime(Dictionary<string, int> otherValues)
        {
            return CompairUnit(otherValues, BaseUnit);
        }

        private int CompairUnit(Dictionary<string, int> others, string unitName)
        {
            var parentResult = 0;
            var parent = FindParent(unitName);
            if(parent != null)
                parentResult = CompairUnit(others, parent.UnitName);

            if (parentResult != 0)
                return parentResult;

            var val = GetValue(unitName);
            var oval = others[unitName];
            if (val == oval)
                return 0;
            return val > oval ? 1 : -1;
        }

        public void AddTime(Dictionary<string, int> addValues)
        {
            var newTime = ProjectTime(addValues);
            _values = newTime;
        }

        public Dictionary<string, int> ProjectTime(Dictionary<string, int> addValues)
        {
            var copyVals = new Dictionary<string, int>();
            foreach (var val in _values)
                copyVals.Add(val.Key, val.Value);

            foreach(var add in addValues)
            {
                var unit = GetUnit(add.Key);
                if (unit.HasDynamicIntervals)
                    throw new Exception($"Can not add time unit '{add.Key}' because interval length is dynamic.");
                copyVals = AddUnit(copyVals, unit, add.Value);
            }
            return copyVals;
        }

        public Dictionary<string, int> AddUnit(Dictionary<string, int> values, TimeUnitConfig unit, int value)
        {
            _cachedStrings[unit.UnitName] = null;

            var parent = FindParent(unit.UnitName);
            // If no parent then the unit won't need to roll over
            if (parent == null)
            {
                values[unit.UnitName] += value;
                return values;
            }

            var sumVal = value + values[unit.UnitName];

            if (!parent.HasDynamicIntervals)
            {
                var parentCount = sumVal / parent.Intervals[0];
                values = AddUnit(values, parent, parentCount);
                var remainder = sumVal - (parent.Intervals[0] * parentCount);
                values[unit.UnitName] = remainder;
                return values;
            }
            else
            {
                var pInterval = _values[parent.UnitName] % parent.Intervals.Length;
                var pIntLength = parent.Intervals[pInterval];
                var parentCount = 0;
                var remainder = sumVal;

                while(remainder > parent.Intervals[pInterval])
                {
                    remainder -= parent.Intervals[pInterval];
                    parentCount++;
                    pInterval = (pInterval + parentCount) % parent.Intervals.Length;
                }

                values = AddUnit(values, parent, parentCount);
                values[unit.UnitName] = remainder;
                return values;

            }
        }

        public int GetValue(string unitName)
        {
            if (!_units.ContainsKey(unitName))
                throw new Exception($"No unit found for name '{unitName}'");
            else
                return _values[unitName];
        }

        public void SetValue(string unitName, int value)
        {
            if (!_units.ContainsKey(unitName))
                throw new Exception($"No unit found for name '{unitName}'");
            else
                _values[unitName] = value; ;
        }

        private TimeUnitConfig GetUnit(string unitName)
        {
            if (!_units.ContainsKey(unitName))
                throw new Exception($"No unit found for name '{unitName}'");
            else
                return _units[unitName];
        }

        public void TickUnit(string unitName)
        {
            var unit = GetUnit(unitName);

            _values[unit.UnitName] = _values[unit.UnitName] + 1;
            _cachedStrings[unit.UnitName] = null;

            var subscribed = GetSubscribedUnits(unit.UnitName);
            foreach (var sub in subscribed)
                if (_values[unit.UnitName] > GetCurrentIntervalLength(sub.UnitName, _values))
                    TickUnit(sub.UnitName);

            var parentUnit = _units.Values.Where(x => x.ChildUnit.Equals(unitName)).SingleOrDefault();
            if (parentUnit != null && _values[unitName] >= GetCurrentIntervalLength(parentUnit.UnitName, _values))
            {
                _values[unitName] = 0;
                TickUnit(parentUnit.UnitName);
            }
        }

        private int GetCurrentIntervalLength(string unitName, Dictionary<string, int> values)
        {
            var unit = GetUnit(unitName);
            var val = values[unitName];
            return unit.Intervals[val % unit.Intervals.Length];
        }

        private int GetValueAsSubUnits(string unitName, Dictionary<string, int> values)
        {
            var unit = GetUnit(unitName);
            var val = values[unitName];

            if (unit.Intervals.Length == 0)
                throw new Exception($"Intervals are not deifined for time unit {unitName}. That unit should not have been allowed");

            if (!unit.HasDynamicIntervals)
                return val * SubUnitsInUnit(unitName);
            
            var fullCycles = (int)Math.Floor((decimal)val / (decimal)unit.Intervals.Length);

            var ticksInThisCycle = 0;
            for (int n = val % unit.Intervals.Length; n >= 0; n--)
                ticksInThisCycle += unit.Intervals[n];

            return ticksInThisCycle + (fullCycles * SubUnitsInUnit(unitName));
        }

        private int SubUnitsInUnit(string unitName)
        {
            var unit = GetUnit(unitName);
            if (string.IsNullOrEmpty(unit.ChildUnit))
                return 0;
            else
                return unit.Intervals.Sum(x => x);
        }

        private TimeUnitConfig FindParent(string unitName)
        {
            return _units.Values.Where(x => x.ChildUnit.Equals(unitName)).SingleOrDefault();
        }

        private List<TimeUnitConfig> GetSubscribedUnits(string unitName)
        {
            var unit = GetUnit(unitName);
            var subscribed = _units.Values.Where(x => x.SubscribeToUnit.Equals(unitName));
            return subscribed.ToList();
        }
    }
}
