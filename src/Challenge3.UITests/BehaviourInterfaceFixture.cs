

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

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize()]
        public void Initialize()
        {
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }

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
        public void HandleUserNeedsLogsFailures()
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
    }
}
