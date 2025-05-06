using CleanArchitecture.SampleProject.Application.Contracts.Common;

namespace CleanArchitecture.SampleProject.API.Dispatchers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TQueryResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            return handler.Handle(query, cancellation);
        }
    }
}
