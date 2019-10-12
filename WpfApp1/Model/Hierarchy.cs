using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Security;

namespace WpfApp1.Hierarchy
{
    public class HierarchyDrive
    {
        public HierarchyDrive(FileInfo i)
        {
            Drive = i;
        }
        public HierarchyDrive(FileSystemInfo dir)
        {
            Drive = dir;
            AddDrive(Drive);
        }
        public FileSystemInfo Drive { get; set; }

        public ObservableCollection<HierarchyDrive> DriveH { get; set; } = new ObservableCollection<HierarchyDrive>();

        public void AddDrive(FileSystemInfo dir)
        {
            try
            {
                DirectoryInfo drive = dir as DirectoryInfo;
                foreach (DirectoryInfo d in drive.EnumerateDirectories())
                {
                    DriveH.Add(new HierarchyDrive(d));
                }
                foreach (FileInfo f in drive.EnumerateFiles())
                {
                    DriveH.Add(new HierarchyDrive(f));
                }
            }
            catch (DirectoryNotFoundException e)
            {
                //MessageBox.Show(e.Message);
            }
            catch (SecurityException e)
            {
                //MessageBox.Show(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                //MessageBox.Show(e.Message);
            }
            catch
            {
               // MessageBox.Show(dir.Name);
            }
        }

    }
}
