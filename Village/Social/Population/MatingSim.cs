using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Social.Population.BloodLines;

namespace Village.Social.Population
{
    public static class MatingSim
    {
        public static List<BloodLineMember> Population;

        public static void Log(string s, bool showTime = false)
        {
            Console.Write(s);
            if (showTime)
                Console.WriteLine(" : " + DateTime.Now.ToLongTimeString());
            Console.WriteLine();
        }
        


        public static void MatingTest()
        {
            Population = new List<BloodLineMember>();
            
            var done = false;
            while(!done)
            {
                var tokens = Console.ReadLine().ToLower().Split(' ');

                switch(tokens[0])
                {
                    case "quit":
                        done = true;
                        break;

                    case "select":
                        if (tokens[1].ToLower().Equals("random"))
                            PrintPop(Population.OrderBy(x => Guid.NewGuid()).First());
                        else
                        {
                            var pop = Population.Where(x => x.PopInstance.Label.ToLower() == tokens[1]).FirstOrDefault();
                            PrintPop(pop);
                        }
                        break;

                    case "mate":
                        var a = memberFromName(tokens[1]);
                        var b = memberFromName(tokens[2]);
                        if(a == null || b == null)
                            { Console.WriteLine("one or more members count not be found"); break; }
                        var newKid = new VillagerPop("NAME", new List<string> { "tag" });
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine(string.Format("{0} and {1} gave birth to {2}", a.Name, b.Name, newKid.Name));
                        Console.WriteLine("----------------------------------------------------");
                        break;

                    case "chain":
                        var pop1 = Population.Where(x => x.PopInstance.Label.ToLower() == tokens[1]).FirstOrDefault();
                        var pop2 = Population.Where(x => x.PopInstance.Label.ToLower() == tokens[2]).FirstOrDefault();
                        if (pop1 == null || pop2 == null)
                            Console.WriteLine("One or both pops are null");
                        else
                        {
                            var relation = BloodLineManager.DeterminRelation(pop1, pop2);
                            Console.WriteLine(string.Format("Chain from {0} to {1}", pop1.PopInstance.Label, pop2.PopInstance.Label));
                            foreach (var rel in relation.RelationTypeChain)
                                Console.Write(rel.ToString() + " -> ");
                            Console.WriteLine();
                        }
                        break;

                    case "pair":
                        var popa = Population.Where(x => x.PopInstance.Label.ToLower() == tokens[1]).FirstOrDefault();
                        var popb = Population.Where(x => x.PopInstance.Label.ToLower() == tokens[2]).FirstOrDefault();
                        if (popa == null || popb == null)
                            Console.WriteLine("One or both pops are null");
                        else
                        {
                            PrintPair(popa, popb);
                        }
                        break;

                    case "add":
                        AddNewPopulation(1);
                        break;

                    case "list":
                        if (tokens.Length > 0 && tokens[1] == "all")
                            foreach (var pop in Population)
                                Console.Write(pop.Name + ", ");
                        break;

                    default:
                        Console.WriteLine("Token not recongnized");
                        break;
                }
            }
        }

        private static BloodLineMember memberFromName(string name)
        {
            var member = Population.Where(x => x.Name.ToLower() == name).FirstOrDefault();
            return member;
        }

        public static void PrintPop(BloodLineMember mem)
        {
            if (mem == null)
            {
                Console.WriteLine("NullPop given for printing");
                return;
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Name: " + mem.PopInstance.Label);
            Console.WriteLine("Parents: " + string.Join(", ", mem.Parents.Select(x => x.PopInstance.Label)));
            Console.WriteLine("Siblings: " + string.Join(", ", mem.Siblings.Select(x => x.PopInstance.Label)));
            Console.WriteLine("Children: " + string.Join(", ", mem.Children.Select(x => x.PopInstance.Label)));
            Console.WriteLine("---------------------------------------");
        }

        public static void PrintPair(BloodLineMember a, BloodLineMember b)
        {
            if (a == null || b == null)
            {
                Console.WriteLine("NullPops given for printing");
                return;
            }


            var relation = BloodLineManager.DeterminRelation(a, b);
            if (relation == null)
            {
                Console.WriteLine("Null relation");
                return;
            }
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(string.Format("Subject: {0}, Target: {1}", a.PopInstance.Label, b.PopInstance.Label));
            Console.WriteLine(string.Format("RelationChain: {0}", string.Join(" => ", relation.RelationTypeChain.Select(x => x.ToString()))));
            Console.WriteLine(string.Format("Term: {0}", relation.GetTerm()));
            if(relation.RelationDef != null)
                Console.WriteLine(string.Format("TermChain: {0}", string.Join(" => ", relation.RelationDef.RelationTypeChain.Select(x => x.ToString()))));
            Console.WriteLine("---------------------------------------");

        }

        public static void AddNewPopulation(int count)
        {
            //for(int i = 0; i < count; count--)
            //{
            //    var villager = PopulationManager.RandomVillager() as VillagerPop;
            //    var newMember = new BloodLineMember(villager);
            //    Population.Add(newMember);
            //}
        }

        public static void RandomMatingPair(out BloodLineMember a, out BloodLineMember b)
        {
            var knownFails = new List<BloodLineMember>();
            var random = new Random();
            var shuffed = Population.OrderBy(x => random.Next());
            a = null;
            b = null;
            foreach(var popA in shuffed)
            {
                if (popA.PastMates.Count() > 0)
                {
                    if (popA.Children.Count() < 5)
                    {
                        a = popA;
                        b = popA.PastMates.First();
                        return;
                    }
                }
                else
                {
                    foreach (var popB in shuffed)
                    {
                        if (popA != popB && !BloodLineManager.HasRelation(popA, popB))
                        {
                            a = popA;
                            b = popB;
                        }
                    }
                }
            };
        }

        public static bool CanMate(BloodLineMember a, BloodLineMember b)
        {
            return !BloodLineManager.HasRelation(a, b);
        }
    }
}
