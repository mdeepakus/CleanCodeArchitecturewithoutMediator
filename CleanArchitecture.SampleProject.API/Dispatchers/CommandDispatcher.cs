using CleanArchitecture.SampleProject.Application.Contracts.Common;

namespace CleanArchitecture.SampleProject.API.Dispatchers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TCommandResult"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
            return handler.Handle(command, cancellation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return handler.Handle(command, cancellation);
        }
    }
}
