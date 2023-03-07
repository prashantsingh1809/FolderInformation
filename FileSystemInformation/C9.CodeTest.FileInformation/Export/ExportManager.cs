using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C9.Feature.FileSystem
{
    /// <summary>
    /// Provide IExporter object to export in deseired object.
    /// </summary>
    public static class ExportManager
    {
        private static List<IExportData> _exporters;
        static ExportManager()
        {
            _exporters = new List<IExportData>();
            // TODO: write logic to load all IExportData type object
            _exporters.Add(new ExportToCSV());
        }

        public static IExportData GetExporter(FileType filetype)
        {
            return _exporters.FirstOrDefault(e => e.FileType == filetype);
        }
    }
}