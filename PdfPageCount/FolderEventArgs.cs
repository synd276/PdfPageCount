using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfPageCount
{
    /// <summary>
    /// class kế thừa EvenArgs để truyền đường dẫn folder đang tìm vào sự kiện
    /// </summary>
    public class FolderEventArgs : EventArgs
    {
        public FolderEventArgs(string folderPath)
        {
            FolderPath = folderPath;
        }

        public string FolderPath
        {
            get;
            set;
        }

    }

}
