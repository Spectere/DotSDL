using System;
using SdlAudio = DotSDL.Sdl.Audio;

namespace DotSDL.Audio {
    internal static class FormatConverter {
        /// <summary>
        /// Converts a DotSDL <see cref="AudioBuffer"/> to an SDL-compatible byte array.
        /// </summary>
        /// <param name="buffer">A DotSDL <see cref="AudioBuffer"/>.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        /// <param name="format">The SDL <see cref="AudioFormat"/> to convert to.</param>
        internal static void ConvertFormat(AudioBuffer buffer, ref byte[] stream, SdlAudio.AudioFormat format) {
            switch(format) {
                case SdlAudio.AudioFormat.SignedShortLittleEndian:
                    ToSint16Lsb(buffer, ref stream);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with little-endian signed shorts.
        /// </summary>
        /// <param name="buffer">A DotSDL <see cref="AudioBuffer"/>.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        private static void ToSint16Lsb(AudioBuffer buffer, ref byte[] stream) {
            if(BitConverter.IsLittleEndian) {
                // Little-endian architecture.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = buffer.Samples[i];
                    var newSample = (short)(sample * short.MaxValue);
                    stream[i * 2 + 1] = (byte)(newSample >> 8);
                    stream[i * 2] = (byte)newSample;
                }
            } else {
                // Big-endian architecture.
                for(var i = 0; i < buffer.Samples.Length; i++) {
                    var sample = buffer.Samples[i];
                    var newSample = (short)(sample * short.MaxValue);
                    stream[i * 2] = (byte)newSample;
                    stream[i * 2 + 1] = (byte)(newSample >> 8);
                }
            }
        }
    }
}
