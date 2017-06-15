// MOTIVATION: This is provided as an alternative to using the
// System.Runtime.InteropServices.RuntimeInformation library. While
// that method works fine when using a .NET Core application, it doesn't have
// a direct analogue in the .NET Framework. As such, Framework applications
// would require additional libraries to be pulled down from NuGet and shipped
// with the final product. This sidesteps those requirements, allowing DotSDL
// to be a self-contained library with no additional dependencies.
namespace DotSDL {
    /// <summary>
    /// Provides methods for detecting the platform that DotSDL is running on.
    /// </summary>
    public static class Platform {
    }
}
