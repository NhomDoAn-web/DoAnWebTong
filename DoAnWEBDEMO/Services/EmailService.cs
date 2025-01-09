using MailKit.Net.Smtp;
using MimeKit;

namespace DoAnWEBDEMO.Services
{
    public class EmailService
    {
        private readonly IConfiguration _cauHinh;

        public EmailService(IConfiguration cauHinh)
        {
            _cauHinh = cauHinh;
        }

        public async Task GuiOTP(string email_KH, string otp)
        {
            if (string.IsNullOrEmpty(email_KH))
            {
                throw new ArgumentNullException(nameof(email_KH), "Email không hợp lệ.");
            }

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Chăm sóc khách hàng", _cauHinh["EmailSettings:SenderEmail"]));
            email.To.Add(new MailboxAddress("Người nhận", email_KH));
            email.Subject = "TechLand - Lấy lại mật khẩu";
            email.Body = new TextPart("plain") { Text = $"Xin chào,\n\nMã OTP để xác minh của bạn là: {otp}. Vui lòng không chia sẻ mã này với bất kỳ ai.\n\nTrân trọng!" };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_cauHinh["EmailSettings:SmtpServer"], int.Parse(_cauHinh["EmailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_cauHinh["EmailSettings:SenderEmail"], _cauHinh["EmailSettings:SenderPassword"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }


    }
}
