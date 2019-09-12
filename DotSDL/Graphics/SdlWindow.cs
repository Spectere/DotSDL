using DotSDL.Events;
using DotSDL.Input;
using DotSDL.Interop.Core;
using DotSDL.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private float _videoUpdateRate, _gameUpdateRate;
        private float _videoUpdateMs, _gameUpdateMs;
        private bool _videoUpdateUncapped, _gameUpdateUncapped;

        private bool _running;

        private float _nextVideoUpdate;
        private float _nextGameUpdate;
        private float _updateDelta;

        private List<Canvas> _drawList = new List<Canvas>();

        private ScalingQuality _scalingQuality = ScalingQuality.Nearest;

        /// <summary>
        /// An <see cref="IPlatform"/> that contains native functions appropriate to
        /// the platform that this application is running on.
        /// </summary>
        protected IPlatform Platform { get; } = PlatformFactory.GetPlatform();

        /// <summary>Gets the background layer of this window. This is equivalent to accessing Layers[0].</summary>
        public Canvas Background => Layers[0];

        /// <summary>Gets the list of background layers for this window.</summary>
        public List<Canvas> Layers { get; }

        /// <summary>
        /// Gets the number of background layers for this windows. This number will always be greater
        /// than or equal to 1.
        /// </summary>
        public int LayerCount => Layers.Count;

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
        public int RenderWidth { get; }
        /// <summary>Gets the height of the rendering target used by this <see cref="SdlWindow"/>.</summary>
        public int RenderHeight { get; }

        /// <summary>The amount of time, in milliseconds, from when the application was started.</summary>
        public float MillisecondsElapsed { get; private set; } = 0.0f;

        /// <summary>Gets or sets the rate, in hertz, between video updates.</summary>
        public float VideoUpdateRate {
            get => _videoUpdateUncapped ? 0 : _videoUpdateRate;
            set {
                _videoUpdateRate = value;
                if(System.Math.Abs(_videoUpdateRate) < 1.0f) {
                    _videoUpdateUncapped = true;
                } else {
                    _videoUpdateUncapped = false;
                    _videoUpdateMs = 1000 / _videoUpdateRate;
                }
            }
        }

        /// <summary>Gets or sets the rate, in hertz, between game (logic) updates.</summary>
        public float GameUpdateRate {
            get => _gameUpdateUncapped ? 0 : _gameUpdateRate;
            set {
                _gameUpdateRate = value;
                if(System.Math.Abs(_gameUpdateRate) < 1.0f) {
                    _gameUpdateUncapped = true;
                } else {
                    _gameUpdateUncapped = false;
                    _gameUpdateMs = 1000 / _gameUpdateRate;
                }
            }
        }

        /// <summary>Gets a <see cref="Rectangle"/> that can be manipulated to modify how much of the scene is displayed.</summary>
        public Rectangle CameraView { get; }

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
        /// <param name="renderWidth">The width of the rendering target.</param>
        /// <param name="renderHeight">The height of the rendering target.</param>
        public SdlWindow(string title, Point position, int windowWidth, int windowHeight, int renderWidth, int renderHeight) : this(title, position, windowWidth, windowHeight, renderWidth, renderHeight, ScalingQuality.Nearest) { }

        /// <summary>
        /// Creates a new <see cref="SdlWindow"/>.
        /// </summary>
        /// <param name="title">The text that is displayed on the window's title bar.</param>
        /// <param name="position">A <see cref="Point"/> representing the starting position of the window. The X and Y coordinates of the Point can be set to <see cref="WindowPosUndefined"/> or <see cref="WindowPosCentered"/>.</param>
        /// <param name="windowWidth">The width of the window.</param>
        /// <param name="windowHeight">The height of the window.</param>
        /// <param name="renderWidth">The width of the rendering target.</param>
        /// <param name="renderHeight">The height of the rendering target.</param>
        /// <param name="scalingQuality">The scaling (filtering) method to use for the background canvas texture.</param>
        public SdlWindow(string title, Point position, int windowWidth, int windowHeight, int renderWidth, int renderHeight, ScalingQuality scalingQuality) {
            _sdlInit.InitSubsystem(Init.SubsystemFlags.Video);

            _windowTitle = title;
            _window = Video.CreateWindow(title, position.X, position.Y, windowWidth, windowHeight, Video.WindowFlags.Hidden);
            _renderer = Render.CreateRenderer(_window, -1, Render.RendererFlags.Accelerated);

            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

            RenderWidth = renderWidth;
            RenderHeight = renderHeight;

            ScalingQuality = scalingQuality;
            CreateTexture();

            Layers = new List<Canvas> {
                new Background(renderWidth, renderHeight) {
                    Renderer = _renderer,
                    BlendMode = BlendMode.None
                }
            };
            Background.ZOrder = int.MinValue;
            Background.CreateTexture();

            CameraView = new Rectangle(
                new Point(0, 0),
                new Point(WindowWidth, WindowHeight)
            );

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
        /// Adds a background layer to the top of the layer stack.
        /// </summary>
        /// <param name="width">The width of the new layer's texture.</param>
        /// <param name="height">The height of the new layer's texture.</param>
        /// <param name="blendMode">The blending mode of the new layer.</param>
        /// <returns>An integer identifying the new layer.</returns>
        public int AddLayer(int width, int height, BlendMode blendMode = BlendMode.Alpha) {
            var layer = new Background(width, height) {
                Renderer = _renderer,
                BlendMode = blendMode
            };
            layer.CreateTexture();
            Layers.Add(layer);

            return Layers.Count - 1;
        }

        /// <summary>
        /// Handles calling the user draw function and passing the CLR objects to SDL2.
        /// </summary>
        private void BaseDraw() {
            if(IsDestroyed || IsMinimized) return;

            OnDraw();

            Render.SetRenderTarget(_renderer, _texture);

            // Sort all of the canvases, then draw them.
            _drawList.Clear();
            _drawList.AddRange(Layers.Where(l => l.Shown).ToArray());
            _drawList.AddRange(Sprites.Where(s => s.Shown).ToArray());

            foreach(var canvas in _drawList.OrderBy(layer => layer.ZOrder)) {
                switch(canvas) {
                    case Sprite sprite:
                        DrawSprite(sprite);
                        break;
                    default:
                        canvas.Clipping.Position = CameraView.Position;
                        canvas.Clipping.Size = CameraView.Size;
                        canvas.UpdateTexture();
                        unsafe {
                            var canvasClippingRect = canvas.Clipping.SdlRect;
                            Render.RenderCopy(_renderer, canvas.Texture, new IntPtr(&canvasClippingRect), IntPtr.Zero);
                        }

                        break;
                }
            }

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
        private void BaseUpdate(float delta) {
            if(IsDestroyed) return;

            Events.EventHandler.ProcessEvents();
            OnUpdate(delta);  // Call the overridden Update function.
        }

        /// <summary>
        /// Creates the rendering target that all of the layers will be drawn to prior to rendering.
        /// </summary>
        private void CreateTexture() {
            DestroyTexture();
            Hints.SetHint(Hints.RenderScaleQuality, ScalingQuality.ToString());
            _texture = Render.CreateTexture(_renderer, Pixels.PixelFormatArgb8888, Render.TextureAccess.Target, RenderWidth, RenderHeight);
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
        /// Plots a sprite to the screen. Please note that this method is called by DotSDL's drawing
        /// routines and does not need to be called manually. Additionally, this method will never be
        /// called if there are no active sprites. You usually do not need to override this method.
        /// </summary>
        public virtual void DrawSprite(Sprite sprite) {
            var srcRect = sprite.Clipping.SdlRect;
            var drawSize = sprite.DrawSize;

            Rectangle dest;
            Point rotationCenter;
            if(sprite.CoordinateSystem == CoordinateSystem.ScreenSpace) {
                dest = new Rectangle(sprite.Position, drawSize);
                rotationCenter = sprite.RotationCenter;
            } else {
                // Create a set of world coordinates based on the position of the camera
                // and this sprite.
                var relPosition = new Point(sprite.Position - CameraView.Position);
                var screenPosition = new Point(
                    (int)((float)relPosition.X / CameraView.Size.X * RenderWidth),
                    (int)((float)relPosition.Y / CameraView.Size.Y * RenderHeight)
                );
                var scaleFactorX = (float)RenderWidth / CameraView.Size.X;
                var scaleFactorY = (float)RenderHeight / CameraView.Size.Y;
                var size = new Point(
                    (int)(drawSize.X * scaleFactorX),
                    (int)(drawSize.Y * scaleFactorY)
                );

                dest = new Rectangle(screenPosition, size);
                rotationCenter = new Point(
                    (int)(sprite.RotationCenter.X * scaleFactorX),
                    (int)(sprite.RotationCenter.Y * scaleFactorY)
                );
            }

            var destRect = dest.SdlRect;
            var rotationCenterPoint = rotationCenter.SdlPoint;

            unsafe {
                var srcRectPtr = new IntPtr(&srcRect);
                var destRectPtr = new IntPtr(&destRect);
                var rotationCenterPtr = new IntPtr(&rotationCenterPoint);

                Render.RenderCopyEx(
                    renderer: _renderer,
                    texture: sprite.Texture,
                    srcRect: srcRectPtr,
                    dstRect: destRectPtr,
                    angle: sprite.Rotation,
                    center: rotationCenterPtr,
                    flip: sprite.Flip
                );
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
            long MsToNs(float ms) => (long)(ms * 1000000);

            var sw = new Stopwatch();
            _running = true;

            while(_running) {
                sw.Restart();

                if(_nextGameUpdate <= 0 || _gameUpdateUncapped) {
                    BaseUpdate(_updateDelta);
                    _updateDelta = 0;
                    _nextGameUpdate += _gameUpdateMs;
                }

                if(_nextVideoUpdate <= 0 || _videoUpdateUncapped) {
                    BaseDraw();
                    _nextVideoUpdate += _videoUpdateMs;
                }

                var cycleElapsed = (float)sw.Elapsed.TotalMilliseconds;
                MillisecondsElapsed += cycleElapsed;
                _nextGameUpdate -= cycleElapsed;
                _nextVideoUpdate -= cycleElapsed;
                _updateDelta += cycleElapsed;

                if(!_videoUpdateUncapped && !_gameUpdateUncapped) {
                    var waitMs = _nextGameUpdate > _nextVideoUpdate ? _nextVideoUpdate : _nextGameUpdate;
                    if(waitMs > 0)
                        Platform.Nanosleep(MsToNs(waitMs));

                    _updateDelta += waitMs;
                    _nextGameUpdate -= waitMs;
                    _nextVideoUpdate -= waitMs;
                }
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
        protected virtual void OnUpdate(float delta) { }

        /// <summary>
        /// Removes a layer from the layer stack.
        /// </summary>
        /// <param name="id">The unique identifier of the layer to remove.</param>
        /// <remarks>The background layer (layer 0) cannot be deleted.</remarks>
        /// <exception cref="ArgumentOutOfRangeException"><c>id</c> is less than 0.</exception>
        /// /// <exception cref="ArgumentOutOfRangeException"><c>id</c> is equal to or greater than <see cref="LayerCount"/>.</exception>
        public void RemoveLayer(int id) {
            if(id == 0) throw new ArgumentOutOfRangeException(nameof(id), "The background object (layer 0) cannot be deleted.");
            Layers.RemoveAt(id);
        }

        /// <summary>
        /// Displays the window and begins executing code that's associated with it.
        /// </summary>
        public void Start() => Start(0, 0);

        /// <summary>
        /// Displays the window and begins executing code that's associated with it.
        /// </summary>
        /// <param name="updateRate">The desired number of video and game logic updates per second. 0 causes the display and game to be updated as quickly as possible.</param>
        public void Start(float updateRate) => Start(updateRate, updateRate);

        /// <summary>
        /// Displays the window and begins executing code that's associated with it.
        /// </summary>
        /// <param name="drawRate">The desired number of draw calls per second. 0 causes the display to be updated as quickly as possible.</param>
        /// <param name="updateRate">The desired number of game logic updates per second. 0 causes the game to be updated as quickly as possible.</param>
        public void Start(float drawRate, float updateRate) {
            VideoUpdateRate = drawRate;
            GameUpdateRate = updateRate;

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
