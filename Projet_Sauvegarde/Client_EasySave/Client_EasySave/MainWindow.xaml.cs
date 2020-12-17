using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
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
        public List<ConfBackup> FocusSave;
        public ObservableCollection<ConfBackup> AllNeedSave { get; set; }
        public string GoodResult { get; set; }
        public string Progression { get; set; }
        public string Sending { get; set; }
        private static byte[] result = new byte[1024];
        Socket clientSocket;
        public MainWindow()
        {
            FocusSave = new List<ConfBackup>();
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
                            ChangeAff();
                           
                            string argo = "argowitch";
                            SendMessage(argo);
                            
                            Thread.Sleep(100);

                        }
                        if (GoodResult.StartsWith("progression"))
                        {
                            ChangeProgression();

                            string argo = "progression";
                            SendMessage(argo);

                            Thread.Sleep(100);

                        }

                    }



            }

        }
        

        public void SendMessage(string msg)
        {
            byte[] msgbuffer = Encoding.Default.GetBytes(msg);
            clientSocket.Send(msgbuffer, 0, msgbuffer.Length, 0);
        }

        public void ChangeProgression()
        {
            int a = 0;
            string[] configs = GoodResult.Split("*");
            int large = configs.Length;
            a = (large - 1) / 2;
            while (a != 0)
            {
                foreach (ConfBackup confBackup in AllNeedSave)
                {
                    if (configs[(a * 2) - 2] == confBackup.Name)
                    {
                        confBackup.Progression = configs[(a * 2) - 1];
                    }
                }
                Dispatcher.Invoke(() => ConfigBackupList.Items.Refresh());
                a--;
            }
        }

      
        public void ChangeAff()
        {

            int up = 0;
            int sah = 0;
            
            string[] configs = GoodResult.Split("*");

            int large = configs.Length;
            Dispatcher.Invoke(() => AllNeedSave.Clear());
            while (sah < large / 6)
            {

                Dispatcher.Invoke(() => AllNeedSave.Add(new ConfBackup() { TypeS = configs[0 + up], Name = configs[1 + up], SourcePath = configs[2 + up], DestinationPath = configs[3 + up], CompleteSavePath = configs[4 + up] , Progression = configs[5 + up]}));
               
                //listBack.Add(new ConfBackup() { TypeS = configs[0 + up], Name = configs[1 + up], SourcePath = configs[2 + up], DestinationPath = configs[3 + up], CompleteSavePath = configs[4 + up] });
                sah += 1;
                up += 6;
            }
                    }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (FocusSave.Count > 0)
            {
                foreach (ConfBackup confBackup in FocusSave)
                {
                    Thread.Sleep(200);
                    Thread sendStart = new Thread(SendStart);
                    sendStart.Start(confBackup.Name);
                }

            }
            FocusSave.Clear();

        }


        public void SendStart(object obj)
        {
            Thread.Sleep(200);
            string startMsg = "Start*" + (string)obj;
            byte[] hopla = Encoding.Default.GetBytes(startMsg);
            clientSocket.Send(hopla, 0, hopla.Length, 0);


        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {

            if (FocusSave.Count > 0)
            {
                foreach (ConfBackup confBackup in FocusSave)
                {
                    Thread.Sleep(200);
                    Thread sendStart = new Thread(SendStop);
                    sendStart.Start(confBackup.Name);
                }

            }
            FocusSave.Clear();
        }
        public void SendStop(object obj)
        {
            Thread.Sleep(200);
            string stopMsg = "Stop*" + (string)obj;
            byte[] hopla = Encoding.Default.GetBytes(stopMsg);
            clientSocket.Send(hopla, 0, hopla.Length, 0);
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (FocusSave.Count > 0)
            {
                foreach (ConfBackup confBackup in FocusSave)
                {
                    Thread.Sleep(200);
                    Thread sendStart = new Thread(SendPause);
                    sendStart.Start(confBackup.Name);
                }

            }
            FocusSave.Clear();

        }
        public void SendPause(object obj)
        {
            Thread.Sleep(200);
            string pauseMsg = "Pause*" + (string)obj;
            byte[] hopla = Encoding.Default.GetBytes(pauseMsg);
            clientSocket.Send(hopla, 0, hopla.Length, 0);
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
            string startallMsg = String.Join("*", allSaves);
            byte[] hopli = Encoding.Default.GetBytes(startallMsg);
            clientSocket.Send(hopli, 0, hopli.Length, 0);
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid data = (DataGrid) sender;
            FocusSave.Clear();
            if (data.SelectedItems.Count > 0)
            {
                foreach (ConfBackup confBackup in data.SelectedItems)
                {
                    FocusSave.Add(confBackup);
                }
            }
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
