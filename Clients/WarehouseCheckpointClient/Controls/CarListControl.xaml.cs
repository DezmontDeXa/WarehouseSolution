using SharedLibrary.DataBaseModels;
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

namespace WarehouseCheckpointClient.Controls
{
    /// <summary>
    /// Логика взаимодействия для CarListControl.xaml
    /// </summary>
    public partial class CarListControl : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CarListControl), new PropertyMetadata(""));

        public IEnumerable<Car> Cars
        {
            get { return (IEnumerable<Car>)GetValue(CarsProperty); }
            set { SetValue(CarsProperty, value); }
        }

        public static readonly DependencyProperty CarsProperty =
            DependencyProperty.Register("Cars", typeof(IEnumerable<Car>), typeof(CarListControl), new PropertyMetadata(null));


        public CarListControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
