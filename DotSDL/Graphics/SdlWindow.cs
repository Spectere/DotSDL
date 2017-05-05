using System;
using DotSDL.Sdl;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents an SDL window.
    /// </summary>
    public class SdlWindow {
        private readonly SdlInit sdlInit = SdlInit.Instance;

        private readonly IntPtr _window;
        private readonly IntPtr _renderer;
        private readonly IntPtr _texture;

        private bool _running;

        public int Width { get; }
        public int Height { get; }

        private Canvas _canvas;

        /// <summary>Indicates that the window manager should position the window.</summary>
        public const int WindowPosUndefined = 0x1FFF0000;
        public static int WindowPosUndefinedDisplay(uint x) {
            return (int)(WindowPosUndefined | x);
        }

        /// <summary>Indicates that the window should be in the center of the screen.</summary>
        public const int WindowPosCentered = 0x2FFF0000;
        internal static int WindowPosCenteredDisplay(uint x) {
            return (int)(WindowPosCentered | x);
        }

        public SdlWindow(string name, Point position, int width, int height) {
            sdlInit.InitSubsystem(Init.SubsystemFlags.Video);

            _window = Video.CreateWindow(name, position.X, position.Y, width, height, Video.WindowFlags.Hidden);
            _renderer = Render.CreateRenderer(_window, -1, Render.RendererFlags.Accelerated);
            _texture = Render.CreateTexture(_renderer, Pixels.PixelFormatArgb8888, Render.TextureAccess.Streaming, width, height);

            _canvas = new Canvas(width, height);

            Width = width;
            Height = height;
        }

        private unsafe void BaseDraw() {
            OnDraw(ref _canvas);  // Call the overridden Draw function.

            fixed(void* pixelsPtr = _canvas.Pixels) {
                var ptr = (IntPtr)pixelsPtr;
                Render.UpdateTexture(_texture, IntPtr.Zero, ptr, Width * 4);
            }

            Render.RenderCopy(_renderer, _texture, IntPtr.Zero, IntPtr.Zero);
            Render.RenderPresent(_renderer);
        }

        private void BaseLoad() {
            OnLoad();  // Call the overridden Load function.
        }

        private void BaseUpdate() {
            OnUpdate();  // Call the overridden Update function.
        }

        private void Loop() {
            _running = true;

            while(_running) {
                BaseDraw();
                BaseUpdate();
            }
        }

        /// <summary>
        /// Fired every time the window is drawn to.
        /// </summary>
        protected virtual void OnDraw(ref Canvas canvas) {}

        /// <summary>
        /// Fired before the window is shown.
        /// </summary>
        protected virtual void OnLoad() {}

        /// <summary>
        /// Fired every time the application logic update runs.
        /// </summary>
        protected virtual void OnUpdate() {}

        /// <summary>
        /// Displays the window and begins executing code that's associated with it.
        /// </summary>
        public void Start() {
            BaseLoad();
            Video.ShowWindow(_window);
            Loop();
        }
    }
}
