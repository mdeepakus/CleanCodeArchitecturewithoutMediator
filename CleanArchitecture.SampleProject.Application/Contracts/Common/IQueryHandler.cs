using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.SampleProject.Application.Contracts.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TQueryResult"></typeparam>
    public interface IQueryHandler<in TQuery, TQueryResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);

    }
}
