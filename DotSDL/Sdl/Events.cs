using ControllerButton = DotSDL.Input.Controller.Button;
using DotSDL.Events;
using DotSDL.Input;
using DotSDL.Input.Controller;
using DotSDL.Input.Joystick;
using DotSDL.Input.Mouse;
using MouseButton = DotSDL.Input.Mouse.Button;
using System;
using System.Runtime.InteropServices;

namespace DotSDL.Sdl {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_events.h.
    /// </summary>
    internal static class Events {
        /// <summary>
        /// Indicates the type of event.
        /// </summary>
        internal enum EventType : uint {
            /// <summary>Unused.</summary>
            FirstEvent = 0,

            /*
             * Application Events
             */

            /// <summary>User-requested quit.</summary>
            Quit = 0x00000100,

            /// <summary>The application is being terminated by the OS. Called on iOS in applicationWillTerminate(); called on Android in onDestroy().</summary>
            AppTerminating = 0x00000101,
            
            /// <summary>The application is low on memory, free memory if possible. Called on iOS in applicationDidReceiveMemoryWarning(); called on Android in onLowMemory().</summary>
            LowMemory = 0x00000102,

            /// <summary>The application is about to enter the background. Called on iOS in applicationWillResignActive(); called on Android in onPause().</summary>
            WillEnterBackground = 0x00000103,
            
            /// <summary>The application did enter the background and may not get CPU for some time. Called on iOS in applicationDidEnterBackground(); called on Android in onPause().</summary>
            DidEnterBackground = 0x00000104,

            /// <summary>The application is about to enter the foreground. Called on iOS in applicationWillEnterForeground(); called on Android in onResume().</summary>
            WillEnterForeground = 0x00000105,

            /// <summary>The application is now interactive. Called on iOS in applicationDidBecomeActive(); called on Android in onResume().</summary>
            DidEnterForeground = 0x00000106,

            /*
             * Window events.
             */

            /// <summary>Window state change.</summary>
            WindowEvent = 0x00000200,

            /// <summary>System-specific event.</summary>
            SysWmEvent = 0x00000201,

            /*
             * Keyboard events.
             */

            /// <summary>Key pressed.</summary>
            KeyDown = 0x00000300,

            /// <summary>Key released.</summary>
            KeyUp = 0x00000301,

            /// <summary>Keyboard text editing (composition).</summary>
            TextEditing = 0x00000302,

            /// <summary>Keyboard text input.</summary>
            TextInput = 0x00000303,

            /// <summary>Keymap changed due to a system event such as an input language or keyboard layout change.</summary>
            KeymapChanged = 0x00000304,

            /*
             * Mouse events.
             */

            /// <summary>Mouse moved.</summary>
            MouseMotion = 0x00000400,

            /// <summary>Mouse button pressed.</summary>
            MouseButtonDown = 0x00000401,

            /// <summary>Mouse button released.</summary>
            MouseButtonUp = 0x00000402,
            
            /// <summary>Mouse wheel motion.</summary>
            MouseWheel = 0x00000403,

            /*
             * Joystick events.
             */

            /// <summary>Joystick axis motion.</summary>
            JoyAxisMotion = 0x00000600,
            
            /// <summary>Joystick trackball motion.</summary>
            JoyBallMotion = 0x00000601,
            
            /// <summary>Joystick hat position change.</summary>
            JoyHatMotion = 0x00000602,
            
            /// <summary>Joystick button pressed.</summary>
            JoyButtonDown = 0x00000603,
            
            /// <summary>Joystick button released.</summary>
            JoyButtonUp = 0x00000604,
            
            /// <summary>A new joystick has been inserted into the system.</summary>
            JoyDeviceAdded = 0x00000605,
            
            /// <summary>An opened joystick has been removed.</summary>
            JoyDeviceRemoved = 0x00000606,

            /*
             * Game controller events.
             */

            /// <summary>Game controller axis motion.</summary>
            ControllerAxisMotion = 0x00000650,

            /// <summary>Game controller button pressed.</summary>
            ControllerButtonDown = 0x00000651,

            /// <summary>Game controller button released.</summary>
            ControllerButtonUp = 0x00000652,

            /// <summary>A new game controller has been inserted into the system.</summary>
            ControllerDeviceAdded = 0x0000653,

            /// <summary>An opened game controller has been removed.</summary>
            ControllerDeviceRemoved = 0x00000654,

            /// <summary>The controller mapping was updated.</summary>
            ControllerDeviceRemapped = 0x00000655,

            /*
             * Touch events.
             */

            /// <summary>The display has been touched..</summary>
            FingerDown = 0x00000700,

            /// <summary>The display has been released.</summary>
            FingerUp = 0x00000701,

            /// <summary>Finger motion was detected by the display.</summary>
            FingerMotion = 0x00000702,

            /*
             * Gesture events.
             */

            /// <summary>A gesture has been performed.</summary>
            DollarGesture = 0x00000800,

            /// <summary>A gesture has been recorded.</summary>
            DollarRecord = 0x00000801,

            /// <summary>A multitouch gesture has been performed.</summary>
            MultiGesture = 0x00000802,

