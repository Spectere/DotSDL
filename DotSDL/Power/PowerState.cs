using System;

namespace DotSDL.Power {
    /// <summary>
    /// Contains functions for handling a system's power supply. This is most
    /// useful on laptops, though it may also be useful for desktop PCs that
    /// are connected to a UPS.
    /// </summary>
    public static class PowerState {
        /// <summary>
        /// Retrieves the current power state of the system as a
        /// <see cref="PowerStatus"/> object.
        /// </summary>
        public static PowerStatus CurrentPowerState {
            get {
                var state = Sdl.Power.GetPowerInfo(out var secs, out var pct);
                return new PowerStatus {
                    BatteryStatus = state,
                    BatteryPercent = pct,
                    TimeRemaining = TimeSpan.FromSeconds(secs)
                };
            }
        }
    }
}
