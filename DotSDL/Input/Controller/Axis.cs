namespace DotSDL.Input.Controller {
    /// <summary>
    /// The axes supported by game controllers.
    /// </summary>
    public enum Axis {
        /// <summary>An invalid or unrecognized axis.</summary>
        Invalid = 0x00,

        /// <summary>The X position on the left stick.</summary>
        LeftX = 0x01,

        /// <summary>The Y position on the left stick.</summary>
        LeftY = 0x02,

        /// <summary>The X position on the right stick.</summary>
        RightX = 0x03,

        /// <summary>The Y position on the right stick.</summary>
        RightY = 0x04,

        /// <summary>The position of the left trigger.</summary>
        TriggerLeft = 0x05,

        /// <summary>The position of the right trigger.</summary>
        TriggerRigh = 0x06
    }
}
