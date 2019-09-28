using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Explorer;
using System.IO;
using System.Windows;
using WpfApp1;

namespace WpfApp1
{




    public class ExplorerViewModel : INotifyPropertyChanged
    {



        private ObservableCollection<DirectoryInfo> drive;


        private ExplorerCommand back;



        public ExplorerCommand Back
        {
            get
            {
                return back = new ExplorerCommand(obj =>
                {
                    //MessageBox.Show("fff1");
                    DirectoryInfo dir = obj as DirectoryInfo;
                    ObservableCollection<DirectoryInfo> collection = new ObservableCollection<DirectoryInfo>();


                    if (dir.Parent != null)
                    {
                        dir = dir.Parent;
                        foreach (DirectoryInfo d in dir.EnumerateDirectories())
                        {
                            collection.Add(d);
                        }
                        
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                if (dir.Parent != null)
                                {
                                    (window as MainWindow).backBut.CommandParameter = dir.Parent;
                                    //                           MessageBox.Show((window as MainWindow).backBut.CommandParameter.ToString());
                                }
                                else
                                    (window as MainWindow).backBut.CommandParameter = dir;
                            }
                            Drive = collection;
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
                        collection.Add(dir);
                        Drive = collection;
                    }
                    

                   
                });
            }
        }




        public ObservableCollection<DirectoryInfo> Drive
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
                   // MessageBox.Show("fff");
                    DirectoryInfo dir = obj as DirectoryInfo;
                    ObservableCollection<DirectoryInfo> collection = new ObservableCollection<DirectoryInfo>();
                    foreach (DirectoryInfo d in dir.EnumerateDirectories())
                    {
                        collection.Add(d);
                    }
                    Drive = collection;
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

            ObservableCollection<DirectoryInfo> collection = new ObservableCollection<DirectoryInfo>();

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

