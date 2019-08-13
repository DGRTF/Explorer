using System;
using System.IO;
using Equals;
using FastSortCompare;
using System.Collections.Generic;

namespace Explorer
{


    public class ComparisonFile : IComparison<FileInfo>
    {
        public int Compare(FileInfo o, FileInfo a)
        {
            if (o.Length == a.Length)
                return 2;
            else if (o.Length < a.Length)
                return 1;
            else
                return 3;
        }
    }




    public class InDirectory
    {
        public DirectoryInfo Backward(DirectoryInfo a)//Backward, in parent directory
        {
            a=a.Parent;
            return a;
        }
        public IEnumerable<DirectoryInfo> Directory(DirectoryInfo a)//return collection directories in current directory
        {
            return a.EnumerateDirectories();
        }
        public FileInfo[] Files(DirectoryInfo a)//return collection files in current directory
        {
            FileInfo[] g = new FileInfo[0];
            foreach (FileInfo n in a.EnumerateFiles())
            {
                Array.Resize(ref g, g.Length + 1);
                g[g.Length - 1] = n;
            }
            Sort<FileInfo> sort = new Sort<FileInfo>();
            g = sort.FastSort(g, new ComparisonFile());
            return g;
        }
        public DirectoryInfo ParentDirectory(DirectoryInfo a)//return Parent Directory
        {
            a = a.Parent;
            return a;
        }
        public FileInfo[] SearchFiles(string a, DirectoryInfo b)//Search Files
        {
            return b.GetFiles(a);
        }
        public DirectoryInfo[] SearchDirectory(string a, DirectoryInfo b)//Search Directories
        {
            return b.GetDirectories(a);
        }
    }
    class SearchDriver
    {
        public DriveInfo[] SearchParrentDirectory()//Return array Parent directory at devise
        {
            DirectoryInfo[] dire = new DirectoryInfo[0];

            foreach (DriveInfo n in DriveInfo.GetDrives())
            {
                if (n.TotalSize != 0)
                {
                    Array.Resize(ref dire, dire.Length + 1);
                    dire[dire.Length - 1] = new DirectoryInfo(n.Name);
                }
            }
            for (int i = 0; i < dire.Length; i++)
            {
                dire[i] = dire[i].Root;
                // while (dire[i].Parent != null)
                // {
                //     dire[i] = dire[i].Parent;

                // }
            }
            ParentDirectory<DirectoryInfo> par = new ParentDirectory<DirectoryInfo>();
            dire = par.ParDire(dire, new EquateI());
            DriveInfo[] drive = new DriveInfo[dire.Length];
            foreach (DirectoryInfo dir in par.ParDire(dire, new EquateI()))
            {
                int i = 0;
                drive[i] = new DriveInfo(dir.Name);
                i++;
            }
            Sort<DriveInfo> sort = new Sort<DriveInfo>();
            drive = sort.FastSort(drive, new Comparison());
            foreach (DriveInfo n in drive)
                System.Console.WriteLine(n.Name);
            return drive;
        }
    }
}