using DotSDL.Exceptions;
using System;
using System.Collections.Generic;

namespace DotSDL.Math.Vector {
    /// <summary>
    /// Represents a generic vector type.
    /// </summary>
    public class Vector2<T> : IEquatable<Vector2<T>> {
        public T X { get; set; }
        public T Y { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Vector2{T}"/> object.
        /// </summary>
        /// <exception cref="InvalidTypeException">Thrown if this <see cref="Vector2{T}"/> is created with an unsupported type.</exception>
        public Vector2() {
            if(!VectorBase<T>.IsNumericType())
                throw new InvalidTypeException(GetType().ToString(), typeof(T));
        }

        /// <summary>
        /// Initializes a new <see cref="Vector2{T}"/> object using the values from another <see cref="Vector2{T}"/>.
        /// </summary>
        /// <param name="initialValue">The <see cref="Vector2{T}"/> that should be used to initialize the new object.</param>
        /// <exception cref="InvalidTypeException">Thrown if this <see cref="Vector2{T}"/> is created with an unsupported type.</exception>
        public Vector2(Vector2<T> initialValue) : this() {
            X = initialValue.X;
            Y = initialValue.Y;
        }

        /// <summary>
        /// Initializes a new <see cref="Vector2{T}"/> object with the X and Y values both set to a given value.
        /// </summary>
        /// <param name="initialValue">The value to initialize the X and Y values to.</param>
        /// <exception cref="InvalidTypeException">Thrown if this <see cref="Vector2{T}"/> is created with an unsupported type.</exception>
        public Vector2(T initialValue) : this() {
            X = Y = initialValue;
        }

        /// <summary>
        /// Initializes a new <see cref="Vector2{T}"/> object with preset X and Y values.
        /// </summary>
        /// <param name="initialX">The initial value to set the X component to.</param>
        /// <param name="initialY">The initial value to set the Y component to.</param>
        /// <exception cref="InvalidTypeException">Thrown if this <see cref="Vector2{T}"/> is created with an unsupported type.</exception>
        public Vector2(T initialX, T initialY) : this() {
            X = initialX;
            Y = initialY;
        }

        /// <summary>
        /// Checks to see if two <see cref="Vector2{T}"/> objects are equal.
        /// </summary>
        /// <param name="left">The first <see cref="Vector2{T}"/> in the comparison.</param>
        /// <param name="right">The second <see cref="Vector2{T}"/> in the comparison.</param>
        /// <returns><c>true</c> if the two <see cref="Vector2{T}"/> objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(Vector2<T> left, Vector2<T> right) {
            if(left is null && right is null) return true;
            if(left is null || right is null) return false;
            if(left.GetType() != right.GetType()) return false;

            return left.X.Equals(right.X) && left.Y.Equals(right.Y);
        }

        /// <summary>
        /// Checks to see if two <see cref="Vector2{T}"/> objects are different.
        /// </summary>
        /// <param name="left">The first <see cref="Vector2{T}"/> in the comparison.</param>
        /// <param name="right">The second <see cref="Vector2{T}"/> in the comparison.</param>
        /// <returns><c>true</c> if the two <see cref="Vector2{T}"/> objects are different, otherwise <c>false</c>.</returns>
        public static bool operator !=(Vector2<T> left, Vector2<T> right) {
            return !(left == right);
        }

        /// <summary>Returns a new <see cref="Vector2{T}"/> with both X and Y set to 1.</summary>
        public static Vector2<T> One => new Vector2<T>(VectorBase<T>.GetOne());

        /// <summary>Returns a new <see cref="Vector2{T}"/> containing (1, 0).</summary>
        public static Vector2<T> UnitX => new Vector2<T>(VectorBase<T>.GetOne(), VectorBase<T>.GetZero());

        /// <summary>Returns a new <see cref="Vector2{T}"/> containing (0, 1).</summary>
        public static Vector2<T> UnitY => new Vector2<T>(VectorBase<T>.GetZero(), VectorBase<T>.GetOne());

        /// <summary>Returns a new <see cref="Vector2{T}"/> with both X and Y set to 0.</summary>
        public static Vector2<T> Zero => new Vector2<T>(VectorBase<T>.GetZero());

        /// <inheritdoc/>
        public bool Equals(Vector2<T> other) {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(X, other.X) && EqualityComparer<T>.Default.Equals(Y, other.Y);
        }

        /// <summary>
        /// Determines whether this <see cref="Vector2{T}"/> is equal to another one.
        /// </summary>
        /// <param name="other">Another <see cref="Vector2{T}"/> to compare this instance to.</param>
        /// <returns><c>true</c> if the two <see cref="Vector2{T}"/> objects are equal, otherwise <c>false</c>.</returns>
        public override bool Equals(object obj) {
            if(!(obj is Vector2<T>)) return false;
            return this == (Vector2<T>)obj;
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            unchecked {
                return (EqualityComparer<T>.Default.GetHashCode(X) * 397) ^ EqualityComparer<T>.Default.GetHashCode(Y);
            }
        }
    }
}
