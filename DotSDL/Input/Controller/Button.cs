namespace DotSDL.Input.Controller {
    /// <summary>
    /// The buttons that are supported by SDL2's game controller subsystem.
    /// </summary>
    public enum Button : sbyte {
        /// <summary>
        /// Invalid button.
        /// </summary>
        Invalid = -1,

        /// <summary>
        /// The controller's A button.
        /// </summary>
        A,

        /// <summary>
        /// The controller's B button.
        /// </summary>
        B,

        /// <summary>
        /// The controller's X button.
        /// </summary>
        X,

        /// <summary>
        /// The controller's Y button.
        /// </summary>
        Y,

        /// <summary>
        /// The controller's back button (View on the Xbox One controller, Share on the DualShock 4).
        /// </summary>
        Back,
        
        /// <summary>
        /// The controller's guide button (usually the Xbox or PlayStation logos).
        /// </summary>
        Guide,

        /// <summary>
        /// The controller's start button (Menu on the Xbox One controller, Options on the DualShock 4).
        /// </summary>
        Start,

        /// <summary>
        /// The left stick button, also known as L3.
        /// </summary>
        LeftStick,

        /// <summary>
        /// The right stick button, also known as R3.
        /// </summary>
        RightStick,

        /// <summary>
        /// The left shoulder button, also known as LB.
        /// </summary>
        LeftShoulder,

        /// <summary>
        /// The right shoulder button, also known as RB.
        /// </summary>
        RightShoulder,

        /// <summary>
        /// Up on the directional pad.
        /// </summary>
        Up,

        /// <summary>
        /// Down on the direction pad.
        /// </summary>
        Down,

        /// <summary>
        /// Left on the directional pad.
        /// </summary>
        Left,

        /// <summary>
        /// Right on the directional pad.
        /// </summary>
        Right
    }
}
