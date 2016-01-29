
namespace Challenge3.UI.Commands
{
    using Challenge3.AppService;
    using System;

    internal class HireProductCommandInterpreter: BaseCommandInterpreter
    {
        private const string CommandKey = Constants.HireKey;
        private readonly IInputOutputDriver driver;
        private readonly IAppRentService rentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter" /> class.
        /// </summary>
        /// <param name="driver">The input output driver.</param>
        public HireProductCommandInterpreter(IInputOutputDriver driver, IAppRentService rentService) :
            base(HireProductCommandInterpreter.CommandKey) 
        {
            this.driver = driver;
            this.rentService = rentService;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        protected override CommandResult Handle()
        {
            try
            {
                this.driver.Output("Please inform product:");
                var bookId = this.driver.Input();
                this.driver.Output("Please inform user:");
                var userID = this.driver.Input();
                var result = rentService.Hire(bookId, userID);
                return new CommandResult(result.Succeed, result.Message);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    }
}
