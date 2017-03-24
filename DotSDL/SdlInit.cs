using DotSDL.Sdl;
using System;
using System.Collections.Generic;

namespace DotSDL {
    /// <summary>
    /// This singleton maintains the SDL initialization state.
    /// </summary>
    internal sealed class SdlInit {
        private static readonly Lazy<SdlInit> Singleton = new Lazy<SdlInit>(() => new SdlInit());
        internal static SdlInit Instance => Singleton.Value;

        private readonly Stack<Init.SubsystemFlags> _subsystems = new Stack<Init.SubsystemFlags>();

        private SdlInit() {
            Init.Initialize(Init.SubsystemFlags.None);
        }

        ~SdlInit() {
            // Pop flags and shut down each subsystem in order.
            while(_subsystems.Count > 0)
                Init.QuitSubSystem(_subsystems.Pop());

            Init.Quit();
        }

        /// <summary>
        /// Initializes an SDL subsystem.
        /// </summary>
        /// <param name="subsystemFlags">The subsystem(s) to initialize.</param>
        /// <returns><c>true</c> if initialization was successful, otherwise <c>false</c>.</returns>
        internal bool InitSubsystem(Init.SubsystemFlags subsystemFlags) {
            _subsystems.Push(subsystemFlags);
            return Init.Initialize(subsystemFlags) == 0;
        }
    }
}
