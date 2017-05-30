using DotSDL.Input.Keyboard;

namespace DotSDL.Sdl {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_keyboard.h.
    /// </summary>
    internal static class Keyboard {
        internal struct Keysym {
            // Since this is used for interop, suppress the "never assigned to" warning.
            #pragma warning disable 0649

            /// <summary>
            /// SDL physical key code.
            /// </summary>
            internal Scancode Scancode;
            
            /// <summary>
            /// SDL virtual key code.
            /// </summary>
            internal Keycode Sym;

            /// <summary>
            /// Current key modifiers.
            /// </summary>
            internal Keymod Mod;
            internal uint Unused;

            #pragma warning restore 0649
        }
    }
}
