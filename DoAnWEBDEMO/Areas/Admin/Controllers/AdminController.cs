using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;
using DoAnWEBDEMO.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
namespace OnTapLan1_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDb _db;

        public AdminController(ApplicationDb db)
        {
            _db = db;
        }

        public bool checkDangNhap()
        {
            var admin = HttpContext.Session.GetString("admin");

            if (admin != null)
            {
                return true;
            }
            return false;
        }


        public IActionResult Index()
        {
            if (!checkDangNhap())
            {
                return RedirectToAction("AdminDangNhap");
            }

            var tongSoLuongSanPhamTonKho = _db.SanPham
                                           .Include(sp => sp.MauSacs)
                                           .Sum(sp => sp.MauSacs.Sum(ms => ms.SoLuongTon_MS));

            var tongSoTaiKhoanKhachHang = _db.KhachHang.Count().ToString();

            var soLuongDonHangDuocTao = _db.DonHang
                                        .Where(dh => dh.NgayDatHang.HasValue && dh.NgayDatHang.Value.Month == DateTime.Now.Month)
                                        .Count().ToString();

            ViewBag.tongSoLuongSanPhamTonKho = tongSoLuongSanPhamTonKho;
            ViewBag.tongSoTaiKhoanKhachHang = tongSoTaiKhoanKhachHang;
            ViewBag.soLuongDonHangDuocTao = soLuongDonHangDuocTao;


            var dataHeader = _db.Header.ToList();
            ViewBag.duLieuHeader = dataHeader;

            var dataFooter = _db.Footer.ToList();
            ViewBag.duLieuFooter = dataFooter;

            var dataDichVuCongTy = _db.DichVuCongTy.ToList();
            ViewBag.duLieuDichVuCongTy = dataDichVuCongTy;





            return View();

        }

        public IActionResult AdminDangNhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminDangNhap(string user, string pass)
        {
            var TAIKHOANNHANVIEN = _db.NhanVien.FirstOrDefault(nv => nv.TENDANGNHAP == user);

            if (TAIKHOANNHANVIEN != null)
            {

                if (TAIKHOANNHANVIEN.MATKHAU == pass)
                {
                    HttpContext.Session.SetString("admin", JsonSerializer.Serialize(TAIKHOANNHANVIEN));
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public IActionResult AdminDangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("AdminDangNhap");
        }


        public IActionResult ThemFooter()
        {
            if (!checkDangNhap())
            {
                return RedirectToAction("AdminDangNhap");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ThemFooter(Footer footer)
        {
            if (footer != null)
            {
                footer.TrangThaiHienThi = 1;
                _db.Footer.Add(footer);
                _db.SaveChanges();
                TempData["ThanhCong"] = "Cập nhật dữ liệu thành công";
                return View();
            }
            TempData["ThatBai"] = "Cập nhật dữ liệu thất bại";

            return View();
        }

        public IActionResult SuaFooter(int ID)
        {
            if (!checkDangNhap())
            {
                return RedirectToAction("AdminDangNhap");
            }
            var data = _db.Footer.FirstOrDefault(f => f.ID == ID);

            if (data != null)
            {
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public IActionResult SuaFooter(Footer footer)
        {
            Debug.WriteLine("===============: " + footer.ID);
            if (footer != null)
            {
                _db.Footer.Update(footer);
                _db.SaveChanges();
                TempData["ThanhCong"] = "Cập nhật dữ liệu thành công";
            }
            else
            {
                TempData["ThatBai"] = "Cập nhật dữ liệu thất bại";
            }
            return View(footer);
        }

        [HttpPost]
        public JsonResult XoaFooter(int ID)
        {

            if (ID != null)
            {
                var data = _db.Footer.FirstOrDefault(ft => ft.ID == ID);
                if (data != null)
                {
                    _db.Footer.Remove(data);
                    _db.SaveChanges();
                    TempData["XoaFooterThanhCong"] = "Cập nhật dữ liệu thành công";
                    return Json(new { value = true });
                }
            }
            TempData["XoaFooterThatBai"] = "Cập nhật dữ liệu thất bại";
            return Json(new { value = false });

        }


        //Dịch vụ công ty
        public IActionResult ThemDichVu()
        {
            if (!checkDangNhap())
            {
                return RedirectToAction("AdminDangNhap");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ThemDichVu(DichVuCongTy dichVuCongTy)
        {
            if (dichVuCongTy != null)
            {
                dichVuCongTy.TrangThai = 1;
                _db.DichVuCongTy.Add(dichVuCongTy);
                _db.SaveChanges();
                TempData["ThanhCong"] = "Cập nhật dữ liệu thành công";
                return View();
            }
            TempData["ThatBai"] = "Cập nhật dữ liệu thất bại";
            return View();
        }

        public IActionResult SuaDichVu(int ID)
        {
            if (!checkDangNhap())
            {
                return RedirectToAction("AdminDangNhap");
            }
            if (ID != null)
            {
                var data = _db.DichVuCongTy.FirstOrDefault(dv => dv.ID == ID);
                return View(data);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult SuaDichVu(DichVuCongTy dichVuCongTy)
        {
            Debug.WriteLine("======= Hàm sửa Dịch vụ");
            if (dichVuCongTy != null)
            {
                _db.DichVuCongTy.Update(dichVuCongTy);
                _db.SaveChanges();
                TempData["ThanhCong"] = "Cập nhật dữ liệu thành công";
            }
            else
            {
                TempData["ThatBai"] = "Cập nhật dữ liệu thất bại";
            }
            return View(dichVuCongTy);
        }

        [HttpPost]
        public JsonResult XoaDichVu(int ID)
        {
            if (ID != null)
            {
                var data = _db.DichVuCongTy.FirstOrDefault(ft => ft.ID == ID);
                if (data != null)
                {
                    _db.DichVuCongTy.Remove(data);
                    _db.SaveChanges();
                    TempData["XoaDVThanhCong"] = "Cập nhật dữ liệu thành công";
                    return Json(new { value = true });
                }
            }
            TempData["XoaDVThatBai"] = "Cập nhật dữ liệu thất bại";
            return Json(new { value = false });

        }

    }
}
