using System;
using System.IO;
using Equals;
using FastSortCompare;
using Explorer;

namespace SMTP
{
    class Program
    {


        public static void Main()
        {
            SearchDriver search = new SearchDriver();
            foreach (DriveInfo n in search.SearchSelect())
                System.Console.WriteLine(n.Name);
        }
    }
}
