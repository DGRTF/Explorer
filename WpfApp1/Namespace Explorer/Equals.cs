using System;
using System.IO;



namespace Equals
{


        public interface IEquals<T>
        {
            bool Equate(T a, T o);
        }

        public class EquateI : IEquals<DirectoryInfo>
        {
            public bool Equate(DirectoryInfo a, DirectoryInfo o)
            {
                if (a.Name == o.Name)
                    return true;
                else
                    return false;
            }
        }

        public class ParentDirectory<T>
        {
            public T[] ParDire(T[] dire, IEquals<T> a)
            {
                if (dire.Length != 1)
                {
                    T[] directory = new T[0];
                    T[] dir = new T[0];
                    for (int i = 1; i < dire.Length; i++)
                    {
                        if (a.Equate(dire[0], dire[i]) == false)
                        {
                            Array.Resize(ref directory, directory.Length + 1);
                            directory[directory.Length - 1] = dire[i];
                        }
                    }
                    Array.Resize(ref dir, dir.Length + 1);
                    dir[dir.Length - 1] = dire[0];
                    if (directory.Length == 0)
                        return dir;
                    else
                    {
                        T[] d = ParDire(directory, a);
                        var z = new T[dir.Length + d.Length];
                        d.CopyTo(z, 0);
                        dir.CopyTo(z, d.Length);
                        return z;
                    }
                }
                else
                    return dire;
            }

        }





        // public static void Main()
        // {
        //     DirectoryInfo[] dire = new DirectoryInfo[0];

        //     foreach (DriveInfo n in DriveInfo.GetDrives())
        //     {
        //         if (n.TotalSize != 0)
        //         {

        //             Array.Resize(ref dire, dire.Length + 1);
        //             dire[dire.Length - 1] = new DirectoryInfo(n.Name);
        //         }
        //     }

        //     for (int i = 0; i < dire.Length; i++)
        //     {
        //         while (dire[i].Parent != null)
        //         {
        //             dire[i] = dire[i].Parent;

        //         }
        //     }

        //     ParentDirectory<DirectoryInfo> par = new ParentDirectory<DirectoryInfo>();
        //     //  par.ParDire(dire);
        //     foreach (DirectoryInfo dir in par.ParDire(dire, new EquateI()))
        //     {
        //         Console.WriteLine("Parent directory is " + dir.FullName);
        //         //Console.Read();
        //     }
        // }
 //   }
}

