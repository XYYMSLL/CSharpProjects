using System;

namespace Vec3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    struct Vector3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Vector3 front { get => new Vector3(0, 0, 1); }
        public Vector3 back { get => new Vector3(0, 0, -1); }
        public Vector3 right { get => new Vector3(1, 0, 0); }
        public Vector3 left { get => new Vector3(-1, 0, 0); }
        public Vector3 up { get => new Vector3(0, 1, 0); }
        public Vector3 down { get => new Vector3(0, -1, 0); }
        public Vector3 zeroV3 { get => new Vector3(0, 0, 0); }

        public Vector3(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y + z * z);
            }
        }

        public Vector3 Normalized
        {
            get
            {
                if (Magnitude == 0)
                {
                    throw new Exception("zero vector or point vector cannot be normalized");
                }

                return new Vector3(x / Magnitude, y / Magnitude, z / Magnitude);
            }
        }

        public static Vector3 Normalize(Vector3 v)
        {
            return v.Normalized;
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.x, -v.y, -v.z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return lhs + -rhs;
        }

        public static Vector3 operator *(Vector3 lhs, float num)
        {
            return new Vector3(lhs.x * num, lhs.y * num, lhs.z * num);
        }

        public static Vector3 operator *(float num, Vector3 lhs)
        {
            return new Vector3(lhs.x * num, lhs.y * num, lhs.z * num);
        }

        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public float Dot(Vector3 v)
        {
            return x * v.x + y * v.y + z * v.z;
        }

        public static float Angle(Vector3 v1, Vector3 v2)
        {
            return (float)(Math.Acos(Dot(v1.Normalized, v2.Normalized)) * 180 / Math.PI);
        }

        public static float UAngle(Vector3 v1, Vector3 v2)
        {
            return (float)Math.Abs(Angle(v1, v2));
        }

        public float Angle(Vector3 v)
        {
            return (float)(Math.Acos(Dot(new Vector3(x,y,z).Normalized, v.Normalized)) * 180 / Math.PI);
        }

        public float UAngle(Vector3 v)
        {
            return (float)Math.Abs(Angle(v));
        }

        /// <summary>
        /// returns a new Vector3 represents the result of the caller X callee
        /// </summary>
        /// <param name="v"></param>
        /// <returns>the cross production result of caller X v</returns>
        public Vector3 Cross(Vector3 v)
        {
            Vector3 result = new Vector3(1, 1, 1);

            result.x = y * v.z - z * v.y;
            result.y = z * v.x - x * v.z;
            result.z = x * v.y - y * v.x;

            return result;
        }

        /// <summary>
        /// returns a new Vector3 contains the result vector of v1 X v2
        /// </summary>
        /// <param name="v1">the lhs vector</param>
        /// <param name="v2">the rhs vector</param>
        /// <returns>v1 X v2</returns>
        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            Vector3 result = new Vector3(1, 1, 1);

            result.x *= v1.y * v2.z - v1.z * v2.y;
            result.y *= v1.z * v2.x - v1.x * v2.z;
            result.z *= v1.x * v2.y - v1.y * v2.x;

            return result;
        }

        /// <summary>
        /// returns the projection vector of v1 on v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 Projection(Vector3 v1, Vector3 v2)
        {
            return v2 * (v1.Dot(v2) / v2.Dot(v2));
        }

        /// <summary>
        /// get projection of caller vector on callee vector direction
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector3 Projection(Vector3 v)
        {
            return Projection(new Vector3(x, y, z), v);
        }

        /// <summary>
        /// return a vector represents the projection vector of a given vector on a given plane
        /// </summary>
        /// <param name="vec">the original vector</param>
        /// <param name="u">the plane's unit vector on u direction</param>
        /// <param name="v">the plane's unit vector on v direction</param>
        /// <returns></returns>
        public static Vector3 ProjectionOnPlane(Vector3 vec, Vector3 u, Vector3 v)
        {
            if (u.Dot(v) != 0)
            {
                throw new Exception("u and v are not perpendicular");
            }
            return Projection(vec, u.Normalized) + Projection(vec, v.Normalized);
        }

        /// <summary>
        /// returns the reflection vector
        /// </summary>
        /// <param name="v">incoming vector</param>
        /// <param name="n">normal vector</param>
        /// <returns></returns>
        public static Vector3 Reflection(Vector3 v, Vector3 n)
        {
            return v - 2 * v.Dot(n) * n;
        }

        public Vector3 Reflection(Vector3 n)
        {
            Vector3 v = new Vector3(x, y, z);
            return Reflection(v, n);
        }
    }
}