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

        static void CatalogDirs(DirectoryInfo[] subDirs, int q)
        {
            if (subDirs.Length >= q)
            {
                int subDirscoll = 0;
                int i1 = subDirscoll;
                bool flag = true;
                do
                {
                    if (subDirscoll + q >= subDirs.Length)
                    {
                        for (int i = subDirscoll; i < subDirs.Length; i++)
                        {
                            Console.Write("├─ ");
                            Console.WriteLine(subDirs[i].Name);
                            subDirscoll++;
                        }

                        flag = false;
                        
                    }
                    else i1 = subDirscoll;
                    for (int i = subDirscoll; i < i1 + q; i++)
                    {
                        Console.Write("├─ ");
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
                                Console.Clear();
                                break;
                            case "нет":
                                flag = false;
                                break;
                            default:
                                Console.Clear();
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
                    Console.Write("├─ ");
                    Console.WriteLine(subDirs[i].Name);
                }
            }
        }

        static void Catalogfile(FileInfo[] subfile, int q)
        {
            if (subfile.Length >= q)
            {
                int subfilecoll = 0;
                int i1 = subfilecoll;
                bool flag = true;
                do
                {
                    if (subfilecoll + q >= subfile.Length)
                    {
                        for (int i = subfilecoll; i < subfile.Length; i++)
                        {
                            Console.Write("─ ");
                            Console.WriteLine(subfile[i].Name);
                            subfilecoll++;
                        }

                        flag = false;
                    }
                    else i1 = subfilecoll;
                    for (int i = subfilecoll; i < i1 + q; i++)
                    {
                        Console.Write("─ ");
                        Console.WriteLine(subfile[i].Name);
                        subfilecoll++;
                    }

                    if (subfilecoll < subfile.Length)
                    {
                        Console.WriteLine("...");
                        Console.WriteLine("Продолжить вывод? дa / нет");
                        string cat = Console.ReadLine();
                        switch (cat)
                        {
                            case "да":
                                flag = true;
                                Console.Clear();
                                break;
                            case "нет":
                                flag = false;
                                break;
                            default:
                                Console.Clear();
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
                    Console.Write("─ ");
                    Console.WriteLine(subfile[i].Name);
                }
            }
        }

        static void sl(DirectoryInfo dir, int q)
        {
            try
            {                
                Console.WriteLine(dir.Name);
                DirectoryInfo[] subDirs = dir.GetDirectories();
                FileInfo[] subfile = dir.GetFiles();
                CatalogDirs(subDirs, q);
                Catalogfile(subfile, q);
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
        static string cd(string dirName, string cat, int q)
        {
            string nevDir = dirName + @"\" + cat;
            DirectoryInfo newDir = new DirectoryInfo(nevDir);
            sl(newDir, q);
            return nevDir;
        }

        static void cp (string dirName, string cat)
        {
            try
            {
                Console.WriteLine("Введите новое название файла: ");
                string newcat = Console.ReadLine();
                CopyDirectory(Path.Combine(dirName, cat), Path.Combine(dirName, newcat));
                Console.WriteLine("Копирование произведено успешно");
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
                if (file == "")
                {
                    Console.WriteLine("Файл не был указан");
                }
                else
                {
                    if (File.Exists(Path.Combine(Dir, file)))
                    {
                        File.Delete(Path.Combine(Dir, file));

                    }
                    else Directory.Delete(Path.Combine(Dir, file), true);
                    Console.WriteLine("Удаление произведено успешно!");
                }
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
            Console.WriteLine("Файл создан");
        }

        static void crcat(string Dir, string file)
        {
            Directory.CreateDirectory(Path.Combine(Dir, file));
            Console.WriteLine("Каталог создан");
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

        static void configSave(string config, string folderPath)
        {
            string[] Config = File.ReadAllLines(config);
            Config[0] = folderPath;
            File.WriteAllLines(config, Config);
        }
        static string configDirecvori(string config)
        {
            string[] configDirmas = File.ReadAllLines(config);
            return configDirmas[0];
        }
        static int configpar(string config)
        {
            string[] configDirmas = File.ReadAllLines(config);
            return Convert.ToInt32(configDirmas[1]);
        }

        static void Main(string[] args)
        {
            string memu;
            bool end = false;
            string config = "Config.txt";
            string configDir = @"C:\";
            int q = 0;
            help();
            do
            {
                Console.WriteLine("Введите команду");
                memu = Console.ReadLine();
                Console.Clear();
                switch (memu)
                {
                    case "help":
                        help();
                        break;
                    case "ls":
                        Console.WriteLine("Введите путь к каталогу: ");
                        string folderPath = Console.ReadLine();
                        DirectoryInfo dir = new DirectoryInfo(folderPath);
                        configSave(config, folderPath);
                        q = configpar(config);
                        sl(dir,q);
                        break;
                    case "cd":
                        Console.WriteLine("Введите название каталога: ");                        
                        string newCat = Console.ReadLine();                      
                        configDir = configDirecvori(config);
                        q = configpar(config);
                        configSave(config, cd(configDir, newCat, q));
                        break;
                    case "rm":
                        Console.WriteLine("Введите название файла или каталога для удаления: ");
                        string rmCat = Console.ReadLine();
                        configDir = configDirecvori(config);
                        rm(configDir, rmCat);
                        break;
                    case "cp":
                        Console.WriteLine("Введите название файла или каталога для копирования: ");
                        string cpCat = Console.ReadLine();
                        configDir = configDirecvori(config);
                        cp(configDir, cpCat);
                        break;
                    case "crfile":
                        Console.WriteLine("Введите название файла для создания ");
                        string crFile = Console.ReadLine();
                        configDir = configDirecvori(config);
                        crfile(configDir, crFile);
                        break;
                    case "crcat":
                        Console.WriteLine("Введите название каталога для создания ");
                        string crCat = Console.ReadLine();
                        configDir = configDirecvori(config);
                        crcat(configDir, crCat);
                        break;
                    case "file":
                        configDir = configDirecvori(config);
                        q = configpar(config);
                        DirectoryInfo dirfile = new DirectoryInfo(configDir);
                        sl(dirfile, q);
                        break;
                    case "info":
                        Console.WriteLine("Введите название файла или каталога: ");
                        string infoCat = Console.ReadLine();
                        configDir = configDirecvori(config);
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
