//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PoisonLogic.Dim
//{
//    public abstract class DimBaseManager<TInst, TDef> : IDimManager<TInst, TDef> where TDef : DimDef where TInst : IDimInstance<TDef>
//    {
//        protected Dictionary<string, TInst> _instances;

//        public IEnumerable<TInst> AllInstances => _instances.Values;
//        public Type InstanceType => typeof(TInst);

//        public IDimCatalog<TDef> Catalog => throw new NotImplementedException();

//        public bool ValidateCanAccept(IDimInstance<DimDef> instance)
//        {
//            if (instance.Def.ManagerType != this.GetType().Name)
//                throw new Exception($"Manager type '{this.GetType().Name}' name does not match def manager type '{instance.Def.ManagerType}'");
//            if (instance.GetManager() != null)
//                throw new Exception($"Manager can not accept instance with existing manager [{instance.GetManager().GetType().Name}]");
//            return true;
//        }

//        public virtual bool TryRegisterNewInstance(TInst instance)
//        {
//            if (instance.Def == null)
//                throw new Exception("Attempted to register instance with null def");
//            if (_instances.ContainsKey(instance.InstanceId))
//                throw new Exception("Attempted to register duplicate instances");
//            //if(!Catalog.IsDefKnown(instance.Def) && !Catalog.TryRegisterNewDef(instance.Def))
//            //    throw new Exception($"Failed to register unknown def '{instance.DefName}'");


//            if (ValidateCanAccept(instance as IDimInstance<DimDef>))
//            {
//                _instances.Add(instance.InstanceId, instance);
//                return true;
//            }
//            return false;
//        }

//        public abstract bool TryDestroyInstance(string instance);
//        public abstract bool TryTransferInstance(TInst instance);
//        public abstract void InformOfInstanceChange(TInst instance);
//    }
//}
