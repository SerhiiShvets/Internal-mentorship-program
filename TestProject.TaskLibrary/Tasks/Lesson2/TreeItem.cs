using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TestProject.TaskLibrary.Tasks.Lesson2
{
    class TreeItem <T, V>
    {
        public V value;
        public V parent;
        public Dictionary<T, V> children;

        public TreeItem(V value)
        {
            this.value = value;
        }

        public DirectoryInfo[] GetDirectoriesInDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryInfo[] directories = directory.GetDirectories();
            return directories;
        }


        public FileInfo[] GetFilesInDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            return files;
        }

        //I dont know how to implement this method using generics
        //public void GetChildren(V treeItemValue)
        //{   }

        public void GetChildren(string treeItemValue)
        {
            Console.WriteLine(treeItemValue);

            var gotDirectories = GetDirectoriesInDirectory(treeItemValue);
            var gotFiles = GetFilesInDirectory(treeItemValue);

            foreach (DirectoryInfo dir in gotDirectories)
            {
                Console.WriteLine(dir.ToString());
                gotDirectories = GetDirectoriesInDirectory(dir.ToString());
                gotFiles = GetFilesInDirectory(dir.ToString());
                foreach (DirectoryInfo d in gotDirectories)
                {
                    Console.WriteLine(d.ToString());
                    GetChildren(d.ToString());
                }
                foreach (FileInfo file in gotFiles)
                {
                    Console.WriteLine(file.ToString());
                }

            }
            foreach (FileInfo file in gotFiles)
            {
                Console.WriteLine(file.ToString());
            }
        }
    }
}
