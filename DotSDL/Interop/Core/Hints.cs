using System.Runtime.InteropServices;

namespace DotSDL.Interop.Core {
    internal static class Hints {
        /// <summary>
        /// Represents a variable controlling the scaling quality. This variable can be set
        /// to one of the following values:
        ///
        ///   "0" or "nearest" - Nearest pixel sampling.
        ///   "1" or "linear"  - Linear filtering (supported by OpenGL and Direct3D)
        ///   "2" or "best"    - Anisotropic filtering (supported by Direct3D)
        ///
        /// By default, nearest pixel sampling is used.
        /// </summary>
        internal const string RenderScaleQuality = "SDL_RENDER_SCALE_QUALITY";

        /// <summary>
        /// Gets the value of a hint.
        /// </summary>
        /// <param name="name">The name of the hint to set.</param>
        /// <returns><c>true</c> if the hint was set, otherwise <c>false</c>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string GetHint(string name);

        /// <summary>
        /// Sets a hint with normal priority.
        /// </summary>
        /// <param name="name">The name of the hint to set.</param>
        /// <param name="value">The value to set the hint to.</param>
        /// <returns><c>true</c> if the hint was set, otherwise <c>false</c>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetHint(string name, string value);
    }
}
