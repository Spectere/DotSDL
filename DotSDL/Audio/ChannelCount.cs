namespace DotSDL.Audio {
    /// <summary>
    /// Describes the number of channels that a sound source supports.
    /// </summary>
    public enum ChannelCount {
        /// <summary>
        /// Monoaural audio.
        /// </summary>
        Mono = 1,

        /// <summary>
        /// Stereo audio.
        /// </summary>
        Stereo = 2,

        /// <summary>
        /// Quadraphonic audio.
        /// </summary>
        Quadraphonic = 4,

        /// <summary>
        /// 5.1 audio.
        /// </summary>
        FiveOne = 6
    }
}
