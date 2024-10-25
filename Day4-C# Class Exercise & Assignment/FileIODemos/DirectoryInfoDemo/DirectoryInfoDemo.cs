using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryInfoDemo
{
    internal class DirectoryInfoDemo
    {
        public void Demo()
        {
            string sourcePath = @"D:\HEXAWARE\C#\DOT NET FSD\Assignments\SampleSource";
            string destinationPath = @"D:\HEXAWARE\C#\DOT NET FSD\Assignments\SampleDestinations";

            DirectoryInfo sdi = new DirectoryInfo(sourcePath);
            DirectoryInfo ddi = new DirectoryInfo(destinationPath);
            //foreach(DirectoryInfo directoryInfo in sdi.GetDirectories())
            //{
            //    Console.WriteLine($"{directoryInfo.FullName} \n");
            //    foreach(FileInfo fileInfo in directoryInfo.GetFiles())
            //    {
            //        Console.WriteLine($"{fileInfo.FullName}");
            //    }
            //}


            foreach(FileInfo fi in sdi.GetFiles())
            {
                fi.CopyTo(Path.Combine(ddi.FullName, fi.Name), true);
                Console.WriteLine($" copying the {ddi.FullName} {fi.Name}");

            }
            foreach(DirectoryInfo directoryInfo in sdi.GetDirectories())
            {
                DirectoryInfo destinationSubDir = ddi.CreateSubdirectory(directoryInfo.Name);
                foreach(FileInfo file in directoryInfo.GetFiles())
                {
                    file.CopyTo(Path.Combine(destinationSubDir.FullName,file.Name), true);
                    {
                        Console.WriteLine($"Copying {destinationSubDir.Name} {file.Name}");
                    }
                }
            }

        }
    }
}
