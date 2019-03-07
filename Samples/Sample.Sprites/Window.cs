using DotSDL.Events;
using DotSDL.Graphics;
using DotSDL.Input.Keyboard;

namespace Sample.Sprites {
    public class Window : SdlWindow {
        public Window(int scale) : base("Sprites Test",
                                        new Point { X = WindowPosUndefined, Y = WindowPosUndefined },
                                        256 * scale, 196 * scale,
                                        256, 196) {
            KeyPressed += OnKeyPressed;
            KeyReleased += OnKeyReleased;

            Background.Width = 1024;
            Background.Height = 1024;

            GenerateBackground();
        }

        private void GenerateBackground() {
            // Draw colored, diagonal strips across the entire background canvas.
            for(var i = 0; i < Background.Width; i++) {
                Background.Pixels[i].R = 128;
                Background.Pixels[i].G = 64;
                Background.Pixels[i].B = 32;
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
