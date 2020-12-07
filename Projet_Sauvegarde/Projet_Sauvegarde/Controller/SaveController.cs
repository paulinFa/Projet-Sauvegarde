using Projet_Sauvegarde.Model;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Projet_Sauvegarde.Controller
{
    /// <summary>
    /// Controller to controll Save System
    /// </summary>
    public class SaveController
    {
        public List<SaveTask> ListSave { get; set; }
        public string Extension { get; set; }
        public string Software { get; set; }
        private ParameterFile parameterFile = new ParameterFile();
        private MainWindow window;
        public SaveController(MainWindow mainWindow)
        {
            this.window = mainWindow;
            parameterFile.GetAllInformation();
            parameterFile.Update();
            this.ListSave = parameterFile.SaveTasksList;
            this.Extension = parameterFile.Extension;
            this.Software = parameterFile.Software;
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
            SaveTask saveTask = new SaveTask(type, name, sourcePath, destinationPath, completeSavePath);
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
            if (TestProcess())
            {
                window.PopupErrorProcess(Software);
            }
            else if (saveTask.Type == "differential")
            {
                DifferentialSave diff = new DifferentialSave();
                diff.CopyFolder(saveTask, Extension);
            }
            else if (saveTask.Type == "complete")
            {
                CompleteSave complete = new CompleteSave();
                complete.CopyFolder(saveTask, Extension);
            }
        }
        /// <summary>
        /// Start all Saves in list
        /// </summary>
        public void StartAllSaves()
        {
            foreach (SaveTask saveTask in ListSave)
            {
                if (TestProcess())
                {
                    window.PopupErrorProcess(Software);
                }
                else if (saveTask.Type == "differential")
                {
                    DifferentialSave diff = new DifferentialSave();
                    diff.CopyFolder(saveTask, Extension);
                }
                else if (saveTask.Type == "complete")
                {
                    CompleteSave complete = new CompleteSave();
                    complete.CopyFolder(saveTask, Extension);
                }
            }
        }
        /// <summary>
        /// Start all save in Parameter
        /// </summary>
        /// <param name="saveTasks">List of save to launch</param>
        public void StartMultipleSaves(List<SaveTask> saveTasks)
        {
            foreach (SaveTask saveTask in saveTasks)
            {
                if (TestProcess())
                {
                    window.PopupErrorProcess(Software);
                }
                else if (saveTask.Type == "differential")
                {
                    DifferentialSave diff = new DifferentialSave();
                    diff.CopyFolder(saveTask, Extension);
                }
                else if (saveTask.Type == "complete")
                {
                    CompleteSave complete = new CompleteSave();
                    complete.CopyFolder(saveTask, Extension);
                }
            }
        }
        /// <summary>
        /// Verify if software is launch
        /// </summary>
        /// <returns>Return false if software is not running</returns>
        public bool TestProcess()
        {
            Process[] pname = Process.GetProcessesByName(Software);
            if (pname.Length == 0)
                return false;
            else
                return true;
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
        /// Modify software from controller and parameterFile
        /// </summary>
        /// <param name="software">Process you want to check if is running bettewen saves</param>
        public void ModifySoftware(string software)
        {
            this.Software = software;
            parameterFile.Software = this.Software;
            parameterFile.Update();
        }
    }
}
