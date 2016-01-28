
namespace Challenge3.UI.Commands
{
using System;

    internal class HireProductCommandInterpreter: BaseCommandInterpreter
    {
        private const string CommandKey = "P";
        private readonly IInputOutputDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter" /> class.
        /// </summary>
        /// <param name="driver">The input output driver.</param>
        public HireProductCommandInterpreter(IInputOutputDriver driver) : base(HireProductCommandInterpreter.CommandKey) 
        {
            this.driver = driver;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        protected override CommandResult Handle()
        {
            try
            {
                this.driver.Output("Please inform product Id:");
                var bookId = this.driver.Input();
                this.driver.Output("Please inform user Id:");
                var userID = this.driver.Input();

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
