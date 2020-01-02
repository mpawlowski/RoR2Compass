using System;
using System.Collections.Generic;
using System.Text;

namespace CompassPlugin
{
    enum Direction
    {
        NORTH,
        NORTH_EAST,
        EAST,
        SOUTH_EAST,
        SOUTH,
        SOUTH_WEST,
        WEST,
        NORTH_WEST,
        UNKNOWN
    }

    static class DirectionMethods
    {
        public static String LongString(this Direction d)
        {
            switch (d)
            {
                case Direction.NORTH:
                    return "North";
                case Direction.NORTH_EAST:
                    return "North East";
                case Direction.EAST:
                    return "East";
                case Direction.SOUTH_EAST:
                    return "South East";
                case Direction.SOUTH:
                    return "South";
                case Direction.SOUTH_WEST:
                    return "South West";
                case Direction.WEST:
                    return "West";
                case Direction.NORTH_WEST:
                    return "North West";
                default:
                    return "Unknown";
            }
        }
        public static String ShortString(this Direction d)
        {
            switch (d)
            {
                case Direction.NORTH:
                    return "N";
                case Direction.NORTH_EAST:
                    return "NE";
                case Direction.EAST:
                    return "E";
                case Direction.SOUTH_EAST:
                    return "SE";
                case Direction.SOUTH:
                    return "S";
                case Direction.SOUTH_WEST:
                    return "SW";
                case Direction.WEST:
                    return "W";
                case Direction.NORTH_WEST:
                    return "NW";
                default:
                    return "-";
            }
        }
    }
}

