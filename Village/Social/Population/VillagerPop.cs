using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;
using Village.Core.DIMCUP;
using Village.Social.Jobs;

namespace Village.Social.Population
{
    public class VillagerPop //: IPopInstance, IJobWorker
    {
        public string InstanceId { get; private set; }
        public string Name { get; private set; }
        public string Label { get { return this.Name; } }



        public EducationLevel EducationLevel { get; private set; }
        public PayLevel PayLevel { get; private set; }
        public IBuilding PlaceOfEmployment { get; private set; }
        public IEnumerable<string> Tags { get; private set; }

        

        public VillagerPop(string name, IEnumerable<string> Tags)
        {
            this.InstanceId = Guid.NewGuid().ToString();
            this.Name = name;
            this.Tags = Tags;
        }

        //private string _currentJobId;
        //bool IJobWorker.HasJob { get { return _currentJobId != null; } }
        //string IJobWorker.JobId => _currentJobId;

        //public string DefName => throw new NotImplementedException();

        //public IDimProvider<BaseDimDef> InstanceProvider => throw new NotImplementedException();

        //public IEnumerable<IDimUser<BaseDimDef>> InstanceUsers => throw new NotImplementedException();

        //bool IJobWorker.TryTransferToNewJob(IJobInstance job)
        //{
        //    _currentJobId = job.InstanceId;
        //    return true;
        //}
        //void IJobWorker.FireFromJob() { _currentJobId = null; }
    }
}
