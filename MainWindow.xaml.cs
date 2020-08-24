using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Key = System.Windows.Input.Key;

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
            const int MIN_AMPL = -20;
            const int MAX_AMPL = 20;

            //List<int> xCoords = new List<int>();
            //List<int> yCoords = new List<int>();

            List<Strobe_Characteristic> listStrobe = new List<Strobe_Characteristic>();
            Strobe_Characteristic strobe1;
            //line.IComponentConnector.li
        public MainWindow()
        {
            InitializeComponent();
            
            //linegraph.Plot(xCoords, yCoords);
        }
        //private void AddToGraph(int time, int aplitude)
        //{
        //    xCoords.Add(aplitude);
        //    yCoords.Add(time);
        //    Point pointTest = new Point(aplitude, time);
        //    linegraph.Points.Add(pointTest);
        //    linegraph.Plot(xCoords, yCoords);
        //    linegraph.LayoutTransform = new LineSegment(amplitude, time);
        //}
        private void Time_start_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(time_start.Text, out int result_t_start))
            {
                ValidateData(ref result_t_start);
                t_start = result_t_start;
            }
             time_start.Text = t_start.ToString();
            if (t_start == MIN_TIME)
                decrement_t_start.IsEnabled = false;

            else if (t_start == MAX_TIME)
                increment_t_start.IsEnabled = false;

            else
            {
                decrement_t_start.IsEnabled = true;
                increment_t_start.IsEnabled = true;
            }
        }
        private void Time_stop_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(time_stop.Text, out int result_t_stop))
            {
                ValidateData(ref result_t_stop);
                t_stop = result_t_stop;
            }
             time_stop.Text = t_stop.ToString();
            if (t_stop == MIN_TIME)
                decrement_t_stop.IsEnabled = false;

            else if (t_start == MAX_TIME)
                increment_t_stop.IsEnabled = false;

            else
            {
                decrement_t_stop.IsEnabled = true;
                increment_t_stop.IsEnabled = true;
            }
        }
        private void Ampl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(ampl.Text, out int result_ampl))
            {
                ValidateDataAmpl(ref result_ampl);
                val_ampl = result_ampl;
            }
              ampl.Text = val_ampl.ToString();
            if (val_ampl == MIN_AMPL)
                decrement_ampl.IsEnabled = false;

            else if (val_ampl == MAX_AMPL)
                increment_ampl.IsEnabled = false;

            else
            {
                decrement_ampl.IsEnabled = true;
                increment_ampl.IsEnabled = true;
            }
        }
        private void ValidateData(ref int value)
        {
            if (value > MAX_TIME)
                value = MAX_TIME;
            if (value < MIN_TIME)
                value = MIN_TIME;
            //if (t_start > t_stop)
               // MessageBox.Show("Время начала должно быть меньше времени конца!");
        }   
        private void ValidateDataAmpl(ref int value)
        {
            if (value > MAX_AMPL)
                value = MAX_AMPL;
            if (value < MIN_AMPL)
                value = MIN_AMPL;
        }
        //private int num = 1;
        private void Table_LoadingRow(object sender, DataGridRowEventArgs e)
        {         
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            strobe1 = new Strobe_Characteristic 
            {
                //Number = num,
                Time_start = t_start,
                Time_stop = t_stop,
                Amplitude = val_ampl,
                Color = color_box.Text
            };
            listStrobe.Add(strobe1);            
            table.Items.Add(listStrobe.Last());
            //AddToGraph(strobe1.Time_start, strobe1.Amplitude);
            //AddToGraph(strobe1.Time_stop, strobe1.Amplitude);
            //Console.WriteLine(listStrobe);
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
            int newTimeStart = --t_start;
            ValidateData(ref newTimeStart);
            time_start.Text = t_start.ToString();
        }
        private void Increment_t_start_Click(object sender, RoutedEventArgs e)
        {
            int newTimeStart = ++t_start;
            ValidateData(ref newTimeStart);
            time_start.Text = t_start.ToString();
        }
        private void Decrement_t_stop_Click(object sender, RoutedEventArgs e)
        {
            int newTimeStop = --t_stop;
            ValidateData(ref newTimeStop);
            time_stop.Text = t_stop.ToString();
        }
        private void Increment_t_stop_Click(object sender, RoutedEventArgs e)
        {
            int newTimeStop = ++t_stop;
            ValidateData(ref newTimeStop);
            time_stop.Text = t_stop.ToString();
        }
        private void Decrement_ampl_Click(object sender, RoutedEventArgs e)
        {
            int newAmplitude = --val_ampl;
            ValidateData(ref newAmplitude);
            ampl.Text = val_ampl.ToString();
        }
        private void Increment_ampl_Click(object sender, RoutedEventArgs e)
        {
            int newAmplitude = ++val_ampl;
            ValidateData(ref newAmplitude);
            ampl.Text = val_ampl.ToString();
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
