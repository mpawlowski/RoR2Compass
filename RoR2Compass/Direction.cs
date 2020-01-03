using System;
using UnityEngine;

namespace CompassPlugin
{
    enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
        Unknown
    }

    enum DirectionMode
    {
        Long,
        Short,
        Numeric
    }

    static class DirectionCalculator
    {

        private static float radiansToDegrees(float radians)
        {
            return radians * (180 / (float)Math.PI);
        }

        public static float ComputeDegrees(Vector2 v)
        {
            var x = v.x;
            var y = v.y;

            if (x == 0)
            {
                return (y > 0) ? 90
                    : (y == 0) ? 0
                    : 270;
            }
            else if (y == 0)
            {
                return (x >= 0) ? 0
                    : 180;
            }
            float deg = radiansToDegrees(Mathf.Atan(y / x));
            if (x < 0 && y < 0)
            {
                deg = 180 + deg;
            }
            else if (x < 0)
            {
                deg = 180 + deg;
            }
            else if (y < 0)
            {
                deg = 360 + deg;
            }
            return deg;
        }

        public static Direction FromDegrees(float angle)
        {
            Direction d = Direction.Unknown;
            if (angle <= 45)
            {
                d = Direction.North;
            }

            if (angle > 45 && angle <= 90)
            {
                d = Direction.NorthWest;
            }

            if (angle > 90 && angle <= 135)
            {
                d = Direction.West;
            }

            if (angle > 135 && angle <= 180)
            {
                d = Direction.SouthWest;
            }

            if (angle > 180 && angle <= 225)
            {
                d = Direction.South;
            }

            if (angle > 225 && angle <= 270)
            {
                d = Direction.SouthEast;
            }

            if (angle > 270 && angle <= 315)
            {
                d = Direction.East;
            }

            if (angle > 315)
            {
                d = Direction.NorthEast;
            }

            return d;
        }
    }

    static class DirectionMethods
    {
        public static String LongString(this Direction d)
        {
            switch (d)
            {
                case Direction.North:
                    return "North";
                case Direction.NorthEast:
                    return "North East";
                case Direction.East:
                    return "East";
                case Direction.SouthEast:
                    return "South East";
                case Direction.South:
                    return "South";
                case Direction.SouthWest:
                    return "South West";
                case Direction.West:
                    return "West";
                case Direction.NorthWest:
                    return "North West";
                default:
                    return "Unknown";
            }
        }
        public static String ShortString(this Direction d)
        {
            switch (d)
            {
                case Direction.North:
                    return "N";
                case Direction.NorthEast:
                    return "NE";
                case Direction.East:
                    return "E";
                case Direction.SouthEast:
                    return "SE";
                case Direction.South:
                    return "S";
                case Direction.SouthWest:
                    return "SW";
                case Direction.West:
                    return "W";
                case Direction.NorthWest:
                    return "NW";
                default:
                    return "-";
            }
        }
    }
}

