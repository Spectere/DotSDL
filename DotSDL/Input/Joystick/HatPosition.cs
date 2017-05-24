using System;

namespace DotSDL.Input.Joystick {
    /// <summary>
    /// A list of possible positions that the joystick hat can be in.
    /// </summary>
    [Flags]
    public enum HatPosition : byte {
        Centered = 0x00,
        Up = 0x01,
        Right = 0x02,
        Down = 0x04,
        Left = 0x08,

        RightUp = Up | Right,
        RightDown = Down | Right,
        LeftUp = Up | Left,
        LeftDown = Down | Left
    }
}
