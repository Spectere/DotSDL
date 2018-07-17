using System;
using System.Runtime.InteropServices;

namespace DotSDL.Interop.Core {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_audio.h.
    /// </summary>
    internal static class Audio {
        internal const ushort FormatMaskBitSize = 0xFF;
        internal const ushort FormatMaskDataType = 1 << 8;
        internal const ushort FormatMaskEndian = 1 << 12;
        internal const ushort FormatMaskSigned = 1 << 15;

        /// <summary>
        /// Returns the bit size of the audio format.
        /// </summary>
        /// <param name="format">The SDL audio format.</param>
        /// <returns>The bit size of the audio format.</returns>
        internal static byte BitSize(ushort format) {
            return (byte)(format & FormatMaskBitSize);
        }

        /// <summary>
        /// Determines whether the specified format is big-endian.
        /// </summary>
        /// <param name="format">The SDL audio format.</param>
        /// <returns><c>true</c> if the audio format is big-endian, otherwise <c>false</c>.</returns>
        internal static bool IsBigEndian(ushort format) {
            return (format & FormatMaskEndian) != 0;
        }

        /// <summary>
        /// Determines whether the specified format is a floating-point format.
        /// </summary>
        /// <param name="format">The SDL audio format.</param>
        /// <returns><c>true</c> if the audio format is represented by floats,
        /// otherwise <c>false</c>.</returns>
        internal static bool IsFloat(ushort format) {
            return (format & FormatMaskDataType) != 0;
        }

        /// <summary>
        /// Determines whether the specified format is a signed integral format.
        /// </summary>
        /// <param name="format">The SDL audio format.</param>
        /// <returns><c>true</c> if the audio format is a signed integral format,
        /// otherwise <c>false</c>.</returns>
        internal static bool IsSigned(ushort format) {
            return (format & FormatMaskSigned) != 0;
        }

        /// <summary>
        /// A structure that contains the audio output format. It also contains a
        /// callback that is called when the audio device needs more data.
        /// </summary>
        internal struct AudioSpec {
            /// <summary>The DSP frequency, in hertz.</summary>
            internal int Freq;

            /// <summary>The audio data format.</summary>
            internal AudioFormat Format;

            /// <summary>The number of sound channels.</summary>
            internal byte Channels;

            /// <summary>Audio buffer silence value (calculated).</summary>
            internal byte Silence;

            /// <summary>Audio buffer size in samples (power of 2).</summary>
            internal ushort Samples;

            /// <summary>Unused.</summary>
            internal ushort Padding;
            
            /// <summary>Audio buffer size in bytes (calculated).</summary>
            internal uint Size;

            /// <summary>The function to call when the audio device needs more data.</summary>
            [MarshalAs(UnmanagedType.FunctionPtr)]
            internal AudioCallback Callback;
            
            /// <summary>A pointer that is passed to the <see cref="Callback"/> function.</summary>
            internal IntPtr Userdata;
        }

        /// <summary>
        /// A function pointer representing the audio callback function.
        /// </summary>
        /// <param name="userdata">Userdata passed to the function.</param>
        /// <param name="stream">The stream to be filled in by the application.</param>
        /// <param name="len">The length of the array in <paramref name="stream"/>.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void AudioCallback(IntPtr userdata, IntPtr stream, int len);

        /// <summary>
        /// Specifies how SDL should behave when a device cannot offer a specific feature. If
        /// the application requests a feature that the hardware doesn't offer, SDL will always
        /// try to get the closest equivalent.
        /// </summary>
        [Flags]
        internal enum AllowedChanges : int {
            /// <summary>Do not allow any audio spec changes.</summary>
            DisallowChange = 0x00,

            /// <summary>Allow frequency changes.</summary>
            AllowFrequencyChange = 0x01,

            /// <summary>Allow the audio format to change.</summary>
            AllowFormatChange = 0x02,

            /// <summary>Allow the number of channels to change.</summary>
            AllowChannelsChange = 0x04,

            /// <summary>Allow any detail in the audio spec to change.</summary>
            AllowAnyChange = AllowFrequencyChange | AllowFormatChange | AllowChannelsChange
        }

        /// <summary>
        /// A list of audio formats supported by SDL.
        /// </summary>
        internal enum AudioFormat : ushort {
            /// <summary>Unsigned byte. Equivalent to AUDIO_U8.</summary>
            UnsignedByte = 0x0008,

