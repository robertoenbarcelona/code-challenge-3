
namespace Infrastructure.Core.Exceptions.Test
{
    using FakeItEasy;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DistribuitedExceptionFixture
    {
        [TestMethod]
        public void DistribuitedException_RetrieveRuntimeInfo()
        {
            var sut = new DistribuitedException();
            sut.MachineName.Should().Be(Environment.MachineName);
            sut.AppDomainName.Should().Be(AppDomain.CurrentDomain.FriendlyName);
            sut.ThreadIdentityName.Should().Be(Thread.CurrentPrincipal.Identity.Name);
            sut.WindowsIdentityName.Should().Be(WindowsIdentity.GetCurrent().Name);
        }

        [TestMethod]
        public void DistribuitedException_GetRuntimeInfo()
        {
            var sut = new DistribuitedException();
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .Append("Infrastructure.Core.Exceptions.DistribuitedException: Exception of type 'Infrastructure.Core.Exceptions.DistribuitedException' was thrown.")
                .ToString();
            sut.ToString().Should().Be(expected);
        }

        [TestMethod]
        public void DistribuitedException_SerializeInfo()
        {
            var sut = new DistribuitedException();
            var doc = new SerializationInfo(typeof(DistribuitedException), new FormatterConverter());
            sut.GetObjectData(doc, new StreamingContext());
            doc.GetValue("machineName", typeof(string)).Should().Be(Environment.MachineName);
            doc.GetValue("appDomainName", typeof(string)).Should().Be(AppDomain.CurrentDomain.FriendlyName);
            doc.GetValue("threadIdentity", typeof(string)).Should().Be(Thread.CurrentPrincipal.Identity.Name);
            doc.GetValue("windowsIdentity", typeof(string)).Should().Be(WindowsIdentity.GetCurrent().Name);
        }

        [TestMethod]
        public void DistribuitedException_WithMessage()
        {
            var sut = new DistribuitedException("Test");
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .Append("Infrastructure.Core.Exceptions.DistribuitedException: Test")
                .ToString();
            sut.ToString().Should().Be(expected);
        }

        [TestMethod]
        public void DistribuitedException_WithMessageAndException()
        {
            var doc = new Exception();
            var sut = new DistribuitedException("Test", doc);
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .Append("Infrastructure.Core.Exceptions.DistribuitedException: Test ---> System.Exception: Exception of type 'System.Exception' was thrown.\r\n   --- End of inner exception stack trace ---")
                .ToString();
            sut.ToString().Should().Be(expected);
        }
    }
}
