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
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        
        public PermissionRepository(DataContext dataContext) : base(dataContext)
        {
            
        }

    }
}

