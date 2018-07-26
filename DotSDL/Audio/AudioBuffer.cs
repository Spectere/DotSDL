namespace DotSDL.Audio {
    /// <summary>
    /// Represents an audio buffer.
    /// </summary>
    public class AudioBuffer {
        private ChannelCount _channels = ChannelCount.Stereo;
        private int _bufferLength = 0;

        /// <summary>
        /// The samples in the audio buffer.
        /// </summary>
        public double[][] Samples;

        /// <summary>
        /// The number of audio channels that are contained in this buffer.
        /// </summary>
        public ChannelCount Channels {
            get => _channels;
            set {
                _channels = value;
                InitializeBuffer();
            }
        }

        /// <summary>
        /// Returns the number of samples represented by this <see cref="AudioBuffer"/>.
        /// </summary>
        public int Length {
            get => _bufferLength;
            set {
                _bufferLength = value;
                InitializeBuffer();
            }
        }

        /// <summary>
        /// Initializes the buffer. This should be called every time the buffer is resized
        /// or the number of channels changes. This will clear the buffer.
        /// </summary>
        private void InitializeBuffer() {
            Samples = new double[(int)_channels][];
            for(var i = 0; i < (int)_channels; i++)
                Samples[i] = new double[_bufferLength];
        }
    }
}
