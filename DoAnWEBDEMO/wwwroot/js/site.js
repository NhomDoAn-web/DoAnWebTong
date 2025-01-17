
const openModalBtn = document.getElementById('openLoginModal');
const closeModalBtn = document.getElementById('closeLoginModal');
const modal = document.getElementById('loginModal');

const chuoiCaptcha = document.querySelector(".chuoiCaptcha");
const nhapCaptcha = document.getElementById("nhapCaptcha");
const thongBaoCaptcha = document.getElementById("thongBaoCaptcha");
const taoCaptcha = document.getElementById("taoCaptcha");
const kiemTraCaptcha = document.getElementById("kiemTraCaptcha");
const btnLogin = document.querySelector(".btn-login");
const btnIconLogin = document.querySelector(".btn-icon-login");
const taiKhoanKhachHang = document.getElementById("taiKhoanKhachHang");
const matKhauKhachHang = document.getElementById("matKhauKhachHang");
const showPasswordCheckbox = document.getElementById("showPassword");
var accountButton = document.getElementById("accountButton");
var logoutButton = document.getElementById("logoutButton");
const checkDangNhap = document.querySelector('.check-dang-nhap');

if (checkDangNhap)
{
    checkDangNhap.addEventListener("click", () =>
    {
        alert("Vui lòng đăng nhập để thực hiện thao tác này!");
    })
}
if (openModalBtn) {
    openModalBtn.addEventListener('click', () => {
        modal.classList.add('show');
        hienThiCaptcha();
    });
}

if (closeModalBtn) {
    closeModalBtn.addEventListener('click', () => {
        modal.classList.remove('show');
        taiKhoanKhachHang.value = matKhauKhachHang.value = "";
    });
}



function toggleChatBox() {
    document.getElementById('chatBox').classList.toggle('show');
}

document.querySelector('.chat-btn').addEventListener('click', function () {
    toggleChatBox();
});

function showAnswer(answerId) {
    const allAnswers = document.querySelectorAll('.answer');
    allAnswers.forEach(answer => answer.style.display = 'none');

    const answer = document.getElementById(answerId);
    answer.style.display = 'block';
}




//Danh mục
//$(document).ready(function () {

//    $.ajax({
//        url: '/DanhMuc/getDanhMuc', 
//        type: 'GET',
//        success: function (data) {

//            var dsDanhMuc = $('#ds-danhmuc');
//            dsDanhMuc.empty(); 

//            data.forEach(function (item) {
//                dsDanhMuc.append('<li><a class="dropdown-item nav-danhmuc" href="/TrangChu/TimKiemSanPham?DanhMucId=' + item.ma_DM + '">' + item.tenDM + '</a></li>');
//            });
//        },
//        error: function (error) {
//            console.log("Có lỗi xảy ra khi lấy dữ liệu danh mục:", error);
//        }
//    });
//});


//Trang chu
//Header
$(document).ready(function () {

    $.ajax({
        url: '/Layout/getDataHeader',
        type: 'GET',
        success: function (data) {

            var dsHeader = $('#ds-header');
            var dsAccount = $('#ds-account');
            var menuFooter = $('#menu-header');

            dsHeader.empty();
            dsAccount.empty();
            menuFooter.empty();

            data.forEach(function (item) {
                if (item.viTriHienThi == 2)
                {
                    dsHeader.append('<li><a href="' + item.duongLienKet + '" style="font-weight: 400;" class="mx-3 nav-header menu-link text-dark nav-link px-2">' + item.tieuDe + '</a></li>');
                    menuFooter.append('<li class="my-2"><a href="' + item.duongLienKet + '" class="my-4 text-dark text-decoration-none nav-footer">' + item.tieuDe + '</a></li>');
                }
                if (item.viTriHienThi == 3)
                {
                    dsAccount.append('<a href="' + item.duongLienKet +'" class="me-2 btn-header text-dark text-decoration-none fs-3 mx-1">'+ item.icon+'</a>')
                }
            });
        },
        error: function (error) {
            console.log("Có lỗi xảy ra khi lấy dữ liệu:", error);
        }
    });
});

