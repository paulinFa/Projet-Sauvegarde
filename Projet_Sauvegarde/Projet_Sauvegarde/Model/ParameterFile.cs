using Newtonsoft.Json;
using Projet_Sauvegarde.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Model
{
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

        public void CreateFolderParameter()
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

        public void ModifyListSave(List<SaveTask> saveTasks) //Method where objetcs are used into parameters, then create the StateFile and insert the parameters in it.
        {
            this.SaveTasksList = saveTasks;
        }

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

        public void TransformToJsonState() //Method to create the JSON file and insert parameters in it 
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
    public class ParameterJson
{
    public string Extension { get; set; }
    public List<SaveTask> SaveTasksList { get; set; }
    public string Software { get; set; }
}

