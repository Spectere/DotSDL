using System;
using System.Runtime.InteropServices;
using DotSDL.Input.Controller;
using State = DotSDL.Interop.Core.Joystick.State;

namespace DotSDL.Interop.Core {
    /// <summary>
    /// Contains the necessary functions and imports from SDL_gamecontroller.h.
    /// </summary>
    internal class GameController {
        internal enum BindType {
            /// <summary>
            /// No binding.
            /// </summary>
            None = 0x00,

            /// <summary>
            /// Bound to a button.
            /// </summary>
            Button = 0x01,

            /// <summary>
            /// Bound to an axis.
            /// </summary>
            Axis = 0x02,

            /// <summary>
            /// Bound to a hat.
            /// </summary>
            Hat = 0x03
        }
        
        /// <summary>
        /// Adds support for controllers that SDL is unaware of or causes an exsting controller to have
        /// a different binding. The mapping is loaded based on a passed string.
        /// </summary>
        /// <param name="mappingString">The mapping string. See https://wiki.libsdl.org/SDL_GameControllerAddMapping for more information.</param>
        /// <returns>1 if a new mapping is added, 0 if an existing mapping is updated, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerAddMapping", CallingConvention = CallingConvention.Cdecl)]
        internal extern int AddMapping(string mappingString);

        /// <summary>
        /// Adds support for controllers that SDL is unaware of or causes an exsting controller to have
        /// a different binding. The mapping is loaded from a file.
        /// </summary>
        /// <param name="filename">The name of the database to load.</param>
        /// <returns>The number of mappings added, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerAddMappingsFromFile", CallingConvention = CallingConvention.Cdecl)]
        internal extern int AddMappingsFromFile(string filename);

        /// <summary>
        /// Adds support for controllers that SDL is unaware of or causes an exsting controller to have
        /// a different binding. The mapping is loaded from an already open file.
        /// </summary>
        /// <param name="rw">The data stream for the mappings to be added.</param>
        /// <param name="freeRw">Non-zero to close the stream after it is read.</param>
        /// <returns>The number of mappings added, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerAddMappingsFromRW", CallingConvention = CallingConvention.Cdecl)]
        internal extern int AddMappingsFromRw(IntPtr rw, int freeRw);

        /// <summary>
        /// Closes a game controller previously opened with <see cref="Open"/>.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerClose", CallingConvention = CallingConvention.Cdecl)]
        internal extern void Close(IntPtr gameController);

        /// <summary>
        /// Querys, enables, or disables events dealing with game controllers. This does not affect
        /// joystick events.
        /// </summary>
        /// <param name="state">The desired event state.</param>
        /// <returns>The passed value, or the current value of the game controller's state if Query is passed.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerEventState", CallingConvention = CallingConvention.Cdecl)]
        internal extern State EventState(State state);

        /// <summary>
        /// Gets a game controller identifier associated with an instance ID.
        /// </summary>
        /// <param name="joystickId">The instance ID of the joystick.</param>
        /// <returns>The game controller identifier, or <c>null</c> on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerFromInstanceID", CallingConvention = CallingConvention.Cdecl)]
        internal extern IntPtr FromInstanceId(int joystickId);

        /// <summary>
        /// Checks if a controller has been opened and is currently connected.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <returns><c>true</c> if the controller has been opened and is currently connected, otherwise <c>false</c>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetAttached", CallingConvention = CallingConvention.Cdecl)]
        internal extern bool GetAttached(IntPtr gameController);

        /// <summary>
        /// Gets the current state of an axis control on a game controller.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <param name="axis">The axis to query.</param>
        /// <returns>Returns the current axis state.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetAxis", CallingConvention = CallingConvention.Cdecl)]
        internal extern short GetAxis(IntPtr gameController, Axis axis);

        /// <summary>
        /// Converts a string into an <see cref="Axis"/> representation.
        /// </summary>
        /// <param name="axisString">The string to convert.</param>
        /// <returns>An <see cref="Axis"/> representation of <paramref name="axisString"/>. <see cref="Axis.Invalid"/> will be returned if no match was found.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetAxisFromString", CallingConvention = CallingConvention.Cdecl)]
        internal extern Axis GetAxisFromString(string axisString);

        /// <summary>
        /// Gets the joystick layer binding for a controller axis mapping.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <param name="axis">The axis to query.</param>
        /// <returns>Returns a <see cref="BindType"/> on success, or an empty binding on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetBindForAxis", CallingConvention = CallingConvention.Cdecl)]
        internal extern BindType GetBindForAxis(IntPtr gameController, Axis axis);

