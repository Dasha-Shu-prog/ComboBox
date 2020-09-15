using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Table1
{
    /// <summary>
    /// Логика взаимодействия для AmplitudeControl.xaml
    /// </summary>
    public partial class AmplitudeControl : UserControl
    {
        int value = 0;
        const int MIN_AMPL = -15;
        const int MAX_AMPL = 15;
        public string Text
        {
            get { return textBoxAmplitude.Text; }
            set { textBoxAmplitude.Text = value; }
        }
        public AmplitudeControl()
        {
            InitializeComponent();
        }
        private void ValidateTime(ref int value)
        {
            if (value > MAX_AMPL)
                value = MAX_AMPL;

            if (value < MIN_AMPL)
                value = MIN_AMPL;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(textBoxAmplitude.Text, out int result))
            {
                ValidateTime(ref result);
                value = result;
            }
            textBoxAmplitude.Text = value.ToString();

            if (value == MIN_AMPL)
                decrement.IsEnabled = false;

            else if (value == MAX_AMPL)
                increment.IsEnabled = false;

            else
            {
                decrement.IsEnabled = true;
                increment.IsEnabled = true;
            }
        }
        private void IncrementClick(object sender, RoutedEventArgs e)
        {
            int newValue = ++value;
            ValidateTime(ref newValue);
            textBoxAmplitude.Text = newValue.ToString();
        }
        private void DecrementClick(object sender, RoutedEventArgs e)
        {
            int newValue = --value;
            ValidateTime(ref newValue);
            textBoxAmplitude.Text = newValue.ToString();
        }
        private void Textbox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxAmplitude.Focusable = true;
        }
        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                IncrementClick(sender, null);
                increment.Focusable = true;
            }
            else if (e.Key == Key.Left)
            {
                DecrementClick(sender, null);
                decrement.Focusable = true;
            }
        }
        private void Textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        IncrementClick(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        DecrementClick(sender, null);
                        break;
                    }
            }
        }
    }
}
