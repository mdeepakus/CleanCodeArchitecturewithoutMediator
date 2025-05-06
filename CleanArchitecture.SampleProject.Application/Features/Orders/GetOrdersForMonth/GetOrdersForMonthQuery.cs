namespace CleanArchitecture.SampleProject.Application.Features.Orders.GetOrdersForMonth
{
    public class GetOrdersForMonthQuery 
    {
        public DateTime Date { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
