using System;
using Projet_Sauvegarde.Controller;
using Projet_Sauvegarde.Model;

namespace Projet_Sauvegarde
{
    class Program
    {
        static void Main(string[] args)
        {
            CompleteSaveController controller = new CompleteSaveController();
            DifferentialSaveController differentialSaveController = new DifferentialSaveController();
            differentialSaveController.CopyFolder("D:/Documents/GitHub", "D:/Bureau/Destination", "D:/Bureau/save complete/test_11-24-2020_11.00.46_","testons");
            //controller.CopyFolder("D:/Documents/GitHub", "D:/Bureau/Destination","test");

        }
    }
}
