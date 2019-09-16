using System;

namespace DotSDL.Platform {
    /// <summary>
    /// Senses the user's platform and returns a new instance of the most
    /// appropriate <see cref="IPlatform"/> implementation.
    /// </summary>
    public static class PlatformFactory {
        public static IPlatform GetPlatform() {
            switch(Environment.OSVersion.Platform) {
                case PlatformID.Unix:
                    return new PosixPlatform();
                default:
                    return new BasePlatform();
            }
        }
    }
}
