using System;
using System.Xml;

namespace DotSDL.Math {
    /// <summary>
    /// Represents a two dimensional vector using single-precision floats.
    /// </summary>
    public class Vector2 {
        public float X { get; set; }
        public float Y { get; set; }

        /// <summary>
        /// Creates a new <see cref="Vector2"/>.
        /// </summary>
        public Vector2() : this(0, 0) { }

        /// <summary>
        /// Creates a new <see cref="Vector2"/>.
        /// </summary>
        /// <param name="x">The X value of the new <see cref="Vector2"/>.</param>
        /// <param name="y">The Y value of the new <see cref="Vector2"/>.</param>
        public Vector2(float x, float y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vec">An existing <see cref="Vector2"/> to copy into the new object.</param>
        public Vector2(Vector2 vec) : this(vec.X, vec.Y) { }

        /// <summary>
        /// Adds two <see cref="Vector2"/> objects together.
        /// </summary>
        /// <param name="vec1">The left <see cref="Vector2"/> operand.</param>
        /// <param name="vec2">The right <see cref="Vector2"/> operand.</param>
        /// <returns>The result of <paramref name="vec1"/> + <paramref name="vec2"/>.</returns>
        public static Vector2 operator +(Vector2 vec1, Vector2 vec2) {
            return new Vector2(vec1.X + vec2.X,
                               vec1.Y + vec2.Y);
        }
        
        /// <summary>
        /// Subtracts two <see cref="Vector2"/> objects.
        /// </summary>
        /// <param name="vec1">The left <see cref="Vector2"/> operand.</param>
        /// <param name="vec2">The right <see cref="Vector2"/> operand.</param>
        /// <returns>The result of <paramref name="vec1"/> - <paramref name="vec2"/>.</returns>
        public static Vector2 operator -(Vector2 vec1, Vector2 vec2) {
            return new Vector2(vec1.X - vec2.X,
                               vec1.Y - vec2.Y);
        }

        /// <summary>
        /// Multiplies a <see cref="Vector2"/> by a scalar value.
        /// </summary>
        /// <param name="vec">The left <see cref="Vector2"/> operand.</param>
        /// <param name="scalar">The right <see cref="float"/> operand.</param>
        /// <returns>The result of <paramref name="vec"/> * <paramref name="scalar"/>.</returns>
        public static Vector2 operator *(Vector2 vec, float scalar) {
            return new Vector2(vec.X * scalar,
                               vec.Y * scalar);
        }

        /// <summary>
        /// Divides a <see cref="Vector2"/> by a scalar value.
        /// </summary>
        /// <param name="vec">The left <see cref="Vector2"/> operand.</param>
        /// <param name="scalar">The right <see cref="float"/> operand.</param>
        /// <returns>The result of <paramref name="vec"/> / <paramref name="scalar"/>.</returns>
        /// <exception cref="DivideByZeroException">The scalar value is zero.</exception>
        public static Vector2 operator /(Vector2 vec, float scalar) {
            return new Vector2(vec.X / scalar,
                               vec.Y / scalar);
        }
    }
}
