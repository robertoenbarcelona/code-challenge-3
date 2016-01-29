
namespace SCA.Infrastructure.Log.Test
{
    using Castle.Windsor;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class Log4NetInstallerFixture
    {
        private IWindsorContainer container;

        [TestMethod]
        public void Log4NetInstaller_RegisterType()
        {
            container = new WindsorContainer();
            var sut = new SCA.Infrastructure.Log.Installer.Log4NetInstaller();
            container.Install(sut);
            container.Resolve<ILoggerFactory>().Should().BeOfType<Impl.LogFactory>();
        }
    }
}
