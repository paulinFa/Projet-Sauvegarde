using Grpc.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SearchOption = System.IO.SearchOption;

namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class to make CompleteSave
    /// </summary>
    internal class CompleteSave : Save, IStartSave
    {

        /// <summary>
        /// Method to initialized complete save
        /// </summary>
        /// <param name="saveTask">All informations for save</param>
        /// <param name="extension">Extension file to crypt</param>
        public void CopyFolder(SaveTask saveTask, string extension, LogFile log,StateFile state,string tall)
        {
            this.Tall = tall;
            this.isRunning = true;
            this.IsPaused = false;
            this.IsPausedProcess = false;
            this.IsStop = false;

            this.Progression = 0;
            //Initialize all values
            DateTime firstDate = DateTime.Now;
            this.SourcePath = saveTask.SourcePath;
            this.Name = saveTask.Name;
            this.DestinationPath = saveTask.DestinationPath;
            this.Extension = extension;

            this.logFile = log;
            this.stateFile = state;

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

            Progression = 100;


            logFile.ModifyData(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, SourcePath, DestinationPath, (int)TotalLengthFile, diffString, TimeEncryption);
            stateFile.ModifyData(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name, "inactive", TotalNumberFile, (int)TotalLengthFile, Progression, RemainingNumberFile, (int)RemainingLengthFile, SourcePath, DestinationPath);
            this.isRunning = false;
        }

        public float GetProgression()
        {
            return Progression;
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
                while (this.IsPaused == true)
                {
                }
                while (this.IsPausedProcess == true)
                {
                }
                while (this.IsStop == true)
                {
                    this.isRunning = false;
                    Save.IsCopyBigFile = false;
                    this.Progression = 0;
                    Thread.CurrentThread.Interrupt();
                }

                //Verify if file existe in destination 
                if (!File.Exists(dest))
                {
                    var fi1 = new FileInfo(file);
                    if(this.Tall != "" || fi1.Length >= int.Parse(this.Tall))
                    {
                        while (Save.IsCopyBigFile == true)
                        {
                        }
                    }
                    if (this.Tall != "" || fi1.Length >= int.Parse(this.Tall))
                    {
                        Save.IsCopyBigFile = true;
                    }
                    if (Path.GetExtension(dest) == Extension)
                    {
                        Process CryptoSoft = new Process();
                        CryptoSoft.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/EasySave/CryptoSoft.exe";
                        CryptoSoft.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        CryptoSoft.StartInfo.Arguments = $"{file} {dest}";
                        CryptoSoft.StartInfo.RedirectStandardOutput = true;
                        CryptoSoft.StartInfo.CreateNoWindow = true;
                        CryptoSoft.Start();
                        StreamReader reader = CryptoSoft.StandardOutput;
                        string CryptTime = reader.ReadToEnd();
                        if (CryptTime == "ERROR" || CryptTime == null || CryptTime == "")
                        {

                        }

                        else
                        {
                            CryptTime = CryptTime.Substring(0,CryptTime.Length - 4);
                            TimeEncryption += float.Parse(CryptTime);
                        }
                        CryptoSoft.Close();
                    }
                    else
                    {
                        File.Copy(file, dest);
                    }
                    if (this.Tall != "" || fi1.Length >= int.Parse(this.Tall))
                    {
                        Save.IsCopyBigFile = false;
                    }

                    RemainingNumberFile--;
                    RemainingLengthFile -= fi1.Length;
                    
                    //Watch the progress of moving files relative to their size
                    Progression = RemainingLengthFile != 0 ? Convert.ToSingle(TotalLengthFile - RemainingLengthFile) / Convert.ToSingle(TotalLengthFile) * 100 : 100;
                    stateFile.ModifyData(DateTime.Now.ToString("MM-dd-yyyy_hh.ss.mm_tt"), Name,"active", TotalNumberFile, (int)TotalLengthFile, Progression, RemainingNumberFile, (int)RemainingLengthFile, SourcePath, DestinationPath);
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
        public void ModifyPause()
        {
            if (IsPaused)
            {
                IsPaused = false;
            }
            else
            {
                IsPaused = true;
            }
        }
        public void ModifyStop()
        {
            if (IsStop)
            {
                IsStop = false;
            }
            else
            {
                IsStop = true;
            }
        }
        public bool GetIfStop()
        {
            return this.IsStop;
        }
        public bool GetIfPause()
        {
            return this.IsPaused;
        }
        public bool GetIsRunning()
        {
            return this.isRunning;
        }
        public void ModifyPauseProcess(bool a)
        {
            IsPausedProcess = a;
        }
        public bool GetIsPausedProcess()
        {
            return IsPausedProcess;
        }
    }

}
