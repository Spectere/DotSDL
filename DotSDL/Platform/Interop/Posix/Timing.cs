using System;
using System.Runtime.InteropServices;

namespace DotSDL.Platform.Interop.Posix {
    public class Timing {
        private struct Timespec {
            /// <summary>
            /// A number of seconds.
            /// </summary>
            public int tvSec;

            /// <summary>
            /// A number of nanoseconds. This field must be in the range of 0 to 999999999.
            /// </summary>
            public long tvNsec;
        }

        /// <summary>
        /// Suspends a thread until at least the time given in <paramref name="req"/> has
        /// elapsed.
        /// </summary>
        /// <param name="req">The requested amount of time to sleep.</param>
        /// <param name="rem">The remaining sleep time, or NULL if this isn't necessary.</param>
        [DllImport("c", EntryPoint = "nanosleep", CallingConvention = CallingConvention.Cdecl)]
        private static extern void PosixNanosleep(in Timespec req, out Timespec rem);

        /// <summary>
        /// Sleeps for a given number of nanoseconds.
        /// </summary>
        /// <param name="ns">The number of nanoseconds to sleep.</param>
        /// <remarks>This implementation uses the nanosleep function introduced
        /// with POSIX.1.</remarks>
        public void Nanosleep(long ns) =>
            PosixNanosleep(new Timespec { tvSec = 0, tvNsec = ns}, out _);
    }
}
