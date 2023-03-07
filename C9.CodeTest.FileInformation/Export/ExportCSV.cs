using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace C9.Feature.FileSystem
{
    public class ExportToCSV : IExportData
    {
        public FileType FileType => FileType.CSV;

        public void ExportData<T>(List<T> files, string exportFileName)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var finalPath = Path.Combine(basePath, exportFileName + ".csv");

            StringBuilder builder = new StringBuilder();
            if (!File.Exists(finalPath))
            {
                // Create file and headers in it for first time.
                CreateCsvFile<T>(finalPath, builder);
            }
            var info = typeof(T).GetProperties();
            // Write all the values of properites.
            foreach (var obj in files)
            {
                builder = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + "; ";
                }
                line = line.Substring(0, line.Length - 2);
                builder.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(builder.ToString());
                sw.Close();
            }
        }

        private static void CreateCsvFile<T>(string finalPath, StringBuilder builder)
        {
            var header = "";
            var file = File.Create(finalPath);
            file.Close();
            foreach (var prop in typeof(T).GetProperties())
            {
                header += prop.Name + "; ";
            }
            header = header.Substring(0, header.Length - 2);
            builder.AppendLine(header);
            TextWriter sw = new StreamWriter(finalPath, true);
            sw.Write(builder.ToString());
            sw.Close();
        }
    }
}