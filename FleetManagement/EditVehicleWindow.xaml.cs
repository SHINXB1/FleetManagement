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
    /// Логика взаимодействия для EditVehicleWindow.xaml
    /// </summary>
    public partial class EditVehicleWindow : Window
    {
        public Vehicles Vehicle { get; private set; }

        public EditVehicleWindow(Vehicles vehicle)
        {
            InitializeComponent();
            Vehicle = vehicle;
            LoadVehicleData();
        }

        private void LoadVehicleData()
        {
            LicensePlateTextBox.Text = Vehicle.LicensePlate;
            ModelTextBox.Text = Vehicle.Model;
            ManufacturerTextBox.Text = Vehicle.Manufacturer;
            YearOfManufactureTextBox.Text = Vehicle.YearOfManufacture.ToString();
            MileageTextBox.Text = Vehicle.Mileage.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Vehicle.LicensePlate = LicensePlateTextBox.Text;
            Vehicle.Model = ModelTextBox.Text;
            Vehicle.Manufacturer = ManufacturerTextBox.Text;
            Vehicle.YearOfManufacture = int.Parse(YearOfManufactureTextBox.Text);
            Vehicle.Mileage = int.Parse(MileageTextBox.Text);
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
