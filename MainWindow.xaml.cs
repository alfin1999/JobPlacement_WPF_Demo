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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace JobPlacementApp
{
    public partial class MainWindow : Window
    {
        // ObservableCollection to hold the list of clients
        private ObservableCollection<Client> Clients { get; set; }
        private ObservableCollection<Volunteer> Volunteers { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            InitializeClientData();
            InitializeVolunteerData();
        }

        private void InitializeVolunteerData()
        {
            Volunteers = new ObservableCollection<Volunteer>
            {
                new Volunteer { Name = "Volunteer A", Email = "volunteer.a@example.com", CompanyName = "Tesco, NHS" },
                new Volunteer { Name = "Volunteer B", Email = "volunteer.b@example.com", CompanyName = "Asda, Lidl" }
            };

            VolunteerComboBox.ItemsSource = Volunteers.Select(v => v.Name).ToList();
        }

        // Add a new volunteer
        private void AddVolunteer_Click(object sender, RoutedEventArgs e)
        {
            var newVolunteer = new Volunteer
            {
                Name = VolunteerNameTextBox.Text.Trim(),
                Email = VolunteerEmailTextBox.Text.Trim(),
                CompanyName = VolunteerSkillsTextBox.Text.Trim()
            };

            if (string.IsNullOrEmpty(newVolunteer.Name) || Volunteers.Any(v => v.Name == newVolunteer.Name))
            {
                MessageBox.Show("Volunteer name is empty or already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Volunteers.Add(newVolunteer);
            RefreshVolunteerList();
            ClearVolunteerFields();
            MessageBox.Show("Volunteer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Update an existing volunteer
        private void UpdateVolunteer_Click(object sender, RoutedEventArgs e)
        {
            var selectedVolunteerName = VolunteerComboBox.SelectedItem as string;
            var volunteer = Volunteers.FirstOrDefault(v => v.Name == selectedVolunteerName);

            if (volunteer != null)
            {
                volunteer.Name = VolunteerNameTextBox.Text.Trim();
                volunteer.Email = VolunteerEmailTextBox.Text.Trim();
                volunteer.CompanyName = VolunteerSkillsTextBox.Text.Trim();

                RefreshVolunteerList();
                ClearVolunteerFields();
                MessageBox.Show("Volunteer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a volunteer to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // View volunteer details
        private void ViewVolunteerDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedVolunteerName = VolunteerComboBox.SelectedItem as string;
            var volunteer = Volunteers.FirstOrDefault(v => v.Name == selectedVolunteerName);

            if (volunteer != null)
            {
                MessageBox.Show(
                    $"Name: {volunteer.Name}\n" +
                    $"Email: {volunteer.Email}\n" +
                    $"CompanyName: {volunteer.CompanyName}",
                    "Volunteer Details",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a volunteer to view details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Refresh the volunteer list in ComboBox
        private void RefreshVolunteerList()
        {
            VolunteerComboBox.ItemsSource = null;
            VolunteerComboBox.ItemsSource = Volunteers.Select(v => v.Name).ToList();
        }

        // Clear input fields
        private void ClearVolunteerFields()
        {
            VolunteerNameTextBox.Clear();
            VolunteerEmailTextBox.Clear();
            VolunteerSkillsTextBox.Clear();
        }
        


        // Initialize client data
        private void InitializeClientData()
        {
            // Sample client data
            Clients = new ObservableCollection<Client>
            {
                new Client
                {
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    JobType = "Office Work",
                    DrivingLicense = "Yes",
                    CriminalConviction = "No",
                    Skills = "Communication, Teamwork"
                },
                new Client
                {
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    JobType = "Gardening",
                    DrivingLicense = "No",
                    CriminalConviction = "Yes",
                    Skills = "Problem-Solving, Physical Strength"
                }
            };

            // Bind the ObservableCollection to ListBox and ComboBox
            ClientListBox.ItemsSource = Clients.Select(c => c.Name).ToList();
            ClientComboBox.ItemsSource = Clients.Select(c => c.Name).ToList();
            ClientFeedbackComboBox.ItemsSource = Clients.Select(c => c.Name).ToList();
        }

        // Add a new client
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var newClient = new Client
            {
                Name = ClientNameTextBox.Text.Trim(),
                Email = $"{ClientNameTextBox.Text.Trim().ToLower().Replace(" ", ".")}@example.com",
                JobType = (JobTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                DrivingLicense = (DrivingLicenseComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                CriminalConviction = (CriminalConvictionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Skills = SkillsTextBox.Text.Trim()
            };

            if (string.IsNullOrEmpty(newClient.Name) || Clients.Any(c => c.Name == newClient.Name))
            {
                MessageBox.Show("Client name is empty or already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Clients.Add(newClient);
            RefreshClientList();
            ClearFields();
            MessageBox.Show("Client added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Update an existing client
        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            var selectedClientName = ClientListBox.SelectedItem as string;
            var client = Clients.FirstOrDefault(c => c.Name == selectedClientName);

            if (client != null)
            {
                client.Name = ClientNameTextBox.Text.Trim();
                client.JobType = (JobTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                client.DrivingLicense = (DrivingLicenseComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                client.CriminalConviction = (CriminalConvictionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                client.Skills = SkillsTextBox.Text.Trim();

                RefreshClientList();
                ClearFields();
                MessageBox.Show("Client updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a client to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CreateMatch_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve match details from the TextBox
            string matchDetails = MatchDetailsTextBox.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(matchDetails))
            {
                MessageBox.Show("Please enter match details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Display a confirmation message
            MessageBox.Show($"Match successfully created:\n\n{matchDetails}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Optionally clear the TextBox after creating a match
            MatchDetailsTextBox.Clear();
        }


        // Delete a client
        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            var selectedClientName = ClientListBox.SelectedItem as string;
            var client = Clients.FirstOrDefault(c => c.Name == selectedClientName);

            if (client != null)
            {
                Clients.Remove(client);
                RefreshClientList();
                ClearFields();
                MessageBox.Show("Client deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a client to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve data from controls
            string client = ClientComboBox.SelectedItem as string;
            string volunteer = VolunteerComboBox.SelectedItem as string;
            DateTime? date = AppointmentDatePicker.SelectedDate;
            ComboBoxItem time = TimeComboBox.SelectedItem as ComboBoxItem;

            // Validate inputs
            if (string.IsNullOrEmpty(client))
            {
                MessageBox.Show("Please select a client.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(volunteer))
            {
                MessageBox.Show("Please select a volunteer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (date == null)
            {
                MessageBox.Show("Please select a date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (time == null)
            {
                MessageBox.Show("Please select a time.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Display booking details
            MessageBox.Show(
                $"Appointment booked:\n\n" +
                $"Client: {client}\n" +
                $"Volunteer: {volunteer}\n" +
                $"Date: {date.Value.ToShortDateString()}\n" +
                $"Time: {time.Content}",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }


        // View client details
        private void ViewClientDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedClientName = ClientComboBox.SelectedItem as string;
            var client = Clients.FirstOrDefault(c => c.Name == selectedClientName);

            if (client != null)
            {
                MessageBox.Show(
                    $"Name: {client.Name}\n" +
                    $"Email: {client.Email}\n" +
                    $"Job Type: {client.JobType}\n" +
                    $"Driving License: {client.DrivingLicense}\n" +
                    $"Criminal Conviction: {client.CriminalConviction}\n" +
                    $"Skills: {client.Skills}",
                    "Client Details",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a client to view details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Submit feedback
        private void SubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            var selectedClientName = ClientFeedbackComboBox.SelectedItem as string;
            var client = Clients.FirstOrDefault(c => c.Name == selectedClientName);

            if (client == null)
            {
                MessageBox.Show("Please select a client.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string feedback = FeedbackTextBox.Text.Trim();

            if (string.IsNullOrEmpty(feedback))
            {
                MessageBox.Show("Please enter feedback.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show(
                $"Feedback submitted for {client.Name} ({client.Email}):\n\n{feedback}",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            // Clear fields
            ClientFeedbackComboBox.SelectedIndex = -1;
            FeedbackTextBox.Clear();
        }

        // Refresh the client list in all ComboBoxes and ListBoxes
        private void RefreshClientList()
        {
            ClientListBox.ItemsSource = null;
            ClientListBox.ItemsSource = Clients.Select(c => c.Name).ToList();
            ClientComboBox.ItemsSource = null;
            ClientComboBox.ItemsSource = Clients.Select(c => c.Name).ToList();
            ClientFeedbackComboBox.ItemsSource = null;
            ClientFeedbackComboBox.ItemsSource = Clients.Select(c => c.Name).ToList();
        }

        // Clear input fields
        private void ClearFields()
        {
            ClientNameTextBox.Clear();
            JobTypeComboBox.SelectedIndex = -1;
            DrivingLicenseComboBox.SelectedIndex = -1;
            CriminalConvictionComboBox.SelectedIndex = -1;
            SkillsTextBox.Clear();
        }
    }

    // Client class to store detailed client information
    public class Client
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string JobType { get; set; }
        public string DrivingLicense { get; set; }
        public string CriminalConviction { get; set; }
        public string Skills { get; set; }
    }
    public class Volunteer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
    }

}

