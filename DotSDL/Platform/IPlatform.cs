using System;

namespace DotSDL.Platform {
    /// <summary>
    /// Defines a set of function pointers for platform-specific native calls.
    /// </summary>
    public interface IPlatform {
        /// <summary>
        /// A high-resolution sleep timer that attempts to rest for a given
        /// number of nanoseconds.
        /// </summary>
        Action<long> Nanosleep { get; }
    }
}
