using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Key = System.Windows.Input.Key;
using InteractiveDataDisplay.WPF;
using System.Windows.Media.Effects;
using System.Collections;
using System.Windows.Controls.Primitives;

namespace Table1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
     {
            private int t_start = 0;
            private int t_stop = 0;
            private int val_ampl = 0;
            const int MIN_TIME = 0;
            const int MAX_TIME = 180;
            const int MIN_AMPL = -15;
            const int MAX_AMPL = 15;
            List<Strobe_Characteristic> listStrobe = new List<Strobe_Characteristic>();

        public MainWindow()
        {
            InitializeComponent();
            var y = Enumerable.Range(0, 2000).Select(i => i / 10.0).ToArray();
            var x = y.Select(v => Math.Sin(v + 100) * 10).ToArray();
            var lg = new LineGraph();
            lines.Children.Add(lg);
            lg.Stroke = new SolidColorBrush(Colors.Black);
            lg.Plot(x, y);
        }        
        private void Time_start_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(time_start, ref t_start, increment_t_start, decrement_t_start);
        }
        private void Time_stop_TextChanged(object sender, TextChangedEventArgs e)
        {          
            TextChanged(time_stop, ref t_stop, increment_t_stop, decrement_t_stop);
        }
        private void Ampl_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(ampl, ref val_ampl, increment_ampl, decrement_ampl, false);
        }
        private void TextChanged(TextBox textBox, ref int inputValue, RepeatButton incrementButton, RepeatButton decrementButton, bool isTime = true)
        {
            int minValue = isTime ? MIN_TIME : MIN_AMPL;
            int maxValue = isTime ? MAX_TIME : MAX_AMPL;

            if (Int32.TryParse(textBox.Text, out int result))
            {
                if (isTime)
                    ValidateTime(ref result);
                else 
                    ValidateAmpl(ref result);

                inputValue = result;
            }
            textBox.Text = inputValue.ToString();
            if (inputValue == minValue)
                decrementButton.IsEnabled = false;
            else if (inputValue == maxValue)
                incrementButton.IsEnabled = false;
            else
            {
                decrementButton.IsEnabled = true;
                incrementButton.IsEnabled = true;
            }
        }       
        private void ValidateTime(ref int value)
        {
            ValidateData(ref value);
        }   
        private void ValidateAmpl(ref int value)
        {
            ValidateData(ref value, false);
        }
        private void ValidateData(ref int value, bool isTime = true)
        {
            int minValidValue = isTime ? MIN_TIME : MIN_AMPL;
            int maxValidValue = isTime ? MAX_TIME : MAX_AMPL;
            if (value > maxValidValue)
                value = maxValidValue;
            if (value < minValidValue)
                value = minValidValue;
        }
        //private int num = 1;
        private void Table_LoadingRow(object sender, DataGridRowEventArgs e)
        {         
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (t_start > t_stop)
                MessageBox.Show("Время начала должно быть меньше времени конца!");
            Strobe_Characteristic strobe = new Strobe_Characteristic 
            {
                //Number = num,
                Time_start = t_start,
                Time_stop = t_stop,
                Amplitude = val_ampl,
                Color = color_box.Text
            };
            listStrobe.Add(strobe);            
            table.Items.Add(listStrobe.Last());
            var line = new LineGraph();
            lines.Children.Add(line);
            switch (strobe.Color)
            {
                case "Красный":
                    line.Stroke = new SolidColorBrush(Colors.Firebrick);
                    break;
                case "Синий":
                    line.Stroke = new SolidColorBrush(Colors.DarkBlue);
                    break;
                case "Зелёный":
                    line.Stroke = new SolidColorBrush(Colors.DarkGreen);
                    break;
                case "Оранжевый":
                    line.Stroke = new SolidColorBrush(Colors.DarkOrange);
                    break;
                case "Фиолетовый":
                    line.Stroke = new SolidColorBrush(Colors.DarkMagenta);
                    break;
                case "Другой цвет":
                    line.Stroke = new SolidColorBrush(Colors.BlueViolet);
                    break;
            }
            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();
            line.StrokeThickness = 2;
            xCoords.Add(strobe.Amplitude);
            yCoords.Add(strobe.Time_start);
            xCoords.Add(strobe.Amplitude);
            yCoords.Add(strobe.Time_stop);
            line.Plot(xCoords, yCoords);
        }
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            table.Items.Remove(table.SelectedItem);            
            table.Items.Refresh();
            listStrobe.Remove((Strobe_Characteristic)table.SelectedItem);            
        }
        private void Time_start_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            time_start.Focusable = true;
        }
        private void Time_stop_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            time_stop.Focusable = true;
        }
        private void Ampl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ampl.Focusable = true;
        }
        private void Decrement_t_start_Click(object sender, RoutedEventArgs e)
        {
            DecrementClick(time_start, ref t_start);
        }
        private void Increment_t_start_Click(object sender, RoutedEventArgs e)
        {
            IncrementClick(time_start, ref t_start);
        }
        private void Decrement_t_stop_Click(object sender, RoutedEventArgs e)
        {
            DecrementClick(time_stop, ref t_stop);
        }
        private void Increment_t_stop_Click(object sender, RoutedEventArgs e)
        {
            IncrementClick(time_stop, ref t_stop);
        }
        private void Decrement_ampl_Click(object sender, RoutedEventArgs e)
        {
            DecrementClick(ampl, ref val_ampl);
        }
        private void Increment_ampl_Click(object sender, RoutedEventArgs e)
        {
            IncrementClick(ampl, ref val_ampl);
        }
        private void IncrementClick(TextBox textBox, ref int data)
        {
            int newValue = ++data;
            ValidateTime(ref newValue);
            textBox.Text = data.ToString();
        }
        private void DecrementClick(TextBox textBox, ref int data)
        {
            int newValue = --data;
            ValidateTime(ref newValue);
            textBox.Text = data.ToString();
        }
        private void Time_start_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        Increment_t_start_Click(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        Decrement_t_start_Click(sender, null);
                        break;
                    }               
            }
        }
        private void Time_start_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                Increment_t_start_Click(sender, null);
                increment_t_start.Focusable = true;
            }
            else if (e.Key == Key.Left)
            {
                Decrement_t_start_Click(sender, null);
                decrement_t_start.Focusable = true;
            }
        }
        private void Time_stop_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        Increment_t_stop_Click(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        Decrement_t_stop_Click(sender, null);
                        break;
                    }
            }
        }
        private void Time_stop_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                Increment_t_stop_Click(sender, null);
                increment_t_stop.Focusable = true;
            }
            else if (e.Key == Key.Left)
            {
                Decrement_t_stop_Click(sender, null);
                decrement_t_stop.Focusable = true;
            }
        }
        private void Ampl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        Increment_ampl_Click(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        Decrement_ampl_Click(sender, null);
                        break;
                    }
            }
        }
        private void Ampl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                Increment_ampl_Click(sender, null);
                increment_ampl.Focusable = true;
            }
            else if (e.Key == Key.Left)
            {
                Decrement_ampl_Click(sender, null);
                decrement_ampl.Focusable = true;
            }
        }      
    }    
}
