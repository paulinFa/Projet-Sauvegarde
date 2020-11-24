using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Projet_Sauvegarde.Model
{
    public class StateFile : FileModel
    {
        JsonFileState dataJsonState = new JsonFileState();
        public string template { get; set; }

        public StateFile(string timestamp, string nameOfSave, string state, int eligibleFile, int transfertSize, float progression, int remainingFile, int sizeOfRemainingFile, string sourcePath, string destinationPath)
        {
            dataJsonState.Timestamp = timestamp;
            dataJsonState.NameOfSave = nameOfSave;
            dataJsonState.State = state;
            dataJsonState.EligibleFile = eligibleFile;
            dataJsonState.TransfertSize = transfertSize;
            dataJsonState.Progression = progression;
            dataJsonState.RemainingFile = remainingFile;
            dataJsonState.SizeOfRemainingFile = sizeOfRemainingFile;
            dataJsonState.SourcePath = sourcePath;
            dataJsonState.DestinationPath = destinationPath;

            PathStateFile = @"D:\StateFile" + "-" + nameOfSave + ".json";

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
        public string State { get; set; }
        public int EligibleFile { get; set; }
        public int TransfertSize { get; set; }
        public float Progression { get; set; }
        public int RemainingFile { get; set; }
        public int SizeOfRemainingFile { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }

    }
} //end namespace
