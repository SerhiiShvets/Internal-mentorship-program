using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TestProject.TaskLibrary.Tasks.Lesson2
{
    class TreeItem <T, V>
    {
        private const string _cross = " ├─";
        private const string _corner = " └─";
        private const string _vertical = " │ ";
        private const string _space = "   ";

        public V Value { get; set; }
        public V Parent { get; set;  }
        public Dictionary<T, V> children;

        public TreeItem(V value)
        {
            this.Value = value;
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

            public void CompareDirectories()
        {

        }

        public void GetChildren(string treeItemValue, string indent)
        {
            var gotDirectories = GetDirectoriesInDirectory(treeItemValue);
            var gotFiles = GetFilesInDirectory(treeItemValue);

            indent = indent+_space;
            foreach (DirectoryInfo dir in gotDirectories)
            {
                
                if (dir.Name == gotDirectories[gotDirectories.Length-1].Name)
                {
                    Console.Write(indent+_corner );
                    //Console.Write(_corner);
                    //indent += _space;
                }
                else
                {
                    Console.Write(indent+_cross);
                    //Console.Write(_cross);
                    //indent += _vertical;
                    //indent += _space;
                }
                Console.WriteLine(dir.ToString());
                gotDirectories = GetDirectoriesInDirectory(dir.ToString());
                FileInfo[] gotFilesNext = GetFilesInDirectory(dir.ToString());
                indent = indent + _space;
                foreach (DirectoryInfo d in gotDirectories)
                {
                    //if(d == gotDirectories[gotDirectories.Length-1])
                    Console.Write(indent);
                    Console.WriteLine(d.ToString());
                    GetChildren(d.ToString(), indent);
                }
                foreach (FileInfo file in gotFilesNext)
                {
                    if (file.Name == gotFilesNext[gotFilesNext.Length - 1].Name)
                    {
                        Console.Write(indent + _corner);
                        //Console.Write(_corner);
                        //indent += _space;
                    }
                    else
                    {
                        Console.Write(indent + _cross);
                        //Console.Write(_cross);
                        //indent += _vertical;
                        //indent += _space;
                    }
                    //Console.Write(indent);
                    Console.WriteLine(file.ToString());
                }

            }
            foreach (FileInfo file in gotFiles)
            {
                if (file.Name == gotFiles[gotFiles.Length - 1].Name)
                {
                    Console.Write(_corner);
                    //Console.Write(_corner);
                    //indent += _space;
                }
                else
                {
                    Console.Write(_cross);
                    //Console.Write(_cross);
                    //indent += _vertical;
                    //indent += _space;
                }
                //Console.Write(indent);
                Console.WriteLine(file.ToString());
            }
        }
    }
}
