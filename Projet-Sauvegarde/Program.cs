using System;
using Projet_Sauvegarde.Controller;
using Projet_Sauvegarde.Model;

namespace Projet_Sauvegarde
{
    class Program
    {
        static void Main(string[] args)
        {
            //CompleteSaveController controller = new CompleteSaveController();
            //controller.CopyFolder("D:/Source", "D:/Destination", "TestCopy");
    
            StateFile statefile = new StateFile();
            LogFile logFile = new LogFile("24 novembre 14:51", "TEstONE", "ALLER", "RETOUR", 52, 3);
        }
    }
}
