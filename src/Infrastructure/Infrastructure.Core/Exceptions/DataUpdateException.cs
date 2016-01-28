
namespace Infrastructure.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Data update exception
    /// </summary>
    [Serializable]
    public class DataUpdateException : DistribuitedException
    {
        private readonly FailType reason;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataUpdateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="reason">The reason.</param>
        public DataUpdateException(string message, Exception ex, FailType reason)
            : base(message, ex)
        {
            this.reason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataUpdateException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        /// <param name="reason">The reason.</param>
        protected DataUpdateException(SerializationInfo info, StreamingContext context, FailType reason)
            : base(info, context)
        {
            this.reason = (FailType)info.GetInt32("reason");
        }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        public FailType Reason { get { return this.reason; } }

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
            info.AddValue("reason", (object)this.reason, typeof(int));
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Gets the description of inherited fields.
        /// </summary>
        /// <param name="desc">The desc.</param>
        protected override void GetDescription(System.Text.StringBuilder desc)
        {
            desc.AppendLine(string.Format("Reason = {0}", this.reason.ToString()));
        }

#pragma warning disable 1591

        /// <summary>
        /// Reason for fail update
        /// </summary>
        public enum FailType
        {
            InvalidOperation,
            ObjectDisposed,
            Validation,
            Concurrency,
            Update
        }

#pragma warning restore 1591

    }
}
