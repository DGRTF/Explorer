using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Explorer;
using System.IO;
using System.Windows;

namespace WpfApp1
{




    public class ExplorerViewModel : INotifyPropertyChanged
    {



        private ObservableCollection<DirectoryInfo> drive;



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
                    DirectoryInfo dir = obj as DirectoryInfo;
                    ObservableCollection<DirectoryInfo> collection = new ObservableCollection<DirectoryInfo>();
                    foreach (DirectoryInfo d in dir.EnumerateDirectories())
                    {
                        collection.Add(d);
                    }
                    Drive = collection;
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

