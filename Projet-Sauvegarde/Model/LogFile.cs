using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Model
{
   
    public class LogFile : FileModel
    {


  
        public LogFile()
        {
            Path = @"D:\TestFile.txt";

            try
            {
                // Create the file, or overwrite if the file exists.
                Console.WriteLine(File.Exists(Path) ? "File exists." : "File does not exist.");
                
                
                using (FileStream fs = File.Create(Path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file. \n" + StringDate);
                    
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

        public void AddData(string data)
        {

        }















    }
}
