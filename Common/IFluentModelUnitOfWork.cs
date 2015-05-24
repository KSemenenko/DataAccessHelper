using System;
using System.Linq;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace EntityModel
{
    public interface IFluentModelUnitOfWork : IUnitOfWork
    {
        IQueryable<CLASSTYPE> PROPERTYNAME { get; }
        Task<IQueryable<CLASSTYPE>> PROPERTYNAMEAsync { get; }
    }
}