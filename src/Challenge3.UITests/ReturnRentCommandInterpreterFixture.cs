﻿
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
    public class ReturnRentCommandInterpreterFixture
    {
        [TestMethod]
        public void ReturnRentCommandInterpreter_HandleReturn()
        {
            //Arrange
            var driver = A.Fake<IInputOutputDriver>();
            var rentService = A.Fake<IAppRentService>();
            var expectedMessage = "Expected";
            A.CallTo(() => driver.Input()).Returns(A.Dummy<string>());
            A.CallTo(() => rentService.ReturnProduct(A<string>.Ignored, A<string>.Ignored, A<DateTime>.Ignored)).Returns(new OperationMessage() { Succeed = true, Message = expectedMessage });
            var sut = new ReturnRentCommandInterpreter(driver, rentService);

            //Act
            var res = sut.HandleCommand(Challenge3.UI.Commands.Constants.ReturnRentKey);

            //Assert
            res.HasSucceed.Should().BeTrue();
            res.IsTerminating.Should().BeFalse();
            res.Message.Should().Be(expectedMessage);
        }

        [TestMethod]
        public void ReturnRentCommandInterpreter_NotHandleAnotherKey()
        {
            //Arrange
            var driver = A.Fake<IInputOutputDriver>();
            var rentService = A.Fake<IAppRentService>();
            A.CallTo(() => driver.Input()).Returns(A.Dummy<string>());
            A.CallTo(() => rentService.ReturnProduct(A<string>.Ignored, A<string>.Ignored, A<DateTime>.Ignored)).Returns(new OperationMessage() { Succeed = true, Message = A.Dummy<string>() });
            var sut = new ReturnRentCommandInterpreter(driver, rentService);

            //Act
            var res = sut.HandleCommand(Constants.AnotherKey);

            //Assert
            res.HasSucceed.Should().BeFalse();
            res.IsTerminating.Should().BeFalse();
            res.Message.Should().Be(UI.Properties.Resources.CommandUnrecognized);
        }

        [TestMethod]
        public void ReturnRentCommandInterpreter_ResistAppServiceFailure()
        {
            //Arrange
            var driver = A.Fake<IInputOutputDriver>();
            var rentService = A.Fake<IAppRentService>();
            var expectedMessage = "Expected";
            A.CallTo(() => driver.Input()).Returns(A.Dummy<string>());
            A.CallTo(() => rentService.ReturnProduct(A<string>.Ignored, A<string>.Ignored, A<DateTime>.Ignored)).Throws(new Exception(expectedMessage));
            var sut = new ReturnRentCommandInterpreter(driver, rentService);

            //Act
            var res = sut.HandleCommand(Challenge3.UI.Commands.Constants.ReturnRentKey);

            //Assert
            res.HasSucceed.Should().BeFalse();
            res.IsTerminating.Should().BeFalse();
            res.Message.Should().Be(expectedMessage);
        }
    }
}
