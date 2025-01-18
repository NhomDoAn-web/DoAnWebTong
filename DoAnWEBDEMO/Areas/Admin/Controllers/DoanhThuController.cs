using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using X.PagedList.Extensions;
using ClosedXML.Excel;
using System.IO;

namespace DoAnWEBDEMO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoanhThuController : Controller
    {
        private readonly ApplicationDb _context;

        public DoanhThuController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult DoanhThu(DateTime? Ngay, int? NamDoanhThu)     
        
        {
            // Xử lý giá trị mặc định nếu tham số truyền vào là null
            if (Ngay == null)
            {
                Ngay = DateTime.Today;
            }
            if (NamDoanhThu == null)
            {
                NamDoanhThu ??= DateTime.Now.Year;
            }
           

            // Lấy danh sách đơn hàng trong ngày
            var donHangHomNay = _context.DonHang
                .Where(dh => dh.NgayDatHang.HasValue && dh.NgayDatHang.Value.Date == Ngay.Value.Date ).ToList();

            // Lọc danh sách các mã đơn hàng có trạng thái thành công
            var maDonHangThanhCong = donHangHomNay
                .Where(dh => dh.TrangThai == 4 && dh.NgayDatHang.Value.Date == Ngay.Value.Date)
                .Select(dh => dh.MaDH)
                .ToList(); 

            // Tính tổng số sản phẩm từ các mã đơn hàng đã lọc
            var tongSanPhamBanHomNay = _context.ChiTietDonHang
                .Where(ctdh => maDonHangThanhCong.Contains(ctdh.MA_DH))
                .Sum(ctdh => ctdh.SOLUONG);

            // Tính tổng doanh thu trong ngày
            var tongDoanhThuHomNay = donHangHomNay.Where(dh => dh.TrangThai == 4).Sum(dh => dh.TongTienDonHang);

            // Thống kê đơn hàng
            var donHangThanhCong = donHangHomNay.Count(dh => dh.TrangThai == 4); 
            var donHangDaHuy = donHangHomNay.Count(dh => dh.TrangThai == 3);

            // Lấy danh sách đơn hàng trong năm đã chọn
            var donHangTheoNam = _context.DonHang
                .Where(dh => dh.NgayDatHang.HasValue && dh.NgayDatHang.Value.Year == NamDoanhThu)
                .ToList();
            // Tính doanh thu cho mỗi tháng trong năm
            var doanhThuTheoThang = new int[12]; // Mảng lưu doanh thu từng tháng (12 tháng)
            foreach (var donHang in donHangTheoNam)
            {
                if (donHang.TrangThai == 4) // Chỉ tính đơn hàng thành công (TrangThai == 4)
                {
                    int thang = donHang.NgayDatHang.Value.Month - 1; // Chuyển tháng từ 1-12 thành chỉ số mảng 0-11
                    doanhThuTheoThang[thang] += (int)(donHang.TongTienDonHang ?? 0m);
                }
            }

            // Trả dữ liệu doanh thu theo tháng vào ViewBag
            ViewBag.DoanhThuTheoThang = doanhThuTheoThang;
            // Đưa dữ liệu vào ViewBag để hiển thị
            ViewBag.TongSanPhamBanHomNay = tongSanPhamBanHomNay;
            ViewBag.TongDoanhThuHomNay = tongDoanhThuHomNay;
            ViewBag.DonHangThanhCong = donHangThanhCong;
            ViewBag.DonHangDaHuy = donHangDaHuy;
            ViewBag.NgayChon = Ngay.Value.ToString("yyyy-MM-dd");

            ViewBag.NamHienTai = NamDoanhThu;

            return View();
        }


    }
}
