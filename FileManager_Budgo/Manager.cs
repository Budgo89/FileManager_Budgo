using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager_Budgo
{
    public class Manager
    {
        public static void help()
        {
            Console.WriteLine(@"ls -Выводит список файлов в каталоге");
            Console.WriteLine(@"cd-Переход в каталог");
            Console.WriteLine(@"rm-Удаление файла или каталога");
            Console.WriteLine(@"cp-Копирование каталога или файла");
            Console.WriteLine(@"crfile-Создание файла");
            Console.WriteLine(@"crcat-Создание каталога");
            Console.WriteLine(@"file-Вывод содержания текущего каталога");
            Console.WriteLine(@"info-Информация о файле или каталоге");

            Console.WriteLine(@"exit-Выход");
        }
        /// <summary>
        /// Вывод дерева каталогов
        /// </summary>
        /// <param name="subDirs">Массив каталогов</param>
        /// <param name="q">Кол-во выводимых данных</param>
        public static void CatalogDirs(DirectoryInfo[] subDirs, int q)
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
        /// <summary>
        /// Вывод дерева файлов
        /// </summary>
        /// <param name="subfile">Массив файлов</param>
        /// <param name="q">Кол-во выводимых данных</param>
        public static void Catalogfile(FileInfo[] subfile, int q)
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
        /// <summary>
        /// Выводит список файлов в каталоге
        /// </summary>
        /// <param name="dir">Каталог</param>
        /// <param name="q">Кол-во выводимых данных</param>
        public static void ls(DirectoryInfo dir, int q)
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
                errors(dirEx);
            }
            catch (IOException dirEx)
            {
                Console.WriteLine("Каталог не найден: ");
                errors(dirEx);
            }
        }
        /// <summary>
        /// Переход в каталог
        /// </summary>
        /// <param name="dirName">Текущая директория</param>
        /// <param name="Cat">Каталог</param>
        /// <param name="q">Кол-во выводимых данных</param>
        public static string cd(string dirName, string cat, int q)
        {
            string nevDir = dirName + @"\" + cat;
            DirectoryInfo newDir = new DirectoryInfo(nevDir);
            ls(newDir, q);
            return nevDir;
        }
        /// <summary>
        /// Копирование каталога или файла
        /// </summary>
        /// <param name="dirName">Адрес директории</param>
        /// <param name="cat">Имя каталоги или файла</param>
        public static void cp(string dirName, string cat)
        {
            try
            {
                Console.WriteLine("Введите новое название файла: ");
                string newcat = Console.ReadLine();
                CopyDirectory(Path.Combine(dirName, cat), Path.Combine(dirName, newcat));
                Console.WriteLine("Копирование произведено успешно");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Такого файла нет!");
                errors(e);
            }
            catch (IOException e)
            {
                Console.WriteLine("Файл с таким именем уже существует или не найден!");
                errors(e);
            }

        }
        /// <summary>
        /// копитование директории
        /// </summary>
        /// <param name="lastDir">Адрес первой директории</param>
        /// <param name="end_dir">Адрес второй директории</param>
        public static void CopyDirectory(string lastDir, string end_dir)
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
        /// <summary>
        /// Удаление файла или каталога
        /// </summary>
        /// <param name="Dir">Адрес директории</param>
        /// <param name="file">Имя каталоги или файла</param>
        public static void rm(string Dir, string file)
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
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Такого файла нет!");
                errors(e);
            }
            catch (IOException e)
            {
                Console.WriteLine("Файл используестя");
                errors(e);
            }
        }
        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="Dir">Адрес директории</param>
        /// <param name="file">Имя файла</param>
        public static void crfile(string Dir, string file)
        {
            File.Create(Path.Combine(Dir, file));
            Console.WriteLine("Файл создан");
        }
        /// <summary>
        /// Создание каталога
        /// </summary>
        /// <param name="Dir">Адрес директории</param>
        /// <param name="file">Имя каталога</param>
        public static void crcat(string Dir, string file)
        {
            Directory.CreateDirectory(Path.Combine(Dir, file));
            Console.WriteLine("Каталог создан");
        }
        /// <summary>
        /// информация о файле или каталоге
        /// </summary>
        /// <param name="Dir">Адрес директории</param>
        /// <param name="file">Имя файла или каталога</param>
        public static void info(string Dir, string file)
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
                else Console.WriteLine("Такого файла нет!");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Такого файла нет!");
                errors(e);
            }
        }
        /// <summary>
        /// Сохранение конфиг файла
        /// </summary>
        /// <param name="config">Имя конфиг файла</param>
        /// <param name="folderPath">Пареметр для добовления</param>
        public static void configSave(string config, string folderPath)
        {
            string[] Config = File.ReadAllLines(config);
            Config[0] = folderPath;
            File.WriteAllLines(config, Config);
        }
        /// <summary>
        /// Вывод адреса из конфиг файла
        /// </summary>
        /// <param name="config">Имя конфиг файла</param>
        /// <returns></returns>
        public static string configDirecvori(string config)
        {
            string[] configDirmas = File.ReadAllLines(config);
            return configDirmas[0];
        }
        /// <summary>
        /// Вывод кол-ва выводимых данных
        /// </summary>
        /// <param name="config">Имя конфиг файла</param>
        /// <returns></returns>
        public static int configpar(string config)
        {
            string[] configDirmas = File.ReadAllLines(config);
            return Convert.ToInt32(configDirmas[1]);
        }

        public static void errors(Exception e)
        {
            string filename = @"..\..\errors\random_name_exception.txt";
            File.AppendAllText(filename, Convert.ToString( e));
            File.AppendAllText(filename, Environment.NewLine);
        }
    }
}
