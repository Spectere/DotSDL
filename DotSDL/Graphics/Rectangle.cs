using DotSDL.Interop.Core;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a rectangle in 2D space.
    /// </summary>
    public class Rectangle {
        private Rect.SdlRect _rect;

        /// <summary>
        /// A <see cref="Point"/> representing the position of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Position {
            get => new Point(_rect.X, _rect.Y);
            set {
                _rect.X = value.X;
                _rect.Y = value.Y;
            }
        }

        /// <summary>
        /// A <see cref="Point"/> representing the size of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Size {
            get => new Point(_rect.W, _rect.H);
            set {
                _rect.W = value.X;
                _rect.H = value.Y;
            }
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="position">A <see cref="Point"/> representing the position of the new <see cref="Rectangle"/>.</param>
        /// <param name="size">A <see cref="Point"/> representing the size of the new <see cref="Rectangle"/>.</param>
        public Rectangle(Point position, Point size) : this(position.X, position.Y, size.X, size.Y) { }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="x">The X position of the new <see cref="Rectangle"/>.</param>
        /// <param name="y">The Y position of the new <see cref="Rectangle"/>.</param>
        /// <param name="width">The width of the new <see cref="Rectangle"/>.</param>
        /// <param name="height">The height of the new <see cref="Rectangle"/>.</param>
        public Rectangle(int x, int y, int width, int height) {
            _rect = new Rect.SdlRect {
                X = x,
                Y = y,
                W = width,
                H = height
            };
        }
    }
}
