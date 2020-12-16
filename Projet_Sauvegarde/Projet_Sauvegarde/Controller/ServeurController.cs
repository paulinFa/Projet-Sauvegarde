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
        MainWindow mainWindow;
        public ServeurController(MainWindow mainWindow, List<SaveTask> listConfig)
        {

            this.mainWindow = mainWindow;

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myPort));
            serverSocket.Listen(10);
            Connecting(serverSocket);
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start(clientSocket);
            Trace.WriteLine("Thread recevant lancé");




        }
        public void TakeList(List<SaveTask> listConfig)
        {
            tempList.Clear();
            listShare = listConfig;

            foreach (SaveTask backup in listShare)
            {
                tempList.Add(backup.Type);
                tempList.Add(backup.Name);
                tempList.Add(backup.SourcePath);
                tempList.Add(backup.DestinationPath);
                tempList.Add(backup.CompleteSavePath);
                tempList.Add(backup.Progression.ToString());
            }
            Result = String.Join("*", tempList);
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




        }

        public void UpdateProgressionClients(List<SaveTask> list)
        {
            string r = "progression";
            foreach (SaveTask saveTask in list)
            {
                r += "*" + saveTask.Name + "*" + saveTask.Progression;
            }
            SendMessageS(r);
        }



        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {

                    Thread.Sleep(200);

                    if (clientSocket != null)
                    {
                        byte[] msgbuffer = new byte[8192];
                        int receiveNumber = myClientSocket.Receive(msgbuffer, 0, msgbuffer.Length, 0);
                        Array.Resize(ref msgbuffer, receiveNumber);
                        ClientMsg = Encoding.Default.GetString(msgbuffer);

                        Thread.Sleep(100);
                        if (ClientMsg == "Connecte")
                        {

                            SendMessageS(Result);
                            Trace.WriteLine(Result);
                            Thread.Sleep(100);
                        }
                        else if (ClientMsg.StartsWith("Start*"))
                        {
                            string[] infosStart = ClientMsg.Split("*");
                            if (infosStart[1] != "")
                            {
                                mainWindow.saveController.StartOneSave(mainWindow.saveController.ListSave.Find((a) => a.Name == infosStart[1]));
                                mainWindow.AddSaveToAllBackupLaunch(mainWindow.saveController.ListSave.Find((a) => a.Name == infosStart[1]));
                                mainWindow.UpdateProgression();
                                Trace.WriteLine("StartSave");
                                string aller = "saveinprogress";

                                SendMessageS(aller);

                                Thread.Sleep(200);
                            }
                        }
                        else if (ClientMsg.StartsWith("StartAll*"))
                        {
                            
                            int club = 1;
                            string[] infosStart = ClientMsg.Split("*");
                            if (infosStart[1] != "")
                            {
                                foreach (string nameDiff in infosStart)
                                {
                                    mainWindow.saveController.StartOneSave(mainWindow.saveController.ListSave.Find((a) => a.Name == infosStart[club]));
                                    club += 1;
                                }
                                
                                Trace.WriteLine("StartSave");
                                string aller = "saveinprogress";

                                SendMessageS(aller);

                                Thread.Sleep(200);
                            }
                        }
                        else if (ClientMsg.StartsWith("Stop"))
                        {
                            
                            string[] infosStop = ClientMsg.Split("*");
                            if (infosStop[1] != "")
                            {
                                Trace.WriteLine("recu le stop");
                                mainWindow.saveController.ListSave.Find((a) => a.Name == infosStop[1]).Stop();
                                Trace.WriteLine("Stop Save");
                                SendMessageS("STOP1");
                            }
                            
                        }
                        else if (ClientMsg.StartsWith("Pause"))
                        {
                            Trace.WriteLine(ClientMsg);
                            string[] infosPause = ClientMsg.Split("*");
                            if (infosPause[1] != "")
                            {
                                mainWindow.saveController.ListSave.Find((a) => a.Name == infosPause[1]).Stop();
                                Trace.WriteLine("Pause");
                                SendMessageS("Pause");
                            }

                           
                        }
                        /*else if (ClientMsg.StartsWith("continuesave"))
                        {
                            string cont = "saveinprogress";
                            SendMessageS(cont);

                            Thread.Sleep(200);
                        }*/

                }
        }

        }


        public void UpdateListShare(List<SaveTask> listQuelq)
        {
            listShare = listQuelq;
            TakeList(listQuelq);
            SendMessageS(Result);

        }
    }
}
