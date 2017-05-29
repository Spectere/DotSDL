using DotSDL.Input;
using DotSDL.Input.Keyboard;

namespace DotSDL.Events {
    /// <summary>
    /// Represents a keyboard event. Keyboard events are thrown when an key is
    /// pressed while an <see cref="Graphics.SdlWindow"/> has focus.
    /// </summary>
    public class KeyboardEvent : IEvent {
        public uint Timestamp { get; set; }
        public IResourceObject Resource { get; set; }

        /// <summary>
        /// A <see cref="Keycode"/> that represents the key being pressed or
        /// released.
        /// </summary>
        public Keycode Keycode { get; set; }

        /// <summary>
        /// A <see cref="Keymod"/> that represents the key modifiers being held
        /// down while the key is being pressed.
        /// </summary>
        public Keymod Keymod { get; set; }

        /// <summary>
        /// <c>true</c> if this event is the result of an auto-repeated
        /// keystroke, otherwise <c>false</c>.
        /// </summary>
        public bool Repeat { get; set; }

        /// <summary>
        /// A <see cref="Scancode"/> that represents the key being pressed or
        /// released.
        /// </summary>
        public Scancode Scancode { get; set; }

        /// <summary>
        /// Indicates the state of the key when the event is fired.
        /// </summary>
        public ButtonState State { get; set; }
    }
}
