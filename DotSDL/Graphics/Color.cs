using System.Runtime.InteropServices;

namespace DotSDL.Graphics {
    /// <summary>
    /// A structure representing a 32-bit ARGB color value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Color {
        /// <summary>
        /// The blue channel.
        /// </summary>
        public byte B;

        /// <summary>
        /// The green channel.
        /// </summary>
        public byte G;

        /// <summary>
        /// The red channel.
        /// </summary>
        public byte R;

        /// <summary>
        /// The alpha channel.
        /// </summary>
        public byte A;
    }
}
