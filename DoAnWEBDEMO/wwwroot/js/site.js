
// Lấy các phần tử cần thiết
const openModalBtn = document.getElementById('openLoginModal');
const closeModalBtn = document.getElementById('closeLoginModal');
const modal = document.getElementById('loginModal');

// Mở modal
openModalBtn.addEventListener('click', () => {
    modal.classList.add('show');
});

// Đóng modal
closeModalBtn.addEventListener('click', () => {
    modal.classList.remove('show');
});

// Đóng modal nếu người dùng nhấn ra ngoài
window.addEventListener('click', (event) => {
    if (event.target === modal) {
        modal.classList.remove('show');
    }
});



function toggleChatBox() {
    document.getElementById('chatBox').classList.toggle('show');
}

// Hiển thị chat box khi nhấn nút
document.querySelector('.chat-btn').addEventListener('click', function () {
    toggleChatBox();
});

function showAnswer(answerId) {
    // Ẩn tất cả câu trả lời
    const allAnswers = document.querySelectorAll('.answer');
    allAnswers.forEach(answer => answer.style.display = 'none');

    // Hiển thị câu trả lời tương ứng
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


//Khách hàng Đăng nhập
$(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault(); 

        var taiKhoan = $('#text').val();  
        var matKhau = $('#password').val(); 

        $.ajax({
            url: '/KhachHang/getKhachHangDangNhap',  
            type: 'GET',
            data: { taiKhoan: taiKhoan, matKhau: matKhau },  
            success: function (response) {
                if (response.value) {
                    alert('Đăng nhập thành công!');
                    
                    window.location.href = '/home'; 
                } else {
                    alert('Tên người dùng hoặc mật khẩu không chính xác!');
                }
            },
            error: function () {
                alert('Có lỗi xảy ra khi đăng nhập.');
            }
        });
    });
});
