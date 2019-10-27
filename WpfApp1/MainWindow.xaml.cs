using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Security;
using System;
using System.Collections.Generic;
using Explorer;
using WpfApp1.ViewModel;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {



        public ExplorerViewModel Explorer { get; set; }



        public MainWindow()
        {
            InitializeComponent();
            //Exp = new ExplorerViewModel(this);
            DataContext = new MainWindowViewModel(this, new ExplorerViewModel(this));
        }

        //Methods:

        public void EnterPath(object sender, KeyEventArgs a)                         //Enter Path
        {
            if (a.Key == Key.Enter)
            {
                TextBox t = sender as TextBox;
                DirectoryInfo dir = new DirectoryInfo(t.Text);
                Explorer.OpenDirecrory(dir);
            }
        }



        public void EnterSearch(object sender, KeyEventArgs a)  //Enter search
        {
            if (a.Key == Key.Enter)
            {
                TextBox t = sender as TextBox;
                Explorer.Drive.Clear();
                SearchDirectory search=new SearchDirectory (Explorer, t.Text);
                Thread myThread = new Thread(new ThreadStart(search.SearchDirectoryE));
                myThread.Start();
                Explorer.EnabledBut = false;
            }
        }



        public void Expand (object sender, RoutedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        /// <summary>
        /// Method for action with window
        /// </summary>


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
