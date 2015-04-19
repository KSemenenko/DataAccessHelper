using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel
{
    public static class BackendHelper
    {
        static BackendHelper()
        {
            BackendNames = new List<string>();
            BackendNames.Add("Microsoft SQL Server");
            BackendNames.Add("Oracle");
            BackendNames.Add("MySql");
            //BackendNames.Add("Advantage Database Server");
            BackendNames.Add("Firebird");
            BackendNames.Add("Microsoft SQL Azure");
            BackendNames.Add("Microsoft SQL Server Compact");
            //BackendNames.Add("VistaDB");
            BackendNames.Add("SQLite");
            BackendNames.Add("PostgreSQL");

            Providers = new Dictionary<string, BackendProvider>();
            Providers.Add("Microsoft SQL Server", new BackendProvider() { BackendString = "MsSql", ProviderName = "System.Data.SqlClient" });
            Providers.Add("Oracle", new BackendProvider() { BackendString = "Oracle", ProviderName = "Oracle.DataAccess.Client" });
            Providers.Add("MySql", new BackendProvider() { BackendString = "MySql", ProviderName = "MySql.Data.MySqlClient" });
            //Providers.Add("Advantage Database Server", new BackendProvider() { BackendString = "Ads", ProviderName = "Advantage.Data.Provider" });
            Providers.Add("Firebird", new BackendProvider() { BackendString = "Firebird", ProviderName = "FirebirdSql.Data.FirebirdClient" });
            Providers.Add("Microsoft SQL Azure", new BackendProvider() { BackendString = "Azure", ProviderName = "System.Data.SqlClient" });
            Providers.Add("Microsoft SQL Server Compact", new BackendProvider() { BackendString = "SqlCe", ProviderName = "Microsoft.SqlServerCe.Client.4.0" });
            //Providers.Add("VistaDB", new BackendProvider() { BackendString = "VistaDb", ProviderName = "System.Data.VistaDB" });
            Providers.Add("SQLite", new BackendProvider() { BackendString = "SQLite", ProviderName = "System.Data.SQLite" });
            Providers.Add("PostgreSQL", new BackendProvider() { BackendString = "PostgreSql", ProviderName = "Npgsql" });
        }
        
        public static Dictionary<string, BackendProvider> Providers { get; set; }

        public static List<string> BackendNames { get; set; }

        /// <summary>
        /// Get DB Provider by Name
        /// </summary>
        /// <param name="dataBase"></param>
        /// <returns></returns>
        public static BackendProvider GetProvider(string dataBase)
        {
            try
            {
                return Providers[dataBase];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       
        
    }
}