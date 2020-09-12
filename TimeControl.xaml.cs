using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Table1
{
    /// <summary>
    /// Логика взаимодействия для TxtBoxControl.xaml
    /// </summary>
    public partial class TimeControl : UserControl
    {
        int value = 0;
        const int MIN_TIME = 0;
        const int MAX_TIME = 180;
        public string Text
        {
            get { return textBoxTime.Text; }
            set { textBoxTime.Text = value; }
        }
        public TimeControl()
        {
            InitializeComponent();
        }
        private void ValidateTime(ref int value)
        {
            if (value > MAX_TIME)           
                value = MAX_TIME;
            
            if (value < MIN_TIME)            
                value = MIN_TIME;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(textBoxTime.Text, out int result))
            {
                ValidateTime(ref result);
                value = result;
            }
            textBoxTime.Text = value.ToString();

            if (value == MIN_TIME)
                decrement.IsEnabled = false;

            else if (value == MAX_TIME)
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
            textBoxTime.Text = newValue.ToString();
        }
        private void DecrementClick(object sender, RoutedEventArgs e)
        {
            int newValue = --value;
            ValidateTime(ref newValue);
            textBoxTime.Text = newValue.ToString();
        }
        private void Textbox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxTime.Focusable = true;
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
