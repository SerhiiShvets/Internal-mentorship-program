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

            TreeItem<string> treeItem = new TreeItem<string>(path);
            DirectoryInfo dInfo = new DirectoryInfo(path);
            DirectoryInfo[] directories = treeItem.GetDirectoriesInDirectory(path);
            FileInfo[] files = treeItem.GetFilesInDirectory(path);


            string parent = dInfo.Parent.ToString();

            treeItem.Value = path;
            treeItem.Parent = new TreeItem<string>(parent);
            string indent = "";
            Console.WriteLine(path);
            treeItem.GetChildren(path, indent);
            Console.ReadKey();

        }

       
    }  
}
