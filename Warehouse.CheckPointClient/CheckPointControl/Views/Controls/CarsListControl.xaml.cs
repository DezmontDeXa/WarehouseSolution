using SharedLibrary.DataBaseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CheckPointControl.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для CarsListControl.xaml
    /// </summary>
    public partial class CarsListControl : UserControl
    {
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public Car SelectedCar
        {
            get { return (Car)GetValue(SelectedCarProperty); }
            set { SetValue(SelectedCarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(CarsListControl), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for SelectedCar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCarProperty =
            DependencyProperty.Register("SelectedCar", typeof(Car), typeof(CarsListControl), new PropertyMetadata(null));

        public int HeaderFontSize
        {
            get { return (int)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(int), typeof(CarsListControl), new PropertyMetadata(20));




        public CarsListControl()
        {
            InitializeComponent();
        }
    }
}
