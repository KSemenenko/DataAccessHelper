using System;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using System.Reflection;

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
        /// Создать новое подключение к базе данных
        /// </summary>
        /// <param name="connection">Строка подключения</param>
        /// <param name="backendProvider">Провайдер с параметрами подключения</param>
        public NameEntityModel(string connection, BackendProvider backendProvider) : base(connection, backend = GetBackendConfiguration(backendProvider), metadataSource)
        {
            //чтоб наверняка
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

            this.SaveChanges();
        }

        public void UpdateBatch<T>(T model) where T : IPrimaryKey
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

        public void UpdateSchema()
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



        #region IFluentModelUnitOfWork
        public IQueryable<CLASSTYPE> PROPERTYNAME
        {
            get
            {
                return this.GetAll<CLASSTYPE>();
            }
        }

        #endregion


    }
}