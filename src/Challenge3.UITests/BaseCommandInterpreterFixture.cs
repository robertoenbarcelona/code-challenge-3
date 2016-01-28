
namespace Challenge3.UITests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Challenge3.UI;
    using FakeItEasy;
    using FluentAssertions;

    /// <summary>
    /// Handle all ConsoleInterface test
    /// </summary>
    [TestClass]
    public class BaseCommandInterpreterFixture
    {
        /// <summary>
        /// Setting the succesor put the next evaluator in chain.
        /// </summary>
        [TestMethod]
        public void SettingSuccesorPutTheNextEvaluator()
        {
            //Arrange
            var sut = A.Fake<BaseCommandInterpreter>();
            var doc = A.Fake<BaseCommandInterpreter>();

            //Act
            sut.SetSuccessor(doc);

            //Assert
            sut.Successor.Should().Be(doc);
        }

        /// <summary>
        /// A Command can interpret his own opertion.
        /// </summary>
        public void CommandInterpretsHisOwnOpertion()
        {
            //Arrange
            var sut = new TestCommandInterpreter();

            //Act
            var res = sut.HandleCommand(Constants.TestKey);

            //Assert
            res.HasSucceed.Should().BeTrue();
            res.IsTerminating.Should().BeFalse();
        }

        /// <summary>
        /// Chains calls next evaluator.
        /// </summary>
        [TestMethod]
        public void ChainCallsNextEvaluator()
        {
            //Arrange
            var doc = A.Fake<BaseCommandInterpreter>();
            var sut = new TestCommandInterpreter();
            sut.SetSuccessor(doc);
            var anotherValue = Constants.AnotherKey;

            //Act
            var dummy = sut.HandleCommand(anotherValue);

            //Assert
            A.CallTo(() => sut.Successor.HandleCommand(anotherValue)).MustHaveHappened(Repeated.Exactly.Once);
        }

        /// <summary>
        /// Chains handles unknown command.
        /// </summary>
        public void ChainHandlesUnknownCommand()
        {
            //Arrange
            var sut = new TestCommandInterpreter();
            var anotherValue = Constants.AnotherKey;

            //Act
            var res = sut.HandleCommand(anotherValue);

            //Assert
            res.HasSucceed.Should().BeFalse();
            res.IsTerminating.Should().BeFalse();
            res.Message.Should().BeEquivalentTo(UI.Properties.Resources.CommandUnrecognized);
        }

    }
}
