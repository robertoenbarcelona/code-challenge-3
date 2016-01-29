
namespace Challenge3.AppService
{
    /// <summary>
    /// Result message
    /// </summary>
    public class OperationMessage
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OperationMessage"/> is succeed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succeed; otherwise, <c>false</c>.
        /// </value>
        public bool Succeed { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }
}
