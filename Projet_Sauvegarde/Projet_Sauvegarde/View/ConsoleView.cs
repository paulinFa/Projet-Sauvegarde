using Projet_Sauvegarde.Controller;
using System;
using System.IO;

namespace Projet_Sauvegarde.View
{

    class ConsoleView : IView
    {
        private SaveController saveController;
        public void setController(SaveController controller)
        {
            this.saveController = controller;
        }
        public void StartingView()
        {
            Console.WriteLine("Welcome in EasySafe, a program to save your files");
            AskUser();
        }
        private void AskUser()
        {
            Console.WriteLine("Do you want to make a save complete,differential or exit ? (complete|differential|exit)");
            string i = Console.ReadLine();
            if (i == "complete")
            {
                InputCompleteToQueue();
            }
            else if (i == "differential")
            {
                InputDifferentialToQueue();
            }
            else if (i == "exit")
            {
                Console.WriteLine("Bye bye");
            }
            else
            {
                Console.WriteLine("Wrong input (complete|differential|exit)");
                AskUser();
            }
        }
        private void InputCompleteToQueue()
        {
            Console.WriteLine("Enter a name for the complete backup");
            string name = Console.ReadLine();

            while (name.Length == 0)
            {
                Console.WriteLine("Wrong input");
                name = Console.ReadLine();
            }
            Console.WriteLine("Enter a source Path");
            string source = Console.ReadLine();

            while (!Directory.Exists(source))
            {
                Console.WriteLine("Wrong source input");
                source = Console.ReadLine();
            }
            Console.WriteLine("Enter a destination Path");
            string destination = Console.ReadLine();

            while (!Directory.Exists(destination))
            {
                Console.WriteLine("Wrong destination input");
                destination = Console.ReadLine();
            }

            string[] temp = new string[] { "complete", name, source, destination };
            saveController.AddSave(temp);
            StartSave();

        }
        private void InputDifferentialToQueue()
        {
            Console.WriteLine("Enter a name for the differential backup");
            string name = Console.ReadLine();

            while (name.Length == 0)
            {
                Console.WriteLine("Wrong input");
                name = Console.ReadLine();
            }
            Console.WriteLine("Enter a source Path");
            string source = Console.ReadLine();

            while (!Directory.Exists(source))
            {
                Console.WriteLine("Wrong source input");
                source = Console.ReadLine();
            }
            Console.WriteLine("Enter a destination Path");
            string destination = Console.ReadLine();

            while (!Directory.Exists(destination))
            {
                Console.WriteLine("Wrong destination input");
                destination = Console.ReadLine();
            }

            Console.WriteLine("Enter the path to the full backup");
            string complete = Console.ReadLine();

            while (!Directory.Exists(complete))
            {
                Console.WriteLine("Wrong complete input");
                complete = Console.ReadLine();
            }

            string[] temp = new string[] { "differential", name, source, destination, complete };
            saveController.AddSave(temp);
            StartSave();

        }
        private void StartSave()
        {
            Console.WriteLine("Do you want to start your backup(s) or add a new one? (add|start) \nYour status and log files are stored in the EasySave folder on your disk D/EasySave");
            string choice = Console.ReadLine();
            if (choice == "add")
            {
                AskUser();
            }
            else if (choice == "start")
            {
                saveController.StartSave();
            }
            else
            {
                Console.WriteLine("Wrong input (add|start)");
                StartSave();
            }
        }
        private void DeleteInQueue()
        {

        }
    }
}
