using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson2
{
    public class Task2 : IRunnable
    {
        public void Run()
        {
            //Console.WriteLine("Please, input the path to the directory you want to explore");
            string path = "C:\\Users\\Сергій\\Documents\\temp"; //Console.ReadLine();

            TreeItem<int, string> treeItem = new TreeItem<int, string>(path);
            DirectoryInfo dInfo = new DirectoryInfo(path);
            DirectoryInfo[] directories = treeItem.GetDirectoriesInDirectory(path);
            FileInfo[] files = treeItem.GetFilesInDirectory(path);


            string parent = dInfo.Parent.ToString();

            treeItem.value = path;
            treeItem.parent = parent;

            treeItem.GetChildren(path);
            Console.ReadKey();

        }
    }  
}
