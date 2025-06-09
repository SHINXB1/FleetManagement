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
    /// Логика взаимодействия для AddVehicleWindow.xaml
    /// </summary>
    public partial class AddVehicleWindow : Window
    {
        public Vehicles Vehicle { get; private set; }

        public AddVehicleWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //валидация
            if (string.IsNullOrWhiteSpace(LicensePlateTextBox.Text) ||
                string.IsNullOrWhiteSpace(ModelTextBox.Text) ||
                string.IsNullOrWhiteSpace(ManufacturerTextBox.Text) ||
                !int.TryParse(YearOfManufactureTextBox.Text, out int year) ||
                !int.TryParse(MileageTextBox.Text, out int mileage) ||
                StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields correctly.");
                return;
            }

            //создание об Vehicle
            Vehicle = new Vehicles
            {
                LicensePlate = LicensePlateTextBox.Text,
                Model = ModelTextBox.Text,
                Manufacturer = ManufacturerTextBox.Text,
                YearOfManufacture = year,
                Mileage = mileage,
                Status = (StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString()
            };

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
