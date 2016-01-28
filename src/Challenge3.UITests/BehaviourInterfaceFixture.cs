

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
    public class BehaviourInterfaceFixture
    {
        /// <summary>
        /// Handling user needs resists input failures.
        /// </summary>
        [TestMethod]
        public void HandleUserNeedsResistsInputFailures()
        {
            //Arrange
            var spy = A.Fake<IInputOutputDriver>();
            A.CallTo(() => spy.Input()).Throws<Exception>();
            var doc = A.Fake<BaseCommandInterpreter>();
            BehaviourInterface sut = new BehaviourInterface(doc, spy);

            //Act
            var keepRun = sut.HandleUserNeeds();

            //Assert
            keepRun.Should().BeTrue();
        }

        /// <summary>
        /// Handling user needs resists input failures.
        /// </summary>
        [TestMethod]
        public void HandleUserNeedsLogsInputFailures()
        {
            //Arrange
            var spy = A.Fake<IInputOutputDriver>();
            var expectedMessage = String.Format(UI.Properties.Resources.Error, "Test");
            A.CallTo(() => spy.Input()).Throws(new Exception("Test"));
            var doc = A.Fake<BaseCommandInterpreter>();
            BehaviourInterface sut = new BehaviourInterface(doc, spy);

            //Act
            var dummy = sut.HandleUserNeeds();

            //Assert
            A.CallTo(() => spy.Output(expectedMessage)).MustHaveHappened(Repeated.Exactly.Once);
        }

        /// <summary>
        /// Handling user needs call chain resolution.
        /// </summary>
        [TestMethod]
        public void HandleUserNeedCallChainResolution()
        {
            //Arrange
            var spy = A.Fake<IInputOutputDriver>();
            A.CallTo(() => spy.Input()).Returns(A.Dummy<string>());
            var doc = A.Fake<BaseCommandInterpreter>();
            BehaviourInterface sut = new BehaviourInterface(doc, spy);

            //Act
            var dummy = sut.HandleUserNeeds();

            //Assert
            A.CallTo(() => doc.HandleCommand(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
