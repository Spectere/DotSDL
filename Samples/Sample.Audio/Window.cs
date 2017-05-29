using DotSDL.Graphics;
using DotSDL.Input.Keyboard;

namespace Sample.Audio {
    internal class Window : SdlWindow {
        private bool UpPressed { get; set; }
        private bool DownPressed { get; set; }
        private bool Fast { get; set; }

        private int _freq = 440;
        private int _minFreq = 1;
        private int _maxFreq = 22050;

        private const int DrawX = 1;
        private const int SpacingX = 5;
        private const int DrawY = 1;

        private const byte OnDelta = 24;
        private const byte OffColor = 72;
        private Color TextColor = new Color { A = 255, R = 159, G = 159, B = 159 };

        public Window(int width, int height) : base("DotSDL Audio Example",
                                                    new Point{ X = WindowPosUndefined, Y = WindowPosUndefined },
                                                    width, height,
                                                    36, 9) {
            KeyPressed += Window_KeyPressed;
            KeyReleased += Window_KeyReleased;
        }

        private void DrawGlyph(ref Canvas canvas, char ch, int xPos, Color c) {
            var xPixel = DrawX + SpacingX * xPos;
            var glyph = Font.Glyph[ch];

            for(var y = 0; y < Font.Height; y++) {
                for(var x = 0; x < Font.Width; x++) {
                    if(!glyph[y, x]) continue;
                    var index = canvas.GetIndex(x + xPixel, y + DrawY);

                    canvas.Pixels[index] = TextColor;
                }
            }
        }

        protected override void OnDraw(ref Canvas canvas) {
            // Clear canvas.
            for(var i = 0; i < canvas.Pixels.Length; i++)
                canvas.Pixels[i].R = canvas.Pixels[i].G = canvas.Pixels[i].B = 0;

            // hz Text
            DrawGlyph(ref canvas, 'h', 5, TextColor);
            DrawGlyph(ref canvas, 'z', 6, TextColor);

            // Number
            var freqText = _freq.ToString();
            var textPos = 5 - freqText.Length;
            for(int x = 0; x < freqText.Length; x++)
                DrawGlyph(ref canvas, freqText[x], textPos + x, TextColor);

            // Background/highlighting.
            var component = 0;

            for(var i = 0; i < canvas.Pixels.Length; i++) {
                canvas.Pixels[i].R += OffColor;
                canvas.Pixels[i].G += OffColor;
                canvas.Pixels[i].B += OffColor;
                switch(component) {
                    default:
                        canvas.Pixels[i].R += OnDelta;
                        break;
                    case 1:
                        canvas.Pixels[i].G += OnDelta;
                        break;
                    case 2:
                        canvas.Pixels[i].B += OnDelta;
                        break;
                }
                component = component >= 2 ? 0 : ++component;
            }


            base.OnDraw(ref canvas);
        }

        protected override void OnUpdate() {
            var delta = Fast ? 10 : 1;
            if(UpPressed)
                _freq += delta;
            if(DownPressed)
                _freq -= delta;

            if(_freq > _maxFreq) _freq = _maxFreq;
            if(_freq < _minFreq) _freq = _minFreq;

            base.OnUpdate();
        }

        private void Window_KeyPressed(object sender, DotSDL.Events.KeyboardEvent e) {
            if(e.Repeat) return;

            switch(e.Keycode) {
                case Keycode.Up:
                    UpPressed = true;
                    break;
                case Keycode.Down:
                    DownPressed = true;
                    break;
                case Keycode.LShift:
                case Keycode.RShift:
                    Fast = true;
                    break;
                case Keycode.Escape:
                    Stop();
                    break;
            }
        }

        private void Window_KeyReleased(object sender, DotSDL.Events.KeyboardEvent e) {
            switch(e.Keycode) {
                case Keycode.Up:
                    UpPressed = false;
                    break;
                case Keycode.Down:
                    DownPressed = false;
                    break;
                case Keycode.LShift:
                case Keycode.RShift:
                    Fast = false;
                    break;
            }
        }
    }
}
