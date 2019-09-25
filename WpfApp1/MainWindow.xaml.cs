using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this, new ExplorerViewModel());
        }




        private bool isWiden;




       private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isWiden = true;
        } 




        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isWiden = false;
            Rectangle rect = (Rectangle)sender;
            rect.ReleaseMouseCapture();
        }




        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            if (isWiden)
            {
                rect.CaptureMouse();
                double newWidth = e.GetPosition(this).X + 5;

                if (newWidth > 0)
                    this.Width = newWidth;
            }
        }



        private void Rectangle_MouseMove1(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            if (isWiden)
            {
                rect.CaptureMouse();
                double newWidth = e.GetPosition(this).Y + 5;

                if (newWidth > 0)
                    this.Height = newWidth;
            
            }
        }



        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }



    }
}
