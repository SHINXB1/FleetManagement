﻿using FleetManagement.Models;
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
    /// Логика взаимодействия для EditDriverWindow.xaml
    /// </summary>
    public partial class EditDriverWindow : Window
    {
        public Drivers Driver { get; private set; }

        public EditDriverWindow(Drivers driver)
        {
            InitializeComponent();
            Driver = driver;
            LoadDriverData();
        }

        private void LoadDriverData()
        {
            FullNameTextBox.Text = Driver.FullName;
            LicenseNumberTextBox.Text = Driver.LicenseNumber;
            PhoneNumberTextBox.Text = Driver.PhoneNumber;
            ExperienceTextBox.Text = Driver.Experience.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Driver.FullName = FullNameTextBox.Text;
            Driver.LicenseNumber = LicenseNumberTextBox.Text;
            Driver.PhoneNumber = PhoneNumberTextBox.Text;
            Driver.Experience = int.Parse(ExperienceTextBox.Text);
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
