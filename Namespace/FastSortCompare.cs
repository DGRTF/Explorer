using System;
using System.IO;

namespace FastSortCompare
{
    public interface IComparison<T>
    {
        int Compare(T o, T a);
    }

   public class Comparison : IComparison<DriveInfo>
    {
        public int Compare(DriveInfo o, DriveInfo a)
        {
            if (o.TotalSize == a.TotalSize)
                return 2;
            else if (o.TotalSize < a.TotalSize)
                return 1;
            else
                return 3;
        }
    } 

        public class Sort<T>
        {
            public T[] FastSort(T[] a, IComparison<T> comp)
            {
                if (a.Length == 0)
                    return a;
                else
                {
                    if (a.Length <= 2)
                    {
                        if (a.Length == 1)
                            return a;
                        else
                        {
                            int compi = comp.Compare(a[0], a[1]);
                            T[] b = new T[a.Length];
                            if (compi == 2)
                                return a;
                            else if (compi == 3)
                            {
                                b[0] = a[0];
                                b[1] = a[1];
                                return b;
                            }
                            else
                            {
                                b[0] = a[1];
                                b[1] = a[0];
                                return b;
                            }
                        }
                    }


                    else
                    {
                        T[] b = new T[0];
                        T[] c = new T[0];
                        T[] d = new T[0];
                        System.Console.WriteLine("gena");
                        for (int i = 1; i < a.Length; i++)
                        {
                            int compi = comp.Compare(a[0], a[i]);

                            if (compi == 1)
                            {
                                Array.Resize(ref c, c.Length + 1);
                                c[c.Length - 1] = a[i];
                            }
                            else if (compi == 2)
                            {
                                Array.Resize(ref d, d.Length + 1);
                                d[d.Length - 1] = a[i];
                            }
                            else
                            {
                                Array.Resize(ref b, b.Length + 1);
                                b[b.Length - 1] = a[i];
                            }
                        }
                        Array.Resize(ref d, d.Length + 1);
                        d[d.Length - 1] = a[0];
                        b = FastSort(b, comp);
                        c = FastSort(c, comp);
                        if (b == null & d == null)
                            return c;
                        else if (c == null & d == null)
                            return b;
                        else if (c == null & b == null)
                            return d;
                        else
                        {
                            var z = new T[d.Length + c.Length];
                            c.CopyTo(z, 0);
                            d.CopyTo(z, c.Length);
                            var g = new T[z.Length + b.Length];
                            z.CopyTo(g, 0);
                            b.CopyTo(g, z.Length);
                            return g;
                        }
                    }
                }
            }
        }
        // static void Main(string[] args)
        // {
        //     Sort<DriveInfo> s = new Sort<DriveInfo>();
        //     foreach (DriveInfo n in DriveInfo.GetDrives())
        //     {
        //         System.Console.WriteLine(n.Name);
        //     }
        //     System.Console.WriteLine("---------");
        //     DriveInfo[] nn = s.FastSort(DriveInfo.GetDrives(), new Comparison());
        //     int nnn = 0;
        //     foreach (DriveInfo n in nn)
        //     {
        //         System.Console.WriteLine(n.TotalSize);
        //         nnn++;
        //     }
        //     System.Console.WriteLine("ll: " + nnn);
        //     foreach (DriveInfo n in nn)
        //     {
        //         System.Console.WriteLine(n.Name);
        //         nnn++;
        //     }

            // List<DriveInfo> Directory = new List<DriveInfo>();
            // Directory.AddRange(DriveInfo.GetDrives());
            // foreach (var name in Directory)
            //     Console.WriteLine("   {0}", name);
            // Console.WriteLine("sadg");
            /*foreach (DriveInfo n in DriveInfo.GetDrives())
            {
            DriveType d =n.DriveType;
            int j=(int)d;
            if (j==3)
            {
            if(n.TotalSize!=0)
            {
            Console.WriteLine(n.Name);
            Console.WriteLine(n.VolumeLabel);
            Console.WriteLine(n.DriveType);
            Console.WriteLine("{0} byte",n.TotalSize);
            float l=(float)n.TotalSize/(1024*1024);
            Console.WriteLine("{0} Mb",l);
            l=l/1024;
            Console.WriteLine("{0} Gb",l);
            Console.WriteLine("--------");

            Console.Read();
            }
            }
            }*/
            /*         DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
                   Console.WriteLine(dir.FullName);
                   while (dir.Parent != null)
                   {
                       dir = dir.Parent;
                       Console.WriteLine(dir.FullName);
                       //Console.Read();
                   }
                   Console.WriteLine("Parent directory is " + dir.FullName);*/
        //     Console.Read();
        // }
    
}

