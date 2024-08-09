using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_HoaDon.Entities;
using WebAPI_HoaDon.Helper;
using WebAPI_HoaDon.IServices;
using WebAPI_HoaDon.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI_HoaDon.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonServices hoaDonServices;
        public HoaDonController()
        {
            hoaDonServices = new HoaDonServices();
        }
        [HttpGet]
        public IActionResult LayHoaDon([FromQuery] string? keywords,
                                       [FromQuery] int? year = null,
                                       [FromQuery] int? month = null,
                                       [FromQuery] DateTime? tuNgay = null,
                                       [FromQuery] DateTime? denNgay = null,
                                       [FromQuery] int? giaTu = null,
                                       [FromQuery] int? giaDen = null,
                                       [FromQuery] Pagination pagination = null)
        {
            var query = hoaDonServices.LayHoaDon(keywords, year, month, tuNgay, denNgay, giaTu, giaDen);
            var hoaDons = PageResult<HoaDon>.ToPageResult(pagination, query);
            pagination.TotalCount = query.Count();
            var res = new PageResult<HoaDon>(pagination, hoaDons);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult ThemHoaDon(HoaDon hoaDon)
        {
            var res = hoaDonServices.ThemHoaDon(hoaDon);
            return Ok(res);
        }
        [HttpPut("{hoaDonId}")]
        public IActionResult SuaHoaDon(int hoaDonId, HoaDon hoaDon)
        {
            var res = hoaDonServices.SuaHoaDon(hoaDonId, hoaDon);
            return Ok(res);
        }
        [HttpDelete("{hoaDonId}")]
        public IActionResult XoaHoaDon(int hoaDonId)
        {
            hoaDonServices.XoaHoaDon(hoaDonId);
            return Ok();
        }
    }
}
