using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C9.Feature.FileSystem.Models
{
    public class FileMetadata
    {
        public string FileName { get; set; }
        public DateTime CreationDate { get; set; }

        public string FileType { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsReadOnly { get; set; }

        public long Lenght { get; set; }
    }
}