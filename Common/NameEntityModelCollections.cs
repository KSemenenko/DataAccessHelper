using System;
using System.Linq;
using Telerik.OpenAccess;

namespace EntityModel
{
    public partial class NameEntityModel : OpenAccessContext, IFluentModelUnitOfWork
    {
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