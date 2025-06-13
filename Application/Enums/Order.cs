using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum OrderStatusEnum
    {
        [Description("Chờ xác nhận")]
        PENDING = 0,
        [Description("Đã xác nhận")]
        CONFIRM = 1,
        [Description("Đang giao hàng")]
        SHIPPING = 2,
        [Description("Hoàn tất")]
        COMPLETED = 3,
        [Description("Đã hủy")]
        CANCELED = 4
    }
}
