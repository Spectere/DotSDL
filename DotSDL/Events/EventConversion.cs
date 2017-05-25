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
                case SdlEvents.SdlWindowEvent e:
                    return ConvertWindowEvent(e);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static WindowEvent ConvertWindowEvent(SdlEvents.SdlWindowEvent e) {
            return new WindowEvent {
                Timestamp = e.Timestamp,
                Event = e.EventId,
                Resource = Resources.GetResourceById(e.WindowId),
                X = e.Data1,
                Y = e.Data2
            };
        }
    }
}