            /// <summary>Signed byte. Equivalent to AUDIO_S8.</summary>
            SignedByte = 0x8008,

            /// <summary>Little-endian unsigned short. Equivalent to AUDIO_U16LSB.</summary>
            UnsignedShortLittleEndian = 0x0010,

            /// <summary>Little-endian signed short. Equivalent to AUDIO_S16LSB.</summary>
            SignedShortLittleEndian = 0x8010,

            /// <summary>Big-endian unsigned short. Equivalent to AUDIO_U16MSB.</summary>
            UnsignedShortBigEndian = 0x1010,

            /// <summary>Big-endian signed short. Equivalent to AUDIO_S16MSB.</summary>
            SignedShortBigEndian = 0x9010,

            /// <summary>Little-endian signed int. Equivalent to AUDIO_S32LSB.</summary>
            SignedIntLittleEndian = 0x8020,

            /// <summary>Big-endian signed int. Equivalent to AUDIO_S32MSB.</summary>
            SignedIntBigEndian = 0x9020,

            /// <summary>Little-endian floats. Equivalent to AUDIO_F32LSB.</summary>
            FloatLittleEndian = 0x8120,

            /// <summary>Big-endian floats. Equivalent to AUDIO_F32MSB.</summary>
            FloatBigEndian = 0x9120
        }

        /// <summary>
        /// The audio playback state..
        /// </summary>
        internal enum AudioState : int {
            /// <summary>Audio playback is paused.</summary>
            Unpaused = 0,

            /// <summary>Audio playback is unpaused.</summary>
            Paused = 1
        }

        /// <summary>
        /// Shuts down audio processing and closes the audio device.
        /// </summary>
        /// <param name="dev">An audio device previously opened by <see cref="OpenAudioDevice"/>.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_CloseAudioDevice", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CloseAudioDevice(uint dev);

        /// <summary>
        /// Open a specific audio device. Passing in a device name of NULL
        /// requests the most reasonable default (and is equivalent to calling
        /// SDL_OpenAudio).
        /// </summary>
        /// <param name="device">The name of the device to open, as reported
        /// SDL_GetAudioDriverName(). Some drivers allow arbitrary and
        /// driver-specific strings, such as a hostname/IP address for a remote
        /// audio server, or a filename in the diskaudio driver.</param>
        /// <param name="isCapture">Non-zero to specify a device should be opened
        /// for recording, not playback.</param>
        /// <param name="desired">An <see cref="AudioSpec"/> structure representing
        /// the desired output format.</param>
        /// <param name="obtained">An <see cref="AudioSpec"/> structure filled in
        /// with the actual output format.</param>
        /// <param name="allowedChanges"></param>
        /// <returns>A non-zero value on success or zero on failure. This will never
        /// return 1, since SDL reserves that ID for the SDL_OpenAudio() function.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_OpenAudioDevice", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint OpenAudioDevice(IntPtr device, int isCapture, ref AudioSpec desired, out AudioSpec obtained, AllowedChanges allowedChanges);

        /// <summary>
        /// Open a specific audio device. Passing in a device name of NULL
        /// requests the most reasonable default (and is equivalent to calling
        /// SDL_OpenAudio).
        /// </summary>
        /// <param name="device">The name of the device to open, as reported
        /// SDL_GetAudioDriverName(). Some drivers allow arbitrary and
        /// driver-specific strings, such as a hostname/IP address for a remote
        /// audio server, or a filename in the diskaudio driver.</param>
        /// <param name="isCapture">Non-zero to specify a device should be opened
        /// for recording, not playback.</param>
        /// <param name="desired">An <see cref="AudioSpec"/> structure representing
        /// the desired output format.</param>
        /// <param name="obtained">An <see cref="AudioSpec"/> structure filled in
        /// with the actual output format.</param>
        /// <param name="allowedChanges"></param>
        /// <returns>A non-zero value on success or zero on failure. This will never
        /// return 1, since SDL reserves that ID for the SDL_OpenAudio() function.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_OpenAudioDevice", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint OpenAudioDevice(string device, int isCapture, ref AudioSpec desired, out AudioSpec obtained, AllowedChanges allowedChanges);

        /// <summary>
        /// Pause and unpause audio callback processing.
        /// </summary>
        /// <param name="dev">The audio device to change.</param>
        /// <param name="pauseOn">The <see cref="AudioState"/> to switch to.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_PauseAudioDevice", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PauseAudioDevice(uint dev, AudioState pauseOn);
    }
}
