namespace DotSDL.Input.Mouse {
    /// <summary>
    /// The direction of the mouse wheel.
    /// </summary>
    public enum WheelDirection : uint {
        /// <summary>
        /// The mouse wheel is normal (up scrolls up, down scrolls down).
        /// </summary>
        Normal,

        /// <summary>
        /// The mouse wheel is flipped (down scrolls up, up scrolls down).
        /// </summary>
        Flipped
    }
}
