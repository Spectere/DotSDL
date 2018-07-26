using System;
using SdlAudio = DotSDL.Interop.Core.Audio;

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
        /// <param name="channels">The number of channels that the final mix should be converted to.</param>
        internal static void ConvertFormat(ref AudioBuffer buffer, ref byte[] stream, SdlAudio.AudioFormat format, ChannelCount channels) {
            double[] samples;

            switch(channels) {
                case ChannelCount.Mono:
                    ToMono(ref buffer, out samples);
                    break;
                case ChannelCount.Stereo:
                    ToStereo(ref buffer, out samples);
                    break;
                case ChannelCount.Quadraphonic:
                    ToQuadraphonic(ref buffer, out samples);
                    break;
                case ChannelCount.FiveOne:
                    ToFiveOne(ref buffer, out samples);
                    break;
                default:
                    throw new NotImplementedException();
            }

            switch(format) {
                case SdlAudio.AudioFormat.SignedByte:
                case SdlAudio.AudioFormat.UnsignedByte:
                    ToInt8(ref samples, ref stream);
                    break;
                case SdlAudio.AudioFormat.SignedShortLittleEndian:
                case SdlAudio.AudioFormat.UnsignedShortLittleEndian:
                    ToInt16(ref samples, ref stream, true);
                    break;
                case SdlAudio.AudioFormat.SignedShortBigEndian:
                case SdlAudio.AudioFormat.UnsignedShortBigEndian:
                    ToInt16(ref samples, ref stream, false);
                    break;
                case SdlAudio.AudioFormat.SignedIntLittleEndian:
                    ToInt32(ref samples, ref stream, true);
                    break;
                case SdlAudio.AudioFormat.SignedIntBigEndian:
                    ToInt32(ref samples, ref stream, false);
                    break;
                case SdlAudio.AudioFormat.FloatLittleEndian:
                    ToFloat32(ref samples, ref stream, true);
                    break;
                case SdlAudio.AudioFormat.FloatBigEndian:
                    ToFloat32(ref samples, ref stream, false);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts the contents of an <see cref="AudioBuffer"/> into a monophonic stream.
        /// </summary>
        /// <param name="buffer">An <see cref="AudioBuffer"/> to process.</param>
        /// <param name="samples">The double array to write the converted data to.</param>
        private static void ToMono(ref AudioBuffer buffer, out double[] samples) {
            var ch = (int)ChannelCount.Mono;
            samples = new double[buffer.Length * (int)ChannelCount.Mono];

            switch(buffer.Channels) {
                case ChannelCount.Mono:
                    samples = buffer.Samples[Channel.Mono];
                    break;
                case ChannelCount.Stereo:
                    throw new NotImplementedException();
                case ChannelCount.Quadraphonic:
                    throw new NotImplementedException();
                case ChannelCount.FiveOne:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts the contents of an <see cref="AudioBuffer"/> into a stereo stream.
        /// </summary>
        /// <param name="buffer">An <see cref="AudioBuffer"/> to process.</param>
        /// <param name="samples">The double array to write the converted data to</param>
        private static void ToStereo(ref AudioBuffer buffer, out double[] samples) {
            var ch = (int)ChannelCount.Stereo;
            samples = new double[buffer.Length * (int)ChannelCount.Stereo];

            switch(buffer.Channels) {
                case ChannelCount.Mono:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.Mono][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.Mono][i];
                    }
                    break;
                case ChannelCount.Stereo:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.StereoLeft][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.StereoRight][i];
                    }
                    break;
                case ChannelCount.Quadraphonic:
                    throw new NotImplementedException();
                case ChannelCount.FiveOne:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts the contents of an <see cref="AudioBuffer"/> into a quadrophonic stream.
        /// </summary>
        /// <param name="buffer">An <see cref="AudioBuffer"/> to process.</param>
        /// <param name="samples">The double array to write the converted data to.</param>
        private static void ToQuadraphonic(ref AudioBuffer buffer, out double[] samples) {
            var ch = (int)ChannelCount.Quadraphonic;
            samples = new double[buffer.Length * (int)ChannelCount.Quadraphonic];

            switch(buffer.Channels) {
                case ChannelCount.Mono:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.Mono][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.Mono][i];
                        samples[i * ch + 2] = buffer.Samples[Channel.Mono][i];
                        samples[i * ch + 3] = buffer.Samples[Channel.Mono][i];
                    }
                    break;
                case ChannelCount.Stereo:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.StereoLeft][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.StereoRight][i];
                        samples[i * ch + 2] = buffer.Samples[Channel.StereoLeft][i];
                        samples[i * ch + 3] = buffer.Samples[Channel.StereoRight][i];
                    }
                    break;
                case ChannelCount.Quadraphonic:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.QuadFrontLeft][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.QuadFrontRight][i];
                        samples[i * ch + 2] = buffer.Samples[Channel.QuadRearLeft][i];
                        samples[i * ch + 3] = buffer.Samples[Channel.QuadRearRight][i];
                    }
                    break;
                case ChannelCount.FiveOne:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts the contents of an <see cref="AudioBuffer"/> into a 5.1 stream.
        /// </summary>
        /// <param name="buffer">An <see cref="AudioBuffer"/> to process.</param>
        /// <param name="samples">The double array to write the converted data to.</param>
        private static void ToFiveOne(ref AudioBuffer buffer, out double[] samples) {
            var ch = (int)ChannelCount.FiveOne;
            samples = new double[buffer.Length * (int)ChannelCount.FiveOne];

            // TODO: Improve upmixing.
            switch(buffer.Channels) {
                case ChannelCount.Mono:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.Mono][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.Mono][i];
                        samples[i * ch + 2] = 0;  // Center channel.
                        samples[i * ch + 3] = 0;  // LFE.
                        samples[i * ch + 4] = buffer.Samples[Channel.Mono][i];
                        samples[i * ch + 5] = buffer.Samples[Channel.Mono][i];
                    }
                    break;
                case ChannelCount.Stereo:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.StereoLeft][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.StereoRight][i];
                        samples[i * ch + 2] = 0;  // Center channel.
                        samples[i * ch + 3] = 0;  // LFE.
                        samples[i * ch + 4] = buffer.Samples[Channel.StereoLeft][i];
                        samples[i * ch + 5] = buffer.Samples[Channel.StereoRight][i];
                    }
                    break;
                case ChannelCount.Quadraphonic:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.QuadFrontLeft][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.QuadFrontRight][i];
                        samples[i * ch + 2] = 0;  // Center channel.
                        samples[i * ch + 3] = 0;  // LFE.
                        samples[i * ch + 4] = buffer.Samples[Channel.QuadRearLeft][i];
                        samples[i * ch + 5] = buffer.Samples[Channel.QuadRearRight][i];
                    }
                    break;
                case ChannelCount.FiveOne:
                    for(var i = 0; i < buffer.Length; i++) {
                        samples[i * ch] = buffer.Samples[Channel.FiveOneFrontLeft][i];
                        samples[i * ch + 1] = buffer.Samples[Channel.FiveOneFrontRight][i];
                        samples[i * ch + 2] = buffer.Samples[Channel.FiveOneCenter][i];
                        samples[i * ch + 3] = buffer.Samples[Channel.FiveOneLfe][i];
                        samples[i * ch + 4] = buffer.Samples[Channel.FiveOneRearLeft][i];
                        samples[i * ch + 5] = buffer.Samples[Channel.FiveOneRearRight][i];
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with 8-bit samples.
        /// </summary>
        /// <param name="samples">An array of double-precision floating point samples.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        private static void ToInt8(ref double[] samples, ref byte[] stream) {
            for(var i = 0; i < samples.Length; i++) {
                var sample = samples[i];
                var newSample = (sbyte)(sample * sbyte.MaxValue);
                stream[i] = (byte)newSample;
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with 16-bit samples.
        /// </summary>
        /// <param name="samples">An array of double-precision floating point samples.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        /// <param name="littleEndian">Indicates whether the target byte stream should be little endian.</param>
        private static void ToInt16(ref double[] samples, ref byte[] stream, bool littleEndian) {
            if((BitConverter.IsLittleEndian && littleEndian) || (!BitConverter.IsLittleEndian && !littleEndian)) {
                // Sample endian == system endian.
                for(var i = 0; i < samples.Length; i++) {
                    var sample = samples[i];
                    var newSample = (short)(sample * short.MaxValue);
                    stream[i * 2] = (byte)newSample;
                    stream[i * 2 + 1] = (byte)(newSample >> 8);
                }
            } else {
                // Sample endian != system endian. Flip bytes.
                for(var i = 0; i < samples.Length; i++) {
                    var sample = samples[i];
                    var newSample = (short)(sample * short.MaxValue);
                    stream[i * 2] = (byte)(newSample >> 8);
                    stream[i * 2 + 1] = (byte)newSample;
                }
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with 32-bit integer samples.
        /// </summary>
        /// <param name="samples">An array of double-precision floating point samples.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        /// <param name="littleEndian">Indicates whether the target byte stream should be little endian.</param>
        private static void ToInt32(ref double[] samples, ref byte[] stream, bool littleEndian) {
            if((BitConverter.IsLittleEndian && littleEndian) || (!BitConverter.IsLittleEndian && !littleEndian)) {
                // Sample endian == system endian.
                for(var i = 0; i < samples.Length; i++) {
                    var sample = samples[i];
                    var newSample = (int)(sample * int.MaxValue);
                    stream[i * 4] = (byte)newSample;
                    stream[i * 4 + 1] = (byte)(newSample >> 8);
                    stream[i * 4 + 2] = (byte)(newSample >> 16);
                    stream[i * 4 + 3] = (byte)(newSample >> 24);
                }
            } else {
                // Sample endian != system endian. Flip bytes.
                for(var i = 0; i < samples.Length; i++) {
                    var sample = samples[i];
                    var newSample = (int)(sample * int.MaxValue);
                    stream[i * 4] = (byte)(newSample >> 24);
                    stream[i * 4 + 1] = (byte)(newSample >> 16);
                    stream[i * 4 + 2] = (byte)(newSample >> 8);
                    stream[i * 4 + 3] = (byte)newSample;
                }
            }
        }

        /// <summary>
        /// Converts an audio buffer to a byte array with little-endian floating point samples.
        /// </summary>
        /// <param name="samples">An array of double-precision floating point samples.</param>
        /// <param name="stream">The byte array to write the converted data to.</param>
        /// <param name="littleEndian">Indicates whether the target byte stream should be little endian.</param>
        private static void ToFloat32(ref double[] samples, ref byte[] stream, bool littleEndian) {
            if((BitConverter.IsLittleEndian && littleEndian) || (!BitConverter.IsLittleEndian && !littleEndian)) {
                // Sample endian == system endian.
                for(var i = 0; i < samples.Length; i++) {
                    var sample = (float)samples[i];
                    var newSample = BitConverter.GetBytes(sample);
                    stream[i * 4] = newSample[0];
                    stream[i * 4 + 1] = newSample[1];
                    stream[i * 4 + 2] = newSample[2];
                    stream[i * 4 + 3] = newSample[3];
                }
            } else {
                // Sample endian != system endian. Flip bytes.
                for(var i = 0; i < samples.Length; i++) {
                    var sample = (float)samples[i];
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
