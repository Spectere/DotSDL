using DotSDL.Graphics;
using System;

namespace DotSDL.Sample.BasicPixels {
    internal class Window : SdlWindow {
        public Window(int width, int height) : base("Basic Pixels", new Point { X = WindowPosUndefined, Y = WindowPosUndefined }, width, height) {}

        private struct Line {
            public Point Start;
            public Point End;
        }

        private void DrawBackground(ref Color[] pixels) {
            byte d = 0;
            var dec = false;

            for(var i = 0; i < pixels.Length; i++) {
                pixels[i].A = 255;
                pixels[i].R = 0;
                pixels[i].G = 0;
                pixels[i].B = 0;

                switch(i % 12 / 3) {
                    case 0:
                        pixels[i].R = (byte)(d / 2);
                        break;
                    case 1:
                        pixels[i].G = (byte)(d / 2);
                        break;
                    case 2:
                        pixels[i].B = (byte)(d / 2);
                        break;
                }

                if(dec) {
                    if(d == 0)
                        dec = false;
                    else
                        d--;
                } else {
                    if(d == 255)
                        dec = true;
                    else
                        d++;
                }
            }
        }

        private void DrawLine(ref Color[] pixels, Color color, Line line) {
            var dx = line.End.X - line.Start.X;
            var dy = line.End.Y - line.Start.Y;

            if(Math.Abs(dx) > Math.Abs(dy)) {
                if(line.End.X > line.Start.X) {
                    for(var x = line.Start.X; x < line.End.X; x++) {
                        var y = line.Start.Y + dy * (x - line.Start.X) / dx;
                        pixels[GetPosition(x, y)] = color;
                    }
                } else {
                    for(var x = line.Start.X; x > line.End.X; x--) {
                        var y = line.Start.Y + dy * (x - line.Start.X) / dx;
                        pixels[GetPosition(x, y)] = color;
                    }
                }
            } else {
                if(line.End.Y > line.Start.Y) {
                    for(var y = line.Start.Y; y < line.End.Y; y++) {
                        var x = line.Start.X + dx * (y - line.Start.Y) / dy;
                        pixels[GetPosition(x, y)] = color;
                    }
                } else {
                    for(var y = line.Start.Y; y > line.End.Y; y--) {
                        var x = line.Start.X + dx * (y - line.Start.Y) / dy;
                        pixels[GetPosition(x, y)] = color;
                    }
                }
            }
        }

        private void DrawSequence(ref Color[] pixels, Color color, params Line[] lines) {
            foreach(var line in lines) {
                DrawLine(ref pixels, color, line);
            }
        }

        private int GetPosition(int x, int y) {
            return (Width * y) + x;
        }

