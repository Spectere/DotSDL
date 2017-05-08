using System.Runtime.InteropServices;

namespace DotSDL.Sdl {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_timer.h.
    /// </summary>
    internal static class Timer {
        /// <summary>
        /// Retrieves the number of milliseconds since the SDL library was initialized.
        /// </summary>
        /// <returns>The number of milliseconds since the SDL library was initialized.</returns>
        [DllImport(Meta.DllName, EntryPoint = "SDL_GetTicks", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetTicks();
    }
}
