using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DotSDL.Sdl {
    /// <summary>
    /// Contains .NET constants to assist with making calls to the SDL library.
    /// </summary>
    internal static class Meta {
        /// <summary>
        /// Performs the initial setup work required to ensure that DotSDL extracts
        /// and loads the appropriate embedded Windows SDL2 DLL.
        /// </summary>
        internal static void InitializeDotSdl() {
            if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;

            // Build a path pointing to the user's temp directory.
            var tempPath = Path.GetTempPath();
            var outPath = $"{tempPath}{DllName}";

            // Extract and load the appropriate Windows SDL2 DLL.
            switch(RuntimeInformation.OSArchitecture) {
                case Architecture.X86:
                    ExtractResource("DotSDL.Binaries.SDL2_x86_32.dll", outPath);
                    LoadLibrary(outPath);
                    return;
                case Architecture.X64:
                    ExtractResource("DotSDL.Binaries.SDL2_x86_64.dll", outPath);
                    LoadLibrary(outPath);
                    return;
            }

            // If we made it here, there are no viable matches. Just let the OS load
            // the library in its own way.
        }

        /// <summary>
        /// Extracts an embedded resource into a specified file.
        /// </summary>
        /// <param name="resourceName">The name of the resource to extract.</param>
        /// <param name="outFile">The path to write the extracted resource to.</param>
        private static void ExtractResource(string resourceName, string outFile) {
            var assembly = typeof(Meta).GetTypeInfo().Assembly;

            using(var resourceStream = assembly.GetManifestResourceStream(resourceName))
            using(var outFileStream = File.Open(outFile, FileMode.Create))
                resourceStream.CopyTo(outFileStream);
        }

        /// <summary>
        /// Contains the name of the SDL library.
        /// </summary>
        internal const string DllName = "SDL2.dll";

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="dllToLoad">The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).</param>
        /// <returns>If the function succeeds, the return value is a handle to the module. If the function fails, the return value is NULL.</returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
    }
}
