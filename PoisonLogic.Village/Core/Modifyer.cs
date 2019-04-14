using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Core
{
    public class Modifyer
    {
        public Guid InstanceId { get; }
        public string Label { get; }
        public int AddValue { get; }
        public float MultValue { get; }
        public bool IsActive { get; }

        public IEnumerable<string> RequierdTags { get; }
        public IEnumerable<string> ForbiddenTags { get; }
    }
}
