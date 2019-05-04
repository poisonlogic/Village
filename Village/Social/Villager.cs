using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;

namespace Village.Social
{
    public class Villager
    {
        public string Name { get; private set; }
        public Guid InstanceId { get; private set; }
        public EducationLevel EducationLevel { get; private set; }
        public PayLevel PayLevel { get; private set; }
        public IBuilding PlaceOfEmployment { get; private set; }
        public IJobInstance ActiveJob { get; private set; }
        public IEnumerable<string> Tags { get; private set; }

        public Villager(string name, IEnumerable<string> Tags)
        {
            this.Name = name;
            this.Tags = Tags;
        }
    }
}
