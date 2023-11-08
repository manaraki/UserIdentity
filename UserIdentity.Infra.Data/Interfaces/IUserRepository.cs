using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities;
using UserIdentity.Domain.Entities.User;

namespace UserIdentity.Infra.Data.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {               
        
    }
}
