
namespace Challenge3.UI
{
    /// <summary>
    /// Represent the user input interpreter for a given command
    /// </summary>
    public abstract class BaseCommandInterpreter
    {
        private BaseCommandInterpreter successor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommandInterpreter"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        protected BaseCommandInterpreter(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        protected string Key { get; private set; }

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
        public CommandResult HandleCommand(string userNeed)
        {
            if (userNeed == this.Key)
            {
                return this.Handle();
            }
            else
            {
                if (this.successor == null) { return new CommandResult(false, Properties.Resources.CommandUnrecognized); }
                else { return this.successor.HandleCommand(userNeed); }
            }
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        protected abstract CommandResult Handle();
    }
}