        /// <summary>
        /// Gets the joystick layer binding for a controller button mapping.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <param name="button">The button to query.</param>
        /// <returns>Returns a <see cref="BindType"/> on success, or an empty binding on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetBindForButton", CallingConvention = CallingConvention.Cdecl)]
        internal extern BindType GetBindForButton(IntPtr gameController, Button button);

        /// <summary>
        /// Gets the current state of a button on a game controller.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <param name="button">The button to query.</param>
        /// <returns>1 if the button is pressed or 0 if it is not pressed.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetButton", CallingConvention = CallingConvention.Cdecl)]
        internal extern byte GetButton(IntPtr gameController, Button button);

        /// <summary>
        /// Converts a string into a button mapping.
        /// </summary>
        /// <param name="buttonString">The string representation of a button.</param>
        /// <returns>A <see cref="Button"/> representation of <paramref name="buttonString"/>. <see cref="Button.Invalid"/> will be returned if no match was found.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetButtonFromString", CallingConvention = CallingConvention.Cdecl)]
        internal extern Button GetButtonFromString(string buttonString);

        /// <summary>
        /// Gets the joystick instance from a game controller instance. This must be called in order to
        /// use the the <see cref="Joystick"/> functions with a gamepad.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <returns>A joystick object.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetJoystick", CallingConvention = CallingConvention.Cdecl)]
        internal extern IntPtr GetJoystick(IntPtr gameController);

        /// <summary>
        /// Gets the implementation-dependent name for an opened game controller.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <returns>The implementation-depenent name for the specified game controller, or <c>null</c> on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerName", CallingConvention = CallingConvention.Cdecl)]
        internal extern string GetName(IntPtr gameController);

        /// <summary>
        /// Gets the implementation-dependent name for a game controller.
        /// </summary>
        /// <param name="deviceIndex">The index of the desired device.</param>
        /// <returns>The implementation-dependent name for the game controller, or <c>null</c> on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerNameForIndex", CallingConvention = CallingConvention.Cdecl)]
        internal extern string GetNameForIndex(int deviceIndex);

        /// <summary>
        /// Converts an <see cref="Axis"/> into a string.
        /// </summary>
        /// <param name="axis">The <see cref="Axis"/> to convert into a string.</param>
        /// <returns>A string representation of the value in <paramref name="axis"/>, or <c>null</c> on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetStringForAxis", CallingConvention = CallingConvention.Cdecl)]
        internal extern string GetStringFromAxis(Axis axis);

        /// <summary>
        /// Converts a <see cref="Button"/> into a string.
        /// </summary>
        /// <param name="button">The <see cref="Button"/> to convert into a string.</param>
        /// <returns>A string representation of the value in <paramref name="button"/>, or <c>null</c> on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerGetStringForButton", CallingConvention = CallingConvention.Cdecl)]
        internal extern string GetStringForButton(Button button);

        /// <summary>
        /// Returns whether the given joystick is supported by the game controller interface.
        /// </summary>
        /// <param name="joystickIndex">The index of the joystick.</param>
        /// <returns><c>true</c> if the joystick is supported, otherwise <c>false</c>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_IsGameController", CallingConvention = CallingConvention.Cdecl)]
        internal extern bool IsGameController(int joystickIndex);

        /// <summary>
        /// Gets the current mapping of a game controller.
        /// </summary>
        /// <param name="gameController">The game controller identifier.</param>
        /// <returns>A string that has the controller's mapping, or <c>null</c> if no mapping is available.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerMapping", CallingConvention = CallingConvention.Cdecl)]
        internal extern string Mapping(IntPtr gameController);

        /// <summary>
        /// Gets the game controller mapping for a given GUID.
        /// </summary>
        /// <param name="guid">The GUID for the desired controller.</param>
        /// <returns>A string that has the controller's mapping, or <c>null</c> on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerMappingForGUID", CallingConvention = CallingConvention.Cdecl)]
        internal extern string MappingForGuid(Guid guid);

        /// <summary>
        /// Opens a game controller for use.
        /// </summary>
        /// <param name="deviceIndex">The index of the desired device.</param>
        /// <returns>A game controller identifier or <c>null</c> if an error occurred.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerOpen", CallingConvention = CallingConvention.Cdecl)]
        internal extern IntPtr Open(int deviceIndex);

        /// <summary>
        /// Manually pumps game controller updates if events are disabled.
        /// </summary>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_GameControllerUpdate", CallingConvention = CallingConvention.Cdecl)]
        internal extern void Update();
    }
}
