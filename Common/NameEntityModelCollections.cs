using System;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<IQueryable<CLASSTYPE>> PROPERTYNAMEAsync
        {
            get
            {
                return System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    return this.GetAll<CLASSTYPE>();
                });
            }
        }

        #endregion
    }
}