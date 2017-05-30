using System;
using SdlAudio = DotSDL.Sdl.Audio;

namespace DotSDL.Audio {
    /// <summary>
    /// Converts between the audio formats supported by SDL.
    /// </summary>
    internal static class FormatConverter {
        /// <summary>
        /// Converts a DotSDL <see cref="AudioBuffer"/> to an SDL-compatible byte array.
        /// </summary>
        /// <param name="buffer">A DotSDL <see cref="AudioBuffer"/>.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        /// <param name="format">The SDL <see cref="AudioFormat"/> to convert to.</param>
        internal static void ConvertFormat(AudioBuffer buffer, ref byte[] stream, SdlAudio.AudioFormat format) {
            switch(format) {
                case SdlAudio.AudioFormat.SignedByte:
                case SdlAudio.AudioFormat.UnsignedByte:
                    ToInt8(buffer, ref stream);
                    break;
                case SdlAudio.AudioFormat.SignedShortLittleEndian:
                case SdlAudio.AudioFormat.UnsignedShortLittleEndian:
                    ToInt16(buffer, ref stream, true);
                    break;
                case SdlAudio.AudioFormat.SignedShortBigEndian:
                case SdlAudio.AudioFormat.UnsignedShortBigEndian:
                    ToInt16(buffer, ref stream, false);
                    break;
                case SdlAudio.AudioFormat.SignedIntLittleEndian:
                    ToInt32(buffer, ref stream, true);
                    break;
                case SdlAudio.AudioFormat.SignedIntBigEndian:
                    ToInt32(buffer, ref stream, false);
                    break;
                case SdlAudio.AudioFormat.FloatLittleEndian:
                    ToFloat32(buffer, ref stream, true);
                    break;
                case SdlAudio.AudioFormat.FloatBigEndian:
                    ToFloat32(buffer, ref stream, false);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with 8-bit samples.
        /// </summary>
        /// <param name="buffer">A DotSDL <see cref="AudioBuffer"/>.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        private static void ToInt8(AudioBuffer buffer, ref byte[] stream) {
            for(var i = 0; i < buffer.Samples.Length; i++) {
                var sample = buffer.Samples[i];
                var newSample = (sbyte)(sample * sbyte.MaxValue);
                stream[i] = (byte)newSample;
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with 16-bit samples.
        /// </summary>
        /// <param name="buffer">A DotSDL <see cref="AudioBuffer"/>.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        /// <param name="littleEndian">Indicates whether the target byte stream should be little endian.</param>
        private static void ToInt16(AudioBuffer buffer, ref byte[] stream, bool littleEndian) {
            if((BitConverter.IsLittleEndian && littleEndian) || (!BitConverter.IsLittleEndian && !littleEndian)) {
                // Sample endian == system endian.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = buffer.Samples[i];
                    var newSample = (short)(sample * short.MaxValue);
                    stream[i * 2] = (byte)newSample;
                    stream[i * 2 + 1] = (byte)(newSample >> 8);
                }
            } else {
                // Sample endian != system endian. Flip bytes.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = buffer.Samples[i];
                    var newSample = (short)(sample * short.MaxValue);
                    stream[i * 2] = (byte)(newSample >> 8);
                    stream[i * 2 + 1] = (byte)newSample;
                }
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with little-endian 32-bit samples.
        /// </summary>
        /// <param name="buffer">A DotSDL <see cref="AudioBuffer"/>.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        /// <param name="littleEndian">Indicates whether the target byte stream should be little endian.</param>
        private static void ToInt32(AudioBuffer buffer, ref byte[] stream, bool littleEndian) {
            if((BitConverter.IsLittleEndian && littleEndian) || (!BitConverter.IsLittleEndian && !littleEndian)) {
                // Sample endian == system endian.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = buffer.Samples[i];
                    var newSample = (int)(sample * int.MaxValue);
                    stream[i * 4] = (byte)newSample;
                    stream[i * 4 + 1] = (byte)(newSample >> 8);
                    stream[i * 4 + 2] = (byte)(newSample >> 16);
                    stream[i * 4 + 3] = (byte)(newSample >> 24);
                }
            } else {
                // Sample endian != system endian. Flip bytes.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = buffer.Samples[i];
                    var newSample = (int)(sample * int.MaxValue);
                    stream[i * 4] = (byte)(newSample >> 24);
                    stream[i * 4 + 1] = (byte)(newSample >> 16);
                    stream[i * 4 + 2] = (byte)(newSample >> 8);
                    stream[i * 4 + 3] = (byte)newSample;
                }
            }
        }

        private static void ToFloat32(AudioBuffer buffer, ref byte[] stream, bool littleEndian) {
            if((BitConverter.IsLittleEndian && littleEndian) || (!BitConverter.IsLittleEndian && !littleEndian)) {
                // Sample endian == system endian.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = (float)buffer.Samples[i];
                    var newSample = BitConverter.GetBytes(sample);
                    stream[i * 4] = newSample[0];
                    stream[i * 4 + 1] = newSample[1];
                    stream[i * 4 + 2] = newSample[2];
                    stream[i * 4 + 3] = newSample[3];
                }
            } else {
                // Sample endian != system endian. Flip bytes.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = (float)buffer.Samples[i];
                    var newSample = BitConverter.GetBytes(sample);
                    stream[i * 4] = newSample[3];
                    stream[i * 4 + 1] = newSample[2];
                    stream[i * 4 + 2] = newSample[1];
                    stream[i * 4 + 3] = newSample[0];
                }
            }
        }
    }
}
