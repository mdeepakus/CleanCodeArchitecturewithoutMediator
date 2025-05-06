namespace CleanArchitecture.SampleProject.Application.Exceptions
{
    /// <summary>
    /// ApiException Class
    /// </summary>
    public class ApiException : Exception

    {
        /// <summary>
        /// Constructor of ApiException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <param name="errors"></param>
        /// <param name="ex"></param>
        public ApiException(string message, int statusCode = 400, List<ValidationError> errors = null, Exception ex = null) : base(message, ex)
        {
            StatusCode = statusCode;
            Errors = errors ?? new List<ValidationError>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        protected ApiException(string message, Exception ex)
            : base(message, ex)
        {
        }

        /// <summary>
        /// Specifies the value of Httpstatus code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Specifies the list of validation error 
        /// </summary>
        public List<ValidationError> Errors { get; set; }
    }
}

/// <summary>
/// 
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Constructor of ValidationError class
    /// </summary>
    /// <param name="message"></param>
    /// <param name="target"></param>
    public ValidationError(string message, string target)
    {
        Message = message;
        Target = target;
    }

    /// <summary>
    /// Specifies the validation error message
    /// </summary>
    /// <value>The error message</value>
    public string Message { get; set; }

    /// <summary>
    /// The name of the field that this error relates to.
    /// </summary>
    /// <value>Target of the error</value>
    public string Target { get; set; }
}

