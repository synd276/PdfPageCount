using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Crypto.Parameters;

namespace PdfPageCount
{
    public class FindFile
    {
        //event khi tìm được file với EventArgs chứa file tìm được kiểu FileInfo
        public event EventHandler<FileEventArgs> FileIsFound;
        //event khi đang tìm file trên 1 folder với EventArgs chứa đường dẫn folder kiểu string
        public event EventHandler<FolderEventArgs> FolderSearch;
        //event khi kết thúc việc tìm kiếm
        public event EventHandler Complete;
        //Thread dùng để tìm kiếm, thread này sẽ tìm sinh ra các event
        //còn form là Thread chính sẽ bắt các event trên
        Thread thrSearch;
        //biến để kiểm tra việc tiếp tục tìm kiếm, mặc định giá trị là true
        //nếu người dùng dừng việc tìm kiếm thì biến này sẽ thành false
        //và thread tìm kiếm sẽ dừng lại 
        bool blnIsContinue;
        //biến đếm số lượng file
        int intNumFileFound;
        //biến đếm số trang
        int intTotalPageNumber;
        //khai báo đối tượng dùng để ghi log
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// property để lấy số lượng file và folder tìm được
        /// </summary>
        public int FileFound
        {
            get { return intNumFileFound; }
        }
        public int TotalPageNumber
        {
            get { return intTotalPageNumber; }
        }

