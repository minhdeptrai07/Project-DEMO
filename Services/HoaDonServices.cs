using Microsoft.EntityFrameworkCore;
using WebAPI_HoaDon.Entities;
using WebAPI_HoaDon.IServices;

namespace WebAPI_HoaDon.Services
{
    public class HoaDonServices : IHoaDonServices
    {
        private readonly AppDbContext appDbContext;

        public HoaDonServices()
        {
            appDbContext = new AppDbContext();
        }

        public IQueryable<HoaDon> LayHoaDon(string? keywords,
                                            int? year = null,
                                            int? month = null,
                                            DateTime? tuNgay = null,
                                            DateTime? denNgay = null,
                                            int? giaTu = null,
                                            int? giaDen = null)
        {
            var query = appDbContext.HoaDons.Include(x => x.ChiTietHoaDons)
                                            .OrderByDescending(x => x.ThoiGianTao).AsQueryable();

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(x => x.TenHoaDon.ToLower().Contains(keywords.ToLower())
                                 || x.MaGiaoDich.ToLower().Contains(keywords.ToLower()));

            }
            if (month.HasValue)
            {
                query = query.Where(x => x.ThoiGianTao.Month == month);
            }
            if (year.HasValue)
            {
                query = query.Where(x => x.ThoiGianTao.Year == year);
            }
            if (tuNgay.HasValue)
            {
                query = query.Where(x => x.ThoiGianTao.Date >= tuNgay.Value.Date);
            }
            if (denNgay.HasValue)
            {
                query = query.Where(x => x.ThoiGianTao.Date <= denNgay.Value.Date);
            }
            if (giaTu.HasValue)
            {
                query = query.Where(x => x.TongTien >= giaTu);
            }
            if (giaDen.HasValue)
            {
                query = query.Where(x => x.TongTien <= giaDen);
            }
            return query;
        }

        public HoaDon SuaHoaDon(int hoaDonId, HoaDon hoaDon)
        {
            if (appDbContext.HoaDons.Any(x => x.HoaDonId == hoaDonId))
            {
                using (var trans = appDbContext.Database.BeginTransaction())
                {
                    if (hoaDon.ChiTietHoaDons == null || hoaDon.ChiTietHoaDons.Count() == 0)
                    {
                        var lstCTHDHienTai = appDbContext.ChiTietHoaDons.Where(x => x.HoaDonId == hoaDon.HoaDonId);
                        {
                            appDbContext.RemoveRange(lstCTHDHienTai);
                            appDbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        var lstCTHDHienTai = appDbContext.ChiTietHoaDons.Where(x => x.HoaDonId == hoaDon.HoaDonId).ToList();
                        var lstDSHDDelete = new List<ChiTietHoaDon>();
                        foreach (var chiTiet in lstCTHDHienTai)
                        {
                            if (hoaDon.ChiTietHoaDons.Any(x => x.HoaDonId == chiTiet.HoaDonId))
                            {
                                lstDSHDDelete.Add(chiTiet);
                            }
                            else
                            {
                                var chiTietMoi = hoaDon.ChiTietHoaDons.FirstOrDefault(x => x.HoaDonId == chiTiet.HoaDonId);
                                chiTiet.SanPhamId = chiTietMoi.SanPhamId;
                                chiTiet.SoLuong = chiTietMoi.SoLuong;
                                chiTiet.DVT = chiTietMoi.DVT;
                                var sanPham = appDbContext.SanPhams.FirstOrDefault(z => z.SanPhamId == chiTietMoi.SanPhamId);
                                chiTiet.ThanhTien = sanPham.GiaThanh * chiTietMoi.SoLuong;
                                appDbContext.Add(chiTiet);
                                appDbContext.SaveChanges();
                            }
                        }
                        appDbContext.RemoveRange(lstDSHDDelete);
                        appDbContext.SaveChanges();
                        foreach (var chiTiet in hoaDon.ChiTietHoaDons)
                        {
                            if (!lstCTHDHienTai.Any(x => x.HoaDonId == chiTiet.HoaDonId))
                            {
                                chiTiet.HoaDonId = hoaDon.HoaDonId;
                            }
                        }

                    }
                    var tongTienMoi = appDbContext.ChiTietHoaDons.Where(x => x.HoaDonId == hoaDon.HoaDonId).Sum(x => x.ThanhTien);
                    hoaDon.TongTien = tongTienMoi;
                    hoaDon.ThoiGianCapNhat = DateTime.Now;
                    hoaDon.ChiTietHoaDons = null;
                    appDbContext.Update(hoaDon);
                    appDbContext.SaveChanges();
                    trans.Commit();
                    return hoaDon;
                }
            }
            else throw new Exception($"hoa don {hoaDonId}khong ton tai");

        }

        public string TaoMaGiaoDich()
        {
            var res = DateTime.Now.ToString("yyyyMMdd") + "_";
            var countSoGiaoDichNgayHomNay = appDbContext.HoaDons.Count(x => x.ThoiGianTao.Date == DateTime.Now.Date);
            if (countSoGiaoDichNgayHomNay > 0)
            {
                int tmp = countSoGiaoDichNgayHomNay + 1;
                if (tmp < 10) return res + "00" + tmp.ToString();
                else if (tmp < 100) return res + "0" + tmp.ToString();
                else return res + tmp.ToString();

            }
            else return res + "001";
        }

        public HoaDon ThemHoaDon(HoaDon hoaDon)
        {
            using (var trans = appDbContext.Database.BeginTransaction())
            {
                hoaDon.ThoiGianTao = DateTime.Now;
                hoaDon.MaGiaoDich = TaoMaGiaoDich();
                var lstChiTietHoaDon = hoaDon.ChiTietHoaDons;
                hoaDon.ChiTietHoaDons = null;
                appDbContext.Add(hoaDon);
                appDbContext.SaveChanges();
                foreach (var chiTiet in lstChiTietHoaDon)
                {
                    if (appDbContext.SanPhams.Any(x => x.SanPhamId == chiTiet.SanPhamId))
                    {

                        chiTiet.HoaDonId = hoaDon.HoaDonId;
                        var sanPham = appDbContext.SanPhams.FirstOrDefault(x => x.SanPhamId == chiTiet.SanPhamId);
                        chiTiet.ThanhTien = chiTiet.SoLuong * sanPham.GiaThanh;
                        appDbContext.Add(chiTiet);
                        appDbContext.SaveChanges();
                    }
                    else throw new Exception($"san pham khong ton tai.vui long kiem tra lai san pham co ID la{chiTiet.SanPhamId}");
                }
                hoaDon.TongTien = lstChiTietHoaDon.Sum(x => x.ThanhTien);
                appDbContext.SaveChanges();
                trans.Commit();
                return hoaDon;
            }
        }

        public void XoaHoaDon(int hoaDonId)
        {
            using (var trans = appDbContext.Database.BeginTransaction())
            {
                var lstCTHDHienTai = appDbContext.ChiTietHoaDons.Where(x => x.HoaDonId == hoaDonId);
                appDbContext.RemoveRange(lstCTHDHienTai);
                appDbContext.SaveChanges();
                var hoaDon = appDbContext.HoaDons.Find(hoaDonId);
                appDbContext.Remove(hoaDon);
                appDbContext.SaveChanges();
                trans.Commit();
            }

        }
    }
}
