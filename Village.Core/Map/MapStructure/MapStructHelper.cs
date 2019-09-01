using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Core.Map.MapStructure
{
    public static class MapStructHelper
    {
        private static int[] RotatePrint(int[] print, MapRotation rotation)
        {
            switch (rotation)
            {
                case MapRotation.Default:
                    return new int[] { print[0], print[1] };
                case MapRotation.Clockwise:
                    return new int[] { print[1], -print[0] };
                case MapRotation.Backwards:
                    return new int[] { -print[0], -print[1] };
                case MapRotation.CounterClockwise:
                    return new int[] { -print[1], print[0] };
            }
            throw new ArgumentOutOfRangeException(nameof(rotation));
        }

        public static IEnumerable<int[]> RotateFootprint(IEnumerable<int[]> footprint, MapRotation rotation)
        {
            foreach(var print in footprint)
            {
                yield return RotatePrint(print, rotation);
            }
        }
        
        public static Dictionary<Tuple<int, int>, MapSpot> FootprintToMapSpotsDictionary(IEnumerable<int[]> footprint, MapRotation rotation, MapSpot anchor)
        {
            if (footprint.Any(print => print.Length != 2))
                throw new Exception($"Malformed footprint at least one print is not int[2]");

            var rotated = RotateFootprint(footprint, rotation);

            var dic = new Dictionary<Tuple<int, int>, MapSpot>();
            foreach (var print in rotated)
                dic.Add(new Tuple<int, int>(print[0], print[1]), new MapSpot(anchor.X + print[0], anchor.Y + print[1]));

            return dic;
        }

        internal static IEnumerable<MapStructSide> RotateOccupiedSides(IEnumerable<MapStructSide> sides, MapRotation rotation)
        {
            foreach(var side in sides)
            {
                if (rotation == MapRotation.Default)
                {
                    yield return side;
                    continue;
                }

                switch (side)
                {
                    case MapStructSide.Center:
                        yield return MapStructSide.Center;
                        continue;
                    case MapStructSide.Top:
                        yield return MapStructSide.Top;
                        continue;
                    case MapStructSide.Bottom:
                        yield return MapStructSide.Bottom;
                        continue;

                    case MapStructSide.Forward:
                        if (rotation == MapRotation.Clockwise)
                            yield return MapStructSide.Right;
                        else if (rotation == MapRotation.Backwards)
                            yield return MapStructSide.Back;
                        else if (rotation == MapRotation.CounterClockwise)
                            yield return MapStructSide.Left;
                        continue;

                    case MapStructSide.Right:
                        if (rotation == MapRotation.Clockwise)
                            yield return MapStructSide.Back;
                        else if (rotation == MapRotation.Backwards)
                            yield return MapStructSide.Left;
                        else if (rotation == MapRotation.CounterClockwise)
                            yield return MapStructSide.Forward;
                        continue;

                    case MapStructSide.Back:
                        if (rotation == MapRotation.Clockwise)
                            yield return MapStructSide.Left;
                        else if (rotation == MapRotation.Backwards)
                            yield return MapStructSide.Forward;
                        else if (rotation == MapRotation.CounterClockwise)
                            yield return MapStructSide.Right;
                        continue;

                    case MapStructSide.Left:
                        if (rotation == MapRotation.Clockwise)
                            yield return MapStructSide.Forward;
                        else if (rotation == MapRotation.Backwards)
                            yield return MapStructSide.Right;
                        else if (rotation == MapRotation.CounterClockwise)
                            yield return MapStructSide.Back;
                        continue;
                }
            }
        }
    }
}
