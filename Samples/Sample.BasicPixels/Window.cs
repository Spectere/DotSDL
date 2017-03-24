using DotSDL.Graphics;

namespace DotSDL.Sample.BasicPixels {
    internal class Window : SdlWindow {
        public Window(int width, int height) : base("Basic Pixels", new Point { X = WindowPosUndefined, Y = WindowPosUndefined }, width, height) {}

        public override void OnDraw() {
            
        }
    }
}
