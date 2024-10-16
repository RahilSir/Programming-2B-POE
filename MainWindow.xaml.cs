using System.Windows;

namespace CMCS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for the Lecturer button
        private void LecturerButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Lecturer input form window
            LecturerWindow lecturerWindow = new LecturerWindow(); // Create a new instance of LecturerWindow
            lecturerWindow.Show(); // Show the Lecturer Window
            this.Close(); // Close the Main Window (optional)
        }

        // Placeholder event handlers for the other buttons
        private void AcademicManagerButton_Click(object sender, RoutedEventArgs e)
        {
            AcademicManagerWindow academicManagerWindow = new AcademicManagerWindow(); // Create a new instance of AcademicManagerWindow
            academicManagerWindow.Show(); // Show the Academic Manager Window
            this.Close(); // Close the Main Window (optional)
        }

        private void ProgrammeCoordinatorButton_Click(object sender, RoutedEventArgs e)
        {
            CoordinatorWindow coordinatorWindow = new CoordinatorWindow();
            coordinatorWindow.Show();
            this.Close(); // Close the Main Window (optional)
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
