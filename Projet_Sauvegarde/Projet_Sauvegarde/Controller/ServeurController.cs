using Projet_Sauvegarde.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Projet_Sauvegarde.Controller;



namespace Projet_Sauvegarde.Controller 
{
    class ServeurController 
    {
        List<SaveTask> listShare;
        List<String> tempList = new List<string>();
        public string Result { get; set; }
        


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

            Trace.WriteLine("Init");
            var socket = Connecting();
            var clientSocket = AcceptConnection(socket);
            SendMessage(clientSocket);
            //ListenNetwork(clientSocket);
            
            
        }
        private Socket Connecting()
        {
            
            IPAddress ipAddress = IPAddress.Parse("192.168.1.13");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            return listener;
        }
        private Socket AcceptConnection(Socket socket)
        {
            
            Socket NewSocket = socket.Accept();
            Trace.WriteLine("COnnecté");
            return NewSocket;
        }
        private void ListenNetwork(Socket client)
        {
            byte[] buffer = new byte[1024];
            int rec = client.Receive(buffer, 0, buffer.Length, 0);
            Array.Resize(ref buffer, rec);
            
           
        }
        private void SendMessage(Socket client)
        {

            Trace.WriteLine("con");
            byte[] msgbuffer = Encoding.Default.GetBytes(Result);
            client.Send(msgbuffer, 0, msgbuffer.Length, 0);
            Trace.WriteLine("finihop");
        }

  
        public void UpdateListShare (List<SaveTask> listQuelq)
        {
            listShare = listQuelq;
            
        }
    }
}
