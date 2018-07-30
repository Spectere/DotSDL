namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a point in 2D space.
    /// </summary>
    public struct Point {
        /// <summary>
        /// The X coordinate of the point.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate of the point.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Creates a new <see cref="Point"/>.
        /// </summary>
        /// <param name="x">The X value of the new <see cref="Point"/>.</param>
        /// <param name="y">The Y value of the new <see cref="Point"/>.</param>
        public Point(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
