
namespace Challenge3.UITests
{
    using Challenge3.UI;
    using FakeItEasy;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class TestCommandInterpreter : BaseCommandInterpreter
    {
        private const string CommandKey = Constants.TestKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterProductCommandInterpreter"/> class.
        /// </summary>
        internal TestCommandInterpreter() : base(TestCommandInterpreter.CommandKey) { }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <returns>A <see cref="CommandResult"/> instance with result information</returns>
        protected override CommandResult Handle()
        {
            return new CommandResult(true, A.Dummy<string>());
        }
    }
}
