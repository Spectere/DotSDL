using System;
using System.Runtime.InteropServices;

namespace DotSDL.Interop.Core {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL.h.
    /// </summary>
    internal static class Init {
        [Flags]
        internal enum SubsystemFlags : uint {
            /// <summary>No subsystem.</summary>
            None = 0x00000000,

            /// <summary>Timer subsystem.</summary>
            Timer = 0x00000001,

            /// <summary>Audio subsystem.</summary>
            Audio = 0x00000010,

            /// <summary>Video subsystem; automatically initializes the events subsystem.</summary>
            Video = 0x00000020,

            /// <summary>Joystick subsystem; automatically initializes the events subsystem.</summary>
            Joystick = 0x00000200,

            /// <summary>Haptic (force feedback) subsystem.</summary>
            Haptic = 0x00001000,

            /// <summary>Controller subsystem; automatically initializes the joystick subsystem.</summary>
            GameController = 0x00002000,

            /// <summary>Events subsystem.</summary>
            Events = 0x00004000,

            /// <summary>Compatibility; this flag is ignored.</summary>
            NoParachute = 0x00100000,

            /// <summary>All of the above subsystems.</summary>
            Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events | NoParachute
        }

        /// <summary>
        /// This function initalizes the subsystems specified by <paramref name="flags"/>.
        /// </summary>
        /// <param name="flags">Flags indicating which subsystem or subsystems to initialize.</param>
        /// <returns>0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_Init", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Initialize(SubsystemFlags flags);

        /// <summary>
        /// This function initializes specific SDL subsystems
        ///
        /// Subsystem initialization is ref-counted, you must call
        /// SDL_QuitSubSystem() for each SDL_InitSubSystem() to correctly
        /// shutdown a subsystem manually(or call SDL_Quit() to force shutdown).
        /// If a subsystem is already loaded then this call will
        /// increase the ref-count and return.
        /// </summary>
        /// <param name="flags">Flags indicating which subsystem or subsystems to initialize.</param>
        /// <returns>0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_InitSubSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int InitSubSystem(SubsystemFlags flags);

        /// <summary>
        /// This function cleans up specific SDL subsystems
        /// </summary>
        /// <param name="flags">Flags indicating which subsystem or subsystems to quit.</param>
        /// <returns>0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_QuitSubSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int QuitSubSystem(SubsystemFlags flags);

        /// <summary>
        /// This function returns a mask of the specified subsystems which have
        /// previously been initialized.
        ///
        /// If <paramref name="flags"/> is 0, it returns a mask of all initialized subsystems.
        /// </summary>
        /// <param name="flags">Flags indicating which subsystem or subsystems to query.</param>
        /// <returns>A set of all initialized subsystems.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_WasInit", CallingConvention = CallingConvention.Cdecl)]
        internal static extern SubsystemFlags WasInit(SubsystemFlags flags);

        /// <summary>
        /// This function cleans up all initialized subsystems. You should call it upon all
        /// exit conditions.
        /// </summary>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_Quit", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Quit();
    }
}
