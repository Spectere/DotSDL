using System;
using System.Runtime.InteropServices;
using SdlEvents = DotSDL.Sdl.Events;

namespace DotSDL.Events {
    /// <summary>
    /// Handles dispatching events to objects that can receive them.
    /// </summary>
    internal static class EventHandler {
        private static readonly ResourceManager Resources = ResourceManager.Instance;

        /// <summary>
        /// An incredibly unsafe function that forcibly casts one type to
        /// another. This is used to convert between SDL2 events, since C# has
        /// no concept of union types.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="sdlEvent">The event that should be converted.</param>
        /// <returns>A structure of type T, containing the data in <paramref name="sdlEvent"/>.</returns>
        private static unsafe T CastEvent<T>(SdlEvents.SdlEvent sdlEvent) {
            var sdlEventIntPtr = new IntPtr(&sdlEvent);
            return Marshal.PtrToStructure<T>(sdlEventIntPtr);
        }

        /// <summary>
        /// Converts an <see cref="SdlEvents.SdlEvent"/> into an <see cref="IEvent"/>
        /// for use with applications.
        /// </summary>
        /// <param name="sdlEvent">The <see cref="SdlEvents.SdlEvent"/> to process.</param>
        /// <returns>An <see cref="IEvent"/> that can be passed to an application.</returns>
        private static IEvent ConvertEvent(SdlEvents.SdlEvent sdlEvent) {
            switch(sdlEvent.Type) {
                case SdlEvents.EventType.WindowEvent:
                    var sdlWindowEvent = CastEvent<SdlEvents.SdlWindowEvent>(sdlEvent);
                    break;
            }
            return null;
        }

        /// <summary>
        /// Dispatches an <see cref="IEvent"/> to the appropriate <see cref="IResourceObject"/>.
        /// </summary>
        /// <param name="newEvent">The <see cref="IEvent"/> to dispatch.</param>
        private static void DispatchEvent(IEvent newEvent) {
        }

        /// <summary>
        /// Processes and dispatches all outstanding events.
        /// </summary>
        internal static void ProcessEvents() {
            var inEvent = new SdlEvents.SdlEvent();

            while(SdlEvents.PollEvent(ref inEvent) != 0) {
                var newEvent = ConvertEvent(inEvent);
                if(newEvent is null) continue;
                if(newEvent.Resource != null)
                    DispatchEvent(newEvent);
            }
        }
    }
}
