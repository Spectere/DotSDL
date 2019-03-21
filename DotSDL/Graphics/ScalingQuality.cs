namespace DotSDL.Graphics {
    /// <summary>
    /// The scaling (filtering) style that should be used for the application or a texture.
    /// </summary>
    /// <!-- This enum must match the values that are valid for SDL_HINT_RENDER_SCALE_QUALITY. -->
    public enum ScalingQuality {
        /// <summary>
        /// Nearest pixel sampling. This should be used if crisp, square pixels are desired.
        /// </summary>
        Nearest = 0,

        /// <summary>
        /// Linear filtering. This will filter the surrounding pixels together, resulting
        /// in a blurrier look.
        /// </summary>
        Linear = 1
    }
}
