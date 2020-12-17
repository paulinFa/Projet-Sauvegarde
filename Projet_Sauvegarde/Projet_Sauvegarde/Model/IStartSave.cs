using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    public interface IStartSave
    {
        public void CopyFolder(SaveTask saveTask, string extension,LogFile log, StateFile state, string tall);

        public float GetProgression();

        public void ModifyPause();

        public void ModifyStop();
        public bool GetIfStop();
        public bool GetIfPause();
        public bool GetIsRunning();
        public void ModifyPauseProcess(bool a);
        public bool GetIsPausedProcess();

    }
}
