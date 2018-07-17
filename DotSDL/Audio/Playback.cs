using SdlAudio = DotSDL.Interop.Core.Audio;
using System;
using System.Runtime.InteropServices;
using DotSDL.Interop.Core;

namespace DotSDL.Audio {
    /// <summary>
    /// Represents a streaming audio playback engine.
    /// </summary>
    public class Playback {
        private const ushort DefaultBufferSize = 4096;

        private readonly SdlInit _sdlInit = SdlInit.Instance;
        private readonly uint _deviceId;

        /// <summary>
        /// The SDL <see cref="SdlAudio.AudioSpec"/> that is currently active.
        /// </summary>
        private SdlAudio.AudioSpec _sdlAudioSpec;

        /// <summary>
        /// The frequency for the open audio device, in hertz.
        /// </summary>
        public int Frequency { get; }

        /// <summary>
        /// The bit size for the open audio device.
        /// </summary>
        public byte BitSize { get; }

        /// <summary>
        /// The number of sound channels for the open audio device.
        /// </summary>
        public byte Channels { get; }

        /// <summary>
        /// The buffer size for the open audio device, in bytes.
        /// </summary>
        public uint BufferSizeBytes { get; }

        /// <summary>
        /// The buffer size for the open audio device, in samples.
        /// </summary>
        public ushort BufferSizeSamples { get; }

        /// <summary>
        /// <c>true</c> if the audio format is floating-point, otherwise <c>false</c>.
        /// </summary>
        public bool FloatingPoint { get; }

        /// <summary>
        /// <c>true</c> if the audio format is little-endian, otherwise <c>false</c>.
        /// </summary>
        public bool LittleEndian { get; }

        /// <summary>
        /// Fired when the audio device is requesting more data.
        /// </summary>
        public event EventHandler<AudioBuffer> BufferEmpty;

        /// <summary>
        /// Initializes a new instance of the audio engine.
        /// </summary>
        /// <param name="freqency">The desired frequency, in hertz.</param>
        /// <param name="format">The desired audio format.</param>
        /// <param name="channels">The desired number of channels.</param>
        public Playback(int freqency, AudioFormat format, byte channels)
            : this(freqency, format, channels, DefaultBufferSize) { }

        /// <summary>
        /// Initializes a new instance of the audio engine.
        /// </summary>
        /// <param name="freqency">The desired frequency, in hertz.</param>
        /// <param name="format">The desired audio format.</param>
        /// <param name="channels">The desired number of channels.</param>
        /// <param name="buffer">The desired buffer size, in samples.</param>
        public Playback(int freqency, AudioFormat format, byte channels, ushort buffer) {
            _sdlInit.InitSubsystem(Init.SubsystemFlags.Audio);

            SdlAudio.AudioSpec actual;
            var desired = new SdlAudio.AudioSpec {
                Freq = freqency,
                Format = GetBestAudioFormat(format),
                Channels = channels,
                Silence = 0,
                Samples = buffer,
                Padding = 0,
                Size = 0,
                Callback = Callback,
                Userdata = IntPtr.Zero
            };

            _deviceId = SdlAudio.OpenAudioDevice(IntPtr.Zero, 0, ref desired, out actual, SdlAudio.AllowedChanges.AllowAnyChange);

            // Populate the obtained values in the object properties.
            Frequency = actual.Freq;
            Channels = actual.Channels;
            BufferSizeSamples = actual.Samples;
            BufferSizeBytes = actual.Size;
            BitSize = SdlAudio.BitSize((ushort)actual.Format);
            FloatingPoint = SdlAudio.IsFloat((ushort)actual.Format);
            LittleEndian = !SdlAudio.IsBigEndian((ushort)actual.Format);

            _sdlAudioSpec = actual;
        }

        /// <summary>
        /// Called when the object is about to be destroyed.
        /// </summary>
        ~Playback() {
            Close();
        }

        /// <summary>
        /// The SDL audio callback function.
        /// </summary>
        /// <param name="userdata">The user data passed in from the audio spec.</param>
        /// <param name="stream">The stream to be filled in.</param>
        /// <param name="len">The length of the stream array, in bytes.</param>
        private void Callback(IntPtr userdata, IntPtr stream, int len) {
            if(BufferEmpty == null) return;

            var buffer = new AudioBuffer { Samples = new double[BufferSizeSamples] };
            BufferEmpty(this, buffer);

            var newStream = new byte[len];
            FormatConverter.ConvertFormat(buffer, ref newStream, _sdlAudioSpec.Format);

            Marshal.Copy(newStream, 0, stream, len);
        }

        /// <summary>
        /// Closes the audio engine.
        /// </summary>
        public void Close() {
            Pause();
            SdlAudio.CloseAudioDevice(_deviceId);
        }

        /// <summary>
        /// Stops processing audio.
        /// </summary>
        public void Pause() {
            SdlAudio.PauseAudioDevice(_deviceId, SdlAudio.AudioState.Paused);
        }

        /// <summary>
        /// Starts processing audio. This must be called after the device is
        /// initialized for callback processing to start occurring.
        /// </summary>
        public void Play() {
            SdlAudio.PauseAudioDevice(_deviceId, SdlAudio.AudioState.Unpaused);
        }

        /// <summary>
        /// Retrieves the best SDL audio format to suit the desired DotSDL format.
        /// </summary>
        /// <param name="format">The DotSDL <see cref="AudioFormat"/> to use.</param>
        /// <returns>An SDL <see cref="SdlAudio.AudioFormat"/> that best fits the value
        /// passed in <paramref name="format"/>.</returns>
        private static SdlAudio.AudioFormat GetBestAudioFormat(AudioFormat format) {
            var littleEndian = BitConverter.IsLittleEndian;

            switch(format) {
                case AudioFormat.Integer8:
                    return SdlAudio.AudioFormat.SignedByte;
                case AudioFormat.Integer32:
                    return littleEndian
                               ? SdlAudio.AudioFormat.SignedIntLittleEndian
                               : SdlAudio.AudioFormat.SignedIntBigEndian;
                case AudioFormat.Float32:
                    return littleEndian
                               ? SdlAudio.AudioFormat.FloatLittleEndian
                               : SdlAudio.AudioFormat.FloatBigEndian;
                default:
                    return littleEndian
                               ? SdlAudio.AudioFormat.SignedShortLittleEndian
                               : SdlAudio.AudioFormat.SignedShortBigEndian;
            }
        }
    }
}
