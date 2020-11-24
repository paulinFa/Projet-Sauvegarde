using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;



namespace Projet_Sauvegarde.Model
{

    public class LogFile : FileModel
    {

        

        public LogFile()
        {
            PathLogFile = "D:/TestLogFile" + "-" + StringDateLogFile + ".json";

            try
            {
                // Create the file, or overwrite if the file exists.
                Console.WriteLine(File.Exists(PathLogFile) ? "File exists." : "File does not exist.");


                TransformToJson();
                



                /*using (FileStream fs = File.Create(PathLogFile))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file. \n" + StringDateLogFile);
                    
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                   
                }*/

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
              
            }
        }

        public void AddData(string data)
        {

        }















    }
}
