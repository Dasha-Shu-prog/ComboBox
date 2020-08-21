using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;

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
            List listStrobe = new List();
        public MainWindow()
        {
            InitializeComponent();
     
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
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
           // e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            num++;
            //         Strobe_Characteristic val = (Strobe_Characteristic) e.Row.DataContext
            //e.Row.Header = NUMB;
            //DataGrid dataGrid = sender as DataGrid;
            //Strobe_Characteristic itemValue = (Strobe_Characteristic) dataGrid.Items.GetItemAt(0);
            //Strobe_Characteristic currentValue = (Strobe_Characteristic)e.Row.DataContext;
            //currentValue.Number = e.Row.GetIndex() + 1;
            //dgr.Item = currentValue;
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Strobe_Characteristic strobe1 = new Strobe_Characteristic
            {
                //Number = num,
                Time_start = t_start,
                Time_stop = t_stop,
                Amplitude = val_ampl,
                Color = color_box.Text
            };

       

            table.Items.Add(strobe1);
            //listStrobe.ListItems.Add;
        }
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            table.Items.Remove(table.SelectedItem);
            table.Items.Refresh();
            listStrobe.ListItems.Remove((ListItem)table.SelectedItem);
         
        }
       // linegraph.Plot(x, y);
    }    
}
