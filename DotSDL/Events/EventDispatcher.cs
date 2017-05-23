using System;

namespace DotSDL.Events {
    /// <summary>
    /// Handles dispatching events to objects that can receive them.
    /// </summary>
    internal static class EventDispatcher {
        private static readonly ResourceManager Resources = ResourceManager.Instance;

        /// <summary>
        /// Processes and dispatches all outstanding events.
        /// </summary>
        internal static void ProcessEvents() {
            throw new NotImplementedException();
        }
    }
}
