using System.Collections.Generic;
using System.Windows;

namespace CMCS
{
    public partial class ViewClaimsWindow : Window
    {
        private Class1 repository;

        public ViewClaimsWindow()
        {
            InitializeComponent();
            repository = new Class1(); // Initialize the repository

            // Load the claims and display them in the DataGrid
            LoadClaims();
        }

        // Method to load the claims and bind them to the DataGrid
        private void LoadClaims()
        {
            List<Claim> claims = repository.GetClaims(); // Retrieve claims from repository
            ClaimsDataGrid.ItemsSource = claims; // Bind the claims to the DataGrid
        }

        // Event handler for the Back button to return to the Lecturer Window
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LecturerWindow lecturerWindow = new LecturerWindow();
            lecturerWindow.Show();
            this.Close(); // Close the current window
        }
    }
}
