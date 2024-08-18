using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EAFC_24_Career_mode_save_extention
{
    public partial class Form1 : Form
    {
        private readonly string configFilePath;
        private readonly string defaultSaveFileLocation;
        private Thread syncThread;
        private bool isRunning;
        private bool exitClicked = false;

        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Icon = this.Icon; // Use the form's icon, or set a custom one
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "FC24 Save Tool";

            // Optional: Set up a context menu for the notify icon
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Stop and close app", null, ExitMenuItem_Click);
            contextMenu.Items.Add(exitMenuItem);
            notifyIcon1.ContextMenuStrip = contextMenu;

            // Handle the double-click event to restore the form
            notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;

            defaultSaveFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"FC 24\settings");
            configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EAFC24_config.txt");

            // Ensure the default save file location directory exists
            Directory.CreateDirectory(defaultSaveFileLocation);

            // Create the configuration file with default values if it doesn't exist
            if (!File.Exists(configFilePath))
            {
                CreateDefaultConfiguration();
            }

            LoadConfiguration();
            //this.resume_button.Click += new System.EventHandler(this.resume_button_Click);
            AddApplicationToStartup();
            resume_button_Click(this, EventArgs.Empty);

        }

        private void AddApplicationToStartup()
        {
            try
            {
                string appName = "FC24 Save Tool"; // Your application name
                string appPath = Application.ExecutablePath; // Path to the application executable

                // Open the registry key for the current user
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                // Add the application to startup
                if (key.GetValue(appName) == null)
                {
                    key.SetValue(appName, appPath);
                    MessageBox.Show("Application added to startup.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add application to startup: {ex.Message}");
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            // Exit the application when the Exit option is clicked in the tray menu
            notifyIcon1.Visible = false; // Hide the notify icon
            Application.Exit(); // Exit the application
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder";
                folderBrowserDialog.ShowNewFolderButton = true;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected folder path
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;

                    // Display the selected folder path in the TextBox
                    textBoxFolderPath.Text = selectedFolderPath;
                }
            }
        }

        private void number_of_saves_trackbar_Scroll(object sender, EventArgs e)
        {
            label4.Text = number_of_saves_trackbar.Value.ToString();
        }

        private void check_frequency_trackbar_Scroll(object sender, EventArgs e)
        {
            label7.Text = check_frequency_trackbar.Value.ToString();
        }

        private void resume_button_Click(object sender, EventArgs e)
        {
            if (resume_button.Text == "Resume")
            {
                // Change button text to "Stop"
                resume_button.Text = "Stop";

                // Disable the trackbars and buttons
                check_frequency_trackbar.Enabled = false;
                number_of_saves_trackbar.Enabled = false;
                buttonBrowse.Enabled = false;
                revert_button.Enabled = false;
                save_button.Enabled = false;
                log.ForeColor = Color.Blue; // Set log color to blue
                // Start the synchronization thread
                isRunning = true;
                syncThread = new Thread(SynchronizeFiles);
                syncThread.Start();
            }
            else
            {
                // Change button text to "Resume"
                resume_button.Text = "Resume";

                // Enable the trackbars and buttons
                check_frequency_trackbar.Enabled = true;
                number_of_saves_trackbar.Enabled = true;
                buttonBrowse.Enabled = true;
                revert_button.Enabled = true;
                save_button.Enabled = true;
                log.ForeColor = Color.Red; // Set log color to red
                // Stop the synchronization thread
                isRunning = false;
                syncThread.Join(); // Wait for the thread to finish
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            // Get the configuration values
            string saveFileLocation = textBoxFolderPath.Text;
            int numberOfSaves = number_of_saves_trackbar.Value;
            int checkFrequency = check_frequency_trackbar.Value;

            // Write the values to the configuration file
            using (StreamWriter writer = new StreamWriter(configFilePath))
            {
                writer.WriteLine($"SaveFileLocation={saveFileLocation}");
                writer.WriteLine($"NumberOfSaves={numberOfSaves}");
                writer.WriteLine($"CheckFrequency={checkFrequency}");
            }

            MessageBox.Show("Configuration saved successfully.");
        }

        private void LoadConfiguration()
        {
            if (File.Exists(configFilePath))
            {
                string[] lines = File.ReadAllLines(configFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        try
                        {
                            switch (parts[0])
                            {
                                case "SaveFileLocation":
                                    textBoxFolderPath.Text = parts[1];
                                    break;
                                case "NumberOfSaves":
                                    int numberOfSaves = int.Parse(parts[1]);
                                    number_of_saves_trackbar.Value = numberOfSaves > 25 ? 25 : numberOfSaves;
                                    label4.Text = number_of_saves_trackbar.Value.ToString();
                                    break;
                                case "CheckFrequency":
                                    int checkFrequency = int.Parse(parts[1]);
                                    check_frequency_trackbar.Value = checkFrequency > 60 ? 60 : checkFrequency;
                                    label7.Text = check_frequency_trackbar.Value.ToString();
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // If there is an error in parsing, run the revert_button action
                            revert_button_Click(null, null);
                            MessageBox.Show("Configuration file contains invalid data. Reverting to default settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break; // Exit the loop since the configuration has been reverted
                        }
                    }
                }
            }
        }

        private void revert_button_Click(object sender, EventArgs e)
        {
            // Show a warning dialog with Cancel and Continue options
            DialogResult result = MessageBox.Show(
                "Reverting the settings to default might prevent the app from working properly. Verify your save location.",
                "Warning",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            // If the user clicks "OK", proceed with reverting the settings to default
            if (result == DialogResult.OK)
            {
                // Set "SaveFileLocation" to the default Documents folder
                textBoxFolderPath.Text = defaultSaveFileLocation;

                // Set "NumberOfSaves" to 7
                number_of_saves_trackbar.Value = 7;
                label4.Text = "7";

                // Set "CheckFrequency" to 10
                check_frequency_trackbar.Value = 10;
                label7.Text = "10";

                // Save the default settings immediately
                SaveConfiguration();
                MessageBox.Show("Settings have been reverted to default.");
            }
        }

        private void CreateDefaultConfiguration()
        {
            // Set default configuration values
            textBoxFolderPath.Text = defaultSaveFileLocation;
            number_of_saves_trackbar.Value = 7;
            label4.Text = "7";
            check_frequency_trackbar.Value = 10;
            label7.Text = "10";

            // Save the default configuration to the config file
            SaveConfiguration();
        }

        private void SaveConfiguration()
        {
            // Write the current configuration values to the configuration file
            using (StreamWriter writer = new StreamWriter(configFilePath))
            {
                writer.WriteLine($"SaveFileLocation={textBoxFolderPath.Text}");
                writer.WriteLine($"NumberOfSaves={number_of_saves_trackbar.Value}");
                writer.WriteLine($"CheckFrequency={check_frequency_trackbar.Value}");
            }
        }

        private void SynchronizeFiles()
        {
            string folderA = textBoxFolderPath.Text;
            string folderB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eafc_save_backup");

            if (!Directory.Exists(folderB))
            {
                Directory.CreateDirectory(folderB);
            }

            while (isRunning)
            {
                try
                {
                    var managerCareerFilesA = Directory.GetFiles(folderA)
                        .Where(file => Path.GetFileName(file).StartsWith("ManagerCareer"))
                        .OrderBy(file => GetFileCreationDateTime(file))
                        .ToList(); // Ordered by oldest first

                    var managerCareerFilesB = Directory.GetFiles(folderB)
                        .Where(file => Path.GetFileName(file).StartsWith("ManagerCareer"))
                        .OrderBy(file => GetFileCreationDateTime(file))
                        .ToList(); // Ordered by oldest first

                    // Log the number of ManagerCareer files in folderA
                    UpdateLog($"\nNumber of ManagerCareer files in Folder A: {managerCareerFilesA.Count}");

                    // Synchronize ManagerCareer files from A to B
                    foreach (string file in managerCareerFilesA)
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(folderB, fileName);

                        if (!File.Exists(destFile))
                        {
                            File.Copy(file, destFile);
                            UpdateLog($"Copied {fileName} from Folder A to Folder B");
                        }
                    }

                    // Synchronize ManagerCareer files from B to A
                    foreach (string file in managerCareerFilesB)
                    {
                        string fileName = Path.GetFileName(file);
                        string sourceFile = Path.Combine(folderA, fileName);

                        if (!File.Exists(sourceFile))
                        {
                            File.Copy(file, sourceFile);
                            UpdateLog($"Copied {fileName} from Folder B to Folder A");
                        }
                    }

                    // Ensure only the allowed number of saves is kept in Folder A
                    while (managerCareerFilesA.Count > number_of_saves_trackbar.Value)
                    {
                        string fileToDelete = managerCareerFilesA[0];
                        File.Delete(fileToDelete);
                        UpdateLog($"Deleted {Path.GetFileName(fileToDelete)} from Folder A");
                        managerCareerFilesA.RemoveAt(0); // Remove the deleted file from the list
                    }

                    // Ensure only the allowed number of saves is kept in Folder B
                    while (managerCareerFilesB.Count > number_of_saves_trackbar.Value)
                    {
                        string fileToDelete = managerCareerFilesB[0];
                        File.Delete(fileToDelete);
                        UpdateLog($"Deleted {Path.GetFileName(fileToDelete)} from Folder B");
                        managerCareerFilesB.RemoveAt(0); // Remove the deleted file from the list
                    }

                    // Sleep interval based on CheckFrequency, never less than 1000 ms
                    int sleepInterval = Math.Max((60 / check_frequency_trackbar.Value) * 1000, 1000);
                    Thread.Sleep(sleepInterval);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Additional logging or actions can be performed here
                }
            }
        }

        private void UpdateLog(string message)
        {
            if (log.InvokeRequired)
            {
                log.Invoke(new Action<string>(UpdateLog), message);
            }
            else
            {
                // Clear the first 1000 characters if the log exceeds 10,000 characters
                if (log.Text.Length > 10000)
                {
                    log.Text = log.Text.Substring(1000);
                }

                // Append the new log message with date and time, and add spacing
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                log.Text += $"\n\n[{timestamp}] {message}";

                // Scroll the panel to the bottom only if auto-scroll is enabled
                if (autoScrollEnabled)
                {
                    panel1.AutoScrollPosition = new Point(0, log.Height - panel1.ClientSize.Height);
                }
            }
        }

        private bool autoScrollEnabled = true;

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            // Check if the user has scrolled up
            if (panel1.VerticalScroll.Value < panel1.VerticalScroll.Maximum - panel1.ClientSize.Height)
            {
                autoScrollEnabled = false; // Disable auto-scroll if the user scrolls up
            }
            else if (panel1.VerticalScroll.Value == panel1.VerticalScroll.Maximum - panel1.ClientSize.Height)
            {
                autoScrollEnabled = true; // Re-enable auto-scroll when scrolled back to the bottom
            }
        }

        private void clear_log_Click(object sender, EventArgs e)
        {
            log.Text = string.Empty;
            autoScrollEnabled = true; // Re-enable auto-scroll when log is cleared
        }

        static DateTime GetFileCreationDateTime(string filePath)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (DateTime.TryParseExact(fileName.Substring(12, 14), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime creationDateTime))
                {
                    return creationDateTime;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse date from {filePath}. Error: {ex.Message}");
                // Additional logging or actions can be performed here
            }
            return DateTime.MinValue;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exitClicked)
            {
                // Allow the form to close when the exit button was clicked
                notifyIcon1.Visible = false; // Hide the notify icon
                Application.Exit(); // Exit the application
            }
            else
            {
                // Minimize to system tray instead of closing
                e.Cancel = true; // Cancel the close event
                this.Hide(); // Hide the form
                notifyIcon1.Visible = true; // Show the notify icon
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            // Restore the form from the system tray
            this.Show(); // Show the form
            this.WindowState = FormWindowState.Normal; // Restore the window state if it was minimized
            notifyIcon1.Visible = false; // Hide the notify icon
        }

       
        private void exit_button_Click(object sender, EventArgs e)
        {

            exitClicked = true;
            this.Close(); // Trigger the FormClosing event
        }
    }
}
