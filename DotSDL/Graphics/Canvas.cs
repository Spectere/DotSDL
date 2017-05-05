namespace DotSDL.Graphics {
    /// <summary>
    /// A representation of the contents of the SDL window, with a number of
    /// helper routines.
    /// </summary>
    public class Canvas {
        /// <summary>
        /// The raw pixels in the <see cref="Canvas"/>.
        /// </summary>
        public Color[] Pixels;

        /// <summary>
        /// Gets the width of the <see cref="Canvas"/>.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the <see cref="Canvas"/>.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Initialization a new <see cref="Canvas"/>.
        /// </summary>
        /// <param name="width">The width of the <see cref="Canvas"/>.</param>
        /// <param name="height">The height of the <see cref="Canvas"/>.</param>
        internal Canvas(int width, int height) {
            SetSize(width, height);
        }

        /// <summary>
        /// Resizes the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="width">The new width of the <see cref="Canvas"/>.</param>
        /// <param name="height">The new height of the <see cref="Canvas"/>.</param>
        internal void SetSize(int width, int height) {
            Width = width;
            Height = height;

            Pixels = new Color[Width * Height];
        }
    }
}
