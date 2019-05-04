using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Population.BloodLines
{
    public class BloodLineMember
    {
        public Villager Villager;
        public string Name { get { return Villager.Name; } }
        private List<BloodLineMember> _parents;
        private List<BloodLineMember> _siblings;
        private List<BloodLineMember> _children;
        private List<BloodLineMember> _pastMates;
        public IEnumerable<BloodLineMember> Parents { get { return _parents; } }
        public IEnumerable<BloodLineMember> Siblings { get { return _siblings; } }
        public IEnumerable<BloodLineMember> Children { get { return _children; } }
        public IEnumerable<BloodLineMember> PastMates { get { return _pastMates; } }
        public BloodLineMember(Villager villager)
        {
            this.Villager = villager;
            _parents = new List<BloodLineMember>();
            _children = new List<BloodLineMember>();
            _siblings = new List<BloodLineMember>();
            _pastMates = new List<BloodLineMember>();
        }
        public void AddParent(BloodLineMember parent)
        {
            if (!this._parents.Contains(parent))
                _parents.Add(parent);
        }
        public void AddSibling(BloodLineMember sibling)
        {
            if (!this._siblings.Contains(sibling))
                _siblings.Add(sibling);
        }
        public void AddChild(BloodLineMember child)
        {
            if (!this._children.Contains(child))
                _children.Add(child);
        }
        public void AddPastMate(BloodLineMember pastMate)
        {
            if (!this._pastMates.Contains(pastMate))
                _pastMates.Add(pastMate);
        }
    }
}
