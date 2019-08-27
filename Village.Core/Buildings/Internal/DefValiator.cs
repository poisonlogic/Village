using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Buildings.Internal
{
    public static class DefValiator
    {
        public static bool Validate(BuildingDef def)
        {
            if(def.Footprint != null)
            {
                var maxX = 0;
                var maxY = 0;

                foreach(var print in def.Footprint)
                {
                    if (print.Length != 2)
                        throw new Exception($"Malformed Footprint in BuildingDef '{def.DefName}'. Must be in format: [ [x,y], [x,y] ... ]");
                    if (print[0] < 0 || print[1] < 0)
                        throw new Exception($"Invalid footprint for BuildingDef '{def.DefName}'. Negative number not allowed.");
                    maxX = (print[0] > maxX) ? print[0] : maxX;
                    maxY = (print[1] > maxY) ? print[1] : maxY;
                }

                if (maxX != def.Width - 1)
                    throw new Exception($"Invalid footprint for BuldingDef '{def.DefName}'. Width must match the widest point of the footprint.");
                if (maxY != def.Height - 1)
                    throw new Exception($"Invalid footprint for BuldingDef '{def.DefName}'. Height must match the tallest point of the footprint.");

            }

            return true;
        }
    }
}
