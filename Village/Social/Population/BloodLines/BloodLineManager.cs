using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Population.BloodLines
{
    public static class BloodLineManager
    {
        private static Dictionary<string, List<BloodRelationInstance>> _knownRelations;
        public static List<BloodRelationInstance> GetKnownRelations(BloodLineMember member)
        {
            if (_knownRelations == null)
                _knownRelations = new Dictionary<string, List<BloodRelationInstance>>();

            var key = member.Villager.InstanceId.ToString();
            if (_knownRelations.ContainsKey(key))
                return _knownRelations[key];

            return null;
        }

        //public static void TryAddNewRelation(BloodRelationInstance instance)
        //{
        //    var check = CheckForKnownRelation(instance.Subject, instance.Target);
        //    if (check != null)
        //        return;

        //    var key = instance.Subject.Villager.InstanceId.ToString();
        //    if (_knownRelations.ContainsKey(key))
        //        _knownRelations[key].Add(instance);
        //    else
        //        _knownRelations.Add(key, new List<BloodRelationInstance> { instance });
        //}

        //public static BloodRelationInstance CheckForKnownRelation(BloodLineMember subject, BloodLineMember target)
        //{
        //    var rel = GetKnownRelations(subject)?.Where(x => x.Target == target);
        //    if (!(rel?.Any() ?? false))
        //        return null;
        //    else
        //        return rel.Single();
        //}

        public static bool HasRelation(BloodLineMember A, BloodLineMember B, int MaxDepth = 3)
        {
            var instance = DeterminRelation(A, B, MaxDepth);
            return instance != null;
        }

        public static BloodRelationInstance DeterminRelation(BloodLineMember subject, BloodLineMember target, int MaxDepth = 3)
        {
            var relationChain = BuildRelationChain(subject, target, MaxDepth);
            if (relationChain != null)
            {
                var def = TryMatchRelationChainToDef(relationChain);
                var relation = new BloodRelationInstance(subject, target, relationChain, def);
                return relation;
            }
            return null;

        }

        public static List<BloodRelationType> BuildRelationChain(BloodLineMember A, BloodLineMember B, int MaxDepth)
        {
            var chain = new List<BloodRelationType>();
            if (_findMostDirRec(A, B, chain, MaxDepth, BloodRelationType.UNSET))
            {
                chain.Reverse();
                return chain;
            }
            else
                return null;

        }
        private static bool _findMostDirRec(BloodLineMember subject, BloodLineMember target, List<BloodRelationType> list, int depthLeft, BloodRelationType checkingFrom)
        {
            if (depthLeft == 0)
                return false;
            if (subject.Parents.Contains(target))
            {
                list.Add(BloodRelationType.Parent);
                return true;
            }
            if (subject.Siblings.Contains(target))
            {
                list.Add(BloodRelationType.Sibling);
                return true;
            }
            if (subject.Children.Contains(target))
            {
                list.Add(BloodRelationType.Child);
                return true;
            }

            if (checkingFrom != BloodRelationType.Parent)
                foreach (var parent in subject.Parents)
                    if (_findMostDirRec(parent, target, list, depthLeft - 1, BloodRelationType.Child))
                    {
                        list.Add(BloodRelationType.Parent);
                        return true;
                    }

            if (checkingFrom != BloodRelationType.Sibling)
                foreach (var sibling in subject.Siblings)
                    if (_findMostDirRec(sibling, target, list, depthLeft - 1, BloodRelationType.Sibling))
                    {
                        list.Add(BloodRelationType.Sibling);
                        return true;
                    }

            if (checkingFrom != BloodRelationType.Child)
                foreach (var child in subject.Children)
                    if (_findMostDirRec(child, target, list, depthLeft - 1, BloodRelationType.Parent))
                    {
                        list.Add(BloodRelationType.Child);
                        return true;
                    }
            return false;
        }

        public static BloodLineMember NewBloodlineMember(IEnumerable<BloodLineMember> parents, Villager villager)
        {
            var newMember = new BloodLineMember(villager);
            foreach (var parent in parents)
            {
                newMember.AddParent(parent);
                parent.AddChild(newMember);

                foreach (var otherParent in parents.Where(x => x != parent))
                    parent.AddPastMate(otherParent);

                foreach (var child in parent.Children.Where(x => x != newMember))
                {
                    child.AddSibling(newMember);
                    newMember.AddSibling(child);
                }
            }
            return newMember;
        }

        public static BloodRelationDef TryMatchRelationChainToDef(IEnumerable<BloodRelationType> chain)
        {
            var allDefs = BuildRelationDefs();

            var match = allDefs.Where(x => DoChainsMatch(x.RelationTypeChain, chain)).FirstOrDefault();

            return match;
        }

        private static bool DoChainsMatch(IEnumerable<BloodRelationType> a, IEnumerable<BloodRelationType> b)
        {
            var arr = a.ToArray();
            var brr = b.ToArray();
            if (arr.Length != brr.Length)
                return false;
            for (int i = 0; i < arr.Length && i < brr.Length; i++)
            {
                if (arr[i] != brr[i])
                    return false;
            }
            return true;
        }

        public static IEnumerable<BloodRelationDef> BuildRelationDefs()
        {
            var relations = new List<BloodRelationDef>();

            var childPar = new BloodRelationDef
            {
                BloodRelationName = "Child_Parent",
                RelationTypeChain = new List<BloodRelationType> { BloodRelationType.Child },
                TermsTags = new Dictionary<string, IEnumerable<string>>()
            };
            childPar.TermsTags.Add("Son", new List<string> { "male" });
            childPar.TermsTags.Add("Daughter", new List<string> { "female" });
            relations.Add(childPar);

            var parChild = new BloodRelationDef
            {
                BloodRelationName = "Parent_Child",
                RelationTypeChain = new List<BloodRelationType> { BloodRelationType.Parent },
                TermsTags = new Dictionary<string, IEnumerable<string>>()
            };
            parChild.TermsTags.Add("Father", new List<string> { "male" });
            parChild.TermsTags.Add("Mother", new List<string> { "female" });
            relations.Add(parChild);

            var broSis = new BloodRelationDef
            {
                BloodRelationName = "Parent_Child",
                RelationTypeChain = new List<BloodRelationType> { BloodRelationType.Sibling },
                TermsTags = new Dictionary<string, IEnumerable<string>>()
            };
            broSis.TermsTags.Add("Brother", new List<string> { "male" });
            broSis.TermsTags.Add("Sister", new List<string> { "female" });
            relations.Add(broSis);

            var grandParents = new BloodRelationDef
            {
                BloodRelationName = "Parent_Child",
                RelationTypeChain = new List<BloodRelationType> { BloodRelationType.Parent, BloodRelationType.Parent },
                TermsTags = new Dictionary<string, IEnumerable<string>>()
            };
            grandParents.TermsTags.Add("Grand Father", new List<string> { "male" });
            grandParents.TermsTags.Add("Grand Mother", new List<string> { "female" });
            relations.Add(grandParents);

            var grandChild = new BloodRelationDef
            {
                BloodRelationName = "Parent_Child",
                RelationTypeChain = new List<BloodRelationType> { BloodRelationType.Child, BloodRelationType.Child },
                TermsTags = new Dictionary<string, IEnumerable<string>>()
            };
            grandChild.TermsTags.Add("Grand Son", new List<string> { "male" });
            grandChild.TermsTags.Add("Grand Daughter", new List<string> { "female" });
            relations.Add(grandChild);

            var auntUncle = new BloodRelationDef
            {
                BloodRelationName = "AuntUncle",
                RelationTypeChain = new List<BloodRelationType> { BloodRelationType.Parent, BloodRelationType.Sibling },
                TermsTags = new Dictionary<string, IEnumerable<string>>()
            };
            auntUncle.TermsTags.Add("Uncle", new List<string> { "male" });
            auntUncle.TermsTags.Add("Aunt", new List<string> { "female" });
            relations.Add(auntUncle);

            var neiceNef = new BloodRelationDef
            {
                BloodRelationName = "Neice_Nef",
                RelationTypeChain = new List<BloodRelationType> { BloodRelationType.Sibling, BloodRelationType.Child },
                TermsTags = new Dictionary<string, IEnumerable<string>>()
            };
            neiceNef.TermsTags.Add("Nefew", new List<string> { "male" });
            neiceNef.TermsTags.Add("Neice", new List<string> { "female" });
            relations.Add(neiceNef);


            return relations;
        }
    }
}
