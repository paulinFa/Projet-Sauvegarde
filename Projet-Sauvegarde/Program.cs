using System;
using Projet_Sauvegarde.Controller;
namespace Projet_Sauvegarde
{
    class Program
    {
        static void Main(string[] args)
        {
            CompleteSaveController controller = new CompleteSaveController();
            controller.CopyFolder("D:/Documents/GitHub", "D:/Bureau/Destination");
    }
    }
}
