using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Buildings;
using Village.Core;
using Village.Core.DIMCUP;

namespace Village.Social.Jobs
{
    public abstract class BaseJobInstance<TDef> : BaseDimcupRunnableInstance<TDef> where TDef : JobDef
    {
        private IEnumerable<IJobWorker<TDef>> _workers => _users.Select(x => x.Value as IJobWorker<TDef>);
        public string Name { get; private set; }
        private SimpleTime _startedAt;

        public BaseJobInstance(IDimcupProvider<TDef> provider, IDimcupManager<TDef> manager, TDef def) : base(provider, manager, def)
        {
        }

        public virtual bool CanAcceptWorker(IJobWorker<TDef> worker) => CanAcceptUser(worker);
        public override bool CanAcceptUser(IDimcupUser<TDef> user)
        {
            return base.CanAcceptUser(user);
        }
        
        public virtual bool TryAddWorker(IJobWorker<TDef> worker) => TryAddUser(worker);
        public override bool TryAddUser(IDimcupUser<TDef> user)
        {
            return base.TryAddUser(user);
        }
        
        public override bool TryStart()
        {
            if (this._workers.Count() == 0)
            {
                return false;
            }
            this._startedAt = SimpleTime.Now;
            return base.TryStart();
        }

        public override bool TryCancel()
        {
            return base.TryCancel();
        }

        public abstract bool TryFinsh();

        public virtual SimpleTime StartedAt()
        {
            return _startedAt;
        }

        public bool HasOpenPosition()
        {
            throw new NotImplementedException();
        }
    }
}
