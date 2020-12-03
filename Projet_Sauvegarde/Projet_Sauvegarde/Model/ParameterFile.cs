using Newtonsoft.Json;
using Projet_Sauvegarde.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class to modify create and deleter ParameterFile
    /// </summary>
    public class ParameterFile : FileModel
    {
        public ParameterJson parameterJson = new ParameterJson();
        public static string pathParameterFile = @"D:\EasySave\Parameter\Parameter.json";
        public string Extension { get; set; }
        public List<SaveTask> SaveTasksList { get; set; }
        public string Software { get; set; }
        public ParameterFile()
        {
            SaveTasksList = new List<SaveTask>();
            CreateFolder();
            CreateFolderParameter();
        }
        /// <summary>
        /// Method to create folder if not exist
        /// </summary>
        private void CreateFolderParameter()
        {
            DirectoryInfo EasyFolderLog = new DirectoryInfo(@"D:\EasySave\Parameter");

            if (EasyFolderLog.Exists)
            {

            }
            else
            {
                EasyFolderLog.Create();
            }
        }
        /// <summary>
        /// Method to modify SaveTask
        /// </summary>
        /// <param name="saveTasks">List of SaveTask</param>
        public void ModifyListSave(List<SaveTask> saveTasks)
        {
            this.SaveTasksList = saveTasks;
        }
        /// <summary>
        /// Method to update Parameter file
        /// </summary>
        public void Update()
        {
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
        /// Méthod to transform class ParameterJson to json and write in file
        /// </summary>
        private void TransformToJsonState() //Method to create the JSON file and insert parameters in it 
        {
            parameterJson.SaveTasksList = this.SaveTasksList;
            parameterJson.Extension = this.Extension;
            string WroteJson = JsonConvert.SerializeObject(parameterJson, Formatting.Indented); //object to add parameters into the JSON file
            using (var tw = new StreamWriter(pathParameterFile, false))
            {
                tw.WriteLine(WroteJson.ToString());  //We write the object WroteJson into the file
                tw.Close();
            }
        }
        /// <summary>
        /// Méthod to update parameter in this class from data in file
        /// </summary>
        public void GetAllInformation()
        {
            if (File.Exists(pathParameterFile)) //Verification if PathStateFile is already create
            {
                try
                {
                    var stream = File.OpenText(pathParameterFile);
                    string json = stream.ReadToEnd();
                    stream.Close();
                    ParameterJson parameterFile = (ParameterJson)JsonConvert.DeserializeObject<ParameterJson>(json);
                    if(parameterFile != null)
                    {
                        if (parameterFile.SaveTasksList != null)
                        {
                            this.SaveTasksList = parameterFile.SaveTasksList;
                        }
                        if (parameterFile.Extension != null)
                        {
                            this.Extension = parameterFile.Extension;
                        }
                        if (parameterFile.Software != null)
                        {
                            this.Software = parameterFile.Software;
                        }
                    }


                    Trace.WriteLine(Extension);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            else if (!File.Exists(PathStateFile))  //create and insert parameters
            {
            }
        }
    }
}
/// <summary>
/// Class to transform data in json and after write in file
/// </summary>
public class ParameterJson
{
    public string Extension { get; set; }
    public List<SaveTask> SaveTasksList { get; set; }
    public string Software { get; set; }
}

