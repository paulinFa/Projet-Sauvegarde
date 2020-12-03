using Projet_Sauvegarde.Model;
using Projet_Sauvegarde.View;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Projet_Sauvegarde.Controller
{
    public class SaveController
    {
        
        private List<SaveTask> listSave = new List<SaveTask>();
        private ParameterFile parameterFile = new ParameterFile();
        private string extension;
        private string software;
        public SaveController()
        {
            IView view = new ConsoleView();
            parameterFile.GetAllInformation();
            parameterFile.Update();
            this.listSave = parameterFile.SaveTasksList;
            this.extension = parameterFile.Extension;
            this.software = parameterFile.Software;
            view.setController(this);
            view.StartingView();
        }
        //Add save in queue
        public void AddOneSave(string type, string name, string sourcePath, string destinationPath, string completeSavePath = "")
        {
            SaveTask saveTask = new SaveTask(type, name, sourcePath, destinationPath, completeSavePath);
            listSave.Add(saveTask);
            parameterFile.SaveTasksList = this.listSave;
            parameterFile.Update();
        }
        public void DeleteSaves(SaveTask[] saveTask)
        {
            foreach(SaveTask save in saveTask)
            {
                listSave.Remove(save);
            }
            parameterFile.SaveTasksList = this.listSave;
            parameterFile.Update();
        }
        public void DeleteSave(SaveTask save)
        {
            listSave.Remove(save);
            parameterFile.SaveTasksList = this.listSave;
            parameterFile.Update();
        }
        public void StartOneSave(SaveTask saveTask)
        {
            if (TestProcess())
            {
                return;
            }
            if (saveTask.Type == "differential")
            {
                DifferentialSave diff = new DifferentialSave();
                diff.CopyFolder(saveTask, extension);
            }
            else if (saveTask.Type == "complete")
            {
                CompleteSave complete = new CompleteSave();
                complete.CopyFolder(saveTask, extension);
            }
        }
        public void StartAllSaves()
        {
            foreach (SaveTask saveTask in listSave)
            {
                if (TestProcess())
                {
                    return;
                }
                if (saveTask.Type == "differential")
                {
                    DifferentialSave diff = new DifferentialSave();
                    diff.CopyFolder(saveTask, extension);
                }
                else if (saveTask.Type == "complete")
                {
                    CompleteSave complete = new CompleteSave();
                    complete.CopyFolder(saveTask, extension);
                }
            }
        }
        public void StartMultipleSaves(List<SaveTask> saveTasks)
        {
            foreach (SaveTask saveTask in saveTasks)
            {
                if(TestProcess())
                {
                    return;
                }
                if (saveTask.Type == "differential")
                {
                    DifferentialSave diff = new DifferentialSave();
                    diff.CopyFolder(saveTask, extension);
                }
                else if (saveTask.Type == "complete")
                {
                    CompleteSave complete = new CompleteSave();
                    complete.CopyFolder(saveTask, extension);
                }
            }
        }
        public void UpdateParameter()
        {

        }
        public bool TestProcess()
        {
            Process[] pname = Process.GetProcessesByName(software);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }

    }
}
