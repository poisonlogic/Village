using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Resources;

namespace Village.Social.Jobs.ResourceJobs
{
    public class BaseResourceJobInstace : BaseJobInstance, IResourceJobInstance
    {
        public ResourceManager ResourceManager { get; }
        public ResourceJobDef ResourceJobDef { get; }
        
        public bool HasActiveRequest => throw new NotImplementedException();
        public IEnumerable<string> AllResourceIds { get { return ResourceJobDef.ResourceExchanges.Select(x => x.Key); } }

        public BaseResourceJobInstace(string jobDefId, IJobProvider provider) : base(jobDefId, provider)
        {

        }

        public override bool TryFinsh()
        {
            var resRequest = new ResourceRequest
            {
                Exchanges = ResourceJobDef.ResourceExchanges,
                Priority = this.Priority,
                RequesterId = this.InstanceId
            };

            if(ResourceManager.CanFullFillRequest(resRequest))
            {
                return ResourceManager.SubitRequest(resRequest);
            }
            return false;
        }

        public IEnumerable<ResourceRequest> GetAllActiveRequest()
        {
            throw new NotImplementedException();
        }
    }
}
