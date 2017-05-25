namespace DotSDL.Events {
    /// <summary>
    /// Represents a window event. Window events are thrown when an event occurs
    /// that affects an <see cref="DotSDL.Graphics.SdlWindow"/> instance and/or
    /// the applicaiton as a whole. This includes the UI toolkit minimizing,
    /// maximizing, and closing a window, as well as application quit messages.
    /// </summary>
    public class WindowEvent : IEvent {
        public uint Timestamp { get; set; }
    }
}
