namespace Sample.Audio {
    class Program {
        static void Main(string[] args) {
            var window = new Window(512, 256);
            window.Start(500, 16);
        }
    }
}
