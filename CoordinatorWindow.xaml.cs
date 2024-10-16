using System;
using System.Diagnostics; // For opening the document
using System.Linq; // For LINQ operations
using System.Windows;

namespace CMCS
{
    public partial class CoordinatorWindow : Window
    {
        private Class1 claimsRepository; // Repository instance to access claims

        public CoordinatorWindow()
        {
            InitializeComponent();
            claimsRepository = new Class1(); // Initialize repository instance
            LoadClaims();
        }

        // Load the pending claims into the ListView
        private void LoadClaims()
        {
            // Clear the existing items
            ClaimsListView.Items.Clear();

            // Get claims from the repository
            var claims = claimsRepository.GetClaims();

            // Filter only pending claims
            var pendingClaims = claims.Where(c => c.Status == "Pending").ToList();

            // Add claims to the ListView
            foreach (var claim in pendingClaims)
            {
               
                

                
                string formattedHourlyRate = $"R {claim.HourlyRate}"; // Format with two decimal places
                string formattedTotalClaim = $"R {claim.TotalClaim}"; // Format total claim with two decimal places


                ClaimsListView.Items.Add(new
                {
                    claim.LecturerName,
                    claim.HoursWorked,
                    HourlyRate = formattedHourlyRate,
                    TotalClaim = formattedTotalClaim,
                    claim.Notes,
                    claim.Status,
                    claim.SubmittedOn,
                    SupportingDocumentPath = claim.SupportingDocumentPath // Keep track of the document path
                });


            }
        }












        // Open the uploaded supporting document when "View Document" button is clicked
        private void ViewDocument_Click(object sender, RoutedEventArgs e)
        {
            // Ensure you have a valid claim selected
            Claim selectedClaim = (Claim)ClaimsListView.SelectedItem;

            // Check if a claim is selected and if the SupportingDocumentPath is set
            if (selectedClaim != null)
            {
                if (!string.IsNullOrEmpty(selectedClaim.SupportingDocumentPath) && System.IO.File.Exists(selectedClaim.SupportingDocumentPath))
                {
                    try
                    {
                        // Log the file path for debugging
                        MessageBox.Show("Opening document at: " + selectedClaim.SupportingDocumentPath);

                        // Open the document using the default application
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = selectedClaim.SupportingDocumentPath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No document uploaded for this claim or file not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a claim to view the document.");
            }
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Close the current window
        }








        // Approve the selected claim
        // Approve the selected claim
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            var selectedClaim = ClaimsListView.SelectedItem as dynamic;
            if (selectedClaim != null)
            {
                // Update the claim status
                var claimToUpdate = claimsRepository.GetClaims().FirstOrDefault(c => c.LecturerName == selectedClaim.LecturerName);
                if (claimToUpdate != null)
                {
                    claimToUpdate.Status = "Approved";
                    claimsRepository.ApproveClaim(claimToUpdate);

                    // Provide feedback to the user
                    MessageBox.Show("Claim Approved");

                    // Reload claims to refresh the view
                    LoadClaims();
                }
            }
            else
            {
                MessageBox.Show("Please select a claim to approve.");
            }
        }

        // Reject the selected claim
        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            var selectedClaim = ClaimsListView.SelectedItem as dynamic;
            if (selectedClaim != null)
            {
                // Update the claim status
                var claimToUpdate = claimsRepository.GetClaims().FirstOrDefault(c => c.LecturerName == selectedClaim.LecturerName);
                if (claimToUpdate != null)
                {
                    claimToUpdate.Status = "Rejected";
                    claimsRepository.RejectClaim(claimToUpdate);

                    // Provide feedback to the user
                    MessageBox.Show("Claim Rejected");

                    // Reload claims to refresh the view
                    LoadClaims();
                }
            }
            else
            {
                MessageBox.Show("Please select a claim to reject.");
            }
        }

    }
}