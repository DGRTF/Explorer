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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {



        public ExplorerViewModel Exp { get; set; }



        public MainWindow()
        {
            InitializeComponent();
            Exp = new ExplorerViewModel();
            DataContext = new MainWindowViewModel(this, Exp);
        }



        public void EnterPath(object sender, KeyEventArgs a)                         //Enter Path
        {
            if (a.Key == Key.Enter)
            {
                TextBox t = sender as TextBox;
                DirectoryInfo dir = new DirectoryInfo(t.Text);
                Exp.OpenDirecrory(dir);
            }
        }

        private void SearchDirectory(DirectoryInfo di, string s)
        {

            if (di != null)
            {
               
                try
                {
                    foreach (DirectoryInfo d in di.GetDirectories(s))
                    {
                        Exp.Drive.Add(d);
                    }

                    foreach (FileInfo d in di.GetFiles(s))
                    {
                        Exp.Drive.Add(d);
                    }
                    IEnumerable<DirectoryInfo> dir = di.EnumerateDirectories();
                    foreach (DirectoryInfo d in dir)
                    {
                        SearchDirectory(d, s);
                    }

                }
                catch
                {

                }
            }

            else
            {
                SearchDriver search = new SearchDriver();
                foreach (DirectoryInfo dd in search.SearchParrentDirectory())
                {
                    try
                    {
                        foreach (DirectoryInfo d in dd.GetDirectories(s))
                        {
                            Exp.Drive.Add(d);
                        }

                        foreach (FileInfo d in dd.GetFiles(s))
                        {
                            Exp.Drive.Add(d);
                        }

                        IEnumerable<DirectoryInfo> dir = dd.EnumerateDirectories();
                        foreach (DirectoryInfo d in dir)
                        {
                            SearchDirectory(d, s);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("sdggggggggggg");

                    }
                }
            }


        }


        public void EnterSearch(object sender, KeyEventArgs a)                         //Enter search
        {
            if (a.Key == Key.Enter)
            {
                TextBox t = sender as TextBox;
                Exp.Drive.Clear();
                SearchDirectory(Exp.Parameter, t.Text);
                Exp.EnabledBut = false;
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