        /// <summary>
        /// phương thức để bắt đầu việc tìm kiếm
        /// </summary>
        public bool StartSearch(string path, DateTime? fromDate = null, DateTime? toDate = null)
        {

            //kiểm tra đường dẫn có tồn tại hay không
            if (!Directory.Exists(path)) return false;
            else
            {
                intNumFileFound = 0;
                intTotalPageNumber = 0;
                object parameters = new object[3] { path, fromDate, toDate };
                //tạo thread mới với hàm SearchFile và bắt đầu thread để tìm kiếm file
                thrSearch = new Thread(new ParameterizedThreadStart(SearchFile));
                thrSearch.Start(parameters);
                return true;
            }
        }
        /// <summary>
        /// phương thức để dừng việc tìm kiếm giữa chừng
        /// </summary>
        public void StopSearch()
        {
            blnIsContinue = false;
        }
        /// <summary>
        /// phương thức để dừng hoàn toàn việc tìm kiếm file
        /// khi đóng form, thread tìm kiếm vẫn còn chạy và sinh ra các sự kiện
        /// nên ta hủy bỏ thread này
        /// </summary>
        public void Abort()
        {
            blnIsContinue = false;
            if (thrSearch != null)
            {
                try
                {
                    thrSearch.Abort();//hủy bỏ thread đang chạy
                }
                catch (ThreadAbortException ex)
                {
                    //Logger.Error(ex);
                }
                catch (Exception )
                {
                    //Logger.Error(ex);
                    throw;
                }
            }
            else { }
        }
        /// <summary>
        /// phương thức tìm file chạy trong thread mới
        /// sẽ gọi hàm đệ quy để tìm file
        /// và phát sinh event hoàn thành việc tìm kiếm khi hàm đệ quy kết thúc
        /// </summary>
        /// <param name="pathInput">đường dẫn mà người dùng nhập vào</param>
        public void SearchFile(object parameters)
        {
            Array parametersArray = new object[3];
            parametersArray = (Array)parameters;
            var pathInput = (string)parametersArray.GetValue(0);
            var fromDate = (DateTime?)parametersArray.GetValue(1);
            var toDate = (DateTime?)parametersArray.GetValue(2);
            blnIsContinue = true;
            if (fromDate == null && toDate == null)   
                Searching(pathInput);
            else
                SearchingWithTime(pathInput, fromDate, toDate);
            Complete(this, EventArgs.Empty);
        }
        /// <summary>
        /// hàm đệ quy tìm file trong folder mà người dùng nhập vào
        /// và gọi hàm tìm tiếp trong các folder con
        /// </summary>
        /// <param name="pathInput"></param>
        private void Searching(object pathInput) //hàm chính tìm kiếm file
        {
            //lấy đường dẫn folder cần tìm
            DirectoryInfo dr = new DirectoryInfo(pathInput.ToString());
            int a4, a3, a2, a1, a0;
            string Uploader, CreatedOn;
            try
            {
                //tìm tất cả file trong folder này
                foreach (FileInfo fileInDir in dr.GetFiles("*.pdf"))
                {
                    try
                    { 
                        if (blnIsContinue)
                        {
                            {//
                                int filePageNumber = GetFilePageNumber(fileInDir.FullName, out Uploader);
                                intTotalPageNumber += filePageNumber;
                                //tìm thấy file thì phát sinh event chứa kết quả tính file tìm được
                                FileIsFound(this, new FileEventArgs(fileInDir.Name, fileInDir.Extension, fileInDir.FullName, filePageNumber, Uploader, fileInDir.CreationTime.ToString()));
                                //tăng số lượng file tìm được
                                intNumFileFound++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        FileIsFound(this, new FileEventArgs(fileInDir.FullName + ": Lỗi k đọc được file này ("+ex.Message+")", "", "", 0, "", ""));
                    }

                }
                //tiếp tục tìm và gọi lại hàm tìm kiếm file trong các folder con
                foreach (DirectoryInfo subPath in dr.GetDirectories())
                {
                    if (blnIsContinue)
                    {
                        //kiểm tra folder con có phải là folder hệ thống hay không
                        //nếu không thì tiếp tục tìm trong folder con này
                        if (!subPath.Attributes.ToString().Contains("System"))
                        {
                            //phát sinh event chứa đường dẫn khi đang tìm file trong folder này
                            FolderSearch(this, new FolderEventArgs(pathInput.ToString()));
                            //đệ quy để tìm tiếp file và folder
                            Searching((object)subPath.FullName);
                        }
                        //nếu là folder hệ thống thì bỏ qua
                        else { }
                    }
                    else
                    {
                        break;
                    }
                }
            }
                //bắt lỗi ko chứng thực để truy cập ở một số folder 
            catch (UnauthorizedAccessException )
            {
                FileIsFound(this, new FileEventArgs(pathInput.ToString() + "Lỗi không đọc được file: ", "", "", 0, "", ""));
            }
            catch (Exception ex)
            {
                FileIsFound(this, new FileEventArgs(pathInput.ToString() + ": Lỗi " + ex.Message, "", "", 0, "", ""));
            }
            finally
            {
            }
        }
        /// <summary>
        /// hàm đệ quy tìm file trong folder và tìm theo thời gian mà người dùng nhập vào
        /// và gọi hàm tìm tiếp trong các folder con
        /// </summary>
        /// <param name="pathInput"></param>
        private void SearchingWithTime(object pathInput, object fromDate, object toDate) //hàm chính thứ 2 tìm kiếm file theo thời gian
        {
            //lấy đường dẫn folder cần tìm
            DirectoryInfo dr = new DirectoryInfo(pathInput.ToString());
            int a4, a3, a2, a1, a0;
            string Uploader;
            string CreatedOn;
            try
            {
                //tìm tất cả file trong folder này
                foreach (FileInfo fileInDir in dr.GetFiles("*.pdf"))
                {
                    try
                    {
                        if (blnIsContinue)
                        {
                            if (fileInDir.CreationTime >= (DateTime)fromDate && fileInDir.CreationTime <= (DateTime)toDate)
                            {//
                                int filePageNumber = GetFilePageNumber(fileInDir.FullName, out Uploader);
                                intTotalPageNumber += filePageNumber;
                                //tìm thấy file thì phát sinh event chứa kết quả tính file tìm được
                                FileIsFound(this, new FileEventArgs(fileInDir.Name, fileInDir.Extension, fileInDir.FullName, filePageNumber, Uploader, fileInDir.CreationTime.ToString("yyyy/MM/dd HH:mm:ss")));
                                //tăng số lượng file tìm được
                                intNumFileFound++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        FileIsFound(this, new FileEventArgs(fileInDir.FullName + ": Lỗi k đọc được file này (" + ex.Message + ")", "", "", 0, "", ""));
                    }

                }
                //tiếp tục tìm và gọi lại hàm tìm kiếm file trong các folder con
                foreach (DirectoryInfo subPath in dr.GetDirectories())
                {
                    if (blnIsContinue)
                    {
                        //kiểm tra folder con có phải là folder hệ thống hay không
                        //nếu không thì tiếp tục tìm trong folder con này
                        if (!subPath.Attributes.ToString().Contains("System"))
                        {
                            //phát sinh event chứa đường dẫn khi đang tìm file trong folder này
                            FolderSearch(this, new FolderEventArgs(pathInput.ToString()));
                            //đệ quy để tìm tiếp file và folder
                            //Searching((object)subPath.FullName);
                            SearchingWithTime((object)subPath.FullName, fromDate, toDate);
                        }
                        //nếu là folder hệ thống thì bỏ qua
                        else { }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //bắt lỗi ko chứng thực để truy cập ở một số folder 
            catch (UnauthorizedAccessException)
            {
                FileIsFound(this, new FileEventArgs(pathInput.ToString() + "Lỗi không đọc được file: ", "", "", 0, "", ""));
            }
            catch (Exception ex)
            {
                FileIsFound(this, new FileEventArgs(pathInput.ToString() + ": Lỗi " + ex.Message, "", "", 0, "", ""));
            }
            finally
            {
            }
        }
        public int GetFilePageNumber(string filepath,out string Uploader)//lấy số trang mỗi file PDF
        {
            int a4 = 0;
            int a3 = 0;
            int a2 = 0;
            int a1 = 0;
            int a0 = 0;
            Uploader = "";
            if (!filepath.ToLower().PadLeft(4).Contains(".pdf")) return 0;
            int a4sizePageNumber = 0;
            PdfReader pdfReader = new PdfReader(filepath);
            if (pdfReader.Info.ContainsKey("Uploader")) Uploader = pdfReader.Info["Uploader"];
            int numberOfPages = pdfReader.NumberOfPages;
            //chạy mồi trang trong file 
            for (var i = 1; i <= numberOfPages; i++)
            {
                var pageSize = pdfReader.GetCropBox(i);
                var pageArea = pageSize.Height * pageSize.Width;
                //tính số trang của hồ sơ theo cách chia diện tích a4 chuẩn
                //nếu dưới gấp rưỡi của mốc thì làm tròn về mốc
                //nếu trên gấp rưỡi của mốc thì làm tròn số thập phân
                //Các mốc lấy theo loại giấy A3=2, A2=4, A1=8, A0=16
                var a4SizeNumber = 0;
                var floatA4SizeNumber = pageArea / 500990;
                //so sánh diện tích từng trang trong file PDF so với trang A4 chuẩn

                if (floatA4SizeNumber <= 1.5)
                {//nếu diện tích nhỏ hơn trang A4 chuẩn thì tính là trang A4 là 1 trang
                    a4SizeNumber = 1;
                    a4++;
                }
                
                else if (floatA4SizeNumber > 1.5 && floatA4SizeNumber < 3.5) 
                //nếu diện tích gấp trang A4 chuẩn khoảng từ 2 đến gấp rưỡi 2 thì tính là trang A3 là 2 trang
                {
                    a4SizeNumber = 2;
                    a3++;
                }
                else if (floatA4SizeNumber >= 3.5 && floatA4SizeNumber < 7.5) 
                //nếu diện tích gấp trang A4 chuẩn khoảng từ 4 đến gấp rưỡi thì tính là trang A2 là 4 trang
                {
                    a4SizeNumber = 4;
                    a2++;
                }
                else if (floatA4SizeNumber >= 7.5 && floatA4SizeNumber < 15) 
                //nếu diện tích gấp trang A4 chuẩn khoảng từ 8 đến gấp rưỡi thì tính là trang A1 là 8 trang
                {
                    a4SizeNumber = 8;
                    a1++;
                }
                else if (floatA4SizeNumber >= 15) 
                //nếu diện tích gấp trang A4 chuẩn khoảng từ 16 đến gấp rưỡi thì tính là trang A0 là 16 trang
                {
                    a4SizeNumber = 16;
                    a0++;
                }
                //else a4SizeNumber = (int)Math.Round(floatA4SizeNumber);
                //các trường hợp còn lại là lớn hơn gấp rưỡi của các mốc thì tính sồ trang là làm tròn (bỏ) 

                a4sizePageNumber += a4SizeNumber;

                //tính số trang theo cách tách theo loại trang A0, A1, A2, A3
                //if (pageArea <= 600000) { a4++; a4pagenumber++; }
                //else if (pageArea >= 900000 && pageArea <= 1500000) { a3++; a4pagenumber += 2; }
                //else if (pageArea >= 1800000 && pageArea <= 3000000) { a2++; a4pagenumber += 4; }
                //else if (pageArea >= 3600000 && pageArea <= 6000000) { a1++; a4pagenumber += 8; }
                //else if (pageArea >= 7200000 && pageArea <= 10000000) { a0++; a4pagenumber += 16; }
                //else throw new Exception("Không xác định đc diện tích trang PDF: " + pageArea.ToString());
            }
            pdfReader.Dispose();
            return a4sizePageNumber;
        }
    }
}
