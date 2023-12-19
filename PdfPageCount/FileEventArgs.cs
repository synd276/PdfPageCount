using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfPageCount
{
    /// <summary>
    /// class kế thừa EvenArgs để truyền file tìm được vào sự kiện
    /// </summary>
    public class FileEventArgs : EventArgs
    {
        //public FileEventArgs(string fileName, string pageNumber)//, string a4, string a3, string a2, string a1, string a0)
        //{
        //    FileName = fileName;
        //    PageNumber = pageNumber;
        //    //A4 = a4;
        //    //A3 = a3;
        //    //A2 = a2;
        //    //A1 = a1;
        //    //A0 = a0;
        //}

        //public FileEventArgs(string fileName, int pageNumber, int a4, int a3, int a2, int a1, int a0)
        //{
        //    FileName = fileName;
        //    PageNumber = pageNumber.ToString();
        //    A4 = a4.ToString();
        //    A3 = a3.ToString();
        //    A2 = a2.ToString();
        //    A1 = a1.ToString();
        //    A0 = a0.ToString();
        //}

        //public FileEventArgs(string fileName, int pageNumber, int a4, int a3, int a2, int a1, int a0, string uploader)
        //{
        //    FileName = fileName;
        //    PageNumber = pageNumber.ToString();
        //    A4 = a4.ToString();
        //    A3 = a3.ToString();
        //    A2 = a2.ToString();
        //    A1 = a1.ToString();
        //    A0 = a0.ToString();
        //    Uploader = uploader;
        //}
        public FileEventArgs(string fileName, string extension, string filePath, int pageNumber, string uploader, string createdOn)
        {
            FileName = fileName;
            Extension = extension;
            FilePath = filePath;
            PageNumber = pageNumber.ToString();
            Uploader = uploader;
            CreatedOn = createdOn;
        }
        public string FileName
        {
            get;
            set;
        }
        public string Extension
        {
            get;
            set;
        }
        public string FilePath
        {
            get;
            set;
        }
        public string PageNumber
        {
            get;
            set;
        }

        //public string A4
        //{
        //    get;
        //    set;
        //}
        //public string A3
        //{
        //    get;
        //    set;
        //}
        //public string A2
        //{
        //    get;
        //    set;
        //}
        //public string A1
        //{
        //    get;
        //    set;
        //}
        //public string A0
        //{
        //    get;
        //    set;
        //}
        public string Uploader
        {
            get;
            set;
        }
        public string CreatedOn
        {
            get;
            set;
        }
    }
}
