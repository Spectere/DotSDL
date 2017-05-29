using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SdlEvents = DotSDL.Sdl.Events;

namespace DotSDL.Events {
    /// <summary>
    /// Handles dispatching events to objects that can receive them.
    /// </summary>
    internal static class EventHandler {
        /// <summary>
        /// An incredibly unsafe function that forcibly casts one type to
        /// another. This is used to convert between SDL2 events, since C# has
        /// no concept of union types.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="sdlEvent">The event that should be converted.</param>
        /// <returns>A structure of type T, containing the data in <paramref name="sdlEvent"/>.</returns>
        private static unsafe T CastEvent<T>(SdlEvents.SdlEvent sdlEvent) {
            var sdlEventPtr = new IntPtr(&sdlEvent);
            return Marshal.PtrToStructure<T>(sdlEventPtr);
        }

        /// <summary>
        /// Converts an <see cref="SdlEvents.SdlEvent"/> into an <see cref="IEvent"/>
        /// for use with applications.
        /// </summary>
        /// <param name="sdlEvent">The <see cref="SdlEvents.SdlEvent"/> to process.</param>
        /// <returns>An <see cref="IEvent"/> that can be passed to an application.</returns>
        private static IEvent ConvertEvent(SdlEvents.SdlEvent sdlEvent) {
            switch(sdlEvent.Type) {
                case SdlEvents.EventType.KeyDown:
                case SdlEvents.EventType.KeyUp:
                    var kbd = CastEvent<SdlEvents.SdlKeyboardEvent>(sdlEvent);
                    return EventConversion.Convert(kbd);
                case SdlEvents.EventType.WindowEvent:
                    var wnd = CastEvent<SdlEvents.SdlWindowEvent>(sdlEvent);
                    return EventConversion.Convert(wnd);
                default:
                    Debug.WriteLine(sdlEvent.Type);
                    break;
            }
            return null;
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
                    EventDispatcher.Dispatch(newEvent);
            }
        }
    }
}
