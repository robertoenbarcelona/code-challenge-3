
namespace Challenge3.UI
{
    using System;

    /// <summary>
    /// Interprets the Devolution command
    /// </summary>
    internal class ReturnProductCommandInterpreter : BaseCommandInterpreter
    {
        private const string CommandKey = "D";

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter"/> class.
        /// </summary>
        public ReturnProductCommandInterpreter() : base(ReturnProductCommandInterpreter.CommandKey) { }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="userNeed">The user need.</param>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        protected override CommandResult Handle()
        {
            try
            {
                // instatiate biz comand
                // ejecute it
                // get response
                return new CommandResult(false, "Fake");
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    }
}
