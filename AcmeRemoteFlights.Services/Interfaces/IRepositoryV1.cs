using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public interface IRepositoryV1<TEntity>
    {
        int Add(TEntity entity);

        TEntity Find(int Id);
    }
}
