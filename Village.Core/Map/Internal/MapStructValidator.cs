using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map.MapStructure;

namespace Village.Core.Map.Internal
{
    internal static class MapStructValidator
    {
        public static bool ValidateDef(MapStructDef def)
        {
            if (def.Footprint == null)
                throw new Exception("MapStructDef Footprint can not be null");

            ValidateFootPrint(def);

            if (!def.FillMapSpots && def.OccupiesSides == null)
                throw new Exception("MapStructDef must fill have FillMapSpots = true OR define OccupiedSpaces");

            if(def.OccupiesSides != null)
                ValidateSides(def);

            return true;
        }

        private static bool ValidateFootPrint(MapStructDef def)
        {
            var maxX = 0;
            var maxY = 0;

            foreach (var print in def.Footprint)
            {
                if (print.Length != 2)
                    throw new Exception($"Malformed Footprint in MapStructDef '{def.DefName}'. Must be in format: [ [x,y], [x,y] ... ]");
                if (print[0] < 0 || print[1] < 0)
                    throw new Exception($"Invalid footprint for MapStructDef '{def.DefName}'. Negative number not allowed.");
                maxX = (print[0] > maxX) ? print[0] : maxX;
                maxY = (print[1] > maxY) ? print[1] : maxY;
            }

            if (maxX != def.Width - 1)
                throw new Exception($"Invalid footprint for MapStructDef '{def.DefName}'. Width must match the widest point of the footprint.");
            if (maxY != def.Height - 1)
                throw new Exception($"Invalid footprint for MapStructDef '{def.DefName}'. Height must match the tallest point of the footprint.");

            return true;
        }

        private static bool ValidateSides(MapStructDef def)
        {
            foreach(var key in def.OccupiesSides.Keys)
            {
                if(key.Length != 2)
                    throw new Exception($"Malformed OccupiesSides in MapStructDef '{def.DefName}'. One or more keys are in the format '[x,y]'.");
            }

            foreach (var print in def.Footprint)
            {
                if (!def.OccupiesSides.ContainsKey(print))
                    throw new Exception($"Occupied sides not defined for spot [{print[0]},{print[1]}].");
                if(def.OccupiesSides[print] == null || def.OccupiesSides[print].Count == 0)
                    throw new Exception($"Occupied sides for spot [{print[0]},{print[1]}] is null or empty. All spots but define at least one side.");

            }

            return true;
        }

    }
}
