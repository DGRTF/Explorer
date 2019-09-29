﻿using Explorer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;

namespace WpfApp1
{




    public class ExplorerViewModel : INotifyPropertyChanged
    {



        private ObservableCollection<FileSystemInfo> drive;




        private ExplorerCommand back;



        public ExplorerCommand Back
        {
            get
            {
                return back = new ExplorerCommand(obj =>
                {
                    //MessageBox.Show("fff1");
                    DirectoryInfo dir = obj as DirectoryInfo;
                    ObservableCollection<FileSystemInfo> collection = new ObservableCollection<FileSystemInfo>();


                    if (dir.Parent != null)                                           //if parent directory is
                    {
                        dir = dir.Parent;                                             //
                        foreach (DirectoryInfo d in dir.EnumerateDirectories())       //add directory
                        {
                            collection.Add(d);
                        }

                        foreach (FileInfo d in dir.EnumerateFiles())                  //add files
                        {
                            collection.Add(d);
                        }

                        foreach (Window window in Application.Current.Windows)          //Search our button to Name (backBut)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                    (window as MainWindow).backBut.CommandParameter = dir;
                            }
                        }
                        foreach (Window window in Application.Current.Windows)          //Search our button to Name (backBut)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                (window as MainWindow).textPath.Text = dir.FullName;
                            }
                        }
                        Drive = collection;
                    }
                    else
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {

                                (window as MainWindow).backBut.IsEnabled = false;
                            }

                        }
                        foreach (Window window in Application.Current.Windows)          //Search our button to Name (backBut)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                (window as MainWindow).textPath.Text = "";
                            }
                        }
                        collection.Add(dir);
                        Drive = collection;
                    }



                });
            }
        }



        //private ObservableCollection<FileInfo> files;


        //public ObservableCollection<FileInfo> Files
        //{
        //    get
        //    {
        //        return files;
        //    }
        //    set
        //    {
        //        files = value;
        //        OnPropertyChanged("Files");
        //    }
        //}




        public ObservableCollection<FileSystemInfo> Drive
        {
            get
            {
                return drive;
            }
            set
            {
                drive = value;
                OnPropertyChanged("Drive");
            }
        }



        private ExplorerCommand collDirToCurDir;



        public ExplorerCommand CollDirToCurDir    //return collection directories in current directory
        {
            get
            {
                return collDirToCurDir ?? (collDirToCurDir = new ExplorerCommand(obj =>
                {
                    try
                    {
                        // MessageBox.Show("fff");
                        DirectoryInfo dir = obj as DirectoryInfo;
                        ObservableCollection<FileSystemInfo> collection = new ObservableCollection<FileSystemInfo>();
                        foreach (DirectoryInfo d in dir.EnumerateDirectories())
                        {
                            collection.Add(d);
                        }
                        Drive = collection;

                        //  ObservableCollection<FileInfo> collectionfiles = new ObservableCollection<FileInfo>();
                        foreach (FileInfo d in dir.EnumerateFiles())
                        {
                            Drive.Add(d);
                        }
                        // Files = collection;

                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                /// MessageBox.Show("fff");
                                (window as MainWindow).backBut.CommandParameter = dir;
                                (window as MainWindow).backBut.IsEnabled = true;
                                /// MessageBox.Show((window as MainWindow).backBut.CommandParameter.ToString());
                            }
                        }
                        foreach (Window window in Application.Current.Windows)          //Search our button to Name (backBut)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                (window as MainWindow).textPath.Text = dir.FullName;
                            }
                        }
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
                }));

            }
        }


        private ExplorerCommand c;



        public ExplorerCommand C   //return collection directories in current directory
        {
            get
            {
                return c ?? (c = new ExplorerCommand(obj =>
                {
                    foreach (DirectoryInfo d in Drive)
                    {
                        MessageBox.Show(d.Name);
                    }
                }));

            }
        }



        // private DirectoryInfo drive;



        public ExplorerViewModel()
        {

            SearchDriver search = new SearchDriver();

            ObservableCollection<FileSystemInfo> collection = new ObservableCollection<FileSystemInfo>();

            foreach (DirectoryInfo d in search.SearchParrentDirectory())
            {
                collection.Add(d);
            }
            Drive = collection;
        }



        public event PropertyChangedEventHandler PropertyChanged;




        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }



}

