using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            const int MIN_AMPL = -50;
            const int MAX_AMPL = 50;

            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();

        List<Strobe_Characteristic> listStrobe = new List<Strobe_Characteristic>();
            Strobe_Characteristic strobe1;
        public MainWindow()
        {
            InitializeComponent();
            linegraph.Plot(xCoords, yCoords);
        }

        private void AddToGraph(int time, int aplitude)
        {
            xCoords.Add(aplitude);
            yCoords.Add(time);
            linegraph.Plot(xCoords, yCoords);
        }

        private void Time_start_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(time_start.Text, out int result_t_start))
            {
                ValidateData(ref result_t_start);
                t_start = result_t_start;
            }
             time_start.Text = t_start.ToString();
        }
        private void Time_stop_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(time_stop.Text, out int result_t_stop))
            {
                ValidateData(ref result_t_stop);
                t_stop = result_t_stop;
            }
             time_stop.Text = t_stop.ToString();
        }
        private void Ampl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(ampl.Text, out int result_ampl))
            {
                ValidateDataAmpl(ref result_ampl);
                val_ampl = result_ampl;
            }
              ampl.Text = val_ampl.ToString();
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
        private int num = 1;
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
            num++;
            listStrobe.Add(strobe1);
            //table.Items.Add(strobe1);
            table.Items.Add(listStrobe.Last());
            AddToGraph(strobe1.Time_start, strobe1.Amplitude);
            AddToGraph(strobe1.Time_stop, strobe1.Amplitude);
            Console.WriteLine(listStrobe);
        }
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            table.Items.Remove(table.SelectedItem);           
            table.Items.Refresh();
            listStrobe.Remove((Strobe_Characteristic)table.SelectedItem);        
            num--;
        }
    }    
}
