using System;
using System.IO;

namespace FileManager_Budgo
{
    class Program
    {
        static void help()
        {
            Console.WriteLine(@"ls -Выводит список файлов в каталоге");
            Console.WriteLine(@"   ls C:\Source -p 2");
            Console.WriteLine(@"cd-Переход в каталог \n  cd C:\Source ");
            Console.WriteLine(@"   cd C:\Source ");
            Console.WriteLine("rm-Удаление файла или каталога");
            Console.WriteLine(@"   rm C:\Source");
            Console.WriteLine(@"   rm C:\source.txt");
            Console.WriteLine(@"cp-Копирование каталога или файла");
            Console.WriteLine(@"   cp C:\Source D:\Target");
            Console.WriteLine(@"   cp C:\source.txt D:\target.txt");
            Console.WriteLine(@"cr-Создание файла");
            Console.WriteLine(@"   cr C:\source.txt");
            Console.WriteLine(@"file -Вывод информации");
            Console.WriteLine(@"   file C:\source.txt");
            Console.WriteLine("exit-Выход");
        }
        
        static void PrintDir(DirectoryInfo dir, string indent, bool lastDirectory, string fail)
        {
            string text;
            Console.Write(indent);
            File.AppendAllText(fail, indent);
            if (lastDirectory)
            {
                Console.Write("└─");
                text = "└─";
                File.AppendAllText(fail, text);
                indent += "  ";
                                
            }
            else
            {
                Console.Write("├─");
                text = "├─";
                File.AppendAllText(fail, text);
                indent += "│ ";                              
            }

            Console.WriteLine(dir.Name);
            File.AppendAllText(fail, dir.Name);
            File.AppendAllText(fail, Environment.NewLine);

            DirectoryInfo[] subDirs = dir.GetDirectories();

            for (int i = 0; i < subDirs.Length; i++)
            {
                PrintDir(subDirs[i], indent, i == subDirs.Length - 1, fail);
            }
        }
        
        
        static void Main(string[] args)
        {
            string memu;
            bool end = false;
            Console.SetBufferSize(120, 100)
            do
            {
                memu = Console.ReadLine();
                switch (@memu)
                {
                    case "help":
                        help();
                        break;
                    case "ls":
                        string put = @"E:\Battle.net";
                        string filename1 = "strukturDir.txt";
                        DateTime localDate = DateTime.Now;
                        File.WriteAllText(filename1, Convert.ToString(localDate));
                        File.AppendAllText(filename1, Environment.NewLine);
                        PrintDir(new DirectoryInfo(put), "", true, filename1);
                        break;
                    case "cd":
                        help();
                        break;
                    case "rm":
                        help();
                        break;
                    case "cp":
                        help();
                        break;
                    case "cr":
                        help();
                        break;
                    case "file":
                        help();
                        break;
                    case "exit":
                        end = true;
                        break;
                    default:
                        Console.WriteLine("Введена не правильная команда");
                        break;
                }
            } while (end == false);
        }
    }
}
