using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SearchOption = System.IO.SearchOption;

namespace Projet_Sauvegarde.Controller
{
    class CompleteSaveController
    {
        public string Name { get; set; }
        public int TotalNumberFile { get; set; }
        public long TotalLengthFile { get; set; }

        [DefaultValue(0)]
        public float Progression { get; set; }

        [DefaultValue(0)]
        public long RemainingLengthFile { get; set; }

        [DefaultValue(0)]
        public int RemainingNumberFile { get; set; }

        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }

        public void CopyFolder(string sourcePath, string destinationPath,string name)
        {
            this.SourcePath = sourcePath;
            
            this.DestinationPath = destinationPath;
            TotalLengthFile = DirSize(SourcePath);
            TotalNumberFile = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories).Length;
            Console.WriteLine(SourcePath + " " + TotalLengthFile);
            RemainingNumberFile = TotalNumberFile;
            RemainingLengthFile = TotalLengthFile;
            StartCopy(this.SourcePath,this.DestinationPath);
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
        public static long DirSize(string d)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(d);
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = dir.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di.FullName);
            }
            return size;
        }

    }
}
