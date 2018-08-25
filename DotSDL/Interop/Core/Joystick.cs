using System;
using System.Runtime.InteropServices;

namespace DotSDL.Interop.Core {
    /// Contains the necessary functions and imports from SDL_joystick.h.
    internal class Joystick {
        /// <summary>
        /// The direction that the hat was pressed in.
        /// </summary>
        [Flags]
        internal enum HatDirection {
            Center = 0x00,
            Up = 0x01,
            Right = 0x02,
            Down = 0x04,
            Left = 0x08,
            
            RightUp = Right | Up,
            RightDown = Right | Down,
            LeftUp = Left | Up,
            LeftDown = Left | Down
        }
        
        /// <summary>
        /// The state that the device should be set to.
        /// </summary>
        internal enum State {
            /// <summary>Retrieves the state that the device is in..</summary>
            Query = -1,
            
            /// <summary>Ignores the device.</summary>
            Ignore = 0,
            
            /// <summary>Activates the device for future use.</summary>
            Enable = 1
        }

        /// <summary>
        /// Represents the power level of an attached device.
        /// </summary>
        internal enum PowerLevel {
            /// <summary>The battery status is unknown.</summary>
            Unknown = 0x00,
            
            /// <summary>The battery is empty.</summary>
            Empty = 0x01,
            
            /// <summary>The battery level is low.</summary>
            Low = 0x02,
            
            /// <summary>The battery level is medium.</summary>
            Medium = 0x03,
            
            /// <summary>The battery is fully charged.</summary>
            Full = 0x04,
            
            /// <summary>The controller is wired.</summary>
            Wired = 0x05,
            
            /// <summary>The battery level is at max.</summary>
            Max = 0x06
        }

        /// <summary>
        /// Closes a joystick that was previously opened with <see cref="Open"/>.
        /// </summary>
        /// <param name="joystick">The joystick object to manipulate.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickClose", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Close(IntPtr joystick);

        /// <summary>
        /// Retrieves the battery level from a device.
        /// </summary>
        /// <param name="joystick">The joystick object to manipulate.</param>
        /// <returns>A <see cref="PowerLevel"/> value representing the battery level of the joystick.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickCurrentPowerLevel", CallingConvention = CallingConvention.Cdecl)]
        internal static extern PowerLevel CurrentPowerLevel(IntPtr joystick);

        /// <summary>
        /// Enables/disables joystick polling.
        /// </summary>
        /// <param name="state">The desired state.</param>
        /// <returns>1 if enabled, 0 if disabled, or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickEventState", CallingConvention = CallingConvention.Cdecl)]
        internal static extern State EventState(State state);

        /// <summary>
        /// Gets a joystick object associated with an instance ID.
        /// </summary>
        /// <param name="joystickId">The instance ID to get the joystick for.</param>
        /// <returns>Returns a joystick object on success or <c>null</c> on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickFromInstanceID", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FromInstanceId(int joystickId);

        /// <summary>
        /// Gets the status of a specified joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <returns><c>true</c> if the joystick has been opened, otherwise <c>false</c>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetAttached(IntPtr joystick);

        /// <summary>
        /// Gets the current state of an axis control on a joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <param name="axis">The index of the axis to query.</param>
        /// <returns>A 16-bit signed integer representing the current position of the axis.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetAxis", CallingConvention = CallingConvention.Cdecl)]
        internal static extern short GetAxis(IntPtr joystick, int axis);

        /// <summary>
        /// Gets the ball axis change since the last poll.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <param name="ball">The ball index to query.</param>
        /// <param name="dX">The difference in the X axis since the last poll.</param>
        /// <param name="dY">The difference in the Y axis since the last poll.</param>
        /// <returns>0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetBall", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetBall(IntPtr joystick, int ball, out int dX, out int dY);

        /// <summary>
        /// Gets the current state of a button on a joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <param name="button">The button index to get the state from.</param>
        /// <returns>1 if the specified button is pressed, otherwise 0.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetButton", CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte GetButton(IntPtr joystick, int button);

        /// <summary>
        /// Gets the implementation-dependent GUID for the joystick at a given device index.
        /// </summary>
        /// <param name="deviceIndex">The index of the joystick to query.</param>
        /// <returns>The GUID of the selected joystick.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetDeviceGUID", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Guid GetDeviceGuid(int deviceIndex);

        /// <summary>
        /// Gets the implementation-dependent GUID for the joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <returns>The GUID of the selected joystick.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetGUID", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Guid GetGuid(IntPtr joystick);

        /// <summary>
        /// Converts the GUID string into a GUID structure.
        /// </summary>
        /// <param name="guid">A string representation of a GUID.</param>
        /// <returns>A <see cref="Guid"/> structure based on the specified string.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetGUIDFromString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Guid GetGuidFromString(string guid);

        /// <summary>
        /// Gets the string representation for a given <see cref="Guid"/>.
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> to convert.</param>
        /// <param name="stringGuid">The buffer in which to write the ASCII string.</param>
        /// <param name="guidSize">The length of the string.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetGUIDString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GetGuidString(Guid guid, out string stringGuid, int guidSize);

        /// <summary>
        /// Gets the current state of a POV hat on a joystick. 
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <param name="hat">The index of the hat to poll.</param>
        /// <returns>A <see cref="HatDirection"/> representing the direction that the stick is being pressed.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickGetHat", CallingConvention = CallingConvention.Cdecl)]
        internal static extern HatDirection GetHat(IntPtr joystick, int hat);

        /// <summary>
        /// Gets the instance ID of an opened joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <returns>The instance ID of the specified joystick.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickInstanceID", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int InstanceId(IntPtr joystick);

        /// <summary>
        /// Gets the implementation-dependent name of the joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to manipulate.</param>
        /// <returns>The name of the selected joystick.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string Name(IntPtr joystick);

        /// <summary>
        /// Gets the implementation-dependent name of the joystick.
        /// </summary>
        /// <param name="deviceIndex">The device index to query.</param>
        /// <returns>The name of the selected joystick.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickNameForIndex", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string NameForIndex(int deviceIndex);

        /// <summary>
        /// Gets the number of general axis controls on a joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <returns>The number of axis controls on a joystick or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickNumAxes", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int NumAxes(IntPtr joystick);

        /// <summary>
        /// Gets the number of trackballs on a joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <returns>The number of trackballs or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickNumBall", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int NumBalls(IntPtr joystick);

        /// <summary>
        /// Gets the number of buttons on a joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <returns>The number of buttons or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickNumButtons", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int NumButtons(IntPtr joystick);

        /// <summary>
        /// Gets the number of POV hats on a joystick.
        /// </summary>
        /// <param name="joystick">The joystick object to query.</param>
        /// <returns>The number of POV hats or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickNumHats", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int NumHats(IntPtr joystick);

        /// <summary>
        /// Opens a joystick for use.
        /// </summary>
        /// <param name="deviceIndex">The index of the joystick to query.</param>
        /// <returns>A joystick identifier, or <c>null</c> if an error occurred.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickOpen", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Open(int deviceIndex);

        /// <summary>
        /// Updates the current state on all open joysticks.
        /// </summary>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_JoystickUpdate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Update();

        /// <summary>
        /// Counts the number of joysticks attached to the system.
        /// </summary>
        /// <returns></returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_NumJoysticks", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int NumJoysticks();
    }
}
