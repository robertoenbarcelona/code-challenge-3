
namespace Infrastructure.Core.Exceptions.Test
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DataUpdateExceptionFixture
    {
        [TestMethod]
        public void DataUpdateException_RetrieveRuntimeInfo()
        {
            var sut = new DataUpdateException("fake", null, DataUpdateException.FailType.Concurrency);
            sut.MachineName.Should().Be(Environment.MachineName);
            sut.AppDomainName.Should().Be(AppDomain.CurrentDomain.FriendlyName);
            sut.ThreadIdentityName.Should().Be(Thread.CurrentPrincipal.Identity.Name);
            sut.WindowsIdentityName.Should().Be(WindowsIdentity.GetCurrent().Name);
            sut.Reason.Should().Be(DataUpdateException.FailType.Concurrency);
        }

        [TestMethod]
        public void DataUpdateException_GetRuntimeInfo()
        {
            var sut = new DataUpdateException("dummy", null, DataUpdateException.FailType.Concurrency);
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .AppendLine(string.Format("Reason = {0}", DataUpdateException.FailType.Concurrency.ToString()))
                .Append("Infrastructure.Core.Exceptions.DataUpdateException: dummy")
                .ToString();
            sut.ToString().Should().Be(expected);
        }
        [TestMethod]
        public void DataUpdateException_SerializeInfo()
        {
            var sut = new DataUpdateException("dummy", null, DataUpdateException.FailType.Concurrency);
            var doc = new SerializationInfo(typeof(DistribuitedException), new FormatterConverter());
            sut.GetObjectData(doc, new StreamingContext());
            doc.GetValue("machineName", typeof(string)).Should().Be(Environment.MachineName);
            doc.GetValue("appDomainName", typeof(string)).Should().Be(AppDomain.CurrentDomain.FriendlyName);
            doc.GetValue("threadIdentity", typeof(string)).Should().Be(Thread.CurrentPrincipal.Identity.Name);
            doc.GetValue("windowsIdentity", typeof(string)).Should().Be(WindowsIdentity.GetCurrent().Name);
            doc.GetValue("reason", typeof(int)).Should().Be(DataUpdateException.FailType.Concurrency);
        }

        [TestMethod]
        public void DataUpdateException_WithMessage()
        {
            var sut = new DataUpdateException("dummy", null, DataUpdateException.FailType.Concurrency);
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .AppendLine(string.Format("Reason = {0}", DataUpdateException.FailType.Concurrency.ToString()))
                .Append("Infrastructure.Core.Exceptions.DataUpdateException: dummy")
                .ToString();
            sut.ToString().Should().Be(expected);
        }

        [TestMethod]
        public void DataUpdateException_WithMessageAndException()
        {
            var doc = new Exception();
            var sut = new DataUpdateException("dummy", doc, DataUpdateException.FailType.Concurrency);
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .AppendLine(string.Format("Reason = {0}", DataUpdateException.FailType.Concurrency.ToString()))
                .Append("Infrastructure.Core.Exceptions.DataUpdateException: dummy ---> System.Exception: Exception of type 'System.Exception' was thrown.\r\n   --- End of inner exception stack trace ---")
                .ToString();
            sut.ToString().Should().Be(expected);
        }
    }
}
