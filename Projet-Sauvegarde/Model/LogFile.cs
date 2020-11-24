using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;




namespace Projet_Sauvegarde.Model
{

    public class LogFile : FileModel
    {
        JsonFileLog dataJsonLog = new JsonFileLog();


        public LogFile(string timestamp, string nameOfSave, string sourcePath, string destinationPath, int sizeSave, string transfertTime)
        {
            dataJsonLog.Timestamp = timestamp;
            dataJsonLog.NameOfSave = nameOfSave;
            dataJsonLog.SourcePath = sourcePath;
            dataJsonLog.DestinationPath = destinationPath;
            dataJsonLog.SizeOfSave = sizeSave;
            dataJsonLog.TransfertTime = transfertTime;


            PathLogFile = "D:/LogFile" + "-" + StringDateLogFile + ".json";

            if (File.Exists(PathLogFile))
            {
                // Create the file, or overwrite if the file exists.
                Console.WriteLine("Log File exists.");

                TransformToJsonLog();
            }

            else if (!File.Exists(PathLogFile))
            {
                Console.WriteLine("Log File does not exist.");
                TransformToJsonLog();
            }
        }


        public void TransformToJsonLog()
        {

            string WroteJson = JsonConvert.SerializeObject(dataJsonLog, Formatting.Indented);

            using (var tw = new StreamWriter(PathLogFile, true))
            {
                tw.WriteLine(WroteJson.ToString());
                tw.Close();
            }

        }
    }
    class JsonFileLog
    {

        public string Timestamp { get; set; }
        public string NameOfSave { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public int SizeOfSave { get; set; }
        public string TransfertTime { get; set; }

    }

}
