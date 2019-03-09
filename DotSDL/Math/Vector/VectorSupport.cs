using DotSDL.Exceptions;
using System.Runtime.CompilerServices;

namespace DotSDL.Math.Vector {
    /// <summary>
    /// A class containing static functions that support the implemented vector types.
    /// </summary>
    /// <typeparam name="T">The type of the vector that should be supported..</typeparam>
    internal static class VectorBase<T> {
        /// <summary>
        /// Retrieves a one value in one of the supported types.
        /// </summary>
        /// <returns>A one value in one of the supported types.</returns>
        /// <exception cref="InvalidTypeException">Thrown if a non-numeric or unsupported type is passed.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetOne() {
            if(typeof(T) == typeof(sbyte)) {
                const sbyte val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(byte)) {
                const byte val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(short)) {
                const short val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(ushort)) {
                const ushort val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(int)) {
                const int val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(uint)) {
                const uint val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(long)) {
                const long val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(ulong)) {
                const ulong val = 1;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(float)) {
                const float val = 1.0f;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(double)) {
                const double val = 1.0d;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(decimal)) {
                const decimal val = 1.0m;
                return (T)(object)val;
            }

            throw new InvalidTypeException("VectorBase<T>.GetOne()", typeof(T));
        }

        /// <summary>
        /// Retrieves a zero value in one of the supported types.
        /// </summary>
        /// <returns>A zero value in one of the supported types.</returns>
        /// <exception cref="InvalidTypeException">Thrown if a non-numeric or unsupported type is passed.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetZero() {
            if(typeof(T) == typeof(sbyte)) {
                const sbyte val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(byte)) {
                const byte val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(short)) {
                const short val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(ushort)) {
                const ushort val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(int)) {
                const int val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(uint)) {
                const uint val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(long)) {
                const long val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(ulong)) {
                const ulong val = 0;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(float)) {
                const float val = 0.0f;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(double)) {
                const double val = 0.0d;
                return (T)(object)val;
            }

            if(typeof(T) == typeof(decimal)) {
                const decimal val = 0.0m;
                return (T)(object)val;
            }

            throw new InvalidTypeException("VectorBase<T>.GetZero()", typeof(T));
        }

        /// <summary>
        /// Determines whether or not the desired type is a valid and supported numeric type.
        /// </summary>
        /// <returns><c>type</c> if the value is a supported numeric type, otherwise <c>false</c>.</returns>
        internal static bool IsNumericType() {
            return typeof(T) == typeof(sbyte)
               || typeof(T) == typeof(byte)
               || typeof(T) == typeof(short)
               || typeof(T) == typeof(ushort)
               || typeof(T) == typeof(int)
               || typeof(T) == typeof(uint)
               || typeof(T) == typeof(long)
               || typeof(T) == typeof(ulong)
               || typeof(T) == typeof(float)
               || typeof(T) == typeof(double)
               || typeof(T) == typeof(decimal);
        }
    }
}
