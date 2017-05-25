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

        /// <summary>
        /// The resources that this event should be dispatched to. If this
        /// is null, the event will not be dispatched.
        /// </summary>
        IResourceObject Resource { get; set; }
    }
}
