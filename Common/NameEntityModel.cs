using System;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EntityModel
{
    public partial class NameEntityModel : OpenAccessContext, IFluentModelUnitOfWork
    {
        private static string connectionStringName = @"DBConnectionString";

        private static BackendConfiguration backend = GetBackendConfiguration();

        private static MetadataSource metadataSource = new OpenAccessMetadataSource();


        public NameEntityModel() : base(connectionStringName, backend, metadataSource)
        {
            //dbContext.ContextOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            //dbContext.ContextOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            //dbContext.ContextOptions.IsolationLevel = IsolationLevel.Snapshot;
            //this.ContextOptions.IsolationLevel = IsolationLevel.Snapshot;
        }

        /// <summary>
        /// Create new EntityModel
        /// </summary>
        /// <param name="connection">Connection string</param>
        /// <param name="backendProvider">Data base provider</param>
        public NameEntityModel(string connection, BackendProvider backendProvider) : base(connection, backend = GetBackendConfiguration(backendProvider), metadataSource)
        {
            connectionStringName = connection;
            backend = GetBackendConfiguration(backendProvider);
        }

        public NameEntityModel(string connection) : base(connection, backend, metadataSource)
        {
        }

        public NameEntityModel(BackendConfiguration backendConfiguration) : base(connectionStringName, backendConfiguration, metadataSource)
        {
        }

        public NameEntityModel(string connection, MetadataSource metadataSource) : base(connection, backend, metadataSource)
        {
        }

        public NameEntityModel(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource) : base(connection, backendConfiguration, metadataSource)
        {
        }

        public static BackendConfiguration GetBackendConfiguration(BackendProvider backendProvider = null)
        {
            if (backendProvider != null)
            {
                var backend = new BackendConfiguration();
                backend.Backend = backendProvider.BackendString;
                backend.ProviderName = backendProvider.ProviderName;
                return backend;
            }
            return null;
        }

        public void UpdateDataBaseSchema()
        {
            ISchemaHandler handler = this.GetSchemaHandler();
            string script = null;
            try
            {
                script = handler.CreateUpdateDDLScript(null);
            }
            catch
            {
                bool throwException = false;
                try
                {
                    handler.CreateDatabase();
                    script = handler.CreateDDLScript();
                }
                catch
                {
                    throwException = true;
                }
                if (throwException)
                {
                    throw;
                }
            }

            if (string.IsNullOrEmpty(script) == false)
            {
                handler.ExecuteDDLScript(script);
            }
        }

        public void Update<T>(T model) where T : IPrimaryKey
        {
            var dbItem = this.GetAll<T>().Where(w => w.Id == model.Id).FirstOrDefault();
            if (dbItem != null)
            {
                foreach (var prop in model.GetType().GetProperties())
                {
                    var val = prop.GetValue(model, null);
                    prop.SetValue(dbItem, val);
                }
            }

        }

        public void UpdateAndSave<T>(T model) where T : IPrimaryKey
        {
            var dbItem = this.GetAll<T>().Where(w => w.Id == model.Id).FirstOrDefault();
            if (dbItem != null)
            {
                foreach (var prop in model.GetType().GetProperties())
                {
                    var val = prop.GetValue(model, null);
                    prop.SetValue(dbItem, val);
                }
                
                this.SaveChanges();
            }
        }

        public Task UpdateAndSaveAsync<T>(T model) where T : IPrimaryKey
        {
            return Task.Factory.StartNew(() =>
            {
                this.UpdateAndSave<T>(model);
            });
        }

        public Task SaveChangesAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                this.SaveChanges();
            });
        }

        public Task UpdateDataBaseSchemaAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                UpdateDataBaseSchema();
            });
        }

        

    }
}