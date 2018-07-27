using DotSDL.Events;
using DotSDL.Graphics;

namespace Sample.Sprites {
    public class Window : SdlWindow {
        public Window(int scale) : base("Sprites Test",
                                        new Point { X = WindowPosUndefined, Y = WindowPosUndefined },
                                        256 * scale, 196 * scale,
                                        256, 196) {
            KeyPressed += OnKeyPressed;
            KeyReleased += OnKeyReleased;
        }

        protected override void OnDraw(ref Canvas canvas) {
        }

        private void OnKeyPressed(object sender, KeyboardEvent e) {
            throw new System.NotImplementedException();
        }

        private void OnKeyReleased(object sender, KeyboardEvent e) {
            throw new System.NotImplementedException();
        }

        protected override void OnUpdate() {
        }
    }
}
