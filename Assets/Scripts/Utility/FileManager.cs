using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Assets.Scripts.Utility
{
    class FileManager
    {
        // Constructor
        public FileManager(string path)
        {
            this.path = path;
        }

        // Attributes
        string path;

        // Methods
        private string ReadFile()
        {
            if (!File.Exists(path))
                File.Create(path);
            return File.ReadAllText(path);
        }

        public string GetElement(string key)
        {
            string[] file = ReadFile().Split('=', '\n');
            string element = null;
            for(int i = 0; i < file.Length; i += 2)
            {
                if (file[i] == key)
                    element = file[i + 1];
            }
            return element;
        }

        public void ChangeElement(string key, string element)
        {
            string[] file = ReadFile().Split('=');
            bool isDone = false;
            for (int i = 0; i < file.Length && !isDone; i += 2)
            {
                if (file[i] == key)
                {
                    file[i + 1] = element;
                    isDone = true;
                }
            }
        }
    }
}
