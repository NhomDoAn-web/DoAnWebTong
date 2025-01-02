using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEBDEMO.SeedData
{
    public class SeedData
    {
        public static void SeedingData(ApplicationDb _db)
        {
            _db.Database.Migrate();

            //Nếu chưa có thì thêm
            if (!_db.SanPham.Any())
            {
                //Dữ liệu Danh Mục
                var danhMucs = new List<DanhMuc>
                {
                    new DanhMuc { TenDM = "Iphone", Trang_Thai = 1 },
                    new DanhMuc { TenDM = "Samsung", Trang_Thai = 1 },
                    new DanhMuc { TenDM = "Xiaomi", Trang_Thai = 1 },
                    new DanhMuc { TenDM = "Oppo", Trang_Thai = 1 },
                };

                _db.DanhMuc.AddRange(danhMucs);
                _db.SaveChanges();

                //Dữ liệu Nhà cung cấp
                var nhaCungCaps = new List<NhaCungCap>
                {
                    new NhaCungCap
                    {
                        TenNCC = "Công ty Cổ Phần Remax Việt Nam",
                        SDT = "+1800123456",
                        Email = "ctcpRemaxVN@gmail.com"
                    },
                    new NhaCungCap
                    {
                        TenNCC = "Công Ty TNHH Tin Học Kim Thiên Bảo",
                        SDT = "+1800987654",
                        Email = "ctcpTH_KTBao@gmail.com"
                    }
                };
                _db.NhaCungCap.AddRange(nhaCungCaps);
                _db.SaveChanges();

                //Dữ liệu Sản phẩm
                var sanPhams = new List<SanPham>
                {
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 1,
                        TEN_SP = "iPhone 14",
                        HinhAnhSP = "iphone_14.jpg",
                        MoTa = "iPhone 14 với thiết kế hiện đại và hiệu năng vượt trội.",
                        MauSP = "Đen",
                        TGianBaoHanh = 12,
                        TrangThai = 1,
                        Slug = "iphone-14",
                        KichThuoc_ManHinh = "6.1 inch",
                        CameraSau = "12MP",
                        Camera_Truoc = "12MP",
                        Chipset = "A15 Bionic",
                        Gpu = "Apple GPU",
                        Dung_Luong_Ram = "6GB",
                        Bo_Nho_Trong = "128GB",
                        Pin = "3279mAh",
                        HeDieuHanh = "iOS 16",
                        TanSoQuet = "120Hz"
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 2,
                        TEN_SP = "Samsung Galaxy S23",
                        HinhAnhSP = "samsung_s23.jpg",
                        MoTa = "Samsung Galaxy S23 với màn hình Dynamic AMOLED 2X.",
                        MauSP = "Trắng",
                        TGianBaoHanh = 12,
                        TrangThai = 1,
                        Slug = "samsung-galaxy-s23",
                        KichThuoc_ManHinh = "6.1 inch",
                        CameraSau = "50MP + 12MP + 10MP",
                        Camera_Truoc = "12MP",
                        Chipset = "Snapdragon 8 Gen 2",
                        Gpu = "Adreno 740",
                        Dung_Luong_Ram = "8GB",
                        Bo_Nho_Trong = "256GB",
                        Pin = "3900mAh",
                        HeDieuHanh = "Android 13",
                        TanSoQuet = "120Hz"
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 3,
                        TEN_SP = "Xiaomi 13",
                        HinhAnhSP = "xiaomi_13.jpg",
                        MoTa = "Xiaomi 13 với thiết kế thanh lịch và hiệu năng mạnh mẽ.",
                        MauSP = "Xanh",
                        TGianBaoHanh = 12,
                        TrangThai = 1,
                        Slug = "xiaomi-13",
                        KichThuoc_ManHinh = "6.36 inch",
                        CameraSau = "50MP + 10MP + 12MP",
                        Camera_Truoc = "32MP",
                        Chipset = "Snapdragon 8 Gen 2",
                        Gpu = "Adreno 740",
                        Dung_Luong_Ram = "12GB",
                        Bo_Nho_Trong = "256GB",
                        Pin = "4500mAh",
                        HeDieuHanh = "MIUI 14",
                        TanSoQuet = "120Hz"
                    },
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 4,
                        TEN_SP = "Oppo Find X6",
                        HinhAnhSP = "oppo_find_x6.jpg",
                        MoTa = "Oppo Find X6 với công nghệ camera tiên tiến.",
                        MauSP = "Hồng",
                        TGianBaoHanh = 12,
                        TrangThai = 1,
                        Slug = "oppo-find-x6",
                        KichThuoc_ManHinh = "6.8 inch",
                        CameraSau = "50MP + 50MP + 13MP",
                        Camera_Truoc = "32MP",
                        Chipset = "Dimensity 9200",
                        Gpu = "Mali-G715",
                        Dung_Luong_Ram = "16GB",
                        Bo_Nho_Trong = "512GB",
                        Pin = "4800mAh",
                        HeDieuHanh = "ColorOS 13",
                        TanSoQuet = "120Hz"
                    }

                };
                _db.SanPham.AddRange(sanPhams);
                _db.SaveChanges();
            }

            //Dữ liệu Khách hàng
            if (!_db.KhachHang.Any())
            {
                var khachHangs = new List<KhachHang>
                {
                     new KhachHang
                    {
                        HoKH = "Trịnh",
                        TenKH = "Trần Phương Tuấn Bỏ Con",
                        GioiTinh = "Nam",
                        Email = "ttptuan_bocon@example.com",
                        SDT = "0123456789",
                        DiaChi = "123 Đường ABC, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "ttptuan",
                        MATKHAU = "Ttptuan@123"
                    },
                    new KhachHang
                    {
                        HoKH = "Trương",
                        TenKH = "Mỹ Lan",
                        GioiTinh = "Nữ",
                        Email = "truongmylan@example.com",
                        SDT = "0123456789",
                        DiaChi = "456 Đường XYZ, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "truongmylan",
                        MATKHAU = "Truongmylan@123"
                    },
                    new KhachHang
                    {
                        HoKH = "Nguyễn",
                        TenKH = "Văn An",
                        GioiTinh = "Khác",
                        Email = "nvan@example.com",
                        SDT = "0123456789",
                        DiaChi = "789 Đường LMN, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "nvanan",
                        MATKHAU = "Nvanan@123"
                    }
                };

                _db.KhachHang.AddRange(khachHangs);
                _db.SaveChanges();
            }

            //Dữ liệu Chi tiết bình luận
            if (!_db.CHI_TIET_BINH_LUAN.Any())
            {
                var chiTietBinhLuans = new List<CHI_TIET_BINH_LUAN>
                {
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 1,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Sản phẩm tuyệt vời, rất hài lòng!",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 2,
                        MA_KH = 2,
                        SO_SAO = 4,
                        NOI_DUNG = "Sản phẩm tốt nhưng cần cải thiện chất lượng camera.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 1,
                        MA_KH = 3,
                        SO_SAO = 3,
                        NOI_DUNG = "Giá hơi cao so với tính năng.",
                        NGAY = DateTime.Now
                    }
                };

                _db.CHI_TIET_BINH_LUAN.AddRange(chiTietBinhLuans);
                _db.SaveChanges();
            }

            //Dữ liệu Nhân Viên
            if (!_db.NhanVien.Any())
            {
                var nhanViens = new List<NhanVien>
                {
                    new NhanVien
                    {
                        
                        TENNV = "Nguyen Van A",
                        SDT = "0123456789",
                        EMAIL = "nguyenvana@example.com",
                        TENDANGNHAP = "nv_a",
                        MATKHAU = "Nva@123456",
                        TRANGTHAI = 1
                    },
                    new NhanVien
                    {
                        
                        TENNV = "Tran Thi B",
                        SDT = "0123456789",
                        EMAIL = "tranthib@example.com",
                        TENDANGNHAP = "nv_b",
                        MATKHAU = "TtB@123456",
                        TRANGTHAI = 1
                    },
                    new NhanVien
                    {
                        
                        TENNV = "Le Van C",
                        SDT = "0123456789",
                        EMAIL = "levanc@example.com",
                        TENDANGNHAP = "nv_c",
                        MATKHAU = "Lvc@123456",
                        TRANGTHAI = 0
                    }
                };

                _db.NhanVien.AddRange(nhanViens);
                _db.SaveChanges();
            }

            //Dữ liệu Đơn hàng
            if (!_db.DonHang.Any())
            {
                var donHangs = new List<DonHang>
                {
                    new DonHang
                    {
                        MaKH = 1,
                        MaNVXL = 1,
                        NgayDatHang = "2025-01-01",
                        TongTienDonHang = 5000000,
                        TrangThai = 1,
                        DiaChiNhanHang = "123 Đường ABC, Quận 1, TP.HCM",
                        SoDienThoai = "0987654321"
                    },
                    new DonHang
                    {
                        MaKH = 2,
                        MaNVXL = 2,
                        NgayDatHang = "2025-01-02",
                        TongTienDonHang = 1500000,
                        TrangThai = 2,
                        DiaChiNhanHang = "456 Đường DEF, Quận 3, TP.HCM",
                        SoDienThoai = "0976543210"
                    },
                    new DonHang
                    {
                        MaKH = 3,
                        MaNVXL = 1,
                        NgayDatHang = "2025-01-03",
                        TongTienDonHang = 2000000,
                        TrangThai = 3,
                        DiaChiNhanHang = "789 Đường GHI, Quận 7, TP.HCM",
                        SoDienThoai = "0965432109"
                    }
                };

                _db.DonHang.AddRange(donHangs);
                _db.SaveChanges();
            }

            //Dữ liệu Chi Tiết Đơn hàng
            if (!_db.CHI_TIET_DON_HANG.Any())
            {
                var chiTietDonHangs = new List<CHI_TIET_DON_HANG>
                {
                    new CHI_TIET_DON_HANG
                    {
                        MA_DH = 1, // Phải khớp với `MaDH` trong bảng DonHang
                        MA_SP = 1, // Phải khớp với `MaSP` trong bảng SanPham
                        SOLUONG = 2,
                        TONGTIENTUNGSANPHAM = 2000000 // Giá 1 sản phẩm là 1.000.000
                    },
                    new CHI_TIET_DON_HANG
                    {
                        MA_DH = 1,
                        MA_SP = 2,
                        SOLUONG = 1,
                        TONGTIENTUNGSANPHAM = 3000000 // Giá 1 sản phẩm là 3.000.000
                    },
                    new CHI_TIET_DON_HANG
                    {
                        MA_DH = 2,
                        MA_SP = 3,
                        SOLUONG = 3,
                        TONGTIENTUNGSANPHAM = 4500000 // Giá 1 sản phẩm là 1.500.000
                    },
                    new CHI_TIET_DON_HANG
                    {
                        MA_DH = 3,
                        MA_SP = 4,
                        SOLUONG = 1,
                        TONGTIENTUNGSANPHAM = 2000000 // Giá 1 sản phẩm là 2.000.000
                    }
                };

                _db.CHI_TIET_DON_HANG.AddRange(chiTietDonHangs);
                _db.SaveChanges();
            }

            //Dữ liệu Liên Hệ
            if (!_db.LienHe.Any())
            {
                var lienHes = new List<LienHe>
                {
                    new LienHe
                    {
                        MA_NVXL = 1, 
                        HO_TEN = "Oggy",
                        EMAIL = "oggy@example.com",
                        SDT = "0912345678",
                        NOI_DUNG = "Yêu cầu hỗ trợ bảo hành sản phẩm.",
                        THOI_GIAN_GUI = DateTime.Now.AddDays(-5),
                        TRANG_THAI = 1 // Đang xử lý
                    },
                    new LienHe
                    {
                        MA_NVXL = 2,
                        HO_TEN = "Jack",
                        EMAIL = "jack@example.com",
                        SDT = "0987654321",
                        NOI_DUNG = "Hỏi về chính sách đổi trả sản phẩm.",
                        THOI_GIAN_GUI = DateTime.Now.AddDays(-3),
                        TRANG_THAI = 2 // Đã xử lý
                    },
                    new LienHe
                    {
                        MA_NVXL = 1,
                        HO_TEN = "Wenda",
                        EMAIL = "wenda@example.com",
                        SDT = "0933456789",
                        NOI_DUNG = "Khiếu nại về giao hàng. Lâu quá",
                        THOI_GIAN_GUI = DateTime.Now.AddDays(-1),
                        TRANG_THAI = 1 
                    }
                };

                _db.LienHe.AddRange(lienHes);
                _db.SaveChanges();
            }

        }
    }
}