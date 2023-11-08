using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Infra.Data.Contexts;
using UserIdentity.Infra.Data.Interfaces;

namespace UserIdentity.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {        
        public UserRepository(DataContext dataContext):base(dataContext) 
        {
            
        }

        
    }
}
