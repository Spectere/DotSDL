using SdlEvents = DotSDL.Interop.Core.Events;

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

        /// <summary>
        /// Converts an <see cref="SdlEvents.SdlKeyboardEvent"/> to a <see cref="KeyboardEvent"/>.
        /// </summary>
        /// <param name="e">The <see cref="SdlEvents.SdlKeyboardEvent"/> to convert.</param>
        /// <returns>The resulting <see cref="KeyboardEvent"/>.</returns>
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
        /// Converts an <see cref="SdlEvents.SdlWindowEvent"/> to a <see cref="WindowEvent"/>.
        /// </summary>
        /// <param name="e">The <see cref="SdlEvents.SdlWindowEvent"/> to convert.</param>
        /// <returns>The resulting <see cref="WindowEvent"/>.</returns>
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
