
namespace Challenge3.UI
{
using System;

    internal class HireProductCommandInterpreter: BaseCommandInterpreter
    {
        private const string CommandKey = "P";

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter"/> class.
        /// </summary>
        public HireProductCommandInterpreter() : base(HireProductCommandInterpreter.CommandKey) { }

        /// <summary>
        /// Handles the command.
        /// </summary>
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
