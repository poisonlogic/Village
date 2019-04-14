using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Buildings
{
    public class BaseBuilding
    {
        public Guid InstanceId { get; }
        public string Name { get; private set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; private set; }
    }
}
