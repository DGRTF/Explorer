using System;
using System.IO;
using Explorer;

namespace SMTP
{
    class Program
    {


        public static void Main()
        {
            SearchDriver search = new SearchDriver();
            foreach (DriveInfo n in search.SearchParrentDirectory())
            {
                System.Console.WriteLine(n.Name);
                DirectoryInfo a = new DirectoryInfo(n.Name);
                System.Console.WriteLine("--------------using System;---------------");
                foreach (DirectoryInfo nn in a.EnumerateDirectories())
                    System.Console.WriteLine(nn.Name);
                    System.Console.WriteLine("-----------1-----------");
                foreach (FileInfo nn in a.EnumerateFiles())
                    System.Console.WriteLine(nn.Name);
            }
        }
    }
}
