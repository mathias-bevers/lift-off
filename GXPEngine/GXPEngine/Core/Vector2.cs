using System;

namespace GXPEngine
{
    [Serializable]
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 Up = new Vector2(0, 1);
        public static readonly Vector2 Down = new Vector2(0, -1);
        public static readonly Vector2 Sprite = new Vector2(64, 64);



        override public string ToString()
        {
            return "[Vector2 " + x + ", " + y + "]";
        }

        public float magnitude
        {
            get
            {
                return Mathf.Sqrt(x * x + y * y);
            }
        }
        public Vector2 normalized
        {
            get
            {
                return new Vector2(
                    x != 0 ? x / magnitude : 0,
                    y != 0 ? y / magnitude : 0);
            }
        }
        public void Normalize() => this = normalized;
        public Vector2 normal { get => new Vector2(-y, x).normalized; }

        public void Reflect(Vector2 pNormal, float pBounciness = 1)
        {
            Vector2 veloIn = new Vector2(x, y);
            Vector2 veloOut = veloIn - (1 + pBounciness) * (Mathf.Dot(veloIn, pNormal)) * pNormal;
            x = veloOut.x;
            y = veloOut.y;
        }


        public Vector2 SetLength(float length) //set length of vector
        {
            return this = normalized * length;
        }

        /// <summary>
        /// Rotates vector towards angle
        /// </summary>
        /// <param name="rotation">Angle of rotation in radians</param>
        /// <returns></returns>
        public void RotateTowards(float rotation)
        {
            float cosAngle = Mathf.Cos(rotation);
            float sinAngle = Mathf.Sin(rotation);

            this = new Vector2(x * cosAngle - y * sinAngle, x * sinAngle + y * cosAngle);
        }

        /// <summary>
        /// MoveTowards from unity (edited for 2d however)
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="target">Desired Position</param>
        /// <param name="maxDistanceDelta">Max distance it can move</param>
        /// <returns>New Position</returns>
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            // avoid vector ops because current scripting backends are terrible at inlining
            float toVector_x = target.x - current.x;
            float toVector_y = target.y - current.y;

            float sqdist = toVector_x * toVector_x + toVector_y * toVector_y;

            if (sqdist == 0 || (maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta))
                return target;
            var dist = (float)Math.Sqrt(sqdist);

            return new Vector2(current.x + toVector_x / dist * maxDistanceDelta,
                current.y + toVector_y / dist * maxDistanceDelta);
        }

        static Random random = new Random();
        public static Vector2 Random(int range)
        {
            return new Vector2(random.Next(-range, range), random.Next(-range, range));
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return Mathf.Sqrt(Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2));
        }
        public static Vector2 DirectionBetween(Vector2 a, Vector2 b)
        {
            return b - a;
        }
        public static float AngleBetween(Vector2 a, Vector2 b)
        {
            Vector2 dirBetween = DirectionBetween(a, b);
            return Mathf.Atan2(dirBetween.x, dirBetween.y);
        }

        public static Vector2 GetUnitVectorDeg(float degrees) => GetUnitVectorRad(Mathf.ToRadians(degrees));
        public static Vector2 GetUnitVectorRad(float radians) => new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        public float angleDegrees { get => Mathf.ToDegrees(angleRadians); set => angleRadians = Mathf.ToRadians(value); }
        public float angleRadians { get => Mathf.Atan2(y, x); set { float mag = magnitude; this = new Vector2(Mathf.Cos(value) * mag, Mathf.Sin(value) * mag); } }

        public void RotateDegrees(float degrees) => angleDegrees += degrees;
        public void RotateRadians(float radians) => RotateDegrees(Mathf.ToDegrees(radians));

        public void RotateAroundPointRadians(float radians, Vector2 point) => RotateAroundPointDegrees(Mathf.ToDegrees(radians), point);
        public void RotateAroundPointDegrees(float degrees, Vector2 point)
        {
            this -= point;
            RotateDegrees(degrees);
            this += point;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 vector &&
                   x == vector.x &&
                   y == vector.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.x * b, a.y * b);
        public static Vector2 operator *(float b, Vector2 a) => new Vector2(a.x * b, a.y * b);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator -(Vector2 a) => new Vector2(-a.x, -a.y);
        public static Vector2 operator /(Vector2 a, float b) => new Vector2(a.x / b, a.y / b);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);
        public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2 a, Vector2 b) => a.x != b.x || a.y != b.y;

    }

}

