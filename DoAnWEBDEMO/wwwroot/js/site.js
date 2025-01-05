
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

const checkDangNhap = document.querySelector('.check-dang-nhap');

if (checkDangNhap) {
    checkDangNhap.addEventListener("click", () => {
        alert("Vui lòng đăng nhập để thực hiện thao tác này!");
    })
}

if (openModalBtn) {
    openModalBtn.addEventListener('click', () => {
        modal.classList.add('show');
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
$(document).ready(function () {

    $.ajax({
        url: '/DanhMuc/getDanhMuc', 
        type: 'GET',
        success: function (data) {
            
            var dsDanhMuc = $('#ds-danhmuc');
            dsDanhMuc.empty(); 

            data.forEach(function (item) {
                dsDanhMuc.append('<li><a class="dropdown-item nav-danhmuc" href="#">' + item + '</a></li>');
            });
        },
        error: function (error) {
            console.log("Có lỗi xảy ra khi lấy dữ liệu danh mục:", error);
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
                    window.location.href = '/home';
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

if (btnIconLogin) {
    btnIconLogin.addEventListener("click", hienThiCaptcha);
}

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

btnLogin.addEventListener("click", kiemTraChuoiCaptcha);



showPasswordCheckbox.addEventListener("change", () => {
    matKhauKhachHang.type = showPasswordCheckbox.checked ? "text" : "password";
});

// Tạo captcha khi tải trang
hienThiCaptcha();



//Box thông tin tài khoản
document.getElementById("accountButton").addEventListener("click", function (event) {
    event.stopPropagation();
    document.getElementById("accountBox").style.display = "block";
});

document.addEventListener("click", function (event) {
    var accountBox = document.getElementById("accountBox");

    if (!accountBox.contains(event.target) && event.target !== document.getElementById("accountButton")) {
        accountBox.style.display = "none";
    }
});
    
//AJAX đăng xuât
document.getElementById("logoutButton").addEventListener("click", function () {

    var xacNhanDangXuat = confirm("Bạn muốn thoát tài khoản?");

    if (xacNhanDangXuat) {
        $.ajax({
            url: '/KhachHang/khachHangDangXuat',
            type: 'POST',
            success: function (response) {
                if (response.value) {
                    window.location.href = '/home';
                }
            },
            error: function () {
                alert('Có lỗi xảy ra khi đăng xuất.');
            }
        });
    }
    

})
