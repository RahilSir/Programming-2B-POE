using Microsoft.Win32; // For file dialog
using System;
using System.Windows;

namespace CMCS
{
    public partial class LecturerWindow : Window
    {
        private string supportingDocumentPath; // Store the path of the uploaded document

        public LecturerWindow()
        {
            InitializeComponent();
        }

        // Event handler for the "Browse" button to upload a file
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*",
                Title = "Select Supporting Document"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                supportingDocumentPath = openFileDialog.FileName; // Store the file path
                DocumentPathTextBlock.Text = supportingDocumentPath; // Display the file path
            }
        }

        // Event handler for input changes in HoursWorkedTextBox and HourlyRateTextBox
        private void OnInputChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CalculateTotalClaim(); // Call the method to calculate the total claim in real-time
        }

        // Calculate and display the total claim in real-time
        private void CalculateTotalClaim()
        {
            // Check if input values for hours worked and hourly rate are valid numbers
            if (double.TryParse(HoursWorkedTextBox.Text, out double hoursWorked) && double.TryParse(HourlyRateTextBox.Text, out double hourlyRate))
            {
                double totalClaim = hoursWorked * hourlyRate;
                TotalClaimTextBlock.Text = $"R {totalClaim}"; // Display the total claim as currency with Rand symbol
            }
            else
            {
                TotalClaimTextBlock.Text = "R 0.00"; // Show default value if inputs are invalid
            }
        }

        // Submit the claim
        private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
        {
            // Get input values
            if (double.TryParse(HoursWorkedTextBox.Text, out double hoursWorked) &&
                double.TryParse(HourlyRateTextBox.Text, out double hourlyRate))
            {
                string notes = NotesTextBox.Text;
                double totalClaim = hoursWorked * hourlyRate;

                // Create a new claim object
                Claim newClaim = new Claim
                {
                    LecturerName = "ST10388332", // Replace with actual lecturer name if you have it
                    HoursWorked = hoursWorked,
                    HourlyRate = hourlyRate,
                    Notes = notes,
                    SupportingDocumentPath = supportingDocumentPath, // Ensure this is set
                    Status = "Pending",
                    SubmittedOn = DateTime.Now,
                    TotalClaim = totalClaim // Set the total claim here
                };

                // Debugging: Check if supporting document path is set
                if (string.IsNullOrEmpty(newClaim.SupportingDocumentPath))
                {
                    MessageBox.Show("No document uploaded.");
                }

                // Save the claim using the repository
                Class1 repository = new Class1(); // Create instance of Class1
                repository.AddClaim(newClaim); // Save the new claim

                // Optional: Notify the user and reset input fields
                MessageBox.Show("Claim submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                HoursWorkedTextBox.Clear();
                HourlyRateTextBox.Clear();
                NotesTextBox.Clear();
                DocumentPathTextBlock.Text = ""; // Clear the document path
                TotalClaimTextBlock.Text = "R 0.00"; // Reset the total claim
            }
            else
            {
                MessageBox.Show("Please enter valid numbers for hours worked and hourly rate.");
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Close the current window
        }

        private void ViewClaimsButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the claims view window
            ViewClaimsWindow viewClaimsWindow = new ViewClaimsWindow();
            viewClaimsWindow.Show(); // Show the claims view window
        }
    }
}

