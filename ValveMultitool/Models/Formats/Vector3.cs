using System.Collections.Generic;

namespace ValveMultitool.Models.Formats
{
    public struct Vector3<T>
    {
        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(IReadOnlyList<T> coordinates)
        {
            X = coordinates[0];
            Y = coordinates[1];
            Z = coordinates[2];
        }

        public readonly T X;
        public readonly T Y;
        public readonly T Z;
    }
}
