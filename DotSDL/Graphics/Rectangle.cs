namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a rectangle in 2D space.
    /// </summary>
    public struct Rectangle {
        /// <summary>
        /// A <see cref="Point"/> representing the position of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Position { get; set; }
        
        /// <summary>
        /// A <see cref="Point"/> representing the size of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Size { get; set; }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="position">A <see cref="Point"/> representing the position of the new <see cref="Rectangle"/>.</param>
        /// <param name="size">A <see cref="Point"/> representing the size of the new <see cref="Rectangle"/>.</param>
        public Rectangle(Point position, Point size) {
            Position = position;
            Size = size;
        }
        
        /// <summary>
        /// Creates a new <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="x">The X position of the new <see cref="Rectangle"/>.</param>
        /// <param name="y">The Y position of the new <see cref="Rectangle"/>.</param>
        /// <param name="width">The width of the new <see cref="Rectangle"/>.</param>
        /// <param name="height">The height of the new <see cref="Rectangle"/>.</param>
        public Rectangle(int x, int y, int width, int height) : this(new Point(x, y), new Point(width, height)) { }
    }
}
