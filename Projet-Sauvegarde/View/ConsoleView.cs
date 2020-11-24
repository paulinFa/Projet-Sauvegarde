using Projet_Sauvegarde.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            Console.WriteLine("Welcom in EasySafe, a program to save your files");
            AskUser();
        }
        private void AskUser()
        {
            Console.WriteLine("Do you want to make a save complete,differential or exit ?");
            string i = Console.ReadLine();
            if (i == "complete")
            {
                InputCompleteToQueue();
            }else if(i == "differential")
            {
                InputDifferentialToQueue();
            }else if(i== "exit")
            {
                Console.WriteLine("Bye bye");
            }
            else{
                Console.WriteLine("Wrong input (complete|differential|exit)");
                AskUser();
            }
        }
        private void InputCompleteToQueue()
        {
            Console.WriteLine("What is name ?");
            string name = Console.ReadLine();

            while (name.Length == 0)
            {
                Console.WriteLine("Wrong input");
                name = Console.ReadLine();
            }
            Console.WriteLine("What is sourcePath");
            string source = Console.ReadLine();

            while (!Directory.Exists(source))
            {
                Console.WriteLine("Wrong source input");
                source = Console.ReadLine();
            }
            Console.WriteLine("What is destinationPath");
            string destination = Console.ReadLine();

            while (!Directory.Exists(destination))
            {
                Console.WriteLine("Wrong destination input");
                destination = Console.ReadLine();
            }

            string[] temp = new string[] { "complete",name, source, destination };
            saveController.AddSave(temp);
            StartSave();

        }
        private void InputDifferentialToQueue()
        {
            Console.WriteLine("What is name ?");
            string name = Console.ReadLine();

            while (name.Length == 0)
            {
                Console.WriteLine("Wrong input");
                name = Console.ReadLine();
            }
            Console.WriteLine("What is sourcePath");
            string source = Console.ReadLine();

            while (!Directory.Exists(source))
            {
                Console.WriteLine("Wrong source input");
                source = Console.ReadLine();
            }
            Console.WriteLine("What is destinationPath");
            string destination = Console.ReadLine();

            while (!Directory.Exists(destination))
            {
                Console.WriteLine("Wrong destination input");
                destination = Console.ReadLine();
            }

            Console.WriteLine("What is completeSavePath");
            string complete = Console.ReadLine();

            while (!Directory.Exists(complete))
            {
                Console.WriteLine("Wrong complete input");
                complete = Console.ReadLine();
            }

            string[] temp = new string[] { "differential",name, source, destination,complete };
            saveController.AddSave(temp);
            StartSave();

        }
        private void StartSave()
        {
            Console.WriteLine("Would you like to start your save(s) or add a new ?");
            string choice = Console.ReadLine();
            if(choice == "add")
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
        public void PrintError()
        {

        }
        private void DeleteInQueue()
        {

        }
    }
}
