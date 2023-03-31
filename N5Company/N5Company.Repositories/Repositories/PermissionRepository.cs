using N5Company.Entities.Models;
using N5Company.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Repositories.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context) { }
    }
}
