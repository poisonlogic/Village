using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimcupRunnableInstance<TDef> : BaseDimcupInstance<TDef>, IDimcupRunnableInstance<TDef> where TDef : IDimcupRunnableDef
    {
        protected RunnableInstanceState _state;
        
        public virtual RunnableInstanceState RunState { get { return _state; } }
        public virtual bool IsActive { get; }

        public BaseDimcupRunnableInstance(IDimcupProvider<TDef> provider, IDimcupManager<TDef> manager, TDef def) : base(provider, manager, def)
        {

        }
        
        public virtual bool IsReadyToStart()
        {
            return this._state == RunnableInstanceState.Waiting;
        }

        public virtual bool TryStart()
        {
            if(this._state == RunnableInstanceState.Waiting)
            {
                this._state = RunnableInstanceState.Running;
                return true;
            }
            else
                return false;
        }

        public virtual bool TryPause()
        {
            if (this._state == RunnableInstanceState.Running)
            {
                this._state = RunnableInstanceState.Paused;
                return true;
            }
            else
                return false;
        }

        public virtual bool TryUnPause()
        {
            if (this._state == RunnableInstanceState.Paused)
            {
                this._state = RunnableInstanceState.Running;
                return true;
            }
            else
                return false;
        }

        public virtual void FlagForCancel()
        {
            if (this._state == RunnableInstanceState.Running)
            {
                this._state = RunnableInstanceState.Canceling;
            }
        }

        public virtual bool TryCancel()
        {
            if (this._state == RunnableInstanceState.Canceling)
            {
                this._state = RunnableInstanceState.Dead;
                return true;
            }
            else
                return false;
        }

        public virtual bool IsReadyToFinish()
        {
            throw new NotImplementedException();
        }

        public virtual bool TryFinish()
        {
            if (this._state == RunnableInstanceState.Finished)
            {
                this._state = RunnableInstanceState.Dead;
                return true;
            }
            else
                return false;
        }

        public virtual bool TryCleanUp()
        {
            _provider = null;
            _users.Clear();
            _users = null;
            return true;
        }
    }
}
