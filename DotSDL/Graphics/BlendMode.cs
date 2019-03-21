namespace DotSDL.Graphics {
    /// <summary>
    /// The blend mode that should be used when plotting an texture.
    /// </summary>
    /// <!-- This enum must match SDL_BlendMode. -->
    public enum BlendMode {
        /// <summary>
        /// No blending is performed. The source will be copied directly to the
        /// destination.
        /// </summary>
        None,

        /// <summary>
        /// Alpha blending will be performed. This uses the alpha channel of the
        /// texture to control the texture's visibility when the copy operation
        /// is performed.
        /// </summary>
        Alpha,

        /// <summary>
        /// Additive blending will be performed. The source's RGB value, modified
        /// by the alpha channel, will be added to that of that destination
        /// </summary>
        Additive
    }
}
