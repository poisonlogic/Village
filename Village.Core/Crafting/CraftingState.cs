using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Crafting
{
    public class CraftingState
    {
        private float _workSoFar;
        private List<ICrafter> _crafterHistory;

        public CraftingDef CraftingDef { get; }
        public float CurrentWork { get; }
        public float PercentDone => _workSoFar / CraftingDef.TotalWork;

        public CraftingState(CraftingDef craftingDef)
        {
            CraftingDef = craftingDef ?? throw new ArgumentNullException(nameof(craftingDef));

            if (!(craftingDef.TotalWork > 0))
                throw new Exception($"TotalWork of '{craftingDef.DefName}' is not greater than zero 0. Must be positive and non zero.");

            _workSoFar = 0;
            _crafterHistory = new List<ICrafter>();
        }

        public void AddWork(float work, ICrafter crafter)
        {
            if (work > 0)
            {
                _workSoFar += work;
                if(!_crafterHistory.Contains(crafter))
                {
                    _crafterHistory.Add(crafter);
                }
            }
            else
                throw new Exception($"Work is not greater than zero 0. Must be positive and non zero.");

        }


    }
}
