using Projet_Sauvegarde.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Projet_Sauvegarde.Controller;
using System.Threading;



namespace Projet_Sauvegarde.Controller
{
    class ServeurController
    {
        List<SaveTask> listShare;
        List<String> tempList = new List<string>();
        public static string Result { get; set; }

        private static byte[] result = new byte[1024];
        private static int myPort = 11000;
        Socket serverSocket;
        Socket clientSocket;
        public ServeurController(MainWindow mainWindow, List<SaveTask> listConfig)
        {

            listShare = listConfig;

            foreach (SaveTask backup in listShare)
            {
                tempList.Add(backup.Type);
                tempList.Add(backup.Name);
                tempList.Add(backup.SourcePath);
                tempList.Add(backup.DestinationPath);
                tempList.Add(backup.CompleteSavePath);
            }
            Result = String.Join(",", tempList);

            IPAddress ip = IPAddress.Parse("192.168.1.13");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myPort));
            serverSocket.Listen(10);
            Trace.WriteLine("connecte toi", serverSocket.LocalEndPoint.ToString());

            new Thread(ListenClientConnect).Start();

            Console.ReadLine();

        }
        public void Connecting()
        {
            clientSocket = serverSocket.Accept();

        }

        private void ListenClientConnect()
        {
            while (clientSocket == null)
            {

            }
            byte[] msgbuffer = Encoding.Default.GetBytes(Result);
            clientSocket.Send(msgbuffer, 0, msgbuffer.Length, 0);

            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start(clientSocket);
            Trace.WriteLine("Envoie lancé");

        }



        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    byte[] msgbuffer = new byte[8192];
                    int receiveNumber = myClientSocket.Receive(msgbuffer, 0, msgbuffer.Length, 0);
                    Array.Resize(ref msgbuffer, receiveNumber);
                    Trace.WriteLine("Recu server :{1}", Encoding.ASCII.GetString(result, 0, receiveNumber));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }

            }

        }

        public void UpdateListShare(List<SaveTask> listQuelq)
        {
            listShare = listQuelq;
            new Thread(ListenClientConnect).Start();

        }
    }
}
