namespace DotSDL.Input.Controller {
    public enum Axis : sbyte {
        Invalid = -1,

        /// <summary>
        /// The X coordinate of the left stick.
        /// </summary>
        LeftX,

        /// <summary>
        /// The Y coordinate of the left stick.
        /// </summary>
        LeftY,

        /// <summary>
        /// The X coordinate of the right stick.
        /// </summary>
        RightX,

        /// <summary>
        /// The Y coordinate of the right stick.
        /// </summary>
        RightY,

        /// <summary>
        /// The left trigger.
        /// </summary>
        TriggerLeft,

        /// <summary>
        /// The right trigger.
        /// </summary>
        TriggerRight,
    }
}
