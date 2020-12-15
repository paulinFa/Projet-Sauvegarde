using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class with Save information
    /// </summary>
    public class SaveTask
    {
        [JsonIgnore]
        public string Tall { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string CompleteSavePath { get; set; }
        [JsonIgnore]
        public float Progression { get; set; }
        private IStartSave StartSave { get; set; }
        private string extension;
        private LogFile logFile;
        private StateFile stateFile;


        /// <summary>
        /// Class with information for save
        /// </summary>
        /// <param name="type">Type of save (Differential|complete)</param>
        /// <param name="name">Name of save</param>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="completeSavePath"></param>
        public SaveTask(string type,string name, string sourcePath,string destinationPath,string tall ,string completeSavePath = "")
        {
            this.Type = type;
            this.Name = name;
            this.SourcePath = sourcePath;
            this.DestinationPath = destinationPath;
            this.CompleteSavePath = completeSavePath;
            this.Tall = tall;

            if(Type == "complete")
            {
                this.StartSave = new CompleteSave();
            }
            else
            {
                this.StartSave = new DifferentialSave();
            }
        }

        public void LaunchThread(string extension, LogFile log,StateFile state,string tall)
        {
            this.extension = extension;
            this.stateFile = state;
            this.logFile = log;
            this.Tall = tall;
            new Thread(LaunchSave).Start();
            new Thread(GetProgression).Start();
        }
        private void LaunchSave()
        {
            if(StartSave.GetIfPause() == true)
            {
                StartSave.ModifyPause();
            }
            if (StartSave.GetIfStop() == true)
            {
                StartSave.ModifyStop();
            }
            StartSave.CopyFolder(this, extension, logFile, stateFile, Tall);
        }
        public void GetProgression()
        {
            (new Thread(() => {
                float progression = 0;
                while (progression != 100.0)
                {
                    progression = this.StartSave.GetProgression();
                    this.Progression = progression;

                }
            })).Start();
        }
        public void Stop()
        {
            this.StartSave.ModifyStop();
        }
        public void Pause()
        {
            this.StartSave.ModifyPause();
        }
        public bool GetIfStop()
        {
            return this.StartSave.GetIfStop();
        }
        public bool GetIfPause()
        {
            return this.StartSave.GetIfPause();
        }
        public bool GetIsRunning()
        {
            return this.StartSave.GetIsRunning();
        }
        public void PauseProcess()
        {
            this.StartSave.ModifyPauseProcess();
        }
        public bool GetIfPauseProcess()
        {
            return this.StartSave.GetIsPausedProcess();
        }
    }
}
