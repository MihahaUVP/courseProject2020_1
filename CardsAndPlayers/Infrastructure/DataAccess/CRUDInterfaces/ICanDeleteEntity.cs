using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataAccess.CRUDInterfaces
{
    public interface ICanDeleteEntity<TEntity> where TEntity : class
    {
        void Remove(TEntity entity);
    }
}
