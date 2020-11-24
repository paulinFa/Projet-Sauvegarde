using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    class DifferentialSave : Save
    {
        public string CompleteSavePath { get; set; }
        public string Folder { get; set; }
        public void CopyFolder(string name ,string sourcePath, string destinationPath, string completeSavePath)
        {
            this.SourcePath = sourcePath;
            this.Name = name;
            this.CompleteSavePath = completeSavePath;
            this.DestinationPath = destinationPath;

            Folder = destinationPath + "/" + name + "_" + DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt");

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            TotalLengthFile = DirSize(SourcePath);
            TotalNumberFile = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories).Length;
            Console.WriteLine(SourcePath + " " + TotalLengthFile);
            RemainingNumberFile = TotalNumberFile;
            RemainingLengthFile = TotalLengthFile;

            StartCopy(this.SourcePath, Folder);
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
                string destWithoutParents = dest.Substring(Folder.Length);
                FileInfo fiComplete = new FileInfo(CompleteSavePath + destWithoutParents);
                FileInfo fiSource = new FileInfo(SourcePath + destWithoutParents);

                if (!File.Exists(CompleteSavePath + destWithoutParents) || fiComplete.LastWriteTimeUtc!= fiSource.LastWriteTimeUtc)
                {

                    File.Copy(file, dest);
                    var fi1 = new FileInfo(dest);
                    RemainingNumberFile--;
                    RemainingLengthFile -= fi1.Length;
                    Progression = RemainingLengthFile != 0 ? Convert.ToSingle(TotalLengthFile - RemainingLengthFile) / Convert.ToSingle(TotalLengthFile) * 100 : 100;
                    Console.WriteLine("remaining length : " + RemainingLengthFile + "remaining number : " + RemainingNumberFile + "length : " + fi1.Length + " Etat d'avancement = " + Progression + " % " + fi1.LastWriteTime);
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
