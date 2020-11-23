using Microsoft.OData.Edm;
using System;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    public class FileModel
    {


        public FileModel()
        {
            CreationDate = DateTime.Now;
            StringDate = CreationDate.ToString("G");
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Path { get; set; }

        public string StringDate { get; set; }


        /*public String TransformToJson(String DataToConverted)
        {

        }

        public DateTime TakeTime()
        {

        }
        */
        




    }  //end Class File

} //end namespace
