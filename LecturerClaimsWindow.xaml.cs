using System;
using System.Diagnostics; // For opening the document
using System.Windows;

namespace CMCS
{
    public partial class LecturerClaimsWindow : Window
    {
        private string lecturerName;

        public LecturerClaimsWindow(string lecturerName)
        {
            InitializeComponent();
            this.lecturerName = lecturerName;
            LoadClaims();
        }

        // Load claims for the specific lecturer
        private void LoadClaims()
        {
            // Filter the claims by lecturer name
          
        }

        // Open the uploaded supporting document
        private void ViewDocument_Click(object sender, RoutedEventArgs e)
        {
            Claim selectedClaim = (Claim)ClaimsListView.SelectedItem;

            if (selectedClaim != null && !string.IsNullOrEmpty(selectedClaim.SupportingDocumentPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = selectedClaim.SupportingDocumentPath,
                        UseShellExecute = true // Opens the file with the default application
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No document uploaded for this claim.");
            }
        }
    }
}
