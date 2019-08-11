using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Dim
{
    public abstract class DimBaseInstance<TDef> : IDimInstance where TDef :DimDef
    {
        protected IDimManager _manager;
        public string InstanceId { get; }
        public string DefName { get; }
        public TDef DimDef { get; }
        public IEnumerable<string> AllTags { get; }

        DimDef IDimInstance.DimDef => DimDef as DimDef;

        public virtual IDimManager GetManager()
        {
            if (_manager == null)
                return null;
            else
                return _manager;
        }

        public abstract string SerializeSaveData();
    }
}
