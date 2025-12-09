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
        // hàm thêm sinh viên 
        public static void AddStudent()
        {
            C.WriteLine("\n-----Thêm sinh viên-----");
            // mã sinh viên 
            C.WriteLine("Nhập mã sinh viên: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Mã sinh viên không được để trống.");
            }
            string maSv = input;
            // họ tên sinh viên 
            Console.Write("Nhập họ và tên: ");
            string fullNameOfStudent = Console.ReadLine();

            while (fullNameOfStudent.Any(char.IsDigit))
            {
                Console.WriteLine("Lỗi: Họ và tên không được chứa số. Vui lòng nhập lại!");
                Console.Write("Nhập họ và tên: ");
                fullNameOfStudent = Console.ReadLine();
            }

            Console.WriteLine("Họ và tên hợp lệ: " + fullNameOfStudent);

        }
        
    }
    
}