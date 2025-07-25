using Application.DTOs.DashBoard;
using Application.Enums;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.GetRevenueByDate
{
    public class GetDailyRevenueByMonthQuery : IRequest<List<DailyRevenueDto>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
    

        public class GetDailyRevenueByMonthHandler : IRequestHandler<GetDailyRevenueByMonthQuery, List<DailyRevenueDto>>
        {
        private readonly IOrderRepositoryAsync _orderRepository;

        public GetDailyRevenueByMonthHandler(IOrderRepositoryAsync orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<DailyRevenueDto>> Handle(GetDailyRevenueByMonthQuery request, CancellationToken cancellationToken)
        {
            var year = request.Year;
            var month = request.Month;

            var firstDay = new DateTime(year, month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            var firstDayMillis = new DateTimeOffset(firstDay).ToUnixTimeMilliseconds();
            var lastDayMillis = new DateTimeOffset(lastDay).AddDays(1).AddTicks(-1).ToUnixTimeMilliseconds(); // cuối ngày

            // Lấy tất cả đơn hàng có trạng thái COMPLETED trong khoảng thời gian tháng đó
            var orders = await _orderRepository.GetOrdersWithPaymentsAsync(
                o => o.Status == OrderStatusEnum.COMPLETED.ToString()
                  && o.CreatedAt >= firstDayMillis
                  && o.CreatedAt <= lastDayMillis
            );

            // Tính doanh thu theo ngày
            var revenueByDay = orders
                .Select(o => new
                {
                    Date = DateTimeOffset.FromUnixTimeMilliseconds(o.CreatedAt).Date,
                    Revenue = (o.ShippingFee ?? 0) + (o.TotalPrice ?? 0)
                })
                .GroupBy(x => x.Date)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Revenue));

            // Tạo danh sách đầy đủ cho từng ngày trong tháng
            var result = new List<DailyRevenueDto>();
            for (var date = firstDay; date <= lastDay; date = date.AddDays(1))
            {
                result.Add(new DailyRevenueDto
                {
                    Day = date.ToString("yyyy-MM-dd"),
                    TotalRevenue = revenueByDay.ContainsKey(date) ? revenueByDay[date] : 0
                });
            }

            return result;
        }
    }
    
}
