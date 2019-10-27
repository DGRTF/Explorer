using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfApp1
{



    public partial class MainWindowViewModel : INotifyPropertyChanged    //Action with window
    {


        public ExplorerViewModel Explorer { get; set; }            //Variable to action with explorer




        public MainWindowViewModel(MainWindow window, ExplorerViewModel exp)       //Add object main window
        {
            MainWindow = window;
            Explorer = exp;   //
            MainWindow.Explorer = exp;
        }


        private MainWindowCommand addCommand;     //Variable for commands




        private MainWindow MainWindow { get; }       //Object main window




        public MainWindowCommand AddCommandMax     //Max-min window
        {
            get
            {
                return addCommand = new MainWindowCommand(obj =>
                {
                    if (MainWindow.WindowState == WindowState.Normal)
                        MainWindow.WindowState = WindowState.Maximized;
                    else
                        MainWindow.WindowState = WindowState.Normal;
                });
            }
        }



        public MainWindowCommand AddCommandMin       //Displace window
        {
            get
            {
                return addCommand = new MainWindowCommand(obj =>
                {
                    MainWindow.WindowState = WindowState.Minimized;
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
                      MainWindow.Close();
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
