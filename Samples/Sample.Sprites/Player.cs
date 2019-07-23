using System;
using DotSDL.Graphics;

namespace Sample.Sprites {
    public class Player : Sprite {
        private const int Radius = 15;
        private const int Size = 32;

        private readonly int _speed;
        private readonly Point _limit;

        public Player(Color color, int speed, int playerId, Point limit) : base(Size, Size, playerId) {
            const byte baseAlpha = 128;

            ColorMod = color;
            _speed = speed;
            _limit = limit;

            Shown = true;

            var gray = (byte)192;

            // Draw some really rough yet strangely lovable circles.
            for(var x = Radius; x > 1; x--) {
                var rX = x;
                var rY = 0;
                var err = 0;

                var alpha = (byte)(baseAlpha + ((float)Radius - x + 1) / Radius * (255 - baseAlpha));
                while(rX >= rY) {
                    PlotMirroredPoints(rX, rY, gray, alpha);
                    PlotMirroredPoints(rY, rX, gray, alpha);

                    rY += 1;
                    if(err <= 0) {
                        err += 2 * rY + 1;
                    }

                    if(err > 0) {
                        rX -= 1;
                        err -= 2 * rX + 1;
                    }
                }

                // Increase the brightness as we move further inside.
                var newGray = (short)(gray * 1.025);
                gray = (byte)(newGray > 255 ? 255 : newGray);
            }

            // Plot a little line so that we can show rotation.
            for(var y = Radius; y >= 0; y--) {
                Pixels[GetIndex(Radius, y)] = new Color { R = 64, G = 64, B = 64, A = 128 };
            }
        }

        public void Move(Point delta) {
            Position.X += delta.X * _speed;
            Position.Y += delta.Y * _speed;

            Position.X = Math.Clamp(Position.X, 0, (int)(_limit.X - (Size * Scale.X)));
            Position.Y = Math.Clamp(Position.Y, 0, (int)(_limit.Y - (Size * Scale.Y)));
        }

        private void PlotMirroredPoints(int x, int y, byte gray, byte alpha) {
            var color = new Color { R = gray, G = gray, B = gray, A = alpha };
            Pixels[GetIndex(Radius + x, Radius + y)] = color;
            Pixels[GetIndex(Radius + x, Radius - y)] = color;
            Pixels[GetIndex(Radius - x, Radius + y)] = color;
            Pixels[GetIndex(Radius - x, Radius - y)] = color;
        }

        public void Rotate(double delta) => Rotation += delta * _speed;
    }
}
