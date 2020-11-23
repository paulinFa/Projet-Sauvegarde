using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Model
{
    class StateFile : FileModel
    {

        public string template { get; set; }

        public StateFile()
        {
            PathStateFile = @"D:\TestStateFile" + "-" + StringDateStateFile  + ".txt";

            try
            {
                // Create the file, or overwrite if the file exists.
                Console.WriteLine(File.Exists(PathStateFile) ? "File exists." : "File does not exist.");
                
                
                using (FileStream fs = File.Create(PathStateFile))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file. \n" + StringDateStateFile);
                    
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                   
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("This file already exist.");
            }
        }

        /*
        public void ModifyData(String Data)
        {

        }

        public void SetProgress(int Progress)
        {
        
        }

        public void SetRemainingFile(int RemainingFile)
        {
        
        }

        public void SetRemainingFileSize(int RemainingFileSize)
        {
        
        }

        public void SetState(bool State)
        {
        
        }

        */


    } //end class StateFile
} //end namespace
