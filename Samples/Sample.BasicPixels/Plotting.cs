using DotSDL.Graphics;
using System;

namespace Sample.BasicPixels {
    internal static class Plotting {
        /// <summary>
        /// An internal function that gets the point on a specific section of a Beziér curve.
        /// </summary>
        /// <param name="points">A list of control points.</param>
        /// <param name="t">The section of the curve to return a point for.</param>
        /// <returns>The requested point on the curve.</returns>
        private static Point BezierGetPoint(Point[] points, double t) {
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
        /// Draws a Beziér curve onto a <see cref="Canvas"/>.
        /// </summary>
        /// <param name="canvas">The <see cref="Canvas"/> to plot pixels on.</param>
        /// <param name="color">The color of the Beziér curve.(r</param>
        /// <param name="segments">The number of segments in the drawn curve.</param>
        /// <param name="points">A collection of points for the Beziér curve. If fewer than two points are specified, an exception will be thrown.</param>
        public static void DrawBezier(Canvas canvas, Color color, int segments, params Point[] points) {
            if(points.Length < 2) throw new ArgumentException("Too few points specified.", nameof(points));
            if(points.Length == 2) // Just draw a line.
                DrawLine(canvas, color, new Line { Start = points[0], End = points[1] });

            var d = 1.0 / segments;
            var newPoints = new Point[segments + 1];
            var i = 0;
            for(var t = 0.0; i < segments; t += d, i++)
                newPoints[i] = BezierGetPoint(points, t);

            // Always make sure that the end point is represented.
            newPoints[segments] = BezierGetPoint(points, 1);

            DrawLines(canvas, color, newPoints);
        }

        /// <summary>
        /// Draws a circle onto a <see cref="Canvas"/>.
        /// </summary>
        /// <param name="canvas">The <see cref="Canvas"/> to plot pixels on.</param>
        /// <param name="color">The color of the circle.</param>
        /// <param name="center">A <see cref="Point"/> indicating the center of the circle.</param>
        /// <param name="radius">The radius of the drawn circle, in pixels.</param>
        public static void DrawCircle(Canvas canvas, Color color, Point center, int radius) {
            var x = radius;
            var y = 0;
            var err = 0;

            while(x >= y) {
                PlotMirroredPointsQuad(canvas, color, center, x, y);
                PlotMirroredPointsQuad(canvas, color, center, y, x);

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
        /// Draws an ellipse onto a <see cref="Canvas"/>.
        /// </summary>
        /// <param name="canvas">The <see cref="Canvas"/> to plot pixels on.</param>
        /// <param name="color">The color of the ellipse.</param>
        /// <param name="center">A <see cref="Point"/> indicating the center of the ellipse.</param>
        /// <param name="width">The radius when measured horizontally, in pixels.</param>
        /// <param name="height">The radius when measured vertically, in pixels.</param>
        public static void DrawEllipse(Canvas canvas, Color color, Point center, int width, int height) {
            var rxSq = width * width;
            var rySq = height * height;
            var x = 0;
            var y = height;
            int p;
            var px = 0;
            var py = 2 * rxSq * y;

            PlotMirroredPointsQuad(canvas, color, center, x, y);

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
                PlotMirroredPointsQuad(canvas, color, center, x, y);
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
                PlotMirroredPointsQuad(canvas, color, center, x, y);
            }
        }

        /// <summary>
        /// Used to reduce the number of calculations needed to plot a circle or ellipse.
        /// </summary>
        /// <param name="canvas">The <see cref="Canvas"/> to plot pixels on.</param>
        /// <param name="color">The color of the shape.</param>
        /// <param name="center">A <see cref="Point"/> representing the center of the shape.</param>
        /// <param name="rX">The relative X coordinate of the pixel to color.</param>
        /// <param name="rY">The relative Y coordinate of the pixel to color.</param>
        private static void PlotMirroredPointsQuad(Canvas canvas, Color color, Point center, int rX, int rY) {
            canvas.Pixels[canvas.GetIndex(center.X + rX, center.Y + rY)] = color;
            canvas.Pixels[canvas.GetIndex(center.X + rX, center.Y - rY)] = color;
            canvas.Pixels[canvas.GetIndex(center.X - rX, center.Y + rY)] = color;
            canvas.Pixels[canvas.GetIndex(center.X - rX, center.Y - rY)] = color;
        }

        /// <summary>
        /// Plots a line on a <see cref="Canvas"/>.
        /// </summary>
        /// <param name="canvas">The <see cref="Canvas"/> to plot pixels on.</param>
        /// <param name="color">The color of the line.</param>
        /// <param name="line">A <see cref="Line"/> object representing the shape that should be drawn.</param>
        public static void DrawLine(Canvas canvas, Color color, Line line) {
            var dx = line.End.X - line.Start.X;
            var dy = line.End.Y - line.Start.Y;

            if(Math.Abs(dx) > Math.Abs(dy)) {
                if(line.End.X > line.Start.X) {
                    for(var x = line.Start.X; x < line.End.X; x++) {
                        var y = line.Start.Y + dy * (x - line.Start.X) / dx;
                        canvas.Pixels[canvas.GetIndex(x, y)] = color;
                    }
                } else {
                    for(var x = line.Start.X; x > line.End.X; x--) {
                        var y = line.Start.Y + dy * (x - line.Start.X) / dx;
                        canvas.Pixels[canvas.GetIndex(x, y)] = color;
                    }
                }
            } else {
                if(line.End.Y > line.Start.Y) {
                    for(var y = line.Start.Y; y < line.End.Y; y++) {
                        var x = line.Start.X + dx * (y - line.Start.Y) / dy;
                        canvas.Pixels[canvas.GetIndex(x, y)] = color;
                    }
                } else {
                    for(var y = line.Start.Y; y > line.End.Y; y--) {
                        var x = line.Start.X + dx * (y - line.Start.Y) / dy;
                        canvas.Pixels[canvas.GetIndex(x, y)] = color;
                    }
                }
            }
        }

        /// <summary>
        /// Plots a sequence of lines on a <see cref="Canvas"/>.
        /// </summary>
        /// <param name="canvas">The <see cref="Canvas"/> to plot pixels on.</param>
        /// <param name="color">The color of the lines.</param>
        /// <param name="lines">A set of <see cref="Line"/> objects representing the shapes that should be drawn.</param>
        public static void DrawLines(Canvas canvas, Color color, params Line[] lines) {
            foreach(var line in lines)
                DrawLine(canvas, color, line);
        }

        /// <summary>
        /// Plots a sequence of lines on a <see cref="Canvas"/>.
        /// </summary>
        /// <param name="canvas">The <see cref="Canvas"/> to plot pixels on.</param>
        /// <param name="color">The color of the lines.</param>
        /// <param name="points">A list of points to draw. There must be at least two points specified.</param>
        public static void DrawLines(Canvas canvas, Color color, params Point[] points) {
            if(points.Length < 2) throw new ArgumentException("Too few points specified.", nameof(points));
            for(var i = 0; i < points.Length - 1; i++)
                DrawLine(canvas, color, new Line { Start = points[i], End = points[i + 1] });
        }
    }
}
