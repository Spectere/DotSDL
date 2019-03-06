using System.Numerics;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a graphical, two-dimensional object in a program.
    /// </summary>
    public class Sprite : Canvas {
        /// <summary>
        /// The position on the screen where the <see cref="Sprite"/> should be drawn.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// The angle that the sprite is drawn, in degrees. Incrementing this will rotate the
        /// sprite clockwise.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// The scale of the <see cref="Sprite"/>. 1.0f is 100%.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Determines the method that will be used to scale this sprite when it is plotted to the
        /// screen.
        /// </summary>
        public ScalingQuality ScalingQuality { get; set; }

        /// <summary>
        /// <c>true</c> if the sprite should be drawn to the screen, otherwise <c>false</c>.
        /// </summary>
        public bool Shown { get; set; }

        /// <summary>
        /// The order in which the sprite is drawn. Lower numbered <see cref="Sprite"/> instances are drawn first
        /// and will appear on the bottom.
        /// </summary>
        public int ZOrder { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height) : this(width, height, Point.Zero, Vector2.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, Point position) : this(width, height, position, Vector2.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="zorder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, int zorder) : this(width, height, Point.Zero, Vector2.One, zorder) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, Point position, Vector2 scale) : this(width, height, position, scale, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="zorder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, int zorder) : this(width, height, position, Vector2.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        /// <param name="zorder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, Vector2 scale, int zorder) : base(width, height) {
            Position = position;
            Scale    = scale;
            ZOrder   = zorder;
            Shown    = false;
        }
    }
}
