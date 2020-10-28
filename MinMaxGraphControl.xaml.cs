using System;
using System.Collections.Generic;
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

namespace Table1
{
    /// <summary>
    /// Логика взаимодействия для MinMaxGraphControl.xaml
    /// </summary>
    public partial class MinMaxGraphControl : UserControl
    {
        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(MinMaxGraphControl), new UIPropertyMetadata(0));

        public int LowerValue
        {
            get { return (int)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(int), typeof(MinMaxGraphControl), new UIPropertyMetadata(0));

        public int UpperValue
        {
            get { return (int)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(int), typeof(MinMaxGraphControl), new UIPropertyMetadata(0));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(MinMaxGraphControl), new UIPropertyMetadata(0));
        public MinMaxGraphControl()
        {
            InitializeComponent();
            //this.Loaded += MinMaxGraphControl_Loaded;
        }
        void MinMaxGraphControl_Loaded(object sender, RoutedEventArgs e)
        {
            //LowerSlider.ValueChanged += LowerSlider_ValueChanged;
            //UpperSlider.ValueChanged += UpperSlider_ValueChanged;
        }

        private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            UpperSlider.Value = Math.Round(UpperSlider.Value, 0);
        }

        private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            LowerSlider.Value = Math.Round(UpperSlider.Value, 0);
        }
    }
}
