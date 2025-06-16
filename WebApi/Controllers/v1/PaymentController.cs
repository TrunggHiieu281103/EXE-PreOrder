

using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using VNPAY.NET.Enums;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseApiController
    {
        private readonly IVnpayPaymentService _vnpayPaymentService;
      

        public PaymentController(IVnpayPaymentService vnpayPaymentService)
        {
            _vnpayPaymentService = vnpayPaymentService;
        }

        /// <summary>
        /// Tạo URL thanh toán VNPAY
        /// </summary>
        /// <param name="amount">Số tiền thanh toán</param>
        /// <param name="orderDescription">Thông tin đơn hàng</param>
        /// <param name="orderId">ID đơn hàng</param>
        /// <param name="bankCode">Mã ngân hàng (tùy chọn)</param>
        /// <returns>URL thanh toán</returns>
        [HttpPost("create-payment-url")]
        public IActionResult CreatePaymentUrl(decimal amount, string orderDescription, long orderId, BankCode bankCode)
        {
            try
            {
                var paymentUrl = _vnpayPaymentService.CreatePaymentUrl(amount, orderDescription, orderId, bankCode);
                return Ok(new { paymentUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("IpnAction")]
        public IActionResult IpnAction()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpayPaymentService.HandleVnpayCallback(Request.Query);
                    if (paymentResult.IsSuccess)
                    {
                        // Thực hiện hành động nếu thanh toán thành công tại đây. Ví dụ: Cập nhật trạng thái đơn hàng trong cơ sở dữ liệu.
                        return Ok();
                    }

                    // Thực hiện hành động nếu thanh toán thất bại tại đây. Ví dụ: Hủy đơn hàng.
                    return BadRequest("Payment failed");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NotFound("Payment info not found.");
        }

        [HttpGet("vnpay-callback")]
        public IActionResult VnpayCallback()
        {
            try
            {
                var result = _vnpayPaymentService.HandleVnpayCallback(Request.Query);
                if (result.IsSuccess)
                {
                    // TODO: xử lý đơn hàng đã thanh toán thành công
                    return Ok(new
                    {
                        paymentId = result.PaymentId,
                        isSuccess = result.IsSuccess,
                        description = result.Description,
                        timestamp = result.Timestamp,
                        vnpayTransactionId = result.VnpayTransactionId,
                        paymentMethod = result.PaymentMethod,
                        paymentResponse = new
                        {
                            code = result.PaymentResponse.Code,
                            description = result.PaymentResponse.Description
                        },
                        transactionStatus = new
                        {
                            code = result.TransactionStatus.Code,
                            description = result.TransactionStatus.Description
                        },
                        bankingInfor = new
                        {
                            bankCode = result.BankingInfor.BankCode,
                            bankTransactionId = result.BankingInfor.BankTransactionId
                        }
                    });

                }
                else
                {
                    return BadRequest(new
                    {
                        status = result.PaymentResponse.Code,
                        message = result.PaymentResponse.Description
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }
}