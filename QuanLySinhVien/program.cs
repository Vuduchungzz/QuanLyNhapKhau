// File: Program.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Cần thiết cho LINQ/Lambda
using System.Text.Json;
using System.Threading;
using C = System.Console;

namespace QuanLyKhoi.QuanLySinhVien
{
    // Đã gom tất cả logic vào Program class
    public class Program
    {
        // 🌐 Lưu trữ dữ liệu sinh viên toàn cục
        private static List<SinhVien> danhSachSv = new List<SinhVien>();
        public const string File_Name = "dataSinhVien.json";

        // -----------------------------------------------------------------------
        // LOGIC CHÍNH: MAIN (KHỞI ĐỘNG & MENU)
        // -----------------------------------------------------------------------
        public static void Main(string[] args)
        {
            DocFile(); 
            
            while (true)
            {
                C.Clear();
                HienThiMenu();
                C.Write("Chọn chức năng (1-8): ");
                
                if (int.TryParse(C.ReadLine(), out int choice))
                {
                    try
                    {
                        XuLyChucNang(choice);
                    }
                    catch (Exception ex)
                    {
                        // 🛡️ Xử lý ngoại lệ tổng quát
                        C.ForegroundColor = ConsoleColor.Red;
                        C.WriteLine($"\n❌ LỖI XẢY RA: {ex.Message}");
                        C.ResetColor();
                        Thread.Sleep(2000); 
                    }
                }
                else
                {
                    C.ForegroundColor = ConsoleColor.Yellow;
                    C.WriteLine("Lựa chọn không hợp lệ, vui lòng nhập số từ 1 đến 8.");
                    C.ResetColor();
                    Thread.Sleep(1500);
                }
            }
        }
        
        // -----------------------------------------------------------------------
        // CÁC HÀM TIỆN ÍCH MENU
        // -----------------------------------------------------------------------
        private static void HienThiMenu()
        {
            C.WriteLine("\n======== QUẢN LÝ SINH VIÊN (NON-OOP) ========");
            C.WriteLine("1. Thêm Sinh viên");
            C.WriteLine("2. Sửa Sinh viên");
            C.WriteLine("3. Xóa Sinh viên");
            C.WriteLine("4. Tìm Sinh viên (theo tên)");
            C.WriteLine("5. Sắp xếp Sinh viên");
            C.WriteLine("6. Xem Danh sách");
            C.WriteLine("7. Lưu File và Thoát");
            C.WriteLine("8. Thoát (Không lưu)");
            C.WriteLine("==============================================");
        }

        private static void XuLyChucNang(int choice)
        {
            switch (choice)
            {
                case 1: AddStudent(); break;
                case 2: SuaSinhVien(); break;
                case 3: xoaSinhVien(); break;
                case 4: TimSinhVien(); break; // <-- Hàm mới
                case 5: SapXepSinhVien(); break; // <-- Hàm mới
                case 6: HienThiDanhSach(); break; // <-- Hàm mới
                case 7: 
                    LuuFile();
                    C.WriteLine("Đã lưu và thoát chương trình.");
                    Thread.Sleep(1000);
                    Environment.Exit(0); 
                    break;
                case 8: 
                    C.WriteLine("Thoát chương trình. Dữ liệu chưa lưu có thể bị mất.");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
                default: 
                    C.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    Thread.Sleep(1500);
                    break;
            }
        }

        // -----------------------------------------------------------------------
        // LOGIC CHỨC NĂNG (ĐÃ CÓ TRONG CODE CŨ CỦA BẠN VÀ ĐƯỢC BỔ SUNG)
        // -----------------------------------------------------------------------
        
        // Đọc file 
        public static void DocFile()
        {
            // Logic DocFile của bạn...
            if (!File.Exists(File_Name))
            {
                C.WriteLine("File dữ liệu chưa tồn tại. Bắt đầu với danh sách trống.");
                danhSachSv = new List<SinhVien>();
                return;
            }
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
        
        // Lưu file 
        public static void LuuFile()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(danhSachSv);
                File.WriteAllText(File_Name, jsonString);
                // C.WriteLine("Lưu file thành công!"); // Bỏ comment nếu cần
            }
            catch (Exception ex)
            {
                C.WriteLine($"Lỗi khi lưu file{ex.Message}");
            }
        }

