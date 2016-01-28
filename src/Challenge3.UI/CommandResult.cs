
namespace Challenge3.UI
{
    /// <summary>
    /// Implemente the operation result
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult"/> class.
        /// </summary>
        /// <param name="hasSucceed">if set to <c>true</c> [has succeed].</param>
        /// <param name="message">The message.</param>
        public CommandResult(bool hasSucceed, string message)
        {
            this.HasSucceed = hasSucceed;
            this.Message = message;
        }

        /// <summary>
        /// Gets a value indicating whether the command instance has succeed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has succeed; otherwise, <c>false</c>.
        /// </value>
        public bool HasSucceed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is terminating.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is terminating; otherwise, <c>false</c>.
        /// </value>
        public bool IsTerminating { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message to be devolved to user.
        /// </value>
        public string Message { get; private set; }
    }
}
