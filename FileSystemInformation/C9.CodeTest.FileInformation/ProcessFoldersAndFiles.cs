using C9.Feature.FileSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace C9.Feature.FileSystem
{
    public class ProcessFoldersAndFiles
    {
        public static List<FileMetadata> GetAllFiles(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
                return null;

            DirectoryInfo dirInfo = new DirectoryInfo(fullPath);
            List<FileMetadata> files = new List<FileMetadata>();
            FileInfo[] totalFiles = dirInfo.GetFiles();
            foreach (FileInfo file in totalFiles)
            {
                files.Add(new FileMetadata()
                {
                    CreationDate = file.CreationTime,
                    FileName = file.Name,
                    FileType = file.Extension.TrimStart('.'),
                    IsReadOnly = file.IsReadOnly,
                    LastModified = file.LastWriteTime,
                    Lenght = file.Length
                });
            }

            DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in subDirectories)
            {
                files.AddRange(GetAllFiles(directoryInfo.FullName));
            }

            return files;
        }
    }
}