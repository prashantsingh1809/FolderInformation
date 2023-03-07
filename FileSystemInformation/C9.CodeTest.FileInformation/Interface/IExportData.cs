using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C9.Feature.FileSystem
{
    public enum FileType
    {
        Any = 0,
        CSV = 1,
        Excel = 2,
        Pdf = 3,
        Docx = 4,
        XML = 5
    }

    public interface IExportData
    {
        FileType FileType { get; }

        void ExportData<T>(List<T> files, string exportFileName);
    }
}