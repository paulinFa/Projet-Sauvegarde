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

namespace Client_EasySave
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string GoodResult { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            var connect = Connecting();
            Trace.WriteLine("Cornichon");
            //SendMessage(connect);
            ListenNetwork(connect);
           

        }
        private Socket Connecting()
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.1.13");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.  
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(remoteEP);
            return socket;
        }
        private void ListenNetwork(Socket client)
        {
            Trace.WriteLine("prems");
            byte[] msgbuffer = new byte[8192];
            Trace.WriteLine("fini1");
            int recieveMsg = client.Receive(msgbuffer, 0, msgbuffer.Length, 0);
            Trace.WriteLine("fini2");
            Array.Resize(ref msgbuffer, recieveMsg);
            Trace.WriteLine("fini3");
            GoodResult = Encoding.Default.GetString(msgbuffer);
            UpdateScreen();
            Trace.WriteLine("fini4");
            Trace.WriteLine(GoodResult);
            
        }
        private void SendMessage(Socket client)
        {
           
            string msg = Console.ReadLine();
            byte[] msgbuffer = Encoding.Default.GetBytes(msg);
            client.Send(msgbuffer, 0, msgbuffer.Length, 0);
        }

        public void UpdateScreen()
        {
            RecieveTestText.Text = String.Empty;
            RecieveTestText.Text = GoodResult;
        }
    }
}
