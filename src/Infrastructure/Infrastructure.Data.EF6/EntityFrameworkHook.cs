
namespace SCA.Infrastructure.Data.Ef6
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.Entity.Core.Objects;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class EntityFrameworkHook
    {
        private readonly DbContext context;
        private readonly Action<string> funcDelegate;
        //private readonly SaveChangesHookHandler funcDelegate;

        //public delegate void SaveChangesHookHandler(IUnitOfWork uow, string command);
        //public event SaveChangesHookHandler SaveChanges;

        public event Action<string> SaveChanges;

        public EntityFrameworkHook(DbContext context, Action<string> funcDelegate)
        {

            this.context = context;
            this.funcDelegate = funcDelegate;
            this.SaveChanges += this.funcDelegate;

            var internalContext = this.context.GetType()
                                           .GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                                           .Where(p => p.Name == "InternalContext")
                                           .Select(p => p.GetValue(this.context, null))
                                           .SingleOrDefault();

            var objectContext = internalContext.GetType()
                                           .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                           .Where(p => p.Name == "ObjectContext")
                                           .Select(p => p.GetValue(internalContext, null))
                                           .SingleOrDefault();

            var saveChangesEvent = objectContext.GetType()
                                                .GetEvents(BindingFlags.Public | BindingFlags.Instance)
                                                .SingleOrDefault(e => e.Name == "SavingChanges");


            var handler = Delegate.CreateDelegate(saveChangesEvent.EventHandlerType, this, "OnSaveChanges");
            saveChangesEvent.AddEventHandler(objectContext, handler);
        }

        private void OnSaveChanges(object sender, EventArgs e)
        {
            ////var commandText = new StringBuilder();
            ////var conn = sender.GetType()
            ////                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            ////                 .Where(p => p.Name == "Connection")
            ////                 .Select(p => p.GetValue(sender, null))
            ////                 .SingleOrDefault();
            ////var entityConn = (EntityConnection)conn;
            ////var objStateManager = (ObjectStateManager)sender.GetType()
            ////      .GetProperty("ObjectStateManager", BindingFlags.Instance | BindingFlags.Public)
            ////      .GetValue(sender, null);
            ////var workspace = entityConn.GetMetadataWorkspace();
            var translatorT = sender.GetType().Assembly.GetType("System.Data.Entity.Core.Mapping.Update.Internal.UpdateTranslator");
            var method = translatorT.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, null, new Type[] { }, null); 
            //var translator = Activator.CreateInstance(translatorT, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { objStateManager, workspace, entityConn, entityConn.ConnectionTimeout }, CultureInfo.InvariantCulture);
            var translator = method.Invoke(new object[] { });
            var produceCommands = translator.GetType().GetMethod("ProduceCommands", BindingFlags.NonPublic | BindingFlags.Instance);
            var commands = (IEnumerable<object>)produceCommands.Invoke(translator, null);
            foreach (var cmd in commands)
            {
                var identifierValues = new Dictionary<int, object>();
                var dcmd =
                    (DbCommand)cmd.GetType()
                       .GetMethod("CreateCommand", BindingFlags.Instance | BindingFlags.NonPublic)
                       .Invoke(cmd, new[] { translator, identifierValues });

                foreach (DbParameter param in dcmd.Parameters)
                {
                    var sqlParam = (SqlParameter)param;

                    commandText.AppendLine(String.Format("declare {0} {1} {2}",
                                                            sqlParam.ParameterName,
                                                            sqlParam.SqlDbType.ToString().ToLower(),
                                                            sqlParam.Size > 0 ? "(" + sqlParam.Size + ")" : ""));
                    commandText.AppendLine(String.Format("set {0} = '{1}'", sqlParam.ParameterName, sqlParam.SqlValue));
                }

                commandText.AppendLine();
                commandText.AppendLine(dcmd.CommandText);
                commandText.AppendLine("go");
                commandText.AppendLine();
            }

            //if (this.SaveChanges != null) { this.SaveChanges.Invoke(this.context, commandText.ToString()); }
            if (this.SaveChanges != null) { this.SaveChanges.Invoke(commandText.ToString()); }
        }
    }
}
