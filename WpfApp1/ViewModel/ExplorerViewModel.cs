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



        private ObservableCollection<FileSystemInfo> drive;



        //public void EnterPath(object sender, KeyEventArgs a)                         //Enter Path
        //{
        //    if (a.Key == Key.Enter)
        //    {
        //        TextBox t = sender as TextBox;
        //        DirectoryInfo dir = new DirectoryInfo(t.Text);
        //        try
        //        {
        //            // MessageBox.Show("fff");
        //            ObservableCollection<FileSystemInfo> collection = new ObservableCollection<FileSystemInfo>();
        //            foreach (DirectoryInfo d in dir.EnumerateDirectories())
        //            {
        //                collection.Add(d);
        //            }
        //            Drive = collection;


        //            foreach (FileInfo d in dir.EnumerateFiles())
        //            {
        //                Drive.Add(d);
        //            }
        //            // Files = collection;

        //            foreach (Window window in Application.Current.Windows)
        //            {
        //                if (window.GetType() == typeof(MainWindow))
        //                {
        //                    /// MessageBox.Show("fff");
        //                    (window as MainWindow).backBut.CommandParameter = dir;
        //                    (window as MainWindow).backBut.IsEnabled = true;
        //                    /// MessageBox.Show((window as MainWindow).backBut.CommandParameter.ToString());
        //                }
        //            }
        //            foreach (Window window in Application.Current.Windows)          //Search our button to Name (backBut)
        //            {
        //                if (window.GetType() == typeof(MainWindow))
        //                {
        //                    (window as MainWindow).textPath.Text = dir.FullName;
        //                }
        //            }
        //        }
        //        catch (DirectoryNotFoundException e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //        catch (SecurityException e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //        catch (UnauthorizedAccessException e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Unknown error");
        //        }
        //    }
        //}





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



        public ObservableCollection<FileSystemInfo> Drive { get; } = new ObservableCollection<FileSystemInfo>();



        private ExplorerCommand collDirToCurDir;



        public ExplorerCommand CollDirToCurDir    //return collection directories in current directory
        {
            get
            {
                return collDirToCurDir ?? (collDirToCurDir = new ExplorerCommand(obj =>
                {
                    OpenDirecrory(obj);

                }));

            }
        }


        public void Initialize()
        {
            SearchDriver search = new SearchDriver();

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
                IEnumerable<DirectoryInfo> dire = dir.EnumerateDirectories();
                Drive.Clear();
                foreach (DirectoryInfo d in dire)
                {

                    Drive.Add(d);
                }


                foreach (FileInfo d in dir.EnumerateFiles())
                {
                    Drive.Add(d);
                }

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).backBut.CommandParameter = dir;
                        (window as MainWindow).backBut.IsEnabled = true;
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
                Drive.Clear();
                Drive.Add(dir);

            }
        }



        public ExplorerViewModel()
        {
            Initialize();
        }



        public event PropertyChangedEventHandler PropertyChanged;




        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }



}