            /*
             * Clipboard events.
             */

            /// <summary>The clipboard changed.</summary>
            ClipboardUpdate = 0x00000900,

            /*
             * Drag and drop events.
             */

            /// <summary>The system requests a file open.</summary>
            DropFile = 0x00001000,

            /// <summary>text/plain drag-and-drop event.</summary>
            DropText = 0x00001001,

            /// <summary>A new set of drops is beginning (NULL filename).</summary>
            DropBegin = 0x00001002,

            /// <summary>Current set of drops is now complete (NULL Filename).</summary>
            DropComplete = 0x00001003,

            /*
             * Audio hotplug events.
             */

            /// <summary>A new audio device is available.</summary>
            AudioDeviceAdded = 0x00001100,

            /// <summary>An audio device has been removed.</summary>
            AudioDeviceRemoved = 0x00001101,

            /*
             * Render events.
             */

            /// <summary>The render targets have been reset and their contents need to be udpated.</summary>
            RenderTargetsReset = 0x00002000,

            /// <summary>The device has been reset and all textures need to be recreated.</summary>
            RenderDeviceReset = 0x00002001,

            /*
             * Other events.
             */

            /// <summary>User events.</summary>
            UserEvent = 0x00008000,
            
            /// <summary>Unused.</summary>
            LastEvent = 0x0000FFFF
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlAudioDeviceEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
            internal byte IsCapture;
            internal byte Padding1;
            internal byte Padding2;
            internal byte Padding3;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlControllerAxisEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
            internal Axis Axis;
            internal byte Padding1;
            internal byte Padding2;
            internal byte Padding3;
            internal short Value;
            internal ushort Padding4;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlControllerButtonEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
            internal ControllerButton Button;
            internal ButtonState State;
            internal byte Padding1;
            internal byte Padding2;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlControllerDeviceEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlDollarGestureEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal long TouchId;
            internal long GestureId;
            internal uint NumFingers;
            internal float Error;
            internal float X;
            internal float Y;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlDropEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal string File;
            internal uint WindowId;
        }

        /// <summary>
        /// General event structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal unsafe struct SdlEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal fixed byte Padding[48];
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlJoyAxisEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
            internal byte Axis;
            internal byte Padding1;
            internal byte Padding2;
            internal byte Padding3;
            internal short Value;
            internal ushort Padding4;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlJoyBallEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
            internal byte Ball;
            internal byte Padding1;
            internal byte Padding2;
            internal byte Padding3;
            internal short XRel;
            internal short YRel;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlJoyButtonEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
            internal byte Button;
            internal ButtonState State;
            internal byte Padding1;
            internal byte Padding2;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlJoyDeviceEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlJoyHatEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint Which;
            internal byte Hat;
            internal HatPosition Value;
            internal byte Padding1;
            internal byte Padding2;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlKeyboardEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal ButtonState State;
            internal byte Repeat;
            internal byte Padding2;
            internal byte Padding3;
            internal Keyboard.Keysym Keysym;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlMouseButtonEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal uint Which;
            internal MouseButton Button;
            internal ButtonState State;
            internal byte Clicks;
            internal byte Padding1;
            internal int X;
            internal int Y;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlMouseMotionEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal uint Which;
            internal ButtonState State;
            internal int X;
            internal int Y;
            internal int XRel;
            internal int YRel;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlMouseWheelEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal uint Which;
            internal int X;
            internal int Y;
            internal WheelDirection Direction;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlMultiGestureEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal long TouchId;
            internal float DTheta;
            internal float DDist;
            internal float X;
            internal float Y;
            internal ushort NumFingers;
            internal ushort Padding;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlOsEvent {
            internal EventType Type;
            internal uint Timestamp;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlQuitEvent {
            internal EventType Type;
            internal uint Timestamp;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlSysWmEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal IntPtr Msg;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal unsafe struct SdlTextEditingEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal fixed byte Text[32];
            internal int Start;
            internal int Length;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal unsafe struct SdlTextInputEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal fixed byte Text[32];
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlTouchFingerEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal long TouchId;
            internal long FingerId;
            internal float X;
            internal float Y;
            internal float DX;
            internal float DY;
            internal float Pressure;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlUserEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal int Code;
            internal IntPtr Data1;
            internal IntPtr Data2;
        }

        [StructLayout(LayoutKind.Sequential, Size = 56)]
        internal struct SdlWindowEvent {
            internal EventType Type;
            internal uint Timestamp;
            internal uint WindowId;
            internal WindowEventType EventId;
            internal byte Padding1;
            internal byte Padding2;
            internal byte Padding3;
            internal int Data1;
            internal int Data2;
        }

        /// <summary>
        /// Polls for currently pending events.
        /// </summary>
        /// <param name="sdlEvent">An object to store event data into. If this is not NULL, the event is removed from the queue and stored into the object.</param>
        /// <returns>1 if there are any pending events, or 0 if there are none available.</returns>
        [DllImport(Meta.DllName, EntryPoint = "SDL_PollEvent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PollEvent(ref SdlEvent sdlEvent);
    }
}
