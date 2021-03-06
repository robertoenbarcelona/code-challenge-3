﻿
namespace Challenge3.UI.Commands
{
    using Challenge3.AppService;
    using System;

    /// <summary>
    /// Interprets the Hire command
    /// </summary>
    internal class RegisterProductCommandInterpreter : BaseCommandInterpreter
    {
        private const string CommandKey = Constants.RegisterProductKey;
        private readonly IAppProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="productService">The product service.</param>
        public RegisterProductCommandInterpreter(IInputOutputDriver driver, IAppProductService productService) :
            base(RegisterProductCommandInterpreter.CommandKey, driver)
        {
            this.productService = productService;
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
                base.Driver.Output(Properties.Resources.InformName);
                var userID = base.Driver.Input();
                var result = this.productService.Register(productId, userID);
                return new CommandResult(result.Succeed, result.Message);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    }
}
