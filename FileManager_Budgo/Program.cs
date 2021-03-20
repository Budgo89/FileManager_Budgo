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
            Console.WriteLine(@"cd-Переход в каталог");
            Console.WriteLine(@"rm-Удаление файла или каталога");
            Console.WriteLine(@"cp-Копирование каталога или файла");
            Console.WriteLine(@"crfile-Создание файла");
            Console.WriteLine(@"crcat-Создание файла");
            Console.WriteLine(@"file-Вывод содержания текущего каталога");
            Console.WriteLine(@"info-информация о файле или каталоге");

            Console.WriteLine(@"exit-Выход");
        }

        static void CatalogDirs(DirectoryInfo[] subDirs)
        {
            if (subDirs.Length >= 10)
            {
                int subDirscoll = 0;
                int i1 = subDirscoll;
                bool flag = true;
                do
                {
                    if (subDirscoll + 10 >= subDirs.Length)
                    {
                        for (int i = subDirscoll; i < subDirs.Length; i++)
                        {
                            Console.Write("├─");
                            Console.WriteLine(subDirs[i].Name);
                            subDirscoll++;
                        }

                        flag = false;
                        
                    }
                    else i1 = subDirscoll;
                    for (int i = subDirscoll; i < i1 + 10; i++)
                    {
                        Console.Write("├─");
                        Console.WriteLine(subDirs[i].Name);
                        subDirscoll++;
                    }

                    if (subDirscoll < subDirs.Length)
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
                for (int i = 0; i < subDirs.Length; i++)
                {
                    Console.Write("├─");
                    Console.WriteLine(subDirs[i].Name);
                }
            }
        }

        static void Catalogfile(FileInfo[] subfile)
        {
            if (subfile.Length >= 10)
            {
                int subfilecoll = 0;
                int i1 = subfilecoll;
                bool flag = true;
                do
                {
                    if (subfilecoll + 10 >= subfile.Length)
                    {
                        for (int i = subfilecoll; i < subfile.Length; i++)
                        {
                            Console.Write("─");
                            Console.WriteLine(subfile[i].Name);
                            subfilecoll++;
                        }

                        flag = false;
                    }
                    else i1 = subfilecoll;
                    for (int i = subfilecoll; i < i1 + 10; i++)
                    {
                        Console.Write("─");
                        Console.WriteLine(subfile[i].Name);
                        subfilecoll++;
                    }

                    if (subfilecoll < subfile.Length - 10)
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
                for (int i = 0; i < subfile.Length; i++)
                {
                    Console.Write("─");
                    Console.WriteLine(subfile[i].Name);
                }
            }
        }

        static void Catalog(DirectoryInfo dir)
        {
            try
            {
                Console.WriteLine(dir.Name);
                DirectoryInfo[] subDirs = dir.GetDirectories();
                FileInfo[] subfile = dir.GetFiles();
                CatalogDirs(subDirs);
                Catalogfile(subfile);
            }
            catch (DirectoryNotFoundException dirEx)
            {
                Console.WriteLine("Каталог не найден: " + dirEx.Message);
            }
            catch (IOException)
            {
                Console.WriteLine("Каталог не найден: ");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirName">Текущая директория</param>
        /// <param name="Cat">Каталог</param>
        static string cdCatalog(string dirName, string cat)
        {
            string nevDir = dirName + @"\" + cat;
            DirectoryInfo newDir = new DirectoryInfo(nevDir);
            Catalog(newDir);
            return nevDir;
        }

        static void cp (string dirName, string cat)
        {
            try
            {
                Console.WriteLine("Введите новое название файла: ");
                string newcat = Console.ReadLine();
                CopyDirectory(Path.Combine(dirName, cat), Path.Combine(dirName, newcat));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Такого файла нет!");
            }
            catch (IOException)
            {
                Console.WriteLine("Файл с таким именем уже существует или не найден!");
            }

        }

        static void CopyDirectory(string lastDir, string end_dir)
        {
            DirectoryInfo dir_inf = new DirectoryInfo(lastDir);
            foreach (DirectoryInfo dir in dir_inf.GetDirectories())
            {
                if (Directory.Exists(end_dir + "\\" + dir.Name) != true)
                {
                    Directory.CreateDirectory(end_dir + "\\" + dir.Name);
                }
                CopyDirectory(dir.FullName, end_dir + "\\" + dir.Name);
                foreach (string file in Directory.GetFiles(lastDir))
                {
                    string filik = file.Substring(file.LastIndexOf('\\'), file.Length - file.LastIndexOf('\\'));
                    File.Copy(file, end_dir + "\\" + filik, true);
                }
            }
        }

        static void rm(string Dir, string file)
        {
            try
            {
                if (File.Exists(Path.Combine(Dir, file)))
                {
                    File.Delete(Path.Combine(Dir, file));

                }
                else Directory.Delete(Path.Combine(Dir, file), true);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Такого файла нет!");
            }
            catch (IOException)
            {
                Console.WriteLine("Файл используестя");
            }
        }

        static void crfile(string Dir, string file)
        {
            File.Create(Path.Combine(Dir, file));
        }

        static void crcat(string Dir, string file)
        {
            Directory.CreateDirectory(Path.Combine(Dir, file));
        }

        static void info(string Dir, string file)
        {
            
            FileInfo newFile = new FileInfo(Path.Combine(Dir, file));
            DirectoryInfo newDir = new DirectoryInfo(Path.Combine(Dir, file));
            try
            {
                if (File.Exists(Path.Combine(Dir, file)))
                {
                    Console.WriteLine("Имя " + newFile.Name);
                    Console.WriteLine("Расширение " + newFile.Extension);
                    Console.WriteLine("Размер в байтах " + newFile.Length);
                }
                if (Directory.Exists(Path.Combine(Dir, file)))
                {
                    Console.WriteLine("Имя " + newDir.Name);
                    Console.WriteLine("Количество вложеных каталогов " + newDir.GetDirectories().Length);
                    Console.WriteLine("Количество вложеных файлов " + newDir.GetFiles().Length);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Такого файла нет!");
            }
        }

        static void Main(string[] args)
        {
            string memu;
            bool end = false;
            string config = "Config.txt";
            string configDir = @"C:\";
            help();
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
                        File.WriteAllText(config, folderPath);
                        Catalog(dir);
                        break;
                    case "cd":
                        Console.WriteLine("Введите название каталога: ");                        
                        string newCat = Console.ReadLine();
                        configDir = File.ReadAllText(config);
                        File.WriteAllText(config, cdCatalog(configDir, newCat));
                        break;
                    case "rm":
                        Console.WriteLine("Введите название файла или каталога для удаления: ");
                        string rmCat = Console.ReadLine();
                        configDir = File.ReadAllText(config);
                        rm(configDir, rmCat);
                        break;
                    case "cp":
                        Console.WriteLine("Введите название файла или каталога для копирования: ");
                        string cpCat = Console.ReadLine();
                        configDir = File.ReadAllText(config);
                        cp(configDir, cpCat);
                        break;
                    case "crfile":
                        Console.WriteLine("Введите название файла для создания ");
                        string crFile = Console.ReadLine();
                        configDir = File.ReadAllText(config);
                        crfile(configDir, crFile);
                        break;
                    case "crcat":
                        Console.WriteLine("Введите название каталога для создания ");
                        string crCat = Console.ReadLine();
                        configDir = File.ReadAllText(config);
                        crcat(configDir, crCat);
                        break;
                    case "file":
                        configDir = File.ReadAllText(config);
                        DirectoryInfo dirfile = new DirectoryInfo(configDir);
                        Catalog(dirfile);
                        break;
                    case "info":
                        configDir = File.ReadAllText(config);
                        string infoCat = Console.ReadLine();
                        configDir = File.ReadAllText(config);
                        info(configDir, infoCat);

                        break;
                    case "exit":
                        end = true;
                        break;
                    default:
                        Console.WriteLine("Введена не правильная команда");
                        Console.WriteLine("Введите help для отоброжения команд");
                        break;
                }
            } while (end == false);
        }
    }
}
