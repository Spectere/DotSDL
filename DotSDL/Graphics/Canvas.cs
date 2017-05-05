using System;

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
        /// Plots a line on the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="color">The color of the line.</param>
        /// <param name="line">A <see cref="Line"/> object representing the shape that should be drawn.</param>
        public void DrawLine(Color color, Line line) {
            var dx = line.End.X - line.Start.X;
            var dy = line.End.Y - line.Start.Y;

            if(Math.Abs(dx) > Math.Abs(dy)) {
                if(line.End.X > line.Start.X) {
                    for(var x = line.Start.X; x < line.End.X; x++) {
                        var y = line.Start.Y + dy * (x - line.Start.X) / dx;
                        Pixels[GetIndex(x, y)] = color;
                    }
                } else {
                    for(var x = line.Start.X; x > line.End.X; x--) {
                        var y = line.Start.Y + dy * (x - line.Start.X) / dx;
                        Pixels[GetIndex(x, y)] = color;
                    }
                }
            } else {
                if(line.End.Y > line.Start.Y) {
                    for(var y = line.Start.Y; y < line.End.Y; y++) {
                        var x = line.Start.X + dx * (y - line.Start.Y) / dy;
                        Pixels[GetIndex(x, y)] = color;
                    }
                } else {
                    for(var y = line.Start.Y; y > line.End.Y; y--) {
                        var x = line.Start.X + dx * (y - line.Start.Y) / dy;
                        Pixels[GetIndex(x, y)] = color;
                    }
                }
            }
        }

        /// <summary>
        /// Plots a sequence of lines on the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="color">The color of the lines.</param>
        /// <param name="lines">A set of <see cref="Line"/> objects representing the shapes that should be drawn.</param>
        public void DrawLines(Color color, params Line[] lines) {
            foreach(var line in lines)
                DrawLine(color, line);
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
