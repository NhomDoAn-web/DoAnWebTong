
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnWEBDEMO.ApplicationDB;
using X.PagedList;
using X.PagedList.Extensions;
using DoAnWEBDEMO.Models;
using System.Text.Json;
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
            var sanPhamKhuyenMai = _context.SanPham
                            .Where(s => s.TrangThai == 1)
                            .OrderByDescending(s => s.Gia)
                            .Take(4)
                            .ToList();

            var sanPhamNoiBat = _context.SanPham
                            .Where(s => s.TrangThai == 1)
                            .OrderByDescending(s => s.Gia)
                            .Take(4)
                            .ToList();

            var sanPham = _context.SanPham
                            .Where(s => s.TrangThai == 1)
                            .OrderByDescending(s => s.Gia)
                            .Take(8)
                            .ToList();

            ViewBag.SanPhamKhuyenMai = sanPhamKhuyenMai;
            ViewBag.SanPhamNoiBat = sanPhamNoiBat;
            ViewBag.SanPham = sanPham;

            return View();
        }


        public IActionResult TimKiemSanPham(string? TenTimKiem, int? DanhMucId, decimal? GiaMin, decimal? GiaMax, string? SortOrder, int page = 1, int pageSize = 2)
        {
            var query = _context.SanPham.AsQueryable();

            // Lọc theo tên sản phẩm
            if (!string.IsNullOrEmpty(TenTimKiem))
            {
                query = query.Where(sp => sp.TEN_SP.Contains(TenTimKiem));
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



        [HttpPost]
        public IActionResult ThemSanPhamYeuThich(int productId)
        {
            var userId = GetLoggedInKhachHangId();

            if (userId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thêm vào danh sách yêu thích." });
            }

            var existingItem = _context.SanPhamYeuThich.FirstOrDefault(x => x.KhachHangId == userId && x.SanPhamId == productId);

            if (existingItem != null)
            {
                return Json(new { success = false, message = "Sản phẩm đã có trong danh sách yêu thích của bạn." });
            }

            var newWishlistItem = new SanPhamYeuThich
            {
                KhachHangId = userId.Value,
                SanPhamId = productId
            };

            _context.SanPhamYeuThich.Add(newWishlistItem);
            _context.SaveChanges();

            return Json(new { success = true, message = "Sản phẩm đã được thêm vào danh sách yêu thích!" });
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
