using System;

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
                        help();
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
                        help();
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
