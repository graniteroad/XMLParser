using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLReader
{
    public class Files
    {
        private string _fileName = "";
        private string _filePath = "";

        public Files()
        { }

        public Files(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
    }
}
