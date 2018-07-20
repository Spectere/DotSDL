namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a graphical, two-dimensional object in a program.
    /// </summary>
    public class Sprite : Canvas {
        public Point Position { get; }
        public Point Scale { get; }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height) : this(width, height, 0, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="x">The initial X position of the new <see cref="Sprite"/>.</param>
        /// <param name="y">The initial Y position of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, int x, int y) : this(width, height, new Point { X = x, Y = y }) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Point"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, Point position) : base(width, height) {
            Position = position;
            SetSize(width, height);

            Scale = new Point { X = 1, Y = 1 };
        }
    }
}
