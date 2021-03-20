using System;
using System.IO;

namespace FileManager_Budgo
{
    public class MyDirectory
    {
        public string FileName { get { return fileName; } }
        string fileName;
        string folderPath;

        public MyDirectory(string folderPath, string fileName)
        {
            this.folderPath = Path.GetFullPath(folderPath);
            this.fileName = fileName;
        }

        public void GetDirsRecurs(string path, int level = 0)
        {
            string[] directories = Directory.GetDirectories(path);

            string indent = "";

            for (int i = 0; i < level; i++)
                indent += "   ";

            File.AppendAllText(fileName, indent + "|" + Environment.NewLine);
            File.AppendAllText(fileName, indent + "|");

            foreach (string dir in directories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                File.AppendAllText(fileName, "--" + directoryInfo.Name + Environment.NewLine);
                GetDirsRecurs(dir, level + 1);
            }
        }
    }
        
}