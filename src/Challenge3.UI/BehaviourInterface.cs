
namespace Challenge3.UI
{
    using System;

    /// <summary>
    /// Handle user interaction for main feature from a console
    /// </summary>
    internal class BehaviourInterface : IHumanToSoftwareInterface
    {
        private readonly BaseCommandInterpreter interpreter;
        private readonly IInputOutputDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviourInterface"/> class.
        /// </summary>
        /// <param name="interpreter">The interpreter.</param>
        public BehaviourInterface(BaseCommandInterpreter interpreter, IInputOutputDriver driver)
        {
            this.driver = driver;
            this.interpreter = interpreter;
        }

        /// <summary>
        /// Handles the user needs.
        /// </summary>
        public bool HandleUserNeeds()
        {
            this.driver.Output(Properties.Resources.AskInputMessage);
            return this.InterpretCommand();
        }

        private bool InterpretCommand()
        {
            try
            {
                string userNeed = this.driver.Input();
                CommandResult result = this.interpreter.HandleCommand(userNeed);
                this.driver.Output(String.Format(Properties.Resources.OperationResult, result.HasSucceed ? Properties.Resources.Succeed : Properties.Resources.Rollback, result.Message));
                return !result.IsTerminating;
            }
            catch (Exception ex)
            {
                this.driver.Output(String.Format(Properties.Resources.Error, ex.Message));
                return true;
            }
        }
    }
}
