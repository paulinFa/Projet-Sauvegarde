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
using System.Resources;
using Projet_Sauvegarde.Utils;
using Projet_Sauvegarde.View;
using System.Diagnostics;

namespace Projet_Sauvegarde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : INotifyPropertyChanged
    {
        
        public SaveController saveController;
        ServeurController serveurController;
        private string _extensionSave;
        public string ExtensionSave //Getter and setter for Event (Extension)
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
        public event PropertyChangedEventHandler PropertyChanged; //Implementing change notification to update the display
        public void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        private string _processSave;
        public string ProcessSave //Getter and setter for Event (Extension)
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
        private string _tallSave;
        public string TallSave //Getter and setter for Event (Extension)
        {
            get { return _tallSave; }
            set
            {
                if (_tallSave != value)
                {
                    _tallSave = value;
                    OnPropertyChanged("TallSave");
                }
            }
        }

        public ObservableCollection<SaveTask> AllConfigBackup { get; set; }
        public ObservableCollection<SaveTask> AllBackupLaunch { get; set; }

        public MainWindow() //Method that initializes new objects 
        {
            
            

            DataContext = this;
            saveController = new SaveController(this);
            serveurController = new ServeurController(this, saveController.ListSave);
            AllConfigBackup = new ObservableCollection<SaveTask>();
            AllBackupLaunch = new ObservableCollection<SaveTask>();





            InitializeComponent();
           

            new Thread(() => {
                while (true)
                {
                    UpdateListBackup();
                }
            });
            

            LangUtil.SetupLang();


            UpdateListBackup();
            UpdateExtension();
            UpdateProcess();
            UpdateTall();

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
            MessageBox.Show(LangUtil.GetString("popupErrorProcessMessage1") + message + LangUtil.GetString("popupErrorProcessMessage2"), LangUtil.GetString("popupErrorProcessTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void AddBackupButton_Click(object sender, RoutedEventArgs e) //Method who add one backup configuration on the config file on click
        {

            //Check if there is an error in the insertion of the values
            if (TextNameOfSave.Text.Length == 0)
            {
                MessageBox.Show(LangUtil.GetString("popupErrorSaveNameMessage"), LangUtil.GetString("popupErrorSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (nameAlreadyExist(TextNameOfSave.Text))
            {
                MessageBox.Show(LangUtil.GetString("popupErrorSaveNameMessageExist"), LangUtil.GetString("popupErrorSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            if (!Directory.Exists(TextSourcePath.Text))
            {
                MessageBox.Show(LangUtil.GetString("popupErrorSaveSourceMessage"), LangUtil.GetString("popupErrorSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (!Directory.Exists(TextDestinationPath.Text))
            {
                MessageBox.Show(LangUtil.GetString("popupErrorSaveDestinationMessage"), LangUtil.GetString("popupErrorSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Look at the type of backup adds the corresponding configuration

            if (CompleteRadio.IsChecked == true)
            {
                saveController.AddOneSave("complete", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text, TextLastComplete.Text);
                MessageBox.Show(LangUtil.GetString("popupSuccesSaveCompleteMessage"), LangUtil.GetString("popupSuccesSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateListBackup();
            }
            else if (DiffRadio.IsChecked == true)
            {
                if (!Directory.Exists(TextLastComplete.Text))
                {
                    MessageBox.Show(LangUtil.GetString("popupErrorSaveCompleteMessage"), LangUtil.GetString("popupErrorSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                saveController.AddOneSave("differential", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text, TextLastComplete.Text);
                MessageBox.Show(LangUtil.GetString("popupSuccesSaveDifferentialMessage"), LangUtil.GetString("popupSuccesSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateListBackup();
            }
            else
            {
                MessageBox.Show(LangUtil.GetString("popupErrorSaveTypeMessage"), LangUtil.GetString("popupErrorSaveTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            TextNameOfSave.Text = String.Empty;
            TextSourcePath.Text = String.Empty;
            TextDestinationPath.Text = String.Empty;
            TextLastComplete.Text = String.Empty;
        }

        private bool nameAlreadyExist(string name)
        {
            return saveController.ListSave.Count(save => (save.Name == name)) > 0;
        }

        private void StartSaveButton_Click(object sender, RoutedEventArgs e) //Method who start all the backup on click
        {
            saveController.StartMultipleSaves(AllBackupLaunch.ToList());
            


            (new Thread(() =>
            {
                Thread.Sleep(20);
                while (!everithingIsFinish(AllBackupLaunch))
                {
                    Thread.Sleep(400);
                    foreach (SaveTask saveTask in AllBackupLaunch)
                    {
                            saveTask.Progression = saveController.GetProgression(saveTask);

                            Trace.WriteLine(saveController.GetProgression(saveTask));
                            
                            OnPropertyChanged("AllBackupLaunch");
                            this.Dispatcher.Invoke(() =>
                            {
                                BackupListLaunch.Items.Refresh();
                            });
                    }
                }
            })).Start();

            /*ObservableCollection<SaveTask> AllBackupLaunchTemp = AllBackupLaunch;
            while (!everithingIsFinish(AllBackupLaunchTemp))
            {

                foreach (SaveTask save in AllBackupLaunchTemp)
                {
                    save.Progression = saveController.ListSave.Find((a) => a.Name == save.Name).Progression;
                }
                AllBackupLaunch.Clear();
                AllBackupLaunch = AllBackupLaunchTemp;
            }*/

        }

        private bool everithingIsFinish(ObservableCollection<SaveTask> allBackupLaunchTemp)
        {
            return !(allBackupLaunchTemp.ToList().Count((a) => a.Progression != 100.0 && a.GetIfStop() == false) > 0);
        }



        /*private void takeProgression()
        {
            float progression = 0;
            while(progression != 100.0)
            {
                progression = this.saveController.ListSave.Find((a) => (a.Name == "a")).GetProgression();
            }
            progression = 0;
        }*/
      

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
            
            foreach(SaveTask saveTask in AllConfigList.SelectedItems)
            {
                if(!AllBackupLaunch.Contains(saveTask))
                {
                    AllBackupLaunch.Add(saveTask);
                }

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
            serveurController.UpdateListShare(saveController.ListSave);

        }      

        private void EnglishButton_Click(object sender, RoutedEventArgs e) //Method who change the language in English
        {
            LangUtil.SetLang("en-US");
        }

        private void FranchButton_Click(object sender, RoutedEventArgs e) //Method who change the language in French
        {
            LangUtil.SetLang("fr-FR");
        }

        private void SaveExtension_Click(object sender, RoutedEventArgs e) //Method that saves the extension to be encrypted
        {
            saveController.ModifyExtension(TextExtEncrypt.Text);
            UpdateExtension();
            TextExtEncrypt.Clear();
        }
        public void UpdateExtension ()
        {
            ExtensionSave = "";
            ExtensionSave = saveController.Extension;            
        }

        private void SaveProcess_Click(object sender, RoutedEventArgs e)  //Method that saves the metering process
        {
            saveController.ModifySoftware(ExecutableText.Text);
            UpdateProcess();
            ExecutableText.Clear();
        }
        public void UpdateProcess()
        {
            ProcessSave = "";
            ProcessSave = saveController.Software;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            foreach (SaveTask saveTask in BackupListLaunch.SelectedItems)
            {
                saveController.ListSave.Find((a) => a.Name == saveTask.Name)?.Pause();
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            foreach (SaveTask saveTask in BackupListLaunch.SelectedItems)
            {
                saveController.ListSave.Find((a) => (a.Name == saveTask.Name && a.GetIfStop() == false))?.Stop();
            }
        }

        private void SaveTall_Click(object sender, RoutedEventArgs e)
        {
            saveController.ModifyTall(TallText.Text);
            UpdateTall();
            TallText.Clear();
        }
        public void UpdateTall()
        {
            TallSave = "";
            TallSave = saveController.Tall;
        }
    }
}
