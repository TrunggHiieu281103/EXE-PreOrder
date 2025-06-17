using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum PaymentTypeEnum
    {
        [Description("Thanh toán khi nhận hàng (COD)")]
        COD = 0,

        [Description("Thanh toán qua VNPAY")]
        VNPAY = 1
    }

    public enum PaymentStatusEnum
    {
        PENDING = 0,
        SUCCESS = 1,
        FAILED = 2,
        CANCELED = 4
    }
}
