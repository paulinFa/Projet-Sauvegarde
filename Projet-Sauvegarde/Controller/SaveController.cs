using Projet_Sauvegarde.Model;
using Projet_Sauvegarde.View;
using System.Collections;

namespace Projet_Sauvegarde.Controller
{
    public class SaveController
    {
        private Queue queue = new Queue();
        public SaveController()
        {
            IView view = new ConsoleView();
            view.setController(this);
            view.StartingView();
        }
        //Add save in queue
        public void AddSave(string[] tbl)
        {
            queue.Enqueue(tbl);
        }
        public void DeleteSave()
        {

        }
        public void StartSave()
        {
            foreach (string[] str in queue)
            {
                if (str[0] == "differential")
                {
                    DifferentialSave diff = new DifferentialSave();
                    diff.CopyFolder(str[1], str[2], str[3], str[4]);
                }
                else if (str[0] == "complete")
                {
                    CompleteSave complete = new CompleteSave();
                    complete.CopyFolder(str[1], str[2], str[3]);
                }
            }
        }

    }
}
