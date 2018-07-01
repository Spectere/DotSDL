using System;
using DotSDL.Events;
using DotSDL.Graphics;
using DotSDL.Input.Keyboard;

namespace Sample.Power {
    public class Window : SdlWindow {
        public Window(int width, int height) : base("Power Test",
                                                    new Point { X = WindowPosUndefined, Y = WindowPosUndefined },
                                                    width, height, width, height) {
            KeyPressed += Window_KeyPressed;
        }

        private void Window_KeyPressed(object sender, KeyboardEvent e) {
            if(e.Keycode == Keycode.Escape)
                Stop();
            
            if(e.Keycode == Keycode.P) {
                var power = DotSDL.Power.PowerState.CurrentPowerState;
                Console.WriteLine($"Status: {power.BatteryStatus}; percent: {power.BatteryPercent}; minutes: {power.TimeRemaining.TotalMinutes}");
            }
        }
    }
}
