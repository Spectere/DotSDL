namespace DotSDL.Events {
    /// <summary>
    /// An interface describing a common DotSDL event.
    /// </summary>
    public interface IEvent {
        /// <summary>
        /// The timestamp of the event. Thsi is measured in milliseconds,
        /// starting from when the application is first executed.
        /// </summary>
        uint Timestamp { get; set; }
    }
}
