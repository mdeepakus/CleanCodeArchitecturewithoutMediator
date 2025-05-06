using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using Mapster;

namespace CleanArchitecture.SampleProject.Application.Features.Orders.GetOrdersForMonth
{
    public class GetOrdersForMonthQueryHandler : IQueryHandler<GetOrdersForMonthQuery, PagedOrdersForMonthVm>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersForMonthQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<PagedOrdersForMonthVm> Handle(GetOrdersForMonthQuery request, CancellationToken cancellationToken)
        {
            var list = await _orderRepository.GetPagedOrdersForMonth(request.Date, request.Page, request.Size);
            var orders = list.Adapt<List<OrdersForMonthDto>>(); 

            var count = await _orderRepository.GetTotalCountOfOrdersForMonth(request.Date);
            return new PagedOrdersForMonthVm() { Count = count, OrdersForMonth = orders, Page = request.Page, Size = request.Size };
        }
    }
}
