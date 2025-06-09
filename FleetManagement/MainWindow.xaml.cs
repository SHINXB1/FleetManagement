using FleetManagement.Models;
using FleetManagement.Service;
using System;
using System.Linq;
using System.Windows;

namespace FleetManagement
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VehicleService _vehicleService = new VehicleService();
        private DriverService _driverService = new DriverService();
        private RouteService _routeService = new RouteService();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            VehiclesListBox.ItemsSource = _vehicleService.GetAllVehicles();
            DriversListBox.ItemsSource = _driverService.GetAllDrivers();
            RoutesListBox.ItemsSource = _routeService.GetAllRoutes();

            // заполнение фильтров транспортными средствами и водителями
            VehicleFilterComboBox.ItemsSource = _vehicleService.GetAllVehicles();
            VehicleFilterComboBox.DisplayMemberPath = "Model";
            VehicleFilterComboBox.SelectedValuePath = "Id";

            DriverFilterComboBox.ItemsSource = _driverService.GetAllDrivers();
            DriverFilterComboBox.DisplayMemberPath = "FullName";
            DriverFilterComboBox.SelectedValuePath = "Id";
        }

        // методы для работы с транспортом
        private void AddVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            var addVehicleWindow = new AddVehicleWindow();
            if (addVehicleWindow.ShowDialog() == true)
            {
                _vehicleService.AddVehicle(addVehicleWindow.Vehicle);
                LoadData();
            }
        }

        private void EditVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            if (VehiclesListBox.SelectedItem is Vehicles selectedVehicle)
            {
                var editVehicleWindow = new EditVehicleWindow(selectedVehicle);
                if (editVehicleWindow.ShowDialog() == true)
                {
                    _vehicleService.UpdateVehicle(selectedVehicle);
                    LoadData();
                }
            }
        }

        private void DeleteVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            if (VehiclesListBox.SelectedItem is Vehicles selectedVehicle)
            {
                _vehicleService.DeleteVehicle(selectedVehicle.Id);
                LoadData();
            }
        }

        // методы для работы с водителями
        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            var addDriverWindow = new AddDriverWindow();
            if (addDriverWindow.ShowDialog() == true)
            {
                _driverService.AddDriver(addDriverWindow.Driver);
                LoadData();
            }
        }

        private void EditDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (DriversListBox.SelectedItem is Drivers selectedDriver)
            {
                var editDriverWindow = new EditDriverWindow(selectedDriver);
                if (editDriverWindow.ShowDialog() == true)
                {
                    _driverService.UpdateDriver(selectedDriver);
                    LoadData();
                }
            }
        }

        private void DeleteDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (DriversListBox.SelectedItem is Drivers selectedDriver)
            {
                _driverService.DeleteDriver(selectedDriver.Id);
                LoadData();
            }
        }

        // методы для работы с маршрутами
        private void AddRouteButton_Click(object sender, RoutedEventArgs e)
        {
            var addRouteWindow = new AddRouteWindow();
            if (addRouteWindow.ShowDialog() == true)
            {
                _routeService.AddRoute(addRouteWindow.Route);
                LoadData();
            }
        }

        private void EditRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoutesListBox.SelectedItem is Routes selectedRoute)
            {
                var editRouteWindow = new EditRouteWindow(selectedRoute);
                if (editRouteWindow.ShowDialog() == true)
                {
                    _routeService.UpdateRoute(selectedRoute);
                    LoadData();
                }
            }
        }

        private void DeleteRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoutesListBox.SelectedItem is Routes selectedRoute)
            {
                _routeService.DeleteRoute(selectedRoute.Id);
                LoadData();
            }
        }

        //  фильтрация маршрутов
        private void FilterRoutesButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime.TryParse(DateFilterTextBox.Text, out DateTime filterDate);
            int vehicleId = VehicleFilterComboBox.SelectedValue != null ? (int)VehicleFilterComboBox.SelectedValue : 0;
            int driverId = DriverFilterComboBox.SelectedValue != null ? (int)DriverFilterComboBox.SelectedValue : 0;

            var filteredRoutes = _routeService.GetAllRoutes()
                .Where(r => (filterDate == default || r.StartDate.Date == filterDate.Date) &&
                            (vehicleId == 0 || r.VehicleId == vehicleId) &&
                            (driverId == 0 || r.DriverId == driverId))
                .ToList();

            RoutesListBox.ItemsSource = filteredRoutes;
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            DateFilterTextBox.Text = string.Empty;
            VehicleFilterComboBox.SelectedIndex = -1;
            DriverFilterComboBox.SelectedIndex = -1;

            RoutesListBox.ItemsSource = _routeService.GetAllRoutes();
        }

        //  фильтрация водителей
        private void FilterDriversButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameFilterTextBox.Text.ToLower();
            string licenseNumber = LicenseFilterTextBox.Text.ToLower();
            int.TryParse(ExperienceFilterTextBox.Text, out int experience);

            var filteredDrivers = _driverService.GetAllDrivers()
                .Where(d => (string.IsNullOrWhiteSpace(fullName) || d.FullName.ToLower().Contains(fullName)) &&
                            (string.IsNullOrWhiteSpace(licenseNumber) || d.LicenseNumber.ToLower().Contains(licenseNumber)) &&
                            (experience == 0 || d.Experience == experience))
                .ToList();

            DriversListBox.ItemsSource = filteredDrivers;
        }

        private void ResetDriversFilterButton_Click(object sender, RoutedEventArgs e)
        {
            FullNameFilterTextBox.Text = string.Empty;
            LicenseFilterTextBox.Text = string.Empty;
            ExperienceFilterTextBox.Text = string.Empty;
            DriversListBox.ItemsSource = _driverService.GetAllDrivers();
        }

        // фильтрация транспортных средств
        private void FilterVehiclesButton_Click(object sender, RoutedEventArgs e)
        {
            string model = ModelFilterTextBox.Text.ToLower();
            string manufacturer = ManufacturerFilterTextBox.Text.ToLower();
            string status = StatusFilterComboBox.SelectedItem != null ? StatusFilterComboBox.SelectedItem.ToString() : "";

            var filteredVehicles = _vehicleService.GetAllVehicles()
                .Where(v => (string.IsNullOrWhiteSpace(model) || v.Model.ToLower().Contains(model)) &&
                            (string.IsNullOrWhiteSpace(manufacturer) || v.Manufacturer.ToLower().Contains(manufacturer)) &&
                            (string.IsNullOrWhiteSpace(status) || v.Status == status))
                .ToList();

            VehiclesListBox.ItemsSource = filteredVehicles;
        }

        private void ResetVehiclesFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ModelFilterTextBox.Text = string.Empty;
            ManufacturerFilterTextBox.Text = string.Empty;
            StatusFilterComboBox.SelectedIndex = -1;
            VehiclesListBox.ItemsSource = _vehicleService.GetAllVehicles();
        }
    }
}
