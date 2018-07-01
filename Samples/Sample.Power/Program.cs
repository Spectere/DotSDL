using System;

namespace Sample.Power {
    class Program {
        static void Main(string[] args) {
            var window = new Window(256, 128);
            window.Start(100, 16);
        }
    }
}
