using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Social.Jobs;
using Village.Social.Population.BloodLines;

namespace Village.Social.Population
{
    public class PopulationManager
    {
        private Dictionary<string, IPopInstance> _population;
        public IEnumerable<IPopInstance> AllVillagers { get { return _population.Select(x => x.Value); } }

        private BloodLineManager _bloodLineManager;


        public PopulationManager()
        {
            _population = new Dictionary<string, IPopInstance>();

        }

        public bool TryAddNewVillager(IPopInstance pop)
        {
            if(_population.ContainsKey(pop.InstanceId))
            {
                throw new Exception("Attemted to add duplicate villager " + pop.Label + " " + pop.InstanceId);
            }

            _population.Add(pop.InstanceId, pop);
            return true;

        }



        //public static IPopInstance RandomVillager()
        //{
        //    var ran = new Random();
        //    var isBoy = (ran.Next(2) > 0);
        //    if (isBoy)
        //        return new VillagerPop(NameSet.RandomName(isBoy), new List<string> { "male" });
        //    else
        //        return new VillagerPop(NameSet.RandomName(isBoy), new List<string> { "female" });

        //}



        public string PrintState()
        {
            var sb = new StringBuilder();
            sb.AppendLine("------------------------------------------------------");
            foreach (var popPair in _population)
            {
                var pop = popPair.Value;
                sb.AppendLine(string.Format("Name: {0} InstanceId: {1}", pop.Label, pop.InstanceId));
                if (pop is IJobWorker<JobDef>)
                {
                    var workPop = pop as IJobWorker<JobDef>;
                    if (workPop.JobId != null)
                    {
                        var job = JobManager<JobDef>.Instance.GetJob(workPop.JobId);
                        var pow = JobManager<JobDef>.Instance.GetJobProvider(workPop);
                        sb.AppendLine(string.Format("Job: {0}, Works At: {1}", job.JobDef.DefName, pow.Label));
                    }
                }

            }
            sb.AppendLine("------------------------------------------------------");
            return sb.ToString();
        }
    }
}
