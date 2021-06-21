using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    public interface IRepository<Entitiy, Key> where Entitiy : class
    {

        IEnumerable<Entitiy> Get();
        Entitiy Get(Key key);
        int insert(Entitiy entitiy);
        int update(Entitiy entity, Key key);
        int delete(Key key);

    }
}
