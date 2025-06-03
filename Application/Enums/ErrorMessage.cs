using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum ErrorMessage
    {
        // Redis
        Redis_Connection_Failed,
        Redis_Timeout,

        // Email
        Email_Name_Invalid,

        // OTP
        Otp_Expried,
        Otp_Incorrect,

        // Auth
        Invalid_Email_Or_Phone,
        Incorrect_Password,
        Email_Or_Phone_Already_Registered,
        Confirm_Password_Mismatch,
        Default_Role_Not_Found,

        // Generic
        Unknown_Error,
        Unauthorized_Access,
        Forbidden_Access
    }

    public static class ErrorMessageExtensions
    {
        public static string GetMessage(this ErrorMessage error)
        {
            return error switch
            {
                // Redis
                ErrorMessage.Redis_Connection_Failed => "Không thể kết nối tới Redis server.",
                ErrorMessage.Redis_Timeout => "Redis bị timeout khi xử lý yêu cầu.",

                // Email
                ErrorMessage.Email_Name_Invalid => "Email không hợp lệ.",

                // OTP
                ErrorMessage.Otp_Expried => "OTP hết hạn.",
                ErrorMessage.Otp_Incorrect => "OTP không chính xác.",

                // Auth
                ErrorMessage.Invalid_Email_Or_Phone => "Email hoặc số điện thoại không đúng.",
                ErrorMessage.Incorrect_Password => "Mật khẩu không đúng.",
                ErrorMessage.Email_Or_Phone_Already_Registered => "Email hoặc số điện thoại đã được đăng ký.",
                ErrorMessage.Confirm_Password_Mismatch => "Xác nhận mật khẩu không khớp.",
                ErrorMessage.Default_Role_Not_Found => "Không tìm thấy quyền mặc định. Vui lòng seed dữ liệu vai trò.",

                // Generic
                ErrorMessage.Unknown_Error => "Đã xảy ra lỗi không xác định.",
                ErrorMessage.Unauthorized_Access => "Bạn không có quyền truy cập.",
                ErrorMessage.Forbidden_Access => "Bạn bị từ chối quyền truy cập vào tài nguyên này.",

                _ => "Lỗi không xác định."
            };
        }
    }
}
