using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Table1
{
    /// <summary>
    /// Логика взаимодействия для TxtBoxControl.xaml
    /// </summary>
    public partial class NumericControl : UserControl
    {
        int value = 0;
        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public NumericControl()
        {
            InitializeComponent();
        }
        private void ValidateTime(ref int value)
        {
            if (value > MaxValue)           
                value = MaxValue;
            
            if (value < MinValue)            
                value = MinValue;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(textBox.Text, out int result))
            {
                ValidateTime(ref result);
                value = result;
            }
            textBox.Text = value.ToString();

            if (value == MinValue)
                decrement.IsEnabled = false;

            else if (value == MaxValue)
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
            textBox.Text = newValue.ToString();
        }
        private void DecrementClick(object sender, RoutedEventArgs e)
        {
            int newValue = --value;
            ValidateTime(ref newValue);
            textBox.Text = newValue.ToString();
        }
        private void Textbox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBox.Focusable = true;
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
