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
    public interface IQueryDispatcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TQueryResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation);
    }
}
