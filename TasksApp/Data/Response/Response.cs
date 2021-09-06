namespace TasksApp.Data.Response
{
    /// <summary>
    /// Responding to user request
    /// </summary>
    public class Response<T>
    {
        /// <summary>
        /// Success of the request
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Error in request processing
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        public T Data { get; set; }
    }
}