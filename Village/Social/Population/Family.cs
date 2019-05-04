using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Population
{

    public class HouseHoldRelation
    {
        public Villager partyA;
        public Villager partyB;
        public string test;
    }

    public class HouseHold
    {
        public IEnumerable<Villager> AllMembers { get; }
        public Villager HomeOwner { get; private set; }
    }
}
