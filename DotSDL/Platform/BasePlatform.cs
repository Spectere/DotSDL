using System;
using DotSDL.Platform.Interop.Fallback;

namespace DotSDL.Platform {
    /// <summary>
    /// Implements the
    /// </summary>
    public class BasePlatform : IPlatform {
        private readonly Timing _fallbackTiming = new Timing();

        public virtual Action<long> Nanosleep => _fallbackTiming.Nanosleep;
    }
}
