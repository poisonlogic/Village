using Village.Core;
using Village.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Buildings
{
    //public class ProducerBuilding : BaseBuilding, IProducer, IModifyerHandlerHolder
    //{
    //    private ModifyerHandler ModifyerHandler { get; set; }
    //    private Dictionary<string, int> _cachedModResources { get; set; }

    //    public Dictionary<string, int> RawProducedResources { get; private set; }

    //    public Dictionary<string, int> ModProducedResources => throw new NotImplementedException();

    //    public IEnumerable<string> AllProducedResources => throw new NotImplementedException();

    //    public string Name => throw new NotImplementedException();

    //    Guid IProducer.InstanceId => throw new NotImplementedException();

    //    public ModifyerHandler GetModHandler()
    //    {
    //        return ModifyerHandler;
    //    }

    //    public bool TryAddMod(Modifyer mod)
    //    {
    //        if(ModifyerHandler.TryAddMod(mod))
    //        {
    //            this.RecachModProducedResources();
    //            return true;
    //        }
    //        return false;
    //    }

    //    protected bool RecachModProducedResources()
    //    {
    //        _cachedModResources.Clear();
    //        foreach(var resKeyVal in RawProducedResources)
    //        {
    //            var res = ResourceCatalog.All[resKeyVal.Key];
    //            var joinedTags = Tags.ToList();
    //            joinedTags.AddRange(res.Tags);
    //            ModifyerHandler.TryApplyMods(resKeyVal.Value, joinedTags);
    //        }
    //        return false;
    //    }
    //}
}
