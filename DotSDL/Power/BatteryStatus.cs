namespace DotSDL.Power {
    /// <summary>
    /// The status of the system's battery.
    /// </summary>
    public enum BatteryStatus {
        /// <summary>
        /// Cannot determine power status.
        /// </summary>
        Unknown = 0x00,
        
        /// <summary>
        /// Not plugged in, running on the battery.
        /// </summary>
        OnBattery = 0x01,
        
        /// <summary>
        /// Plugged in, no battery available.
        /// </summary>
        NoBattery = 0x02,
        
        /// <summary>
        /// Plugged in, charging battery.
        /// </summary>
        Charging = 0x03,
        
        /// <summary>
        /// Plugged in, battery charged.
        /// </summary>
        Charged = 0x04
    }
}
