
namespace Challenge3.UI.Commands
{
    using Challenge3.AppService;
    using System;

    /// <summary>
    /// Interprets the devolution command
    /// </summary>
    internal class RentProductCommandInterpreter: BaseCommandInterpreter
    {
        private const string CommandKey = Constants.RentKey;
        private readonly IAppRentService rentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter" /> class.
        /// </summary>
        /// <param name="driver">The input output driver.</param>
        public RentProductCommandInterpreter(IInputOutputDriver driver, IAppRentService rentService) :
            base(RentProductCommandInterpreter.CommandKey, driver) 
        {
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
                base.Driver.Output(Properties.Resources.InformProduct);
                var productId = base.Driver.Input();
                base.Driver.Output(Properties.Resources.InformUser);
                var userID = base.Driver.Input();
                var result = this.rentService.RentProduct(productId, userID);
                return new CommandResult(result.Succeed, result.Message);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    }
}
