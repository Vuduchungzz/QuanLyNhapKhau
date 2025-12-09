
namespace QuanLyKhoi.QuanLySinhVien
{
    // Cần là public hoặc internal, tùy thuộc vào cách bạn tổ chức
    public class SinhVien 
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public double DiemTB { get; set; }

        public SinhVien() { } // Cần cho Deserialization JSON
        
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