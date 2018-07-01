using System;

namespace DotSDL.Power {
    public class PowerStatus {
        /// <summary>
        /// The current state of the system's battery.
        /// </summary>
        public BatteryStatus BatteryStatus { get; internal set; }
        
        /// <summary>
        /// The approximate percentage of battery power remaining.
        /// </summary>
        public int BatteryPercent { get; internal set; }
        
        /// <summary>
        /// The approximate amount of time that the system can be powered by the
        /// battery. 
        /// </summary>
        public TimeSpan TimeRemaining { get; internal set; }
    }
}
