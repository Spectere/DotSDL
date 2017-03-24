using System.Runtime.InteropServices;

namespace DotSDL.Sdl {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_rect.h.
    /// </summary>
    internal class Rect {
        /// <summary>
        /// The structure that defines a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SdlPoint {
            public int X, Y;
        }

        /// <summary>
        /// A rectangle, with the origin at the upper left.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SdlRect {
            public int X, Y;
            public int W, H;
        }
    }
}
