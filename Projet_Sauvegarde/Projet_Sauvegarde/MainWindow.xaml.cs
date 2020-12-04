using Projet_Sauvegarde.Controller;
using Projet_Sauvegarde.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Linq;
using System.IO;
using System.ComponentModel;

namespace Projet_Sauvegarde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : INotifyPropertyChanged //Method who initialize components
    {
        SaveController saveController;
        private string _extensionSave;
        public string ExtensionSave
        {
            get { return _extensionSave; }
            set
            {
                if (_extensionSave != value)
                {
                    _extensionSave = value;
                    OnPropertyChanged("ExtensionSave");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged; //Contains the data of an event
        public void OnPropertyChanged(string propertyName = null) //Method that will make the change during an event
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); //Provides the data of an event
            }

        }
        private string _processSave;
        public string ProcessSave 
        {
            get { return _processSave; }
            set
            {
                if (_processSave != value)
                {
                    _processSave = value;
                    OnPropertyChanged("ProcessSave");
                }
            }
        }

        public ObservableCollection<SaveTask> AllConfigBackup { get; set; }
        public ObservableCollection<SaveTask> AllBackupLaunch { get; set; }

        public MainWindow()
        {
            
            DataContext = this;
            saveController = new SaveController(this);
            AllConfigBackup = new ObservableCollection<SaveTask>();
            AllBackupLaunch = new ObservableCollection<SaveTask>();
            
            
           
            InitializeComponent();
            UpdateListBackup();
            UpdateExtension();
            UpdateProcess();
            
        }

       
        private void CompleteRadioButton_Checked(object sender, RoutedEventArgs e)
        {
          
        }

        private void DiffRadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void QuitAppButton_Click(object sender, RoutedEventArgs e) //Method who shutdown the app on click
        {
            System.Environment.Exit(0);
        }

        public void PopupErrorProcess(string message) //Method who show pop-up when there is an error
        {
            MessageBox.Show(message,"Error Save Stop", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void AddBackupButton_Click(object sender, RoutedEventArgs e) //Method who add one backup configuration on the config file on click
        {

            //Check if there is an error in the insertion of the values
            if (TextNameOfSave.Text.Length == 0)
            {
                MessageBox.Show("Enter a Valid Name", "Error Add Save", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            

            if (!Directory.Exists(TextSourcePath.Text))
            {
                MessageBox.Show("The Source Path does not exist", "Error Add Save", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            if (!Directory.Exists(TextDestinationPath.Text))
            {
                MessageBox.Show("The Destination Path does not exist", "Error Add Save", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Look at the type of backup adds the corresponding configuration

            if (CompleteRadio.IsChecked == true)
            {
                saveController.AddOneSave("complete", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text, TextLastComplete.Text);
                MessageBox.Show("The Complete Backup has been added to the list", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateListBackup();
            }
            else if (DiffRadio.IsChecked == true)
            {
                if (!Directory.Exists(TextLastComplete.Text))
                {
                    MessageBox.Show("The Path of the Complete Backup does not exist", "Error Add Save", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                saveController.AddOneSave("differential", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text, TextLastComplete.Text);
                MessageBox.Show("The Differential Backup has been added to the list","Sucess",MessageBoxButton.OK,MessageBoxImage.Information);
                UpdateListBackup();
            }
            else
            {
                MessageBox.Show("Select Type Of Backup", "Error Add Save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            TextNameOfSave.Text = String.Empty;
            TextSourcePath.Text = String.Empty;
            TextDestinationPath.Text = String.Empty;
            TextLastComplete.Text = String.Empty;
        }

        private void StartSaveButton_Click(object sender, RoutedEventArgs e) //Method who start all the backup on click
        {

            saveController.StartMultipleSaves(AllBackupLaunch.ToList());
            AllBackupLaunch.Clear();
        }

      

        private void DeleteConfigButton_Click(object sender, RoutedEventArgs e) //Method who Delete one backup configuration on click
        {
            var allConfig = AllConfigList.SelectedItems;
            List<SaveTask> listSaveTask = new List<SaveTask>();
            foreach (SaveTask save in allConfig)
            {
                listSaveTask.Add(save);
            }
            saveController.DeleteSaves(listSaveTask);
            UpdateListBackup();
        }


        private void TakeConfigButton_Click(object sender, RoutedEventArgs e) //Method who transmits a backup configuration to be executed on click
        {
            
            foreach(SaveTask LaunchBackup in AllConfigList.SelectedItems)
            {
                AllBackupLaunch.Add(LaunchBackup);
            }
        }

        private void DeleteQueueButton_Click(object sender, RoutedEventArgs e) //Method who delete all the backup configuration in the queue on click
        {
            AllBackupLaunch.Clear();
        }

        public void UpdateListBackup() //Method who update the graphic interface 
        {

            AllConfigBackup.Clear();
            foreach (SaveTask backup in saveController.ListSave)
            {
               
                AllConfigBackup.Add(backup);
            }

        }
        
        



        private void EnglishButton_Click(object sender, RoutedEventArgs e) //Method who change the language in English
        {

        }

        private void FranchButton_Click(object sender, RoutedEventArgs e) //Method who change the language in French
        {

        }

        private void SaveExtension_Click(object sender, RoutedEventArgs e) //Method that sends the extensions to be encrypted
        {
            saveController.ModifyExtension(TextExtEncrypt.Text);
            UpdateExtension();
            TextExtEncrypt.Clear();
        }
        public void UpdateExtension () //Method that updates the extension display
        {
            ExtensionSave = "";
            ExtensionSave = saveController.Extension;
            
        }

        private void SaveProcess_Click(object sender, RoutedEventArgs e) //Method that sends the business software that will stop the backups
        {
            saveController.ModifySoftware(ExecutableText.Text);
            UpdateProcess();
            ExecutableText.Clear();
        }
        public void UpdateProcess()  // Method that updates the business software display
        {
            ProcessSave = "";
            ProcessSave = saveController.Software;
        }

    }
}
