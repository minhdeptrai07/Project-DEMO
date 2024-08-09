using System.ComponentModel.DataAnnotations;

namespace WebAPI_HoaDon.Entities
{
    public class SanPham
    {
    
        public int SanPhamId { get; set; }
        public int LoaiSanPhamId { get; set; }
        public string TenSanPham {  get; set; }
        public double GiaThanh {  get; set; }
        public string MoTa {  get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string KiHieuSanPham { get; set; }
        public virtual LoaiSanPham LoaiSanPham { get;  set; }
    }
}
