
namespace Infrastructure.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Base class exception in a distribuited environment.
    /// </summary>
    [Serializable]
    public class DistribuitedException : Exception
    {
        private const string NotAvailable = "Not Available";
        private string machineName;
        private string appDomainName;
        private string threadIdentity;
        private string windowsIdentity;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistribuitedException"/> class.
        /// </summary>
        public DistribuitedException()
            : base()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistribuitedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DistribuitedException(string message)
            : base(message)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistribuitedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public DistribuitedException(string message, Exception inner)
            : base(message, inner)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistribuitedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected DistribuitedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.machineName = info.GetString("machineName");
            this.appDomainName = info.GetString("appDomainName");
            this.threadIdentity = info.GetString("threadIdentity");
            this.windowsIdentity = info.GetString("windowsIdentity");
        }

        /// <summary>
        /// Gets the name of the machine.
        /// </summary>
        public string MachineName { get { return this.machineName; } }

        /// <summary>
        /// Gets the name of the application domain.
        /// </summary>
        public string AppDomainName { get { return this.appDomainName; } }

        /// <summary>
        /// Gets the name of the thread identity.
        /// </summary>
        public string ThreadIdentityName { get { return this.threadIdentity; } }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("machineName", (object)this.machineName, typeof(string));
            info.AddValue("appDomainName", (object)this.appDomainName, typeof(string));
            info.AddValue("threadIdentity", (object)this.threadIdentity, typeof(string));
            info.AddValue("windowsIdentity", (object)this.windowsIdentity, typeof(string));
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Gets the name of the windows identity.
        /// </summary>
        public string WindowsIdentityName
        {
            get
            {
                return this.windowsIdentity;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override string ToString()
        {
            var desc = new StringBuilder("Runtime:\r\n")
              .AppendLine(string.Format("MachineName = {0}", Environment.MachineName))
              .AppendLine(string.Format("AppDomainName = {0}", AppDomain.CurrentDomain.FriendlyName))
              .AppendLine(string.Format("WindowsIdentityName = {0}", WindowsIdentity.GetCurrent().Name))
              .AppendLine(string.Format("ThreadIdentityName = {0}", Thread.CurrentPrincipal.Identity.Name));
            GetDescription(desc);
            return desc.ToString() + base.ToString();
        }

        /// <summary>
        /// Gets the description of inherited fields.
        /// </summary>
        /// <param name="desc">The desc.</param>
        protected virtual void GetDescription(StringBuilder desc)
        { }

        private void Initialize()
        {
            this.machineName = Environment.MachineName;
            this.appDomainName = AppDomain.CurrentDomain.FriendlyName;
            this.threadIdentity = Thread.CurrentPrincipal.Identity.Name;
            this.windowsIdentity = WindowsIdentity.GetCurrent().Name; 
        }
    }
}
