namespace WebAPI_HoaDon.Entities
{
    public class ChiTietHoaDon
    {
        public int ChiTietHoaDonId { get; set; }
        public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }
        public int SoLuong {  get; set; }
        public string DVT {  get; set; }
        public double? ThanhTien { get; set; }
        public virtual HoaDon HoaDon { get; set; }
        public virtual SanPham SanPham { get; set; }
        
    }
}
