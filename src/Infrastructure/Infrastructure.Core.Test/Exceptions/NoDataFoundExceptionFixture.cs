
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
    public class NoDataFoundExceptionFixture
    {
        [TestMethod]
        public void NoDataFoundException_RetrieveRuntimeInfo()
        {
            var sut = new NoDataFoundException("dummy", "dummyCrit", "dummyEnt");
            sut.MachineName.Should().Be(Environment.MachineName);
            sut.AppDomainName.Should().Be(AppDomain.CurrentDomain.FriendlyName);
            sut.ThreadIdentityName.Should().Be(Thread.CurrentPrincipal.Identity.Name);
            sut.WindowsIdentityName.Should().Be(WindowsIdentity.GetCurrent().Name);
            sut.SearchCriteria.Should().Be("dummyCrit");
            sut.EntityType.Should().Be("dummyEnt");
        }

        [TestMethod]
        public void NoDataFoundException_GetRuntimeInfo()
        {
            var sut = new NoDataFoundException("dummy", "dummyCrit", "dummyEnt");
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .AppendLine(string.Format("EntityType = {0}", "dummyEnt"))
                .AppendLine(string.Format("SearchCriteria = {0}", "dummyCrit"))
                .Append("Infrastructure.Core.Exceptions.NoDataFoundException: dummy")
                .ToString();
            sut.ToString().Should().Be(expected);
        }

        [TestMethod]
        public void NoDataFoundException_SerializeInfo()
        {
            var sut = new NoDataFoundException("dummy", "dummyCrit", "dummyEnt");
            var doc = new SerializationInfo(typeof(DistribuitedException), new FormatterConverter());
            sut.GetObjectData(doc, new StreamingContext());
            doc.GetValue("machineName", typeof(string)).Should().Be(Environment.MachineName);
            doc.GetValue("appDomainName", typeof(string)).Should().Be(AppDomain.CurrentDomain.FriendlyName);
            doc.GetValue("threadIdentity", typeof(string)).Should().Be(Thread.CurrentPrincipal.Identity.Name);
            doc.GetValue("windowsIdentity", typeof(string)).Should().Be(WindowsIdentity.GetCurrent().Name);
            doc.GetValue("entityType", typeof(string)).Should().Be("dummyEnt");
            doc.GetValue("searchCriteria", typeof(string)).Should().Be("dummyCrit");
        }

        [TestMethod]
        public void NoDataFoundException_WithMessage()
        {
            var sut = new NoDataFoundException("dummy", "dummyCrit", "dummyEnt");
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .AppendLine(string.Format("EntityType = {0}", "dummyEnt"))
                .AppendLine(string.Format("SearchCriteria = {0}", "dummyCrit"))
                .Append("Infrastructure.Core.Exceptions.NoDataFoundException: dummy")
                .ToString();
            sut.ToString().Should().Be(expected);
        }

        [TestMethod]
        public void NoDataFoundException_WithMessageAndException()
        {
            var doc = new Exception();
            var sut = new NoDataFoundException("dummy", doc, "dummyCrit", "dummyEnt");
            var expected = new StringBuilder("Runtime:\r\n")
                .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
                .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
                .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
                .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name))
                .AppendLine(string.Format("EntityType = {0}", "dummyEnt"))
                .AppendLine(string.Format("SearchCriteria = {0}", "dummyCrit"))
                .Append("Infrastructure.Core.Exceptions.NoDataFoundException: dummy ---> System.Exception: Exception of type 'System.Exception' was thrown.\r\n   --- End of inner exception stack trace ---")
                .ToString();
            sut.ToString().Should().Be(expected);
        }
    }
}
