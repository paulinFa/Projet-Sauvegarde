using Newtonsoft.Json;
using System.IO;




namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class to modify create and deleter Logfile
    /// </summary>
    public class LogFile : FileModel
    {

        JsonFileLog dataJsonLog = new JsonFileLog();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timestamp">Elapsed time for backup</param>
        /// <param name="nameOfSave"></param>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="sizeSave"></param>
        /// <param name="transfertTime"></param>
        /// <param name="timeEncryptionTransfert"></param>
        public LogFile(string timestamp, string nameOfSave, string sourcePath, string destinationPath, int sizeSave, string transfertTime,string timeEncryptionTransfert) //Method where objetcs are used into parameters, then create the LogFile and insert the parameters in it.
        {


            CreateFolderLog();


            dataJsonLog.Timestamp = timestamp;
            dataJsonLog.NameOfSave = nameOfSave;
            dataJsonLog.SourcePath = sourcePath;
            dataJsonLog.DestinationPath = destinationPath;
            dataJsonLog.SizeOfSave = sizeSave;
            dataJsonLog.TransfertTime = transfertTime;
            dataJsonLog.TimeEncryptionTransfert = timeEncryptionTransfert;


            PathLogFile = @"D:\EasySave\Logs\Log" + "_" + StringDateLogFile + ".json"; //Definition path of LogFile + json format

            if (File.Exists(PathLogFile)) //Verification if PathLogFile is already create
            {


                TransformToJsonLog(); //insert parameters
            }

            else if (!File.Exists(PathLogFile)) //create and insert parameters
            {

                TransformToJsonLog();
            }
        }

        private void TransformToJsonLog() //Method to create the JSON file and insert parameters in it 
        {

            string WroteJson = JsonConvert.SerializeObject(dataJsonLog, Formatting.Indented); //object to add parameters into the JSON file

            using (var tw = new StreamWriter(PathLogFile, true))
            {
                tw.WriteLine(WroteJson.ToString()); //We write the object WroteJson into the file
                tw.Close();
            }

        }
    } // end class LogFile 
    class JsonFileLog //class to declare alls objects for the parameters 
    {

        public string Timestamp { get; set; }
        public string NameOfSave { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public int SizeOfSave { get; set; }
        public string TransfertTime { get; set; }
        public string TimeEncryptionTransfert { get; set; }

    } //end class JsonFileState

} //end namespace
