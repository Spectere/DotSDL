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
        /// An internal function that gets the point on a specific section of a Beziér curve.
        /// </summary>
        /// <param name="points">A list of control points.</param>
        /// <param name="t">The section of the curve to return a point for.</param>
        /// <returns>The requested point on the curve.</returns>
        private Point BezierGetPoint(Point[] points, double t) {
            while(true) {
                if(points.Length == 1)
                    return points[0];

                var newPoints = new Point[points.Length - 1];
                for(var i = 0; i < points.Length - 1; i++) {
                    newPoints[i] = new Point {
                        X = (int)Math.Round((1 - t) * points[i].X + t * points[i + 1].X),
                        Y = (int)Math.Round((1 - t) * points[i].Y + t * points[i + 1].Y)
                    };
                }
                points = newPoints;
            }
        }

        /// <summary>
        /// Draws a Beziér curve onto the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="color">The color of the Beziér curve.(r</param>
        /// <param name="segments">The number of segments in the drawn curve.</param>
        /// <param name="points">A collection of points for the Beziér curve. If fewer than two points are specified, an exception will be thrown.</param>
        public void DrawBezier(Color color, int segments, params Point[] points) {
            if(points.Length < 2) throw new ArgumentException("Too few points specified.", nameof(points));
            if(points.Length == 2) // Just draw a line.
                DrawLine(color, new Line { Start = points[0], End = points[1] });

            var d = 1.0 / segments;
            var newPoints = new Point[segments + 1];
            var i = 0;
            for(var t = 0.0; i < segments; t += d, i++)
                newPoints[i] = BezierGetPoint(points, t);

            // Always make sure that the end point is represented.
            newPoints[segments] = BezierGetPoint(points, 1);

            DrawLines(color, newPoints);
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
                PlotMirroredPointsQuad(color, center, x, y);
                PlotMirroredPointsQuad(color, center, y, x);

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

            PlotMirroredPointsQuad(color, center, x, y);

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
                PlotMirroredPointsQuad(color, center, x, y);
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
                PlotMirroredPointsQuad(color, center, x, y);
            }
        }

        /// <summary>
        /// Used to reduce the number of calculations needed to plot a circle or ellipse.
        /// </summary>
        /// <param name="color">The color of the shape.</param>
        /// <param name="center">A <see cref="Point"/> representing the center of the shape.</param>
        /// <param name="rX">The relative X coordinate of the pixel to color.</param>
        /// <param name="rY">The relative Y coordinate of the pixel to color.</param>
        private void PlotMirroredPointsQuad(Color color, Point center, int rX, int rY) {
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
        /// Plots a sequence of lines on the <see cref="Canvas"/>.
        /// </summary>
        /// <param name="color">The color of the lines.</param>
        /// <param name="points">A list of points to draw. There must be at least two points specified.</param>
        public void DrawLines(Color color, params Point[] points) {
            if(points.Length < 2) throw new ArgumentException("Too few points specified.", nameof(points));
            for(var i = 0; i < points.Length - 1; i++)
                DrawLine(color, new Line { Start = points[i], End = points[i + 1] });
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