        protected override void OnDraw(ref Canvas canvas) {
            const byte min = 128, max = 255;
            const int offsetX = 96, offsetY = 80;

            DrawBackground(ref canvas.Pixels);
            // D
            DrawSequence(ref canvas.Pixels, RandomColor(min, max),
                new Line { Start = new Point { X = offsetX, Y = offsetY + 96 }, End = new Point { X = offsetX, Y = offsetY } },
                new Line { Start = new Point { X = offsetX, Y = offsetY }, End = new Point { X = offsetX + 32, Y = offsetY } },
                new Line { Start = new Point { X = offsetX + 32, Y = offsetY }, End = new Point { X = offsetX + 48, Y = offsetY + 16 } },
                new Line { Start = new Point { X = offsetX + 48, Y = offsetY + 16 }, End = new Point { X = offsetX + 48, Y = offsetY + 80 } },
                new Line { Start = new Point { X = offsetX + 48, Y = offsetY + 80 }, End = new Point { X = offsetX + 32, Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 32, Y = offsetY + 96 }, End = new Point { X = offsetX, Y = offsetY + 96 } }
            );
            // o
            DrawSequence(ref canvas.Pixels, RandomColor(min, max),
                new Line { Start = new Point { X = offsetX + 64, Y = offsetY + 88}, End = new Point { X = offsetX + 64, Y = offsetY + 56 } },
                new Line { Start = new Point { X = offsetX + 64, Y = offsetY + 56}, End = new Point { X = offsetX + 72, Y = offsetY + 48 } },
                new Line { Start = new Point { X = offsetX + 72, Y = offsetY + 48}, End = new Point { X = offsetX + 80, Y = offsetY + 48 } },
                new Line { Start = new Point { X = offsetX + 80, Y = offsetY + 48}, End = new Point { X = offsetX + 88, Y = offsetY + 56 } },
                new Line { Start = new Point { X = offsetX + 88, Y = offsetY + 56}, End = new Point { X = offsetX + 88, Y = offsetY + 88 } },
                new Line { Start = new Point { X = offsetX + 88, Y = offsetY + 88}, End = new Point { X = offsetX + 80, Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 80, Y = offsetY + 96}, End = new Point { X = offsetX + 72, Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 72, Y = offsetY + 96}, End = new Point { X = offsetX + 64, Y = offsetY + 88 } }
            );
            // t
            DrawSequence(ref canvas.Pixels, RandomColor(min, max), 
                new Line { Start = new Point { X = offsetX + 104 + 12, Y = offsetY + 24}, End = new Point { X = offsetX + 104 + 12, Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 104, Y = offsetY + 48}, End = new Point { X = offsetX + 104 + 24, Y = offsetY + 48 } }
            );

            // S
            DrawSequence(ref canvas.Pixels, RandomColor(min, max),
                new Line { Start = new Point { X = offsetX + 144 + 48, Y = offsetY }, End = new Point { X = offsetX + 144, Y = offsetY } },
                new Line { Start = new Point { X = offsetX + 144, Y = offsetY }, End = new Point { X = offsetX + 144, Y = offsetY + 48 } },
                new Line { Start = new Point { X = offsetX + 144, Y = offsetY + 48 }, End = new Point { X = offsetX + 144 + 48 , Y = offsetY + 48 } },
                new Line { Start = new Point { X = offsetX + 144 + 48, Y = offsetY + 48 }, End = new Point { X = offsetX + 144 + 48 , Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 144 + 48, Y = offsetY + 96 }, End = new Point { X = offsetX + 144, Y = offsetY + 96 } }
            );
            // D
            DrawSequence(ref canvas.Pixels, RandomColor(min, max),
                new Line { Start = new Point { X = offsetX + 208, Y = offsetY + 96 }, End = new Point { X = offsetX + 208, Y = offsetY } },
                new Line { Start = new Point { X = offsetX + 208, Y = offsetY }, End = new Point { X = offsetX + 208 + 32, Y = offsetY } },
                new Line { Start = new Point { X = offsetX + 208 + 32, Y = offsetY }, End = new Point { X = offsetX + 208 + 48, Y = offsetY + 16 } },
                new Line { Start = new Point { X = offsetX + 208 + 48, Y = offsetY + 16 }, End = new Point { X = offsetX + 208 + 48, Y = offsetY + 80 } },
                new Line { Start = new Point { X = offsetX + 208 + 48, Y = offsetY + 80 }, End = new Point { X = offsetX + 208 + 32, Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 208 + 32, Y = offsetY + 96 }, End = new Point { X = offsetX + 208, Y = offsetY + 96 } }
            );
            // L
            DrawSequence(ref canvas.Pixels, RandomColor(min, max),
                new Line { Start = new Point { X = offsetX + 272, Y = offsetY }, End = new Point { X = offsetX + 272, Y = offsetY + 96} },
                new Line { Start = new Point { X = offsetX + 272, Y = offsetY + 96 }, End = new Point { X = offsetX + 272 + 48, Y = offsetY + 96} }
            );
        }

        private Color RandomColor(byte min, byte max) {
            var color = new Color();
            var rng = new Random();

            color.A = 255;
            color.R = (byte)rng.Next(min, max);
            color.G = (byte)rng.Next(min, max);
            color.B = (byte)rng.Next(min, max);

            return color;
        }
    }
}
