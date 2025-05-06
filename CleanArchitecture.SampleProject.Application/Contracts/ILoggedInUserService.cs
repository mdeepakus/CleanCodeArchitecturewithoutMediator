namespace CleanArchitecture.SampleProject.Application.Contracts
{
    /// <summary>
    /// Interface for logged in user service
    /// </summary>
    public interface ILoggedInUserService
    {
        /// <summary>
        /// Gets the userid for the logged in user.
        /// </summary>
        public string UserId { get; }
    }
}
