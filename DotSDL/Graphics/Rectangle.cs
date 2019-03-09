using DotSDL.Interop.Core;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a rectangle in 2D space.
    /// </summary>
    public class Rectangle {
        private readonly Point _position;
        private readonly Point _size;

        /// <summary>
        /// The base <see cref="SdlRect"/> structure that this class wraps around.
        /// </summary>
        internal Rect.SdlRect SdlRect => new Rect.SdlRect {
                X = _position.X, Y = _position.Y, W = _size.X, H = _size.Y
        };

        /// <summary>
        /// A <see cref="Point"/> representing the position of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Position {
            get => _position;
            set {
                _position.X = value.X;
                _position.Y = value.Y;
            }
        }

        /// <summary>
        /// A <see cref="Point"/> representing the size of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Size {
            get => _size;
            set {
                _size.X = value.X;
                _size.Y = value.Y;
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
            _position = new Point {
                X = x,
                Y = y,
            };

            _size = new Point {
                X = width,
                Y = height
            };
        }
    }
}
