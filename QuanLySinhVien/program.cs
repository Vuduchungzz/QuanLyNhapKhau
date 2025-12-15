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
                C.WriteLine("File dữ liệu chưa tồn tại. Bắt đầu với danh sách trống.");
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
                C.WriteLine("Lỗi định dạng JSON trong file. Khởi tạo danh sách trống.");
                danhSachSv = new List<SinhVien>();
            }
            catch (IOException ex)
            {
                C.WriteLine($"Lỗi I/O khi đọc file: {ex.Message}. Khởi tạo danh sách trống.");
                danhSachSv = new List<SinhVien>();
            }
            catch (Exception ex)
            {
                C.WriteLine($"Lỗi không xác định khi đọc file: {ex.Message}. Khởi tạo danh sách trống.");
                danhSachSv = new List<SinhVien>();

            }
        }
        // lưu fille 
        public static void LuuFile()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(danhSachSv);
                File.WriteAllText(File_Name, jsonString);
            }
            catch (Exception ex)
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
            string? input = C.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Mã sinh viên không được để trống.");
            }
            string maSv = input;
            // họ tên sinh viên 
            C.Write("Nhập họ và tên: ");
            string? fullNameOfStudent = C.ReadLine();

            if (string.IsNullOrWhiteSpace(fullNameOfStudent))
            {
                throw new Exception("Họ và tên không được để trống.");
            }

            while (fullNameOfStudent.Any(char.IsDigit))
            {
                C.WriteLine("Lỗi: Họ và tên không được chứa số. Vui lòng nhập lại!");
                C.Write("Nhập họ và tên: ");
                fullNameOfStudent = C.ReadLine();

                if (string.IsNullOrWhiteSpace(fullNameOfStudent))
                {
                    throw new Exception("Họ và tên không được để trống.");
                }
            }

            C.WriteLine("Họ và tên hợp lệ: " + fullNameOfStudent);

            // nhập tuổi của sinh viên 

            C.Write("Nhập số tuổi của sinh viên: ");
            if (!int.TryParse(C.ReadLine(), out int tuoi) || tuoi <= 0)
            {
                throw new ArgumentException("Tuổi không hợp lệ.");
            }

            // nhập điểm trung bình 

            C.Write("Nhập điểm trung bình (0-10): ");

            if (!double.TryParse(C.ReadLine(), out double diemTbinh) || diemTbinh < 0 || diemTbinh >= 10)
            {
                throw new ArgumentException("Điểm trung bình không hợp lệ!");
            }
            danhSachSv.Add(new SinhVien(maSv, fullNameOfStudent, tuoi, diemTbinh));
            C.WriteLine("Lưu file thành công!");
            LuuFile();
            Thread.Sleep(1500);
        }
        // sửa sinh viên 
        public static void SuaSinhVien()
        {
            C.Write("\n---Sửa sinh viên---");

            C.WriteLine("Nhập mã sinh viên cần sửa: ");

            string? MaSinhVien = C.ReadLine();

            SinhVien? svCanSua =
                (from sv in danhSachSv
                 where sv.MaSV.Equals(MaSinhVien, StringComparison.OrdinalIgnoreCase)
                 select sv).FirstOrDefault();

            if (svCanSua == null)
            {
                C.WriteLine("Không tìm thấy sinh viên với mã này.");
                return;
            }

            C.WriteLine($"\n ---thông tin cũ {svCanSua.ToString}---");

            C.Write("Nhập Tên mới (Enter để giữ nguyên): ");
            string? HoTenMoi = C.ReadLine();
            if (!string.IsNullOrEmpty(HoTenMoi))
            {
                svCanSua.HoTen = HoTenMoi;
            }
            C.WriteLine("\nSửa thông tin thành công!");
            LuuFile();
            Thread.Sleep(1500);

            // nhập tuổi của sinh viên 
            C.Write("Nhập tuổi mới của sinh viên: ");
            
        }

        // xoá sinh viên 
        public static void xoaSinhVien()
        {
            C.WriteLine("=== Xoá sinh viên === ");
            C.WriteLine("Nhập mã sinh viên cần xoá: ");
            string maSv = Console.ReadLine();
            int soLuongXoa = danhSachSv.RemoveAll(sv => sv.MaSV.Equals(maSv,StringComparison.OrdinalIgnoreCase));

            if(soLuongXoa > 0)
            {
                Console.WriteLine($"\n✅ Đã xóa thành công {soLuongXoa} sinh viên có Mã SV '{maSv}'.");
                LuuFile();
            }
            else
            {
                throw new KeyNotFoundException($"Không tìm thấy sinh viên có Mã SV '{maSv}' để xóa.");
            }
            Thread.Sleep(1500);
        }
        
    }

}