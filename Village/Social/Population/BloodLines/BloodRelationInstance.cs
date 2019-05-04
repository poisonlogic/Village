using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Population.BloodLines
{
    public enum BloodRelationType
    {
        UNSET = -1,
        Parent = 0,
        Sibling = 1,
        Child = 2
    }

    public class BloodRelationDef
    {
        public string BloodRelationName;
        public Dictionary<string, IEnumerable<string>> TermsTags;

        public List<BloodRelationType> RelationTypeChain;
    }

    public class BloodRelationInstance
    {
        public BloodLineMember Subject { get; private set; }
        public BloodLineMember Target { get; private set; }
        public List<BloodRelationType> RelationTypeChain { get; private set; }
        public BloodRelationDef RelationDef;

        public BloodRelationInstance(BloodLineMember subject, BloodLineMember target, List<BloodRelationType> relationChain, BloodRelationDef relationDef)
        {
            this.Subject = subject;
            this.Target = target;
            this.RelationTypeChain = relationChain;
            this.RelationDef = relationDef;
        }

        private string _cachedTerm;
        public string GetTerm()
        {
            if (RelationDef == null)
            {
                return default(string);
            }
            if (_cachedTerm != default(string))
                return _cachedTerm;
            // Where the term
            //      does not have a requred tag
            //          that is not in the village's tags
            var match = RelationDef.TermsTags.Where(termTag =>
                !termTag.Value.Where(tag =>
                    !Subject.Villager.Tags.Contains(tag)).Any());
            var term = default(string);
            if (match.Any())
                term = match.First().Key ?? "";
            return _cachedTerm = term;
        }
    }
}
