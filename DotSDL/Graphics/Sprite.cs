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
        public double Rotation { get; set; }

        /// <summary>
        /// The point around which the sprite will be rotated. By default, this will be set to
        /// the center of the sprite.
        /// </summary>
        public Point RotationCenter { get; set; }

        /// <summary>
        /// Indicates which axes this sprice should be flipped across, if any.
        /// </summary>
        public FlipDirection Flip { get; set; }

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
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, int zOrder) : this(width, height, Point.Zero, Vector2.One, zOrder) { }

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
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, int zOrder) : this(width, height, position, Vector2.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, Vector2 scale, int zOrder)
            : this(width, height, position, new Rectangle(0, 0, width, height), scale, zOrder) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        /// <param name="clipping">A rectangle specifying which part of this <see cref="Sprite"/> should be drawn.</param>
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, Rectangle clipping, Vector2 scale, int zOrder) : base(width, height, clipping) {
            Position = position;
            Scale    = scale;
            ZOrder   = zOrder;
            Shown    = false;

            RotationCenter.X = clipping.Size.X / 2;
            RotationCenter.Y = clipping.Size.Y / 2;
        }
    }
}
