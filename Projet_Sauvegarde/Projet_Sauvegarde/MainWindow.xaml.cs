using Projet_Sauvegarde.Controller;
using System;
using System.Collections.Generic;
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

namespace Projet_Sauvegarde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //SaveController saveController = new SaveController();
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
            if(CompleteRadio.IsChecked == true)
            {
                //AddOneSave("complete", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text);
                MessageBox.Show("The Complete Backup has been added to the list");
                UpdateListBackup();
            }
            else if (DiffRadio.IsChecked == true)
            {
                //AddOneSave("differential", TextNameOfSave.Text, TextSourcePath.Text, TextDestinationPath.Text, TextLastComplete.Text);
                MessageBox.Show("The Differential Backup has been added to the list");
                UpdateListBackup2();
            }
            else
            {
                MessageBox.Show("Select Type Of Backup");
            }
        }

        private void StartSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

      

        private void DeleteConfigButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void UpdateListBackup()
        {
            //ListBackup.Items.Clear();
            Thread.Sleep(100);
            /*foreach (saveTask backup in listSave)
            {
                ListBackup.Items.Add(new MyItem { typeBackup = backup.Type, nameBackup = backup.Name, sourceBackup = backup.SoucePath, destinationBackup = backup.DestinationPath, lastCompleteBackup = backup.CompleteSavePath });
            }*/
            ListBackup.Items.Add(new MyItem { typeBackup = "Complete", nameBackup = "FirstTry", sourceBackup = "Source", destinationBackup = "Destination", lastCompleteBackup = "" });
            
        }
        public void UpdateListBackup2()
        {
            //ListBackup.Items.Clear();
            Thread.Sleep(100);
            /*foreach (saveTask backup in listSave)
            {
                ListBackup.Items.Add(new MyItem { typeBackup = backup.Type, nameBackup = backup.Name, sourceBackup = backup.SoucePath, destinationBackup = backup.DestinationPath, lastCompleteBackup = backup.CompleteSavePath });
            }*/
            ListBackup.Items.Add(new MyItem { typeBackup = "differential", nameBackup = "FirstTry", sourceBackup = "Source", destinationBackup = "Destination", lastCompleteBackup = "fa" });

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
