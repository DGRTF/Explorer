using Explorer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{




    public class ExplorerViewModel : INotifyPropertyChanged
    {


        //Properties and field:



        private bool enabledBut=false;  //Parameter readiness button
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
                        if ((obj as DirectoryInfo).Parent == null)
                        {
                            Initialize();
                        }
                        else
                            OpenDirecrory(obj);
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

            if (dir.Parent != null)                                           //if parent directory is
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


        //Construcrors:
        public ExplorerViewModel()
        {
            Initialize();
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

