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
        private BaseTime _time;

        public ITime Time => _time as ITime;
        public string QuickTime => _time.QuickValues;

        public TimeKeeper()
        {
            _config = ConfigLoader.LoadConfig<TimeConfig>("Village.Core.Time.Internal.TimeConfig.json");
            
            _time = new BaseTime(_config.TimeUnits);
        }

        public void Tick()
        {
            _time.TickUnit(_time.BaseUnit);
        }

        /// <summary>
        /// Prits curent values by replaceing '[UNIT_LABEL]'
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string Print(string format)
        {
            return _time.Print(format);
        }

        public void AddTime(Dictionary<string, int> values)
        {
            _time.AddTime(values);
        }

        public Dictionary<string, int> ProjectTime(Dictionary<string, int> values)
        {
            return _time.ProjectTime(values);
        }

        public int IsItTime(Dictionary<string, int> values)
        {
            return _time.CompairTime(values);
        }
    }
}
