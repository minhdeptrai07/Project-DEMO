using WebAPI_HoaDon.Entities;

namespace WebAPI_HoaDon.IServices
{
    public interface IHoaDonServices
    {
        public IQueryable<HoaDon> LayHoaDon(string keywords,int? year = null,
                                            int? month = null,
                                            DateTime? tuNgay = null,
                                            DateTime? denNgay = null,
                                            int? giaTu = null,
                                            int? giaDen = null);
        public HoaDon ThemHoaDon(HoaDon hoaDon);
        public string TaoMaGiaoDich();
        public HoaDon SuaHoaDon(int hoaDonId,HoaDon hoaDon);
        public void XoaHoaDon(int hoaDonId);
    }
}