        // Hàm thêm sinh viên 
        public static void AddStudent()
        {
            // Logic AddStudent cũ của bạn...
            C.WriteLine("\n-----Thêm sinh viên-----");
            // Kiểm tra trùng lặp
            C.WriteLine("Nhập mã sinh viên: ");
            string? inputMa = C.ReadLine();
            if (string.IsNullOrWhiteSpace(inputMa)) throw new Exception("Mã sinh viên không được để trống.");
            string maSv = inputMa;
            
            // 🚀 Kiểm tra trùng lặp bằng Lambda/LINQ .Any()
            if (danhSachSv.Any(sv => sv.MaSV.Equals(maSv, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"Mã SV '{maSv}' đã tồn tại.");
            }

            // Họ tên
            C.Write("Nhập họ và tên: ");
            string? fullNameOfStudent = C.ReadLine();
            if (string.IsNullOrWhiteSpace(fullNameOfStudent)) throw new Exception("Họ và tên không được để trống.");
            while (fullNameOfStudent.Any(char.IsDigit))
            {
                C.WriteLine("Lỗi: Họ và tên không được chứa số. Vui lòng nhập lại!");
                C.Write("Nhập họ và tên: ");
                fullNameOfStudent = C.ReadLine();
                if (string.IsNullOrWhiteSpace(fullNameOfStudent)) throw new Exception("Họ và tên không được để trống.");
            }

            // Tuổi
            C.Write("Nhập số tuổi của sinh viên: ");
            if (!int.TryParse(C.ReadLine(), out int tuoi) || tuoi <= 0)
            {
                throw new ArgumentException("Tuổi không hợp lệ.");
            }

            // Điểm trung bình
            C.Write("Nhập điểm trung bình (0-10): ");
            if (!double.TryParse(C.ReadLine(), out double diemTbinh) || diemTbinh < 0 || diemTbinh > 10) // Sửa >= 10 thành > 10
            {
                throw new ArgumentException("Điểm trung bình không hợp lệ!");
            }
            
            danhSachSv.Add(new SinhVien(maSv, fullNameOfStudent, tuoi, diemTbinh));
            C.WriteLine("✅ Thêm sinh viên thành công!");
            LuuFile();
            Thread.Sleep(1500);
        }
        
        // Sửa sinh viên (Đã hoàn thiện logic sửa Tuổi/Điểm TB)
        public static void SuaSinhVien()
        {
            C.WriteLine("\n--- Sửa sinh viên ---");
            C.Write("Nhập mã sinh viên cần sửa: ");
            string? MaSinhVien = C.ReadLine();
            
            // 🚀 Tìm kiếm bằng LINQ Query Syntax (tương đương FirstOrDefault)
            SinhVien? svCanSua = danhSachSv
                .Where(sv => sv.MaSV.Equals(MaSinhVien, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (svCanSua == null)
            {
                throw new KeyNotFoundException("Không tìm thấy sinh viên với mã này.");
            }

            C.WriteLine($"\n--- Thông tin cũ: {svCanSua.ToString()} ---");

            C.Write("Nhập Tên mới (Enter để giữ nguyên): ");
            string? HoTenMoi = C.ReadLine();
            if (!string.IsNullOrEmpty(HoTenMoi))
            {
                svCanSua.HoTen = HoTenMoi;
            }

            // 💡 Sửa Tuổi
            C.Write("Nhập Tuổi mới (Enter để giữ nguyên): ");
            string? tuoiMoiStr = C.ReadLine();
            if (!string.IsNullOrEmpty(tuoiMoiStr) && int.TryParse(tuoiMoiStr, out int tuoiMoi) && tuoiMoi > 0)
            {
                svCanSua.Tuoi = tuoiMoi;
            }

            // 💡 Sửa Điểm TB
            C.Write("Nhập Điểm TB mới (Enter để giữ nguyên): ");
            string? diemTBMoiStr = C.ReadLine();
            if (!string.IsNullOrEmpty(diemTBMoiStr) && double.TryParse(diemTBMoiStr, out double diemTBMoi) && diemTBMoi >= 0 && diemTBMoi <= 10)
            {
                svCanSua.DiemTB = diemTBMoi;
            }

            C.WriteLine("\n✅ Sửa thông tin thành công!");
            LuuFile();
            Thread.Sleep(1500);
        }
        
        // Xoá sinh viên 
        public static void xoaSinhVien()
        {
            C.WriteLine("\n=== Xoá sinh viên === ");
            C.Write("Nhập mã sinh viên cần xoá: ");
            string? maSv = C.ReadLine();
            
            // 🚀 Xóa bằng Lambda/List.RemoveAll()
            int soLuongXoa = danhSachSv.RemoveAll(sv => sv.MaSV.Equals(maSv,StringComparison.OrdinalIgnoreCase));

            if(soLuongXoa > 0)
            {
                C.WriteLine($"\n✅ Đã xóa thành công {soLuongXoa} sinh viên có Mã SV '{maSv}'.");
                LuuFile();
            }
            else
            {
                throw new KeyNotFoundException($"Không tìm thấy sinh viên có Mã SV '{maSv}' để xóa.");
            }
            Thread.Sleep(1500);
        }

        // 4. Tìm sinh viên (Hàm mới)
        public static void TimSinhVien()
        {
            C.WriteLine("\n--- TÌM KIẾM SINH VIÊN ---");
            C.Write("Nhập tên (hoặc một phần tên) cần tìm: ");
            string tenCanTim = C.ReadLine() ?? string.Empty;

            // 🚀 Tìm kiếm bằng Lambda/LINQ .Where()
            var ketQua = danhSachSv
                .Where(sv => sv.HoTen.IndexOf(tenCanTim, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            HienThiDanhSach(ketQua, $"Kết quả tìm kiếm cho '{tenCanTim}'");
        }

        // 5. Sắp xếp sinh viên (Hàm mới)
        public static void SapXepSinhVien()
        {
            C.WriteLine("\n--- SẮP XẾP SINH VIÊN ---");
            C.WriteLine("1. Sắp xếp theo Điểm TB (Giảm dần)");
            C.WriteLine("2. Sắp xếp theo Tên (A-Z)");
            C.Write("Chọn tiêu chí: ");
            string tieuChi = C.ReadLine() ?? string.Empty;

            List<SinhVien> danhSachSapXep;
            if (tieuChi == "1")
            {
                // 🚀 Sắp xếp Giảm dần theo Điểm TB
                danhSachSapXep = danhSachSv.OrderByDescending(sv => sv.DiemTB).ToList();
                HienThiDanhSach(danhSachSapXep, "Danh sách sắp xếp theo Điểm TB (Giảm dần)");
            }
            else if (tieuChi == "2")
            {
                // 🚀 Sắp xếp Tăng dần theo Tên
                danhSachSapXep = danhSachSv.OrderBy(sv => sv.HoTen).ToList();
                HienThiDanhSach(danhSachSapXep, "Danh sách sắp xếp theo Tên (A-Z)");
            }
            else
            {
                C.WriteLine("Lựa chọn không hợp lệ.");
                Thread.Sleep(1500);
            }
        }
        
        // 6. Hiển thị danh sách (Hàm mới)
        public static void HienThiDanhSach()
        {
            HienThiDanhSach(danhSachSv, "DANH SÁCH TẤT CẢ SINH VIÊN");
        }

        public static void HienThiDanhSach(List<SinhVien> danhSach, string tieuDe)
        {
            C.WriteLine($"\n*** {tieuDe} ***");
            if (danhSach == null || danhSach.Count == 0)
            {
                C.WriteLine("Danh sách trống.");
                C.ReadKey();
                return;
            }

            // Định dạng hiển thị dạng bảng
            C.WriteLine(new string('-', 70));
            C.WriteLine($"{"Mã SV",-10} | {"Họ Tên",-30} | {"Tuổi",-5} | {"Điểm TB",-10}");
            C.WriteLine(new string('-', 70));

            foreach (var sv in danhSach)
            {
                C.WriteLine($"{sv.MaSV,-10} | {sv.HoTen,-30} | {sv.Tuoi,-5} | {sv.DiemTB,-10:F2}");
            }
            C.WriteLine(new string('-', 70));
            C.WriteLine($"Tổng số sinh viên: {danhSach.Count}");
            C.ReadKey(); 
        }
    }
}