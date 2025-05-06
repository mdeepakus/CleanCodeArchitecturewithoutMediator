namespace CleanArchitecture.SampleProject.Api.Utility
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class FileResultContentTypeAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        public FileResultContentTypeAttribute(string contentType)
        {
            ContentType = contentType;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContentType { get; }
    }
}
