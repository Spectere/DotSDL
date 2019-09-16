using DotSDL.Interop.Core;

namespace DotSDL.Platform.Interop.Fallback {
    public class Timing {
        private float _sleepSkew = 0.0f;

        /// <summary>
        /// Sleeps for a given number of nanoseconds.
        /// </summary>
        /// <param name="ns">The number of nanoseconds to sleep.</param>
        /// <remarks>This implementation uses the SDL_Delay function and should work
        /// with all platforms. It attempts to skirt around the resolution issues using
        /// the number of fractional milliseconds.</remarks>
        public void Nanosleep(long ns) {
            var waitTime = ns / 1000000;
            _sleepSkew += (float)ns / 1000000 - waitTime;

            if(_sleepSkew >= 1) {
                // Take the whole part of the skew and add it to the sleep time.
                var skewAdd = (long)_sleepSkew;
                waitTime += skewAdd;
                _sleepSkew -= skewAdd;
            }

            if(waitTime > 0)
                Timer.Delay((uint)waitTime);
        }
    }
}
