

namespace Challenge3.UITests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;
    using Challenge3.UI;
    using FakeItEasy;
    using Challenge3.AppService;
    using Challenge3.UI.Commands;
    using FluentAssertions;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RegisterProductCommandInterpreterFixture
    {
        [TestMethod]
        public void RegisterProductCommandInterpreter_HandleHire()
        {
            //Arrange
            var driver = A.Fake<IInputOutputDriver>();
            var productService = A.Fake<IAppProductService>();
            var expectedMessage = "Expected";
            A.CallTo(() => driver.Input()).Returns(A.Dummy<string>());
            A.CallTo(() => productService.Register(A<string>.Ignored, A<string>.Ignored)).Returns(new OperationMessage() { Succeed = true, Message = expectedMessage });
            var sut = new RegisterProductCommandInterpreter(driver, productService);

            //Act
            var res = sut.HandleCommand(Challenge3.UI.Commands.Constants.RegisterProductKey);

            //Assert
            res.HasSucceed.Should().BeTrue();
            res.IsTerminating.Should().BeFalse();
            res.Message.Should().Be(expectedMessage);
        }

        [TestMethod]
        public void RegisterProductCommandInterpreter_NotHandleAnotherKey()
        {
            //Arrange
            var driver = A.Fake<IInputOutputDriver>();
            var productService = A.Fake<IAppProductService>();
            A.CallTo(() => driver.Input()).Returns(A.Dummy<string>());
            A.CallTo(() => productService.Register(A<string>.Ignored, A<string>.Ignored)).Returns(new OperationMessage() { Succeed = true, Message = A.Dummy<string>() });
            var sut = new RegisterProductCommandInterpreter(driver, productService);

            //Act
            var res = sut.HandleCommand(Constants.AnotherKey);

            //Assert
            res.HasSucceed.Should().BeFalse();
            res.IsTerminating.Should().BeFalse();
            res.Message.Should().Be(UI.Properties.Resources.CommandUnrecognized);
        }

        [TestMethod]
        public void RegisterProductCommandInterpreter_ResistAppServiceFailure()
        {
            //Arrange
            var driver = A.Fake<IInputOutputDriver>();
            var productService = A.Fake<IAppProductService>();
            var expectedMessage = "Expected";
            A.CallTo(() => driver.Input()).Returns(A.Dummy<string>());
            A.CallTo(() => productService.Register(A<string>.Ignored, A<string>.Ignored)).Throws(new Exception(expectedMessage));
            var sut = new RegisterProductCommandInterpreter(driver, productService);

            //Act
            var res = sut.HandleCommand(Challenge3.UI.Commands.Constants.RegisterProductKey);

            //Assert
            res.HasSucceed.Should().BeFalse();
            res.IsTerminating.Should().BeFalse();
            res.Message.Should().Be(expectedMessage);
        }
    }
}
