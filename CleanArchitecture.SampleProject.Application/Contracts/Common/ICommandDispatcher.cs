using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.SampleProject.Application.Contracts.Common;

/// <summary>
/// 
/// </summary>
public interface ICommandDispatcher
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TCommandResult"></typeparam>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation);
    Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation);
}


