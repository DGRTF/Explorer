using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Explorer;
using System.IO;


namespace WpfApp1
{




    public class ExplorerViewModel : INotifyPropertyChanged
    {



        private ExplorerCommand collDirToCurDir;



        public ExplorerCommand CollDirToCurDir    //return collection directories in current directory
        {
            get
            {
                return collDirToCurDir = new ExplorerCommand(obj =>
                {
                    DirectoryInfo dir = obj as DirectoryInfo;
                    ObservableCollection<DirectoryInfo> collection = new ObservableCollection<DirectoryInfo>();
                    foreach (DirectoryInfo d in dir.EnumerateDirectories())
                    {
                        // Drive = null;

                        collection.Add(d);

                    }
                    Drive = collection;
                });

            }
        }


        public ObservableCollection<DirectoryInfo> Drive { get; set; }



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

