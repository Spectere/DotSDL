using DotSDL.Graphics;

namespace Sample.Sprites {
    public class Player : Sprite {
        private const int Radius = 15;
        private const int Size = 32;

        private Color _color;
        private int _speed;

        public Player(Color color, int speed, int playerId) : base(Size, Size, playerId) {
            _color = color;
            _speed = speed;

            Shown = true;

            // Draw some really rough yet strangely lovable circles.
            for(var x = Radius; x > 1; x--) {
                var rX = x;
                var rY = 0;
                var err = 0;

                while(rX >= rY) {
                    PlotMirroredPoints(rX, rY);
                    PlotMirroredPoints(rY, rX);

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
                var newR = (short)(_color.R * 1.1);
                _color.R = (byte)(newR > 255 ? 255 : newR);

                var newG = (short)(_color.G * 1.1);
                _color.G = (byte)(newG > 255 ? 255 : newG);

                var newB = (short)(_color.B * 1.1);
                _color.B = (byte)(newB > 255 ? 255 : newB);
            }

            // Plot a little line so that we can show rotation.
            for(var y = Radius; y >= 0; y--) {
                Pixels[GetIndex(Radius, y)] = new Color { R = 64, G = 255, B = 64 };
            }
        }

        public void Move(Point delta) {
            // TODO: Support some basic vector arithmetic for points.
            Position.X += delta.X * _speed;
            Position.Y += delta.Y * _speed;
        }

        private void PlotMirroredPoints(int x, int y) {
            Pixels[GetIndex(Radius + x, Radius + y)] = _color;
            Pixels[GetIndex(Radius + x, Radius - y)] = _color;
            Pixels[GetIndex(Radius - x, Radius + y)] = _color;
            Pixels[GetIndex(Radius - x, Radius - y)] = _color;
        }
    }
}
