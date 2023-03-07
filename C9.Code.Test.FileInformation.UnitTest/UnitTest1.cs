using C9.Feature.FileSystem;
using C9.Feature.FileSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace C9.Code.Test.FileInformation.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetFileList()
        {
            string folderpath = @"C:\Sample-XML";
            var list = ProcessFoldersAndFiles.GetAllFiles(folderpath);
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void SelectFolderWithoutFiles()
        {
            string folderpath = @"C:\Test";
            var list = ProcessFoldersAndFiles.GetAllFiles(folderpath);
            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void ExportDatatoCSV()
        {
            string folderpath = @"C:\Sample-XML";
            var list = ProcessFoldersAndFiles.GetAllFiles(folderpath);
            if (list.Count > 0)
            {
                ExportToCSV exportToCSV = new ExportToCSV();
                exportToCSV.ExportData<FileMetadata>(list, "TestReportFile");
            }
        }
    }
}
