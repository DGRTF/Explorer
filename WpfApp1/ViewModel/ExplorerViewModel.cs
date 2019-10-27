using Explorer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp1.Hierarchy;

namespace WpfApp1
{




    public class ExplorerViewModel : INotifyPropertyChanged
    {


        //Properties and field:



        public MainWindow Window { get; }



        private bool enabledBut = false;  //Parameter readiness button
        public bool EnabledBut
        {
            get
            {
                return enabledBut;
            }
            set
            {
                enabledBut = value;
                OnPropertyChanged("EnabledBut");
            }
        }



        private DirectoryInfo parameter;   //general parameter commands for manage directoties
        public DirectoryInfo Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
                OnPropertyChanged("Parameter");
            }
        }



        //Commands:
        private ExplorerCommand collDirInCurDir;
        public ExplorerCommand CollDirInCurDir    //return collection directories in current directory
        {
            get
            {
                return collDirInCurDir ?? (collDirInCurDir = new ExplorerCommand(obj =>
                {
                    OpenDirecrory(obj);

                }));

            }
        }



        private ExplorerCommand back;
        public ExplorerCommand Back
        {
            get
            {
                return back = new ExplorerCommand(obj =>
                {
                    BackParent(obj);
                });
            }
        }



        private ExplorerCommand refresh;
        public ExplorerCommand Refresh   //Command for button to refresh current direcrory
        {
            get
            {
                return refresh = new ExplorerCommand(obj =>
                {
                    if (obj != null)
                    {
                        OpenDirecrory(obj);

                        ObservableCollection<HierarchyDrive> D = new ObservableCollection<HierarchyDrive>();
                        foreach (HierarchyDrive h in DriveHier)
                        {
                            D.Add(h);
                        }

                        DriveHier.Clear();
                        foreach (HierarchyDrive h in SearchDirectoryInHier(D, (obj as FileSystemInfo)))
                        {
                            DriveHier.Add(h);
                        }
                    }
                    else
                        Initialize();
                });
            }
        }



        private ExplorerCommand home;
        public ExplorerCommand Home
        {
            get
            {
                return refresh = new ExplorerCommand(obj =>
                {
                    Initialize();
                    Parameter = null;
                    EnabledBut = false;
                });
            }
        }




        //Collections:
        public ObservableCollection<HierarchyDrive> DriveHier { get; set; } = new ObservableCollection<HierarchyDrive>();


        public ObservableCollection<FileSystemInfo> Drive { get; } = new ObservableCollection<FileSystemInfo>();




        //Methods:




        public void Initialize()
        {
            SearchDriver search = new SearchDriver();
            Drive.Clear();
            foreach (DirectoryInfo d in search.SearchParrentDirectory())
            {
                Drive.Add(d);
            }
        }



        public void OpenDirecrory(Object obj)
        {
            try
            {

                DirectoryInfo dir = obj as DirectoryInfo;
                IEnumerable<DirectoryInfo> dire = dir.EnumerateDirectories();  //add directories
                Drive.Clear();
                foreach (DirectoryInfo d in dire)
                {

                    Drive.Add(d);
                }

                foreach (FileInfo d in dir.EnumerateFiles())  //add files
                {
                    Drive.Add(d);
                }

                Parameter = dir;

                EnabledBut = true;


            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (SecurityException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
            }
            catch
            {
                MessageBox.Show("Unknown error");

            }
        }



        private void BackParent(Object obj)
        {

            //MessageBox.Show("fff1");
            DirectoryInfo dir = obj as DirectoryInfo;

            if (dir.Parent != null)                                           //if parent directory is exist
            {
                dir = dir.Parent;
                Drive.Clear();
                foreach (DirectoryInfo d in dir.EnumerateDirectories())       //add directory
                {
                    Drive.Add(d);
                }

                foreach (FileInfo d in dir.EnumerateFiles())                  //add files
                {
                    Drive.Add(d);
                }
                Parameter = dir;

            }
            else
            {
                EnabledBut = false;
                Parameter = null;
                Drive.Clear();
                Drive.Add(dir);

            }
        }



        private void InitHierarchy()//Initialize Hierarchy collection
        {
            SearchDriver search = new SearchDriver();
            ObservableCollection < HierarchyDrive > i = new ObservableCollection<HierarchyDrive>();
            try
            {
                foreach (DirectoryInfo d in search.SearchParrentDirectory())
                    i.Add(new HierarchyDrive(d));
            }
            catch
            {

            }
            Window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    foreach (HierarchyDrive h in i)
                        DriveHier.Add(h);
                });
        }



        public ObservableCollection<HierarchyDrive> SearchDirectoryInHier(ObservableCollection<HierarchyDrive> hier, FileSystemInfo dir, List<FileSystemInfo> list = null)
        {
            DirectoryInfo dit = (dir as DirectoryInfo);

            if (list == null)
            {
                DirectoryInfo d = dit;
                list = new List<FileSystemInfo>//collection parent directory
                    {
                        d
                    };
                while (d.Parent != null)
                {
                    d = d.Parent;
                    list.Add(d);
                }
            }

            List<FileSystemInfo> listm = new List<FileSystemInfo>();
            foreach (FileSystemInfo f in list)
            {
                listm.Add(f);
            }

            ObservableCollection<HierarchyDrive> hierm = new ObservableCollection<HierarchyDrive>();
            foreach (HierarchyDrive h in hier)
            {
                hierm.Add(h);
            }

            foreach (FileSystemInfo di in list)
            {
                foreach (HierarchyDrive hh in hier)
                {
                    if (di.FullName == hh.Drive.FullName)
                    {
                        listm.Remove(di);

                        if (listm.Count == 0)
                        {
                            List<FileSystemInfo> id = new List<FileSystemInfo>();//collection

                            foreach (HierarchyDrive hi in hh.DriveH)
                            {
                                id.Add(hi.Drive);
                            }

                            List<FileSystemInfo> idm = new List<FileSystemInfo>();
                            foreach (FileSystemInfo f in id)
                            {
                                idm.Add(f);
                            }


                            List<FileSystemInfo> i = new List<FileSystemInfo>();//fact collection

                            foreach (DirectoryInfo info in (di as DirectoryInfo).EnumerateDirectories())//Initialize fact collection(Directories)
                            {
                                i.Add(info);
                            }

                            foreach (FileSystemInfo f in (di as DirectoryInfo).EnumerateFiles())//Initialize fact collection(Files)
                                i.Add(f);

                            List<FileSystemInfo> im = new List<FileSystemInfo>();
                            foreach (FileSystemInfo f in i)
                            {
                                im.Add(f);
                            }


                            foreach (FileSystemInfo f in i)//equals fact collection and collection
                            {
                                foreach (FileSystemInfo ff in id)
                                {
                                    if (f.FullName == ff.FullName)
                                    {
                                        im.Remove(f);
                                        idm.Remove(ff);
                                    }
                                }
                            }

                            if (idm.Count != 0)
                            {
                                List<HierarchyDrive> dm = new List<HierarchyDrive>();
                                foreach (HierarchyDrive hi in hh.DriveH)//Remove object
                                {
                                    foreach (FileSystemInfo s in idm)
                                    {
                                        if (hi.Drive.FullName == s.FullName)
                                        {
                                            dm.Add(hi);
                                        }
                                    }
                                }
                                foreach (HierarchyDrive h in dm)
                                {
                                    hh.DriveH.Remove(h);
                                }
                            }

                            if (im.Count != 0)
                            {
                                foreach (FileSystemInfo s in im)//Add object
                                {
                                    hh.DriveH.Add(new HierarchyDrive(s));
                                }
                            }

                            if (im.Count != 0 || idm.Count != 0)//Sort collection
                            {
                                List<HierarchyDrive> list1 = new List<HierarchyDrive>();
                                List<HierarchyDrive> list2 = new List<HierarchyDrive>();
                                foreach (HierarchyDrive hi in hh.DriveH)
                                {
                                    if (hi.Drive.GetType().FullName.Equals("System.IO.DirectoryInfo") == true)
                                    {
                                        list1.Add(hi);
                                    }

                                    if (hi.Drive.GetType().FullName.Equals("System.IO.FileInfo") == true)
                                    {
                                        list2.Add(hi);
                                    }
                                }
                                list1.Sort(Sort);//Sort directories for name
                                list2.Sort(Sort);//Sort files for name
                                hh.DriveH.Clear();

                                foreach (HierarchyDrive h in list1)
                                {
                                    hh.DriveH.Add(h);
                                }
                                foreach (HierarchyDrive h in list2)
                                    hh.DriveH.Add(h);
                            }
                        }
                        else
                        {
                            hierm[hier.IndexOf(hh)].DriveH = SearchDirectoryInHier(hh.DriveH, dit, listm);//cause this method
                        }
                    }
                }
            }
            return hierm;
        }



        public int Sort(HierarchyDrive a, HierarchyDrive b)//Sort HierarchyDrive for alphabet
        {
            return String.Compare(a.Drive.Name, b.Drive.Name);
        }



        //Construcrors:
        public ExplorerViewModel(MainWindow window)
        {
            Window = window;
            Initialize();
            Thread threadHier = new Thread(InitHierarchy);
            threadHier.Start();
        }



        //Over:


        public event PropertyChangedEventHandler PropertyChanged;




        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }



}

