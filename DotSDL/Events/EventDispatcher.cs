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
            SdlWindow window;

            switch(ev) {
                case KeyboardEvent kbd:
                    window = (SdlWindow)kbd.Resource;
                    window.HandleEvent(kbd);
                    break;
                case WindowEvent wnd:
                    window = (SdlWindow)wnd.Resource;
                    window.HandleEvent(wnd);
                    break;
            }
        }
    }
}
