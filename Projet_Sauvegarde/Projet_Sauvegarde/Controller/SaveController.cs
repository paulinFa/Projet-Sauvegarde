using Projet_Sauvegarde.Model;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Projet_Sauvegarde.Controller
{
    /// <summary>
    /// Controller to controll Save System
    /// </summary>
    public class SaveController
    {
        public List<SaveTask> ListSave { get; set; }
        public string Extension { get; set; }
        public string Tall { get; set; }
        public volatile string Software;
        private ParameterFile parameterFile = new ParameterFile();
        private MainWindow window;
        private LogFile logFile;
        private StateFile stateFile;
        public SaveController(MainWindow mainWindow)
        {
            this.window = mainWindow;
            parameterFile.GetAllInformation();
            parameterFile.Update();
            this.ListSave = parameterFile.SaveTasksList;
            this.Extension = parameterFile.Extension;
            this.Software = parameterFile.Software;
            this.Tall = parameterFile.TallMax;
            this.logFile = new LogFile();
            this.stateFile = new StateFile();
            new Thread(() =>
            {
                while(true)
                {
                    Thread.Sleep(200);                    
                    TestProcess();
                }
            }).Start();

        }
        /// <summary>
        /// Add One save to List 
        /// </summary>
        /// <param name="type">Type Of save (complete|differential)</param>
        /// <param name="name">Name of save</param>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="completeSavePath">If is differential save</param>
        public void AddOneSave(string type, string name, string sourcePath, string destinationPath, string completeSavePath = "")
        {
            SaveTask saveTask = new SaveTask(type, name, sourcePath, destinationPath,Tall,completeSavePath);
            ListSave.Add(saveTask);
            parameterFile.SaveTasksList = this.ListSave;
            parameterFile.Update();
        }
        /// <summary>
        /// Delete save from list and file parameter
        /// </summary>
        /// <param name="saveTask">Save to delete</param>
        public void DeleteSave(SaveTask saveTask)
        {
            ListSave.Remove(saveTask);
            parameterFile.SaveTasksList = this.ListSave;
            parameterFile.Update();
        }

        /// <summary>
        /// Delete a list of Save
        /// </summary>
        /// <param name="saveTasks">List of save to delete</param>
        public void DeleteSaves(List<SaveTask> saveTasks)
        {
            foreach (SaveTask save in saveTasks)
            {
                ListSave.Remove(save);
            }
            parameterFile.SaveTasksList = this.ListSave;
            parameterFile.Update();
        }

        /// <summary>
        /// Launch one save
        /// </summary>
        /// <param name="saveTask">Save to launch</param>
        public void StartOneSave(SaveTask saveTask)
        {
            if (saveTask != null)
            {
                if (!this.ListSave.Find((a) => a.Name == saveTask.Name).GetIsRunning())
                {
                    this.ListSave.Find((a) => a.Name == saveTask.Name).LaunchThread(this.Extension, logFile, stateFile, this.Tall);
                }
            }
            else
            {
                Trace.WriteLine("error task not existed");   
            }

        }
        /// <summary>
        /// Start all Saves in list
        /// </summary>
        public void StartAllSaves()
        {
            foreach (SaveTask saveTask in ListSave)
            {
                    if (!this.ListSave.Find((a) => a.Name == saveTask.Name).GetIsRunning())
                    {
                        this.ListSave.Find((a) => a.Name == saveTask.Name).LaunchThread(this.Extension, logFile, stateFile, this.Tall);
                    }
            }
        }
        /// <summary>
        /// Start all save in Parameter
        /// </summary>
        /// <param name="saveTasks">List of save to launch</param>
        public void StartMultipleSaves(List<SaveTask> saveTasks)
        {
            System.Console.WriteLine("multiple " + saveTasks.Count);
            foreach (SaveTask saveTask in saveTasks)
            {
                if (!this.ListSave.Find((a) => a.Name == saveTask.Name).GetIsRunning())
                {
                    this.ListSave.Find((a) => a.Name == saveTask.Name).LaunchThread(this.Extension, logFile, stateFile, this.Tall);
                }
            }
        }
        /// <summary>
        /// Modify extension from controller and parameterFile
        /// </summary>
        /// <param name="extension">Extension of file you want to crypt</param>
        public void ModifyExtension(string extension)
        {
            this.Extension = extension;
            parameterFile.Extension = this.Extension;
            parameterFile.Update();
        }
        /// <summary>
        /// Modify extension from controller and parameterFile
        /// </summary>
        /// <param name="extension">Extension of file you want to crypt</param>
        public void ModifyTall(string tall)
        {
            this.Tall = tall;
            parameterFile.TallMax = this.Tall;
            parameterFile.Update();
        }
        /// <summary>
        /// Modify software from controller and parameterFile
        /// </summary>
        /// <param name="software">Process you want to check if is running bettewen saves</param>
        public void ModifySoftware(string software)
        {
            this.Software = software;
            parameterFile.Software = this.Software;
            parameterFile.Update();
        }

        public float GetProgression(SaveTask saveTask)
        {
            return this.ListSave.Find((a) => a.Name == saveTask.Name).Progression;
        }
        public void TestProcess()
        {
            if(VerifyProcess() == true)
            {
                foreach(SaveTask saveTask in ListSave)
                {
                    if(saveTask.GetIfPauseProcess()==false)
                    {
                        saveTask.PauseProcess(true);
                    }
                }
            }
            else
            {
                foreach (SaveTask saveTask in ListSave)
                {
                    if (saveTask.GetIfPauseProcess() == true)
                    {
                        saveTask.PauseProcess(false);
                    }
                }
            }
        }
        public bool VerifyProcess()
        {
            Process[] pname = Process.GetProcessesByName(Software);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
    }
}
