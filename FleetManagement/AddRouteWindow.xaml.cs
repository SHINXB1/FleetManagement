using FleetManagement.Models;
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
using System.Windows.Shapes;

namespace FleetManagement
{
    /// <summary>
    /// Логика взаимодействия для AddRouteWindow.xaml
    /// </summary>
    public partial class AddRouteWindow : Window
    {
        public Routes Route { get; private set; }

        public AddRouteWindow()
        {
            InitializeComponent();
            Route = new Routes();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Route.VehicleId = int.Parse(VehicleIdTextBox.Text);
            Route.DriverId = int.Parse(DriverIdTextBox.Text);
            Route.StartDate = DateTime.Parse(StartDateTextBox.Text);
            Route.EndDate = DateTime.TryParse(EndDateTextBox.Text, out var endDate) ? endDate : (DateTime?)null;
            Route.StartLocation = StartLocationTextBox.Text;
            Route.EndLocation = EndLocationTextBox.Text;
            Route.Distance = float.Parse(DistanceTextBox.Text);
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
