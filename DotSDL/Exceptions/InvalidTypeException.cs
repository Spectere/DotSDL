using System;

namespace DotSDL.Exceptions {
    /// <summary>
    /// This <see cref="Exception"/> is thrown when an invalid type is used for
    /// a genetic class or method.
    /// </summary>
    public class InvalidTypeException : Exception {
        /// <summary>
        /// Initializes an <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="objectName">The class or method that was called.</param>
        /// <param name="invalidType">The invalid type that was passed.</param>
        /// <param name="message">A message that should be appended to the base message.</param>
        public InvalidTypeException(string objectName, Type invalidType, string message = "")
            : base($"{objectName} cannot accept the type {nameof(invalidType)}" + message != "" ? $" ({message})" : "") { }
    }
}
