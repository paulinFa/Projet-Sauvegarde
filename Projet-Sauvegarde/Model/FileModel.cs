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
            StringDateLogFile = CreationDate.ToString("D");
            StringDateStateFile = CreationDate.ToString("D");
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string PathLogFile { get; set; }
        public string PathStateFile { get; set; }
        public string StringDateLogFile { get; set; }
        public string StringDateStateFile { get; set; }


        /*public String TransformToJson(String DataToConverted)
        {

        }

        public DateTime TakeTime()
        {

        }
        */





    }  //end Class File

} //end namespace
