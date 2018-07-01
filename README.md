# DotSDL

DotSDL is a .NET Standard library designed to allow easy access to the SDL2
library using either the .NET Framework or .NET Core.

Unlike SDL2#, DotSDL is not a direct SDL wrapper. It attempts to add some
additional functionality to make writing SDL applications easier.

### Current Features

At this time, DotSDL supports the following features:

* Audio
  * Support for all audio formats supported by SDL.
  * Mono output.
* Input
  * Keyboard input.
  * Window events.
* Graphics
  * A single 32-bit ARGB canvas (useful for simple pixel plotting).
* Power
  * Battery state.

### How To Use DotSDL

DotSDL is currently under heavy development and, as such, doesn't have much
in the way of documentation. Currently, the best way to use it is to look over
the sample/test projects and to read over the XMLDocs on the classes and
methods.

If you would still like to play around with DotSDL, the project can be built
using Microsoft Visual Studio 2017. You will also need a SDL2.dll binary for
each architecture that you plan to build your project against.