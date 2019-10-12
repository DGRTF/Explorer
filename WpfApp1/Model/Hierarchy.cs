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
        public HierarchyDrive(DirectoryInfo dir)
        {
            Drive = dir;
            AddDrive(Drive);
        }
        public DirectoryInfo Drive { get; set; }

        public ObservableCollection<HierarchyDrive> DriveH { get; set; } = new ObservableCollection<HierarchyDrive>();

        public void AddDrive(DirectoryInfo dir)
        {
            try
            {
                foreach (DirectoryInfo d in dir.EnumerateDirectories())
                {
                    DriveH.Add(new HierarchyDrive(d));
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
               // MessageBox.Show(e.Message);
            }
            catch
            {
               // MessageBox.Show(dir.Name);
            }
        }

    }
}
