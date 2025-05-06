using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.SampleProject.Application.Contracts.Common;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TCommandResult"></typeparam>
public interface ICommandHandler<in TCommand, TCommandResult>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}

public interface ICommandHandler<in TCommand>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task Handle(TCommand command, CancellationToken cancellation);
}
