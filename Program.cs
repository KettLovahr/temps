using System;
using Gtk;

namespace temps
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows()) {
                // Adds the bin directory to the Path env variable so that it can find the GTK libraries
                // This solves the problem with deploying GTK# applications to Windows
                string systemPath = Environment.GetEnvironmentVariable("Path");
                Environment.SetEnvironmentVariable("Path", $"{(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"))};{systemPath}");
                // This makes GTK# applications use the native Windows theme
                Environment.SetEnvironmentVariable("GTK_THEME", "win32");
            }

            Application.Init();

            var app = new Application("org.temps.temps", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
