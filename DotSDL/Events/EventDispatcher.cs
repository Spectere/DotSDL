using DotSDL.Graphics;

namespace DotSDL.Events {
    /// <summary>
    /// Handles dispatching events to an application's windows and other objects.
    /// </summary>
    internal static class EventDispatcher {
        /// <summary>
        /// Dispatches an event to its associated object.
        /// </summary>
        /// <param name="ev">The event to process.</param>
        internal static void Dispatch(IEvent ev) {
            switch(ev) {
                case WindowEvent e:
                    var window = (SdlWindow)e.Resource;
                    window.HandleEvent(e);
                    break;
            }
        }
    }
}
