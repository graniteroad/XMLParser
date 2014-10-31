using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XMLReader
{
    interface ISearch
    {
        // XML search specific
        List<FileInfo> XMLfiles(string path);
        bool isXmlFile(FileInfo file);
        // Non-specific search
        List<FileInfo> Files(string path, string extension);
    }
}
