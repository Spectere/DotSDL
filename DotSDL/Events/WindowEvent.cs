namespace DotSDL.Events {
    /// <summary>
    /// Represents a window event. Window events are thrown when an event occurs
    /// that affects an <see cref="Graphics.SdlWindow"/> instance and/or
    /// the applicaiton as a whole. This includes the UI toolkit minimizing,
    /// maximizing, and closing a window, as well as application quit messages.
    /// </summary>
    public class WindowEvent : IEvent {
        public uint Timestamp { get; set; }
        public IResourceObject Resource { get; set; }

        /// <summary>
        /// The event that was triggered.
        /// </summary>
        public WindowEventType Event { get; set; }

        /// <summary>
        /// When Event is WindowEventType.Moved, this contains the new X coordinate
        /// of the window. When Event is WindowEventType.Resized, this contains the
        /// new width of the window. For all other events, this value is unused.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// When Event is WindowEventType.Moved, this contains the new Y coordinate
        /// of the window. When Event is WindowEventType.Resized, this contains the
        /// new height of the window. For all other events, this value is unused.
        /// </summary>
        public int Y { get; set; }
    }
}
