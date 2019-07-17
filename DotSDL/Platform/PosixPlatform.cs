using System;
using DotSDL.Platform.Interop.Posix;

namespace DotSDL.Platform {
    public class PosixPlatform : BasePlatform {
        private readonly Timing _posixTiming = new Timing();

        public override Action<long> Nanosleep => _posixTiming.Nanosleep;
    }
}
