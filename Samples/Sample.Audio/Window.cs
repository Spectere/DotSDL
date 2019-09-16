using DotSDL.Audio;
using DotSDL.Graphics;
using DotSDL.Input.Keyboard;
using System;

namespace Sample.Audio {
    internal class Window : SdlWindow {
        private bool UpPressed { get; set; }
        private bool DownPressed { get; set; }
        private bool Fast { get; set; }

        private int _freq = 440;
        private int _minFreq = 1;
        private readonly int _maxFreq;

        private const int DrawX = 1;
        private const int SpacingX = 5;
        private const int DrawY = 1;

        private const byte OnDelta = 24;
        private const byte OffColor = 72;
        private readonly Color _textColor = new Color { A = 255, R = 159, G = 159, B = 159 };

        private readonly Playback _audio;
        private ulong _time;
        private readonly int _audioFreq;

        public Window(int width, int height) : base("DotSDL Audio Example",
                                                    new Point{ X = WindowPosUndefined, Y = WindowPosUndefined },
                                                    width, height,
                                                    36, 9) {
            KeyPressed += Window_KeyPressed;
            KeyReleased += Window_KeyReleased;

            _audio = new Playback(44100, AudioFormat.Integer16, ChannelCount.Mono);
            _audioFreq = _audio.Frequency;

            var floatingPointText = _audio.FloatingPoint ? "floating-point, " : "";
            var endianText = _audio.LittleEndian ? "little-endian" : "big-endian";
            Console.WriteLine($"Audio opened: {_audioFreq}hz, {_audio.BitSize}-bit ({floatingPointText}{endianText}), {_audio.Channels} channels, {_audio.BufferSizeSamples} sample buffer, {_audio.BufferSizeBytes} byte buffer.");
            _audio.BufferEmpty += Audio_BufferEmpty;

            _maxFreq = _audioFreq / 2;
            _audio.Play();
        }

        ~Window() {
            _audio.Close();
        }

        private void Audio_BufferEmpty(object sender, AudioBuffer e) {
            var t = (Math.PI * 2.0 * _freq) / _audioFreq;
            for(var i = 0; i < e.Length; i++)
                e.Samples[Channel.Mono][i] = Math.Sin(_time++ * t);
        }

        private void DrawGlyph(Canvas canvas, char ch, int xPos, Color c) {
            var xPixel = DrawX + SpacingX * xPos;
            var glyph = Font.Glyph[ch];

            for(var y = 0; y < Font.Height; y++) {
                for(var x = 0; x < Font.Width; x++) {
                    if(!glyph[y, x]) continue;
                    var index = canvas.GetIndex(x + xPixel, y + DrawY);

                    canvas.Pixels[index] = c;
                }
            }
        }

        protected override void OnDraw() {
            // Clear canvas.
            for(var i = 0; i < Background.Pixels.Length; i++)
                Background.Pixels[i].R = Background.Pixels[i].G = Background.Pixels[i].B = 0;

            // hz Text
            DrawGlyph(Background, 'h', 5, _textColor);
            DrawGlyph(Background, 'z', 6, _textColor);

            // Number
            var freqText = _freq.ToString();
            var textPos = 5 - freqText.Length;
            for(var x = 0; x < freqText.Length; x++)
                DrawGlyph(Background, freqText[x], textPos + x, _textColor);

            // Background/highlighting.
            var component = 0;

            for(var i = 0; i < Background.Pixels.Length; i++) {
                Background.Pixels[i].R += OffColor;
                Background.Pixels[i].G += OffColor;
                Background.Pixels[i].B += OffColor;
                switch(component) {
                    default:
                        Background.Pixels[i].R += OnDelta;
                        break;
                    case 1:
                        Background.Pixels[i].G += OnDelta;
                        break;
                    case 2:
                        Background.Pixels[i].B += OnDelta;
                        break;
                }
                component = component >= 2 ? 0 : ++component;
            }

            base.OnDraw();
        }

        protected override void OnUpdate(float _) {
            var delta = Fast ? 10 : 1;
            if(UpPressed)
                _freq += delta;
            if(DownPressed)
                _freq -= delta;

            if(_freq > _maxFreq) _freq = _maxFreq;
            if(_freq < _minFreq) _freq = _minFreq;

            base.OnUpdate(delta);
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
