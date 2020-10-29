using System;
using System.IO;
using System.Text;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using InteractiveDataDisplay.WPF;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Threading.Tasks;

namespace Table1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int id = 0;
        private int t_start = 0;
        private int t_stop = 0;
        private int val_ampl = 0;
        StrobeCharacteristic strobe;
        LineGraph signal;
        LineGraph line;
        //readonly CheckBoxList checkBoxLegendGraph = new CheckBoxList();
        List<StrobeCharacteristic> listStrobe = new List<StrobeCharacteristic>();        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Добавление синусоиды на график
            double[] y = Enumerable.Range(0, 2000).Select(i => i / 10.0).ToArray();
            double[] x = y.Select(v => Math.Sin(v + 100) * 10).ToArray();
            signal = new LineGraph();
            linesPlot.Children.Add(signal);
            signal.Stroke = new SolidColorBrush(Colors.Black);
            signal.StrokeThickness = 2;
            signal.Plot(x, y);
            signal.Description = String.Format("Сигнал");
            MinMaxLineMove();
            //Task.Run(() => TaskUpdateMinMaxLine());
            //MinMaxGraphControl graphControl = new MinMaxGraphControl();
            //Binding bindingLeftLine = new Binding();
            //bindingLeftLine.Source = graphControl.LowerSlider;
            //bindingLeftLine.Path = new PropertyPath("ElementPosition");
            //bindingLeftLine.Mode = BindingMode.TwoWay;
            //leftLine.SetBinding(Canvas.LeftProperty, bindingLeftLine);
            //leftLine.MouseLeftButtonDown += LeftLine_MouseDown;
            //leftLine.MouseLeftButtonUp += LeftLine_MouseUp;
            //leftLine.MouseMove += LeftLine_MouseMove;
            //List<int> xLineMin = new List<int>();
            //List<int> yLineMin = new List<int>();
            //xLineMin.Add(-16);
            //yLineMin.Add((int)plotter.PlotOriginY);
            //xLineMin.Add(-16);
            //yLineMin.Add((int)plotter.PlotHeight);
            //var lineMin = new LineGraph();            
            //lineMin.Stroke = new SolidColorBrush(Colors.MediumBlue);
            //lineMin.StrokeThickness = 2;
            //lineMin.Plot(xLineMin, yLineMin);
            //linesPlot.Children.Add(lineMin);
            ConfigLoad(); 
        }
        //private void LeftLine_MouseDown(object sender, MouseButtonEventArgs mouseArgs)
        //{
        //    var item = sender as FrameworkElement;
            
        //}
        //private void LeftLine_MouseUp(object sender, MouseButtonEventArgs mouseArgs)
        //{

        //}
        //private void LeftLine_MouseMove(object sender, MouseButtonEventArgs mouseArgs)
        //{

        //}
        //private void Bind ()
        //{
        //    MinMaxGraphControl graphControl = new MinMaxGraphControl();
        //    Binding bindingLeftLine = new Binding();
        //    bindingLeftLine.Source = graphControl.LowerSlider;
        //    bindingLeftLine.Path = new PropertyPath("Canvas.SetLeft");
        //    bindingLeftLine.Mode = BindingMode.TwoWay;
        //    leftLine.SetBinding(Canvas.LeftProperty, bindingLeftLine);
        //}

        //private void TaskUpdateMinMaxLine()
        //{
        //    MinMaxGraphControl minMaxGraphControl = new MinMaxGraphControl();
        //    if (minMaxGraphControl.LowerSlider. == true)
        //    leftLine.Dispatcher.Invoke(new Action(() =>
        //    {
        //        leftLine.Visibility = Visibility;
        //        leftLine.Margin = new Thickness(258, 0, 0, 0);
        //    }));
        //    rightLine.Dispatcher.Invoke(new Action(() =>
        //    {
        //        rightLine.Visibility = Visibility;
        //        rightLine.Margin = new Thickness(0, 0, 208, 0);
        //    }));
        //}
        private void MinMaxLineMove ()
        {
            //bool moveLine = true;
            MinMaxGraphControl minMaxGraphControl = new MinMaxGraphControl();
            //minMaxGraphControl.LowerSlider.ValueChanged += LeftLineMove;
            //if(moveLine)
            //{
                
            //}
            minMaxGraphControl.LowerValue -= 1;
            leftLine.Margin = new Thickness(258, 0, 0, 0);
            minMaxGraphControl.UpperValue += 1;
            rightLine.Margin = new Thickness(0, 0, 208, 0);
        }
        //private void LeftLineMove(object sender, RoutedEventArgs eventArgs)
        //{
            
        //}
        private void FillLineGraph(StrobeCharacteristic strobe)
        {
            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();
            xCoords.Add(strobe.Amplitude);
            yCoords.Add(strobe.Time_start);
            xCoords.Add(strobe.Amplitude);
            yCoords.Add(strobe.Time_stop);
            line = new LineGraph();
            line.StrokeThickness = 3;
            line.Description = "Строб " + listStrobe.Count;
            line.Plot(xCoords, yCoords);
            linesPlot.Children.Add(line);
            switch (strobe.Color)
            {
                case "Красный":
                    {
                        line.Stroke = new SolidColorBrush(Colors.Firebrick);
                        break;
                    }
                case "Синий":
                    {
                        line.Stroke = new SolidColorBrush(Colors.DarkBlue);
                        break;
                    }
                case "Зелёный":
                    {
                        line.Stroke = new SolidColorBrush(Colors.DarkGreen);
                        break;
                    }
                case "Оранжевый":
                    {
                        line.Stroke = new SolidColorBrush(Colors.DarkOrange);
                        break;
                    }
                case "Фиолетовый":
                    {
                        line.Stroke = new SolidColorBrush(Colors.DarkMagenta);
                        break;
                    }
                case "Другой цвет":
                    {
                        line.Stroke = new SolidColorBrush(Colors.BlueViolet);
                        break;
                    }
            }
        }
        // Загрузка конфигурации
        private void ConfigLoad()
        {
            string path = @"C:\Users\Админ\Desktop\Projects\StrobeProject-Table-\dataFile.txt";
            //string path = @"C:\Users\butterfly\Desktop\Projects\Table1\ComboBox\datafile.txt";        
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string lineRow;
                for (int index = id; ; index++)
                {
                    strobe = new StrobeCharacteristic();
                    if ((lineRow = sr.ReadLine()) == null)
                        return;

                    strobe.Id = id++;

                    if ((lineRow = sr.ReadLine()) != null)
                    {
                        if (Int32.TryParse(lineRow, out int time_start))
                            strobe.Time_start = time_start;
                        else
                            return;
                    }
                    if ((lineRow = sr.ReadLine()) != null)
                    {
                        if (Int32.TryParse(lineRow, out int time_stop))
                            strobe.Time_stop = time_stop;
                        else
                            return;
                    }
                    if ((lineRow = sr.ReadLine()) != null)
                    {
                        if (Int32.TryParse(lineRow, out int amplitude))
                            strobe.Amplitude = amplitude;
                        else
                            return;
                    }
                    if ((lineRow = sr.ReadLine()) != null)
                    {
                        strobe.Color = lineRow;
                    }
                    listStrobe.Insert(index, strobe);
                    table.Items.Insert(index, strobe);
                    FillLineGraph(strobe);
                }
            }
        }
        // Генерация строк
        private void Table_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        // Добавление строк в таблицу и строб на график
        private void PlusClick(object sender, RoutedEventArgs e)
        {
            t_start = int.Parse(timeStart.textBox.Text);
            t_stop = int.Parse(timeStop.textBox.Text);
            val_ampl = int.Parse(amplit.textBox.Text);
            if (t_start >= t_stop)
            {
                string messageBoxText = "Время начала должно быть меньше времени конца!";
                string caption = "Ошибка";
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            strobe = new StrobeCharacteristic
            {
                //Id = id++,
                Id = listStrobe.Count + id++,
                Time_start = t_start,
                Time_stop = t_stop,
                Amplitude = val_ampl,
                Color = color_box.Text
            };
            // Проверка уникальности строб
            bool isUnique = true;
            for (int i = 0; i < listStrobe.Count; i++)
            {
                if (listStrobe.ElementAt(i).Color == strobe.Color)
                {
                    string messageBoxText = "Строб такого цвета уже существует!\nВыберете любой другой цвет!";
                    string caption = "Предупреждение";
                    MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (listStrobe.ElementAt(i).Time_start == strobe.Time_start &&
                    listStrobe.ElementAt(i).Time_stop == strobe.Time_stop &&
                    listStrobe.ElementAt(i).Amplitude == strobe.Amplitude &&
                    listStrobe.ElementAt(i).Color == strobe.Color)
                {
                    isUnique = false;
                    break;
                }
            }
            if (isUnique)
            {
                listStrobe.Add(strobe);
                table.Items.Add(listStrobe.Last());
                FillLineGraph(strobe);
            }
        }
        // Удаление строк и строб
        private void MinusClick(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
                return;

            int selectedId = ((StrobeCharacteristic)table.SelectedItem).Id;
            for (int row = 0; row < listStrobe.Count; row++)
            {
                if (listStrobe.ElementAt(row).Id == selectedId)
                {
                    listStrobe.RemoveAt(row);
                    linesPlot.Children.RemoveAt(row + 1);
                    break;
                }
            }
            table.Items.Remove(table.SelectedItem);
            table.Items.Refresh();
            int count = 0;
            var enumerator = linesPlot.Children.GetEnumerator();
            if (enumerator.MoveNext())
            {
                while (enumerator.MoveNext())
                {
                    LineGraph graph = (LineGraph)enumerator.Current;
                    graph.Description = $"Строб {++count}";
                }
            }
        }
        // Подсказка с координатами
        private void LineMouseDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(linesPlot);
            var heightY = plotter.ActualHeight - position.Y;
            var widthX = plotter.ActualWidth - position.X;
            var realHeightY = heightY * plotter.PlotHeight / plotter.ActualHeight;
            var realWidthX = widthX * plotter.PlotWidth / plotter.ActualWidth;
            int y = (int)realHeightY - 11;
            int x = (int)realWidthX - 27;
            plotter.ToolTip = $"Время: " + y + " мкс\n Амплитуда: " + x + " мВ";
        }
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Запись строб в файл
            string path = @"C:\Users\Админ\Desktop\Projects\StrobeProject-Table-\dataFile.txt";
            //string path = @"C:\Users\butterfly\Desktop\Projects\Table1\ComboBox\datafile.txt";            
            using (StreamWriter dataFile = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (var strobe in listStrobe)
                {
                    dataFile.WriteLine(strobe.Id);
                    dataFile.WriteLine(strobe.Time_start);
                    dataFile.WriteLine(strobe.Time_stop);
                    dataFile.WriteLine(strobe.Amplitude);
                    dataFile.WriteLine(strobe.Color);
                }
            }
        }
        private void LegendGraph_Click(object sender, RoutedEventArgs e)
        {
            if (LegendGraph.IsChecked == true)
            {
                plotter.LegendVisibility = Visibility.Hidden;
                LegendGraph.Content = "Показать легенду";
            }
            else
            {
                plotter.LegendVisibility = Visibility.Visible;
                LegendGraph.Content = "Скрыть легенду";
            }
        }

        //private void SliderGraph_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    leftLine.AllowDrop = true;
        //    leftLine.FlowDirection = FlowDirection.RightToLeft;
        //    rightLine.AllowDrop = true;
        //    rightLine.FlowDirection = FlowDirection.LeftToRight;
        //}
        //private void CheckBoxLegendItems_Indeterminate(object sender, RoutedEventArgs e)
        //{
        //switch (((ContentControl)sender).Content)
        //{
        //    case "Строб Красный":
        //        {
        //            line.Visibility = Visibility.Visible;
        //            signal.Stroke = new SolidColorBrush(Colors.White);
        //            double[] y1 = Enumerable.Range(strobe.Time_start, 2000).Select(i => i / 10.0).ToArray();
        //            double[] x1 = y1.Select(v => Math.Sin(v + 100) * 10).ToArray();
        //            LineGraph signal1 = new LineGraph();
        //            linesPlot.Children.Add(signal1);
        //            signal1.Description = "Сигнал";
        //            signal1.Stroke = new SolidColorBrush(Colors.Black);
        //            signal1.StrokeThickness = 2;
        //            signal1.Plot(x1, y1);
        //            break;
        //        }
        //    case "Строб Синий":
        //        {
        //            line.Visibility = Visibility.Visible;
        //            signal.Stroke = new SolidColorBrush(Colors.White);
        //            double[] y1 = Enumerable.Range(strobe.Time_start, 2000).Select(i => i / 10.0).ToArray();
        //            double[] x1 = y1.Select(v => Math.Sin(v + 100) * 10).ToArray();
        //            LineGraph signal1 = new LineGraph();
        //            linesPlot.Children.Add(signal1);
        //            signal1.Description = "Сигнал";
        //            signal1.Stroke = new SolidColorBrush(Colors.Black);
        //            signal1.StrokeThickness = 2;
        //            signal1.Plot(x1, y1);
        //            break;
        //        }
        //    case "Строб Зелёный":
        //        {
        //            line.Visibility = Visibility.Visible;
        //            signal.Stroke = new SolidColorBrush(Colors.White);
        //            double[] y1 = Enumerable.Range(strobe.Time_start, 2000).Select(i => i / 10.0).ToArray();
        //            double[] x1 = y1.Select(v => Math.Sin(v + 100) * 10).ToArray();
        //            LineGraph signal1 = new LineGraph();
        //            linesPlot.Children.Add(signal1);
        //            signal1.Description = "Сигнал";
        //            signal1.Stroke = new SolidColorBrush(Colors.Black);
        //            signal1.StrokeThickness = 2;
        //            signal1.Plot(x1, y1);
        //            break;
        //        }
        //    case "Строб Оранжевый":
        //        {
        //            line.Visibility = Visibility.Visible;
        //            signal.Stroke = new SolidColorBrush(Colors.White);
        //            double[] y1 = Enumerable.Range(strobe.Time_start, 2000).Select(i => i / 10.0).ToArray();
        //            double[] x1 = y1.Select(v => Math.Sin(v + 100) * 10).ToArray();
        //            LineGraph signal1 = new LineGraph();
        //            linesPlot.Children.Add(signal1);
        //            signal1.Description = "Сигнал";
        //            signal1.Stroke = new SolidColorBrush(Colors.Black);
        //            signal1.StrokeThickness = 2;
        //            signal1.Plot(x1, y1);
        //            break;
        //        }
        //    case "Строб Фиолетовый":
        //        {
        //            line.Visibility = Visibility.Visible;
        //            signal.Stroke = new SolidColorBrush(Colors.White);
        //            double[] y1 = Enumerable.Range(strobe.Time_start, 2000).Select(i => i / 10.0).ToArray();
        //            double[] x1 = y1.Select(v => Math.Sin(v + 100) * 10).ToArray();
        //            LineGraph signal1 = new LineGraph();
        //            linesPlot.Children.Add(signal1);
        //            signal1.Description = "Сигнал";
        //            signal1.Stroke = new SolidColorBrush(Colors.Black);
        //            signal1.StrokeThickness = 2;
        //            signal1.Plot(x1, y1);
        //            break;
        //        }
        //    case "Строб Другой цвет":
        //        {
        //            line.Visibility = Visibility.Visible;
        //            signal.Stroke = new SolidColorBrush(Colors.White);
        //            double[] y1 = Enumerable.Range(strobe.Time_start, 2000).Select(i => i / 10.0).ToArray();
        //            double[] x1 = y1.Select(v => Math.Sin(v + 100) * 10).ToArray();
        //            LineGraph signal1 = new LineGraph();
        //            linesPlot.Children.Add(signal1);
        //            signal1.Description = "Сигнал";
        //            signal1.Stroke = new SolidColorBrush(Colors.Black);
        //            signal1.StrokeThickness = 2;
        //            signal1.Plot(x1, y1);
        //            break;
        //        }
        //}
        //}
    }
    public class VisibilityToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return ((Visibility)value) == Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
              return null;

            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}