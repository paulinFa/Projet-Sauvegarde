﻿using System;
using Projet_Sauvegarde.Controller;
using Projet_Sauvegarde.Model;

namespace Projet_Sauvegarde
{
    class Program
    {
        static void Main(string[] args)
        {
            CompleteSaveController controller = new CompleteSaveController();
            controller.CopyFolder("D:/Documents/GitHub", "D:/Bureau/Destination");
    }
            StateFile statefile = new StateFile();
            LogFile logfile = new LogFile();
        }
    }
}
