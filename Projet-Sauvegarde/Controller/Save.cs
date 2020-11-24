using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Projet_Sauvegarde.Controller
{
    class Save
    {
        public string Name { get; set; }
        public int TotalNumberFile { get; set; }
        public long TotalLengthFile { get; set; }

        [DefaultValue(0)]
        public float Progression { get; set; }

        [DefaultValue(0)]
        public long RemainingLengthFile { get; set; }

        [DefaultValue(0)]
        public int RemainingNumberFile { get; set; }

        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }

        public static long DirSize(string d)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(d);
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = dir.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di.FullName);
            }
            return size;
        }
    }
}
