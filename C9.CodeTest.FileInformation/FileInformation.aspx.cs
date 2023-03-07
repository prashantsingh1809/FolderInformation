using C9.Feature.FileSystem;
using C9.Feature.FileSystem.Models;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace C9.CodeTest.FileInformation
{
    /// <summary>
    /// This page is to iterate over folders and files for specific location.
    /// </summary>
    public partial class FileInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEnumToDropDown();
            }
        }

        #region Control Events
        protected void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            LoadFiles();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                List<FileMetadata> files;

                // If user wants to export selected file type information
                if (ddlFileList.SelectedValue == "0")
                    files = (List<FileMetadata>)Session["files"];
                else
                    files = (List<FileMetadata>)Session["filteredlist"];

                if (files != null && files.Count > 0)
                {
                    // Get Desired exporter type object.
                    var exporter = ExportManager.GetExporter(FileType.CSV);
                    exporter.ExportData<FileMetadata>(files, "ReportFile");

                    lblMessage.Text = "File exported successfully";
                }
                else
                {
                    lblMessage.Text = "Files not selected!!";
                }
            }
            catch (Exception ex)
            {
                Global.logger.Error(ex.Message, ex);
                lblMessage.Text = $"Failed to export. {Environment.NewLine} {ex.Message}";
            }
        }

        protected void ddlFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FileMetadata> files = (List<FileMetadata>)Session["files"];
            FileType value = (FileType)Enum.Parse(typeof(FileType), ddlFileList.SelectedValue);

            // when all files should be display.
            if (value == FileType.Any)
            {
                gvFileList.DataSource = files;
            }
            else
            {
                // Get filtered data based on file type selected from dropdown.
                var filteredList = files.Where(f => f.FileType.ToLower() == value.ToString().ToLower()).ToList();
                Session["filteredlist"] = filteredList;
                gvFileList.DataSource = filteredList;
            }

            gvFileList.DataBind();
        }

        protected void gvFileList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFileList.PageIndex = e.NewPageIndex;
            gvFileList.DataSource = (List<FileMetadata>)Session["files"];
            gvFileList.DataBind();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load all files path for selected folder. 
        /// </summary>
        private void LoadFiles()
        {
            try
            {
                using (CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog()
                {
                    IsFolderPicker = true
                })
                {
                    if (openFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        txtBrowseFolder.Text = openFileDialog.FileName;
                        Global.logger.Info($"Selected Path: {txtBrowseFolder.Text}");

                        // Get All files from selected folder.
                        List<FileMetadata> files = ProcessFoldersAndFiles.GetAllFiles(txtBrowseFolder.Text);
                        Global.logger.Info($"Total File count: {files.Count}");

                        // get item by group by and count of those.
                        files.GroupBy(a => a.FileType);
                        var lst = files.GroupBy(u => u.FileType).Select(grp => new { FileType = grp.Key.ToLower(), Count = grp.Count() }).ToList();
                        Session["numberoffilesbytype"] = lst;
                        Session["files"] = files;
                        //Show total file count.
                        lblFileCountValue.Text = files.Count().ToString();

                        // Bind the file list to gridview
                        gvFileList.DataSource = files;
                        gvFileList.DataBind();

                        // Bind file count with respect to file type.
                        gvFilesByType.DataSource = lst;
                        gvFilesByType.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Method to bind FileType to dropdown so that user can filter it.
        /// </summary>
        private void BindEnumToDropDown()
        {
            try
            {
                Array itemValues = Enum.GetValues(typeof(FileType));
                ddlFileList.DataSource = null;
                foreach (FileType type in itemValues)
                {
                    ListItem item = new ListItem(type.ToString(), ((int)type).ToString());
                    ddlFileList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Global.logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}