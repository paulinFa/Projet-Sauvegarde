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
        bool Lock = true;
        public string ClientMsg { get; set; }
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
            Connecting(serverSocket);
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start(clientSocket);
            Trace.WriteLine("Threag recevant lancé");




        }
        public void Connecting(Socket socket)
        {
            clientSocket = serverSocket.Accept();

        }

        private void SendMessageS(string msg)
        {
            while (clientSocket == null)
            {

            }

            byte[] msgbuffer = Encoding.Default.GetBytes(msg);
            clientSocket.Send(msgbuffer, 0, msgbuffer.Length, 0);


            Trace.WriteLine("Envoie lancé");

        }



        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {

                try
                {
                    Thread.Sleep(200);
                    
                    if (clientSocket != null)
                    {
                        byte[] msgbuffer = new byte[8192];
                        int receiveNumber = myClientSocket.Receive(msgbuffer, 0, msgbuffer.Length, 0);
                        Array.Resize(ref msgbuffer, receiveNumber);
                        ClientMsg = Encoding.Default.GetString(msgbuffer);
                        Trace.WriteLine("Recup data sur serv");
                        Thread.Sleep(100);
                        if (ClientMsg == "Connecte")
                        {

                            SendMessageS(Result);
                            Trace.WriteLine(Result);
                            Thread.Sleep(100);
                        }
                        else if (ClientMsg.StartsWith("Start"))
                        {
                            string aller = "renvoyé";
                            SendMessageS(aller);
                            Trace.WriteLine("hop");
                            Thread.Sleep(200);
                        }
                        else if (ClientMsg.StartsWith("Stop"))
                        {
                            Trace.WriteLine("Stop Save");
                            SendMessageS("STOP1");
                        }
                        else if (ClientMsg.StartsWith("continue"))
                        {
                            string cont = "renvoyé";
                            SendMessageS(cont);
                            Trace.WriteLine("renvoyé");
                            Thread.Sleep(200);
                        }
                    }

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

            SendMessageS(Result);

        }
    }
}
