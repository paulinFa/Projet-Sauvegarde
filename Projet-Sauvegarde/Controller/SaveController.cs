using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Projet_Sauvegarde.Model;

namespace Projet_Sauvegarde.Controller
{
    class SaveController
    {
        Queue queue = new Queue();
        SaveController()
        {

        }
        public void AddSave(string[] tbl)
        {
            queue.Enqueue(tbl);
        }
        public void DeleteSave()
        {

        }
        public void StartSave()
        {
            foreach(string[] str in queue)
            {
                if(str[0]=="differential")
                {
                    DifferentialSave diff = new DifferentialSave();
                    diff.CopyFolder(str[1], str[2], str[3], str[4]);
                }
                else if(str[0]=="complete")
                {
                    CompleteSave complete = new CompleteSave();
                    complete.CopyFolder(str[1], str[2], str[3]);
                }
            }
        }

    }
}
