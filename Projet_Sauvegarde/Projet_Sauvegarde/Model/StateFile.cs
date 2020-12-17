using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class to modify create and deleter StateFile
    /// </summary>
    public class StateFile : FileModel
    {
        JsonFileState dataJsonState = new JsonFileState();
        private readonly object _lockObject = new object();

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
        public StateFile() //Method where objetcs are used into parameters, then create the StateFile and insert the parameters in it.
        {
            CreateFolderStatus();
        }
        public void ModifyData(string timestamp, string nameOfSave, string state, int eligibleFile, int transfertSize, float progression, int remainingFile, int sizeOfRemainingFile, string sourcePath, string destinationPath) //Method where objetcs are used into parameters, then create the StateFile and insert the parameters in it.

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

            PathStateFile = @"D:\EasySave\Status\StatusBackup" + "_" + nameOfSave + ".json"; //Definition path of StateFile + json format
            new Thread(() => TransformToJsonState()).Start();
        }

        /// <summary>
        /// Tranfsorm JsonFileState in json and write in file
        /// </summary>
        private void TransformToJsonState() //Method to create the JSON file and insert parameters in it 
        {

            string WroteJson = JsonConvert.SerializeObject(dataJsonState, Formatting.Indented); //object to add parameters into the JSON file
            lock (_lockObject)
            {
                FileStream fs = new FileStream(PathStateFile, FileMode.OpenOrCreate);
                StreamWriter str = new StreamWriter(fs);
                str.BaseStream.Seek(0, SeekOrigin.End);
                str.WriteLine(WroteJson.ToString());
                str.Flush();
                str.Close();
                fs.Close();
            }    

        }
    }
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
