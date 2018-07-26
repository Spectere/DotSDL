# DotSDL

DotSDL is a .NET Standard library designed to allow easy access to the SDL2
library using either the .NET Framework or .NET Core.

Unlike SDL2#, DotSDL is not a direct SDL wrapper. It attempts to add some
additional functionality to make writing SDL applications easier.

### Current Features

At this time, DotSDL supports the following features:

* Audio
  * Support for all audio formats supported by SDL.
  * Full upmixing and downmixing for mono, stereo, and quadraphonic audio.
    * 5.1 audio is supported, but upmixing and downmixing support for it is
      currently limited.
* Input
  * Keyboard input.
  * Window events.
* Graphics
  * A single 32-bit ARGB canvas (useful for pixel plotting).
* Power
  * Battery state.

### How To Use DotSDL

DotSDL is currently under heavy development and, as such, doesn't have much
in the way of documentation. Currently, the best way to use it is to look over
the sample/test projects and to read over the XMLDocs on the classes and
methods.

If you would still like to play around with DotSDL, the project can be built
using the .NET Core SDK or any IDE that support .NET Standard projects. You
will also need a native SDL2 library for each architecture that you plan to
build your project against.

