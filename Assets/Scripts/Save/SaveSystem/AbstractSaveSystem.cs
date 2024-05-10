using System.IO;
using UnityEngine;

namespace Save.SaveSystem
{
    public abstract class AbstractSaveSystem
    {
        protected readonly string filePath;
        private const string FileName = "save";

        protected AbstractSaveSystem(string formatFilePath)
        {
            filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + $"{FileName}.{formatFilePath}";
        }
        
        protected void TryCreateFile()
        {
            if(File.Exists(filePath)) return;

            using (File.Create(filePath)) { }
        }
    }
}