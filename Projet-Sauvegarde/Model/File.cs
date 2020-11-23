using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    class File
    {
        private String name;
        private String path;
        private DateTime creationDate;

        public String name
        {
            get { return name; }
            set { name = value; }
        
        }

        public String path
        {
            get { return path; }
            set { path = value; }
        
        }

        public DateTime creationDate
         {
            get { return creationDate; }
            set { creationDate = value; }
        
        }




        public void SetCreationDate(DateTime : date)
        {

        }

        public String TransformToJson(String DataToConverted)
        {

        }

        public DateTime TakeTime()
        {

        }




    }  //end Class File

} //end namespace
