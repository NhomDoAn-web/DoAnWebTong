    
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

            var sanPhamKhuyenMai = from sp in _context.SanPham
                                   join km in _context.KhuyenMai on sp.MaSP equals km.SanPhamKhuyenMaiId
                                   where DateTime.Now >= km.NgayBatDau && DateTime.Now <= km.NgayKetThuc && sp.TrangThai == 1
                                   select new
                                   {
                                       MaSP = sp.MaSP,
                                       HinhAnhSP = sp.HinhAnhSP,
                                       MoTa = sp.MoTa,
                                       TEN_SP = sp.TEN_SP,
                                       Gia = sp.Gia,
                                       GiaSauKhiGiam = sp.Gia - km.MucGiamGia
                                   };

            var sanPhamNoiBat = from sp in _context.SanPham
                                join km in _context.KhuyenMai on sp.MaSP equals km.SanPhamKhuyenMaiId into kmGroup
                                from km in kmGroup.DefaultIfEmpty()
                                where sp.TrangThai == 1 && (km == null || (DateTime.Now >= km.NgayBatDau && DateTime.Now <= km.NgayKetThuc))
                                select new
                                {
                                   MaSP = sp.MaSP,
                                   HinhAnhSP = sp.HinhAnhSP,
                                   MoTa = sp.MoTa,
                                   TEN_SP = sp.TEN_SP,
                                   Gia = sp.Gia,
                                   GiaSauKhiGiam = km != null ? sp.Gia - km.MucGiamGia : sp.Gia, 
                                   LuotXem = sp.LuotXem
                                };
            
                var sanPhamMoi =    _context.SanPham
                                    .Where(s => s.TrangThai == 1)
                                    .OrderByDescending(s => s.Gia)
                                    .Take(4)
                                    .ToList();


            ViewBag.SanPhamKhuyenMai = sanPhamKhuyenMai;
            ViewBag.SanPhamNoiBat = sanPhamNoiBat.OrderByDescending(sp => sp.LuotXem).Take(4).ToList();
            ViewBag.SanPham = sanPhamNoiBat.Take(8).ToList();
            ViewBag.SanPham_SlideShow = sanPham_SlideShow;
            return View();
        }


        public IActionResult TimKiemSanPham(string? TenTimKiem, int? DanhMucId, decimal? GiaMin, decimal? GiaMax, string? SortOrder, int page = 1, int pageSize = 8)
        {
            var query = _context.SanPham.Where(sp => sp.TrangThai == 1).AsQueryable();
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
                query = query.Where(sp => sp.Gia >= GiaMin);
            }
            if (GiaMax.HasValue)
            {
                query = query.Where(sp => sp.Gia <= GiaMax);
            }

            // Sắp xếp theo giá
            if (SortOrder == "asc")
            {
                query = query.OrderBy(sp => sp.Gia); // Tăng dần
            }
            else if (SortOrder == "desc")
            {
                query = query.OrderByDescending(sp => sp.Gia); // Giảm dần
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
            var userId = GetLoggedInKhachHangId(); 

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
            return Json(new { success = true, message = "Sản phẩm đã được thêm vào danh sách yêu thích!", newLikeCount = GetLikeCount(productId), isAdded = true });
        }

        // Hàm lấy số lượt yêu thích của sản phẩm
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
