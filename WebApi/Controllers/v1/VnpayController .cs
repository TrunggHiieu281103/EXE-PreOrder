

using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VNPAY.NET.Enums;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnpayController : BaseApiController
    {
        private readonly IVnpayPaymentService _vnpayPaymentService;
      

        public VnpayController(IVnpayPaymentService vnpayPaymentService)
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
        [Authorize]
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

        [HttpGet("Callback")]
        public async Task<IActionResult> Callback()
        {
            var result = await _vnpayPaymentService.HandleVnpayCallback(Request.Query);

            var frontendUrl = "http://gundam.thanhnt-tech.id.vn";

            if (result.IsSuccess)
            {
                var successUrl = $"{frontendUrl}/result" +
                                 $"?status=success" +
                                 $"&paymentId={result.PaymentId}" +
                                 $"&code={result.PaymentResponse?.Code}" +
                                 $"&description={Uri.EscapeDataString(result.Description)}" +
                                 $"&timestamp={result.Timestamp:O}" + // ISO 8601
                                 $"&vnpayTransactionId={result.VnpayTransactionId}" +
                                 $"&paymentMethod={result.PaymentMethod}" +
                                 $"&transactionCode={result.TransactionStatus?.Code}" +
                                 $"&transactionDescription={Uri.EscapeDataString(result.TransactionStatus?.Description ?? "")}" +
                                 $"&bankCode={result.BankingInfor?.BankCode}" +
                                 $"&bankTransactionId={result.BankingInfor?.BankTransactionId}";

                return Redirect(successUrl);
            }
            else
            {
                var failUrl = $"{frontendUrl}/result" +
                              $"?status=fail" +
                              $"&code={result.PaymentResponse?.Code}" +
                              $"&description={Uri.EscapeDataString(result.Description)}" +
                              $"&transactionCode={result.TransactionStatus?.Code}" +
                              $"&transactionDescription={Uri.EscapeDataString(result.TransactionStatus?.Description ?? "")}";

                return Redirect(failUrl);
            }
        }


        [HttpGet("IpnAction")]
        public async Task<IActionResult> IpnAction()
        {
            var result = await _vnpayPaymentService.HandleVnpayCallback(Request.Query);

            if (result.IsSuccess)
            {
                return Ok("IPN SUCCESS");
            }

            return BadRequest("IPN FAILED");
        }
    }
}