
using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;
using System.Text.Json;
using X.PagedList.Extensions;

namespace DoAnWEBDEMO.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDb _db;

        public GioHangController(ApplicationDb db)
        {
            _db = db;
        }
        public IActionResult Index(string? name, int? page)
        {
            int pageSize = 5; // Số lượng sản phẩm mỗi trang
            int pageNumber = (page ?? 1); // Mặc định trang đầu tiên nếu không có tham số trang

            var KH_JSON = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(KH_JSON))
            {
                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
                Debug.WriteLine(thongTinkhachHang);
                var result = from ctgh in _db.ChiTietGioHang
                             join sp in _db.SanPham on ctgh.MaSP equals sp.MaSP
                             join m in _db.MauSac on ctgh.MaMau equals m.ID_MauSac
                             where ctgh.MaKH == Convert.ToInt32(thongTinkhachHang.MaKH)
                             select new
                             {
                                 MaSP = sp.MaSP,
                                 TenSP = sp.TEN_SP,
                                 Gia = sp.Gia,
                                 HinhAnh = m.HinhAnhSP_MauSac,
                                 SoLuong = ctgh.Soluong,
                                 Mau = m.TenMauSac,
                                 MaMau = m.ID_MauSac,
                                 TongTien = sp.Gia * ctgh.Soluong
                             };

                var pagedList = result.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang
                if (result.Count() > 0)
                {
                    // Truyền dữ liệu phân trang qua ViewBag
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.TotalPages = pagedList.PageCount;
                    ViewBag.TongTien = result.Sum(e => e.TongTien);
                    ViewBag.ChiTietGioHang = pagedList;
                }
                else
                {
                    ViewBag.ChiTietGioHang = null;
                }
            }
            return View();
        }
        public IActionResult ThanhToan()
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(KH_JSON))
            {
                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
                Debug.WriteLine(thongTinkhachHang);
                var result = from ctgh in _db.ChiTietGioHang
                             join sp in _db.SanPham on ctgh.MaSP equals sp.MaSP
                             join m in _db.MauSac on ctgh.MaMau equals m.ID_MauSac
                             where ctgh.MaKH == Convert.ToInt32(thongTinkhachHang.MaKH)
                             select new
                             {
                                 MaSP = sp.MaSP,
                                 TenSP = sp.TEN_SP,
                                 Gia = sp.Gia,
                                 HinhAnh = m.HinhAnhSP_MauSac,
                                 SoLuong = ctgh.Soluong,
                                 Mau = m.TenMauSac,
                                 TongTien = sp.Gia * ctgh.Soluong
                             };
                ViewBag.Total = result.Sum(e => e.TongTien);
                ViewBag.ThanhToan = result;
            }
            return View();
        }
        public int? GetLoggedInKhachHangId()
        {
            var userName = HttpContext.Session.GetString("user");
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(userName);

            if (thongTinkhachHang.MaKH == null)
            {
                return null;
            }
            var customer = _db.KhachHang.FirstOrDefault(kh => kh.MaKH == thongTinkhachHang.MaKH);

            if (customer == null)
            {
                return null;
            }

            return customer.MaKH;
        }
        [HttpPost]
        public IActionResult TangSoLuong(int maSP, int maMau)
        {
            var sanpham = _db.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == maSP && sp.MaMau == maMau);
            if (sanpham != null)
            {
                sanpham.Soluong += 1;
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult GiamSoLuong(int maSP, int maMau)
        {
            var sanpham = _db.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == maSP && sp.MaMau == maMau);
            if (sanpham != null)
            {
                sanpham.Soluong -= 1;
                if (sanpham.Soluong <= 0)
                {
                    _db.ChiTietGioHang.Remove(sanpham);
                }
                else
                {
                    _db.ChiTietGioHang.Update(sanpham);
                }
                _db.SaveChanges();
                return Json(new { success = true });

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult XoaKhoiGioHang(int maSP)
        {
            var sanpham = _db.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == maSP);
            if (sanpham != null)
            {
                Debug.WriteLine("Xoa san pham khoi gio hang" + sanpham.MaKH);
                _db.Remove(sanpham);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ThemVaoGioHang(int maSP, int soluong, int mau)
        {

            var KH_JSON = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(KH_JSON))
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập!" });
            }
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
            if (thongTinkhachHang.MaKH == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập!" });
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng với cùng mã sản phẩm và mã màu
            var existingItem = _db.ChiTietGioHang.FirstOrDefault(x => x.MaKH == thongTinkhachHang.MaKH && x.MaSP == maSP && x.MaMau == mau);

            if (existingItem != null)
            {
                return Json(new { success = false, message = "Sản phẩm đã có trong giỏ hàng!" });
            }
            else
            {
                // Nếu sản phẩm chưa tồn tại với màu này, thêm sản phẩm mới
                var newWishlistItem = new ChiTietGioHang
                {
                    MaKH = thongTinkhachHang.MaKH,
                    MaSP = maSP,
                    MaMau = mau,
                    Soluong = soluong
                };

                _db.ChiTietGioHang.Add(newWishlistItem);
                _db.SaveChanges();

                return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng!" });
            }
        }
        [HttpPost]
        public IActionResult XoaToanBoGioHang()
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
            if (thongTinkhachHang.MaKH != null)
            {

                var gioHangItems = _db.ChiTietGioHang.Where(p => p.MaKH == thongTinkhachHang.MaKH);
                _db.ChiTietGioHang.RemoveRange(gioHangItems);
                _db.SaveChanges();

                return Json(new { success = true, message = "Đã xóa toàn bộ giỏ hàng." });
            }

            return Json(new { success = false, message = "Không thể xóa giỏ hàng." });
        }
        [HttpPost]
        public IActionResult XacNhanTT()
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);

            if (thongTinkhachHang?.MaKH != null)
            {
                var ctgh = LayChiTietGioHang(thongTinkhachHang.MaKH);

                // Kiểm tra số lượng tồn
                var errorMessage = KiemTraSoLuongTon(ctgh);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    TempData["ErrorMessage"] = errorMessage;
                    return RedirectToAction("Index");
                }

                // Tạo hóa đơn
                TaoHoaDon(thongTinkhachHang, ctgh);

                // Thêm chi tiết hóa đơn 
                ThemChiTietHoaDon(ctgh);
                // Giảm số lượng tồn
                GiamSoLuongTon(ctgh);
                // Xóa giỏ hàng
                XoaGioHang(thongTinkhachHang.MaKH);

                TempData["SuccessMessage"] = "Thanh toán thành công!";
                return RedirectToAction("Index", "TrangChu");
            }

            return RedirectToAction("Index", "TrangChu");
        }

        private List<ChiTietGioHang> LayChiTietGioHang(int maKH)
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
            return _db.ChiTietGioHang.Include(c=>c.SanPham)
                .Include(c=>c.MauSac)
                .Where(c=>c.MaKH == maKH).ToList();
                   
        }

        private string KiemTraSoLuongTon(List<ChiTietGioHang>ctgh)
        {
            foreach (var item in ctgh)
            {
                if (item.Soluong > item.MauSac.SoLuongTon_MS)
                {
                    return $"Sản phẩm {item.SanPham.TEN_SP} {item.MauSac.TenMauSac} không đủ số lượng tồn.";
                }
            }
            return null;
        }

        private DonHang TaoHoaDon(KhachHang thongTinkhachHang,List<ChiTietGioHang>ctgh)
        {
            decimal tongtien = ctgh.Sum(e => (decimal)(e.Soluong*e.SanPham.Gia));
            var hoaDon = new DonHang
            {
                MaKH = thongTinkhachHang.MaKH,
                NgayDatHang = DateTime.Now,
                TongTienDonHang = tongtien,
                DiaChiNhanHang = thongTinkhachHang.DiaChi,
                SoDienThoai = thongTinkhachHang.SDT,
                TrangThai = 1
            };

            _db.DonHang.Add(hoaDon);
            _db.SaveChanges();

            return hoaDon;
        }

        private void ThemChiTietHoaDon( List<ChiTietGioHang> ctgh)
        {

            if (ctgh == null || !ctgh.Any())
            {
                Debug.WriteLine("The list ctgh is null or empty.");
            }
            foreach (ChiTietGioHang item in ctgh)
            {
                ChiTietDonHang chitietdonhang = new ChiTietDonHang
                {
                    MA_DH = _db.DonHang.Max(dh=>dh.MaDH),
                    MA_SP = (int)item.MaSP,
                    SOLUONG = (int)item.Soluong,
                    MA_MAU = (int)item.MaMau,
                    TONGTIENTUNGSANPHAM = (decimal)(item.Soluong*item.SanPham.Gia)
                };  
                    _db.ChiTietDonHang.Add(chitietdonhang);
            }

            _db.SaveChanges();
        }
        private void GiamSoLuongTon(List<ChiTietGioHang> ctgh)
        {
            foreach (ChiTietGioHang item in ctgh)
            {
                
                var sanPham = _db.MauSac.FirstOrDefault(m => m.MaSP == item.MaSP && m.ID_MauSac == item.MaMau);
                if (sanPham != null)
                {
                    sanPham.SoLuongTon_MS -= (int)item.Soluong;
                }
            }
            _db.SaveChanges();
        }
        private void XoaGioHang(int maKH)
        {
            var chiTietGioHangs = _db.ChiTietGioHang.Where(ct => ct.MaKH == maKH);
            _db.ChiTietGioHang.RemoveRange(chiTietGioHangs);
            _db.SaveChanges();
        }

    }
}

