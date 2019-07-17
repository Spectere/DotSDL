namespace Sample.BasicPixels {
    internal class Program {
        private static void Main(string[] args) {
            var window = new Window(512, 256);
            window.Start(10, 60);  // 10 fps, 60hz updates
        }
    }
}
