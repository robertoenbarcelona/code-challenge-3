
namespace Challenge3.UI
{
    /// <summary>
    /// Represent the user input interpreter for a given command
    /// </summary>
    public abstract class BaseCommandInterpreter
    {
        private readonly string key;
        private BaseCommandInterpreter successor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommandInterpreter"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        protected BaseCommandInterpreter(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// Gets the successor.
        /// </summary>
        protected internal BaseCommandInterpreter Successor { get { return this.successor; } }

        /// <summary>
        /// Sets the successor in the responsibility chain.
        /// </summary>
        /// <param name="successor">The successor of command chain.</param>
        public void SetSuccessor(BaseCommandInterpreter successor)
        {
            this.successor = successor;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="userNeed">The user need.</param>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        public virtual CommandResult HandleCommand(string userNeed)
        {
            if (userNeed == this.key)
            {
                return this.Handle();
            }
            else
            {
                if (this.Successor == null) { return new CommandResult(false, Properties.Resources.CommandUnrecognized); }
                else { return this.Successor.HandleCommand(userNeed); }
            }
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        protected abstract CommandResult Handle();
    }
}
