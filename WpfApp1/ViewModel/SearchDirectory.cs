using Explorer;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp1.ViewModel
{
    class SearchDirectory
    {



        public SearchDirectory(ExplorerViewModel explorer, string s)
        {
            Explorer = explorer;
            Directory = explorer.Parameter;
            Str = s;
        }



       public ExplorerViewModel Explorer { get; set; }



        public DirectoryInfo Directory { get; set; }



        public string Str { get; set; }



        public void SearchDirectoryE()//search for name
        {

            if (Directory != null)
            {

                try
                {
                    foreach (DirectoryInfo d in Directory.GetDirectories(Str))
                    {
                        Explorer.Window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            Explorer.Drive.Add(d);
                        });
                    }

                    foreach (FileInfo d in Directory.GetFiles(Str))
                    {
                        Explorer.Window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            Explorer.Drive.Add(d);
                        });
                    }
                    IEnumerable<DirectoryInfo> dir = Directory.EnumerateDirectories();
                    foreach (DirectoryInfo d in dir)
                    {
                        Directory = d;
                        SearchDirectoryE();
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

                    Directory = dd;
                    try
                    {
                        foreach (DirectoryInfo d in Directory.GetDirectories(Str))
                        {
                            Explorer.Window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                            {
                                Explorer.Drive.Add(d);
                            });
                        }

                        foreach (FileInfo d in Directory.GetFiles(Str))
                        {
                            Explorer.Window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                            {
                                Explorer.Drive.Add(d);
                            });
                        }

                        IEnumerable<DirectoryInfo> dir = Directory.EnumerateDirectories();
                        foreach (DirectoryInfo d in dir)
                        {
                            Directory = d;
                            SearchDirectoryE();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("sdggggggggggg");

                    }
                }
            }


        }

    }
}
