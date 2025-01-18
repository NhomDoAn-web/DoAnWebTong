    
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnWEBDEMO.ApplicationDB;
using X.PagedList;
using X.PagedList.Extensions;
using DoAnWEBDEMO.Models;
using System.Text.Json;
using System;
using System.Globalization;
using System.Text;
namespace DoAnWEBDEMO.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly ApplicationDb _context;

        public TrangChuController(ApplicationDb context)
        {
            _context = context ;
        }


        public IActionResult Index()
        {

            var sanPham_SlideShow = _context.SanPham
                                    .Where(sp => sp.SlideShow != null)
                                    .Take(4)
                                    .ToList();


            var SanPhamBanChay = from sp in _context.SanPham
                                 where sp.TrangThai == 1
                                 join ctdh in _context.ChiTietDonHang on sp.MaSP equals ctdh.MA_SP
                                 join dh in _context.DonHang on ctdh.MA_DH equals dh.MaDH
                                 where dh.TrangThai == 4 && dh.NgayDatHang.HasValue && dh.NgayDatHang.Value.Month == DateTime.Now.Month && dh.NgayDatHang.Value.Year == DateTime.Now.Year
                                 group new { sp, ctdh } by new
                                 {
                                     sp.MaSP,
                                     sp.HinhAnhSP,
                                     sp.MoTa,
                                     sp.TEN_SP,
                                     sp.Gia,
                                     sp.Slug
                                 } into g
                                 select new
                                 {
                                     MaSP = g.Key.MaSP,
                                     HinhAnhSP = g.Key.HinhAnhSP,
                                     MoTa = g.Key.MoTa,
                                     TEN_SP = g.Key.TEN_SP,
                                     Gia = g.Key.Gia,
                                     SoLuongBan = g.Sum(x => x.ctdh.SOLUONG),
                                     Slug = g.Key.Slug
                                 };
            ViewBag.SanPhamBanChay = SanPhamBanChay.OrderByDescending(sp => sp.SoLuongBan).Take(4).ToList();


            var sanPhamMoi = from sp in _context.SanPham
                             join km in _context.KhuyenMai on sp.MaSP equals km.SanPhamKhuyenMaiId into kmGroup
                             from km in kmGroup.DefaultIfEmpty()
                             where sp.TrangThai == 1 && sp.NgayRaMat.HasValue && (km == null || (DateTime.Now >= km.NgayBatDau && DateTime.Now <= km.NgayKetThuc))
                             select new
                             {
                                 MaSP = sp.MaSP,
                                 HinhAnhSP = sp.HinhAnhSP,
                                 MoTa = sp.MoTa,
                                 TEN_SP = sp.TEN_SP,
                                 Gia = sp.Gia,
                                 GiaSauKhiGiam = km != null ? sp.Gia - km.MucGiamGia : sp.Gia,
                                 LuotXem = sp.LuotXem,
                                 NgayRaMat = sp.NgayRaMat,
                                 Slug = sp.Slug
                             };
            ViewBag.SanPhamMoi = sanPhamMoi.OrderByDescending(sp => sp.NgayRaMat).Take(4).ToList();


            var dataDichVuCongTy = _context.DichVuCongTy
                       .Where(dv => dv.TrangThai == 1)
                       .OrderBy(dv => dv.ID)
                       .ToList();

            ViewBag.DichVuCongTy = dataDichVuCongTy;


            var sanPhamYeuThich = _context.SanPhamYeuThich.ToList();
            // Kiểm tra trạng thái đăng nhập
            var userId = GetLoggedInKhachHangId();
            bool trangThaiDangNhap = userId != null;

            var danhMuc = _context.DanhMuc.Where(dm => dm.Trang_Thai == 1).Take(7).ToList();

            ViewBag.DanhMuc = danhMuc;


            ViewBag.TrangThaiDangNhap = trangThaiDangNhap;




            ViewBag.SanPham = sanPhamMoi.Take(8).ToList();

            ViewBag.SanPham_SlideShow = sanPham_SlideShow;


            ViewBag.SanPhamYeuThich = sanPhamYeuThich;


            return View();
        }


        public IActionResult TimKiemSanPham(string? TenTimKiem, int? DanhMucId, decimal? GiaMin, decimal? GiaMax, string? SortOrder, int page = 1, int pageSize = 8)
        {
            var userId = GetLoggedInKhachHangId();
            bool trangThaiDangNhap = userId != null;
            ViewBag.TrangThaiDangNhap = trangThaiDangNhap;

            var query = from sp in _context.SanPham
                        join km in _context.KhuyenMai on sp.MaSP equals km.SanPhamKhuyenMaiId into kmGroup
                        from km in kmGroup.DefaultIfEmpty()
                        where sp.TrangThai == 1
                        select new SanPhamKhuyenMai
                        {
                            MaDanhMuc = sp.MaDanhMuc,
                            MoTa = sp.MoTa,
                            MaSP = sp.MaSP,
                            TEN_SP = sp.TEN_SP,
                            Gia = sp.Gia,
                            Slug = sp.Slug,
                            HinhAnhSP = sp.HinhAnhSP,
                            GiaSauKhiGiam = km != null && km.NgayBatDau <= DateTime.Now && km.NgayKetThuc >= DateTime.Now ? sp.Gia - km.MucGiamGia : sp.Gia
                        };
        
            var categories = _context.DanhMuc.ToList();
            ViewBag.DanhMuc = categories;
        
            // Lọc theo tên sản phẩm
            if (!string.IsNullOrEmpty(TenTimKiem))
            {
                var TenDaDuocMaHoa = MaHoaTenTimKiem(TenTimKiem.ToLower());
        
                query = query.AsEnumerable()
                    .Where(sp => MaHoaTenTimKiem(sp.TEN_SP.ToLower()).Contains(TenDaDuocMaHoa))
                    .AsQueryable();
            }
        
            // Lọc theo danh mục
            if (DanhMucId.HasValue && DanhMucId > 0)
            {
                query = query.Where(sp => sp.MaDanhMuc == DanhMucId);
            }
        
            // Lọc theo khoảng giá
            if (GiaMin.HasValue)
            {
                query = query.Where(sp => sp.GiaSauKhiGiam >= GiaMin);
            }
            if (GiaMax.HasValue)
            {
                query = query.Where(sp => sp.GiaSauKhiGiam <= GiaMax);
            }
        
            // Sắp xếp theo giá
            if (SortOrder == "asc")
            {
                query = query.OrderBy(sp => sp.GiaSauKhiGiam); // Tăng dần
            }
            else if (SortOrder == "desc")
            {
                query = query.OrderByDescending(sp => sp.GiaSauKhiGiam); // Giảm dần
            }
            else
            {
                query = query.OrderBy(sp => sp.TEN_SP); // Sắp xếp theo tên nếu không chọn sắp xếp giá
            }
        
            var pagedList = query.ToPagedList(page, pageSize);
        
            // Đặt lại ViewBag để duy trì các giá trị hiện tại
            ViewBag.CurrentSearchTerm = TenTimKiem;
            ViewBag.CurrentCategory = DanhMucId;
            ViewBag.CurrentPriceMin = GiaMin;
            ViewBag.CurrentPriceMax = GiaMax;
            ViewBag.CurrentSortOrder = SortOrder;
            var sanPhamYeuThich = _context.SanPhamYeuThich.ToList();
            ViewBag.SanPhamYeuThich = sanPhamYeuThich;

            return View(pagedList);
        }

        public static string MaHoaTenTimKiem(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        [HttpPost]
        public IActionResult ThemSanPhamYeuThich(int productId)
        {
            var userId = GetLoggedInKhachHangId(); // Lấy ID của khách hàng đã đăng nhập

            if (userId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thêm vào danh sách yêu thích." });
            }

            var existingItem = _context.SanPhamYeuThich
                .FirstOrDefault(x => x.KhachHangId == userId && x.SanPhamId == productId);

            if (existingItem != null)
            {
                _context.SanPhamYeuThich.Remove(existingItem);
                _context.SaveChanges();

                return Json(new { success = true, message = "Sản phẩm đã được xóa khỏi danh sách yêu thích.", newLikeCount = GetLikeCount(productId), isAdded = false });
            }

            var newWishlistItem = new SanPhamYeuThich
            {
                KhachHangId = userId.Value,
                SanPhamId = productId
            };

            _context.SanPhamYeuThich.Add(newWishlistItem);
            _context.SaveChanges();

            // Trả về số lượt yêu thích mới và trạng thái sản phẩm đã được thêm
            return Json(new { success = true, message = "Sản phẩm đã được thêm vào danh sách yêu thích!", newLikeCount = GetLikeCount(productId), isAdded = true });
        }

        private int GetLikeCount(int productId)
        {
            return _context.SanPhamYeuThich.Count(x => x.SanPhamId == productId);
        }

        public int? GetLoggedInKhachHangId()
        {
            var khachHangJson = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(khachHangJson))
            {
                return null;
            }

            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson); //*

            if(thongTinkhachHang != null)
            {
                var customer = _context.KhachHang.FirstOrDefault(kh => kh.MaKH == thongTinkhachHang.MaKH);

                if(customer != null)
                {
                    return customer.MaKH;
                }    
            }    


            return null; 
        }


    }
}
