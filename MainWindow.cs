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

        private Label _celsiusLabel;
        private Label _fahrenheitLabel;

        private Button _closeButton;

        private double _celsius;
        private double _fahrenheit;

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
            _celsiusScale.DrawValue = false;

            _celsiusLabel = new Label("0°C");
            _celsiusBox.Add(_celsiusLabel);
            _celsiusLabel.Show();
            _celsiusLabel.WidthRequest = 100;

            _fahrenheitScale = new Scale(Orientation.Horizontal, -148, 212, 1);
            _fahrenheitBox.Add(_fahrenheitScale);
            _fahrenheitScale.Show();
            _fahrenheitScale.ChangeValue += FahrenheitScale_ChangeValueEvent;
            _fahrenheitScale.Expand = true;
            _fahrenheitScale.DrawValue = false;

            _fahrenheitLabel = new Label("0°F");
            _fahrenheitBox.Add(_fahrenheitLabel);
            _fahrenheitLabel.Show();
            _fahrenheitLabel.WidthRequest = 100;

            _closeButton = new Button("Close");
            _mainBox.Add(_closeButton);
            _closeButton.Clicked += (object s, EventArgs e) => Application.Quit();
            _closeButton.Show();
        }

        private void UpdateScales()
        {
            _celsiusScale.Value = _celsius;
            _fahrenheitScale.Value = _fahrenheit;
            _celsiusLabel.Text = $"{_celsius:F1}°C";
            _fahrenheitLabel.Text = $"{_fahrenheit:F1}°F";
        }

        private void FahrenheitScale_ChangeValueEvent(object o, ChangeValueArgs args)
        {
            if (o is Scale s)
            {
                _fahrenheit = s.Value;
                _celsius = (_fahrenheit - 32) * (5.0 / 9.0);
                UpdateScales();
            }
        }

        private void CelsiusScale_ChangeValueEvent(object o, ChangeValueArgs args)
        {
            if (o is Scale s)
            {
                _celsius = s.Value;
                _fahrenheit = _celsius * (9.0 / 5.0) + 32;
                UpdateScales();
            }
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}
