﻿using Newtonsoft.Json;
using System.IO;


namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class to use Statefile
    /// </summary>
    public class StateFile : FileModel
    {
        JsonFileState dataJsonState = new JsonFileState();
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timestamp">Elapsed time for backup</param>
        /// <param name="nameOfSave"></param>
        /// <param name="state">State of save (Active|inactive)</param>
        /// <param name="eligibleFile">Total Number of file</param>
        /// <param name="transfertSize"></param>
        /// <param name="progression">progression in percentage</param>
        /// <param name="remainingFile">Number of file remaining</param>
        /// <param name="sizeOfRemainingFile">Size in byte of file remaining</param>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public StateFile(string timestamp, string nameOfSave, string state, int eligibleFile, int transfertSize, float progression, int remainingFile, int sizeOfRemainingFile, string sourcePath, string destinationPath) //Method where objetcs are used into parameters, then create the StateFile and insert the parameters in it.
        {
            CreateFolderStatus();

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

            PathStateFile = @"D:\EasySave\Status\StatusBackup" + "_" + nameOfSave + ".json"; //Definition path of StateFile + json format

            if (File.Exists(PathStateFile)) //Verification if PathStateFile is already create
            {
                TransformToJsonState(); //insert parameters
            }

            else if (!File.Exists(PathStateFile))  //create and insert parameters
            {
                TransformToJsonState();
            }
        }
        /// <summary>
        /// Tranfsorm JsonFileState in json and write in file
        /// </summary>
        private void TransformToJsonState() //Method to create the JSON file and insert parameters in it 
        {

            string WroteJson = JsonConvert.SerializeObject(dataJsonState, Formatting.Indented); //object to add parameters into the JSON file

            using (var tw = new StreamWriter(PathStateFile, true))
            {
                tw.WriteLine(WroteJson.ToString());  //We write the object WroteJson into the file
                tw.Close();
            }
        }
    } // end class StateFile 
    /// <summary>
    /// Class to transform data in json and after write in file
    /// </summary>
    class JsonFileState   //class to declare alls objects for the parameters 
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

    } //end class JsonFileState
} //end namespace
