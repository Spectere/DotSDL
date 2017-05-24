using System;

namespace DotSDL.Input.Mouse {
    /// <summary>
    /// The set of mouse buttons supported by SDL2.
    /// </summary>
    public enum Button : byte {
        Left = 1,
        Middle = 2,
        Right = 3,
        X1 = 4,
        X2 = 5
    }

    /// <summary>
    /// A mask used to indicate multiple buttons being pressed simultaneously.
    /// </summary>
    [Flags]
    public enum ButtonMask {
        Left = 0x0001,
        Middle = 0x0002,
        Right = 0x0004,
        X1 = 0x0008,
        X2 = 0x0010
    }
}
