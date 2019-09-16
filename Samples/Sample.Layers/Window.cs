using System;
using DotSDL.Events;
using DotSDL.Graphics;
using DotSDL.Input.Keyboard;

namespace Sample.Layers {
    public class Window : SdlWindow {
        private const double Amplitude = 32.0f;
        private const double Period = Math.PI * 4;

        private readonly int _redLayer, _greenLayer, _blueLayer;

        private double _redPhase = 0.0, _greenPhase = 2.0, _bluePhase = 4.0;
        private const double RedSpeed = 0.010, GreenSpeed = 0.015, BlueSpeed = 0.020;

        public Window(int scale) : base("Layer Test",
                                        new Point(WindowPosUndefined, WindowPosUndefined),
                                        256 * scale, 196 * scale,
                                        256, 196) {
            KeyPressed += OnKeyPressed;

            // Base background layer.
            for(var i = RenderHeight / 2 * RenderWidth; i < RenderHeight / 2 * RenderWidth + RenderWidth; i++) {
                Background.Pixels[i].A = 255;
                Background.Pixels[i].R =
                    Background.Pixels[i].G =
                        Background.Pixels[i].B = 32;
            }

            for(var i = (RenderHeight / 2 + 1) * RenderWidth; i < RenderWidth * RenderHeight; i++) {
                Background.Pixels[i].A = 255;
                Background.Pixels[i].R =
                    Background.Pixels[i].G =
                        Background.Pixels[i].B = 64;
            }

            // Add new layers.
            _redLayer = AddLayer(RenderWidth, RenderHeight, BlendMode.Additive);
            _greenLayer = AddLayer(RenderWidth, RenderHeight, BlendMode.Additive);
            _blueLayer = AddLayer(RenderWidth, RenderHeight, BlendMode.Additive);
        }

        private void DrawSlice(in Color[] pixels, int x, double phase, byte rVal, byte gVal, byte bVal) {
            var val = Math.Abs(Math.Sin((double)x / RenderWidth * Period + phase));

            var yMax = val * Amplitude;
            var yMaxInt = (int)yMax;
            var center = RenderHeight / 2;

            Func<int, int, int> pos = (xPix, yPix) => (yPix + center) * RenderWidth + xPix;

            for(var y = -yMaxInt; y < yMaxInt + 1; y++) {
                var idx = pos(x, y);
                pixels[idx].A = 255;
                pixels[idx].R = rVal;
                pixels[idx].G = gVal;
                pixels[idx].B = bVal;
            }

            // Update the alpha value on the top/bottom pixels to make it sorta look
            // antialiased. :)
            pixels[pos(x, yMaxInt)].A = pixels[pos(x, -yMaxInt)].A = (byte)(255.0 * (yMax - yMaxInt));
        }

        protected override void OnDraw() {
            // Clear the sine wave from each of the layers.
            for(var i = (RenderHeight / 2 - (int)Amplitude) * RenderWidth; i < (RenderHeight / 2 + (int)Amplitude) * RenderWidth; i++) {
                Layers[_redLayer].Pixels[i].R = 0;
                Layers[_greenLayer].Pixels[i].G = 0;
                Layers[_blueLayer].Pixels[i].B = 0;
            }

            for(var x = 0; x < RenderWidth; x++) {
                DrawSlice(Layers[_redLayer].Pixels, x, _redPhase, 255, 0, 0);
                DrawSlice(Layers[_greenLayer].Pixels, x, _greenPhase, 0, 255, 0);
                DrawSlice(Layers[_blueLayer].Pixels, x, _bluePhase, 0, 0, 255);
            }
        }

        private void OnKeyPressed(object sender, KeyboardEvent e) {
            if(e.Keycode == Keycode.Escape)
                Stop();
        }

        protected override void OnUpdate(float delta) {
            _redPhase += RedSpeed;
            _greenPhase += GreenSpeed;
            _bluePhase += BlueSpeed;
        }
    }
}
