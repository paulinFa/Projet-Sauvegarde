using System;
using System.IO;

namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Model to modify create and delete different File
    /// </summary>
    public class FileModel
    {


        public FileModel()
        {
            CreateFolder();

            CreationDate = DateTime.Now;

            StringDateLogFile = CreationDate.ToString("D"); //We convert the format of the date "monday 15 june 2009"
            StringDateStateFile = CreationDate.ToString("D"); //We convert the format of the date "monday 15 june 2009"
        }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string PathLogFile { get; set; }

        public string PathStateFile { get; set; }

        public string StringDateLogFile { get; set; }

        public string StringDateStateFile { get; set; }
        /// <summary>
        /// Method to create parent directory
        /// </summary>
        public void CreateFolder() // Method which makes it possible to check if the EsaySave folder exists and to create it if need be.
        {
            DirectoryInfo EasyFolder = new DirectoryInfo(@"D:\EasySave");

            if (EasyFolder.Exists)
            {

            }
            else
            {
                EasyFolder.Create();
            }


        }
        /// <summary>
        /// Method to create log directory
        /// </summary>
        public void CreateFolderLog() //Method which makes it possible to check if the Logs folder exists and to create it if need be.
        {
            DirectoryInfo EasyFolderLog = new DirectoryInfo(@"D:\EasySave\Logs");

            if (EasyFolderLog.Exists)
            {

            }
            else
            {
                EasyFolderLog.Create();
            }
        }
        /// <summary>
        /// Method to create status directory
        /// </summary>
        public void CreateFolderStatus() //Method which makes it possible to check if the Status folder exists and to create it if need be.
        {
            DirectoryInfo EasyFolderStatus = new DirectoryInfo(@"D:\EasySave\Status");

            if (EasyFolderStatus.Exists)
            {

            }
            else
            {
                EasyFolderStatus.Create();
            }
        }


    }  //end Class File




} //end namespace
