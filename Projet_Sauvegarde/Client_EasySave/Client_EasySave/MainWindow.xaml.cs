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

namespace Client_EasySave
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public string GoodResult { get; set; }
        private static byte[] result = new byte[1024];
        public MainWindow()
        {
            InitializeComponent();
            IPAddress ip = IPAddress.Parse("192.168.1.13");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 11000));
                Trace.WriteLine("Connecté");


            }
            catch
            {
                Trace.WriteLine("Echec de la connexion");
                return;
            }

            
            byte[] msgbuffer = new byte[8192];
            int receiveMsg = clientSocket.Receive(msgbuffer, 0, msgbuffer.Length, 0);
            Array.Resize(ref msgbuffer, receiveMsg);
            GoodResult = Encoding.Default.GetString(msgbuffer);
            ChangeAff();

           


            Trace.WriteLine("Fini de recevoir");
            Console.ReadLine();
        }
        public void UpdateScreen()
        {
            
        }
        public void ChangeAff()
        {
            int up = 0;
            int sah = 0;
   
            string[] configs = GoodResult.Split(",");
            List<ConfBackup> listBack = new List<ConfBackup>();
            int large = configs.Length;
            while( sah < large/5)
            {

                listBack.Add(new ConfBackup() { TypeS = configs[0 + up], Name = configs[1 + up], SourcePath = configs[2 + up], DestinationPath = configs[3 + up], CompleteSavePath = configs[4 + up] });

                Trace.WriteLine(configs[up]);
                sah += 1;
                up += 5;
                

            }
            ConfigBackupList.ItemsSource = listBack;
        }
    }
    public class ConfBackup
    {
        public string TypeS{ get; set; }
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string CompleteSavePath { get; set; }
    }
 
}
