// File: SinhVien.cs
using System;

namespace QuanLyKhoi.QuanLySinhVien
{
    // Đã sửa lại là public class để dễ truy cập hơn.
    public class SinhVien 
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public double DiemTB { get; set; }

        // 💡 BẮT BUỘC: Constructor mặc định cho JSON Deserialization
        public SinhVien() { }
        
        public SinhVien(string maSv, string hoTen, int tuoi, double diemTb)
        {
            MaSV = maSv; HoTen = hoTen; Tuoi = tuoi; DiemTB = diemTb;
        }

        public override string ToString()
        {
            return $"Mã SV: {MaSV}, Tên: {HoTen}, Tuổi: {Tuoi}, Điểm TB: {DiemTB:F2}";
        }
    }
}