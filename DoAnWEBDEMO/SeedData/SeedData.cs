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
                        SlideShow = "slide_1.jpg",
                        MoTa = "iPhone 14 với thiết kế hiện đại và hiệu năng vượt trội. Sản phẩm được trang bị chip A15 Bionic mạnh mẽ, camera kép 12MP sắc nét, và thời lượng pin ổn định. Với màn hình 6.1 inch Liquid Retina, iPhone 14 mang lại trải nghiệm tuyệt vời cho người dùng yêu thích sự nhỏ gọn nhưng không kém phần sang trọng.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 9, 16),
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
                        TanSoQuet = "120Hz",
                        Gia = 22990000,
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 2,
                        TEN_SP = "Samsung Galaxy S23",
                        HinhAnhSP = "samsung_s23.jpg",
                        SlideShow = "slide_2.jpg",
                        MoTa = "Samsung Galaxy S23 mang đến một thiết kế hoàn hảo với màn hình Dynamic AMOLED 2X 6.1 inch, đảm bảo độ sắc nét cao. Máy được trang bị chip Snapdragon 8 Gen 2 mạnh mẽ, hệ thống camera ấn tượng 50MP và dung lượng pin tối ưu 3900mAh, cho trải nghiệm lâu dài trong suốt cả ngày.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 1, 26),
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
                        TanSoQuet = "120Hz",
                        Gia = 23990000,
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 3,
                        TEN_SP = "Xiaomi 13",
                        HinhAnhSP = "xiaomi_13.jpg",
                        SlideShow = "slide_3.jpg",
                        MoTa = "Xiaomi 13 là sự kết hợp hoàn hảo giữa thiết kế sang trọng và hiệu năng mạnh mẽ. Với chipset Snapdragon 8 Gen 2, RAM 12GB và bộ nhớ trong 256GB, sản phẩm sẵn sàng đáp ứng mọi nhu cầu từ làm việc đến giải trí. Camera Leica 50MP, màn hình AMOLED 6.36 inch và pin 4500mAh giúp người dùng tận hưởng trải nghiệm tuyệt vời.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2025, 1, 1),
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
                        TanSoQuet = "120Hz",
                        Gia = 17990000,
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 4,
                        TEN_SP = "Oppo Find X6",
                        HinhAnhSP = "oppo_find_x6.jpg",
                        SlideShow = "slide_4.jpg",
                        MoTa = "Oppo Find X6 sở hữu thiết kế tinh tế với công nghệ camera đỉnh cao. Camera sau 50MP mang lại hình ảnh sống động, chip Dimensity 9200 và RAM 16GB đảm bảo hiệu năng mượt mà. Sản phẩm nổi bật với màn hình AMOLED 6.8 inch và pin 4800mAh, lý tưởng cho cả công việc và giải trí.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 9, 12),
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
                        TanSoQuet = "120Hz",
                        Gia = 32990000,
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 1,
                        TEN_SP = "iPhone 14 Pro Max",
                        HinhAnhSP = "iphone_14_pro_max.jpg",
                        MoTa = "iPhone 14 Pro Max là chiếc flagship hàng đầu của Apple, nổi bật với thiết kế Dynamic Island độc đáo, hiệu năng vượt trội nhờ chipset A16 Bionic, màn hình Super Retina XDR 6.7 inch sắc nét, cùng cụm camera chuyên nghiệp với độ phân giải cao lên đến 48MP. Đây là sự lựa chọn hoàn hảo cho những ai yêu thích công nghệ hiện đại.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 12, 2),
                        TrangThai = 1,
                        Slug = "iphone-14-pro-max",
                        KichThuoc_ManHinh = "6.7 inch",
                        CameraSau = "48MP + 12MP + 12MP",
                        Camera_Truoc = "12MP",
                        Chipset = "A16 Bionic",
                        Gpu = "Apple GPU",
                        Dung_Luong_Ram = "6GB",
                        Bo_Nho_Trong = "1TB",
                        Pin = "4323mAh",
                        HeDieuHanh = "iOS 16",
                        TanSoQuet = "120Hz",
                        Gia = 33990000, // Đơn giá của sản phẩm
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 2,
                        TEN_SP = "Samsung Galaxy Z Fold4",
                        HinhAnhSP = "samsung_z_fold4.jpg",
                        MoTa = "Samsung Galaxy Z Fold4 là mẫu điện thoại gập tiên tiến, mang lại trải nghiệm sử dụng độc đáo với màn hình lớn 7.6 inch. Được trang bị chip Snapdragon 8+ Gen 1 mạnh mẽ, camera chất lượng cao với khả năng quay video 4K, và thiết kế gọn gàng, Z Fold4 là lựa chọn lý tưởng cho người dùng yêu thích sự đổi mới.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 12, 12),
                        TrangThai = 1,
                        Slug = "samsung-galaxy-z-fold4",
                        KichThuoc_ManHinh = "7.6 inch",
                        CameraSau = "50MP + 12MP + 10MP",
                        Camera_Truoc = "10MP",
                        Chipset = "Snapdragon 8+ Gen 1",
                        Gpu = "Adreno 730",
                        Dung_Luong_Ram = "12GB",
                        Bo_Nho_Trong = "512GB",
                        Pin = "4400mAh",
                        HeDieuHanh = "Android 13",
                        TanSoQuet = "120Hz",
                        Gia = 41990000,
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 3,
                        TEN_SP = "Xiaomi Redmi Note 12 Pro",
                        HinhAnhSP = "xiaomi_redmi_note_12_pro.jpg",
                        MoTa = "Xiaomi Redmi Note 12 Pro là smartphone tầm trung với nhiều tính năng cao cấp, bao gồm màn hình AMOLED 6.67 inch, cụm camera 108MP chụp ảnh sắc nét, và viên pin lớn 5000mAh hỗ trợ sạc nhanh. Đây là sản phẩm lý tưởng cho người dùng cần hiệu năng mạnh mẽ với giá cả phải chăng.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 12, 1),
                        TrangThai = 1,
                        Slug = "xiaomi-redmi-note-12-pro",
                        KichThuoc_ManHinh = "6.67 inch",
                        CameraSau = "108MP + 8MP + 2MP",
                        Camera_Truoc = "16MP",
                        Chipset = "Dimensity 1080",
                        Gpu = "Mali-G68",
                        Dung_Luong_Ram = "8GB",
                        Bo_Nho_Trong = "256GB",
                        Pin = "5000mAh",
                        HeDieuHanh = "MIUI 14",
                        TanSoQuet = "120Hz",
                        Gia = 9990000,
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 4,
                        TEN_SP = "Oppo Reno 9 Pro",
                        HinhAnhSP = "oppo_reno_9_pro.jpg",
                        MoTa = "Oppo Reno 9 Pro là dòng sản phẩm cao cấp với thiết kế mỏng nhẹ, màu sắc sang trọng, và camera selfie 32MP chuyên nghiệp. Sản phẩm được trang bị chipset Dimensity 8100 mạnh mẽ, màn hình OLED 6.7 inch, và pin dung lượng lớn, hỗ trợ sạc nhanh 65W.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 8, 12),
                        TrangThai = 1,
                        Slug = "oppo-reno-9-pro",
                        KichThuoc_ManHinh = "6.7 inch",
                        CameraSau = "50MP + 8MP + 2MP",
                        Camera_Truoc = "32MP",
                        Chipset = "Dimensity 8100",
                        Gpu = "Mali-G610",
                        Dung_Luong_Ram = "12GB",
                        Bo_Nho_Trong = "512GB",
                        Pin = "4500mAh",
                        HeDieuHanh = "ColorOS 13",
                        TanSoQuet = "120Hz",
                        Gia = 17990000,
                        LuotXem = 0
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 1,
                        TEN_SP = "iPhone SE (2024)",
                        HinhAnhSP = "iphone_se_2024.jpg",
                        MoTa = "iPhone SE (2024) mang đến hiệu năng vượt trội với chip A15 Bionic trong một thiết kế nhỏ gọn. Sản phẩm có camera sắc nét, màn hình Retina HD 4.7 inch, và hỗ trợ cập nhật phần mềm lâu dài, phù hợp với người dùng yêu thích sự tiện lợi và hiệu quả.",
                        TGianBaoHanh = 12,
                        NgayRaMat = new DateTime(2024, 1, 30),
                        TrangThai = 1,
                        Slug = "iphone-se-2024",
                        KichThuoc_ManHinh = "4.7 inch",
                        CameraSau = "12MP",
                        Camera_Truoc = "7MP",
                        Chipset = "A15 Bionic",
                        Gpu = "Apple GPU",
                        Dung_Luong_Ram = "4GB",
                        Bo_Nho_Trong = "64GB",
                        Pin = "1821mAh",
                        HeDieuHanh = "iOS 16",
                        TanSoQuet = "60Hz",
                        Gia = 10990000,
                        LuotXem = 0
                    }

                };
                _db.SanPham.AddRange(sanPhams);
                _db.SaveChanges();
            }

            // Dữ liệu màu sắc sản phẩm
            if (!_db.MauSac.Any())
            {
                var mauSacs = new List<MauSac>
                {
                    // iphone 14 (MASP 1)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054086797665958_ip-14-den-1.jpg",
                        MaSP = 1,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Trắng",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054091427671235_ip-14-trang-1.jpg",
                        MaSP = 1,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Tím",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054090260153672_ip-14-tim-1.jpg",
                        MaSP = 1,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Xanh",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054088819837718_ip-14-do-1.jpg",
                        MaSP = 1,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // samsung-galaxy-s23 (MASP 2)
                    new MauSac
                    {
                        TenMauSac = "Xanh",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_17_638122486969031761_samsung-galaxy-s23-xanh-4.jpg",
                        MaSP = 2,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Kem",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_17_638122486170765497_samsung-galaxy-s23-tim-4.jpg",
                        MaSP = 2,
                        SoLuongTon_MS = 30,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2024_5_15_638513664899586826_samsung-galaxy-s23-den-docquyen.jpg",
                        MaSP = 2,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Tím",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_17_638122486170765497_samsung-galaxy-s23-tim-4.jpg",
                        MaSP = 2,
                        SoLuongTon_MS = 30,
                        TrangThai = 1
                    },
                    // Xiaomi 13 (MASP 3)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_20_638125113177912131_xiaomi-13-den-5.jpg",
                        MaSP = 3,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Xanh",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_20_638125111639884515_xiaomi-13-xanh-5.jpg",
                        MaSP = 3,
                        SoLuongTon_MS = 20,
                        TrangThai = 1
                    },
                    //Oppo Find X6 (MASP 4)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_find_x8_space_black_1_6a9c3746b3.png",
                        MaSP = 4,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // iPhone 14 Pro Max (MASP 5)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054218956629637_ip-14-pro-max-den-1.jpg",
                        MaSP = 5,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Bạc",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054217303176240_ip-14-pro-max-bac-1.jpg",
                        MaSP = 5,
                        SoLuongTon_MS = 15,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Vàng",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054222139728415_ip-14-pro-max-vang-1.jpg",
                        MaSP = 5,
                        SoLuongTon_MS = 20,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Tím",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054220350691584_ip-14-pro-max-tim-1.jpg",
                        MaSP = 5,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // Samsung Galaxy Z Fold4 (MASP 6)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_8_10_637957713446350652_samsung-galaxy-z-fold4-den-2.jpg",
                        MaSP = 6,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Xanh",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_8_10_637957717267662603_samsung-galaxy-z-fold4-xanh-2.jpg",
                        MaSP = 6,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Đỏ",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_4_10_638167221773487338_samsung-galaxy-z-fold4-do-2.jpg",
                        MaSP = 6,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // Xiaomi Redmi Note 12 Pro (MASP 7)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_3_22_638151013110509349_xiaomi-redmi-note-12-pro-trang-xam-5.jpg",
                        MaSP = 7,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Trắng",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_3_22_638151007412242456_xiaomi-redmi-note-12-pro-trang-5.jpg",
                        MaSP = 7,
                        SoLuongTon_MS = 70,
                        TrangThai = 1
                    },
                    // Oppo Reno 9 Pro (MASP 8)
                    new MauSac
                    {
                        TenMauSac = "Bạc",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_reno_12_trang_d776379731.jpg",
                        MaSP = 8,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Nâu",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_reno_12_den_f4fa1cfa2a.jpg",
                        MaSP = 8,
                        SoLuongTon_MS = 40,
                        TrangThai = 1
                    },
                    // iPhone SE (2024) (MASP 9)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_3_9_637823948840474346_iphone-se-2022-den-1.jpg",
                        MaSP = 9,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Bạc",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_reno_12_trang_d776379731.jpg",
                        MaSP = 9,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                };
                _db.MauSac.AddRange(mauSacs);
                _db.SaveChanges();
            }

            // Dữ liệu Khách hàng
            if (!_db.KhachHang.Any())
            {
                var khachHangs = new List<KhachHang>
                {
                    new KhachHang
                    {
                        HoKH = "Trịnh",
                        TenKH = "Tuấn",
                        GioiTinh = "Nam",
                        Email = "ttptuan_bocon@example.com",
                        SDT = "0123456789",
                        DiaChi = "123 Đường ABC, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "ttptuan",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
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
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
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
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    // Thêm khách hàng mới
                    new KhachHang
                    {
                        HoKH = "Lê",
                        TenKH = "Hồng Hoa",
                        GioiTinh = "Nữ",
                        Email = "lehonghoa@example.com",
                        SDT = "0987654321",
                        DiaChi = "321 Đường DEF, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "lehonghoa",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    new KhachHang
                    {
                        HoKH = "Phan",
                        TenKH = "Thanh Sơn",
                        GioiTinh = "Nam",
                        Email = "phanthanhson@example.com",
                        SDT = "0765432189",
                        DiaChi = "654 Đường GHI, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "phanthanhson",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    // Dữ liệu khách hàng mới thêm
                    new KhachHang
                    {
                        HoKH = "Vũ",
                        TenKH = "Minh Hoàng",
                        GioiTinh = "Nam",
                        Email = "vuminhhoang@example.com",
                        SDT = "0901122334",
                        DiaChi = "987 Đường JKL, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "vuminhhoang",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    new KhachHang
                    {
                        HoKH = "Đặng",
                        TenKH = "Ngọc Bích",
                        GioiTinh = "Nữ",
                        Email = "dangngocbich@example.com",
                        SDT = "0912233445",
                        DiaChi = "654 Đường MNO, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "dangngocbich",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    new KhachHang
                    {
                        HoKH = "Lý",
                        TenKH = "Trường Giang",
                        GioiTinh = "Nam",
                        Email = "lytruonggiang@example.com",
                        SDT = "0933445566",
                        DiaChi = "321 Đường PQR, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "lytruonggiang",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    new KhachHang
                    {
                        HoKH = "Phạm",
                        TenKH = "Thu Hà",
                        GioiTinh = "Nữ",
                        Email = "phamthuha@example.com",
                        SDT = "0944556677",
                        DiaChi = "789 Đường STU, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "phamthuha",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    new KhachHang
                    {
                        HoKH = "Hoàng",
                        TenKH = "Anh Tuấn",
                        GioiTinh = "Nam",
                        Email = "hoanganhtuan@example.com",
                        SDT = "0955667788",
                        DiaChi = "456 Đường VWX, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "hoanganhtuan",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("Ttptuan@123")
                    },
                    new KhachHang
                    {
                        HoKH = "Le",
                        TenKH = "Thinh",
                        GioiTinh = "Nam",
                        Email = "aspnetcore9@gmail.com",
                        SDT = "0955667788",
                        DiaChi = "456 Đường VWX, TP. Hồ Chí Minh",
                        TENNGUOIDUNG = "zz",
                        MATKHAU = BCrypt.Net.BCrypt.HashPassword("1")
                    },

                };

                _db.KhachHang.AddRange(khachHangs);
                _db.SaveChanges();
            }

            // Dữ liệu Chi tiết bình luận
            if (!_db.ChiTietBinhLuan.Any())
            {
                var chiTietBinhLuans = new List<ChiTietBinhLuan>
                {
                    // Bình luận cho iPhone 14
                    new ChiTietBinhLuan
                    {
                        MA_SP = 6,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Thiết kế đẹp, hiệu năng mạnh mẽ. Rất đáng tiền!",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 6,
                        MA_KH = 3,
                        SO_SAO = 4,
                        NOI_DUNG = "Sản phẩm tốt nhưng thời lượng pin cần cải thiện.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Samsung Galaxy S23
                    new ChiTietBinhLuan
                    {
                        MA_SP = 7,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Màn hình đẹp, camera rất sắc nét. Rất hài lòng!",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 7,
                        MA_KH = 2,
                        SO_SAO = 3,
                        NOI_DUNG = "Hiệu năng tốt nhưng máy hơi nóng khi chơi game.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Xiaomi 13
                    new ChiTietBinhLuan
                    {
                        MA_SP = 8,
                        MA_KH = 1,
                        SO_SAO = 4,
                        NOI_DUNG = "Thiết kế đẹp, giá cả hợp lý. Nhưng camera không tốt lắm.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 8,
                        MA_KH = 2,
                        SO_SAO = 5,
                        NOI_DUNG = "Tuyệt vời! Hiệu năng rất ổn định.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Oppo Find X6
                    new ChiTietBinhLuan
                    {
                        MA_SP = 9,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Camera cực kỳ tốt, màn hình sáng rõ. Rất thích!",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 9,
                        MA_KH = 2,
                        SO_SAO = 4,
                        NOI_DUNG = "Thiết kế đẹp nhưng giá hơi cao.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho các sản phẩm mới thêm
                    new ChiTietBinhLuan
                    {
                        MA_SP = 9,
                        MA_KH = 3,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng tốt, thiết kế ổn. Rất đáng mua.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 2,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Mua tặng bạn bè, mọi người đều rất thích!",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 8,
                        MA_KH = 3,
                        SO_SAO = 4,
                        NOI_DUNG = "Tính năng rất đa dạng, hiệu năng ổn định.",
                        NGAY = DateTime.Now
                    },

                    new ChiTietBinhLuan
                    {
                        MA_SP = 1,
                        MA_KH = 4,
                        SO_SAO = 5,
                        NOI_DUNG = "Thiết kế gọn nhẹ, hiệu năng tốt.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 1,
                        MA_KH = 5,
                        SO_SAO = 4,
                        NOI_DUNG = "Sản phẩm tốt nhưng thời lượng pin cần cải thiện.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Samsung Galaxy S22
                    new ChiTietBinhLuan
                    {
                        MA_SP = 2,
                        MA_KH = 3,
                        SO_SAO = 5,
                        NOI_DUNG = "Hiệu năng tuyệt vời, thiết kế sang trọng.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 2,
                        MA_KH = 4,
                        SO_SAO = 3,
                        NOI_DUNG = "Camera tốt nhưng giá hơi cao.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Xiaomi Redmi Note 11
                    new ChiTietBinhLuan
                    {
                        MA_SP = 3,
                        MA_KH = 2,
                        SO_SAO = 4,
                        NOI_DUNG = "Giá cả hợp lý, pin rất tốt.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 3,
                        MA_KH = 5,
                        SO_SAO = 5,
                        NOI_DUNG = "Màn hình đẹp, hiệu năng ổn định.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Oppo Reno 8
                    new ChiTietBinhLuan
                    {
                        MA_SP = 4,
                        MA_KH = 3,
                        SO_SAO = 5,
                        NOI_DUNG = "Camera sắc nét, thiết kế đẹp.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 4,
                        MA_KH = 1,
                        SO_SAO = 4,
                        NOI_DUNG = "Sản phẩm tốt nhưng cần cải thiện phần mềm.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Vivo X80////////////////////////
                    new ChiTietBinhLuan
                    {
                        MA_SP = 5,
                        MA_KH = 2,
                        SO_SAO = 4,
                        NOI_DUNG = "Màn hình đẹp, thiết kế cao cấp.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 5,
                        MA_KH = 4,
                        SO_SAO = 5,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 5,
                        MA_KH = 8,
                        SO_SAO = 5,
                        NOI_DUNG = "Hiệu năng ổn định, mượt mà, màn hình sáng.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 5,
                        MA_KH = 9,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 2,
                        MA_KH = 9,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 3,
                        MA_KH = 9,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 3,
                        MA_KH = 10,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 1,
                        MA_KH = 10,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new ChiTietBinhLuan
                    {
                        MA_SP = 1,
                        MA_KH = 7,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                };

                _db.ChiTietBinhLuan.AddRange(chiTietBinhLuans);
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

            //
            if (!_db.KhuyenMai.Any())
            {
                var KhuyenMais = new List<KhuyenMai>
                {
                    new KhuyenMai
                    {
                      SanPhamKhuyenMaiId = 1,
                      MucGiamGia = 30000,
                      NgayBatDau = new DateTime(2025, 1, 1),
                      NgayKetThuc = DateTime.Now,
                    },
                    new KhuyenMai
                    {
                      SanPhamKhuyenMaiId = 2,
                      MucGiamGia = 500000,
                      NgayBatDau = new DateTime(2025, 1, 1),
                      NgayKetThuc = new DateTime(2025, 12, 1),
                    }
                };

                _db.KhuyenMai.AddRange(KhuyenMais);
                _db.SaveChanges();
            }



            //Seed Data Bài viết
            if (!_db.BaiViet.Any())
            {
                var BaiViet = new List<BaiViet>
                {
                    new BaiViet
                    {
                        TieuDe = "Giới thiệu về TechLand",
                        NoiDung = @"
                            <p>TechLand là nền tảng thương mại điện tử chuyên cung cấp các sản phẩm điện thoại di động chính hãng. Với sứ mệnh mang lại trải nghiệm mua sắm an toàn, tiện lợi, chúng tôi tự hào là đối tác của các thương hiệu hàng đầu như <strong>Apple, Samsung, Xiaomi, Oppo</strong>, và nhiều thương hiệu khác. Với hơn 10 năm kinh nghiệm trong ngành, chúng tôi luôn nỗ lực không ngừng để nâng cao chất lượng dịch vụ và sản phẩm nhằm phục vụ khách hàng tốt nhất.</p>
                            <div style=""text-align: center;"">
                            <img src='/image/baiviet/gioi-thieu-techland.jpg' alt='Giới thiệu về TechLand' style='max-width: 500px; height: auto;'>
                            </div>

                            <p>Tại TechLand, khách hàng không chỉ được tiếp cận với các sản phẩm chất lượng cao mà còn được hỗ trợ bởi các dịch vụ vượt trội. Mỗi sản phẩm của chúng tôi đều được kiểm tra kỹ lưỡng, đảm bảo chất lượng tốt nhất khi đến tay người tiêu dùng. Chúng tôi hiểu rằng sự hài lòng của khách hàng chính là yếu tố quan trọng nhất để duy trì sự phát triển lâu dài của công ty.</p>
                            <p>Với phương châm <strong>'Chất lượng là hàng đầu'</strong>, TechLand luôn cam kết cung cấp những sản phẩm chính hãng và mới nhất. Ngoài ra, chúng tôi còn cung cấp các dịch vụ sau bán hàng như bảo hành và hỗ trợ kỹ thuật nhằm đảm bảo quyền lợi cho khách hàng.</p>
                             <h3>Địa chỉ và lĩnh vực kinh doanh</h3>
                            <p>Phạm vi thương mại, TechLand hiện có duy nhất một chi nhánh tại 60 Huỳnh Thúc Kháng, Quận 1, TP.HCM. Là nơi tập trung vào việc cung cấp các sản phẩm công nghệ, từ điện thoại, đến các phụ kiện đi kèm. Hình thức kinh doanh của TechLand là kết hợp giữa thương mại điện tử và bán hàng trực tiếp tại cửa hàng...</p>
                            <ul>
                                <li>Địa chỉ: 60 Huỳnh Thúc Kháng, Quận 1, TP.HCM.</li>
                                <li>Lĩnh vực: Cung cấp điện thoại.</li>
                                <li>Hình thức kinh doanh: Thương mại điện tử kết hợp bán hàng trực tiếp tại cửa hàng.</li>
                            </ul>
                            <h3>Dịch vụ vượt trội tại TechLand</h3>
                            <ul>
                                <li>Giao hàng nhanh toàn quốc, chỉ từ 1-3 ngày làm việc.</li>
                                <li>Chính sách đổi trả linh hoạt trong vòng 30 ngày nếu sản phẩm có lỗi từ nhà sản xuất.</li>
                                <li>Dịch vụ bảo hành chính hãng tại các trung tâm ủy quyền trên toàn quốc.</li>
                                <li>Chính sách hỗ trợ kỹ thuật miễn phí cho tất cả khách hàng mua sản phẩm tại TechLand.</li>
                            </ul>
                            <h3>Ưu đãi đặc biệt cho khách hàng</h3>
                            <p>Nhằm tri ân khách hàng, chúng tôi thường xuyên có các chương trình khuyến mãi hấp dẫn, giảm giá lên đến 50% cho một số sản phẩm, đặc biệt là vào các dịp lễ, tết. Các khách hàng mua hàng tại TechLand sẽ được hưởng các chính sách ưu đãi như:</p>
                            <ul>
                                <li>Giảm giá trực tiếp khi thanh toán qua các phương thức thanh toán điện tử.</li>
                                <li>Chương trình quà tặng hấp dẫn cho những khách hàng mua sản phẩm cao cấp.</li>
                                <li>Chương trình tích điểm thưởng cho mỗi lần mua hàng, điểm thưởng có thể sử dụng để giảm giá cho các lần mua sau.</li>
                            </ul>
                            <p>Với những nỗ lực không ngừng, TechLand cam kết sẽ tiếp tục mang đến những sản phẩm công nghệ đỉnh cao và dịch vụ tốt nhất cho khách hàng. Chúng tôi luôn luôn cải tiến để mang đến những trải nghiệm mua sắm tuyệt vời nhất.</p>",
                        NgayDang = new DateTime(2025, 1, 1),
                        HinhAnh = "gioi-thieu-techland.jpg"
                    },
                    new BaiViet
                    {
                        TieuDe = "Chính sách bảo hành và đổi trả tại TechLand",
                        NoiDung = @"
                            <p>Tại TechLand, chúng tôi luôn đặt lợi ích của khách hàng lên hàng đầu thông qua các chính sách bảo hành và đổi trả minh bạch, linh hoạt:</p>
                            <h3>1. Chính sách bảo hành:</h3>
                            <ul>
                                <li>Tất cả sản phẩm điện thoại mua tại TechLand đều được bảo hành chính hãng từ 12-24 tháng (tuỳ từng sản phẩm cụ thể).</li>
                                <li>TechLand hỗ trợ gửi sản phẩm đến các trung tâm bảo hành ủy quyền nếu khách hàng không có thời gian đến trực tiếp.</li>
                            </ul>
                            <h3>2. Chính sách đổi trả:</h3>
                            <ul>
                                <li>Khách hàng có thể đổi trả sản phẩm trong vòng 30 ngày nếu phát hiện lỗi từ nhà sản xuất.</li>
                                <li>Sản phẩm phải còn nguyên vẹn, đầy đủ phụ kiện, và không bị hư hỏng do người sử dụng.</li>
                            </ul>
                            <h3>3. Quy trình bảo hành và đổi trả:</h3>
                            <ul>
                                <li>Bước 1: Liên hệ đội ngũ hỗ trợ của TechLand qua hotline hoặc email để thông báo vấn đề.</li>
                                <li>Bước 2: Gửi sản phẩm kèm hóa đơn mua hàng tới chi nhánh gần nhất hoặc qua đường bưu điện.</li>
                                <li>Bước 3: TechLand xử lý và thông báo kết quả bảo hành/đổi trả trong vòng 7 ngày làm việc.</li>
                            </ul>
                            <p>Chúng tôi luôn cố gắng mang lại trải nghiệm mua sắm và hậu mãi tốt nhất cho khách hàng. Đừng ngần ngại liên hệ với TechLand nếu bạn có bất kỳ thắc mắc nào!</p>",
                        NgayDang = new DateTime(2025, 1, 1),
                        HinhAnh = "chinh-sach-bao-hanh.jpg"
                    },
                      new BaiViet
                    {
                        TieuDe = "Khám phá iPhone 15 Pro Max",
                        NoiDung = @"
                            <p>iPhone 15 Pro Max là siêu phẩm công nghệ mới nhất từ Apple, hiện đang có mặt tại TechLand. Sản phẩm này không chỉ mang lại thiết kế tinh tế mà còn tích hợp những công nghệ đột phá:</p>
                            <h3>1. Thiết kế:</h3>
                            <p>iPhone 15 Pro Max được chế tác từ vật liệu titanium cao cấp, giúp sản phẩm vừa nhẹ hơn vừa bền bỉ hơn. Màn hình Super Retina XDR mang lại độ sáng và sắc nét vượt trội.</p>
                            <h3>2. Hiệu năng:</h3>
                            <p>Với chip A17 Pro, iPhone 15 Pro Max đảm bảo hiệu năng mạnh mẽ, xử lý mọi tác vụ nhanh chóng, từ chơi game đồ họa cao đến chỉnh sửa video chuyên nghiệp.</p>
                            <h3>3. Camera đỉnh cao:</h3>
                            <p>Hệ thống camera cải tiến với khả năng zoom quang học 10x, chế độ chụp đêm ấn tượng, và hỗ trợ quay video 4K ProRes, đáp ứng mọi nhu cầu chụp ảnh và quay phim của người dùng.</p>
                            <div style=""text-align: center;"">
                            </div>
                            <p>Tại TechLand, iPhone 15 Pro Max được bán với giá cạnh tranh nhất thị trường. Đặc biệt, khách hàng mua sản phẩm trong tháng này sẽ nhận ngay các ưu đãi:</p>
                            <ul>
                                <li>Tặng kèm ốp lưng cao cấp và cường lực.</li>
                                <li>Gói bảo hành mở rộng thêm 6 tháng.</li>
                            </ul>
                            <p>Hãy đến với TechLand để trải nghiệm ngay iPhone 15 Pro Max và các dòng sản phẩm Apple khác!</p>",
                        NgayDang = new DateTime(2025, 1, 1),
                        HinhAnh = "iphone-15-pro-max.jpg"
                    },
                      new BaiViet
                    {
                        TieuDe = "Đi tìm những điểm nhấn đáng tiền có mặt trên chiếc smartphone realme Note 60X",
                        NoiDung = @"
                            <p><strong>realme Note 60X</strong> là một sản phẩm đến từ thương hiệu realme, với nhiều điểm nhấn nổi bật và giá thành phải chăng, hứa hẹn là sự lựa chọn lý tưởng cho những ai muốn sở hữu một chiếc smartphone với cấu hình ổn định nhưng không quá tốn kém. Hãy cùng khám phá những đặc điểm khiến realme Note 60X trở thành sự lựa chọn đáng tiền trong phân khúc giá rẻ.</p>
                
                            <h3>Màn hình lớn, tần số quét cao mượt mà</h3>
                            <p>realme Note 60X trang bị màn hình IPS LCD 6.74 inch, độ phân giải HD+ mang đến một không gian hiển thị rộng rãi, thoải mái để xem phim, lướt web hay chơi game. Dù độ phân giải chưa cao, nhưng chiếc điện thoại này bù lại với tần số quét 90Hz, giúp trải nghiệm vuốt chạm mượt mà, nhanh nhạy hơn rất nhiều so với những màn hình 60Hz truyền thống. Đặc biệt, màn hình của realme Note 60X có độ sáng tối đa 560 nits, giúp hiển thị rõ ràng trong môi trường ánh sáng mạnh, như khi sử dụng ngoài trời dưới ánh nắng trực tiếp.</p>
                
                            <h3>Hiệu năng ổn định, đáp ứng tốt nhu cầu cơ bản</h3>
                            <p>Được trang bị vi xử lý Unisoc T612 với tiến trình 12nm, kết hợp cùng RAM 4GB và bộ nhớ trong 128GB, realme Note 60X mang đến hiệu năng ổn định cho các tác vụ sử dụng cơ bản như nghe gọi, nhắn tin, lướt web hay xem phim. Máy có thể chơi được các tựa game nhẹ như Candy Crush, Subway Surfers mà không gặp hiện tượng giật lag, mặc dù không phải là một “quái vật” về hiệu năng. Đặc biệt, với bộ nhớ trong lên tới 128GB, bạn có thể thoải mái lưu trữ ảnh, video, và ứng dụng mà không phải lo lắng về dung lượng lưu trữ.</p>
                
                            <h3>Dung lượng pin khủng, thoải mái sử dụng cả ngày dài</h3>
                            <p>realme Note 60X được trang bị viên pin dung lượng lên đến 5.000mAh, mang lại khả năng sử dụng cả ngày dài mà không cần phải lo lắng về việc hết pin giữa chừng. Với viên pin này, người dùng có thể thoải mái thực hiện các tác vụ thường ngày, từ lướt web, xem video cho đến nhắn tin mà không phải sạc lại quá thường xuyên. Máy cũng hỗ trợ sạc nhanh 10W, giúp tiết kiệm thời gian sạc, mặc dù tốc độ sạc không quá nhanh, nhưng vẫn đáp ứng nhu cầu sử dụng của đa số người dùng.</p>
                
                            <h3>Thiết kế trẻ trung, năng động và bền bỉ</h3>
                            <p>Về thiết kế, realme Note 60X mang đến một vẻ ngoài trẻ trung, hiện đại với mặt lưng nhựa nhám, giúp hạn chế bám vân tay và mồ hôi, giữ cho máy luôn sạch sẽ và mới mẻ. Máy có hai tùy chọn màu sắc là Xanh và Đen, phù hợp với sở thích của nhiều đối tượng người dùng. Đặc biệt, realme Note 60X còn đạt chuẩn kháng nước, bụi IP54, giúp bảo vệ máy trong các tình huống vô tình làm đổ nước hoặc khi sử dụng dưới trời mưa nhỏ. Đây là một điểm cộng lớn, đặc biệt là với những người dùng thường xuyên di chuyển hoặc làm việc trong môi trường ẩm ướt, bảo vệ chiếc điện thoại khỏi những sự cố không mong muốn.</p>

                            <p>Với mức giá hợp lý, realme Note 60X không chỉ đáp ứng đầy đủ các nhu cầu cơ bản mà còn mang lại những tính năng đặc biệt, giúp người dùng có được một trải nghiệm sử dụng tuyệt vời. Chiếc smartphone này xứng đáng là lựa chọn đáng tiền trong phân khúc giá rẻ, nếu bạn đang tìm kiếm một chiếc điện thoại với hiệu năng ổn định, pin trâu và thiết kế bền bỉ.</p>
                        ",
                        NgayDang = new DateTime(2025, 1, 7),
                        HinhAnh = "realme-note-60x.jpg"
                    },
                      new BaiViet
                    {
                        TieuDe = "OPPO Reno13 Series đã kế thừa các tính năng cao cấp trên dòng Find X8 như thế nào!",
                        NoiDung = @"
                            <p>OPPO luôn là thương hiệu tiên phong trong việc mang đến những công nghệ đột phá cho người dùng. Với dòng Reno, OPPO đã định hình một phân khúc smartphone cận cao cấp, sở hữu thiết kế thời thượng, hiệu năng mạnh mẽ và đặc biệt là khả năng chụp ảnh ấn tượng. Tiếp nối thành công của các thế hệ trước, <strong>OPPO Reno13 Series</strong> hứa hẹn sẽ là một bước tiến lớn, khi kế thừa hàng loạt tính năng cao cấp từ dòng flagship <strong>OPPO Find X8 Series</strong>, mang đến trải nghiệm người dùng vượt trội, tiệm cận đẳng cấp flagship.</p>

                            <h3>Thiết kế tinh tế, hiện đại với độ hoàn thiện cao</h3>
                            <p>OPPO Reno13 Series mang đến một thiết kế sang trọng, trẻ trung nhưng không kém phần cao cấp. Với mặt lưng kính cường lực được phủ lớp mờ, sản phẩm tạo cảm giác thoải mái khi cầm nắm, đồng thời hạn chế bám vân tay và mồ hôi. Các cạnh viền được bo cong mềm mại, mang lại cảm giác liền mạch và chắc chắn.</p>
                            <p>Màu sắc của Reno13 Series cũng được đầu tư với các tông màu thời thượng như Bạc Ánh Sao, Xanh Ngọc Lục Bảo và Đen Tuyệt Đỉnh, phù hợp với nhiều phong cách cá nhân. Máy chỉ mỏng 7.7mm và nặng khoảng 185g, mang lại sự gọn nhẹ, tiện lợi khi sử dụng hằng ngày.</p>

                            <h3>Trải nghiệm Live Photo hoàn toàn mới</h3>
                            <p>Kế thừa tinh hoa từ dòng flagship Find X8 Series, OPPO Reno13 Series mang đến trải nghiệm Live Photo vượt trội, giúp bạn không chỉ đơn thuần ghi lại khoảnh khắc mà còn sáng tạo với những thước phim ngắn đầy nghệ thuật. Live Photo ghi lại các khoảnh khắc trước và sau khi chụp, tạo nên những bức ảnh động sống động, lưu giữ trọn vẹn cảm xúc của bạn.</p>
                            <p>Với Reno13 Series, bạn có thể chỉnh sửa Live Photo một cách linh hoạt: thêm bộ lọc màu, điều chỉnh độ sáng, thêm văn bản hoặc sticker để cá nhân hóa. Đặc biệt, tính năng này tương thích tốt với các nền tảng mạng xã hội như Facebook, Instagram và thậm chí chia sẻ mượt mà với các thiết bị iPhone mà không bị mất hiệu ứng động.</p>

                            <h3>Khả năng kháng nước và bụi bẩn ấn tượng</h3>
                            <p>Không chỉ kế thừa chuẩn kháng nước và bụi bẩn IP68 từ Find X8 Series, Reno13 Series còn đạt chuẩn IP69 - cấp độ bảo vệ cao nhất hiện nay. Điều này giúp máy chịu được áp lực nước lớn, nhiệt độ cao, đảm bảo hoạt động tốt trong các môi trường khắc nghiệt như mưa lớn, bụi mịn, hay ngay cả khi ngâm dưới nước.</p>
                            <p>Bên cạnh đó, máy còn hỗ trợ chụp ảnh dưới nước bằng phím cứng, mở ra một thế giới sáng tạo đầy mới mẻ. Bạn có thể ghi lại những khoảnh khắc độc đáo trong các chuyến du lịch biển hay hồ bơi mà không cần lo lắng về an toàn thiết bị.</p>

                            <h3>Bộ tính năng nhiếp ảnh AI mạnh mẽ</h3>
                            <p>OPPO Reno13 Series được trang bị bộ tính năng AI tiên tiến, giúp nâng cao trải nghiệm nhiếp ảnh ở mọi điều kiện. Các tính năng nổi bật bao gồm:</p>
                            <ul>
                                <li><strong>AI Portrait Enhancement:</strong> Nhận diện khuôn mặt, làm mịn da và tạo hiệu ứng xóa phông tự nhiên, giúp ảnh chân dung lung linh và chuyên nghiệp.</li>
                                <li><strong>AI Night Mode:</strong> Tăng cường chi tiết và giảm nhiễu, mang lại những bức ảnh đêm rõ nét, ấn tượng.</li>
                                <li><strong>AI Scene Recognition:</strong> Tự động nhận diện khung cảnh và tối ưu hóa thông số chụp, từ đó cho ra những bức ảnh phong cảnh tuyệt đẹp.</li>
                                <li><strong>AI HDR:</strong> Cân bằng ánh sáng và tăng độ tương phản, giúp bức ảnh nổi bật ngay cả trong điều kiện ngược sáng.</li>
                            </ul>
                            <p>Nhờ những tính năng này, Reno13 Series không chỉ đơn thuần là một chiếc smartphone mà còn là một công cụ sáng tạo nghệ thuật đầy cảm hứng.</p>

                            <h3>Trợ thủ AI hỗ trợ công việc hiệu quả</h3>
                            <p>OPPO Reno13 Series không chỉ mạnh mẽ về nhiếp ảnh mà còn là trợ thủ đắc lực trong công việc. Các tính năng AI như dịch thuật tức thì, tóm tắt văn bản hay gợi ý nội dung viết giúp bạn tối ưu hóa thời gian và tăng hiệu quả làm việc.</p>
                            <p>Ví dụ, bạn có thể dịch nhanh tài liệu nước ngoài trực tiếp từ hình ảnh, hoặc sử dụng AI để chỉnh sửa văn phong cho các bài viết quan trọng, giúp chúng trở nên chuyên nghiệp và cuốn hút hơn.</p>

                            <h3>Kết luận</h3>
                            <p>OPPO Reno13 Series là minh chứng cho sự nỗ lực không ngừng của OPPO trong việc mang đến những sản phẩm tốt nhất cho người dùng. Việc kế thừa các tính năng cao cấp từ dòng flagship Find X8 Series không chỉ nâng tầm trải nghiệm người dùng mà còn giúp Reno13 Series trở thành lựa chọn hoàn hảo trong phân khúc cận cao cấp. Nếu bạn đang tìm kiếm một chiếc smartphone thời thượng với hiệu năng mạnh mẽ, khả năng chụp ảnh ấn tượng và các tính năng tiện ích hàng đầu, Reno13 Series chắc chắn sẽ không làm bạn thất vọng.</p>
                        ",
                        NgayDang = new DateTime(2025, 1, 7),
                        HinhAnh = "oppo-reno13-series.jpg"
                    },
                      new BaiViet
                    {
                        TieuDe = "Trên tay OPPO Reno13: Thiết kế bắt mắt, trang bị chip Dimensity 8350, pin 5.600 mAh",
                        NoiDung = @"
                            <p><strong>OPPO Reno13</strong> đã chính thức ra mắt tại thị trường Việt Nam sau khi giới thiệu tại Trung Quốc, mang theo nhiều điểm nhấn nổi bật. Trong bài viết này, chúng ta hãy cùng khám phá chi tiết hơn về thiết kế, cấu hình, và trải nghiệm sử dụng, qua bài trên tay OPPO Reno13.</p>
                        <p><strong>Xem thêm:</strong> <a href='/danh-gia-oppo-find-x8-pro'>Đánh giá OPPO Find X8 Pro: Sản phẩm chất lượng nhất 2024 của OPPO</a></p>

                        <h2>Mở hộp và phụ kiện đi kèm OPPO Reno13</h2>
                        <p>Hộp đựng của OPPO Reno13 sử dụng tông màu trắng - bạc đặc trưng, với thiết kế tinh tế và tối giản. Khi mở hộp, chúng ta sẽ nhận được:</p>
                        <ul>
                            <li>Chiếc smartphone OPPO Reno13</li>
                            <li>Ốp lưng silicon trong suốt</li>
                            <li>Bộ sạc nhanh SuperVOOC 80W</li>
                            <li>Dây cáp USB-C</li>
                            <li>Que chọc SIM và sách hướng dẫn sử dụng</li>
                        </ul>
                        <p>OPPO luôn chú trọng việc cung cấp đầy đủ phụ kiện, giúp người dùng không phải tốn thêm chi phí để mua thêm các món cần thiết.</p>

                        <h2>Thiết kế hiện đại và sang trọng</h2>
                        <p>OPPO Reno13 gây ấn tượng với thiết kế vuông vắn, các cạnh phẳng tạo cảm giác cứng cáp và hiện đại. Máy sở hữu màn hình AMOLED kích thước 6.59 inch, tần số quét 120 Hz, với viền mỏng đều 4 cạnh, mang đến trải nghiệm thị giác tốt.</p>
                        <p>Mặt lưng của Reno13 sử dụng kính nhám mịn, không chỉ đẹp mắt mà còn chống bám dấu vân tay hiệu quả. Cụm camera sau được thiết kế trong khung hình chữ nhật, với viền màu sắc nổi bật, làm tăng tính nhận diện.</p>
                        <p>OPPO Reno13 có nhiều tùy chọn màu sắc bắt mắt, phù hợp với nhiều đối tượng người dùng, từ thanh niên năng động đến người dùng trung niên yêu thích sự sang trọng.</p>

                        <h2>Cấu hình mạnh mẽ với chip Dimensity 8350</h2>
                        <p>Về hiệu năng, OPPO Reno13 được trang bị con chip <strong>MediaTek Dimensity 8350</strong> sản xuất trên tiến trình 4nm. Đây là con chip không chỉ tối ưu về hiệu suất xử lý mà còn tiết kiệm năng lượng, phù hợp với nhu cầu chơi game và đa nhiệm.</p>
                        <p>Máy đi kèm các tùy chọn RAM lên đến 12GB và bộ nhớ trong 256GB, đáp ứng tốt nhu cầu lưu trữ và sử dụng lâu dài. Reno13 còn hỗ trợ kết nối 5G, giúp người dùng trải nghiệm tốc độ mạng nhanh chóng và ổn định.</p>
                        <p>Viên pin 5.600 mAh trên Reno13 đảm bảo thời lượng sử dụng thoải mái trong cả ngày dài. Công nghệ sạc nhanh SuperVOOC 80W giúp nạp đầy pin trong thời gian ngắn, giảm thiểu sự gián đoạn trong trải nghiệm của người dùng.</p>

                        <h2>Camera: Đỉnh cao của sự sáng tạo</h2>
                        <p>Hệ thống camera sau của Reno13 bao gồm:</p>
                        <ul>
                            <li>Cảm biến chính 50 MP với khả năng chụp đêm ấn tượng</li>
                            <li>Camera góc siêu rộng 8 MP</li>
                            <li>Camera đơn sắc 2 MP hỗ trợ hiệu ứng nghệ thuật</li>
                        </ul>
                        <p>Camera trước có độ phân giải 50 MP, tích hợp AI và hỗ trợ lấy nét tự động, cho ra những bức ảnh selfie sắc nét và sống động.</p>
                        <p>Những tính năng như quay video 4K, chế độ chân dung và các bộ lọc màu sắc sáng tạo cũng giúp Reno13 trở thành một thiết bị lý tưởng cho những người yêu thích nhiếp ảnh.</p>

                        <h2>Trải nghiệm người dùng</h2>
                        <p>OPPO Reno13 chạy trên hệ điều hành ColorOS 13.1, mang đến giao diện thân thiện và nhiều tính năng thông minh. Người dùng có thể tùy chỉnh giao diện, tận dụng các tính năng như chia đôi màn hình, chế độ tập trung, và các tiện ích khác để tăng năng suất làm việc.</p>

                        <h2>Kết luận</h2>
                        <p>Nhìn chung, OPPO Reno13 là một sự lựa chọn xuất sắc trong phân khúc tầm trung, với thiết kế đẹp mắt, cấu hình mạnh mẽ và camera chất lượng cao. Đây sẽ là một thiết bị lý tưởng cho những ai đang tìm kiếm một chiếc smartphone toàn diện về cả hiệu năng lẫn thẩm mỹ.</p>
                    ",
                    NgayDang = new DateTime(2025, 1, 8),
                    HinhAnh = "oppo-reno13.jpg"
                    },
                    new BaiViet
                    {
                        TieuDe = "realme GT7 xuất hiện trên TENAA Trung Quốc: Hé lộ các thông số kỹ thuật và thiết kế",
                        NoiDung = @"
                            <p><strong>realme GT7 Pro</strong> được đánh giá là một trong những flagship rẻ nhất trang bị chip Snapdragon 8 Elite. Tuy nhiên, có vẻ như phiên bản cơ bản của dòng sản phẩm này cũng đang được realme phát triển. Một thiết bị được cho là realme GT7 đã lộ diện trên nền tảng chứng nhận TENAA tại Trung Quốc, hé lộ một số thông số kỹ thuật và thiết kế quan trọng.</p>

                            <p>Theo TheTechOutlook, danh sách trên TENAA không tiết lộ tên chính thức của thiết bị, nhưng số model RMX5090 đã xác nhận đây là realme GT7. Những thông tin về thông số kỹ thuật chính của thiết bị đã được công bố trên cơ sở dữ liệu trực tuyến.</p>

                            <h2>Màn hình AMOLED 6.78 inch sắc nét</h2>
                            <p>realme GT7 sở hữu màn hình <strong>AMOLED</strong> kích thước 6.78 inch với độ phân giải 2780 x 1264 pixel (1.5K). Đây là màn hình hỗ trợ độ sáng cao và tần số quét cao, hứa hẹn mang lại trải nghiệm mượt mà cho người dùng, đặc biệt trong các tác vụ giải trí như chơi game hay xem phim.</p>

                            <h2>Hiệu năng mạnh mẽ với Snapdragon 8 Elite</h2>
                            <p>Chip xử lý của thiết bị có 8 nhân với tốc độ xung nhịp tối đa 4.3 GHz, nhiều khả năng là <strong>Snapdragon 8 Elite</strong>. Nếu thông tin này chính xác, realme GT7 sẽ tiếp tục là một trong những flagship sử dụng Snapdragon 8 Elite có mức giá rẻ nhất trên thị trường.</p>
                            <p>Danh sách trên TENAA xác nhận rằng thiết bị hỗ trợ nhiều tùy chọn RAM, bao gồm 8GB, 12GB, 16GB, và thậm chí lên đến 24GB. Bộ nhớ trong cũng đa dạng với các phiên bản 128GB, 256GB, 512GB, và 1TB, đáp ứng tốt nhu cầu lưu trữ đa dạng của người dùng.</p>

                            <h2>Cụm camera kép và pin dung lượng lớn</h2>
                            <p>Mặt sau của realme GT7 được trang bị cụm camera kép với cảm biến chính 50MP và một camera phụ góc siêu rộng 8MP, cho khả năng chụp ảnh linh hoạt trong nhiều tình huống. Camera trước có độ phân giải 16MP, hỗ trợ các tính năng làm đẹp và chụp ảnh selfie chất lượng cao.</p>
                            <p>Bên trong, thiết bị sở hữu viên pin dung lượng lớn 6,310mAh, đảm bảo thời gian sử dụng lâu dài cho người dùng. Ngoài ra, các tính năng như Bluetooth, hồng ngoại, cảm biến vân tay dưới màn hình và nhận diện khuôn mặt cũng được tích hợp, mang lại sự tiện lợi và bảo mật tốt hơn.</p>

                            <h2>Thiết kế tương đồng với realme GT7 Pro</h2>
                            <p>Một trong những lý do khiến thiết bị này được cho là realme GT7 chính là thiết kế của nó. Hình ảnh từ danh sách chứng nhận TENAA cho thấy thiết bị có kiểu dáng rất giống với realme GT7 Pro. Với mặt lưng bóng bẩy, cụm camera được thiết kế tối giản, realme GT7 tiếp tục giữ vững ngôn ngữ thiết kế đặc trưng của dòng GT.</p>

                            <h2>Kết luận</h2>
                            <p>realme GT7 hứa hẹn sẽ là một lựa chọn hấp dẫn trong phân khúc flagship giá rẻ, với thiết kế đẹp mắt, hiệu năng mạnh mẽ và nhiều tính năng cao cấp. Bạn có mong chờ sự ra mắt chính thức của <strong>realme GT7</strong>?</p>
                        ",
                        NgayDang = new DateTime(2025, 1, 8),
                        HinhAnh = "realme-gt7.jpg"
                    },
                    new BaiViet
                    {
                        TieuDe = "Dòng Samsung Galaxy S25 sẽ mang đến nhiều tính năng AI đầy mới lạ",
                        NoiDung = @"
                            <p><strong>Dòng Samsung Galaxy S25</strong> được cho là sẽ được Samsung trình làng vào ngày 22/1 tới đây. Mới đây, một nguồn tin đã tiết lộ rằng người dùng mua Galaxy S25 Series sẽ được trải nghiệm miễn phí Gemini Advanced, một tính năng tương tự như trên dòng smartphone Google Pixel 9.</p>

                            <p>Theo đó, chương trình trải nghiệm miễn phí này hoàn toàn hợp lý vì dòng Galaxy sắp tới được nhiều người đồn đoán sẽ là mẫu smartphone AI cực kỳ mạnh mẽ. Sức mạnh này được kỳ vọng sẽ đến từ <strong>chip Snapdragon 8 Elite</strong>, giúp Galaxy S25 Series đáp ứng nhiều mục đích sử dụng khác nhau.</p>

                            <h2>Sức mạnh xử lý AI đến từ Snapdragon 8 Elite</h2>
                            <p>Sức mạnh xử lý AI của Galaxy S25 Series sẽ đến từ vi xử lý Snapdragon 8 Elite, mang lại hiệu năng vượt trội để hỗ trợ các tính năng AI tiên tiến. Điều này không chỉ giúp tối ưu hóa trải nghiệm người dùng mà còn mở ra nhiều khả năng ứng dụng mới trong việc quay, chụp ảnh và chỉnh sửa nội dung.</p>

                            <h2>Nhiều tính năng AI hoàn toàn mới</h2>
                            <p>Bên cạnh Gemini Advanced miễn phí, các công cụ AI đã xuất hiện trước đó cũng sẽ được tích hợp. Tuy nhiên, nhiều tin đồn cho rằng Samsung sẽ mang đến những tính năng AI mới mẻ và độc đáo mà chưa từng có ai nghĩ đến. Đây có thể là bước đi của Samsung nhằm giảm sự phụ thuộc vào công nghệ AI của Google.</p>
                            <p>Leaker nổi tiếng <strong>Ice Universe</strong> đã đăng tải trên X/Twitter rằng Samsung đang phát triển những tính năng AI có thể khiến hãng bỏ xa các đối thủ. Hiện tại, nhiều tính năng vẫn còn bí ẩn, nhưng dự kiến chúng sẽ tập trung vào cải thiện khả năng quay phim, chụp ảnh và tự động hóa các tác vụ hằng ngày.</p>

                            <h2>Chờ đợi sự ra mắt của Galaxy S25 Series</h2>
                            <p>Leaker Ice Universe cũng dự đoán rằng Samsung có thể sẽ tích hợp AI vào các công cụ chỉnh sửa ảnh và hỗ trợ tự động hoàn thành công việc cho người dùng. Những tính năng này được cho là tương đồng với những gì Google dự kiến mang đến trên Android 16.</p>
                            <p>Chúng ta có thể cần chờ thêm vài tuần nữa để Galaxy S25 Series chính thức ra mắt cùng hàng loạt tính năng AI độc đáo. Tuy nhiên, các thông tin chi tiết có thể bị rò rỉ sớm hơn trong những ngày tới.</p>

                            <h2>Kết luận</h2>
                            <p>Bạn có mong đợi sự ra mắt của <strong>Galaxy S25 Series</strong> không? Trong thời gian chờ đợi, bạn có thể đặt mua Samsung Galaxy S24, Galaxy S24 Plus hoặc Galaxy S24 Ultra tại Thế Giới Di Động để tận hưởng nhiều ưu đãi hấp dẫn!</p>
                        ",
                        NgayDang = new DateTime(2025, 1, 8),
                        HinhAnh = "galaxy-s25.jpg"
                    }
                };
                _db.BaiViet.AddRange(BaiViet);
                _db.SaveChanges();
            }


        }
    }
}
