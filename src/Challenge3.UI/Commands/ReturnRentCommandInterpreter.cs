
namespace Challenge3.UI.Commands
{
    using Challenge3.AppService;
    using System;

    /// <summary>
    /// Interprets the Devolution command
    /// </summary>
    internal class ReturnRentCommandInterpreter : BaseCommandInterpreter
    {
        private const string CommandKey = Constants.ReturnRentKey;
        private readonly IAppRentService rentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter"/> class.
        /// </summary>
        public ReturnRentCommandInterpreter(IInputOutputDriver driver, IAppRentService rentService)
            : base(ReturnRentCommandInterpreter.CommandKey, driver) 
        {
            this.rentService = rentService;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="userNeed">The user need.</param>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        protected override CommandResult Handle()
        {
            try
            {
                base.Driver.Output(Properties.Resources.InformProduct);
                var productId = base.Driver.Input();
                base.Driver.Output(Properties.Resources.InformUser);
                var userID = base.Driver.Input();
                var result = this.rentService.ReturnProduct(productId, userID, DateTime.Now);
                return new CommandResult(result.Succeed, result.Message);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    }
}
