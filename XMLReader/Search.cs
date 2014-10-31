using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace XMLReader
{
    /// <summary>
    /// Search For files
    /// </summary>
    public class Search : ISearch
    {
        List<FileInfo> files = null;
        #region Public Methods
        public List<FileInfo> Files(string path, string extension)
        {
            // initialize List
            files = new List<FileInfo>();
            // Search Directory
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var f in dir.EnumerateFiles(string.Format("*{0}", extension), SearchOption.AllDirectories))
            {
                files.Add(f); // add XML file to list
            }
            return files;
        }
        public List<FileInfo> XMLfiles(string path)
        {
            // initialize List
            files = new List<FileInfo>();
            // Search Directory
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var f in dir.EnumerateFiles("*.xml", SearchOption.AllDirectories))
            { 
                // double check
                if (isXmlFile(f))
                    files.Add(f); // add XML file to list
            }
            return files;
        }
        public List<Files> XMLfiles(string path, BackgroundWorker bw, ref int iProgress)
        {
            // initialize List
            List<Files> Files = new List<Files>();
            // Search Directory
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var f in dir.EnumerateFiles("*.xml", SearchOption.AllDirectories))
            {
                // double check
                if (isXmlFile(f))
                {
                    Files newFile = new Files(f.Name, f.FullName);
                    Files.Add(newFile); // add XML file to list
                }
                iProgress++;
                bw.ReportProgress(iProgress);
            }
            return Files;
        }
        /// <summary>
        /// Checks for XML extension of file
        /// </summary>
        /// <param name="file">File to check</param>
        /// <returns>bool</returns>
        public bool isXmlFile(FileInfo file)
        {
            return file.Extension.Equals(".xml");
        }
        /// <summary>
        /// Checks if List is nothing or 0
        /// </summary>
        /// <returns>bool</returns>
        public bool isListNullOrEmpty()
        {
            if (files == null)
                return true;

            return files.Count.Equals(0);
        }
        #endregion
    }
}
