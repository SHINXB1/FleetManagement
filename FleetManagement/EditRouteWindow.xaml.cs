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
    /// Логика взаимодействия для EditRouteWindow.xaml
    /// </summary>
    public partial class EditRouteWindow : Window
    {
        public Routes Route { get; private set; }

        public EditRouteWindow(Routes route)
        {
            InitializeComponent();
            Route = route;

            //заполнение текст полей данными маршрута
            VehicleIdTextBox.Text = Route.VehicleId.ToString();
            DriverIdTextBox.Text = Route.DriverId.ToString();
            StartDateTextBox.Text = Route.StartDate.ToString("yyyy-MM-dd");

            //проверка на есть ли EndDate
            EndDateTextBox.Text = Route.EndDate.HasValue ? Route.EndDate.Value.ToString("yyyy-MM-dd") : string.Empty;

            StartLocationTextBox.Text = Route.StartLocation;
            EndLocationTextBox.Text = Route.EndLocation;
            DistanceTextBox.Text = Route.Distance.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //обновляются данные маршрута с учетом значений из текстовых полей
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
