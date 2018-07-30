using System.Numerics;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a graphical, two-dimensional object in a program.
    /// </summary>
    public class Sprite : Canvas {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public int ZOrder { get; set; }
        public bool Shown { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height) : this(width, height, Vector2.Zero, Vector2.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, Vector2 position) : this(width, height, position, Vector2.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="zorder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, int zorder) : this(width, height, Vector2.Zero, Vector2.One, zorder) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, Vector2 position, Vector2 scale) : this(width, height, position, scale, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="zorder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Vector2 position, int zorder) : this(width, height, position, Vector2.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Vector2"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        /// <param name="zorder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Vector2 position, Vector2 scale, int zorder) : base(width, height) {
            SetSize(width, height);

            Position = position;
            Scale    = scale;
            ZOrder   = zorder;
            Shown    = false;
        }
    }
}
