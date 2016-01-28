
namespace Challenge3.UI.Commands
{
    using System;

    /// <summary>
    /// Interprets the Hire command
    /// </summary>
    internal class RegisterProductCommandInterpreter : BaseCommandInterpreter
    {
        private const string CommandKey = "R";

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter"/> class.
        /// </summary>
        public RegisterProductCommandInterpreter() : base(RegisterProductCommandInterpreter.CommandKey) { }

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
