using DotSDL.Events;
using DotSDL.Input;
using DotSDL.Interop.Core;
using System;
using System.Linq;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents an SDL window.
    /// </summary>
    public class SdlWindow : IResourceObject {
        private readonly SdlInit _sdlInit = SdlInit.Instance;
        private readonly ResourceManager _resources = ResourceManager.Instance;
        private string _windowTitle;

        private readonly IntPtr _window;
        private readonly IntPtr _renderer;
        private IntPtr _texture;
        private bool _hasTexture;

        public readonly Canvas Background;

        private bool _running;

        private uint _nextVideoUpdate;
        private uint _nextGameUpdate;

        private ScalingQuality _scalingQuality = ScalingQuality.Nearest;

        /// <summary><c>true</c> if this <see cref="SdlWindow"/> instance has been destroyed, othersize <c>false</c>.</summary>
        public bool IsDestroyed { get; set; }

        /// <summary><c>true</c> if this <see cref="SdlWindow"/> has been minimized, othersize <c>false</c>.</summary>
        public bool IsMinimized { get; set; }

        public ResourceType ResourceType => ResourceType.Window;

        /// <summary>Gets the width of this <see cref="SdlWindow"/>.</summary>
        public int WindowWidth { get; }
        /// <summary>Gets the height of this <see cref="SdlWindow"/>.</summary>
        public int WindowHeight { get; }

        /// <summary>Gets or sets the title of this <see cref="SdlWindow"/>.</summary>
        public string WindowTitle {
            get => _windowTitle;
            set {
                _windowTitle = value;
                Video.SetWindowTitle(_window, _windowTitle);
            }
        }

        /// <summary>Gets the width of the rendering target used by this <see cref="SdlWindow"/>.</summary>
        public int TextureWidth { get; }
        /// <summary>Gets the height of the rendering target used by this <see cref="SdlWindow"/>.</summary>
        public int TextureHeight { get; }

        /// <summary>The amount of time, in milliseconds, from when the application was started.</summary>
        public uint TicksElapsed => Timer.GetTicks();
        /// <summary>Gets or sets the amount of time, in milliseconds, between video updates.</summary>
        public uint VideoUpdateTicks { get; set; }
        /// <summary>Gets or sets the amount of time, in milliseconds, between game (logic) updates.</summary>
        public uint GameUpdateTicks { get; set; }

        /// <summary>The list of active <see cref="Sprite"/> objects.</summary>
        public SpriteList Sprites { get; }

        /// <summary>Indicates that the window manager should position the window. To place the window on a specific display, use the <see cref="WindowPosCenteredDisplay"/> function.</summary>
        public const int WindowPosUndefined = 0x1FFF0000;

        /// <summary>Fired when tboolhe window's close button is clicked.</summary>
        public event EventHandler<WindowEvent> Closed;

        /// <summary>Fired when a key is pressed.</summary>
        public event EventHandler<KeyboardEvent> KeyPressed;

        /// <summary>Fired when a key is released.</summary>
        public event EventHandler<KeyboardEvent> KeyReleased;

        /// <summary>Fired when the window's minimize button is clicked.</summary>
        public event EventHandler<WindowEvent> Minimized;

        /// <summary>Fired when the window is restored.</summary>
        public event EventHandler<WindowEvent> Restored;

        public ScalingQuality ScalingQuality {
            get => _scalingQuality;
            set {
                _scalingQuality = value;

                if(_hasTexture)
                    CreateTexture();
            }
        }

        /// <summary>
        /// Calculates a value that allows the window to be placed on a specific display, with its exact position determined by the window manager.
        /// </summary>
        /// <param name="display">The index of the display to place the window on.</param>
        /// <returns>A coordinate value that should be passed to the <see cref="SdlWindow"/> constructor.</returns>
        public static int WindowPosUndefinedDisplay(uint display) {
            return (int)(WindowPosUndefined | display);
        }

        /// <summary>
        /// Indicates that the window should be in the center of the screen. To center the window on a specific display, use the <see cref="WindowPosCenteredDisplay"/> function.
        /// </summary>
        public const int WindowPosCentered = 0x2FFF0000;

        /// <summary>
        /// Calculates a value that allows the window to be placed in the center of a specified display.
        /// </summary>
        /// <param name="display">The index of the display to place the window on.</param>
        /// <returns>A coordinate value that should be passed to the <see cref="SdlWindow"/> constructor.</returns>
        public static int WindowPosCenteredDisplay(uint display) {
            return (int)(WindowPosCentered | display);
        }

        /// <summary>
        /// Creates a new <see cref="SdlWindow"/>.readonly
        /// </summary>
        /// <param name="title">The text that is displayed on the window's title bar.</param>
        /// <param name="position">A <see cref="Point"/> representing the starting position of the window. The X and Y coordinates of the Point can be set to <see cref="WindowPosUndefined"/> or <see cref="WindowPosCentered"/>.</param>
        /// <param name="windowWidth">The width of the window.</param>
        /// <param name="windowHeight">The height of the window.</param>
        public SdlWindow(string title, Point position, int windowWidth, int windowHeight) : this(title, position, windowWidth, windowHeight, windowWidth, windowHeight, ScalingQuality.Nearest) { }

        /// <summary>
        /// Creates a new <see cref="SdlWindow"/>.
        /// </summary>
        /// <param name="title">The text that is displayed on the window's title bar.</param>
        /// <param name="position">A <see cref="Point"/> representing the starting position of the window. The X and Y coordinates of the Point can be set to <see cref="WindowPosUndefined"/> or <see cref="WindowPosCentered"/>.</param>
        /// <param name="windowWidth">The width of the window.</param>
        /// <param name="windowHeight">The height of the window.</param>
        /// <param name="scalingQuality">The scaling (filtering) method to use for the background canvas texture.</param>
        public SdlWindow(string title, Point position, int windowWidth, int windowHeight, ScalingQuality scalingQuality) : this(title, position, windowWidth, windowHeight, windowWidth, windowHeight, scalingQuality) { }

        /// <summary>
        /// Creates a new <see cref="SdlWindow"/>.
        /// </summary>
        /// <param name="title">The text that is displayed on the window's title bar.</param>
        /// <param name="position">A <see cref="Point"/> representing the starting position of the window. The X and Y coordinates of the Point can be set to <see cref="WindowPosUndefined"/> or <see cref="WindowPosCentered"/>.</param>
        /// <param name="windowWidth">The width of the window.</param>
        /// <param name="windowHeight">The height of the window.</param>
        /// <param name="textureWidth">The width of the window's texture.</param>
        /// <param name="textureHeight">The height of the window's texture.</param>
        public SdlWindow(string title, Point position, int windowWidth, int windowHeight, int textureWidth, int textureHeight) : this(title, position, windowWidth, windowHeight, textureWidth, textureHeight, ScalingQuality.Nearest) { }

        /// <summary>
        /// Creates a new <see cref="SdlWindow"/>.
        /// </summary>
        /// <param name="title">The text that is displayed on the window's title bar.</param>
        /// <param name="position">A <see cref="Point"/> representing the starting position of the window. The X and Y coordinates of the Point can be set to <see cref="WindowPosUndefined"/> or <see cref="WindowPosCentered"/>.</param>
        /// <param name="windowWidth">The width of the window.</param>
        /// <param name="windowHeight">The height of the window.</param>
        /// <param name="textureWidth">The width of the window's texture.</param>
        /// <param name="textureHeight">The height of the window's texture.</param>
        /// <param name="scalingQuality">The scaling (filtering) method to use for the background canvas texture.</param>
        public SdlWindow(string title, Point position, int windowWidth, int windowHeight, int textureWidth, int textureHeight, ScalingQuality scalingQuality) {
            _sdlInit.InitSubsystem(Init.SubsystemFlags.Video);

            _windowTitle = title;
            _window = Video.CreateWindow(title, position.X, position.Y, windowWidth, windowHeight, Video.WindowFlags.Hidden);
            _renderer = Render.CreateRenderer(_window, -1, Render.RendererFlags.Accelerated);

            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

            TextureWidth = textureWidth;
            TextureHeight = textureHeight;

            ScalingQuality = scalingQuality;
            CreateTexture();

            Background = new Canvas(textureWidth, textureHeight) {
                Renderer = _renderer,
                BlendMode = BlendMode.None
            };
            Background.CreateTexture();

            Sprites = new SpriteList(_renderer);

            IsDestroyed = false;
            _resources.RegisterResource(this);
        }

        /// <summary>
        /// Releases resources used by the <see cref="SdlWindow"/> instance.
        /// </summary>
        ~SdlWindow() {
            DestroyObject();
            _resources.UnregisterResource(this);
        }

        /// <summary>
        /// Handles calling the user draw function and passing the CLR objects to SDL2.
        /// </summary>
        private void BaseDraw() {
            if(IsDestroyed || IsMinimized) return;

            OnDraw();

            Render.SetRenderTarget(_renderer, _texture);

            // Blit the Canvas to the target texture.
            Background.UpdateTexture();
            unsafe {
                var canvasClippingRect = Background.Clipping.SdlRect;
                Render.RenderCopy(_renderer, Background.Texture, new IntPtr(&canvasClippingRect), IntPtr.Zero);
            }

            // Plot sprites on top of the background layer.
            if(Sprites.Count > 0) DrawSprites();

            Render.SetRenderTarget(_renderer, IntPtr.Zero);
            Render.RenderCopy(_renderer, _texture, IntPtr.Zero, IntPtr.Zero);

            Render.RenderPresent(_renderer);
        }

        /// <summary>
        /// Handles setting up the <see cref="SdlWindow"/>.
        /// </summary>
        private void BaseLoad() {
            OnLoad();  // Call the overridden Load function.
        }

        /// <summary>
        /// Handles updating the application logic for the <see cref="SdlWindow"/>.
        /// </summary>
        private void BaseUpdate() {
            if(IsDestroyed) return;

            Events.EventHandler.ProcessEvents();
            OnUpdate();  // Call the overridden Update function.
        }

        /// <summary>
        /// Creates the rendering target that all of the layers will be drawn to prior to rendering.
        /// </summary>
        private void CreateTexture() {
            DestroyTexture();
            Hints.SetHint(Hints.RenderScaleQuality, ScalingQuality.ToString());
            _texture = Render.CreateTexture(_renderer, Pixels.PixelFormatArgb8888, Render.TextureAccess.Target, TextureWidth, TextureHeight);
            _hasTexture = true;
        }

        /// <summary>
        /// Destroys this <see cref="SdlWindow"/>.
        /// </summary>
        public void DestroyObject() {
            Video.DestroyWindow(_window);
            IsDestroyed = true;
        }

        /// <summary>
        /// Destroys the render target associated with this <see cref="SdlWindow"/>.
        /// </summary>
        private void DestroyTexture() {
            if(!_hasTexture) return;

            Render.DestroyTexture(_texture);
            _hasTexture = false;
        }

        /// <summary>
        /// Plots the sprites stored in <see cref="Sprites"/> to the screen. Please note that this method is called by
        /// DotSDL's drawing routines and does not need to be called manually. Additionally, this method will not be
        /// called if there are no sprites defined. You usually do not need to override this method.
        /// </summary>
        public virtual void DrawSprites() {
            Render.SetRenderTarget(_renderer, _texture);
            foreach(var sprite in Sprites.Where(e => e.Shown).OrderBy(e => e.ZOrder)) {
                var srcRect = sprite.Clipping.SdlRect;
                var drawSize = new Point(
                    (int)(srcRect.W * sprite.Scale.X),
                    (int)(srcRect.H * sprite.Scale.Y)
                );
                var destRect = new Rectangle(sprite.Position, drawSize).SdlRect;

                unsafe {
                    var srcRectPtr = new IntPtr(&srcRect);
                    var destRectPtr = new IntPtr(&destRect);

                    Render.RenderCopyEx(
                        renderer: _renderer,
                        texture: sprite.Texture,
                        srcRect: srcRectPtr,
                        dstRect: destRectPtr,
                        angle: sprite.Rotation,
                        center: sprite.RotationCenter.SdlPoint,
                        flip: sprite.Flip
                    );
                }
            }
        }

        /// <summary>
        /// Retrieves the SDL resource ID for this <see cref="SdlWindow"/>.
        /// </summary>
        /// <returns></returns>
        public uint GetResourceId() {
            return Video.GetWindowId(_window);
        }

        /// <summary>
        /// Triggers this window to handle a specified <see cref="KeyboardEvent"/>.
        /// </summary>
        /// <param name="ev">The <see cref="KeyboardEvent"/> to handle.</param>
        internal void HandleEvent(KeyboardEvent ev) {
            switch(ev.State) {
                case ButtonState.Pressed:
                    KeyPressed?.Invoke(this, ev);
                    break;
                case ButtonState.Released:
                    KeyReleased?.Invoke(this, ev);
                    break;
            }
        }

        /// <summary>
        /// Triggers this window to handle a specified <see cref="WindowEvent"/>.
        /// </summary>
        /// <param name="ev">The <see cref="WindowEvent"/> to handle.</param>
        internal void HandleEvent(WindowEvent ev) {
            switch(ev.Event) {
                case WindowEventType.Close:
                    OnClose(ev);
                    break;
                case WindowEventType.Minimized:
                    OnMinimize(ev);
                    break;
                case WindowEventType.Restored:
                    OnRestore(ev);
                    break;
            }
        }

        /// <summary>
        /// A game loop that calls the <see cref="SdlWindow"/> update and draw functions.
        /// </summary>
        private void Loop() {
            _running = true;

            while(_running) {
                var ticks = TicksElapsed;

                if(ticks > _nextGameUpdate || GameUpdateTicks == 0) {
                    _nextGameUpdate = ticks + GameUpdateTicks;
                    BaseUpdate();
                }

                if(ticks > _nextVideoUpdate || VideoUpdateTicks == 0) {
                    _nextVideoUpdate = ticks + VideoUpdateTicks;
                    BaseDraw();
                }

                if(VideoUpdateTicks <= 0 && GameUpdateTicks <= 0) continue;  // Cook the CPU!

                var updateTicks = (long)(_nextGameUpdate > _nextVideoUpdate ? _nextVideoUpdate : _nextGameUpdate) - TicksElapsed;
                if(updateTicks > 0)
                    Timer.Delay((uint)updateTicks);
            }
        }

        /// <summary>
        /// Called when the window's close button is clicked.
        /// </summary>
        private void OnClose(WindowEvent ev) {
            if(Closed is null) Stop();
            else Closed(this, ev);
        }

        /// <summary>
        /// Called every time the window is drawn to.
        /// </summary>
        protected virtual void OnDraw() { }

        /// <summary>
        /// Called before the window is shown.
        /// </summary>
        protected virtual void OnLoad() { }

        /// <summary>
        /// Called when the window is minimized.
        /// </summary>
        private void OnMinimize(WindowEvent ev) {
            IsMinimized = true;
            Minimized?.Invoke(this, ev);
        }

        /// <summary>
        /// Called when the window is restored.
        /// </summary>
        private void OnRestore(WindowEvent ev) {
            IsMinimized = false;
            Restored?.Invoke(this, ev);
        }

        /// <summary>
        /// Called every time the application logic update runs.
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// Displays the window and begins executing code that's associated with it.
        /// </summary>
        public void Start() {
            Start(0, 0);
        }

        /// <summary>
        /// Displays the window and begins executing code that's associated with it.
        /// </summary>
        /// <param name="updateRate">The desired number of milliseconds between frames and game logic updates. 0 causes the display and game to be continuously updated.</param>
        public void Start(uint updateRate) {
            Start(updateRate, updateRate);
        }

        /// <summary>
        /// Displays the window and begins executing code that's associated with it.
        /// </summary>
        /// <param name="drawRate">The desired number of milliseconds between draw calls. 0 causes the display to be continuously updated.</param>
        /// <param name="updateRate">The desired number of milliseconds between game logic updates. 0 causes the game to be continuously updated.</param>
        public void Start(uint drawRate, uint updateRate) {
            VideoUpdateTicks = drawRate;
            GameUpdateTicks = updateRate;

            BaseLoad();
            Video.ShowWindow(_window);
            Loop();
        }

        /// <summary>
        /// Stops executing the game loop and destroys the window.
        /// </summary>
        public void Stop() {
            _running = false;
            DestroyObject();
        }
    }
}
