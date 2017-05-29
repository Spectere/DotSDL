using DotSDL.Graphics;

namespace Sample.Audio {
    internal class Window : SdlWindow {
        public Window(int width, int height) : base("DotSDL Audio Example", new Point{ X = WindowPosUndefined, Y = WindowPosUndefined }, width, height, 128, 32) {
            
        }
    }
}
