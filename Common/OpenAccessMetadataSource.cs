using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace EntityModel
{
    public class OpenAccessMetadataSource : FluentMetadataSource
    {
        protected override void SetContainerSettings(MetadataContainer container)
        {
            container.Name = "EntityModel";
            container.DefaultNamespace = "EntityModel";
            container.NameGenerator.RemoveCamelCase = false;
            container.NameGenerator.SourceStrategy = Telerik.OpenAccess.Metadata.NamingSourceStrategy.Property;
        }

        protected override IList<MappingConfiguration> PrepareMapping()
        {
            // Getting Started with the Fluent Mapping API
            //http://www.telerik.com/help/openaccess-orm/fluent-mapping-overview.html
            //http://docs.telerik.com/data-access/developers-guide/code-only-mapping/mapping-clr-types-properties-and-associations/mapping-associations/fluent-mapping-mapping-clr-mapping-associations-one-to-one
            //http://docs.telerik.com/data-access/developers-guide/code-only-mapping/mapping-clr-types-properties-and-associations/advanced-mapping/backend-independent-mapping/fluent-mapping-mapping-clr-advanced-backend-independent-string-properties
            //configuration.HasProperty(x => x.Body).WithInfiniteLength(); // string lenght
            
            //index
            //http://docs.telerik.com/data-access/developers-guide/code-only-mapping/mapping-clr-types-properties-and-associations/advanced-mapping/fluent-mapping-mapping-clr-advanced-defining-indexes
            
            
            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            configurations.Add(this.GetTable());


            return configurations;
        }

        private MappingConfiguration<CLASSNAME> GetTable()
        {
            MappingConfiguration<Position> configuration = new MappingConfiguration<CLASSNAME>();
            configuration.MapType()
                         .Inheritance(Telerik.OpenAccess.InheritanceStrategy.Flat)
                         .WithConcurencyControl(OptimisticConcurrencyControlStrategy.Version);
            configuration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            //configuration.HasIndex(x => new {
            //   x.Price,
            //   x.ProductName
            //}).IsUnique()
            //.IsClustered()
            return configuration;
        }

        
    }
}