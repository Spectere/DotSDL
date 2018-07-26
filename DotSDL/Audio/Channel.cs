namespace DotSDL.Audio {
    /// <summary>
    /// Describes an individual audio channel. This is typically used to target
    /// a specific speaker when populating an <see cref="AudioBuffer"/>.
    /// </summary>
    public static class Channel {
        /********
         * Mono *
         ********/
        /// <summary>
        /// Describes the single channel in a monoaural configuration.
        /// </summary>
        public static int Mono = 0;

        /**********
         * Stereo *
         **********/
        /// <summary>
        /// Describes the left channel in a stereo configuration.
        /// </summary>
        public static int StereoLeft = 0;

        /// <summary>
        /// Describes the right channel in a stereo configuration.
        /// </summary>
        public static int StereoRight = 1;

        /****************
         * Quadraphonic *
         ****************/
        /// <summary>
        /// Describes the front-left channel in a quadraphonic configuration.
        /// </summary>
        public static int QuadFrontLeft = 0;

        /// <summary>
        /// Describes the front-right channel in a quadraphonic configuration.
        /// </summary>
        public static int QuadFrontRight = 1;

        /// <summary>
        /// Describes the rear-left channel in a quadraphonic configuration.
        /// </summary>
        public static int QuadRearLeft = 2;

        /// <summary>
        /// Describes the rear-right channel in a quadraphonic configuration.
        /// </summary>
        public static int QuadRearRight = 3;

        /*******
         * 5.1 *
         *******/
        /// <summary>
        /// Describes the front-left channel in a 5.1 configuration.
        /// </summary>
        public static int FiveOneFrontLeft = 0;

        /// <summary>
        /// Describes the front-right channel in a 5.1 configuration.
        /// </summary>
        public static int FiveOneFrontRight = 1;

        /// <summary>
        /// Describes the center channel in a 5.1 configuration.
        /// </summary>
        public static int FiveOneCenter = 2;

        /// <summary>
        /// Describes the subwoofer channel in a 5.1 configuration.
        /// </summary>
        public static int FiveOneLfe = 3;

        /// <summary>
        /// Describes the rear-left channel in a 5.1 configuration.
        /// </summary>
        public static int FiveOneRearLeft = 4;

        /// <summary>
        /// Describes the rear-right channel in a 5.1 configuration.
        /// </summary>
        public static int FiveOneRearRight = 5;
    }
}
