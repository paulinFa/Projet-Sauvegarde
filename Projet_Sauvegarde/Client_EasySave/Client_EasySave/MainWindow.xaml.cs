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

            int receiveLength = clientSocket.Receive(result);
            Trace.WriteLine("Recu client：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                    string sendMessage = "client send Message Hello" + DateTime.Now;
                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    Trace.WriteLine("a envoyé ：" + sendMessage);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }

            }

            Trace.WriteLine("Fini de recevoir");
            Console.ReadLine();
        }
    }
}
