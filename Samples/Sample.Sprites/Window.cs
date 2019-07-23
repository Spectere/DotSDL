using DotSDL.Events;
using DotSDL.Graphics;
using DotSDL.Input.Keyboard;
using System;

namespace Sample.Sprites {
    public class Window : SdlWindow {
        private Player _player1, _player2;
        private Point _player1Delta, _player2Delta;
        private double _player1Rotation, _player2Rotation;

        private const int ViewMargin = 16;

        public Window(int scale) : base("Sprites Test",
                                        new Point(WindowPosUndefined, WindowPosUndefined),
                                        256 * scale, 196 * scale,
                                        256, 196) {
            KeyPressed += OnKeyPressed;
            KeyReleased += OnKeyReleased;

            Background.Height = Background.Width = 1024;

            GenerateBackground();
            GeneratePlayers();
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

            // Darken every other line, because why not. :)
            for(var y = 2; y < Background.Height; y += 2) {
                for(var x = 0; x < Background.Width; x++) {
                    var pix = Background.Width * y + x;
                    Background.Pixels[pix].R = (byte)(Background.Pixels[pix].R * 0.8);
                    Background.Pixels[pix].G = (byte)(Background.Pixels[pix].G * 0.8);
                    Background.Pixels[pix].B = (byte)(Background.Pixels[pix].B * 0.8);
                }
            }

            // Finally, draw a dashed border around the edge to show the edge boundaries.
            // This routine assumes that the canvas is square.
            const int lineSize = 7;
            const int margin = 4;
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

        private void GeneratePlayers() {
            var limit = new Point(1024, 1024);
            _player1 = new Player(new Color { R = 255, G = 64, B = 64 }, 2, 1, limit);
            _player2 = new Player(new Color { R = 64, G = 64, B = 255 }, 3, 2, limit);

            _player1.Position.X = 24;
            _player1.Position.Y = 24;
            _player1.Opacity = 128;
            _player1Delta = new Point();

            _player2.Position.X = 96;
            _player2.Position.Y = 24;
            _player2.Opacity = 196;
            _player2Delta = new Point();

            _player1.Scale.X = 1.5f;
            _player1.BlendMode = BlendMode.Additive;

            _player2.ScalingQuality = ScalingQuality.Linear;
            _player2.Scale.Y = 2.0f;

            _player1.RotationCenter = _player1.Center;
            _player2.RotationCenter = _player2.Center;

            Sprites.Add(_player1);
            Sprites.Add(_player2);
        }

        private void OnKeyPressed(object sender, KeyboardEvent e) {
            if(e.Keycode == Keycode.Escape)
                Stop();

            if(e.Keycode == Keycode.W)
                _player1Delta.Y = -1;
            if(e.Keycode == Keycode.S)
                _player1Delta.Y = 1;
            if(e.Keycode == Keycode.A)
                _player1Delta.X = -1;
            if(e.Keycode == Keycode.D)
                _player1Delta.X = 1;
            if(e.Keycode == Keycode.Q)
                _player1Rotation = -1;
            if(e.Keycode == Keycode.E)
                _player1Rotation = 1;

            if(e.Keycode == Keycode.Up)
                _player2Delta.Y = -1;
            if(e.Keycode == Keycode.Down)
                _player2Delta.Y = 1;
            if(e.Keycode == Keycode.Left)
                _player2Delta.X = -1;
            if(e.Keycode == Keycode.Right)
                _player2Delta.X = 1;
            if(e.Keycode == Keycode.Delete)
                _player2Rotation = -1;
            if(e.Keycode == Keycode.PageDown)
                _player2Rotation = 1;
        }

        private void OnKeyReleased(object sender, KeyboardEvent e) {
            if(e.Keycode == Keycode.W || e.Keycode == Keycode.S)
                _player1Delta.Y = 0;
            if(e.Keycode == Keycode.A || e.Keycode == Keycode.D)
                _player1Delta.X = 0;
            if(e.Keycode == Keycode.Q || e.Keycode == Keycode.E)
                _player1Rotation = 0;

            if(e.Keycode == Keycode.Up || e.Keycode == Keycode.Down)
                _player2Delta.Y = 0;
            if(e.Keycode == Keycode.Left || e.Keycode == Keycode.Right)
                _player2Delta.X = 0;
            if(e.Keycode == Keycode.Delete || e.Keycode == Keycode.PageDown)
                _player2Rotation = 0;
        }

        protected override void OnUpdate() {
            _player1.Move(_player1Delta);
            _player1.Rotate(_player1Rotation);

            _player2.Move(_player2Delta);
            _player2.Rotate(_player2Rotation);

            var p1End = _player1.Position + _player1.DrawSize;
            var p2End = _player2.Position + _player2.DrawSize;

            var x1 = (_player1.Position.X <= _player2.Position.X
                          ? _player1.Position.X
                          : _player2.Position.X) - ViewMargin;

            var x2 = (p1End.X >= p2End.X ? p1End.X : p2End.X) + ViewMargin;

            var y1 = (_player1.Position.Y <= _player2.Position.Y
                          ? _player1.Position.Y
                          : _player2.Position.Y) - ViewMargin;

            var y2 = (p1End.Y >= p2End.Y ? p1End.Y : p2End.Y) + ViewMargin;

            x1 = Math.Clamp(x1, 0, Background.Width - ViewMargin);
            x2 = Math.Clamp(x2, ViewMargin, Background.Width);
            y1 = Math.Clamp(y1, 0, Background.Height - ViewMargin);
            y2 = Math.Clamp(y2, ViewMargin, Background.Height);

            CameraView.Position.X = x1;
            CameraView.Position.Y = y1;
            CameraView.Size.X = x2 - x1;
            CameraView.Size.Y = y2 - y1;

            WindowTitle = $"({x1} {y2}), ({x2 - x1}, {y2 - y1})" +
                          $" | P1: {_player1.Position}: {_player1.Rotation:F2}"
                        + $" | P2: {_player2.Position}: {_player2.Rotation:F2}";
        }
    }
}
