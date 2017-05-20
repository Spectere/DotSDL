using System;
using System.Runtime.InteropServices;

namespace DotSDL.Sdl {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_video.h.
    /// </summary>
    internal static class Video {
        /// <summary>
        /// An enumeration of window statesm.
        /// </summary>
        [Flags]
        internal enum WindowFlags : uint {
            /// <summary>No special flags.</summary>
            None = 0x00000000,

            /// <summary>Fullscreen window.</summary>
            Fullscreen = 0x00000001,

            /// <summary>Window usable with OpenGL context.</summary>
            OpenGl = 0x00000002,

            /// <summary>Window is visible.</summary>
            Shown = 0x00000004,

            /// <summary>Window is not visible.</summary>
            Hidden = 0x00000008,

            /// <summary>No window decoration.</summary>
            Borderless = 0x00000010,

            /// <summary>Window can be resized.</summary>
            Resizable = 0x00000020,

            /// <summary>Window is minimized.</summary>
            Minimized = 0x00000040,

            /// <summary>Window is maximized.</summary>
            Maximized = 0x00000080,

            /// <summary>Window has grabbed input focus.</summary>
            InputGrabbed = 0x00000100,

            /// <summary>Window has input focus.</summary>
            InputFocus = 0x00000200,

            /// <summary>Window has mouse focus.</summary>
            MouseFocus = 0x00000400,

            /// <summary>Window not created by SDL.</summary>
            Foreign = 0x00000800,

            /// <summary>Fullscreen window at the current desktop resolution.</summary>
            FullscreenDesktop = Fullscreen | 0x00001000,

            /// <summary>Window should be created in high-DPI mode if supported (>= SDL 2.0.1).</summary>
            AllowHighDpi = 0x00002000,

            /// <summary>Window has mouse captured (unrelated to <see cref="InputGrabbed"/>, >= SDL 2.0.4).</summary>
            MouseCapture = 0x00004000,

            /// <summary>Window should always be above others (X11 only, >= SDL 2.0.5).</summary>
            AlwaysOnTop = 0x00008000,

            /// <summary>Window should not be added to the taskbar (X11 only, >= SDL 2.0.5).</summary>
            SkipTaskbar = 0x00010000,

            /// <summary>Window should be treated as a utility window (X11 only, >= SDL 2.0.5).</summary>
            Utility = 0x00020000,

            /// <summary>Window should be treated as a popup (X11 only, >= SDL 2.0.5).</summary>
            Tooltip = 0x00040000,

            /// <summary>Window should be treated as a popup menu (X11 only, >= SDL 2.0.5).</summary>
            PopupMenu = 0x00080000
        }

        /// <summary>
        /// Creates a window with the specified position, dimensions, and flags.
        /// </summary>
        /// <param name="title">The title of the window, in UTF-8 encoding.</param>
        /// <param name="x">The x position of the window, <see cref="Graphics.SdlWindow.WindowPosCentered"/>, or <see cref="Graphics.SdlWindow.WindowPosUndefined"/>.</param>
        /// <param name="y">The y position of the window, <see cref="Graphics.SdlWindow.WindowPosCentered"/>, or <see cref="Graphics.SdlWindow.WindowPosUndefined"/>.</param>
        /// <param name="w">The width of the window, in screen coordinates.</param>
        /// <param name="h">The height of the window, in screen coordinates.</param>
        /// <param name="flags">One or more <see cref="WindowFlags"/> OR'd together.</param>
        /// <returns>The window that was created, or NULL on failure.</returns>
        [DllImport(Meta.DllName, EntryPoint = "SDL_CreateWindow", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CreateWindow(string title, int x, int y, int w, int h, WindowFlags flags);

        /// <summary>
        /// Destroys an SDL window.
        /// </summary>
        /// <param name="window">The window to destroy.</param>
        [DllImport(Meta.DllName, EntryPoint = "SDL_DestroyWindow", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DestroyWindow(IntPtr window);

        /// <summary>
        /// Gets the numberic ID of a window.
        /// </summary>
        /// <param name="window">The window to query.</param>
        /// <returns>The ID of the window.</returns>
        [DllImport(Meta.DllName, EntryPoint = "SDL_GetWindowID", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetWindowId(IntPtr window);

        /// <summary>
        /// Shows an SDL window.
        /// </summary>
        /// <param name="window">The window to show.</param>
        [DllImport(Meta.DllName, EntryPoint = "SDL_ShowWindow", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ShowWindow(IntPtr window);
    }
}
