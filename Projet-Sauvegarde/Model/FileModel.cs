using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Projet_Sauvegarde.Model
{
    public class FileModel
    {

        JsonFile dataJson = new JsonFile();
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



        public void TransformToJson()
        {
            string WroteJson = JsonConvert.SerializeObject(dataJson);

            using (var tw = new StreamWriter(PathLogFile, true))
            {
                tw.WriteLine(WroteJson.ToString());
                tw.Close();
            }

        }

        /*public String TransformToJson(String DataToConverted)
        {

        }

        public DateTime TakeTime()
        {

        }
        */





    }  //end Class File

} //end namespace
