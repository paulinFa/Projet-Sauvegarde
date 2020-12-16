using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Threading;
using System.Collections.ObjectModel;

namespace Client_EasySave
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ConfBackup> AllNeedSave { get; set; }
        public string GoodResult { get; set; }
        public string Progression { get; set; }
        public string Sending { get; set; }
        private static byte[] result = new byte[1024];
        Socket clientSocket;
        public MainWindow()
        {
            AllNeedSave = new ObservableCollection<ConfBackup>();
            DataContext = this;
            InitializeComponent();
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 11000));

                string message = "Connecte";

                byte[] msgbuffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(msgbuffer, 0, msgbuffer.Length, 0);

                Thread listen = new Thread(Listen);
                listen.Start(clientSocket);
                Trace.WriteLine("ListenThread");

            }
            catch
            {
                Trace.WriteLine("Echec de la connexion");
                return;
            }

        }


        public void Listen(Object socket)
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(100);
                    if (clientSocket != null)
                    {
                        byte[] msgbuffer = new byte[8192];
                        int receiveMsg = clientSocket.Receive(msgbuffer, 0, msgbuffer.Length, 0);
                        Array.Resize(ref msgbuffer, receiveMsg);
                        GoodResult = Encoding.Default.GetString(msgbuffer);
                       
                        Thread.Sleep(100);
                        if (GoodResult.StartsWith("complete") || GoodResult.StartsWith("differential"))
                        {
                            Trace.WriteLine(GoodResult);
                            ChangeAff();
                           
                            string argo = "argowitch";
                            SendMessage(argo);
                            
                            Thread.Sleep(100);

                        }
                        else if (GoodResult == "saveinprogress")
                        {
                            
                            SendMessage("continuesave");
                            Thread.Sleep(200);
                        }
                        else if (GoodResult == "STOP1")
                        {
                            Trace.WriteLine("STOP");

                            Thread.Sleep(200);
                        }
                        else if (GoodResult == "Pause")
                        {
                            Trace.WriteLine("Save is in Pause");

                            Thread.Sleep(200);
                        }
                        else if (GoodResult == "StartAll")
                        {
                            Trace.WriteLine("All Saves are laucnh");

                            Thread.Sleep(200);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }



            }

        }
        

        public void SendMessage(string msg)
        {
            byte[] msgbuffer = Encoding.Default.GetBytes(msg);
            clientSocket.Send(msgbuffer, 0, msgbuffer.Length, 0);
        }

      
        public void ChangeAff()
        {

            int up = 0;
            int sah = 0;
            
            string[] configs = GoodResult.Split(",");
            List<ConfBackup> listBack = new List<ConfBackup>();
            int large = configs.Length;
            Dispatcher.Invoke(() => AllNeedSave.Clear());
            while (sah < large / 6)
            {

                Dispatcher.Invoke(() => AllNeedSave.Add(new ConfBackup() { TypeS = configs[0 + up], Name = configs[1 + up], SourcePath = configs[2 + up], DestinationPath = configs[3 + up], CompleteSavePath = configs[4 + up] , Progression = configs[5 + up]}));
               
                //listBack.Add(new ConfBackup() { TypeS = configs[0 + up], Name = configs[1 + up], SourcePath = configs[2 + up], DestinationPath = configs[3 + up], CompleteSavePath = configs[4 + up] });
                sah += 1;
                up += 6;
            }
            
            //Dispatcher.Invoke(() => ConfigBackupList.Items.Refresh());
            Trace.WriteLine("passé");
        }
        public ConfBackup nameOfBackup;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            nameOfBackup = (ConfBackup)ConfigBackupList.SelectedItems[0];
            Thread.Sleep(200);
            Thread sendStart = new Thread(SendStart);
            sendStart.Start();
            Trace.WriteLine("Start," + nameOfBackup.Name);
            
        }


        public void SendStart()
        {
            Thread.Sleep(200);
            string startMsg = "Start," + nameOfBackup.Name;
            byte[] hopla = Encoding.Default.GetBytes(startMsg);
            clientSocket.Send(hopla, 0, hopla.Length, 0);
            Trace.WriteLine("AppelStart");


        }

        public ConfBackup stopBackup;
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            stopBackup = (ConfBackup)ConfigBackupList.SelectedItems[0];
            Thread.Sleep(200);
            Thread sendStop = new Thread(SendStop);
            sendStop.Start();
            Trace.WriteLine("Stop," + stopBackup.Name);

        }
        public void SendStop()
        {
            Thread.Sleep(200);
            string stopMsg = "Stop," + stopBackup.Name;
            byte[] hopli = Encoding.Default.GetBytes(stopMsg);
            clientSocket.Send(hopli, 0, hopli.Length, 0);
            Trace.WriteLine(stopMsg);
        }

        public ConfBackup pauseBackup;
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            pauseBackup = (ConfBackup)ConfigBackupList.SelectedItems[0];
            Thread.Sleep(200);
            Thread sendPause = new Thread(SendPause);
            sendPause.Start();
            Trace.WriteLine("Pause," + pauseBackup.Name);

        }
        public void SendPause()
        {
            Thread.Sleep(200);
            string pauseMsg = "Pause," + pauseBackup.Name;
            byte[] hoplo = Encoding.Default.GetBytes(pauseMsg);
            clientSocket.Send(hoplo, 0, hoplo.Length, 0);
            Trace.WriteLine(pauseMsg);
        }
        public ConfBackup startAllBackup;
        List<String> allSaves = new List<string>();
        private void StartAllButton_Click(object sender, RoutedEventArgs e)
        {
            allSaves.Add("StartAll");
            foreach (ConfBackup rils in AllNeedSave)
            { 
                allSaves.Add(rils.Name);
            }
            
            
            Thread.Sleep(200);
            Thread sendStartall = new Thread(SendStartAll);
            sendStartall.Start();
            
        }
        public void SendStartAll()
        {
            Thread.Sleep(200);
            string startallMsg = String.Join(",", allSaves);
            Trace.WriteLine(startallMsg);
            byte[] hopli = Encoding.Default.GetBytes(startallMsg);
            clientSocket.Send(hopli, 0, hopli.Length, 0);
            Trace.WriteLine(startallMsg);
        }

        
    }
    public class ConfBackup
    {
        public string TypeS { get; set; }
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string CompleteSavePath { get; set; }

        public string Progression { get; set; }
    }

}
