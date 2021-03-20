using System;
using System.Collections.Generic;
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

        static void Catalog(DirectoryInfo dir, string indent, bool lastDirectory)
        {
           
            Console.Write(indent);            
            if (lastDirectory)
            {
                Console.Write("└─");     
                indent += "  ";

            }
            else
            {
                Console.Write("├─");
                indent += "│ ";
            }

            Console.WriteLine(dir.Name);

            DirectoryInfo[] subDirs = dir.GetDirectories();
            
            FileInfo[] subfill = dir.GetFiles();           

            if (subDirs.Length >= 10)
            {
                int subDirscoll = 0;
                int i1 = subDirscoll;
                bool flag = true;
                do
                {
                    if(subDirscoll+10 >= subDirs.Length )
                    {
                        i1 = subDirs.Length-11;
                    }
                    else i1 = subDirscoll;                    
                    for (int i = subDirscoll; i < i1 + 10; i++)
                    {
                        Console.WriteLine(subDirs[i]);                        
                        subDirscoll++;                        
                    }
                    
                    if (subDirscoll < subDirs.Length - 10)
                    {
                        Console.WriteLine("...");
                        Console.WriteLine("Продолжить вывод? дa / нет");
                        string cat = Console.ReadLine();
                        switch (cat)
                        {
                            case "да":
                                flag = true;
                                break;
                            case "нет":
                                flag = false;
                                break;
                            default:
                                Console.WriteLine("Введена не правильная команда. Вывод продолжится автомотически.");
                                flag = true;
                                break;
                        }                       
                    }
                    else flag = false;
                }
                while (flag);
            }
            else 
            {
                for (int i = 0; i < subDirs.Length; i++)
                {
                    Console.WriteLine(subDirs[i]);
                }
            }
            if (subfill.Length >= 10)
            {
                int subfillcoll = 0;
                int i1 = subfillcoll;
                bool flag = true;
                do
                {                    
                    if (subfillcoll + 10 >= subfill.Length)
                    {
                        i1 = subDirs.Length - 11;
                    }
                    else i1 = subfillcoll;
                    for (int i = subfillcoll; i < i1 + 10; i++)
                    {
                        Console.WriteLine(subfill[i]);                        
                        subfillcoll++;
                    }
                    
                    if (subfillcoll <= subfill.Length - 10)
                    {
                        Console.WriteLine("...");
                        Console.WriteLine("Продолжить вывод? дa / нет");
                        string cat = Console.ReadLine();
                        switch (cat)
                        {
                            case "да":
                                flag = true;
                                break;
                            case "нет":
                                flag = false;
                                break;
                            default:
                                Console.WriteLine("Введена не правильная команда. Вывод продолжится автомотически.");
                                flag = true;
                                break;
                        }
                    }
                }
                while (flag);
            }
            else
            {
                for (int i = 0; i < subfill.Length; i++)
                {
                    Console.WriteLine(subfill[i]);
                }
            }
        }



        static void Main(string[] args)
        {
            string memu;
            bool end = false;            
            do
            {
                memu = Console.ReadLine();
                switch (@memu)
                {
                    case "help":
                        help();
                        break;
                    case "ls":
                        Console.WriteLine("Введите путь к каталогу: ");
                        string folderPath = Console.ReadLine();
                        DirectoryInfo dir = new DirectoryInfo(folderPath);
                        Catalog(dir, "", true);

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
