using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public interface IRepositoryBase<TEntity>: IRepositoryV1<TEntity>
    {
        IEnumerable<TEntity> GetAll();
    }
}
