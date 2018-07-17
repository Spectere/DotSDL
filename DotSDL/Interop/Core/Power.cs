using System.Runtime.InteropServices;
using DotSDL.Power;

namespace DotSDL.Interop.Core {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_power.h.
    /// </summary>
    internal static class Power {
        /// <summary>
        /// Get the current power supply details.
        /// </summary>
        /// <param name="secs">Seconds of battery life left. You can pass a NULL
        /// here if you don't care. Will return -1 if we can't determine a value,
        /// or we're not running on a battery.</param>
        /// <param name="pct">Percentage of battery life left, between 0 and 100.
        /// You can pass a NULL here if you don't care. Will return -1 if we
        /// can't determine a value, or we're not running on a battery.</param>
        /// <returns>The state of the battery (if any).</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GetPowerInfo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern BatteryStatus GetPowerInfo(out int secs, out int pct);
    }
}
