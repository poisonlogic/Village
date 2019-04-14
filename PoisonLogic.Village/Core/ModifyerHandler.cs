using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoisonLogic.Village.Core
{
    public class ModifyerHandler
    {
        private IModifyerHandlerHolder _holder { get; }
        private List<Modifyer> _activeMods { get; set; }
        private float _cachedMultMod { get; set; }
        private float _cachedAddMod { get; set; }
        private bool _dirty { get; set; }
        
        public ModifyerHandler(IModifyerHandlerHolder holder)
        {
            _activeMods = new List<Modifyer>();
            _holder = holder;
        }

        public bool TryAddMod(Modifyer mod)
        {
            if (mod.ForbiddenTags.Where(s => _holder.Tags.Contains(s)).Any())
                return false;

            if (mod.RequierdTags.Where(s => !_holder.Tags.Contains(s)).Any())
                return false;

            if (!mod.IsActive)
                return false;

            _activeMods.Add(mod);
            _dirty = true;
            return TryRecache();
        }

        private static bool DoesModApply(Modifyer mod, IEnumerable<string> tags)
        {
            // Has any forbidden tag that are included
            if (mod.ForbiddenTags.Where(f => tags.Contains(f)).Any())
                return false;

            // Has any required tags that are NOT included
            if (mod.RequierdTags.Where(r => !tags.Contains(r)).Any())
                return false;

            return true;
        }

        public bool TryRecache()
        {
            if (!_dirty)
                return false;

            _cachedAddMod = 0;
            _cachedMultMod = 0;
            for(int n = 0; n < _activeMods.Count;)
            {
                if(_activeMods[n].IsActive)
                {
                    _cachedAddMod += _activeMods[n].AddValue;
                    _cachedMultMod += _activeMods[n].MultValue;
                    n++;
                }
                else
                {
                    _activeMods.RemoveAt(n);
                }
            }
            _dirty = false;
            return true;
        }

        public float ApplyMods(float raw)
        {
            return (raw + _cachedAddMod) * _cachedMultMod;
        }
    }
}
