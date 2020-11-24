using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SearchOption = System.IO.SearchOption;

namespace Projet_Sauvegarde.Model
{
    class CompleteSave : Save
    {
        public void CopyFolder(string name,string sourcePath, string destinationPath)
        {
            this.SourcePath = sourcePath;
            this.Name = name;

            TotalLengthFile = DirSize(SourcePath);
            TotalNumberFile = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories).Length;
            Console.WriteLine(SourcePath + " " + TotalLengthFile);
            RemainingNumberFile = TotalNumberFile;
            RemainingLengthFile = TotalLengthFile;
            string folder = destinationPath + "/" + name + "_" + DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            StartCopy(this.SourcePath, folder);
        }

        public void StartCopy(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);
            string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destinationPath, name);
                if (!File.Exists(dest)){
                    File.Copy(file, dest);
                    var fi1 = new FileInfo(dest);
                    RemainingNumberFile--;
                    RemainingLengthFile -= fi1.Length;
                    Progression = RemainingLengthFile !=0 ? Convert.ToSingle(TotalLengthFile - RemainingLengthFile) / Convert.ToSingle(TotalLengthFile) * 100 : 100;
                    Console.WriteLine("remaining length : " + RemainingLengthFile + "remaining number : " + RemainingNumberFile + "length : " + fi1.Length +" Etat d'avancement = " +Progression + " %");
                }              
                
            }
            string[] folders = Directory.GetDirectories(sourcePath);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destinationPath, name);
                if (!Directory.Exists(dest))
                {
                    StartCopy(folder, dest);
                }
            }
        }
    }
}
