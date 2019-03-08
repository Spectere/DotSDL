using DotSDL.Events;
using DotSDL.Graphics;
using DotSDL.Input.Keyboard;
using System;

namespace Sample.Sprites {
    public class Window : SdlWindow {
        public Window(int scale) : base("Sprites Test",
                                        new Point { X = WindowPosUndefined, Y = WindowPosUndefined },
                                        256 * scale, 196 * scale,
                                        256, 196) {
            KeyPressed += OnKeyPressed;
            KeyReleased += OnKeyReleased;

            Background.Height = Background.Width = 1024;

            GenerateBackground();
        }

        private void GenerateBackground() {
            // Draw colored, diagonal strips across the entire background canvas.
            var stripRef = new Color[Background.Width * 2];
            for(var i = 0; i < Background.Width * 2; i++) {
                stripRef[i].R = (byte)(Math.Abs(i % 511 - 255) / 2);
                stripRef[i].G = (byte)(80 / (stripRef[i].R + 4) / 2);
                stripRef[i].B = (byte)(120 * (Math.Sin(i * Math.PI / 128 + 256) * 0.2 + 0.8));
            }

            for(var y = 0; y < Background.Height; y++) {
                var stripIdx = y % Background.Width;
                for(var x = 0; x < Background.Width; x++) {
                    var pix = y * Background.Width + x;
                    Background.Pixels[pix] = stripRef[stripIdx + x];
                }
            }

            // Finally, draw a dashed border around the edge to show the edge boundaries.
            // This routine assumes that the canvas is square.
            const int lineSize = 7;
            const int margin = 1;
            var black = new Color { R = 0, G = 0, B = 0 };
            var yellow = new Color { R = 255, G = 255, B = 0 };

            for(var i = margin; i < Background.Width - margin; i++) {
                // Y axis.
                var activeColor = (i - margin) / lineSize % 2 == 1 ? black : yellow;

                var pix = Background.Width * i + margin;
                Background.Pixels[pix] = activeColor;
                pix = Background.Width * i + Background.Width - 1 - margin;
                Background.Pixels[pix] = activeColor;

                // X axis.
                activeColor = (i - margin) / lineSize % 2 == 1 ? yellow : black;

                pix = Background.Width * margin + i;
                Background.Pixels[pix] = activeColor;
                pix = Background.Width * (Background.Height - 1 - margin) + i;
                Background.Pixels[pix] = activeColor;
            }
        }

        protected override void OnDraw() {
        }

        private void OnKeyPressed(object sender, KeyboardEvent e) {
            if(e.Keycode == Keycode.Escape)
                Stop();
        }

        private void OnKeyReleased(object sender, KeyboardEvent e) {
        }

        protected override void OnUpdate() {
        }
    }
}
