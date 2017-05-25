namespace DotSDL.Events {
    /// <summary>
    /// Events that can occur on a window.
    /// </summary>
    public enum WindowEventType : byte {
        /// <summary>Unused.</summary>
        None,

        /// <summary>Window has been shown.</summary>
        Shown,

        /// <summary>Window has been hidden.</summary>
        Hidden,

        /// <summary>Window has been exposed and should be redrawn.</summary>
        Exposed,

        /// <summary>Window has been moved.</summary>
        Moved,

        /// <summary>Window has been resized.</summary>
        Resized,

        /// <summary>The window size has changed, either as a result of an API call or through the system or user changing the window size.</summary>
        SizeChanged,

        /// <summary>Window has been minimized.</summary>
        Minimized,

        /// <summary>Window has been maximized.</summary>
        Maximized,

        /// <summary>Window has been restored to normal size and position.</summary>
        Restored,

        /// <summary>Window has gained mouse focus.</summary>
        Enter,

        /// <summary>Window has lost mouse focus.</summary>
        Leave,

        /// <summary>Window has gained keyboard focus.</summary>
        FocusGained,

        /// <summary>Window has lost keyboard focus.</summary>
        FocusLost,

        /// <summary>The window manager requests that the window be closed.</summary>
        Close,

        /// <summary>Window is being offered a focus (should SetWindowInputFocus() on itself or a subwindow, or ignore).</summary>
        TakeFocus,

        /// <summary>Window had a hit test that wasn't SDL_HITTEST_NORMAL.</summary>
        HitTest
    }
}
