using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataAccess.CRUDInterfaces
{
    public interface ICanAddEntity<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
    }
}
