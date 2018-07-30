using System;

namespace DotSDL.Graphics {
    /// <summary>
    /// A representation of the contents of the SDL window, with a number of
    /// helper routines.
    /// </summary>
    public class Canvas {
        private int _width, _height;

        /// <summary>
        /// The raw pixels in the <see cref="Canvas"/>.
        /// </summary>
        public Color[] Pixels;

        /// <summary>
        /// Gets or sets the width of the <see cref="Canvas"/> texture.
        /// </summary>
        public int Width {
            get => _width;
            set {
                if(value <= 0) throw new ArgumentException("Width must be greater than 0.");

                _width = value;
                Resize();
            }
        }

        /// <summary>
        /// Gets or sets the height of the <see cref="Canvas"/> texture.
        /// </summary>
        public int Height {
            get => _height;
            set {
                if(value <= 0) throw new ArgumentException("Height must be greater than 0.");

                _height = value;
                Resize();
            }
        }

        /// <summary>
        /// Sets the section of the <see cref="Canvas"/> that should be drawn. If the size values are set to 0, the
        /// <see cref="Canvas"/> will fill as much of its containing object as possible.
        /// </summary>
        public Rectangle Clipping { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Canvas"/>.
        /// </summary>
        /// <param name="textureWidth">The width of the <see cref="Canvas"/>.</param>
        /// <param name="textureHeight">The height of the <see cref="Canvas"/>.</param>
        internal Canvas(int textureWidth, int textureHeight) : this(textureWidth, textureHeight, new Rectangle(0, 0, textureWidth, textureHeight)) { }

        /// <summary>
        /// Initializes a new <see cref="Canvas"/>.
        /// </summary>
        /// <param name="textureWidth">The width of the <see cref="Canvas"/>.</param>
        /// <param name="textureHeight">The height of the <see cref="Canvas"/>.</param>
        /// <param name="clipping">The clipping <see cref="Rectangle"/> for the <see cref="Canvas"/>.</param>
        internal Canvas(int textureWidth, int textureHeight, Rectangle clipping) {
            _width = textureWidth;
            _height = textureHeight;

            Clipping = clipping;

            Resize();
        }

        /// <summary>
        /// Retrieves an array index on the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="x">The Y coordinate of the desired location on the <see cref="Canvas"/>.</param>
        /// <param name="y">The Y coordinate of the desired location on the <see cref="Canvas"/>.</param>
        /// <returns>The array index for the given point.</returns>
        public int GetIndex(int x, int y) {
            return (Width * y) + x;
        }

        /// <summary>
        /// Retrieves an array index on the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="point">A <see cref="Point"/> representing the desired location on the <see cref="Canvas"/>.</param>
        /// <returns>The array index for the given point.</returns>
        public int GetIndex(Point point) {
            return (Width * point.Y) + point.X;
        }

        /// <summary>
        /// Resizes the <see cref="Canvas"/>. Please note that this will also clear the canvas of
        /// its existing contents.
        /// </summary>
        protected void Resize() {
            Pixels = new Color[Width * Height];
        }
    }
}
