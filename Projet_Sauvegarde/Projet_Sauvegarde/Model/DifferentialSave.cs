using System;
using System.IO;

namespace Projet_Sauvegarde.Model
{
    class DifferentialSave : Save
    {

        public string CompleteSavePath { get; set; }
        public string Folder { get; set; }
        public void CopyFolder(SaveTask saveTask, string extension)
        {

            //Initialize all values
            DateTime firstDate = DateTime.Now;
            this.SourcePath = saveTask.SourcePath;
            this.Name = saveTask.Name;
            this.CompleteSavePath = saveTask.CompleteSavePath;
            this.DestinationPath = saveTask.DestinationPath;
            this.Extension = extension;

            Folder = DestinationPath + "/" + Name + "_" + DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt");

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            TotalLengthFile = DirSize(SourcePath);
            TotalNumberFile = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories).Length;

            RemainingNumberFile = TotalNumberFile;
            RemainingLengthFile = TotalLengthFile;

            //Start copy after initialization
            StartCopy(this.SourcePath, Folder);

            DateTime secondDate = DateTime.Now;

            TimeSpan diff = secondDate.Subtract(firstDate);

            //Calcul time duration copy
            string diffString = diff.ToString();

            //Update log and state file
            new LogFile(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, SourcePath, DestinationPath, (int)TotalLengthFile, diffString);
            new StateFile(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, "active", TotalNumberFile, (int)TotalLengthFile, Progression, RemainingNumberFile, (int)RemainingLengthFile, SourcePath, DestinationPath);

        }

        public void StartCopy(string sourcePath, string destinationPath)
        {

            //Check if directory exist else create
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

                //Verify if file existe in destination or if source file and complete file
                if (!File.Exists(CompleteSavePath + destWithoutParents) || fiComplete.LastWriteTimeUtc != fiSource.LastWriteTimeUtc)
                {
                    if (Path.GetExtension(dest) == Extension)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        File.Copy(file, dest);
                    }
                    var fi1 = new FileInfo(dest);
                    RemainingNumberFile--;
                    RemainingLengthFile -= fi1.Length;

                    //Watch the progress of moving files relative to their size
                    Progression = RemainingLengthFile != 0 ? Convert.ToSingle(TotalLengthFile - RemainingLengthFile) / Convert.ToSingle(TotalLengthFile) * 100 : 100;
                    Console.WriteLine("remaining length : " + RemainingLengthFile + "remaining number : " + RemainingNumberFile + "length : " + fi1.Length + " Etat d'avancement = " + Progression + " % " + fi1.LastWriteTime);
                    new StateFile(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, name, TotalNumberFile, (int)TotalLengthFile, Progression, RemainingNumberFile, (int)RemainingLengthFile, SourcePath, DestinationPath);
                }

            }
            string[] folders = Directory.GetDirectories(sourcePath);
            //Create folder is not exist
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
