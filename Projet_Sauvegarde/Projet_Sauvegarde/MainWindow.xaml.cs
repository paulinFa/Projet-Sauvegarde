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

namespace Projet_Sauvegarde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SaveController saveController;
        public ObservableCollection<SaveTask> AllConfigBackup { get; set; }
        public ObservableCollection<SaveTask> AllBackupLaunch { get; set; }
        public MainWindow()
        {
            
            DataContext = this;
            saveController = new SaveController();
            AllConfigBackup = new ObservableCollection<SaveTask>();
            AllBackupLaunch = new ObservableCollection<SaveTask>();
           
            InitializeComponent();
            UpdateListBackup();

        }


        private void CompleteRadioButton_Checked(object sender, RoutedEventArgs e)
        {
          
        }

        private void DiffRadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void QuitAppButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void AddBackupButton_Click(object sender, RoutedEventArgs e)
        {

           
            if (TextNameOfSave.Text.Length == 0)
            {
                MessageBox.Show("Enter a Valid Name");
                return;
            }

            

            if (!Directory.Exists(TextSourcePath.Text))
            {
                MessageBox.Show("The Source Path does not exist");
                return;
            }
            

            if (!Directory.Exists(TextDestinationPath.Text))
            {
                MessageBox.Show("The Destination Path does not exist");
                return;
            }

            


            if (CompleteRadio.IsChecked == true)
            {
                saveController.AddOneSave("complete", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text, TextLastComplete.Text);
                MessageBox.Show("The Complete Backup has been added to the list");
                UpdateListBackup();
            }
            else if (DiffRadio.IsChecked == true)
            {
                if (!Directory.Exists(TextLastComplete.Text))
                {
                    MessageBox.Show("The Path of the Complete Backup does not exist");
                    return;
                }
                saveController.AddOneSave("differential", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text, TextLastComplete.Text);
                MessageBox.Show("The Differential Backup has been added to the list");
                UpdateListBackup();
            }
            else
            {
                MessageBox.Show("Select Type Of Backup");
            }
        }

        private void StartSaveButton_Click(object sender, RoutedEventArgs e)
        {

            //saveController.StartMulitpleSaves(AllBackupLaunch.ToList<SaveTask>);
            
        }

      

        private void DeleteConfigButton_Click(object sender, RoutedEventArgs e)
        {
            
            //saveController.DeleteSaves(AllConfigList.SelectedItems);
        }


        private void TakeConfigButton_Click(object sender, RoutedEventArgs e)
        {
            
            foreach(SaveTask LaunchBackup in AllConfigList.SelectedItems)
            {
                AllBackupLaunch.Add(LaunchBackup);
            }
        }

        private void DeleteQueueButton_Click(object sender, RoutedEventArgs e)
        {
            AllBackupLaunch.Clear();
        }

        public void UpdateListBackup()
        {

            AllConfigBackup.Clear();
            foreach (SaveTask backup in saveController.ListSave)
            {
               
                AllConfigBackup.Add(backup);
            }

        }
        
        

        public class MyItem
        {
            public string typeBackup { get; set; }
            public string nameBackup { get; set; }
            public string sourceBackup { get; set; }
            public string destinationBackup { get; set; }
            public string lastCompleteBackup { get; set; }

        }

    }
}
