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
                        MoTa = "iPhone 14 với thiết kế hiện đại và hiệu năng vượt trội. Sản phẩm được trang bị chip A15 Bionic mạnh mẽ, camera kép 12MP sắc nét, và thời lượng pin ổn định. Với màn hình 6.1 inch Liquid Retina, iPhone 14 mang lại trải nghiệm tuyệt vời cho người dùng yêu thích sự nhỏ gọn nhưng không kém phần sang trọng.",
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
                        TanSoQuet = "120Hz",
                        Gia = 22990000,
                        SoLuongTon = 50 // Số lượng tồn kho
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 2,
                        TEN_SP = "Samsung Galaxy S23",
                        HinhAnhSP = "samsung_s23.jpg",
                        MoTa = "Samsung Galaxy S23 mang đến một thiết kế hoàn hảo với màn hình Dynamic AMOLED 2X 6.1 inch, đảm bảo độ sắc nét cao. Máy được trang bị chip Snapdragon 8 Gen 2 mạnh mẽ, hệ thống camera ấn tượng 50MP và dung lượng pin tối ưu 3900mAh, cho trải nghiệm lâu dài trong suốt cả ngày.",
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
                        TanSoQuet = "120Hz",
                        Gia = 23990000,
                        SoLuongTon = 100 // Số lượng tồn kho
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 3,
                        TEN_SP = "Xiaomi 13",
                        HinhAnhSP = "xiaomi_13.jpg",
                        MoTa = "Xiaomi 13 là sự kết hợp hoàn hảo giữa thiết kế sang trọng và hiệu năng mạnh mẽ. Với chipset Snapdragon 8 Gen 2, RAM 12GB và bộ nhớ trong 256GB, sản phẩm sẵn sàng đáp ứng mọi nhu cầu từ làm việc đến giải trí. Camera Leica 50MP, màn hình AMOLED 6.36 inch và pin 4500mAh giúp người dùng tận hưởng trải nghiệm tuyệt vời.",
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
                        TanSoQuet = "120Hz",
                        Gia = 17990000,
                        SoLuongTon = 150 // Số lượng tồn kho
                    },
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 4,
                        TEN_SP = "Oppo Find X6",
                        HinhAnhSP = "oppo_find_x6.jpg",
                        MoTa = "Oppo Find X6 sở hữu thiết kế tinh tế với công nghệ camera đỉnh cao. Camera sau 50MP mang lại hình ảnh sống động, chip Dimensity 9200 và RAM 16GB đảm bảo hiệu năng mượt mà. Sản phẩm nổi bật với màn hình AMOLED 6.8 inch và pin 4800mAh, lý tưởng cho cả công việc và giải trí.",
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
                        TanSoQuet = "120Hz",
                        Gia = 32990000,
                        SoLuongTon = 200 // Số lượng tồn kho
                    },
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 1,
                        TEN_SP = "iPhone 14 Pro Max",
                        HinhAnhSP = "iphone_14_pro_max.jpg",
                        MoTa = "iPhone 14 Pro Max là chiếc flagship hàng đầu của Apple, nổi bật với thiết kế Dynamic Island độc đáo, hiệu năng vượt trội nhờ chipset A16 Bionic, màn hình Super Retina XDR 6.7 inch sắc nét, cùng cụm camera chuyên nghiệp với độ phân giải cao lên đến 48MP. Đây là sự lựa chọn hoàn hảo cho những ai yêu thích công nghệ hiện đại.",
                        TGianBaoHanh = 12,
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
                        SoLuongTon = 200
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 2,
                        TEN_SP = "Samsung Galaxy Z Fold4",
                        HinhAnhSP = "samsung_z_fold4.jpg",
                        MoTa = "Samsung Galaxy Z Fold4 là mẫu điện thoại gập tiên tiến, mang lại trải nghiệm sử dụng độc đáo với màn hình lớn 7.6 inch. Được trang bị chip Snapdragon 8+ Gen 1 mạnh mẽ, camera chất lượng cao với khả năng quay video 4K, và thiết kế gọn gàng, Z Fold4 là lựa chọn lý tưởng cho người dùng yêu thích sự đổi mới.",
                        TGianBaoHanh = 12,
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
                        SoLuongTon = 300
                    },
                    new SanPham
                    {
                        MaNCC = 1,
                        MaDanhMuc = 3,
                        TEN_SP = "Xiaomi Redmi Note 12 Pro",
                        HinhAnhSP = "xiaomi_redmi_note_12_pro.jpg",
                        MoTa = "Xiaomi Redmi Note 12 Pro là smartphone tầm trung với nhiều tính năng cao cấp, bao gồm màn hình AMOLED 6.67 inch, cụm camera 108MP chụp ảnh sắc nét, và viên pin lớn 5000mAh hỗ trợ sạc nhanh. Đây là sản phẩm lý tưởng cho người dùng cần hiệu năng mạnh mẽ với giá cả phải chăng.",
                        TGianBaoHanh = 12,
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
                        SoLuongTon = 200
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 4,
                        TEN_SP = "Oppo Reno 9 Pro",
                        HinhAnhSP = "oppo_reno_9_pro.jpg",
                        MoTa = "Oppo Reno 9 Pro là dòng sản phẩm cao cấp với thiết kế mỏng nhẹ, màu sắc sang trọng, và camera selfie 32MP chuyên nghiệp. Sản phẩm được trang bị chipset Dimensity 8100 mạnh mẽ, màn hình OLED 6.7 inch, và pin dung lượng lớn, hỗ trợ sạc nhanh 65W.",
                        TGianBaoHanh = 12,
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
                        SoLuongTon = 150
                    },
                    new SanPham
                    {
                        MaNCC = 2,
                        MaDanhMuc = 1,
                        TEN_SP = "iPhone SE (2024)",
                        HinhAnhSP = "iphone_se_2024.jpg",
                        MoTa = "iPhone SE (2024) mang đến hiệu năng vượt trội với chip A15 Bionic trong một thiết kế nhỏ gọn. Sản phẩm có camera sắc nét, màn hình Retina HD 4.7 inch, và hỗ trợ cập nhật phần mềm lâu dài, phù hợp với người dùng yêu thích sự tiện lợi và hiệu quả.",
                        TGianBaoHanh = 12,
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
                        SoLuongTon = 400
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
                        Gia = 22990000,
                        MaSP = 1,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Trắng",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054091427671235_ip-14-trang-1.jpg",
                        Gia = 22990000,
                        MaSP = 1,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Tím",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054090260153672_ip-14-tim-1.jpg",
                        Gia = 22500000,
                        MaSP = 1,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Xanh dương",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054088819837718_ip-14-do-1.jpg",
                        Gia = 22890000,
                        MaSP = 1,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // samsung-galaxy-s23 (MASP 2)
                    new MauSac
                    {
                        TenMauSac = "Xanh lá",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_17_638122486969031761_samsung-galaxy-s23-xanh-4.jpg",
                        Gia = 23990000,
                        MaSP = 2,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Kem",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_17_638122486170765497_samsung-galaxy-s23-tim-4.jpg",
                        Gia = 23990000,
                        MaSP = 2,
                        SoLuongTon_MS = 30,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2024_5_15_638513664899586826_samsung-galaxy-s23-den-docquyen.jpg",
                        Gia = 23990000,
                        MaSP = 2,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Tím",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_17_638122486170765497_samsung-galaxy-s23-tim-4.jpg",
                        Gia = 23990000,
                        MaSP = 2,
                        SoLuongTon_MS = 30,
                        TrangThai = 1
                    },
                    // Xiaomi 13 (MASP 3)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_20_638125113177912131_xiaomi-13-den-5.jpg",
                        Gia = 17990000,
                        MaSP = 3,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Xanh dương",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_2_20_638125111639884515_xiaomi-13-xanh-5.jpg",
                        Gia = 17990000,
                        MaSP = 3,
                        SoLuongTon_MS = 20,
                        TrangThai = 1
                    },
                    //Oppo Find X6 (MASP 4)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_find_x8_space_black_1_6a9c3746b3.png",
                        Gia = 32990000,
                        MaSP = 4,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // iPhone 14 Pro Max (MASP 5)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054218956629637_ip-14-pro-max-den-1.jpg",
                        Gia = 33990000,
                        MaSP = 5,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Bạc",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054217303176240_ip-14-pro-max-bac-1.jpg",
                        Gia = 33990000,
                        MaSP = 5,
                        SoLuongTon_MS = 15,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Vàng",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054222139728415_ip-14-pro-max-vang-1.jpg",
                        Gia = 33990000,
                        MaSP = 5,
                        SoLuongTon_MS = 20,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Tím",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_11_30_638054220350691584_ip-14-pro-max-tim-1.jpg",
                        Gia = 33990000,
                        MaSP = 5,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // Samsung Galaxy Z Fold4 (MASP 6)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_8_10_637957713446350652_samsung-galaxy-z-fold4-den-2.jpg",
                        Gia = 41990000,
                        MaSP = 6,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Xanh đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_8_10_637957717267662603_samsung-galaxy-z-fold4-xanh-2.jpg",
                        Gia = 41990000,
                        MaSP = 6,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Đỏ",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_4_10_638167221773487338_samsung-galaxy-z-fold4-do-2.jpg",
                        Gia = 41990000,
                        MaSP = 6,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    // Xiaomi Redmi Note 12 Pro (MASP 7)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_3_22_638151013110509349_xiaomi-redmi-note-12-pro-trang-xam-5.jpg",
                        Gia = 9990000,
                        MaSP = 7,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Trắng",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2023_3_22_638151007412242456_xiaomi-redmi-note-12-pro-trang-5.jpg",
                        Gia = 9990000,
                        MaSP = 7,
                        SoLuongTon_MS = 70,
                        TrangThai = 1
                    },
                    // Oppo Reno 9 Pro (MASP 8)
                    new MauSac
                    {
                        TenMauSac = "Bạc",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_reno_12_trang_d776379731.jpg",
                        Gia = 17990000,
                        MaSP = 8,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Nâu",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_reno_12_den_f4fa1cfa2a.jpg",
                        Gia = 17990000,
                        MaSP = 8,
                        SoLuongTon_MS = 40,
                        TrangThai = 1
                    },
                    // iPhone SE (2024) (MASP 9)
                    new MauSac
                    {
                        TenMauSac = "Đen",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/2022_3_9_637823948840474346_iphone-se-2022-den-1.jpg",
                        Gia = 10990000,
                        MaSP = 9,
                        SoLuongTon_MS = 50,
                        TrangThai = 1
                    },
                    new MauSac
                    {
                        TenMauSac = "Bạc",
                        HinhAnhSP_MauSac = "https://cdn2.fptshop.com.vn/unsafe/384x0/filters:quality(100)/oppo_reno_12_trang_d776379731.jpg",
                        Gia = 10990000,
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
                        MATKHAU = "Lehonghoa@123"
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
                        MATKHAU = "Phanthanhson@123"
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
                        MATKHAU = "Vuminhhoang@123"
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
                        MATKHAU = "Dangngocbich@123"
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
                        MATKHAU = "Lytruonggiang@123"
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
                        MATKHAU = "Phamthuha@123"
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
                        MATKHAU = "Hoanganhtuan@123"
                    }
                };

                _db.KhachHang.AddRange(khachHangs);
                _db.SaveChanges();
            }

            // Dữ liệu Chi tiết bình luận
            if (!_db.CHI_TIET_BINH_LUAN.Any())
            {
                var chiTietBinhLuans = new List<CHI_TIET_BINH_LUAN>
                {
                    // Bình luận cho iPhone 14
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 6,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Thiết kế đẹp, hiệu năng mạnh mẽ. Rất đáng tiền!",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 6,
                        MA_KH = 3,
                        SO_SAO = 4,
                        NOI_DUNG = "Sản phẩm tốt nhưng thời lượng pin cần cải thiện.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Samsung Galaxy S23
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 7,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Màn hình đẹp, camera rất sắc nét. Rất hài lòng!",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 7,
                        MA_KH = 2,
                        SO_SAO = 3,
                        NOI_DUNG = "Hiệu năng tốt nhưng máy hơi nóng khi chơi game.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Xiaomi 13
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 8,
                        MA_KH = 1,
                        SO_SAO = 4,
                        NOI_DUNG = "Thiết kế đẹp, giá cả hợp lý. Nhưng camera không tốt lắm.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 8,
                        MA_KH = 2,
                        SO_SAO = 5,
                        NOI_DUNG = "Tuyệt vời! Hiệu năng rất ổn định.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Oppo Find X6
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 9,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Camera cực kỳ tốt, màn hình sáng rõ. Rất thích!",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 9,
                        MA_KH = 2,
                        SO_SAO = 4,
                        NOI_DUNG = "Thiết kế đẹp nhưng giá hơi cao.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho các sản phẩm mới thêm
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 9,
                        MA_KH = 3,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng tốt, thiết kế ổn. Rất đáng mua.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 2,
                        MA_KH = 1,
                        SO_SAO = 5,
                        NOI_DUNG = "Mua tặng bạn bè, mọi người đều rất thích!",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 8,
                        MA_KH = 3,
                        SO_SAO = 4,
                        NOI_DUNG = "Tính năng rất đa dạng, hiệu năng ổn định.",
                        NGAY = DateTime.Now
                    },

                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 1,
                        MA_KH = 4,
                        SO_SAO = 5,
                        NOI_DUNG = "Thiết kế gọn nhẹ, hiệu năng tốt.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 1,
                        MA_KH = 5,
                        SO_SAO = 4,
                        NOI_DUNG = "Sản phẩm tốt nhưng thời lượng pin cần cải thiện.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Samsung Galaxy S22
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 2,
                        MA_KH = 3,
                        SO_SAO = 5,
                        NOI_DUNG = "Hiệu năng tuyệt vời, thiết kế sang trọng.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 2,
                        MA_KH = 4,
                        SO_SAO = 3,
                        NOI_DUNG = "Camera tốt nhưng giá hơi cao.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Xiaomi Redmi Note 11
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 3,
                        MA_KH = 2,
                        SO_SAO = 4,
                        NOI_DUNG = "Giá cả hợp lý, pin rất tốt.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 3,
                        MA_KH = 5,
                        SO_SAO = 5,
                        NOI_DUNG = "Màn hình đẹp, hiệu năng ổn định.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Oppo Reno 8
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 4,
                        MA_KH = 3,
                        SO_SAO = 5,
                        NOI_DUNG = "Camera sắc nét, thiết kế đẹp.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 4,
                        MA_KH = 1,
                        SO_SAO = 4,
                        NOI_DUNG = "Sản phẩm tốt nhưng cần cải thiện phần mềm.",
                        NGAY = DateTime.Now
                    },

                    // Bình luận cho Vivo X80////////////////////////
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 5,
                        MA_KH = 2,
                        SO_SAO = 4,
                        NOI_DUNG = "Màn hình đẹp, thiết kế cao cấp.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 5,
                        MA_KH = 4,
                        SO_SAO = 5,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 5,
                        MA_KH = 8,
                        SO_SAO = 5,
                        NOI_DUNG = "Hiệu năng ổn định, mượt mà, màn hình sáng.",
                        NGAY = DateTime.Now
                    }, 
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 5,
                        MA_KH = 9,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 2,
                        MA_KH = 9,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 3,
                        MA_KH = 9,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 3,
                        MA_KH = 10,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },                    
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 1,
                        MA_KH = 10,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },                    
                    new CHI_TIET_BINH_LUAN
                    {
                        MA_SP = 1,
                        MA_KH = 7,
                        SO_SAO = 4,
                        NOI_DUNG = "Hiệu năng ổn định, camera rất tốt, máy mạnh, chơi game rất mượt, âm thanh lớn đánh giá 5 sao.",
                        NGAY = DateTime.Now
                    },
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