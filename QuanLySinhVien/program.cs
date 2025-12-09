using System;
using System.Text.Json;
using C = System.Console;


namespace QuanLyKhoi.QuanLySinhVien
{
    public class Program
    {
        // lưub  trữ dữ liệu sinh viên toàn cục 
        private static List<SinhVien> danhSachSv = new List<SinhVien>();
        public const string File_Name = "dataSinhVien.json";

        // doc file 
        public static void DocFile()
        {
            // kiem tra file ton tai
            if (!File.Exists(File_Name))
            {
                Console.WriteLine("File dữ liệu chưa tồn tại. Bắt đầu với danh sách trống.");
                danhSachSv = new List<SinhVien>();
                return;
            }
            // neu ton tai file thi doc file 
            try
            {
                string jsonString = File.ReadAllText(File_Name);
                var loadlist = JsonSerializer.Deserialize<List<SinhVien>>(jsonString);

                if (loadlist != null)
                {
                    danhSachSv = loadlist;
                    C.WriteLine("Đã đọc thành công file dữ liệu sinh viên ");
                }
                else
                {
                    C.WriteLine("File trống or không hợp lệ");
                    danhSachSv = new List<SinhVien>();
                }
                // Bắt các lỗi cụ thể liên quan đến JSON và IO}

            }
            catch (JsonException)
            {
                Console.WriteLine("Lỗi định dạng JSON trong file. Khởi tạo danh sách trống.");
                danhSachSv = new List<SinhVien>();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Lỗi I/O khi đọc file: {ex.Message}. Khởi tạo danh sách trống.");
                danhSachSv = new List<SinhVien>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định khi đọc file: {ex.Message}. Khởi tạo danh sách trống.");
                danhSachSv = new List<SinhVien>();
                
            }
        }


        // lưu fille 
        public static void LuuFile()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(danhSachSv);
                File.WriteAllText(File_Name,jsonString);
            }
            catch(Exception ex)
            {
                C.WriteLine($"Lỗi khi lưu file{ex.Message}");
            }

        }

        public static void AddBook()
        {
            
        }
        
    }
    
}