using System;
using System.IO;
using Equals;
using FastSortCompare;

namespace Explorer
{
    class SearchDriver
    {
        public DriveInfo[] SearchSelect()
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
            //  foreach (DirectoryInfo n in dire)
            //     System.Console.WriteLine(n.Root);
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