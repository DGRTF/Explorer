using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Security;
using System;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {



        public ExplorerViewModel exp { get; set; }



        public MainWindow()
        {
            InitializeComponent();
            exp = new ExplorerViewModel();
            DataContext = new MainWindowViewModel(this, exp);
        }



        public void EnterPath(object sender, KeyEventArgs a)                         //Enter Path
        {
            if (a.Key == Key.Enter)
            {
                TextBox t = sender as TextBox;
                DirectoryInfo dir = new DirectoryInfo(t.Text);
                exp.OpenDirecrory(dir);
            }
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
