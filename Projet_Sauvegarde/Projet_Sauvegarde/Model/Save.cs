using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Projet_Sauvegarde.Model
{
    /// <summary>
    /// Class for gestion save
    /// </summary>
    class Save
    {
        [DefaultValue("")]
        internal volatile string Tall;
        [DefaultValue(false)]
        internal static volatile bool IsCopyBigFile;
        [DefaultValue(false)]
        internal volatile bool IsPausedProcess;
        [DefaultValue(false)]
        internal volatile bool isRunning;
        [DefaultValue(false)]
        internal volatile bool IsStop;
        [DefaultValue(false)]
        internal volatile bool IsPaused;
        [DefaultValue(0)]
        public float TimeEncryption { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public int TotalNumberFile { get; set; }
        public long TotalLengthFile { get; set; }

        [DefaultValue(0)]
        public volatile float Progression;

        [DefaultValue(0)]
        public long RemainingLengthFile { get; set; }

        [DefaultValue(0)]
        public int RemainingNumberFile { get; set; }

        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        internal LogFile logFile;
        internal StateFile stateFile;

        /// <summary>
        /// Méthod to take the size of directory
        /// </summary>
        /// <param name="directoryPath">Path of directory</param>
        /// <returns></returns>
        public static long DirSize(string directoryPath)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(directoryPath);
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
