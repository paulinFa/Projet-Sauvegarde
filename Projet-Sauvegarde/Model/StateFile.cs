using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    public class StateFile : FileModel
    {
        JsonFileState dataJsonState = new JsonFileState();
        public string template { get; set; }

        public StateFile(string timestamp, string nameOfSave, string state, int eligibleFile, int transfertSize, float )
        {
            PathStateFile = @"D:\StateFile" + "-" + StringDateStateFile + ".json";

            if (File.Exists(PathStateFile))
            {
                // Create the file, or overwrite if the file exists.
                Console.WriteLine("State File exists.");

                TransformToJsonState();




            }

            else if (!File.Exists(PathStateFile))
            {
                Console.WriteLine("State File does not exist.");
                TransformToJsonState();
            }
        }

        public void TransformToJsonState()
        {

            string WroteJson = JsonConvert.SerializeObject(dataJsonState, Formatting.Indented);

            using (var tw = new StreamWriter(PathLogFile, true))
            {
                tw.WriteLine(WroteJson.ToString());
                tw.Close();
            }
        }
    }

    class JsonFileState
    {

        public string Timestamp { get; set; }
        public string NameOfSave { get; set; }
        public string SourcePath { get; set; }
        public string DesinationPath { get; set; }
        public int SizeOfSave { get; set; }
        public int TransfertTime { get; set; }

    }
} //end namespace
