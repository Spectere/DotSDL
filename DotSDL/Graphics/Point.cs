using DotSDL.Interop.Core;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a point in 2D space.
    /// </summary>
    public class Point {
        private Rect.SdlPoint _point;

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (1, 1).
        /// </summary>
        public static Point One = new Point(1, 1);

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (1, 0).
        /// </summary>
        public static Point UnitX = new Point(1, 0);

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (0, 1).
        /// </summary>
        public static Point UnitY = new Point(0, 1);

        /// <summary>
        /// Returns a <see cref="Point"/> with the coordinates (0, 0).
        /// </summary>
        public static Point Zero = new Point(0, 0);

        /// <summary>
        /// The X coordinate of the point.
        /// </summary>
        public int X {
            get => _point.X;
            set => _point.X = value;
        }

        /// <summary>
        /// The Y coordinate of the point.
        /// </summary>
        public int Y {
            get => _point.Y;
            set => _point.Y = value;
        }

        /// <summary>
        /// Creates a new <see cref="Point"/>.
        /// </summary>
        /// <param name="x">The X value of the new <see cref="Point"/>.</param>
        /// <param name="y">The Y value of the new <see cref="Point"/>.</param>
        public Point(int x = 0, int y = 0) {
            _point = new Rect.SdlPoint {
                X = x,
                Y = y
            };
        }
    }
}
