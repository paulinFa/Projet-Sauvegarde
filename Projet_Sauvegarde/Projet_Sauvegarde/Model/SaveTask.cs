using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    public class SaveTask
    {

        public string Type { get; set; }
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string CompleteSavePath { get; set; }
        public SaveTask(string type,string name, string sourcePath,string destinationPath, string completeSavePath = "")
        {
            this.Type = type;
            this.Name = name;
            this.SourcePath = sourcePath;
            this.DestinationPath = destinationPath;
            this.CompleteSavePath = completeSavePath;
        }
    }
}
