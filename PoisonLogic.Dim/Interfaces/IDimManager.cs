using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonLogic.Dim
{
    public interface IDimManager
    {
        IEnumerable<IDimInstance> AllInstances { get; }
        //Type TypeOfInstances => typeof(TInst);
        Type InstanceType { get; }
        //IEnumerable<Type> AllSubscribeableTypes { get; }

        //IEnumerable<Type> GetAllManagedTypes();
        //IEnumerable<Type> GetLinkableForeignInstanceTypes();
        bool TryLinkForeignInstance(IDimInstance instance);
        bool ValidateCanAccept(IDimInstance instance);

        void Update();

        string PrintState();

    }
}
