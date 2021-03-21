using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager_Budgo
{
     class Program
    {   
        static void Main(string[] args)
        {
            string memu;
            bool end = false;
            string config = "Config.txt";
            string configDir = @"C:\";
            int q = 0;
            Manager.help();
            do
            {
                Console.WriteLine("Введите команду");
                memu = Console.ReadLine();
                Console.Clear();
                switch (memu)
                {
                    case "help":
                        Manager.help();
                        break;
                    case "ls":
                        Console.WriteLine("Введите путь к каталогу: ");
                        string folderPath = Console.ReadLine();
                        DirectoryInfo dir = new DirectoryInfo(folderPath);
                        Manager.configSave(config, folderPath);
                        q = Manager.configpar(config);
                        Manager.ls(dir,q);
                        break;
                    case "cd":
                        Console.WriteLine("Введите название каталога: ");                        
                        string newCat = Console.ReadLine();                      
                        configDir = Manager.configDirecvori(config);
                        q = Manager.configpar(config);
                        Manager.configSave(config, Manager.cd(configDir, newCat, q));
                        break;
                    case "rm":
                        Console.WriteLine("Введите название файла или каталога для удаления: ");
                        string rmCat = Console.ReadLine();
                        configDir = Manager.configDirecvori(config);
                        Manager.rm(configDir, rmCat);
                        break;
                    case "cp":
                        Console.WriteLine("Введите название файла или каталога для копирования: ");
                        string cpCat = Console.ReadLine();
                        configDir = Manager.configDirecvori(config);
                        Manager.cp(configDir, cpCat);
                        break;
                    case "crfile":
                        Console.WriteLine("Введите название файла для создания ");
                        string crFile = Console.ReadLine();
                        configDir = Manager.configDirecvori(config);
                        Manager.crfile(configDir, crFile);
                        break;
                    case "crcat":
                        Console.WriteLine("Введите название каталога для создания ");
                        string crCat = Console.ReadLine();
                        configDir = Manager.configDirecvori(config);
                        Manager.crcat(configDir, crCat);
                        break;
                    case "file":
                        configDir = Manager.configDirecvori(config);
                        q = Manager.configpar(config);
                        DirectoryInfo dirfile = new DirectoryInfo(configDir);
                        Manager.ls(dirfile, q);
                        break;
                    case "info":
                        Console.WriteLine("Введите название файла или каталога: ");
                        string infoCat = Console.ReadLine();
                        configDir = Manager.configDirecvori(config);
                        Manager.info(configDir, infoCat);
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
