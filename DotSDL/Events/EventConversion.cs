using SdlEvents = DotSDL.Sdl.Events;

namespace DotSDL.Events {
    /// <summary>
    /// Converts from a SDL2-style event to a DotSDL-style <see cref="IEvent"/>.
    /// </summary>
    internal static class EventConversion {
        private static readonly ResourceManager Resources = ResourceManager.Instance;

        /// <summary>
        /// Converts an SDL2-style event to a DotSDL-style <see cref="IEvent"/>.
        /// </summary>
        /// <param name="sdlEvent">The event to convert.</param>
        /// <returns>A DotSDL <see cref="IEvent"/> that can be passed to applications.</returns>
        internal static IEvent Convert(object sdlEvent) {
            switch(sdlEvent) {
                case SdlEvents.SdlWindowEvent wnd:
                    return ConvertWindowEvent(wnd);
                case SdlEvents.SdlKeyboardEvent kbd:
                    return ConvertKeyboardEvent(kbd);
            }

            return null;
        }

        private static KeyboardEvent ConvertKeyboardEvent(SdlEvents.SdlKeyboardEvent e) {
            return new KeyboardEvent {
                Timestamp = e.Timestamp,
                Resource = Resources.GetResourceById(e.WindowId),
                Keycode = e.Keysym.Sym,
                Keymod = e.Keysym.Mod,
                Repeat = e.Repeat != 0,
                Scancode = e.Keysym.Scancode,
                State = e.State
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static WindowEvent ConvertWindowEvent(SdlEvents.SdlWindowEvent e) {
            return new WindowEvent {
                Timestamp = e.Timestamp,
                Resource = Resources.GetResourceById(e.WindowId),
                Event = e.EventId,
                X = e.Data1,
                Y = e.Data2
            };
        }
    }
}
