namespace DotSDL.Input.Controller {
    /// <summary>
    /// The buttons supported by game controllers.
    /// </summary>
    public enum Button {
        /// <summary>An invalid or unknown button.</summary>
        Invalid = 0x00,

        /// <summary>The A face button (X on PlayStation controllers).</summary>
        A = 0x01,

        /// <summary>The B face button (O on PlayStation controllers).</summary>
        B = 0x02,

        /// <summary>The X face button (Square on PlayStation controllers).</summary>
        X = 0x03,

        /// <summary>The Y face button (Triangle on PlayStation controllers).</summary>
        Y = 0x04,

        /// <summary>
        /// The back button (Select on older controllers, Share on the PS4 controller, and View on the
        /// Xbox One controller).
        /// </summary>
        Back = 0x05,

        /// <summary>
        /// The guide button (Xbox button on Xbox 360/One controllers or the PS button on PS3/PS4 controllers).
        /// </summary>
        Guide = 0x06,

        /// <summary>
        /// The start button (Menu button on Xbox One controllers or Options on the PS4 controller).
        /// </summary>
        Start = 0x07,

        /// <summary>The left stick button (L3).</summary>
        LeftStick = 0x08,

        /// <summary>The right stick button (R3).</summary>
        RightStick = 0x09,

        /// <summary>The left shoulder button (LB on Xbox 360/One, L1 on PS3/PS4).</summary>
        LeftShoulder = 0x0A,

        /// <summary>The right shoulder button (RB on Xbox 360/One, R1 on PS3/PS4).</summary>
        RightShoulder = 0x0B,

        /// <summary>The up direction on the directional pad.</summary>
        DpadUp = 0x0C,

        /// <summary>The down direction on the directional pad.</summary>
        DpadDown = 0x0D,

        /// <summary>The left direction on the directional pad.</summary>
        DpadLeft = 0x0E,

        /// <summary>The right direction on the directional pad.</summary>
        DpadRigh = 0x0F
    }
}
