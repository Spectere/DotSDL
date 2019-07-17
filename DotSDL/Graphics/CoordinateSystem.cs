namespace DotSDL.Graphics {
    public enum CoordinateSystem {
        /// <summary>
        /// Uses screen space coordinates. This directly maps to pixels in the window.
        /// </summary>
        ScreenSpace,

        /// <summary>
        /// Uses world space coordinates. This maps to a location based on the position of
        /// the camera, or view.
        /// </summary>
        WorldSpace
    }
}
