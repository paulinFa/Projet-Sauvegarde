using Projet_Sauvegarde.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_Sauvegarde.View
{
    interface IView
    {
        public void StartingView();

        public void setController(SaveController controller);
    }
}
