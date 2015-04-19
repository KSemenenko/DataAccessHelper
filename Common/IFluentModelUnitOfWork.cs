using System;
using System.Linq;
using Telerik.OpenAccess;

namespace EntityModel
{
    public interface IFluentModelUnitOfWork : IUnitOfWork
    {
        IQueryable<CLASSTYPE> PROPERTYNAME { get; }
    }
}