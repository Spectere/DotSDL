using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace DotSDL.Interop.Core {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_mouse.h.
    /// </summary>
    internal static class Mouse {
        /// <summary>
        /// Specifies valid mouse cursor states.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        internal enum SystemCursor : uint {
            /// <summary>The standard mouse pointer.</summary>
            Arrow,

            /// <summary>The I-beam cursor, usually used for text boxes.</summary>
            IBeam,

            /// <summary>The wait cursor, usually represented by an hourglass.</summary>
            Wait,

            /// <summary>The crosshair cursor.</summary>
            Crosshair,

            /// <summary>The wait/arrow cursor, usually represented by an arrow with
            /// an hourglass next to it.</summary>
            WaitArrow,

            /// <summary>Double arrow pointing northwest and southeast.</summary>
            SizeNwSe,

            /// <summary>Double arrow pointing northeast and southwest.</summary>
            SizeNeSw,

            /// <summary>Double arrow pointing west and east.</summary>
            SizeWE,

            /// <summary>Double arrow pointing north and south.</summary>
            SizeNS,

            /// <summary>Four pointed arrow pointing north, south, east, and west.</summary>
            SizeAll,

            /// <summary>Slashed circle.</summary>
            No,

            /// <summary>Hand pointer, usually used for hyperlinks.</summary>
            Hand
        }

        /// <summary>
        /// Defines the valid scroll wheel directions for the system.
        /// </summary>
        internal enum MouseWheelDirection : uint {
            /// <summary>The scroll direction is normal (up increments, down decrements).</summary>
            Normal,

            /// <summary>The scroll direction is inverted/natural (up decrements, down increments).</summary>
            Inverted
        }

        /// <summary>
        /// <para>Capture the mouse in order to track input outside an SDL window. Capturing allows
        /// mouse events to be obtained globally instead of just within the main window. Unlike
        /// relative mode, no change is made to the cursor and it is not constrained to the window.</para>
        /// <para>This function may deny mouse input to other windows--both in this application and
        /// others on the system--so it should be used sparingly and in small bursts. For example,
        /// this can be used to track the mouse while the user is dragging something, until the
        /// item is dropped by the user releasing the mouse button.</para>
        /// <para>While captured, mouse events report coordinates relative to the current
        /// (foreground) window, but those coordinates may be outside of the bounds of the window,
        /// including negative values. If the window loses focus while capturing, the capture
        /// will be disabled automatically.</para>
        /// </summary>
        /// <param name="enabled"><c>true</c> to capture the mouse, otherwise <c>false</c> (default).</param>
        /// <returns>0 on success, or -1 if mouse capture is not supported.</returns>
        /// <remarks>While capturing is enabled, the current window will have the
        /// <see cref="Video.WindowFlags.MouseCapture"/> flag set.</remarks>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_CaptureMouse", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CaptureMouse(bool enabled);

        /// <summary>
        /// Creates a color cursor from an SDL2 surface.
        /// </summary>
        /// <param name="surface">The surface representing the cursor's appearance.'</param>
        /// <param name="hotX">The X coordinate of this cursor's hotspot.</param>
        /// <param name="hotY">The Y coordinate of this cursor's hotspot.</param>
        /// <returns>The created cursor.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_CreateColorCursor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CreateColorCursor(IntPtr surface, int hotX, int hotY);

        /// <summary>
        /// Creates a system cursor.
        /// </summary>
        /// <param name="id">A <see cref="SystemCursor"/> designating what kind of cursor
        /// to create.</param>
        /// <returns>The created cursor.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_CreateSystemCursor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CreateSystemCursor(SystemCursor id);

        /// <summary>
        /// Frees a cursor created with <see cref="CreateColorCursor"/> or similar functions.
        /// </summary>
        /// <param name="cursor">The cursor to free.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_FreeCursor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FreeCursor(IntPtr cursor);

        /// <summary>
        /// Returns the default cursor.
        /// </summary>
        /// <returns>The default cursor.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GetDefaultCursor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetDefaultCursor();

        /// <summary>
        /// Gets the location of the mouse pointer relative to the window.
        /// </summary>
        /// <param name="x">The current X coordinate, relative to the window.</param>
        /// <param name="y">The current Y coordinate, relative to the window.</param>
        /// <returns>The current button state as a bitmask.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GetMouseState", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetMouseState(ref int x, ref int y);

        /// <summary>
        /// Gets the location of the mouse pointer relative to the top-left of the desktop.
        /// </summary>
        /// <param name="x">The current X coordinate, relative to the desktop.</param>
        /// <param name="y">The current Y coordinate, relative to the desktop.</param>
        /// <returns>The current button state as a bitmask.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GetGlobalMouseState", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetGlobalMouseState(ref int x, ref int y);

        /// <summary>
        /// Queries whether relative mouse mode is enabled.
        /// </summary>
        /// <returns><c>true</c> if relative mouse mode is enabled, otherwise <c>false</c>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GetRelativeMouseMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetRelativeMouseMode();

        /// <summary>
        /// Gets the location of the mouse pointer relative to the last time this function
        /// was called.
        /// </summary>
        /// <param name="x">The current X coordinate, relative to last invocation.</param>
        /// <param name="y">The current Y coordinate, relative to last invocation.</param>
        /// <returns>The current button state as a bitmask.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GetRelativeMouseState", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetRelativeMouseState(ref int x, ref int y);

        /// <summary>
        /// Set relative mouse mode. While the mouse is in relative mode, the cursor is hidden and
        /// the driver will try to report continuous motion in the current window. Only relative
        /// motion events will be delivered and the mouse position will not change.
        /// </summary>
        /// <param name="enabled"><c>true</c> to enable relative mode, or <c>false</c> to use
        /// absolute mode (default).</param>
        /// <returns>0 on success, or -1 if relative mode is not supported.</returns>
        /// <remarks>This function will flush any pending mosue motion.</remarks>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_SetRelativeMouseMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetRelativeMouseMode(bool enabled);

        /// <summary>
        /// Toggle whether or not the cursor is shown.
        /// </summary>
        /// <param name="toggle">1 to show the cursor, 0 to hide it, -1 to query the current state.</param>
        /// <returns>1 if the cursor is shown, or 0 if the cursor is hidden.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_ShowCursor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ShowCursor(int toggle);

        /// <summary>
        /// Moves the mouse to the given position within the window.
        /// </summary>
        /// <param name="window">The window to move the mouse into, or <see cref="IntPtr.Zero"/> for
        /// the current mouse focus.</param>
        /// <param name="x">The X coordinate within the window.</param>
        /// <param name="y">The Y coordinate within the window.</param>
        /// <remarks>This function generates a mouse motion event.</remarks>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_WarpMouseInWindow", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void WarpMouseInWindow(IntPtr window, int x, int y);

        /// <summary>
        /// Moves the mouse to the given position in global screen space.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>0 on success, -1 on error (usually: unsupported by a platform).</returns>
        /// <remarks>This function generates a mouse motion event.</remarks>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_WarpMouseGlobal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int WarpMouseGlobal(int x, int y);
    }
}
