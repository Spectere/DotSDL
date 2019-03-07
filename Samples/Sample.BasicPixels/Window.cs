using DotSDL.Graphics;
using DotSDL.Input.Keyboard;
using System;

namespace Sample.BasicPixels {
    internal class Window : SdlWindow {
        public Window(int width, int height) : base("Basic Pixels", new Point { X = WindowPosUndefined, Y = WindowPosUndefined }, width, height) {
            KeyPressed += Window_KeyPressed;
        }

        private void DrawBackground() {
            byte d = 0;
            var dec = false;

            var pixels = Background.Pixels;
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

        protected override void OnDraw() {
            const byte min = 128, max = 255;
            const int offsetX = 96, offsetY = 80;

            DrawBackground();
            var color = RandomColor(min, max);

            // D
            Plotting.DrawLines(Background, color,
                new Line { Start = new Point { X = offsetX, Y = offsetY + 96 }, End = new Point { X = offsetX, Y = offsetY } },
                new Line { Start = new Point { X = offsetX, Y = offsetY }, End = new Point { X = offsetX + 10, Y = offsetY } },
                new Line { Start = new Point { X = offsetX, Y = offsetY + 96 }, End = new Point { X = offsetX + 10, Y = offsetY + 96 } }
            );
            Plotting.DrawBezier(Background, color, 100,
                new Point { X = offsetX + 8, Y = offsetY },
                new Point { X = offsetX + 48, Y = offsetY },
                new Point { X = offsetX + 48, Y = offsetY + 96 },
                new Point { X = offsetX + 8, Y = offsetY + 96 }
            );

            // o
            Plotting.DrawEllipse(Background, color, new Point { X = offsetX + 76, Y = offsetY + 72 }, 12, 24);

            // t
            Plotting.DrawLines(Background, color,
                new Line { Start = new Point { X = offsetX + 104 + 12, Y = offsetY + 24 }, End = new Point { X = offsetX + 104 + 12, Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 104, Y = offsetY + 48 }, End = new Point { X = offsetX + 104 + 24, Y = offsetY + 48 } }
            );

            // S
            Plotting.DrawBezier(Background, color, 100,
                new Point { X = offsetX + 144 + 48, Y = offsetY },
                new Point { X = offsetX + 144 - 24, Y = offsetY },
                new Point { X = offsetX + 144, Y = offsetY + 48 },
                new Point { X = offsetX + 144 + 48, Y = offsetY + 48 },
                new Point { X = offsetX + 144 + 72, Y = offsetY + 96 },
                new Point { X = offsetX + 144, Y = offsetY + 96 }
            );

            // D
            Plotting.DrawLines(Background, color,
                new Line { Start = new Point { X = offsetX + 208, Y = offsetY + 96 }, End = new Point { X = offsetX + 208, Y = offsetY } },
                new Line { Start = new Point { X = offsetX + 208, Y = offsetY }, End = new Point { X = offsetX + 208 + 10, Y = offsetY } },
                new Line { Start = new Point { X = offsetX + 208, Y = offsetY + 96 }, End = new Point { X = offsetX + 208 + 10, Y = offsetY + 96 } }
            );
            Plotting.DrawBezier(Background, color, 100,
                new Point { X = offsetX + 208 + 8, Y = offsetY },
                new Point { X = offsetX + 208 + 48, Y = offsetY },
                new Point { X = offsetX + 208 + 48, Y = offsetY + 96 },
                new Point { X = offsetX + 208 + 8, Y = offsetY + 96 }
            );

            // L
            Plotting.DrawLines(Background, color,
                new Line { Start = new Point { X = offsetX + 272, Y = offsetY }, End = new Point { X = offsetX + 272, Y = offsetY + 96 } },
                new Line { Start = new Point { X = offsetX + 272, Y = offsetY + 96 }, End = new Point { X = offsetX + 272 + 48, Y = offsetY + 96 } }
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

        private void Window_KeyPressed(object sender, DotSDL.Events.KeyboardEvent e) {
            if(e.Keycode == Keycode.Escape)
                Stop();
        }
    }
}
