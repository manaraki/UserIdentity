using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Infra.Data.Contexts;
using UserIdentity.Infra.Data.Interfaces;

namespace UserIdentity.Infra.Data.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }

}

