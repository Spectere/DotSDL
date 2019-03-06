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
        }

        private void GenerateBackground(ref Color[] pixels) {
            for(var i = 0; i < pixels.Length; i++) {
                pixels[i].R = 128;
                pixels[i].G = 64;
                pixels[i].B = 32;
            }
        }

        protected override void OnDraw(ref Canvas canvas) {
            GenerateBackground(ref canvas.Pixels);
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
