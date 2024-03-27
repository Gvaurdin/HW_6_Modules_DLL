using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemUtility
{
    public static class FileManager
    {
        // метод для копирования файла
        public static void CopyFile(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath);
        }

        // метод для копирования директории

        public static void CopyDirectory(string sourceDir, string destinationDir)
        {
            // создаем объект, представляющий информацию о директории исходного пути
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDir);

            // проверяем, существует ли исходная директория 
            if (!sourceDirectory.Exists)
                throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");

            // создаем целевую директорию, если она не существует 
            if (!Directory.Exists(destinationDir))
                Directory.CreateDirectory(destinationDir);

            // копируем саму директорию
            string destDir = Path.Combine(destinationDir, sourceDirectory.Name);
            Directory.CreateDirectory(destDir);

            // копируем все файлы из исходной директории в целевую 
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destDir, fileName);
                File.Copy(file, destFile, true);
            }

            // рекурсивно копируем поддиректории и их содержимое
            foreach (string subdir in Directory.GetDirectories(sourceDir))
            {
                CopyDirectory(subdir, destDir);
            }

        }

        // метод для удаления файлов
        public static void DeleteFile(string directoryPath, string fileName)
        {
            string filePath = Path.Combine(directoryPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        //метод для удаления файлов по набору имен
        public static void DeleteFiles(string directoryPath, string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                DeleteFile(directoryPath, fileName);
            }
        }

        // метод для удаления файлов по маске
        public static void DeleteFilesByMask(string directoryPath, string searchPattern)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), searchPattern);

            // вызываем метод DeleteFiles для удаления найденных файлов
            DeleteFiles(directoryPath, files);
        }

        //метод для перемещения файла
        public static void MoveFile(string sourcePath, string destinationPath)
        {
            File.Move(sourcePath, destinationPath); // используем метод Move класса File для перемещения
        }

        // метод для поиска слова в файле и создания отчета
        public static void SearchWordInFile(string filePath, string searchWord, string reportDirectory)
        {
            try
            {
                // проверяем, существование файла
                if(!File.Exists(filePath)) 
                {
                    throw new FileNotFoundException("File not found.", filePath);
                }

                //читаем содержимое файла
                string[] inputFile = File.ReadAllLines(filePath);

                //создаем путь к файлу отчета
                string reportFilePath = Path.Combine(reportDirectory,$"Report_{DateTime.Now:d}.txt");
                
                //создаем новый файл отчета
                using(StreamWriter writer = new StreamWriter(reportFilePath)) 
                {
                    writer.WriteLine($"Search word : {searchWord}");
                    writer.WriteLine($"File : {filePath}");
                    writer.WriteLine($"Occurrences : ");

                    //ищем слово в каждой 
                    for(int i  = 0; i < inputFile.Length; i++) 
                    {
                        if (inputFile[i].Contains(searchWord))
                        {
                            writer.WriteLine($"Line {i + 1}: {inputFile[i]}");
                        }   
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //метод для поиска слова в папке и создания отчета
        public static void SearchWordInFolder(string folderPath, string searchWord, string reportDirectory)
        {
            try
            {
                if(!Directory.Exists(folderPath))
                {
                    throw new DirectoryNotFoundException($"Directory not found to path : {folderPath}");
                }

                //получаем все файлы в папке
                string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

                string reportFilePath = Path.Combine(reportDirectory, $"Report_{DateTime.Now:d}.txt");

                using (StreamWriter writer = new StreamWriter(reportFilePath))
                {
                    writer.WriteLine($"Search word : {searchWord}");
                    writer.WriteLine($"Folder : {folderPath}");
                    writer.WriteLine($"Occurrences : ");

                    // просматриваем каждый файл в папке и ищем слово в нем
                    foreach (string file in files)
                    {
                        string[] lines = File.ReadAllLines(file);
                        for (global::System.Int32 i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Contains(searchWord))
                            {
                                writer.WriteLine($"File: {file}, Line {i+1}: {lines[i]}");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
