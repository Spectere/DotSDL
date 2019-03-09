using DotSDL.Interop.Core;
using DotSDL.Math.Vector;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a point in 2D space.
    /// </summary>
    public class Point : Vector2<int> {
        /// <summary>
        /// The base <see cref="SdlPoint"/> structure that this class wraps around.
        /// </summary>
        internal Rect.SdlPoint SdlPoint => new Rect.SdlPoint { X = X, Y = Y };

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (1, 1).
        /// </summary>
        public new static Point One => new Point(1, 1);

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (1, 0).
        /// </summary>
        public new static Point UnitX => new Point(1, 0);

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (0, 1).
        /// </summary>
        public new static Point UnitY => new Point(0, 1);

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (0, 0).
        /// </summary>
        public new static Point Zero => new Point(0, 0);

        /// <summary>
        /// Creates a new <see cref="Point"/> with the coordinates (0, 0).
        /// </summary>
        public Point() : this(Zero) { }

        /// <summary>
        /// Creates a new <see cref="Point"/> based on an existing <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to base this new object on.</param>
        public Point(Point point) {
            X = point.X;
            Y = point.Y;
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> based on an existing <see cref="Vector2{T}"/>.
        /// </summary>
        /// <param name="vec">The <see cref="Vector2{T}"/> to based this new object on.</param>
        public Point(Vector2<int> vec) {
            X = vec.X;
            Y = vec.Y;
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> based on integer coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the new <see cref="Point"/>.</param>
        /// <param name="y">The Y coordinate of the new <see cref="Point"/>.</param>
        public Point(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
