using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Win32;

namespace PdfPageCount
{
    public partial class frmMain : Form
    {
        FindFile findFile;
        private delegate void dlgAddFile(string fileName, string extension, string filePath, string PageNumber, string uploader, string CreatedOn);
        private delegate void dlgDisplayStatus(string strFolderSearch);
        private delegate void dlgShowResult(IWin32Window owner);
        IAsyncResult IAsyncFile;
        public frmMain()
        {
            InitializeComponent();
            //tạo đối mới tượng dùng để tìm file
            findFile = new FindFile();
            //thêm hàm bắt sự kiện từ đối tương findFile
            findFile.FileIsFound += new System.EventHandler<FileEventArgs>(this.findFile_FileIsFound);
            findFile.FolderSearch += new System.EventHandler<FolderEventArgs>(this.findFile_FolderSearch);
            findFile.Complete += new System.EventHandler(this.findFile_SearhComplete);

            //lấy lại đường dẫn folder trước đó đã lưu trong registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PdfPageCount");
            if (key != null)
            {
                lblPath.Text = key.GetValue("Path").ToString();
            }

            //Load ngày cho dtpToDate
            dtpToDate.Value = new DateTime(2023,01,31);
        }
        private void lblPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog Browse = new FolderBrowserDialog();
            //Browse.RootFolder = Environment.SpecialFolder.;
            Browse.Reset();
            Browse.RootFolder = Environment.SpecialFolder.MyComputer;
            Browse.SelectedPath = lblPath.Text+@"\";
            if (Browse.ShowDialog() == DialogResult.OK)
            {
                lblPath.Text = Browse.SelectedPath;
                //rootFolder = Browse.RootFolder;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                StartSearch();
            }
            else
            {
                StopSearch();
            }
        }

        private void StartSearch()
        {
            DateTime? fromDate = null, toDate = null;
            if (chkIsFilterDate.Checked == true)
            {
                fromDate = dtpFromDate.Value;
                toDate = dtpToDate.Value;
                if (fromDate.Value > toDate.Value)
                {
                    MessageBox.Show("Ngày bắt đầu lớn hơn ngày kết thúc là không được!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
            //gọi hàm để bắt đầu thread tìm kiếm trong findFile
            if (findFile.StartSearch(lblPath.Text, fromDate, toDate))
            {
                grvFiles.Rows.Clear();//xóa các tất cả các dòng trên grid
                btnStart.Text = "Stop";//đổi tên nút
                lblPath.Enabled = false;//vô hiệu hóa textbox nhập đường dẫn
                //Stopwatcher.Reset();
                //Stopwatcher.Start();//bắt đầu đếm thời gian chạy

            }
            else
            {
                //hiện thông báo nếu hàm StartSearch trả về là false
                //nghĩa là đường dẫn mà người dùng nhập vào không tồn tại
                MessageBox.Show("Đường dẫn không tồn tại!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void StopSearch()
        {
            findFile.StopSearch();//gọi hàm để dừng thread tìm kiếm
            btnStart.Text = "Start";//đổi tên nút 
            lblPath.Enabled = true;//mở lại textbox
            //staFolderPath.Text = "Current folder process: Searching done";//sửa lại thanh status

            if (IAsyncFile==null)
            {
                DisplayStatus("Folder không có file pdf nào");
            }    
            else
            grvFiles.EndInvoke(IAsyncFile);
            //this.EndInvoke(IAsyncFolder);
        }

        private void grvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void findFile_FileIsFound(object sender, FileEventArgs e)
        {//xử lý thông tin file của FileEventArgs khi thread tìm được trả về
            IAsyncFile = grvFiles.BeginInvoke(new dlgAddFile(AddFile), e.FileName, e.Extension, e.FilePath, e.PageNumber, e.Uploader, e.CreatedOn);
        }
        private void AddFile(string fileName, string extension, string filePath, string PageNumber, string uploader, string createdOn)
        {
            //intNumFileCount++;
            //grvFiles.Rows.Add(intNumFileCount.ToString(), fileName, fileSize.ToString() + " KB");
            //grvFiles.Rows.Add(fileName, PageNumber, a4, a3, a2, a1, a0);
            grvFiles.Rows.Add(fileName, extension, filePath, PageNumber, uploader, createdOn);
            //MessageBox.Show(uploader);
            
            grvFiles.FirstDisplayedScrollingRowIndex = grvFiles.RowCount - 1; //scroll xuống dòng mới thêm
        }
        //private void AddFile(string fileName, string PageNumber, string a4, string a3, string a2, string a1, string a0, string uploader)
        //{
        //    //intNumFileCount++;
        //    //grvFiles.Rows.Add(intNumFileCount.ToString(), fileName, fileSize.ToString() + " KB");
        //    grvFiles.Rows.Add(fileName, PageNumber, a4, a3, a2, a1, a0, uploader);
        //    grvFiles.FirstDisplayedScrollingRowIndex = grvFiles.RowCount - 1;
        //}
        private void findFile_FolderSearch(object sender, FolderEventArgs e)
        {
            this.Invoke(new dlgDisplayStatus(DisplayStatus), e.FolderPath);
        }
        private void DisplayStatus(string text)
        {
            staFolderPath.Text = text;
        }
        private void findFile_SearhComplete(object sender, EventArgs e)
        {
            this.Invoke(new dlgShowResult(ShowResult), (Control)this);
        }
        private void ShowResult(IWin32Window owner)
        {
            staFolderPath.Text = "Tổng cộng: "+ findFile.FileFound + " file PDF với " + findFile.TotalPageNumber + " trang";
            StopSearch();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            findFile.Abort();
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PdfPageCount");
            key.SetValue("Path", lblPath.Text);
            key.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void chkIsFilterDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsFilterDate.Checked)
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
            }    
            else
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }    
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            //grvFiles.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            grvFiles.SelectAll();
            DataObject dataObj = grvFiles.GetClipboardContent();
            
            Clipboard.SetDataObject(dataObj.GetData(DataFormats.UnicodeText) as string, true);
        }
    }
}
