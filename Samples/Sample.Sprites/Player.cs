using DotSDL.Graphics;

namespace Sample.Sprites {
    public class Player : Sprite {
        private Color _color;
        private int _speed;

        public Player(Color color, int speed) : base(64, 64) {
            _color = color;
            _speed = speed;

            Shown = true;
        }
    }
}
