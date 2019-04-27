using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social
{
    public class Villager
    {
        public string Name { get; private set; }
        public Guid InstanceId { get; private set; }
        public EducationLevel EducationLevel { get; private set; }
        public PayLevel PayLevel { get; private set; }
    }
}
