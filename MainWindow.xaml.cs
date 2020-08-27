using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Configuration;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using InteractiveDataDisplay.WPF;
using Key = System.Windows.Input.Key;
using System.Collections.Specialized;
using System.Windows.Media.Effects;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Data;
using System.IO;
using System.ComponentModel;
using Table1.Properties;
using System.Text;

namespace Table1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int id = 0;
        private int t_start = 0;
        private int t_stop = 0;
        private int val_ampl = 0;
        const int MIN_TIME = 0;
        const int MAX_TIME = 180;
        const int MIN_AMPL = -15;
        const int MAX_AMPL = 15;
        List<StrobeCharacteristic> listStrobe = new List<StrobeCharacteristic>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Добавление синусоиды на график
            var y = Enumerable.Range(0, 2000).Select(i => i / 10.0).ToArray();
            var x = y.Select(v => Math.Sin(v + 100) * 10).ToArray();
            var lg = new LineGraph();
            linesPlot.Children.Add(lg);
            lg.Stroke = new SolidColorBrush(Colors.Black);
            lg.Plot(x, y);
            lg.Description = String.Format("Сигнал");
        }
        // Изменения значений в textbox
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
        private void TimeStartTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(timeStartTextBox, ref t_start, incrementTimeStartButton, decrementTimeStartButton);
        }
        private void TimeStopTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(timeStopTextBox, ref t_stop, incrementTimeStopButton, decrementTimeStopButton);
        }
        private void AmplitudeTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(amplTextBox, ref val_ampl, incrementAmplitudeButton, decrementAmplitudeButton, false);
        }
        // Проверка значений в textbox
        private void ValidateData(ref int value, bool isTime = true)
        {
            int minValidValue = isTime ? MIN_TIME : MIN_AMPL;
            int maxValidValue = isTime ? MAX_TIME : MAX_AMPL;
            if (value > maxValidValue)
                value = maxValidValue;

            if (value < minValidValue)
                value = minValidValue;
        }
        private void ValidateTime(ref int value)
        {
            ValidateData(ref value);
        }
        private void ValidateAmpl(ref int value)
        {
            ValidateData(ref value, false);
        }
        // Генерация строк
        private void Table_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        // Добавление строк в таблицу и строб на график
        private void PlusClick(object sender, RoutedEventArgs e)
        {
            if (t_start > t_stop)
            {
                string messageBoxText = "Время начала должно быть меньше времени конца!";
                string caption = "Ошибка";
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            StrobeCharacteristic strobe = new StrobeCharacteristic
            {
                Id = id++,
                Time_start = t_start,
                Time_stop = t_stop,
                Amplitude = val_ampl,
                Color = color_box.Text
            };
            listStrobe.Add(strobe);
            table.Items.Add(listStrobe.Last());
            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();
            xCoords.Add(strobe.Amplitude);
            yCoords.Add(strobe.Time_start);
            xCoords.Add(strobe.Amplitude);
            yCoords.Add(strobe.Time_stop);
            var line = new LineGraph
            {
                StrokeThickness = 3,
                //Description = $"Строб {listStrobe.Count}"
                //Description = $"Строб {table.Items.Count}"
            };
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
            //int count = listStrobe.Count;
            //for (int i = 1; i < listStrobe.Count; i++)
            //{
            //    line.Description = $"Строб {table.Items.Count}";
            //}
        }
        // Удаление строк и строб
        private void MinusClick(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
                return;

            int selectedId = ((StrobeCharacteristic)table.SelectedItem).Id;
            for (int row = 0; row < listStrobe.Count; ++row)
            {
                if (listStrobe.ElementAt(row).Id == selectedId)
                {
                    listStrobe.RemoveAt(row);
                    linesPlot.Children.RemoveAt(row + 1);
                    //linesPlot.Drop ;

                    break;
                }
            }
            table.Items.Remove(table.SelectedItem);
            table.Items.Refresh();
        }
        private void MouseDownClick(TextBox box)
        {
            box.Focusable = true;
        }
        private void TimeStartMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseDownClick(timeStartTextBox);
        }
        private void TimeStopMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseDownClick(timeStopTextBox);
        }
        private void AmplMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseDownClick(amplTextBox);
        }
        private void IncrementClick(TextBox textBox, ref int data)
        {
            int newValue = ++data;
            ValidateTime(ref newValue);
            textBox.Text = data.ToString();
        }
        private void IncrementTimeStartClick(object sender, RoutedEventArgs e)
        {
            IncrementClick(timeStartTextBox, ref t_start);
        }
        private void IncrementTimeStopClick(object sender, RoutedEventArgs e)
        {
            IncrementClick(timeStopTextBox, ref t_stop);
        }
        private void IncrementAmplitudeClick(object sender, RoutedEventArgs e)
        {
            IncrementClick(amplTextBox, ref val_ampl);
        }
        private void DecrementClick(TextBox textBox, ref int data)
        {
            int newValue = --data;
            ValidateTime(ref newValue);
            textBox.Text = data.ToString();
        }
        private void DecrementTimeStartClick(object sender, RoutedEventArgs e)
        {
            DecrementClick(timeStartTextBox, ref t_start);
        }
        private void DecrementTimeStopClick(object sender, RoutedEventArgs e)
        {
            DecrementClick(timeStopTextBox, ref t_stop);
        }
        private void DecrementAmplitudeClick(object sender, RoutedEventArgs e)
        {
            DecrementClick(amplTextBox, ref val_ampl);
        }
        private void TimestartPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        IncrementTimeStartClick(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        DecrementTimeStartClick(sender, null);
                        break;
                    }
            }
        }
        private void TimestartKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                IncrementTimeStartClick(sender, null);
                incrementTimeStartButton.Focusable = true;
            }
            else if (e.Key == Key.Left)
            {
                DecrementTimeStartClick(sender, null);
                decrementTimeStartButton.Focusable = true;
            }
        }
        private void TimestopPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        IncrementTimeStopClick(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        DecrementTimeStopClick(sender, null);
                        break;
                    }
            }
        }
        private void TimestopKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                IncrementTimeStopClick(sender, null);
                incrementTimeStopButton.Focusable = true;
            }
            else if (e.Key == Key.Left)
            {
                DecrementTimeStopClick(sender, null);
                decrementTimeStopButton.Focusable = true;
            }
        }
        private void AmplPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        IncrementAmplitudeClick(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        DecrementAmplitudeClick(sender, null);
                        break;
                    }
            }
        }
        private void AmplKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                IncrementAmplitudeClick(sender, null);
                incrementAmplitudeButton.Focusable = true;
            }
            else if (e.Key == Key.Left)
            {
                DecrementAmplitudeClick(sender, null);
                decrementAmplitudeButton.Focusable = true;
            }
        }
        private void LinesMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //var linegraph = linesPlot.Children.GetEnumerator().Current;
            //coordTextBlock.Text = "Время: " + linesPlot.Children.IndexOf(linesPlot) + " мкс " + " Амплитуда: " + 2 + "мВ";
        }
        //private void WindowClosed(object sender, EventArgs e)
        //{
        //    string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //    string project = appData;
        //}
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string projectPath = appDataPath + "\\project_label";
            string saveFilePath = projectPath + "\\save.txt";
            //byte[] res = UnicodeEncoding.Unicode(table.Items);
            //using (FileStream dataFile = File.Open(saveFilePath, FileMode.OpenOrCreate))
            //{
            //    dataFile.Seek(listStrobe.Count, SeekOrigin.End);
            //    await dataFile.WriteAsync(res, listStrobe.Count, res.Length);
            //}          
        }
        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    Settings.Default.Save();
        //    base.OnClosing(e);
        //}
    }
}
