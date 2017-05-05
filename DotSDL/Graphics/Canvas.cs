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
        /// Draws a circle onto the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="color">The color of the circle.</param>
        /// <param name="center">A <see cref="Point"/> indicating the center of the circle.</param>
        /// <param name="radius">The radius of the drawn circle, in pixels.</param>
        public void DrawCircle(Color color, Point center, int radius) {
            var x = radius;
            var y = 0;
            var err = 0;

            while(x >= y) {
                PlotMirroredPoints(color, center, x, y);
                PlotMirroredPoints(color, center, y, x);

                y += 1;
                if(err <= 0) {
                    err += 2 * y + 1;
                }

                if(err > 0) {
                    x -= 1;
                    err -= 2 * x + 1;
                }
            }
        }

        /// <summary>
        /// Draws an ellipse onto the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="color">The color of the ellipse.</param>
        /// <param name="center">A <see cref="Point"/> indicating the center of the ellipse.</param>
        /// <param name="width">The radius when measured horizontally, in pixels.</param>
        /// <param name="height">The radius when measured vertically, in pixels.</param>
        public void DrawEllipse(Color color, Point center, int width, int height) {
            var rxSq = width * width;
            var rySq = height * height;
            var x = 0;
            var y = height;
            int p;
            var px = 0;
            var py = 2 * rxSq * y;

            PlotMirroredPoints(color, center, x, y);

            // Region 1
            p = (int)(rySq - (rxSq * height) + (0.25 * rxSq));
            while(px < py) {
                x++;
                px = px + 2 * rySq;
                if(p < 0) {
                    p = p + rySq + px;
                } else {
                    y--;
                    py = py - 2 * rxSq;
                    p = p + rySq + px - py;
                }
                PlotMirroredPoints(color, center, x, y);
            }
            
            // Region 2
            p = (int)(rySq * (x + 0.5) * (x + 0.5) + rxSq * (y - 1) * (y - 1) - rxSq * rySq);
            while(y > 0) {
                y--;
                py = py - 2 * rxSq;
                if(p > 0) {
                    p = p + rxSq - py;
                } else {
                    x++;
                    px = px + 2 * rySq;
                    p = p + rxSq - py + px;
                }
                PlotMirroredPoints(color, center, x, y);
            }
        }

        private void PlotMirroredPoints(Color color, Point center, int rX, int rY) {
            Pixels[GetIndex(center.X + rX, center.Y + rY)] = color;
            Pixels[GetIndex(center.X + rX, center.Y - rY)] = color;
            Pixels[GetIndex(center.X - rX, center.Y + rY)] = color;
            Pixels[GetIndex(center.X - rX, center.Y - rY)] = color;
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