//Trang chu
//Footer
$(document).ready(function () {

    $.ajax({
        url: '/Layout/getDataFooter',
        type: 'GET',
        success: function (data) {

            var dsFooter = $('#ds-footer');
            dsFooter.empty();

            data.forEach(function (item) {

                var htmlContent = '<div class="col-md-4 mb-3">';

                if (item.tieuDe) {
                    htmlContent += '<h5 class="fw-bold">' + item.tieuDe + '</h5>';
                }

                if (item.moTa) {
                    htmlContent += '<p>' + item.moTa + '</p>';
                }

                if (item.hinhAnh) {
                    htmlContent += '<img src="' + item.hinhAnh + '" alt="Hình ảnh" class="" style="width:60px">';
                }

                if (item.duongLienKet) {
                    htmlContent += '<a href="' + item.duongLienKet + '" class="text-decoration-none" target="_blank"> Xem thêm</a>';
                }

                htmlContent += '</div>';

                dsFooter.append(htmlContent);

            });
        },
        error: function (error) {
            console.log("Có lỗi xảy ra khi lấy dữ liệu:", error);
        }
    });
});


// Captcha chuỗi ký tự ngẫu nhiên


function taoChuoiNgauNhien() {
    const kyTu = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    let ketQua = "";
    for (let i = 0; i < 5; i++) {
        ketQua += kyTu.charAt(Math.floor(Math.random() * kyTu.length));
    }
    return ketQua;
}

function hienThiCaptcha() {
    chuoiCaptcha.textContent = taoChuoiNgauNhien();
    nhapCaptcha.value = "";
    thongBaoCaptcha.textContent = "";
}

function kiemTraChuoiCaptcha() {
    if (nhapCaptcha.value === chuoiCaptcha.textContent) {
        console.log("check")
        const taiKhoan = taiKhoanKhachHang.value;
        const matKhau = matKhauKhachHang.value;

        $.ajax({
            url: '/KhachHang/getKhachHangDangNhap',
            type: 'POST', 
            data: { taiKhoan: taiKhoan, matKhau: matKhau },
            success: function (response) {
                if (response.value) {
                    alert('Đăng nhập thành công!');
                    location.reload();
                } else {
                    alert('Tên người dùng hoặc mật khẩu không chính xác!');
                    hienThiCaptcha();
                }
            },
            error: function () {
                alert('Có lỗi xảy ra khi đăng nhập.');
            }
        });
    }
    else
    {
        thongBaoCaptcha.textContent = "Captcha không đúng!";
        thongBaoCaptcha.style.color = "red";
    }
}

btnLogin.addEventListener("click", kiemTraChuoiCaptcha);

if (taoCaptcha) {
    taoCaptcha.addEventListener("click", hienThiCaptcha);
}


document.querySelectorAll('.input-taikhoan').forEach(item => {
    item.addEventListener("keydown", event => {
        if (event.key === "Enter")
        {
            if (item.value.trim() === "") {
                alert("Vui lòng nhập thông tin trước khi tiếp tục!");
                return;
            }
            else
            {
                btnLogin.click();
            }
        }
    })
})





showPasswordCheckbox.addEventListener("change", () => {
    matKhauKhachHang.type = showPasswordCheckbox.checked ? "text" : "password";
});

// Tạo captcha khi tải trang
hienThiCaptcha();



//Box thông tin tài khoản
if (accountButton) {
    accountButton.addEventListener("click", function (event) {
        event.stopPropagation();
        document.getElementById("accountBox").style.display = "block";
    });

    document.addEventListener("click", function (event) {
        var accountBox = document.getElementById("accountBox");
        if (!accountBox.contains(event.target) && event.target !== accountButton) {
            accountBox.style.display = "none";
        }
    });

}


//AJAX đăng xuât

if (logoutButton) {
    logoutButton.addEventListener("click", function () {

        var xacNhanDangXuat = confirm("Bạn muốn thoát tài khoản?");

        if (xacNhanDangXuat) {
            $.ajax({
                url: '/KhachHang/khachHangDangXuat',
                type: 'POST',
                success: function (response) {
                    if (response.value) {
                        location.reload();
                    }
                },
                error: function () {
                    alert('Có lỗi xảy ra khi đăng xuất.');
                }
            });
        }
    })
}

