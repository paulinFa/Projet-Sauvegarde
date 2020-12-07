using Grpc.Core;
using System;
using System.Diagnostics;
using System.IO;
using SearchOption = System.IO.SearchOption;

namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class to make CompleteSave
    /// </summary>
    class CompleteSave : Save
    {
        /// <summary>
        /// Method to initialized complete save
        /// </summary>
        /// <param name="saveTask">All information for save</param>
        /// <param name="extension">Extension file to crypt</param>
        public void CopyFolder(SaveTask saveTask, string extension)
        {
            this.CryptoSoft = new Process();
            CryptoSoft.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/EasySave/CryptoSoft.exe";
            //CryptoSoft.StartInfo.FileName = @"D:/Documents/CryptoSoft.exe";


            //Initialize all values
            DateTime firstDate = DateTime.Now;
            this.SourcePath = saveTask.SourcePath;
            this.Name = saveTask.Name;
            this.DestinationPath = saveTask.DestinationPath;
            this.Extension = extension;

            TotalLengthFile = DirSize(SourcePath);
            TotalNumberFile = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories).Length;
            
            RemainingNumberFile = TotalNumberFile;
            RemainingLengthFile = TotalLengthFile;
            string folder = DestinationPath + "/" + Name + "_" + DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt");




            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);


            StartCopy(this.SourcePath, folder);

            DateTime secondDate = DateTime.Now;

            TimeSpan diff = secondDate.Subtract(firstDate);

            //Calcul time duration copy
            string diffString = diff.ToString();


            LogFile logFile = new LogFile(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, SourcePath, DestinationPath, (int)TotalLengthFile, diffString, TimeEncryption);
            StateFile stateFile = new StateFile(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, "active", TotalNumberFile, (int)TotalLengthFile, Progression, RemainingNumberFile, (int)RemainingLengthFile, SourcePath, DestinationPath);


        }
        /// <summary>
        /// Start Copy
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        private void StartCopy(string sourcePath, string destinationPath)
        {
            //Check if directory exist else create
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);
            string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destinationPath, name);
                //Verify if file existe in destination 
                if (!File.Exists(dest))
                {
                    if(Path.GetExtension(dest) == Extension)
                    {
                        CryptoSoft.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        CryptoSoft.StartInfo.Arguments = $"{file[0]} {dest}";
                        CryptoSoft.StartInfo.RedirectStandardOutput = true;
                        CryptoSoft.Start();
                        StreamReader reader = CryptoSoft.StandardOutput;
                        if(reader.ReadToEnd() != null)
                        {
                            string CryptTime = reader.ReadToEnd();
                            if(CryptTime != "")
                            {
                                TimeEncryption += Single.Parse(CryptTime);
                            }
                        }
                        CryptoSoft.WaitForExit();
                        CryptoSoft.Close();
                    }
                    else
                    {
                        File.Copy(file, dest);
                    }
                    var fi1 = new FileInfo(file);
                    RemainingNumberFile--;
                    RemainingLengthFile -= fi1.Length;

                    //Watch the progress of moving files relative to their size
                    Progression = RemainingLengthFile != 0 ? Convert.ToSingle(TotalLengthFile - RemainingLengthFile) / Convert.ToSingle(TotalLengthFile) * 100 : 100;
                    StateFile stateFile = new StateFile(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, name, TotalNumberFile, (int)TotalLengthFile, Progression, RemainingNumberFile, (int)RemainingLengthFile, SourcePath, DestinationPath);
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
