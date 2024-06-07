using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace temps
{
    class MainWindow : Window
    {
        /* Don't forget to create an object for widgets like this:
        [UI] private Button _button1 = null;
        */
        [UI] private Box _mainBox = null;
        [UI] private Box _celsiusBox = null;
        [UI] private Box _fahrenheitBox = null;

        private Scale _celsiusScale;
        private Scale _fahrenheitScale;

        private Button _closeButton;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            _celsiusScale = new Scale(Orientation.Horizontal, -100, 100, 1);
            _celsiusBox.Add(_celsiusScale);
            _celsiusScale.Show();
            _celsiusScale.ChangeValue += CelsiusScale_ChangeValueEvent;
            _celsiusScale.Expand = true;

            _fahrenheitScale = new Scale(Orientation.Horizontal, -148, 212, 1);
            _fahrenheitBox.Add(_fahrenheitScale);
            _fahrenheitScale.Show();
            _fahrenheitScale.ChangeValue += FahrenheitScale_ChangeValueEvent;
            _fahrenheitScale.Expand = true;

            _closeButton = new Button("Close");
            _mainBox.Add(_closeButton);
            _closeButton.Clicked += (object s, EventArgs e) => Application.Quit();
            _closeButton.Show();
        }

        private void FahrenheitScale_ChangeValueEvent(object o, ChangeValueArgs args)
        {
            if (o is Scale s) {
                _celsiusScale.Value = (_fahrenheitScale.Value - 32) * (5.0 / 9.0);
            }
        }

        private void CelsiusScale_ChangeValueEvent(object o, ChangeValueArgs args)
        {
            if (o is Scale s) {
                _fahrenheitScale.Value = _celsiusScale.Value * (9.0 / 5.0) + 32;
            }
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}
