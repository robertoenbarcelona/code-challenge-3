
namespace Infrastructure.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class NoDataFoundException : DistribuitedException
    {
        private string searchCriteria = string.Empty;
        private string entityType;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoDataFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="criteria">The search criteria.</param>
        /// <param name="entityType">The entity type.</param>
        public NoDataFoundException(string message, string criteria, string entityType)
            : base(message)
        {
            this.searchCriteria = criteria;
            this.entityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoDataFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="criteria">The search criteria.</param>
        /// <param name="entityType">The entity type.</param>
        public NoDataFoundException(string message, Exception ex, string criteria, string entityType)
            : base(message, ex)
        {
            this.searchCriteria = criteria;
            this.entityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoDataFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        private NoDataFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.searchCriteria = info.GetString("searchCriteria");
            this.entityType = info.GetString("entityType");
        }

        /// <summary>
        /// Gets the search criteria.
        /// </summary>
        public string SearchCriteria { get { return this.searchCriteria; } }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        public string EntityType { get { return this.entityType; } }

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
            info.AddValue("searchCriteria", (object)this.searchCriteria, typeof(string));
            info.AddValue("entityType", (object)this.entityType);
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Gets the description of inherited fields.
        /// </summary>
        /// <param name="desc">The desc.</param>
        protected override void GetDescription(System.Text.StringBuilder desc)
        {
            desc.AppendLine(string.Format("EntityType = {0}", this.entityType));
            desc.AppendLine(string.Format("SearchCriteria = {0}", this.searchCriteria));
            base.GetDescription(desc);
        }
    }
}
