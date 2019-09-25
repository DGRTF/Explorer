using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using Explorer;

namespace WpfApp1
{



    public partial class MainWindowViewModel : INotifyPropertyChanged    //Action with window
    {


        //private MainWindowCommand collDirToCurDir
        //{
        //    get; set;
        //}


        //private ObservableCollection<DirectoryInfo> drive;



        //public ObservableCollection<DirectoryInfo> Drive { get { return drive; } set { drive = value; OnPropertyChanged("Drive"); } }



        //public MainWindowCommand CollDirToCurDir    //return collection directories in current directory
        //{
        //    get
        //    {
        //        return collDirToCurDir = new MainWindowCommand(obj =>
        //        {
        //            DirectoryInfo dir = obj as DirectoryInfo;
        //            ObservableCollection<DirectoryInfo> collection = new ObservableCollection<DirectoryInfo>();
        //            foreach (DirectoryInfo d in dir.EnumerateDirectories())
        //            {
        //                collection.Add(d);
        //            }
        //            Drive = collection;
        //        });

        //    }
        //}



        private ExplorerViewModel explorer;




        public ExplorerViewModel Explorer
        {
            get
            {
                return explorer;
            }
            set
            {
                explorer = value;
                OnPropertyChanged("Explorer");
            }
        }            //Variable to action with explorer




        public MainWindowViewModel(MainWindow window, ExplorerViewModel exp)       //Add object main window
        {
            mainWindow = window;
            Explorer = exp;       //
            //SearchDriver search = new SearchDriver();

            //ObservableCollection<DirectoryInfo> collection = new ObservableCollection<DirectoryInfo>();

            //foreach (DirectoryInfo d in search.SearchParrentDirectory())
            //{
            //    collection.Add(d);
            //}
            //Drive = collection;
        }


        private MainWindowCommand addCommand;     //Variable for commands




        private MainWindow mainWindow { get; }       //Object main window




        public MainWindowCommand AddCommandMax     //Max-min window
        {
            get
            {
                return addCommand = new MainWindowCommand(obj =>
                  {
                      if (mainWindow.WindowState == WindowState.Normal)
                          mainWindow.WindowState = WindowState.Maximized;
                      else
                          mainWindow.WindowState = WindowState.Normal;
                  });
            }
        }



        public MainWindowCommand AddCommandMin       //Displace window
        {
            get
            {
                return addCommand = new MainWindowCommand(obj =>
                {
                    mainWindow.WindowState = WindowState.Minimized;
                });
            }
        }




        public MainWindowCommand AddCommandClose    //Close window
        {
            get
            {
                return addCommand ??
                  (addCommand = new MainWindowCommand(obj =>
                  {
                      mainWindow.Close();
                  }));
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;




        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }



    }
}
