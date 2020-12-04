using Projet_Sauvegarde.Controller;
using System;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Projet_Sauvegarde.View
{

    class ConsoleView : IView
    {
        private ResourceManager rm = fr.ResourceManager;
        private SaveController saveController;
        public void setController(SaveController controller)
        {
            this.saveController = controller;
        }
        public void StartingView()
        {
            Console.WriteLine("Choice your language (english|francais)");
            string i = Console.ReadLine();
            if (i == "francais")
            {
                
                rm = fr.ResourceManager;
                Welcom();
            }
            else if (i=="english")
            {
                rm = en.ResourceManager;
                Welcom();
            }
            else
            {
                StartingView();
            }
        }
        public void Welcom()
        {
            Console.WriteLine(rm.GetString("welcom"));
            AskUser();
        }
        private void AskUser()
        {
            Console.WriteLine(rm.GetString("typeSave"));
            string i = Console.ReadLine();
            if (i == rm.GetString("complete"))
            {
                InputCompleteToQueue();
            }
            else if (i == rm.GetString("differential"))
            {
                InputDifferentialToQueue();
            }
            else if (i == rm.GetString("exit"))
            {
                Console.WriteLine(rm.GetString("byeBye"));
            }
            else
            {
                Console.WriteLine(rm.GetString("Entrez incorrecte (complète|differentiellle|quitter)"));
                AskUser();
            }
        }
        private void InputCompleteToQueue()
        {
            Console.WriteLine(rm.GetString("enterNameCompleteBackup"));
            string name = Console.ReadLine();

            while (name.Length == 0)
            {
                Console.WriteLine(rm.GetString("wrongInputName"));
                name = Console.ReadLine();
            }
            Console.WriteLine(rm.GetString("enterSourcePath"));
            string source = Console.ReadLine();

            while (!Directory.Exists(source))
            {
                Console.WriteLine(rm.GetString("wrongInputSource"));
                source = Console.ReadLine();
            }
            Console.WriteLine(rm.GetString("enterDestinationPath"));
            string destination = Console.ReadLine();

            while (!Directory.Exists(destination))
            {
                Console.WriteLine(rm.GetString("wrongInputDestination"));
                destination = Console.ReadLine();
            }

            string[] temp = new string[] { "complete", name, source, destination };
            saveController.AddSave(temp);
            StartSave();

        }
        private void InputDifferentialToQueue()
        {
            Console.WriteLine(rm.GetString("enterNameDifferentialBackup"));
            string name = Console.ReadLine();

            while (name.Length == 0)
            {
                Console.WriteLine(rm.GetString("wrongInputName"));
                name = Console.ReadLine();
            }
            Console.WriteLine(rm.GetString("enterSourcePath"));
            string source = Console.ReadLine();

            while (!Directory.Exists(source))
            {
                Console.WriteLine(rm.GetString("wrongInputSource"));
                source = Console.ReadLine();
            }
            Console.WriteLine(rm.GetString("enterDestinationPath"));
            string destination = Console.ReadLine();

            while (!Directory.Exists(destination))
            {
                Console.WriteLine(rm.GetString("wrongInputDestination"));
                destination = Console.ReadLine();
            }

            Console.WriteLine(rm.GetString("enterCompletePath"));
            string complete = Console.ReadLine();

            while (!Directory.Exists(complete))
            {
                Console.WriteLine(rm.GetString("wrongCompletePath"));
                complete = Console.ReadLine();
            }

            string[] temp = new string[] { "differential", name, source, destination, complete };
            saveController.AddSave(temp);
            StartSave();

        }
        private void StartSave()
        {
            Console.WriteLine(rm.GetString("chooseBetwwenAddOrStart"));
            string choice = Console.ReadLine();
            if (choice == rm.GetString("add"))
            {
                AskUser();
            }
            else if (choice == rm.GetString("start"))
            {
                saveController.StartSave();
            }
            else
            {
                Console.WriteLine(rm.GetString("wrongInputAddOrStart"));
                StartSave();
            }
        }
        private void DeleteInQueue()
        {

        }
    }
}
